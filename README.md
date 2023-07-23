# The use is very simple

If you need to, then rebuild the project and add the necessary dependencies to using

Usage is simple and consists of one method. Just instantiate the class and use the method to vectorize the text into a 150 dimensional vector:


```c#
            var wordEmbedding = new WordEmbedding();
            var text = "This is a sample text.";

            var vector = wordEmbedding.Vector(text);

            Console.WriteLine("Word Embedding Vector:");
            foreach (var value in vector)
            {
                Console.WriteLine(value);
            }
```
