using Newtonsoft.Json;
using PRMOVIL2CARWASH.Models;
using PRMOVIL2CARWASH.Utils;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net.Http;
using System.Text;

namespace PRMOVIL2CARWASH.ViewModels
{
    public class ListVehiclesViewModel
    {
        private ListVehicles oldVehicles;
        public ObservableCollection<ListVehicles> VehiclesList { get; set; }

        public ListVehiclesViewModel()
        {
            HttpClient cliente = new HttpClient();
            //Debug.WriteLine("----------------------------");
            //var url = 
            //Debug.WriteLine(url);
            try
            {
                var api= Constanst.GetUrl("/vehicle/all/") + App.CurrentUser().IdUsuario;
                var respuesta = cliente.GetAsync(api).Result;
                Debug.WriteLine("------------------------------------------------------------");

                Debug.WriteLine(respuesta);
                var json = respuesta.Content.ReadAsStringAsync().Result;

                Debug.WriteLine("------------------------------------------------------------");
                Debug.WriteLine(json);
                var ListaVehiculos1 = JsonConvert.DeserializeObject<List<ListVehicles>>(json.ToString());
                Debug.WriteLine(ListaVehiculos1);
                VehiclesList = new ObservableCollection<ListVehicles>(ListaVehiculos1);
                Debug.WriteLine(VehiclesList);
            }
            catch (Exception e)
            {
                Debug.WriteLine("Error", "Error " + e.Message, "Ok");
            }
        }

        private void FillObservableCollection(List<ListVehicles> vehicles)
        {
            foreach (ListVehicles list in vehicles)
            {
                VehiclesList.Add(list);
            }
        }
    }

       
}
