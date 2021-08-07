using Acr.UserDialogs;
using MonkeyCache.FileStore;
using PRMOVIL2CARWASH.Models;
using PRMOVIL2CARWASH.Utils;
using PRMOVIL2CARWASH.Views;
using System;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace PRMOVIL2CARWASH.ViewModels
{
    public class UsersViewModel : BaseViewModel
    {
        public Command SendVerifyCommand { get; }
        public Command ThakePhotoCommand { get; }
        public Command OpenGaleryCommand { get; }
        public Command CancelRegisterCommand { get; }

        Page Page;

        #region Declaracion de variables
        string name;
        string lastName;
        string address;
        string mail;
        string telephone;
        string user;
        string password;
        bool lastMethodVerify = false;
        byte[] photoArray;
        bool verifyByMail=true;
        bool takeNewPhoto;
        
        
        ImageSource photoProfile;
        User UserInCache { get; set; }
      
        public string Name
        {
            get => name;
            set { SetProperty(ref name, value);  }
        }
        public string LastName
        {
            get => lastName;
            set { SetProperty(ref lastName, value);  }
        }
        public string Address
        {
            get => address;
            set { SetProperty(ref address, value);  }
        }
        public string Mail  
        {
            get => mail;
            set { SetProperty(ref mail, value);  }
        }
        public string Telephone
        {
            get => telephone;
            set { SetProperty(ref telephone, value); }
        }
        public string User
        {
            get => user;
            set { SetProperty(ref user, value);}
        }
        public string Password { get => password; set { SetProperty(ref password, value); } }


        public byte[] PhotoByteArray { get => photoArray; set{ photoArray = value; } }
        public ImageSource PhotoProfile
        {
            get => photoProfile;
            set
            {
                SetProperty(ref photoProfile, value);
            }
        }

        public bool VerifyByMail
        {
            get => verifyByMail;
            set { SetProperty(ref verifyByMail, value); }
        }
        public bool TakeNewPhoto { get => takeNewPhoto; set => takeNewPhoto = value; }
       
       
        #endregion
        public UsersViewModel(Page pag)
        {
             Page = pag;
            SendVerifyCommand = new Command(OnRequestVerify);
            OpenGaleryCommand = new Command(OnOpenGalery);
            CancelRegisterCommand = new Command(OnCancelRegister);
            ThakePhotoCommand = new Command(OnTakePhoto);
            LoadCache();

           
        }
        private async void OnRequestVerify(object obj)
            {
            
           
            if (Barrel.Current.Exists(Constanst.USER_CACHE) && !IsChange())//Valida si existe un usuario existen y no hay cambios lo envia  a la pantalla de validadicion
            {   
                await Page.Navigation.PushAsync(new Validacion());
                return;
            }
            if(!IsNotConnect)//valida la conexion a internet
            {
                int respuesta = Constanst.REQUEST_ERROR;
                if (CheckRequires())//Valida los campos requeridos
                {
                  

                    if (!string.IsNullOrEmpty(UserInCache.Token))//Si tiene un Token entonces es una actualizacion de lo contrario es primera vez que llena el formulario
                    {

                        bool changeMail = !UserInCache.Correo.Equals(Mail);
                        bool changePhone = !UserInCache.Telefono.Equals(Telephone);
                        bool changeMethod = VerifyByMail != lastMethodVerify;

                        if (changeMethod || changeMail || changeMail) //Se reenviara solo si se cambio el correo, el telefono, o el metodo de verificación
                        {
                            UserInCache.ModoVerificacion = VerifyByMail ? Constanst.VERIFY_MAIL : Constanst.VERIFY_PHONE_NUMBER;
                          
                            SetDataToInstance(UserInCache);
                            Cache.SaveCache(Constanst.USER_CACHE, UserInCache, Constanst.EXPIRE_DAYS);

                            UserDialogs.Instance.ShowLoading("Reenviando");
                            respuesta = await UserInCache.ResendVerifyCode();
                            UserDialogs.Instance.HideLoading();

                            if (respuesta == Constanst.REQUEST_OK)
                                await Page.Navigation.PushAsync(new Validacion());
                            else
                                UserDialogs.Instance.Toast("Error al reenviar codigo de verificación.");
                        }
                        else//Sino se realizaron cambios entonces guardamos los cambios en cache
                        {
                            SetDataToInstance(UserInCache);
                            Cache.SaveCache(Constanst.USER_CACHE, UserInCache, Constanst.EXPIRE_DAYS);
                            await Page.Navigation.PushAsync(new Validacion());
                        }
                     
                    }
                    else
                    {
                        if (VerifyByMail) //Verifica si esta habilitado enviar por mail en caso contrario se envia por telefono
                        {
                            respuesta = await UserInCache.VerfyAccount(Constanst.VERIFY_MAIL, Mail);
                        }
                        else
                        {
                            respuesta = await UserInCache.VerfyAccount(Constanst.VERIFY_PHONE_NUMBER, Telephone);
                        }

                        UserDialogs.Instance.ShowLoading("Cargando");
                        //Si el resultado es correcto entonces se procede a validar si existe algun dato guardado en cache de lo contrario lo guarda
                        if (respuesta == Constanst.REQUEST_OK)
                        {
                            SetDataToInstance(UserInCache);//Asignamos los datos a una instacia de usuario para posteriormente guardar en cache
                            Cache.SaveCache(Constanst.USER_CACHE, UserInCache, Constanst.EXPIRE_DAYS);
                            await Page.Navigation.PushAsync(new Validacion());

                        }
                        else
                            UserDialogs.Instance.Toast("Lo sentimos no hemos podido enviar código de verificación.");

                        UserDialogs.Instance.HideLoading();
                    }
                }
                else if (respuesta == Constanst.USER_EXIST)
                {
                    await Page.DisplayAlert("Usuario existnte", "Ya existe un usuario registrado con este " + (VerifyByMail ? "correo " + Mail : "Numero de télefono " + Telephone), "Aceptar");
                }
                else
                    UserDialogs.Instance.Toast("Lo sentimos no fue imposible enviar código."); 
            }
            else
            {
                UserDialogs.Instance.Toast("Necesita acceso a internet para registrarse");
            }



        }

        /*Abre la galeria*/
        private async void OnOpenGalery()
        {
            var media = new MediaManager();

            UserDialogs.Instance.ShowLoading("Cargando");
            bool isSuccess = await media.TakePicture();
            LoadPhoto(isSuccess, media);
            UserDialogs.Instance.HideLoading();
        }
        #region Foto
        /*Abre la camarara*/
        private async void OnTakePhoto()

        {
            var media = new MediaManager();
            //Se crea una instancia de la clase MediaManager para mostrar los cuando se esta cargando
            UserDialogs.Instance.ShowLoading("Cargando");
            bool isSuccess = await media.PickPicture();
            LoadPhoto(isSuccess, media);
            UserDialogs.Instance.HideLoading();
        }

        //Se carga la foto obtenidad de la galeria o camara y la almacena en las variables para mostrarla en la vista
        private void LoadPhoto(bool isSucces, MediaManager media)
        {
            if (isSucces)
            {
                PhotoProfile = media.Image;
                PhotoByteArray = media.ByteImage;
                TakeNewPhoto = true;
            }
        }
        #endregion
        private async void OnCancelRegister()
        {

            bool IsCanceled = await Page.DisplayAlert("Cancelar registro", "¿Esta seguro que desea cancelar?, si tiene datos ingresados se perderan.", "Aceptar", "Cancelar");
            if (IsCanceled)
            {
                Barrel.Current.EmptyAll();
                Preferences.Clear();
                await Page.Navigation.PopAsync();

            }
        }



        private async void LoadCache()
        {

            string usuario =  Constanst.USER_CACHE;
            if(Barrel.Current.Exists(key: usuario))
            {
                if (!Barrel.Current.IsExpired(key: usuario))
                {

                    UserInCache = Barrel.Current.Get<User>(usuario);
                    Name = UserInCache.Nombre;
                    LastName = UserInCache.Apellido;
                    Address = UserInCache.Direccion;
                    Mail = UserInCache.Correo;
                    Telephone = UserInCache.Telefono;
                    User = UserInCache.Usuario;
                    Password = UserInCache.Contrasena;
                    PhotoByteArray = UserInCache.FotoByteArray;
                    if (UserInCache.FotoByteArray == null)
                        PhotoProfile = ImageSource.FromFile(Constanst.USER_IMAGE_DEFAULT);
                    else
                    {
                        PhotoProfile = new MediaManager().ConvertByteArrayToImage(UserInCache.FotoByteArray);
                        TakeNewPhoto = true;
                    }
                      
                   //Establce nuevamente el metodo de verificacion
                    if (UserInCache.ModoVerificacion.Equals(Constanst.VERIFY_MAIL))
                        VerifyByMail = true;
                    else
                        VerifyByMail = false;

                    lastMethodVerify = VerifyByMail;
                }
                else
                {
                    UserInCache = new User();
                    PhotoProfile = ImageSource.FromFile(Constanst.USER_IMAGE_DEFAULT);
                }
                    
            }
          
            else
            {
                PhotoProfile = ImageSource.FromFile(Constanst.USER_IMAGE_DEFAULT);
                UserInCache = new User();
            }


        }   
     

        private bool CheckRequires()
        {
            if (!string.IsNullOrEmpty(User) && !string.IsNullOrEmpty(Name) && !string.IsNullOrEmpty(LastName)
                && !string.IsNullOrEmpty(Password) && !string.IsNullOrEmpty(Address) && !string.IsNullOrEmpty(Mail))
                return true;
            else 
                return false;
        }

        


        public async Task<int> OnSaveUserClicked()
        {
            return 0;
        }

       private void SetDataToInstance(User user)
        {
            user.Nombre = Name;
            user.Apellido = LastName;
            user.Direccion = Address;
            user.Correo = Mail;
            user.Telefono = Telephone;
            user.Usuario = User;
            user.Contrasena = Password;
            if (TakeNewPhoto)
                user.FotoByteArray = PhotoByteArray;
            else
                user.FotoByteArray = null;
        }

        private bool IsChange()
        {
             if (UserInCache.Nombre.Equals(Name) && UserInCache.Apellido.Equals(LastName) && UserInCache.Direccion.Equals(Address) &&
                 UserInCache.Telefono.Equals(Telephone) && UserInCache.Usuario.Equals(User) && UserInCache.Contrasena.Equals(Password) &&
                 UserInCache.Correo.Equals(Mail) && UserInCache.FotoByteArray == PhotoByteArray && lastMethodVerify == VerifyByMail)
                return false;
            else
                return true;
        }
        public void Verify()
        {

        }
    }
}
