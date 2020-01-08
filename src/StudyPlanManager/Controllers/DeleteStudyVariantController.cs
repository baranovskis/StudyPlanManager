using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace StudyPlanManager.Controllers
{
    public class DeleteStudyVariantController : ApiController
    {
        [HttpGet]
        public void Get(string id)
        {
            // Find by id and delete from memory + file.
        }
    }
}
