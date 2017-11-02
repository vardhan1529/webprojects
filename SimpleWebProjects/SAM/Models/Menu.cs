using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SAM.Models
{
    public class Menu
    {
        public string Name { get; set; }

        public int Id { get; set; }

        public int? PId { get; set; }

        public List<Menu> ChildMenu { get; set; }


        public static List<Menu> GroupMenu(List<Menu> items)
        {
            var res = new List<Menu>();

            foreach (var item in items.Where(m => !m.PId.HasValue))
            {
                var i = new Menu() { Id = item.Id, Name = item.Name, PId = item.PId };
                AddChildItems(i, items);
                res.Add(i);
            }

            return res;
        }

        public static void AddChildItems(Menu r, List<Menu> items)
        {
            r.ChildMenu = new List<Menu>();
            var citems = items.Where(n => n.PId == r.Id);

            foreach (var item in citems)
            {
                var m = new Menu() { Id = item.Id, Name = item.Name, PId = item.PId };
                r.ChildMenu.Add(m);
                AddChildItems(m, items);
            }
        }
    }
}