using ERP.Model;
using Postal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace ERPSolution.Controllers
{
    public class MailSendOTP4OrderConfirmService : Email
    {
        public string To { get; set; }
        private CAP_BKING4OTP_VM_Model _data = null;


        public CAP_BKING4OTP_VM_Model data
        {
            get
            {
                if (_data == null)
                {
                    _data = new CAP_BKING4OTP_VM_Model();
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
