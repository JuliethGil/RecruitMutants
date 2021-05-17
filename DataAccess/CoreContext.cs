
// ***********************************************************************
// Assembly         : DataAccess
// Author           : Julieth Gil
// Created          : 13-05-2021
//
// ***********************************************************************
// <copyright file="DnaSequenceQuery.cs" company="">
//     Copyright (c) Julieth Gil. All rights reserved.
// </copyright>
// <summary>Contesxt DB</summary>

using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

#nullable disable

namespace DataAccess
{
    /// <summary>
    /// Class CoreContext.
    /// </summary>
    /// <remarks>Julieth Gil</remarks>
    [ExcludeFromCodeCoverage]
    public partial class CoreContext : DbContext
    {
        public CoreContext()
        {
        }

        public CoreContext(DbContextOptions<CoreContext> options)
            : base(options)
        {
        }

        public virtual DbSet<DnaSequence> DnaSequences { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "English_United States.1252");

            modelBuilder.Entity<DnaSequence>(entity =>
            {
                entity.Property(e => e.Id).UseIdentityAlwaysColumn();

                entity.Property(e => e.PersonDna).IsRequired();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
