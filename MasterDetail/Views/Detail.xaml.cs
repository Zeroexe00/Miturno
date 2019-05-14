using MasterDetail;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using Xamarin.Forms;

namespace MasterDetail
{
    public partial class Detail : ContentPage
    {
        EmpaqueModel empa = new EmpaqueModel();
        public Detail(EmpaqueModel empaque)
        {
            this.empa = empaque;
            InitializeComponent();
            Bienvenido.Text = string.Format("Bienvenid@ : {0} {1}", empaque.FirstName, empaque.LastName);
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();

            MessagingCenter.Subscribe<object, string>(this, App.NotificationReceivedKey, OnMessageReceived);
            btnSend.Clicked += OnBtnSendClicked;
        }
        void OnMessageReceived(object sender, string msg)
        {
            Device.BeginInvokeOnMainThread(() => {
                lblMsg.Text = msg;
            });
        }

        async void OnBtnSendClicked(object sender, EventArgs e)
        {
            Debug.WriteLine($"Sending message: " + txtMsg.Text);

            var content = new StringContent("\"" + txtMsg.Text + "\"", Encoding.UTF8, "application/json");
            //var result = await _client.PostAsync("xamunotifications", content);
            // Debug.WriteLine("Send result: " + result.IsSuccessStatusCode);
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            MessagingCenter.Unsubscribe<object>(this, App.NotificationReceivedKey);
        }

        private async void ListaTurnos(object sender, EventArgs e)
        {
            App.MasterD.IsPresented = false;
            await App.MasterD.Detail.Navigation.PushAsync(new ListaTurnos(empa));
        }

        private void MisTurnos(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MisTurnos(empa));
        }

        private void MiRendimiento(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MiRendimiento());
        }

        private void MisNotificaciones(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MisNotificaciones());
        }
    }
}
