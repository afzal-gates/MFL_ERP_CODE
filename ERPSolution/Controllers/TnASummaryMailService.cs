using ERP.Model;
using Postal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace ERPSolution.Controllers
{
    public class TnASummaryMailService : Email
    {
        public string To { get; set; }
        private List<TnASummaryMail> _data = null;
        public List<TnASummaryMail> data
        {
            get
            {
                if (_data == null)
                {
                    _data = new List<TnASummaryMail>();
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
