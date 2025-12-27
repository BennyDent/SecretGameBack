using SecretHitlerGeekTime.Models.JoinTables;
using System;
using System.Collections.Generic;
using System.Text;

namespace SecretHitlerGeekTime.Models.InputModels
{
  public class PlayerWinInput
    {
        public string PlayerName { get; set; }
        public int role { get; set; }
        public bool is_won { get; set; }



        public PlayerWinInput() { }
        public PlayerWinInput(string playerName, int Role, bool Is_won) { 
        
        
        PlayerName = playerName;
        role = Role;
        is_won = Is_won;
        
        
        }


        public PlayerWinInput(PlayerGame player_game)
        {
            PlayerName = player_game.PlayerName;
            role = (int)player_game.role;
            is_won = player_game.is_won;



        }

    }
}
