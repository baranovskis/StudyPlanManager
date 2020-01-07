using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace StudyPlanManager.Controllers
{
    public class StudyVariantListController : ApiController
    {
        [HttpGet]
        public IEnumerable<Tuple<string, string>> Get()
        {
            var studyVariants = new List<Tuple<string, string>>
            {
                new Tuple<string, string>("Variant 1", Guid.NewGuid().ToString("N")),
                new Tuple<string, string>("Variant 2", Guid.NewGuid().ToString("N")),
                new Tuple<string, string>("Variant 3", Guid.NewGuid().ToString("N"))
            };

            return studyVariants;
        }
    }
}
