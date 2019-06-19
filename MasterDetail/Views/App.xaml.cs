﻿using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Linq;
using MasterDetail.Models;
using System.Threading.Tasks;
using MasterDetail.Helpers;
using MasterDetail.Servicio;
using Newtonsoft.Json;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace MasterDetail
{
    public partial class App : Application
    {
        public static MasterDetailPage MasterD { get; set; }
        public const string NotificationReceivedKey = "NotificationReceived";
        public const string MobileServiceUrl = "https://miturno.azurewebsites.net";
        public App()
        {
            InitializeComponent();

           if(Settings.Remember && Settings.Empaque!=string.Empty && Settings.EmpaquePass != string.Empty)
            {
                EmpaqueModel empaque = new EmpaqueModel()
                {
                    Email = Settings.Empaque,
                    Password = Settings.EmpaquePass
                };
                
                var empty = Obtener(empaque).Result;
                EmpaqueModel emp = JsonConvert.DeserializeObject<EmpaqueModel>(empty.ToString());

                MainPage = new NavigationPage(new MainPage(emp));
            }
            else
            {
                MainPage = new NavigationPage(new Login());
            }
                

        }

        private async Task<string> Obtener(EmpaqueModel empaque)
        {
            var empty = await Service.GetOneApi("api/User/Authenticate", empaque);

            return empty;
        }
        //private bool Exists()
        //{
        //   var emp = Existe();
        //    if ()
        //    {
        //        return true;

        //    }
        //    else
        //    {
        //        return false;

        //    }
        //}
        private async Task<EmpaqueModel> Existe()
        {
            using (var datos = new DataService())
            {
                EmpaqueModel emp = await datos.GetEmpaque();
                if (emp != null)
                {
                    return emp;
                }
                else
                {
                    return null;
                }
            }
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
