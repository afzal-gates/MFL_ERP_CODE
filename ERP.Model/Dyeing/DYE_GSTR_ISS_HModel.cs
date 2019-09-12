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
    public class DYE_GSTR_ISS_HModel
    {
        public Int64 DYE_GSTR_ISS_H_ID { get; set; }
        public Int64? DYE_GSTR_REQ_H_ID { get; set; }
        public Int64? SCM_STORE_ID { get; set; }
        public Int64? ITEM_ISS_BY { get; set; }
        public DateTime? ISS_DT { get; set; }
        public string ISS_CHALAN_NO { get; set; }
        public string VEHICLE_NO { get; set; }
        public string CARRIER_NAME { get; set; }
        public string REMARKS { get; set; }
        public Int64? RF_ACTN_STATUS_ID { get; set; }
        public DateTime? CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }
        public int pOption { get; set; }

        public string Save()
        {
            const string sp = "PKG_DYE_GFAB_REQ.dye_gstr_iss_h_save";
            string jsonStr="{";
            var ob = this;
            var i = 1;
            try
             {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_GSTR_ISS_H_ID", Value = ob.DYE_GSTR_ISS_H_ID},
                     new CommandParameter() {ParameterName = "pDYE_GSTR_REQ_H_ID", Value = ob.DYE_GSTR_REQ_H_ID},
                     new CommandParameter() {ParameterName = "pSCM_STORE_ID", Value = ob.SCM_STORE_ID},
                     new CommandParameter() {ParameterName = "pITEM_ISS_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pISS_DT", Value = ob.ISS_DT},
                     new CommandParameter() {ParameterName = "pISS_CHALAN_NO", Value = ob.ISS_CHALAN_NO},
                     new CommandParameter() {ParameterName = "pVEHICLE_NO", Value = ob.VEHICLE_NO},
                     new CommandParameter() {ParameterName = "pCARRIER_NAME", Value = ob.CARRIER_NAME},
                     new CommandParameter() {ParameterName = "pREMARKS", Value = ob.REMARKS},
                     new CommandParameter() {ParameterName = "pRF_ACTN_STATUS_ID", Value = ob.RF_ACTN_STATUS_ID},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value =HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value =  HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pOption", Value = pOption},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output},
                     new CommandParameter() {ParameterName = "OP_DYE_GSTR_ISS_H_ID", Value =0, Direction = ParameterDirection.Output}
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

        public List<DYE_GSTR_ISS_HModel> Query(int pOption, Int64 pDYE_GSTR_REQ_H_ID)
        {
            string sp = "PKG_DYE_GFAB_REQ.dye_gstr_iss_h_select";
            //pOption=3000=>Select All Data
            //pOption=3002=>Select All Data By pDYE_GSTR_REQ_H_ID
            try
            {
                var obList = new List<DYE_GSTR_ISS_HModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_GSTR_REQ_H_ID", Value = pDYE_GSTR_REQ_H_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =pOption},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
              foreach (DataRow dr in ds.Tables[0].Rows)
               {
                            DYE_GSTR_ISS_HModel ob = new DYE_GSTR_ISS_HModel();
                            ob.DYE_GSTR_ISS_H_ID = (dr["DYE_GSTR_ISS_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_GSTR_ISS_H_ID"]);
                            ob.DYE_GSTR_REQ_H_ID = (dr["DYE_GSTR_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_GSTR_REQ_H_ID"]);
                            ob.SCM_STORE_ID = (dr["SCM_STORE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_STORE_ID"]);
                            ob.ITEM_ISS_BY = (dr["ITEM_ISS_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ITEM_ISS_BY"]);
                            ob.ISS_DT = (dr["ISS_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["ISS_DT"]);
                            ob.ISS_CHALAN_NO = (dr["ISS_CHALAN_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ISS_CHALAN_NO"]);
                            ob.VEHICLE_NO = (dr["VEHICLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["VEHICLE_NO"]);
                            ob.CARRIER_NAME = (dr["CARRIER_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CARRIER_NAME"]);
                            ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                            ob.RF_ACTN_STATUS_ID = (dr["RF_ACTN_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_ACTN_STATUS_ID"]);
                            obList.Add(ob);
               }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DYE_GSTR_ISS_HModel Select(Int64 pDYE_GSTR_ISS_H_ID, int pOption)
        {
            string sp = "PKG_DYE_GFAB_REQ.dye_gstr_iss_h_select";
            //pOption=3001=>Select Single Data
            try
            {
                var ob = new DYE_GSTR_ISS_HModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_GSTR_ISS_H_ID", Value = pDYE_GSTR_ISS_H_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =pOption},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
              foreach (DataRow dr in ds.Tables[0].Rows)
               {
                        ob.DYE_GSTR_ISS_H_ID = (dr["DYE_GSTR_ISS_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_GSTR_ISS_H_ID"]);
                        ob.DYE_GSTR_REQ_H_ID = (dr["DYE_GSTR_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_GSTR_REQ_H_ID"]);
                        ob.SCM_STORE_ID = (dr["SCM_STORE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_STORE_ID"]);
                        ob.ITEM_ISS_BY = (dr["ITEM_ISS_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ITEM_ISS_BY"]);
                        ob.ISS_DT = (dr["ISS_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["ISS_DT"]);
                        ob.ISS_CHALAN_NO = (dr["ISS_CHALAN_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ISS_CHALAN_NO"]);
                        ob.VEHICLE_NO = (dr["VEHICLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["VEHICLE_NO"]);
                        ob.CARRIER_NAME = (dr["CARRIER_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CARRIER_NAME"]);
                        ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                        ob.RF_ACTN_STATUS_ID = (dr["RF_ACTN_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_ACTN_STATUS_ID"]);

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