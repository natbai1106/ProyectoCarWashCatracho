﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PRMOVIL2CARWASH.Models
{
    public class Brand
    {
        HttpClient clientMarca;

        [JsonProperty("idMarcaVehiculos")]
        public int IdMarca
        {
            get; set;
        }

        [JsonProperty("marca")]
        public string NombreMarca
        {
            get; set;
        }

        //public List<Brand> GetMarcas()
        //{
        //    var vMarcas = new List<Brand>()
        //    {
        //        new Brand (){ IdMarca = 1, NombreMarca = "Toyota"},
        //        new Brand (){ IdMarca = 2, NombreMarca = "Honda"},
        //        new Brand (){ IdMarca = 3, NombreMarca = "Mazda"}
        //    };
        //    return vMarcas;
        //}

        public Brand()
        {
            clientMarca = new HttpClient();
        }


        public const string Url = "http://173.249.21.6/v1/vehicle/brand";
        public async Task<ObservableCollection<Brand>> ObtenerMarcas()
        {

            try
            {
                var responseMarca = clientMarca.GetStringAsync(Url).Result;
                Console.WriteLine("VALOR DE LA VARIABLE RESPONSE" + responseMarca);
                ObservableCollection<Brand> taskMarca = JsonConvert.DeserializeObject<ObservableCollection<Brand>>(responseMarca);
                Console.WriteLine("VALOR DE LA VARIABLE TASRESULT" + taskMarca);
                return taskMarca;
            }
            catch (Exception e)
            {
                throw;
                Debug.WriteLine("Error", "Error " + e.Message, "Ok");
            }

        }

    }
}
