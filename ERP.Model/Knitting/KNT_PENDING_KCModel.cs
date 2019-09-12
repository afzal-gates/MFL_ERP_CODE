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
    public class KNT_PENDING_KCModel
    {
        public Int64 MC_FAB_PROD_ORD_D_ID { get; set; }
        public string COLOR_NAME_EN { get; set; }
        public string FABRIC_SNAME { get; set; }
        public string FIN_DIA_N_DIA_TYPE { get; set; }
        public string WORK_STYLE_NO { get; set; }
        public string STYLE_DESC { get; set; }
        public string BYR_ACC_NAME_EN { get; set; }
        public string ORDER_NO_LIST { get; set; }
        public Int64 KNT_PLAN_H_ID { get; set; }
        public string PLAN_NOTE { get; set; }
        public string MOU_CODE { get; set; }
        public string FIN_DIA { get; set; }
        public string FIN_GSM { get; set; }
        public string MC_DIA_GG { get; set; }
        public Int64 KNT_JOB_CRD_H_ID { get; set; }
        public DateTime START_DT { get; set; }
        public DateTime END_DT { get; set; }
        public Decimal TG_D_PROD_QTY { get; set; }
        public string REMARKS { get; set; }
        public Decimal ASIGN_QTY { get; set; }
        public Decimal UN_ASIGN_QTY { get; set; }
        public string JOB_CRD_NO { get; set; }
        public string FAB_TYPE_NAME { get; set; }      
        public long? KNT_MACHINE_ID { get; set; }
        public long ACT_MC_DIA_ID { get; set; }
        public long HR_PROD_FLR_ID { get; set; }
        public DateTime? SHIP_DT { get; set; }


        public DateTime? CREATION_DATE { get; set; }
        public string KNT_MACHINE_NO { get; set; }
        public string COMP_NAME_EN { get; set; }
        public string JC_STS_TYP_NAME { get; set; }

        public List<KNT_PENDING_KCModel> QueryData(Int64 pOption, Int64? pHR_PROD_FLR_ID, Int64? pACT_MC_DIA_ID, String pWORK_STYLE_NO, Int64? pLK_COL_TYPE_ID, string pIS_RIB)
        {
            string sp = "PKG_MC_LOAD_PLAN.KNT_PENDING_KC_select";
            try
            {
                var obList = new List<KNT_PENDING_KCModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pHR_PROD_FLR_ID", Value = pHR_PROD_FLR_ID},
                     new CommandParameter() {ParameterName = "pACT_MC_DIA_ID", Value = pACT_MC_DIA_ID},
                     new CommandParameter() {ParameterName = "pWORK_STYLE_NO", Value = pWORK_STYLE_NO},
                     new CommandParameter() {ParameterName = "pLK_COL_TYPE_ID", Value = pLK_COL_TYPE_ID},
                     new CommandParameter() {ParameterName = "pIS_RIB", Value = pIS_RIB},
                     new CommandParameter() {ParameterName = "pSC_USER_ID", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pOption", Value = pOption},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    KNT_PENDING_KCModel ob = new KNT_PENDING_KCModel();
                    ob.MC_FAB_PROD_ORD_D_ID = (dr["MC_FAB_PROD_ORD_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_FAB_PROD_ORD_D_ID"]);
                    ob.COLOR_NAME_EN = (dr["COLOR_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_NAME_EN"]);
                    ob.FABRIC_SNAME = (dr["FABRIC_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FABRIC_SNAME"]);
                    ob.FIN_DIA_N_DIA_TYPE = (dr["FIN_DIA_N_DIA_TYPE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FIN_DIA_N_DIA_TYPE"]);
                    ob.WORK_STYLE_NO = (dr["WORK_STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["WORK_STYLE_NO"]);
                    ob.STYLE_DESC = (dr["STYLE_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_DESC"]);
                    ob.BYR_ACC_NAME_EN = (dr["BYR_ACC_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BYR_ACC_NAME_EN"]);
                    ob.ORDER_NO_LIST = (dr["ORDER_NO_LIST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_NO_LIST"]);
                    ob.KNT_PLAN_H_ID = (dr["KNT_PLAN_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_PLAN_H_ID"]);
                    ob.PLAN_NOTE = (dr["PLAN_NOTE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PLAN_NOTE"]);
                    ob.MOU_CODE = (dr["MOU_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MOU_CODE"]);
                    ob.FIN_DIA = (dr["FIN_DIA"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FIN_DIA"]);
                    ob.FIN_GSM = (dr["FIN_GSM"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FIN_GSM"]);
                    ob.MC_DIA_GG = (dr["MC_DIA_GG"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MC_DIA_GG"]);
                    ob.KNT_JOB_CRD_H_ID = (dr["KNT_JOB_CRD_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_JOB_CRD_H_ID"]);

                    ob.HR_PROD_FLR_ID = (dr["HR_PROD_FLR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_PROD_FLR_ID"]);

                    ob.START_DT = (dr["START_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["START_DT"]);
                    ob.END_DT = (dr["END_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["END_DT"]);

                    ob.TG_D_PROD_QTY = (dr["TG_D_PROD_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["TG_D_PROD_QTY"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                    ob.ASIGN_QTY = (dr["ASIGN_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["ASIGN_QTY"]);
                    ob.UN_ASIGN_QTY = (dr["UN_ASIGN_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["UN_ASIGN_QTY"]);
                    ob.JOB_CRD_NO = (dr["JOB_CRD_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["JOB_CRD_NO"]);
                    ob.FAB_TYPE_NAME = (dr["FAB_TYPE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FAB_TYPE_NAME"]);

                    if (dr["KNT_MACHINE_ID"] != DBNull.Value)
                    {
                        ob.KNT_MACHINE_ID = Convert.ToInt64(dr["KNT_MACHINE_ID"]);
                    }

                    ob.ACT_MC_DIA_ID = (dr["ACT_MC_DIA_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ACT_MC_DIA_ID"]);

                    if (dr["SHIP_DT"] != DBNull.Value)
                    {
                        ob.SHIP_DT = Convert.ToDateTime(dr["SHIP_DT"]);
                    } 

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public KNT_PENDING_KCModel selectData(Int64 pKNT_JOB_CRD_H_ID)
        {
            string sp = "PKG_MC_LOAD_PLAN.KNT_PENDING_KC_select";
            try
            {
              
                OraDatabase db = new OraDatabase();
                KNT_PENDING_KCModel ob = new KNT_PENDING_KCModel();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_JOB_CRD_H_ID", Value = pKNT_JOB_CRD_H_ID},
                     new CommandParameter() {ParameterName = "pOption", Value = 3002},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    
                    ob.MC_FAB_PROD_ORD_D_ID = (dr["MC_FAB_PROD_ORD_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_FAB_PROD_ORD_D_ID"]);
                    ob.COLOR_NAME_EN = (dr["COLOR_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_NAME_EN"]);
                    ob.FABRIC_SNAME = (dr["FABRIC_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FABRIC_SNAME"]);
                    ob.FIN_DIA_N_DIA_TYPE = (dr["FIN_DIA_N_DIA_TYPE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FIN_DIA_N_DIA_TYPE"]);
                    ob.WORK_STYLE_NO = (dr["WORK_STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["WORK_STYLE_NO"]);
                    ob.STYLE_DESC = (dr["STYLE_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_DESC"]);
                    ob.BYR_ACC_NAME_EN = (dr["BYR_ACC_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BYR_ACC_NAME_EN"]);
                    ob.ORDER_NO_LIST = (dr["ORDER_NO_LIST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_NO_LIST"]);
                    ob.KNT_PLAN_H_ID = (dr["KNT_PLAN_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_PLAN_H_ID"]);
                    ob.PLAN_NOTE = (dr["PLAN_NOTE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PLAN_NOTE"]);
                    ob.MOU_CODE = (dr["MOU_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MOU_CODE"]);
                    ob.FIN_DIA = (dr["FIN_DIA"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FIN_DIA"]);
                    ob.FIN_GSM = (dr["FIN_GSM"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FIN_GSM"]);
                    ob.MC_DIA_GG = (dr["MC_DIA_GG"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MC_DIA_GG"]);
                    ob.KNT_JOB_CRD_H_ID = (dr["KNT_JOB_CRD_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_JOB_CRD_H_ID"]);

                    ob.START_DT = (dr["START_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["START_DT"]);
                    ob.END_DT = (dr["END_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["END_DT"]);

                    ob.TG_D_PROD_QTY = (dr["TG_D_PROD_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["TG_D_PROD_QTY"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                    ob.ASIGN_QTY = (dr["ASIGN_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["ASIGN_QTY"]);
                    ob.UN_ASIGN_QTY = (dr["UN_ASIGN_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["UN_ASIGN_QTY"]);
                    ob.JOB_CRD_NO = (dr["JOB_CRD_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["JOB_CRD_NO"]);
                    ob.FAB_TYPE_NAME = (dr["FAB_TYPE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FAB_TYPE_NAME"]);

                    if (dr["KNT_MACHINE_ID"] != DBNull.Value)
                    {
                        ob.KNT_MACHINE_ID = Convert.ToInt64(dr["KNT_MACHINE_ID"]);
                    }

                    ob.ACT_MC_DIA_ID = (dr["ACT_MC_DIA_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ACT_MC_DIA_ID"]);
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