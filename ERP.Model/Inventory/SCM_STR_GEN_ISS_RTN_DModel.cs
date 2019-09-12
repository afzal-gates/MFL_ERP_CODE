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
    public class SCM_STR_GEN_ISS_RTN_DModel
    {
        public Int64 SCM_STR_GEN_ISS_RTN_D_ID { get; set; }
        public Int64 SCM_STR_GEN_ISS_RTN_H_ID { get; set; }
        public Int64 SCM_STR_GEN_ISS_D_ID { get; set; }
        public Int64 INV_ITEM_CAT_ID { get; set; }
        public Int64 INV_ITEM_ID { get; set; }
        public Decimal RTN_QTY { get; set; }
        public Decimal RCV_QTY { get; set; }
        public Int64 QTY_MOU_ID { get; set; }
        public Decimal COST_PRICE { get; set; }
        public Int64 SEQ_NO { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }

        public string Save()
        {
            const string sp = "PKG_SCM_STR_REQ.SCM_STR_GEN_ISS_RTN_H_INSERT";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pSCM_STR_GEN_ISS_RTN_D_ID", Value = ob.SCM_STR_GEN_ISS_RTN_D_ID},
                     new CommandParameter() {ParameterName = "pSCM_STR_GEN_ISS_RTN_H_ID", Value = ob.SCM_STR_GEN_ISS_RTN_H_ID},
                     new CommandParameter() {ParameterName = "pSCM_STR_GEN_ISS_D_ID", Value = ob.SCM_STR_GEN_ISS_D_ID},
                     new CommandParameter() {ParameterName = "pINV_ITEM_CAT_ID", Value = ob.INV_ITEM_CAT_ID},
                     new CommandParameter() {ParameterName = "pINV_ITEM_ID", Value = ob.INV_ITEM_ID},
                     new CommandParameter() {ParameterName = "pRTN_QTY", Value = ob.RTN_QTY},
                     new CommandParameter() {ParameterName = "pRCV_QTY", Value = ob.RCV_QTY},
                     new CommandParameter() {ParameterName = "pQTY_MOU_ID", Value = ob.QTY_MOU_ID},
                     new CommandParameter() {ParameterName = "pCOST_PRICE", Value = ob.COST_PRICE},
                     new CommandParameter() {ParameterName = "pSEQ_NO", Value = ob.SEQ_NO},
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
            const string sp = "SP_SCM_STR_GEN_ISS_RTN_D";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pSCM_STR_GEN_ISS_RTN_D_ID", Value = ob.SCM_STR_GEN_ISS_RTN_D_ID},
                     new CommandParameter() {ParameterName = "pSCM_STR_GEN_ISS_RTN_H_ID", Value = ob.SCM_STR_GEN_ISS_RTN_H_ID},
                     new CommandParameter() {ParameterName = "pSCM_STR_GEN_ISS_D_ID", Value = ob.SCM_STR_GEN_ISS_D_ID},
                     new CommandParameter() {ParameterName = "pINV_ITEM_CAT_ID", Value = ob.INV_ITEM_CAT_ID},
                     new CommandParameter() {ParameterName = "pINV_ITEM_ID", Value = ob.INV_ITEM_ID},
                     new CommandParameter() {ParameterName = "pRTN_QTY", Value = ob.RTN_QTY},
                     new CommandParameter() {ParameterName = "pRCV_QTY", Value = ob.RCV_QTY},
                     new CommandParameter() {ParameterName = "pQTY_MOU_ID", Value = ob.QTY_MOU_ID},
                     new CommandParameter() {ParameterName = "pCOST_PRICE", Value = ob.COST_PRICE},
                     new CommandParameter() {ParameterName = "pSEQ_NO", Value = ob.SEQ_NO},
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
            const string sp = "SP_SCM_STR_GEN_ISS_RTN_D";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pSCM_STR_GEN_ISS_RTN_D_ID", Value = ob.SCM_STR_GEN_ISS_RTN_D_ID},
                     new CommandParameter() {ParameterName = "pSCM_STR_GEN_ISS_RTN_H_ID", Value = ob.SCM_STR_GEN_ISS_RTN_H_ID},
                     new CommandParameter() {ParameterName = "pSCM_STR_GEN_ISS_D_ID", Value = ob.SCM_STR_GEN_ISS_D_ID},
                     new CommandParameter() {ParameterName = "pINV_ITEM_CAT_ID", Value = ob.INV_ITEM_CAT_ID},
                     new CommandParameter() {ParameterName = "pINV_ITEM_ID", Value = ob.INV_ITEM_ID},
                     new CommandParameter() {ParameterName = "pRTN_QTY", Value = ob.RTN_QTY},
                     new CommandParameter() {ParameterName = "pRCV_QTY", Value = ob.RCV_QTY},
                     new CommandParameter() {ParameterName = "pQTY_MOU_ID", Value = ob.QTY_MOU_ID},
                     new CommandParameter() {ParameterName = "pCOST_PRICE", Value = ob.COST_PRICE},
                     new CommandParameter() {ParameterName = "pSEQ_NO", Value = ob.SEQ_NO},
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

        public List<SCM_STR_GEN_ISS_RTN_DModel> SelectAll()
        {
            string sp = "PKG_SCM_STR_REQ.SCM_STR_GEN_ISS_RTN_D_SELECT";
            try
            {
                var obList = new List<SCM_STR_GEN_ISS_RTN_DModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pSCM_STR_GEN_ISS_RTN_D_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    SCM_STR_GEN_ISS_RTN_DModel ob = new SCM_STR_GEN_ISS_RTN_DModel();
                    ob.SCM_STR_GEN_ISS_RTN_D_ID = (dr["SCM_STR_GEN_ISS_RTN_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_STR_GEN_ISS_RTN_D_ID"]);
                    ob.SCM_STR_GEN_ISS_RTN_H_ID = (dr["SCM_STR_GEN_ISS_RTN_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_STR_GEN_ISS_RTN_H_ID"]);
                    ob.SCM_STR_GEN_ISS_D_ID = (dr["SCM_STR_GEN_ISS_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_STR_GEN_ISS_D_ID"]);
                    ob.INV_ITEM_CAT_ID = (dr["INV_ITEM_CAT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_ITEM_CAT_ID"]);
                    ob.INV_ITEM_ID = (dr["INV_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_ITEM_ID"]);
                    ob.RTN_QTY = (dr["RTN_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RTN_QTY"]);
                    ob.RCV_QTY = (dr["RCV_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RCV_QTY"]);
                    ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);
                    ob.COST_PRICE = (dr["COST_PRICE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["COST_PRICE"]);
                    ob.SEQ_NO = (dr["SEQ_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SEQ_NO"]);
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


        public List<SCM_STR_GEN_ISS_RTN_DModel> SelectByID(Int64? pSCM_STR_GEN_ISS_RTN_H_ID=null)
        {
            string sp = "PKG_SCM_STR_REQ.SCM_STR_GEN_ISS_RTN_D_SELECT";
            try
            {
                var obList = new List<SCM_STR_GEN_ISS_RTN_DModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pSCM_STR_GEN_ISS_RTN_H_ID", Value =pSCM_STR_GEN_ISS_RTN_H_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    SCM_STR_GEN_ISS_RTN_DModel ob = new SCM_STR_GEN_ISS_RTN_DModel();
                    ob.SCM_STR_GEN_ISS_RTN_D_ID = (dr["SCM_STR_GEN_ISS_RTN_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_STR_GEN_ISS_RTN_D_ID"]);
                    ob.SCM_STR_GEN_ISS_RTN_H_ID = (dr["SCM_STR_GEN_ISS_RTN_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_STR_GEN_ISS_RTN_H_ID"]);
                    ob.SCM_STR_GEN_ISS_D_ID = (dr["SCM_STR_GEN_ISS_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_STR_GEN_ISS_D_ID"]);
                    ob.INV_ITEM_CAT_ID = (dr["INV_ITEM_CAT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_ITEM_CAT_ID"]);
                    ob.INV_ITEM_ID = (dr["INV_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_ITEM_ID"]);
                    ob.RTN_QTY = (dr["RTN_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RTN_QTY"]);
                    ob.RCV_QTY = (dr["RCV_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RCV_QTY"]);
                    ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);
                    ob.COST_PRICE = (dr["COST_PRICE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["COST_PRICE"]);
                    ob.SEQ_NO = (dr["SEQ_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SEQ_NO"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);
                    ob.ISS_QTY = (dr["ISS_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["ISS_QTY"]);
                    
                    ob.ITEM_CAT_NAME_EN = (dr["ITEM_CAT_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_CAT_NAME_EN"]);
                    ob.ITEM_NAME_EN = (dr["ITEM_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_NAME_EN"]);
                    ob.MOU_CODE = (dr["MOU_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MOU_CODE"]);
                    
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public SCM_STR_GEN_ISS_RTN_DModel Select(long ID)
        {
            string sp = "PKG_SCM_STR_REQ.SCM_STR_GEN_ISS_RTN_D_SELECT";
            try
            {
                var ob = new SCM_STR_GEN_ISS_RTN_DModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pSCM_STR_GEN_ISS_RTN_D_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.SCM_STR_GEN_ISS_RTN_D_ID = (dr["SCM_STR_GEN_ISS_RTN_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_STR_GEN_ISS_RTN_D_ID"]);
                    ob.SCM_STR_GEN_ISS_RTN_H_ID = (dr["SCM_STR_GEN_ISS_RTN_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_STR_GEN_ISS_RTN_H_ID"]);
                    ob.SCM_STR_GEN_ISS_D_ID = (dr["SCM_STR_GEN_ISS_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_STR_GEN_ISS_D_ID"]);
                    ob.INV_ITEM_CAT_ID = (dr["INV_ITEM_CAT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_ITEM_CAT_ID"]);
                    ob.INV_ITEM_ID = (dr["INV_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_ITEM_ID"]);
                    ob.RTN_QTY = (dr["RTN_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RTN_QTY"]);
                    ob.RCV_QTY = (dr["RCV_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RCV_QTY"]);
                    ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);
                    ob.COST_PRICE = (dr["COST_PRICE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["COST_PRICE"]);
                    ob.SEQ_NO = (dr["SEQ_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SEQ_NO"]);
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

        public string ITEM_CAT_NAME_EN { get; set; }

        public string ITEM_NAME_EN { get; set; }

        public string MOU_CODE { get; set; }

        public decimal ISS_QTY { get; set; }
    }
}