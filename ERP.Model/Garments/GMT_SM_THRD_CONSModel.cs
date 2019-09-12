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
    public class GMT_SM_THRD_CONSModel
    {
        public Int64 GMT_SM_THRD_CONS_ID { get; set; }
        public Int64 GMT_SW_MCN_SPEC_ID { get; set; }
        public Int64 LK_THRD_TYPE_ID { get; set; }
        public Int64 THRD_COUNT { get; set; }
        public Decimal THRD_CONS { get; set; }
        public Int64 CONS_MOU_ID { get; set; }
        public Decimal PCT_CONS { get; set; }
        public string THRD_TYPE_NAME { get; set; }
        public string CONS_MOU_CODE { get; set; }
        




        public List<GMT_SM_THRD_CONSModel> GetThrdConsList(Int64 pGMT_SW_MCN_SPEC_ID)
        {
            string sp = "pkg_gmt_swmcn.gmt_sw_mcn_spec_select";
            try
            {
                var obList = new List<GMT_SM_THRD_CONSModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pGMT_SW_MCN_SPEC_ID", Value = pGMT_SW_MCN_SPEC_ID},
                     new CommandParameter() {ParameterName = "pOption", Value = 3002},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    GMT_SM_THRD_CONSModel ob = new GMT_SM_THRD_CONSModel();
                    ob.GMT_SM_THRD_CONS_ID = (dr["GMT_SM_THRD_CONS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_SM_THRD_CONS_ID"]);
                    ob.GMT_SW_MCN_SPEC_ID = (dr["GMT_SW_MCN_SPEC_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_SW_MCN_SPEC_ID"]);
                    ob.LK_THRD_TYPE_ID = (dr["LK_THRD_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_THRD_TYPE_ID"]);
                    ob.THRD_COUNT = (dr["THRD_COUNT"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["THRD_COUNT"]);
                    ob.THRD_CONS = (dr["THRD_CONS"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["THRD_CONS"]);
                    ob.CONS_MOU_ID = (dr["CONS_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CONS_MOU_ID"]);
                    ob.PCT_CONS = (dr["PCT_CONS"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PCT_CONS"]);

                    ob.THRD_TYPE_NAME = (dr["THRD_TYPE_NAME"] == DBNull.Value) ? String.Empty : Convert.ToString(dr["THRD_TYPE_NAME"]);
                    ob.CONS_MOU_CODE = (dr["CONS_MOU_CODE"] == DBNull.Value) ? String.Empty : Convert.ToString(dr["CONS_MOU_CODE"]);
                    
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public GMT_SM_THRD_CONSModel Select(long ID)
        {
            string sp = "Select_GMT_SM_THRD_CONS";
            try
            {
                var ob = new GMT_SM_THRD_CONSModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pGMT_SM_THRD_CONS_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.GMT_SM_THRD_CONS_ID = (dr["GMT_SM_THRD_CONS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_SM_THRD_CONS_ID"]);
                    ob.GMT_SW_MCN_SPEC_ID = (dr["GMT_SW_MCN_SPEC_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_SW_MCN_SPEC_ID"]);
                    ob.LK_THRD_TYPE_ID = (dr["LK_THRD_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_THRD_TYPE_ID"]);
                    ob.THRD_COUNT = (dr["THRD_COUNT"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["THRD_COUNT"]);
                    ob.THRD_CONS = (dr["THRD_CONS"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["THRD_CONS"]);
                    ob.CONS_MOU_ID = (dr["CONS_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CONS_MOU_ID"]);
                    ob.PCT_CONS = (dr["PCT_CONS"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PCT_CONS"]);
                }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        
    }
}