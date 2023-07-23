# Usage

Translation of text from any language into English.
It uses google translator for this, but not using api.
In constants, you can replace with other languages.

Usage
```c#
          var Translate = new AccessURL();
            string text = await Translate.GetTextAsync("привет");
```
