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
    public class DYE_ITEM_STKModel
    {
        public Int64 DYE_ITEM_STK_ID { get; set; }
        public Int64 DC_ITEM_ID { get; set; }
        public Int64 SCM_STORE_ID { get; set; }
        public Int64 QTY_MOU_ID { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }
        public Decimal RCV_QTY { get; set; }
        public Decimal ISSUE_QTY { get; set; }
        public Decimal ADJ_QTY { get; set; }
        public Decimal ALOC_QTY { get; set; }

        public string Save()
        {
            const string sp = "SP_DYE_ITEM_STK";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_ITEM_STK_ID", Value = ob.DYE_ITEM_STK_ID},
                     new CommandParameter() {ParameterName = "pDC_ITEM_ID", Value = ob.DC_ITEM_ID},
                     new CommandParameter() {ParameterName = "pSCM_STORE_ID", Value = ob.SCM_STORE_ID},
                     new CommandParameter() {ParameterName = "pQTY_MOU_ID", Value = ob.QTY_MOU_ID},
                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = ob.CREATED_BY},
                     new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = ob.LAST_UPDATED_BY},
                     new CommandParameter() {ParameterName = "pRCV_QTY", Value = ob.RCV_QTY},
                     new CommandParameter() {ParameterName = "pISSUE_QTY", Value = ob.ISSUE_QTY},
                     new CommandParameter() {ParameterName = "pADJ_QTY", Value = ob.ADJ_QTY},
                     new CommandParameter() {ParameterName = "pALOC_QTY", Value = ob.ALOC_QTY},
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
            const string sp = "SP_DYE_ITEM_STK";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_ITEM_STK_ID", Value = ob.DYE_ITEM_STK_ID},
                     new CommandParameter() {ParameterName = "pDC_ITEM_ID", Value = ob.DC_ITEM_ID},
                     new CommandParameter() {ParameterName = "pSCM_STORE_ID", Value = ob.SCM_STORE_ID},
                     new CommandParameter() {ParameterName = "pQTY_MOU_ID", Value = ob.QTY_MOU_ID},
                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = ob.CREATED_BY},
                     new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = ob.LAST_UPDATED_BY},
                     new CommandParameter() {ParameterName = "pRCV_QTY", Value = ob.RCV_QTY},
                     new CommandParameter() {ParameterName = "pISSUE_QTY", Value = ob.ISSUE_QTY},
                     new CommandParameter() {ParameterName = "pADJ_QTY", Value = ob.ADJ_QTY},
                     new CommandParameter() {ParameterName = "pALOC_QTY", Value = ob.ALOC_QTY},
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
            const string sp = "SP_DYE_ITEM_STK";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_ITEM_STK_ID", Value = ob.DYE_ITEM_STK_ID},
                     new CommandParameter() {ParameterName = "pDC_ITEM_ID", Value = ob.DC_ITEM_ID},
                     new CommandParameter() {ParameterName = "pSCM_STORE_ID", Value = ob.SCM_STORE_ID},
                     new CommandParameter() {ParameterName = "pQTY_MOU_ID", Value = ob.QTY_MOU_ID},
                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = ob.CREATED_BY},
                     new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = ob.LAST_UPDATED_BY},
                     new CommandParameter() {ParameterName = "pRCV_QTY", Value = ob.RCV_QTY},
                     new CommandParameter() {ParameterName = "pISSUE_QTY", Value = ob.ISSUE_QTY},
                     new CommandParameter() {ParameterName = "pADJ_QTY", Value = ob.ADJ_QTY},
                     new CommandParameter() {ParameterName = "pALOC_QTY", Value = ob.ALOC_QTY},
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

        public List<DYE_ITEM_STKModel> SelectAll()
        {
            string sp = "Select_DYE_ITEM_STK";
            try
            {
                var obList = new List<DYE_ITEM_STKModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_ITEM_STK_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DYE_ITEM_STKModel ob = new DYE_ITEM_STKModel();
                    ob.DYE_ITEM_STK_ID = (dr["DYE_ITEM_STK_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_ITEM_STK_ID"]);
                    ob.DC_ITEM_ID = (dr["DC_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DC_ITEM_ID"]);
                    ob.SCM_STORE_ID = (dr["SCM_STORE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_STORE_ID"]);
                    ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);
                    ob.RCV_QTY = (dr["RCV_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RCV_QTY"]);
                    ob.ISSUE_QTY = (dr["ISSUE_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["ISSUE_QTY"]);
                    ob.ADJ_QTY = (dr["ADJ_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["ADJ_QTY"]);
                    ob.ALOC_QTY = (dr["ALOC_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["ALOC_QTY"]);
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DYE_ITEM_STKModel> SelectByID(Int64? pINV_ITEM_ID = null, Int64? pSCM_STORE_ID = null, string pINV_ITEM_CAT_LST = null)
        {
            string sp = "PKG_SCM_INV_ADJ.DYE_ITEM_STK_SELECT";
            try
            {
                var obList = new List<DYE_ITEM_STKModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pINV_ITEM_ID", Value =pINV_ITEM_ID},
                     new CommandParameter() {ParameterName = "pSCM_STORE_ID", Value =pSCM_STORE_ID},
                     new CommandParameter() {ParameterName = "pINV_ITEM_CAT_LST", Value =pINV_ITEM_CAT_LST},
                     new CommandParameter() {ParameterName = "pOption", Value =3001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DYE_ITEM_STKModel ob = new DYE_ITEM_STKModel();
                    ob.SCM_STORE_ID = (dr["SCM_STORE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_STORE_ID"]);
                    ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);
                    ob.RCV_QTY = (dr["RCV_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RCV_QTY"]);
                    ob.ISSUE_QTY = (dr["ISSUE_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["ISSUE_QTY"]);
                    ob.ADJ_QTY = (dr["ADJ_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["ADJ_QTY"]);
                    ob.ALOC_QTY = (dr["ALOC_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["ALOC_QTY"]);

                    ob.INV_ITEM_ID = (dr["INV_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_ITEM_ID"]);
                    ob.ITEM_NAME_EN = (dr["ITEM_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_NAME_EN"]);
                    ob.MOU_CODE = "Kg"; // (dr["MOU_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MOU_CODE"]);

                    ob.STOCK_QTY = Math.Round((ob.RCV_QTY - ob.ISSUE_QTY)/1000,3);
                    //ob.ADJ_STOCK_QTY = 0;
                    

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DYE_ITEM_STKModel Select(long ID)
        {
            string sp = "Select_DYE_ITEM_STK";
            try
            {
                var ob = new DYE_ITEM_STKModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_ITEM_STK_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.DYE_ITEM_STK_ID = (dr["DYE_ITEM_STK_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_ITEM_STK_ID"]);
                    ob.DC_ITEM_ID = (dr["DC_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DC_ITEM_ID"]);
                    ob.SCM_STORE_ID = (dr["SCM_STORE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_STORE_ID"]);
                    ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);
                    ob.RCV_QTY = (dr["RCV_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RCV_QTY"]);
                    ob.ISSUE_QTY = (dr["ISSUE_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["ISSUE_QTY"]);
                    ob.ADJ_QTY = (dr["ADJ_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["ADJ_QTY"]);
                    ob.ALOC_QTY = (dr["ALOC_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["ALOC_QTY"]);

                }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string ITEM_NAME_EN { get; set; }

        public string MOU_CODE { get; set; }

        public long INV_ITEM_ID { get; set; }

        public decimal STOCK_QTY { get; set; }

        public decimal ADJ_STOCK_QTY { get; set; }
    }
}