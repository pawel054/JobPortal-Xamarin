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

namespace JobPortal.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilePage : ContentPage
    {
        public ProfilePage()
        {
            InitializeComponent();
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
                    DisplayAlert("1", "1", "1");
                }
                else
                    UserLoggedIn(user.FirstOrDefault());
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
                DatabaseCreator.AddNewUser(new User(email, pass));
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
            Navigation.PushAsync(new AdminPage());
        }
    }
}