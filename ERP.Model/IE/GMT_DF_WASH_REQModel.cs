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
    public class GMT_DF_WASH_REQModel
    {
        public Int64 GMT_DF_WASH_REQ_ID { get; set; }
        public string WASH_REQ_NO { get; set; }
        public DateTime WASH_REQ_DT { get; set; }
        public Int64 WASH_REQ_BY { get; set; }
        public Int64 WASH_REQ_TO { get; set; }
        public Int64? MC_ORDER_H_ID { get; set; }
        public Int64 LK_GMT_PROD_CAT_ID { get; set; }
        public Int64 DF_PROC_TYPE_ID { get; set; }
        public Int64 WASH_RQD_QTY { get; set; }
        public Int64 TGT_HR_PROD_QTY { get; set; }
        public Int64 TGT_DAY_PROD_QTY { get; set; }
        public DateTime TGT_ST_DT { get; set; }
        public DateTime TGT_END_DT { get; set; }
        public Int64 RF_REQ_TYPE_ID { get; set; }
        public Int64 RF_ACTN_STATUS_ID { get; set; }
        public string REMARKS { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }

        public Int64 DUE_WASH_QTY { get; set; }
        public Int64 TODAY_PROD_QTY { get; set; }

        public string Save()
        {
            const string sp = "PKG_IE_GMT_WASH_REQ.gmt_df_wash_req_insert";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pGMT_DF_WASH_REQ_ID", Value = ob.GMT_DF_WASH_REQ_ID},
                     new CommandParameter() {ParameterName = "pWASH_REQ_NO", Value = ob.WASH_REQ_NO},
                     new CommandParameter() {ParameterName = "pWASH_REQ_DT", Value = ob.WASH_REQ_DT},
                     new CommandParameter() {ParameterName = "pWASH_REQ_BY", Value = ob.WASH_REQ_BY},
                     new CommandParameter() {ParameterName = "pWASH_REQ_TO", Value = ob.WASH_REQ_TO},
                     new CommandParameter() {ParameterName = "pHR_PROD_FLR_ID", Value = ob.HR_PROD_FLR_ID},
                     new CommandParameter() {ParameterName = "pHR_PROD_LINE_ID", Value = ob.HR_PROD_LINE_ID},
                     new CommandParameter() {ParameterName = "pMC_ORDER_H_ID", Value = ob.MC_ORDER_H_ID},
                     new CommandParameter() {ParameterName = "pLK_GMT_PROD_CAT_ID", Value = ob.LK_GMT_PROD_CAT_ID},
                     new CommandParameter() {ParameterName = "pDF_PROC_TYPE_ID", Value = ob.DF_PROC_TYPE_ID},
                     new CommandParameter() {ParameterName = "pWASH_RQD_QTY", Value = ob.WASH_RQD_QTY},
                     new CommandParameter() {ParameterName = "pTGT_HR_PROD_QTY", Value = ob.TGT_HR_PROD_QTY},
                     new CommandParameter() {ParameterName = "pTGT_DAY_PROD_QTY", Value = ob.TGT_DAY_PROD_QTY},
                     new CommandParameter() {ParameterName = "pTGT_ST_DT", Value = ob.TGT_ST_DT},
                     new CommandParameter() {ParameterName = "pTGT_END_DT", Value = ob.TGT_END_DT},
                     new CommandParameter() {ParameterName = "pRF_REQ_TYPE_ID", Value = ob.RF_REQ_TYPE_ID},
                     new CommandParameter() {ParameterName = "pRF_ACTN_STATUS_ID", Value = ob.RF_ACTN_STATUS_ID},
                     new CommandParameter() {ParameterName = "pREMARKS", Value = ob.REMARKS},
                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pOption", Value =1000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output},
                     new CommandParameter() {ParameterName = "opGMT_DF_WASH_REQ_ID", Value =00, Direction = ParameterDirection.Output}
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
            const string sp = "SP_GMT_DF_WASH_REQ";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pGMT_DF_WASH_REQ_ID", Value = ob.GMT_DF_WASH_REQ_ID},
                     new CommandParameter() {ParameterName = "pWASH_REQ_NO", Value = ob.WASH_REQ_NO},
                     new CommandParameter() {ParameterName = "pWASH_REQ_DT", Value = ob.WASH_REQ_DT},
                     new CommandParameter() {ParameterName = "pWASH_REQ_BY", Value = ob.WASH_REQ_BY},
                     new CommandParameter() {ParameterName = "pWASH_REQ_TO", Value = ob.WASH_REQ_TO},
                     new CommandParameter() {ParameterName = "pMC_ORDER_H_ID", Value = ob.MC_ORDER_H_ID},
                     new CommandParameter() {ParameterName = "pLK_GMT_PROD_CAT_ID", Value = ob.LK_GMT_PROD_CAT_ID},
                     new CommandParameter() {ParameterName = "pDF_PROC_TYPE_ID", Value = ob.DF_PROC_TYPE_ID},
                     new CommandParameter() {ParameterName = "pWASH_RQD_QTY", Value = ob.WASH_RQD_QTY},
                     new CommandParameter() {ParameterName = "pTGT_HR_PROD_QTY", Value = ob.TGT_HR_PROD_QTY},
                     new CommandParameter() {ParameterName = "pTGT_DAY_PROD_QTY", Value = ob.TGT_DAY_PROD_QTY},
                     new CommandParameter() {ParameterName = "pTGT_ST_DT", Value = ob.TGT_ST_DT},
                     new CommandParameter() {ParameterName = "pTGT_END_DT", Value = ob.TGT_END_DT},
                     new CommandParameter() {ParameterName = "pRF_REQ_TYPE_ID", Value = ob.RF_REQ_TYPE_ID},
                     new CommandParameter() {ParameterName = "pRF_ACTN_STATUS_ID", Value = ob.RF_ACTN_STATUS_ID},
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
            const string sp = "SP_GMT_DF_WASH_REQ";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pGMT_DF_WASH_REQ_ID", Value = ob.GMT_DF_WASH_REQ_ID},
                     new CommandParameter() {ParameterName = "pWASH_REQ_NO", Value = ob.WASH_REQ_NO},
                     new CommandParameter() {ParameterName = "pWASH_REQ_DT", Value = ob.WASH_REQ_DT},
                     new CommandParameter() {ParameterName = "pWASH_REQ_BY", Value = ob.WASH_REQ_BY},
                     new CommandParameter() {ParameterName = "pWASH_REQ_TO", Value = ob.WASH_REQ_TO},
                     new CommandParameter() {ParameterName = "pMC_ORDER_H_ID", Value = ob.MC_ORDER_H_ID},
                     new CommandParameter() {ParameterName = "pLK_GMT_PROD_CAT_ID", Value = ob.LK_GMT_PROD_CAT_ID},
                     new CommandParameter() {ParameterName = "pDF_PROC_TYPE_ID", Value = ob.DF_PROC_TYPE_ID},
                     new CommandParameter() {ParameterName = "pWASH_RQD_QTY", Value = ob.WASH_RQD_QTY},
                     new CommandParameter() {ParameterName = "pTGT_HR_PROD_QTY", Value = ob.TGT_HR_PROD_QTY},
                     new CommandParameter() {ParameterName = "pTGT_DAY_PROD_QTY", Value = ob.TGT_DAY_PROD_QTY},
                     new CommandParameter() {ParameterName = "pTGT_ST_DT", Value = ob.TGT_ST_DT},
                     new CommandParameter() {ParameterName = "pTGT_END_DT", Value = ob.TGT_END_DT},
                     new CommandParameter() {ParameterName = "pRF_REQ_TYPE_ID", Value = ob.RF_REQ_TYPE_ID},
                     new CommandParameter() {ParameterName = "pRF_ACTN_STATUS_ID", Value = ob.RF_ACTN_STATUS_ID},
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

        public List<GMT_DF_WASH_REQModel> SelectAll(int pageNo, int pageSize, string pWASH_REQ_NO = null, string pWASH_REQ_DT = null, Int64? pMC_BYR_ACC_GRP_ID = null, Int64? pMC_STYLE_H_EXT_ID = null, Int64? pMC_ORDER_H_ID = null, Int64? pUSER_ID = null, string pMC_ORDER_H_ID_LST = null)
        {
            string sp = "PKG_IE_GMT_WASH_REQ.GMT_DF_WASH_REQ_Select";
            try
            {
                var obList = new List<GMT_DF_WASH_REQModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pageNo", Value =pageNo},
                     new CommandParameter() {ParameterName = "pageSize", Value =pageSize},
                     new CommandParameter() {ParameterName = "pMC_BYR_ACC_GRP_ID", Value =pMC_BYR_ACC_GRP_ID},
                     new CommandParameter() {ParameterName = "pMC_STYLE_H_EXT_ID", Value =pMC_STYLE_H_EXT_ID},
                     new CommandParameter() {ParameterName = "pMC_ORDER_H_ID", Value =pMC_ORDER_H_ID},
                     new CommandParameter() {ParameterName = "pWASH_REQ_NO", Value =pWASH_REQ_NO},
                     new CommandParameter() {ParameterName = "pWASH_REQ_DT", Value =pWASH_REQ_DT},
                     new CommandParameter() {ParameterName = "pUSER_ID", Value =pUSER_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    GMT_DF_WASH_REQModel ob = new GMT_DF_WASH_REQModel();
                    ob.GMT_DF_WASH_REQ_ID = (dr["GMT_DF_WASH_REQ_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_DF_WASH_REQ_ID"]);
                    ob.WASH_REQ_NO = (dr["WASH_REQ_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["WASH_REQ_NO"]);
                    ob.WASH_REQ_DT = (dr["WASH_REQ_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["WASH_REQ_DT"]);
                    ob.WASH_REQ_BY = (dr["WASH_REQ_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["WASH_REQ_BY"]);
                    ob.WASH_REQ_TO = (dr["WASH_REQ_TO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["WASH_REQ_TO"]);
                    ob.MC_ORDER_H_ID = (dr["MC_ORDER_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_ORDER_H_ID"]);
                    ob.LK_GMT_PROD_CAT_ID = (dr["LK_GMT_PROD_CAT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_GMT_PROD_CAT_ID"]);
                    ob.DF_PROC_TYPE_ID = (dr["DF_PROC_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_PROC_TYPE_ID"]);
                    ob.WASH_RQD_QTY = (dr["WASH_RQD_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["WASH_RQD_QTY"]);
                    ob.TGT_HR_PROD_QTY = (dr["TGT_HR_PROD_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["TGT_HR_PROD_QTY"]);
                    ob.TGT_DAY_PROD_QTY = (dr["TGT_DAY_PROD_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["TGT_DAY_PROD_QTY"]);
                    ob.TGT_ST_DT = (dr["TGT_ST_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["TGT_ST_DT"]);
                    ob.TGT_END_DT = (dr["TGT_END_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["TGT_END_DT"]);
                    ob.RF_REQ_TYPE_ID = (dr["RF_REQ_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_REQ_TYPE_ID"]);
                    ob.RF_ACTN_STATUS_ID = (dr["RF_ACTN_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_ACTN_STATUS_ID"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);

                    ob.BYR_ACC_GRP_NAME_EN = (dr["BYR_ACC_GRP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BYR_ACC_GRP_NAME_EN"]);
                    ob.STYLE_NO = (dr["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO"]);
                    ob.ORDER_NO = (dr["ORDER_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_NO"]);
                    ob.SHIP_DT = (dr["SHIP_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["SHIP_DT"]);

                    ob.PERMISSION = (dr["PERMISSION"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PERMISSION"]);
                    ob.TOTAL_REC = (dr["TOTAL_REC"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["TOTAL_REC"]);

                    ob.ACTN_STATUS_NAME = (dr["ACTN_STATUS_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ACTN_STATUS_NAME"]);
                    ob.EVENT_NAME = (dr["EVENT_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EVENT_NAME"]);
                    ob.NXT_ACTION_NAME = (dr["NXT_ACTION_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["NXT_ACTION_NAME"]);
                    ob.ACTN_ROLE_FLAG = (dr["ACTN_ROLE_FLAG"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ACTN_ROLE_FLAG"]);

                    ob.HR_PROD_FLR_ID = (dr["HR_PROD_FLR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_PROD_FLR_ID"]);
                    ob.HR_PROD_LINE_ID = (dr["HR_PROD_LINE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_PROD_LINE_ID"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public GMT_DF_WASH_REQModel SelectByID(Int64? pGMT_DF_WASH_REQ_ID = null)
        {
            string sp = "PKG_IE_GMT_WASH_REQ.GMT_DF_WASH_REQ_Select";
            try
            {
                var ob = new GMT_DF_WASH_REQModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pGMT_DF_WASH_REQ_ID", Value =pGMT_DF_WASH_REQ_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.GMT_DF_WASH_REQ_ID = (dr["GMT_DF_WASH_REQ_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_DF_WASH_REQ_ID"]);
                    ob.WASH_REQ_NO = (dr["WASH_REQ_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["WASH_REQ_NO"]);
                    ob.WASH_REQ_DT = (dr["WASH_REQ_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["WASH_REQ_DT"]);
                    ob.WASH_REQ_BY = (dr["WASH_REQ_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["WASH_REQ_BY"]);
                    ob.WASH_REQ_TO = (dr["WASH_REQ_TO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["WASH_REQ_TO"]);
                    ob.MC_ORDER_H_ID = (dr["MC_ORDER_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_ORDER_H_ID"]);
                    ob.LK_GMT_PROD_CAT_ID = (dr["LK_GMT_PROD_CAT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_GMT_PROD_CAT_ID"]);
                    ob.DF_PROC_TYPE_ID = (dr["DF_PROC_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_PROC_TYPE_ID"]);
                    ob.WASH_RQD_QTY = (dr["WASH_RQD_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["WASH_RQD_QTY"]);
                    ob.TGT_HR_PROD_QTY = (dr["TGT_HR_PROD_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["TGT_HR_PROD_QTY"]);
                    ob.TGT_DAY_PROD_QTY = (dr["TGT_DAY_PROD_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["TGT_DAY_PROD_QTY"]);
                    ob.TGT_ST_DT = (dr["TGT_ST_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["TGT_ST_DT"]);
                    ob.TGT_END_DT = (dr["TGT_END_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["TGT_END_DT"]);
                    ob.RF_REQ_TYPE_ID = (dr["RF_REQ_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_REQ_TYPE_ID"]);
                    ob.RF_ACTN_STATUS_ID = (dr["RF_ACTN_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_ACTN_STATUS_ID"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);
                    ob.ORDER_NO = (dr["ORDER_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_NO"]);
                    
                    ob.MC_BYR_ACC_GRP_ID = (dr["MC_BYR_ACC_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BYR_ACC_GRP_ID"]);
                    ob.MC_STYLE_H_EXT_ID = (dr["MC_STYLE_H_EXT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_H_EXT_ID"]);
                    ob.MC_ORDER_SHIP_ID = (dr["MC_ORDER_SHIP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_ORDER_SHIP_ID"]);
                    ob.HR_PROD_FLR_ID = (dr["HR_PROD_FLR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_PROD_FLR_ID"]);
                    ob.HR_PROD_LINE_ID = (dr["HR_PROD_LINE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_PROD_LINE_ID"]);

                    //,
                }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public int TOTAL_REC { get; set; }

        public long MC_BYR_ACC_GRP_ID { get; set; }

        public long MC_STYLE_H_EXT_ID { get; set; }

        public long MC_ORDER_SHIP_ID { get; set; }

        public string BYR_ACC_GRP_NAME_EN { get; set; }

        public string STYLE_NO { get; set; }

        public string ORDER_NO { get; set; }

        public DateTime SHIP_DT { get; set; }

        public string ACTN_STATUS_NAME { get; set; }

        public string EVENT_NAME { get; set; }

        public string NXT_ACTION_NAME { get; set; }

        public string ACTN_ROLE_FLAG { get; set; }

        public long PERMISSION { get; set; }

        public long HR_PROD_FLR_ID { get; set; }

        public long HR_PROD_LINE_ID { get; set; }
    }
}