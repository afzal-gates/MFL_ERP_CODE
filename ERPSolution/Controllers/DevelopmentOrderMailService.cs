using ERP.Model;
using Postal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace ERPSolution.Controllers
{
    public class DevelopmentOrderMailService : Email
    {
        public string To { get; set; }
        private List<TNA_CROSS_TAB_RPTModel> _data = null;
        public List<TNA_CROSS_TAB_RPTModel> data
        {
            get
            {
                if (_data == null)
                {
                    _data = new List<TNA_CROSS_TAB_RPTModel>();
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
