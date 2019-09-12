using ERP.Model;
using Postal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ERPSolution.Controllers
{
    public class SendMixLotBatchApprovalMailService : Email
    {        
        public string To { get; set; }
        private DYE_BATCH_PLANModel _data = null;


        public DYE_BATCH_PLANModel data
        {
            get
            {
                if (_data == null)
                {
                    _data = new DYE_BATCH_PLANModel();
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