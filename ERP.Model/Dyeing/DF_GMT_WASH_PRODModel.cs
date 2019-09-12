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
    public class DF_GMT_WASH_PRODModel
    {
        public Int64 DF_GMT_WASH_PROD_ID { get; set; }
        public Int64 GMT_DF_WASH_REQ_ID { get; set; }
        public DateTime PROD_DT { get; set; }
        public Int64 HR_SCHEDULE_H_ID { get; set; }
        public Int64 OPERATOR_ID { get; set; }
        public Int64 DF_MACHINE_ID { get; set; }
        public Int64 DF_PROC_TYPE_ID { get; set; }
        public Int64 WASH_QTY { get; set; }
        public Int64 QTY_MOU_ID { get; set; }
        public string REF_GT_PASS_NO { get; set; }
        public string REMARKS { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }
        public Int64 DUE_WASH_QTY { get; set; }
        public Int64 TODAY_PROD_QTY { get; set; }

        
        public string WASH_REQ_NO { get; set; }
        public DateTime WASH_REQ_DT { get; set; }
        public Int64 WASH_REQ_BY { get; set; }
        public Int64 WASH_REQ_TO { get; set; }
        public Int64 MC_ORDER_H_ID { get; set; }
        public Int64 LK_GMT_PROD_CAT_ID { get; set; }
        public Int64 WASH_RQD_QTY { get; set; }
        public Int64 TGT_HR_PROD_QTY { get; set; }
        public Int64 TGT_DAY_PROD_QTY { get; set; }
        public DateTime TGT_ST_DT { get; set; }
        public DateTime TGT_END_DT { get; set; }
        public Int64 HR_PROD_FLR_ID { get; set; }
        public Int64 HR_PROD_LINE_ID { get; set; }


        public long MC_BYR_ACC_GRP_ID { get; set; }
        public long MC_STYLE_H_EXT_ID { get; set; }
        public long MC_ORDER_SHIP_ID { get; set; }
        public string BYR_ACC_GRP_NAME_EN { get; set; }
        public string STYLE_NO { get; set; }
        public string ORDER_NO { get; set; }
        public DateTime SHIP_DT { get; set; }


        public string Save()
        {
            const string sp = "PKG_IE_GMT_WASH_REQ.DF_GMT_WASH_PROD_INSERT";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_GMT_WASH_PROD_ID", Value = ob.DF_GMT_WASH_PROD_ID},
                     new CommandParameter() {ParameterName = "pGMT_DF_WASH_REQ_ID", Value = ob.GMT_DF_WASH_REQ_ID},
                     new CommandParameter() {ParameterName = "pPROD_DT", Value = ob.PROD_DT},
                     new CommandParameter() {ParameterName = "pHR_SCHEDULE_H_ID", Value = ob.HR_SCHEDULE_H_ID},
                     new CommandParameter() {ParameterName = "pOPERATOR_ID", Value = ob.OPERATOR_ID},
                     new CommandParameter() {ParameterName = "pDF_MACHINE_ID", Value = ob.DF_MACHINE_ID},
                     new CommandParameter() {ParameterName = "pDF_PROC_TYPE_ID", Value = ob.DF_PROC_TYPE_ID},
                     new CommandParameter() {ParameterName = "pWASH_QTY", Value = ob.WASH_QTY},
                     new CommandParameter() {ParameterName = "pQTY_MOU_ID", Value = ob.QTY_MOU_ID},
                     new CommandParameter() {ParameterName = "pREF_GT_PASS_NO", Value = ob.REF_GT_PASS_NO},
                     new CommandParameter() {ParameterName = "pREMARKS", Value = ob.REMARKS},
                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pOption", Value =1000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output},
                     new CommandParameter() {ParameterName = "opDF_GMT_WASH_PROD_ID", Value =0, Direction = ParameterDirection.Output}
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
            const string sp = "PKG_IE_GMT_WASH_REQ.DF_GMT_WASH_PROD_INSERT";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_GMT_WASH_PROD_ID", Value = ob.DF_GMT_WASH_PROD_ID},
                     new CommandParameter() {ParameterName = "pGMT_DF_WASH_REQ_ID", Value = ob.GMT_DF_WASH_REQ_ID},
                     new CommandParameter() {ParameterName = "pPROD_DT", Value = ob.PROD_DT},
                     new CommandParameter() {ParameterName = "pHR_SCHEDULE_H_ID", Value = ob.HR_SCHEDULE_H_ID},
                     new CommandParameter() {ParameterName = "pOPERATOR_ID", Value = ob.OPERATOR_ID},
                     new CommandParameter() {ParameterName = "pDF_MACHINE_ID", Value = ob.DF_MACHINE_ID},
                     new CommandParameter() {ParameterName = "pDF_PROC_TYPE_ID", Value = ob.DF_PROC_TYPE_ID},
                     new CommandParameter() {ParameterName = "pWASH_QTY", Value = ob.WASH_QTY},
                     new CommandParameter() {ParameterName = "pQTY_MOU_ID", Value = ob.QTY_MOU_ID},
                     new CommandParameter() {ParameterName = "pREF_GT_PASS_NO", Value = ob.REF_GT_PASS_NO},
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

        public string Delete()
        {
            const string sp = "SP_DF_GMT_WASH_PROD";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_GMT_WASH_PROD_ID", Value = ob.DF_GMT_WASH_PROD_ID},
                     new CommandParameter() {ParameterName = "pGMT_DF_WASH_REQ_ID", Value = ob.GMT_DF_WASH_REQ_ID},
                     new CommandParameter() {ParameterName = "pPROD_DT", Value = ob.PROD_DT},
                     new CommandParameter() {ParameterName = "pHR_SCHEDULE_H_ID", Value = ob.HR_SCHEDULE_H_ID},
                     new CommandParameter() {ParameterName = "pOPERATOR_ID", Value = ob.OPERATOR_ID},
                     new CommandParameter() {ParameterName = "pDF_MACHINE_ID", Value = ob.DF_MACHINE_ID},
                     new CommandParameter() {ParameterName = "pDF_PROC_TYPE_ID", Value = ob.DF_PROC_TYPE_ID},
                     new CommandParameter() {ParameterName = "pWASH_QTY", Value = ob.WASH_QTY},
                     new CommandParameter() {ParameterName = "pQTY_MOU_ID", Value = ob.QTY_MOU_ID},
                     new CommandParameter() {ParameterName = "pREF_GT_PASS_NO", Value = ob.REF_GT_PASS_NO},
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

        public List<DF_GMT_WASH_PRODModel> SelectAll(Int64? pGMT_DF_WASH_REQ_ID = null)
        {
            string sp = "PKG_IE_GMT_WASH_REQ.DF_GMT_WASH_PROD_SELECT";
            try
            {
                var obList = new List<DF_GMT_WASH_PRODModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pGMT_DF_WASH_REQ_ID", Value =pGMT_DF_WASH_REQ_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DF_GMT_WASH_PRODModel ob = new DF_GMT_WASH_PRODModel();
                    ob.DF_GMT_WASH_PROD_ID = (dr["DF_GMT_WASH_PROD_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_GMT_WASH_PROD_ID"]);
                    ob.GMT_DF_WASH_REQ_ID = (dr["GMT_DF_WASH_REQ_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_DF_WASH_REQ_ID"]);
                    ob.PROD_DT = (dr["PROD_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["PROD_DT"]);
                    ob.HR_SCHEDULE_H_ID = (dr["HR_SCHEDULE_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_SCHEDULE_H_ID"]);
                    ob.OPERATOR_ID = (dr["OPERATOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["OPERATOR_ID"]);
                    ob.DF_MACHINE_ID = (dr["DF_MACHINE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_MACHINE_ID"]);
                    ob.DF_PROC_TYPE_ID = (dr["DF_PROC_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_PROC_TYPE_ID"]);
                    ob.WASH_QTY = (dr["WASH_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["WASH_QTY"]);
                    ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);
                    ob.REF_GT_PASS_NO = (dr["REF_GT_PASS_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REF_GT_PASS_NO"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);

                    ob.SCHEDULE_NAME_EN = (dr["SCHEDULE_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SCHEDULE_NAME_EN"]);
                    
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DF_GMT_WASH_PRODModel Select(Int64? pDF_GMT_WASH_PROD_ID = null)
        {
            string sp = "PKG_IE_GMT_WASH_REQ.DF_GMT_WASH_PROD_SELECT";
            try
            {
                var ob = new DF_GMT_WASH_PRODModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_GMT_WASH_PROD_ID", Value =pDF_GMT_WASH_PROD_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.DF_GMT_WASH_PROD_ID = (dr["DF_GMT_WASH_PROD_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_GMT_WASH_PROD_ID"]);
                    ob.GMT_DF_WASH_REQ_ID = (dr["GMT_DF_WASH_REQ_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_DF_WASH_REQ_ID"]);
                    ob.PROD_DT = (dr["PROD_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["PROD_DT"]);
                    ob.HR_SCHEDULE_H_ID = (dr["HR_SCHEDULE_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_SCHEDULE_H_ID"]);
                    ob.OPERATOR_ID = (dr["OPERATOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["OPERATOR_ID"]);
                    ob.DF_MACHINE_ID = (dr["DF_MACHINE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_MACHINE_ID"]);
                    ob.DF_PROC_TYPE_ID = (dr["DF_PROC_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_PROC_TYPE_ID"]);
                    ob.WASH_QTY = (dr["WASH_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["WASH_QTY"]);
                    ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);
                    ob.REF_GT_PASS_NO = (dr["REF_GT_PASS_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REF_GT_PASS_NO"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);

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
                    
                    ob.MC_BYR_ACC_GRP_ID = (dr["MC_BYR_ACC_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BYR_ACC_GRP_ID"]);
                    ob.MC_STYLE_H_EXT_ID = (dr["MC_STYLE_H_EXT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_H_EXT_ID"]);
                    ob.MC_ORDER_SHIP_ID = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_ORDER_SHIP_ID"]);
                    ob.HR_PROD_FLR_ID = (dr["HR_PROD_FLR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_PROD_FLR_ID"]);
                    ob.HR_PROD_LINE_ID = (dr["HR_PROD_LINE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_PROD_LINE_ID"]);

                }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string SCHEDULE_NAME_EN { get; set; }
    }
}