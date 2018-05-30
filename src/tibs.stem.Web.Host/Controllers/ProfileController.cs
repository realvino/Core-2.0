using Abp.AspNetCore.Mvc.Authorization;
using Abp.Domain.Repositories;
using Microsoft.AspNetCore.Hosting;
using tibs.stem.Products;

namespace tibs.stem.Web.Controllers
{
    [AbpMvcAuthorize]
    public class ProfileController : ProfileControllerBase
    {
        public ProfileController(IAppFolders appFolders, IHostingEnvironment hostingEnvironment)
            : base(appFolders, hostingEnvironment)
        {
        }
    }
}