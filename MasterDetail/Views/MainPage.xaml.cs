using MasterDetail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MasterDetail
{
    public partial class MainPage : MasterDetailPage
    {
        EmpaqueModel empaque;

        public MainPage(EmpaqueModel empaque)
        {
            this.empaque = empaque;
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            this.Master = new Master(this.empaque);
            this.Detail = new NavigationPage(new Detail(this.empaque));
            App.MasterD = this;
        }
        
    }
}

