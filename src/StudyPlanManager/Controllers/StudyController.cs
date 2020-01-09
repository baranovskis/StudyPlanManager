using StudyPlanManager.Logic;
using StudyPlanManager.Models;
using StudyPlanManager.ViewModels;
using System.Collections.Generic;
using System.Web.Http;

namespace StudyPlanManager.Controllers
{
    public class StudyController : ApiController
    {
        [HttpGet]
        public IEnumerable<StudyVariant> GetProjects()
        {
            // Get all study variants
            return StudyManager.Instance.StudyVariants;
        }

        [HttpGet]
        public IHttpActionResult GetProject(string id)
        {
            if (string.IsNullOrEmpty(id))
                return BadRequest("Empty id");
            
            var studyVariant = StudyManager.Instance.GetStudyVariant(id);
            
            if (studyVariant == null)
                return NotFound();

            return Ok(studyVariant);
        }

        [HttpPost]
        public IHttpActionResult CreateProject(StudyVariantViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest("Not a valid data");
            
            var studyVariant = StudyManager.Instance.CreateStudyVariant(model.Name, model.ParentId);
            return Ok(studyVariant);
        }

        [HttpPut]
        public IHttpActionResult UpdateProject(StudyViewModel model, string id)
        {
            if (string.IsNullOrEmpty(id))
                return BadRequest("Empty id");

            if (!ModelState.IsValid)
                return BadRequest("Not a valid data");

            // TODO: Code me Senpai :3
            //return NotFound();

            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult DeleteProject(string id)
        {
            if (string.IsNullOrEmpty(id))
                return BadRequest("Empty id");

            if (!StudyManager.Instance.DeleteStudyVariant(id))
                return NotFound();

            return Ok();
        }
    }
}
