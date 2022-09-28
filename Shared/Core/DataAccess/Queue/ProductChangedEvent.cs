using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DataAccess.Queue
{
    public class ProductChangedEvent
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int Line { get; set; }
        public string Link { get; set; }
        public bool Status { get; set; }
        public int CategoryId { get; set; }
    }
}
