using AutoMapper;
using Moq;
using PhoneStoreWeb.Service.DiscountService;

namespace PhoneStoreWeb.Test.Utilities
{
    public class FakeDiscountService : DiscountService
    {
        public FakeDiscountService() : base(new Mock<IMapper>().Object)
        {

        }
    }
}
