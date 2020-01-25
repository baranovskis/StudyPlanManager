using StudyPlanManager.Logic;
using System;
using System.Web.Http;

namespace StudyPlanManager.Controllers
{
    public class SettingController : ApiController
    {
        // So, a few thoughts on a subject matter. My idea is that we can set up two kinds of things:
        // 1. Default settings.This will be set up from Home view and will include DefaultStudyProject and AvailableStudies list.
        // For that one, we have to add "Default settings" button on Home view.
        // 2. Current project settings. If we open "Settings" from the project view, we can specifically alter currently open project.
        // This includes project structure, we will have the option to remove studies from the structure, move them or add from AvailableStudies list.
        // The big difference will be that we will not be able to alter AvailableStudies list from these settings, only use it.
        // Optionally, we can add "Reset structure" button to reset the current project structure to DefaultStudyProject.Credit points should be kept.

        // This class hosts setting controller for the current project. It will only alter the current structure, not default ones.
        // For default settings go to DefaultStudyProjectController and AvailableStudiesController.

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
    }
}
