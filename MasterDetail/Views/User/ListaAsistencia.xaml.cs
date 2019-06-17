using MasterDetail.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static MasterDetail.ViewModels.ListaEmpaquesViewModel;

namespace MasterDetail.Views.User
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListaAsistencia : ContentPage
    {
        public ListaAsistencia(int id)
        {
            InitializeComponent();
            BindingContext = new ListaEmpaquesViewModel(id);

        }

        private void Enviar_Clicked(object sender, EventArgs e)
        {
            var empaques = ListaEmp.ItemsSource;


        }

        private void Handle_Toggled(object sender, ToggledEventArgs e)
        {
            ObservableCollection<Empaque> emp = new ObservableCollection<Empaque>();
            var items = ListaEmp.ItemsSource;
            if (swtAll.IsToggled)
            {
                foreach(var item in (ObservableCollection<Empaque>)ListaEmp.ItemsSource)
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
    }
}