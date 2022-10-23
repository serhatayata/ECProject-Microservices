using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public enum DiscountTypes
    {
        Price = 1,
        Percentage = 2
    }

    public enum CampaignTypes
    {
        Price = 1,
        Percentage = 2,
        OverPrice = 3,
        OverPercentage = 4
    }
}
