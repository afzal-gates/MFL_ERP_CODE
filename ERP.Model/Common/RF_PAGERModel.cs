using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Web;

namespace ERP.Model
{
    public class RF_PAGERModel
    {
        public Int64 total { get; set; }

        public object data { get; set; }

        //public List<MC_STYLE_ITEM_ViewModel> data
        //{
        //    get
        //    {
        //        if (_data == null)
        //        {
        //            _data = new List<MC_STYLE_ITEM_ViewModel>();
        //        }
        //        return _data;
        //    }
        //    set
        //    {
        //        _data = value;
        //    }
        //}

        
    }
}