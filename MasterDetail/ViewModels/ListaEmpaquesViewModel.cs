using GalaSoft.MvvmLight.Command;
using MasterDetail.Servicio;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace MasterDetail.ViewModels
{
    public class ListaEmpaquesViewModel : BaseFodyObservable
    {
        public ObservableCollection<Empaque> ListaEmpaques
        {
            get { return this.lista; }
            set
            {
                if (value == lista) return;
                lista = value;

            }
        }
        private ObservableCollection<Empaque> lista;

        public class Empaque
        { 
        public bool IsChecked { get; set; }
        public string FullName { get; set; }

        }
        
        public ListaEmpaquesViewModel(int id)
        {
            Llenar(id);
            PostAsistencia = new Command<ObservableCollection<Empaque>>(HandlePostAsistencia);
        }

        private async void Llenar(int id)
        {
            try
            {
                var respuesta = await Service.GetAllApi("api/GetListaEmpaquesTurno?id=" + id.ToString());
                ObservableCollection<Empaque> empaques = JsonConvert.DeserializeObject<ObservableCollection<Empaque>>(respuesta);
                ListaEmpaques = empaques;
            }catch(Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("error ", ex.Message, "Aceptar");

            }

        }

        public Command<ObservableCollection<Empaque>> PostAsistencia { get; set; }
        public async void HandlePostAsistencia(ObservableCollection<Empaque> empaque)
        {
            ObservableCollection<Empaque> e = empaque;
            
            foreach(var item in e)
            {
                string hola;
                hola=item.FullName;
            }
            await Application.Current.MainPage.DisplayAlert("hola ", e.ToString(), "Aceptar");


        }



    }
}
