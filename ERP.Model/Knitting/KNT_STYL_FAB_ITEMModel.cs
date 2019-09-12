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
    public class KNT_STYL_FAB_ITEMModel
    {
        public Int64 KNT_STYL_FAB_ITEM_ID { get; set; }
        public Int64 MC_FAB_PROD_ORD_H_ID { get; set; }
        public Int64 FAB_COLOR_ID { get; set; }
        public Int64 LK_COL_TYPE_ID { get; set; }
        public Int64 RF_FAB_TYPE_ID { get; set; }
        public Int64 RF_FIB_COMP_ID { get; set; }
        public string FIN_GSM { get; set; }
        public string FIN_DIA { get; set; }
        public Int64 DIA_MOU_ID { get; set; }
        public Int64 LK_DIA_TYPE_ID { get; set; }
        public Int64 KNT_MC_DIA_ID { get; set; }
        public Int64 LK_MC_GG_ID { get; set; }
        public Int64 CONS_MOU_ID { get; set; }
        public string KNT_YRN_REF { get; set; }
        public string KNT_YRN_LOT_LST { get; set; }

        public string FAB_PROD_CAT_SNAME { get; set; }
        public DateTime? PROD_DT { get; set; }
        public string MC_DIA { get; set; }
        public string KNT_MACHINE_NO { get; set; }
        public string MC_RPM { get; set; }
        public string MC_GG { get; set; }
        public string ORDER_NO_LST { get; set; }
        public string WORK_STYLE_NO { get; set; }
        public string STYLE_NO { get; set; }
        public string FAB_TYPE_SNAME { get; set; }
        public string FABRIC_DESC { get; set; }
        public string BYR_ACC_NAME_EN { get; set; }
        public string COLOR_NAME_EN { get; set; }
        public decimal? A_SHIFT_QTY { get; set; }
        public decimal? B_SHIFT_QTY { get; set; }
        public decimal? C_SHIFT_QTY { get; set; }
        public decimal? TOT_QTY { get; set; }
        public decimal? TARGET_QTY { get; set; }
        public decimal? CUMULA_QTY { get; set; }
        public decimal? BAL_QTY { get; set; }



        public object GetDailyProdList(int pageNumber, int pageSize, Int32? pRF_FAB_PROD_CAT_ID, Int32? pMC_BYR_ACC_ID,
            Int32? pMC_COLOR_ID, Int64? pMC_FAB_PROD_ORD_H_ID, string pORDER_NO_LST, DateTime? pFROM_DT, DateTime? pTO_DT)
        {

            string sp = "PKG_KNIT_ROLL.knt_roll_production_select";
            try
            {
                Int64 vTotalRec = 0;
                var obList = new List<KNT_STYL_FAB_ITEMModel>();
                var obj = new RF_PAGERModel();

                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pRF_FAB_PROD_CAT_ID", Value = pRF_FAB_PROD_CAT_ID},
                     new CommandParameter() {ParameterName = "pMC_BYR_ACC_ID", Value = pMC_BYR_ACC_ID},
                     new CommandParameter() {ParameterName = "pMC_COLOR_ID", Value = pMC_COLOR_ID},
                     new CommandParameter() {ParameterName = "pMC_FAB_PROD_ORD_H_ID", Value = pMC_FAB_PROD_ORD_H_ID},
                     new CommandParameter() {ParameterName = "pORDER_NO_LST", Value = pORDER_NO_LST},
                     new CommandParameter() {ParameterName = "pFROM_DT", Value = pFROM_DT},
                     new CommandParameter() {ParameterName = "pTO_DT", Value = pTO_DT},
                     new CommandParameter() {ParameterName = "pageNumber", Value = pageNumber},
                     new CommandParameter() {ParameterName = "pageSize", Value = pageSize},

                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    KNT_STYL_FAB_ITEMModel ob = new KNT_STYL_FAB_ITEMModel();
                    //ob.KNT_STYL_FAB_ITEM_ID = (dr["KNT_STYL_FAB_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_STYL_FAB_ITEM_ID"]);
                    //ob.MC_FAB_PROD_ORD_H_ID = (dr["MC_FAB_PROD_ORD_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_FAB_PROD_ORD_H_ID"]);
                    //ob.FAB_COLOR_ID = (dr["FAB_COLOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["FAB_COLOR_ID"]);
                    //ob.LK_COL_TYPE_ID = (dr["LK_COL_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_COL_TYPE_ID"]);
                    //ob.RF_FAB_TYPE_ID = (dr["RF_FAB_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_FAB_TYPE_ID"]);
                    //ob.RF_FIB_COMP_ID = (dr["RF_FIB_COMP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_FIB_COMP_ID"]);
                    ob.FIN_GSM = (dr["FIN_GSM"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FIN_GSM"]);
                    ob.FAB_PROD_CAT_SNAME = (dr["FAB_PROD_CAT_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FAB_PROD_CAT_SNAME"]);
                    ob.BYR_ACC_NAME_EN = (dr["BYR_ACC_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BYR_ACC_NAME_EN"]);
                    ob.FAB_TYPE_SNAME = (dr["FAB_TYPE_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FAB_TYPE_SNAME"]);
                    ob.FABRIC_DESC = (dr["FABRIC_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FABRIC_DESC"]);
                    ob.COLOR_NAME_EN = (dr["COLOR_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_NAME_EN"]);

                    //ob.PROD_DT = (dr["PROD_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["PROD_DT"]);
                    ob.FIN_DIA = (dr["FIN_DIA"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FIN_DIA"]);
                    ob.MC_DIA = (dr["MC_DIA"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MC_DIA"]);
                    ob.KNT_MACHINE_NO = (dr["KNT_MACHINE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["KNT_MACHINE_NO"]);
                    ob.MC_RPM = (dr["MC_RPM"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MC_RPM"]);
                    ob.MC_GG = (dr["MC_GG"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MC_GG"]);
                    ob.ORDER_NO_LST = (dr["ORDER_NO_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_NO_LST"]);
                    ob.WORK_STYLE_NO = (dr["WORK_STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["WORK_STYLE_NO"]);
                    ob.STYLE_NO = (dr["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO"]);


                    //ob.DIA_MOU_ID = (dr["DIA_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DIA_MOU_ID"]);
                    //ob.LK_DIA_TYPE_ID = (dr["LK_DIA_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_DIA_TYPE_ID"]);
                    //ob.KNT_MC_DIA_ID = (dr["KNT_MC_DIA_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_MC_DIA_ID"]);
                    //ob.LK_MC_GG_ID = (dr["LK_MC_GG_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_MC_GG_ID"]);
                    //ob.CONS_MOU_ID = (dr["CONS_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CONS_MOU_ID"]);
                    //ob.KNT_YRN_REF = (dr["KNT_YRN_REF"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["KNT_YRN_REF"]);
                    ob.KNT_YRN_LOT_LST = (dr["KNT_YRN_LOT_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["KNT_YRN_LOT_LST"]);

                    ob.A_SHIFT_QTY = (dr["A_SHIFT_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["A_SHIFT_QTY"]);
                    ob.B_SHIFT_QTY = (dr["B_SHIFT_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["B_SHIFT_QTY"]);
                    ob.C_SHIFT_QTY = (dr["C_SHIFT_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["C_SHIFT_QTY"]);
                    ob.TOT_QTY = (dr["TOT_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["TOT_QTY"]);
                    ob.CUMULA_QTY = (dr["CUMULA_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["CUMULA_QTY"]);
                    ob.TARGET_QTY = (dr["TARGET_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["TARGET_QTY"]);
                    ob.BAL_QTY = (dr["BAL_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["BAL_QTY"]);

                    if (vTotalRec < 1)
                    {
                        vTotalRec = (dr["TOTAL_REC"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["TOTAL_REC"]);
                    }

                    obList.Add(ob);

                }

                obj.total = vTotalRec;
                obj.data = obList;
                return obj;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }




        public List<KNT_STYL_FAB_ITEMModel> GetProdWithLotList(Int32? pMC_COLOR_ID, Int32? pMC_STYLE_H_ID = null, Int32? pMC_STYLE_D_FAB_ID = null, Int32? pRF_FAB_TYPE_ID = null, Int32? pRF_FIB_COMP_ID = null)
        {
            string sp = "PKG_KNIT_ROLL.knt_roll_production_select";
            try
            {
                var obList = new List<KNT_STYL_FAB_ITEMModel>();
                var obj = new RF_PAGERModel();

                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     //new CommandParameter() {ParameterName = "pMC_BYR_ACC_ID", Value = pMC_BYR_ACC_ID},
                     new CommandParameter() {ParameterName = "pMC_COLOR_ID", Value = pMC_COLOR_ID},
                     new CommandParameter() {ParameterName = "pRF_FAB_TYPE_ID", Value = pRF_FAB_TYPE_ID},
                     new CommandParameter() {ParameterName = "pRF_FIB_COMP_ID", Value = pRF_FIB_COMP_ID},
                     new CommandParameter() {ParameterName = "pMC_STYLE_D_FAB_ID", Value = pMC_STYLE_D_FAB_ID},
                     new CommandParameter() {ParameterName = "pMC_STYLE_H_ID", Value = pMC_STYLE_H_ID},
                     
                     new CommandParameter() {ParameterName = "pOption", Value =3001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    KNT_STYL_FAB_ITEMModel ob = new KNT_STYL_FAB_ITEMModel();
                    ob.KNT_STYL_FAB_ITEM_ID = (dr["KNT_STYL_FAB_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_STYL_FAB_ITEM_ID"]);
                    ob.MC_FAB_PROD_ORD_H_ID = (dr["MC_FAB_PROD_ORD_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_FAB_PROD_ORD_H_ID"]);
                    ob.FAB_COLOR_ID = (dr["FAB_COLOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["FAB_COLOR_ID"]);
                    ob.LK_COL_TYPE_ID = (dr["LK_COL_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_COL_TYPE_ID"]);
                    ob.RF_FAB_TYPE_ID = (dr["RF_FAB_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_FAB_TYPE_ID"]);
                    ob.RF_FIB_COMP_ID = (dr["RF_FIB_COMP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_FIB_COMP_ID"]);

                    ob.FIN_GSM = (dr["FIN_GSM"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FIN_GSM"]);
                    ob.FIN_DIA = (dr["FIN_DIA"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FIN_DIA"]);
                    //ob.MC_DIA = (dr["MC_DIA"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MC_DIA"]);
                   
                    ob.DIA_MOU_ID = (dr["DIA_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DIA_MOU_ID"]);
                    ob.LK_DIA_TYPE_ID = (dr["LK_DIA_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_DIA_TYPE_ID"]);
                    ob.KNT_MC_DIA_ID = (dr["KNT_MC_DIA_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_MC_DIA_ID"]);
                    ob.LK_MC_GG_ID = (dr["LK_MC_GG_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_MC_GG_ID"]);
                    ob.CONS_MOU_ID = (dr["CONS_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CONS_MOU_ID"]);
                    ob.KNT_YRN_REF = (dr["KNT_YRN_REF"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["KNT_YRN_REF"]);
                    ob.KNT_YRN_LOT_LST = (dr["KNT_YRN_LOT_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["KNT_YRN_LOT_LST"]);
                    ob.YRN_LOT_NO = (dr["YRN_LOT_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["YRN_LOT_NO"]);

                    obList.Add(ob);
                }

                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public string YRN_LOT_NO { get; set; }
    }
}