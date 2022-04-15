using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace NsfwSpyNS.PerformanceTesting
{
    class Program
    {
        static void Main(string[] args)
        {
            var assetsPath = @"E:\NsfwSpy\Images";
            var testImagesPath = @"E:\NsfwSpy\Test";

            var classificationTypes = new[]
            {
                "Hentai",
                "Neutral",
                "Pornography",
                "Sexy",
            };

            var results = new List<PerformanceResult>();
            var nsfwSpy = new NsfwSpy();

            foreach (var classificationType in classificationTypes)
            {
                var testFilesDirectory = Path.Combine(testImagesPath, classificationType);
                var testFiles = Directory.Exists(testFilesDirectory) ? Directory.GetFiles(testFilesDirectory).ToList() : new List<string>();

                if (!testFiles.Any())
                {
                    var directory = Path.Combine(assetsPath, classificationType);
                    var files = Directory.GetFiles(directory).OrderBy(f => Guid.NewGuid()).ToList();

                    var length = files.Count > 10000 ? 10000 : files.Count;
                    files = files.Take(length).ToList();

                    Console.WriteLine($"Copying {classificationType} test files...");

                    if (!Directory.Exists(testFilesDirectory))
                        Directory.CreateDirectory(testFilesDirectory);

                    Parallel.ForEach(files, file => {
                        var filename = Path.GetFileName(file);
                        var dest = Path.Combine(testFilesDirectory, filename);
                        File.Copy(file, dest);
                    });

                    testFiles = Directory.GetFiles(testFilesDirectory).ToList();
                }

                var pr = new PerformanceResult(classificationType);

                nsfwSpy.ClassifyImages(testFiles, (filePath, result) =>
                {
                    pr.Results.Add(result);
                    Console.WriteLine($"{pr.Key} | Correct Asserts: {pr.CorrectAsserts} / {pr.TotalAsserts} ({(Convert.ToDouble(pr.CorrectAsserts) / pr.TotalAsserts) * 100}%) | IsNsfw: {pr.NsfwAsserts} / {pr.TotalAsserts} ({(Convert.ToDouble(pr.NsfwAsserts) / pr.TotalAsserts) * 100}%)");
                });

                results.Add(pr);
            }

            Console.WriteLine(Environment.NewLine);
            foreach (var pr in results)
            {
                Console.WriteLine($"{pr.Key} | Correct Asserts: {pr.CorrectAsserts} / {pr.TotalAsserts} ({(Convert.ToDouble(pr.CorrectAsserts) / pr.TotalAsserts) * 100}%) | IsNsfw: {pr.NsfwAsserts} / {pr.TotalAsserts} ({(Convert.ToDouble(pr.NsfwAsserts) / pr.TotalAsserts) * 100}%)");
            }

            Console.WriteLine(Environment.NewLine);
            Console.WriteLine("Confusion Matrix\n");

            Console.WriteLine("\t\t\tPredicted Label");
            Console.WriteLine("Actual Label\t\tHentai\t\tNeutral\t\tPornography\tSexy");
            Console.WriteLine();
            foreach (var pr in results)
            {
                Console.WriteLine($"{pr.Key}\t\t{(pr.Key != "Pornography" ? "\t" : "")}{pr.HentaiAsserts}\t\t{pr.NeutralAsserts}\t\t{pr.PornographyAsserts}\t\t{pr.SexyAsserts}");
            }

            Console.WriteLine(Environment.NewLine);
            Console.WriteLine("Average Confidence\n");

            foreach (var pr in results)
            {
                Console.WriteLine($"{pr.Key}\t\t{(pr.Key != "Pornography" ? "\t" : "")}{pr.Results.Sum(r => r.ToDictionary()[pr.Key]) / pr.Results.Count}");
            }
        }
    }
}
