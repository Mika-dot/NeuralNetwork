using Microsoft.ML.OnnxRuntime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NearestIMG.Single
{
    public static class StaticClass
    {
        public static string ModelONNX = @"C:\Users\Akim\Desktop\NearestIMG\NearestIMG\model.onnx";
        public static InferenceSession session = new InferenceSession(StaticClass.ModelONNX);
    }
}
