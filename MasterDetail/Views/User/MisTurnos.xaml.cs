using MasterDetail.Servicio;
using MasterDetail;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Net.Http;
using System.Windows.Input;

namespace MasterDetail
{
    public partial class MisTurnos : ContentPage
    {
        EmpaqueModel em = new EmpaqueModel();
        public MisTurnos(EmpaqueModel empaque)
        {
            em = empaque;
            InitializeComponent();
            Cargar();
            NavigationPage.SetBackButtonTitle(this, "MiTurnoAPP");
            
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
                
                    List<TraceabilityWorkShift> turnosTomados = new List<TraceabilityWorkShift>();
                  
                    if (Mis_Turnos.ItemsSource == null || turnosTomados.Count == 0)
                    {
                        string response = await Service.GetAllApi("api/TraceabilityWorkShiftsByEmpaque?Id=" + em.Id.ToString());

                        List<TraceabilityWorkShift> Mturnos = JsonConvert.DeserializeObject<List<TraceabilityWorkShift>>(response);

                        waitActivityIndicator.IsRunning = false;

                       

                        Mis_Turnos.ItemsSource = Mturnos;
                    }

                    else
                    {
                        Mis_Turnos.ItemsSource = turnosTomados;
                    }
                
            }

            catch (Exception ex)
            {
                waitActivityIndicator.IsRunning = false;
                await DisplayAlert("Error", "Fallo la conexión :  " + ex, "Ok");
            }
        }

        private async void RegalarTurno_Clicked(object sender, EventArgs e)
        {

            Button buton = (Button)sender;
            Grid grid = (Grid)buton.Parent;
            Label label = (Label)grid.Children[5];


            string Label = label.Text;
            
            try {

                //   HttpResponseMessage response = await Service.Delete("api/TraceabilityWorkShift/" + turnoRegalado.Id.ToString());
                //if (response.StatusCode != System.Net.HttpStatusCode.NotFound && response.StatusCode != System.Net.HttpStatusCode.InternalServerError)
                //{
                //    await DisplayAlert("Éxito", "Tu turno ha sido regalado", "Ok");
                //}
            }
            catch (Exception ex)
            {
                await DisplayAlert("Fallo", "Error " + ex, "Ok");

            }
        }
        private ICommand _deleteVolumeCommand;
        public ICommand Regalo
        {
            get
            {
                {
                    if (_deleteVolumeCommand == null)
                    {
                        _deleteVolumeCommand = new Command((e) =>
                        {
                            var item = (e as TraceabilityWorkShift);
                            item.ToString();
                        });
                    }

                    return _deleteVolumeCommand;
                }

            }
        }
        private void IntercambiarTurno_Clicked(object sender, EventArgs e)
        {

        }
    }
}
