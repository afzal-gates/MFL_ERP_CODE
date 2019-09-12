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
    public class DYE_BT_PRODModel
    {
        public Int64 DYE_BT_PROD_ID { get; set; }
        public DateTime PROD_DT { get; set; }
        public Int64 DYE_BT_CARD_H_ID { get; set; }
        public Int64 DYE_RE_PROC_TYPE_ID { get; set; }
        public DateTime? LOAD_TIME { get; set; }
        public DateTime? UN_LOAD_TIME { get; set; }
        public DateTime? CHK_ROLL_DT { get; set; }
        public Int64 CHECK_BY { get; set; }
        public string IS_ROLL_OK { get; set; }
        public string RF_DY_DFCT_TYPE_LST { get; set; }
        public string REMARKS { get; set; }
        public Int64? REQ_RE_PROC_TYPE_ID { get; set; }
        public Int64 DYE_BT_STS_TYPE_ID { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }
        public Int64 HOURS { get; set; }
        public Int64 SECONDS { get; set; }
        public string IS_EDT_LOCKED { get; set; }
        public string IS_FINALIZED { get; set; }


        public long DYE_BATCH_PLAN_ID { get; set; }

        public long MC_LD_RECIPE_H_ID { get; set; }

        public long LQR_RATIO { get; set; }

        public string DYE_LOT_NO { get; set; }

        public string DYE_BATCH_NO { get; set; }
        public DateTime BATCH_REQ_DT { get; set; }
        public decimal ACT_BATCH_QTY { get; set; }
        public long QTY_MOU_ID { get; set; }
        public long MERGE_BT_CRD_ID { get; set; }
        public long LK_SHIFT_TYPE_ID { get; set; }
        public DateTime ACT_LOAD_TIME { get; set; }
        public DateTime ACT_UN_LOAD_TIME { get; set; }
        public long OPERATOR_ID { get; set; }
        public string DYE_MACHINE_NO { get; set; }
        public string BUYER_NAME_EN { get; set; }
        public string STYLE_NO { get; set; }
        public string BT_STS_TYP_NAME { get; set; }
        public string COLOR_GRP_NAME_EN { get; set; }
        public string ORDER_NO { get; set; }
        public string COLOR_NAME_EN { get; set; }
        public string LD_RECIPE_NO { get; set; }
        public long MC_COLOR_GRP_ID { get; set; }
        public string DYE_MTHD_NAME { get; set; }
        public string FABRIC_SNAME { get; set; }
        public string BYR_ACC_NAME_EN { get; set; }
        public long DYE_MACHINE_ID { get; set; }
        public long IS_DISABLE { get; set; }
        public long DYE_STR_REQ_H_ID { get; set; }
        public long TOTAL_REC { get; set; }
        public string DYE_BT_CARD_H_LST { get; set; }

        public Int64? LK_CHQ_RL_STS_ID { get; set; }

        public DateTime? CK_ROLL_RCV_DT { get; set; }
        public DateTime? CK_ROLL_FIN_DT { get; set; }
        public DateTime? UN_HOLD_DT { get; set; }

        public Int64? DFCT_TYPE_ID { get; set; }


        public Int64 LK_RIB_CHQ_RL_STS_ID { get; set; }
        public DateTime? RIB_QC_DT { get; set; }
        public DateTime? RIB_HOLD_DT { get; set; }
        public DateTime? RIB_UN_HOLD_DT { get; set; }
        public Int64? RIB_QC_BY { get; set; }
        public string IS_RIB_QC { get; set; }
        public string RIB_RF_DY_DFCT_TYPE_LST { get; set; }
        public string RIB_DF_PROC_TYP_LST { get; set; }
        public string RIB_REMARKS { get; set; }

        
        public string Save()
        {
            const string sp = "PKG_DYE_BATCH_PROGRAM.dye_bt_prod_update";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_BT_PROD_ID", Value = ob.DYE_BT_PROD_ID},
                     new CommandParameter() {ParameterName = "pPROD_DT", Value = ob.PROD_DT},
                     new CommandParameter() {ParameterName = "pDYE_BT_CARD_H_ID", Value = ob.DYE_BT_CARD_H_ID},
                     new CommandParameter() {ParameterName = "pDYE_RE_PROC_TYPE_ID", Value = ob.DYE_RE_PROC_TYPE_ID},
                     new CommandParameter() {ParameterName = "pLOAD_TIME", Value = ob.LOAD_TIME},
                     new CommandParameter() {ParameterName = "pUN_LOAD_TIME", Value = ob.UN_LOAD_TIME},
                     new CommandParameter() {ParameterName = "pBT_STOP_DESC", Value = ob.BT_STOP_DESC},
                     new CommandParameter() {ParameterName = "pCHK_ROLL_DT", Value = ob.CHK_ROLL_DT},
                     new CommandParameter() {ParameterName = "pCHECK_BY", Value = ob.CHECK_BY},
                     new CommandParameter() {ParameterName = "pIS_ROLL_OK", Value = ob.IS_ROLL_OK},
                     new CommandParameter() {ParameterName = "pLK_CHQ_RL_STS_ID", Value = ob.LK_CHQ_RL_STS_ID},                     
                     new CommandParameter() {ParameterName = "pRF_DY_DFCT_TYPE_LST", Value = ob.RF_DY_DFCT_TYPE_LST},
                     new CommandParameter() {ParameterName = "pREMARKS", Value = ob.REMARKS},
                     new CommandParameter() {ParameterName = "pREQ_RE_PROC_TYPE_ID", Value = ob.REQ_RE_PROC_TYPE_ID},
                     new CommandParameter() {ParameterName = "pDYE_BT_STS_TYPE_ID", Value = ob.DYE_BT_STS_TYPE_ID},
                     new CommandParameter() {ParameterName = "pDYE_BATCH_PLAN_ID", Value = ob.DYE_BATCH_PLAN_ID},                     
                     new CommandParameter() {ParameterName = "pIS_FINALIZED", Value = ob.IS_FINALIZED},                     
                     new CommandParameter() {ParameterName = "pIS_EDT_LOCKED", Value = ob.IS_EDT_LOCKED},                     
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
            const string sp = "PKG_DYE_BATCH_PROGRAM.dye_bt_prod_update";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_BT_PROD_ID", Value = ob.DYE_BT_PROD_ID},
                     new CommandParameter() {ParameterName = "pPROD_DT", Value = ob.PROD_DT},
                     new CommandParameter() {ParameterName = "pDYE_BT_CARD_H_ID", Value = ob.DYE_BT_CARD_H_ID},
                     new CommandParameter() {ParameterName = "pDYE_STR_REQ_H_ID", Value = ob.DYE_STR_REQ_H_ID},                     
                     new CommandParameter() {ParameterName = "pDYE_RE_PROC_TYPE_ID", Value = ob.DYE_RE_PROC_TYPE_ID},
                     new CommandParameter() {ParameterName = "pDYE_MACHINE_ID", Value = ob.DYE_MACHINE_ID},                     
                     new CommandParameter() {ParameterName = "pLOAD_TIME", Value = ob.LOAD_TIME},
                     new CommandParameter() {ParameterName = "pUN_LOAD_TIME", Value = ob.UN_LOAD_TIME},
                     new CommandParameter() {ParameterName = "pBT_STOP_DESC", Value = ob.BT_STOP_DESC},
                     new CommandParameter() {ParameterName = "pCHK_ROLL_DT", Value = ob.CHK_ROLL_DT},
                     new CommandParameter() {ParameterName = "pCHECK_BY", Value = ob.CHECK_BY},
                     new CommandParameter() {ParameterName = "pIS_ROLL_OK", Value = ob.IS_ROLL_OK},
                     new CommandParameter() {ParameterName = "pLK_CHQ_RL_STS_ID", Value = ob.LK_CHQ_RL_STS_ID},                     
                     new CommandParameter() {ParameterName = "pRF_DY_DFCT_TYPE_LST", Value = ob.RF_DY_DFCT_TYPE_LST},
                     new CommandParameter() {ParameterName = "pREMARKS", Value = ob.REMARKS},
                     new CommandParameter() {ParameterName = "pREQ_RE_PROC_TYPE_ID", Value = ob.REQ_RE_PROC_TYPE_ID},
                     new CommandParameter() {ParameterName = "pDYE_BT_STS_TYPE_ID", Value = ob.DYE_BT_STS_TYPE_ID},
                     new CommandParameter() {ParameterName = "pIS_EDT_LOCKED", Value = ob.IS_EDT_LOCKED},     
                     new CommandParameter() {ParameterName = "pDYE_BATCH_PLAN_ID", Value = ob.DYE_BATCH_PLAN_ID},                 
                     new CommandParameter() {ParameterName = "pCK_ROLL_RCV_DT", Value = ob.CK_ROLL_RCV_DT},
                     new CommandParameter() {ParameterName = "pCK_ROLL_FIN_DT", Value = ob.CK_ROLL_FIN_DT},
                     new CommandParameter() {ParameterName = "pIS_FINALIZED", Value = ob.IS_FINALIZED},                     
                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
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


        public string Update_Batch()
        {
            const string sp = "PKG_DYE_BATCH_PROGRAM.dye_bt_prod_update_data";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_BT_PROD_ID", Value = ob.DYE_BT_PROD_ID},
                     new CommandParameter() {ParameterName = "pPROD_DT", Value = ob.PROD_DT},
                     new CommandParameter() {ParameterName = "pDYE_BT_CARD_H_ID", Value = ob.DYE_BT_CARD_H_ID},
                     new CommandParameter() {ParameterName = "pDYE_STR_REQ_H_ID", Value = ob.DYE_STR_REQ_H_ID},                     
                     new CommandParameter() {ParameterName = "pDYE_RE_PROC_TYPE_ID", Value = ob.DYE_RE_PROC_TYPE_ID},
                     new CommandParameter() {ParameterName = "pDYE_MACHINE_ID", Value = ob.DYE_MACHINE_ID},                     
                     new CommandParameter() {ParameterName = "pLOAD_TIME", Value = ob.LOAD_TIME},
                     new CommandParameter() {ParameterName = "pUN_LOAD_TIME", Value = ob.UN_LOAD_TIME},
                     new CommandParameter() {ParameterName = "pBT_STOP_DESC", Value = ob.BT_STOP_DESC},
                     new CommandParameter() {ParameterName = "pCHK_ROLL_DT", Value = ob.CHK_ROLL_DT},
                     new CommandParameter() {ParameterName = "pCHECK_BY", Value = ob.CHECK_BY},
                     new CommandParameter() {ParameterName = "pIS_ROLL_OK", Value = ob.IS_ROLL_OK},
                     new CommandParameter() {ParameterName = "pLK_CHQ_RL_STS_ID", Value = ob.LK_CHQ_RL_STS_ID},                     
                     new CommandParameter() {ParameterName = "pRF_DY_DFCT_TYPE_LST", Value = ob.RF_DY_DFCT_TYPE_LST},
                     new CommandParameter() {ParameterName = "pDF_PROC_TYP_LST", Value = ob.DF_PROC_TYP_LST},
                     new CommandParameter() {ParameterName = "pREMARKS", Value = ob.REMARKS},
                     new CommandParameter() {ParameterName = "pREQ_RE_PROC_TYPE_ID", Value = ob.REQ_RE_PROC_TYPE_ID},
                     new CommandParameter() {ParameterName = "pDYE_BT_STS_TYPE_ID", Value = ob.DYE_BT_STS_TYPE_ID},
                     new CommandParameter() {ParameterName = "pIS_EDT_LOCKED", Value = ob.IS_EDT_LOCKED},     
                     new CommandParameter() {ParameterName = "pDYE_BATCH_PLAN_ID", Value = ob.DYE_BATCH_PLAN_ID},                 
                     new CommandParameter() {ParameterName = "pCK_ROLL_RCV_DT", Value = ob.CK_ROLL_RCV_DT},
                     new CommandParameter() {ParameterName = "pCK_ROLL_FIN_DT", Value = ob.CK_ROLL_FIN_DT},
                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pOption", Value =5000},
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


        public string UpdateBarcode(Int64? pOption = 1000)
        {
            const string sp = "PKG_DYE_BATCH_PROGRAM.dye_bt_prod_barcode_update";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_BT_PROD_ID", Value = ob.DYE_BT_PROD_ID},
                     new CommandParameter() {ParameterName = "pPROD_DT", Value = ob.PROD_DT},
                     new CommandParameter() {ParameterName = "pDYE_BT_CARD_H_ID", Value = ob.DYE_BT_CARD_H_ID},
                     new CommandParameter() {ParameterName = "pDYE_STR_REQ_H_ID", Value = ob.DYE_STR_REQ_H_ID},                     
                     new CommandParameter() {ParameterName = "pDYE_RE_PROC_TYPE_ID", Value = ob.DYE_RE_PROC_TYPE_ID},
                     new CommandParameter() {ParameterName = "pDYE_MACHINE_ID", Value = ob.DYE_MACHINE_ID},                     
                     new CommandParameter() {ParameterName = "pLOAD_TIME", Value = ob.LOAD_TIME},
                     new CommandParameter() {ParameterName = "pUN_LOAD_TIME", Value = ob.UN_LOAD_TIME},
                     new CommandParameter() {ParameterName = "pBT_STOP_DESC", Value = ob.BT_STOP_DESC},
                     new CommandParameter() {ParameterName = "pCHK_ROLL_DT", Value = ob.CHK_ROLL_DT},
                     new CommandParameter() {ParameterName = "pCHECK_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pIS_ROLL_OK", Value = ob.IS_ROLL_OK},
                     new CommandParameter() {ParameterName = "pLK_CHQ_RL_STS_ID", Value = ob.LK_CHQ_RL_STS_ID},                     
                     new CommandParameter() {ParameterName = "pRF_DY_DFCT_TYPE_LST", Value = ob.RF_DY_DFCT_TYPE_LST},
                     new CommandParameter() {ParameterName = "pDF_PROC_TYP_LST", Value = ob.DF_PROC_TYP_LST},
                     new CommandParameter() {ParameterName = "pREMARKS", Value = ob.REMARKS},
                     new CommandParameter() {ParameterName = "pREQ_RE_PROC_TYPE_ID", Value = ob.REQ_RE_PROC_TYPE_ID},
                     new CommandParameter() {ParameterName = "pDYE_BT_STS_TYPE_ID", Value = ob.DYE_BT_STS_TYPE_ID},
                     new CommandParameter() {ParameterName = "pIS_EDT_LOCKED", Value = ob.IS_EDT_LOCKED},     
                     new CommandParameter() {ParameterName = "pDYE_BATCH_PLAN_ID", Value = ob.DYE_BATCH_PLAN_ID},                 
                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pUN_HOLD_DT", Value = ob.UN_HOLD_DT},     
                     new CommandParameter() {ParameterName = "pDFCT_TYPE_ID", Value = ob.DFCT_TYPE_ID},     
                     new CommandParameter() {ParameterName = "pIS_FINALIZED", Value = ob.IS_FINALIZED},                     
                     new CommandParameter() {ParameterName = "pRF_RESP_DEPT_ID", Value = ob.RF_RESP_DEPT_ID},                     
                     
                     new CommandParameter() {ParameterName = "pRIB_QC_DT", Value = ob.RIB_QC_DT},
                     new CommandParameter() {ParameterName = "pRIB_HOLD_DT", Value = ob.RIB_HOLD_DT},
                     new CommandParameter() {ParameterName = "pRIB_UN_HOLD_DT", Value = ob.RIB_UN_HOLD_DT},
                     new CommandParameter() {ParameterName = "pRIB_QC_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pIS_RIB_QC", Value = ob.IS_RIB_QC},
                     new CommandParameter() {ParameterName = "pLK_RIB_CHQ_RL_STS_ID", Value = ob.LK_RIB_CHQ_RL_STS_ID},                     
                     new CommandParameter() {ParameterName = "pRIB_DF_PROC_TYP_LST", Value = ob.RIB_DF_PROC_TYP_LST},
                     new CommandParameter() {ParameterName = "pRIB_REMARKS", Value = ob.RIB_REMARKS},

                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pOption", Value =pOption},
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


        public string UpdateRibBarcode(Int64? pOption = 1000)
        {
            const string sp = "PKG_DYE_BATCH_PROGRAM.dye_bt_prod_rib_barcode_update";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_BT_PROD_ID", Value = ob.DYE_BT_PROD_ID},
                     new CommandParameter() {ParameterName = "pPROD_DT", Value = ob.PROD_DT},
                     new CommandParameter() {ParameterName = "pDYE_BT_CARD_H_ID", Value = ob.DYE_BT_CARD_H_ID},
                     new CommandParameter() {ParameterName = "pDYE_STR_REQ_H_ID", Value = ob.DYE_STR_REQ_H_ID},                     
                     new CommandParameter() {ParameterName = "pDYE_RE_PROC_TYPE_ID", Value = ob.DYE_RE_PROC_TYPE_ID},
                     new CommandParameter() {ParameterName = "pDYE_MACHINE_ID", Value = ob.DYE_MACHINE_ID},                     
                     new CommandParameter() {ParameterName = "pLOAD_TIME", Value = ob.LOAD_TIME},
                     new CommandParameter() {ParameterName = "pUN_LOAD_TIME", Value = ob.UN_LOAD_TIME},
                     new CommandParameter() {ParameterName = "pBT_STOP_DESC", Value = ob.BT_STOP_DESC},
                     new CommandParameter() {ParameterName = "pCHK_ROLL_DT", Value = ob.CHK_ROLL_DT},
                     new CommandParameter() {ParameterName = "pCHECK_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pIS_ROLL_OK", Value = ob.IS_ROLL_OK},
                     new CommandParameter() {ParameterName = "pLK_CHQ_RL_STS_ID", Value = ob.LK_CHQ_RL_STS_ID},                     
                     new CommandParameter() {ParameterName = "pRF_DY_DFCT_TYPE_LST", Value = ob.RF_DY_DFCT_TYPE_LST},
                     new CommandParameter() {ParameterName = "pDF_PROC_TYP_LST", Value = ob.DF_PROC_TYP_LST},
                     new CommandParameter() {ParameterName = "pREMARKS", Value = ob.REMARKS},
                     new CommandParameter() {ParameterName = "pREQ_RE_PROC_TYPE_ID", Value = ob.REQ_RE_PROC_TYPE_ID},
                     new CommandParameter() {ParameterName = "pDYE_BT_STS_TYPE_ID", Value = ob.DYE_BT_STS_TYPE_ID},
                     new CommandParameter() {ParameterName = "pIS_EDT_LOCKED", Value = ob.IS_EDT_LOCKED},     
                     new CommandParameter() {ParameterName = "pDYE_BATCH_PLAN_ID", Value = ob.DYE_BATCH_PLAN_ID},                 
                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pUN_HOLD_DT", Value = ob.UN_HOLD_DT},     
                     new CommandParameter() {ParameterName = "pDFCT_TYPE_ID", Value = ob.DFCT_TYPE_ID},     
                     new CommandParameter() {ParameterName = "pIS_FINALIZED", Value = ob.IS_FINALIZED},                     
                     new CommandParameter() {ParameterName = "pRF_RESP_DEPT_ID", Value = ob.RF_RESP_DEPT_ID},                     
                     
                     new CommandParameter() {ParameterName = "pRIB_QC_DT", Value = ob.RIB_QC_DT},
                     new CommandParameter() {ParameterName = "pRIB_HOLD_DT", Value = ob.RIB_HOLD_DT},
                     new CommandParameter() {ParameterName = "pRIB_UN_HOLD_DT", Value = ob.RIB_UN_HOLD_DT},
                     new CommandParameter() {ParameterName = "pRIB_QC_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pIS_RIB_QC", Value = ob.IS_RIB_QC},
                     new CommandParameter() {ParameterName = "pLK_RIB_CHQ_RL_STS_ID", Value = ob.LK_RIB_CHQ_RL_STS_ID},                     
                     new CommandParameter() {ParameterName = "pRIB_RF_DY_DFCT_TYPE_LST", Value = ob.RIB_RF_DY_DFCT_TYPE_LST},
                     new CommandParameter() {ParameterName = "RIB_DF_PROC_TYP_LST", Value = ob.RIB_DF_PROC_TYP_LST},
                     new CommandParameter() {ParameterName = "pRIB_REMARKS", Value = ob.RIB_REMARKS},
                     
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pOption", Value =pOption},
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


        public string PreventionalMaintenance(Int64? pOption = 1000)
        {
            const string sp = "PKG_DYE_BATCH_PROGRAM.dye_bt_prod_PrevMTN_update";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_BT_PROD_ID", Value = ob.DYE_BT_PROD_ID},
                     new CommandParameter() {ParameterName = "pPROD_DT", Value = ob.PROD_DT},
                     new CommandParameter() {ParameterName = "pDYE_BT_CARD_H_ID", Value = ob.DYE_BT_CARD_H_ID},
                     new CommandParameter() {ParameterName = "pDYE_STR_REQ_H_ID", Value = ob.DYE_STR_REQ_H_ID},                     
                     new CommandParameter() {ParameterName = "pDYE_RE_PROC_TYPE_ID", Value = ob.DYE_RE_PROC_TYPE_ID},
                     new CommandParameter() {ParameterName = "pDYE_MACHINE_ID", Value = ob.DYE_MACHINE_ID},                     
                     new CommandParameter() {ParameterName = "pLOAD_TIME", Value = ob.LOAD_TIME},
                     new CommandParameter() {ParameterName = "pUN_LOAD_TIME", Value = ob.UN_LOAD_TIME},
                     new CommandParameter() {ParameterName = "pCHK_ROLL_DT", Value = ob.CHK_ROLL_DT},
                     new CommandParameter() {ParameterName = "pCHECK_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pIS_ROLL_OK", Value = ob.IS_ROLL_OK},
                     new CommandParameter() {ParameterName = "pLK_CHQ_RL_STS_ID", Value = ob.LK_CHQ_RL_STS_ID},                     
                     new CommandParameter() {ParameterName = "pRF_DY_DFCT_TYPE_LST", Value = ob.RF_DY_DFCT_TYPE_LST},
                     new CommandParameter() {ParameterName = "pREMARKS", Value = ob.REMARKS},
                     new CommandParameter() {ParameterName = "pREQ_RE_PROC_TYPE_ID", Value = ob.REQ_RE_PROC_TYPE_ID},
                     new CommandParameter() {ParameterName = "pDYE_BT_STS_TYPE_ID", Value = ob.DYE_BT_STS_TYPE_ID},
                     new CommandParameter() {ParameterName = "pIS_EDT_LOCKED", Value = ob.IS_EDT_LOCKED},     
                     new CommandParameter() {ParameterName = "pDYE_BATCH_PLAN_ID", Value = ob.DYE_BATCH_PLAN_ID},                 
                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pUN_HOLD_DT", Value = ob.UN_HOLD_DT},     
                     new CommandParameter() {ParameterName = "pDFCT_TYPE_ID", Value = ob.DFCT_TYPE_ID},     
                     
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pOption", Value =pOption},
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


        public string BatchQCStatusUpdate()
        {
            const string sp = "PKG_DYE_BATCH_PROGRAM.dye_bt_prod_final_qc_update";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_BT_PROD_ID", Value = ob.DYE_BT_PROD_ID},
                     new CommandParameter() {ParameterName = "pPROD_DT", Value = ob.PROD_DT},
                     new CommandParameter() {ParameterName = "pDYE_BT_CARD_H_ID", Value = ob.DYE_BT_CARD_H_ID},
                     new CommandParameter() {ParameterName = "pDYE_STR_REQ_H_ID", Value = ob.DYE_STR_REQ_H_ID},                     
                     new CommandParameter() {ParameterName = "pDYE_RE_PROC_TYPE_ID", Value = ob.DYE_RE_PROC_TYPE_ID},
                     new CommandParameter() {ParameterName = "pDYE_MACHINE_ID", Value = ob.DYE_MACHINE_ID},                     
                     new CommandParameter() {ParameterName = "pLOAD_TIME", Value = ob.LOAD_TIME},
                     new CommandParameter() {ParameterName = "pUN_LOAD_TIME", Value = ob.UN_LOAD_TIME},
                     new CommandParameter() {ParameterName = "pCHK_ROLL_DT", Value = ob.CHK_ROLL_DT},
                     new CommandParameter() {ParameterName = "pCHECK_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pIS_ROLL_OK", Value = ob.IS_ROLL_OK},
                     new CommandParameter() {ParameterName = "pLK_CHQ_RL_STS_ID", Value = ob.LK_CHQ_RL_STS_ID},                     
                     new CommandParameter() {ParameterName = "pRF_DY_DFCT_TYPE_LST", Value = ob.RF_DY_DFCT_TYPE_LST},
                     new CommandParameter() {ParameterName = "pREMARKS", Value = ob.REMARKS},
                     new CommandParameter() {ParameterName = "pREQ_RE_PROC_TYPE_ID", Value = ob.REQ_RE_PROC_TYPE_ID},
                     new CommandParameter() {ParameterName = "pDYE_BT_STS_TYPE_ID", Value = ob.DYE_BT_STS_TYPE_ID},
                     new CommandParameter() {ParameterName = "pIS_EDT_LOCKED", Value = ob.IS_EDT_LOCKED},     
                     new CommandParameter() {ParameterName = "pDYE_BATCH_PLAN_ID", Value = ob.DYE_BATCH_PLAN_ID},                 
                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pUN_HOLD_DT", Value = ob.UN_HOLD_DT},     
                     new CommandParameter() {ParameterName = "pDFCT_TYPE_ID", Value = ob.DFCT_TYPE_ID},     
                     
                     new CommandParameter() {ParameterName = "pIS_FINALIZED", Value = ob.IS_FINALIZED},     
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

        public string Delete()
        {
            const string sp = "SP_DYE_BT_PROD";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_BT_PROD_ID", Value = ob.DYE_BT_PROD_ID},
                     new CommandParameter() {ParameterName = "pPROD_DT", Value = ob.PROD_DT},
                     new CommandParameter() {ParameterName = "pDYE_BT_CARD_H_ID", Value = ob.DYE_BT_CARD_H_ID},
                     new CommandParameter() {ParameterName = "pDYE_RE_PROC_TYPE_ID", Value = ob.DYE_RE_PROC_TYPE_ID},
                     new CommandParameter() {ParameterName = "pLOAD_TIME", Value = ob.LOAD_TIME},
                     new CommandParameter() {ParameterName = "pUN_LOAD_TIME", Value = ob.UN_LOAD_TIME},
                     new CommandParameter() {ParameterName = "pCHK_ROLL_DT", Value = ob.CHK_ROLL_DT},
                     new CommandParameter() {ParameterName = "pCHECK_BY", Value = ob.CHECK_BY},
                     new CommandParameter() {ParameterName = "pIS_ROLL_OK", Value = ob.IS_ROLL_OK},
                     new CommandParameter() {ParameterName = "pRF_DY_DFCT_TYPE_LST", Value = ob.RF_DY_DFCT_TYPE_LST},
                     new CommandParameter() {ParameterName = "pREMARKS", Value = ob.REMARKS},
                     new CommandParameter() {ParameterName = "pREQ_RE_PROC_TYPE_ID", Value = ob.REQ_RE_PROC_TYPE_ID},
                     new CommandParameter() {ParameterName = "pDYE_BT_STS_TYPE_ID", Value = ob.DYE_BT_STS_TYPE_ID},
                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = ob.CREATED_BY},
                     new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = ob.LAST_UPDATED_BY},
                     new CommandParameter() {ParameterName = "pOption", Value =4000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);

                foreach (DataRow dr in ds.Tables["OUTPARAM"].Rows)
                {
                    jsonStr += dr["KEY"].ToString() + ":" + dr["VALUE"].ToString() + ",";
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

        public List<DYE_BT_PRODModel> SelectAll()
        {
            string sp = "Select_DYE_BT_PROD";
            try
            {
                var obList = new List<DYE_BT_PRODModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_BT_PROD_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DYE_BT_PRODModel ob = new DYE_BT_PRODModel();
                    ob.DYE_BT_PROD_ID = (dr["DYE_BT_PROD_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_BT_PROD_ID"]);
                    ob.PROD_DT = (dr["PROD_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["PROD_DT"]);
                    ob.DYE_BT_CARD_H_ID = (dr["DYE_BT_CARD_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_BT_CARD_H_ID"]);
                    ob.DYE_RE_PROC_TYPE_ID = (dr["DYE_RE_PROC_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_RE_PROC_TYPE_ID"]);
                    ob.LOAD_TIME = (dr["LOAD_TIME"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LOAD_TIME"]);
                    ob.UN_LOAD_TIME = (dr["UN_LOAD_TIME"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["UN_LOAD_TIME"]);
                    ob.CHK_ROLL_DT = (dr["CHK_ROLL_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CHK_ROLL_DT"]);
                    ob.CHECK_BY = (dr["CHECK_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CHECK_BY"]);
                    ob.IS_ROLL_OK = (dr["IS_ROLL_OK"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ROLL_OK"]);
                    ob.RF_DY_DFCT_TYPE_LST = (dr["RF_DY_DFCT_TYPE_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["RF_DY_DFCT_TYPE_LST"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                    ob.REQ_RE_PROC_TYPE_ID = (dr["REQ_RE_PROC_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["REQ_RE_PROC_TYPE_ID"]);
                    ob.DYE_BT_STS_TYPE_ID = (dr["DYE_BT_STS_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_BT_STS_TYPE_ID"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);

                    if (dr["IS_FINALIZED"] != DBNull.Value)
                        ob.IS_FINALIZED = (dr["IS_FINALIZED"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_FINALIZED"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DYE_BT_PRODModel> Select(long ID)
        {
            string sp = "Select_DYE_BT_PROD";
            try
            {
                var obList = new List<DYE_BT_PRODModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_BT_PROD_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3003},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DYE_BT_PRODModel ob = new DYE_BT_PRODModel();
                    ob.DYE_BT_PROD_ID = (dr["DYE_BT_PROD_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_BT_PROD_ID"]);
                    ob.PROD_DT = (dr["PROD_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["PROD_DT"]);
                    ob.DYE_BT_CARD_H_ID = (dr["DYE_BT_CARD_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_BT_CARD_H_ID"]);
                    ob.DYE_RE_PROC_TYPE_ID = (dr["DYE_RE_PROC_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_RE_PROC_TYPE_ID"]);
                    ob.LOAD_TIME = (dr["LOAD_TIME"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LOAD_TIME"]);
                    ob.UN_LOAD_TIME = (dr["UN_LOAD_TIME"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["UN_LOAD_TIME"]);
                    ob.CHK_ROLL_DT = (dr["CHK_ROLL_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CHK_ROLL_DT"]);
                    ob.CHECK_BY = (dr["CHECK_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CHECK_BY"]);
                    ob.IS_ROLL_OK = (dr["IS_ROLL_OK"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ROLL_OK"]);
                    ob.RF_DY_DFCT_TYPE_LST = (dr["RF_DY_DFCT_TYPE_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["RF_DY_DFCT_TYPE_LST"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                    ob.REQ_RE_PROC_TYPE_ID = (dr["REQ_RE_PROC_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["REQ_RE_PROC_TYPE_ID"]);
                    ob.DYE_BT_STS_TYPE_ID = (dr["DYE_BT_STS_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_BT_STS_TYPE_ID"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);

                    if (dr["IS_FINALIZED"] != DBNull.Value)
                        ob.IS_FINALIZED = (dr["IS_FINALIZED"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_FINALIZED"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DYE_BT_PRODModel> SelectBatch(Int64? pPageNo = null, Int64? pMC_BYR_ACC_ID = null, string pSTYLE_ORDER_NO = null, Int64? pMC_COLOR_ID = null, string pDYE_MACHINE_NO = null, string pDYE_BATCH_NO = null, Int64? pDYE_BT_STS_TYPE_ID = null, Int64? pRF_FAB_PROD_CAT_ID = null, Int64? pLK_CHQ_RL_STS_ID = null)
        {
            string sp = "pkg_dye_batch_program.dye_bt_card_h_select";
            try
            {
                var obList = new List<DYE_BT_PRODModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_BYR_ACC_ID", Value =pMC_BYR_ACC_ID},
                     new CommandParameter() {ParameterName = "pSTYLE_ORDER_NO", Value =pSTYLE_ORDER_NO},
                     new CommandParameter() {ParameterName = "pMC_COLOR_ID", Value =pMC_COLOR_ID},
                     new CommandParameter() {ParameterName = "pDYE_MACHINE_NO", Value =pDYE_MACHINE_NO},
                     new CommandParameter() {ParameterName = "pDYE_BATCH_NO", Value =pDYE_BATCH_NO},
                     new CommandParameter() {ParameterName = "pDYE_BT_STS_TYPE_ID", Value =pDYE_BT_STS_TYPE_ID},
                     new CommandParameter() {ParameterName = "pRF_FAB_PROD_CAT_ID", Value =pRF_FAB_PROD_CAT_ID},
                     new CommandParameter() {ParameterName = "pLK_CHQ_RL_STS_ID", Value =pLK_CHQ_RL_STS_ID},
                     
                     new CommandParameter() {ParameterName = "pPageNo", Value =pPageNo},
                     new CommandParameter() {ParameterName = "pOption", Value =3003},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DYE_BT_PRODModel ob = new DYE_BT_PRODModel();

                    //ob.DYE_BT_CARD_H_ID = (dr["DYE_BT_CARD_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_BT_CARD_H_ID"]);
                    ob.DYE_BATCH_PLAN_ID = (dr["DYE_BATCH_PLAN_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_BATCH_PLAN_ID"]);
                    ob.MC_LD_RECIPE_H_ID = (dr["MC_LD_RECIPE_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_LD_RECIPE_H_ID"]);
                    //ob.LQR_RATIO = (dr["LQR_RATIO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LQR_RATIO"]);
                    ob.DYE_MACHINE_ID = (dr["DYE_MACHINE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_MACHINE_ID"]);

                    //ob.DYE_LOT_NO = (dr["DYE_LOT_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DYE_LOT_NO"]);
                    ob.DYE_BATCH_NO = (dr["DYE_BATCH_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DYE_BATCH_NO"]);

                    //ob.BATCH_REQ_DT = (dr["BATCH_REQ_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["BATCH_REQ_DT"]);
                    ob.ACT_BATCH_QTY = (dr["ACT_BATCH_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["ACT_BATCH_QTY"]);

                    ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);
                    ob.MERGE_BT_CRD_ID = (dr["MERGE_BT_CRD_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MERGE_BT_CRD_ID"]);
                    ob.LK_SHIFT_TYPE_ID = (dr["LK_SHIFT_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_SHIFT_TYPE_ID"]);

                    //ob.ACT_LOAD_TIME = (dr["ACT_LOAD_TIME"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["ACT_LOAD_TIME"]);
                    //ob.ACT_UN_LOAD_TIME = (dr["ACT_UN_LOAD_TIME"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["ACT_UN_LOAD_TIME"]);
                    ob.OPERATOR_ID = (dr["OPERATOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["OPERATOR_ID"]);

                    ob.IS_EDT_LOCKED = (dr["IS_EDT_LOCKED"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_EDT_LOCKED"]);

                    ob.DYE_STR_REQ_H_ID = (dr["DYE_STR_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_STR_REQ_H_ID"]);
                    ob.LK_CHQ_RL_STS_ID = (dr["LK_CHQ_RL_STS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_CHQ_RL_STS_ID"]);

                    ob.DYE_MACHINE_NO = (dr["DYE_MACHINE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DYE_MACHINE_NO"]);
                    ob.BUYER_NAME_EN = (dr["BUYER_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BUYER_NAME_EN"]);
                    ob.STYLE_NO = (dr["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO"]);
                    ob.BT_STS_TYP_NAME = (dr["BT_STS_TYP_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BT_STS_TYP_NAME"]);
                    ob.COLOR_GRP_NAME_EN = (dr["COLOR_GRP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_GRP_NAME_EN"]);
                    ob.ORDER_NO = (dr["ORDER_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_NO"]);
                    ob.COLOR_NAME_EN = (dr["COLOR_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_NAME_EN"]);
                    ob.LD_RECIPE_NO = (dr["LD_RECIPE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LD_RECIPE_NO"]);
                    ob.DYE_MTHD_NAME = (dr["DYE_MTHD_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DYE_MTHD_NAME"]);
                    ob.FABRIC_SNAME = (dr["FABRIC_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FABRIC_SNAME"]);

                    ob.BYR_ACC_NAME_EN = (dr["BYR_ACC_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BYR_ACC_NAME_EN"]);

                    ob.MC_COLOR_GRP_ID = (dr["MC_COLOR_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_COLOR_GRP_ID"]);

                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);

                    ob.DYE_BT_PROD_ID = (dr["DYE_BT_PROD_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_BT_PROD_ID"]);
                    ob.PROD_DT = (dr["PROD_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["PROD_DT"]);
                    //ob.DYE_BT_CARD_H_ID = (dr["DYE_BT_CARD_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_BT_CARD_H_ID"]);
                    ob.DYE_RE_PROC_TYPE_ID = (dr["DYE_RE_PROC_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_RE_PROC_TYPE_ID"]);
                    if (dr["LOAD_TIME"] != DBNull.Value)
                        ob.LOAD_TIME = Convert.ToDateTime(dr["LOAD_TIME"]);
                    if (dr["UN_LOAD_TIME"] != DBNull.Value)
                        ob.UN_LOAD_TIME = Convert.ToDateTime(dr["UN_LOAD_TIME"]);

                    if (dr["RE_PROC_TYPE_NAME"] != DBNull.Value)
                        ob.RE_PROC_TYPE_NAME = Convert.ToString(dr["RE_PROC_TYPE_NAME"]);

                    //ob.UN_LOAD_TIME = (dr["UN_LOAD_TIME"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["UN_LOAD_TIME"]);
                    ob.CHK_ROLL_DT = (dr["CHK_ROLL_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CHK_ROLL_DT"]);
                    ob.CHECK_BY = (dr["CHECK_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CHECK_BY"]);
                    ob.IS_ROLL_OK = (dr["IS_ROLL_OK"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ROLL_OK"]);
                    ob.RF_DY_DFCT_TYPE_LST = (dr["RF_DY_DFCT_TYPE_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["RF_DY_DFCT_TYPE_LST"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                    ob.REQ_RE_PROC_TYPE_ID = (dr["REQ_RE_PROC_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["REQ_RE_PROC_TYPE_ID"]);
                    ob.DYE_BT_STS_TYPE_ID = (dr["DYE_BT_STS_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_BT_STS_TYPE_ID"]);
                    ob.IS_DISABLE = (dr["IS_DISABLE"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["IS_DISABLE"]);
                    ob.TOTAL_REC = (dr["TOTAL_REC"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["TOTAL_REC"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<DYE_BT_PRODModel> GetBatchForProduction(string pDYE_BATCH_NO = null, Int64? pDYE_STR_REQ_H_ID = null, Int64? pOption = null)
        {
            string sp = "pkg_dye_batch_program.scan_dye_bt_card_select";
            try
            {
                var obList = new List<DYE_BT_PRODModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_BATCH_NO", Value =pDYE_BATCH_NO}, 
                     new CommandParameter() {ParameterName = "pDF_BT_SUB_LOT_LST", Value =pDYE_BATCH_NO}, 
                     new CommandParameter() {ParameterName = "pPRE_DYE_STR_REQ_H_ID", Value =pDYE_STR_REQ_H_ID}, 
                     
                     new CommandParameter() {ParameterName = "pOption", Value =pOption},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DYE_BT_PRODModel ob = new DYE_BT_PRODModel();

                    if (pOption == 3001 || pOption == 3005 | pOption == 3007)
                    {
                        ob.FABRIC_DESC = (dr["FABRIC_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FABRIC_DESC"]);
                    }

                    else if (pOption == 3011)
                    {
                        ob.FABRIC_DESC = (dr["FABRIC_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FABRIC_DESC"]);
                        ob.LK_FBR_GRP_ID = (dr["LK_FBR_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_FBR_GRP_ID"]);
                    }
                    else if (pOption == 3002)
                    {
                        ob.DYE_MACHINE_NO = (dr["DYE_MACHINE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DYE_MACHINE_NO"]);
                        if (dr["LOAD_TIME"] != DBNull.Value)
                            ob.LOAD_TIME = Convert.ToDateTime(dr["LOAD_TIME"]);
                        if (dr["UN_LOAD_TIME"] != DBNull.Value)
                            ob.UN_LOAD_TIME = Convert.ToDateTime(dr["UN_LOAD_TIME"]);

                        if (dr["CK_ROLL_RCV_DT"] != DBNull.Value)
                            ob.CK_ROLL_RCV_DT = Convert.ToDateTime(dr["CK_ROLL_RCV_DT"]);
                        if (dr["CK_ROLL_FIN_DT"] != DBNull.Value)
                            ob.CK_ROLL_FIN_DT = Convert.ToDateTime(dr["CK_ROLL_FIN_DT"]);
                        if (dr["UN_HOLD_DT"] != DBNull.Value)
                            ob.UN_HOLD_DT = Convert.ToDateTime(dr["UN_HOLD_DT"]);

                        ob.TIME_DIFF = (dr["TIME_DIFF"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["TIME_DIFF"]);
                        ob.IS_EDT_LOCKED = (dr["IS_EDT_LOCKED"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_EDT_LOCKED"]);
                        ob.IS_BP_LOCKED = (dr["IS_BP_LOCKED"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_BP_LOCKED"]);
                        
                        ob.DYE_BT_PROD_ID = (dr["DYE_BT_PROD_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_BT_PROD_ID"]);
                        ob.DYE_MACHINE_ID = (dr["DYE_MACHINE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_MACHINE_ID"]);
                        ob.DYE_BT_STS_TYPE_ID = (dr["DYE_BT_STS_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_BT_STS_TYPE_ID"]);
                        ob.LK_CHQ_RL_STS_ID = (dr["LK_CHQ_RL_STS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_CHQ_RL_STS_ID"]);
                        ob.IS_DISABLE = (dr["IS_DISABLE"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["IS_DISABLE"]);
                        ob.RE_PROC_TYPE_NAME = (dr["RE_PROC_TYPE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["RE_PROC_TYPE_NAME"]);
                        ob.BT_STS_TYP_NAME = (dr["BT_STS_TYP_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BT_STS_TYP_NAME"]);
                        ob.IS_RPROC_DONE = (dr["IS_RPROC_DONE"] == DBNull.Value) ? "Y" : Convert.ToString(dr["IS_RPROC_DONE"]);
                        ob.RF_DY_DFCT_TYPE_LST = (dr["RF_DY_DFCT_TYPE_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["RF_DY_DFCT_TYPE_LST"]);
                        ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);

                        ob.IS_ROLL_OK = (dr["IS_ROLL_OK"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ROLL_OK"]);
                        ob.STR_REQ_NO = (dr["STR_REQ_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STR_REQ_NO"]);
                        ob.DF_PROC_TYP_LST = (dr["DF_PROC_TYP_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DF_PROC_TYP_LST"]);

                        ob.IS_RIB_QC = (dr["IS_RIB_QC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_RIB_QC"]);
                        ob.RIB_QC_DT = (dr["RIB_QC_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["RIB_QC_DT"]);
                        ob.RIB_QC_BY = (dr["RIB_QC_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RIB_QC_BY"]);
                        ob.RIB_HOLD_DT = (dr["RIB_HOLD_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["RIB_HOLD_DT"]);
                        ob.RIB_UN_HOLD_DT = (dr["RIB_UN_HOLD_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["RIB_UN_HOLD_DT"]);
                        ob.LK_RIB_CHQ_RL_STS_ID = (dr["LK_RIB_CHQ_RL_STS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_RIB_CHQ_RL_STS_ID"]);
                        ob.RIB_DF_PROC_TYP_LST = (dr["RIB_DF_PROC_TYP_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["RIB_DF_PROC_TYP_LST"]);
                        ob.RIB_REMARKS = (dr["RIB_REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["RIB_REMARKS"]);
                        ob.RIB_RF_DY_DFCT_TYPE_LST = (dr["RIB_RF_DY_DFCT_TYPE_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["RIB_RF_DY_DFCT_TYPE_LST"]);

                    }
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DYE_BT_DC_REQ_D1Model> GetBatchForProductionBI(string pDYE_BATCH_NO = null)
        {
            string sp = "pkg_dye_batch_program.scan_dye_bt_card_select";
            try
            {
                var obList = new List<DYE_BT_DC_REQ_D1Model>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_BATCH_NO", Value = pDYE_BATCH_NO},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DYE_BT_DC_REQ_D1Model ob = new DYE_BT_DC_REQ_D1Model();
                    ob.DYE_STR_REQ_H_ID = (dr["DYE_STR_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_STR_REQ_H_ID"]);
                    ob.DYE_BT_CARD_H_ID = (dr["DYE_BT_CARD_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_BT_CARD_H_ID"]);
                    ob.LQR_RATIO = (dr["LQR_RATIO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LQR_RATIO"]);
                    ob.DYE_BATCH_NO = (dr["DYE_BATCH_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DYE_BATCH_NO"]);
                    ob.ACT_BATCH_QTY = (dr["ACT_BATCH_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ACT_BATCH_QTY"]);
                    ob.DYE_MACHINE_ID = (dr["DYE_MACHINE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_MACHINE_ID"]);

                    if (dr["DYE_RE_PROC_TYPE_ID"] != DBNull.Value)
                        ob.DYE_RE_PROC_TYPE_ID = Convert.ToInt64(dr["DYE_RE_PROC_TYPE_ID"]);

                    ob.FAB_COLOR_ID = (dr["FAB_COLOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["FAB_COLOR_ID"]);
                    
                    ob.DYE_MACHINE_NO = (dr["DYE_MACHINE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DYE_MACHINE_NO"]);
                    ob.BUYER_NAME_EN = (dr["BUYER_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BUYER_NAME_EN"]);
                    ob.STYLE_NO = (dr["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO"]);
                    ob.ORDER_NO = (dr["ORDER_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_NO"]);
                    ob.COLOR_NAME_EN = (dr["COLOR_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_NAME_EN"]);
                    ob.LD_RECIPE_NO = (dr["LD_RECIPE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LD_RECIPE_NO"]);
                    ob.COLOR_GRP_NAME_EN = (dr["COLOR_GRP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_GRP_NAME_EN"]);
                    ob.STR_REQ_NO = (dr["STR_REQ_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STR_REQ_NO"]);
                    ob.STR_REQ_DT = (dr["STR_REQ_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["STR_REQ_DT"]);
                    ob.DYE_MTHD_NAME = (dr["DYE_MTHD_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DYE_MTHD_NAME"]);

                    ob.ACTN_ROLE_FLAG = (dr["ACTN_ROLE_FLAG"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ACTN_ROLE_FLAG"]);
                    ob.DF_PROC_TYP_LST = (dr["DF_PROC_TYP_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DF_PROC_TYP_LST"]);
                    ob.RP_SL_NO = (dr["RP_SL_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RP_SL_NO"]);
                    
                    if (dr["CK_ROLL_RCV_DT"] != DBNull.Value)
                        ob.CK_ROLL_RCV_DT = Convert.ToDateTime(dr["CK_ROLL_RCV_DT"]);
                    if (dr["CK_ROLL_FIN_DT"] != DBNull.Value)
                        ob.CK_ROLL_FIN_DT = Convert.ToDateTime(dr["CK_ROLL_FIN_DT"]);
                    if (dr["UN_HOLD_DT"] != DBNull.Value)
                        ob.UN_HOLD_DT = Convert.ToDateTime(dr["UN_HOLD_DT"]);
                    if (dr["CHK_ROLL_DT"] != DBNull.Value)
                        ob.CHK_ROLL_DT = Convert.ToDateTime(dr["CHK_ROLL_DT"]);

                    ob.LK_CHQ_RL_STS_ID = (dr["LK_CHQ_RL_STS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_CHQ_RL_STS_ID"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<DYE_BT_DC_REQ_D1Model> GetBatchForDF(string pDYE_BATCH_NO = null, Int64? pLK_DIA_TYPE_ID = null, string pLK_FBR_GRP_LST = null)
        {
            string sp = "pkg_dye_batch_program.scan_dye_bt_card_select";
            try
            {
                var obList = new List<DYE_BT_DC_REQ_D1Model>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_BATCH_NO", Value = pDYE_BATCH_NO},
                     new CommandParameter() {ParameterName = "pLK_DIA_TYPE_ID", Value = pLK_DIA_TYPE_ID},
                     new CommandParameter() {ParameterName = "pLK_FBR_GRP_LST", Value = pLK_FBR_GRP_LST},
                     new CommandParameter() {ParameterName = "pOption", Value =3004},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DYE_BT_DC_REQ_D1Model ob = new DYE_BT_DC_REQ_D1Model();
                    //ob.DYE_STR_REQ_H_ID = (dr["DYE_STR_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_STR_REQ_H_ID"]);

                    //ob.LQR_RATIO = (dr["LQR_RATIO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LQR_RATIO"]);
                    ob.DYE_BATCH_NO = (dr["DYE_BATCH_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DYE_BATCH_NO"]);
                    ob.ACT_BATCH_QTY = (dr["ACT_BATCH_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ACT_BATCH_QTY"]);
                    ob.DYE_BT_CARD_H_ID = (dr["DYE_BT_CARD_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_BT_CARD_H_ID"]);
                    ob.DYE_MACHINE_ID = (dr["DYE_MACHINE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_MACHINE_ID"]);

                    ob.DF_BT_SUB_LOT_ID = (dr["DF_BT_SUB_LOT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_BT_SUB_LOT_ID"]);

                    ob.BATCH_QTY = (dr["BATCH_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["BATCH_QTY"]);
                    ob.DIA_TYPE_NAME = (dr["DIA_TYPE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DIA_TYPE_NAME"]);
                    
                    ob.DYE_BATCH_PLAN_ID = (dr["DYE_BATCH_PLAN_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_BATCH_PLAN_ID"]);

                    ob.DYE_MACHINE_NO = (dr["DYE_MACHINE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DYE_MACHINE_NO"]);
                    ob.BUYER_NAME_EN = (dr["BUYER_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BUYER_NAME_EN"]);
                    ob.STYLE_NO = (dr["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO"]);
                    ob.ORDER_NO = (dr["ORDER_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_NO"]);
                    ob.COLOR_NAME_EN = (dr["COLOR_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_NAME_EN"]);
                    ob.LD_RECIPE_NO = (dr["LD_RECIPE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LD_RECIPE_NO"]);
                    ob.COLOR_GRP_NAME_EN = (dr["COLOR_GRP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_GRP_NAME_EN"]);
                    //ob.STR_REQ_NO = (dr["STR_REQ_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STR_REQ_NO"]);
                    //ob.STR_REQ_DT = (dr["STR_REQ_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["STR_REQ_DT"]);
                    ob.DYE_MTHD_NAME = (dr["DYE_MTHD_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DYE_MTHD_NAME"]);
                    ob.DYE_LOT_NO = (dr["DYE_LOT_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DYE_LOT_NO"]);
                    ob.FAB_TYPE_NAME = (dr["FAB_TYPE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FAB_TYPE_NAME"]);
                    ob.RF_FAB_TYPE_ID = (dr["RF_FAB_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_FAB_TYPE_ID"]);
                    ob.FAB_COLOR_ID = (dr["FAB_COLOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["FAB_COLOR_ID"]);
                    ob.MC_FAB_PROD_ORD_H_ID = (dr["MC_FAB_PROD_ORD_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_FAB_PROD_ORD_H_ID"]);

                    if (dr["IS_FINALIZED"] != DBNull.Value)
                        ob.IS_FINALIZED = (dr["IS_FINALIZED"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_FINALIZED"]);

                    ob.SPECIAL_INSTRUCTION = (dr["SPECIAL_INSTRUCTION"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SPECIAL_INSTRUCTION"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<DYE_BT_DC_REQ_D1Model> GetBatchForDfQc(string pDYE_BATCH_NO = null)
        {
            string sp = "pkg_dye_batch_program.scan_dye_bt_card_select";
            try
            {
                var obList = new List<DYE_BT_DC_REQ_D1Model>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_BATCH_NO", Value = pDYE_BATCH_NO},
                     new CommandParameter() {ParameterName = "pOption", Value =3008},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DYE_BT_DC_REQ_D1Model ob = new DYE_BT_DC_REQ_D1Model();
                    //ob.DYE_STR_REQ_H_ID = (dr["DYE_STR_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_STR_REQ_H_ID"]);

                    //ob.LQR_RATIO = (dr["LQR_RATIO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LQR_RATIO"]);
                    ob.DYE_BATCH_NO = (dr["DYE_BATCH_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DYE_BATCH_NO"]);
                    ob.ACT_BATCH_QTY = (dr["ACT_BATCH_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ACT_BATCH_QTY"]);
                    ob.DYE_BT_CARD_H_ID = (dr["DYE_BT_CARD_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_BT_CARD_H_ID"]);
                    ob.DYE_MACHINE_ID = (dr["DYE_MACHINE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_MACHINE_ID"]);

                    ob.DF_BT_SUB_LOT_ID = (dr["DF_BT_SUB_LOT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_BT_SUB_LOT_ID"]);

                    ob.BATCH_QTY = (dr["BATCH_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["BATCH_QTY"]);
                    ob.DIA_TYPE_NAME = (dr["DIA_TYPE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DIA_TYPE_NAME"]);

                    ob.DYE_BATCH_PLAN_ID = (dr["DYE_BATCH_PLAN_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_BATCH_PLAN_ID"]);

                    ob.DYE_MACHINE_NO = (dr["DYE_MACHINE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DYE_MACHINE_NO"]);
                    ob.BUYER_NAME_EN = (dr["BUYER_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BUYER_NAME_EN"]);
                    ob.STYLE_NO = (dr["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO"]);
                    ob.ORDER_NO = (dr["ORDER_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_NO"]);
                    ob.COLOR_NAME_EN = (dr["COLOR_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_NAME_EN"]);
                    ob.LD_RECIPE_NO = (dr["LD_RECIPE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LD_RECIPE_NO"]);
                    ob.COLOR_GRP_NAME_EN = (dr["COLOR_GRP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_GRP_NAME_EN"]);
                    //ob.STR_REQ_NO = (dr["STR_REQ_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STR_REQ_NO"]);
                    //ob.STR_REQ_DT = (dr["STR_REQ_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["STR_REQ_DT"]);
                    ob.DYE_MTHD_NAME = (dr["DYE_MTHD_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DYE_MTHD_NAME"]);
                    ob.DYE_LOT_NO = (dr["DYE_LOT_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DYE_LOT_NO"]);
                    ob.FAB_TYPE_NAME = (dr["FAB_TYPE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FAB_TYPE_NAME"]);
                    ob.RF_FAB_TYPE_ID = (dr["RF_FAB_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_FAB_TYPE_ID"]);
                    ob.FAB_COLOR_ID = (dr["FAB_COLOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["FAB_COLOR_ID"]);
                    ob.MC_FAB_PROD_ORD_H_ID = (dr["MC_FAB_PROD_ORD_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_FAB_PROD_ORD_H_ID"]);
                    ob.LAST_DF_PROC_TYPE_ID = (dr["LAST_DF_PROC_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_DF_PROC_TYPE_ID"]);

                    ob.BT_STS_TYP_NAME = (dr["BT_STS_TYP_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BT_STS_TYP_NAME"]);
                    
                    if (dr["IS_FINALIZED"] != DBNull.Value)
                        ob.IS_FINALIZED = (dr["IS_FINALIZED"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_FINALIZED"]);

                    ob.SPECIAL_INSTRUCTION = (dr["SPECIAL_INSTRUCTION"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SPECIAL_INSTRUCTION"]);
                    ob.HR_COMPANY_ID = (dr["HR_COMPANY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COMPANY_ID"]);
                    
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<DYE_BT_DC_REQ_D1Model> GetHoldQCList(Int64? pMC_BYR_ACC_GRP_ID = null, string pDYE_BATCH_NO = null, string pORDER_NO = null, string pCHECK_ROLL_DT = null)
        {
            string sp = "pkg_dye_batch_program.scan_dye_bt_card_select";
            try
            {
                var obList = new List<DYE_BT_DC_REQ_D1Model>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_BYR_ACC_GRP_ID", Value = pMC_BYR_ACC_GRP_ID},
                     new CommandParameter() {ParameterName = "pDYE_BATCH_NO", Value = pDYE_BATCH_NO},
                     new CommandParameter() {ParameterName = "pORDER_NO", Value = pORDER_NO},
                     new CommandParameter() {ParameterName = "pFROM_DATE", Value = pCHECK_ROLL_DT},
                     new CommandParameter() {ParameterName = "pOption", Value =3003},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DYE_BT_DC_REQ_D1Model ob = new DYE_BT_DC_REQ_D1Model();
                    ob.DYE_STR_REQ_H_ID = (dr["DYE_STR_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_STR_REQ_H_ID"]);

                    ob.LQR_RATIO = (dr["LQR_RATIO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LQR_RATIO"]);
                    ob.DYE_BATCH_NO = (dr["DYE_BATCH_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DYE_BATCH_NO"]);
                    ob.ACT_BATCH_QTY = (dr["ACT_BATCH_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ACT_BATCH_QTY"]);
                    ob.DYE_MACHINE_ID = (dr["DYE_MACHINE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_MACHINE_ID"]);

                    ob.DYE_MACHINE_NO = (dr["DYE_MACHINE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DYE_MACHINE_NO"]);
                    ob.BUYER_NAME_EN = (dr["BUYER_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BUYER_NAME_EN"]);
                    ob.STYLE_NO = (dr["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO"]);
                    ob.ORDER_NO = (dr["ORDER_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_NO"]);
                    ob.COLOR_NAME_EN = (dr["COLOR_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_NAME_EN"]);
                    ob.LD_RECIPE_NO = (dr["LD_RECIPE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LD_RECIPE_NO"]);
                    ob.COLOR_GRP_NAME_EN = (dr["COLOR_GRP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_GRP_NAME_EN"]);
                    ob.STR_REQ_NO = (dr["STR_REQ_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STR_REQ_NO"]);
                    ob.STR_REQ_DT = (dr["STR_REQ_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["STR_REQ_DT"]);
                    ob.DYE_MTHD_NAME = (dr["DYE_MTHD_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DYE_MTHD_NAME"]);
                    ob.CHK_ROLL_DT = (dr["CHK_ROLL_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CHK_ROLL_DT"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }




        public string RE_PROC_TYPE_NAME { get; set; }
        public string FABRIC_DESC { get; set; }
        public Decimal? TIME_DIFF { get; set; }
        public string IS_RPROC_DONE { get; set; }


        public string FAB_TYPE_SNAME { get; set; }
        public string FABRIC_GROUP_NAME { get; set; }
        public string FIB_COMP_NAME { get; set; }
        public long RQD_QTY { get; set; }
        public long FIN_QTY { get; set; }
        public string KNT_YRN_LOT { get; set; }

        public string STR_REQ_NO { get; set; }

        public string DF_PROC_TYP_LST { get; set; }

        public string BT_STOP_DESC { get; set; }

        public Int64? RF_RESP_DEPT_ID { get; set; }

        public long LK_FBR_GRP_ID { get; set; }

        public string IS_BP_LOCKED { get; set; }
    }
}