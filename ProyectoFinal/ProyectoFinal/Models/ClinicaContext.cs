using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using ProyectoFinal.Models.Tablas;

namespace ProyectoFinal.Models
{
    public class ClinicaContext:DbContext
    {
        public ClinicaContext() : base("ClinicaDataBase") { }

        public DbSet<Pacientes> Pacientes { get; set; }
        public DbSet<Habitaciones> Habitaciones { get; set; }
        public DbSet<Medicos> Medicos { get; set; }
        public DbSet<TCitas> Citas { get; set; }
        public DbSet<TIngresos> Ingresos { get; set; }
        public DbSet<TAltaMedica> AltaMedicas { get; set; }

    }
}