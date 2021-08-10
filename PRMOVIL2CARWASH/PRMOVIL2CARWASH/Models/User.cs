using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using MonkeyCache.FileStore;
using Newtonsoft.Json;
using Plugin.Media.Abstractions;
using PRMOVIL2CARWASH.Utils;
using Xamarin.Essentials;
using Xamarin.Forms;

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
        [JsonProperty("urlFoto")]
        public string UrlFoto { get; set; }
        [JsonProperty("codigo")]
        public string Codigo { get; set; }

        [JsonProperty("token")]
        public string Token { get; set; }

        [JsonProperty("playerId")]
        public Response PlayerId;

        [JsonProperty("respuesta")]
        public  Response Respuesta;

        [JsonProperty("metodoVerificacion")]
        public string ModoVerificacion { get; set; }
        
        
        [JsonProperty("foto")]
        public byte[] FotoByteArray { get; set; }

        public ImageSource FotoPerfil { get; set; }

        public MediaFile MediaFile;

        private HttpClient cliente;
        private HttpResponseMessage requestMessage;
        string url = Constanst.GetUrl("/users");
        
        //Se crea un constructor con el fin de mantener una solo instacia de la clase 
        public User()
        {
            cliente = new HttpClient();
        }

        public async Task<int> RegisterUser()
        {

            var data = JsonConvert.SerializeObject(this);
            var content = new StringContent(data, Encoding.UTF8, "application/json");

            requestMessage = await cliente.PostAsync(string.Concat(url, "/add"), content);
            if (requestMessage.IsSuccessStatusCode)
            {
                var contents = await requestMessage.Content.ReadAsStringAsync();
                var respuesta = JsonConvert.DeserializeObject<Response>(contents);
                if (respuesta.Status.Equals("ok"))
                {
                    /*Se recupera el token y se almacena en la variable TOKEN*/

                    Respuesta = respuesta;
                    return Constanst.REQUEST_OK;
                }
                else
                    return Constanst.REQUEST_ERROR;
            }
            else
                return Constanst.REQUEST_ERROR;

        }

        public async Task<int> VerfyAccount(string verificationMethod, string value)
        {
            var objeto = new
            {
                metodoVerificacion = verificationMethod,
                destinatario = value
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
                    /*Se recupera el token y se almacena en la variable TOKEN*/
                    ModoVerificacion = verificationMethod;
                    Token = respuesta.Token;
                    return Constanst.REQUEST_OK;
                }
                else
                    return Constanst.REQUEST_ERROR;
            }
            else
                return Constanst.REQUEST_ERROR;

        }

        public async Task<int> LogIn(string User, string Password)
        {
            var objeto = new
            {
                usuario = User,
                contrasena = Password
            };
            var data = JsonConvert.SerializeObject(objeto);
            var content = new StringContent(data, Encoding.UTF8, "application/json");
            requestMessage = await cliente.PostAsync(string.Concat(url, "/login"), content);
            if (requestMessage.IsSuccessStatusCode)
            {
                var contents = await requestMessage.Content.ReadAsStringAsync();
                var respuesta = JsonConvert.DeserializeObject<Response>(contents);
                if (respuesta.Status.Equals("ok"))
                {
                    await GetUser(User, Password);
                    return Constanst.REQUEST_OK;
                }
                else if (respuesta.Status.Equals("userExist"))
                    return Constanst.USER_EXIST;

                else
                    return Constanst.REQUEST_ERROR;
            }
            else
                return Constanst.REQUEST_ERROR;
        }

        public async Task GetUser(string user, string password)
        {
            var objeto = new
            {
                usuario = user,
                contrasena = password
            };

            var data = JsonConvert.SerializeObject(objeto);
            var content = new StringContent(data, Encoding.UTF8, "application/json");
            requestMessage = await cliente.PostAsync(url, content);

            if (requestMessage.IsSuccessStatusCode)
            {
                var contents = await requestMessage.Content.ReadAsStringAsync();
                var currentUser = JsonConvert.DeserializeObject<User>(contents);
               
                if (currentUser.Respuesta.Status.Equals("ok"))
                {
                    App.SetCurrentUser(currentUser);
                    Cache.SaveCache(currentUser.Usuario, currentUser, Constanst.EXPIRE_CURREN_USER);
                    Preferences.Set(Constanst.CURRENT_USER, currentUser.Usuario);
                    Preferences.Set(Constanst.STATE_SESSION, true);
                }

            }

        }
         
       public async Task<int> ResendVerifyCode()
        {

            string verificationMethod, valueMethod;

            if (ModoVerificacion.Equals(Constanst.VERIFY_MAIL))//Se obtiene el metodo de verificacion para reenviar el codigo de verificación
            {
                verificationMethod = Constanst.VERIFY_MAIL;
                valueMethod = Correo;
            }
               
            else
            { 
                verificationMethod = Constanst.VERIFY_PHONE_NUMBER;
                valueMethod = Telefono;
            }

            //Objeto anonimo para codificar
            var objeto = new {
                metodoVerificacion = verificationMethod,
                destinatario = valueMethod,
                token = Token
            };
          
            var data = JsonConvert.SerializeObject(objeto);
            var content = new StringContent(data, Encoding.UTF8, "application/json");
            requestMessage = await cliente.PostAsync(string.Concat(url, "/verify/resend"), content);
            if (requestMessage.IsSuccessStatusCode)
            {
                var contents = await requestMessage.Content.ReadAsStringAsync();
                var respuesta = JsonConvert.DeserializeObject<Response>(contents);
                if (respuesta.Status.Equals("ok"))
                {
                    return Constanst.REQUEST_OK;
                }
                else if (respuesta.Status.Equals("userExist"))
                    return Constanst.USER_EXIST;
                else
                    return Constanst.REQUEST_ERROR;
            }
            else
                return Constanst.REQUEST_ERROR;

        }

        public async Task<int> CheckStatusSession()
        {
            //Objeto anonimo para codificar
            requestMessage = await cliente.GetAsync(string.Concat(url,"/status?usuario=",Usuario));
            if (requestMessage.IsSuccessStatusCode)
            {
                var contents = await requestMessage.Content.ReadAsStringAsync();
                var respuesta = JsonConvert.DeserializeObject<Response>(contents);
                if (respuesta.StatusSession)//Si esta inciada la sesion entones deuelve true
                {
                    await  GetUser(Usuario,Contrasena);
                    return Constanst.REQUEST_OK;
                }
                else
                    return Constanst.SESSION_CLOSED;
            }
            else
                return Constanst.REQUEST_ERROR;

        }

        public async Task<int> LogOut()
        {
            var objeto = new
            {
                usuario = Usuario,
                contrasena = Contrasena
            };

            var data = JsonConvert.SerializeObject(objeto);
            var content = new StringContent(data, Encoding.UTF8, "application/json");

            requestMessage = await cliente.PostAsync(string.Concat(url, "/logout"), content);
            if (requestMessage.IsSuccessStatusCode)
            {
                var contents = await requestMessage.Content.ReadAsStringAsync();
                var respuesta = JsonConvert.DeserializeObject<Response>(contents);
                if (respuesta.Status.Equals("ok"))
                {
                    Barrel.Current.EmptyAll();//Si se cerro sesion correctamente entonces limpia el cache y las preferencias
                    Preferences.Clear();
                    return Constanst.REQUEST_OK;

                }
                else if (respuesta.Status.Equals("noExist"))
                    return Constanst.USER_NO_EXIST;
                else
                    return Constanst.REQUEST_ERROR;
            }
            else
                return Constanst.REQUEST_ERROR;
        }

        public async Task<int> ResetPassword(string user)
        { 
            //Objeto anonimo para codificar
            var objeto = new
            {
                usuario = user
            };
            var data = JsonConvert.SerializeObject(objeto);
            var content = new StringContent(data, Encoding.UTF8, "application/json");
            requestMessage = await cliente.PutAsync(string.Concat(url, "/resetpass"), content);
            if (requestMessage.IsSuccessStatusCode)
            {
                var contents = await requestMessage.Content.ReadAsStringAsync();
                var respuesta = JsonConvert.DeserializeObject<Response>(contents);
                if (respuesta.Status.Equals("ok"))
                {
                    return Constanst.REQUEST_OK;
                }
                else if (respuesta.Status.Equals("failAuth"))
                    return Constanst.FAIL_AUTH;
                else
                    return Constanst.REQUEST_ERROR;
            }
            else
                return Constanst.REQUEST_ERROR;

        }

        public async Task<int> ChangePassword(string user, string type,string newPass,string oldPass)
        {
            //Objeto anonimo para codificar
            var objeto = new
            {  
                usuario= user,
                contrasenaVieja=oldPass,
                contrasenaNueva = newPass,
                tipo= type
            };

            var data = JsonConvert.SerializeObject(objeto);
            var content = new StringContent(data, Encoding.UTF8, "application/json");
            requestMessage = await cliente.PutAsync(string.Concat(url, "/changepass"), content);
            if (requestMessage.IsSuccessStatusCode)
            {
                var contents = await requestMessage.Content.ReadAsStringAsync();
                var respuesta = JsonConvert.DeserializeObject<Response>(contents);
                if (respuesta.Status.Equals("ok"))
                {
                    return Constanst.REQUEST_OK;
                }
                else if (respuesta.Status.Equals("noMatch"))
                    return Constanst.NO_MATCH_PASS;
                else
                    return Constanst.REQUEST_ERROR;
            }
            else
                return Constanst.REQUEST_ERROR;

        }

        public async  Task<int> UpdateUser()
        {
            MultipartFormDataContent form = new MultipartFormDataContent();

            form.Add(new StringContent(Nombre), "nombre");
            form.Add(new StringContent(Correo), "correo");
            form.Add(new StringContent(Telefono), "telefono");
            form.Add(new StringContent(App.CurrentUser().Usuario), "usuario");
            form.Add(new StringContent(App.CurrentUser().Contrasena), "contrasena");
            form.Add(new StringContent(App.CurrentUser().UrlFoto), "urlFoto");
            if(MediaFile!=null)
            form.Add(new StreamContent( MediaFile.GetStream()), Constanst.NAME_IMAGE, "imgUserUpdadate.jgp");
            
            requestMessage = await cliente.PostAsync(string.Concat(url, "/update"), form);
            var contents = await requestMessage.Content.ReadAsStringAsync();
            if (requestMessage.IsSuccessStatusCode)
            {
                 contents = await requestMessage.Content.ReadAsStringAsync();
                var respuesta = JsonConvert.DeserializeObject<Response>(contents);
                if (respuesta.Status.Equals("ok"))
                {
                    /*Se envia la nueva URL en el mensaje*/
                    App.CurrentUser().UrlFoto = respuesta.Message;
                    App.CurrentUser().Nombre = Nombre;
                    App.CurrentUser().Correo = Correo;
                    App.CurrentUser().Telefono = Telefono;
                    Cache.SaveCache(App.CurrentUser().Usuario, App.CurrentUser(), Constanst.EXPIRE_CURREN_USER);
                    Respuesta = respuesta;
                    return Constanst.REQUEST_OK;
                }
                else if(respuesta.Status.Equals("noExist"))
                    return Constanst.USER_NO_EXIST;
                else
                    return Constanst.REQUEST_ERROR;
            }
            else
                return Constanst.REQUEST_ERROR;
        }
    }



}
