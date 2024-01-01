using JobPortal.Database;
using JobPortal.Class;
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
    public partial class CategoryPage : ContentPage
    {
        public CategoryPage()
        {
            InitializeComponent();
            collectionCategory.ItemsSource = DatabaseAdmin.GetAllCategories();
        }

        private void BtnCategoryView(object sender, EventArgs e)
        {
            entryCategoryName.Text = string.Empty;
            AddCategoryView.IsVisible = true;
            dodajBtn.IsVisible = false;
        }

        private void BtnAddCategory(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(entryCategoryName.Text))
            {
                AddCategoryView.IsVisible = false;
                dodajBtn.IsVisible = true;
                DatabaseAdmin.InsertCategory(new Category(entryCategoryName.Text));
                collectionCategory.ItemsSource = DatabaseAdmin.GetAllCategories();
            }
        }

        private void BtnClose(object sender, EventArgs e)
        {
            AddCategoryView.IsVisible = false;
            dodajBtn.IsVisible = true;
        }
    }
}