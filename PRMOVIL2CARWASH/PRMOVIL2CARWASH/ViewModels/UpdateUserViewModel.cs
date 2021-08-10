using Acr.UserDialogs;
using Plugin.Media.Abstractions;
using PRMOVIL2CARWASH.Models;
using PRMOVIL2CARWASH.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace PRMOVIL2CARWASH.ViewModels
{
    public class UpdateUserViewModel:BaseViewModel
    {
        string name;
        string mail;
        string telephone;
        string urlPhoto;
        bool takeNewPhoto;
        public Command CancelUpdateCommand { get; }
        public Command SelectMedia { get; }
        public Command UpdateUserCommand { get; }
        

        MediaFile PhotoMediaFile { get; set; }
        public string Mail
        {
            get => mail;
            set { SetProperty(ref mail, value); }
        }
        public string Telephone
        {
            get => telephone;
            set { SetProperty(ref telephone, value); }
        }
        ImageSource photoProfile;
        public ImageSource PhotoProfile
        {
            get => photoProfile;
            set
            {
                SetProperty(ref photoProfile, value);
            }
        }
        public string Name
        {
            get => name;
            set { SetProperty(ref name, value); }
        }

        public bool TakeNewPhoto { get => takeNewPhoto; set => takeNewPhoto = value; }
        public string UrlPhoto { get => urlPhoto; set => SetProperty(ref urlPhoto, value); }

        Page Page;
        public UpdateUserViewModel(Page page)
        {
            Page = page;
            SelectMedia = new Command(OnSelectedMedia);
            UpdateUserCommand = new Command(OnUpdateUserCliked);
            LoadData();

        }




        public async void OnSelectedMedia()
        {

            string action = await Page.DisplayActionSheet("Abrir", "Cancel", null, Constanst.CAMARA, Constanst.GALERIA);
            Console.WriteLine("Selecciono " + action);

            if (action == Constanst.CAMARA)
            {
                OnOpenGalery();

            }
            else if (action == Constanst.GALERIA)
            {
                OnTakePhoto();
            }
        }
        private async void OnOpenGalery()
        {
            var media = new MediaManager();

            UserDialogs.Instance.ShowLoading("Cargando");
            bool isSuccess = await media.TakePicture();
            LoadPhoto(isSuccess, media);
            UserDialogs.Instance.HideLoading();
        }
   
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
                UrlPhoto = media.Path;
                PhotoMediaFile = media.MediaFile;
                
            }
        }

        public async void OnUpdateUserCliked()
        {
            if (!IsNotConnect)
            {

                User auxUser = new User
                {
                    MediaFile = PhotoMediaFile,
                    Nombre = Name,
                    Correo = Mail,
                    Telefono = Telephone
                };

                UserDialogs.Instance.ShowLoading("Actualizando");
                int respuesta = await auxUser.UpdateUser();
                if (respuesta == Constanst.REQUEST_OK)
                {
                    await Page.DisplayAlert("Realizado", "Información actualizada", "Aceptar");
                    await Shell.Current.GoToAsync("..");
                }
                else if (respuesta == Constanst.ALL_UPDATE)
                    UserDialogs.Instance.Toast("Tu información esta actualizada.");
                else if (respuesta == Constanst.USER_NO_EXIST)
                    UserDialogs.Instance.Toast("El usuario no existe.");
                else
                    UserDialogs.Instance.Toast("Lo sentimos no se pudo actualizar");

                UserDialogs.Instance.HideLoading();
            }
            else
                UserDialogs.Instance.Toast("Lo sentimos no tienes acceso a internet");
        }


        private async void LoadData()
        {
            Name = App.CurrentUser().Nombre;
            Mail = App.CurrentUser().Correo;
            Telephone = App.CurrentUser().Telefono;
            UrlPhoto = App.CurrentUser().UrlFoto;
            //if (string.IsNullOrEmpty(App.CurrentUser().UrlFoto))
            //    UrlPhoto = Constanst.USER_IMAGE_DEFAULT;
            //else
            //    UrlPhoto = App.CurrentUser().UrlFoto;


        }
    }
}
