using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Abp.AspNetCore.Mvc.Authorization;
using tibs.stem.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace tibs.stem.Web.Controllers
{
    public class PdfController : PdfControllerBase
    {
        public PdfController(IAppFolders appFolders, stemDbContext stemDbContext, IHostingEnvironment hostingEnvironment)
               : base(appFolders, stemDbContext, hostingEnvironment)
        {
        }
    }
}
