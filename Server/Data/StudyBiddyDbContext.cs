using Microsoft.EntityFrameworkCore;
using Servr.Models;

namespace Server.Data;

public class StudyBiddyDbContext : DbContext
{
    public StudyBiddyDbContext(DbContextOptions<StudyBiddyDbContext> options)
        : base(options) { }

    public DbSet<User> Users { get; set; }
}
