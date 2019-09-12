using ERP.Model;
using Postal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace ERPSolution.Controllers
{
    public class TnATaskFailureMailService : Email
    {
        public string To { get; set; }
        private List<VM_TNA_TASK_FAILURE_MAILModel> _data = null;
        public List<VM_TNA_TASK_FAILURE_MAILModel> data
        {
            get
            {
                if (_data == null)
                {
                    _data = new List<VM_TNA_TASK_FAILURE_MAILModel>();
                }
                return _data;
            }
            set
            {
                _data = value;
            }
        }

        private List<VM_TNA_TASK_FAILURE_MAILModel> _data1 = null;
        public List<VM_TNA_TASK_FAILURE_MAILModel> data1
        {
            get
            {
                if (_data1 == null)
                {
                    _data1 = new List<VM_TNA_TASK_FAILURE_MAILModel>();
                }
                return _data1;
            }
            set
            {
                _data1 = value;
            }
        }


    }
}
