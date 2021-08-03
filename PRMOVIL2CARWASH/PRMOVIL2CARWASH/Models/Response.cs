using System;
using System.Collections.Generic;
using System.Text;

namespace PRMOVIL2CARWASH.Models
{
   public class Response
    {
        public string Status { get; set; }//Devuelve el estado de en formato de string
        public int CodeStatus { get; set; }// Devuelve el codigio de estado de la peticion 
        public string Message { get; set; }//Envia un mensaje 
        public bool StatusSession { get; set; }
    }
}
