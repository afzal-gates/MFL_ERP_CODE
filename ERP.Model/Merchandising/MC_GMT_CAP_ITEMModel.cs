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
    public class MC_GMT_CAP_ITEMModel
    {
        public Int64 MC_GMT_CAP_ITEM_ID { get; set; }
        public Int64 MC_ORDER_SHIP_ID { get; set; }
        public Int64 GMT_CAPACITY_WK_ID { get; set; }
        public Int64 LK_GARM_TYPE_ID { get; set; }
        public Int64 ORDER_QTY { get; set; }
        public Int64 QTY_MOU_ID { get; set; }
        public Decimal UNIT_PRICE { get; set; }
        public Int64 RF_CURRENCY_ID { get; set; }
        public string GMT_TYP_NAME { get; set; }
        public decimal SMV { get; set; }
        public List<MC_GMT_CAP_ITEMModel> Query(Int64? pMC_ORDER_SHIP_ID)
        {
            string sp = "PKG_MERCHANDISING.mc_gmt_cap_item_select";
            //pOption=3000=>Select All Data
            try
            {
                var obList = new List<MC_GMT_CAP_ITEMModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_ORDER_SHIP_ID", Value = pMC_ORDER_SHIP_ID },
                     new CommandParameter() {ParameterName = "pOption", Value = 3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
              foreach (DataRow dr in ds.Tables[0].Rows)
               {
                            MC_GMT_CAP_ITEMModel ob = new MC_GMT_CAP_ITEMModel();
                            ob.MC_GMT_CAP_ITEM_ID = (dr["MC_GMT_CAP_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_GMT_CAP_ITEM_ID"]);
                            ob.GMT_CAPACITY_WK_ID = (dr["GMT_CAPACITY_WK_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_CAPACITY_WK_ID"]);
                            ob.GMT_TYP_NAME = (dr["GMT_TYP_NAME"] == DBNull.Value) ? String.Empty : Convert.ToString(dr["GMT_TYP_NAME"]);
                  
                            ob.LK_GARM_TYPE_ID = (dr["LK_GARM_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_GARM_TYPE_ID"]);
                            ob.ORDER_QTY = (dr["ORDER_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ORDER_QTY"]);
                            ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);
                            ob.UNIT_PRICE = (dr["UNIT_PRICE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["UNIT_PRICE"]);
                            ob.RF_CURRENCY_ID = (dr["RF_CURRENCY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_CURRENCY_ID"]);
                            ob.SMV = (dr["SMV"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["SMV"]);
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