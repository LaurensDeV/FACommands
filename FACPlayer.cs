using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.ComponentModel;
using System.Timers;
using Terraria;
using TShockAPI;

namespace FACommands
{
    public class FACPlayer
    {
        public int Index { get; set; }
        public TSPlayer TSPlayer { get { return TShock.Players[Index]; } }
        public int GlobalCooldown { get; set; }
        public FACPlayer(int index)
        {
            this.Index = index;
        }
    }
}