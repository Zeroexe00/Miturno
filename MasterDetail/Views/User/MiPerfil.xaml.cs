using MasterDetail.Servicio;
using MasterDetail;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using System.Net.Http;
using System.Text.RegularExpressions;

namespace MasterDetail
{
    public partial class MiPerfil : ContentPage
    {
        EmpaqueModel empa;
        List<Supermercado> listaSuperm;

        public MiPerfil(EmpaqueModel empaque)
        {
            this.empa = empaque;
            InitializeComponent();
            Carga(empa);
        }

        private async void Carga(EmpaqueModel empaque)
        {
            await CargaPerfil(empaque);
        }

        private async Task CargaPerfil(EmpaqueModel emp)
        {
            string response = await Service.GetAllApi("api/supermarket");
            List<Supermercado> supermercados2 = JsonConvert.DeserializeObject<List<Supermercado>>(response);
            lblSuperm.ItemsSource = supermercados2;
            listaSuperm = supermercados2;
            Supermercado super = new Supermercado();

            foreach (var item in listaSuperm as List<Supermercado>)
            {
                if (item.Id == empa.Supermarket)
                {
                    super = item;
                }
            }

            lblSuperm.SelectedItem = super;

            List<Genero> llenar = new List<Genero>();
            Genero f = new Genero()
            {
                Id = 0,
                Sexo = "Femenino"
            };

            llenar.Add(f);

            Genero m = new Genero()
            {
                Id = 1,
                Sexo = "Masculino"
            };

            llenar.Add(m);

            Genero o = new Genero()
            {
                Id = 2,
                Sexo = "Otro"
            };

            llenar.Add(o);

            PckGender.ItemsSource = llenar;
            PckGender.SelectedIndex = Convert.ToInt32(emp.Gender);
            lblRut.Text = emp.Rut.ToString();
            lblEmail.Text = emp.Email;
            lblName.Text = emp.FirstName;
            lblLastname.Text = emp.LastName;
            lblPass.Text = emp.Password;
            dtpNacimiento.Date = emp.BirthDate;
            lblAdress.Text = emp.Address;
            lblPhone.Text = emp.PhoneNumber.ToString();
            lblJobTitle.Text = emp.JobTitle;
            }

            public class Genero
            {
                public int Id
                {
                    get; set;
                }

                public string Sexo
                {
                    get; set;
                }
            }

        private void lblRut_Validated(object sender, EventArgs e)
        {
            if (lblRut.Text.Length > 1)
            {

            lblRut.Text = formatearRut(lblRut.Text);

            }else
            {
                return;
            }
        }

        public string formatearRut(string rut)
        {
            int cont = 0;
            string format;
            if (rut.Length == 0)
            {
                return "";
            }

            else
            {
                rut = rut.Replace(".", "");
                rut = rut.Replace("-", "");
                format = "-" + rut.Substring(rut.Length - 1);
                for (int i = rut.Length - 2; i >= 0; i--)
                {
                    format = rut.Substring(i, 1) + format;
                    cont++;
                    if (cont == 3 && i != 0)
                    {
                        format = "." + format;
                        cont = 0;
                    }
                }
                return format;
            }
        }

        public void ShowPass(object sender, EventArgs args)
        {
            lblPass.IsPassword = !lblPass.IsPassword;
        }

        private async void Btn_EditarPerfil_Clicked(object sender, EventArgs e)
        {
            Btn_EditarPerfil.IsEnabled = false;
            waitActivityIndicator.IsRunning = true;
            Supermercado super = new Supermercado();
            foreach (var item in listaSuperm as List<Supermercado>)
            {
                if (item.Equals (lblSuperm.SelectedItem))
                {
                    super.Id = item.Id;
                }
            }

            EmpaqueModel empaque = new EmpaqueModel()
            {
                Id = empa.Id,
                Rut = Convert.ToInt32(lblRut.Text.Replace(".","").Replace("-","")),
                Email = lblEmail.Text,
                Password = lblPass.Text,
                FirstName = lblName.Text,
                LastName = lblLastname.Text,
                BirthDate = dtpNacimiento.Date,
                Gender = PckGender.SelectedIndex,
                Address = lblAdress.Text,
                PhoneNumber = Convert.ToInt32(lblPhone.Text),
                Supermarket = super.Id,
                JobTitle = lblJobTitle.Text
            };

            try
            {
                HttpResponseMessage response = await Service.Put("api/user/" + empa.Id.ToString(), empaque);
                if (response.StatusCode != System.Net.HttpStatusCode.NotFound)
                {
                    waitActivityIndicator.IsRunning = false;
                    await DisplayAlert("Cambios Guardados", "Cambios realizados con éxito", "Ok");
                    Btn_EditarPerfil.IsEnabled = true;
                    return;
                }

                else
                {
                    waitActivityIndicator.IsRunning = false;
                    await DisplayAlert("Error de conexión", "Error inesperado al editar sus datos", "Ok");
                    Btn_EditarPerfil.IsEnabled = true;
                    return;
                }
            }

            catch (Exception ex)
            {
                Btn_EditarPerfil.IsEnabled = true;
                await DisplayAlert("Error de edición", "Error al editar empaque "+ ex, "Ok");
                return;
            }
        }

        
    }
}