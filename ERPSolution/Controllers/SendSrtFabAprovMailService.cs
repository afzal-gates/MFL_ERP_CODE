using ERP.Model;
using Postal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace ERPSolution.Controllers
{
    public class SendSrtFabAprovMailService : Email
    {
        public string To { get; set; }
        private KNT_SRT_FAB_REQ_RPT_VM _data = null;


        public KNT_SRT_FAB_REQ_RPT_VM data
        {
            get
            {
                if (_data == null)
                {
                    _data = new KNT_SRT_FAB_REQ_RPT_VM();
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
