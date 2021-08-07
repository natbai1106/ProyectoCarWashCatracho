using Newtonsoft.Json;
using PRMOVIL2CARWASH.Utils;
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

        public async Task<ObservableCollection<MotorType>> ObtenerTipoMotor()
        {
            try
            {
                var responseTipoMotor = await clientTipoMotor.GetStringAsync(Constanst.GetUrl("/vehicle/gas"));
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
