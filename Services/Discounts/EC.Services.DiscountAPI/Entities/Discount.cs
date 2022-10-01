using Core.Entities;

namespace EC.Services.DiscountAPI.Entities
{
    public class Discount:IEntity
    {
        //Discount type olarak bir entity daha olacak ve ilişkili olacak
        //Bununla beraber DiscountType mesela TL ise ve Rate 50 ise 50TL indirim
        //Eğer DiscountType Yüzde ise ve Rate 20 ise yüzde 20 indirim olacak.

        public int Id { get; set; }
        public int DiscountType { get; set; }
        public int Rate { get; set; }
        public string Code { get; set; }
        public DateTime CDate { get; set; }

    }
}