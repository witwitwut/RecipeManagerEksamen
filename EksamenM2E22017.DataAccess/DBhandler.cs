using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EksamenM2E22017.Entities;

namespace EksamenM2E22017.DataAccess
{
    public class DBhandler
    {
        private string conString;

        public DBhandler(string conString)
        {
            ConString = conString;
        }

        public string ConString
        {
            get { return conString; }
            set { conString = value; }
        }
        public List<Ingredient> GetAllIngredients()
        {
            List<Ingredient> ingredients = new List<Ingredient>();
            string q = "SELECT * FROM Ingredients";

            using (SqlConnection con = new SqlConnection(conString))
            {
                using (SqlCommand command = new SqlCommand(q, con))
                {
                    con.Open();

                    DataSet ds = new DataSet();
                    SqlDataAdapter da = new SqlDataAdapter(command);

                    da.Fill(ds);
                                                                                                                                 
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        string g = row.Field<string>("IngredientType");
                        IngredientType it = (IngredientType)Enum.Parse(typeof(IngredientType), g);
                        ingredients.Add(new Ingredient(row.Field<int>("IngredientPrice"), row.Field<string>("IngredientName"),it,row.Field<int>("IngredientID")));
                    }
                }
            }
            return ingredients;
        }
        public List<Recipe> GetAllRecipies()
        {
            List<Recipe> recipies = new List<Recipe>();
            List<Ingredient> recipeingredients = GetAllIngredients();
            string q = "SELECT * FROM Recipies";
            using (SqlConnection con = new SqlConnection(conString))
            {
                using (SqlCommand command = new SqlCommand(q, con))
                {
                    con.Open();

                    DataSet ds = new DataSet();
                    SqlDataAdapter da = new SqlDataAdapter(command);

                    da.Fill(ds);

                    foreach (DataRow row in ds.Tables[0].Rows)
                    {

                        recipies.Add(new Recipe(row.Field<int>("RecipeID"),row.Field<int>("RecipePerson"), row.Field<string>("RecipeName")));

                    }
                    string qt = "SELECT * FROM RecipeVsIngredient";
                    using (SqlCommand com = new SqlCommand(qt,con))
                    {
                        DataSet dsqt = new DataSet();
                        SqlDataAdapter daqt = new SqlDataAdapter(com);

                        daqt.Fill(dsqt);

                        foreach (Recipe recipe in recipies)
                        {
                            foreach (DataRow row in dsqt.Tables[0].Rows)
                            {
                                if (recipe.ID == row.Field<int>("RecipeID"))
                                {
                                    foreach (Ingredient ing in recipeingredients)
                                    {
                                        if (ing.ID == row.Field<int>("IngredientID"))                                       
                                        {
                                            recipe.Ingredients.Add(ing);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return recipies;
        }
        public Ingredient GetIngredientByName(string name)
        {
            Ingredient ingredient = new Ingredient();
            string q = "SELECT * FROM Ingredient" +
                $"Where IngredientName LIKE '{name}'";
            using (SqlConnection con = new SqlConnection(conString))
            {
                using (SqlCommand command = new SqlCommand(q,con))
                {
                    con.Open();

                    DataSet ds = new DataSet();
                    SqlDataAdapter da = new SqlDataAdapter(command);
                    
                    da.Fill(ds);

                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        ingredient = new Ingredient(row.Field<decimal>("Price"),row.Field<string>("name"),row.Field<IngredientType>("type"));
                    }
                }
            }
            return ingredient;
        }
        public void MakeIngredient(Ingredient ing)
        {
            using (SqlConnection con = new SqlConnection(ConString))
            {
                string q = "Insert INTO Ingredients(IngredientName,IngredientPrice,IngredientType)" +
                    $"Values('{ing.Name}','{ing.Price}','{ing.Type}')";
                using (SqlCommand command = new SqlCommand(q,con))
                {
                    con.Open();

                    command.ExecuteNonQuery();
                }
            }
        }
        public void MakeRecipe(Recipe r)
        {
            using (SqlConnection con = new SqlConnection(conString))
            {
                string q = "INSERT INTO Recipies(RecipeName,RecipePerson)" +
                    $"Values('{r.Name}','{r.Persons}')";
                using (SqlCommand command = new SqlCommand(q,con))
                {
                    con.Open();

                    command.ExecuteNonQuery();

                    List<Recipe> recipelist = GetAllRecipies();
                    List<Ingredient> recipeIngredient = GetAllIngredients();

                    foreach (Recipe recipe in recipelist)
                    {
                        if (recipe.Name == r.Name)
                        {
                            r.ID = recipe.ID;
                        }
                    }
                    foreach (Ingredient ing in recipeIngredient)
                    {
                        foreach (Ingredient g in r.Ingredients)
                        {
                            if (ing == g)
                            {
                                string qt = "INSERT INTO RecipeVsIngredient(RecipeID,IngredientID)" +
                                                $"VALUES('{r.ID}','{ing.ID}')";
                                using (SqlCommand com = new SqlCommand(qt,con))
                                {
                                    con.Open();

                                    command.ExecuteNonQuery();
                                }
                            }
                        }
                    }

                }
            }
        }
    }
}
