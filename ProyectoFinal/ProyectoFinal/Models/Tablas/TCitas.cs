using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProyectoFinal.Models.Tablas
{
    public class TCitas
    {
        [Key]
        public int IdCitas { set; get; }

        [Required]
        public int Pacientes_Registrado { set; get; }
        [ForeignKey("Pacientes_Registrado")]
        public Pacientes Pacientes { set; get; }

        [Required]
        public string Fecha_Reserva { set; get; }

        [Required]
        public string Hora_Reserva { set; get; }

        [Required]
        public int Medicos_Registrado { set; get; }
        [ForeignKey("Medicos_Registrado")]
        public Medicos Medicos { set; get; }
    }
}