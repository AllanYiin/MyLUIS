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
            PredictIntents = new List<PredictIntent>();
            PredictEntities = new List<PredictEntity>();
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
        public string EntityDesc
        {
            get
            {
                return EntityType.ToString();
            }
        }
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
        打開app應用=0,
        詢問公車路線=1,
        數學計算=2,
        閒聊=3,
        詢問電影場次=4,
        查詢聯絡人=5,
        查詢食譜=6,
        詢問日期=7,
        電子郵件指令=8,
        詢問電視節目=9,
        詢問航班=10,
        詢問健康資訊=11,
        詢問樂透=12,
        詢問地圖=13,
        詢問足球賽事=14,
        簡訊指令=15,
        音樂指令=16,
        詢問新聞=17,
        詢問小說=18,
        詢問詩詞= 19,
        詢問廣播節目=20,
        詢問謎語=21,
        鬧鐘時程設定=22,
        詢問股票=23,
        電話指令=24,
        詢問火車班次=25,
        詢問翻譯=26,
        切換電視頻道=27,
        影片播放指令=28,
        詢問天氣=29,
        開啟網站=30,
        意圖清單之外=-1
    }

    //實體枚舉
    public enum entity_type : int
    {
        人名,
        地點,
        時間
    }



}
