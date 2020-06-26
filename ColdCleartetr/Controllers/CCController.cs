using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using Cold_Clear_SF;
using ColdCleartetr.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace ColdCleartetr.Controllers
{

    public class resmove
    {
        public bool hold { get; set; }
        public string[] moves { get; set; }
    }
    public class postgg
    {
        public string[] next { get; set; }
    }

    public static class bot
    {
        public static ColdClear CC = new ColdClear();
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
            bot.CC.nextPieces(ids);
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
        public resmove Post()
        {
            resmove res = new resmove();
            //res.hold = true;
            //Console.WriteLine("-------" + 0 + "----");
            //Console.WriteLine("-------" + res.hold + "----");
            //res.moves = new string[] { "Left", "Left" };
            //Console.WriteLine(res.moves[0]);
            //return res;
            CCMove temp = bot.CC.nextMove();
            if (temp.nodes == 0 )return null ;
            res.hold = (temp.hold != 0);
            int cnt = 0;
            for (int i = 0; i < temp.movement_count; ++i)
            {
                unsafe { 
                if (temp.movements[i] == 4) cnt++;
                }
            }
            res.moves = new string[temp.movement_count + 0 * cnt];
            Console.WriteLine("temp.movement_count" + (int)temp.movement_count);
            string[] tepl = { "Left", "Right", "Cw", "Ccw","SonicDrop" };
            for (int i = 0,  j = 0 ; i < temp.movement_count; ++i,++ j)
            {
                unsafe {
                    
                    string fin = tepl[temp.movements[i]];
                    if(temp.movements[i] == 4)
                    {
                        for (int _ = 0; _ < 0; ++_) res.moves[j++] = fin;
                    }

                    res.moves[j] = fin;

                    Console.WriteLine(fin);
                    
                }
            }
            return res;
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
        public async void GetResponseVal()
        {
            bot.CC.newGame();
            //return new string[] { "46", "5446" };

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
        public void Post()
        {

            bot.CC.endGame();
            bot.CC = new ColdClear();
            //return poststring.ToString();
        }
    }


    [Route("[controller]")]
    [ApiController]
    public class resetBoardController : ControllerBase
    {
        [EnableCors]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public void Post([FromBody] string[,] ids)
        {
            char[] ff = new char[400];
            int idx = 0;
            for (int i = 39; i >= 0; --i)
            {
                for (int j = 0; j < 10; ++j)
                {
                    if (ids[i,j] != null)
                        ff[(39 - i) * 10 + j] = (char)1;
                }
            }
            bot.CC.resetBoard(ff);
        }
    }
}


