﻿using System;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchedBettingAssistant.DataAccess
{
    public class MatchedBettingAssistantDbContext : DbContext
    {
        public MatchedBettingAssistantDbContext(IDatabaseInitializer<MatchedBettingAssistantDbContext> intialiser)
        {
            Database.SetInitializer(intialiser);
        }

        public DbSet<DataModel.Account> Accounts { get; set; }

        public DbSet<DataModel.TransactionDetail> TransactionDetails { get; set; }

        public DbSet<DataModel.BetType> BetTypes { get; set; }

        public DbSet<DataModel.OfferType> OfferTypes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DataModel.Bookmaker>().ToTable("Bookmaker");
            modelBuilder.Entity<DataModel.Wallet>().ToTable("Wallet");
            modelBuilder.Entity<DataModel.Bank>().ToTable("Bank");

            modelBuilder.Entity<DataModel.TransactionDetail>().HasOptional(t => t.BetType);
            modelBuilder.Entity<DataModel.TransactionDetail>().HasOptional(o => o.OfferType);


            base.OnModelCreating(modelBuilder);
        }
    }
}
