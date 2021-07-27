using System;
using System.Collections.Generic;
using System.Text;

namespace PRMOVIL2CARWASH.Models
{
   public class Response
    {
        public string Status { get; set; }
        public int CodeStatus { get; set; }
        public string Message { get; set; }
        public bool StatusSession { get; set; }
    }
}
