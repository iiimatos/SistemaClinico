using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProyectoFinal.Models.Tablas
{
    public class Pacientes
    {
        [Key]
        public int IdPacientes { set; get; }

        [Required]
        public string Cedula { get; set; }

        [Required]
        public string Nombre { get; set; }

        [Required]
        public Boolean Asegurado { get; set; }
    }


}