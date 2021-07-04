using AutoMapper;
using Moq;
using PhoneStoreWeb.Service.OrderService;

namespace PhoneStoreWeb.Test.Utilities
{
    public class FakeOrderService : OrderService 
    {
        public FakeOrderService() : base(new Mock<IMapper>().Object, new FakeUserManager())
        {

        }
    }
}
