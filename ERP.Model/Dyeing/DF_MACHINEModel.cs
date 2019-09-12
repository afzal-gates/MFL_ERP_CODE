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
    public class DF_MACHINEModel
    {
        public Int64 DF_MACHINE_ID { get; set; }
        public string DF_MACHINE_NO { get; set; }
        public string DF_MC_NAME { get; set; }
        public Int64 DF_MC_TYPE_ID { get; set; }
        public Int64 RF_BRAND_ID { get; set; }
        public string MODEL_NO { get; set; }
        public Int64 C_ORIGIN_ID { get; set; }
        public Decimal D_PRD_CAPACITY { get; set; }
        public Int64 CAP_MOU_ID { get; set; }
        public Decimal PCT_EFFCNCY { get; set; }
        public string MC_SERIAL_NO { get; set; }
        public Int64 MFG_YEAR { get; set; }
        public Int64 LK_MAC_STATUS_ID { get; set; }
        public Int64 MC_SPEED { get; set; }
        public Int64 RF_LOCATION_ID { get; set; }
        public Int64 RF_MAC_PROD_STS_ID { get; set; }
        public DateTime ACTN_TIME { get; set; }
        public string MC_IMAGE { get; set; }
        //public HttpPostedFileBase MC_IMAGE_FILE { get; set; }    
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }
        public Int64? DF_PROC_TYPE_ID { get; set; }
        public Int64? HR_PROD_FLR_ID { get; set; }


        public string Save()
        {
            const string sp = "PKG_DYE_COMMON.DF_MACHINE_INSERT";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_MACHINE_ID", Value = ob.DF_MACHINE_ID},
                     new CommandParameter() {ParameterName = "pDF_MACHINE_NO", Value = ob.DF_MACHINE_NO},
                     new CommandParameter() {ParameterName = "pDF_MC_NAME", Value = ob.DF_MC_NAME},
                     new CommandParameter() {ParameterName = "pDF_MC_TYPE_ID", Value = ob.DF_MC_TYPE_ID},
                     new CommandParameter() {ParameterName = "pRF_BRAND_ID", Value = ob.RF_BRAND_ID},
                     new CommandParameter() {ParameterName = "pMODEL_NO", Value = ob.MODEL_NO},
                     new CommandParameter() {ParameterName = "pC_ORIGIN_ID", Value = ob.C_ORIGIN_ID},
                     new CommandParameter() {ParameterName = "pD_PRD_CAPACITY", Value = ob.D_PRD_CAPACITY},
                     new CommandParameter() {ParameterName = "pCAP_MOU_ID", Value = ob.CAP_MOU_ID},
                     new CommandParameter() {ParameterName = "pPCT_EFFCNCY", Value = ob.PCT_EFFCNCY},
                     new CommandParameter() {ParameterName = "pMC_SERIAL_NO", Value = ob.MC_SERIAL_NO},
                     new CommandParameter() {ParameterName = "pMFG_YEAR", Value = ob.MFG_YEAR},
                     new CommandParameter() {ParameterName = "pLK_MAC_STATUS_ID", Value = ob.LK_MAC_STATUS_ID},
                     new CommandParameter() {ParameterName = "pMC_SPEED", Value = ob.MC_SPEED},
                     new CommandParameter() {ParameterName = "pRF_LOCATION_ID", Value = ob.RF_LOCATION_ID},
                     new CommandParameter() {ParameterName = "pRF_MAC_PROD_STS_ID", Value = ob.RF_MAC_PROD_STS_ID},
                     new CommandParameter() {ParameterName = "pACTN_TIME", Value = ob.ACTN_TIME},
                     new CommandParameter() {ParameterName = "pMC_IMAGE", Value = ob.MC_IMAGE},
                     new CommandParameter() {ParameterName = "pHR_PROD_FLR_ID", Value = ob.HR_PROD_FLR_ID==0?null:ob.HR_PROD_FLR_ID},                     
                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
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
            const string sp = "PKG_DYE_COMMON.DF_MACHINE_INSERT";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_MACHINE_ID", Value = ob.DF_MACHINE_ID},
                     new CommandParameter() {ParameterName = "pDF_MACHINE_NO", Value = ob.DF_MACHINE_NO},
                     new CommandParameter() {ParameterName = "pDF_MC_NAME", Value = ob.DF_MC_NAME},
                     new CommandParameter() {ParameterName = "pDF_MC_TYPE_ID", Value = ob.DF_MC_TYPE_ID},
                     new CommandParameter() {ParameterName = "pRF_BRAND_ID", Value = ob.RF_BRAND_ID},
                     new CommandParameter() {ParameterName = "pMODEL_NO", Value = ob.MODEL_NO},
                     new CommandParameter() {ParameterName = "pC_ORIGIN_ID", Value = ob.C_ORIGIN_ID},
                     new CommandParameter() {ParameterName = "pD_PRD_CAPACITY", Value = ob.D_PRD_CAPACITY},
                     new CommandParameter() {ParameterName = "pCAP_MOU_ID", Value = ob.CAP_MOU_ID},
                     new CommandParameter() {ParameterName = "pPCT_EFFCNCY", Value = ob.PCT_EFFCNCY},
                     new CommandParameter() {ParameterName = "pMC_SERIAL_NO", Value = ob.MC_SERIAL_NO},
                     new CommandParameter() {ParameterName = "pMFG_YEAR", Value = ob.MFG_YEAR},
                     new CommandParameter() {ParameterName = "pLK_MAC_STATUS_ID", Value = ob.LK_MAC_STATUS_ID},
                     new CommandParameter() {ParameterName = "pMC_SPEED", Value = ob.MC_SPEED},
                     new CommandParameter() {ParameterName = "pRF_LOCATION_ID", Value = ob.RF_LOCATION_ID},
                     new CommandParameter() {ParameterName = "pRF_MAC_PROD_STS_ID", Value = ob.RF_MAC_PROD_STS_ID},
                     new CommandParameter() {ParameterName = "pACTN_TIME", Value = ob.ACTN_TIME},
                     new CommandParameter() {ParameterName = "pMC_IMAGE", Value = ob.MC_IMAGE},
                     new CommandParameter() {ParameterName = "pHR_PROD_FLR_ID", Value = ob.HR_PROD_FLR_ID==0?null:ob.HR_PROD_FLR_ID},  
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
            const string sp = "PKG_DYE_COMMON.DF_MACHINE_DELETE";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_MACHINE_ID", Value = ob.DF_MACHINE_ID},
                     //new CommandParameter() {ParameterName = "pDF_MACHINE_NO", Value = ob.DF_MACHINE_NO},
                     //new CommandParameter() {ParameterName = "pDF_MC_NAME", Value = ob.DF_MC_NAME},
                     //new CommandParameter() {ParameterName = "pDF_MC_TYPE_ID", Value = ob.DF_MC_TYPE_ID},
                     //new CommandParameter() {ParameterName = "pRF_BRAND_ID", Value = ob.RF_BRAND_ID},
                     //new CommandParameter() {ParameterName = "pMODEL_NO", Value = ob.MODEL_NO},
                     //new CommandParameter() {ParameterName = "pC_ORIGIN_ID", Value = ob.C_ORIGIN_ID},
                     //new CommandParameter() {ParameterName = "pD_PRD_CAPACITY", Value = ob.D_PRD_CAPACITY},
                     //new CommandParameter() {ParameterName = "pCAP_MOU_ID", Value = ob.CAP_MOU_ID},
                     //new CommandParameter() {ParameterName = "pPCT_EFFCNCY", Value = ob.PCT_EFFCNCY},
                     //new CommandParameter() {ParameterName = "pMC_SERIAL_NO", Value = ob.MC_SERIAL_NO},
                     //new CommandParameter() {ParameterName = "pMFG_YEAR", Value = ob.MFG_YEAR},
                     //new CommandParameter() {ParameterName = "pLK_MAC_STATUS_ID", Value = ob.LK_MAC_STATUS_ID},
                     //new CommandParameter() {ParameterName = "pMC_SPEED", Value = ob.MC_SPEED},
                     //new CommandParameter() {ParameterName = "pRF_LOCATION_ID", Value = ob.RF_LOCATION_ID},
                     //new CommandParameter() {ParameterName = "pRF_MAC_PROD_STS_ID", Value = ob.RF_MAC_PROD_STS_ID},
                     //new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     //new CommandParameter() {ParameterName = "pCREATED_BY", Value = ob.CREATED_BY},
                     //new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
                     //new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = ob.LAST_UPDATED_BY},
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

        public List<DF_MACHINEModel> SelectAll()
        {
            string sp = "PKG_DYE_COMMON.DF_MACHINE_SELECT";
            try
            {
                var obList = new List<DF_MACHINEModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_MACHINE_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DF_MACHINEModel ob = new DF_MACHINEModel();
                    ob.DF_MACHINE_ID = (dr["DF_MACHINE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_MACHINE_ID"]);
                    ob.DF_MACHINE_NO = (dr["DF_MACHINE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DF_MACHINE_NO"]);
                    ob.DF_MC_NAME = (dr["DF_MC_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DF_MC_NAME"]);
                    ob.DF_MC_TYPE_ID = (dr["DF_MC_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_MC_TYPE_ID"]);
                    ob.RF_BRAND_ID = (dr["RF_BRAND_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_BRAND_ID"]);
                    ob.MODEL_NO = (dr["MODEL_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MODEL_NO"]);
                    ob.C_ORIGIN_ID = (dr["C_ORIGIN_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["C_ORIGIN_ID"]);
                    ob.D_PRD_CAPACITY = (dr["D_PRD_CAPACITY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["D_PRD_CAPACITY"]);
                    ob.CAP_MOU_ID = (dr["CAP_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CAP_MOU_ID"]);
                    ob.PCT_EFFCNCY = (dr["PCT_EFFCNCY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PCT_EFFCNCY"]);
                    ob.MC_SERIAL_NO = (dr["MC_SERIAL_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MC_SERIAL_NO"]);
                    ob.MFG_YEAR = (dr["MFG_YEAR"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MFG_YEAR"]);
                    ob.LK_MAC_STATUS_ID = (dr["LK_MAC_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_MAC_STATUS_ID"]);
                    ob.MC_SPEED = (dr["MC_SPEED"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_SPEED"]);
                    ob.RF_LOCATION_ID = (dr["RF_LOCATION_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_LOCATION_ID"]);
                    ob.RF_MAC_PROD_STS_ID = (dr["RF_MAC_PROD_STS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_MAC_PROD_STS_ID"]);
                    ob.HR_PROD_FLR_ID = (dr["HR_PROD_FLR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_PROD_FLR_ID"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.ACTN_TIME = (dr["ACTN_TIME"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["ACTN_TIME"]);
                    ob.MC_IMAGE = (dr["MC_IMAGE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MC_IMAGE"]);
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


        public List<DF_MACHINEModel> SelectByProcType(Int64? pDYE_BT_CARD_H_ID = null, Int64? pDF_SC_PT_ISS_H_ID = null, Int64? pDF_BT_SUB_LOT_ID = null, Int64? pLK_DIA_TYPE_ID = null)
        {
            string sp = "PKG_DYE_COMMON.DF_MACHINE_SELECT";
            try
            {
                var obList = new List<DF_MACHINEModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_MACHINE_ID", Value =0},
                     new CommandParameter() {ParameterName = "pDYE_BT_CARD_H_ID", Value =pDYE_BT_CARD_H_ID},                     
                     new CommandParameter() {ParameterName = "pDF_SC_PT_ISS_H_ID", Value =pDF_SC_PT_ISS_H_ID},    
                     new CommandParameter() {ParameterName = "pDF_BT_SUB_LOT_ID", Value =pDF_BT_SUB_LOT_ID},    
                     new CommandParameter() {ParameterName = "pLK_DIA_TYPE_ID", Value =pLK_DIA_TYPE_ID},    
                     
                     new CommandParameter() {ParameterName = "pOption", Value =3002},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DF_MACHINEModel ob = new DF_MACHINEModel();
                    ob.DF_MACHINE_ID = (dr["DF_MACHINE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_MACHINE_ID"]);
                    ob.DF_MACHINE_NO = (dr["DF_MACHINE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DF_MACHINE_NO"]);
                    ob.DF_MC_NAME = (dr["DF_MC_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DF_MC_NAME"]);
                    ob.DF_MC_TYPE_ID = (dr["DF_MC_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_MC_TYPE_ID"]);
                    ob.RF_BRAND_ID = (dr["RF_BRAND_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_BRAND_ID"]);
                    ob.MODEL_NO = (dr["MODEL_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MODEL_NO"]);
                    ob.C_ORIGIN_ID = (dr["C_ORIGIN_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["C_ORIGIN_ID"]);
                    ob.D_PRD_CAPACITY = (dr["D_PRD_CAPACITY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["D_PRD_CAPACITY"]);
                    ob.CAP_MOU_ID = (dr["CAP_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CAP_MOU_ID"]);
                    ob.PCT_EFFCNCY = (dr["PCT_EFFCNCY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PCT_EFFCNCY"]);
                    ob.MC_SERIAL_NO = (dr["MC_SERIAL_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MC_SERIAL_NO"]);
                    ob.MFG_YEAR = (dr["MFG_YEAR"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MFG_YEAR"]);
                    ob.LK_MAC_STATUS_ID = (dr["LK_MAC_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_MAC_STATUS_ID"]);
                    ob.MC_SPEED = (dr["MC_SPEED"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_SPEED"]);
                    ob.RF_LOCATION_ID = (dr["RF_LOCATION_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_LOCATION_ID"]);
                    ob.RF_MAC_PROD_STS_ID = (dr["RF_MAC_PROD_STS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_MAC_PROD_STS_ID"]);
                    ob.ACTN_TIME = (dr["ACTN_TIME"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["ACTN_TIME"]);
                    ob.MC_IMAGE = (dr["MC_IMAGE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MC_IMAGE"]);
                    ob.HR_PROD_FLR_ID = (dr["HR_PROD_FLR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_PROD_FLR_ID"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);
                    
                    ob.DYE_BT_STS_TYPE_ID = (dr["DYE_BT_STS_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_BT_STS_TYPE_ID"]);
                    ob.NXT_BT_STS_TYPE_LST = (dr["NXT_BT_STS_TYPE_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["NXT_BT_STS_TYPE_LST"]);
                    ob.IS_DEFA_FINIS = (dr["IS_DEFA_FINIS"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_DEFA_FINIS"]);

                    ob.DF_PROC_TYPE_ID = (dr["DF_PROC_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_PROC_TYPE_ID"]);
                    ob.DF_MC_TYPE_NAME_EN = (dr["DF_MC_TYPE_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DF_MC_TYPE_NAME_EN"]);
                    ob.DF_PROC_TYPE_NAME = (dr["DF_PROC_TYPE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DF_PROC_TYPE_NAME"]);
                    ob.IS_DONE = (dr["IS_DONE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_DONE"]);
                    ob.PROC_SEQ = (dr["PROC_SEQ"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PROC_SEQ"]);

                    ob.PRE_DF_PROC_TYPE_ID = (dr["PRE_DF_PROC_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PRE_DF_PROC_TYPE_ID"]);
                    if (ob.PRE_DF_PROC_TYPE_ID > 0)
                        ob.IS_REPROCESS = "N";
                    else
                        ob.IS_REPROCESS = (dr["IS_REPROCESS"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_REPROCESS"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DF_MACHINEModel Select(Int64? ID)
        {
            string sp = "PKG_DYE_COMMON.DF_MACHINE_SELECT";
            try
            {
                var ob = new DF_MACHINEModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_MACHINE_ID", Value =ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.DF_MACHINE_ID = (dr["DF_MACHINE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_MACHINE_ID"]);
                    ob.DF_MACHINE_NO = (dr["DF_MACHINE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DF_MACHINE_NO"]);
                    ob.DF_MC_NAME = (dr["DF_MC_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DF_MC_NAME"]);
                    ob.DF_MC_TYPE_ID = (dr["DF_MC_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_MC_TYPE_ID"]);
                    ob.RF_BRAND_ID = (dr["RF_BRAND_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_BRAND_ID"]);
                    ob.MODEL_NO = (dr["MODEL_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MODEL_NO"]);
                    ob.C_ORIGIN_ID = (dr["C_ORIGIN_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["C_ORIGIN_ID"]);
                    ob.D_PRD_CAPACITY = (dr["D_PRD_CAPACITY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["D_PRD_CAPACITY"]);
                    ob.CAP_MOU_ID = (dr["CAP_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CAP_MOU_ID"]);
                    ob.PCT_EFFCNCY = (dr["PCT_EFFCNCY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PCT_EFFCNCY"]);
                    ob.MC_SERIAL_NO = (dr["MC_SERIAL_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MC_SERIAL_NO"]);
                    ob.MFG_YEAR = (dr["MFG_YEAR"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MFG_YEAR"]);
                    ob.LK_MAC_STATUS_ID = (dr["LK_MAC_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_MAC_STATUS_ID"]);
                    ob.MC_SPEED = (dr["MC_SPEED"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_SPEED"]);
                    ob.RF_LOCATION_ID = (dr["RF_LOCATION_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_LOCATION_ID"]);
                    ob.RF_MAC_PROD_STS_ID = (dr["RF_MAC_PROD_STS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_MAC_PROD_STS_ID"]);
                    ob.ACTN_TIME = (dr["ACTN_TIME"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["ACTN_TIME"]);
                    ob.MC_IMAGE = (dr["MC_IMAGE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MC_IMAGE"]);
                    ob.HR_PROD_FLR_ID = (dr["HR_PROD_FLR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_PROD_FLR_ID"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);

                }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string DF_MC_TYPE_NAME_EN { get; set; }

        public string DF_PROC_TYPE_NAME { get; set; }

        public long PROC_SEQ { get; set; }

        public string IS_DONE { get; set; }

        public string IS_REPROCESS { get; set; }

        public long? PRE_DF_PROC_TYPE_ID { get; set; }

        public long DYE_BT_STS_TYPE_ID { get; set; }

        public string NXT_BT_STS_TYPE_LST { get; set; }

        public string IS_DEFA_FINIS { get; set; }
    }
}