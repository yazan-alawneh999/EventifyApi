using LearningHub.Core.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningHub.Core.Repository
{
    public interface IDiscountsRepository
    {
        public List<DiscountDto> GetDiscountsByUserID(decimal userID);
        public DiscountDto GetDiscountsByUserAndCode(decimal userID, String Code);
    }
}
