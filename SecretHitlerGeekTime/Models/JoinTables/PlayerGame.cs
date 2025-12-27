using System.ComponentModel.DataAnnotations;
using SecretHitlerGeekTime.Enums;
namespace SecretHitlerGeekTime.Models.JoinTables
{
    public class PlayerGame
    {
        [Key]
        public Guid Id { get; set; }


       
        


        public Game Game { get; set; }
        public Guid GameId { get; set; }
        public Player Player { get; set; }
        public string PlayerName { get; set; }
        public PlayerRole role { get; set; }
        public  bool  is_won { get; set; }

        public PlayerGame(Game game, Player player, int player_role, bool Is_won)
        {
            Game = game;
            Player = player;
            GameId = game.Id;
            PlayerName = player.Name;
            role = (PlayerRole)player_role;
            is_won = Is_won;
        }
    }
}
