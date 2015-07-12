﻿using System.Linq;
using Microsoft.Practices.Unity;
using ScanWord.Domain.Common;
using ScanWord.Domain.Data;
using ScanWord.Infrastructure;

namespace ScanWord.Console.UI
{
    using System;

    /// <summary>Console for ScanWord program.</summary>
    public class Program
    {
        /// <summary>Unity container.</summary>
        private static readonly IUnityContainer Container = UnityConfig.GetConfiguredContainer();

        /// <summary>Entry point of the test console program.</summary>
        /// <param name="args">Console arguments.</param>
        public static void Main(string[] args)
        {
            var repository = Container.Resolve<IScanDataRepository>();
            var parser = Container.Resolve<IScanWordParser>();

            var start = Environment.TickCount;
            var compositions = parser.ParseFile("C:/Interstellar.srt");
            Console.WriteLine("Parse File time: {0} ms.", Environment.TickCount - start);

            Console.WriteLine("All words: " + compositions.Count);
            var uniqueWords = compositions.GroupBy(w => w.Word.TheWord).Count();
            Console.WriteLine("Unique words: " + uniqueWords);

            start = Environment.TickCount;
            repository.AddCompositionsAsync(compositions).Wait();
            Console.WriteLine("Add Compositions time: {0} ms.", Environment.TickCount - start);

            Console.ReadLine();
        }
    }
}