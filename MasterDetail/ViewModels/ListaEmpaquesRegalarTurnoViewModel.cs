﻿using GalaSoft.MvvmLight.Command;
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
    public class ListaEmpaquesRegalarTurnoViewModel : BaseFodyObservable
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
        public int EmpID { get; set; }
            public string FullName { get; set; }
            public int Supermarket { get; set; }
            public int EmpaqueSolicita { get; set; }
            public int Turno { get; set; }
            public int TrazaTurno { get; set; }
        }

        public ListaEmpaquesRegalarTurnoViewModel(int id,int spmt, int TurnID)
        {
            Llenar(id,spmt,TurnID);
            //PostRegalar = new Command<ObservableCollection<Empaque>>(HandleRegalar);
        }

        private async void Llenar(int id,int spmt,int TurnID)
        {
            try 
            {
                var respuesta = await Service.GetAllApi("api/GetListaEmpaquesSupermarket?id=" + id.ToString()+"&spmt="+spmt.ToString()+"&TurnID="+ TurnID.ToString());
                ObservableCollection<Empaque> empaques = JsonConvert.DeserializeObject<ObservableCollection<Empaque>>(respuesta);
                ListaEmpaques = empaques;
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("error ", ex.Message, "Aceptar");

            }

        }

        



    }
}
