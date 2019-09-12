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
    public class RF_FAB_CLASSModel
    {
        public Int64 RF_FAB_CLASS_ID { get; set; }
        public string FAB_CLASS_CODE { get; set; }
        public string FAB_CLASS_NAME_EN { get; set; }
        public string FAB_CLASS_NAME_BN { get; set; }
        public string FAB_CLASS_SNAME { get; set; }
        public Int64 DISPLAY_ORDER { get; set; }
        public string IS_ACTIVE { get; set; }


        public string Save()
        {
            const string sp = "SP_RF_FAB_CLASS";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pRF_FAB_CLASS_ID", Value = ob.RF_FAB_CLASS_ID},
                     new CommandParameter() {ParameterName = "pFAB_CLASS_CODE", Value = ob.FAB_CLASS_CODE},
                     new CommandParameter() {ParameterName = "pFAB_CLASS_NAME_EN", Value = ob.FAB_CLASS_NAME_EN},
                     new CommandParameter() {ParameterName = "pFAB_CLASS_NAME_BN", Value = ob.FAB_CLASS_NAME_BN},
                     new CommandParameter() {ParameterName = "pFAB_CLASS_SNAME", Value = ob.FAB_CLASS_SNAME},
                     new CommandParameter() {ParameterName = "pDISPLAY_ORDER", Value = ob.DISPLAY_ORDER},
                     new CommandParameter() {ParameterName = "pIS_ACTIVE", Value = ob.IS_ACTIVE},
                     
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



        public List<RF_FAB_CLASSModel> GmtFabClassList()
        {
            string sp = "pkg_common.rf_fab_class_select";
            try
            {
                var obList = new List<RF_FAB_CLASSModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     //new CommandParameter() {ParameterName = "pRF_FAB_CLASS_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value = 3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    RF_FAB_CLASSModel ob = new RF_FAB_CLASSModel();
                    ob.RF_FAB_CLASS_ID = (dr["RF_FAB_CLASS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_FAB_CLASS_ID"]);
                    ob.FAB_CLASS_CODE = (dr["FAB_CLASS_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FAB_CLASS_CODE"]);
                    ob.FAB_CLASS_NAME_EN = (dr["FAB_CLASS_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FAB_CLASS_NAME_EN"]);
                    ob.FAB_CLASS_NAME_BN = (dr["FAB_CLASS_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FAB_CLASS_NAME_BN"]);
                    ob.FAB_CLASS_SNAME = (dr["FAB_CLASS_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FAB_CLASS_SNAME"]);
                    ob.DISPLAY_ORDER = (dr["DISPLAY_ORDER"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DISPLAY_ORDER"]);
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

        public RF_FAB_CLASSModel Select(long ID)
        {
            string sp = "Select_RF_FAB_CLASS";
            try
            {
                var ob = new RF_FAB_CLASSModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pRF_FAB_CLASS_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.RF_FAB_CLASS_ID = (dr["RF_FAB_CLASS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_FAB_CLASS_ID"]);
                    ob.FAB_CLASS_CODE = (dr["FAB_CLASS_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FAB_CLASS_CODE"]);
                    ob.FAB_CLASS_NAME_EN = (dr["FAB_CLASS_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FAB_CLASS_NAME_EN"]);
                    ob.FAB_CLASS_NAME_BN = (dr["FAB_CLASS_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FAB_CLASS_NAME_BN"]);
                    ob.FAB_CLASS_SNAME = (dr["FAB_CLASS_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FAB_CLASS_SNAME"]);
                    ob.DISPLAY_ORDER = (dr["DISPLAY_ORDER"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DISPLAY_ORDER"]);
                    ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);
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