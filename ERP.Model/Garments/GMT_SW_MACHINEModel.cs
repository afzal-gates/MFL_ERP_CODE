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
    public class GMT_SW_MACHINEModel
    {
        public Int64 GMT_SW_MACHINE_ID { get; set; }
        public Int64 GMT_SW_MCN_SPEC_ID { get; set; }
        public string MC_CODE_NO { get; set; }
        public Int64 SCM_SUPPLIER_ID { get; set; }
        public Int64 RF_BRAND_ID { get; set; }
        public string MC_SERIAL_NO { get; set; }
        public string MODEL_NO { get; set; }
        public Int64 C_ORIGIN_ID { get; set; }
        public DateTime REG_DT { get; set; }
        public Int64 HR_PROD_FLR_ID { get; set; }
        public string IS_HIRED { get; set; }
        public string REMARKS { get; set; }
        public Int64 LK_MAC_STATUS_ID { get; set; }
        
        public string SUP_TRD_NAME_EN { get; set; }
        public string BRAND_NAME_EN { get; set; }
        public string FLOOR_DESC_EN { get; set; }



        public string Save()
        {
            const string sp = "SP_GMT_SW_MACHINE";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pGMT_SW_MACHINE_ID", Value = ob.GMT_SW_MACHINE_ID},
                     new CommandParameter() {ParameterName = "pGMT_SW_MCN_SPEC_ID", Value = ob.GMT_SW_MCN_SPEC_ID},
                     new CommandParameter() {ParameterName = "pMC_CODE_NO", Value = ob.MC_CODE_NO},
                     new CommandParameter() {ParameterName = "pSCM_SUPPLIER_ID", Value = ob.SCM_SUPPLIER_ID},
                     new CommandParameter() {ParameterName = "pRF_BRAND_ID", Value = ob.RF_BRAND_ID},
                     new CommandParameter() {ParameterName = "pMC_SERIAL_NO", Value = ob.MC_SERIAL_NO},
                     new CommandParameter() {ParameterName = "pMODEL_NO", Value = ob.MODEL_NO},
                     new CommandParameter() {ParameterName = "pC_ORIGIN_ID", Value = ob.C_ORIGIN_ID},
                     new CommandParameter() {ParameterName = "pREG_DT", Value = ob.REG_DT},
                     new CommandParameter() {ParameterName = "pHR_PROD_FLR_ID", Value = ob.HR_PROD_FLR_ID},
                     new CommandParameter() {ParameterName = "pIS_HIRED", Value = ob.IS_HIRED},
                     new CommandParameter() {ParameterName = "pREMARKS", Value = ob.REMARKS},
                     new CommandParameter() {ParameterName = "pLK_MAC_STATUS_ID", Value = ob.LK_MAC_STATUS_ID},
                     
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     
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



        public List<GMT_SW_MACHINEModel> GetSwMcnProfileList(Int64 pGMT_SW_MCN_SPEC_ID)
        {
            string sp = "pkg_gmt_swmcn.gmt_sw_mcn_spec_select";
            try
            {
                var obList = new List<GMT_SW_MACHINEModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pGMT_SW_MCN_SPEC_ID", Value = pGMT_SW_MCN_SPEC_ID},
                     new CommandParameter() {ParameterName = "pOption", Value = 3004},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    GMT_SW_MACHINEModel ob = new GMT_SW_MACHINEModel();
                    ob.GMT_SW_MACHINE_ID = (dr["GMT_SW_MACHINE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_SW_MACHINE_ID"]);
                    ob.GMT_SW_MCN_SPEC_ID = (dr["GMT_SW_MCN_SPEC_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_SW_MCN_SPEC_ID"]);
                    ob.MC_CODE_NO = (dr["MC_CODE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MC_CODE_NO"]);
                    ob.SCM_SUPPLIER_ID = (dr["SCM_SUPPLIER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_SUPPLIER_ID"]);
                    ob.RF_BRAND_ID = (dr["RF_BRAND_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_BRAND_ID"]);
                    ob.MC_SERIAL_NO = (dr["MC_SERIAL_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MC_SERIAL_NO"]);
                    ob.MODEL_NO = (dr["MODEL_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MODEL_NO"]);
                    ob.C_ORIGIN_ID = (dr["C_ORIGIN_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["C_ORIGIN_ID"]);
                    ob.REG_DT = (dr["REG_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["REG_DT"]);
                    ob.HR_PROD_FLR_ID = (dr["HR_PROD_FLR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_PROD_FLR_ID"]);
                    ob.IS_HIRED = (dr["IS_HIRED"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_HIRED"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                    ob.LK_MAC_STATUS_ID = (dr["LK_MAC_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_MAC_STATUS_ID"]);

                    ob.SUP_TRD_NAME_EN = (dr["SUP_TRD_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SUP_TRD_NAME_EN"]);
                    ob.BRAND_NAME_EN = (dr["BRAND_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BRAND_NAME_EN"]);
                    ob.FLOOR_DESC_EN = (dr["FLOOR_DESC_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FLOOR_DESC_EN"]);
                   
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public GMT_SW_MACHINEModel Select(long ID)
        {
            string sp = "Select_GMT_SW_MACHINE";
            try
            {
                var ob = new GMT_SW_MACHINEModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pGMT_SW_MACHINE_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.GMT_SW_MACHINE_ID = (dr["GMT_SW_MACHINE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_SW_MACHINE_ID"]);
                    ob.GMT_SW_MCN_SPEC_ID = (dr["GMT_SW_MCN_SPEC_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_SW_MCN_SPEC_ID"]);
                    ob.MC_CODE_NO = (dr["MC_CODE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MC_CODE_NO"]);
                    ob.SCM_SUPPLIER_ID = (dr["SCM_SUPPLIER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_SUPPLIER_ID"]);
                    ob.RF_BRAND_ID = (dr["RF_BRAND_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_BRAND_ID"]);
                    ob.MC_SERIAL_NO = (dr["MC_SERIAL_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MC_SERIAL_NO"]);
                    ob.MODEL_NO = (dr["MODEL_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MODEL_NO"]);
                    ob.C_ORIGIN_ID = (dr["C_ORIGIN_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["C_ORIGIN_ID"]);
                    ob.REG_DT = (dr["REG_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["REG_DT"]);
                    ob.HR_PROD_FLR_ID = (dr["HR_PROD_FLR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_PROD_FLR_ID"]);
                    ob.IS_HIRED = (dr["IS_HIRED"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_HIRED"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                    ob.LK_MAC_STATUS_ID = (dr["LK_MAC_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_MAC_STATUS_ID"]);
                    
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