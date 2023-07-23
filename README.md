# Initialization

First of all, you need to download the weights located in the folder and put them in the root folder of the library and / or code. They are not compiled into libraries. Click here to download [download](https://drive.google.com/drive/folders/1f8NNwBIC6MR3ZP_S34tO6CqYRCko4m_m?usp=sharing).

## Usage

Initialize an instance of the class first

```c#
        Gpt gpt = new Gpt();
        Model? model = await Gpt.LoadModel("Weights", 50257, 1024, 768, 12, 12);

```

After just start using

```c#
          string? input = Console.ReadLine();
          foreach (string token in gpt.Infere(input ?? "", model))
        {
            Console.Write(token);
        }
```
> All of it works. But very slowly.
