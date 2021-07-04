using AutoMapper;
using Moq;
using PhoneStoreWeb.Service.CategoryService;

namespace PhoneStoreWeb.Test.Utilities
{
    public class FakeCategoryService : CategoryService
    {
        public FakeCategoryService() : base (new Mock<IMapper>().Object, new FakeFileService())
        { }
    }
}
