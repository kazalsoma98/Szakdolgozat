using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace OwinWebApi.DB.Models
{
    public partial class Data
    {
        public int Id { get; set; }
        public DateTime? Idopont { get; set; }
        public int? AkkuFeszultseg { get; set; }
        public int? AkkuToltoaram { get; set; }
        public int? AkkuHomerseklet { get; set; }
        public int? MotorHomerseklet { get; set; }
        public int? MotorAram { get; set; }
        public bool? AkkuHutes { get; set; }
        public bool? MotorHutes { get; set; }
        public int? RelativSebesseg { get; set; }
        public int? AbszolutSebesseg { get; set; }
        public int? Fenyerosseg { get; set; }
    }
}
