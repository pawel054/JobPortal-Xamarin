using JobPortal.Database;
using JobPortal.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Text.RegularExpressions;
using JobPortal.View.Admin;
using System.Collections.ObjectModel;

namespace JobPortal.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilePage : ContentPage
    {
        public ObservableCollection<CarouselClass> CarouselItems { get; set; }
        public ProfilePage()
        {
            InitializeComponent();
            CarouselItems = new ObservableCollection<CarouselClass>
        {
            new CarouselClass { value = DatabaseAdmin.GetCountOfRecords("oferta")},
            new CarouselClass { value = DatabaseAdmin.GetCountOfRecords("firma")},
        };

            BindingContext = this;
        }

        private void BtnChangeViewRegister(object sender, EventArgs e)
        {
            labelTitle.Text = "Utwórz konto";
            btnLogin.IsVisible = false;
            btnRegister.IsVisible = true;
            btnViewRegister.IsVisible = false;
            btnViewLogin.IsVisible = true;
        }

        private void BtnChangeViewLogin(object sender, EventArgs e)
        {
            labelTitle.Text = "Zaloguj się";
            btnLogin.IsVisible = true;
            btnRegister.IsVisible = false;
            btnViewRegister.IsVisible = true;
            btnViewLogin.IsVisible = false;
        }

        private void BtnLoginClicked(object sender, EventArgs e)
        {
            List<User> user = new List<User>();
            if (!string.IsNullOrEmpty(entryEmail.Text) && !string.IsNullOrEmpty(entryPassword.Text))
            {
                user = DatabaseCreator.LogIn(new User(entryEmail.Text, entryPassword.Text));

                if (user == null)
                {
                    DisplayAlert("Alert", "Niepoprawny email lub hasło!", "OK");
                }
                else
                {
                    App.LoggedInUserId = user.FirstOrDefault().UserID;
                    UserLoggedIn(user.FirstOrDefault());
                }
            }
            else
            {
                DisplayAlert("Alert", "Pola nie mogą być puste!", "OK");
            }
        }

        private void BtnRegisterClicked(object sender, EventArgs e)
        {
            string email = entryEmail.Text;
            string pass = entryPassword.Text;

            bool status_ok = true;

            if (!string.IsNullOrEmpty(pass) && !string.IsNullOrEmpty(email))
            {
                if (pass.Length < 3 || pass.Length > 60)
                {
                    DisplayAlert("Alert", "Hasło może zawierać od 3 do 60 znaów!", "OK");
                    status_ok = false;
                }

                if (!Regex.IsMatch(email, @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$"))
                {
                    status_ok = false;
                    DisplayAlert("Alert", "E-mail ma niepoprawny format!", "OK");
                }
            }
            else
            {
                DisplayAlert("Alert", "Pola nie mogą być puste!", "OK");
                status_ok=false;
            }
            if (status_ok)
            {
                DatabaseCreator.AddNewUser(new User(email, pass, false));
                DisplayAlert("Alert", "Utworzono konto!", "OK");
            }
        }

        private void UserLoggedIn(User user)
        {
            loginView.IsVisible = false;
            loggedView.IsVisible = true;
            labelUser.Text = "Zalogowano jako: " + user.Email;
            if (user.IsAdmin)
                btnAdmin.IsVisible = true;
            else
                btnAdmin.IsVisible = false;
        }

        private void BtnAdminClicked(object sender, EventArgs e)
        {
            loggedView.IsVisible=false;
            adminPanelView.IsVisible=true;
        }

        private void BtnGoBack(object sender, EventArgs e)
        {
            loggedView.IsVisible = true;
            adminPanelView.IsVisible = false;
        }

        private void BtnGoBack2(object sender, EventArgs e)
        {
            loggedView.IsVisible = true;
            applicationsView.IsVisible = false;
        }

        private void BtnOfferClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new OfferPage());
        }

        private void BtnCategoryClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CategoryPage());
        }

        private void BtnCompanyClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CompanyPage());
        }

        private void BtnApplications(object sender, EventArgs e)
        {
            loggedView.IsVisible = false;
            applicationsView.IsVisible = true;
            collectionApplications.ItemsSource = DatabaseOffer.GetApplicationByID(App.LoggedInUserId);
        }

        private void BtnDeleteApplication(object sender, EventArgs e)
        {
            var button = (Button)sender;
            int applicationID = (int)button.CommandParameter;
            DatabaseOffer.DeleteApplication(applicationID);
            collectionApplications.ItemsSource = DatabaseOffer.GetApplicationByID(App.LoggedInUserId);
        }

        private void BtnLogOut(object sender, EventArgs e)
        {
            App.LoggedInUserId = 0;
            loggedView.IsVisible = false;
            loginView.IsVisible = true;
            entryEmail.Text = string.Empty;
            entryPassword.Text = string.Empty;
        }
    }
}