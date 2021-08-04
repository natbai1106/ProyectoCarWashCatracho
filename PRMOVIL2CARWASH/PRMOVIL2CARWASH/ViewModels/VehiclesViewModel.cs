using System;
using System.Collections.Generic;
using PRMOVIL2CARWASH.Utils;
using Xamarin.Forms;
using System.Linq;
using PRMOVIL2CARWASH.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace PRMOVIL2CARWASH.ViewModels
{
    public class VehiclesViewModel:BaseViewModel
    {
        public Command SaveInformation { get; }
        public Command TakePhotoCommand { get; }
        public Command OpenGalleryCommand { get; }

        Page Page;

        ObservableCollection<Brand> brand;
        ObservableCollection<Modelos> modelo;
        List<TypeVehicle> type;
        List<MotorType> motor;
        MotorType motorSelected;
        TypeVehicle typeSelected;
        Brand brandSelected;
        Modelos modeloSelected;
        int year;
        string plaque;
        string observation;
        byte[] foto;
        ImageSource photo = ImageSource.FromUri(new Uri("https://images.vexels.com/media/users/3/204554/isolated/lists/53193fd7db3d2618ab56635e69e64515-pequenas-hojas-de-frutos-rojos.png"));

        /*--------------------BRAND = MARCA--------------------*/

        public Brand BrandSelected
        {
            get => brandSelected;
            set => SetProperty(ref brandSelected, value);
        }

        public ObservableCollection<Brand> Brand
        {
            get => brand;
            set => brand = value;
        }

        ///*--------------------MODELO DEL VEHICULO--------------------*/

        public Modelos ModeloSelected
        {
            get => modeloSelected;
            set => SetProperty(ref modeloSelected, value);
        }
        public ObservableCollection<Modelos> Modelo
        {
            get => modelo;
            set => modelo = value;
        }

        /*--------------------TYPE VEHICLE = TIPO DE VEHICULO--------------------*/

        public TypeVehicle TypeSelected
        {
            get => typeSelected;
            set => SetProperty(ref typeSelected, value);
        }
        public List<TypeVehicle> Type
        {
            get => type;
            set => type = value;
        }

        /*--------------------MOTOR TYPE = TIPO DE MOTOR--------------------*/

        public MotorType MotorSelected
        {
            get => motorSelected;
            set => SetProperty(ref motorSelected, value);
        }

        public List<MotorType> Motor { get => motor; 
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

            Modelos Models = new Modelos();
            Brand Marca = new Brand();

            Modelo = Models.ObtenerModelos().Result;
            //Brand = Marca.ObtenerMarcas().Result;

            //MotorType motorType = new MotorType();
            //TypeVehicle typeVehicle = new TypeVehicle();


            //Type = typeVehicle.GetTipos().OrderBy(c => c.NombreTipoVehicle).ToList();
            //Motor = motorType.GetMotor().OrderBy(c => c.NombreMotor).ToList();


            SaveInformation = new Command(OnRequestSave);
            OpenGalleryCommand = new Command(OnOpenGallery);
            TakePhotoCommand = new Command(OnTakePhoto);
        }

        private async void OnRequestSave(object obj)
        {
            DateTime fechaActual = DateTime.Now;
            int anio = int.Parse(fechaActual.ToString("yyyy"));
            await Page.DisplayAlert("Mensaje", ""+anio, "Ok");

            //if (MotorSelected == null || TypeSelected == null || BrandSelected == null || ModeloSelected == null || Year == 0 || Plaque == null)
            //{
            //    await Page.DisplayAlert("Mensaje", "No deben haber campos vacíos", "Ok");
            //}
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
