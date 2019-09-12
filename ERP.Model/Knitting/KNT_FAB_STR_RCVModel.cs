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
    public class KNT_FAB_STR_RCVModel
    {
        public Int64 KNT_FAB_STR_RCV_ID { get; set; }
        public Int32 SCM_STORE_ID { get; set; }
        public DateTime RCV_DT { get; set; }
        public string IS_G_F { get; set; }
        public Int64 KNT_STYL_FAB_ITEM_ID { get; set; }
        public Int64 RCV_NO_ROLL { get; set; }
        public Decimal RCV_ROLL_WT { get; set; }
        public Int64 WT_MOU_ID { get; set; }
        public string KNT_FAB_ROLL_ID { get; set; }
        public string REMARKS { get; set; }
        public string IS_FINALIZE { get; set; }
        public string RCV_STATUS { get; set; }
        public string XML_DTL { get; set; }


        public string FAB_PROD_CAT_SNAME { get; set; }
        public string STYLE_NO { get; set; }
        public string ORDER_NO_LST { get; set; }
        public string BUYER_SNAME { get; set; }
        public string COLOR_NAME_EN { get; set; }
        public string FAB_TYPE_SNAME { get; set; }
        public string ACT_FIN_GSM { get; set; }
        public string ACT_FIN_DIA { get; set; }
        public string MC_DIA { get; set; }
        public string MC_GG { get; set; }

        public string FAB_ROLL_NO { get; set; }
        public string EMP_FULL_NAME_EN { get; set; }
        public string EMPLOYEE_CODE { get; set; }
        public string JOB_CRD_NO { get; set; }
        public string KNT_MACHINE_NO { get; set; }
        public string FIN_DIA_TYPE { get; set; }
        public string STORE_NAME_EN { get; set; }
        public string FIB_COMP_CODE { get; set; }
        public string KNT_YRN_LOT_DESC { get; set; }
        public string SCHEDULE_NAME_EN { get; set; }
        public string GRADE_NO { get; set; }
        public decimal TOT_RCV_ROLL_WT { get; set; }




        public string BatchSave()
        {
            const string sp = "pkg_knit_fab_rcv_delv.knt_fab_str_rcv_batch_save";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {                    
                    new CommandParameter() {ParameterName = "pSCM_STORE_ID", Value = ob.SCM_STORE_ID},
                    new CommandParameter() {ParameterName = "pRCV_DT", Value = ob.RCV_DT},
                    new CommandParameter() {ParameterName = "pIS_G_F", Value = ob.IS_G_F}, 
                    new CommandParameter() {ParameterName = "pIS_FINALIZE", Value = ob.IS_FINALIZE},
                    new CommandParameter() {ParameterName = "pXML_DTL", Value = ob.XML_DTL},  
                    new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},                                       
                    new CommandParameter() {ParameterName = "pOption", Value = 1000},
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
            const string sp = "SP_KNT_FAB_STR_RCV";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_FAB_STR_RCV_ID", Value = ob.KNT_FAB_STR_RCV_ID},
                     new CommandParameter() {ParameterName = "pRCV_DT", Value = ob.RCV_DT},
                     new CommandParameter() {ParameterName = "pIS_G_F", Value = ob.IS_G_F},
                     new CommandParameter() {ParameterName = "pKNT_STYL_FAB_ITEM_ID", Value = ob.KNT_STYL_FAB_ITEM_ID},
                     new CommandParameter() {ParameterName = "pRCV_NO_ROLL", Value = ob.RCV_NO_ROLL},
                     new CommandParameter() {ParameterName = "pRCV_ROLL_WT", Value = ob.RCV_ROLL_WT},
                     new CommandParameter() {ParameterName = "pWT_MOU_ID", Value = ob.WT_MOU_ID},
                     new CommandParameter() {ParameterName = "pREMARKS", Value = ob.REMARKS},
                                                    
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



        public List<KNT_FAB_STR_RCVModel> SelectAll()
        {
            string sp = "Select_KNT_FAB_STR_RCV";
            try
            {
                var obList = new List<KNT_FAB_STR_RCVModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_FAB_STR_RCV_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    KNT_FAB_STR_RCVModel ob = new KNT_FAB_STR_RCVModel();
                    ob.KNT_FAB_STR_RCV_ID = (dr["KNT_FAB_STR_RCV_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_FAB_STR_RCV_ID"]);
                    ob.RCV_DT = (dr["RCV_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["RCV_DT"]);
                    ob.IS_G_F = (dr["IS_G_F"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_G_F"]);
                    ob.KNT_STYL_FAB_ITEM_ID = (dr["KNT_STYL_FAB_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_STYL_FAB_ITEM_ID"]);
                    ob.RCV_NO_ROLL = (dr["RCV_NO_ROLL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RCV_NO_ROLL"]);
                    ob.RCV_ROLL_WT = (dr["RCV_ROLL_WT"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RCV_ROLL_WT"]);
                    ob.WT_MOU_ID = (dr["WT_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["WT_MOU_ID"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public KNT_FAB_STR_RCVModel Select(long ID)
        {
            string sp = "Select_KNT_FAB_STR_RCV";
            try
            {
                var ob = new KNT_FAB_STR_RCVModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_FAB_STR_RCV_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.KNT_FAB_STR_RCV_ID = (dr["KNT_FAB_STR_RCV_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_FAB_STR_RCV_ID"]);
                    ob.RCV_DT = (dr["RCV_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["RCV_DT"]);
                    ob.IS_G_F = (dr["IS_G_F"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_G_F"]);
                    ob.KNT_STYL_FAB_ITEM_ID = (dr["KNT_STYL_FAB_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_STYL_FAB_ITEM_ID"]);
                    ob.RCV_NO_ROLL = (dr["RCV_NO_ROLL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RCV_NO_ROLL"]);
                    ob.RCV_ROLL_WT = (dr["RCV_ROLL_WT"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RCV_ROLL_WT"]);
                    ob.WT_MOU_ID = (dr["WT_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["WT_MOU_ID"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);

                }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object GetFabRcvDataByDate(int pageNumber, int pageSize, Int32? pSCM_STORE_ID, DateTime? pRCV_DT)
        {
            string sp = "pkg_knit_fab_rcv_delv.knt_fab_str_rcv_select";
            try
            {
                Int64 vTotalRec = 0;
                var obList = new List<KNT_FAB_STR_RCVModel>();
                var obj = new RF_PAGERModel();

                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                    new CommandParameter() {ParameterName = "pSCM_STORE_ID", Value = pSCM_STORE_ID},
                     new CommandParameter() {ParameterName = "pRCV_DT", Value = pRCV_DT},
                     new CommandParameter() {ParameterName = "pageNumber", Value = pageNumber},
                     new CommandParameter() {ParameterName = "pageSize", Value = pageSize},
                     new CommandParameter() {ParameterName = "pOption", Value =3002},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    KNT_FAB_STR_RCVModel ob = new KNT_FAB_STR_RCVModel();
                    ob.KNT_FAB_STR_RCV_ID = (dr["KNT_FAB_STR_RCV_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_FAB_STR_RCV_ID"]);
                    ob.SCM_STORE_ID = (dr["SCM_STORE_ID"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["SCM_STORE_ID"]);
                    ob.RCV_DT = (dr["RCV_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["RCV_DT"]);
                    ob.IS_G_F = (dr["IS_G_F"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_G_F"]);
                    ob.KNT_STYL_FAB_ITEM_ID = (dr["KNT_STYL_FAB_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_STYL_FAB_ITEM_ID"]);

                    ob.FAB_PROD_CAT_SNAME = (dr["FAB_PROD_CAT_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FAB_PROD_CAT_SNAME"]);
                    ob.BUYER_SNAME = (dr["BUYER_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BUYER_SNAME"]);
                    ob.STYLE_NO = (dr["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO"]);
                    ob.ORDER_NO_LST = (dr["ORDER_NO_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_NO_LST"]);
                    ob.COLOR_NAME_EN = (dr["COLOR_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_NAME_EN"]);
                    ob.FAB_TYPE_SNAME = (dr["FAB_TYPE_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FAB_TYPE_SNAME"]);
                    ob.ACT_FIN_GSM = (dr["ACT_FIN_GSM"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ACT_FIN_GSM"]);
                    ob.ACT_FIN_DIA = (dr["ACT_FIN_DIA"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ACT_FIN_DIA"]);
                    ob.MC_DIA = (dr["MC_DIA"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MC_DIA"]);
                    ob.MC_GG = (dr["MC_GG"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MC_GG"]);

                    ob.RCV_NO_ROLL = (dr["RCV_NO_ROLL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RCV_NO_ROLL"]);
                    ob.RCV_ROLL_WT = (dr["RCV_ROLL_WT"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RCV_ROLL_WT"]);
                    ob.WT_MOU_ID = (dr["WT_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["WT_MOU_ID"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                    ob.IS_FINALIZE = (dr["IS_FINALIZE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_FINALIZE"]);
                    ob.RCV_STATUS = (dr["RCV_STATUS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["RCV_STATUS"]);
                    ob.KNT_FAB_ROLL_ID = (dr["KNT_FAB_ROLL_ID"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["KNT_FAB_ROLL_ID"]);
                    ob.FAB_ROLL_NO = (dr["FAB_ROLL_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FAB_ROLL_NO"]);
                    ob.EMP_FULL_NAME_EN = (dr["EMP_FULL_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EMP_FULL_NAME_EN"]);
                    ob.EMPLOYEE_CODE = (dr["EMPLOYEE_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EMPLOYEE_CODE"]);
                    ob.KNT_MACHINE_NO = (dr["KNT_MACHINE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["KNT_MACHINE_NO"]);
                    ob.JOB_CRD_NO = (dr["JOB_CRD_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["JOB_CRD_NO"]);
                    ob.FIN_DIA_TYPE = (dr["FIN_DIA_TYPE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FIN_DIA_TYPE"]);

                    ob.STORE_NAME_EN = (dr["STORE_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STORE_NAME_EN"]);
                    ob.KNT_YRN_LOT_DESC = (dr["KNT_YRN_LOT_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["KNT_YRN_LOT_DESC"]);
                    ob.FIB_COMP_CODE = (dr["FIB_COMP_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FIB_COMP_CODE"]);
                    ob.SCHEDULE_NAME_EN = (dr["SCHEDULE_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SCHEDULE_NAME_EN"]);
                    ob.GRADE_NO = (dr["GRADE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["GRADE_NO"]);

                    ob.TOT_RCV_ROLL_WT = (dr["TOT_RCV_ROLL_WT"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["TOT_RCV_ROLL_WT"]);

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


    }


    public class KNT_FAB_STR_RCV_VM
    {
        public Int64 KNT_STYL_FAB_ITEM_ID { get; set; }
        public Int64 RF_FAB_PROD_CAT_ID { get; set; }
        public long KNT_QC_STS_TYPE_ID { get; set; }
        public string FAB_PROD_CAT_SNAME { get; set; }
        public string STYLE_NO { get; set; }
        public string ORDER_NO_LST { get; set; }
        public string BUYER_SNAME { get; set; }
        public string COLOR_NAME_EN { get; set; }
        public string FAB_TYPE_SNAME { get; set; }
        public string ACT_FIN_GSM { get; set; }
        public string ACT_FIN_DIA { get; set; }
        public string MC_DIA { get; set; }
        public string MC_GG { get; set; }
        public Int64 KNT_FAB_ROLL_ID { get; set; }
        public string FAB_ROLL_NO { get; set; }
        public Int32 RCV_NO_ROLL { get; set; }
        public decimal RCV_ROLL_WT { get; set; }
        public Int32 WT_MOU_ID { get; set; }
        public string ITM_ROLL_REF { get; set; }
        public DateTime RCV_DT { get; set; }
        public string IS_G_F { get; set; }
        public string IS_DELV { get; set; }
        public Int32 KNT_SC_PRG_RCV_ID { get; set; }
        public Int32 SC_MAP_BYR_STR_ID { get; set; }
        public Int32 SC_MAP_STR_PRD_CAT_ID { get; set; }
        

        public KNT_FAB_STR_RCV_VM GetRollData(string pFAB_ROLL_NO, Int64? pSCM_SUPPLIER_ID = null, Int64? pSCM_STORE_ID = null)
        {
            string sp = "pkg_knit_fab_rcv_delv.knt_fab_str_rcv_select";
            try
            {
                var ob = new KNT_FAB_STR_RCV_VM();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                    new CommandParameter() {ParameterName = "pSCM_SUPPLIER_ID", Value = pSCM_SUPPLIER_ID},
                    new CommandParameter() {ParameterName = "pSCM_STORE_ID", Value = pSCM_STORE_ID},
                    new CommandParameter() {ParameterName = "pFAB_ROLL_NO", Value = pFAB_ROLL_NO},
                    new CommandParameter() {ParameterName = "pOption", Value = 3003},
                    new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.KNT_STYL_FAB_ITEM_ID = (dr["KNT_STYL_FAB_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_STYL_FAB_ITEM_ID"]);
                    ob.RF_FAB_PROD_CAT_ID = (dr["RF_FAB_PROD_CAT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_FAB_PROD_CAT_ID"]);
                    
                    ob.FAB_PROD_CAT_SNAME = (dr["FAB_PROD_CAT_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FAB_PROD_CAT_SNAME"]);
                    ob.BUYER_SNAME = (dr["BUYER_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BUYER_SNAME"]);
                    ob.STYLE_NO = (dr["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO"]);
                    ob.ORDER_NO_LST = (dr["ORDER_NO_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_NO_LST"]);
                    ob.COLOR_NAME_EN = (dr["COLOR_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_NAME_EN"]);
                    ob.FAB_TYPE_SNAME = (dr["FAB_TYPE_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FAB_TYPE_SNAME"]);
                    ob.ACT_FIN_GSM = (dr["ACT_FIN_GSM"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ACT_FIN_GSM"]);
                    ob.ACT_FIN_DIA = (dr["ACT_FIN_DIA"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ACT_FIN_DIA"]);
                    ob.MC_DIA = (dr["MC_DIA"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MC_DIA"]);
                    ob.MC_GG = (dr["MC_GG"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MC_GG"]);
                    ob.KNT_FAB_ROLL_ID = (dr["KNT_FAB_ROLL_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_FAB_ROLL_ID"]);
                    ob.FAB_ROLL_NO = (dr["FAB_ROLL_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FAB_ROLL_NO"]);
                    ob.RCV_NO_ROLL = (dr["RCV_NO_ROLL"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["RCV_NO_ROLL"]);
                    ob.RCV_ROLL_WT = (dr["FAB_ROLL_WT"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["FAB_ROLL_WT"]);
                    ob.WT_MOU_ID = (dr["WT_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["WT_MOU_ID"]);
                    ob.ITM_ROLL_REF = (dr["ITM_ROLL_REF"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITM_ROLL_REF"]);
                    ob.RCV_DT = (dr["RCV_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["RCV_DT"]);
                    ob.IS_G_F = (dr["IS_G_F"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_G_F"]);
                    ob.IS_DELV = (dr["IS_DELV"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_DELV"]);

                    ob.KNT_SC_PRG_RCV_ID = (dr["KNT_SC_PRG_RCV_ID"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["KNT_SC_PRG_RCV_ID"]);
                    ob.SC_MAP_BYR_STR_ID = (dr["SC_MAP_BYR_STR_ID"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["SC_MAP_BYR_STR_ID"]);
                    ob.SC_MAP_STR_PRD_CAT_ID = (dr["SC_MAP_STR_PRD_CAT_ID"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["SC_MAP_STR_PRD_CAT_ID"]);
                }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public KNT_FAB_STR_RCV_VM GetRollData4Delv(string pFAB_ROLL_NO, Int64? pSCM_SUPPLIER_ID = null, Int64? pSCM_STORE_ID = null)
        {
            string sp = "pkg_knit_fab_rcv_delv.knt_fab_str_rcv_select";
            try
            {
                var ob = new KNT_FAB_STR_RCV_VM();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                    new CommandParameter() {ParameterName = "pSCM_SUPPLIER_ID", Value = pSCM_SUPPLIER_ID},
                    new CommandParameter() {ParameterName = "pSCM_STORE_ID", Value = pSCM_STORE_ID},
                    new CommandParameter() {ParameterName = "pFAB_ROLL_NO", Value = pFAB_ROLL_NO},
                    new CommandParameter() {ParameterName = "pOption", Value = 3005},
                    new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.KNT_STYL_FAB_ITEM_ID = (dr["KNT_STYL_FAB_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_STYL_FAB_ITEM_ID"]);
                    ob.RF_FAB_PROD_CAT_ID = (dr["RF_FAB_PROD_CAT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_FAB_PROD_CAT_ID"]);

                    ob.KNT_QC_STS_TYPE_ID = (dr["KNT_QC_STS_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_QC_STS_TYPE_ID"]);                    

                    ob.FAB_PROD_CAT_SNAME = (dr["FAB_PROD_CAT_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FAB_PROD_CAT_SNAME"]);
                    ob.BUYER_SNAME = (dr["BUYER_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BUYER_SNAME"]);
                    ob.STYLE_NO = (dr["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO"]);
                    ob.ORDER_NO_LST = (dr["ORDER_NO_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_NO_LST"]);
                    ob.COLOR_NAME_EN = (dr["COLOR_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_NAME_EN"]);
                    ob.FAB_TYPE_SNAME = (dr["FAB_TYPE_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FAB_TYPE_SNAME"]);
                    ob.ACT_FIN_GSM = (dr["ACT_FIN_GSM"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ACT_FIN_GSM"]);
                    ob.ACT_FIN_DIA = (dr["ACT_FIN_DIA"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ACT_FIN_DIA"]);
                    ob.MC_DIA = (dr["MC_DIA"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MC_DIA"]);
                    ob.MC_GG = (dr["MC_GG"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MC_GG"]);
                    ob.KNT_FAB_ROLL_ID = (dr["KNT_FAB_ROLL_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_FAB_ROLL_ID"]);
                    ob.FAB_ROLL_NO = (dr["FAB_ROLL_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FAB_ROLL_NO"]);
                    ob.RCV_NO_ROLL = (dr["RCV_NO_ROLL"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["RCV_NO_ROLL"]);
                    ob.RCV_ROLL_WT = (dr["FAB_ROLL_WT"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["FAB_ROLL_WT"]);
                    ob.WT_MOU_ID = (dr["WT_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["WT_MOU_ID"]);
                    ob.RCV_DT = (dr["RCV_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["RCV_DT"]);
                    ob.IS_G_F = (dr["IS_G_F"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_G_F"]);
                    ob.IS_DELV = (dr["IS_DELV"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_DELV"]);
                }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        
    }


    public class KNT_FAB_STR_STK_VM
    {
        public DateTime? RCV_DT { get; set; }
        public string IS_G_F { get; set; }
        public string FAB_PROD_CAT_SNAME { get; set; }
        public string FIN_DIA { get; set; }
        public string MC_DIA { get; set; }
        public string MC_GG { get; set; }
        public string ORDER_NO_LST { get; set; }
        public string WORK_STYLE_NO { get; set; }
        public string STYLE_NO { get; set; }
        public string FAB_TYPE_SNAME { get; set; }
        public string FABRIC_DESC { get; set; }
        public string KNT_YRN_LOT_LST { get; set; }
        public string BYR_ACC_NAME_EN { get; set; }
        public string COLOR_NAME_EN { get; set; }
        public Int32 TODAY_RCV_NO_ROLL { get; set; }
        public decimal? TODAY_RCV_ROLL_WT { get; set; }
        public decimal? TODAY_DELV_ROLL_WT { get; set; }
        public decimal? TARGET_QTY { get; set; }
        public decimal? CUMULA_QTY { get; set; }
        public decimal? BAL_QTY { get; set; }
        public string GROUP_FIELD { get; set; }


        public object GetFabStrStkList(int pageNumber, int pageSize, Int32? pRF_FAB_PROD_CAT_ID, Int32? pMC_BYR_ACC_ID,
            Int32? pMC_COLOR_ID, Int64? pMC_FAB_PROD_ORD_H_ID, string pORDER_NO_LST, string pIS_G_F, DateTime? pFROM_DT, DateTime? pTO_DT)
        {

            string sp = "pkg_knit_fab_rcv_delv.knt_fab_str_rcv_select";
            try
            {
                Int64 vTotalRec = 0;
                var obList = new List<KNT_FAB_STR_STK_VM>();
                var obj = new RF_PAGERModel();

                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pRF_FAB_PROD_CAT_ID", Value = pRF_FAB_PROD_CAT_ID},
                     new CommandParameter() {ParameterName = "pMC_BYR_ACC_ID", Value = pMC_BYR_ACC_ID},
                     new CommandParameter() {ParameterName = "pMC_COLOR_ID", Value = pMC_COLOR_ID},
                     new CommandParameter() {ParameterName = "pMC_FAB_PROD_ORD_H_ID", Value = pMC_FAB_PROD_ORD_H_ID},
                     new CommandParameter() {ParameterName = "pORDER_NO_LST", Value = pORDER_NO_LST},
                     new CommandParameter() {ParameterName = "pIS_G_F", Value = pIS_G_F},
                     new CommandParameter() {ParameterName = "pFROM_DT", Value = pFROM_DT},
                     new CommandParameter() {ParameterName = "pTO_DT", Value = pTO_DT},
                     new CommandParameter() {ParameterName = "pageNumber", Value = pageNumber},
                     new CommandParameter() {ParameterName = "pageSize", Value = pageSize},

                     new CommandParameter() {ParameterName = "pOption", Value =3004},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    KNT_FAB_STR_STK_VM ob = new KNT_FAB_STR_STK_VM();

                    ob.GROUP_FIELD = "Type: " + Convert.ToString(dr["FAB_PROD_CAT_SNAME"]) + " Order: " + Convert.ToString(dr["ORDER_NO_LST"]) + " Color: " + Convert.ToString(dr["COLOR_NAME_EN"]) + " Fabric: " + Convert.ToString(dr["FABRIC_DESC"]); //Convert.ToString(dr["MC_FAB_PROD_ORD_D_ID"]);

                    ob.FAB_PROD_CAT_SNAME = (dr["FAB_PROD_CAT_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FAB_PROD_CAT_SNAME"]);
                    ob.BYR_ACC_NAME_EN = (dr["BYR_ACC_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BYR_ACC_NAME_EN"]);
                    ob.IS_G_F = (dr["IS_G_F"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_G_F"]);
                    ob.FAB_TYPE_SNAME = (dr["FAB_TYPE_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FAB_TYPE_SNAME"]);
                    ob.FABRIC_DESC = (dr["FABRIC_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FABRIC_DESC"]);
                    ob.COLOR_NAME_EN = (dr["COLOR_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_NAME_EN"]);

                    //ob.PROD_DT = (dr["PROD_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["PROD_DT"]);
                    ob.FIN_DIA = (dr["FIN_DIA"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FIN_DIA"]);
                    ob.MC_DIA = (dr["MC_DIA"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MC_DIA"]);

                    ob.MC_GG = (dr["MC_GG"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MC_GG"]);
                    ob.ORDER_NO_LST = (dr["ORDER_NO_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_NO_LST"]);
                    ob.WORK_STYLE_NO = (dr["WORK_STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["WORK_STYLE_NO"]);
                    ob.STYLE_NO = (dr["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO"]);

                    ob.KNT_YRN_LOT_LST = (dr["KNT_YRN_LOT_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["KNT_YRN_LOT_LST"]);

                    ob.TODAY_RCV_NO_ROLL = (dr["TODAY_RCV_NO_ROLL"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["TODAY_RCV_NO_ROLL"]);
                    ob.TODAY_RCV_ROLL_WT = (dr["TODAY_RCV_ROLL_WT"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["TODAY_RCV_ROLL_WT"]);
                    ob.TODAY_DELV_ROLL_WT = (dr["TODAY_RCV_ROLL_WT"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["TODAY_RCV_ROLL_WT"]);

                    if (dr["TARGET_QTY"] != DBNull.Value)
                        ob.TARGET_QTY = (dr["TARGET_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["TARGET_QTY"]);

                    ob.CUMULA_QTY = (dr["CUMULA_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["CUMULA_QTY"]);
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





    }



}