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
                new ClassificationType("Drawing", false),
                new ClassificationType("Hentai", true),
                new ClassificationType("Neutral", false),
                new ClassificationType("Pornography", true),
                new ClassificationType("Sexy", true),
            };

            var results = new List<PerformanceResult>();
            var nsfwSpy = new NsfwSpy();

            foreach (var classificationType in classificationTypes)
            {
                var directory = Path.Combine(assetsPath, classificationType.Key);
                var files = Directory.GetFiles(directory).OrderBy(f => Guid.NewGuid()).ToList();

                var length = files.Count > 10000 ? 10000 : files.Count;
                files = files.Take(length).ToList();

                var pr = new PerformanceResult(classificationType.Key);

                nsfwSpy.ClassifyImages(files, (filePath, result) =>
                {
                    if (result.PredictedLabel == classificationType.Key) pr.CorrectAsserts++;
                    else pr.FailedAsserts++;

                    if (result.IsNsfw == classificationType.IsNsfw) pr.CorrectSpecialAsserts++;
                    else pr.FailedSpecialAsserts++;

                    Console.WriteLine($"{pr.Key} | Correct Asserts: {pr.CorrectAsserts} / {pr.TotalAsserts} ({(Convert.ToDouble(pr.CorrectAsserts) / pr.TotalAsserts) * 100}%) | IsNsfw: {pr.CorrectSpecialAsserts} / {pr.TotalSpecialAsserts} ({(Convert.ToDouble(pr.CorrectSpecialAsserts) / pr.TotalSpecialAsserts) * 100}%)");
                });

                results.Add(pr);
            }

            Console.WriteLine(Environment.NewLine);
            foreach (var pr in results)
            {
                Console.WriteLine($"{pr.Key} | Correct Asserts: {pr.CorrectAsserts} / {pr.TotalAsserts} ({(Convert.ToDouble(pr.CorrectAsserts) / pr.TotalAsserts) * 100}%) | IsNsfw: {pr.CorrectSpecialAsserts} / {pr.TotalSpecialAsserts} ({(Convert.ToDouble(pr.CorrectSpecialAsserts) / pr.TotalSpecialAsserts) * 100}%)");
            }
        }
    }
}
