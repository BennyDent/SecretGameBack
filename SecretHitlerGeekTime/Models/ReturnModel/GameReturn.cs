using Microsoft.Identity.Client;
using SecretHitlerGeekTime.Models.InputModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace SecretHitlerGeekTime.Models.ReturnModel
{
    internal class GameReturn
    {
        public  string game_name {  get; set; }
        public List<PlayerWinInput> player_win {  get; set; }
        public DateTime date { get; set; }




        public GameReturn(Game game) {


            player_win = new List<PlayerWinInput>();
            date = game.date;
            foreach(var player in game.players)
            {
                player_win.Add(new PlayerWinInput(player));
            }

        
        }
    }
}
