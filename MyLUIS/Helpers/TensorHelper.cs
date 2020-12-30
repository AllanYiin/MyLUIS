using Microsoft.ML.OnnxRuntime;
using Microsoft.ML.OnnxRuntime.Tensors;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Text;
using NumSharp;

namespace MyLUIS
{
    public static class TensorHelper
    {
        public static NDArray char_embedding=null;
        public static char[] chars_list = Properties.Resources.charlist.ToCharArray();

        public static int Char2Index(char input_char)
        {
            if (chars_list.Contains<char>(input_char))
            {
                return Array.IndexOf<char>(chars_list, input_char);
            }
            else 
            {
                return -1;
            }
        }
        public static List<NamedOnnxValue> StringToEmbeddedTensor(string sentence)
        {
            if (char_embedding == null)
            {
                char_embedding = np.Load<float[,]>(Properties.Resources.embed);
            }
            var container = new List<NamedOnnxValue>();
            NDArray features = np.zeros((256,128));
            for (int i = 0; i < sentence.Length; i++)
            {
                int idx = Char2Index(sentence[i]);
                features[string.Format(":,{0}", i)] = idx != -1 ? char_embedding[string.Format("{0}", idx)] : np.random.stardard_normal(256); 
            }
            features=features.astype(np.float32);
            features = features.flatten();
            var tensor = new DenseTensor<float>(features.ToArray<float>(), new int[] { 1,256,128 });
            var onnxvalue = NamedOnnxValue.CreateFromTensor<float>("input", tensor);
            container.Add(onnxvalue);
            return container;
        }



        // var tensor = Onnx.TensorProto.Parser.ParseFrom(file);
    }
}
