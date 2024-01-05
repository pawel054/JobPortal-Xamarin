using JobPortal.Database;
using JobPortal.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.ComponentModel.Design;

namespace JobPortal.View.Admin
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CategoryPage : ContentPage
    {
        private int CategoryID;
        public CategoryPage()
        {
            InitializeComponent();
            UpdateView();
        }

        private void BtnCategoryView(object sender, EventArgs e)
        {
            entryCategoryName.Text = string.Empty;
            AddCategoryView.IsVisible = true;
            CategoryView.IsVisible = false;
        }

        private void BtnAddCategory(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(entryCategoryName.Text))
            {
                AddCategoryView.IsVisible = false;
                CategoryView.IsVisible= true;
                DatabaseAdmin.InsertCategory(new Category(entryCategoryName.Text));
                UpdateView();
            }
        }

        private void BtnEditCategory(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(entryCategoryName.Text))
            {
                btnDodaj.IsVisible = true;
                btnEdytuj.IsVisible = false;
                AddCategoryView.IsVisible = false;
                CategoryView.IsVisible = true;
                DatabaseAdmin.UpdateCategory(new Category(CategoryID, entryCategoryName.Text));
                UpdateView();
            }
        }

        private void BtnClose(object sender, EventArgs e)
        {
            AddCategoryView.IsVisible = false;
            CategoryView.IsVisible = true;
        }

        private async void SwipeDelete(object sender, EventArgs e)
        {
            var swipeItem = sender as SwipeItem;
            if (swipeItem != null)
            {
                var item = swipeItem.BindingContext as Category;
                if (item != null)
                {
                    bool result = await DisplayAlert("Uwaga!", "Czy na pewno chcesz usunąć kategorię?", "Tak", "Nie");
                    if (result)
                    {
                        DatabaseAdmin.RemoveCategory(item.CategoryID);
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
                var item = swipeItem.BindingContext as Category;
                if (item != null)
                {
                    CategoryView.IsVisible = false;
                    AddCategoryView.IsVisible = true;
                    btnDodaj.IsVisible = false;
                    btnEdytuj.IsVisible = true;
                    entryCategoryName.Text = item.Name;
                    CategoryID = item.CategoryID;
                }
            }
        }

        private void UpdateView()
        {
            collectionCategory.ItemsSource = DatabaseAdmin.GetAllCategories();
        }
    }
}