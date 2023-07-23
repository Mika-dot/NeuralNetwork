namespace WordEmbedding
{
    using System.Collections.Generic;
    using Microsoft.ML;
    using Microsoft.ML.Transforms.Text;
    public class WordEmbedding
    {
        PredictionEngine<TextInput, TextFeatures> predictionEngine;
        public WordEmbedding()
        {
            var context = new MLContext();
            var emptyData = context.Data.LoadFromEnumerable(new List<TextInput>());

            var pipeline = context.Transforms.Text.NormalizeText("Text", null, keepDiacritics: false, keepPunctuations: false, keepNumbers: false)
                .Append(context.Transforms.Text.TokenizeIntoWords("Tokens", "Text"))
                .Append(context.Transforms.Text.ApplyWordEmbedding("Features", "Tokens", WordEmbeddingEstimator.PretrainedModelKind.SentimentSpecificWordEmbedding));

            var transformer = pipeline.Fit(emptyData);
            predictionEngine = context.Model.CreatePredictionEngine<TextInput, TextFeatures>(transformer);
        }

        public float[] Vector(string Text)
        {
            var wordOfEmbedding = new TextInput { Text = Text };
            var prediction = predictionEngine.Predict(wordOfEmbedding);

            return prediction.Features;
        }

        private class TextInput
        {
            public string Text { get; set; }
        }

        private class TextFeatures
        {
            public float[] Features { get; set; }
        }
    }
}