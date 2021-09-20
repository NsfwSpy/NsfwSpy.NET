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
            var assetsPath = @"D:\Nsfw\Images";

            var classificationTypes = new[]
            {
                "Drawing",
                "Hentai",
                "Neutral",
                "Pornography",
                "Sexy",
            };

            var results = new List<PerformanceResult>();
            var nsfwSpy = new NsfwSpy();

            foreach (var classificationType in classificationTypes)
            {
                var directory = Path.Combine(assetsPath, classificationType);
                var files = Directory.GetFiles(directory).OrderBy(f => Guid.NewGuid()).ToList();

                var length = files.Count > 10000 ? 10000 : files.Count;
                files = files.Take(length).ToList();

                var pr = new PerformanceResult(classificationType);

                nsfwSpy.ClassifyImages(files, (filePath, result) =>
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
            Console.WriteLine("Actual Label\t\tDrawing\t\tHentai\t\tNeutral\t\tPornography\t\tSexy");
            Console.WriteLine();
            foreach (var pr in results)
            {
                Console.WriteLine($"{pr.Key}\t\t{(pr.Key != "Pornography" ? "\t" : "")}{pr.DrawingAsserts}\t\t{pr.HentaiAsserts}\t\t{pr.NeutralAsserts}\t\t{pr.PornographyAsserts}\t\t{pr.SexyAsserts}");
            }
        }
    }
}
