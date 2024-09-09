# Intelligence

RU
---

Задаем путь до нейросети.
```Sharp
StaticClass.ModelONNX = @"Img2Vec.onnx";
```

Задаем значения класса с векторизацией
```Sharp
List<Picture> original = new List<Picture>();
localPictures.Add(new Picture(new Bitmap($@"C:\Users\...\coil-100\obj{i}__{j}.png")));
```

Можно сохранить в json но без картинки так как она не сериализуеться
```Sharp
PictureSerializer.SaveToFile(original, @"C:\Users\...\file.json");
```

Можно дисириализовать 
```Sharp
List<Picture> original = PictureSerializer.LoadFromFile(@"C:\Users\...\file.json");
```

Можно ближайщий получить вектор тип
```Sharp
Picture fresh = new Picture(new Bitmap($@"C:\Users\...\obj{1}__{5}.png"));
var result = PictureComparer.FindClosestMatchParallel(original, fresh);
```

Нахождение ближайщих n векторов, тут 20
```Sharp
(List<(Picture picture, float similarity)> closestPictures, float averageSimilarity) result_n = PictureComparer.FindClosestMatches(original, fresh, 20);
```

Нахождение ближайщих значений класса
```Sharp
List<List<Picture>> original = new List<List<Picture>>();
(List<Picture> closestClass, float averageSimilarity) klass = PictureComparer.ClassifyPicture(original, fresh);
```

EN
---

Set the path to the neural network.
```Sharp
StaticClass.ModelONNX = @"Img2Vec.onnx";
```

Set the class values ​​with vectorization
```Sharp
List<Picture> original = new List<Picture>();
localPictures.Add(new Picture(new Bitmap($@"C:\Users\...\coil-100\obj{i}__{j}.png")));
```

You can save it in json but without the picture since it is not serialized
```Sharp
PictureSerializer.SaveToFile(original, @"C:\Users\...\file.json");
```

Can be serialized
```Sharp
List<Picture> original = PictureSerializer.LoadFromFile(@"C:\Users\...\file.json");
```

Can be obtained closest vector type
```Sharp
Picture fresh = new Picture(new Bitmap($@"C:\Users\...\obj{1}__{5}.png"));
var result = PictureComparer.FindClosestMatchParallel(original, fresh);
```

Finding closest n vectors, here 20
```Sharp
(List<(Picture picture, float similarity)> closestPictures, float averageSimilarity) result_n = PictureComparer.FindClosestMatches(original, fresh, 20);
```
Finding the closest values ​​of the class 
```Sharp List<List<Picture>> original = new List<List<Picture>>();
(List<Picture> closestClass, float averageSimilarity) class = PictureComparer.ClassifyPicture(original, fresh);
```
