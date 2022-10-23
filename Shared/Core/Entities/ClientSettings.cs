using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class ClientSettings
    {
        public Client BasketClient { get; set; }
        public Client CategoryClient { get; set; }
        public Client DiscountClient { get; set; }
        public Client OrderClient { get; set; }
        public Client PaymentClient { get; set; }
        public Client PhotoStockClient { get; set; }
        public Client ProductClient { get; set; }

    }

    public class Client
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
    }
}
