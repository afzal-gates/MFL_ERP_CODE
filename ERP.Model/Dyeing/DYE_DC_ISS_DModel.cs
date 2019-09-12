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
    public class DYE_DC_ISS_DModel
    {
        public Int64 DYE_DC_ISS_D_ID { get; set; }
        public Int64 DYE_DC_ISS_H_ID { get; set; }
        public Int64 DC_ITEM_ID { get; set; }
        public Int64 SL_NO { get; set; }
        public Decimal PACK_ISS_QTY { get; set; }
        public Int64 PACK_MOU_ID { get; set; }
        public Decimal QTY_PER_PACK { get; set; }
        public Int64 ISS_QTY_K { get; set; }
        public Int64 ISS_QTY_G { get; set; }
        public Int64 ISS_QTY_M { get; set; }
        public Decimal ISS_QTY { get; set; }
        public Int64 QTY_MOU_ID { get; set; }
        public Decimal COST_PRICE { get; set; }
        public string SP_NOTE { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }

        public string Save()
        {
            const string sp = "SP_DYE_DC_ISS_D";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_DC_ISS_D_ID", Value = ob.DYE_DC_ISS_D_ID},
                     new CommandParameter() {ParameterName = "pDYE_DC_ISS_H_ID", Value = ob.DYE_DC_ISS_H_ID},
                     new CommandParameter() {ParameterName = "pDC_ITEM_ID", Value = ob.DC_ITEM_ID},
                     new CommandParameter() {ParameterName = "pSL_NO", Value = ob.SL_NO},
                     new CommandParameter() {ParameterName = "pPACK_ISS_QTY", Value = ob.PACK_ISS_QTY},
                     new CommandParameter() {ParameterName = "pPACK_MOU_ID", Value = ob.PACK_MOU_ID},
                     new CommandParameter() {ParameterName = "pQTY_PER_PACK", Value = ob.QTY_PER_PACK},
                     new CommandParameter() {ParameterName = "pISS_QTY_K", Value = ob.ISS_QTY_K},
                     new CommandParameter() {ParameterName = "pISS_QTY_G", Value = ob.ISS_QTY_G},
                     new CommandParameter() {ParameterName = "pISS_QTY_M", Value = ob.ISS_QTY_M},
                     new CommandParameter() {ParameterName = "pISS_QTY", Value = ob.ISS_QTY},
                     new CommandParameter() {ParameterName = "pQTY_MOU_ID", Value = ob.QTY_MOU_ID},
                     new CommandParameter() {ParameterName = "pCOST_PRICE", Value = ob.COST_PRICE},
                     new CommandParameter() {ParameterName = "pSP_NOTE", Value = ob.SP_NOTE},
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
            const string sp = "SP_DYE_DC_ISS_D";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_DC_ISS_D_ID", Value = ob.DYE_DC_ISS_D_ID},
                     new CommandParameter() {ParameterName = "pDYE_DC_ISS_H_ID", Value = ob.DYE_DC_ISS_H_ID},
                     new CommandParameter() {ParameterName = "pDC_ITEM_ID", Value = ob.DC_ITEM_ID},
                     new CommandParameter() {ParameterName = "pSL_NO", Value = ob.SL_NO},
                     new CommandParameter() {ParameterName = "pPACK_ISS_QTY", Value = ob.PACK_ISS_QTY},
                     new CommandParameter() {ParameterName = "pPACK_MOU_ID", Value = ob.PACK_MOU_ID},
                     new CommandParameter() {ParameterName = "pQTY_PER_PACK", Value = ob.QTY_PER_PACK},
                     new CommandParameter() {ParameterName = "pISS_QTY_K", Value = ob.ISS_QTY_K},
                     new CommandParameter() {ParameterName = "pISS_QTY_G", Value = ob.ISS_QTY_G},
                     new CommandParameter() {ParameterName = "pISS_QTY_M", Value = ob.ISS_QTY_M},
                     new CommandParameter() {ParameterName = "pISS_QTY", Value = ob.ISS_QTY},
                     new CommandParameter() {ParameterName = "pQTY_MOU_ID", Value = ob.QTY_MOU_ID},
                     new CommandParameter() {ParameterName = "pCOST_PRICE", Value = ob.COST_PRICE},
                     new CommandParameter() {ParameterName = "pSP_NOTE", Value = ob.SP_NOTE},
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
            const string sp = "PKG_DYE_DC_ISSUE.DYE_DC_ISS_D_DELETE";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_DC_ISS_D_ID", Value = ob.DYE_DC_ISS_D_ID},
                     new CommandParameter() {ParameterName = "pDYE_DC_ISS_H_ID", Value = ob.DYE_DC_ISS_H_ID},
                     new CommandParameter() {ParameterName = "pDC_ITEM_ID", Value = ob.DC_ITEM_ID},
                     new CommandParameter() {ParameterName = "pSL_NO", Value = ob.SL_NO},
                     new CommandParameter() {ParameterName = "pPACK_ISS_QTY", Value = ob.PACK_ISS_QTY},
                     new CommandParameter() {ParameterName = "pPACK_MOU_ID", Value = ob.PACK_MOU_ID},
                     new CommandParameter() {ParameterName = "pQTY_PER_PACK", Value = ob.QTY_PER_PACK},
                     new CommandParameter() {ParameterName = "pISS_QTY_K", Value = ob.ISS_QTY_K},
                     new CommandParameter() {ParameterName = "pISS_QTY_G", Value = ob.ISS_QTY_G},
                     new CommandParameter() {ParameterName = "pISS_QTY_M", Value = ob.ISS_QTY_M},
                     new CommandParameter() {ParameterName = "pISS_QTY", Value = ob.ISS_QTY},
                     new CommandParameter() {ParameterName = "pQTY_MOU_ID", Value = ob.QTY_MOU_ID},
                     new CommandParameter() {ParameterName = "pCOST_PRICE", Value = ob.COST_PRICE},
                     new CommandParameter() {ParameterName = "pSP_NOTE", Value = ob.SP_NOTE},
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

        public List<DYE_DC_ISS_DModel> SelectAll()
        {
            string sp = "pkg_dye_dc_issue.dye_dc_iss_d_select";
            try
            {
                var obList = new List<DYE_DC_ISS_DModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_DC_ISS_H_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DYE_DC_ISS_DModel ob = new DYE_DC_ISS_DModel();
                    ob.DYE_DC_ISS_D_ID = (dr["DYE_DC_ISS_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_DC_ISS_D_ID"]);
                    ob.DYE_DC_ISS_H_ID = (dr["DYE_DC_ISS_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_DC_ISS_H_ID"]);
                    ob.DC_ITEM_ID = (dr["DC_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DC_ITEM_ID"]);
                    ob.SL_NO = (dr["SL_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SL_NO"]);
                    ob.PACK_ISS_QTY = (dr["PACK_ISS_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PACK_ISS_QTY"]);
                    ob.PACK_MOU_ID = (dr["PACK_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PACK_MOU_ID"]);
                    ob.QTY_PER_PACK = (dr["QTY_PER_PACK"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["QTY_PER_PACK"]);
                    ob.ISS_QTY_K = (dr["ISS_QTY_K"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ISS_QTY_K"]);
                    ob.ISS_QTY_G = (dr["ISS_QTY_G"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ISS_QTY_G"]);
                    ob.ISS_QTY_M = (dr["ISS_QTY_M"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ISS_QTY_M"]);
                    ob.ISS_QTY = (dr["ISS_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["ISS_QTY"]);
                    ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);
                    ob.COST_PRICE = (dr["COST_PRICE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["COST_PRICE"]);
                    ob.SP_NOTE = (dr["SP_NOTE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SP_NOTE"]);
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

        public List<DYE_DC_ISS_DModel> Select(Int64? pDYE_DC_ISS_H_ID)
        {
            string sp = "pkg_dye_dc_issue.dye_dc_iss_d_select";
            try
            {
                var obList = new List<DYE_DC_ISS_DModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_DC_ISS_H_ID", Value =pDYE_DC_ISS_H_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DYE_DC_ISS_DModel ob = new DYE_DC_ISS_DModel();
                    ob.DYE_DC_ISS_D_ID = (dr["DYE_DC_ISS_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_DC_ISS_D_ID"]);
                    ob.DYE_DC_ISS_H_ID = (dr["DYE_DC_ISS_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_DC_ISS_H_ID"]);
                    ob.DC_ITEM_ID = (dr["DC_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DC_ITEM_ID"]);
                    ob.INV_ITEM_CAT_ID = (dr["INV_ITEM_CAT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_ITEM_CAT_ID"]);
                    ob.SL_NO = (dr["SL_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SL_NO"]);
                    ob.PACK_ISS_QTY = (dr["PACK_ISS_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PACK_ISS_QTY"]);
                    ob.PACK_MOU_ID = (dr["PACK_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PACK_MOU_ID"]);
                    ob.QTY_PER_PACK = (dr["QTY_PER_PACK"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["QTY_PER_PACK"]);
                    ob.ISS_QTY_K = (dr["ISS_QTY_K"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ISS_QTY_K"]);
                    ob.ISS_QTY_G = (dr["ISS_QTY_G"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ISS_QTY_G"]);
                    ob.ISS_QTY_M = (dr["ISS_QTY_M"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ISS_QTY_M"]);
                    ob.ISS_QTY = (dr["ISS_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["ISS_QTY"]);
                    ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);
                    ob.COST_PRICE = (dr["COST_PRICE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["COST_PRICE"]);
                    ob.SP_NOTE = (dr["SP_NOTE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SP_NOTE"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);

                    ob.ITEM_NAME_EN = (dr["ITEM_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_NAME_EN"]);
                    ob.BRAND_NAME_EN = (dr["BRAND_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BRAND_NAME_EN"]);
                    ob.MOU_CODE = (dr["MOU_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MOU_CODE"]);
                    ob.CENTRAL_STK = (dr["CENTRAL_STK"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["CENTRAL_STK"]);
                    ob.SUB_STK = (dr["SUB_STK"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["SUB_STK"]);
                    ob.RQD_QTY = (dr["RQD_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RQD_QTY"]);
                    ob.SUB_STK = ob.SUB_STK + ob.RQD_QTY;
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string ITEM_NAME_EN { get; set; }
        public string BRAND_NAME_EN { get; set; }
        public string MOU_CODE { get; set; }
        public decimal CENTRAL_STK { get; set; }
        public decimal SUB_STK { get; set; }

        public decimal RQD_QTY { get; set; }

        public long INV_ITEM_CAT_ID { get; set; }

        public List<INV_ITEMModel> ItemList { get; set; }
    }
}