using StudyPlanManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace StudyPlanManager.Controllers
{
    public class StudyController : ApiController
    {
        // GET api/study
        public IEnumerable<StudyCourse> Get()
        {
            var courses = new List<StudyCourse>
            {
                new StudyCourse
                {
                    CourseName = "Pamatkursi",
                    Groups = new List<StudyGroup>
                    {
                        new StudyGroup
                        {
                            GroupName = "Valodu",
                            Studies = new List<Study>
                            {
                                new Study
                                {
                                    StudyName = "Latviesu valoda un literatura"
                                },
                                new Study
                                {
                                    StudyName = "Anglu val. I"
                                },
                                new Study
                                {
                                    StudyName = "Vacu val. I"
                                }
                            }
                        },
                        new StudyGroup
                        {
                            GroupName = "Sociala un pilsoniska",
                            Studies = new List<Study>
                            {
                                new Study
                                {
                                    StudyName = "Vestures un socialas zinatnes I"
                                }
                            }
                        },
                        new StudyGroup
                        {
                            GroupName = "Dabaszinatnu",
                            Studies = new List<Study>
                            {
                                new Study
                                {
                                    StudyName = "Fizika I"
                                },
                                new Study
                                {
                                    StudyName = "Kimija I"
                                },
                                new Study
                                {
                                    StudyName = "Biologija I"
                                }
                            }
                        },
                    }
                },

                new StudyCourse
                {
                    CourseName = "Specialie kursi",
                    Groups = new List<StudyGroup>
                    {
                        new StudyGroup
                        {
                            GroupName = "Valodu",
                            Studies = new List<Study>
                            {
                                new Study
                                {
                                    StudyName = "Anglu val. II"
                                },
                                new Study
                                {
                                    StudyName = "Vacu val. II"
                                }
                            }
                        },
                        new StudyGroup
                        {
                            GroupName = "Dabaszinatnu",
                            Studies = new List<Study>
                            {
                                new Study
                                {
                                    StudyName = "Fizika II"
                                },
                                new Study
                                {
                                    StudyName = "Kimija II"
                                },
                            }
                        },
                    }
                }
            };

            var rand = new Random();

            foreach (var course in courses)
            {
                foreach (var group in course.Groups)
                {
                    foreach (var study in group.Studies)
                    {
                        for (var i = 0; i < study.CreditPoints.Length; i++)
                        {
                            study.CreditPoints[i] = rand.Next(0, 10);
                        }
                    }
                }
            }

            return courses;
        }
    }
}
