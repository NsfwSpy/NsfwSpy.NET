using Microsoft.ML;
using Microsoft.ML.Vision;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace NsfwSpyNS.Train
{
    class Program
    {
        static void Main(string[] args)
        {
            var projectDirectory = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "../../../"));
            var workspaceRelativePath = Path.Combine(projectDirectory, "Workspace");
            var saveModelPath = Path.Combine(projectDirectory, "Models");
            var assetsPath = @"D:\Nsfw\Images";

            var mlContext = new MLContext();

            var images = LoadImagesFromDirectory(assetsPath);
            var imageData = mlContext.Data.LoadFromEnumerable(images);
            var shuffledData = mlContext.Data.ShuffleRows(imageData);

            var preprocessingPipeline = mlContext.Transforms.Conversion.MapValueToKey(
                    inputColumnName: "Label",
                    outputColumnName: "LabelAsKey")
                .Append(mlContext.Transforms.LoadRawImageBytes(
                    outputColumnName: "Image",
                    imageFolder: assetsPath,
                    inputColumnName: "ImagePath"));

            var preProcessedData = preprocessingPipeline
                                .Fit(shuffledData)
                                .Transform(shuffledData);

            var trainSplit = mlContext.Data.TrainTestSplit(data: preProcessedData, testFraction: 0.1);

            var trainSet = trainSplit.TrainSet;
            var validationSet = trainSplit.TestSet;

            var classifierOptions = new ImageClassificationTrainer.Options()
            {
                FeatureColumnName = "Image",
                LabelColumnName = "LabelAsKey",
                ValidationSet = validationSet,
                Arch = ImageClassificationTrainer.Architecture.ResnetV250,
                MetricsCallback = (metrics) => Console.WriteLine(metrics),
                TestOnTrainSet = true,
                ReuseTrainSetBottleneckCachedValues = true,
                ReuseValidationSetBottleneckCachedValues = true,
                Epoch = 250,
                BatchSize = 32,
                LearningRate = 0.0075f,
                EarlyStoppingCriteria = null,
                WorkspacePath = workspaceRelativePath
            };

            var trainingPipeline = mlContext.MulticlassClassification.Trainers.ImageClassification(classifierOptions)
                .Append(mlContext.Transforms.Conversion.MapKeyToValue("PredictedLabel"));

            var trainedModel = trainingPipeline.Fit(trainSet);

            saveModelPath = Path.Combine(saveModelPath, "NsfwSpyModel.zip");
            mlContext.Model.Save(trainedModel, trainSet.Schema, saveModelPath);

            Console.WriteLine("Complete");
        }

        public static IEnumerable<ImageData> LoadImagesFromDirectory(string folder)
        {
            var allowedFileExtensions = new[] { ".jpg", ".jpeg", ".png" };
            var files = Directory.GetFiles(folder, "*", searchOption: SearchOption.AllDirectories);

            foreach (var file in files)
            {
                var extension = Path.GetExtension(file);
                if (!allowedFileExtensions.Contains(extension))
                    continue;

                var label = Directory.GetParent(file).Name;

                yield return new ImageData()
                {
                    ImagePath = file,
                    Label = label
                };
            }
        }
    }
}
