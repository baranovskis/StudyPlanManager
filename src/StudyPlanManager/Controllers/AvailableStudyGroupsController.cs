using StudyPlanManager.Logic;
using StudyPlanManager.Models;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace StudyPlanManager.Controllers
{
    public class AvailableStudyGroupsController : ApiController
    {
        [HttpGet]
        public IEnumerable<StudyGroup> GetAvailableStudyGroups()
        {
            return SettingManager.Instance.AvailableStudyGroups;
        }

        [HttpPost]
        public void SetAvailableStudyGroups(List<StudyGroup> studyGroups)
        {
            SettingManager.Instance.AvailableStudyGroups = studyGroups;
        }
    }
}