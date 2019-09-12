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
    public class GMT_PLN_EVNT_ACCESSModel
    {
        public Int64 GMT_PLN_EVNT_ACCESS_ID { get; set; }
        public Int64 SC_USER_ID { get; set; }
        public string IS_CLK_DISABLED { get; set; }
        public string IS_MOVE_V_DISABLED { get; set; }
        public string IS_MOVE_H_DISABLED { get; set; }
        public string IS_DELETE_DISABLED { get; set; }
        public string IS_DBL_CLK_DISABLED { get; set; }
        public string IS_MOVE_DISABLED { get; set; }
        public string IS_RESIZE_DISABLED { get; set; }
        public string IS_RIGHT_CLK_DISABLED { get; set; }


        public string Save()
        {
            const string sp = "pkg_planning_common.gmt_pln_evnt_access_save";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pGMT_PLN_EVNT_ACCESS_ID", Value = ob.GMT_PLN_EVNT_ACCESS_ID},
                     new CommandParameter() {ParameterName = "pSC_USER_ID", Value = ob.SC_USER_ID},
                     new CommandParameter() {ParameterName = "pIS_CLK_DISABLED", Value = ob.IS_CLK_DISABLED},
                     new CommandParameter() {ParameterName = "pIS_MOVE_V_DISABLED", Value = ob.IS_MOVE_V_DISABLED},
                     new CommandParameter() {ParameterName = "pIS_MOVE_H_DISABLED", Value = ob.IS_MOVE_H_DISABLED},
                     new CommandParameter() {ParameterName = "pIS_DELETE_DISABLED", Value = ob.IS_DELETE_DISABLED},
                     new CommandParameter() {ParameterName = "pIS_DBL_CLK_DISABLED", Value = ob.IS_DBL_CLK_DISABLED},
                     new CommandParameter() {ParameterName = "pIS_MOVE_DISABLED", Value = ob.IS_MOVE_DISABLED},
                     new CommandParameter() {ParameterName = "pIS_RESIZE_DISABLED", Value = ob.IS_RESIZE_DISABLED},
                     new CommandParameter() {ParameterName = "pIS_RIGHT_CLK_DISABLED", Value = ob.IS_RIGHT_CLK_DISABLED},
                     
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


        public List<GMT_PLN_EVNT_ACCESSModel> SelectAll()
        {
            string sp = "Select_GMT_PLN_EVNT_ACCESS";
            try
            {
                var obList = new List<GMT_PLN_EVNT_ACCESSModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pGMT_PLN_EVNT_ACCESS_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    GMT_PLN_EVNT_ACCESSModel ob = new GMT_PLN_EVNT_ACCESSModel();
                    ob.GMT_PLN_EVNT_ACCESS_ID = (dr["GMT_PLN_EVNT_ACCESS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_PLN_EVNT_ACCESS_ID"]);
                    ob.SC_USER_ID = (dr["SC_USER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SC_USER_ID"]);
                    ob.IS_CLK_DISABLED = (dr["IS_CLK_DISABLED"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_CLK_DISABLED"]);
                    ob.IS_MOVE_V_DISABLED = (dr["IS_MOVE_V_DISABLED"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_MOVE_V_DISABLED"]);
                    ob.IS_MOVE_H_DISABLED = (dr["IS_MOVE_H_DISABLED"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_MOVE_H_DISABLED"]);
                    ob.IS_DELETE_DISABLED = (dr["IS_DELETE_DISABLED"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_DELETE_DISABLED"]);
                    ob.IS_DBL_CLK_DISABLED = (dr["IS_DBL_CLK_DISABLED"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_DBL_CLK_DISABLED"]);
                    ob.IS_MOVE_DISABLED = (dr["IS_MOVE_DISABLED"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_MOVE_DISABLED"]);
                    ob.IS_RESIZE_DISABLED = (dr["IS_RESIZE_DISABLED"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_RESIZE_DISABLED"]);
                    ob.IS_RIGHT_CLK_DISABLED = (dr["IS_RIGHT_CLK_DISABLED"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_RIGHT_CLK_DISABLED"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public GMT_PLN_EVNT_ACCESSModel GetEventAccess(long pSC_USER_ID)
        {
            string sp = "pkg_planning_common.gmt_pln_evnt_access_select";
            try
            {
                var ob = new GMT_PLN_EVNT_ACCESSModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pSC_USER_ID", Value =pSC_USER_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.GMT_PLN_EVNT_ACCESS_ID = (dr["GMT_PLN_EVNT_ACCESS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_PLN_EVNT_ACCESS_ID"]);
                    ob.SC_USER_ID = (dr["SC_USER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SC_USER_ID"]);
                    ob.IS_CLK_DISABLED = (dr["IS_CLK_DISABLED"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_CLK_DISABLED"]);
                    ob.IS_MOVE_V_DISABLED = (dr["IS_MOVE_V_DISABLED"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_MOVE_V_DISABLED"]);
                    ob.IS_MOVE_H_DISABLED = (dr["IS_MOVE_H_DISABLED"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_MOVE_H_DISABLED"]);
                    ob.IS_DELETE_DISABLED = (dr["IS_DELETE_DISABLED"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_DELETE_DISABLED"]);
                    ob.IS_DBL_CLK_DISABLED = (dr["IS_DBL_CLK_DISABLED"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_DBL_CLK_DISABLED"]);
                    ob.IS_MOVE_DISABLED = (dr["IS_MOVE_DISABLED"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_MOVE_DISABLED"]);
                    ob.IS_RESIZE_DISABLED = (dr["IS_RESIZE_DISABLED"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_RESIZE_DISABLED"]);
                    ob.IS_RIGHT_CLK_DISABLED = (dr["IS_RIGHT_CLK_DISABLED"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_RIGHT_CLK_DISABLED"]);
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