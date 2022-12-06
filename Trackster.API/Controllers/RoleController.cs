using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Trackster.Core.ViewModels;
using Trackster.Core;
using Trackster.Repository;

namespace Trackster.API.Controllers
{
    
    //[Route("api/[controller]")]
    [ApiController]
    [Route("[controller]/[action]")]
    public class RoleController : ControllerBase
    {
        private readonly TracksterContext dbContext;

        public RoleController(TracksterContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet("{Id}")]
        public ActionResult GetById(int Id)
        {
            return Ok(dbContext.Roles.FirstOrDefault(r => (r.RoleID == Id)));
        }
        [HttpGet]
        public List<Role> GetAll()
        {
            var uloga = dbContext.Roles.OrderBy(r => r.RoleID);
            return uloga.Take(50).ToList();
        }
    }
}