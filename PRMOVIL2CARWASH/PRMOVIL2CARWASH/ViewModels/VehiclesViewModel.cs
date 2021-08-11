using System;
using System.Collections.Generic;
using PRMOVIL2CARWASH.Utils;
using Xamarin.Forms;
using System.Linq;
using PRMOVIL2CARWASH.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Acr.UserDialogs;

namespace PRMOVIL2CARWASH.ViewModels
{

    public class VehiclesViewModel : BaseViewModel
    {
        public Command SaveInformation { get; }
        public Command TakePhotoCommand { get; }
        public Command OpenGalleryCommand { get; }

        Page Page;

        ObservableCollection<Brand> brand;
        ObservableCollection<Modelos> modelo;
        ObservableCollection<TypeVehicle> type;
        ObservableCollection<MotorType> motor;
        MotorType motorSelected;
        TypeVehicle typeSelected;
        Brand brandSelected;
        Modelos modeloSelected;
        int year;
        string plaque;
        string observation;
        byte[] photoArray;
        bool takeNewPhoto;
        ImageSource photoProfile;

        public ObservableCollection<Modelos> Modelo
        {
            get => modelo; set => SetProperty(ref modelo, value);
        }

        public MotorType MotorSelected
        {
            get => motorSelected; set => SetProperty(ref motorSelected, value);
        }
        public TypeVehicle TypeSelected
        {
            get => typeSelected; set => SetProperty(ref typeSelected, value);
        }
        public Brand BrandSelected
        {
            get => brandSelected; set => SetProperty(ref brandSelected, value);
        }

        public Modelos ModeloSelected
        {
            get => modeloSelected; set => SetProperty(ref modeloSelected, value);
        }

        public ObservableCollection<Brand> Brand
        {
            get => brand; set => SetProperty(ref brand, value);
        }

        public ObservableCollection<TypeVehicle> Type
        {
            get => type; set => SetProperty(ref type, value);
        }

        public ObservableCollection<MotorType> Motor
        {
            get => motor; set => SetProperty(ref motor, value);
        }

        public int Year
        {
            get => year; set => year = value;
        }

        public string Plaque
        {
            get => plaque; set => plaque = value;
        }

        public string Observation
        {
            get => observation; set => observation = value;
        }

        public byte[] PhotoByteArray { get => photoArray; set { photoArray = value; } }

        public ImageSource PhotoProfile
        {
            get => photoProfile;
            set
            {
                SetProperty(ref photoProfile, value);
            }
        }
        public bool TakeNewPhoto { get => takeNewPhoto; set => takeNewPhoto = value; }

        public VehiclesViewModel(Page pag)
        {
            Page = pag;

            Cargar();

            SaveInformation = new Command(OnRequestSave);
            OpenGalleryCommand = new Command(OnOpenGallery);
            TakePhotoCommand = new Command(OnTakePhoto);
        }
<<<<<<< HEAD
        private async Task Cargar() 
=======
        private async void Cargar()
>>>>>>> ac145be25bfc6722713ceadb03fc3d7b42baf605
        {
            Modelos Models = new Modelos();
            Brand Marcas = new Brand();
            TypeVehicle TipoVehiculo = new TypeVehicle();
            MotorType MotorTipo = new MotorType();
            if(IsNotConnect)
                await Shell.Current.GoToAsync("..");
            //Modelo = await Models.ObtenerModelos();
            //Brand = await Marcas.ObtenerMarcas();
            //Motor = await MotorTipo.ObtenerTipoMotor();
            //Type = await TipoVehiculo.ObtenerTipoVehiculo();
        }

        private async void OnRequestSave(object obj)
        {
            DateTime fechaActual = DateTime.Now;
            int anio = int.Parse(fechaActual.ToString("yyyy"));
            await Page.DisplayAlert("Mensaje", "" + anio, "Ok");

            if (MotorSelected == null || TypeSelected == null || BrandSelected == null || ModeloSelected == null || Year == 0 || Plaque == null)
            {
                await Page.DisplayAlert("Mensaje", "No deben haber campos vacíos", "Ok");
            }

        }
        /*Abre la galeria*/
        private async void OnOpenGallery()
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
    }
}