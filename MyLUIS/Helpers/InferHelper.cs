using Microsoft.ML.OnnxRuntime;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NumSharp;
using System.IO;
using MyLUIS.Models;
using JiebaNet.Segmenter;


namespace MyLUIS
{
    public static class InferHelper
    {

        public static SessionOptions options = new SessionOptions();
        public static InferenceSession luis_session;
        public static string inputName = "input";
        public static Dictionary<int, string> index2intent;
        

        public static Dictionary<int, string> Prepare_mapping(string path)
        {
            var rows = path.Split("\n\r".ToCharArray());
            index2intent = new Dictionary<int, string>();
            for (int i = 0; i < rows.Count(); i++)
            {
                var cols = rows[i].Split("\t".ToCharArray());
                if (cols.Length == 2)
                {
                    index2intent.Add(int.Parse(cols[0].Trim()), cols[1].Trim());
                }
            }
            return index2intent;
        }


        public static void SettingSession(string model_path)
        {

            //options.GraphOptimizationLevel= GraphOptimizationLevel.ORT_ENABLE_BASIC;

            //try
            //{
            //    //測試是否能載入gpu
            //    session = new InferenceSession(model_path, SessionOptions.MakeSessionOptionWithCudaProvider(0));
            //}
            //catch (Exception e)
            //{
            //如果不行則改成cpu
 
                luis_session = new InferenceSession(model_path);
                inputName = luis_session.InputMetadata.Keys.ToList()[0];
            
        

        }

        /// <summary>
        /// 傳入一個句子，將句子編碼為張量後透過onnx推估其意圖，並且產出機率前3高的結果
        /// </summary>
        /// <param name="sentence"></param>
        /// <returns></returns>
        public static List<PredictIntent> Sentence2Intent(string sentence)
        {
            //將句子轉成onnx張量容器(最大長度128個字，每個字對應成長度為256的特徵向量)
            var container = TensorHelper.StringToEmbeddedTensor(sentence);
       
            //產生推論結果
            var  results =np.array(luis_session.Run(container).ToList()[0].AsEnumerable<float>().ToArray());
            //將推論結果由大至小排序(argsort是由小至大，然後"::-1"是反轉)取前三名
            var top_results = np.argsort<float>(results);
            //根據索引串回intent(透過index2intent)以及對應機率
            List<PredictIntent> final_result = new List<PredictIntent>();
     
            for (int i = -1; i >-4; i--)
            {
                int idx = top_results.GetData(new int[] { i });
                float probs = results.GetData(new int[] {idx});
                final_result.Add(new PredictIntent() { Intent = (intent)idx, Ordinal =- i, Probability = probs });
            }
            return final_result;

        }


    }
}
