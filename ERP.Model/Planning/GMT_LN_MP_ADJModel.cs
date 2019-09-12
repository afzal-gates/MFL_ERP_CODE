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
    public class GMT_LN_MP_ADJModel
    {

        public Int64 HR_PROD_FLR_ID { get; set; }
        public Int64 HR_PROD_LINE_ID { get; set; }
        public string LINE_NO { get; set; }
        public string LINE_CODE { get; set; }
        public string LINE_DESC_EN { get; set; }
        public string FLOOR_CODE { get; set; }
        public string FLOOR_NO { get; set; }
        public string FLOOR_DESC_EN { get; set; }

        public DateTime? PROD_DT { get; set; }
        public string XML { get; set; }

        public long USE_OPERATOR { get; set; }
        public long USE_HELPER { get; set; }

        public Int64 GMT_LN_MP_ADJ_ID { get; set; }
        public Int64? H9_MP_ADD { get; set; }
        public Int64? H10_MP_ADD { get; set; }
        public Int64? H11_MP_ADD { get; set; }
        public Int64? H12_MP_ADD { get; set; }
        public Int64? H13_MP_ADD { get; set; }
        public Int64? H14_MP_ADD { get; set; }
        public Int64? H15_MP_ADD { get; set; }
        public Int64? H15_P_MP_ADD { get; set; }
        public Int64? H15_P_WH { get; set; }



        public string Save()
         // pOption=1000=>General Form Save
        {
            const string sp = "pkg_hourly_prod.gmt_ln_mp_adj_save";
            string jsonStr="{";
            var ob = this;
            var i = 1;
            try
             {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pXML", Value = ob.XML},
                     new CommandParameter() {ParameterName = "pPROD_DT", Value = ob.PROD_DT},

                     new CommandParameter() {ParameterName = "pOption", Value =1000},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
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
        public List<GMT_LN_MP_ADJModel> Query(int pOption, DateTime pPROD_DT, string pHR_PROD_LINE_LST, string pHR_PROD_FLR_LST)
        {
            string sp = "pkg_hourly_prod.gmt_ln_mp_adj_select";
            //pOption=3000=>Select All Data
            try
            {
                var obList = new List<GMT_LN_MP_ADJModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() { ParameterName = "pPROD_DT", Value =  pPROD_DT},
                     new CommandParameter() { ParameterName = "pHR_PROD_LINE_LST", Value =  pHR_PROD_LINE_LST},
                     new CommandParameter() { ParameterName = "pHR_PROD_FLR_LST", Value =  pHR_PROD_FLR_LST},
                     new CommandParameter() { ParameterName = "pOption", Value = pOption },
                     new CommandParameter() { ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);

              foreach (DataRow dr in ds.Tables[0].Rows)
               {
                            GMT_LN_MP_ADJModel ob = new GMT_LN_MP_ADJModel();
                            ob.GMT_LN_MP_ADJ_ID = (dr["GMT_LN_MP_ADJ_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_LN_MP_ADJ_ID"]);
                            ob.HR_PROD_LINE_ID = (dr["HR_PROD_LINE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_PROD_LINE_ID"]);

                            ob.LINE_NO = (dr["LINE_NO"] == DBNull.Value) ? String.Empty : Convert.ToString(dr["LINE_NO"]);
                            ob.LINE_CODE = (dr["LINE_CODE"] == DBNull.Value) ? String.Empty : Convert.ToString(dr["LINE_CODE"]);
                            ob.LINE_DESC_EN = (dr["LINE_DESC_EN"] == DBNull.Value) ? String.Empty : Convert.ToString(dr["LINE_DESC_EN"]);

                            ob.FLOOR_CODE = (dr["FLOOR_CODE"] == DBNull.Value) ? String.Empty : Convert.ToString(dr["FLOOR_CODE"]);

                            ob.FLOOR_NO = (dr["FLOOR_NO"] == DBNull.Value) ? String.Empty : Convert.ToString(dr["FLOOR_NO"]);
                            ob.FLOOR_DESC_EN = (dr["FLOOR_DESC_EN"] == DBNull.Value) ? String.Empty : Convert.ToString(dr["FLOOR_DESC_EN"]);

                            ob.USE_OPERATOR = (dr["USE_OPERATOR"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["USE_OPERATOR"]);
                            ob.USE_HELPER = (dr["USE_HELPER"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["USE_HELPER"]);

     
                  
                            if (dr["H9_MP_ADD"] != DBNull.Value)
                                ob.H9_MP_ADD = Convert.ToInt64(dr["H9_MP_ADD"]);

                            if (dr["H10_MP_ADD"] != DBNull.Value)
                                ob.H10_MP_ADD = Convert.ToInt64(dr["H10_MP_ADD"]);

                            if (dr["H11_MP_ADD"] != DBNull.Value)
                                ob.H11_MP_ADD = Convert.ToInt64(dr["H11_MP_ADD"]);

                            if (dr["H12_MP_ADD"] != DBNull.Value)
                                ob.H12_MP_ADD = Convert.ToInt64(dr["H12_MP_ADD"]);

                            if (dr["H13_MP_ADD"] != DBNull.Value)
                                ob.H13_MP_ADD = Convert.ToInt64(dr["H13_MP_ADD"]);

                            if (dr["H14_MP_ADD"] != DBNull.Value)
                                ob.H14_MP_ADD = Convert.ToInt64(dr["H14_MP_ADD"]);

                            if (dr["H15_MP_ADD"] != DBNull.Value)
                                ob.H15_MP_ADD = Convert.ToInt64(dr["H15_MP_ADD"]);

                            if (dr["H15_P_MP_ADD"] != DBNull.Value)
                                ob.H15_P_MP_ADD = Convert.ToInt64(dr["H15_P_MP_ADD"]);

                            if (dr["H15_P_WH"] != DBNull.Value)
                                ob.H15_P_WH = Convert.ToInt64(dr["H15_P_WH"]);
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