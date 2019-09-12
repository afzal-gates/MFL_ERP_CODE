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

    public class CORE_DEPT_MODEL
    {
        public Int32? CORE_DEPT_ID { get; set; }
        public string DEPT_NAME { get; set; }

        private List<GMT_PLN_HOLYDAYModel> _items = null;
        public List<GMT_PLN_HOLYDAYModel> items
        {
            get
            {
                if (_items == null)
                {
                    _items = new List<GMT_PLN_HOLYDAYModel>();
                }
                return _items;
            }
            set
            {
                _items = value;
            }
        }

        public List<CORE_DEPT_MODEL> query(Int32 pHR_COMPANY_ID, Int32 pHR_OFFICE_ID, DateTime? pSTART_DT, DateTime? pEND_DT)
        {
            string sp = "pkg_planning_common.gmt_pln_holyday_select";
            //pOption=3000=>Select All Data
            try
            {
                var obList = new List<CORE_DEPT_MODEL>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() { ParameterName = "pHR_COMPANY_ID", Value = pHR_COMPANY_ID },
                     new CommandParameter() { ParameterName = "pHR_OFFICE_ID", Value = pHR_OFFICE_ID },
                     new CommandParameter() { ParameterName = "pOption", Value = 3001 },
                     new CommandParameter() { ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output }
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    CORE_DEPT_MODEL ob = new CORE_DEPT_MODEL();
                    ob.CORE_DEPT_ID = (dr["CORE_DEPT_ID"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["CORE_DEPT_ID"]);
                    ob.DEPT_NAME = (dr["DEPT_NAME"] == DBNull.Value) ? String.Empty : Convert.ToString(dr["DEPT_NAME"]);
                    ob.items = new GMT_PLN_HOLYDAYModel().Query(pHR_COMPANY_ID, pHR_OFFICE_ID, ob.CORE_DEPT_ID, pSTART_DT, pEND_DT);
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    };

    public class GMT_PLN_HOLYDAYModel
    {
        public Int64 GMT_PLN_HOLYDAY_ID { get; set; }
        public Int64 HR_COMPANY_ID { get; set; }
        public Int64 HR_OFFICE_ID { get; set; }
        public Int64 CORE_DEPT_ID { get; set; }
        public Int64 GMT_PROD_PLN_CLNDR_ID { get; set; }
        public Int64 HR_DAY_TYPE_ID { get; set; }
        public DateTime start { get; set; }
        public DateTime end { get; set; }
        public Int64 id { get; set; }
        public string text { get; set; }
        public string backColor { get; set; }
        public string IS_SAME_FOF_OTH { get; set; }

        public string Save()
        {
            const string sp = "pkg_planning_common.gmt_pln_holyday_save";
            string jsonStr="{";
            var ob = this;
            var i = 1;
            try
             {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                     new CommandParameter() {ParameterName = "pHR_OFFICE_ID", Value = ob.HR_OFFICE_ID},
                     new CommandParameter() {ParameterName = "pCORE_DEPT_ID", Value = ob.CORE_DEPT_ID},
                     new CommandParameter() {ParameterName = "pHR_DAY_TYPE_ID", Value = ob.HR_DAY_TYPE_ID},
                      new CommandParameter() {ParameterName = "pIS_SAME_FOF_OTH", Value = ob.IS_SAME_FOF_OTH},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pSTART_DT", Value = ob.start},
                     new CommandParameter() {ParameterName = "pEND_DT", Value = ob.end},
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
                         i++ ;
                 }
                 return jsonStr;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<GMT_PLN_HOLYDAYModel> Query(Int32 pHR_COMPANY_ID, Int32 pHR_OFFICE_ID, Int32? pCORE_DEPT_ID, DateTime? pSTART_DT, DateTime? pEND_DT)
        {
            string sp = "pkg_planning_common.gmt_pln_holyday_select";
            //pOption=3000=>Select All Data
            try
            {
                var obList = new List<GMT_PLN_HOLYDAYModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = pHR_COMPANY_ID},
                     new CommandParameter() {ParameterName = "pHR_OFFICE_ID", Value = pHR_OFFICE_ID},
                     new CommandParameter() {ParameterName = "pCORE_DEPT_ID", Value = pCORE_DEPT_ID},
                     new CommandParameter() {ParameterName = "pSTART_DT", Value = pSTART_DT},
                     new CommandParameter() {ParameterName = "pEND_DT", Value = pEND_DT},

                     new CommandParameter() {ParameterName = "pOption", Value = 3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
              foreach (DataRow dr in ds.Tables[0].Rows)
               {
                            GMT_PLN_HOLYDAYModel ob = new GMT_PLN_HOLYDAYModel();
                            ob.GMT_PLN_HOLYDAY_ID = (dr["GMT_PLN_HOLYDAY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_PLN_HOLYDAY_ID"]);
                            ob.HR_COMPANY_ID = (dr["HR_COMPANY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COMPANY_ID"]);
                            ob.HR_OFFICE_ID = (dr["HR_OFFICE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_OFFICE_ID"]);
                            ob.CORE_DEPT_ID = (dr["CORE_DEPT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CORE_DEPT_ID"]);
                            ob.GMT_PROD_PLN_CLNDR_ID = (dr["GMT_PROD_PLN_CLNDR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_PROD_PLN_CLNDR_ID"]);
                            ob.HR_DAY_TYPE_ID = (dr["HR_DAY_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_DAY_TYPE_ID"]);
                            ob.start = (dr["START_DT"] == DBNull.Value) ? DateTime.Now.Date : Convert.ToDateTime(dr["START_DT"]);
                            ob.end = (dr["END_DT"] == DBNull.Value) ? DateTime.Now.Date : Convert.ToDateTime(dr["END_DT"]);
                            ob.id = ob.GMT_PLN_HOLYDAY_ID;
                            ob.text = (dr["TEXT"] == DBNull.Value) ? String.Empty : Convert.ToString(dr["TEXT"]);
                            ob.backColor = (dr["BACK_COLOR"] == DBNull.Value) ? String.Empty : Convert.ToString(dr["BACK_COLOR"]);

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