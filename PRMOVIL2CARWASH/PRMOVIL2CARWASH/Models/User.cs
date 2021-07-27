using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
namespace PRMOVIL2CARWASH.Models
{
  
	public class User
	{
        

		[JsonProperty("idUsuario")]
		public int IdUsuario {get; set;}
		[JsonProperty("nombre")]
		public int Nombre { get; set; }
        [JsonProperty("apellido")]
        public string Apellido { get; set; }
        [JsonProperty("direccion")]
        public string Direccion { get; set; }
        [JsonProperty("correo")]
        public string Correo { get; set; }
        [JsonProperty("telefono")]
        public string Telefono { get; set; }
        [JsonProperty("usuario")]
        public string Usuario { get; set; }
        [JsonProperty("contraseña")]
        public string Contraseña { get; set; }
        [JsonProperty("estadoSesion")]
         public bool EstadoSesion { get; set; }
        [JsonProperty("foto")]
        public byte [] Foto { get; set; }
        [JsonProperty("urlFoto")]
        public string UrlFoto { get; set; }

        public int RegisterUser(User usuario)
        {

            
            return 0;

        }
    }
}
