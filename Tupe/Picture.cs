using Microsoft.ML.OnnxRuntime;
using NearestIMG.OnnxModel;
using NearestIMG.Single;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;
using static System.Net.Mime.MediaTypeNames;

namespace NearestIMG.Tupe
{
    public class Picture
    {
        [JsonIgnore]
        public Bitmap Img { get; set; }
        public float[] Vector { get; set; }

        [JsonIgnore]
        public Bitmap ImgSize
        {
            get => new Bitmap(Img, new Size(224, 224));
        }

        public Picture(Bitmap @img)
        {
            Img = img;

            float[] output = Vectorization.ProcessImage(@img, StaticClass.session);
            Vector = output;
        }
    }
}
