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
    public class DYE_SC_PRG_ISSModel
    {
        public Int64 DYE_SC_PRG_ISS_ID { get; set; }
        public string PRG_ISS_NO { get; set; }
        public DateTime SC_PRG_DT { get; set; }
        public DateTime EXP_DELV_DT { get; set; }
        public Int64 SCM_SUPPLIER_ID { get; set; }
        public Int64 LK_SC_PRG_STATUS_ID { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }

        public string SUP_TRD_NAME_EN { get; set; }

        public string Save()
        {
            const string sp = "PKG_DYE_SC_PROGRAM.dye_sc_prg_iss_save";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_SC_PRG_ISS_ID", Value = ob.DYE_SC_PRG_ISS_ID},
                     new CommandParameter() {ParameterName = "pPRG_ISS_NO", Value = ob.PRG_ISS_NO},
                     new CommandParameter() {ParameterName = "pSC_PRG_DT", Value = ob.SC_PRG_DT},
                     new CommandParameter() {ParameterName = "pSCM_SUPPLIER_ID", Value = ob.SCM_SUPPLIER_ID},
                     new CommandParameter() {ParameterName = "pLK_SC_PRG_STATUS_ID", Value = ob.LK_SC_PRG_STATUS_ID},
                     new CommandParameter() {ParameterName = "pEXP_DELV_DT", Value = ob.EXP_DELV_DT},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pOption", Value =1000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output},
                     new CommandParameter() {ParameterName = "OP_DYE_SC_PRG_ISS_ID", Value =0, Direction = ParameterDirection.Output}
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

        public List<DYE_SC_PRG_ISSModel> Query(String pPRG_ISS_NO, Int64? pDYE_SC_PRG_ISS_ID, int pOption)
        {
            string sp = "PKG_DYE_SC_PROGRAM.dye_sc_prg_iss_select";
            //pOption=3000=>Select All Data
            try
            {
                var obList = new List<DYE_SC_PRG_ISSModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_SC_PRG_ISS_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =pOption},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DYE_SC_PRG_ISSModel ob = new DYE_SC_PRG_ISSModel();
                    ob.DYE_SC_PRG_ISS_ID = (dr["DYE_SC_PRG_ISS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_SC_PRG_ISS_ID"]);
                    ob.SUP_TRD_NAME_EN = (dr["SUP_TRD_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SUP_TRD_NAME_EN"]);
                    ob.PRG_ISS_NO = (dr["PRG_ISS_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PRG_ISS_NO"]);
                    ob.SC_PRG_DT = (dr["SC_PRG_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["SC_PRG_DT"]);
                    ob.EXP_DELV_DT = (dr["EXP_DELV_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["EXP_DELV_DT"]);
                    ob.SCM_SUPPLIER_ID = (dr["SCM_SUPPLIER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_SUPPLIER_ID"]);
                    ob.LK_SC_PRG_STATUS_ID = (dr["LK_SC_PRG_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_SC_PRG_STATUS_ID"]);
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DYE_SC_PRG_ISSModel Select(Int64 pDYE_SC_PRG_ISS_ID, int pOption)
        {
            string sp = "PKG_DYE_SC_PROGRAM.dye_sc_prg_iss_select";
            //pOption=3001=>Select Single Data
            try
            {
                var ob = new DYE_SC_PRG_ISSModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_SC_PRG_ISS_ID", Value = pDYE_SC_PRG_ISS_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =pOption},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.DYE_SC_PRG_ISS_ID = (dr["DYE_SC_PRG_ISS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_SC_PRG_ISS_ID"]);
                    ob.PRG_ISS_NO = (dr["PRG_ISS_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PRG_ISS_NO"]);
                    ob.SC_PRG_DT = (dr["SC_PRG_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["SC_PRG_DT"]);
                    ob.EXP_DELV_DT = (dr["EXP_DELV_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["EXP_DELV_DT"]);
                    ob.SCM_SUPPLIER_ID = (dr["SCM_SUPPLIER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_SUPPLIER_ID"]);
                    ob.LK_SC_PRG_STATUS_ID = (dr["LK_SC_PRG_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_SC_PRG_STATUS_ID"]);

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