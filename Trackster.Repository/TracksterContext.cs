using Microsoft.EntityFrameworkCore;

namespace Trackster.Repository
{
    public class TracksterContext : DbContext
    {
        public TracksterContext(DbContextOptions<TracksterContext> dbContext)
            :base(dbContext)
        { 
        
        }

    }
}