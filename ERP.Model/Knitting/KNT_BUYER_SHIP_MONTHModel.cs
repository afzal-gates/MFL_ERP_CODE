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
    public class KNT_BUYER_SHIP_MONTHModel
    {
        public string MONTH_NAME { get; set; }
        public DateTime FIRST_DT { get; set; }
        public DateTime LAST_DT { get; set; }
        public string BYR_ACC_NAME_EN { get; set; }
        public Int64 MC_BYR_ACC_ID { get; set; }

        private List<KNT_TNA_PRODUCTIONModel> _items = null;
        public List<KNT_TNA_PRODUCTIONModel> items
        {
            get
            {
                if (_items == null)
                {
                    _items = new List<KNT_TNA_PRODUCTIONModel>();
                }
                return _items;
            }
            set
            {
                _items = value;
            }
        }


        public List<KNT_BUYER_SHIP_MONTHModel> SelectAll()
        {
            string sp = "pkg_tna.buyer_ship_month_select";
            try
            {
                var obList = new List<KNT_BUYER_SHIP_MONTHModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
              foreach (DataRow dr in ds.Tables[0].Rows)
               {
                            KNT_BUYER_SHIP_MONTHModel ob = new KNT_BUYER_SHIP_MONTHModel();
                            ob.MONTH_NAME = (dr["MONTH_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MONTH_NAME"]);
                            ob.FIRST_DT = (dr["FIRST_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["FIRST_DT"]);
                            ob.LAST_DT = (dr["LAST_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_DT"]);
                            ob.BYR_ACC_NAME_EN = (dr["BYR_ACC_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BYR_ACC_NAME_EN"]);
                            ob.MC_BYR_ACC_ID = (dr["MC_BYR_ACC_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BYR_ACC_ID"]);
                            ob.items = new KNT_TNA_PRODUCTIONModel().QueryData(ob.MC_BYR_ACC_ID, ob.FIRST_DT, ob.LAST_DT);
                            obList.Add(ob);
               }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}