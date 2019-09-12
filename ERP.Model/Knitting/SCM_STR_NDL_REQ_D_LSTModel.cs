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
    public class SCM_STR_NDL_REQ_D_LSTModel
    {
        public Int64 SCM_STR_NDL_REQ_D_LST_ID { get; set; }
        public Int64 SCM_STR_NDL_REQ_H_ID { get; set; }
        public Int64 INV_ITEM_ID { get; set; }
        public string MCN_ITEM_LST { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }
        public Int64 INV_ITEM_CAT_ID { get; set; }

        public string Save()
        {
            const string sp = "SP_SCM_STR_NDL_REQ_D_LST";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pSCM_STR_NDL_REQ_D_LST_ID", Value = ob.SCM_STR_NDL_REQ_D_LST_ID},
                     new CommandParameter() {ParameterName = "pSCM_STR_NDL_REQ_H_ID", Value = ob.SCM_STR_NDL_REQ_H_ID},
                     new CommandParameter() {ParameterName = "pINV_ITEM_ID", Value = ob.INV_ITEM_ID},
                     new CommandParameter() {ParameterName = "pMCN_ITEM_LST", Value = ob.MCN_ITEM_LST},
                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = ob.CREATED_BY},
                     new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = ob.LAST_UPDATED_BY},
                     new CommandParameter() {ParameterName = "pOption", Value =1000},
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

        public string Update()
        {
            const string sp = "SP_SCM_STR_NDL_REQ_D_LST";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pSCM_STR_NDL_REQ_D_LST_ID", Value = ob.SCM_STR_NDL_REQ_D_LST_ID},
                     new CommandParameter() {ParameterName = "pSCM_STR_NDL_REQ_H_ID", Value = ob.SCM_STR_NDL_REQ_H_ID},
                     new CommandParameter() {ParameterName = "pINV_ITEM_ID", Value = ob.INV_ITEM_ID},
                     new CommandParameter() {ParameterName = "pMCN_ITEM_LST", Value = ob.MCN_ITEM_LST},
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
            const string sp = "SP_SCM_STR_NDL_REQ_D_LST";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pSCM_STR_NDL_REQ_D_LST_ID", Value = ob.SCM_STR_NDL_REQ_D_LST_ID},
                     new CommandParameter() {ParameterName = "pSCM_STR_NDL_REQ_H_ID", Value = ob.SCM_STR_NDL_REQ_H_ID},
                     new CommandParameter() {ParameterName = "pINV_ITEM_ID", Value = ob.INV_ITEM_ID},
                     new CommandParameter() {ParameterName = "pMCN_ITEM_LST", Value = ob.MCN_ITEM_LST},
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

        public List<SCM_STR_NDL_REQ_D_LSTModel> SelectByID(Int64? pSCM_STR_NDL_REQ_H_ID=null)
        {
            string sp = "PKG_KNT_MCN_NEEDLE.SCM_STR_NDL_REQ_D_LST_Select";
            try
            {
                var obList = new List<SCM_STR_NDL_REQ_D_LSTModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pSCM_STR_NDL_REQ_H_ID", Value =pSCM_STR_NDL_REQ_H_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    SCM_STR_NDL_REQ_D_LSTModel ob = new SCM_STR_NDL_REQ_D_LSTModel();
                    ob.SCM_STR_NDL_REQ_D_LST_ID = (dr["SCM_STR_NDL_REQ_D_LST_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_STR_NDL_REQ_D_LST_ID"]);
                    ob.SCM_STR_NDL_REQ_H_ID = (dr["SCM_STR_NDL_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_STR_NDL_REQ_H_ID"]);
                    ob.INV_ITEM_ID = (dr["INV_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_ITEM_ID"]);
                    ob.MCN_ITEM_LST = (dr["MCN_ITEM_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MCN_ITEM_LST"]);
                    ob.INV_ITEM_CAT_ID = (dr["INV_ITEM_CAT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_ITEM_CAT_ID"]);
                    //ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    //ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    //ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    //ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);

                    ob.BRK_QTY_PCS = (dr["BRK_QTY_PCS"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["BRK_QTY_PCS"]);
                    ob.RQD_QTY_PCS = (dr["RQD_QTY_PCS"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RQD_QTY_PCS"]);

                    ob.INV_ITEM_ID = (dr["INV_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_ITEM_ID"]);
                    ob.ITEM_NAME_EN = (dr["ITEM_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_NAME_EN"]);

                    ob.CENTRAL_STR_STOCK = (dr["CENTRAL_STR_STOCK"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CENTRAL_STR_STOCK"]);
                    ob.SUB_STR_STOCK = (dr["SUB_STR_STOCK"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SUB_STR_STOCK"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public SCM_STR_NDL_REQ_D_LSTModel Select(long ID)
        {
            string sp = "PKG_KNT_MCN_NEEDLE.SCM_STR_NDL_REQ_D_LST_Select";
            try
            {
                var ob = new SCM_STR_NDL_REQ_D_LSTModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pSCM_STR_NDL_REQ_D_LST_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.SCM_STR_NDL_REQ_D_LST_ID = (dr["SCM_STR_NDL_REQ_D_LST_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_STR_NDL_REQ_D_LST_ID"]);
                    ob.SCM_STR_NDL_REQ_H_ID = (dr["SCM_STR_NDL_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_STR_NDL_REQ_H_ID"]);
                    ob.INV_ITEM_ID = (dr["INV_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_ITEM_ID"]);
                    ob.INV_ITEM_CAT_ID = (dr["INV_ITEM_CAT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_ITEM_CAT_ID"]);
                    ob.MCN_ITEM_LST = (dr["MCN_ITEM_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MCN_ITEM_LST"]);
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

        public string ITEM_NAME_EN { get; set; }

        public long BRK_QTY_PCS { get; set; }

        public long RQD_QTY_PCS { get; set; }

        public long CENTRAL_STR_STOCK { get; set; }

        public long SUB_STR_STOCK { get; set; }
    }
}