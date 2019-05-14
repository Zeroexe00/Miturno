using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Linq;
using MasterDetail.Models;
using System.Threading.Tasks;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace MasterDetail
{
    public partial class App : Application
    {
        public static MasterDetailPage MasterD { get; set; }
        public const string NotificationReceivedKey = "NotificationReceived";
        public  App()
        {
            InitializeComponent();

            //Solucionar Problema de Asincronicos
            //using(var datos = new DataService())
            //{
            //    if ( emp != null)
            //    {
                   // MainPage = new NavigationPage(new MainPage(emp));

                //}
                //else
                //{
                    MainPage = new NavigationPage(new Login());

            //    }
            //}
                

            


        }

        

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
