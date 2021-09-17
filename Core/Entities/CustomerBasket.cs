using System.Collections.Generic;

namespace Core.Entities
{
    public class CustomerBasket
    {
        
        public CustomerBasket()
        { // empty constructor prevents that no additional new instance of basket gets created for a customer
        }

        public CustomerBasket(string id)
        {
            Id = id;
        }

        public string Id { get; set; }
        public List<BasketItem> Items { get; set; } = new List<BasketItem>();
    }
}