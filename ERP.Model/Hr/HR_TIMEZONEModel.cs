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
    public class HR_TIMEZONEModel
    {
        public Int64 HR_TIMEZONE_ID { get; set; }
        public Decimal TIMEZONE_VALUE { get; set; }
        public string TIMEZONE_TEXT { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }

        public string Save()
        {
            const string sp = "SP_HR_TIMEZONE";

            var ob = this;
            try
             {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pHR_TIMEZONE_ID", Value = ob.HR_TIMEZONE_ID},
                     new CommandParameter() {ParameterName = "pTIMEZONE_VALUE", Value = ob.TIMEZONE_VALUE},
                     new CommandParameter() {ParameterName = "pTIMEZONE_TEXT", Value = ob.TIMEZONE_TEXT},
                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = ob.CREATED_BY},
                     new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = ob.LAST_UPDATED_BY},
                     new CommandParameter() {ParameterName = "pOption", Value =1000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                 string jsonString = "{";
                 foreach (DataRow dr in ds.Tables["OUTPARAM"].Rows)
                 {
                    jsonString += dr["KEY"].ToString() + ":"+ dr["VALUE"].ToString()+",";
                 }
                    jsonString+="}";
                 return jsonString;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string Update()
        {
            const string sp = "SP_HR_TIMEZONE";
            string vMsg="";
            var ob = this;
            try
             {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pHR_TIMEZONE_ID", Value = ob.HR_TIMEZONE_ID},
                     new CommandParameter() {ParameterName = "pTIMEZONE_VALUE", Value = ob.TIMEZONE_VALUE},
                     new CommandParameter() {ParameterName = "pTIMEZONE_TEXT", Value = ob.TIMEZONE_TEXT},
                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = ob.CREATED_BY},
                     new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = ob.LAST_UPDATED_BY},
                     new CommandParameter() {ParameterName = "pOption", Value =2000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                 string jsonString = "{";
                 foreach (DataRow dr in ds.Tables["OUTPARAM"].Rows)
                 {
                    jsonString += dr["KEY"].ToString() + ":"+ dr["VALUE"].ToString()+",";
                 }
                    jsonString+="}";
                 return jsonString;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string Delete()
        {
            const string sp = "SP_HR_TIMEZONE";
            string vMsg="";
            var ob = this;
            try
             {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pHR_TIMEZONE_ID", Value = ob.HR_TIMEZONE_ID},
                     new CommandParameter() {ParameterName = "pTIMEZONE_VALUE", Value = ob.TIMEZONE_VALUE},
                     new CommandParameter() {ParameterName = "pTIMEZONE_TEXT", Value = ob.TIMEZONE_TEXT},
                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = ob.CREATED_BY},
                     new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = ob.LAST_UPDATED_BY},
                     new CommandParameter() {ParameterName = "pOption", Value =4000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                 string jsonString = "{";
                 foreach (DataRow dr in ds.Tables["OUTPARAM"].Rows)
                 {
                    jsonString += dr["KEY"].ToString() + ":"+ dr["VALUE"].ToString()+",";
                 }
                    jsonString+="}";
                 return jsonString;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<HR_TIMEZONEModel> SelectAll()
        {
            string sp = "Select_HR_TIMEZONE";
            try
            {
                var obList = new List<HR_TIMEZONEModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pHR_TIMEZONE_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
              foreach (DataRow dr in ds.Tables[0].Rows)
               {
                            HR_TIMEZONEModel ob = new HR_TIMEZONEModel();
                            ob.HR_TIMEZONE_ID = (dr["HR_TIMEZONE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_TIMEZONE_ID"]);
                            ob.TIMEZONE_VALUE = (dr["TIMEZONE_VALUE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["TIMEZONE_VALUE"]);
                            ob.TIMEZONE_TEXT = (dr["TIMEZONE_TEXT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TIMEZONE_TEXT"]);
                            ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                            ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                            ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                            ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);
                            obList.Add(ob);
               }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public HR_TIMEZONEModel Select(long ID)
        {
            string sp = "Select_HR_TIMEZONE";
            try
            {
                var ob = new HR_TIMEZONEModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pHR_TIMEZONE_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
              foreach (DataRow dr in ds.Tables[0].Rows)
               {
                        ob.HR_TIMEZONE_ID = (dr["HR_TIMEZONE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_TIMEZONE_ID"]);
                        ob.TIMEZONE_VALUE = (dr["TIMEZONE_VALUE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["TIMEZONE_VALUE"]);
                        ob.TIMEZONE_TEXT = (dr["TIMEZONE_TEXT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TIMEZONE_TEXT"]);
                        ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                        ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                        ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                        ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);

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