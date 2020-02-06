using StudyPlanManager.Logic;
using StudyPlanManager.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using StudyPlanManager.Utility;

namespace StudyPlanManager.Controllers
{
    public class SettingsController : ApiController
    {
        public IHttpActionResult GetSettings()
        {
            return Ok(new SetupViewModel
            {
                Studies = SettingManager.Instance.AvailableStudies,
                Groups = SettingManager.Instance.AvailableStudyGroups,
                Courses = SettingManager.Instance.AvailableStudyCourses
            });
        }

        [HttpPost]
        public IHttpActionResult SaveSettings(SetupViewModel model)
        {
            SettingManager.Instance.AvailableStudies = model.Studies;
            SettingManager.Instance.AvailableStudyGroups = model.Groups;
            SettingManager.Instance.AvailableStudyCourses = model.Courses;
            SettingManager.Instance.SaveSettings();

            return Ok();
        }

        [HttpGet]
        public IHttpActionResult GetProjectSettings(string id)
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

            var settings = new SettingsViewModel
            {
                Name = studyProject.Name
            };

            var usedStudies = new List<string>();
            var usedGroups = new List<string>();
            var usedCourses = new List<string>();

            foreach (var course in studyProject.Courses)
            {
                var courseNode = NewCourseNode(course.TreeId, course.CourseName);

                foreach (var group in course.Groups)
                {
                    var groupNode = NewGroupNode(group.TreeId, group.GroupName);

                    foreach (var study in group.Studies)
                    {
                        groupNode.Items.Add(NewStudyNode(study.TreeId, study.StudyName));
                        usedStudies.Add(study.TreeId);
                    }

                    courseNode.Items.Add(groupNode);
                    usedGroups.Add(group.TreeId);
                }

                settings.ProjectTree.Add(courseNode);
                usedCourses.Add(course.TreeId);
            }

            foreach (var study in SettingManager.Instance.AvailableStudies.Where(e => !usedStudies.Contains(e.TreeId)))
            {
                settings.AvailableStudies.Add(NewStudyNode(study.TreeId, study.StudyName));
            }

            foreach (var group in SettingManager.Instance.AvailableStudyGroups.Where(e => !usedGroups.Contains(e.TreeId)))
            {
                settings.AvailableGroups.Add(NewGroupNode(group.TreeId, group.GroupName));
            }

            foreach (var course in SettingManager.Instance.AvailableStudyCourses.Where(e => !usedCourses.Contains(e.TreeId)))
            {
                settings.AvailableCourses.Add(NewCourseNode(course.TreeId, course.CourseName));
            }

            return Ok(settings);
        }

        [HttpPost]
        public IHttpActionResult SaveProjectSettings(SaveSettingsNodeViewModel model, string id)
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

            // Backup data :)
            var clone = studyProject.Clone();

            // Cleanup
            studyProject.Courses.Clear();

            try
            {
                foreach (var courseNode in model.Items)
                {
                    var course = clone.GetCourseByTreeId(courseNode.TreeId);

                    if (course == null)
                    {
                        course = SettingManager.Instance.GetCourseByTreeId(courseNode.TreeId);
                    }

                    course = course.Clone();
                    course.Groups.Clear();

                    foreach (var groupNode in courseNode.Items)
                    {
                        var group = clone.GetGroupByTreeId(groupNode.TreeId);

                        if (group == null)
                        {
                            group = SettingManager.Instance.GetGroupByTreeId(groupNode.TreeId);
                        }

                        group = group.Clone();
                        group.Studies.Clear();

                        foreach (var studyNode in groupNode.Items)
                        {
                            var study = clone.GetStudyByTreeId(studyNode.TreeId);

                            if (study == null)
                            {
                                study = SettingManager.Instance.GetStudyByTreeId(studyNode.TreeId);
                            }

                            study = study.Clone();
                            group.Studies.Add(study);
                        }

                        course.Groups.Add(group);
                    }

                    studyProject.Courses.Add(course);
                }
            }
            catch
            {
                // Restore backup
                foreach (var course in clone.Courses)
                {
                    studyProject.Courses.Add(course);
                }

                return InternalServerError();
            }

            return Ok();
        }

        private SettingsNodeViewModel NewStudyNode(string treeId, string name)
        {
            return new SettingsNodeViewModel
            {
                TreeId = treeId,
                Name = name,
                Type = "study"
            };
        }

        private SettingsNodeViewModel NewGroupNode(string treeId, string name)
        {
            var node = new SettingsNodeViewModel
            {
                TreeId = treeId,
                Name = name,
                Type = "group"
            };

            node.AllowedTypes.Add("study");
            return node;
        }

        private SettingsNodeViewModel NewCourseNode(string treeId, string name)
        {
            var node = new SettingsNodeViewModel
            {
                TreeId = treeId,
                Name = name,
                Type = "course"
            };

            node.AllowedTypes.Add("group");
            return node;
        }
    }
}
