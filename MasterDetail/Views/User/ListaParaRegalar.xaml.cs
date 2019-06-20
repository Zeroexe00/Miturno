using MasterDetail.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static MasterDetail.ViewModels.ListaEmpaquesRegalarTurnoViewModel;

namespace MasterDetail.Views.User
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListaParaRegalar : ContentPage
    {
        public ListaParaRegalar(int id,int spmt,int TurnID)
        {
            InitializeComponent();
            BindingContext = new ListaEmpaquesRegalarTurnoViewModel(id,spmt,TurnID);
            _client.BaseAddress = new Uri(App.MobileServiceUrl);
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _client.Timeout = TimeSpan.FromSeconds(120);
        }
        HttpClient _client = new HttpClient();

        private void Handle_Toggled(object sender, ToggledEventArgs e)
        {
            ObservableCollection<Empaque> emp = new ObservableCollection<Empaque>();
            var items = ListaEmp.ItemsSource;
            if (swtAll.IsToggled)
            {
                foreach (var item in (ObservableCollection<Empaque>)ListaEmp.ItemsSource)
                {
                    item.IsChecked = true;
                    emp.Add(item);

                }
                ListaEmp.ItemsSource = emp;
            }
            else
            {
                foreach (var item in (ObservableCollection<Empaque>)ListaEmp.ItemsSource)
                {
                    item.IsChecked = false;
                    emp.Add(item);

                }
                ListaEmp.ItemsSource = emp;
            }
        }
        private async void Enviar_Clicked(object sender, EventArgs e)
        {
            var empaques = (ObservableCollection<Empaque>)ListaEmp.ItemsSource;
            int checks = 0;
            if (empaques.Count != 0)
            {
                foreach (var item in empaques)
                {
                    if (item.IsChecked && (checks < 1 || checks == 0))
                    {
                        checks++;
                        TraceabilityWorkShift shift = new TraceabilityWorkShift
                        {
                            Id = item.TrazaTurno,
                            ActualState = "2",
                            EffectiveQuantity = 1,
                            UserID = item.EmpaqueSolicita,
                            Id_Wor = item.Turno
                        };
                        TraceabilityWorkShift shift1 = new TraceabilityWorkShift
                        {
                            Id = 0,
                            ActualState = "1",
                            EffectiveQuantity = 1,
                            UserID = item.EmpID,
                            Id_Wor = item.Turno
                        };

                        

                        var response = await Servicio.Service.Post("api/PostRegalarIntercambiar", shift);

                        var response1 = await Servicio.Service.Post("api/TraceabilityWorkShift", shift1);

                        string txtMsg = "Se regalo un turno correctamente.";
                        var content = new StringContent("\"" + txtMsg + "\"", Encoding.UTF8, "application/json");
                        var result = await _client.PostAsync("xamunotifications", content);

                        await DisplayAlert("Exito!", "Se regalo turno correctamente", "Aceptar");


                    }
                }
            }
            else
            {
               await DisplayAlert("Error", "No se encontraron empaques para regalar", "Aceptar");
            }
        }
    }

}