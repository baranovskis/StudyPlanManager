using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace StudyPlanManager.Controllers
{
    class NeedsSavingStudyVariantController : ApiController
    {
        [HttpGet]
        public bool Get()
        {
            // Return true if not empty + not saved.
            return true;
        }
    }
}
