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
    public class GMT_THRPT_TIMEModel
    {
        public Int64? GMT_THRPT_TIME_ID { get; set; }
        public Int64? FRM_PROD_TYP_ID { get; set; }
        public Int64? TO_PROD_TYP_ID { get; set; }
        public Decimal? THRPT_HR_FRS { get; set; }
        public Decimal? THRPT_MIN_FRS { get; set; }
        public Decimal? THRPT_HR_OLD { get; set; }
        public Decimal? THRPT_MIN_OLD { get; set; }

        public string FROM_PRODUCT_TYP_NAME_EN { get; set; }
        public string TO_PRODUCT_TYP_NAME_EN { get; set; }
        public string GMT_THRPT_TIME_XML { get; set; }

        public Int32 SCO_HRS { get; set; }
        public object FRM_PRODUCT_TYP
        {
            get
            {
                return new { FRM_PROD_TYP_ID = this.FRM_PROD_TYP_ID, FROM_PRODUCT_TYP_NAME_EN = this.FROM_PRODUCT_TYP_NAME_EN ?? "" };
            }
        }
        public object TO_PRODUCT_TYP
        {
            get
            {
                return new { TO_PROD_TYP_ID = this.TO_PROD_TYP_ID, TO_PRODUCT_TYP_NAME_EN = this.TO_PRODUCT_TYP_NAME_EN ?? "" };
            }
        }




        public string BatchSave()
        {
            const string sp = "pkg_planning_common.gmt_thrpt_time_save";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pGMT_THRPT_TIME_ID", Value = ob.GMT_THRPT_TIME_ID},
                     new CommandParameter() {ParameterName = "pFRM_PROD_TYP_ID", Value = ob.FRM_PROD_TYP_ID},
                     new CommandParameter() {ParameterName = "pTO_PROD_TYP_ID", Value = ob.TO_PROD_TYP_ID},
                     new CommandParameter() {ParameterName = "pTHRPT_HR_FRS", Value = ob.THRPT_HR_FRS},
                     new CommandParameter() {ParameterName = "pTHRPT_MIN_FRS", Value = ob.THRPT_MIN_FRS},
                     new CommandParameter() {ParameterName = "pTHRPT_HR_OLD", Value = ob.THRPT_HR_OLD},
                     new CommandParameter() {ParameterName = "pTHRPT_MIN_OLD", Value = ob.THRPT_MIN_OLD},
                     new CommandParameter() {ParameterName = "pGMT_THRPT_TIME_XML", Value = ob.GMT_THRPT_TIME_XML},
                     
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


        public List<GMT_THRPT_TIMEModel> GetThrghputTimeList()
        {
            string sp = "pkg_planning_common.gmt_thrpt_time_select";
            try
            {
                var obList = new List<GMT_THRPT_TIMEModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {                     
                     new CommandParameter() {ParameterName = "pOption", Value = 3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    GMT_THRPT_TIMEModel ob = new GMT_THRPT_TIMEModel();
                    ob.GMT_THRPT_TIME_ID = (dr["GMT_THRPT_TIME_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_THRPT_TIME_ID"]);

                    if (dr["FRM_PROD_TYP_ID"] != DBNull.Value)
                        ob.FRM_PROD_TYP_ID = (dr["FRM_PROD_TYP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["FRM_PROD_TYP_ID"]);
                    if (dr["TO_PROD_TYP_ID"] != DBNull.Value)
                        ob.TO_PROD_TYP_ID = (dr["TO_PROD_TYP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["TO_PROD_TYP_ID"]);
                    if (dr["THRPT_HR_FRS"] != DBNull.Value)
                        ob.THRPT_HR_FRS = (dr["THRPT_HR_FRS"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["THRPT_HR_FRS"]);
                    if (dr["THRPT_MIN_FRS"] != DBNull.Value)
                        ob.THRPT_MIN_FRS = (dr["THRPT_MIN_FRS"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["THRPT_MIN_FRS"]);
                    if (dr["THRPT_HR_OLD"] != DBNull.Value)
                        ob.THRPT_HR_OLD = (dr["THRPT_HR_OLD"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["THRPT_HR_OLD"]);
                    if (dr["THRPT_MIN_OLD"] != DBNull.Value)
                        ob.THRPT_MIN_OLD = (dr["THRPT_MIN_OLD"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["THRPT_MIN_OLD"]);

                    ob.FROM_PRODUCT_TYP_NAME_EN = (dr["FROM_PRODUCT_TYP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FROM_PRODUCT_TYP_NAME_EN"]);
                    ob.TO_PRODUCT_TYP_NAME_EN = (dr["TO_PRODUCT_TYP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TO_PRODUCT_TYP_NAME_EN"]);

                    ob.SCO_HRS = (dr["SCO_HRS"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["SCO_HRS"]); 

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public GMT_THRPT_TIMEModel Select(long ID)
        {
            string sp = "Select_GMT_THRPT_TIME";
            try
            {
                var ob = new GMT_THRPT_TIMEModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pGMT_THRPT_TIME_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.GMT_THRPT_TIME_ID = (dr["GMT_THRPT_TIME_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_THRPT_TIME_ID"]);
                    ob.FRM_PROD_TYP_ID = (dr["FRM_PROD_TYP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["FRM_PROD_TYP_ID"]);
                    ob.TO_PROD_TYP_ID = (dr["TO_PROD_TYP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["TO_PROD_TYP_ID"]);
                    ob.THRPT_HR_FRS = (dr["THRPT_HR_FRS"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["THRPT_HR_FRS"]);
                    ob.THRPT_MIN_FRS = (dr["THRPT_MIN_FRS"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["THRPT_MIN_FRS"]);
                    ob.THRPT_HR_OLD = (dr["THRPT_HR_OLD"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["THRPT_HR_OLD"]);
                    ob.THRPT_MIN_OLD = (dr["THRPT_MIN_OLD"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["THRPT_MIN_OLD"]);
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