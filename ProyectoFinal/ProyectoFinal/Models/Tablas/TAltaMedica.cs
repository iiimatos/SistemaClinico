using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProyectoFinal.Models.Tablas
{
    public class TAltaMedica
    {
            [Key]
            public int IdAltaMedica { set; get; }

            [Required]
            public string fechaIngreso { get; set; }

            [Required]
            public string Paciente { set; get; }

            [Required]
            public string Habitacion { set; get; }

            [Required]
            public string fechaSalida { set; get; }

            [Required]
            public double montoPagar { set; get; }

            [Required]
            public int Ingresos_Registrado { set; get; }
            [ForeignKey("Ingresos_Registrado")]
            public TIngresos Ingresos { set; get; }

        }
    }
