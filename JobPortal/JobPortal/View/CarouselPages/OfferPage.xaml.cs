using JobPortal.Database;
using JobPortal.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.GTKSpecific;
using Xamarin.Forms.Xaml;

namespace JobPortal.View.CarouselPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OfferPage : ContentPage
    {
        Company selectedCompany;
        public OfferPage()
        {
            InitializeComponent();
            pickerCategory.ItemsSource = DatabaseAdmin.GetAllCategories();
            collectionOffer.ItemsSource = DatabaseOffer.GetAllOffers();
        }

        private void BtnAddView(object sender, EventArgs e)
        {
            AddOfferView.IsVisible = true;
            OfferView.IsVisible = false;
            pickerCategory.ItemsSource = DatabaseAdmin.GetAllCategories();
            pickerCompany.ItemsSource = DatabaseAdmin.GetAllCompanies();
        }

        private void BtnAddOffer(object sender, EventArgs e)
        {
            if (IsValid())
            {
                var SelectedItemCategory = pickerCategory.SelectedItem as Category;
                var SelectedItemCompany = pickerCompany.SelectedItem as Company;
                DatabaseAdmin.InsertOffer(new Offer(SelectedItemCompany, SelectedItemCategory, entryPosition.Text, pickerPositionLevel.SelectedItem.ToString(), pickerContract.SelectedItem.ToString(), pickerWorkType.SelectedItem.ToString(), pickerEtat.SelectedItem.ToString(), entrySalary.Text, entryWorkDays.Text, entryWorkHours.Text, datePicker.Date, entryImgSrc.Text));
                OfferView.IsVisible = true;
                AddOfferView.IsVisible = false;
                collectionOffer.ItemsSource = DatabaseOffer.GetAllOffers();
            }
        }

        private void BtnClose(object sender, EventArgs e)
        {
            AddOfferView.IsVisible = false;
            OfferView.IsVisible = true;
        }

        private bool IsValid()
        {
            bool isOk = true;
            if (string.IsNullOrWhiteSpace(entryPosition.Text) || string.IsNullOrWhiteSpace(entrySalary.Text) || string.IsNullOrWhiteSpace(entryWorkDays.Text) || string.IsNullOrWhiteSpace(entryImgSrc.Text) || string.IsNullOrWhiteSpace(entryWorkHours.Text) || pickerPositionLevel.SelectedIndex == 0 || pickerContract.SelectedIndex == 0 || pickerEtat.SelectedIndex == 0 || pickerWorkType.SelectedIndex == 0)
            {
                isOk = false;
                DisplayAlert("Alert", "Pola nie mogą być puste!", "OK");
            }
            else
            {
                if (!Regex.IsMatch(entrySalary.Text, @"^\d+(?:\s*-\s*\d+)?$")) { isOk = false; DisplayAlert("Alert", "Niepoprawna wartość dla pola wynagrodzenie!", "OK"); }
                if (!Regex.IsMatch(entryWorkHours.Text, @"^\d{1,2}:\d{2}(?:\s*-\s*\d{1,2}:\d{2})?$")) { isOk = false; DisplayAlert("Alert", "Niepoprawna wartość dla pola godziny pracy!", "OK"); }
            }
            return isOk;
        }

        private void CompanyIndexChange(object sender, EventArgs e)
        {
            var testt = pickerCompany.SelectedItem as Company;
            DisplayAlert("alert", testt.Name, "ok");
            selectedCompany = testt;
        }
    }
}