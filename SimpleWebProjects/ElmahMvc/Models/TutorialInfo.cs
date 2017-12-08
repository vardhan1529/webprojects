using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace ElmahMvc.Models
{
    public class TutorialInfo
    {
        [DisplayName("Title")]
        public string Title { get; set; }
        [DisplayName("Description")]
        public string Description { get; set; }
        [DisplayName("File")]
        public List<HttpPostedFileBase> Data { get; set; }
    }
}