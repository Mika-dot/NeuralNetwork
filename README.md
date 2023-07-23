# Installation

First, you need to connect Selenium to work. Sorry, but I did this a long time ago and I don’t remember how it is done.

## usage

Just initialize the class instance with the network settings.

```c#
            Gpt gpt = new Gpt("4", "1", false);
```
> it uses external dependencies so something can break

Then start using the neural network with prompts.

```c#
            ( Bitmap g , string f ) = gpt.Request("чем я могу помочь");
```

How did you finish the visualization of the use.

```c#
           gpt.GptQuit();
```
