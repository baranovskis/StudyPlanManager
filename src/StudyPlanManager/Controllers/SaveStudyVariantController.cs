using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace StudyPlanManager.Controllers
{
    public class SaveStudyVariantController : ApiController
    {
        [HttpGet]
        public void Get()
        {
            // Save to new file if variant has no file assigned.
            // Save to existing if filename exists.
        }
    }
}
