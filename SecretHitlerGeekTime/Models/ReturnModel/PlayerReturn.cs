using System;
using System.Collections.Generic;
using System.Text;

namespace SecretHitlerGeekTime.Models.ReturnModel
{
    internal class PlayerReturn
    {

        public string Name { get; set; }

        public PlayerReturn(Player player)
        {
            Name = player.Name;
        }
    }
}
