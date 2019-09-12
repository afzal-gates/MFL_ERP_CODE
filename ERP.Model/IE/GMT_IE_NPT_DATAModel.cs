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

    public class GMT_IE_NPT_DATA_HModel
    {
        
        public Int64 GMT_PROD_PLN_CLNDR_ID { get; set; }
        public Int64 HR_PROD_LINE_ID { get; set; }
        public string LINE_CODE { get; set; }
        public string XML { get; set; }
        public List<GMT_IE_NPT_DATAModel> npts { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }

        public string Save()
        // pOption=1000=>General Form Save
        {
            const string sp = "PKG_GMT_IE.gmt_ie_npt_data_save";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pGMT_PROD_PLN_CLNDR_ID", Value = ob.GMT_PROD_PLN_CLNDR_ID},
                     new CommandParameter() {ParameterName = "pHR_PROD_LINE_ID", Value = ob.HR_PROD_LINE_ID},
                     new CommandParameter() {ParameterName = "pXML", Value = ob.XML},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
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
    }
    public class GMT_IE_NPT_DATAModel
    {
        public Int64 GMT_IE_NPT_DATA_ID { get; set; }
        public Int64 GMT_PROD_PLN_CLNDR_ID { get; set; }
        public Int64 HR_PROD_LINE_ID { get; set; }
        public Int64 RF_RESP_DEPT_ID { get; set; }
        public List<Int32> HOUR_NO_LST { get; set; }
        public Int64 AFFECTED_MP { get; set; }
        public Int64 MIN_ADJUST { get; set; }
        public Int64 NPT { get; set; }
        public Int64 GMT_IE_NPT_REASON_ID { get; set; }
        public Int64 AVG_NPT_HR { get; set; }
        public string LINE_CODE { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }

        public GMT_IE_NPT_DATA_HModel getNptHeaderData(Int64 pGMT_PROD_PLN_CLNDR_ID, Int64 pHR_PROD_LINE_ID)
        {
            try
            {
                var ob = new GMT_IE_NPT_DATA_HModel();
                ob.GMT_PROD_PLN_CLNDR_ID = pGMT_PROD_PLN_CLNDR_ID;
                ob.HR_PROD_LINE_ID = pHR_PROD_LINE_ID;
                ob.npts = new List<GMT_IE_NPT_DATAModel>();
                ob.npts = this.Query(ob.GMT_PROD_PLN_CLNDR_ID, ob.HR_PROD_LINE_ID);
                ob.LINE_CODE = ob.npts.FirstOrDefault().LINE_CODE;
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private List<GMT_IE_NPT_DATAModel> Query(Int64 pGMT_PROD_PLN_CLNDR_ID, Int64 pHR_PROD_LINE_ID)
        {
            string sp = "PKG_GMT_IE.gmt_ie_npt_data_select";
            //pOption=3000=>Select All Data
            try
            {
                var obList = new List<GMT_IE_NPT_DATAModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pGMT_PROD_PLN_CLNDR_ID", Value =pGMT_PROD_PLN_CLNDR_ID},
                     new CommandParameter() {ParameterName = "pHR_PROD_LINE_ID", Value =pHR_PROD_LINE_ID},

                     new CommandParameter() {ParameterName = "pOption", Value = 3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
              foreach (DataRow dr in ds.Tables[0].Rows)
               {
                            GMT_IE_NPT_DATAModel ob = new GMT_IE_NPT_DATAModel();
                            ob.GMT_IE_NPT_DATA_ID = (dr["GMT_IE_NPT_DATA_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_IE_NPT_DATA_ID"]);
                            ob.GMT_PROD_PLN_CLNDR_ID = (dr["GMT_PROD_PLN_CLNDR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_PROD_PLN_CLNDR_ID"]);
                            ob.HR_PROD_LINE_ID = (dr["HR_PROD_LINE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_PROD_LINE_ID"]);
                            ob.RF_RESP_DEPT_ID = (dr["RF_RESP_DEPT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_RESP_DEPT_ID"]);
                            ob.HOUR_NO_LST = (dr["HOUR_NO_LST"] == DBNull.Value) ? new List<Int32>(){ 1 } :
                                Convert.ToString(dr["HOUR_NO_LST"]).Split(',').Select(Int32.Parse).ToList();
                            ob.LINE_CODE = (dr["LINE_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LINE_CODE"]);
                            ob.AFFECTED_MP = (dr["AFFECTED_MP"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["AFFECTED_MP"]);
                            ob.MIN_ADJUST = (dr["MIN_ADJUST"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MIN_ADJUST"]);
                            ob.NPT = (dr["NPT"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["NPT"]);
                            ob.GMT_IE_NPT_REASON_ID = (dr["GMT_IE_NPT_REASON_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_IE_NPT_REASON_ID"]);
                            ob.AVG_NPT_HR = (dr["AVG_NPT_HR"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["AVG_NPT_HR"]);
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