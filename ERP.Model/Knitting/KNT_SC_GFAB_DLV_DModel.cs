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
    public class KNT_SC_GFAB_DLV_DModel
    {
        public Int64 KNT_SC_GFAB_DLV_D_ID { get; set; }
        public Int64 KNT_SC_GFAB_DLV_H_ID { get; set; }
        public Int64 KNT_FAB_ROLL_ID { get; set; }
        public Decimal DELV_ROLL_WT { get; set; }
        public Int64 WT_MOU_ID { get; set; }


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

        public Int32 DELV_NO_ROLL { get; set; }
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
        public string DELV_STATUS { get; set; }



        public object GetScFabDelvDtlByDelvHid(int pageNumber, int pageSize, Int64 pKNT_SC_GFAB_DLV_H_ID)
        {
            string sp = "pkg_knit_fab_rcv_delv.knt_sc_gfab_dlv_h_select";
            try
            {
                Int64 vTotalRec = 0;
                var obList = new List<KNT_SC_GFAB_DLV_DModel>();
                var obj = new RF_PAGERModel();

                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_SC_GFAB_DLV_H_ID", Value =pKNT_SC_GFAB_DLV_H_ID},
                     new CommandParameter() {ParameterName = "pageNumber", Value =pageNumber},
                     new CommandParameter() {ParameterName = "pageSize", Value =pageSize},

                     new CommandParameter() {ParameterName = "pOption", Value = 3003},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    KNT_SC_GFAB_DLV_DModel ob = new KNT_SC_GFAB_DLV_DModel();
                    ob.KNT_SC_GFAB_DLV_D_ID = (dr["KNT_SC_GFAB_DLV_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_SC_GFAB_DLV_D_ID"]);
                    ob.KNT_SC_GFAB_DLV_H_ID = (dr["KNT_SC_GFAB_DLV_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_SC_GFAB_DLV_H_ID"]);
                    ob.KNT_FAB_ROLL_ID = (dr["KNT_FAB_ROLL_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_FAB_ROLL_ID"]);
                    ob.DELV_ROLL_WT = (dr["DELV_ROLL_WT"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["DELV_ROLL_WT"]);
                    ob.WT_MOU_ID = (dr["WT_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["WT_MOU_ID"]);
                    ob.DELV_NO_ROLL = (dr["DELV_NO_ROLL"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["DELV_NO_ROLL"]);
                    
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
                    ob.DELV_STATUS = (dr["DELV_STATUS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DELV_STATUS"]);

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

        public string RemoveFabDelv()
        {
            const string sp = "pkg_knit_fab_rcv_delv.knt_sc_gfab_dlv_remove";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_SC_GFAB_DLV_D_ID", Value = ob.KNT_SC_GFAB_DLV_D_ID},                                                     
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pOption", Value = 4000},
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
        

        
    }
}