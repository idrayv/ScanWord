﻿using System.Data.Entity;
using ScanWord.Core.Entity;
using ScanWord.Data.Sql.Repositories.Generic;

namespace ScanWord.Data.Sql.Repositories
{
    /// <summary>Represents repository pattern for words.</summary>
    public class WordsRepository : EFGenericRepository<Word, int>, IWordsRepository
    {
        /// <summary>Initializes a new instance of the <see cref="WordsRepository{Word,int}"/> class.</summary>
        /// <param name="context">Entity framework context.</param>
        public WordsRepository(DbContext context) : base(context)
        {
        }
    }
}