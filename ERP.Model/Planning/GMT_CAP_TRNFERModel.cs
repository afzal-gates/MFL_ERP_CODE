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
    public class GMT_CAP_TRNFERModel
    {
        public Int64 GMT_CAP_TRNFER_ID { get; set; }
        public Int64 GMT_CAPACITY_MN_ID { get; set; }
        public Int64 TO_PROD_TYP_ID { get; set; }
        public Int64 TRNFER_QTY_PCS { get; set; }
        public string FROM_PROD_TYP_NAME { get; set; }
        public string TO_PROD_TYP_NAME { get; set; }
        public decimal AVG_SMV { get; set; }
        public decimal AVG_CM { get; set; }
        public decimal AVG_FOB { get; set; }

        public List<GMT_CAP_TRNFERModel> Query(Int64 pGMT_CAPACITY_MN_ID,Int64? pGMT_PRODUCT_TYP_ID)
        {
            string sp = "pkg_planning_common.gmt_capacity_mn_select";
            //pOption=3000=>Select All Data
            try
            {
                var obList = new List<GMT_CAP_TRNFERModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pGMT_CAPACITY_MN_ID", Value =pGMT_CAPACITY_MN_ID},
                     new CommandParameter() {ParameterName = "pGMT_PRODUCT_TYP_ID", Value =pGMT_PRODUCT_TYP_ID},

                     new CommandParameter() {ParameterName = "pOption", Value = 3003},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
              foreach (DataRow dr in ds.Tables[0].Rows)
               {
                            GMT_CAP_TRNFERModel ob = new GMT_CAP_TRNFERModel();
                            ob.GMT_CAP_TRNFER_ID = (dr["GMT_CAP_TRNFER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_CAP_TRNFER_ID"]);
                            ob.GMT_CAPACITY_MN_ID = (dr["GMT_CAPACITY_MN_ID"] == DBNull.Value) ? -1 : Convert.ToInt64(dr["GMT_CAPACITY_MN_ID"]);

                            ob.TO_PROD_TYP_ID = (dr["TO_PROD_TYP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["TO_PROD_TYP_ID"]);
                            ob.TRNFER_QTY_PCS = (dr["TRNFER_QTY_PCS"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["TRNFER_QTY_PCS"]);
                            ob.FROM_PROD_TYP_NAME = (dr["FROM_PROD_TYP_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FROM_PROD_TYP_NAME"]);
                            ob.TO_PROD_TYP_NAME = (dr["TO_PROD_TYP_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TO_PROD_TYP_NAME"]);

                            ob.AVG_SMV = (dr["AVG_SMV"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["AVG_SMV"]);
                            ob.AVG_CM = (dr["AVG_CM"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["AVG_CM"]);
                            ob.AVG_FOB = (dr["AVG_FOB"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["AVG_FOB"]);

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