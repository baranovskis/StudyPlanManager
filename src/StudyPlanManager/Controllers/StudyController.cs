using StudyPlanManager.Logic;
using StudyPlanManager.Models;
using StudyPlanManager.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace StudyPlanManager.Controllers
{
    public class StudyController : ApiController
    {
        [HttpGet]
        public IEnumerable<StudyProject> GetProjects()
        {
            return StudyManager.Instance.StudyProjects.OrderBy(x => x.Name);
        }

        [HttpGet]
        public IHttpActionResult GetProject(string id)
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

        [HttpPost]
        public IHttpActionResult CreateProject(CreateStudyProjectViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest("Not a valid data");
            
            return Ok(StudyManager.Instance.CreateStudyProject(model.Name, model.ParentId));
        }

        [HttpPost]
        public IHttpActionResult SaveProject(SaveStudyProjectViewModel model, string id)
        {
            if (String.IsNullOrEmpty(id))
            {
                return BadRequest("Empty id");
            }

            bool savedSuccessfully = StudyManager.Instance.SaveStudyProject(id, model.Name);

            if (!savedSuccessfully)
            {
                return NotFound();
            }

            return Ok();
        }

        [HttpPut]
        public IHttpActionResult UpdateProject(StudyViewModel model, string id)
        {
            if (String.IsNullOrEmpty(id))
            {
                return BadRequest("Empty id");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest("Not a valid data");
            }

            var study = StudyManager.Instance.GetStudy(id, model.TreeId);

            if (study == null)
            {
                return NotFound();
            }

            int studyYear;
            string trimmedStudyYear = model.Column;
            trimmedStudyYear = trimmedStudyYear.Replace("class_", String.Empty);

            if (!int.TryParse(trimmedStudyYear, out studyYear))
            {
                return BadRequest("Study year is not numeric");
            }

            int creditPoints;
            string creditPointsText = model.Value;
            creditPointsText = String.IsNullOrEmpty(creditPointsText) ? "0" : creditPointsText;

            if (!int.TryParse(creditPointsText, out creditPoints))
            {
                return BadRequest("Credit points are  not numeric");
            }

            if (studyYear >= 0 && studyYear <= 2)
            {
                study.CreditPoints[studyYear] = creditPoints;
            }
            else
            {
                return BadRequest("Study year is invalid");
            }

            return Ok(study);
        }

        [HttpDelete]
        public IHttpActionResult DeleteProject(string id)
        {
            if (String.IsNullOrEmpty(id))
            {
                return BadRequest("Empty id");
            }

            bool result = StudyManager.Instance.DeleteStudyProject(id);

            if (!result)
            {
                return NotFound();
            }

            return Ok();
        }

        [HttpPatch]
        public IHttpActionResult RestoreProject(string id)
        {
            if (String.IsNullOrEmpty(id))
            {
                return BadRequest("Empty id");
            }

            bool result = StudyManager.Instance.RestoreStudyProject(id);

            if (!result)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}