using System;
using System.Collections.Generic;
using PRMOVIL2CARWASH.Utils;
using Xamarin.Forms;
using System.Linq;
using PRMOVIL2CARWASH.Models;

namespace PRMOVIL2CARWASH.ViewModels
{
    public class VehiclesViewModel:BaseViewModel
    {
        public Command SaveInformation { get; }
        public Command TakePhotoCommand { get; }
        public Command OpenGalleryCommand { get; }

        Page Page;

        List<Brand> brand;
        List<Modelos> modelo;
        List<TypeVehicle> type;
        List<MotorType> motor;
        int idmotor;
        int idmarca;
        int idmodelo;
        int idtipovehiculo;
        int year;
        string plaque;
        string observation;
        byte[] foto;
        ImageSource photo = ImageSource.FromUri(new Uri("https://images.vexels.com/media/users/3/204554/isolated/lists/53193fd7db3d2618ab56635e69e64515-pequenas-hojas-de-frutos-rojos.png"));

        public int IdMotor
        {
            get => idmotor;
            set => SetProperty(ref idmotor, value);
        }
        public int IdMarca
        {
            get => idmarca;
            set => SetProperty(ref idmarca, value);
        }
        public int IdModelo
        {
            get => idmodelo;
            set => SetProperty(ref idmodelo, value);
        }
        public int IdTypeVehicle
        {
            get => idtipovehiculo;
            set => SetProperty(ref idtipovehiculo, value);
        }

        public List<Brand> Brand { get => brand; 
            set => brand = value; }

        public List<Modelos> Modelo { get => modelo; 
            set => modelo = value; }

        public List<TypeVehicle> TypeVehicle { get => type; 
            set => type = value; }

        public List<MotorType> MotorType { get => motor; 
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
            MotorType motorType = new MotorType();
            Brand listaBrand = new Brand();
            Modelos modelos = new Modelos();
            TypeVehicle typeVehicle = new TypeVehicle();

            Brand = listaBrand.GetMarcas().OrderBy(c => c.NombreMarca).ToList();
            TypeVehicle = typeVehicle.GetTipos().OrderBy(c => c.NombreTipoVehicle).ToList();
            MotorType = motorType.GetMotor().OrderBy(c => c.NombreMotor).ToList();
            Modelo = modelos.GetModelos().OrderBy(c => c.NombreModelo).ToList();

            SaveInformation = new Command(OnRequestSave);
            OpenGalleryCommand = new Command(OnOpenGallery);
            TakePhotoCommand = new Command(OnTakePhoto);
        }
        
        private async void OnRequestSave(object obj)
        {
            var ListaBrand = new List<Brand>();
            if (ListaBrand.Count == 0)
            {
                await Page.DisplayAlert("Mensaje", "Debe seleccionar una Marca para su auto", "Ok");
            }

            var ListaModelos = new List<Modelos>();
            if (ListaModelos.Count == 0)
            {
                await Page.DisplayAlert("Mensaje", "Debe seleccionar un Modelo para su auto", "Ok");
            }

            var ListaType = new List<TypeVehicle>();
            if (ListaType.Count == 0)
            {
                await Page.DisplayAlert("Mensaje", "Debe seleccionar un Tipo de auto", "Ok");
            }

            var ListaMotor = new List<MotorType>();
            if (ListaMotor.Count == 0)
            {
                await Page.DisplayAlert("Mensaje", "Debe seleccionar el Tipo de combustible para el Motor de su auto", "Ok");
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
