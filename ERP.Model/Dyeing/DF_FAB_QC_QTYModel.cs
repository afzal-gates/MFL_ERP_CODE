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
    public class DF_FAB_QC_QTYModel
    {
        public Int64 DF_FAB_QC_QTY_ID { get; set; }
        public Int64 DF_FAB_QC_RPT_ID { get; set; }
        public Int64 LK_FBR_GRP_ID { get; set; }
        public Int64 RF_FAB_TYPE_ID { get; set; }
        public Int64 RF_FIB_COMP_ID { get; set; }
        public Int64 LK_DIA_TYPE_ID { get; set; }
        public string YARN_SPEC { get; set; }
        public Int64 BATCH_QTY { get; set; }
        public Decimal FINIS_QTY { get; set; }
        public Decimal RE_PROC_QTY { get; set; }
        public Int64 QTY_MOU_ID { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }

        public string Save()
        {
            const string sp = "SP_DF_FAB_QC_QTY";
            string jsonStr="{";
            var ob = this;
            var i = 1;
            try
             {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_FAB_QC_QTY_ID", Value = ob.DF_FAB_QC_QTY_ID},
                     new CommandParameter() {ParameterName = "pDF_FAB_QC_RPT_ID", Value = ob.DF_FAB_QC_RPT_ID},
                     new CommandParameter() {ParameterName = "pLK_FBR_GRP_ID", Value = ob.LK_FBR_GRP_ID},
                     new CommandParameter() {ParameterName = "pRF_FAB_TYPE_ID", Value = ob.RF_FAB_TYPE_ID},
                     new CommandParameter() {ParameterName = "pRF_FIB_COMP_ID", Value = ob.RF_FIB_COMP_ID},
                     new CommandParameter() {ParameterName = "pLK_DIA_TYPE_ID", Value = ob.LK_DIA_TYPE_ID},
                     new CommandParameter() {ParameterName = "pYARN_SPEC", Value = ob.YARN_SPEC},
                     new CommandParameter() {ParameterName = "pBATCH_QTY", Value = ob.BATCH_QTY},
                     new CommandParameter() {ParameterName = "pFINIS_QTY", Value = ob.FINIS_QTY},
                     new CommandParameter() {ParameterName = "pRE_PROC_QTY", Value = ob.RE_PROC_QTY},
                     new CommandParameter() {ParameterName = "pQTY_MOU_ID", Value = ob.QTY_MOU_ID},
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
            const string sp = "SP_DF_FAB_QC_QTY";
            string jsonStr="{";
            var ob = this;
            var i = 1;
            try
             {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_FAB_QC_QTY_ID", Value = ob.DF_FAB_QC_QTY_ID},
                     new CommandParameter() {ParameterName = "pDF_FAB_QC_RPT_ID", Value = ob.DF_FAB_QC_RPT_ID},
                     new CommandParameter() {ParameterName = "pLK_FBR_GRP_ID", Value = ob.LK_FBR_GRP_ID},
                     new CommandParameter() {ParameterName = "pRF_FAB_TYPE_ID", Value = ob.RF_FAB_TYPE_ID},
                     new CommandParameter() {ParameterName = "pRF_FIB_COMP_ID", Value = ob.RF_FIB_COMP_ID},
                     new CommandParameter() {ParameterName = "pLK_DIA_TYPE_ID", Value = ob.LK_DIA_TYPE_ID},
                     new CommandParameter() {ParameterName = "pYARN_SPEC", Value = ob.YARN_SPEC},
                     new CommandParameter() {ParameterName = "pBATCH_QTY", Value = ob.BATCH_QTY},
                     new CommandParameter() {ParameterName = "pFINIS_QTY", Value = ob.FINIS_QTY},
                     new CommandParameter() {ParameterName = "pRE_PROC_QTY", Value = ob.RE_PROC_QTY},
                     new CommandParameter() {ParameterName = "pQTY_MOU_ID", Value = ob.QTY_MOU_ID},
                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = ob.CREATED_BY},
                     new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = ob.LAST_UPDATED_BY},
                     new CommandParameter() {ParameterName = "pOption", Value =2000},
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

        public string Delete()
        {
            const string sp = "SP_DF_FAB_QC_QTY";
            string jsonStr="{";
            var ob = this;
            var i = 1;
            try
             {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_FAB_QC_QTY_ID", Value = ob.DF_FAB_QC_QTY_ID},
                     new CommandParameter() {ParameterName = "pDF_FAB_QC_RPT_ID", Value = ob.DF_FAB_QC_RPT_ID},
                     new CommandParameter() {ParameterName = "pLK_FBR_GRP_ID", Value = ob.LK_FBR_GRP_ID},
                     new CommandParameter() {ParameterName = "pRF_FAB_TYPE_ID", Value = ob.RF_FAB_TYPE_ID},
                     new CommandParameter() {ParameterName = "pRF_FIB_COMP_ID", Value = ob.RF_FIB_COMP_ID},
                     new CommandParameter() {ParameterName = "pLK_DIA_TYPE_ID", Value = ob.LK_DIA_TYPE_ID},
                     new CommandParameter() {ParameterName = "pYARN_SPEC", Value = ob.YARN_SPEC},
                     new CommandParameter() {ParameterName = "pBATCH_QTY", Value = ob.BATCH_QTY},
                     new CommandParameter() {ParameterName = "pFINIS_QTY", Value = ob.FINIS_QTY},
                     new CommandParameter() {ParameterName = "pRE_PROC_QTY", Value = ob.RE_PROC_QTY},
                     new CommandParameter() {ParameterName = "pQTY_MOU_ID", Value = ob.QTY_MOU_ID},
                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = ob.CREATED_BY},
                     new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = ob.LAST_UPDATED_BY},
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

        public List<DF_FAB_QC_QTYModel> SelectAll()
        {
            string sp = "Select_DF_FAB_QC_QTY";
            try
            {
                var obList = new List<DF_FAB_QC_QTYModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_FAB_QC_QTY_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
              foreach (DataRow dr in ds.Tables[0].Rows)
               {
                            DF_FAB_QC_QTYModel ob = new DF_FAB_QC_QTYModel();
                            ob.DF_FAB_QC_QTY_ID = (dr["DF_FAB_QC_QTY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_FAB_QC_QTY_ID"]);
                            ob.DF_FAB_QC_RPT_ID = (dr["DF_FAB_QC_RPT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_FAB_QC_RPT_ID"]);
                            ob.LK_FBR_GRP_ID = (dr["LK_FBR_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_FBR_GRP_ID"]);
                            ob.RF_FAB_TYPE_ID = (dr["RF_FAB_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_FAB_TYPE_ID"]);
                            ob.RF_FIB_COMP_ID = (dr["RF_FIB_COMP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_FIB_COMP_ID"]);
                            ob.LK_DIA_TYPE_ID = (dr["LK_DIA_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_DIA_TYPE_ID"]);
                            ob.YARN_SPEC = (dr["YARN_SPEC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["YARN_SPEC"]);
                            ob.BATCH_QTY = (dr["BATCH_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["BATCH_QTY"]);
                            ob.FINIS_QTY = (dr["FINIS_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["FINIS_QTY"]);
                            ob.RE_PROC_QTY = (dr["RE_PROC_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RE_PROC_QTY"]);
                            ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);
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

        public DF_FAB_QC_QTYModel Select(long ID)
        {
            string sp = "Select_DF_FAB_QC_QTY";
            try
            {
                var ob = new DF_FAB_QC_QTYModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_FAB_QC_QTY_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
              foreach (DataRow dr in ds.Tables[0].Rows)
               {
                        ob.DF_FAB_QC_QTY_ID = (dr["DF_FAB_QC_QTY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_FAB_QC_QTY_ID"]);
                        ob.DF_FAB_QC_RPT_ID = (dr["DF_FAB_QC_RPT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_FAB_QC_RPT_ID"]);
                        ob.LK_FBR_GRP_ID = (dr["LK_FBR_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_FBR_GRP_ID"]);
                        ob.RF_FAB_TYPE_ID = (dr["RF_FAB_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_FAB_TYPE_ID"]);
                        ob.RF_FIB_COMP_ID = (dr["RF_FIB_COMP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_FIB_COMP_ID"]);
                        ob.LK_DIA_TYPE_ID = (dr["LK_DIA_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_DIA_TYPE_ID"]);
                        ob.YARN_SPEC = (dr["YARN_SPEC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["YARN_SPEC"]);
                        ob.BATCH_QTY = (dr["BATCH_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["BATCH_QTY"]);
                        ob.FINIS_QTY = (dr["FINIS_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["FINIS_QTY"]);
                        ob.RE_PROC_QTY = (dr["RE_PROC_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RE_PROC_QTY"]);
                        ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);
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