using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyLUIS.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using JiebaNet.Segmenter.PosSeg;

namespace MyLUIS.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly PosSegmenter seg;
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            seg = new PosSegmenter(new JiebaNet.Segmenter.JiebaSegmenter());
            //pre-load
            var result=this.InferIntents("今天台積電股價多少?");
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost("home/InferIntents")]
        public IActionResult InferIntents( string sentence)
        {
            string sentence1 = string.Empty;
            if (sentence != null)
            {
                sentence1 = sentence;
            }
            else
            {
                sentence1 = this.Request.Form.Keys.ToList().FirstOrDefault();
            }
            SemanticAnaysis sa = new SemanticAnaysis(sentence1);

            DateTime starttime = DateTime.Now;

            //轉半形與繁體
            string sentence2 = ChineseHelper.ToHalfWidth(sentence1);
            sentence2 = ChineseHelper.ToTraditionalChinese(sentence2);
            sa.PreprocessedString = sentence2;
            //將繁體中文轉成簡體中文後分詞(配合結巴，但是分詞效果很爛，僅用於實體識別教學示範)
            sa.WordSegs=seg.Cut(ChineseHelper.ToSimplifiedChinese(sentence2)).ToList();

            sa.WordSegs.Where(x => x.Flag == "nr" || x.Flag == "ns" || x.Flag == "t").ToList().ForEach(x =>
                   sa.PredictEntities.Add(new PredictEntity() { EntityString = x.Word, EntityType = x.Flag == "nr" ? entity_type.人名 : x.Flag == "ns" ? entity_type.地點 : entity_type.時間 })
               );

            int pos = 0;
            for (int i = 0; i < sa.WordSegs.Count(); i++)
            {
                var seg = sa.WordSegs[i];
                seg.Word = sentence2.Substring(pos, seg.Word.Length);
                pos += seg.Word.Length;
            }

            try
            {

                sa.PredictIntents = InferHelper.Sentence2Intent(sentence2);
                sa.ProcessTime =( DateTime.Now - starttime).TotalMilliseconds/1000.0;


                return new OkObjectResult(sa);
            }
            catch (Exception e)
            {
                return Json(e);

            }

        }

    }
}
