using MasterDetail.Servicio;
using MasterDetail;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MasterDetail.Views.User
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Register : ContentPage
    {
        private List<Supermercado> supermercados;
        public Register()
        {
            InitializeComponent();
            Cargar();
        }

        private void Cargar()
        {
            Cargapicker();
        }

        private async void Cargapicker()
        {
            string response = await Service.GetAllApi("api/supermarket");
            List<Supermercado> supermercados2 = JsonConvert.DeserializeObject<List<Supermercado>>(response);
            pckSupermarket.ItemsSource = supermercados2;
            supermercados = supermercados2;
        }

        public void ShowPass(object sender, EventArgs args)
        {
            Contraseña.IsPassword = !Contraseña.IsPassword;
        }

        private async void Registrar(object sender, EventArgs e)
        {
            string email = Email.Text;
            string pass = Contraseña.Text;
            Supermercado sup = (Supermercado)pckSupermarket.SelectedItem;
            EmpaqueModel empaque = new EmpaqueModel()
            {
                Email = email,
                Password = pass,
                Supermarket = sup.Id
            };

            waitActivityIndicator.IsRunning = true;

            HttpResponseMessage response = await Service.Post("api/User", empaque);
            if (response.StatusCode != System.Net.HttpStatusCode.NotFound)
            {
                    await DisplayAlert("¡Bienvenido!", "Registro realizado con éxito", "Ok");
                    await Navigation.PushAsync(new Login());
            }
            else

            {
                await DisplayAlert("Error de registro", "Información de usuario no corresponde", "Ok");
            }
        }
    }
}