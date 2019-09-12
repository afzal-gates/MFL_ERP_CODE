using ERP.Model;
using Postal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERPSolution.Controllers
{
    public class SendYarnReplacementMailService : Email
    {
        public KNT_YRN_STR_RPL_REQ_HModel obj { get; set; }
        public List<KNT_YRN_STR_RPL_REQ_DModel> data { get; set; }

    }
}