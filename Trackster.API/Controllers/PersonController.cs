using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Trackster.Core;
using Trackster.Core.ViewModels;
using Trackster.Repository;

namespace Trackster.API.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly TracksterContext dbContext;

        public PersonController(TracksterContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [HttpPost]
        public ActionResult Add([FromForm] PersonAddVM x, string Picture)
        {
            bool userClear = Provjera(x, Picture);
            if (userClear)
            {
                var newPerson = new Person
                {
                    Name = x.Name,
                    LastName = x.LastName,
                    Birthday = x.Birthday,
                    Bio = x.Bio,
                    GenderID = x.Gender,
                    Picture = Picture
                };
                dbContext.People.Add(newPerson);
                dbContext.SaveChanges();
                return Ok();
            }
            return null;
        }

        private bool Provjera(PersonAddVM x, string picture)
        {
            if (x.Name.IsNullOrEmpty() || x.Name == "string" || x.LastName.IsNullOrEmpty() || x.LastName == "string"
                || x.Birthday == null || x.Bio.IsNullOrEmpty() || x.Bio == "string" || picture == null || x.Gender == null)
                return false;
            foreach (Person person in dbContext.People)
            {
                if (x.Name.ToLower() == person.Name.ToLower() && x.LastName.ToLower() == person.LastName.ToLower())
                    return false;
            }
            return true;
        }

        [HttpPost("{id}")]
        public ActionResult Update(int id, [FromForm] PersonAddVM x, string? Picture)
        {
            Person person = dbContext.People.FirstOrDefault(r => r.PersonId == id);
            if (person == null)
                return BadRequest("Pogresan ID");
            if (x.Name.IsNullOrEmpty() || x.Name == "string" || x.LastName.IsNullOrEmpty() || x.LastName == "string"
                || x.Birthday == null || x.Bio.IsNullOrEmpty() || x.Bio == "string" || x.Gender == null)
                return BadRequest("Loš unos");
           
            byte[] fileBytes = null;
            if (Picture != null)
            {
                person.Picture = Picture;
            }
            else
                person.Picture = person.Picture;

            person.Name = x.Name;
            person.LastName = x.LastName;
            person.Birthday = x.Birthday;
            person.Bio = x.Bio;
            person.GenderID = x.Gender;
            dbContext.SaveChanges();
            return Ok();
        }

        [HttpPost("{Id}")]
        public ActionResult Delete(int Id)
        {
            Person person = dbContext.People.Find(Id);

            if (person == null)
                return BadRequest("Pogresan ID");

            dbContext.People.Remove(person);
            dbContext.SaveChanges();
            return Ok(person);
        }

        [HttpGet("{Id}")]
        public ActionResult GetById(int Id)
        {
            return Ok(dbContext.People.Include(t=>t.Gender).Where(r => (r.PersonId == Id)).FirstOrDefault());
        }

        [HttpGet]
        public List<Person> GetAll(int? id, string? name, string? lastName)
        {
            var person = dbContext.People.Include(t => t.Gender)
                .Where(r => (id == null || id == r.PersonId) && (name == null || r.Name.ToLower() == name.ToLower()) && (lastName == null || r.LastName.ToLower() == lastName.ToLower()))
                .OrderBy(r => r.PersonId);
            return person.ToList();
        }
    }
}
