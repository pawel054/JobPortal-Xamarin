using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobPortal.Database;
using JobPortal.Class;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace JobPortal.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            DatabaseCreator.CreateDb();
        }

        private void testclick(object sender, EventArgs e)
        {
           
        }
    }
}