﻿using System.Data.Entity;
using ScanWord.Core.Entity;
using WatchWord.Domain.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace WatchWord.DataAccess
{
    /// <summary>Represents a Unit Of Work with WatchWord database.</summary>
    [DbConfigurationType(typeof(WatchDbConfiguration))]
    public class WatchDataContainer : DbContext
    {
        /// <summary>Initializes a new instance of the <see cref="WatchDataContainer"/> class.</summary>
        public WatchDataContainer() : base("name=WatchWord")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<WatchDataContainer, Migrations.Configuration>());
        }

        #region ScanWord
        /// <summary>Gets or sets the files.</summary>
        public DbSet<File> Files { get; set; }

        /// <summary>Gets or sets the words.</summary>
        public DbSet<Word> Words { get; set; }

        /// <summary>Gets or sets the compositions.</summary>
        public DbSet<Composition> Compositions { get; set; }
        #endregion

        /// <summary>Gets or sets the accounts.</summary>
        public DbSet<Account> Accounts { get; set; }

        /// <summary>Gets or sets the materials.</summary>
        public DbSet<Material> Materials { get; set; }

        /// <summary>Gets or sets the vocabulary of known words.</summary>
        public DbSet<KnownWord> KnownWords { get; set; }

        /// <summary>Gets or sets the vocabulary of learning words.</summary>
        public DbSet<LearnWord> LearnWords { get; set; }

        /// <summary>Gets or sets user's or site's settings.</summary>
        public DbSet<Setting> Settings { get; set; }

        /// <summary>Gets or sets translations.</summary>
        public DbSet<Translation> Translations { get; set; }

        /// <summary>Configures model with fluent API.</summary>
        /// <param name="modelBuilder">The model builder.</param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            #region ScanWord
            modelBuilder.Entity<File>().HasKey(f => f.Id);
            modelBuilder.Entity<Word>().HasKey(w => w.Id);
            modelBuilder.Entity<Composition>().HasKey(c => c.Id);
            #endregion

            modelBuilder.Entity<Account>().HasKey(a => a.Id);
            modelBuilder.Entity<Material>().HasKey(m => m.Id);
            modelBuilder.Entity<KnownWord>().HasKey(k => k.Id);
            modelBuilder.Entity<LearnWord>().HasKey(l => l.Id);
            modelBuilder.Entity<Translation>().HasKey(t => t.Id);

            // Settings
            modelBuilder.Entity<Setting>().HasKey(s => s.Id);
            modelBuilder.Entity<Setting>().Property(p => p.Int).IsOptional();
            modelBuilder.Entity<Setting>().Property(p => p.String).IsOptional();
            modelBuilder.Entity<Setting>().Property(p => p.Boolean).IsOptional();
            modelBuilder.Entity<Setting>().Property(p => p.Date).IsOptional();
            modelBuilder.Entity<Setting>().Property(p => p.Type).IsRequired();
            modelBuilder.Entity<Setting>().Property(p => p.Key).IsRequired();
        }
    }
}