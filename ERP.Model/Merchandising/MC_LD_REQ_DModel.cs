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
    public class MC_LD_REQ_DModel
    {
        public Int64 MC_LD_REQ_D_ID { get; set; }
        public Int64 MC_LD_REQ_H_ID { get; set; }
        public Int64 LK_ORD_TYPE_ID { get; set; }
        public Int64? MC_ORDER_H_ID { get; set; }
        public Int64 MC_COLOR_ID { get; set; }
        public string COLOR_REF { get; set; }
        public Int64? MC_STYLE_D_FAB_ID { get; set; }
        public Int64 REQD_QTY { get; set; }
        public DateTime TARGET_DT { get; set; }
        public Int64 LK_PRIORITY_ID { get; set; }
        public Int64 MC_TNA_TASK_STATUS_ID { get; set; }
        public string COMMENTS { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }
        public string APRV_LD_REF { get; set; }
        public string ORDER_NO { get; set; }
        public string COLOR_NAME_EN { get; set; }
        public string TASK_STATUS_NAME { get; set; }

        public DateTime SHIP_DT { get; set; }
        public Int64 LK_LTSRC_ID { get; set; }
        public string LK_LTSRC_NAME { get; set; }
        public string FABRIC_DESC { get; set; }

        public Int64 MC_STYLE_H_ID { get; set; }
        public string STYLE_NO { get; set; }
        public string PANTON_NO { get; set; }
        public Int64? RF_FIB_COMP_ID { get; set; }
        public string MC_STYLE_D_ITEM_LST { get; set; }

        public string Save()
        {
            const string sp = "SP_MC_LD_REQ_D";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_LD_REQ_D_ID", Value = ob.MC_LD_REQ_D_ID},
                     new CommandParameter() {ParameterName = "pMC_LD_REQ_H_ID", Value = ob.MC_LD_REQ_H_ID},
                     new CommandParameter() {ParameterName = "pLK_ORD_TYPE_ID", Value = ob.LK_ORD_TYPE_ID},
                     //new CommandParameter() {ParameterName = "pMC_STYLE_H_ID", Value = ob.MC_STYLE_H_ID},
                     //new CommandParameter() {ParameterName = "pSTYLE_NO", Value = ob.STYLE_NO},
                     new CommandParameter() {ParameterName = "pMC_ORDER_H_ID", Value = ob.MC_ORDER_H_ID},
                     new CommandParameter() {ParameterName = "pMC_COLOR_ID", Value = ob.MC_COLOR_ID},
                     //new CommandParameter() {ParameterName = "pLK_COL_REF_ID", Value = ob.LK_COL_REF_ID},
                     new CommandParameter() {ParameterName = "pCOLOR_REF", Value = ob.COLOR_REF},
                     new CommandParameter() {ParameterName = "pMC_STYLE_D_FAB_ID", Value = ob.MC_STYLE_D_FAB_ID},
                     //new CommandParameter() {ParameterName = "pFABRIC_DESC", Value = ob.FABRIC_DESC},
                     new CommandParameter() {ParameterName = "pREQD_QTY", Value = ob.REQD_QTY},
                     new CommandParameter() {ParameterName = "pTARGET_DT", Value = ob.TARGET_DT},
                     new CommandParameter() {ParameterName = "pLK_PRIORITY_ID", Value = ob.LK_PRIORITY_ID},
                     new CommandParameter() {ParameterName = "pMC_TNA_TASK_STATUS_ID", Value = ob.MC_TNA_TASK_STATUS_ID},
                     new CommandParameter() {ParameterName = "pCOMMENTS", Value = ob.COMMENTS},
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
            const string sp = "SP_MC_LD_REQ_D";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_LD_REQ_D_ID", Value = ob.MC_LD_REQ_D_ID},
                     new CommandParameter() {ParameterName = "pMC_LD_REQ_H_ID", Value = ob.MC_LD_REQ_H_ID},
                     new CommandParameter() {ParameterName = "pLK_ORD_TYPE_ID", Value = ob.LK_ORD_TYPE_ID},
                     //new CommandParameter() {ParameterName = "pMC_STYLE_H_ID", Value = ob.MC_STYLE_H_ID},
                     //new CommandParameter() {ParameterName = "pSTYLE_NO", Value = ob.STYLE_NO},
                     new CommandParameter() {ParameterName = "pMC_ORDER_H_ID", Value = ob.MC_ORDER_H_ID},
                     new CommandParameter() {ParameterName = "pMC_COLOR_ID", Value = ob.MC_COLOR_ID},
                     //new CommandParameter() {ParameterName = "pLK_COL_REF_ID", Value = ob.LK_COL_REF_ID},
                     new CommandParameter() {ParameterName = "pCOLOR_REF", Value = ob.COLOR_REF},
                     new CommandParameter() {ParameterName = "pMC_STYLE_D_FAB_ID", Value = ob.MC_STYLE_D_FAB_ID},
                     //new CommandParameter() {ParameterName = "pFABRIC_DESC", Value = ob.FABRIC_DESC},
                     new CommandParameter() {ParameterName = "pREQD_QTY", Value = ob.REQD_QTY},
                     new CommandParameter() {ParameterName = "pTARGET_DT", Value = ob.TARGET_DT},
                     new CommandParameter() {ParameterName = "pLK_PRIORITY_ID", Value = ob.LK_PRIORITY_ID},
                     new CommandParameter() {ParameterName = "pMC_TNA_TASK_STATUS_ID", Value = ob.MC_TNA_TASK_STATUS_ID},
                     new CommandParameter() {ParameterName = "pCOMMENTS", Value = ob.COMMENTS},
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
            const string sp = "SP_MC_LD_REQ_D";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_LD_REQ_D_ID", Value = ob.MC_LD_REQ_D_ID},
                     new CommandParameter() {ParameterName = "pMC_LD_REQ_H_ID", Value = ob.MC_LD_REQ_H_ID},
                     new CommandParameter() {ParameterName = "pLK_ORD_TYPE_ID", Value = ob.LK_ORD_TYPE_ID},
                     //new CommandParameter() {ParameterName = "pMC_STYLE_H_ID", Value = ob.MC_STYLE_H_ID},
                     //new CommandParameter() {ParameterName = "pSTYLE_NO", Value = ob.STYLE_NO},
                     new CommandParameter() {ParameterName = "pMC_ORDER_H_ID", Value = ob.MC_ORDER_H_ID},
                     new CommandParameter() {ParameterName = "pMC_COLOR_ID", Value = ob.MC_COLOR_ID},
                     //new CommandParameter() {ParameterName = "pLK_COL_REF_ID", Value = ob.LK_COL_REF_ID},
                     new CommandParameter() {ParameterName = "pCOLOR_REF", Value = ob.COLOR_REF},
                     new CommandParameter() {ParameterName = "pMC_STYLE_D_FAB_ID", Value = ob.MC_STYLE_D_FAB_ID},
                     //new CommandParameter() {ParameterName = "pFABRIC_DESC", Value = ob.FABRIC_DESC},
                     new CommandParameter() {ParameterName = "pREQD_QTY", Value = ob.REQD_QTY},
                     new CommandParameter() {ParameterName = "pTARGET_DT", Value = ob.TARGET_DT},
                     new CommandParameter() {ParameterName = "pLK_PRIORITY_ID", Value = ob.LK_PRIORITY_ID},
                     new CommandParameter() {ParameterName = "pMC_TNA_TASK_STATUS_ID", Value = ob.MC_TNA_TASK_STATUS_ID},
                     new CommandParameter() {ParameterName = "pCOMMENTS", Value = ob.COMMENTS},
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

        public List<MC_LD_REQ_DModel> SelectAll()
        {
            string sp = "Select_MC_LD_REQ_D";
            try
            {
                var obList = new List<MC_LD_REQ_DModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_LD_REQ_D_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_LD_REQ_DModel ob = new MC_LD_REQ_DModel();
                    ob.MC_LD_REQ_D_ID = (dr["MC_LD_REQ_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_LD_REQ_D_ID"]);
                    ob.MC_LD_REQ_H_ID = (dr["MC_LD_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_LD_REQ_H_ID"]);
                    ob.LK_ORD_TYPE_ID = (dr["LK_ORD_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_ORD_TYPE_ID"]);
                    //ob.MC_STYLE_H_ID = (dr["MC_STYLE_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_H_ID"]);
                    //ob.STYLE_NO = (dr["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO"]);

                    if (dr["MC_ORDER_H_ID"] != DBNull.Value)
                    {
                        ob.MC_ORDER_H_ID = Convert.ToInt64(dr["MC_ORDER_H_ID"]);
                    } 
                    ob.MC_COLOR_ID = (dr["MC_COLOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_COLOR_ID"]);
                    //ob.LK_COL_REF_ID = (dr["LK_COL_REF_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_COL_REF_ID"]);
                    ob.COLOR_REF = (dr["COLOR_REF"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_REF"]);
                    ob.MC_STYLE_D_FAB_ID = (dr["MC_STYLE_D_FAB_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_D_FAB_ID"]);
                    //ob.FABRIC_DESC = (dr["FABRIC_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FABRIC_DESC"]);
                    ob.REQD_QTY = (dr["REQD_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["REQD_QTY"]);
                    ob.TARGET_DT = (dr["TARGET_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["TARGET_DT"]);
                    ob.LK_PRIORITY_ID = (dr["LK_PRIORITY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_PRIORITY_ID"]);
                    ob.MC_TNA_TASK_STATUS_ID = (dr["MC_TNA_TASK_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_TNA_TASK_STATUS_ID"]);
                    ob.COMMENTS = (dr["COMMENTS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMMENTS"]);
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

        public MC_LD_REQ_DModel Select(long ID)
        {
            string sp = "Select_MC_LD_REQ_D";
            try
            {
                var ob = new MC_LD_REQ_DModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_LD_REQ_D_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.MC_LD_REQ_D_ID = (dr["MC_LD_REQ_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_LD_REQ_D_ID"]);
                    ob.MC_LD_REQ_H_ID = (dr["MC_LD_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_LD_REQ_H_ID"]);
                    ob.LK_ORD_TYPE_ID = (dr["LK_ORD_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_ORD_TYPE_ID"]);
                    //ob.MC_STYLE_H_ID = (dr["MC_STYLE_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_H_ID"]);
                    //ob.STYLE_NO = (dr["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO"]);
                   

                    if (dr["MC_ORDER_H_ID"] != DBNull.Value)
                    {
                        ob.MC_ORDER_H_ID = Convert.ToInt64(dr["MC_ORDER_H_ID"]);
                    } 

                    ob.MC_COLOR_ID = (dr["MC_COLOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_COLOR_ID"]);
                    //ob.LK_COL_REF_ID = (dr["LK_COL_REF_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_COL_REF_ID"]);
                    ob.COLOR_REF = (dr["COLOR_REF"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_REF"]);
                    ob.MC_STYLE_D_FAB_ID = (dr["MC_STYLE_D_FAB_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_D_FAB_ID"]);
                    //ob.FABRIC_DESC = (dr["FABRIC_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FABRIC_DESC"]);
                    ob.REQD_QTY = (dr["REQD_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["REQD_QTY"]);
                    ob.TARGET_DT = (dr["TARGET_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["TARGET_DT"]);
                    ob.LK_PRIORITY_ID = (dr["LK_PRIORITY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_PRIORITY_ID"]);
                    ob.MC_TNA_TASK_STATUS_ID = (dr["MC_TNA_TASK_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_TNA_TASK_STATUS_ID"]);
                    ob.COMMENTS = (dr["COMMENTS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMMENTS"]);
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


    }
}