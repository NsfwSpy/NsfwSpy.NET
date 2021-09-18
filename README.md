<img src="https://raw.githubusercontent.com/d00ML0rDz/NsfwSpy/main/_art/NsfwSpy-Logo.jpg" alt="NsfwSpy Logo" width="400"/>

# Introduction
NsfwSpy is a nudity/pornography image classifier built for .NET Core 2.0 and later to aid in moderating user-generated content for various different application types, written in C#. The [ML.NET](https://github.com/dotnet/machinelearning) model has been trained against the ResNet V250 neural net architecture with 380,000 images (84GB), from 5 different categories:

| Label       | Description |
| ----------- | ----------- |
| Pornography | Images that depict sexual acts and nudity. |
| Sexy        | Images of people in their underwear and men who are topless. |
| Hentai      | Drawings or animations of sexual acts and nudity. |
| Neutral     | Images that are not sexual in nature. |
| Drawing     | Drawings or animations that are not sexual in nature. |

<img src="https://raw.githubusercontent.com/d00ML0rDz/NsfwSpy/main/_art/Examples.gif" />

# Performance
NsfwSpy isn't perfect, but the accuracy should be good enough to detect approximately 96% of Nsfw images, those being images that are classed as pornography, sexy or hentai.

|   | Pornography | Sexy | Hentai | Neutral | Drawing
| --- | --- | --- | --- | --- | --- |
| Is Nsfw  <sub><sup>(pornography + sexy + hentai >= 0.5)</sup></sub> | 96.4% | 96.9% | 95.7% | 2.3% | 2.8%
| Correctly Predicted Label | 87.2% | 83.3% | 87.7% | 97.6% | 89.6%

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

### Classify an Image from a Byte Array
```csharp
var fileBytes = File.ReadAllBytes(filePath);
var nsfwSpy = new NsfwSpy();
var result = nsfwSpy.ClassifyImage(fileBytes);
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
# GPU Support
GPU usage is currently only supported on Windows and Linux. To get this working, please follow the prerequisite steps [here](https://docs.microsoft.com/en-us/dotnet/api/microsoft.ml.vision.imageclassificationtrainer?view=ml-dotnet&fbclid=IwAR3Ng6Pe1BWDZ3hR20tchutSozmdMojxvpy3pqdwA3fZ_OEstU8C-ptSRZw#gpu-support) to install CUDA v10.1 and CUDNN v7.6.4. Later versions do not work (as I tried with CUDA v11.4). The SciSharp.TensorFlow.Redist-Windows-GPU and SciSharp.TensorFlow.Redist-Linux-GPU packages are already included as part of the NsfwSpy package.

# Notes
Using NsfwSpy? Let us know! We're keen to hear how the technology is being used and improving the safety of applications.

Got an issue or found something not quite right? Report it [here](https://github.com/d00ML0rDz/NsfwSpy/issues) on GitHub and we'll try to help as best as possible.
