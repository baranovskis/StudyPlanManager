using StudyPlanManager.Logic;
using System;
using System.Web.Http;

namespace StudyPlanManager.Controllers
{
    public class SettingController : ApiController
    {
        [HttpGet]
        public IHttpActionResult GetSettings(string id)
        {
            if (String.IsNullOrEmpty(id))
            {
                return BadRequest("Empty id");
            }

            var studyProject = StudyManager.Instance.GetStudyProject(id);

            if (studyProject == null)
            {
                return NotFound();
            }

            return Ok(studyProject);
        }

        /*[HttpGet]
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
        }*/
    }
}
