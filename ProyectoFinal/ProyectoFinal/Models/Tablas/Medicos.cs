using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProyectoFinal.Models.Tablas
{
    public class Medicos
    {
        [Key]
        public int IdMedicos { set; get; }

        [Required]
        public string Nombre { set; get; }

        [Required]
        public string Exequatur { get; set; }

        [Required]
        public string Especialidad { get; set; }
    }
}