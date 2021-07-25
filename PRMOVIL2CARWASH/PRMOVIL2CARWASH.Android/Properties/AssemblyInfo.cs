using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Android.App;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]


// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("prmovil2carwash.android")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("")]
[assembly: AssemblyProduct("prmovil2carwash.android")]
[assembly: AssemblyCopyright("copyright ©  2014")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]
[assembly: ComVisible(false)]


//Fonts
[assembly: ExportFont("Questrial_Regular.ttf", Alias = "Questrial")]
[assembly: ExportFont("Quattrocento_Bold.ttf", Alias = "Bold")]
[assembly: ExportFont("Quattrocento_BoldItalic.ttf", Alias = "BoldItalic")]
[assembly: ExportFont("Quattrocento_Italic.ttf", Alias = "Italic")]
[assembly: ExportFont("Quattrocento_Regular.ttf", Alias = "Quattrocento_Regular")]
[assembly: ExportFont("Racing_Regular.ttf", Alias = "Racing")]
//[assembly: ExportFont("Encode.ttf", Alias = "Encode")]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
[assembly: AssemblyVersion("1.0.0.0")]
[assembly: AssemblyFileVersion("1.0.0.0")]

// Add some common permissions, these can be removed if not needed
[assembly: UsesPermission(Android.Manifest.Permission.Internet)]
[assembly: UsesPermission(Android.Manifest.Permission.WriteExternalStorage)]
