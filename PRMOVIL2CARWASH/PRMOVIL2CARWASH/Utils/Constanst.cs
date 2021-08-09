using System;
using System.Collections.Generic;
using System.Text;

namespace PRMOVIL2CARWASH.Utils
{
     public class Constanst
    {

        public const string USER_CACHE = "usuariocache";
        public const string CAMARA= "Camara";
        public const string GALERIA = "Galería";
        public const string VERIFY_PHONE_NUMBER = "phoneNumber";
        public const string CURRENT_USER = "currentUser";
        public const string DEFAULT_USER = "defaultUser";
        public const string STATE_SESSION = "stateSesion";
        public const string RECENT_SESSION = "resentSession";
        public const string TYPE_CHANGE = "change";
        public const string TYPE_RESET= "reset";
        public const string VERIFY_MAIL = "mail";
        private const string UrlBase = "http://173.249.21.6/v1";
        public const int  REQUEST_OK= 201;
        public const int REQUEST_ERROR= 400;
        public const int SESSION_CLOSED = 3;
        public const int EXPIRE_DAYS = 7;
        public const int EXPIRE_CURREN_USER = 365;
        public const int FAIL_AUTH = 35;
        public const int NO_MATCH_PASS = 21;


        public const string USER_IMAGE_DEFAULT = "camara.png";
        public static string TOKEN { get; set; }
        public const int USER_EXIST = 350;

        public const int USER_NO_EXIST = 365;
        public const int LENGTH_CODE = 4;

        public const int YEAR_INIT = 1950;
        public static string GetUrl(String route)
        {
           return string.Concat(UrlBase, route);
           
        }

        
    }
}
