
using Microsoft.AspNetCore.Mvc;
using SecretHitlerGeekTime.Models;
using SecretHitlerGeekTime.Models.WinLoss;
using SecretHitlerGeekTime.Models.InputModels;
using SecretHitlerGeekTime.Models.JoinTables;
using SecretHitlerGeekTime.Enums;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using SecretHitlerGeekTime.Models.ReturnModel;
using Newtonsoft.Json.Schema;
using System.Diagnostics;

namespace SecretHitlerGeekTime.Controllers
{





    [ApiController]
    internal class MainController : Controller
    {

        private readonly SecretHitlerContext _main_context;


        public MainController(SecretHitlerContext input)
        {
            _main_context = input;
        }

        [HttpPost("")]
        public async Task<ActionResult> CreatePlayer(InputModel input)
        {
            if (_main_context.players.Where(a => a.Name == input.Name).Any())
            {
                return BadRequest();
            }
            var player = new Player(input.Name);

            _main_context.players.Add(player);

            try
            {
                await _main_context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                return BadRequest();
            }




            return Ok();

        }



        [HttpPost("")]
        public async Task<ActionResult> CreateGame(GameInput input)
        {
            Game game = new Game(input.date, input.is_liberal, input.game_index);
            foreach (var player_win in input.players_win)
            {
                Player player = await _main_context.players.FindAsync(player_win.PlayerName);
                game.players.Add(new PlayerGame(game, player, player_win.role, player_win.is_won));
            }


            _main_context.games.Add(game);
            await _main_context.SaveChangesAsync();
            return Ok();
        }


        [HttpGet("get/player/{Name}")]
        public async Task<ActionResult> ReturnPlayer(string Name)
        {
            try
            {
                Player player = await _main_context.players.FindAsync(Name);
                return Ok(player);
            }
            catch (Exception ex)
            {

                return BadRequest();
            }




        }


        [HttpGet("get/last/games/{date}/{take}/")]
        [HttpGet("get/last/games/player/{player_name}/{date}/{take}")]
        public async Task<ActionResult> LastGames(string date_string, int take, string? player_name)
        {
            var date = DateTime.Parse(date_string);

            var games_result = _main_context.games.Where(a => (a.date.CompareTo(date) > 0)&&(player_name==null||a.players.Where(a=>a.PlayerName==player_name).Any())).OrderBy(a => a.date).Take(take).ToList();

            var game_return = new List<GameReturn>();
            foreach (var game in games_result)
            {
                game_return.Add(new GameReturn(game));
            }

            if (games_result.Count() > 0)
            {
                return Ok(new {page=game_return, date_coursor=games_result.Last().date.ToString()});
            }


            return Ok();

        }


        [HttpGet("get/game/{id}")]
        public async Task<ActionResult> FindGame(Guid id)
        {
            var game = await _main_context.games.FindAsync(id);


            return Ok(new GameReturn( game));
        }


        [HttpGet("get/player/find/{Name}/{similar_char}/{auto_increment}/{take}")]
        public async Task<ActionResult> PlayerSearcher(string Name, int similar_char, int auto_increment, int take)
        {
            var  list_result= _main_context.players.Where(a=>a.Name.Intersect(Name).Count()<=similar_char&&a.auto_increment_index>=auto_increment ).OrderBy(a=>a.auto_increment_index)
                .Take(take).ToList();

            var list_return = new List<PlayerReturn>();
            foreach(var result in list_result)
            {
                list_return.Add(new PlayerReturn(result));
            }

            if (list_result.Count()> 0)
            {


                return Ok(new {page=list_return, first_coursor=list_result.Last().Name.Intersect(Name).Count(), 
                second_coursor=list_result.Last().auto_increment_index
                });
            }

            return Ok();
        }



        [HttpGet("get/best/players/{skip}/{take}")]
        [HttpGet("get/best/players/{skip}/{take}/{role}")]
        public async Task<ActionResult> BestPlayers(int skip, int take,int? role)
        {
            int OrderByFunc(Player a)
            {
                if (role == null)
                {
                    return a.games.Where(b => b.is_won).Count();
                }
                else
                {
                    return a.games.Where(b=> b.is_won&&b.role == (PlayerRole)role).Count();
                }
            }

            var result = _main_context.players.OrderBy(OrderByFunc).Skip(skip).Take(take).ToList();


            return Ok();
        }


        [NonAction]
        public async Task<ActionResult> ReturnWinrate(string Name)
        {
            Player player = await _main_context.players.FindAsync(Name);
            var played_games = player.games.Count();
            var liberal_winrate = player.games.Where(a => a.role == PlayerRole.LIBERAL&& a.is_won).Count()/played_games*100;
            var fascist_winrate = player.games.Where(a=>a.role==PlayerRole.FASCIST && a.is_won).Count() / played_games * 100;
            var hitler_winrate = player.games.Where(a => a.role == PlayerRole.HITLER && a.is_won).Count() / played_games * 100;
            return Ok(new { liberal_winrate = liberal_winrate, fascist_winrate=fascist_winrate, hitler_winrate=hitler_winrate });
        }
    }
}
