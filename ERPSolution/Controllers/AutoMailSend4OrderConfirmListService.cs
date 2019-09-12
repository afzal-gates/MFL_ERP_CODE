using ERP.Model;
using Postal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace ERPSolution.Controllers
{
    public class AutoMailSend4OrderConfirmListService : Email
    {
        //public string To { get; set; }
        private MC_ORDER_ENTRY_NOTIFICATION_VM_Model _data = null;


        public MC_ORDER_ENTRY_NOTIFICATION_VM_Model data
        {
            get
            {
                if (_data == null)
                {
                    _data = new MC_ORDER_ENTRY_NOTIFICATION_VM_Model();
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
