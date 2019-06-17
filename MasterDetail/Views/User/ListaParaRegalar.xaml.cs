using MasterDetail.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

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
    }
}