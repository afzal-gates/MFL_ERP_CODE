using Postal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace ERPSolution.Controllers
{
    public class JobCardCheckingMailService : Email
    {
        public string ACC_PAY_PERIOD_NAME { get; set; }
    }
}
