using MasterDetail.Servicio;
using MasterDetail;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using Plugin.Connectivity;

namespace MasterDetail
{
    public partial class ListaTurnos : ContentPage
    {
        List<Turnos> gTurnos = new List<Turnos>();
        EmpaqueModel empaq = new EmpaqueModel();
        public ListaTurnos(EmpaqueModel empaque)
        {
            this.empaq = empaque;
            InitializeComponent();
            Cargar();
        }

        private void Cargar()
        {
            GrillaTurnosAsync();
        }

        private async Task GrillaTurnosAsync()
        {
            waitActivityIndicator.IsRunning = true;

            try
            {

                string response = await Service.GetAllApi("api/turn");

                List<Turnos> turnos = JsonConvert.DeserializeObject<List<Turnos>>(response);
                gTurnos = turnos;
                waitActivityIndicator.IsRunning = false;

                LV_Turnos.ItemsSource = turnos;
                

            }

            catch (Exception ex)
            {
                waitActivityIndicator.IsRunning = false;
                await DisplayAlert("Fallo", "Error al cargar turnos " + ex, "OK");
            }
            waitActivityIndicator.IsRunning = false;
        }

        private void TomarTurno(object sender, ItemTappedEventArgs e)
        {
            waitActivityIndicator.IsRunning = true;
            LV_Turnos.IsEnabled = false;
            Turnos item = (Turnos)e.Item;
            TomandoT(item);

            foreach (var turn in LV_Turnos.ItemsSource as List<Turnos>)
            {
                if (item.Id == turn.Id)
                {
                    gTurnos.Remove(turn);
                    LV_Turnos.ItemsSource = null;
                    LV_Turnos.ItemsSource = gTurnos;
                    return;
                }
            }
        }

        private void TomandoT(Turnos item)
        {
            TomarT(item);

        }

        private async Task TomarT(Turnos item)
        {
            TraceabilityWorkShift turnotomado = new TraceabilityWorkShift()
            {
                Id = 0,
                ActualState = "0",
                EffectiveQuantity = 1,
                Id_Wor = item.Id,
                UserID = empaq.Id
            };

            if (CrossConnectivity.Current.IsConnected)
            {

            }
            else

            {


                return;
            }

            HttpResponseMessage response = await Service.Post("api/TraceabilityWorkShift", turnotomado);

            if (response.StatusCode != System.Net.HttpStatusCode.NotFound && response.StatusCode != System.Net.HttpStatusCode.NoContent && response.StatusCode != System.Net.HttpStatusCode.InternalServerError)
            {
                waitActivityIndicator.IsRunning = false;
                await DisplayAlert("¡Éxito!", "Turno asignado exitosamente", "Aceptar", "Cancelar");
                LV_Turnos.IsEnabled = true;
            }

            else
            {
                LV_Turnos.IsEnabled = true;

                await DisplayAlert("Fallo", "Error al asignar turno", "OK");

            }
            //}
            //catch (Exception ex)
            //{
            //    LV_Turnos.IsEnabled = true;

            //    await DisplayAlert("Fallo", "Error al asignar turno " + ex, "OK");
            //}
        }



    }
}
