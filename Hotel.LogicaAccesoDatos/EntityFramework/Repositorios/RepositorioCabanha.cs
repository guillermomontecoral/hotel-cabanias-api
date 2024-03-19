using LogicaNegocio.Entidades;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System;
using Hotel.LogicaNegocio.InterfacesRepositorios;
using Hotel.LogicaNegocio.ExcepcionesEntidades;

namespace Hotel.LogicaAccesoDatos.EntityFramework.RepositoriosEF
{
    public class RepositorioCabanha : IRepositorioCabanha
    {

        //private HotelContext _db = new HotelContext();

        private HotelContext _db;
        public RepositorioCabanha(HotelContext db)
        {
            _db = db;
        }

        #region Operaciones CRUD

        public void Add(Cabanha obj)
        {
            if (obj == null) throw new ArgumentNullException("Error: El tipo de cabaña no puede ser nulo");

            try
            {

                if (!_db.Cabanhas.Any())
                {
                    obj.NumHabitacion = 1;
                }
                else
                {
                    var q = _db.Cabanhas.Select(c => c.NumHabitacion).Max();
                    obj.NumHabitacion = q + 1;
                }


                //obj.Nombre = obj.Nombre.Trim();
                //obj.Nombre = TipoCabanha.UpperFirstChar(obj.Nombre);
                //obj.Descripcion = obj.Descripcion.Trim();
                //obj.Descripcion = TipoCabanha.UpperFirstChar(obj.Descripcion);
                //obj.ValidarDatos();
                // _db.Entry(obj.MisFotos[0]).State = EntityState.Added;
                obj.Update(obj);
                _db.Cabanhas.Add(obj);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void Update(Cabanha obj)
        {
            if (obj == null) throw new ArgumentNullException("Error: El tipo de cabaña no puede ser nulo");

            try
            {
                //var objOriginal = FindById(obj.Id);
                Cabanha objOriginal = _db.Cabanhas.Find(obj.Id);
                if (objOriginal == null)
                    throw new CabanhaException($"No existe una cabaña con el ID: {obj.Id}");

                //objOriginal.Nombre = obj.Nombre;
                //objOriginal.IdTipoCabanha = obj.IdTipoCabanha;
                //obj.Descripcion = obj.Descripcion;
                //objOriginal.Jacuzzi = obj.Jacuzzi;
                //objOriginal.HabilitadaParaReservas = obj.HabilitadaParaReservas;
                //objOriginal.NumHabitacion = obj.NumHabitacion;
                //objOriginal.CantMaxPersonas = obj.CantMaxPersonas;
                obj.NumHabitacion = objOriginal.NumHabitacion;
                objOriginal.Update(obj);
                _db.SaveChanges();
            }
            catch (CabanhaException ex)
            {
                throw;
            }
            catch (DbUpdateException ex)
            {
                throw new DbUpdateException(ex.InnerException.Message);
            }
            catch (Exception ex)
            {
                //throw new Exception($"No se pudo dar de alta el tipo de cabanha.");
                throw new Exception(ex.Message);
            }
        }

        public Cabanha FindById(int? id)
        {
            try
            {
                var cabanha = _db.Cabanhas
                    .Include(t => t.TipoCabanha)
                    .Where(c => c.Id == id)
                    .FirstOrDefault();

                return cabanha;
            }
            catch (Exception ex)
            {
                throw new Exception($"No se puede recuperar la cabaña con el ID: {id}");
            }
        }

        public IEnumerable<Cabanha> FindAll()
        {
            try
            {
                var cabanha = _db.Cabanhas
                                .Include(c => c.TipoCabanha)
                                .Include(c => c.MisFotos)
                                .ToList();
                return cabanha;

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void Delete(Cabanha obj)
        {
            if (obj == null) throw new ArgumentNullException("Error: La cabaña no puede ser nulo");

            try
            {
                _db.Cabanhas.Remove(obj);
                //_db.Entry(obj).State = EntityState.Deleted;
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void Delete(int? id)
        {
            try
            {
                var cabanha = FindById(id);
                Delete(cabanha);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        #endregion

        /// <see>LogicaNegocio.InterfacesRepositorios.IRepositorioCabanha#BuscarPorTextoEnNombre(string)</see>
        ///  
        public IEnumerable<Cabanha> BuscarPorTextoEnNombre(string texto)
        {
            if (string.IsNullOrEmpty(texto)) throw new ArgumentNullException("Error: La cabaña no puede ser nulo");


            try
            {

                //var cabanha = _db.Cabanhas.Where(t => t.Nombre.Contains(texto.Trim())).ToList();
                //return cabanha;

                var cabanhas = _db.Cabanhas.AsQueryable();
                if (texto != null)
                    //abanhas = cabanhas.Where(t => t.Nombre.Contains(texto.Trim()));
                    cabanhas = cabanhas.Where(t => t.Nombre.Nombre.Contains(texto.Trim()));
                return cabanhas.ToList();
            }
            catch (Exception ex)
            {
                throw;
            }

        }


        /// <see>LogicaNegocio.InterfacesRepositorios.IRepositorioCabanha#BuscarPorTipo(string)</see>
        ///  
        public IEnumerable<Cabanha> BuscarPorTipo(int? idTipo)
        {
            if (idTipo == null) throw new ArgumentNullException("Error: El tipo de cabaña no puede ser nulo");



            try
            {
                var tipoCabanha = _db.Cabanhas.Where(t => t.TipoCabanha.Id == idTipo).ToList();
                return tipoCabanha;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public IEnumerable<Cabanha> BuscarPorMaxPer(int? num)
        {
            if (num == null) throw new ArgumentNullException("Error: El numero maximo de personas no puede ser nulo");



            try
            {
                var numPersonas = _db.Cabanhas.Where(n => n.CantMaxPersonas >= num).ToList();
                return numPersonas;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public IEnumerable<Cabanha> BuscarPorHabilitada()
        {
            try
            {
                var cabanhaHabilitadas = _db.Cabanhas.Where(c => c.HabilitadaParaReservas == true).ToList();
                return cabanhaHabilitadas;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public IEnumerable<Cabanha> Consulta6_OblParteA(decimal? monto)
        {
            if (monto == null)
                throw new CabanhaException("El monto no puede ser nulo.");

            var q = _db.Cabanhas.Where(cab => cab.TipoCabanha != null && cab.TipoCabanha.CostoPorHuesped < monto
                                    && cab.HabilitadaParaReservas == true 
                                    && cab.Jacuzzi == true
                                     )
                                    .ToList();

            return q;
        }
    }

}

