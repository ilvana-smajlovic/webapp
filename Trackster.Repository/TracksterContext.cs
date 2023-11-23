using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Trackster.Core;

namespace Trackster.Repository
{
    public class TracksterContext : DbContext
    {
        public TracksterContext(DbContextOptions<TracksterContext> dbContext)
            : base(dbContext) { }

        public DbSet<Gender> Genders { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<GenreMedia> GenreMedia { get; set; }
        public DbSet<Media> Medias { get; set; }
        public DbSet<MediaPersonRole> MediaPersonRoles { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<RegisteredUser> RegisteredUsers { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<Status> Status { get; set; }
        public DbSet<TVShow> TVShows { get; set; }
        public DbSet<UserFavourites> UserFavourites { get; set; }
        public DbSet<WatchlistMovie> WatchlistMovies { get; set; }
        public DbSet<WatchlistTVShow> WatchlistTVShows { get; set; }
        public DbSet<CustomError> Errors { get; set; }
        public DbSet<AuthenticationToken> authenticationTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}