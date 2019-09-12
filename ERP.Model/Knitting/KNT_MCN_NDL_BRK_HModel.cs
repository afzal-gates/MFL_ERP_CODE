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
    public class KNT_MCN_NDL_BRK_HModel
    {
        public Int64 KNT_MCN_NDL_BRK_H_ID { get; set; }
        public Int64 HR_COMPANY_ID { get; set; }
        public Int64 HR_OFFICE_ID { get; set; }
        public Int64 HR_SCHEDULE_H_ID { get; set; }
        public string TRAN_REF_NO { get; set; }
        public DateTime TRAN_REF_DT { get; set; }
        public Int64 REPORT_BY { get; set; }
        public string IS_FINALIZED { get; set; }
        public string REMARKS { get; set; }
        public string COMP_NAME_EN { get; set; }
        public string OFFICE_NAME_EN { get; set; }
        public string SCHEDULE_NAME_EN { get; set; }
        public decimal COST_PRICE { get; set; }
        public long BRK_QTY_PCS { get; set; }
        public string MCN_NDL_BRK_DTL_XML { get; set; }


        public string BatchSave()
        {
            const string sp = "pkg_knt_mcn_needle.knt_mcn_ndl_brk_batch_save";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_MCN_NDL_BRK_H_ID", Value = ob.KNT_MCN_NDL_BRK_H_ID},
                     new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                     new CommandParameter() {ParameterName = "pHR_OFFICE_ID", Value = ob.HR_OFFICE_ID},
                     new CommandParameter() {ParameterName = "pHR_SCHEDULE_H_ID", Value = ob.HR_SCHEDULE_H_ID},
                     new CommandParameter() {ParameterName = "pTRAN_REF_NO", Value = ob.TRAN_REF_NO},
                     new CommandParameter() {ParameterName = "pTRAN_REF_DT", Value = ob.TRAN_REF_DT},
                     new CommandParameter() {ParameterName = "pREPORT_BY", Value = ob.REPORT_BY},
                     new CommandParameter() {ParameterName = "pIS_FINALIZED", Value = ob.IS_FINALIZED},
                     new CommandParameter() {ParameterName = "pREMARKS", Value = ob.REMARKS}, 
                     new CommandParameter() {ParameterName = "pMCN_NDL_BRK_DTL_XML", Value = ob.MCN_NDL_BRK_DTL_XML.Replace("null", "")},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     
                     new CommandParameter() {ParameterName = "pOption", Value = 1000},
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


        public string BatchRevise()
        {
            const string sp = "pkg_knt_mcn_needle.knt_mcn_ndl_brk_batch_save";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_MCN_NDL_BRK_H_ID", Value = ob.KNT_MCN_NDL_BRK_H_ID},
                     new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                     new CommandParameter() {ParameterName = "pHR_OFFICE_ID", Value = ob.HR_OFFICE_ID},
                     new CommandParameter() {ParameterName = "pHR_SCHEDULE_H_ID", Value = ob.HR_SCHEDULE_H_ID},
                     new CommandParameter() {ParameterName = "pTRAN_REF_NO", Value = ob.TRAN_REF_NO},
                     new CommandParameter() {ParameterName = "pTRAN_REF_DT", Value = ob.TRAN_REF_DT},
                     new CommandParameter() {ParameterName = "pREPORT_BY", Value = ob.REPORT_BY},
                     new CommandParameter() {ParameterName = "pIS_FINALIZED", Value = ob.IS_FINALIZED},
                     new CommandParameter() {ParameterName = "pREMARKS", Value = ob.REMARKS}, 
                     new CommandParameter() {ParameterName = "pMCN_NDL_BRK_DTL_XML", Value = ob.MCN_NDL_BRK_DTL_XML.Replace("null", "")},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     
                     new CommandParameter() {ParameterName = "pOption", Value = 1000},
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

        public object GetNeedleBrokenList(Int64 pageNumber, Int64 pageSize)
        {
            string sp = "pkg_knt_mcn_needle.knt_mcn_ndl_brk_h_select";
            try
            {
                Int64 vTotalRec = 0;
                var obList = new List<KNT_MCN_NDL_BRK_HModel>();
                var obj = new RF_PAGERModel();

                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pageNumber", Value = pageNumber},
                     new CommandParameter() {ParameterName = "pageSize", Value = pageSize},
                     new CommandParameter() {ParameterName = "pSC_USER_ID", Value = HttpContext.Current.Session["multiScUserId"]},
                     
                     new CommandParameter() {ParameterName = "pOption", Value = 3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    KNT_MCN_NDL_BRK_HModel ob = new KNT_MCN_NDL_BRK_HModel();
                    ob.KNT_MCN_NDL_BRK_H_ID = (dr["KNT_MCN_NDL_BRK_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_MCN_NDL_BRK_H_ID"]);
                    //ob.HR_COMPANY_ID = (dr["HR_COMPANY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COMPANY_ID"]);
                    //ob.HR_OFFICE_ID = (dr["HR_OFFICE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_OFFICE_ID"]);
                    //ob.HR_SCHEDULE_H_ID = (dr["HR_SCHEDULE_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_SCHEDULE_H_ID"]);
                    ob.TRAN_REF_NO = (dr["TRAN_REF_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TRAN_REF_NO"]);
                    ob.TRAN_REF_DT = (dr["TRAN_REF_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["TRAN_REF_DT"]);
                    //ob.REPORT_BY = (dr["REPORT_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["REPORT_BY"]);
                    ob.IS_FINALIZED = (dr["IS_FINALIZED"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_FINALIZED"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                    ob.MC_TYPE_NAME_EN = (dr["MC_TYPE_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MC_TYPE_NAME_EN"]);
                    
                    ob.BRK_QTY_PCS = (dr["BRK_QTY_PCS"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["BRK_QTY_PCS"]);
                    ob.COST_PRICE = (dr["COST_PRICE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["COST_PRICE"]);

                    ob.COMP_NAME_EN = (dr["COMP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_NAME_EN"]);
                    ob.OFFICE_NAME_EN = (dr["OFFICE_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["OFFICE_NAME_EN"]);
                    ob.SCHEDULE_NAME_EN = (dr["SCHEDULE_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SCHEDULE_NAME_EN"]);

                    if (vTotalRec < 1)
                    {
                        vTotalRec = (dr["TOTAL_REC"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["TOTAL_REC"]);
                    }

                    obList.Add(ob);
                }

                obj.total = vTotalRec;
                obj.data = obList;
                return obj;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public KNT_MCN_NDL_BRK_HModel GetNeedleBrokenByHdrID(long pKNT_MCN_NDL_BRK_H_ID)
        {
            string sp = "pkg_knt_mcn_needle.knt_mcn_ndl_brk_h_select";
            try
            {
                var ob = new KNT_MCN_NDL_BRK_HModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_MCN_NDL_BRK_H_ID", Value = pKNT_MCN_NDL_BRK_H_ID},
                     new CommandParameter() {ParameterName = "pOption", Value = 3001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.KNT_MCN_NDL_BRK_H_ID = (dr["KNT_MCN_NDL_BRK_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_MCN_NDL_BRK_H_ID"]);
                    ob.HR_COMPANY_ID = (dr["HR_COMPANY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COMPANY_ID"]);
                    ob.HR_OFFICE_ID = (dr["HR_OFFICE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_OFFICE_ID"]);
                    ob.HR_SCHEDULE_H_ID = (dr["HR_SCHEDULE_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_SCHEDULE_H_ID"]);
                    ob.TRAN_REF_NO = (dr["TRAN_REF_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TRAN_REF_NO"]);
                    ob.TRAN_REF_DT = (dr["TRAN_REF_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["TRAN_REF_DT"]);
                    ob.REPORT_BY = (dr["REPORT_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["REPORT_BY"]);
                    ob.IS_FINALIZED = (dr["IS_FINALIZED"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_FINALIZED"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);                

                }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<KNT_MCN_NDL_BRK_DModel> GetNeedleBrokenDtlByID(long pKNT_MCN_NDL_BRK_H_ID)
        {
            string sp = "pkg_knt_mcn_needle.knt_mcn_ndl_brk_h_select";
            try
            {
                var obList = new List<KNT_MCN_NDL_BRK_DModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_MCN_NDL_BRK_H_ID", Value = pKNT_MCN_NDL_BRK_H_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3002},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    KNT_MCN_NDL_BRK_DModel ob = new KNT_MCN_NDL_BRK_DModel();
                    ob.KNT_MCN_NDL_BRK_D_ID = (dr["KNT_MCN_NDL_BRK_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_MCN_NDL_BRK_D_ID"]);
                    ob.KNT_MCN_NDL_BRK_H_ID = (dr["KNT_MCN_NDL_BRK_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_MCN_NDL_BRK_H_ID"]);
                    ob.INV_ITEM_ID = (dr["INV_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_ITEM_ID"]);
                    ob.KNT_MACHINE_ID = (dr["KNT_MACHINE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_MACHINE_ID"]);
                    ob.OPERATOR_ID = (dr["OPERATOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["OPERATOR_ID"]);
                    ob.BRK_QTY_PCS = (dr["BRK_QTY_PCS"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["BRK_QTY_PCS"]);
                    ob.COST_PRICE = (dr["COST_PRICE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["COST_PRICE"]);

                    ob.MC_DIA = (dr["MC_DIA"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MC_DIA"]);
                    ob.KNT_MACHINE_NO = (dr["KNT_MACHINE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["KNT_MACHINE_NO"]);
                    ob.EMP_FULL_NAME_EN = (dr["EMP_FULL_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EMP_FULL_NAME_EN"]);
                    ob.ITEM_NAME_EN = (dr["ITEM_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_NAME_EN"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object GetItemStockByID(Int64? pSCM_STORE_ID = null, Int64? pINV_ITEM_ID = null, Int64? pHR_OFFICE_ID = null)
        {
            string sp = "pkg_knt_mcn_needle.knt_mcn_ndl_brk_h_select";
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {                     
                     new CommandParameter() {ParameterName = "pSCM_STORE_ID", Value = pSCM_STORE_ID},                     
                     new CommandParameter() {ParameterName = "pINV_ITEM_ID", Value = pINV_ITEM_ID},
                     new CommandParameter() {ParameterName = "pHR_OFFICE_ID", Value = pHR_OFFICE_ID},
                     
                     new CommandParameter() {ParameterName = "pOption", Value = 3003},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    var ob = new
                    {
                        STOCK_QTY = (dr["STOCK_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["STOCK_QTY"]),                        
                    };

                    return ob;
                }
                return new { STOCK_QTY = 0 };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }




        public string MC_TYPE_NAME_EN { get; set; }
    }
}