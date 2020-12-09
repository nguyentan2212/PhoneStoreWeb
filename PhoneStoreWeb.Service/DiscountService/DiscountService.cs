using AutoMapper;
using PhoneStoreWeb.Communication.Discounts;
using PhoneStoreWeb.Communication.ResponseResult;
using PhoneStoreWeb.Data.Models;
using PhoneStoreWeb.Data.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneStoreWeb.Service.DiscountService
{
    public class DiscountService : ServiceBase, IDiscountService
    {
        public DiscountService(IMapper mapper):base(mapper)
        {

        }

        public async Task<string> CreateDiscount(DiscountRequest request)
        {
            try
            {
                using (UnitOfWork uow = new UnitOfWork())
                {
                    var discount = mapper.Map<DiscountRequest, Discount>(request);
                    await uow.Discounts.AddAsync(discount);
                    await uow.SaveAsync();
                    return null;
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public async Task<List<DiscountResponse>> GetAllDiscounts()
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                var discounts = await uow.Discounts.GetAllAsync();
                var result = mapper.Map<List<Discount>, List<DiscountResponse>>(discounts.ToList());
                return result;
            }
        }

        public async Task<string> UpdateDiscount(DiscountRequest request)
        {
            try
            {
                using (UnitOfWork uow = new UnitOfWork())
                {
                    var result = await uow.Discounts.GetAsync(request.Id);
                    if (result is null)
                        return "Not found";
                    var discount = mapper.Map<DiscountRequest, Discount>(request);
                    discount.Id = request.Id;
                    uow.Discounts.Update(discount);
                    await uow.SaveAsync();
                    return null;
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
    }
}
