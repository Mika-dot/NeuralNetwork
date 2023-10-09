
using LightGbmRegression_Console;

L_BFGS.ModelInput sampleData = new L_BFGS.ModelInput()
{
    Col0 = -47F,
    Col1 = -6F,
    Col2 = -5F,
    Col3 = -7F,
    Col4 = 13F,
    Col5 = -1F,
    Col6 = 35F,
    Col7 = -10F,
    Col8 = 10F,
    Col9 = -4F,
    Col10 = 0F,
    Col11 = 7F,
    Col12 = -31F,
    Col13 = -33F,
    Col14 = -48F,
    Col15 = -52F,
    Col16 = 34F,
    Col17 = 10F,
    Col18 = 5F,
    Col19 = -9F,
    Col20 = 23F,
    Col21 = 63F,
    Col22 = 76F,
    Col23 = 49F,
    Col24 = -27F,
    Col25 = -7F,
    Col26 = -7F,
    Col27 = -3F,
    Col28 = 0F,
    Col29 = 4F,
    Col30 = -111F,
    Col31 = -2F,
    Col32 = -7F,
    Col33 = 7F,
    Col34 = 11F,
    Col35 = 11F,
    Col36 = 2F,
    Col37 = 2F,
    Col38 = 76F,
    Col39 = 3F,
    Col40 = 7F,
    Col41 = -2F,
    Col42 = 0F,
    Col43 = -6F,
    Col44 = 21F,
    Col45 = -3F,
    Col46 = 0F,
    Col47 = -8F,
    Col48 = -40F,
    Col49 = -4F,
    Col50 = -1F,
    Col51 = 6F,
    Col52 = -2F,
    Col53 = -2F,
    Col54 = -105F,
    Col55 = -25F,
    Col56 = 47F,
    Col57 = 6F,
    Col58 = 6F,
    Col59 = 5F,
    Col60 = 13F,
    Col61 = 21F,
    Col62 = 111F,
    Col63 = 15F,
};

var predictionResult = L_BFGS.Predict(sampleData);

Console.WriteLine("Using model to make single prediction -- Comparing actual Col64 with predicted Col64 from sample data...\n\n");


Console.WriteLine($"Col0: {-47F}");
Console.WriteLine($"Col1: {-6F}");
Console.WriteLine($"Col2: {-5F}");
Console.WriteLine($"Col3: {-7F}");
Console.WriteLine($"Col4: {13F}");
Console.WriteLine($"Col5: {-1F}");
Console.WriteLine($"Col6: {35F}");
Console.WriteLine($"Col7: {-10F}");
Console.WriteLine($"Col8: {10F}");
Console.WriteLine($"Col9: {-4F}");
Console.WriteLine($"Col10: {0F}");
Console.WriteLine($"Col11: {7F}");
Console.WriteLine($"Col12: {-31F}");
Console.WriteLine($"Col13: {-33F}");
Console.WriteLine($"Col14: {-48F}");
Console.WriteLine($"Col15: {-52F}");
Console.WriteLine($"Col16: {34F}");
Console.WriteLine($"Col17: {10F}");
Console.WriteLine($"Col18: {5F}");
Console.WriteLine($"Col19: {-9F}");
Console.WriteLine($"Col20: {23F}");
Console.WriteLine($"Col21: {63F}");
Console.WriteLine($"Col22: {76F}");
Console.WriteLine($"Col23: {49F}");
Console.WriteLine($"Col24: {-27F}");
Console.WriteLine($"Col25: {-7F}");
Console.WriteLine($"Col26: {-7F}");
Console.WriteLine($"Col27: {-3F}");
Console.WriteLine($"Col28: {0F}");
Console.WriteLine($"Col29: {4F}");
Console.WriteLine($"Col30: {-111F}");
Console.WriteLine($"Col31: {-2F}");
Console.WriteLine($"Col32: {-7F}");
Console.WriteLine($"Col33: {7F}");
Console.WriteLine($"Col34: {11F}");
Console.WriteLine($"Col35: {11F}");
Console.WriteLine($"Col36: {2F}");
Console.WriteLine($"Col37: {2F}");
Console.WriteLine($"Col38: {76F}");
Console.WriteLine($"Col39: {3F}");
Console.WriteLine($"Col40: {7F}");
Console.WriteLine($"Col41: {-2F}");
Console.WriteLine($"Col42: {0F}");
Console.WriteLine($"Col43: {-6F}");
Console.WriteLine($"Col44: {21F}");
Console.WriteLine($"Col45: {-3F}");
Console.WriteLine($"Col46: {0F}");
Console.WriteLine($"Col47: {-8F}");
Console.WriteLine($"Col48: {-40F}");
Console.WriteLine($"Col49: {-4F}");
Console.WriteLine($"Col50: {-1F}");
Console.WriteLine($"Col51: {6F}");
Console.WriteLine($"Col52: {-2F}");
Console.WriteLine($"Col53: {-2F}");
Console.WriteLine($"Col54: {-105F}");
Console.WriteLine($"Col55: {-25F}");
Console.WriteLine($"Col56: {47F}");
Console.WriteLine($"Col57: {6F}");
Console.WriteLine($"Col58: {6F}");
Console.WriteLine($"Col59: {5F}");
Console.WriteLine($"Col60: {13F}");
Console.WriteLine($"Col61: {21F}");
Console.WriteLine($"Col62: {111F}");
Console.WriteLine($"Col63: {15F}");
Console.WriteLine($"Col64: {0F}");


Console.WriteLine($"\n\nPredicted Col64: {predictionResult.Score}\n\n");
Console.WriteLine("=============== End of process, hit any key to finish ===============");
Console.ReadKey();

