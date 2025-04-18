using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningHub.Core.Dto
{
    public class DiscountDto
    {
        public decimal DISCOUNTID {  get; set; }
        public decimal USERID { get; set; }
        public String DISCOUNTCODE { get; set; }
        public decimal DISCOUNTAMOUNT { get; set; }
    }
}