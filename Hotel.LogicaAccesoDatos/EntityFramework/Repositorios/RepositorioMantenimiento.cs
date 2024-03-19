using LogicaNegocio.Entidades;
using System.Collections.Generic;
using System;
using Microsoft.EntityFrameworkCore;
using Hotel.LogicaNegocio.InterfacesRepositorios;
using Hotel.LogicaNegocio.ExcepcionesEntidades;

namespace Hotel.LogicaAccesoDatos.EntityFramework.RepositoriosEF
{
    public class RepositorioMantenimiento : IRepositorioMantenimiento
    {
        //private HotelContext _db = new HotelContext();

        private HotelContext _db;
        public RepositorioMantenimiento(HotelContext db)
        {
            _db = db;
        }

        #region Operaciones CRUD
        public void Add(Mantenimiento obj)
        {
            if (obj == null) throw new ArgumentNullException("El objeto no puede ser nulo.");

            try
            {
                obj.Update(obj);
                _db.Mantenimientos.Add(obj);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public IEnumerable<Mantenimiento> FindAll()
        {
            try
            {
                var m = _db.Mantenimientos.Include(c => c.Cabanha).ToList();
                return m;

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public IEnumerable<Mantenimiento> FindAllMantCabanhas(int? idCabanha)
        {
            try
            {
                if (idCabanha == null)
                    throw new MantenimientoException("El ide de la cabaña no puede ser nulo");

                if (_db.Cabanhas.Find(idCabanha) == null)
                    throw new MantenimientoException($"No existe una cabaña con el id: {idCabanha}");

                var m = _db.Mantenimientos.Include(m => m.Cabanha)
                                           .Where(m => m.IdCabanha == idCabanha)
                                           .OrderByDescending(m => m.CostoMantenimiento)
                                           .ToList();
                return m;

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Mantenimiento FindById(int? id)
        {
            try
            {
                var m = _db.Mantenimientos
                            .Include(t => t.Cabanha)
                            .Where(c => c.Id == id)
                            .FirstOrDefault();
                return m;
            }
            catch (Exception ex)
            {
                throw new Exception($"No se puede recuperar mantenimiento con el ID: {id}");
            }
        }

        public void Update(Mantenimiento obj)
        {
            if (obj == null) throw new ArgumentNullException("Error: El tipo de cabaña no puede ser nulo");

            var objOriginal = FindById(obj.Id);

            try
            {
                if (objOriginal != null)
                {
                    objOriginal.Fecha = obj.Fecha;
                    objOriginal.Descripcion = obj.Descripcion;
                    objOriginal.CostoMantenimiento = obj.CostoMantenimiento;
                    objOriginal.RealizadoPor = obj.RealizadoPor;

                    objOriginal.ValidarDatos();
                    _db.Entry(objOriginal).State = EntityState.Modified;
                    _db.SaveChanges();
                }
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
                var m = FindById(id);
                Delete(m);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void Delete(Mantenimiento obj)
        {
            if (obj == null) throw new ArgumentNullException("Error: El tipo de cabaña no puede ser nulo");

            try
            {
                _db.Mantenimientos.Remove(obj);
                _db.Entry(obj).State = EntityState.Deleted;
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion

        /// <see>LogicaNegocio.InterfacesRepositorios.IRepositorioMantenimiento#MostrarMantenimientosEntreFechas(System.DateTime, System.DateTime)</see>
        ///  
        //public IEnumerable<Mantenimiento> MostrarMantenimientosEntreFechas(DateTime? fecha1, DateTime? fecha2)
        //{
        //    if (fecha1 == null || fecha2 == null)
        //        throw new ArgumentNullException("Las fechas no puede ser nulas.");

        //    if (fecha1 > fecha2)
        //        throw new Exception("La fecha 'Desde' debe ser menor a la fecha 'Hasta'.");

        //    if (fecha1 > DateTime.Today || fecha2 > DateTime.Today)
        //        throw new InvalidOperationException($"Las fechas deben ser menor igual al día de hoy. Hoy es: {DateTime.Today.ToString("dd/MM/yyyy")}.");

        //    try
        //    {
        //        /*
        //        var m = _db.Mantenimientos.Where(m => m.Fecha >= fecha1 && m.Fecha <= fecha2)
        //                                    .OrderByDescending(m => m.CostoMantenimiento)
        //                                    .ToList();
        //        */

        //        var m = _db.Mantenimientos.Where(m => m.Fecha.Fecha >= fecha1 && m.Fecha.Fecha <= fecha2)
        //                                    .OrderByDescending(m => m.CostoMantenimiento)
        //                                    .ToList();
        //        return m;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        //}

        public IEnumerable<Mantenimiento> MostrarMantenimientosEntreFechas(DateTime? fecha1, DateTime? fecha2, int? idCabanha)
        {
            if (fecha1 == null || fecha2 == null)
                throw new ArgumentNullException("Las fechas no puede ser nulas.");

            if (fecha1 > fecha2)
                throw new Exception("La fecha 'Desde' debe ser menor a la fecha 'Hasta'.");

            if (fecha1 > DateTime.Today || fecha2 > DateTime.Today)
                throw new InvalidOperationException($"Las fechas deben ser menor igual al día de hoy. Hoy es: {DateTime.Today.ToString("dd/MM/yyyy")}.");

            if (idCabanha == null)
                throw new ArgumentNullException("El id de la cabaña no puede ser nulo.");

            try
            {
                var m = _db.Mantenimientos.Where(m => m.Fecha.Fecha >= fecha1 && m.Fecha.Fecha <= fecha2 && m.IdCabanha == idCabanha)
                                            .OrderByDescending(m => m.CostoMantenimiento)
                                            .ToList();
                return m;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <see>LogicaNegocio.InterfacesRepositorios.IRepositorioMantenimiento#ControlarRegistrosPorDia(System.DateTime)</see>
        ///
        public int ControlarRegistrosPorDia(DateTime? fecha, int idCabanha)
        {
            if (fecha == null) throw new ArgumentNullException("La fecha no puede ser nula");

            try
            {
                //var m = _db.Mantenimientos.Where(m => m.Fecha == fecha && m.IdCabanha == idCabanha).Count();
                var m = _db.Mantenimientos.Where(m => m.Fecha.Fecha == fecha && m.IdCabanha == idCabanha).Count();

                return m;
            }
            catch (Exception ex)
            {
                throw;

            }
        }

        //public IEnumerable<Mantenimiento> Consulta6_B(decimal? valor1, decimal? valor2)
        //{
        //    if (valor1 == null || valor2 == null)
        //        throw new MantenimientoException("Los valores no pueden ser nulos.");

        //    var q = _db.Mantenimientos.Where(x => x.CostoMantenimiento >= valor1 && x.CostoMantenimiento <= valor2)
        //                                .GroupBy(x => new { x.RealizadoPor })
        //                                .Select(x => new
        //                                {
        //                                    x.Key.RealizadoPor,
        //                                    Total = x.Select(y => y.CostoMantenimiento).Sum()
        //                                });

        //    return q;
        //}

    }

}

