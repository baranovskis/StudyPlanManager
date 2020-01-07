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
        public IEnumerable<StudyCourse> Get()
        {
            var studyVariant = StudyVariant.GetCurrentStudyVariant();

            return studyVariant.Courses;
        }

        [HttpPut]
        public IHttpActionResult Put(StudyViewModel model, string id)
        {
            if (string.IsNullOrEmpty(id))
                return BadRequest("Empty id");

            if (!ModelState.IsValid)
                return BadRequest("Not a valid data");

            // TODO: Code me Senpai :3
            //return NotFound();

            return Ok();
        }
    }
}
