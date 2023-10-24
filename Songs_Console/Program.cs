using Songs_Console;

Songs.ModelInput sampleData = new Songs.ModelInput()
{
    Artist = @"The Killers",
    Song = @"Mr. Brightside",
    Ids = @"003vvx7Niy0yvhvHt4a68B",
    Acousticness = 0.00121F,
    Danceability = 0.352F,
    Duration_ms = 222973F,
    Energy = 0.911F,
    Instrumentalness = 0F,
    Key = 1F,
    Liveness = 0.0995F,
    Loudness = -5.23F,
    Speechiness = 0.0747F,
    Tempo = 148.033F,
    Mode = 1F,
    Valence = 0.236F,
};

var predictionResult = Songs.Predict(sampleData);


Console.WriteLine($"Artist: {@"The Killers"}");
Console.WriteLine($"Song: {@"Mr. Brightside"}");
Console.WriteLine($"Ids: {@"003vvx7Niy0yvhvHt4a68B"}");
Console.WriteLine($"Genre: {@"Rock"}");
Console.WriteLine($"Acousticness: {0.00121F}");
Console.WriteLine($"Danceability: {0.352F}");
Console.WriteLine($"Duration_ms: {222973F}");
Console.WriteLine($"Energy: {0.911F}");
Console.WriteLine($"Instrumentalness: {0F}");
Console.WriteLine($"Key: {1F}");
Console.WriteLine($"Liveness: {0.0995F}");
Console.WriteLine($"Loudness: {-5.23F}");
Console.WriteLine($"Speechiness: {0.0747F}");
Console.WriteLine($"Tempo: {148.033F}");
Console.WriteLine($"Mode: {1F}");
Console.WriteLine($"Valence: {0.236F}");


Console.WriteLine($"\n\nPredicted Genre: {predictionResult.PredictedLabel}\n\n");


