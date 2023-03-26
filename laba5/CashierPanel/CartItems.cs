using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laba5
{
    internal class CartItems
    {
        public string Name { get; set; }
        public string Brand { get; set; }
        public int Amount { get; set; }
        public decimal Price { get; set; }
        public CartItems(string Name, string Brand, int Amount, decimal Price) 
        {
            this.Name = Name;
            this.Brand = Brand;
            this.Amount = Amount;
            this.Price = Price;
        }
    }
}
