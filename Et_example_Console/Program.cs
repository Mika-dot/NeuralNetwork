using Et_example_Console;

Et_example.ModelInput sampleData = new Et_example.ModelInput()
{
    Date = @"2012-01-02",
    Precipitation = 10.9F,
    Temp_max = 10.6F,
    Temp_min = 2.8F,
    Wind = 4.5F,
};

var predictionResult = Et_example.Predict(sampleData);


Console.WriteLine($"Date: {@"2012-01-02"}");
Console.WriteLine($"Precipitation: {10.9F}");
Console.WriteLine($"Temp_max: {10.6F}");
Console.WriteLine($"Temp_min: {2.8F}");
Console.WriteLine($"Wind: {4.5F}");
Console.WriteLine($"Weather: {@"rain"}");


Console.WriteLine($"\n\nPredicted Weather: {predictionResult.PredictedLabel}\n\n");


