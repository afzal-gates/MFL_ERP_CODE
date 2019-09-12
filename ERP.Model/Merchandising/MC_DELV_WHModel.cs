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
    public class MC_DELV_WHModel
    {
        public Int64 MC_DELV_WH_ID { get; set; }
        public string DELV_WH_CODE { get; set; }
        public string DELV_WH_NAME { get; set; }
        public Int64 LK_WH_TYPE_ID { get; set; }
        public Int64 HR_OFFICE_ID { get; set; }
        public string IS_ACTIVE { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }

        public string Save()
        {
            const string sp = "SP_MC_DELV_WH";
            string jsonStr="{";
            var ob = this;
            var i = 1;
            try
             {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_DELV_WH_ID", Value = ob.MC_DELV_WH_ID},
                     new CommandParameter() {ParameterName = "pDELV_WH_CODE", Value = ob.DELV_WH_CODE},
                     new CommandParameter() {ParameterName = "pDELV_WH_NAME", Value = ob.DELV_WH_NAME},
                     new CommandParameter() {ParameterName = "pLK_WH_TYPE_ID", Value = ob.LK_WH_TYPE_ID},
                     new CommandParameter() {ParameterName = "pHR_OFFICE_ID", Value = ob.HR_OFFICE_ID},
                     new CommandParameter() {ParameterName = "pIS_ACTIVE", Value = ob.IS_ACTIVE},
                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = ob.CREATED_BY},
                     new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = ob.LAST_UPDATED_BY},
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

        public List<MC_DELV_WHModel> SelectAll()
        {
            string sp = "pkg_acc_booking.mc_delv_wh_select";
            try
            {
                var obList = new List<MC_DELV_WHModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
              foreach (DataRow dr in ds.Tables[0].Rows)
               {
                            MC_DELV_WHModel ob = new MC_DELV_WHModel();
                            ob.MC_DELV_WH_ID = (dr["MC_DELV_WH_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_DELV_WH_ID"]);
                            ob.DELV_WH_CODE = (dr["DELV_WH_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DELV_WH_CODE"]);
                            ob.DELV_WH_NAME = (dr["DELV_WH_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DELV_WH_NAME"]);
                            ob.LK_WH_TYPE_ID = (dr["LK_WH_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_WH_TYPE_ID"]);
                            ob.HR_OFFICE_ID = (dr["HR_OFFICE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_OFFICE_ID"]);
                            obList.Add(ob);
               }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public MC_DELV_WHModel Select(long ID)
        {
            string sp = "Select_MC_DELV_WH";
            try
            {
                var ob = new MC_DELV_WHModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_DELV_WH_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
              foreach (DataRow dr in ds.Tables[0].Rows)
               {
                        ob.MC_DELV_WH_ID = (dr["MC_DELV_WH_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_DELV_WH_ID"]);
                        ob.DELV_WH_CODE = (dr["DELV_WH_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DELV_WH_CODE"]);
                        ob.DELV_WH_NAME = (dr["DELV_WH_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DELV_WH_NAME"]);
                        ob.LK_WH_TYPE_ID = (dr["LK_WH_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_WH_TYPE_ID"]);
                        ob.HR_OFFICE_ID = (dr["HR_OFFICE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_OFFICE_ID"]);
                        ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);
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