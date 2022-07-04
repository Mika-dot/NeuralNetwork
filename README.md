# CNN
Пакет NuGet для оболочки классификации изображений с использованием ML.NET.

#  использование #
###  использование ###

1. Подготовьте свернутые данные, как показано ниже. Достаточно 100 изображений каждого.

    > ![Формирование папок](https://github.com/teonsen/ImageClassification/raw/master/dataset_foldering.png)

 2. Это все!


## Обучите и сгенерируйте модель ##

```c#
using ImageClassification;
using ImageClassification.IO;

// Define data-set folder.
string dataDir = @"C:\Data\Fruits";
            
// Define hyper-paramters such as Epoch or BatchSize.
var hp = new HyperParameter {
    Epoch = 200,
    BatchSize = 10,
    LearningRate = 0.01f,
    eTrainerArchitecture = eTrainerArchitectures.ResnetV250,
    TestFraction = 0.3f,
    ResultsToShow = 10
};

// Train and generate the model.
var results = Trainer.GenerateModel(dataDir, hp);
// Save the results as HTML file.
results.SaveAsHTML();
```

После запуска приведенного выше кода в папке набора данных будут созданы pipeline.zip и model.zip.

## Классифицировать ##

Чтобы предсказать изображение, передайте конвейер и выходной файл model.zip с помощью Trainer.GenerateModel () выше, а также файл изображения, как показано ниже.

```c#
// Classify the single image.
string imageToClassify = @"C:\your\imageToClassify(apple_or_banana_or_orange).png";
var p = Classifier.GetSingleImagePrediction(results.Resultfiles.PipelineZip, results.Resultfiles.ModelZip, imageToClassify);
Console.WriteLine($@"Predicted image label is: ""{p.PredictedLabel}"". Score:{p.HighScore}");
```




