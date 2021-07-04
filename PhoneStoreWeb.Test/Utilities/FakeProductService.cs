using AutoMapper;
using Moq;
using PhoneStoreWeb.Service.ProductService;

namespace PhoneStoreWeb.Test.Utilities
{
    public class FakeProductService : ProductService
    {
        public FakeProductService() : base(new Mock<IMapper>().Object, new FakeFileService())
        {

        }
    }
}
