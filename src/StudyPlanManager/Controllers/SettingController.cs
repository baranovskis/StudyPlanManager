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
    public class SettingController : ApiController
    {
        [HttpGet]
        public StudyProject GetDefaultStudyProject()
        {
            return SettingManager.Instance.DefaultStudyProject;
        }

        [HttpGet]
        public IEnumerable<Study> GetAvailableStudies()
        {
            return SettingManager.Instance.AvailableStudies;
        }

        [HttpPost]
        public void SetDefaultStudyProject(StudyProject studyProject)
        {
            SettingManager.Instance.DefaultStudyProject = studyProject;
        }

        [HttpPost]
        public void SetAvailableStudies(List<Study> studies)
        {
            SettingManager.Instance.AvailableStudies = studies;
        }
    }
}
