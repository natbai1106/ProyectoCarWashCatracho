using Newtonsoft.Json;
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
        HttpClient clientMarcas;
        [JsonProperty("idModeloVehiculos")]
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
            clientMarcas = new HttpClient();
        }
        public const string Url = "http://173.249.21.6/v1/vehicle/brand";
        public async Task<ObservableCollection<Brand>> ObtenerMarcas()
        {
            try
            {
                var response = clientMarcas.GetStringAsync(Url).Result;
                Console.WriteLine("VALOR DE LA VARIABLE RESPONSE" + response);
                ObservableCollection<Brand> taskresultMarcas = JsonConvert.DeserializeObject<ObservableCollection<Brand>>(response);
                Console.WriteLine("VALOR DE LA VARIABLE TASRESULT" + taskresultMarcas);
                return taskresultMarcas;
            }
            catch (Exception e)
            {
                throw;
                //Debug.WriteLine("Error", "Error " + e.Message, "Ok");
            }
        }
    }
}
