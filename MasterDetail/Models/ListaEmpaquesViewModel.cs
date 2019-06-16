using GalaSoft.MvvmLight.Command;
using MasterDetail.Servicio;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Newtonsoft.Json;

namespace MasterDetail.Models
{
    public class ListaEmpaquesViewModel : BaseFodyObservable
    {
        private Service service { get; set; }
        public bool IsChecked { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ObservableCollection<EmpaqueModel> lista {get;set;}
        public ListaEmpaquesViewModel()
        {
            this.service = new Service();
            
        }

        //private async ObservableCollection<EmpaqueModel> Llenar()
        //{

        //    var respuesta = await Service.GetAllApi("api/User");
        //    ObservableCollection<EmpaqueModel> empaques = JsonConvert.DeserializeObject<ObservableCollection<EmpaqueModel>>(respuesta);
        //    return empaques;
        //}
        


        public ICommand EnviarCommand
        {
            get
            {
                return new RelayCommand(Enviar);
            }
        }

        private void Enviar()
        {
            
        }

        public ObservableCollection<ListaEmpaquesViewModel> Items { get; set; }


    }
}
