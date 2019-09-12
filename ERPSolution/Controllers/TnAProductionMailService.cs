using ERP.Model;
using Postal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace ERPSolution.Controllers
{
    public class TnAProductionMailService : Email
    {
        public string To { get; set; }
        private List<KNT_BUYER_SHIP_MONTHModel> _data = null;
        public List<KNT_BUYER_SHIP_MONTHModel> data
        {
            get
            {
                if (_data == null)
                {
                    _data = new List<KNT_BUYER_SHIP_MONTHModel>();
                }
                return _data;
            }
            set
            {
                _data = value;
            }
        }
    }
}
