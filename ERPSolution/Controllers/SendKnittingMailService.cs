using ERP.Model;
using Postal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace ERPSolution.Controllers
{
    public class SendOTP4GreyFabTransMailService : Email
    {
        public string To { get; set; }
        public string SUBJECT_TEXT { get; set; }
        public Int32 LK_TRN_SRC_ID { get; set; }
        private KNT_ORD_TRN_REQ_HModel _data = null;


        public KNT_ORD_TRN_REQ_HModel data
        {
            get
            {
                if (_data == null)
                {
                    _data = new KNT_ORD_TRN_REQ_HModel();
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
