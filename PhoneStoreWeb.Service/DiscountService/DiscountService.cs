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

        public virtual async Task<MessageResponse> CreateDiscount(DiscountRequest request)
        {
            if (request.FromDate > request.ToDate)
            {
                return new MessageResponse() { Type = "error", Title = "Tạo mới thất bại", Content = "Thời hạn không đúng" };
            }
            try
            {
                using (UnitOfWork uow = new UnitOfWork())
                {
                    Discount d = await uow.Discounts.SingleOrDefaultAsync(x => x.Code == request.Code);
                    if (d != null)
                    {
                        return new MessageResponse("error", "Tạo mới thất bại", "Lỗi: Mã code đã tồn tại.");
                    }
                    var discount = mapper.Map<DiscountRequest, Discount>(request);
                    await uow.Discounts.AddAsync(discount);
                    await uow.SaveAsync();
                    return new MessageResponse("success", "Tạo mới thành công");
                }
            }
            catch (Exception e)
            {
                return new MessageResponse("error", "Tạo mới thất bại", "Lỗi: " + e.Message);
            }
        }

        public virtual async Task<MessageResponse> Delete(int id)
        {
            try
            {
                using (UnitOfWork uow = new UnitOfWork())
                {
                    var result = await uow.Discounts.GetAsync(id);                    
                    uow.Discounts.Remove(result);
                    await uow.SaveAsync();
                    return new MessageResponse("success", "Xóa thành công");
                }
            }
            catch (Exception e)
            {
                return new MessageResponse("error", "Xóa thất bại", "Lỗi: " + e.Message);
            }
        }

        public virtual async Task<List<DiscountResponse>> GetAllDiscounts()
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                var discounts = await uow.Discounts.GetAllAsync();
                var result = mapper.Map<List<Discount>, List<DiscountResponse>>(discounts.ToList());
                return result;
            }
        }

        public virtual async Task<List<DiscountResponse>> GetAllValidDiscounts()
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                var discounts = await uow.Discounts.FindAsync(x => x.FromDate <= DateTime.Today && DateTime.Today <= x.ToDate);
                var result = mapper.Map<List<Discount>, List<DiscountResponse>>(discounts.ToList());
                return result;
            }
        }

        public virtual async Task<DiscountResponse> GetDiscount(int id)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                var discount = await uow.Discounts.GetAsync(id);
                var result = mapper.Map<Discount, DiscountResponse>(discount);
                return result;
            }
        }

        public virtual async Task<MessageResponse> UpdateDiscount(DiscountRequest request)
        {
            if (request.FromDate > request.ToDate)
            {
                return new MessageResponse() { Type = "error", Title = "Cập nhật thất bại", Content = "Thời hạn không đúng" };
            }
            try
            {
                using (UnitOfWork uow = new UnitOfWork())
                {
                    Discount d = await uow.Discounts.SingleOrDefaultAsync(x => x.Code == request.Code);
                    if (d != null && d.Id != request.Id)
                    {
                        return new MessageResponse("error", "Tạo mới thất bại", "Lỗi: Mã code đã tồn tại.");
                    }
                    d.Code = request.Code;
                    d.DiscountAmount = request.DiscountAmount;
                    d.DiscountPercent = request.DiscountPercent;
                    d.FromDate = request.FromDate;
                    d.ToDate = request.ToDate;
                    uow.Discounts.Update(d);
                    await uow.SaveAsync();
                    return new MessageResponse("success", "Cập nhật thành công");
                }
            }
            catch (Exception e)
            {
                return new MessageResponse("error", "Cập nhật thất bại", "Lỗi: " + e.Message);
            }
        }
    }
}
