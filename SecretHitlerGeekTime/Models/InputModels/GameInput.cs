using System;
using System.Collections.Generic;
using System.Text;

namespace SecretHitlerGeekTime.Models.InputModels
{
    internal class GameInput
    {

        public bool is_liberal { get; set; }
        public DateTime date { get; set; }
        public int game_index { get; set; }
        public Guid series_input {  get; set; }
        public IList<PlayerWinInput> players_win{ get; set; }

    }
}
