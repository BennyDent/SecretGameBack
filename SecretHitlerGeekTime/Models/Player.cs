using System.ComponentModel.DataAnnotations;
using SecretHitlerGeekTime.Models.JoinTables;
using SecretHitlerGeekTime.Models.WinLoss;
using SecretHitlerGeekTime.Models;
using System.ComponentModel.DataAnnotations.Schema;
namespace SecretHitlerGeekTime.Models
{
    public class Player
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int auto_increment_index { get; set; }
        //public Guid Id { get; set; }
        [Key]
        public string Name { get; set; }
        public ICollection<PlayerGame> games { get; set; }
     

        public DateTime registration_date { get; set; }
        public Player() {
           games = new List<PlayerGame>();
          
        }
        public Player(string name) {
        Name = name;
            games = new List<PlayerGame>();
      
        }
    }
}
