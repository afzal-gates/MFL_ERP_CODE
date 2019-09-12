using Postal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace ERPSolution.Controllers
{
    public class OrderConfirmationMailService : Email
    {
        public string MAIL_LIST { get; set; }
        public string DESC { get; set; }
        public string REDIRECT_URL { get; set; }
    }
}
