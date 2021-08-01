using Acr.UserDialogs;
using MonkeyCache.FileStore;
using PRMOVIL2CARWASH.Models;
using PRMOVIL2CARWASH.Utils;
using PRMOVIL2CARWASH.Views;
using System;
using Xamarin.Forms;

namespace PRMOVIL2CARWASH.ViewModels
{
    public class UsersViewModel:BaseViewModel
    {
        public Command SendVerifyCommand {  get; }
        public Command ThakePhotoCommand { get; }
        public Command OpenGaleryCommand { get; }
        public Command CancelRegisterCommand { get; }

        Page Page;

        string name;
        string lastName;
        string address;
        string mail;
        string telephone;
        string user;
        string password;
        byte[] foto;
        bool verifyMethod;
        ImageSource photo;
   
        bool isBusy = false;
        public bool IsBussy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }
        public string Name { get => name;
            set { SetProperty(ref name, value); }
        }
        public string LastName { get => lastName;
            set { SetProperty(ref lastName, value); } }
        public string Address { get => address;
            set { SetProperty(ref address, value); } }
        public string Mail { get =>     mail; 
            set { SetProperty(ref mail, value); } }
        public string Telephone { get => telephone; 
            set { SetProperty(ref telephone, value); } }
        public string User { get => user;
            set { SetProperty(ref user, value); }
        }
        public string Password { get => password;set { SetProperty(ref password, value); } }


       public byte[] PhotoByte { get => foto; set => foto = value; }
        public ImageSource Photo
        { 
            get => photo;
            set
            {
                SetProperty(ref photo, value);
            }
        }

        public bool VerifyByMail { get => verifyMethod; set => verifyMethod = value; }

        public UsersViewModel(Page pag)
        {
            Page = pag;
            SendVerifyCommand = new Command(OnRequestVerify);
            OpenGaleryCommand = new Command(OnOpenGalery, () => !IsBussy);
            CancelRegisterCommand = new Command(OnCancelRegister);
            ThakePhotoCommand = new Command(OnTakePhoto, () => !IsBussy);
            //  Photo = ImageSource.FromUri(new Uri("https://images.vexels.com/media/users/3/204554/isolated/lists/53193fd7db3d2618ab56635e69e64515-pequenas-hojas-de-frutos-rojos.png"));
              LoadCache();
        }


        private async void OnRequestVerify(object obj)
        {
            
            //realiza una validacion  a la para obtener un codigo de verificación
            User user = new User
            {
                Nombre = Name,
                Apellido = LastName,
                Direccion=Address,
                Correo=Mail,
                Telefono=Telephone,
                Usuario=User,
                Contrasena=Password,
                Foto=PhotoByte
            };
            int respuesta;

            //Verifica si esta habilitado enviar por mail en caso contrario se envia por telefono
            if (VerifyByMail)
            {
                respuesta = await user.VerfyAccount(Constanst.VERIFY_MAIL, Mail);
            }
            else
            {
               respuesta = await user.VerfyAccount(Constanst.VERIFY_PHONE_NUMBER, Name);
            }
             
              //Si el resultado es correcto entonces se procede a validar si existe algun dato guardado en cache de lo contrario lo guarda
               if(respuesta==Constanst.REQUEST_OK)
                {
                   if(Barrel.Current.Exists(key: Constanst.USER_CACHE))
                   {
                     Barrel.Current.Empty(key: Constanst.USER_CACHE);
                   }
                    Barrel.Current.Add<User>(key: Constanst.USER_CACHE, data: user, expireIn: TimeSpan.FromDays(7));
                    await Page.Navigation.PushAsync(new Validacion());
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

        private async void OnCancelRegister()
        {
          

            bool IsCanceled = await Page.DisplayAlert("Cancelar registro","¿Esta seguro que desea cancelar?, si tiene datos ingresados se perderan.","Aceptar","Cancelar");
           if (IsCanceled)
            {
                Name = string.Empty;
                LastName = string.Empty;
                Address= string.Empty;
                Mail = string.Empty;
                Telephone = string.Empty;
                User = string.Empty;
                Password = string.Empty;
                Photo = FileImageSource.FromFile("camara.png");
                PhotoByte = null; 

            }
        }

        //Se carga la foto obtenidad de la galeria o camara y la almacena en las variables para mostrarla en la vista
        private void LoadPhoto(bool isSucces, MediaManager media)
        {
            if (isSucces)
            {
                Photo = media.Image;
                PhotoByte = media.ByteImage;
            }
        }


       private void LoadCache()
        {
            
            if (!Barrel.Current.IsExpired(key: Constanst.USER_CACHE))
            {
              var userCache = Barrel.Current.Get<User>(Constanst.USER_CACHE);

                Name = userCache.Nombre;
                LastName = userCache.Apellido;
                Address = userCache.Direccion;
                Mail = userCache.Correo;
                Telephone = userCache.Telefono;
                User = userCache.Usuario;
                Password = userCache.Contrasena;
                Photo = FileImageSource.FromFile("camara.png");
                PhotoByte = userCache.Foto;
            }
        }

    }
}
