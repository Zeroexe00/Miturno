using MasterDetail.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
        }

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
        private void Enviar_Clicked(object sender, EventArgs e)
        {
            var empaques = (ObservableCollection<Empaque>)ListaEmp.ItemsSource;
            int checks = 0;
            if (empaques.Count != 0)
            {
                foreach (var item in empaques)
                {
                    if (item.IsChecked && (checks<1||checks==0))
                    {
                        checks++;
                        TraceabilityWorkShift shift = new TraceabilityWorkShift();
                        shift.ActualState = "2";

                    }    
                }
            }
            else
            {
                DisplayAlert("Error", "No se encontraron empaques para regalar", "Aceptar");
            }
        }
    }

}