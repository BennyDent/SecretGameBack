using SecretHitlerGeekTime.Models;
using SecretHitlerGeekTime.Models.InputModels;
using SecretHitlerGeekTime.Models.JoinTables;
using SecretHitlerGeekTime.Models.WinLoss;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace SecretHitlerGeekTime.Models
{
    public class Game
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int auto_increment_index { get; set; }
        public bool is_liberal {  get; set; }
        [Key]
        public Guid Id { get; set; }
        public DateTime date { get; set; }
        public int  game_index {  get; set; }  
        public ICollection<PlayerGame> players { get; set; }

        public Game() {
        
            players = new List<PlayerGame>();
        }

        public Game( DateTime Date, bool won, int Game_index ) {
            game_index = Game_index;
            date = Date;
            is_liberal = won;
        Id = Guid.NewGuid();
       
        players = new List<PlayerGame>();
            
            is_liberal = won;        
        }

    }
}
