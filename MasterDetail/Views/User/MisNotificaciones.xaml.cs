using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Xamarin.Forms;

namespace MasterDetail
{
    public partial class MisNotificaciones : ContentPage
    {
        public MisNotificaciones()
        {
            InitializeComponent();
            NavigationPage.SetBackButtonTitle(this, "MiTurnoAPP");
            _client.BaseAddress = new Uri(App.MobileServiceUrl);
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _client.Timeout = TimeSpan.FromSeconds(120);
        }
        HttpClient _client = new HttpClient();
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
            var result = await _client.PostAsync("xamunotifications", content);
            Debug.WriteLine("Send result: " + result.IsSuccessStatusCode);
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            MessagingCenter.Unsubscribe<object>(this, App.NotificationReceivedKey);
        }
    }
}
