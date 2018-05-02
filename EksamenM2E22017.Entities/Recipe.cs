using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EksamenM2E22017.Entities
{
    public class Recipe
    {
        private List<Ingredient> ingredients;
        private string name;
        private int persons;
        private int id;

        public Recipe(int persons, string name, List<Ingredient> ingredients)
        {
            Persons = persons;
            Name = name;
            Ingredients = ingredients;
        }

        public Recipe(int iD, int persons, string name)
        {
            ID = iD;
            Persons = persons;
            Name = name;
        }

        public Recipe(int iD, int persons, string name, List<Ingredient> ingredients)
        {
            ID = iD;
            Persons = persons;
            Name = name;
            Ingredients = ingredients;
        }

        public int ID
        {
            get { return id; }
            set { id = value; }
        }


        public int Persons
        {
            get { return persons; }
            set { persons = value; }
        }


        public string Name
        {
            get { return name; }
            set { name = value; }
        }


        public List<Ingredient> Ingredients
        {
            get { return ingredients; }
            set { ingredients = value; }
        }

        public List<IngredientType> GetIngredientTypes()
        {
            List<IngredientType> Ingtypes = new List<IngredientType>();

            foreach (Ingredient i in ingredients)
            {
                Ingtypes.Add(i.Type);
            }
            return Ingtypes;
        }

        public decimal GetPrice()
        {
            decimal k = 0;
            foreach (Ingredient i in ingredients)
            {
                k += i.Price;
            }
            return k;
        }
        public override string ToString()
        {
            return $"{Name}";
        }

    }
}
