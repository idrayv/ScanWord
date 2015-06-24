﻿using System.Data.Entity;
using ScanWord.Core.Entity;

namespace ScanWord.Data.Sql
{
    /// <summary>
    /// Represents a Unit Of Work with ScanWord database.
    /// </summary>
    public class ScanDataContainer : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ScanDataContainer"/> class.
        /// </summary>
        /// <param name="dataBaseName">The database name.</param>
        public ScanDataContainer(string dataBaseName)
            : base("name=" + dataBaseName)
        {
        }

        /// <summary>
        /// Gets or sets the files.
        /// </summary>
        public DbSet<File> Files { get; set; }

        /// <summary>
        /// Gets or sets the words.
        /// </summary>
        public DbSet<Word> Words { get; set; }

        /// <summary>
        /// Gets or sets the compositions.
        /// </summary>
        public DbSet<Composition> Compositions { get; set; }

        /// <summary>
        /// Configure model with fluent API.
        /// </summary>
        /// <param name="modelBuilder">The model builder.</param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<File>().HasKey(f => f.Id);
            modelBuilder.Entity<Word>().HasKey(w => w.Id);
            modelBuilder.Entity<Composition>().HasKey(c => c.Id);
            ////TODO: File full name index

            modelBuilder.Entity<Composition>().HasRequired<File>(s => s.File).WithMany(f => f.Compositions).HasForeignKey(c => c.FileId);
            modelBuilder.Entity<Composition>().HasRequired<Word>(s => s.Word).WithMany(w => w.Compositions).HasForeignKey(c => c.WordId);
        }
    }
}