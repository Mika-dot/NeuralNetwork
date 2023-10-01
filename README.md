# Intelligence

## Data

Create an application that will use this model.
Next, you need to initialize the data in the program code.

You must insert data into this data:

```c#
List<List<string>> input = new List<List<string>>();
List<List<string>> output = new List<List<string>>();
```

Initializing the model in code:

```c#
         SynonymCoderDecrypt model = new SynonymCoderDecrypt(64, 32, 1, input, output, true);
```

Learning process in code:

```c#
            Thread mainThread = new Thread(new ThreadStart(Teach));
            mainThread.Start();
            void Teach()
            {
                ss.Teach(300);
                ss.Save();
            }
```
>Here, most likely, I messed up somewhere and could have been written more optimally.

Next, you need to wait some time, you know, so that the model will turn around, specify the time in minutes, and you know that you want the model to be saved:

```c#
            await Task.Delay(TimeSpan.FromMinutes(0.5));
            
            model.Save();
```

Using the model for work:

```c#
            string TxtBox = "...,...";
            var Temp = model.Predict(TxtBox.ToLower().Trim().Split(',').ToList());
```
