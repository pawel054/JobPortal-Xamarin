using JobPortal.Class;
using JobPortal.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace JobPortal.View.CarouselPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CompanyPage : ContentPage
    {
        public CompanyPage()
        {
            InitializeComponent();
            collectionCompany.ItemsSource = DatabaseAdmin.GetAllCompanies();
        }
        private void BtnCompanyView(object sender, EventArgs e)
        {
            entryCompanyName.Text = string.Empty;
            AddCompanyView.IsVisible = true;
            dodajBtn.IsVisible = false;
        }

        private void BtnAddCompany(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(entryCompanyName.Text) && !string.IsNullOrEmpty(entryCompanyAdress.Text) && !string.IsNullOrEmpty(entryCompanyInfo.Text))
            {
                AddCompanyView.IsVisible = false;
                dodajBtn.IsVisible = true;
                DatabaseAdmin.InsertCompany(new Company(entryCompanyName.Text, entryCompanyAdress.Text, entryCompanyInfo.Text));
                collectionCompany.ItemsSource = DatabaseAdmin.GetAllCompanies();
            }
        }

        private void BtnClose(object sender, EventArgs e)
        {
            AddCompanyView.IsVisible = false;
            dodajBtn.IsVisible = true;
        }

        private void SwipeDelete(object sender, EventArgs e)
        {

        }
    }
}