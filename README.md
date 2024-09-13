```Sharp
string pageImage = @"C:\Users\Akim\Downloads\1.png";
var location = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
var tessData = Path.Combine(location, @"tessdata");
using (var engine = new TesseractEngine(tessData, "eng", EngineMode.LstmOnly))
{
    // Perform OCR
    using (Pix img = Pix.LoadFromFile(pageImage))
    {
        using (Page recognizedPage = engine.Process(img))
        {
            Console.WriteLine($"Mean confidence for page #: {recognizedPage.GetMeanConfidence()}");

            string recognizedText = recognizedPage.GetText();
            Console.WriteLine(recognizedText);
        }
    }
}
```
