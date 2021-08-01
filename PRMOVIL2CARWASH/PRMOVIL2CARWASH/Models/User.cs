using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PRMOVIL2CARWASH.Utils;

namespace PRMOVIL2CARWASH.Models
{

    public class User
    {
        [JsonProperty("idUsuario")]
        public int IdUsuario { get; set; }
        [JsonProperty("nombre")]
        public string Nombre { get; set; }
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
        [JsonProperty("contrasena")]
        public string Contrasena { get; set; }
        [JsonProperty("estadoSesion")]
        public bool EstadoSesion { get; set; }
        [JsonProperty("foto")]
        public byte[] Foto { get; set; }
        [JsonProperty("urlFoto")]
        public string UrlFoto { get; set; }
        [JsonProperty("codigo")]
        public string Codigo { get; set; }

        [JsonProperty("token")]
        public string Token { get; set; }

        HttpClient cliente;
        HttpResponseMessage requestMessage;
        string url = Constanst.GetUrl("/users");
        public User()
        {
            cliente = new HttpClient();

        }
        public int RegisterUser()
        {
            var m = JsonConvert.SerializeObject(this);
            return 0;
        }

        public async Task<int> VerfyAccount(string typeVerification,string value )
        {
            var objeto = new {
                  VericationMethod = typeVerification,
                  ValueMethod = value
                 };
          
            var data = JsonConvert.SerializeObject(objeto);
             var content = new StringContent(data, Encoding.UTF8, "application/json");
           
            requestMessage = await cliente.PostAsync(string.Concat(url, "/verify"), content);
            if (requestMessage.IsSuccessStatusCode)
            {
                var contents = await requestMessage.Content.ReadAsStringAsync();
                var respuesta = JsonConvert.DeserializeObject<Response>(contents);
                if (respuesta.Status.Equals("ok"))
                {
                    return Constanst.REQUEST_OK;
                }
                else
                    return Constanst.REQUEST_ERROR;
            }
            else
                return Constanst.REQUEST_ERROR;

        }
    }
  


}
