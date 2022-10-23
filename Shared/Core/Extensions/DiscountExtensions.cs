using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Extensions
{
    public static class DiscountExtensions
    {
        public static decimal CalculateDiscount(DiscountTypes discountType, int discountRate, decimal price)
        {
            decimal rateValue = discountRate;

            if (discountType == DiscountTypes.Price)
            {
                decimal calculatedPrice = price - rateValue;
                return calculatedPrice;
            }
            else if (discountType == DiscountTypes.Percentage)
            {
                decimal calculatedPrice = price - (price * (rateValue / 100));
                return calculatedPrice;
            }
            return price;
        }

        public static decimal CalculateCampaign(CampaignTypes campaignType, int rate, decimal price)
        {
            decimal rateValue = rate;

            if (campaignType == CampaignTypes.Price)
            {
                decimal calculatedPrice = price - rateValue;
                return calculatedPrice;
            }
            else if (campaignType == CampaignTypes.Percentage)
            {
                decimal calculatedPrice = price - (price * (rateValue / 100));
                return calculatedPrice;
            }
            else if (campaignType == CampaignTypes.OverPrice)
            {
                decimal calculatedPrice = price - rateValue;
                return calculatedPrice;
            }
            else if (campaignType == CampaignTypes.OverPercentage)
            {
                decimal calculatedPrice = price - (price * (rateValue / 100));
                return calculatedPrice;
            }
            return price;
        }

    }
}
