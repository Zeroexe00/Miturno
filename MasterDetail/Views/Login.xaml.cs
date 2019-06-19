using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using MasterDetail.Servicio;
using MasterDetail.Views.User;
using MasterDetail;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Newtonsoft.Json;
using MasterDetail.Views;
using MasterDetail.Interface.firebasesample.Services.FirebaseAuth;
using Acr.UserDialogs;
using MasterDetail.Models;
using MasterDetail.Helpers;

namespace MasterDetail
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Login : ContentPage
    {
        
        public Login()
        
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        public void ShowPass(object sender, EventArgs args)
        {
            Pass.IsPassword = !Pass.IsPassword;
        }

        private async void Ingresar(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty (Email.Text))
            {
                await DisplayAlert("Error de Acceso", "Debe ingresar email valido.", "Ok");
            }

            if (string.IsNullOrEmpty(Pass.Text))
            {
                await DisplayAlert("Error de Acceso", "Debe ingresar contraseña.", "Ok");
            }
                waitActivityIndicator.IsRunning = true;

                EmpaqueModel empaque = new EmpaqueModel() { Email = Email.Text, Password = Pass.Text };

            try { 
                HttpResponseMessage response = await Service.Post("api/User/Authenticate", empaque);

                if (response.StatusCode != System.Net.HttpStatusCode.NotFound)
                {
                    var empty = await Service.GetOneApi("api/User/Authenticate", empaque);
                    EmpaqueModel emp = JsonConvert.DeserializeObject<EmpaqueModel>(empty.ToString());
                    
                    if (remenberMeSwitch.IsToggled) 
                    {

                        Settings.Empaque = emp.Email; 
                        Settings.EmpaquePass = emp.Password; 
                        Settings.Remember = true;
                        await Navigation.PushAsync(new MainPage(emp));

                    }

                    waitActivityIndicator.IsRunning = false;

                await Navigation.PushAsync(new MainPage(emp));
                }

                else
                {
                    waitActivityIndicator.IsRunning = false;
                    await DisplayAlert("Error de Acceso", "Informacion de usuario no corresponde", "Ok");
                }
            }catch(Exception ex) { 
                    waitActivityIndicator.IsRunning = false;
                await DisplayAlert("Error de Acceso", "Compruebe conexion a internet   "+ex, "Ok");

            }
        } 

        private void Btn_Register(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Register());
        }
    }
}