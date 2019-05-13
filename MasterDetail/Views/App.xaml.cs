using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Linq;
using MasterDetail.Models;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace MasterDetail
{
    public partial class App : Application
    {
        public static MasterDetailPage MasterD { get; set; }

        public App()
        {
            InitializeComponent();
            
                
         MainPage = new NavigationPage(new Login());
                
            
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
