using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyLUIS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.NewtonsoftJson;
using Newtonsoft.Json.Converters;


namespace MyLUIS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LuisController : ControllerBase
    {



        //[HttpGet("{id}")]
        //public async Task<ActionResult<TodoItem>> GetTodoItem(long id)
        //{
        //    var todoItem = await _context.TodoItems.FindAsync(id);

        //    if (todoItem == null)
        //    {
        //        return NotFound();
        //    }

        //    return todoItem;
        //}


        [Route("api/luis/InferIntents")]
        [HttpPost]
        public IActionResult InferIntents(string sentence)
        {
            SemanticAnaysis sa = new SemanticAnaysis(sentence);
            try
            {

                sa.PredictIntents = InferHelper.Sentence2Intent(sentence);


                return Ok(sa);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);

            }

        }



    }
}
