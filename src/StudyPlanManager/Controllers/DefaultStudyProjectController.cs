using StudyPlanManager.Logic;
using StudyPlanManager.Models;
using System;
using System.Web.Http;

namespace StudyPlanManager.Controllers
{
    public class DefaultStudyProjectController : ApiController
    {
        [HttpGet]
        public StudyProject GetDefaultStudyProject()
        {
            return SettingManager.Instance.DefaultStudyProject;
        }

        [HttpPost]
        public void SetDefaultStudyProject(StudyProject studyProject)
        {
            SettingManager.Instance.DefaultStudyProject = studyProject;
        }
    }
}