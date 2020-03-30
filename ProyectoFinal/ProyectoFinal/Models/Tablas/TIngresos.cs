using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProyectoFinal.Models.Tablas
{
    public class TIngresos
    {
        [Key]
        public int IdIngresos { set; get; }

        [Required]
        public int Pacientes_Registrado { set; get; }
        [ForeignKey("Pacientes_Registrado")]
        public Pacientes Pacientes { set; get; }

        [Required]
        public int Habitaciones_Registrada { set; get; }
        [ForeignKey("Habitaciones_Registrada")]
        public Habitaciones Habitaciones { get; set; }

        [Required]
        public string Fecha_Ingreso { set; get; }

    }
}