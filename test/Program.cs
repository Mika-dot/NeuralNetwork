using PdfSharp.Pdf;
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;

using Tesseract;

//@"C:\Users\Akim\Desktop\Edge-Detector-Canny-Hough-master\Hough\bin\Debug\text1.jpg";
//@"C:\Users\Akim\Downloads\photo16910449411.jpg";

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

Console.ReadKey();