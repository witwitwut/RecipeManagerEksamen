using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using EksamenM2E22017.Entities;
using EksamenM2E22017.DataAccess;

namespace EksamenM2E2017.Opskrifter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static string constring = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=RecipiesDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        DBhandler handler = new DBhandler(constring);
        Recipe k;
        List<Ingredient> RecipeIngredients = new List<Ingredient>();
        public MainWindow()
        {
           
            InitializeComponent();


            ListBoxRecipeList.ItemsSource = handler.GetAllRecipies();

            DtgAllIngredients.ItemsSource = handler.GetAllIngredients();


            cbbxIngType.ItemsSource = IngredientType.GetValues(typeof(IngredientType)).Cast<IngredientType>();
            DTGIng.ItemsSource = handler.GetAllIngredients();
            
        }

        private void cbbxIngType_MouseDown(object sender, MouseButtonEventArgs e)
        {
        }

        private void btbNewIng_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbxIngName.Text) && string.IsNullOrWhiteSpace(tbxIngPrice.Text) && cbbxIngType.SelectedItem == null)
            {
                MessageBox.Show("Du mangler at udfylde et felt");
            }
            else
            {
                IngredientType selectedtype = (IngredientType)Enum.Parse(typeof(IngredientType), cbbxIngType.SelectedItem.ToString());                
                Ingredient ing = new Ingredient(decimal.Parse(tbxIngPrice.Text), tbxIngName.Text,selectedtype);
                handler.MakeIngredient(ing);
                handler.GetAllIngredients();
            }

        }

        private void ListBoxRecipeList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            k = (Recipe)ListBoxRecipeList.SelectedItem;
            TxtBoxPrice.Text = k.GetPrice().ToString();
            TxtBoxPersons.Text = k.Persons.ToString();
            DtgIngredientsInSelectedRecipe.ItemsSource = k.Ingredients;
        }

        private void tbxIngName_MouseDown(object sender, MouseButtonEventArgs e)
        {
            tbxIngName.Text = "";
        }

        private void tbxIngPrice_MouseDown(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void tbxIngName_MouseEnter(object sender, MouseEventArgs e)
        {
            tbxIngName.Text = "";
        }

        private void tbxIngPrice_MouseEnter(object sender, MouseEventArgs e)
        {
            tbxIngPrice.Text = "";
        }

        private void BtnAddNewRecipe_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TxtBoxRecipeName.Text) && string.IsNullOrWhiteSpace(TxtBoxCountOfPersonsInRecipe.Text) && string.IsNullOrWhiteSpace(TxtBoxPrice.Text))
            {
                MessageBox.Show("Du mangler at udfylde et felt");
            }
            else
            {

                k = new Recipe(int.Parse(TxtBoxCountOfPersonsInRecipe.Text),TxtBoxRecipeName.Text,RecipeIngredients);
                handler.MakeRecipe(k);
                

            }
        }

        private void BtnMoveItemRight_Click(object sender, RoutedEventArgs e)
        {
            decimal p = 0;
            RecipeIngredients.Add((Ingredient)DtgAllIngredients.SelectedItem);
            DtgItemsInNewRecipe.ItemsSource = RecipeIngredients;
            foreach (Ingredient i in RecipeIngredients)
            {
                p += i.Price;
            }
            LblTotalPrice.Content = p;
        }
    }
}
