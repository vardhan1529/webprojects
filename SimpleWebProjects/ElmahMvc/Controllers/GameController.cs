using ElmahMvc.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ElmahMvc.Controllers
{
    public class GameController : Controller
    {
        // GET: Game
        public ActionResult Index()
        {
            var c = new GameContext();
            c.Game.Add(new GameInfo() { Name = "   asfdasf af fasfsa     ", NoofPlayers = 15, OriginCountry = " ateat  " });
            c.SaveChanges();
            var data = c.Game.Where(m => m.Id > 5 && m.Id < 11).ToList();
            for (var i=0; i< data.Count; i++)
            {
                data[i].Name = data[i].Name + "  " + data[i].Name + "    ";
                data[i].OriginCountry = data[i].OriginCountry.Trim();
            }
            //c.Entry<Game.GameInfo>().State = System.Data.Entity.EntityState.Modified;
            c.SaveChanges();
            return View();
        }
    }
}