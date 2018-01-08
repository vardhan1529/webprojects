using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ElmahMvc.Game
{
    public class GameInfo
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int NoofPlayers { get; set; }

        public string OriginCountry { get; set; }
    }
}