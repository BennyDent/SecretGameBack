using System.ComponentModel.DataAnnotations;
using SecretHitlerGeekTime.Enums;
namespace SecretHitlerGeekTime.Models.WinLoss
{
    public class WinLoss
    {
        [Key]
        public Guid Id { get; set; }
        public string PlayerName { get; set; }
        public PlayerRole role { get; set; }
        public Player Player { get; set; }
        public Game Game { get; set; }
        public Guid GameId { get; set; }
    
        public WinLoss() {}
    public WinLoss(Player player, Game game, int is_liberal)
        {
            Id = Guid.NewGuid();
            PlayerName = player.Name;
            Player = player;
            Game = game;
            GameId = game.Id;
            this.role = (PlayerRole)is_liberal;
        }
    }
}
