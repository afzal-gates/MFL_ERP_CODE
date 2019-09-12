using ERP.Model;
using Postal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace ERPSolution.Controllers
{
    public class PlanChangeMailService : Email
    {
        public string To { get; set; }
        private List<GMT_PLN_STYL_CNGE_LOGModel> _log = null;
        public List<GMT_PLN_STYL_CNGE_LOGModel> log
        {
            get
            {
                if (_log == null)
                {
                    _log = new List<GMT_PLN_STYL_CNGE_LOGModel>();
                }
                return _log;
            }
            set
            {
                _log = value;
            }
        }

        private List<GMT_PLN_STYL_CNG_DEPTModel> _logDept = null;
        public List<GMT_PLN_STYL_CNG_DEPTModel> logDept
        {
            get
            {
                if (_logDept == null)
                {
                    _logDept = new List<GMT_PLN_STYL_CNG_DEPTModel>();
                }
                return _logDept;
            }
            set
            {
                _logDept = value;
            }
        }


    }
}
