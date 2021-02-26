using System;
using System.Collections.Generic;
using System.Text;
using CoreRpsCrud_Elias.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CoreRpsCrud_Elias.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Trainee> Trainees { get; set; }
        public DbSet<Instractor> Instractor { get; set; }

        public DbSet<Admission> Admissions { get; set; }

    }
}
