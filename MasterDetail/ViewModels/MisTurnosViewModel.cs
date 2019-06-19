using MasterDetail.Servicio;
using MasterDetail.Views.User;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MasterDetail.ViewModels
{
    public class MisTurnosViewModel : BaseFodyObservable
    {
        public class MisTurno : BaseFodyObservable
        {
            public int id { get; set; }
            public int TurnID { get; set; }
            public int spmt { get; set; }
            public string EmpaqueState { get; set; }
            public string FechaTurno { get; set; }
            public string HoraInicio { get; set; }
            public string HoraFinaliza { get; set; }
        }
        public ObservableCollection<MisTurno> Lista
        {
            get { return this.lista; }
            set
            {
                if (value == lista) return;
                lista = value;
                
            }
        }
            private  ObservableCollection<MisTurno> lista;


        private EmpaqueModel empaque { get; set; }
        public MisTurnosViewModel(EmpaqueModel empa)
        {
            this.empaque = empa;
            GetListaMisTurno(this.empaque);
            Asistencia = new Command<MisTurno>(HandleAsistencia);
            Regalar = new Command<MisTurno>(HandleRegalar);

        }

        private async void GetListaMisTurno(EmpaqueModel model)
        {
            try
            {
                string response = await Service.GetAllApi("api/GetMisTurnos?Id=" + model.Id.ToString());

                ObservableCollection<MisTurno> Mturnos = JsonConvert.DeserializeObject<ObservableCollection<MisTurno>>(response);

                Lista = Mturnos;



            }
            catch(Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error ", ex.Message, "Aceptar");

            }
        }

        

        public Command<MisTurno> Asistencia { get; set; }
        public async void HandleAsistencia(MisTurno turno)
        {

            await App.MasterD.Detail.Navigation.PushAsync(new ListaAsistencia(turno.id));

        }

        public Command<MisTurno> Regalar { get; set; }
        public async void HandleRegalar(MisTurno turno)
        {
            await App.MasterD.Detail.Navigation.PushAsync(new ListaParaRegalar(turno.id,turno.spmt,turno.TurnID));

        }




    }
}
