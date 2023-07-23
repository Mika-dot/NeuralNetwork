# Intelligence

## Use, preliminary work.

You need to download the file from google drive and upload it to the [Model](https://drive.google.com/file/d/1jpTya3u7-g7uIYkmeOsXy_6wnXLSZLoU/view?usp=sharing) folder

## Usage

Create an application that will use this model.
Next, you need to initialize the model files in the program code.

To do this, use the model reference replacement in this code snippet:

```c#
            var modelConfig = new BertModelConfiguration()
            {
                VocabularyFile = "C:\\Users\\...\\Model\\vocab.txt",
                ModelPath = "C:\\Users\\...\\Model\\bertsquad-10.onnx"
            };

            var model = new BertModel(modelConfig);
```

After initializing the model, you need to run it with this code:

```c#
            model.Initialize();
```

Now, as we have done this, we can start working with the network by giving context and asking a question:

```c#
            var (tokens, probability) = model.Predict(
                "One day, a strange event happened in a small mountain village. For several nights, the locals heard mysterious sounds from the forest that no one could explain. They were frightened and found countless sounds, and decided what was really going on. One of the villagers, young and brave Alexander, decided to gather a group of people and find the source of the mysterious sounds. Together with their friends, they set off on their journey, unaware of the secrets to be uncovered. While exploring, the group discovered a long-abandoned cave where, officially, no one has set foot in years. Boldly entering, they discovered a hidden vault full of sealed chests. Carried away by curiosity, they began to pick locks and open chests. In one of the chests they found a universal secret. This village once had a territorial affiliation between two mysterious peoples. The mysterious sounds that were heard were recreated by the same formula that arose in the forest a year ago.",
                "What did the group discover in the long-abandoned cave where they went to explore the forest?");

```
>How do you understand that the first is the context, and the second is the question. You can replace it with a string.

If you find it difficult to use, then you can take this where Probability is the accuracy of the answer, and tokens are the answers.

```c#
            Console.WriteLine(JsonSerializer.Serialize(new
            {
                Probability = probability,
                Tokens = tokens
            }));
```

When you need to complete the work, then call this from the code:

```c#
            model.Dispose();
```
