using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERPSolution.Models
{
    public class MenuModel
    {
        public Int64 ID { get; set; }
        public string Name { get; set; }
        public string Application { get; set; }
        public string Functionality { get; set; }
        public string Page { get; set; }
    }
}