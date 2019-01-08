using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CamhsFinal.Models;

namespace CamhsFinal.DAL
{
    public class CamhsContext : DbContext
    {
        public CamhsContext(DbContextOptions<CamhsContext> options)
            : base(options)
        { }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Referral> Referrals { get; set; }
        public DbSet<Episode> Episodes { get; set; }
    }
}
