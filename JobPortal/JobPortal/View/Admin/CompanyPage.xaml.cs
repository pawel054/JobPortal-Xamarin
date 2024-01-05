using JobPortal.Class;
using JobPortal.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace JobPortal.View.Admin
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CompanyPage : ContentPage
    {
        private int CompanyID;
        public CompanyPage()
        {
            InitializeComponent();
            collectionCompany.ItemsSource = DatabaseAdmin.GetAllCompanies();
        }
        private void BtnCompanyView(object sender, EventArgs e)
        {
            entryCompanyName.Text = string.Empty;
            entryCompanyAdress.Text = string.Empty;
            entryCompanyInfo.Text = string.Empty;
            AddCompanyView.IsVisible = true;
            CompanyView.IsVisible = false;
        }

        private void BtnAddCompany(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(entryCompanyName.Text) && !string.IsNullOrEmpty(entryCompanyAdress.Text) && !string.IsNullOrEmpty(entryCompanyInfo.Text))
            {
                AddCompanyView.IsVisible = false;
                CompanyView.IsVisible = true;
                DatabaseAdmin.InsertCompany(new Company(entryCompanyName.Text, entryCompanyAdress.Text, entryCompanyInfo.Text));
                collectionCompany.ItemsSource = DatabaseAdmin.GetAllCompanies();
            }
        }

        private void BtnEditCompany(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(entryCompanyName.Text) && !string.IsNullOrEmpty(entryCompanyAdress.Text) && !string.IsNullOrEmpty(entryCompanyInfo.Text))
            {
                btnDodaj.IsVisible = true;
                btnEdytuj.IsVisible = false;
                AddCompanyView.IsVisible = false;
                CompanyView.IsVisible = true;
                DatabaseAdmin.UpdateCompany(new Company(CompanyID, entryCompanyName.Text, entryCompanyAdress.Text, entryCompanyInfo.Text));
                collectionCompany.ItemsSource = DatabaseAdmin.GetAllCompanies();
            }
        }

        private void BtnClose(object sender, EventArgs e)
        {
            AddCompanyView.IsVisible = false;
            CompanyView.IsVisible = true;
        }

        private async void SwipeDelete(object sender, EventArgs e)
        {
            var swipeItem = sender as SwipeItem;
            if (swipeItem != null)
            {
                var item = swipeItem.BindingContext as Company;
                if (item != null)
                {
                    bool result = await DisplayAlert("Uwaga!", "Czy na pewno chcesz usunąć firmę?", "Tak", "Nie");
                    if (result)
                        DatabaseAdmin.RemoveCompany(item.CompanyID);
                }
            }
        }

        private void SwipeEdit(object sender, EventArgs e)
        {
            var swipeItem = sender as SwipeItem;
            if (swipeItem != null)
            {
                var item = swipeItem.BindingContext as Company;
                if (item != null)
                {
                    CompanyView.IsVisible = false;
                    AddCompanyView.IsVisible = true;
                    btnDodaj.IsVisible = false;
                    btnEdytuj.IsVisible = true;
                    entryCompanyName.Text = item.Name;
                    entryCompanyAdress.Text = item.Adress;
                    entryCompanyInfo.Text = item.Description;
                    CompanyID = item.CompanyID;
                }
            }
        }
    }
}