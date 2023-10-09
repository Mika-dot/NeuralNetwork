CombineFiles(
    "C:\\Users\\akimp\\Downloads\\0.csv",
    "C:\\Users\\akimp\\Downloads\\1.csv",
    "C:\\Users\\akimp\\Downloads\\2.csv",
    "C:\\Users\\akimp\\Downloads\\3.csv",
    "C:\\Users\\akimp\\Downloads\\outputNamber.csv");
static void CombineFiles(params string[] filePaths)
{
    string outputFilePath = filePaths[filePaths.Length - 1];
    using (StreamWriter writer = new StreamWriter(outputFilePath))
    {
        foreach (string filePath in filePaths)
        {
            using (StreamReader reader = new StreamReader(filePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    writer.WriteLine(line);
                }
            }
        }
    }
    Console.WriteLine("Files combined successfully. Output file: " + outputFilePath);
}