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
    public class KNT_SCO_CHL_RCV_DModel
    {
        public Int64 KNT_SCO_CHL_RCV_D_ID { get; set; }
        public Int64 KNT_SCO_CHL_RCV_H_ID { get; set; }
        public Int64 KNT_STYL_FAB_ITEM_ID { get; set; }
        public Int64 RCV_NO_ROLL { get; set; }
        public Decimal RCV_ROLL_WT { get; set; }
        public Int64 WT_MOU_ID { get; set; }
        public Int64 KNT_QC_STS_TYPE_ID { get; set; }
        public string NOTE { get; set; }
        public string IS_STORED { get; set; }
        public string QC_STS_TYP_NAME { get; set; }

        public decimal PROD_RATE { get; set; }

        public string BYR_ACC_NAME_EN { get; set; }
        public string STYLE_NO { get; set; }
        public string ORDER_NO_LST { get; set; }
        public string COLOR_NAME_EN { get; set; }
        public string FAB_TYPE_SNAME { get; set; }
        public string FIB_COMP_NAME { get; set; }
        public string ACT_FIN_DIA { get; set; }
        public string FIN_GSM { get; set; }
        public string MC_DIA { get; set; }
        public string YARN_DETAILS { get; set; }
        public string FAB_ITEM_DESC { get; set; }


        public List<KNT_SCO_CHL_RCV_DModel> SelectAll()
        {
            string sp = "Select_KNT_SCO_CHL_RCV_D";
            try
            {
                var obList = new List<KNT_SCO_CHL_RCV_DModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_SCO_CHL_RCV_D_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    KNT_SCO_CHL_RCV_DModel ob = new KNT_SCO_CHL_RCV_DModel();
                    ob.KNT_SCO_CHL_RCV_D_ID = (dr["KNT_SCO_CHL_RCV_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_SCO_CHL_RCV_D_ID"]);
                    ob.KNT_SCO_CHL_RCV_H_ID = (dr["KNT_SCO_CHL_RCV_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_SCO_CHL_RCV_H_ID"]);
                    ob.KNT_STYL_FAB_ITEM_ID = (dr["KNT_STYL_FAB_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_STYL_FAB_ITEM_ID"]);
                    ob.RCV_NO_ROLL = (dr["RCV_NO_ROLL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RCV_NO_ROLL"]);
                    ob.RCV_ROLL_WT = (dr["RCV_ROLL_WT"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RCV_ROLL_WT"]);
                    ob.WT_MOU_ID = (dr["WT_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["WT_MOU_ID"]);
                    ob.KNT_QC_STS_TYPE_ID = (dr["KNT_QC_STS_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_QC_STS_TYPE_ID"]);
                    ob.NOTE = (dr["NOTE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["NOTE"]);
                    ob.IS_STORED = (dr["IS_STORED"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_STORED"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public KNT_SCO_CHL_RCV_DModel Select(long ID)
        {
            string sp = "Select_KNT_SCO_CHL_RCV_D";
            try
            {
                var ob = new KNT_SCO_CHL_RCV_DModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_SCO_CHL_RCV_D_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.KNT_SCO_CHL_RCV_D_ID = (dr["KNT_SCO_CHL_RCV_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_SCO_CHL_RCV_D_ID"]);
                    ob.KNT_SCO_CHL_RCV_H_ID = (dr["KNT_SCO_CHL_RCV_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_SCO_CHL_RCV_H_ID"]);
                    ob.KNT_STYL_FAB_ITEM_ID = (dr["KNT_STYL_FAB_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_STYL_FAB_ITEM_ID"]);
                    ob.RCV_NO_ROLL = (dr["RCV_NO_ROLL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RCV_NO_ROLL"]);
                    ob.RCV_ROLL_WT = (dr["RCV_ROLL_WT"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RCV_ROLL_WT"]);
                    ob.WT_MOU_ID = (dr["WT_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["WT_MOU_ID"]);
                    ob.KNT_QC_STS_TYPE_ID = (dr["KNT_QC_STS_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_QC_STS_TYPE_ID"]);
                    ob.NOTE = (dr["NOTE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["NOTE"]);
                    ob.IS_STORED = (dr["IS_STORED"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_STORED"]);


                }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<KNT_SCO_CHL_RCV_DModel> GetScoFabRcvList(long? pKNT_SCO_CHL_RCV_H_ID)
        {
            string sp = "pkg_knit_subcontract.knt_sco_chl_rcv_d_select";
            try
            {
                var obList = new List<KNT_SCO_CHL_RCV_DModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_SCO_CHL_RCV_H_ID", Value = pKNT_SCO_CHL_RCV_H_ID},
                     new CommandParameter() {ParameterName = "pOption", Value = 3002},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    KNT_SCO_CHL_RCV_DModel ob = new KNT_SCO_CHL_RCV_DModel();
                    //ob.KNT_SCO_CHL_RCV_D_ID = (dr["KNT_SCO_CHL_RCV_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_SCO_CHL_RCV_D_ID"]);
                    ob.KNT_SCO_CHL_RCV_H_ID = (dr["KNT_SCO_CHL_RCV_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_SCO_CHL_RCV_H_ID"]);
                    //ob.KNT_STYL_FAB_ITEM_ID = (dr["KNT_STYL_FAB_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_STYL_FAB_ITEM_ID"]);
                    ob.RCV_NO_ROLL = (dr["RCV_NO_ROLL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RCV_NO_ROLL"]);
                    ob.RCV_ROLL_WT = (dr["RCV_ROLL_WT"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RCV_ROLL_WT"]);
                    //ob.WT_MOU_ID = (dr["WT_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["WT_MOU_ID"]);
                    //ob.KNT_QC_STS_TYPE_ID = (dr["KNT_QC_STS_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_QC_STS_TYPE_ID"]);
                    //ob.NOTE = (dr["NOTE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["NOTE"]);
                    //ob.IS_STORED = (dr["IS_STORED"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_STORED"]);

                    ob.PROD_RATE = (dr["PROD_RATE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PROD_RATE"]);

                    ob.QC_STS_TYP_NAME = (dr["QC_STS_TYP_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["QC_STS_TYP_NAME"]);
                    ob.BYR_ACC_NAME_EN = (dr["BYR_ACC_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BYR_ACC_NAME_EN"]);
                    ob.STYLE_NO = (dr["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO"]);
                    ob.ORDER_NO_LST = (dr["ORDER_NO_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_NO_LST"]);
                    //ob.COLOR_NAME_EN = (dr["COLOR_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_NAME_EN"]);
                    ob.FAB_ITEM_DESC = (dr["FAB_ITEM_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FAB_ITEM_DESC"]);
                    ob.FIB_COMP_NAME = (dr["FIB_COMP_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FIB_COMP_NAME"]);
                    ob.ACT_FIN_DIA = (dr["ACT_FIN_DIA"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ACT_FIN_DIA"]);
                    ob.FIN_GSM = (dr["FIN_GSM"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FIN_GSM"]);
                    ob.MC_DIA = (dr["MC_DIA"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MC_DIA"]);
                    ob.YARN_DETAILS = (dr["YARN_DETAILS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["YARN_DETAILS"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<KNT_SCO_CHL_RCV_DModel> GetScoFabRcvDtl(long? pKNT_SCO_CHL_RCV_H_ID)
        {
            string sp = "pkg_knit_subcontract.knt_sco_chl_rcv_d_select";
            try
            {
                var obList = new List<KNT_SCO_CHL_RCV_DModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_SCO_CHL_RCV_H_ID", Value = pKNT_SCO_CHL_RCV_H_ID},
                     new CommandParameter() {ParameterName = "pOption", Value = 3003},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    KNT_SCO_CHL_RCV_DModel ob = new KNT_SCO_CHL_RCV_DModel();
                    ob.KNT_SCO_CHL_RCV_D_ID = (dr["KNT_SCO_CHL_RCV_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_SCO_CHL_RCV_D_ID"]);
                    ob.KNT_SCO_CHL_RCV_H_ID = (dr["KNT_SCO_CHL_RCV_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_SCO_CHL_RCV_H_ID"]);
                    ob.KNT_STYL_FAB_ITEM_ID = (dr["KNT_STYL_FAB_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_STYL_FAB_ITEM_ID"]);
                    ob.RCV_NO_ROLL = (dr["RCV_NO_ROLL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RCV_NO_ROLL"]);
                    ob.RCV_ROLL_WT = (dr["RCV_ROLL_WT"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RCV_ROLL_WT"]);
                    ob.WT_MOU_ID = (dr["WT_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["WT_MOU_ID"]);
                    ob.KNT_QC_STS_TYPE_ID = (dr["KNT_QC_STS_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_QC_STS_TYPE_ID"]);
                    ob.NOTE = (dr["NOTE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["NOTE"]);
                    ob.IS_STORED = (dr["IS_STORED"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_STORED"]);

                    ob.PROD_RATE = (dr["PROD_RATE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PROD_RATE"]);

                    ob.QC_STS_TYP_NAME = (dr["QC_STS_TYP_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["QC_STS_TYP_NAME"]);
                    ob.BYR_ACC_NAME_EN = (dr["BYR_ACC_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BYR_ACC_NAME_EN"]);
                    ob.STYLE_NO = (dr["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO"]);
                    ob.ORDER_NO_LST = (dr["ORDER_NO_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_NO_LST"]);
                    ob.COLOR_NAME_EN = (dr["COLOR_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_NAME_EN"]);
                    ob.FAB_TYPE_SNAME = (dr["FAB_TYPE_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FAB_TYPE_SNAME"]);
                    ob.FIB_COMP_NAME = (dr["FIB_COMP_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FIB_COMP_NAME"]);
                    ob.ACT_FIN_DIA = (dr["ACT_FIN_DIA"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ACT_FIN_DIA"]);
                    ob.FIN_GSM = (dr["FIN_GSM"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FIN_GSM"]);
                    ob.MC_DIA = (dr["MC_DIA"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MC_DIA"]);
                    ob.YARN_DETAILS = (dr["YARN_DETAILS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["YARN_DETAILS"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<KNT_SCO_CHL_RCV_DModel> GetChalnDtlt4Bill(long? pKNT_SCO_CHL_RCV_H_ID)
        {
            string sp = "pkg_knit_subcontract.knt_sco_chl_rcv_d_select";
            try
            {
                var obList = new List<KNT_SCO_CHL_RCV_DModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_SCO_CHL_RCV_H_ID", Value = pKNT_SCO_CHL_RCV_H_ID},
                     new CommandParameter() {ParameterName = "pOption", Value = 3004},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    KNT_SCO_CHL_RCV_DModel ob = new KNT_SCO_CHL_RCV_DModel();
                    //ob.KNT_SCO_CHL_RCV_D_ID = (dr["KNT_SCO_CHL_RCV_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_SCO_CHL_RCV_D_ID"]);
                    ob.KNT_SCO_CHL_RCV_H_ID = (dr["KNT_SCO_CHL_RCV_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_SCO_CHL_RCV_H_ID"]);
                    //ob.KNT_STYL_FAB_ITEM_ID = (dr["KNT_STYL_FAB_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_STYL_FAB_ITEM_ID"]);
                    ob.RCV_NO_ROLL = (dr["RCV_NO_ROLL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RCV_NO_ROLL"]);
                    ob.RCV_ROLL_WT = (dr["RCV_ROLL_WT"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RCV_ROLL_WT"]);
                    //ob.WT_MOU_ID = (dr["WT_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["WT_MOU_ID"]);
                    //ob.KNT_QC_STS_TYPE_ID = (dr["KNT_QC_STS_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_QC_STS_TYPE_ID"]);
                    //ob.NOTE = (dr["NOTE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["NOTE"]);
                    //ob.IS_STORED = (dr["IS_STORED"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_STORED"]);

                    ob.PROD_RATE = (dr["PROD_RATE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PROD_RATE"]);

                    ob.QC_STS_TYP_NAME = (dr["QC_STS_TYP_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["QC_STS_TYP_NAME"]);
                    ob.BYR_ACC_NAME_EN = (dr["BYR_ACC_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BYR_ACC_NAME_EN"]);
                    ob.STYLE_NO = (dr["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO"]);
                    ob.ORDER_NO_LST = (dr["ORDER_NO_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_NO_LST"]);
                    //ob.COLOR_NAME_EN = (dr["COLOR_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_NAME_EN"]);
                    ob.FAB_ITEM_DESC = (dr["FAB_ITEM_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FAB_ITEM_DESC"]);
                    ob.FIB_COMP_NAME = (dr["FIB_COMP_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FIB_COMP_NAME"]);
                    ob.ACT_FIN_DIA = (dr["ACT_FIN_DIA"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ACT_FIN_DIA"]);
                    ob.FIN_GSM = (dr["FIN_GSM"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FIN_GSM"]);
                    ob.MC_DIA = (dr["MC_DIA"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MC_DIA"]);
                    ob.YARN_DETAILS = (dr["YARN_DETAILS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["YARN_DETAILS"]);

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