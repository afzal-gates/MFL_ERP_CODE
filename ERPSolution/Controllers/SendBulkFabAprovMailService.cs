using ERP.Model;
using Postal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace ERPSolution.Controllers
{
    public class SendBulkFabAprovMailService : Email
    {
        public string To { get; set; }
        private MC_BLK_FAB_REQ_HModel _data = null;


        public MC_BLK_FAB_REQ_HModel data
        {
            get
            {
                if (_data == null)
                {
                    _data = new MC_BLK_FAB_REQ_HModel();
                }
                return _data;
            }
            set
            {
                _data = value;
            }
        }

    }

    public class SendProjectionAproveMailService : Email
    {
        public string To { get; set; }
        private MC_PROV_FAB_BK_HModel _data = null;


        public MC_PROV_FAB_BK_HModel data
        {
            get
            {
                if (_data == null)
                {
                    _data = new MC_PROV_FAB_BK_HModel();
                }
                return _data;
            }
            set
            {
                _data = value;
            }
        }

    }

    public class SendSmplProgAprovMailService : Email
    {
        public string To { get; set; }
        private MC_SMP_REQ_HModel _data = null;


        public MC_SMP_REQ_HModel data
        {
            get
            {
                if (_data == null)
                {
                    _data = new MC_SMP_REQ_HModel();
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
