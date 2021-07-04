using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Moq;
using PhoneStoreWeb.Service.FileService;

namespace PhoneStoreWeb.Test.Utilities
{
    public class FakeFileService : FileService
    {
        public FakeFileService(): base(new Mock<IHostingEnvironment>().Object, new Mock<IHttpContextAccessor>().Object)
        {

        }
    }
}
