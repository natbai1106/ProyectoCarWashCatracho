using Acr.UserDialogs;
using PRMOVIL2CARWASH.Models;
using PRMOVIL2CARWASH.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Essentials;
using Xamarin.Forms;
using System.Linq;
namespace PRMOVIL2CARWASH.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public IDataStore<Item> DataStore => DependencyService.Get<IDataStore<Item>>();

        public BaseViewModel()
        {
            Connectivity.ConnectivityChanged += Connectivity_ConnectivityChanged;
            
            //Validamos el estado del internet
            CheckNetworkState(Connectivity.NetworkAccess, Connectivity.ConnectionProfiles);
            
        }
        bool isBusy = false;
        bool isEmpty = true;
        bool isNotConnect;
        bool wasChanged = false;
        string networkMessage;
        string title = string.Empty;
        /*Se utilizan para validar el estado de la conexión*/

        public bool IsNotConnect
        {
            get { return isNotConnect; }
            set { SetProperty(ref isNotConnect, value); }
        }
        
        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }

        public bool IsEmpty
        {
            get { return isEmpty; }
            set { SetProperty(ref isEmpty, value); }
        }

        
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        public string NetworkMessage { get => networkMessage; set => SetProperty(ref networkMessage, value);  }
        public bool WasChanged { get => wasChanged; set => SetProperty(ref wasChanged, value); }

        protected bool SetProperty<T>(ref T backingStore, T value, [CallerMemberName] string propertyName = "", Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region NetworkState
       
         void Connectivity_ConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
           var access = e.NetworkAccess;
            var profiles = e.ConnectionProfiles;
            //Se valida el tipo de perfil de conexión

            CheckNetworkState(access, profiles);
           
            
        }


        void CheckNetworkState(NetworkAccess access, IEnumerable<ConnectionProfile> profiles)
        {
            //if (profiles.Contains(ConnectionProfile.Cellular) || profiles.Contains(ConnectionProfile.WiFi))
            //{

                
            switch (access)
                {
                    case NetworkAccess.ConstrainedInternet:
                        NetworkMessage = "Acceso a internet limitado";
                        IsNotConnect = true;
                        break;
                    case NetworkAccess.Internet:
                        IsNotConnect = false;
                        break;
                    case NetworkAccess.Unknown:
                        NetworkMessage = "No ha sido posible determinar la conectiviad a internet";
                        IsNotConnect = true;
                        break;
                    case NetworkAccess.None:
                        NetworkMessage = "Sin acceso a internet";
                        IsNotConnect = true;
                        break;
                    case NetworkAccess.Local:
                        NetworkMessage = "Connectivad local";
                        IsNotConnect = true;
                        break;
                }

            //}
            //else if(profiles.Contains(ConnectionProfile.Unknown))
            //{
            //    NetworkMessage = "Sin acceso a internet";
            //    IsNotConnect = true;
            //}

        }
        #endregion



    }
}
