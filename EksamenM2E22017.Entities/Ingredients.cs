using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EksamenM2E22017.Entities
{
    public class Ingredient
    {
        private int id;
        private IngredientType type;
        private string name;
        private decimal price;

        public Ingredient()
        {

        }
        public Ingredient(decimal price, string name, IngredientType type)
        {
            Price = price;
            Name = name;
            Type = type;
        }

        public Ingredient(decimal price, string name, IngredientType type, int iD)
        {
            Price = price;
            Name = name;
            Type = type;
            ID = iD;
        }

        public decimal Price
        {
            get { return price; }
            set { price = value; }
        }


        public string Name
        {
            get { return name; }
            set { name = value; }
        }


        public IngredientType Type
        {
            get { return type; }
            set { type = value; }
        }


        public int ID
        {
            get { return id; }
            set { id = value; }
        }
        public override string ToString()
        {
            return $"{Type} {Price} {Name}";
        }
    }
}
