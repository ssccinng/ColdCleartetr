using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
// using Newtonsoft.Json;
// using Newtonsoft.Json.Linq;
namespace zzztetr.Controllers
{
    public class resmove
    {
        public bool hold { get; set; }
        public string[] moves { get; set; }
        public int[][] expected_cells { get; set; }

    }
    public class postgg
    {
        public string[] next { get; set; }
    }

    public static class bot
    {
        public static ZZZTOJ zzz_toj = new ZZZTOJ();
    }

    [Produces(MediaTypeNames.Application.Json)]
    [Route("[controller]")]
    [ApiController]


    public class newPiecesController : ControllerBase
    {


        [EnableCors]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async void Create([FromBody] string[] ids)
        //public void Create( string ids)
        {

            //bot.CC.nextPieces(new string[] { "I","O","S","Z","J","L", "T"});
            bot.zzz_toj.nextPieces(ids);
            //return ids[0];
        }
        //public ActionResult<Pet> Create(Pet pet)
        //{
        //    pet.Id = _petsInMemoryStore.Any() ?
        //             _petsInMemoryStore.Max(p => p.Id) + 1 : 1;
        //    _petsInMemoryStore.Add(pet);

        //    return CreatedAtAction(nameof(GetById), new { id = pet.Id }, pet);
        //}
        [HttpGet]
        public resmove Get()
        {
            var rng = new Random();
            return new resmove();
            //return Enumerable.Range(1, 5).Select(index => new resmove
            //{

            //})
            //.ToArray();
        }
    }
    [Produces(MediaTypeNames.Application.Json)]
    [Route("[controller]")]
    [ApiController]
    public class nextMoveController : ControllerBase
    {
        [HttpGet]
        public resmove Get()
        {
            var rng = new Random();
            return new resmove();
            //return Enumerable.Range(1, 5).Select(index => new resmove
            //{

            //})
            //.ToArray();
        }
        [EnableCors]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<resmove> Post([FromBody] int ids)
        {
            resmove temp;
            //res.hold = true;
            // Console.WriteLine("upcot" + ids + "----");
            //Console.WriteLine("-------" + res.hold + "----");
            //res.moves = new string[] { "Left", "Left" };
            //Console.WriteLine(res.moves[0]);
            //return res;
            try { 
                temp = await bot.zzz_toj.nextMove(ids);
            }
            catch (Exception e)
            {
                // Console.WriteLine("I think gg");
                Console.WriteLine(e);
                return null;
            }
            
            if (temp.moves.Length == 1 && temp.moves[0] == "oraora") return null;
            bot.zzz_toj.calumove = false;
            return temp;
            //if (temp.nodes == 0) return null;
            //res.hold = (temp.hold != 0);
            //int cnt = 0;
            //for (int i = 0; i < temp.movement_count; ++i)
            //{
            //    unsafe
            //    {
            //        if (temp.movements[i] == 4) cnt++;
            //    }
            //}
            //res.moves = new string[temp.movement_count + 0 * cnt];
            //Console.WriteLine("temp.movement_count" + (int)temp.movement_count);
            //string[] tepl = { "Left", "Right", "Cw", "Ccw", "SonicDrop" };
            //for (int i = 0, j = 0; i < temp.movement_count; ++i, ++j)
            //{
            //    unsafe
            //    {

            //        string fin = tepl[temp.movements[i]];
            //        if (temp.movements[i] == 4)
            //        {
            //            for (int _ = 0; _ < 0; ++_) res.moves[j++] = fin;
            //        }

            //        res.moves[j] = fin;

            //        Console.WriteLine(fin);

            //    }
            //}
            //return res;
        }
    }
    [Produces(MediaTypeNames.Application.Json)]
    [Route("[controller]")]
    [ApiController]
    public class newGameController : ControllerBase
    {

        [HttpGet]
        public string Get()
        {
            return "4489";
        }
        [EnableCors]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async void GetResponseVal([FromBody] string[] ids)
        {
            await Task.Run(() =>
            {
// Console.WriteLine(string.Join(",", ids));
                //bot.zzz_toj.newGame();
                bot.zzz_toj.nextPieces(ids);
            });
            //return new string[] { "46", "5446" };
            Console.WriteLine("新的一局开始了！");
        }
    }
    [Produces(MediaTypeNames.Application.Json)]
    [Route("[controller]")]
    [ApiController]
    public class endGameController : ControllerBase
    {
        [EnableCors]
        [HttpPost]

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async void Post()
        {

            await Task.Run(() =>
            {
                bot.zzz_toj.endGame();
                Thread.Sleep(500);
                bot.zzz_toj.newGame();
            });
            Console.WriteLine("一局完了！");
            //return poststring.ToString();
        }
    }
    [Produces(MediaTypeNames.Application.Json)]
    [Route("[controller]")]
    [ApiController]
    public class resetBoardController : ControllerBase
    {
        [EnableCors]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async void Post([FromBody] JsonDocument gg)
        {
            // Console.WriteLine(gg.GetRawText());
            // Console.WriteLine(gg.GetProperty("board"));
            if (gg == null) return;
            // Console.WriteLine(gg);
            var g = gg;
            // var g = JsonDocument.Parse(gg.ToString());
            //string [][] ids = JsonSerializer.Deserialize<string[][]>(gg.GetProperty("board").GetRawText());
            //g.RootElement.GetProperty("board").
            string[][] ids = JsonSerializer.Deserialize<string[][]>(g.RootElement.GetProperty("board").ToString());
            System.Text.Json.JsonElement a = new System.Text.Json.JsonElement();
            int cnt = g.RootElement.GetProperty("garbage").GetInt32();
            // Console.WriteLine("-----------------------------------------------------------------");
            // Console.WriteLine("garbage = " + cnt);
            await Task.Run(() =>
            {
                //char[] ff = new char[400];
                int[,] ff = new int[40, 10];

                int idx = 0;
                for (int i = 39; i >= 0; --i)
                {
                    for (int j = 0; j < 10; ++j)
                    {
                        if (ids[i][j] != null)
                            ff[39 - i, j] = 1;
                            //ff[(39 - i) * 10 + j] = (char)1;
                    }
                }

                bot.zzz_toj.resetBoard(ff);
                bot.zzz_toj.updategar(cnt);

            });
        }
    }

    public class pendingGarbageController : ControllerBase
    {


        [EnableCors]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        ///public async void Create([FromBody] JArray ids)
        public async void Create(int ids)
        {

            int cnt = 0;
            //bot.CC.nextPieces(new string[] { "I","O","S","Z","J","L", "T"});
            //foreach (JObject a in ids)
            //{
            //    if (a["sender"].ToString() == bot.CC.botname)
            //    {
            //        cnt += int.Parse(a["len"].ToString());
            //    }
            //}

            bot.zzz_toj.updategar(cnt);
            //return ids[0];
        }
        //public ActionResult<Pet> Create(Pet pet)
        //{
        //    pet.Id = _petsInMemoryStore.Any() ?
        //             _petsInMemoryStore.Max(p => p.Id) + 1 : 1;
        //    _petsInMemoryStore.Add(pet);

        //    return CreatedAtAction(nameof(GetById), new { id = pet.Id }, pet);
        //}
        [HttpGet]
        public resmove Get()
        {
            var rng = new Random();
            return new resmove();
            //return Enumerable.Range(1, 5).Select(index => new resmove
            //{

            //})
            //.ToArray();
        }
    }

    public class resetjson {
        public string[][] ids {get; set;}
        public int cnt {get; set;}
    }
}
