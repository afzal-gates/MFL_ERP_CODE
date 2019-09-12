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
    public class SCM_PURC_REQ_D_YRN1Model
    {
        public Int64 SCM_PURC_REQ_D_YRN1_ID { get; set; }
        public Int64 SCM_PURC_REQ_D_YRN_ID { get; set; }
        public Int64 CONF_SUPPLIER_ID { get; set; }
        public Int64 CONF_BRAND_ID { get; set; }
        public Decimal CONF_RATE { get; set; }
        public Decimal CONF_QTY { get; set; }
        public string REMARKS { get; set; }
        public string IS_PO_DONE { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }

        public string SUP_TRD_NAME_EN { get; set; }
        public string SUP_SNAME { get; set; }
        public Int64 SCM_SUPPLIER_ID { get; set; }

        public string SUP_OFFICE_NAME { get; set; }
        public string ADDRESS_EN { get; set; }
        public string CITY_NAME { get; set; }
        public string POST_CODE { get; set; }
        public string PO_BOX_NO { get; set; }
        public string WORK_PHONE { get; set; }
        public string MOB_PHONE { get; set; }
        public string CP_NAME_EN { get; set; }
        public string CP_DESIG { get; set; }
        public string CP_WORK_PHONE { get; set; }
        public string CP_MOB_PHONE { get; set; }


        public string Save()
        {
            const string sp = "SP_SCM_PURC_REQ_D_YRN1";
            string jsonStr="{";
            var ob = this;
            var i = 1;
            try
             {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pSCM_PURC_REQ_D_YRN1_ID", Value = ob.SCM_PURC_REQ_D_YRN1_ID},
                     new CommandParameter() {ParameterName = "pSCM_PURC_REQ_D_YRN_ID", Value = ob.SCM_PURC_REQ_D_YRN_ID},
                     new CommandParameter() {ParameterName = "pCONF_SUPPLIER_ID", Value = ob.CONF_SUPPLIER_ID},
                     new CommandParameter() {ParameterName = "pCONF_BRAND_ID", Value = ob.CONF_BRAND_ID},
                     new CommandParameter() {ParameterName = "pCONF_RATE", Value = ob.CONF_RATE},
                     new CommandParameter() {ParameterName = "pCONF_QTY", Value = ob.CONF_QTY},
                     new CommandParameter() {ParameterName = "pREMARKS", Value = ob.REMARKS},
                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = ob.CREATED_BY},
                     new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = ob.LAST_UPDATED_BY},
                     new CommandParameter() {ParameterName = "pOption", Value =1000},
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

        public string Update()
        {
            const string sp = "SP_SCM_PURC_REQ_D_YRN1";
            string jsonStr="{";
            var ob = this;
            var i = 1;
            try
             {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pSCM_PURC_REQ_D_YRN1_ID", Value = ob.SCM_PURC_REQ_D_YRN1_ID},
                     new CommandParameter() {ParameterName = "pSCM_PURC_REQ_D_YRN_ID", Value = ob.SCM_PURC_REQ_D_YRN_ID},
                     new CommandParameter() {ParameterName = "pCONF_SUPPLIER_ID", Value = ob.CONF_SUPPLIER_ID},
                     new CommandParameter() {ParameterName = "pCONF_BRAND_ID", Value = ob.CONF_BRAND_ID},
                     new CommandParameter() {ParameterName = "pCONF_RATE", Value = ob.CONF_RATE},
                     new CommandParameter() {ParameterName = "pCONF_QTY", Value = ob.CONF_QTY},
                     new CommandParameter() {ParameterName = "pREMARKS", Value = ob.REMARKS},
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
            const string sp = "SP_SCM_PURC_REQ_D_YRN1";
            string jsonStr="{";
            var ob = this;
            var i = 1;
            try
             {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pSCM_PURC_REQ_D_YRN1_ID", Value = ob.SCM_PURC_REQ_D_YRN1_ID},
                     new CommandParameter() {ParameterName = "pSCM_PURC_REQ_D_YRN_ID", Value = ob.SCM_PURC_REQ_D_YRN_ID},
                     new CommandParameter() {ParameterName = "pCONF_SUPPLIER_ID", Value = ob.CONF_SUPPLIER_ID},
                     new CommandParameter() {ParameterName = "pCONF_BRAND_ID", Value = ob.CONF_BRAND_ID},
                     new CommandParameter() {ParameterName = "pCONF_RATE", Value = ob.CONF_RATE},
                     new CommandParameter() {ParameterName = "pCONF_QTY", Value = ob.CONF_QTY},
                     new CommandParameter() {ParameterName = "pREMARKS", Value = ob.REMARKS},
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

        public List<SCM_PURC_REQ_D_YRN1Model> SelectAll()
        {
            string sp = "Select_SCM_PURC_REQ_D_YRN1";
            try
            {
                var obList = new List<SCM_PURC_REQ_D_YRN1Model>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pSCM_PURC_REQ_D_YRN1_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
              foreach (DataRow dr in ds.Tables[0].Rows)
               {
                            SCM_PURC_REQ_D_YRN1Model ob = new SCM_PURC_REQ_D_YRN1Model();
                            ob.SCM_PURC_REQ_D_YRN1_ID = (dr["SCM_PURC_REQ_D_YRN1_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_PURC_REQ_D_YRN1_ID"]);
                            ob.SCM_PURC_REQ_D_YRN_ID = (dr["SCM_PURC_REQ_D_YRN_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_PURC_REQ_D_YRN_ID"]);
                            ob.CONF_SUPPLIER_ID = (dr["CONF_SUPPLIER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CONF_SUPPLIER_ID"]);
                            ob.CONF_BRAND_ID = (dr["CONF_BRAND_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CONF_BRAND_ID"]);
                            ob.CONF_RATE = (dr["CONF_RATE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["CONF_RATE"]);
                            ob.CONF_QTY = (dr["CONF_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["CONF_QTY"]);
                            ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
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

        public SCM_PURC_REQ_D_YRN1Model Select(long ID)
        {
            string sp = "Select_SCM_PURC_REQ_D_YRN1";
            try
            {
                var ob = new SCM_PURC_REQ_D_YRN1Model();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pSCM_PURC_REQ_D_YRN1_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
              foreach (DataRow dr in ds.Tables[0].Rows)
               {
                        ob.SCM_PURC_REQ_D_YRN1_ID = (dr["SCM_PURC_REQ_D_YRN1_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_PURC_REQ_D_YRN1_ID"]);
                        ob.SCM_PURC_REQ_D_YRN_ID = (dr["SCM_PURC_REQ_D_YRN_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_PURC_REQ_D_YRN_ID"]);
                        ob.CONF_SUPPLIER_ID = (dr["CONF_SUPPLIER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CONF_SUPPLIER_ID"]);
                        ob.CONF_BRAND_ID = (dr["CONF_BRAND_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CONF_BRAND_ID"]);
                        ob.CONF_RATE = (dr["CONF_RATE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["CONF_RATE"]);
                        ob.CONF_QTY = (dr["CONF_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["CONF_QTY"]);
                        ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
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


        public List<SCM_PURC_REQ_D_YRN1Model> SelectPendingSupplier()
        {
            string sp = "PKG_SCM_PURCHASE.SCM_PURC_REQ_D_YRN_Select";
            try
            {
                var obList = new List<SCM_PURC_REQ_D_YRN1Model>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pSCM_PURC_REQ_D_YRN_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    SCM_PURC_REQ_D_YRN1Model ob = new SCM_PURC_REQ_D_YRN1Model();
                    ob.SCM_SUPPLIER_ID = (dr["SCM_SUPPLIER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_SUPPLIER_ID"]);
                    ob.SUP_TRD_NAME_EN = (dr["SUP_TRD_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SUP_TRD_NAME_EN"]);

                    ob.SUP_SNAME = (dr["SUP_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SUP_SNAME"]);
                    ob.IS_LOCAL = (dr["IS_LOCAL"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_LOCAL"]);
                    
                    ob.SUP_OFFICE_NAME = (dr["SUP_OFFICE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SUP_OFFICE_NAME"]);
                    ob.ADDRESS_EN = (dr["ADDRESS_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ADDRESS_EN"]);
                    ob.CITY_NAME = (dr["CITY_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CITY_NAME"]);
                    ob.POST_CODE = (dr["POST_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["POST_CODE"]);
                    ob.PO_BOX_NO = (dr["PO_BOX_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PO_BOX_NO"]);
                    ob.WORK_PHONE = (dr["WORK_PHONE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["WORK_PHONE"]);
                    ob.MOB_PHONE = (dr["MOB_PHONE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MOB_PHONE"]);
                    ob.CP_NAME_EN = (dr["CP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CP_NAME_EN"]);
                    ob.CP_DESIG = (dr["CP_DESIG"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CP_DESIG"]);
                    ob.CP_WORK_PHONE = (dr["CP_WORK_PHONE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CP_WORK_PHONE"]);
                    ob.CP_MOB_PHONE = (dr["CP_MOB_PHONE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CP_MOB_PHONE"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string IS_LOCAL { get; set; }
    }
}