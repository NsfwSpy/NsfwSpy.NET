<img src="https://raw.githubusercontent.com/d00ML0rDz/NsfwSpy/main/_art/NsfwSpy-Logo.jpg" alt="NsfwSpy Logo" width="400"/>

# Introduction
NsfwSpy is a nudity/pornography image and video classifier built for .NET Core 2.0 and later, with support for Windows, [macOS](#macos-support) and Linux, to aid in moderating user-generated content for various different application types, written in C#. The [ML.NET](https://github.com/dotnet/machinelearning) model has been trained against the ResNet V250 neural net architecture with 500,000 images (158GB), from 4 different categories:

| Label       | Description |
| ----------- | ----------- |
| Pornography | Images that depict sexual acts and nudity. |
| Sexy        | Images of people in their underwear and men who are topless. |
| Hentai      | Drawings or animations of sexual acts and nudity. |
| Neutral     | Images that are not sexual in nature. |

<img src="https://raw.githubusercontent.com/d00ML0rDz/NsfwSpy/main/_art/Examples.gif" />

# Performance
NsfwSpy isn't perfect, but the accuracy should be good enough to detect approximately 96% of Nsfw images, those being images that are classed as pornography, sexy or hentai.

|     | Pornography | Sexy | Hentai | Neutral |
| --- | --- | --- | --- | --- |
| Is Nsfw  <sub><sup>(pornography + sexy + hentai >= 0.5)</sup></sub> | 96.3% | 97.2% | 96.9% | 3.8% | 
| Correctly Predicted Label | 85.0% | 83.5% | 93.6% | 96.6% |

# Quick Start
This project is available as a [NuGet](https://www.nuget.org/packages/NsfwSpy/) package and can be installed with the following commands:

**Package Manager**
```
Install-Package NsfwSpy
```

**.NET CLI**
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
var results = nsfwSpy.ClassifyImages(files, (filePath, result) =>
{
    Console.WriteLine($"{filePath} - {result.PredictedLabel}");
});
```

### Classify a Gif File
```csharp
var nsfwSpy = new NsfwSpy();
var result = nsfwSpy.ClassifyGif(@"C:\Users\username\Documents\happy.gif");
```

### Classify a Web Gif
```csharp
var uri = new Uri("https://raw.githubusercontent.com/d00ML0rDz/NsfwSpy/main/NsfwSpy.Test/Assets/cool.gif");
var nsfwSpy = new NsfwSpy();
var result = nsfwSpy.ClassifyGif(uri);
```

### Classify a Video File
```csharp
var nsfwSpy = new NsfwSpy();
var result = nsfwSpy.ClassifyVideo(@"C:\Users\username\Documents\happy.mp4");
```

### Classify a Web Video
```csharp
var uri = new Uri("https://raw.githubusercontent.com/d00ML0rDz/NsfwSpy/main/NsfwSpy.Test/Assets/bikini.mp4");
var nsfwSpy = new NsfwSpy();
var result = nsfwSpy.ClassifyVideo(uri);
```

### Dependency Injection
```csharp
services.AddScoped<INsfwSpy, NsfwSpy>();
```

# Classify Video Support
To be able to make use of the ClassifyVideo methods, [FFmpeg](https://www.ffmpeg.org/) needs to be installed and available in the command line via the 'ffmpeg' command.

## Windows 
Follow [this guide](https://www.geeksforgeeks.org/how-to-install-ffmpeg-on-windows/) to download FFmpeg, extract it to your C:\ drive and add the required environment path variable.

## macOS
Install FFmpeg on macOS using [Homebrew](https://brew.sh/) via the following command:
```
brew install ffmpeg
```

## Ubuntu
Install FFmpeg on Ubuntu using the following command:
```
sudo apt install ffmpeg
```

# GPU Support
To get GPU support working, please follow the prerequisite steps [here](https://docs.microsoft.com/en-us/dotnet/api/microsoft.ml.vision.imageclassificationtrainer?view=ml-dotnet&fbclid=IwAR3Ng6Pe1BWDZ3hR20tchutSozmdMojxvpy3pqdwA3fZ_OEstU8C-ptSRZw#gpu-support) to install [CUDA v10.1](https://developer.nvidia.com/cuda-10.1-download-archive-update2) and [CUDNN v7.6.4](https://developer.nvidia.com/rdp/cudnn-download). Later versions do not work (as I tried with CUDA v11.4). The SciSharp.TensorFlow.Redist-Windows-GPU and SciSharp.TensorFlow.Redist-Linux-GPU packages are already included as part of the NsfwSpy package.

# macOS Support
To get NsfwSpy working on macOS, the [SciSharp.TensorFlow.Redist v2.3.1](https://www.nuget.org/packages/SciSharp.TensorFlow.Redist/2.3.1) NuGet package also needs to be installed. This not included by default as it interfers with supporting GPUs on Windows and Linux. You can do this with either of the following commands:

**Package Manager**
```
Install-Package SciSharp.TensorFlow.Redist -Version 2.3.1
```

**.NET CLI**
```
dotnet add package SciSharp.TensorFlow.Redist --version 2.3.1
```

# Notes
Using NsfwSpy? Let us know! We're keen to hear how the technology is being used and improving the safety of applications.

Got an issue or found something not quite right? Report it [here](https://github.com/d00ML0rDz/NsfwSpy/issues) on GitHub and we'll try to help as best as possible.
