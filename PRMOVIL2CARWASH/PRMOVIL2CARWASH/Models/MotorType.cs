using System;
using System.Collections.Generic;
using System.Text;

namespace PRMOVIL2CARWASH.Models
{
    public class MotorType
    {
        public int IdMotor
        {
            get;
            set;
        }
        public string NombreMotor
        {
            get; 
            set;
        }

        public List<MotorType> GetMotor()
        {
            var vMotor = new List<MotorType>()
            {
                new MotorType (){ IdMotor = 1, NombreMotor = "Gasolina"},
                new MotorType (){ IdMotor = 2, NombreMotor = "Diesel"},
            };
            return vMotor;
        }
    }
}
