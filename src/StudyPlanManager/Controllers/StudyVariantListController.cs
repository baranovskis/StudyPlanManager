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

        [HttpPut]
        public IHttpActionResult Put(string id)
        {
            // Open variant by id
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest("Empty id");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest("Not a valid data");
            }

            // TODO: Code me Senpai :3
            //return NotFound();

            return Ok();
        }
    }
}
