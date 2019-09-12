
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
    public class SCM_STR_NDL_REQ_DModel
    {
        public Int64 SCM_STR_NDL_REQ_D_ID { get; set; }
        public Int64 SCM_STR_NDL_REQ_H_ID { get; set; }
        public Int64 INV_ITEM_ID { get; set; }
        public Int64 KNT_MACHINE_ID { get; set; }
        public Int64 OPERATOR_ID { get; set; }
        public Int64 BRK_QTY_PCS { get; set; }
        public Int64 RQD_QTY_PCS { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }
        public Int64 INV_ITEM_CAT_ID { get; set; }

        public string Save()
        {
            const string sp = "SP_SCM_STR_NDL_REQ_D";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pSCM_STR_NDL_REQ_D_ID", Value = ob.SCM_STR_NDL_REQ_D_ID},
                     new CommandParameter() {ParameterName = "pSCM_STR_NDL_REQ_H_ID", Value = ob.SCM_STR_NDL_REQ_H_ID},
                     new CommandParameter() {ParameterName = "pINV_ITEM_ID", Value = ob.INV_ITEM_ID},
                     new CommandParameter() {ParameterName = "pKNT_MACHINE_ID", Value = ob.KNT_MACHINE_ID},
                     new CommandParameter() {ParameterName = "pOPERATOR_ID", Value = ob.OPERATOR_ID},
                     new CommandParameter() {ParameterName = "pBRK_QTY_PCS", Value = ob.BRK_QTY_PCS},
                     new CommandParameter() {ParameterName = "pRQD_QTY_PCS", Value = ob.RQD_QTY_PCS},
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
            const string sp = "SP_SCM_STR_NDL_REQ_D";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pSCM_STR_NDL_REQ_D_ID", Value = ob.SCM_STR_NDL_REQ_D_ID},
                     new CommandParameter() {ParameterName = "pSCM_STR_NDL_REQ_H_ID", Value = ob.SCM_STR_NDL_REQ_H_ID},
                     new CommandParameter() {ParameterName = "pINV_ITEM_ID", Value = ob.INV_ITEM_ID},
                     new CommandParameter() {ParameterName = "pKNT_MACHINE_ID", Value = ob.KNT_MACHINE_ID},
                     new CommandParameter() {ParameterName = "pOPERATOR_ID", Value = ob.OPERATOR_ID},
                     new CommandParameter() {ParameterName = "pBRK_QTY_PCS", Value = ob.BRK_QTY_PCS},
                     new CommandParameter() {ParameterName = "pRQD_QTY_PCS", Value = ob.RQD_QTY_PCS},
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
            const string sp = "SP_SCM_STR_NDL_REQ_D";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pSCM_STR_NDL_REQ_D_ID", Value = ob.SCM_STR_NDL_REQ_D_ID},
                     new CommandParameter() {ParameterName = "pSCM_STR_NDL_REQ_H_ID", Value = ob.SCM_STR_NDL_REQ_H_ID},
                     new CommandParameter() {ParameterName = "pINV_ITEM_ID", Value = ob.INV_ITEM_ID},
                     new CommandParameter() {ParameterName = "pKNT_MACHINE_ID", Value = ob.KNT_MACHINE_ID},
                     new CommandParameter() {ParameterName = "pOPERATOR_ID", Value = ob.OPERATOR_ID},
                     new CommandParameter() {ParameterName = "pBRK_QTY_PCS", Value = ob.BRK_QTY_PCS},
                     new CommandParameter() {ParameterName = "pRQD_QTY_PCS", Value = ob.RQD_QTY_PCS},
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

        public List<SCM_STR_NDL_REQ_DModel> SelectAll(Int64? pSCM_STR_NDL_REQ_H_ID = null, Int64? pOption = null)
        {
            string sp = "PKG_KNT_MC_NDL.SCM_STR_NDL_REQ_D_SELECT";
            try
            {
                var obList = new List<SCM_STR_NDL_REQ_DModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pSCM_STR_NDL_REQ_H_ID", Value =pSCM_STR_NDL_REQ_H_ID},
                     new CommandParameter() {ParameterName = "pOption", Value = pOption == null? 3001 : 3002},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    SCM_STR_NDL_REQ_DModel ob = new SCM_STR_NDL_REQ_DModel();
                    if (pOption != 3002)
                    {
                        ob.SCM_STR_NDL_REQ_D_ID = (dr["SCM_STR_NDL_REQ_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_STR_NDL_REQ_D_ID"]);
                        ob.KNT_MACHINE_ID = (dr["KNT_MACHINE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_MACHINE_ID"]);
                        ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                        ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                        ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                        ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);

                        ob.OPERATOR_ID = (dr["OPERATOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["OPERATOR_ID"]);
                        ob.KNT_MACHINE_NO = (dr["KNT_MACHINE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["KNT_MACHINE_NO"]);
                        ob.EMP_FULL_NAME_EN = (dr["EMP_FULL_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EMP_FULL_NAME_EN"]);
                    }
                    ob.SCM_STR_NDL_REQ_H_ID = (dr["SCM_STR_NDL_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_STR_NDL_REQ_H_ID"]);

                    ob.INV_ITEM_ID = (dr["INV_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_ITEM_ID"]);
                    ob.BRK_QTY_PCS = (dr["BRK_QTY_PCS"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["BRK_QTY_PCS"]);
                    ob.RQD_QTY_PCS = (dr["RQD_QTY_PCS"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RQD_QTY_PCS"]);
                    ob.INV_ITEM_CAT_ID = (dr["INV_ITEM_CAT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_ITEM_CAT_ID"]);
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

        public SCM_STR_NDL_REQ_DModel Select(long ID)
        {
            string sp = "PKG_KNT_MC_NDL.SCM_STR_NDL_REQ_D_SELECT";
            try
            {
                var ob = new SCM_STR_NDL_REQ_DModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pSCM_STR_NDL_REQ_D_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.SCM_STR_NDL_REQ_D_ID = (dr["SCM_STR_NDL_REQ_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_STR_NDL_REQ_D_ID"]);
                    ob.SCM_STR_NDL_REQ_H_ID = (dr["SCM_STR_NDL_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_STR_NDL_REQ_H_ID"]);
                    ob.INV_ITEM_ID = (dr["INV_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_ITEM_ID"]);
                    ob.KNT_MACHINE_ID = (dr["KNT_MACHINE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_MACHINE_ID"]);
                    ob.OPERATOR_ID = (dr["OPERATOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["OPERATOR_ID"]);
                    ob.BRK_QTY_PCS = (dr["BRK_QTY_PCS"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["BRK_QTY_PCS"]);
                    ob.RQD_QTY_PCS = (dr["RQD_QTY_PCS"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RQD_QTY_PCS"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);

                    ob.INV_ITEM_CAT_ID = (dr["INV_ITEM_CAT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_ITEM_CAT_ID"]);
                    ob.ITEM_NAME_EN = (dr["ITEM_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_NAME_EN"]);
                    ob.KNT_MACHINE_NO = (dr["KNT_MACHINE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["KNT_MACHINE_NO"]);
                    ob.EMP_FULL_NAME_EN = (dr["ITEM_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EMP_FULL_NAME_EN"]);

                }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public SCM_STR_NDL_REQ_DModel SelectStock(Int64? pINV_ITEM_ID = null, Int64? pFRM_REQ_STR_ID = null, Int64? pTO_REQ_STR_ID = null, Int64? pRF_NDL_REQ_TYPE_ID = null, Int64? pKNT_MACHINE_ID = null)
        {
            string sp = "PKG_KNT_MC_NDL.KNT_MCN_NDL_STK_SELECT";
            try
            {
                var ob = new SCM_STR_NDL_REQ_DModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pINV_ITEM_ID", Value =pINV_ITEM_ID},
                     new CommandParameter() {ParameterName = "pFRM_REQ_STR_ID", Value =pFRM_REQ_STR_ID},
                     new CommandParameter() {ParameterName = "pTO_REQ_STR_ID", Value =pTO_REQ_STR_ID},
                     new CommandParameter() {ParameterName = "pRF_NDL_REQ_TYPE_ID", Value =pRF_NDL_REQ_TYPE_ID},
                     new CommandParameter() {ParameterName = "pKNT_MACHINE_ID", Value =pKNT_MACHINE_ID},
                     
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.CENTRAL_STR_STOCK = (dr["CENTRAL_STR_STOCK"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CENTRAL_STR_STOCK"]);
                    ob.SUB_STR_STOCK = (dr["SUB_STR_STOCK"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SUB_STR_STOCK"]);
                    ob.BRK_QTY_PCS = (dr["BRK_QTY_PCS"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["BRK_QTY_PCS"]);
                }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<SCM_STR_NDL_REQ_DModel> SelectPendingBrokenItem(Int64? pFRM_REQ_STR_ID = null, Int64? pTO_REQ_STR_ID = null, Int64? pRF_NDL_REQ_TYPE_ID = null, Int64? pKNT_MC_TYPE_ID = null, DateTime? pFROM_DATE = null, DateTime? pTO_DATE = null)
        {
            string sp = "PKG_KNT_MCN_NEEDLE.knt_mcn_ndl_brk_h_select";
            try
            {
                var list = new List<SCM_STR_NDL_REQ_DModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pFRM_REQ_STR_ID", Value =pFRM_REQ_STR_ID},
                     new CommandParameter() {ParameterName = "pTO_REQ_STR_ID", Value =pTO_REQ_STR_ID},
                     new CommandParameter() {ParameterName = "pKNT_MC_TYPE_ID", Value =pKNT_MC_TYPE_ID},

                     new CommandParameter() {ParameterName = "pFROM_DATE", Value =pFROM_DATE},
                     new CommandParameter() {ParameterName = "pTO_DATE", Value =pTO_DATE},
                     
                     new CommandParameter() {ParameterName = "pOption", Value =3004},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    var ob = new SCM_STR_NDL_REQ_DModel();
                    ob.CENTRAL_STR_STOCK = (dr["CENTRAL_STR_STOCK"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CENTRAL_STR_STOCK"]);
                    ob.SUB_STR_STOCK = (dr["SUB_STR_STOCK"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SUB_STR_STOCK"]);
                    ob.BRK_QTY_PCS = (dr["BRK_QTY_PCS"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["BRK_QTY_PCS"]);
                    ob.RQD_QTY_PCS = ob.BRK_QTY_PCS;

                    ob.INV_ITEM_CAT_ID = (dr["INV_ITEM_CAT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_ITEM_CAT_ID"]);
                    ob.INV_ITEM_ID = (dr["INV_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_ITEM_ID"]);
                    ob.ITEM_NAME_EN = (dr["ITEM_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_NAME_EN"]);
                    ob.MCN_ITEM_LST = (dr["MCN_ITEM_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MCN_ITEM_LST"]);
                    ob.KNT_MACHINE_ID_LIST = (dr["KNT_MACHINE_ID_LIST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["KNT_MACHINE_ID_LIST"]);

                    list.Add(ob);
                }
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string ITEM_NAME_EN { get; set; }

        public string KNT_MACHINE_NO { get; set; }

        public string EMP_FULL_NAME_EN { get; set; }

        public long CENTRAL_STR_STOCK { get; set; }

        public long SUB_STR_STOCK { get; set; }

        public string KNT_MACHINE_ID_LIST { get; set; }

        public string MCN_ITEM_LST { get; set; }
    }
}
