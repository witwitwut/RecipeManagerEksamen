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
                        ingredients.Add(new Ingredient(row.Field<int>("IngredientPrice"), row.Field<string>("IngredientName"),it));
                    }
                }
            }
            return ingredients;
        }
        public List<Recipe> GetAllRecipies()
        {
            List<Recipe> recipies = new List<Recipe>();
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
                        recipies.Add(new Recipe(row.Field<int>("Persons"), row.Field<string>("Name"), row.Field<List<Ingredient>>("Ingredients")));
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
    }
}
