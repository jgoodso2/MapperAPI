﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using MapperAPI.Entities; 

namespace MapperAPI.Entities
{
    public class MapperContext : DbContext
    {
        public MapperContext(DbContextOptions<MapperContext> options)
     : base(options)   //base class already has a constructor we like, so use it
        {
            Database.Migrate();
        }

        public DbSet<Project> Projects { get; set; }
        public DbSet<PlanViewProject> PlanViewProjects { get; set; }

        public DbSet<AuthorizedPlanViewProject> AuthorizedPlanViewProjects { get; set; } 

        public DbSet<AdminUser> AdminUsers { get; set; }

        public MapperContext() {    // another constructor
        }   

        
    }
}
