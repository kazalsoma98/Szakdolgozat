using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoatSimulation
{
    internal class Motorallapot
    {
        public int MotorAram { get; set; } = 0;
        public int MotorHomerseklet { get; set; } = 20;
        public int AkkuFeszultseg { get; set; } = 0;
        public int AkkuToltoaram { get; set; } = 0;
        public int AkkuHomerseklet { get; set; } = 20;
        public bool AkkuHutes { get; set; }=false;
        public bool MotorHutes { get; set; } = false;
        public int RelativSebesseg { get; set; } = 0;
        public int AbszolutSebesseg { get; set; } = 0;
        public int Fenyerosseg { get; set; } = 10;
    }
}
