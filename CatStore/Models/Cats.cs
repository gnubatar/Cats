using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CatStore.Models
{
    public class Cats
    {
        public List<Cat> cats = new List<Cat>();

        public double totalPrice()
        {
            double i =0;
            foreach (var cat in cats)
            {
                i += cat.price;
            }
            return i;
        }
        public void removeCatById(string id)
        {
            foreach (var cat in cats)
            {
                if (cat.id.Equals(id))
                    cats.Remove(cat);
            }
        }
    }
}