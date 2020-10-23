using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MapperAPI.Entities;

namespace MapperAPI.Data
{
    public class MapperAPIContextboo : DbContext
    {
        public MapperAPIContextboo (DbContextOptions<MapperAPIContextboo> options)
            : base(options)
        {
        }

        public DbSet<MapperAPI.Entities.Project> Project { get; set; }
    }
}
