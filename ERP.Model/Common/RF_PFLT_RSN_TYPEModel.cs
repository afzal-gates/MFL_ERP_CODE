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
    public class RF_PFLT_RSN_TYPEModel
    {
        public Int64 RF_PFLT_RSN_TYPE_ID { get; set; }
        public string RSN_TYPE_CODE { get; set; }
        public string RSN_TYPE_NAME_EN { get; set; }
        public string RSN_TYPE_NAME_BN { get; set; }
        public string RSN_TYPE_DESC { get; set; }
        public string IS_ACTIVE { get; set; }

        public string Save()
        {
            const string sp = "pkg_common.rf_pflt_rsn_type_insert";
            string jsonStr="{";
            var ob = this;
            var i = 1;
            try
             {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pRF_PFLT_RSN_TYPE_ID", Value = ob.RF_PFLT_RSN_TYPE_ID},
                     new CommandParameter() {ParameterName = "pRSN_TYPE_CODE", Value = ob.RSN_TYPE_CODE},
                     new CommandParameter() {ParameterName = "pRSN_TYPE_NAME_EN", Value = ob.RSN_TYPE_NAME_EN},
                     new CommandParameter() {ParameterName = "pRSN_TYPE_NAME_BN", Value = ob.RSN_TYPE_NAME_BN ?? ob.RSN_TYPE_NAME_EN},
                     new CommandParameter() {ParameterName = "pRSN_TYPE_DESC", Value = ob.RSN_TYPE_DESC ?? ob.RSN_TYPE_NAME_EN},
                     new CommandParameter() {ParameterName = "pIS_ACTIVE", Value = ob.IS_ACTIVE??"Y"},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pOption", Value =1000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output},
                     new CommandParameter() {ParameterName = "OP_RF_PFLT_RSN_TYPE_ID", Value =0, Direction = ParameterDirection.Output}
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

        public List<RF_PFLT_RSN_TYPEModel> SelectAll()
        {
            string sp = "pkg_common.rf_pflt_rsn_type_select";
            try
            {
                var obList = new List<RF_PFLT_RSN_TYPEModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pRF_PFLT_RSN_TYPE_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);

              obList.Add( new RF_PFLT_RSN_TYPEModel(){
                  RF_PFLT_RSN_TYPE_ID =-100,
                  RSN_TYPE_NAME_EN ="--New Reason--",
                  RSN_TYPE_DESC=""
              });


              foreach (DataRow dr in ds.Tables[0].Rows)
               {
                            RF_PFLT_RSN_TYPEModel ob = new RF_PFLT_RSN_TYPEModel();
                            ob.RF_PFLT_RSN_TYPE_ID = (dr["RF_PFLT_RSN_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_PFLT_RSN_TYPE_ID"]);
                            ob.RSN_TYPE_NAME_EN = (dr["RSN_TYPE_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["RSN_TYPE_NAME_EN"]);
                            ob.RSN_TYPE_DESC = (dr["RSN_TYPE_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["RSN_TYPE_DESC"]);
                            obList.Add(ob);
               }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public RF_PFLT_RSN_TYPEModel Select(long ID)
        {
            string sp = "Select_RF_PFLT_RSN_TYPE";
            try
            {
                var ob = new RF_PFLT_RSN_TYPEModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pRF_PFLT_RSN_TYPE_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
              foreach (DataRow dr in ds.Tables[0].Rows)
               {
                        ob.RF_PFLT_RSN_TYPE_ID = (dr["RF_PFLT_RSN_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_PFLT_RSN_TYPE_ID"]);
                        ob.RSN_TYPE_CODE = (dr["RSN_TYPE_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["RSN_TYPE_CODE"]);
                        ob.RSN_TYPE_NAME_EN = (dr["RSN_TYPE_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["RSN_TYPE_NAME_EN"]);
                        ob.RSN_TYPE_NAME_BN = (dr["RSN_TYPE_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["RSN_TYPE_NAME_BN"]);
                        ob.RSN_TYPE_DESC = (dr["RSN_TYPE_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["RSN_TYPE_DESC"]);
                        ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);
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