using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace PRMOVIL2CARWASH.Models
{
   public class Response
    {
        

        [JsonProperty("status")]
        public string Status { get; set; }//Devuelve el estado de en formato de string
        [JsonProperty("codeStatus")]
        public int CodeStatus { get; set; }// Devuelve el codigio de estado de la peticion 

        [JsonProperty("token")]
        public string Token { get; set; }//Envia un mensaje 

        [JsonProperty("message")]
        public string Message { get; set; }//Envia un mensaje 
        [JsonProperty("statusSession")]

        public bool StatusSession { get; set; }
        
    }
}
