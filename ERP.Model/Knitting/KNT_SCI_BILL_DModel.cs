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
    public class KNT_SCI_BILL_DModel
    {
        public Int64 KNT_SCI_BILL_D_ID { get; set; }
        public Int64 KNT_SCI_BILL_H_ID { get; set; }
        public Int64 KNT_SC_GFAB_DLV_H_ID { get; set; }
        public string CHALAN_NO { get; set; }
        public Int64 KNT_STYL_FAB_ITEM_ID { get; set; }
        public Decimal CHLN_QTY { get; set; }
        public Decimal DEDUCT_QTY { get; set; }
        public Decimal NET_QTY { get; set; }
        public Int64 QTY_MOU_ID { get; set; }
        public Decimal PROD_RATE { get; set; }
        public Int64 ACC_COA_ID { get; set; }
        public string IS_PAY_BY_BILL { get; set; }

        public string BUYER_SNAME { get; set; }
        public string STYLE_NO { get; set; }
        public string ORDER_NO_LST { get; set; }
        public string COLOR_NAME_EN { get; set; }
        public string FAB_TYPE_SNAME { get; set; }
        public string ACT_FIN_GSM { get; set; }
        public string ACT_FIN_DIA { get; set; }
        public string FIB_COMP_CODE { get; set; }
        public string FABRIC_DESC { get; set; }
        public Decimal BILL_AMT { get; set; }




        public List<KNT_SCI_BILL_DModel> GetBillDtl(Int64 pKNT_SCI_BILL_H_ID)
        {
            string sp = "pkg_knit_subcontract.knt_sci_bill_h_select";
            try
            {
                var obList = new List<KNT_SCI_BILL_DModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_SCI_BILL_H_ID", Value =pKNT_SCI_BILL_H_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3002},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    KNT_SCI_BILL_DModel ob = new KNT_SCI_BILL_DModel();
                    ob.KNT_SCI_BILL_D_ID = (dr["KNT_SCI_BILL_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_SCI_BILL_D_ID"]);
                    ob.KNT_SCI_BILL_H_ID = (dr["KNT_SCI_BILL_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_SCI_BILL_H_ID"]);
                    ob.KNT_SC_GFAB_DLV_H_ID = (dr["KNT_SC_GFAB_DLV_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_SC_GFAB_DLV_H_ID"]);
                    ob.CHALAN_NO = (dr["CHALAN_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CHALAN_NO"]);
                    ob.KNT_STYL_FAB_ITEM_ID = (dr["KNT_STYL_FAB_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_STYL_FAB_ITEM_ID"]);
                    ob.CHLN_QTY = (dr["CHLN_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["CHLN_QTY"]);
                    ob.DEDUCT_QTY = (dr["DEDUCT_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["DEDUCT_QTY"]);
                    ob.NET_QTY = (dr["NET_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["NET_QTY"]);
                    ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);
                    ob.PROD_RATE = (dr["PROD_RATE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PROD_RATE"]);
                    ob.ACC_COA_ID = (dr["ACC_COA_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ACC_COA_ID"]);


                    ob.BUYER_SNAME = (dr["BUYER_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BUYER_SNAME"]);
                    ob.STYLE_NO = (dr["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO"]);
                    ob.ORDER_NO_LST = (dr["ORDER_NO_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_NO_LST"]);
                    ob.COLOR_NAME_EN = (dr["COLOR_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_NAME_EN"]);
                    ob.FAB_TYPE_SNAME = (dr["FAB_TYPE_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FAB_TYPE_SNAME"]);
                    ob.ACT_FIN_GSM = (dr["ACT_FIN_GSM"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ACT_FIN_GSM"]);
                    ob.ACT_FIN_DIA = (dr["ACT_FIN_DIA"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ACT_FIN_DIA"]);
                    ob.FIB_COMP_CODE = (dr["FIB_COMP_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FIB_COMP_CODE"]);
                    ob.FABRIC_DESC = (dr["FABRIC_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FABRIC_DESC"]);
                    ob.IS_PAY_BY_BILL = (dr["IS_PAY_BY_BILL"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_PAY_BY_BILL"]);
                    
                    ob.BILL_AMT = ob.NET_QTY * ob.PROD_RATE;
                    
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public KNT_SCI_BILL_DModel Select(long ID)
        {
            string sp = "Select_KNT_SCI_BILL_D";
            try
            {
                var ob = new KNT_SCI_BILL_DModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_SCI_BILL_D_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.KNT_SCI_BILL_D_ID = (dr["KNT_SCI_BILL_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_SCI_BILL_D_ID"]);
                    ob.KNT_SCI_BILL_H_ID = (dr["KNT_SCI_BILL_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_SCI_BILL_H_ID"]);
                    ob.KNT_SC_GFAB_DLV_H_ID = (dr["KNT_SC_GFAB_DLV_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_SC_GFAB_DLV_H_ID"]);
                    ob.CHALAN_NO = (dr["CHALAN_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CHALAN_NO"]);
                    ob.KNT_STYL_FAB_ITEM_ID = (dr["KNT_STYL_FAB_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_STYL_FAB_ITEM_ID"]);
                    ob.CHLN_QTY = (dr["CHLN_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["CHLN_QTY"]);
                    ob.DEDUCT_QTY = (dr["DEDUCT_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["DEDUCT_QTY"]);
                    ob.NET_QTY = (dr["NET_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["NET_QTY"]);
                    ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);
                    ob.PROD_RATE = (dr["PROD_RATE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PROD_RATE"]);
                    ob.ACC_COA_ID = (dr["ACC_COA_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ACC_COA_ID"]);

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