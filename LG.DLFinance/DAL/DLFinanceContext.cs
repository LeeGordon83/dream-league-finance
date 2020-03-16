using LG.DLFinance.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace LG.DLFinance.DAL
{


        public class DLFinanceContext: IdentityDbContext<AppUser>
        {
        
        public DLFinanceContext() : base("DLFinanceContext")
        {
                {
                    this.Configuration.AutoDetectChangesEnabled = false;
                    //System.Data.Entity.Database.SetInitializer<DLFinanceContext>(null);
                }
        }

        public virtual DbSet<Fees> Fees { get; set; }

        public virtual DbSet<Transaction> Transaction { get; set; }

        public virtual DbSet<Manager> Managers { get; set; }

        public virtual DbSet<Emails> Emails { get; set; }

        public virtual DbSet<Week> Week { get; set; }

        public virtual DbSet<Prizes> Prizes { get; set; }

        public virtual DbSet<History> History { get; set; }

        public virtual DbSet<Jackpot> Jackpot { get; set; }


        public virtual void SetModified(object obj)
        {
            this.Entry(obj).State = System.Data.Entity.EntityState.Modified;
        }
    }
}