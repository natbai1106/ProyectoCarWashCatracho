using System;
using System.Collections.Generic;
using PRMOVIL2CARWASH.Utils;
using Xamarin.Forms;
using System.Linq;
using PRMOVIL2CARWASH.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Acr.UserDialogs;
using PRMOVIL2CARWASH.Services;

namespace PRMOVIL2CARWASH.ViewModels
{

    public class VehiclesViewModel : BaseViewModel
    {
        public Command SaveInformation { get; }
        public Command TakePhotoCommand { get; }
        public Command OpenGalleryCommand { get; }

        Page Page;



        ObservableCollection<MarcaVehiculo> brand;
        ObservableCollection<ModeloVehiculo> modelo;
        ObservableCollection<TipoVehiculo> type;
        ObservableCollection<Combustible> motor;
        Combustible motorSelected;
        TipoVehiculo typeSelected;
        MarcaVehiculo brandSelected;
        ModeloVehiculo modeloSelected;
        int year;
        string plaque;
        string observation;
        byte[] photoArray;
        bool takeNewPhoto;
        ImageSource photoProfile;

        public ObservableCollection<ModeloVehiculo> ModeloV
        {
            get => modelo; set => SetProperty(ref modelo, value);
        }

        public Combustible MotorSelected
        {
            get => motorSelected; set => SetProperty(ref motorSelected, value);
        }
        public TipoVehiculo TypeSelected
        {
            get => typeSelected; set { SetProperty(ref typeSelected, value); }
        }
        public MarcaVehiculo BrandSelected
        {
            get => brandSelected; set { SetProperty(ref brandSelected, value); getModelos(); }
        }

        public ModeloVehiculo ModeloSelected
        {
            get => modeloSelected; set { SetProperty(ref modeloSelected, value); getTipos(); }
        }

        public ObservableCollection<MarcaVehiculo> Brand
        {
            get => brand; set => SetProperty(ref brand, value);
        }

        public ObservableCollection<TipoVehiculo> Type
        {
            get => type; set => SetProperty(ref type, value);
        }

        public ObservableCollection<Combustible> Motor
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
        private async Task Cargar() 
        {
            BrandService Marcas = new BrandService();
            MotorService MotorServ = new MotorService();

            if (IsNotConnect)
                await Shell.Current.GoToAsync("..");

            UserDialogs.Instance.ShowLoading("Cargando");
            var resultMarcas = await Marcas.ObtenerMarcas();
            if (resultMarcas != null)
                Brand = new ObservableCollection<MarcaVehiculo>(resultMarcas);
            UserDialogs.Instance.HideLoading();

            UserDialogs.Instance.ShowLoading("Cargando");
            var resultMotor = await MotorServ.ObtenerGas();
            if (resultMotor != null)
                Motor = new ObservableCollection<Combustible>(resultMotor);
            UserDialogs.Instance.HideLoading();

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

        private async Task getModelos()
        {
            Type = null;
            ModelService Modelos = new ModelService();
            UserDialogs.Instance.ShowLoading("Cargando");

            var resultModelos = await Modelos.ObtenerModelos(brandSelected.IdMarcaVehiculos);
            if (resultModelos != null)
                ModeloV = new ObservableCollection<ModeloVehiculo>(resultModelos);
            UserDialogs.Instance.HideLoading();

        }

        private async Task getTipos()
        {
            TypeService Tipo = new TypeService();
            UserDialogs.Instance.ShowLoading("Cargando");
            var resultTV = await Tipo.ObtenerTipos(ModeloSelected.IdTipoVehiculos);
            if (resultTV != null)
                Type = new ObservableCollection<TipoVehiculo>(resultTV);
            UserDialogs.Instance.HideLoading();

        }
    }

}