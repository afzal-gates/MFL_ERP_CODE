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
    public class GMT_PLN_LINE_LOAD_DModel
    {
        public Int64 GMT_PLN_LINE_LOAD_D_ID { get; set; }
        public Int64 GMT_PLN_LINE_LOAD_ID { get; set; }
        public Int64 GMT_PROD_PLN_CLNDR_ID { get; set; }
        public DateTime WH_START_TIME { get; set; }
        public Int64 GEN_WH { get; set; }
        public Int64 GEN_WH_PASSED { get; set; }
        public Int64 OT_HR { get; set; }
        public DateTime NON_BSN_HR_START { get; set; }
        public DateTime NON_BSN_HR_END { get; set; }
        public Int64 OT_HR_PASSED { get; set; }
        public Int64 GEN_MP { get; set; }
        public Int64 OT_MP { get; set; }
        public Decimal RF_PLAN_EFF { get; set; }
        public Int64 REF_PLN_G_AH { get; set; }
        public Int64 REF_PLN_OT_AH { get; set; }
        public Decimal SMV { get; set; }
        public Int64 DAY_NO { get; set; }
        public Int64 ACHV_GEN_SAH { get; set; }
        public Int64 ACHV_OT_SAH { get; set; }
        public Int64 TOT_REF_PLN_AH { get; set; }
        public Int64 TOT_REF_PLN_SAH { get; set; }
        public Int64 TOT_ACHV_SAH { get; set; }
        public Int64 TOT_AH_USED { get; set; }
        public Int64 TOT_AH_TO_USE { get; set; }
        public Int64 REQ_SAH { get; set; }
        public Decimal REQ_EFF { get; set; }
        public string STS_FLAG { get; set; }
        public DateTime CALENDAR_DT { get; set; }
        public long PLAN_QTY { get; set; }
        public long ACHV_GEN_PROD { get; set; }
        public long ACHV_OT_PROD { get; set; }
        public long TOT_PROD { get; set; }
        public string IS_LC_APPLIED { get; set; }
        public string IS_INDIV_CHANGE { get; set; }
        public List<GMT_PLN_LINE_LOAD_DModel> Query(Int64 pGMT_PLN_LINE_LOAD_ID,Int32? pOption =null)
        {
            string sp = "pkg_gmt_pln_line_load.gmt_pln_line_load_select";
            //pOption=3000=>Select All Data
            try
            {
                var obList = new List<GMT_PLN_LINE_LOAD_DModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pGMT_PLN_LINE_LOAD_ID", Value = pGMT_PLN_LINE_LOAD_ID},
                     new CommandParameter() {ParameterName = "pOption", Value = pOption??3016},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
              foreach (DataRow dr in ds.Tables[0].Rows)
               {
                            GMT_PLN_LINE_LOAD_DModel ob = new GMT_PLN_LINE_LOAD_DModel();
                            ob.GMT_PLN_LINE_LOAD_D_ID = (dr["GMT_PLN_LINE_LOAD_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_PLN_LINE_LOAD_D_ID"]);
                            ob.GMT_PLN_LINE_LOAD_ID = (dr["GMT_PLN_LINE_LOAD_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_PLN_LINE_LOAD_ID"]);
                            ob.GMT_PROD_PLN_CLNDR_ID = (dr["GMT_PROD_PLN_CLNDR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_PROD_PLN_CLNDR_ID"]);
                            ob.WH_START_TIME = (dr["WH_START_TIME"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["WH_START_TIME"]);
                            ob.GEN_WH = (dr["GEN_WH"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GEN_WH"]);
                            ob.GEN_WH_PASSED = (dr["GEN_WH_PASSED"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GEN_WH_PASSED"]);
                            ob.OT_HR = (dr["OT_HR"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["OT_HR"]);
                            ob.NON_BSN_HR_START = (dr["NON_BSN_HR_START"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["NON_BSN_HR_START"]);
                            ob.NON_BSN_HR_END = (dr["NON_BSN_HR_END"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["NON_BSN_HR_END"]);
                            ob.CALENDAR_DT = (dr["CALENDAR_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CALENDAR_DT"]);
                            ob.OT_HR_PASSED = (dr["OT_HR_PASSED"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["OT_HR_PASSED"]);
                            ob.GEN_MP = (dr["GEN_MP"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GEN_MP"]);
                            ob.OT_MP = (dr["OT_MP"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["OT_MP"]);
                            ob.RF_PLAN_EFF = (dr["RF_PLAN_EFF"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RF_PLAN_EFF"]);
                            ob.REF_PLN_G_AH = (dr["REF_PLN_G_AH"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["REF_PLN_G_AH"]);
                            ob.REF_PLN_OT_AH = (dr["REF_PLN_OT_AH"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["REF_PLN_OT_AH"]);
                            ob.SMV = (dr["SMV"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["SMV"]);
                            ob.DAY_NO = (dr["DAY_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DAY_NO"]);
                            ob.ACHV_GEN_SAH = (dr["ACHV_GEN_SAH"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ACHV_GEN_SAH"]);
                            ob.ACHV_OT_SAH = (dr["ACHV_OT_SAH"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ACHV_OT_SAH"]);
                            ob.TOT_REF_PLN_AH = (dr["TOT_REF_PLN_AH"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["TOT_REF_PLN_AH"]);
                            ob.TOT_REF_PLN_SAH = (dr["TOT_REF_PLN_SAH"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["TOT_REF_PLN_SAH"]);
                            ob.TOT_ACHV_SAH = (dr["TOT_ACHV_SAH"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["TOT_ACHV_SAH"]);
                            ob.TOT_AH_USED = (dr["TOT_AH_USED"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["TOT_AH_USED"]);
                            ob.TOT_AH_TO_USE = (dr["TOT_AH_TO_USE"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["TOT_AH_TO_USE"]);
                            ob.REQ_SAH = (dr["REQ_SAH"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["REQ_SAH"]);
                            ob.REQ_EFF = (dr["REQ_EFF"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["REQ_EFF"]);
                            ob.STS_FLAG = (dr["STS_FLAG"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STS_FLAG"]);

                            if (dr["RF_RESP_DEPT_ID"] != DBNull.Value)
                            {
                                ob.RF_RESP_DEPT_ID = Convert.ToInt64(dr["RF_RESP_DEPT_ID"]);
                            }
                            ob.ACHV_GEN_PROD = (dr["ACHV_GEN_PROD"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ACHV_GEN_PROD"]);

                            ob.PLAN_OP = (dr["PLAN_OP"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PLAN_OP"]);
                            ob.PLAN_HP = (dr["PLAN_HP"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PLAN_HP"]);

                            ob.ACHV_OT_PROD = (dr["ACHV_OT_PROD"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ACHV_OT_PROD"]);
                            ob.TOT_PROD = (dr["TOT_PROD"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["TOT_PROD"]);
                            ob.IS_LC_APPLIED = (dr["IS_LC_APPLIED"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_LC_APPLIED"]);
                            ob.IS_INDIV_CHANGE = (dr["IS_INDIV_CHANGE"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_INDIV_CHANGE"]);
                            ob.REQ_PROD_QTY = (dr["REQ_PROD_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["REQ_PROD_QTY"]);

                            ob.MAX_ALLOWED_OT_HRS = (dr["MAX_ALLOWED_OT_HRS"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MAX_ALLOWED_OT_HRS"]);
                            ob.EWH = (dr["EWH"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["EWH"]);

                            if (ob.EWH > 0)
                            {
                                ob.REQ_PROD_HR = Convert.ToInt64(ob.REQ_PROD_QTY / ob.EWH);
                            }

                            ob.REASON_OF_EOT = (dr["REASON_OF_EOT"] == DBNull.Value) ? String.Empty : Convert.ToString(dr["REASON_OF_EOT"]);


                            obList.Add(ob);
               }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public long REQ_PROD_QTY { get; set; }
        public long REQ_PROD_HR { get; set; }
        public long? RF_RESP_DEPT_ID { get; set; }

        public long MAX_ALLOWED_OT_HRS { get; set; }

        public string REASON_OF_EOT { get; set; }

        public long PLAN_OP { get; set; }

        public long PLAN_HP { get; set; }

        public long EWH { get; set; }
    }
}