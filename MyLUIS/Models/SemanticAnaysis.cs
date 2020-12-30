using JiebaNet.Segmenter;
using JiebaNet.Segmenter.PosSeg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyLUIS.Models
{
    //語意分析資料模型
    public class SemanticAnaysis
    {
        private string input_sentence;
        public SemanticAnaysis()
        { 
        }
        public SemanticAnaysis(string sentence)
        {
            this.input_sentence = sentence;
            PredictIntents = new List<PredictIntent>();
            PredictEntities = new List<PredictEntity>();
        }

        //輸入字串
        public string InputSentence
        {
            get
            {
                return this.input_sentence;
            }
            set
            {
                this.input_sentence = value;
            }
        }
        //處理後字串
        public string PreprocessedString { get; set; }
        //回傳字串
        public string ReturnString { get; set; }
        //偵測到的可能意圖
        public List<PredictIntent> PredictIntents { get; set; }
        //偵測到的可能實體
        public List<PredictEntity> PredictEntities { get; set; }
        //中文分詞解析結果
        public List<Pair> WordSegs { get; set; }

        public double ProcessTime { get; set; }



    }




    //偵測實體結果
    public class PredictEntity
    {
        public PredictEntity()
        {
        }

        public entity_type EntityType { get; set; }
        public string  EntityString { get; set; }
        public float Probability { get; set; }
        

    }

    //偵測意圖結果
    public class  PredictIntent
    {
        public PredictIntent()
        { 
        }

        public intent Intent { get; set; }
        public string IntentDesc 
        { get
            {
                return Intent.ToString();
            }
        }
        public float Probability { get; set; }
        public int Ordinal { get; set; }

    }


    //意圖枚舉
    public enum intent:int
    {
        打開app應用,
        詢問公車路線,
        數學計算,
        閒聊,
        詢問電影場次,
        查詢聯絡人,
        查詢食譜,
        詢問日期,
        電子郵件指令,
        詢問電視節目,
        詢問航班,
        詢問健康資訊,
        詢問樂透,
        詢問地圖,
        詢問足球賽事,
        簡訊指令,
        音樂指令,
        詢問新聞,
        詢問小說,
        詢問詩詞,
        詢問廣播節目,
        詢問謎語,
        鬧鐘時程設定,
        詢問股票,
        電話指令,
        詢問火車班次,
        詢問翻譯,
        切換電視頻道,
        影片播放指令,
        詢問天氣,
        開啟網站,
    }

    //實體枚舉
    public enum entity_type : int
    {
        人名,
        地點,
        時間
    }



}
