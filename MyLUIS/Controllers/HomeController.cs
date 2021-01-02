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
            sentence1 = ChineseHelper.ToHalfWidth(sentence1);
            sentence1 = ChineseHelper.ToTraditionalChinese(sentence1);

            //將繁體中文轉成簡體中文後分詞(配合結巴，但是效果很爛未直接使用，僅教學示範)
            sa.WordSegs=seg.Cut(ChineseHelper.ToSimplifiedChinese(sentence1)).ToList();
            int pos = 0;
            for (int i = 0; i < sa.WordSegs.Count(); i++)
            {
                var seg = sa.WordSegs[i];
                seg.Word = sentence1.Substring(pos, seg.Word.Length);
                pos += seg.Word.Length;
            }

            try
            {

                sa.PredictIntents = InferHelper.Sentence2Intent(sentence1);
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
