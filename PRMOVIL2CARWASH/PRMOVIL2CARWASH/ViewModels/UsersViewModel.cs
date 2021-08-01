using PRMOVIL2CARWASH.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PRMOVIL2CARWASH.ViewModels
{
   public class UsersViewModel:BaseViewModel
    {
        public Command SendVerifyCommand { get; }
        public Command ThakePhotoCommand { get; }
        public Command OpenGaleryCommand { get; }
        Page Page;

        string name;
        string lastName;
        string address;
        string mail;
        string telephone;
        string user;
        string password;
        byte[] foto;
        ImageSource photo = ImageSource.FromUri(new Uri("https://images.vexels.com/media/users/3/204554/isolated/lists/53193fd7db3d2618ab56635e69e64515-pequenas-hojas-de-frutos-rojos.png"));
        
        public string Name { get => name;
            set => name = value; }
        public string LastName { get => lastName; set => lastName = value; }
        public string Address { get => address; 
            set => address = value; }
        public string Mail { get => mail; set => mail = value; }
        public string Telephone { get => telephone; 
            set => telephone = value; }
        public string User { get => user; 
            set => user = value; }
        public string Password { get => password; set => password = value; }
        public byte[] Foto { get => foto; set => foto = value; }
        public ImageSource Photo
        {
            get => photo;
            set
            {
                SetProperty(ref photo, value);
            }
        }

        public UsersViewModel(Page pag)
        {
            Page = pag;
            SendVerifyCommand = new Command(OnRequestVerify);
            OpenGaleryCommand = new Command(OnOpenGalery);

            ThakePhotoCommand = new Command(OnTakePhoto);
           
        }

        private async void OnRequestVerify(object obj)
        {
            if (!string.IsNullOrEmpty(Name))
            {
                await Page.DisplayAlert("Su nombre es: ", Name, "Ok");
            }
            else 
            {
                await Page.DisplayAlert("El nombre no puede ir vacio", Name, "ok");
            }
        }


        private async void OnOpenGalery()
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
