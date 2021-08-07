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
    public class MotorType
    {
        HttpClient clientTipoMotor;

        [JsonProperty("idTipoCombustible")]
        public int IdMotor
        {
            get;
            set;
        }
        [JsonProperty("tipoCombustible")]
        public string NombreMotor
        {
            get; 
            set;
        }

        public MotorType()
        {
            clientTipoMotor = new HttpClient();
        }
        public const string Url = "http://173.249.21.6/v1/vehicle/brand";
        public async Task<ObservableCollection<MotorType>> ObtenerTipoMotor()
        {
            try
            {
                var responseTipoMotor = await clientTipoMotor.GetStringAsync(Url);
                ObservableCollection<MotorType> taskTipoMotor = JsonConvert.DeserializeObject<ObservableCollection<MotorType>>(responseTipoMotor);
                return taskTipoMotor;
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}
