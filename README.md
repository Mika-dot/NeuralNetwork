# RandomForest
Random forest is a commonly-used machine learning algorithm trademarked by Leo Breiman and Adele Cutler

Example data for classification
 ```CSharp
            List<double[]> features = new List<double[]>()
            {
                new double[] { 1, 2, 3 },
                new double[] { 4, 5, 6 },
                new double[] { 7, 8, 9 },
                new double[] { 10, 11, 12 },
                new double[] { 13, 14, 15 },
                new double[] { 16, 17, 18 }
            };
```

Example of qualification output data
```CSharp
List<int> labels = new List<int>() { 0, 0, 1, 1, 2, 2 };
```  
   
---

Model training
```CSharp
            RandomForest randomForest = new RandomForest();
            randomForest.Train(features, labels);
```

---

Using the model
```CSharp
            double[] testFeature = new double[] { 7, 8, 9 };
            int predictedLabel = randomForest.Predict(testFeature);
```
                 
         
#### Additionally           
            
I didn’t save because I use it extremely rarely (this method)
            

