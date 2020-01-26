using StudyPlanManager.Logic;
using StudyPlanManager.Models;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace StudyPlanManager.Controllers
{
    public class AvailableStudyCoursesController : ApiController
    {
        [HttpGet]
        public IEnumerable<StudyCourse> GetAvailableStudyCourses()
        {
            return SettingManager.Instance.AvailableStudyCourses;
        }

        [HttpPost]
        public void SetAvailableStudyCourses(List<StudyCourse> studyCourses)
        {
            SettingManager.Instance.AvailableStudyCourses = studyCourses;
        }
    }
}