using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace StudyPlanManager.Controllers
{
    class NeedsNamingStudyVariantController : ApiController
    {
        [HttpGet]
        public bool Get()
        {
            // Check if variant already named.
            return true;
        }

    }
}