using StudyPlanManager.Logic;
using StudyPlanManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace StudyPlanManager.Controllers
{
    public class StudyRuleController : ApiController
    {
        [HttpGet]
        public IEnumerable<StudyInfoMessage> GetInfoMessages(string id)
        {
            var messages = new List<StudyInfoMessage>();

            var studyProject = StudyManager.Instance.GetStudyProject(id);

            if (studyProject != null)
            {
                messages = StudyRuleManager.CheckStudyProject(studyProject);
            }

            return messages;
        }
    }
}
