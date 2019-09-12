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
    public class GMT_CUT_PNL_INSPTN_DModel
    {
        public Int64 GMT_CUT_PNL_INSPTN_D_ID { get; set; }
        public string FB_DFCT_TYPE_NAME { get; set; }
        public Int32 DISPLAY_ORDER { get; set; }
        public Int64 RF_FB_DFCT_TYPE_ID { get; set; }
        public Int64 NO_OF_DFCT { get; set; }
        public List<GMT_CUT_PNL_INSPTN_DModel> Query(Int64 pGMT_CUT_PNL_INSPTN_H_ID)
        {
            string sp = "PKG_GMT_CUT_PLN_INS.gmt_cut_pnl_insptn_h_select";
 
            try
            {
                var obList = new List<GMT_CUT_PNL_INSPTN_DModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pGMT_CUT_PNL_INSPTN_H_ID", Value = pGMT_CUT_PNL_INSPTN_H_ID },
                     new CommandParameter() {ParameterName = "pOption", Value = 3006},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
              foreach (DataRow dr in ds.Tables[0].Rows)
               {
                            GMT_CUT_PNL_INSPTN_DModel ob = new GMT_CUT_PNL_INSPTN_DModel();
                            ob.GMT_CUT_PNL_INSPTN_D_ID = (dr["GMT_CUT_PNL_INSPTN_D_ID"] == DBNull.Value) ? -1 : Convert.ToInt64(dr["GMT_CUT_PNL_INSPTN_D_ID"]);
                            ob.FB_DFCT_TYPE_NAME = (dr["FB_DFCT_TYPE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FB_DFCT_TYPE_NAME"]);
                            ob.DISPLAY_ORDER = (dr["DISPLAY_ORDER"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["DISPLAY_ORDER"]);
                            ob.RF_FB_DFCT_TYPE_ID = (dr["RF_FB_DFCT_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_FB_DFCT_TYPE_ID"]);
                            ob.NO_OF_DFCT = (dr["NO_OF_DFCT"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["NO_OF_DFCT"]);
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