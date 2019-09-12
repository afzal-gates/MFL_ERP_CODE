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
    public class DYE_BT_DC_REQ_D1Model
    {
        public Int64 DYE_BT_DC_REQ_D1_ID { get; set; }
        public Int64 DYE_STR_REQ_H_ID { get; set; }
        public Int64 DYE_BT_CARD_H_ID { get; set; }
        public Int64 DYE_DOSE_TMPLT_H_ID { get; set; }

        public Int64 BATCH_QTY { get; set; }
        public Int64 ADDL_QTY { get; set; }
        public Int64 QTY_MOU_ID { get; set; }
        public string REMARKS { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }

        public string Save()
        {
            const string sp = "SP_DYE_BT_DC_REQ_D1";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_BT_DC_REQ_D1_ID", Value = ob.DYE_BT_DC_REQ_D1_ID},
                     new CommandParameter() {ParameterName = "pDYE_STR_REQ_H_ID", Value = ob.DYE_STR_REQ_H_ID},
                     new CommandParameter() {ParameterName = "pDYE_BT_CARD_H_ID", Value = ob.DYE_BT_CARD_H_ID},
                     new CommandParameter() {ParameterName = "pBATCH_QTY", Value = ob.BATCH_QTY},
                     new CommandParameter() {ParameterName = "pADDL_QTY", Value = ob.ADDL_QTY},
                     new CommandParameter() {ParameterName = "pQTY_MOU_ID", Value = ob.QTY_MOU_ID},
                     new CommandParameter() {ParameterName = "pREMARKS", Value = ob.REMARKS},
                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = ob.CREATED_BY},
                     new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = ob.LAST_UPDATED_BY},
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

        public string Update()
        {
            const string sp = "SP_DYE_BT_DC_REQ_D1";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_BT_DC_REQ_D1_ID", Value = ob.DYE_BT_DC_REQ_D1_ID},
                     new CommandParameter() {ParameterName = "pDYE_STR_REQ_H_ID", Value = ob.DYE_STR_REQ_H_ID},
                     new CommandParameter() {ParameterName = "pDYE_BT_CARD_H_ID", Value = ob.DYE_BT_CARD_H_ID},
                     new CommandParameter() {ParameterName = "pBATCH_QTY", Value = ob.BATCH_QTY},
                     new CommandParameter() {ParameterName = "pADDL_QTY", Value = ob.ADDL_QTY},
                     new CommandParameter() {ParameterName = "pQTY_MOU_ID", Value = ob.QTY_MOU_ID},
                     new CommandParameter() {ParameterName = "pREMARKS", Value = ob.REMARKS},
                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = ob.CREATED_BY},
                     new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = ob.LAST_UPDATED_BY},
                     new CommandParameter() {ParameterName = "pOption", Value =2000},
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

        public string Delete()
        {
            const string sp = "SP_DYE_BT_DC_REQ_D1";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_BT_DC_REQ_D1_ID", Value = ob.DYE_BT_DC_REQ_D1_ID},
                     new CommandParameter() {ParameterName = "pDYE_STR_REQ_H_ID", Value = ob.DYE_STR_REQ_H_ID},
                     new CommandParameter() {ParameterName = "pDYE_BT_CARD_H_ID", Value = ob.DYE_BT_CARD_H_ID},
                     new CommandParameter() {ParameterName = "pBATCH_QTY", Value = ob.BATCH_QTY},
                     new CommandParameter() {ParameterName = "pADDL_QTY", Value = ob.ADDL_QTY},
                     new CommandParameter() {ParameterName = "pQTY_MOU_ID", Value = ob.QTY_MOU_ID},
                     new CommandParameter() {ParameterName = "pREMARKS", Value = ob.REMARKS},
                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = ob.CREATED_BY},
                     new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = ob.LAST_UPDATED_BY},
                     new CommandParameter() {ParameterName = "pOption", Value =4000},
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

        public List<DYE_BT_DC_REQ_D1Model> SelectAll()
        {
            string sp = "Select_DYE_BT_DC_REQ_D1";
            try
            {
                var obList = new List<DYE_BT_DC_REQ_D1Model>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_STR_REQ_H_ID", Value = 0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DYE_BT_DC_REQ_D1Model ob = new DYE_BT_DC_REQ_D1Model();
                    ob.DYE_BT_DC_REQ_D1_ID = (dr["DYE_BT_DC_REQ_D1_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_BT_DC_REQ_D1_ID"]);
                    ob.DYE_STR_REQ_H_ID = (dr["DYE_STR_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_STR_REQ_H_ID"]);
                    ob.DYE_BT_CARD_H_ID = (dr["DYE_BT_CARD_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_BT_CARD_H_ID"]);
                    ob.DYE_DOSE_TMPLT_H_ID = (dr["DYE_DOSE_TMPLT_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_DOSE_TMPLT_H_ID"]);
                    ob.BATCH_QTY = (dr["BATCH_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["BATCH_QTY"]);
                    ob.ADDL_QTY = (dr["ADDL_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ADDL_QTY"]);
                    ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DYE_BT_DC_REQ_D1Model> Select(Int64? ID = null, string pLOT_ID = null, string pON_LINE_QC_ID = null, string pRIB_ID = null)
        {
            string sp = "PKG_DYE_BATCH_PROGRAM.DYE_BT_DC_REQ_D1_SELECT";
            try
            {
                var obList = new List<DYE_BT_DC_REQ_D1Model>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_STR_REQ_H_ID", Value = ID},
                     new CommandParameter() {ParameterName = "pDF_BT_SUB_LOT_LST", Value = pLOT_ID},
                     new CommandParameter() {ParameterName = "pDF_PROC_QC_RPT_H_LST", Value = pON_LINE_QC_ID},
                     new CommandParameter() {ParameterName = "pDF_RIB_SHADE_RPT_H_LST", Value = pRIB_ID},
                     
                     new CommandParameter() {ParameterName = "pOption", Value =3001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DYE_BT_DC_REQ_D1Model ob = new DYE_BT_DC_REQ_D1Model();
                    ob.DYE_BT_DC_REQ_D1_ID = (dr["DYE_BT_DC_REQ_D1_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_BT_DC_REQ_D1_ID"]);
                    ob.DYE_STR_REQ_H_ID = (dr["DYE_STR_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_STR_REQ_H_ID"]);
                    ob.DYE_BT_CARD_H_ID = (dr["DYE_BT_CARD_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_BT_CARD_H_ID"]);
                    ob.DYE_DOSE_TMPLT_H_ID = (dr["DYE_DOSE_TMPLT_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_DOSE_TMPLT_H_ID"]);
                    ob.DYE_RE_PROC_TYPE_ID = (dr["DYE_RE_PROC_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_RE_PROC_TYPE_ID"]);

                    ob.SCM_STORE_ID = (dr["REQ_STORE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["REQ_STORE_ID"]);
                    ob.REQ_STORE_ID = (dr["REQ_STORE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["REQ_STORE_ID"]);
                    ob.ISS_STORE_ID = (dr["ISS_STORE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ISS_STORE_ID"]);

                    ob.BATCH_QTY = (dr["BATCH_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["BATCH_QTY"]);
                    ob.ADDL_QTY = (dr["ADDL_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ADDL_QTY"]);
                    ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);

                    ob.MC_LD_RECIPE_H_ID = (dr["MC_LD_RECIPE_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_LD_RECIPE_H_ID"]);
                    ob.LQR_RATIO = (dr["LQR_RATIO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LQR_RATIO"]);
                    ob.DYE_LOT_NO = (dr["DYE_LOT_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DYE_LOT_NO"]);
                    ob.DYE_BATCH_NO = (dr["DYE_BATCH_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DYE_BATCH_NO"]);
                    ob.BATCH_REQ_DT = (dr["BATCH_REQ_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["BATCH_REQ_DT"]);
                    ob.ACT_BATCH_QTY = (dr["ACT_BATCH_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ACT_BATCH_QTY"]);
                    ob.MERGE_BT_CRD_ID = (dr["MERGE_BT_CRD_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MERGE_BT_CRD_ID"]);
                    ob.ACT_LOAD_TIME = (dr["ACT_LOAD_TIME"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["ACT_LOAD_TIME"]);
                    ob.ACT_UN_LOAD_TIME = (dr["ACT_UN_LOAD_TIME"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["ACT_UN_LOAD_TIME"]);
                    ob.OPERATOR_ID = (dr["OPERATOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["OPERATOR_ID"]);
                    ob.LK_SHIFT_TYPE_ID = (dr["LK_SHIFT_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_SHIFT_TYPE_ID"]);
                    ob.BT_FLR_TEMP = (dr["BT_FLR_TEMP"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BT_FLR_TEMP"]);
                    ob.DYE_BT_STS_TYPE_ID = (dr["DYE_BT_STS_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_BT_STS_TYPE_ID"]);
                    ob.DYE_MACHINE_ID = (dr["DYE_MACHINE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_MACHINE_ID"]);

                    ob.MC_COLOR_GRP_ID = (dr["MC_COLOR_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_COLOR_GRP_ID"]);
                    ob.DYE_MACHINE_NO = (dr["DYE_MACHINE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DYE_MACHINE_NO"]);
                    ob.BUYER_NAME_EN = (dr["BUYER_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BUYER_NAME_EN"]);
                    ob.STYLE_NO = (dr["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO"]);
                    ob.ORDER_NO = (dr["ORDER_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_NO"]);
                    ob.COLOR_NAME_EN = (dr["COLOR_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_NAME_EN"]);
                    ob.LD_RECIPE_NO = (dr["LD_RECIPE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LD_RECIPE_NO"]);
                    ob.COLOR_GRP_NAME_EN = (dr["COLOR_GRP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_GRP_NAME_EN"]);
                    ob.STR_REQ_NO = (dr["STR_REQ_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STR_REQ_NO"]);
                    ob.STR_REQ_DT = (dr["STR_REQ_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["STR_REQ_DT"]);

                    if (dr["DYE_BT_DC_ISS_H_ID"] != DBNull.Value)
                        ob.DYE_BT_DC_ISS_H_ID = Convert.ToInt64(dr["DYE_BT_DC_ISS_H_ID"]);

                    if (dr["DYE_BT_DC_ISS_H_ID"] != DBNull.Value)
                        ob.DYE_BT_DC_ISS_H_ID = Convert.ToInt64(dr["DYE_BT_DC_ISS_H_ID"]);

                    if (dr["HR_COMPANY_ID"] != DBNull.Value)
                        ob.HR_COMPANY_ID = Convert.ToInt64(dr["HR_COMPANY_ID"]);

                    if (dr["ISS_REF_NO"] != DBNull.Value)
                        ob.ISS_REF_NO = Convert.ToString(dr["ISS_REF_NO"]);

                    if (dr["DYE_MTHD_NAME"] != DBNull.Value)
                        ob.DYE_MTHD_NAME = Convert.ToString(dr["DYE_MTHD_NAME"]);

                    if (dr["ACTN_ROLE_FLAG"] != DBNull.Value)
                        ob.ACTN_ROLE_FLAG = Convert.ToString(dr["ACTN_ROLE_FLAG"]);

                    if (dr["DF_BT_SUB_LOT_ID"] != DBNull.Value)
                        ob.DF_BT_SUB_LOT_ID = Convert.ToInt64(dr["DF_BT_SUB_LOT_ID"]);

                    ob.RF_ACTN_STATUS_ID = (dr["RF_ACTN_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_ACTN_STATUS_ID"]);
                    ob.RF_REQ_TYPE_ID = (dr["RF_REQ_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_REQ_TYPE_ID"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DYE_BT_DC_REQ_D1Model> GetDCBatchProgramAdditionRequisitionInfo(Int64? ID)
        {
            string sp = "PKG_DYE_BATCH_PROGRAM.DYE_BT_DC_REQ_D1_SELECT";
            try
            {
                var obList = new List<DYE_BT_DC_REQ_D1Model>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_STR_REQ_H_ID", Value = ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3002},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DYE_BT_DC_REQ_D1Model ob = new DYE_BT_DC_REQ_D1Model();
                    ob.DYE_BT_DC_REQ_D1_ID = (dr["DYE_BT_DC_REQ_D1_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_BT_DC_REQ_D1_ID"]);
                    ob.DYE_STR_REQ_H_ID = (dr["DYE_STR_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_STR_REQ_H_ID"]);
                    ob.DYE_BT_CARD_H_ID = (dr["DYE_BT_CARD_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_BT_CARD_H_ID"]);
                    ob.DYE_DOSE_TMPLT_H_ID = (dr["DYE_DOSE_TMPLT_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_DOSE_TMPLT_H_ID"]);
                    ob.DYE_RE_PROC_TYPE_ID = (dr["DYE_RE_PROC_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_RE_PROC_TYPE_ID"]);

                    ob.BATCH_QTY = (dr["BATCH_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["BATCH_QTY"]);
                    ob.ADDL_QTY = (dr["ADDL_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ADDL_QTY"]);
                    ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);

                    ob.MC_LD_RECIPE_H_ID = (dr["MC_LD_RECIPE_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_LD_RECIPE_H_ID"]);
                    ob.LQR_RATIO = (dr["LQR_RATIO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LQR_RATIO"]);
                    ob.DYE_LOT_NO = (dr["DYE_LOT_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DYE_LOT_NO"]);
                    ob.DYE_BATCH_NO = (dr["DYE_BATCH_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DYE_BATCH_NO"]);
                    ob.BATCH_REQ_DT = (dr["BATCH_REQ_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["BATCH_REQ_DT"]);
                    ob.ACT_BATCH_QTY = (dr["ACT_BATCH_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ACT_BATCH_QTY"]);
                    ob.MERGE_BT_CRD_ID = (dr["MERGE_BT_CRD_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MERGE_BT_CRD_ID"]);
                    ob.ACT_LOAD_TIME = (dr["ACT_LOAD_TIME"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["ACT_LOAD_TIME"]);
                    ob.ACT_UN_LOAD_TIME = (dr["ACT_UN_LOAD_TIME"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["ACT_UN_LOAD_TIME"]);
                    ob.OPERATOR_ID = (dr["OPERATOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["OPERATOR_ID"]);
                    ob.LK_SHIFT_TYPE_ID = (dr["LK_SHIFT_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_SHIFT_TYPE_ID"]);
                    ob.BT_FLR_TEMP = (dr["BT_FLR_TEMP"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BT_FLR_TEMP"]);
                    ob.DYE_BT_STS_TYPE_ID = (dr["DYE_BT_STS_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_BT_STS_TYPE_ID"]);
                    ob.DYE_MACHINE_ID = (dr["DYE_MACHINE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_MACHINE_ID"]);

                    ob.MC_COLOR_GRP_ID = (dr["MC_COLOR_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_COLOR_GRP_ID"]);
                    ob.DYE_MACHINE_NO = (dr["DYE_MACHINE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DYE_MACHINE_NO"]);
                    ob.BUYER_NAME_EN = (dr["BUYER_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BUYER_NAME_EN"]);
                    ob.STYLE_NO = (dr["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO"]);
                    ob.ORDER_NO = (dr["ORDER_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_NO"]);
                    ob.COLOR_NAME_EN = (dr["COLOR_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_NAME_EN"]);
                    ob.LD_RECIPE_NO = (dr["LD_RECIPE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LD_RECIPE_NO"]);
                    ob.COLOR_GRP_NAME_EN = (dr["COLOR_GRP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_GRP_NAME_EN"]);
                    ob.STR_REQ_NO = (dr["STR_REQ_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STR_REQ_NO"]);
                    ob.STR_REQ_DT = (dr["STR_REQ_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["STR_REQ_DT"]);

                    if (dr["DYE_BT_DC_ISS_H_ID"] != DBNull.Value)
                        ob.DYE_BT_DC_ISS_H_ID = Convert.ToInt64(dr["DYE_BT_DC_ISS_H_ID"]);

                    if (dr["DYE_BT_DC_ISS_H_ID"] != DBNull.Value)
                        ob.DYE_BT_DC_ISS_H_ID = Convert.ToInt64(dr["DYE_BT_DC_ISS_H_ID"]);

                    if (dr["ISS_REF_NO"] != DBNull.Value)
                        ob.ISS_REF_NO = Convert.ToString(dr["ISS_REF_NO"]);

                    if (dr["DYE_MTHD_NAME"] != DBNull.Value)
                        ob.DYE_MTHD_NAME = Convert.ToString(dr["DYE_MTHD_NAME"]);

                    if (dr["HR_COMPANY_ID"] != DBNull.Value)
                        ob.HR_COMPANY_ID = Convert.ToInt64(dr["HR_COMPANY_ID"]);

                    ob.SCM_STORE_ID = (dr["REQ_STORE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["REQ_STORE_ID"]);
                    ob.REQ_STORE_ID = (dr["REQ_STORE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["REQ_STORE_ID"]);
                    ob.ISS_STORE_ID = (dr["ISS_STORE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ISS_STORE_ID"]);

                    ob.RF_ACTN_STATUS_ID = (dr["RF_ACTN_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_ACTN_STATUS_ID"]);
                    ob.RF_REQ_TYPE_ID = (dr["RF_REQ_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_REQ_TYPE_ID"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<DYE_BT_DC_REQ_D1Model> GetBatchSubLotByID(string pDYE_BATCH_NO, Int64? pLK_DIA_TYPE_ID = null)
        {
            string sp = "PKG_DYE_BATCH_PROGRAM.DYE_BT_CARD_SUB_LOT_SELECT";
            try
            {
                var obList = new List<DYE_BT_DC_REQ_D1Model>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_BATCH_NO", Value = pDYE_BATCH_NO},
                     new CommandParameter() {ParameterName = "pLK_DIA_TYPE_ID", Value = pLK_DIA_TYPE_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DYE_BT_DC_REQ_D1Model ob = new DYE_BT_DC_REQ_D1Model();
                    ob.DYE_STR_REQ_H_ID = (dr["DYE_STR_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_STR_REQ_H_ID"]);
                    ob.DYE_LOT_NO = (dr["DYE_LOT_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DYE_LOT_NO"]);
                    //ob.DYE_BATCH_NO = (dr["DYE_BATCH_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DYE_BATCH_NO"]);
                    ob.DYE_BATCH_NO = pDYE_BATCH_NO;
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<DYE_BT_DC_REQ_D1Model> SelectBatchWaitingList()
        {
            string sp = "PKG_DYE_BATCH_PROGRAM.DYE_BT_DC_REQ_D1_SELECT";
            try
            {
                var obList = new List<DYE_BT_DC_REQ_D1Model>();

                Int64 mod = Convert.ToInt64(HttpContext.Current.Session["multiLoginTime"].ToString()) % 30;
                Int64 cur = Convert.ToInt64(DateTime.Now.ToString("yyyyMMddHHmm")) % 30;
                if (mod == cur)
                {
                    OraDatabase db = new OraDatabase();
                    var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                    {
                         new CommandParameter() {ParameterName = "pUSER_ID", Value = HttpContext.Current.Session["multiScUserId"]},
                         new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = HttpContext.Current.Session["multiLoginEmpCompId"]},
                     
                         new CommandParameter() {ParameterName = "pOption", Value =3003},
                         new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                     }, sp);
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        DYE_BT_DC_REQ_D1Model ob = new DYE_BT_DC_REQ_D1Model();
                        ob.BATCH_QTY = (dr["BATCH_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["BATCH_QTY"]);
                        ob.DYE_BATCH_NO = (dr["DYE_BATCH_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DYE_BATCH_NO"]);
                        ob.IS_LOCKED = (dr["IS_LOCKED"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_LOCKED"]);
                        ob.DYE_LOT_NO = (dr["DYE_LOT_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DYE_LOT_NO"]);
                        ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                        ob.BATCH_LOCK_TIME = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900").ToString() : Convert.ToDateTime(dr["CREATION_DATE"]).ToString("dd-MM-yyyy hh:mm tt");
                        obList.Add(ob);
                    }
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public long MC_LD_RECIPE_H_ID { get; set; }

        public long LQR_RATIO { get; set; }

        public string DYE_LOT_NO { get; set; }

        public string DYE_BATCH_NO { get; set; }

        public DateTime BATCH_REQ_DT { get; set; }

        public long ACT_BATCH_QTY { get; set; }

        public long MERGE_BT_CRD_ID { get; set; }

        public DateTime ACT_LOAD_TIME { get; set; }

        public DateTime ACT_UN_LOAD_TIME { get; set; }

        public long OPERATOR_ID { get; set; }

        public long LK_SHIFT_TYPE_ID { get; set; }

        public string BT_FLR_TEMP { get; set; }

        public long DYE_BT_STS_TYPE_ID { get; set; }

        public long MC_COLOR_GRP_ID { get; set; }

        public string DYE_MACHINE_NO { get; set; }

        public string BUYER_NAME_EN { get; set; }

        public string STYLE_NO { get; set; }

        public string ORDER_NO { get; set; }

        public string COLOR_NAME_EN { get; set; }

        public string LD_RECIPE_NO { get; set; }

        public string COLOR_GRP_NAME_EN { get; set; }

        public string STR_REQ_NO { get; set; }

        public DateTime STR_REQ_DT { get; set; }

        public long RF_ACTN_STATUS_ID { get; set; }

        public long RF_REQ_TYPE_ID { get; set; }

        public long DYE_BT_DC_ISS_H_ID { get; set; }

        public string ISS_REF_NO { get; set; }
        public string DYE_MTHD_NAME { get; set; }

        public long DYE_MACHINE_ID { get; set; }

        public Int64? DYE_RE_PROC_TYPE_ID { get; set; }



        public DateTime CHK_ROLL_DT { get; set; }

        public string FAB_TYPE_NAME { get; set; }

        public long RF_FAB_TYPE_ID { get; set; }

        public long FAB_COLOR_ID { get; set; }

        public long MC_FAB_PROD_ORD_H_ID { get; set; }

        public long HR_COMPANY_ID { get; set; }

        public long SCM_STORE_ID { get; set; }

        public long REQ_STORE_ID { get; set; }

        public long ISS_STORE_ID { get; set; }

        public DateTime? CK_ROLL_RCV_DT { get; set; }
        public DateTime? CK_ROLL_FIN_DT { get; set; }
        public DateTime? UN_HOLD_DT { get; set; }


        public long LK_CHQ_RL_STS_ID { get; set; }

        public string ACTN_ROLE_FLAG { get; set; }

        public long DF_BT_SUB_LOT_ID { get; set; }

        public string IS_FINALIZED { get; set; }

        public string DF_PROC_TYP_LST { get; set; }

        public string SPECIAL_INSTRUCTION { get; set; }

        public long DYE_BATCH_PLAN_ID { get; set; }


        public string DIA_TYPE_NAME { get; set; }

        public string BT_STS_TYP_NAME { get; set; }

        public long LAST_DF_PROC_TYPE_ID { get; set; }

        public long RP_SL_NO { get; set; }

        public string BATCH_LOCK_TIME { get; set; }

        public string IS_LOCKED { get; set; }
    }
}