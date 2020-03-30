using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProyectoFinal.Models.Tablas
{
    public class Habitaciones
    {
        [Key]
        public int IdHabitaciones { set; get; }

        [Required]
        public string Numero { set; get; }

        [Required]
        public tipoHabitaciones Tipo { get; set; }

        [Required]
        public double Precio { get; set; }
    }

    public enum tipoHabitaciones
    {
        Doble, Privada, Suite
    }
}