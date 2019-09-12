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
    public class DF_BT_PRODModel
    {
        public Int64 DF_BT_PROD_ID { get; set; }
        public DateTime PROD_DT { get; set; }
        public Int64? DYE_BT_CARD_H_ID { get; set; }
        public string DYE_LOT_NO { get; set; }
        public Int64? DF_BT_SUB_LOT_ID { get; set; }
        public Int64? MC_FAB_PROD_ORD_H_ID { get; set; }
        public Int64 FAB_COLOR_ID { get; set; }
        public Int64 RF_FAB_TYPE_ID { get; set; }
        public Int64 DF_PROC_TYPE_ID { get; set; }
        public Int64 DF_MACHINE_ID { get; set; }
        public Int64 OPERATOR_ID { get; set; }
        public Int64 HR_SCHEDULE_H_ID { get; set; }
        public Int64 BT_FIN_QTY { get; set; }
        public Int64 QTY_MOU_ID { get; set; }
        public DateTime LOAD_TIME { get; set; }
        public DateTime UN_LOAD_TIME { get; set; }
        public string BT_STOP_DESC { get; set; }
        public Int64 DYE_BT_STS_TYPE_ID { get; set; }
        public string IS_FINALIZED { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }
        public Int64 LK_DIA_TYPE_ID { get; set; }
        public Int64? DF_SC_PT_ISS_H_ID { get; set; }
        public Int64? NXT_BT_STS_TYPE_ID { get; set; }
        public string XML_PROD_D { get; set; }
        public string LK_FBR_GRP_LST { get; set; }
        

        public string Save()
        {
            const string sp = "PKG_DF_PRODUCTION.DF_BT_PROD_INSERT";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_BT_PROD_ID", Value = ob.DF_BT_PROD_ID},
                     new CommandParameter() {ParameterName = "pPROD_DT", Value = ob.PROD_DT},
                     new CommandParameter() {ParameterName = "pDYE_BT_CARD_H_ID", Value = ob.DYE_BT_CARD_H_ID},
                     new CommandParameter() {ParameterName = "pDF_SC_PT_ISS_H_ID", Value = ob.DF_SC_PT_ISS_H_ID},
                     new CommandParameter() {ParameterName = "pDYE_LOT_NO", Value = ob.DYE_LOT_NO},
                     new CommandParameter() {ParameterName = "pDF_BT_SUB_LOT_ID", Value = ob.DF_BT_SUB_LOT_ID==0?null:ob.DF_BT_SUB_LOT_ID},
                     new CommandParameter() {ParameterName = "pMC_FAB_PROD_ORD_H_ID", Value = ob.MC_FAB_PROD_ORD_H_ID==0?null:ob.MC_FAB_PROD_ORD_H_ID},
                     new CommandParameter() {ParameterName = "pFAB_COLOR_ID", Value = ob.FAB_COLOR_ID},
                     new CommandParameter() {ParameterName = "pRF_FAB_TYPE_ID", Value = ob.RF_FAB_TYPE_ID},
                     new CommandParameter() {ParameterName = "pLK_DIA_TYPE_ID", Value = ob.LK_DIA_TYPE_ID},
                     new CommandParameter() {ParameterName = "pIS_REFINIS", Value = ob.IS_REFINIS},
                     new CommandParameter() {ParameterName = "pXML_PROD_D", Value = ob.XML_PROD_D},
                     new CommandParameter() {ParameterName = "pLK_FBR_GRP_LST", Value = ob.LK_FBR_GRP_LST},
                     
                     new CommandParameter() {ParameterName = "pDF_PROC_TYPE_ID", Value = ob.DF_PROC_TYPE_ID},
                     new CommandParameter() {ParameterName = "pDF_MACHINE_ID", Value = ob.DF_MACHINE_ID},
                     new CommandParameter() {ParameterName = "pOPERATOR_ID", Value = ob.OPERATOR_ID},
                     new CommandParameter() {ParameterName = "pHR_SCHEDULE_H_ID", Value = ob.HR_SCHEDULE_H_ID},
                     new CommandParameter() {ParameterName = "pBT_FIN_QTY", Value = ob.BT_FIN_QTY},
                     new CommandParameter() {ParameterName = "pQTY_MOU_ID", Value = ob.QTY_MOU_ID==0?3: ob.QTY_MOU_ID},
                     new CommandParameter() {ParameterName = "pLOAD_TIME", Value = ob.LOAD_TIME},
                     new CommandParameter() {ParameterName = "pUN_LOAD_TIME", Value = ob.UN_LOAD_TIME},
                     new CommandParameter() {ParameterName = "pBT_STOP_DESC", Value = ob.BT_STOP_DESC},
                     new CommandParameter() {ParameterName = "pDYE_BT_STS_TYPE_ID", Value = ob.DYE_BT_STS_TYPE_ID==0?1:ob.DYE_BT_STS_TYPE_ID},
                     new CommandParameter() {ParameterName = "pNXT_BT_STS_TYPE_ID", Value = ob.NXT_BT_STS_TYPE_ID},
                     new CommandParameter() {ParameterName = "pIS_FINALIZED", Value = ob.IS_FINALIZED},
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
            const string sp = "SP_DF_BT_PROD";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_BT_PROD_ID", Value = ob.DF_BT_PROD_ID},
                     new CommandParameter() {ParameterName = "pPROD_DT", Value = ob.PROD_DT},
                     new CommandParameter() {ParameterName = "pDYE_BT_CARD_H_ID", Value = ob.DYE_BT_CARD_H_ID},
                     new CommandParameter() {ParameterName = "pDYE_LOT_NO", Value = ob.DYE_LOT_NO},
                     new CommandParameter() {ParameterName = "pDF_BT_SUB_LOT_ID", Value = ob.DF_BT_SUB_LOT_ID},
                     new CommandParameter() {ParameterName = "pMC_FAB_PROD_ORD_H_ID", Value = ob.MC_FAB_PROD_ORD_H_ID},
                     new CommandParameter() {ParameterName = "pFAB_COLOR_ID", Value = ob.FAB_COLOR_ID},
                     new CommandParameter() {ParameterName = "pRF_FAB_TYPE_ID", Value = ob.RF_FAB_TYPE_ID},
                     new CommandParameter() {ParameterName = "pDF_PROC_TYPE_ID", Value = ob.DF_PROC_TYPE_ID},
                     new CommandParameter() {ParameterName = "pDF_MACHINE_ID", Value = ob.DF_MACHINE_ID},
                     new CommandParameter() {ParameterName = "pOPERATOR_ID", Value = ob.OPERATOR_ID},
                     new CommandParameter() {ParameterName = "pHR_SCHEDULE_H_ID", Value = ob.HR_SCHEDULE_H_ID},
                     new CommandParameter() {ParameterName = "pBT_FIN_QTY", Value = ob.BT_FIN_QTY},
                     new CommandParameter() {ParameterName = "pQTY_MOU_ID", Value = ob.QTY_MOU_ID},
                     new CommandParameter() {ParameterName = "pLOAD_TIME", Value = ob.LOAD_TIME},
                     new CommandParameter() {ParameterName = "pUN_LOAD_TIME", Value = ob.UN_LOAD_TIME},
                     new CommandParameter() {ParameterName = "pBT_STOP_DESC", Value = ob.BT_STOP_DESC},
                     new CommandParameter() {ParameterName = "pDYE_BT_STS_TYPE_ID", Value = ob.DYE_BT_STS_TYPE_ID},
                     new CommandParameter() {ParameterName = "pIS_FINALIZED", Value = ob.IS_FINALIZED},
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
            const string sp = "SP_DF_BT_PROD";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_BT_PROD_ID", Value = ob.DF_BT_PROD_ID},
                     new CommandParameter() {ParameterName = "pPROD_DT", Value = ob.PROD_DT},
                     new CommandParameter() {ParameterName = "pDYE_BT_CARD_H_ID", Value = ob.DYE_BT_CARD_H_ID},
                     new CommandParameter() {ParameterName = "pDYE_LOT_NO", Value = ob.DYE_LOT_NO},
                     new CommandParameter() {ParameterName = "pDF_BT_SUB_LOT_ID", Value = ob.DF_BT_SUB_LOT_ID},
                     new CommandParameter() {ParameterName = "pMC_FAB_PROD_ORD_H_ID", Value = ob.MC_FAB_PROD_ORD_H_ID},
                     new CommandParameter() {ParameterName = "pFAB_COLOR_ID", Value = ob.FAB_COLOR_ID},
                     new CommandParameter() {ParameterName = "pRF_FAB_TYPE_ID", Value = ob.RF_FAB_TYPE_ID},
                     new CommandParameter() {ParameterName = "pDF_PROC_TYPE_ID", Value = ob.DF_PROC_TYPE_ID},
                     new CommandParameter() {ParameterName = "pDF_MACHINE_ID", Value = ob.DF_MACHINE_ID},
                     new CommandParameter() {ParameterName = "pOPERATOR_ID", Value = ob.OPERATOR_ID},
                     new CommandParameter() {ParameterName = "pHR_SCHEDULE_H_ID", Value = ob.HR_SCHEDULE_H_ID},
                     new CommandParameter() {ParameterName = "pBT_FIN_QTY", Value = ob.BT_FIN_QTY},
                     new CommandParameter() {ParameterName = "pQTY_MOU_ID", Value = ob.QTY_MOU_ID},
                     new CommandParameter() {ParameterName = "pLOAD_TIME", Value = ob.LOAD_TIME},
                     new CommandParameter() {ParameterName = "pUN_LOAD_TIME", Value = ob.UN_LOAD_TIME},
                     new CommandParameter() {ParameterName = "pBT_STOP_DESC", Value = ob.BT_STOP_DESC},
                     new CommandParameter() {ParameterName = "pDYE_BT_STS_TYPE_ID", Value = ob.DYE_BT_STS_TYPE_ID},
                     new CommandParameter() {ParameterName = "pIS_FINALIZED", Value = ob.IS_FINALIZED},
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

        public List<DF_BT_PRODModel> SelectAll()
        {
            string sp = "Select_DF_BT_PROD";
            try
            {
                var obList = new List<DF_BT_PRODModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_BT_PROD_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DF_BT_PRODModel ob = new DF_BT_PRODModel();
                    ob.DF_BT_PROD_ID = (dr["DF_BT_PROD_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_BT_PROD_ID"]);
                    ob.PROD_DT = (dr["PROD_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["PROD_DT"]);
                    ob.DYE_BT_CARD_H_ID = (dr["DYE_BT_CARD_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_BT_CARD_H_ID"]);
                    ob.DYE_LOT_NO = (dr["DYE_LOT_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DYE_LOT_NO"]);
                    ob.DF_BT_SUB_LOT_ID = (dr["DF_BT_SUB_LOT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_BT_SUB_LOT_ID"]);
                    ob.MC_FAB_PROD_ORD_H_ID = (dr["MC_FAB_PROD_ORD_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_FAB_PROD_ORD_H_ID"]);
                    ob.FAB_COLOR_ID = (dr["FAB_COLOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["FAB_COLOR_ID"]);
                    ob.RF_FAB_TYPE_ID = (dr["RF_FAB_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_FAB_TYPE_ID"]);
                    ob.DF_PROC_TYPE_ID = (dr["DF_PROC_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_PROC_TYPE_ID"]);
                    ob.DF_MACHINE_ID = (dr["DF_MACHINE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_MACHINE_ID"]);
                    ob.OPERATOR_ID = (dr["OPERATOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["OPERATOR_ID"]);
                    ob.HR_SCHEDULE_H_ID = (dr["HR_SCHEDULE_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_SCHEDULE_H_ID"]);
                    ob.BT_FIN_QTY = (dr["BT_FIN_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["BT_FIN_QTY"]);
                    ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);
                    ob.LOAD_TIME = (dr["LOAD_TIME"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LOAD_TIME"]);
                    ob.UN_LOAD_TIME = (dr["UN_LOAD_TIME"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["UN_LOAD_TIME"]);
                    ob.BT_STOP_DESC = (dr["BT_STOP_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BT_STOP_DESC"]);
                    ob.DYE_BT_STS_TYPE_ID = (dr["DYE_BT_STS_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_BT_STS_TYPE_ID"]);
                    ob.IS_FINALIZED = (dr["IS_FINALIZED"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_FINALIZED"]);
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


        public List<DF_BT_PRODModel> GetDFProductionByBatchNo(string pDYE_BATCH_NO = null, string pSC_PRG_NO = null, Int64? pLK_DIA_TYPE_ID = null, string pLK_FBR_GRP_LST = null)
        {
            string sp = "pkg_df_production.df_bt_prod_select";
            try
            {
                var obList = new List<DF_BT_PRODModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_BATCH_NO", Value =pDYE_BATCH_NO},
                     new CommandParameter() {ParameterName = "pSC_PRG_NO", Value =pSC_PRG_NO},                     
                     new CommandParameter() {ParameterName = "pLK_DIA_TYPE_ID", Value =pLK_DIA_TYPE_ID},    
                     new CommandParameter() {ParameterName = "pLK_FBR_GRP_LST", Value =pLK_FBR_GRP_LST},    
                     new CommandParameter() {ParameterName = "pOption", Value =3002},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DF_BT_PRODModel ob = new DF_BT_PRODModel();
                    ob.DF_BT_PROD_ID = (dr["DF_BT_PROD_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_BT_PROD_ID"]);
                    ob.PROD_DT = (dr["PROD_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["PROD_DT"]);
                    ob.DYE_BT_CARD_H_ID = (dr["DYE_BT_CARD_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_BT_CARD_H_ID"]);
                    ob.DYE_LOT_NO = (dr["DYE_LOT_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DYE_LOT_NO"]);
                    ob.DF_BT_SUB_LOT_ID = (dr["DF_BT_SUB_LOT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_BT_SUB_LOT_ID"]);
                    ob.MC_FAB_PROD_ORD_H_ID = (dr["MC_FAB_PROD_ORD_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_FAB_PROD_ORD_H_ID"]);
                    ob.LK_DIA_TYPE_ID = (dr["LK_DIA_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_DIA_TYPE_ID"]);
                    ob.FAB_COLOR_ID = (dr["FAB_COLOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["FAB_COLOR_ID"]);
                    ob.RF_FAB_TYPE_ID = (dr["RF_FAB_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_FAB_TYPE_ID"]);
                    ob.DF_PROC_TYPE_ID = (dr["DF_PROC_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_PROC_TYPE_ID"]);
                    ob.DF_MACHINE_ID = (dr["DF_MACHINE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_MACHINE_ID"]);
                    ob.OPERATOR_ID = (dr["OPERATOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["OPERATOR_ID"]);
                    ob.HR_SCHEDULE_H_ID = (dr["HR_SCHEDULE_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_SCHEDULE_H_ID"]);
                    ob.BT_FIN_QTY = (dr["BT_FIN_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["BT_FIN_QTY"]);
                    ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);
                    ob.LOAD_TIME = (dr["LOAD_TIME"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LOAD_TIME"]);
                    ob.UN_LOAD_TIME = (dr["UN_LOAD_TIME"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["UN_LOAD_TIME"]);
                    ob.BT_STOP_DESC = (dr["BT_STOP_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BT_STOP_DESC"]);
                    ob.DYE_BT_STS_TYPE_ID = (dr["DYE_BT_STS_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_BT_STS_TYPE_ID"]);
                    ob.IS_FINALIZED = (dr["IS_FINALIZED"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_FINALIZED"]);
                    ob.IS_REFINIS = (dr["IS_REFINIS"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_REFINIS"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);

                    ob.DF_PROC_TYPE_NAME = (dr["DF_PROC_TYPE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DF_PROC_TYPE_NAME"]);
                    ob.DF_MACHINE_NO = (dr["DF_MACHINE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DF_MACHINE_NO"]);
                    ob.DF_MC_NAME = (dr["DF_MC_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DF_MC_NAME"]);
                    ob.OPERATOR_NAME = (dr["OPERATOR_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["OPERATOR_NAME"]);

                    
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DF_BT_PRODModel Select(long ID)
        {
            string sp = "Select_DF_BT_PROD";
            try
            {
                var ob = new DF_BT_PRODModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_BT_PROD_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.DF_BT_PROD_ID = (dr["DF_BT_PROD_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_BT_PROD_ID"]);
                    ob.PROD_DT = (dr["PROD_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["PROD_DT"]);
                    ob.DYE_BT_CARD_H_ID = (dr["DYE_BT_CARD_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_BT_CARD_H_ID"]);
                    ob.DYE_LOT_NO = (dr["DYE_LOT_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DYE_LOT_NO"]);
                    ob.DF_BT_SUB_LOT_ID = (dr["DF_BT_SUB_LOT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_BT_SUB_LOT_ID"]);
                    ob.MC_FAB_PROD_ORD_H_ID = (dr["MC_FAB_PROD_ORD_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_FAB_PROD_ORD_H_ID"]);
                    ob.FAB_COLOR_ID = (dr["FAB_COLOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["FAB_COLOR_ID"]);
                    ob.RF_FAB_TYPE_ID = (dr["RF_FAB_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_FAB_TYPE_ID"]);
                    ob.DF_PROC_TYPE_ID = (dr["DF_PROC_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_PROC_TYPE_ID"]);
                    ob.DF_MACHINE_ID = (dr["DF_MACHINE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_MACHINE_ID"]);
                    ob.OPERATOR_ID = (dr["OPERATOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["OPERATOR_ID"]);
                    ob.HR_SCHEDULE_H_ID = (dr["HR_SCHEDULE_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_SCHEDULE_H_ID"]);
                    ob.BT_FIN_QTY = (dr["BT_FIN_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["BT_FIN_QTY"]);
                    ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);
                    ob.LOAD_TIME = (dr["LOAD_TIME"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LOAD_TIME"]);
                    ob.UN_LOAD_TIME = (dr["UN_LOAD_TIME"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["UN_LOAD_TIME"]);
                    ob.BT_STOP_DESC = (dr["BT_STOP_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BT_STOP_DESC"]);
                    ob.DYE_BT_STS_TYPE_ID = (dr["DYE_BT_STS_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_BT_STS_TYPE_ID"]);
                    ob.IS_FINALIZED = (dr["IS_FINALIZED"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_FINALIZED"]);
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


        public List<DF_BT_PRODModel> SelectForScProgram(string pDYE_BATCH_NO = null, string pIS_AOP = null)
        {
            string sp = "PKG_DF_SC_PROGRAM.scan_dye_bt_card_df_select";
            try
            {
                List<DF_BT_PRODModel> list = new List<DF_BT_PRODModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_BATCH_NO", Value =pDYE_BATCH_NO},
                     new CommandParameter() {ParameterName = "pIS_AOP", Value =pIS_AOP},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    var ob = new DF_BT_PRODModel();
                    ob.KNT_STYL_FAB_ITEM_H_ID = (dr["KNT_STYL_FAB_ITEM_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_STYL_FAB_ITEM_H_ID"]);
                    ob.DYE_BT_CARD_H_ID = (dr["DYE_BT_CARD_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_BT_CARD_H_ID"]);
                    ob.DYE_BATCH_NO = (dr["DYE_BATCH_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DYE_BATCH_NO"]);
                    ob.DYE_LOT_NO = (dr["DYE_LOT_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DYE_LOT_NO"]);
                    ob.FAB_TYPE_NAME = (dr["FAB_TYPE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FAB_TYPE_NAME"]);
                    ob.FIB_COMP_NAME = (dr["FIB_COMP_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FIB_COMP_NAME"]);
                    ob.BUYER_NAME_EN = (dr["BUYER_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BUYER_NAME_EN"]);
                    ob.STYLE_NO = (dr["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO"]);
                    ob.ORDER_NO = (dr["ORDER_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_NO"]);
                    ob.COLOR_NAME_EN = (dr["COLOR_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_NAME_EN"]);
                    ob.FAB_ITEM_DESC = (dr["FAB_ITEM_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FAB_ITEM_DESC"]);

                    ob.FIN_DIA = (dr["FIN_DIA"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FIN_DIA"]);
                    ob.FIN_GSM = (dr["FIN_GSM"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FIN_GSM"]);
                    ob.KNT_YRN_LOT_LST = (dr["KNT_YRN_LOT_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["KNT_YRN_LOT_LST"]);
                    ob.DIA_TYPE_NAME = (dr["DIA_TYPE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DIA_TYPE_NAME"]);

                    ob.LK_DIA_TYPE_ID = (dr["LK_DIA_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_DIA_TYPE_ID"]);
                    ob.LK_FBR_GRP_ID = (dr["LK_FBR_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_FBR_GRP_ID"]);
                    ob.RF_FAB_TYPE_ID = (dr["RF_FAB_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_FAB_TYPE_ID"]);
                    ob.RF_FIB_COMP_ID = (dr["RF_FIB_COMP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_FIB_COMP_ID"]);
                    ob.RQD_GSM = (dr["RQD_GSM"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["RQD_GSM"]);

                    ob.ACT_BATCH_QTY = (dr["ACT_BATCH_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ACT_BATCH_QTY"]);
                    ob.PRE_ISS_QTY = (dr["PRE_ISS_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PRE_ISS_QTY"]);
                    ob.ISS_QTY = ob.ACT_BATCH_QTY - ob.PRE_ISS_QTY;

                    list.Add(ob);
                }
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string DF_PROC_TYPE_NAME { get; set; }

        public string DF_MACHINE_NO { get; set; }

        public string DF_MC_NAME { get; set; }

        public long ACT_BATCH_QTY { get; set; }

        public string COLOR_NAME_EN { get; set; }

        public string ORDER_NO { get; set; }

        public string STYLE_NO { get; set; }

        public string BUYER_NAME_EN { get; set; }

        public string FIB_COMP_NAME { get; set; }

        public string FAB_TYPE_NAME { get; set; }

        public string DYE_BATCH_NO { get; set; }

        public long MC_STYLE_D_FAB_ID { get; set; }

        public long KNT_STYL_FAB_ITEM_H_ID { get; set; }

        public string FIN_DIA { get; set; }

        public string FIN_GSM { get; set; }

        public string KNT_YRN_LOT_LST { get; set; }

        public string DIA_TYPE_NAME { get; set; }

        public long ISS_QTY { get; set; }

        public long PRE_ISS_QTY { get; set; }

        public long LK_FBR_GRP_ID { get; set; }

        public long RF_FIB_COMP_ID { get; set; }

        public string RQD_GSM { get; set; }

        public string OPERATOR_NAME { get; set; }
        public string FAB_ITEM_DESC { get; set; }
        
        public string IS_REFINIS { get; set; }
    }
}