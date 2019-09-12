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
    public class GMT_CUT_TBL_TGTModel
    {
        public Int64? GMT_CUT_TBL_TGT_ID { get; set; }
        public Int64? GMT_PROD_PLN_CLNDR_ID { get; set; }
        public Int64? GMT_CUT_TABLE_ID { get; set; }
        public Int64? PLAN_MP_QTY { get; set; }
        public Int64? TGT_WORK_HR { get; set; }
        public Int64? TARGET_QTY { get; set; }
        public Int64? H9_MP_ADD { get; set; }
        public Int64? H10_MP_ADD { get; set; }
        public Int64? H11_MP_ADD { get; set; }
        public Int64? H12_MP_ADD { get; set; }
        public Int64? H13_MP_ADD { get; set; }
        public Int64? H14_MP_ADD { get; set; }
        public Int64? H15_MP_ADD { get; set; }
        public Int64? H15_P_MP_ADD { get; set; }
        public Int64? H15_P_WH { get; set; }
        public Int64? REVISED_TRGT { get; set; }
        public string REMARKS { get; set; }
        public Int64? TOT_MP_ADD { get; set; }
        public Int64? QTY_IN_BNDL { get; set; }
        public decimal? STNDR_TRGT_PER_HR_PER_EMP { get; set; }
        public string IS_FINALIZED { get; set; }
        public string IS_SHOW_FINALIZED { get; set; }
        public string TABLE_NO { get; set; }
        public string GMT_CUT_TBL_TGT_XML { get; set; }


        public string Save()
        {
            const string sp = "pkg_cut_common.gmt_cut_tbl_tgt_save";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pGMT_CUT_TBL_TGT_XML", Value = ob.GMT_CUT_TBL_TGT_XML},
                                   
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},              
                     new CommandParameter() {ParameterName = "pOption", Value =1000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);

                foreach (DataRow dr in ds.Tables["OUTPARAM"].Rows)
                {
                    jsonStr += Convert.ToString('"') + dr["KEY"].ToString() + Convert.ToString('"') + ":" + Convert.ToString('"') + (dr["VALUE"].ToString().Replace(@"""", @"\""")) + Convert.ToString('"');
                    if (i < ds.Tables["OUTPARAM"].Rows.Count)
                    {
                        jsonStr += ",";
                    }
                    else
                    {
                        jsonStr += "}";
                    }
                    i++;
                }
                return jsonStr;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public object GetProdPlnClndrId(DateTime? pCALENDAR_DT = null)
        {
            string sp = "pkg_cut_common.gmt_cut_tbl_tgt_select";
            try
            {
                Object obj = new { };
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pCALENDAR_DT", Value = pCALENDAR_DT!=null?pCALENDAR_DT:DateTime.Now },
                     new CommandParameter() {ParameterName = "pOption", Value = 3002},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    obj = new
                    {
                        GMT_PROD_PLN_CLNDR_ID = (dr["GMT_PROD_PLN_CLNDR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_PROD_PLN_CLNDR_ID"])
                    };
                }
                return obj;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<GMT_CUT_TBL_TGTModel> GetCuttingTableTergetList(Int64 pGMT_PROD_PLN_CLNDR_ID)
        {
            string sp = "pkg_cut_common.gmt_cut_tbl_tgt_select";
            try
            {
                var obList = new List<GMT_CUT_TBL_TGTModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pGMT_PROD_PLN_CLNDR_ID", Value =pGMT_PROD_PLN_CLNDR_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    GMT_CUT_TBL_TGTModel ob = new GMT_CUT_TBL_TGTModel();
                    
                    ob.GMT_CUT_TBL_TGT_ID = (dr["GMT_CUT_TBL_TGT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_CUT_TBL_TGT_ID"]);
                    ob.GMT_PROD_PLN_CLNDR_ID = (dr["GMT_PROD_PLN_CLNDR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_PROD_PLN_CLNDR_ID"]);
                    ob.GMT_CUT_TABLE_ID = (dr["GMT_CUT_TABLE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_CUT_TABLE_ID"]);
                    ob.PLAN_MP_QTY = (dr["PLAN_MP_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PLAN_MP_QTY"]);
                    ob.TGT_WORK_HR = (dr["TGT_WORK_HR"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["TGT_WORK_HR"]);
                    ob.TARGET_QTY = (dr["TARGET_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["TARGET_QTY"]);
                    ob.QTY_IN_BNDL = (dr["QTY_IN_BNDL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_IN_BNDL"]);

                    ob.H9_MP_ADD = (dr["H9_MP_ADD"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["H9_MP_ADD"]);
                    ob.H10_MP_ADD = (dr["H10_MP_ADD"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["H10_MP_ADD"]);
                    ob.H11_MP_ADD = (dr["H11_MP_ADD"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["H11_MP_ADD"]);
                    ob.H12_MP_ADD = (dr["H12_MP_ADD"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["H12_MP_ADD"]);
                    ob.H13_MP_ADD = (dr["H13_MP_ADD"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["H13_MP_ADD"]);
                    ob.H14_MP_ADD = (dr["H14_MP_ADD"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["H14_MP_ADD"]);
                    ob.H15_MP_ADD = (dr["H15_MP_ADD"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["H15_MP_ADD"]);
                    ob.H15_P_MP_ADD = (dr["H15_P_MP_ADD"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["H15_P_MP_ADD"]);
                    ob.H15_P_WH = (dr["H15_P_WH"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["H15_P_WH"]);
                    
                    if (dr["REVISED_TRGT"] != DBNull.Value)
                        ob.REVISED_TRGT = (dr["REVISED_TRGT"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["REVISED_TRGT"]);
                    
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                    ob.STNDR_TRGT_PER_HR_PER_EMP = (dr["STNDR_TRGT_PER_HR_PER_EMP"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["STNDR_TRGT_PER_HR_PER_EMP"]);
                    ob.TOT_MP_ADD = (dr["TOT_MP_ADD"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["TOT_MP_ADD"]);
                    ob.IS_FINALIZED = (dr["IS_FINALIZED"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_FINALIZED"]);
                    ob.IS_SHOW_FINALIZED = (ob.IS_FINALIZED == "Y") ? "Y" : "N";

                    ob.TABLE_NO = (dr["TABLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TABLE_NO"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public GMT_CUT_TBL_TGTModel Select(long ID)
        {
            string sp = "Select_GMT_CUT_TBL_TGT";
            try
            {
                var ob = new GMT_CUT_TBL_TGTModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pGMT_CUT_TBL_TGT_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.GMT_CUT_TBL_TGT_ID = (dr["GMT_CUT_TBL_TGT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_CUT_TBL_TGT_ID"]);
                    ob.GMT_PROD_PLN_CLNDR_ID = (dr["GMT_PROD_PLN_CLNDR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_PROD_PLN_CLNDR_ID"]);
                    ob.GMT_CUT_TABLE_ID = (dr["GMT_CUT_TABLE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_CUT_TABLE_ID"]);
                    ob.PLAN_MP_QTY = (dr["PLAN_MP_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PLAN_MP_QTY"]);
                    ob.TGT_WORK_HR = (dr["TGT_WORK_HR"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["TGT_WORK_HR"]);
                    ob.TARGET_QTY = (dr["TARGET_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["TARGET_QTY"]);
                    ob.IS_FINALIZED = (dr["IS_FINALIZED"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_FINALIZED"]);

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