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
using System.ComponentModel.Design;

namespace JobPortal.View.Admin
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OfferPage : ContentPage
    {
        private int OfferID;
        public OfferPage()
        {
            InitializeComponent();
            pickerCategory.ItemsSource = DatabaseAdmin.GetAllCategories();
            pickerCompany.ItemsSource = DatabaseAdmin.GetAllCompanies();
            UpdateView();
            pickerWorkType.SelectedIndex = 0;
        }

        private void BtnAddView(object sender, EventArgs e)
        {
            AddOfferView.IsVisible = true;
            OfferView.IsVisible = false;
            pickerPositionLevel.SelectedIndex = 0;
            pickerContract.SelectedIndex = 0;
            pickerEtat.SelectedIndex = 0;
            pickerWorkType.SelectedIndex = 0;
            entryPosition.Text = string.Empty;
            entrySalary.Text = string.Empty;
            entryWorkDays.Text = string.Empty;
            entryWorkHours.Text = string.Empty;
            entryImgSrc.Text = string.Empty;
            datePicker.Date = DateTime.Now;
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
                UpdateView();
            }
        }

        private void BtnEditOffer(object sender, EventArgs e)
        {
            if (IsValid())
            {
                btnDodaj.IsVisible = true;
                btnEdytuj.IsVisible = false;
                AddOfferView.IsVisible = false;
                OfferView.IsVisible = true;
                var selectedItemCategory = pickerCategory.SelectedItem as Category;
                var selectedItemCompany = pickerCompany.SelectedItem as Company;
                DatabaseAdmin.UpdateOffer(new Offer(OfferID, selectedItemCompany, selectedItemCategory, entryPosition.Text, pickerPositionLevel.SelectedItem.ToString(), pickerContract.SelectedItem.ToString(), pickerWorkType.SelectedItem.ToString(), pickerEtat.SelectedItem.ToString(), entrySalary.Text, entryWorkDays.Text, entryWorkHours.Text, datePicker.Date, entryImgSrc.Text), true);
                UpdateView();
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

        private async void SwipeDelete(object sender, EventArgs e)
        {
            var swipeItem = sender as SwipeItem;
            if (swipeItem != null)
            {
                var item = swipeItem.BindingContext as Offer;
                if (item != null)
                {
                    bool result = await DisplayAlert("Uwaga!", "Czy na pewno chcesz usunąć ogłoszenie?", "Tak", "Nie");
                    if (result)
                    {
                        DatabaseAdmin.RemoveOffer(item.OfferID);
                        UpdateView();
                    }
                }
            }
        }

        private void SwipeEdit(object sender, EventArgs e)
        {
            var swipeItem = sender as SwipeItem;
            if (swipeItem != null)
            {
                var item = swipeItem.BindingContext as Offer;
                if (item != null)
                {
                    OfferView.IsVisible = false;
                    AddOfferView.IsVisible = true;
                    btnDodaj.IsVisible = false;
                    btnEdytuj.IsVisible = true;
                    entryPosition.Text = item.NazwaStanowiska;
                    entrySalary.Text = item.Wynagrodzenie;
                    entryWorkDays.Text = item.DniPracy;
                    entryWorkHours.Text = item.GodzinyPracy;
                    entryImgSrc.Text = item.SciezkaObraz;
                    datePicker.Date = item.DataWaznosci;
                    pickerPositionLevel.SelectedItem = item.PoziomStanowiska;
                    pickerContract.SelectedItem = item.RodzajUmowy;
                    pickerEtat.SelectedItem = item.WymiarZatrudnienia;
                    pickerWorkType.SelectedItem = item.RodzajPracy;
                    OfferID = item.OfferID; 
                }
            }
        }


        private void UpdateView()
        {
            collectionOffer.ItemsSource = DatabaseOffer.GetAllOffers();
        }
    }
}