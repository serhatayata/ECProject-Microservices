using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class ValidatePhoneNumberModel
    {
        public bool IsValidNumber { get; set; }
        public bool IsValidNumberForRegion { get; set; }
        public bool IsMobile { get; set; }
        public string Region { get; set; }
        public string FormattedNumber { get; set; }
    }
}
