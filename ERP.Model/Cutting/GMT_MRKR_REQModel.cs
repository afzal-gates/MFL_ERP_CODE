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
    public class GMT_MRKR_REQModel
    {
        public Int64 GMT_MRKR_REQ_ID { get; set; }
        public Int64 HR_COMPANY_ID { get; set; }
        public Int64 PROD_UNIT_ID { get; set; }
        public Int64? HR_DEPARTMENT_ID { get; set; }
        public Int64? RF_REQ_TYPE_ID { get; set; }
        public string MRKR_REQ_NO { get; set; }
        public DateTime MRKR_REQ_DT { get; set; }
        public Int64 MRKR_REQ_BY { get; set; }
        public Int64? MRKR_REQ_TO { get; set; }
        public Int64 GMT_CUT_TABLE_ID { get; set; }
        public Int64 GMT_MRKR_ID { get; set; }
        public Int64 MRKR_RQD_QTY { get; set; }
        public Int64? MAX_PLY_QTY { get; set; }
        public Int64 RF_ACTN_STATUS_ID { get; set; }
        public string REMARKS { get; set; }
        public string REQ_BY_NAME { get; set; }

        public Int64? MC_BYR_ACC_GRP_ID { get; set; }
        public Int64? MC_STYLE_H_EXT_ID { get; set; }
        public Int64? MC_ORDER_H_ID { get; set; }
        public Int64? GMT_COLOR_ID { get; set; }
        public Int64? HR_PROD_FLR_ID { get; set; }
        public string ORDER_NO { get; set; }
        public string STYLE_NO { get; set; }
        public string BYR_ACC_GRP_NAME_EN { get; set; }
        public string ACTN_STATUS_NAME { get; set; }
        public string MRKR_SH_DESC { get; set; }



        public string MrkrReqSave()
        {
            const string sp = "pkg_cut_marker.gmt_mrkr_req_save";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pGMT_MRKR_REQ_ID", Value = ob.GMT_MRKR_REQ_ID},
                     new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                     new CommandParameter() {ParameterName = "pPROD_UNIT_ID", Value = ob.PROD_UNIT_ID},
                     new CommandParameter() {ParameterName = "pHR_DEPARTMENT_ID", Value = ob.HR_DEPARTMENT_ID},
                     new CommandParameter() {ParameterName = "pRF_REQ_TYPE_ID", Value = ob.RF_REQ_TYPE_ID},
                     new CommandParameter() {ParameterName = "pMRKR_REQ_NO", Value = ob.MRKR_REQ_NO},
                     new CommandParameter() {ParameterName = "pMRKR_REQ_DT", Value = ob.MRKR_REQ_DT},
                     new CommandParameter() {ParameterName = "pMRKR_REQ_BY", Value = HttpContext.Current.Session["multiLoginEmpId"]},
                     new CommandParameter() {ParameterName = "pMRKR_REQ_TO", Value = ob.MRKR_REQ_TO},
                     new CommandParameter() {ParameterName = "pGMT_CUT_TABLE_ID", Value = ob.GMT_CUT_TABLE_ID},
                     new CommandParameter() {ParameterName = "pGMT_MRKR_ID", Value = ob.GMT_MRKR_ID},
                     new CommandParameter() {ParameterName = "pMRKR_RQD_QTY", Value = ob.MRKR_RQD_QTY},
                     new CommandParameter() {ParameterName = "pMAX_PLY_QTY", Value = ob.MAX_PLY_QTY},
                     new CommandParameter() {ParameterName = "pRF_ACTN_STATUS_ID", Value = ob.RF_ACTN_STATUS_ID},
                     new CommandParameter() {ParameterName = "pREMARKS", Value = ob.REMARKS},                     
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

        public string MrkrReqFinalize()
        {
            const string sp = "pkg_cut_marker.gmt_mrkr_req_save";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pGMT_MRKR_REQ_ID", Value = ob.GMT_MRKR_REQ_ID},
                     new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                     new CommandParameter() {ParameterName = "pPROD_UNIT_ID", Value = ob.PROD_UNIT_ID},
                     new CommandParameter() {ParameterName = "pHR_DEPARTMENT_ID", Value = ob.HR_DEPARTMENT_ID},
                     new CommandParameter() {ParameterName = "pRF_REQ_TYPE_ID", Value = ob.RF_REQ_TYPE_ID},
                     new CommandParameter() {ParameterName = "pMRKR_REQ_NO", Value = ob.MRKR_REQ_NO},
                     new CommandParameter() {ParameterName = "pMRKR_REQ_DT", Value = ob.MRKR_REQ_DT},
                     new CommandParameter() {ParameterName = "pMRKR_REQ_BY", Value = HttpContext.Current.Session["multiLoginEmpId"]},
                     new CommandParameter() {ParameterName = "pMRKR_REQ_TO", Value = ob.MRKR_REQ_TO},
                     new CommandParameter() {ParameterName = "pGMT_CUT_TABLE_ID", Value = ob.GMT_CUT_TABLE_ID},
                     new CommandParameter() {ParameterName = "pGMT_MRKR_ID", Value = ob.GMT_MRKR_ID},
                     new CommandParameter() {ParameterName = "pMRKR_RQD_QTY", Value = ob.MRKR_RQD_QTY},
                     new CommandParameter() {ParameterName = "pMAX_PLY_QTY", Value = ob.MAX_PLY_QTY},
                     new CommandParameter() {ParameterName = "pRF_ACTN_STATUS_ID", Value = ob.RF_ACTN_STATUS_ID},
                     new CommandParameter() {ParameterName = "pREMARKS", Value = ob.REMARKS},                     
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     
                     new CommandParameter() {ParameterName = "pOption", Value =1001},
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

        public object GetMrkrReqList(int pageNumber, int pageSize, Int64 pMC_BYR_ACC_GRP_ID, Int64? pMC_STYLE_H_EXT_ID = null)
        {
            string sp = "pkg_cut_marker.gmt_mrkr_req_select";
            try
            {
                Int64 vTotalRec = 0;
                var obList = new List<GMT_MRKR_REQModel>();
                var obj = new RF_PAGERModel();

                
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_BYR_ACC_GRP_ID", Value =pMC_BYR_ACC_GRP_ID},
                     new CommandParameter() {ParameterName = "pMC_STYLE_H_EXT_ID", Value =pMC_STYLE_H_EXT_ID},

                     new CommandParameter() {ParameterName = "pageNumber", Value =pageNumber},
                     new CommandParameter() {ParameterName = "pageSize", Value =pageSize},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    GMT_MRKR_REQModel ob = new GMT_MRKR_REQModel();
                    ob.GMT_MRKR_REQ_ID = (dr["GMT_MRKR_REQ_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_MRKR_REQ_ID"]);
                    ob.HR_COMPANY_ID = (dr["HR_COMPANY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COMPANY_ID"]);
                    ob.PROD_UNIT_ID = (dr["PROD_UNIT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PROD_UNIT_ID"]);
                    ob.HR_DEPARTMENT_ID = (dr["HR_DEPARTMENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_DEPARTMENT_ID"]);
                    ob.RF_REQ_TYPE_ID = (dr["RF_REQ_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_REQ_TYPE_ID"]);
                    ob.MRKR_REQ_NO = (dr["MRKR_REQ_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MRKR_REQ_NO"]);
                    ob.MRKR_REQ_DT = (dr["MRKR_REQ_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["MRKR_REQ_DT"]);
                    ob.MRKR_REQ_BY = (dr["MRKR_REQ_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MRKR_REQ_BY"]);
                    ob.MRKR_REQ_TO = (dr["MRKR_REQ_TO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MRKR_REQ_TO"]);
                    ob.GMT_CUT_TABLE_ID = (dr["GMT_CUT_TABLE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_CUT_TABLE_ID"]);
                    ob.GMT_MRKR_ID = (dr["GMT_MRKR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_MRKR_ID"]);
                    ob.MRKR_RQD_QTY = (dr["MRKR_RQD_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MRKR_RQD_QTY"]);

                    ob.MC_BYR_ACC_GRP_ID = (dr["MC_BYR_ACC_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BYR_ACC_GRP_ID"]);
                    ob.MC_STYLE_H_EXT_ID = (dr["MC_STYLE_H_EXT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_H_EXT_ID"]);
                    ob.MC_ORDER_H_ID = (dr["MC_ORDER_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_ORDER_H_ID"]);
                    ob.GMT_COLOR_ID = (dr["GMT_COLOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_COLOR_ID"]);

                    if (dr["MAX_PLY_QTY"] != DBNull.Value)
                        ob.MAX_PLY_QTY = (dr["MAX_PLY_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MAX_PLY_QTY"]);

                    ob.RF_ACTN_STATUS_ID = (dr["RF_ACTN_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_ACTN_STATUS_ID"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);

                    ob.BYR_ACC_GRP_NAME_EN = (dr["BYR_ACC_GRP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BYR_ACC_GRP_NAME_EN"]);
                    ob.STYLE_NO = (dr["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO"]);
                    ob.ORDER_NO = (dr["ORDER_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_NO"]);

                    ob.MRKR_SH_DESC = (dr["MRKR_SH_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MRKR_SH_DESC"]);
                    ob.ACTN_STATUS_NAME = (dr["ACTN_STATUS_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ACTN_STATUS_NAME"]);

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

        public GMT_MRKR_REQModel GetMrkrReqHdr(long pGMT_MRKR_REQ_ID)
        {
            string sp = "pkg_cut_marker.gmt_mrkr_req_select";
            try
            {
                var ob = new GMT_MRKR_REQModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pGMT_MRKR_REQ_ID", Value =pGMT_MRKR_REQ_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.GMT_MRKR_REQ_ID = (dr["GMT_MRKR_REQ_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_MRKR_REQ_ID"]);
                    ob.HR_COMPANY_ID = (dr["HR_COMPANY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COMPANY_ID"]);
                    ob.PROD_UNIT_ID = (dr["PROD_UNIT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PROD_UNIT_ID"]);
                    ob.HR_DEPARTMENT_ID = (dr["HR_DEPARTMENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_DEPARTMENT_ID"]);
                    ob.RF_REQ_TYPE_ID = (dr["RF_REQ_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_REQ_TYPE_ID"]);
                    ob.MRKR_REQ_NO = (dr["MRKR_REQ_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MRKR_REQ_NO"]);
                    ob.MRKR_REQ_DT = (dr["MRKR_REQ_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["MRKR_REQ_DT"]);
                    ob.MRKR_REQ_BY = (dr["MRKR_REQ_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MRKR_REQ_BY"]);
                    ob.MRKR_REQ_TO = (dr["MRKR_REQ_TO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MRKR_REQ_TO"]);
                    ob.GMT_CUT_TABLE_ID = (dr["GMT_CUT_TABLE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_CUT_TABLE_ID"]);
                    ob.GMT_MRKR_ID = (dr["GMT_MRKR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_MRKR_ID"]);
                    ob.MRKR_RQD_QTY = (dr["MRKR_RQD_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MRKR_RQD_QTY"]);

                    if (dr["MAX_PLY_QTY"] != DBNull.Value)
                        ob.MAX_PLY_QTY = (dr["MAX_PLY_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MAX_PLY_QTY"]);

                    ob.RF_ACTN_STATUS_ID = (dr["RF_ACTN_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_ACTN_STATUS_ID"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);

                    ob.REQ_BY_NAME = (dr["REQ_BY_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REQ_BY_NAME"]);

                    ob.MC_BYR_ACC_GRP_ID = (dr["MC_BYR_ACC_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BYR_ACC_GRP_ID"]);
                    ob.MC_STYLE_H_EXT_ID = (dr["MC_STYLE_H_EXT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_H_EXT_ID"]);
                    ob.MC_ORDER_H_ID = (dr["MC_ORDER_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_ORDER_H_ID"]);
                    ob.GMT_COLOR_ID = (dr["GMT_COLOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_COLOR_ID"]);
                    ob.HR_PROD_FLR_ID = (dr["HR_PROD_FLR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_PROD_FLR_ID"]);

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