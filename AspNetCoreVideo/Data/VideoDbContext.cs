using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AspNetCoreVideo.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace AspNetCoreVideo.Data
{
    public class VideoDbContext : IdentityDbContext<User>  // Connecting to our own database in this class
                                             // Defines entity classes as DbSet properties, which are mirrored as tables in the database
    {
        public DbSet<Video> Videos { get; set; }

        public VideoDbContext(DbContextOptions<VideoDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
