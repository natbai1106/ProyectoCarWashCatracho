using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Java.Security;
using Java.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PRMOVIL2CARWASH.Droid.HashKey
{
    public class AppHashKeyHelper
    {
        private static string HASH_TYPE = "SHA-256";//Se establece el tipo de algoritmo que se va utilizar para generar el hashs
        private static int NUM_HASHED_BYTES = 9; //El numero de bytes que vamos a generar
        private static int NUM_BASE64_CHAR = 11; //Por ultimo la la numero de caracteres que vamos a obtener



        [Obsolete]
        private static string GetPackageSignature(Context context)
        {
            PackageManager packageManager = context.PackageManager;
            var signatures = packageManager.GetPackageInfo(context.PackageName, PackageInfoFlags.Signatures).Signatures;
            // return signatures.ToString();//Obtemos el nombre del paquete 
            return signatures.First().ToCharsString();//Obtemos firmas del paquete 
        }

        [Obsolete]
        public string GetAppHashKey(Context context)
        {
            string keystoreHexSignature = GetPackageSignature(context);

            String appInfo = context.PackageName + " " + keystoreHexSignature;
            try
            {
                MessageDigest messageDigest = MessageDigest.GetInstance(HASH_TYPE);
                messageDigest.Update(Encoding.UTF8.GetBytes(appInfo));
                byte[] hashSignature = messageDigest.Digest();
                hashSignature = Arrays.CopyOfRange(hashSignature, 0, NUM_HASHED_BYTES);
                String base64Hash = global::Android.Util.Base64.EncodeToString(hashSignature, Base64Flags.NoPadding | Base64Flags.NoWrap);
                base64Hash = base64Hash.Substring(0, NUM_BASE64_CHAR);

                return base64Hash; //Retorna el Hash que identifica nuestro paquete
            }
            catch (NoSuchAlgorithmException e)
            {
                return null;
            }
        }
    }
}