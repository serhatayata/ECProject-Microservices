using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class SourceOriginSettings
    {
        public SourceOrigin Development { get; set; }
        public SourceOrigin Product { get; set; }
    }

    public class SourceOrigin
    {
        public string Baskets { get; set; }
        public string Categories { get; set; }
        public string Discounts { get; set; }
        public string Orders { get; set; }
        public string Payments { get; set; }
        public string PhotoStocks { get; set; }
        public string Products { get; set; }
    }
}
