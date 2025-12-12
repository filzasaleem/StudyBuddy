using Microsoft.EntityFrameworkCore;
using Server.Models;
using Servr.Models;

namespace Server.Data;

public class StudyBuddyDbContext : DbContext
{
    public StudyBuddyDbContext(DbContextOptions<StudyBuddyDbContext> options)
        : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Event> Events { get; set; }
    public DbSet<Connection> Connections { get; set; }
    
}
