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
    public class RF_LOCATIONModel
    {
        public Int64 RF_LOCATION_ID { get; set; }
        public string LOCATION_NAME { get; set; }



        public string Save()
        {
            const string sp = "SP_RF_LOCATION";
            string jsonStr="{";
            var ob = this;
            var i = 1;
            try
             {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pRF_LOCATION_ID", Value = ob.RF_LOCATION_ID},
                     new CommandParameter() {ParameterName = "pLOCATION_NAME", Value = ob.LOCATION_NAME},
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

        public string Update()
        {
            const string sp = "SP_RF_LOCATION";
            string jsonStr="{";
            var ob = this;
            var i = 1;
            try
             {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pRF_LOCATION_ID", Value = ob.RF_LOCATION_ID},
                     new CommandParameter() {ParameterName = "pLOCATION_NAME", Value = ob.LOCATION_NAME},
                     new CommandParameter() {ParameterName = "pOption", Value =2000},
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

        

        public List<RF_LOCATIONModel> SelectAll()
        {
            string sp = "pkg_common.rf_location_select";
            try
            {
                var obList = new List<RF_LOCATIONModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pRF_LOCATION_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
              foreach (DataRow dr in ds.Tables[0].Rows)
               {
                            RF_LOCATIONModel ob = new RF_LOCATIONModel();
                            ob.RF_LOCATION_ID = (dr["RF_LOCATION_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_LOCATION_ID"]);
                            ob.LOCATION_NAME = (dr["LOCATION_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LOCATION_NAME"]);
                            obList.Add(ob);
               }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public RF_LOCATIONModel Select(long ID)
        {
            string sp = "Select_RF_LOCATION";
            try
            {
                var ob = new RF_LOCATIONModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pRF_LOCATION_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
              foreach (DataRow dr in ds.Tables[0].Rows)
               {
                        ob.RF_LOCATION_ID = (dr["RF_LOCATION_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_LOCATION_ID"]);
                        ob.LOCATION_NAME = (dr["LOCATION_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LOCATION_NAME"]);

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