using JobPortal.Class;
using JobPortal.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace JobPortal.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OfferDetailsPage : ContentPage
    {
        private int offerID;
        Offer offer;
        public OfferDetailsPage(int offerID)
        {
            InitializeComponent();
            this.offerID = offerID;

            offer = DatabaseOffer.GetOfferByID(offerID).FirstOrDefault();
            txtPosition.Text = offer.NazwaStanowiska;
            txtPositionLvl.Text = offer.PoziomStanowiska;
            txtCompany.Text = offer.Company.Name;
            txtSalary.Text = offer.Wynagrodzenie + " zł";
            txtExpiration.Text =  "Wygasa: " + offer.DataWaznosci.ToString("dd.MM.yyyy");
            txtPosition.Text = offer.NazwaStanowiska;
            txtContract.Text = offer.RodzajUmowy;
            txtEtat.Text = offer.WymiarZatrudnienia;
            txtWorkType.Text = offer.RodzajPracy;
            txtWorkDays.Text = offer.DniPracy + " | " + offer.GodzinyPracy;
            txtInfo.Text = offer.Company.Description;
            imgOffer.Source = offer.SciezkaObraz;
        }

        private void BtnApplication(object sender, EventArgs e)
        {
            if(App.LoggedInUserId == 0)
            {
                DisplayAlert("Alert", "Musisz zalogować się na konto!", "OK");
            }
            else
            {
                DatabaseOffer.InsertApplication(new UserApplication(App.LoggedInUserId, offer.OfferID, "Oczekuje"));
                DisplayAlert("Alert", "Złożono aplikację", "OK");
            }
        }
    }
}