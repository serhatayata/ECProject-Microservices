using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EC.Services.Order.Domain.OrderAggregate
{
    public class Address
    {
        public string CountryName { get; set; }
        public string CountyName { get; set; }
        public string CityName { get; set; }
        public string AddressDetail { get; set; }
        public string ZipCode { get; set; }
    }
}
