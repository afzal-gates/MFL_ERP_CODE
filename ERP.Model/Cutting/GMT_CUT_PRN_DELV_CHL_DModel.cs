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
    public class GMT_CUT_PRN_DELV_CHL_DModel
    {
        public Int64 GMT_CUT_PRN_DELV_CHL_D_ID { get; set; }
        public Int64 GMT_CUT_PRN_DELV_CHL_H_ID { get; set; }
        public Int64 GMT_CUT_PNL_BNK_ID { get; set; }
        public Int64 DELV_QTY { get; set; }
        public List<GMT_CUT_PRN_DELV_CHL_DModel> Query(int pOption)
        {
            string sp = "pkg_name.gmt_cut_prn_delv_chl_d_select";
            //pOption=3000=>Select All Data
            try
            {
                var obList = new List<GMT_CUT_PRN_DELV_CHL_DModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pGMT_CUT_PRN_DELV_CHL_D_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =pOption},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
              foreach (DataRow dr in ds.Tables[0].Rows)
               {
                            GMT_CUT_PRN_DELV_CHL_DModel ob = new GMT_CUT_PRN_DELV_CHL_DModel();
                            ob.GMT_CUT_PRN_DELV_CHL_D_ID = (dr["GMT_CUT_PRN_DELV_CHL_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_CUT_PRN_DELV_CHL_D_ID"]);
                            ob.GMT_CUT_PRN_DELV_CHL_H_ID = (dr["GMT_CUT_PRN_DELV_CHL_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_CUT_PRN_DELV_CHL_H_ID"]);
                            ob.GMT_CUT_PNL_BNK_ID = (dr["GMT_CUT_PNL_BNK_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_CUT_PNL_BNK_ID"]);
                            ob.DELV_QTY = (dr["DELV_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DELV_QTY"]);
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