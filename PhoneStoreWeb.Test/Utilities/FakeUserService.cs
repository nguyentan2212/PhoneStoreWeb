using AutoMapper;
using Moq;
using PhoneStoreWeb.Service.UserService;

namespace PhoneStoreWeb.Test.Utilities
{
    public class FakeUserService : UserService
    {
        public FakeUserService() : base(new Mock<IMapper>().Object,
                                        new FakeUserManager(),
                                        new Mock<FakeRoleManager>().Object, 
                                        new FakeFileService())
        {

        }
    }
}
