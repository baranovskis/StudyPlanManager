using StudyPlanManager.Logic;
using StudyPlanManager.Models;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace StudyPlanManager.Controllers
{
    public class AvailableStudiesController : ApiController
    {
        [HttpGet]
        public IEnumerable<Study> GetAvailableStudies()
        {
            return SettingManager.Instance.AvailableStudies;
        }

        [HttpPost]
        public void SetAvailableStudies(List<Study> studies)
        {
            SettingManager.Instance.AvailableStudies = studies;
        }
    }
}