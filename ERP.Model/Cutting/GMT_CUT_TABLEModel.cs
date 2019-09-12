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
    public class GMT_CUT_TABLEModel
    {
        public Int64 GMT_CUT_TABLE_ID { get; set; }
        public string TABLE_NO { get; set; }
        public Int64 HR_PROD_FLR_ID { get; set; }
        public Decimal TBL_LENGTH { get; set; }
        public Decimal TBL_WIDTH { get; set; }
        public Int64 LW_MOU_ID { get; set; }
        public Int64 MAX_LAY { get; set; }
        public Int64 SCM_SUPPLIER_ID { get; set; }
        public Int64 TBL_BRND_ID { get; set; }
        public Int64 MCN_BRND_ID { get; set; }
        public Int64 C_ORIGIN_ID { get; set; }
        public string IS_AUTO { get; set; }
        public string IS_AUTO_SPRD { get; set; }
        public string MODEL_NO { get; set; }
        public DateTime REG_DT { get; set; }
        public string REMARKS { get; set; }
        public Int64 LK_MAC_STATUS_ID { get; set; }
       

        public string Save()
        {
            const string sp = "SP_GMT_CUT_TABLE";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pGMT_CUT_TABLE_ID", Value = ob.GMT_CUT_TABLE_ID},
                     new CommandParameter() {ParameterName = "pTABLE_NO", Value = ob.TABLE_NO},
                     new CommandParameter() {ParameterName = "pHR_PROD_FLR_ID", Value = ob.HR_PROD_FLR_ID},
                     new CommandParameter() {ParameterName = "pTBL_LENGTH", Value = ob.TBL_LENGTH},
                     new CommandParameter() {ParameterName = "pTBL_WIDTH", Value = ob.TBL_WIDTH},
                     new CommandParameter() {ParameterName = "pLW_MOU_ID", Value = ob.LW_MOU_ID},
                     new CommandParameter() {ParameterName = "pMAX_LAY", Value = ob.MAX_LAY},
                     new CommandParameter() {ParameterName = "pSCM_SUPPLIER_ID", Value = ob.SCM_SUPPLIER_ID},
                     new CommandParameter() {ParameterName = "pTBL_BRND_ID", Value = ob.TBL_BRND_ID},
                     new CommandParameter() {ParameterName = "pMCN_BRND_ID", Value = ob.MCN_BRND_ID},
                     new CommandParameter() {ParameterName = "pC_ORIGIN_ID", Value = ob.C_ORIGIN_ID},
                     new CommandParameter() {ParameterName = "pIS_AUTO", Value = ob.IS_AUTO},
                     new CommandParameter() {ParameterName = "pIS_AUTO_SPRD", Value = ob.IS_AUTO_SPRD},
                     new CommandParameter() {ParameterName = "pMODEL_NO", Value = ob.MODEL_NO},
                     new CommandParameter() {ParameterName = "pREG_DT", Value = ob.REG_DT},
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


        public List<GMT_CUT_TABLEModel> GetCuttingTableList()
        {
            string sp = "pkg_cut_marker.gmt_cut_table_select";
            try
            {
                var obList = new List<GMT_CUT_TABLEModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {                     
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    GMT_CUT_TABLEModel ob = new GMT_CUT_TABLEModel();
                    ob.GMT_CUT_TABLE_ID = (dr["GMT_CUT_TABLE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_CUT_TABLE_ID"]);
                    ob.TABLE_NO = (dr["TABLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TABLE_NO"]);
                    ob.HR_PROD_FLR_ID = (dr["HR_PROD_FLR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_PROD_FLR_ID"]);
                    ob.TBL_LENGTH = (dr["TBL_LENGTH"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["TBL_LENGTH"]);
                    ob.TBL_WIDTH = (dr["TBL_WIDTH"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["TBL_WIDTH"]);
                    ob.LW_MOU_ID = (dr["LW_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LW_MOU_ID"]);
                    ob.MAX_LAY = (dr["MAX_LAY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MAX_LAY"]);
                    ob.SCM_SUPPLIER_ID = (dr["SCM_SUPPLIER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_SUPPLIER_ID"]);
                    ob.TBL_BRND_ID = (dr["TBL_BRND_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["TBL_BRND_ID"]);
                    ob.MCN_BRND_ID = (dr["MCN_BRND_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MCN_BRND_ID"]);
                    ob.C_ORIGIN_ID = (dr["C_ORIGIN_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["C_ORIGIN_ID"]);
                    ob.IS_AUTO = (dr["IS_AUTO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_AUTO"]);
                    ob.IS_AUTO_SPRD = (dr["IS_AUTO_SPRD"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_AUTO_SPRD"]);
                    ob.MODEL_NO = (dr["MODEL_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MODEL_NO"]);
                    ob.REG_DT = (dr["REG_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["REG_DT"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                    ob.LK_MAC_STATUS_ID = (dr["LK_MAC_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_MAC_STATUS_ID"]);
                  
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public GMT_CUT_TABLEModel Select(long ID)
        {
            string sp = "Select_GMT_CUT_TABLE";
            try
            {
                var ob = new GMT_CUT_TABLEModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pGMT_CUT_TABLE_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.GMT_CUT_TABLE_ID = (dr["GMT_CUT_TABLE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_CUT_TABLE_ID"]);
                    ob.TABLE_NO = (dr["TABLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TABLE_NO"]);
                    ob.HR_PROD_FLR_ID = (dr["HR_PROD_FLR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_PROD_FLR_ID"]);
                    ob.TBL_LENGTH = (dr["TBL_LENGTH"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["TBL_LENGTH"]);
                    ob.TBL_WIDTH = (dr["TBL_WIDTH"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["TBL_WIDTH"]);
                    ob.LW_MOU_ID = (dr["LW_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LW_MOU_ID"]);
                    ob.MAX_LAY = (dr["MAX_LAY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MAX_LAY"]);
                    ob.SCM_SUPPLIER_ID = (dr["SCM_SUPPLIER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_SUPPLIER_ID"]);
                    ob.TBL_BRND_ID = (dr["TBL_BRND_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["TBL_BRND_ID"]);
                    ob.MCN_BRND_ID = (dr["MCN_BRND_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MCN_BRND_ID"]);
                    ob.C_ORIGIN_ID = (dr["C_ORIGIN_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["C_ORIGIN_ID"]);
                    ob.IS_AUTO = (dr["IS_AUTO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_AUTO"]);
                    ob.IS_AUTO_SPRD = (dr["IS_AUTO_SPRD"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_AUTO_SPRD"]);
                    ob.MODEL_NO = (dr["MODEL_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MODEL_NO"]);
                    ob.REG_DT = (dr["REG_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["REG_DT"]);
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