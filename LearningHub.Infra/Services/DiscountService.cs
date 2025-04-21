using LearningHub.Core.Dto;
using LearningHub.Core.Repository;
using LearningHub.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningHub.Infra.Services
{
    public class DiscountService: IDiscountService
    {
        private readonly IDiscountsRepository _countsRepository;

        public DiscountService(IDiscountsRepository countsRepository)
        {
            _countsRepository = countsRepository;
        }

        public List<DiscountDto> GetDiscountsByUserID(decimal userID)
        {
            return _countsRepository.GetDiscountsByUserID(userID);
        }

        public DiscountDto GetDiscountsByUserAndCode(decimal userID, String Code) 
        { 
            return _countsRepository.GetDiscountsByUserAndCode(userID, Code);
        }

        public List<DiscountDto> GetAllDiscounts() { 
            return _countsRepository.GetAllDiscounts();
        }

        public void AddDiscount(DiscountDto discount) { 
             _countsRepository.AddDiscount(discount);
        }
    }
}
