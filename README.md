<img src="https://raw.githubusercontent.com/d00ML0rDz/NsfwSpy/main/NsfwSpy-Logo.jpg" alt="NsfwSpy Logo" width="400"/>

# Introduction
NsfwSpy is a nudity/pornography image classifier built for .NET Core 2.0 and later to aid in moderating user-generated content for various different application types, written in C#. The [ML.NET](https://github.com/dotnet/machinelearning) model has been trained against the ResNet V250 neural net architecture with over 360,000 images, from 5 different categories:

| Label       | Description |
| ----------- | ----------- |
| Pornography | Images that depict sexual acts and nudity. |
| Sexy        | Images of people in their underwear and men who are topless. |
| Hentai      | Drawings or animations of nudity and sexual acts. |
| Neutral     | Images that are not sexual in nature. |
| Drawing     | Drawings or animations that are not sexual in nature. |

# Performance
NsfwSpy isn't perfect, but the accuracy should be good enough to detect approximately 96% of Nsfw images, those being images that are classed as pornography, sexy or hentai.

| a | Pornography | Sexy | Hentai | Neutral | Drawing
| --- | --- | --- | --- | --- | --- |
| IsNsfw  <sub><sup>(pornography + sexy + hentai >= 0.5)</sup></sub> | 96.4% | 96.7% | 95.7% | 2.3% | 2.5%
| Correctly Predicted Label | 86.8% | 82.8% | 87.1% | 97.6% | 89.6%

# Quick Start
This project is available as a [Nuget](https://www.nuget.org/packages/NsfwSpy/) package and can be installed with the following commands:

Package Manager
```
Install-Package NsfwSpy
```

.NET CLI
```
dotnet add package NsfwSpy
```

### Classify an Image File
```csharp
var nsfwSpy = new NsfwSpy();
var result = nsfwSpy.ClassifyImage(@"C:\Users\username\Documents\flower.jpg");
```

### Classify a Web Image
```csharp
var uri = new Uri("https://raw.githubusercontent.com/d00ML0rDz/NsfwSpy/main/NsfwSpy.Test/Assets/flower.jpg");
var nsfwSpy = new NsfwSpy();
var result = nsfwSpy.ClassifyImage(uri);
```

### Classify Multiple Image Files
```csharp
var files = Directory.GetFiles(@"C:\Users\username\Pictures");
var nsfwSpy = new NsfwSpy();
nsfwSpy.ClassifyImages(files, (filePath, result) =>
{
    Console.WriteLine($"{filePath} - {result.PredictedLabel}");
});
```
