using System;
using System.Collections.Generic;
using System.Text;

namespace PRMOVIL2CARWASH.Models
{
    public class Modelos
    {
        public int IdModelo
        {
            get; set;
        }
        public string NombreModelo
        {
            get; set;
        }
        public List<Modelos> GetModelos()
        {
            var vModelos = new List<Modelos>()
            {
                new Modelos (){ IdModelo = 1, NombreModelo = "Honda Civic"},
                new Modelos (){ IdModelo = 2, NombreModelo = "Toyota Hilux"},
                new Modelos (){ IdModelo = 3, NombreModelo = "Mazda 6"}
            };
            return vModelos;
        }
    }
}
