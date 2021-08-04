using System;
using System.Collections.Generic;
using System.Text;

namespace PRMOVIL2CARWASH.Models
{
    public class Brand
    {
        public int IdMarca
        {
            get; set;
        }
        public string NombreMarca
        {
            get; set;
        }
        public List<Brand> GetMarcas()
        {
            var vMarcas = new List<Brand>()
            {
                new Brand (){ IdMarca = 1, NombreMarca = "Toyota"},
                new Brand (){ IdMarca = 2, NombreMarca = "Honda"},
                new Brand (){ IdMarca = 3, NombreMarca = "Mazda"}
            };
            return vMarcas;
        }
    }
}
