using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rozproszone.Models
{
    public class CourseContext : DbContext
    {
        public DbSet<Course> Courses { get; set; }
        public DbSet<User> Users { get; set; }

        public CourseContext(DbContextOptions<CourseContext> options) : base(options)
        {
        }
    }
}
