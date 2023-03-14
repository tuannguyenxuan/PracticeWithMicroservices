using System.Collections.Generic;

namespace Basket.API.Entities
{
    public class ShoppingCart
    {
        public string Username { get; set; }
        public List<ShoppingCartItem> Items { get; set; }
        public decimal Total
        {
            get
            {
                decimal total = 0;

                foreach (var item in Items)
                {
                    total += item.Price * item.Quantity;
                }

                return total;
            }
        }
    }
}
