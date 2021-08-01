using System;
using System.Collections.Generic;
using System.Text;
using PRMOVIL2CARWASH.Utils;
using System;
using Xamarin.Forms;

namespace PRMOVIL2CARWASH.ViewModels
{
    public class VehiclesViewModel:BaseViewModel
    {
        public Command SaveInformation { get; }
        public Command TakePhotoCommand { get; }
        public Command OpenGalleryCommand { get; }

        Page Page;

        string brand;
        string modelo;
        string type;
        string motor;
        int year;
        string plaque;
        string observation;
        byte[] foto;
        ImageSource photo = ImageSource.FromUri(new Uri("https://images.vexels.com/media/users/3/204554/isolated/lists/53193fd7db3d2618ab56635e69e64515-pequenas-hojas-de-frutos-rojos.png"));

        public string Brand { get => brand; 
            set => brand = value; }

        public string Modelo { get => modelo; 
            set => modelo = value; }

        public string Type { get => type; 
            set => type = value; }

        public string Motor { get => motor; 
            set => motor = value; }

        public int Year { get => year; 
            set => year = value; } 

        public string Plaque { get => plaque; 
            set => plaque = value; }

        public string Observation { get => observation; 
            set => observation = value; }

        public byte[] Foto { get => foto; 
            set => foto = value; }

        public ImageSource Photo
        {
            get => photo;
            set
            {
                SetProperty(ref photo, value);
            }
        }

        public VehiclesViewModel(Page pag)
        {
            Page = pag;
            SaveInformation = new Command(OnRequestSave);
            OpenGalleryCommand = new Command(OnOpenGallery);
            TakePhotoCommand = new Command(OnTakePhoto);
        }

        private async void OnRequestSave(object obj)
        {
            if (!string.IsNullOrEmpty(Brand))
            {
                await Page.DisplayAlert("La marca de su vehículo es: ", Brand, "Ok");
            }
            else
            {
                await Page.DisplayAlert("La marca del vehículo no puede ir vacío", Brand, "ok");
            }
        }
        private async void OnOpenGallery()
        {
            IsBusy = true;
            var media = new MediaManager();
            bool isSuccess = await media.TakePicture();
            if (isSuccess)
                Photo = media.Image;

            IsBusy = false;
        }
        private async void OnTakePhoto()
        {
            var media = new MediaManager();
            bool isSuccess = await media.PickPicture();
            if (isSuccess)
                Photo = media.Image;
        }
    }
}
