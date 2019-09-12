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
    public class RF_REPORTModel //: IRF_REPORTModel
    {
        public Int64? RF_REPORT_ID { get; set; }
        public Int64? RF_REPORT_GRP_ID { get; set; }
        public string RPT_CODE { get; set; }
        public string RPT_NAME_EN { get; set; }
        public string RPT_NAME_BN { get; set; }
        public Int64? DISPLAY_ORDER { get; set; }
        public string RPT_SQL { get; set; }
        public string RPT_PARAM { get; set; }
        public string IS_ACTIVE { get; set; }
        //public DateTime CREATION_DATE { get; set; }
        //public Int64 CREATED_BY { get; set; }
        //public DateTime LAST_UPDATE_DATE { get; set; }
        //public Int64 LAST_UPDATED_BY { get; set; }

        public string ROLE_RPT_XML { get; set; }
        public Int64? SC_ROLE_ID { get; set; }
        public Int64? SC_ROLE_REPORT_ID { get; set; }


        public List<RF_REPORTModel> ReportListData(long pSC_MENU_ID)
        {
            string sp = "pkg_security.rf_report_select";
            try
            {
                var obList = new List<RF_REPORTModel>();

                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
                    new CommandParameter() {ParameterName = "pSC_MENU_ID", Value = pSC_MENU_ID},
                    new CommandParameter() {ParameterName = "pOption", Value = 3002},
                    new CommandParameter() {ParameterName = "pMsg", Value = 500, Direction = ParameterDirection.Output}
                }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    RF_REPORTModel ob = new RF_REPORTModel();
                    ob.RF_REPORT_ID = (dr["RF_REPORT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_REPORT_ID"]);
                    ob.RF_REPORT_GRP_ID = (dr["RF_REPORT_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_REPORT_GRP_ID"]);
                    ob.RPT_CODE = (dr["RPT_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["RPT_CODE"]);
                    ob.RPT_NAME_EN = (dr["RPT_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["RPT_NAME_EN"]);
                    ob.RPT_NAME_BN = (dr["RPT_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["RPT_NAME_BN"]);
                    ob.DISPLAY_ORDER = (dr["DISPLAY_ORDER"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DISPLAY_ORDER"]);
                    ob.RPT_SQL = (dr["RPT_SQL"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["RPT_SQL"]);
                    ob.RPT_PARAM = (dr["RPT_PARAM"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["RPT_PARAM"]);
                    ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object getRoleRptDataList(int pRF_REPORT_GRP_ID, int pSC_ROLE_ID)
        {
            string sp = "pkg_security.rf_report_select";
            try
            {
                var obList = new List<RF_REPORTModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                    new CommandParameter() {ParameterName = "pRF_REPORT_GRP_ID", Value = pRF_REPORT_GRP_ID},
                    new CommandParameter() {ParameterName = "pSC_ROLE_ID", Value = pSC_ROLE_ID},
                    new CommandParameter() {ParameterName = "pOption", Value = 3003},
                    new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    RF_REPORTModel ob = new RF_REPORTModel();
                    ob.SC_ROLE_REPORT_ID = (dr["SC_ROLE_REPORT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SC_ROLE_REPORT_ID"]);
                    ob.RF_REPORT_ID = (dr["RF_REPORT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_REPORT_ID"]);
                    ob.SC_ROLE_ID = (dr["SC_ROLE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SC_ROLE_ID"]);
                    ob.RF_REPORT_GRP_ID = (dr["RF_REPORT_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_REPORT_GRP_ID"]);
                    ob.RPT_NAME_EN = (dr["RPT_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["RPT_NAME_EN"]);
                    ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //public List<RF_REPORTModel> ScSideMenuData(Int64 pSC_USER_ID)
        //{
        //    string sp = "pkg_menu.sc_menu_select";
            
        //    try
        //    {

        //        var obList = new List<ScMenuModel>();
        //        var obUserMenuList = new List<ScMenuModel>();

        //        OraDatabase db = new OraDatabase();
        //        var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
        //        {   new CommandParameter() {ParameterName = "pOption", Value = 3006},                    
        //            new CommandParameter() {ParameterName = "pSC_USER_ID", Value = pSC_USER_ID},
        //        }, sp);
        //        foreach (DataRow dr in ds.Tables[0].Rows)
        //        {
        //            RF_REPORTModel ob = new RF_REPORTModel();
        //            ob.SC_ROLE_REPORT_ID = (dr["SC_ROLE_REPORT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SC_ROLE_REPORT_ID"]);
        //            ob.RF_REPORT_ID = (dr["RF_REPORT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_REPORT_ID"]);
        //            ob.SC_ROLE_ID = (dr["SC_ROLE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SC_ROLE_ID"]);
        //            ob.RF_REPORT_GRP_ID = (dr["RF_REPORT_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_REPORT_GRP_ID"]);
        //            ob.RPT_NAME_EN = (dr["RPT_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["RPT_NAME_EN"]);
        //            ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);

        //            obList.Add(ob);
        //        }
        //        return obList;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        public string SaveRoleReport()
        {
            const string sp = "pkg_security.sc_role_report_save";
            string jsonStr = "{";
            var ob = this;
            var i = 1;

            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {                    
                    new CommandParameter() {ParameterName = "pROLE_RPT_XML", Value = ob.ROLE_RPT_XML}, 
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




        public object getReportDataListByUser(int pRF_REPORT_GRP_ID)
        {
            string sp = "pkg_security.rf_report_select";
            try
            {
                string vUserType= Convert.ToString(HttpContext.Current.Session["multiUserType"]);

                var obList = new List<RF_REPORTModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                    new CommandParameter() {ParameterName = "pRF_REPORT_GRP_ID", Value = pRF_REPORT_GRP_ID},
                    new CommandParameter() {ParameterName = "pSC_USER_ID", Value = HttpContext.Current.Session["multiScUserId"]},
                    new CommandParameter() {ParameterName = "pOption", Value = vUserType=="B"?3005:3004},
                    new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    RF_REPORTModel ob = new RF_REPORTModel();                    
                    ob.RPT_NAME_EN = (dr["RPT_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["RPT_NAME_EN"]);
                    ob.RPT_CODE = (dr["RPT_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["RPT_CODE"]);

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







    /// <summary>
    /// /////////////////////////////////////////////////////////////////////////////
    /// </summary>
    public class RF_REPORT_GRPModel
    {
        public Int64 RF_REPORT_GRP_ID { get; set; }
        public Int64? SC_MENU_ID { get; set; }
        public string RPT_GRP_NAME_EN { get; set; }


        public object getRptGrpDataList()
        {
            string sp = "pkg_security.rf_report_select";
            try
            {
                var obList = new List<RF_REPORT_GRPModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                    //new CommandParameter() {ParameterName = "pSC_ROLE_ID", Value = pSC_ROLE_ID},
                    new CommandParameter() {ParameterName = "pOption", Value = 3002},
                    new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    RF_REPORT_GRPModel ob = new RF_REPORT_GRPModel();
                    ob.RF_REPORT_GRP_ID = (dr["RF_REPORT_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_REPORT_GRP_ID"]);
                    ob.RPT_GRP_NAME_EN = (dr["RPT_GRP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["RPT_GRP_NAME_EN"]);                    
                    ob.SC_MENU_ID = (dr["SC_MENU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SC_MENU_ID"]);

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