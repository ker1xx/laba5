using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;

namespace laba5
{
    public class CartItems
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
        public string Sex { get; set; }
        public int Size { get; set; }
        public int Amount { get; set; }
        public decimal Price { get; set; }
        public decimal Profit { get; set; }
        public CartItems(int Id, string Name, string Brand, string Sex, int Size, int Amount, decimal Price) 
        {
            this.Id = Id;
            this.Name = Name;
            this.Brand = Brand;
            this.Sex = Sex;
            this.Size = Size;
            this.Amount = Amount;
            this.Price = Price;
        }
    }
}
