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
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace PRMOVIL2CARWASH.ViewModels
{

    public class VehiclesViewModel : BaseViewModel
    {
        public Command SaveInformation { get; }
        public Command TakePhotoCommand { get; }
        public Command OpenGalleryCommand { get; }
        public Command SelectMedia { get; }

        Page Page;

        HttpClient cliente;
        HttpResponseMessage requestMessage;
        string url = Constanst.GetUrl("/vehicle");

        ObservableCollection<MarcaVehiculo> brand;
        ObservableCollection<ModeloVehiculo> modelo;
        ObservableCollection<TipoVehiculo> type;
        ObservableCollection<Combustible> motor;
        Combustible motorSelected;
        TipoVehiculo typeSelected;
        MarcaVehiculo brandSelected;
        ModeloVehiculo modeloSelected;
        String UrlFoto;
        String urlphoto;
        int year;
        string plaque;
        string observation;
        byte[] photoArray;
        bool takeNewPhoto;
        ImageSource photoProfile;

        [JsonProperty("respuesta")]
        public Response Respuesta;

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

        
        public string UrlPhoto
        {
            get => urlphoto; set { SetProperty(ref urlphoto, value); }
        }

        public VehiclesViewModel(Page pag)
        {
            Page = pag;
            cliente = new HttpClient();
            UrlPhoto = Constanst.CAR_IMAGE_DEFAULT;
            Cargar();

            SaveInformation = new Command(OnRequestSave);
            SelectMedia = new Command(OnSelectedMedia);
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
            Year = int.Parse(fechaActual.ToString("yyyy"));
            //await Page.DisplayAlert("Mensaje", "" + anio, "Ok");

            

            if (BrandSelected == null || ModeloSelected == null || TypeSelected == null || MotorSelected == null || Plaque == null)
            {
                await Page.DisplayAlert("Mensaje", "No deben haber campos vacíos", "Ok");
            }
            else
            {
                await Page.DisplayAlert("Mensaje", "Felicidades, ahora sigue trabajando", "Ok");

                

                UserDialogs.Instance.ShowLoading("Cargando");
                
                var Res = await RegisterVehicle();
                //await Page.DisplayAlert("Mensaje", ""+ Res + "", "Ok");
                UserDialogs.Instance.HideLoading();

            }

        }

        public async Task<int> RegisterVehicle()
        {

            String observacion = string.IsNullOrEmpty(Observation) ? "" : Observation;

            MultipartFormDataContent form = new MultipartFormDataContent();

            form.Add(new StringContent(Plaque), "numeroPlaca");
            form.Add(new StringContent(Year.ToString()), "anio");
            form.Add(new StringContent(observacion), "observacion");
            form.Add(new StringContent(BrandSelected.IdMarcaVehiculos.ToString()), "idMarcaVehiculos");
            form.Add(new StringContent(ModeloSelected.IdModeloVehiculos.ToString()), "idModeloVehiculos");
            form.Add(new StringContent(TypeSelected.IdTipoVehiculos.ToString()), "IdTipoVehiculos");
            form.Add(new StringContent(MotorSelected.IdTipoCombustible.ToString()), "idTipoCombustible");
            form.Add(new StringContent(App.CurrentUser().IdUsuario.ToString()), "idUsuario");
            UrlFoto = UrlPhoto;

            if (!UrlFoto.Equals(Constanst.CAR_IMAGE_DEFAULT))
                form.Add(new StreamContent(MediaManager.GetImageStream(UrlFoto)), Constanst.NAME_IMAGE, "imgUserUpdadate.jgp");


            requestMessage = await cliente.PostAsync(string.Concat(url, "/add"), form);
            var contents = await requestMessage.Content.ReadAsStringAsync();
            if (requestMessage.IsSuccessStatusCode)
            {
                var respuesta = JsonConvert.DeserializeObject<Response>(contents);
                if (respuesta.Status.Equals("ok"))
                {
                    UrlFoto = respuesta.Message;
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

        

        /*Abre la camarara*/
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

        //Se carga la foto obtenidad de la galeria o camara y la almacena en las variables para mostrarla en la vista
        private void LoadPhoto(bool isSucces, MediaManager media)
        {
            if (isSucces)
            {

                UrlPhoto = media.Path;
                TakeNewPhoto = true;
            }
        }

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

        public async void OnSelectedMedia()
        {

            string action = await Page.DisplayActionSheet("Abrir", "Cancel", null, Constanst.CAMARA, Constanst.GALERIA);
            Console.WriteLine("Seleccionó " + action);

            if (action == Constanst.CAMARA)
            {
                OnOpenGalery();

            }
            else if (action == Constanst.GALERIA)
            {
                OnTakePhoto();
            }
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