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
    public class DYE_DC_RCV_DModel
    {
        public Int64 DYE_DC_RCV_D_ID { get; set; }
        public Int64 DYE_DC_RCV_H_ID { get; set; }
        public Int64 DC_ITEM_ID { get; set; }
        public Decimal PACK_RCV_QTY { get; set; }
        public Int64 PACK_MOU_ID { get; set; }
        public Decimal QTY_PER_PACK { get; set; }
        public Decimal LOOSE_QTY { get; set; }
        public Decimal RCV_QTY { get; set; }
        public Int64 QTY_MOU_ID { get; set; }
        public Decimal UNIT_PRICE { get; set; }
        public Decimal LOC_UNIT_PRICE { get; set; }
        public string SP_NOTE { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }

        public string ITEM_NAME_EN { get; set; }
        public string MOU_CODE { get; set; }

        public string Save()
        {
            const string sp = "SP_DYE_DC_RCV_D";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_DC_RCV_D_ID", Value = ob.DYE_DC_RCV_D_ID},
                     new CommandParameter() {ParameterName = "pDYE_DC_RCV_H_ID", Value = ob.DYE_DC_RCV_H_ID},
                     new CommandParameter() {ParameterName = "pDC_ITEM_ID", Value = ob.DC_ITEM_ID},
                     new CommandParameter() {ParameterName = "pPACK_RCV_QTY", Value = ob.PACK_RCV_QTY},
                     new CommandParameter() {ParameterName = "pPACK_MOU_ID", Value = ob.PACK_MOU_ID},
                     new CommandParameter() {ParameterName = "pQTY_PER_PACK", Value = ob.QTY_PER_PACK},
                     new CommandParameter() {ParameterName = "pLOOSE_QTY", Value = ob.LOOSE_QTY},
                     new CommandParameter() {ParameterName = "pRCV_QTY", Value = ob.RCV_QTY},
                     new CommandParameter() {ParameterName = "pQTY_MOU_ID", Value = ob.QTY_MOU_ID},
                     new CommandParameter() {ParameterName = "pUNIT_PRICE", Value = ob.UNIT_PRICE},
                     new CommandParameter() {ParameterName = "pLOC_UNIT_PRICE", Value = ob.LOC_UNIT_PRICE},
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
            const string sp = "SP_DYE_DC_RCV_D";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_DC_RCV_D_ID", Value = ob.DYE_DC_RCV_D_ID},
                     new CommandParameter() {ParameterName = "pDYE_DC_RCV_H_ID", Value = ob.DYE_DC_RCV_H_ID},
                     new CommandParameter() {ParameterName = "pDC_ITEM_ID", Value = ob.DC_ITEM_ID},
                     new CommandParameter() {ParameterName = "pPACK_RCV_QTY", Value = ob.PACK_RCV_QTY},
                     new CommandParameter() {ParameterName = "pPACK_MOU_ID", Value = ob.PACK_MOU_ID},
                     new CommandParameter() {ParameterName = "pQTY_PER_PACK", Value = ob.QTY_PER_PACK},
                     new CommandParameter() {ParameterName = "pLOOSE_QTY", Value = ob.LOOSE_QTY},
                     new CommandParameter() {ParameterName = "pRCV_QTY", Value = ob.RCV_QTY},
                     new CommandParameter() {ParameterName = "pQTY_MOU_ID", Value = ob.QTY_MOU_ID},
                     new CommandParameter() {ParameterName = "pUNIT_PRICE", Value = ob.UNIT_PRICE},
                     new CommandParameter() {ParameterName = "pLOC_UNIT_PRICE", Value = ob.LOC_UNIT_PRICE},
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
            const string sp = "SP_DYE_DC_RCV_D";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_DC_RCV_D_ID", Value = ob.DYE_DC_RCV_D_ID},
                     new CommandParameter() {ParameterName = "pDYE_DC_RCV_H_ID", Value = ob.DYE_DC_RCV_H_ID},
                     new CommandParameter() {ParameterName = "pDC_ITEM_ID", Value = ob.DC_ITEM_ID},
                     new CommandParameter() {ParameterName = "pPACK_RCV_QTY", Value = ob.PACK_RCV_QTY},
                     new CommandParameter() {ParameterName = "pPACK_MOU_ID", Value = ob.PACK_MOU_ID},
                     new CommandParameter() {ParameterName = "pQTY_PER_PACK", Value = ob.QTY_PER_PACK},
                     new CommandParameter() {ParameterName = "pLOOSE_QTY", Value = ob.LOOSE_QTY},
                     new CommandParameter() {ParameterName = "pRCV_QTY", Value = ob.RCV_QTY},
                     new CommandParameter() {ParameterName = "pQTY_MOU_ID", Value = ob.QTY_MOU_ID},
                     new CommandParameter() {ParameterName = "pUNIT_PRICE", Value = ob.UNIT_PRICE},
                     new CommandParameter() {ParameterName = "pLOC_UNIT_PRICE", Value = ob.LOC_UNIT_PRICE},
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

        public List<DYE_DC_RCV_DModel> SelectAll()
        {
            string sp = "pkg_dye_dc_receive.dye_dc_rcv_h_select";
            try
            {
                var obList = new List<DYE_DC_RCV_DModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_DC_RCV_D_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DYE_DC_RCV_DModel ob = new DYE_DC_RCV_DModel();
                    ob.DYE_DC_RCV_D_ID = (dr["DYE_DC_RCV_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_DC_RCV_D_ID"]);
                    ob.DYE_DC_RCV_H_ID = (dr["DYE_DC_RCV_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_DC_RCV_H_ID"]);
                    ob.DC_ITEM_ID = (dr["DC_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DC_ITEM_ID"]);
                    ob.PACK_RCV_QTY = (dr["PACK_RCV_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PACK_RCV_QTY"]);
                    ob.PACK_MOU_ID = (dr["PACK_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PACK_MOU_ID"]);
                    ob.QTY_PER_PACK = (dr["QTY_PER_PACK"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["QTY_PER_PACK"]);
                    ob.LOOSE_QTY = (dr["LOOSE_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["LOOSE_QTY"]);
                    ob.RCV_QTY = (dr["RCV_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RCV_QTY"]);
                    ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);
                    ob.UNIT_PRICE = (dr["UNIT_PRICE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["UNIT_PRICE"]);
                    ob.LOC_UNIT_PRICE = (dr["LOC_UNIT_PRICE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["LOC_UNIT_PRICE"]);
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

        public List<DYE_DC_RCV_DModel> Select(Int64? ID)
        {
            string sp = "pkg_dye_dc_receive.dye_dc_rcv_d_select";
            try
            {
                List<DYE_DC_RCV_DModel> list = new List<DYE_DC_RCV_DModel>();
                
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_DC_RCV_H_ID", Value = ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    var ob = new DYE_DC_RCV_DModel();
                    ob.DYE_DC_RCV_D_ID = (dr["DYE_DC_RCV_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_DC_RCV_D_ID"]);
                    ob.DYE_DC_RCV_H_ID = (dr["DYE_DC_RCV_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_DC_RCV_H_ID"]);
                    ob.DC_ITEM_ID = (dr["DC_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DC_ITEM_ID"]);
                    ob.PACK_RCV_QTY = (dr["PACK_RCV_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PACK_RCV_QTY"]);
                    ob.PACK_MOU_ID = (dr["PACK_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PACK_MOU_ID"]);
                    ob.QTY_PER_PACK = (dr["QTY_PER_PACK"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["QTY_PER_PACK"]);
                    ob.LOOSE_QTY = (dr["LOOSE_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["LOOSE_QTY"]);
                    ob.RCV_QTY = (dr["RCV_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RCV_QTY"]);
                    ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);
                    ob.UNIT_PRICE = (dr["UNIT_PRICE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["UNIT_PRICE"]);
                    //ob.LOC_UNIT_PRICE = (dr["LOC_UNIT_PRICE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["LOC_UNIT_PRICE"]);                   
                    ob.PCT_ADDL_PRICE = (dr["PCT_ADDL_PRICE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PCT_ADDL_PRICE"]);
                    
                    ob.SP_NOTE = (dr["SP_NOTE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SP_NOTE"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);

                    ob.INV_ITEM_CAT_ID = (dr["INV_ITEM_CAT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_ITEM_CAT_ID"]);
                    ob.COST_PRICE = (dr["COST_PRICE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["COST_PRICE"]);
                    ob.TOTAL_AMT = ob.RCV_QTY * ob.UNIT_PRICE;

                    ob.TOTAL_AMT_BDT = ob.RCV_QTY * ob.COST_PRICE;

                    ob.ITEM_NAME_EN = (dr["ITEM_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_NAME_EN"]);
                    ob.MOU_CODE = (dr["MOU_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MOU_CODE"]);
                    ob.BRAND_NAME_EN = (dr["BRAND_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BRAND_NAME_EN"]);
                    ob.PACK_MOU_CODE = (dr["PACK_MOU_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PACK_MOU_CODE"]);
                    
                    list.Add(ob);
                }
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public decimal TOTAL_AMT { get; set; }

        public long INV_ITEM_CAT_ID { get; set; }

        public string BRAND_NAME_EN { get; set; }

        public decimal PCT_ADDL_PRICE { get; set; }

        public string PACK_MOU_CODE { get; set; }

        public decimal TOTAL_AMT_BDT { get; set; }

        public decimal COST_PRICE { get; set; }
    }
}