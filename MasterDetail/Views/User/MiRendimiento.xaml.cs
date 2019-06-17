using MasterDetail.Models;
using MasterDetail.Servicio;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace MasterDetail
{
    public partial class MiRendimiento : ContentPage
    {
        public MiRendimiento()
        {
            InitializeComponent();
            //IniciarLista();
            NavigationPage.SetBackButtonTitle(this, "MiTurnoAPP");
        }

        //private async void IniciarLista()
        //{
        //    string Usuarios = await Service.GetAllApi("api/User");
        //    ObservableCollection<ListaChecks> lista = new ObservableCollection<ListaChecks>();
        //    List<EmpaqueModel> empaques = JsonConvert.DeserializeObject<List<EmpaqueModel>>(Usuarios);
        //    foreach(var item in empaques)
        //    {
        //        ListaChecks lis = new ListaChecks();
        //        lis.Title = item.FirstName;
                
        //        lis.IsChecked = false;

        //        lista.Add(lis);
                
        //    }
        //    Empaques.ItemsSource = lista;

        //}
    }
}
