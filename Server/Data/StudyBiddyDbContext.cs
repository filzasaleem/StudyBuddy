using Microsoft.EntityFrameworkCore;

namespace Server;

public class StudyBiddyDbContext : DbContext
{
    public StudyBiddyDbContext(DbContextOptions<StudyBiddyDbContext> options)
        : base(options) { }

    public DbSet<User> Users { get; set; }
}
