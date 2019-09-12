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
    public class KNT_TNA_PRODUCTIONModel
    {
        public string WORK_STYLE_NO { get; set; }
        public string MC_ORDER_NO_LST { get; set; }
        public DateTime SHIP_DT { get; set; }
        public string TOT_ORD_QTY { get; set; }
        public string TA_TASK_NAME_EN { get; set; }
        public DateTime PLAN_END_DT { get; set; }
        public Int64 NET_GFAB_QTY { get; set; }
        public Int64 MC_FAB_PROD_ORD_H_ID { get; set; }
        public Int64 ORDER_SPAN_SPAN { get; set; }
        public Int64 ORDER_SPAN_SL { get; set; }
        public Int64 PROD_QC_PASS_QTY { get; set; }
        public Int64 DAYS_2_GO { get; set; }
        public Decimal PCT_COMPLETE { get; set; }
        public DateTime ORD_CONF_DT { get; set; }
        public int LEAD_TIME { get; set; }
        public string STYLE_DESC { get; set; }

        public List<KNT_TNA_PRODUCTIONModel> QueryData(Int64 pMC_BYR_ACC_ID, DateTime pFIRST_DT, DateTime pLAST_DT)
        {
            string sp = "pkg_tna.buyer_ship_month_select";
            try
            {
                var obList = new List<KNT_TNA_PRODUCTIONModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_BYR_ACC_ID", Value = pMC_BYR_ACC_ID},
                      new CommandParameter() {ParameterName = "pFIRST_DT", Value = pFIRST_DT.Date},
                       new CommandParameter() {ParameterName = "pLAST_DT", Value = pLAST_DT.Date},
                     new CommandParameter() {ParameterName = "pOption", Value =3001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
              foreach (DataRow dr in ds.Tables[0].Rows)
               {
                            KNT_TNA_PRODUCTIONModel ob = new KNT_TNA_PRODUCTIONModel();
                            ob.WORK_STYLE_NO = (dr["WORK_STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["WORK_STYLE_NO"]);
                            ob.MC_ORDER_NO_LST = (dr["MC_ORDER_NO_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MC_ORDER_NO_LST"]);
                            ob.SHIP_DT = (dr["SHIP_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["SHIP_DT"]);
                            ob.ORD_CONF_DT = (dr["ORD_CONF_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["ORD_CONF_DT"]);

                            ob.LEAD_TIME = (dr["LEAD_TIME"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["LEAD_TIME"]);

                            ob.TOT_ORD_QTY = (dr["TOT_ORD_QTY"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TOT_ORD_QTY"]);
                            ob.STYLE_DESC = (dr["STYLE_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_DESC"]);

                            ob.TA_TASK_NAME_EN = (dr["TA_TASK_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TA_TASK_NAME_EN"]);
                            ob.PLAN_END_DT = (dr["PLAN_END_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["PLAN_END_DT"]);
                            ob.NET_GFAB_QTY = (dr["NET_GFAB_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["NET_GFAB_QTY"]);
                            ob.MC_FAB_PROD_ORD_H_ID = (dr["MC_FAB_PROD_ORD_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_FAB_PROD_ORD_H_ID"]);
                            ob.ORDER_SPAN_SPAN = (dr["ORDER_SPAN_SPAN"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ORDER_SPAN_SPAN"]);
                            ob.ORDER_SPAN_SL = (dr["ORDER_SPAN_SL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ORDER_SPAN_SL"]);
                            ob.PROD_QC_PASS_QTY = (dr["PROD_QC_PASS_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PROD_QC_PASS_QTY"]);
                            ob.DAYS_2_GO = (dr["DAYS_2_GO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DAYS_2_GO"]);
                            ob.PCT_COMPLETE = (dr["PCT_COMPLETE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PCT_COMPLETE"]);
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