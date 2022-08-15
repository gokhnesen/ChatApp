using DateApp.Entity.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DateApp.Entity.DataContext
{
    public class DataContextModel:DbContext
    {
        public DataContextModel(DbContextOptions options):base(options)
        {

        }
        public DbSet<AppUser> Users  { get; set; }
    }
}
