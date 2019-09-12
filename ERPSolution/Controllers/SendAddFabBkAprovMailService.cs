using ERP.Model;
using Postal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace ERPSolution.Controllers
{
    public class SendAddFabBkAprovMailService : Email
    {
        public string To { get; set; }
        private MC_BLK_ADFB_REQ_RPT_VM _data = null;


        public MC_BLK_ADFB_REQ_RPT_VM data
        {
            get
            {
                if (_data == null)
                {
                    _data = new MC_BLK_ADFB_REQ_RPT_VM();
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
