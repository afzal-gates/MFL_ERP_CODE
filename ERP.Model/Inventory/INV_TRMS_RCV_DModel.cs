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
    public class INV_TRMS_RCV_DModel
    {
        public Int64 INV_TRMS_RCV_D_ID { get; set; }
        public Int64 INV_TRMS_RCV_H_ID { get; set; }
        public Int64 MC_ORD_TRMS_ITEM_ID { get; set; }
        public string ITEM_SPEC_AUTO { get; set; }
        public Decimal RCV_QTY { get; set; }
        public Int64 QTY_MOU_ID { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }

        public string ITEM_NAME_EN { get; set; }
        public string MOU_CODE { get; set; }

        public string Save()
        {
            const string sp = "SP_INV_TRMS_RCV_D";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pINV_TRMS_RCV_D_ID", Value = ob.INV_TRMS_RCV_D_ID},
                     new CommandParameter() {ParameterName = "pINV_TRMS_RCV_H_ID", Value = ob.INV_TRMS_RCV_H_ID},
                     new CommandParameter() {ParameterName = "pMC_ORD_TRMS_ITEM_ID", Value = ob.MC_ORD_TRMS_ITEM_ID},
                     new CommandParameter() {ParameterName = "pITEM_SPEC_AUTO", Value = ob.ITEM_SPEC_AUTO},
                     new CommandParameter() {ParameterName = "pRCV_QTY", Value = ob.RCV_QTY},
                     new CommandParameter() {ParameterName = "pQTY_MOU_ID", Value = ob.QTY_MOU_ID},
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
            const string sp = "SP_INV_TRMS_RCV_D";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pINV_TRMS_RCV_D_ID", Value = ob.INV_TRMS_RCV_D_ID},
                     new CommandParameter() {ParameterName = "pINV_TRMS_RCV_H_ID", Value = ob.INV_TRMS_RCV_H_ID},
                     new CommandParameter() {ParameterName = "pMC_ORD_TRMS_ITEM_ID", Value = ob.MC_ORD_TRMS_ITEM_ID},
                     new CommandParameter() {ParameterName = "pITEM_SPEC_AUTO", Value = ob.ITEM_SPEC_AUTO},
                     new CommandParameter() {ParameterName = "pRCV_QTY", Value = ob.RCV_QTY},
                     new CommandParameter() {ParameterName = "pQTY_MOU_ID", Value = ob.QTY_MOU_ID},
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
            const string sp = "SP_INV_TRMS_RCV_D";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pINV_TRMS_RCV_D_ID", Value = ob.INV_TRMS_RCV_D_ID},
                     new CommandParameter() {ParameterName = "pINV_TRMS_RCV_H_ID", Value = ob.INV_TRMS_RCV_H_ID},
                     new CommandParameter() {ParameterName = "pMC_ORD_TRMS_ITEM_ID", Value = ob.MC_ORD_TRMS_ITEM_ID},
                     new CommandParameter() {ParameterName = "pITEM_SPEC_AUTO", Value = ob.ITEM_SPEC_AUTO},
                     new CommandParameter() {ParameterName = "pRCV_QTY", Value = ob.RCV_QTY},
                     new CommandParameter() {ParameterName = "pQTY_MOU_ID", Value = ob.QTY_MOU_ID},
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

        public List<INV_TRMS_RCV_DModel> SelectAll()
        {
            string sp = "pkg_inv_trims_rcv.inv_trms_rcv_d_select";
            try
            {
                var obList = new List<INV_TRMS_RCV_DModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pINV_TRMS_RCV_D_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    INV_TRMS_RCV_DModel ob = new INV_TRMS_RCV_DModel();
                    ob.INV_TRMS_RCV_D_ID = (dr["INV_TRMS_RCV_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_TRMS_RCV_D_ID"]);
                    ob.INV_TRMS_RCV_H_ID = (dr["INV_TRMS_RCV_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_TRMS_RCV_H_ID"]);
                    ob.MC_ORD_TRMS_ITEM_ID = (dr["MC_ORD_TRMS_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_ORD_TRMS_ITEM_ID"]);
                    ob.ITEM_SPEC_AUTO = (dr["ITEM_SPEC_AUTO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_SPEC_AUTO"]);
                    ob.RCV_QTY = (dr["RCV_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RCV_QTY"]);
                    ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);
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

        public List<INV_TRMS_RCV_DModel> SelectByID(Int64? pINV_TRMS_RCV_H_ID = null)
        {
            string sp = "pkg_inv_trims_rcv.inv_trms_rcv_d_select";
            try
            {
                var obList = new List<INV_TRMS_RCV_DModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pINV_TRMS_RCV_H_ID", Value =pINV_TRMS_RCV_H_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    INV_TRMS_RCV_DModel ob = new INV_TRMS_RCV_DModel();
                    ob.INV_TRMS_RCV_D_ID = (dr["INV_TRMS_RCV_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_TRMS_RCV_D_ID"]);
                    ob.INV_TRMS_RCV_H_ID = (dr["INV_TRMS_RCV_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_TRMS_RCV_H_ID"]);
                    ob.MC_ORD_TRMS_ITEM_ID = (dr["MC_ORD_TRMS_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_ORD_TRMS_ITEM_ID"]);
                    ob.ITEM_SPEC_AUTO = (dr["ITEM_SPEC_AUTO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_SPEC_AUTO"]);
                    ob.RCV_QTY = (dr["RCV_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RCV_QTY"]);
                    ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);
                    ob.CM_IMP_PO_D_ID = (dr["CM_IMP_PO_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CM_IMP_PO_D_ID"]);

                    ob.ITEM_NAME_EN = (dr["ITEM_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_NAME_EN"]);
                    ob.MOU_CODE = (dr["MOU_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MOU_CODE"]);
                    ob.PO_QTY = (dr["PO_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PO_QTY"]);
                    
                    ob.BUYER_NAME_EN = (dr["BUYER_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BUYER_NAME_EN"]);
                    ob.STYLE_NO = (dr["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO"]);
                    ob.ORDER_NO = (dr["ORDER_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_NO"]);

                    ob.ORDER_DTL = ob.BUYER_NAME_EN + " | " + ob.STYLE_NO + " | " + ob.ORDER_NO;

                    ob.ITEM_SPEC_AUTO = (dr["ITEM_SPEC_AUTO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_SPEC_AUTO"]);
                    ob.ITEM_CODE = (dr["ITEM_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_CODE"]);
                    ob.PARTICULARS = (dr["PARTICULARS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PARTICULARS"]);
                    

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public string BUYER_NAME_EN { get; set; }

        public string STYLE_NO { get; set; }

        public string ORDER_NO { get; set; }

        public string ORDER_DTL { get; set; }

        public string ITEM_CODE { get; set; }

        public string PARTICULARS { get; set; }

        public decimal PO_QTY { get; set; }

        public long CM_IMP_PO_D_ID { get; set; }
    }
}