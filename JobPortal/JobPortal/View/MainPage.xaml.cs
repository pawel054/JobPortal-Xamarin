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

        private void OnSearchBarTextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = e.NewTextValue.ToLower(); // Konwertuj tekst na małe litery

            var searchResult = DatabaseOffer.GetAllOffers().Where(item =>
                item.NazwaStanowiska.ToLower().StartsWith(searchText) ||
                item.Company.Name.ToLower().StartsWith(searchText) ||
                item.Company.Adress.ToLower().StartsWith(searchText)
            );
            collectionOffer.ItemsSource = searchResult;
        }
    }
}