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
    public class TnASummaryMail
    {
        public Int64 MC_ORD_TNA_ID { get; set; }
        public long MC_BYR_ACC_ID { get; set; }
        public Int64 MC_ORDER_H_ID { get; set; }
        public String PLAN_START_DT { get; set; }
        public String SHIP_DT { get; set; }
        public Int64? LAG_DAYS { get; set; }
        public string REMARKS { get; set; }
        public string TA_TASK_NAME_EN { get; set; }
        public string ORDER_NO { get; set; }
        public string WORK_STYLE { get; set; }
        public string BYR_ACC_NAME_EN { get; set; }

        public Int32 BYR_ACC_SPAN { get; set; }
        public Int32 BYR_ACC_SL { get; set; }
        public Int32 STYL_EXT_SPAN { get; set; }
        public Int32 STYL_EXT_SL { get; set; }
        public Int32 ORDER_SPAN { get; set; }
        public Int32 ORDER_SL { get; set; }

        public Int32 SC_USER_ID { get; set; }
        public String MAIL_ADDRESS { get; set; }


        public List<TnASummaryMail> SelectAll(Int32 pSC_USER_ID)
        {
            string sp = "pkg_tna.mc_order_tna_select_grid";
            try
            {
                var obList = new List<TnASummaryMail>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pSC_USER_ID", Value = pSC_USER_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3003},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    TnASummaryMail ob = new TnASummaryMail();
                    ob.MC_ORD_TNA_ID = (dr["MC_ORD_TNA_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_ORD_TNA_ID"]);
                    ob.MC_BYR_ACC_ID = (dr["MC_BYR_ACC_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BYR_ACC_ID"]);
                    ob.MC_ORDER_H_ID = (dr["MC_ORDER_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_ORDER_H_ID"]);
                    ob.PLAN_START_DT = (dr["PLAN_START_DT"] == DBNull.Value) ?string.Empty : Convert.ToDateTime(dr["PLAN_START_DT"]).ToString("dd-MMM-yyyy");
                    ob.SHIP_DT = (dr["SHIP_DT"] == DBNull.Value) ? string.Empty : Convert.ToDateTime(dr["SHIP_DT"]).ToString("dd-MMM-yyyy");


                    if (dr["LAG_DAYS"] != DBNull.Value)
                    {
                        ob.LAG_DAYS = Convert.ToInt64(dr["LAG_DAYS"]);
                    }
                    ob.TA_TASK_NAME_EN = (dr["TA_TASK_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TA_TASK_NAME_EN"]);
                    ob.WORK_STYLE = (dr["WORK_STYLE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["WORK_STYLE"]);
                    ob.BYR_ACC_NAME_EN = (dr["BYR_ACC_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BYR_ACC_NAME_EN"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                    ob.ORDER_NO = (dr["ORDER_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_NO"]);
                    ob.BYR_ACC_SPAN = (dr["BYR_ACC_SPAN"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["BYR_ACC_SPAN"]);
                    ob.BYR_ACC_SL = (dr["BYR_ACC_SL"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["BYR_ACC_SL"]);
                    ob.STYL_EXT_SPAN = (dr["STYL_EXT_SPAN"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["STYL_EXT_SPAN"]);
                    ob.STYL_EXT_SL = (dr["STYL_EXT_SL"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["STYL_EXT_SL"]);
                    ob.ORDER_SPAN = (dr["ORDER_SPAN"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["ORDER_SPAN"]);
                    ob.ORDER_SL = (dr["ORDER_SL"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["ORDER_SL"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<TnASummaryMail> getUserAndMailAddress()
        {
            string sp = "pkg_tna.mc_order_tna_select_grid";
            try
            {
                var obList = new List<TnASummaryMail>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pOption", Value =3004},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    TnASummaryMail ob = new TnASummaryMail();
                    ob.MAIL_ADDRESS = (dr["MAIL_ADDRESS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MAIL_ADDRESS"]);
                    ob.SC_USER_ID = (dr["SC_USER_ID"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["SC_USER_ID"]);
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