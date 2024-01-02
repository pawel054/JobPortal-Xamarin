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
            collectionOffer.ItemsSource = DatabaseOffer.GetLatestAddedOffers();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            collectionOffer.ItemsSource = DatabaseOffer.GetLatestAddedOffers();
        }

        private void OfferClicked(object sender, EventArgs e)
        {
            var button = (Button)sender;
            int offerID = (int)button.CommandParameter;
            Navigation.PushAsync(new OfferDetailsPage(offerID));
        }
    }
}