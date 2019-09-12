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
    public class SC_MAP_USR_PRD_CATModel
    {
        public Int64 SC_MAP_USR_PRD_CAT_ID { get; set; }
        public Int64 SC_USER_ID { get; set; }
        public Int64 RF_FAB_PROD_CAT_ID { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }

        public string FAB_PROD_CAT_CODE { get; set; }
        public string FAB_PROD_CAT_NAME { get; set; }
        public string PRD_PROD_CAT_PRFX { get; set; }
        public string IS_ACTIVE { get; set; }
        public string IS_LEAF { get; set; }
        public string IS_SUB_CONTR { get; set; }
        public string FAB_PROD_CAT_SNAME { get; set; }
        public string XML_MAP_D { get; set; }
        

        public string Save()
        {
            const string sp = "pkg_common.sc_map_usr_prd_cat_insert";
            string jsonStr="{";
            var ob = this;
            var i = 1;
            try
             {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pSC_MAP_USR_PRD_CAT_ID", Value = ob.SC_MAP_USR_PRD_CAT_ID},
                     new CommandParameter() {ParameterName = "pSC_USER_ID", Value = ob.SC_USER_ID},
                     new CommandParameter() {ParameterName = "pRF_FAB_PROD_CAT_ID", Value = ob.RF_FAB_PROD_CAT_ID},
                     new CommandParameter() {ParameterName = "pXML_MAP_D", Value = ob.XML_MAP_D},
                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
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

        public string Update()
        {
            const string sp = "SP_SC_MAP_USR_PRD_CAT";
            string jsonStr="{";
            var ob = this;
            var i = 1;
            try
             {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pSC_MAP_USR_PRD_CAT_ID", Value = ob.SC_MAP_USR_PRD_CAT_ID},
                     new CommandParameter() {ParameterName = "pSC_USER_ID", Value = ob.SC_USER_ID},
                     new CommandParameter() {ParameterName = "pRF_FAB_PROD_CAT_ID", Value = ob.RF_FAB_PROD_CAT_ID},
                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = ob.CREATED_BY},
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
                     i++;
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
            const string sp = "SP_SC_MAP_USR_PRD_CAT";
            string jsonStr="{";
            var ob = this;
            var i = 1;
            try
             {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pSC_MAP_USR_PRD_CAT_ID", Value = ob.SC_MAP_USR_PRD_CAT_ID},
                     new CommandParameter() {ParameterName = "pSC_USER_ID", Value = ob.SC_USER_ID},
                     new CommandParameter() {ParameterName = "pRF_FAB_PROD_CAT_ID", Value = ob.RF_FAB_PROD_CAT_ID},
                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = ob.CREATED_BY},
                     new CommandParameter() {ParameterName = "pOption", Value =4000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);

                 foreach (DataRow dr in ds.Tables["OUTPARAM"].Rows)
                 {
                    jsonStr += dr["KEY"].ToString() + ":"+ dr["VALUE"].ToString()+",";
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


        public List<SC_MAP_USR_PRD_CATModel> SelectByID(Int64? pSC_USER_ID = null)
        {
            string sp = "pkg_common.sc_map_usr_prd_cat_select";
            try
            {
                var obList = new List<SC_MAP_USR_PRD_CATModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pSC_USER_ID", Value =pSC_USER_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    SC_MAP_USR_PRD_CATModel ob = new SC_MAP_USR_PRD_CATModel();
                    ob.SC_MAP_USR_PRD_CAT_ID = (dr["SC_MAP_USR_PRD_CAT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SC_MAP_USR_PRD_CAT_ID"]);
                    ob.SC_USER_ID = (dr["SC_USER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SC_USER_ID"]);
                    ob.RF_FAB_PROD_CAT_ID = (dr["RF_FAB_PROD_CAT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_FAB_PROD_CAT_ID"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);

                    ob.FAB_PROD_CAT_CODE = (dr["FAB_PROD_CAT_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FAB_PROD_CAT_CODE"]);
                    ob.FAB_PROD_CAT_NAME = (dr["FAB_PROD_CAT_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FAB_PROD_CAT_NAME"]);
                    ob.PRD_PROD_CAT_PRFX = (dr["PRD_PROD_CAT_PRFX"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PRD_PROD_CAT_PRFX"]);
                    ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);
                    ob.IS_LEAF = (dr["IS_LEAF"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_LEAF"]);
                    ob.IS_SUB_CONTR = (dr["IS_SUB_CONTR"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_SUB_CONTR"]);
                    ob.FAB_PROD_CAT_SNAME = (dr["FAB_PROD_CAT_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FAB_PROD_CAT_SNAME"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<SC_MAP_USR_PRD_CATModel> SelectAll()
        {
            string sp = "Select_SC_MAP_USR_PRD_CAT";
            try
            {
                var obList = new List<SC_MAP_USR_PRD_CATModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pSC_MAP_USR_PRD_CAT_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
              foreach (DataRow dr in ds.Tables[0].Rows)
               {
                            SC_MAP_USR_PRD_CATModel ob = new SC_MAP_USR_PRD_CATModel();
                            ob.SC_MAP_USR_PRD_CAT_ID = (dr["SC_MAP_USR_PRD_CAT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SC_MAP_USR_PRD_CAT_ID"]);
                            ob.SC_USER_ID = (dr["SC_USER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SC_USER_ID"]);
                            ob.RF_FAB_PROD_CAT_ID = (dr["RF_FAB_PROD_CAT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_FAB_PROD_CAT_ID"]);
                            ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                            ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                            obList.Add(ob);
               }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public SC_MAP_USR_PRD_CATModel Select(long ID)
        {
            string sp = "Select_SC_MAP_USR_PRD_CAT";
            try
            {
                var ob = new SC_MAP_USR_PRD_CATModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pSC_MAP_USR_PRD_CAT_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
              foreach (DataRow dr in ds.Tables[0].Rows)
               {
                        ob.SC_MAP_USR_PRD_CAT_ID = (dr["SC_MAP_USR_PRD_CAT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SC_MAP_USR_PRD_CAT_ID"]);
                        ob.SC_USER_ID = (dr["SC_USER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SC_USER_ID"]);
                        ob.RF_FAB_PROD_CAT_ID = (dr["RF_FAB_PROD_CAT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_FAB_PROD_CAT_ID"]);
                        ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                        ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);

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