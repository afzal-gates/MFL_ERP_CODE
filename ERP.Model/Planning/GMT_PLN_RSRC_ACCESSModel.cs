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
    public class GMT_PLN_RSRC_ACCESSModel
    {
        public Int64 GMT_PLN_RSRC_ACCESS_ID { get; set; }
        public Int64 SC_USER_ID { get; set; }
        public Int64 HR_PROD_LINE_ID { get; set; }
        public string RSRC_ACCESS_XML { get; set; }



        public string Save()
        {
            const string sp = "pkg_planning_common.gmt_pln_rsrc_access_save";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pRSRC_ACCESS_XML", Value = ob.RSRC_ACCESS_XML},
                     new CommandParameter() {ParameterName = "pSC_USER_ID", Value = ob.SC_USER_ID},
                                          
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


        public List<GMT_PLN_RSRC_ACCESSModel> SelectAll()
        {
            string sp = "Select_GMT_PLN_RSRC_ACCESS";
            try
            {
                var obList = new List<GMT_PLN_RSRC_ACCESSModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pGMT_PLN_RSRC_ACCESS_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    GMT_PLN_RSRC_ACCESSModel ob = new GMT_PLN_RSRC_ACCESSModel();
                    ob.GMT_PLN_RSRC_ACCESS_ID = (dr["GMT_PLN_RSRC_ACCESS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_PLN_RSRC_ACCESS_ID"]);
                    ob.SC_USER_ID = (dr["SC_USER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SC_USER_ID"]);
                    ob.HR_PROD_LINE_ID = (dr["HR_PROD_LINE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_PROD_LINE_ID"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public GMT_PLN_RSRC_ACCESSModel Select(long ID)
        {
            string sp = "Select_GMT_PLN_RSRC_ACCESS";
            try
            {
                var ob = new GMT_PLN_RSRC_ACCESSModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pGMT_PLN_RSRC_ACCESS_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.GMT_PLN_RSRC_ACCESS_ID = (dr["GMT_PLN_RSRC_ACCESS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_PLN_RSRC_ACCESS_ID"]);
                    ob.SC_USER_ID = (dr["SC_USER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SC_USER_ID"]);
                    ob.HR_PROD_LINE_ID = (dr["HR_PROD_LINE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_PROD_LINE_ID"]);
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