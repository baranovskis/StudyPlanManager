using StudyPlanManager.Logic;
using StudyPlanManager.Models;
using StudyPlanManager.ViewModels;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace StudyPlanManager.Controllers
{
    public class StudyController : ApiController
    {
        [HttpGet]
        public IEnumerable<StudyProject> GetProjects()
        {
            // Get all study projects
            return StudyManager.Instance.StudyProjects;
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
        public IHttpActionResult CreateProject(CreatestudyProjectViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest("Not a valid data");
            
            var studyProject = StudyManager.Instance.CreateStudyProject(model.Name, model.ParentId);
            return Ok(studyProject);
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

            string trimmedStudyYear = model.Column.Replace("class_", String.Empty);

            int studyYear;
            int creditPoints;

            if (int.TryParse(trimmedStudyYear, out studyYear)
                && int.TryParse(model.Value, out creditPoints))
            {
                if (studyYear >= 0 && studyYear <= 2)
                {
                    study.CreditPoints[studyYear] = creditPoints;
                }
                else
                {
                    return BadRequest("Study year is invalid");
                }
            }
            else
            {
                return BadRequest("Study year or credit points are not numeric");
            }

            return Ok(study);
        }

        [HttpPost]
        public IHttpActionResult SaveProject(SavestudyProjectViewModel model, string id)
        {
            if (String.IsNullOrEmpty(id))
            {
                return BadRequest("Empty id");
            }

            bool savedSuccessfully = StudyManager.Instance.SaveStudyProject(id);
            if (!savedSuccessfully)
            {
                return NotFound();
            }

            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult DeleteProject(string id)
        {
            if (String.IsNullOrEmpty(id))
            {
                return BadRequest("Empty id");
            }

            bool deletedSuccessfully = StudyManager.Instance.DeleteStudyProject(id);
            if (!deletedSuccessfully)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}