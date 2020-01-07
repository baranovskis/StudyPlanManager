using StudyPlanManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace StudyPlanManager.Controllers
{
    public class NewStudyVariantController : ApiController
    {
        [HttpGet]
        public void Get()
        {
            var studyVariant = StudyVariant.GetDefaultStudyVariant();

            StudyVariant.SetCurrentStudyVariant(studyVariant);
        }
    }
}
