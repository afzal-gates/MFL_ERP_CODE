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
    public class KNT_SC_GFAB_DLV_D1Model
    {
        public Int64 KNT_SC_GFAB_DLV_D1_ID { get; set; }
        public Int64 KNT_SC_GFAB_DLV_H_ID { get; set; }
        public Int64 KNT_STYL_FAB_ITEM_ID { get; set; }
        public Int64 DELV_ROLL_QTY { get; set; }
        public Decimal DELV_GFAB_WT { get; set; }
        public Int64 WT_MOU_ID { get; set; }
        public string IS_PAY_BY_BILL { get; set; }

        public decimal PROD_RATE { get; set; }
        public int? MIN_PROD_RATE { get; set; }

        public string BUYER_SNAME { get; set; }
        public string STYLE_NO { get; set; }
        public string COLOR_NAME_EN { get; set; }
        public string ORDER_NO_LST { get; set; }
        public string FAB_TYPE_SNAME { get; set; }
        public string ACT_FIN_GSM { get; set; }
        public string ACT_FIN_DIA { get; set; }
        public string FIB_COMP_CODE { get; set; }
        public string FABRIC_DESC { get; set; }


        public List<KNT_SC_GFAB_DLV_D1Model> SelectAll()
        {
            string sp = "Select_KNT_SC_GFAB_DLV_D1";
            try
            {
                var obList = new List<KNT_SC_GFAB_DLV_D1Model>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_SC_GFAB_DLV_D1_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    KNT_SC_GFAB_DLV_D1Model ob = new KNT_SC_GFAB_DLV_D1Model();
                    ob.KNT_SC_GFAB_DLV_D1_ID = (dr["KNT_SC_GFAB_DLV_D1_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_SC_GFAB_DLV_D1_ID"]);
                    ob.KNT_SC_GFAB_DLV_H_ID = (dr["KNT_SC_GFAB_DLV_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_SC_GFAB_DLV_H_ID"]);
                    ob.KNT_STYL_FAB_ITEM_ID = (dr["KNT_STYL_FAB_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_STYL_FAB_ITEM_ID"]);
                    ob.DELV_ROLL_QTY = (dr["DELV_ROLL_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DELV_ROLL_QTY"]);
                    ob.DELV_GFAB_WT = (dr["DELV_GFAB_WT"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["DELV_GFAB_WT"]);
                    ob.WT_MOU_ID = (dr["WT_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["WT_MOU_ID"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<KNT_SC_GFAB_DLV_D1Model> GetScFabDelvDtl(long pKNT_SC_GFAB_DLV_H_ID)
        {
            string sp = "pkg_knit_fab_rcv_delv.knt_sc_gfab_dlv_h_select";
            try
            {
                var obList = new List<KNT_SC_GFAB_DLV_D1Model>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_SC_GFAB_DLV_H_ID", Value =pKNT_SC_GFAB_DLV_H_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3008},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    KNT_SC_GFAB_DLV_D1Model ob = new KNT_SC_GFAB_DLV_D1Model();
                    //ob.KNT_SC_GFAB_DLV_D1_ID = (dr["KNT_SC_GFAB_DLV_D1_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_SC_GFAB_DLV_D1_ID"]);
                    //ob.KNT_SC_GFAB_DLV_H_ID = (dr["KNT_SC_GFAB_DLV_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_SC_GFAB_DLV_H_ID"]);
                    ob.KNT_STYL_FAB_ITEM_ID = (dr["KNT_STYL_FAB_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_STYL_FAB_ITEM_ID"]);
                    ob.DELV_ROLL_QTY = (dr["DELV_ROLL_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DELV_ROLL_QTY"]);
                    ob.DELV_GFAB_WT = (dr["DELV_GFAB_WT"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["DELV_GFAB_WT"]);
                    ob.WT_MOU_ID = (dr["WT_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["WT_MOU_ID"]);

                    ob.PROD_RATE = (dr["PROD_RATE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PROD_RATE"]);

                    ob.BUYER_SNAME = (dr["BUYER_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BUYER_SNAME"]);
                    ob.STYLE_NO = (dr["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO"]);
                    ob.ORDER_NO_LST = (dr["ORDER_NO_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_NO_LST"]);
                    ob.COLOR_NAME_EN = (dr["COLOR_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_NAME_EN"]);
                    ob.FAB_TYPE_SNAME = (dr["FAB_TYPE_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FAB_TYPE_SNAME"]);
                    ob.ACT_FIN_GSM = (dr["ACT_FIN_GSM"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ACT_FIN_GSM"]);
                    ob.ACT_FIN_DIA = (dr["ACT_FIN_DIA"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ACT_FIN_DIA"]);
                    ob.FIB_COMP_CODE = (dr["FIB_COMP_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FIB_COMP_CODE"]);
                    ob.FABRIC_DESC = (dr["FABRIC_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FABRIC_DESC"]);


                    ob.IS_PAY_BY_BILL = (dr["IS_PAY_BY_BILL"] == DBNull.Value) ? "Y" : Convert.ToString(dr["IS_PAY_BY_BILL"]);

                    if (ob.IS_PAY_BY_BILL=="N")
                        ob.MIN_PROD_RATE = 0;
                    else
                        ob.MIN_PROD_RATE = 1;

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }








        
    }
}