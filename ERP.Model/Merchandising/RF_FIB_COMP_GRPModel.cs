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
    public class RF_FIB_COMP_GRPModel
    {
        public Int64 RF_FIB_COMP_GRP_ID { get; set; }
        public string RF_FIB_COMP_GRP_NAME { get; set; }
        public Int64 DISPLAY_ORDER { get; set; }
        public string IS_BLEND { get; set; }
        public string IS_ELA_MXD { get; set; }
        public string IS_ACTIVE { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }
        public string FIB_TYPE_ID_LST { get; set; }

        public string Save()
        {
            const string sp = "SP_RF_FIB_COMP_GRP";
            string jsonStr="{";
            var ob = this;
            var i = 1;
            try
             {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pRF_FIB_COMP_GRP_ID", Value = ob.RF_FIB_COMP_GRP_ID},
                     new CommandParameter() {ParameterName = "pRF_FIB_COMP_GRP_NAME", Value = ob.RF_FIB_COMP_GRP_NAME},
                     new CommandParameter() {ParameterName = "pDISPLAY_ORDER", Value = ob.DISPLAY_ORDER},
                     new CommandParameter() {ParameterName = "pIS_BLEND", Value = ob.IS_BLEND},
                     new CommandParameter() {ParameterName = "pIS_ELA_MXD", Value = ob.IS_ELA_MXD},
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
            const string sp = "SP_RF_FIB_COMP_GRP";
            string jsonStr="{";
            var ob = this;
            var i = 1;
            try
             {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pRF_FIB_COMP_GRP_ID", Value = ob.RF_FIB_COMP_GRP_ID},
                     new CommandParameter() {ParameterName = "pRF_FIB_COMP_GRP_NAME", Value = ob.RF_FIB_COMP_GRP_NAME},
                     new CommandParameter() {ParameterName = "pDISPLAY_ORDER", Value = ob.DISPLAY_ORDER},
                     new CommandParameter() {ParameterName = "pIS_BLEND", Value = ob.IS_BLEND},
                     new CommandParameter() {ParameterName = "pIS_ELA_MXD", Value = ob.IS_ELA_MXD},
                     new CommandParameter() {ParameterName = "pIS_ACTIVE", Value = ob.IS_ACTIVE},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
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

        public string Delete()
        {
            const string sp = "SP_RF_FIB_COMP_GRP";
            string jsonStr="{";
            var ob = this;
            var i = 1;
            try
             {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pRF_FIB_COMP_GRP_ID", Value = ob.RF_FIB_COMP_GRP_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =4000},
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

        public List<RF_FIB_COMP_GRPModel> SelectAll(string Short)
        {
            string sp = "pkg_merchandising.rf_fib_comp_grp_select";
            try
            {
                var obList = new List<RF_FIB_COMP_GRPModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pRF_FIB_COMP_GRP_ID", Value =0},
                     new CommandParameter() {ParameterName = "pShort", Value =Short},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
              foreach (DataRow dr in ds.Tables[0].Rows)
               {
                            RF_FIB_COMP_GRPModel ob = new RF_FIB_COMP_GRPModel();
                            ob.RF_FIB_COMP_GRP_ID = (dr["RF_FIB_COMP_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_FIB_COMP_GRP_ID"]);
                            ob.RF_FIB_COMP_GRP_NAME = (dr["RF_FIB_COMP_GRP_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["RF_FIB_COMP_GRP_NAME"]);
                            ob.DISPLAY_ORDER = (dr["DISPLAY_ORDER"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DISPLAY_ORDER"]);
                            ob.IS_BLEND = (dr["IS_BLEND"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_BLEND"]);
                            ob.IS_ELA_MXD = (dr["IS_ELA_MXD"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ELA_MXD"]);
                            ob.FIB_TYPE_ID_LST = (dr["FIB_TYPE_ID_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FIB_TYPE_ID_LST"]);
                            obList.Add(ob);
               }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public RF_FIB_COMP_GRPModel Select(long ID)
        {
            string sp = "Select_RF_FIB_COMP_GRP";
            try
            {
                var ob = new RF_FIB_COMP_GRPModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pRF_FIB_COMP_GRP_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
              foreach (DataRow dr in ds.Tables[0].Rows)
               {
                        ob.RF_FIB_COMP_GRP_ID = (dr["RF_FIB_COMP_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_FIB_COMP_GRP_ID"]);
                        ob.RF_FIB_COMP_GRP_NAME = (dr["RF_FIB_COMP_GRP_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["RF_FIB_COMP_GRP_NAME"]);
                        ob.DISPLAY_ORDER = (dr["DISPLAY_ORDER"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DISPLAY_ORDER"]);
                        ob.IS_BLEND = (dr["IS_BLEND"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_BLEND"]);
                        ob.IS_ELA_MXD = (dr["IS_ELA_MXD"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ELA_MXD"]);
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