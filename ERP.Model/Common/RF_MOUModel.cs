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
    public class RF_MOUModel
    {
        public Int64 RF_MOU_ID { get; set; }
        public string MOU_CODE { get; set; }
        public string MOU_NAME_EN { get; set; }
        public string MOU_NAME_BN { get; set; }
        public Int64 DECIMAL_VAL { get; set; }
        public Int64 PARAM_VAL_1 { get; set; }
        public string IS_ACTIVE { get; set; }
        //public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        //public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }


        


        public string Save()
        {
            const string sp = "SP_RF_MOU";
            string jsonStr="{";
            var ob = this;
            var i = 1;
            try
             {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pRF_MOU_ID", Value = ob.RF_MOU_ID},
                     new CommandParameter() {ParameterName = "pMOU_CODE", Value = ob.MOU_CODE},
                     new CommandParameter() {ParameterName = "pMOU_NAME_EN", Value = ob.MOU_NAME_EN},
                     new CommandParameter() {ParameterName = "pMOU_NAME_BN", Value = ob.MOU_NAME_BN},
                     new CommandParameter() {ParameterName = "pDECIMAL_VAL", Value = ob.DECIMAL_VAL},
                     new CommandParameter() {ParameterName = "pPARAM_VAL_1", Value = ob.PARAM_VAL_1},
                     new CommandParameter() {ParameterName = "pIS_ACTIVE", Value = ob.IS_ACTIVE},
                     //new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = ob.CREATED_BY},
                     //new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
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

        public string Update()
        {
            const string sp = "SP_RF_MOU";
            string jsonStr="{";
            var ob = this;
            var i = 1;
            try
             {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pRF_MOU_ID", Value = ob.RF_MOU_ID},
                     new CommandParameter() {ParameterName = "pMOU_CODE", Value = ob.MOU_CODE},
                     new CommandParameter() {ParameterName = "pMOU_NAME_EN", Value = ob.MOU_NAME_EN},
                     new CommandParameter() {ParameterName = "pMOU_NAME_BN", Value = ob.MOU_NAME_BN},
                     new CommandParameter() {ParameterName = "pDECIMAL_VAL", Value = ob.DECIMAL_VAL},
                     new CommandParameter() {ParameterName = "pPARAM_VAL_1", Value = ob.PARAM_VAL_1},
                     new CommandParameter() {ParameterName = "pIS_ACTIVE", Value = ob.IS_ACTIVE},
                     //new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = ob.CREATED_BY},
                     //new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = ob.LAST_UPDATED_BY},
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
            const string sp = "SP_RF_MOU";
            string jsonStr="{";
            var ob = this;
            var i = 1;
            try
             {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pRF_MOU_ID", Value = ob.RF_MOU_ID},
                     new CommandParameter() {ParameterName = "pMOU_CODE", Value = ob.MOU_CODE},
                     new CommandParameter() {ParameterName = "pMOU_NAME_EN", Value = ob.MOU_NAME_EN},
                     new CommandParameter() {ParameterName = "pMOU_NAME_BN", Value = ob.MOU_NAME_BN},
                     new CommandParameter() {ParameterName = "pDECIMAL_VAL", Value = ob.DECIMAL_VAL},
                     new CommandParameter() {ParameterName = "pPARAM_VAL_1", Value = ob.PARAM_VAL_1},
                     new CommandParameter() {ParameterName = "pIS_ACTIVE", Value = ob.IS_ACTIVE},
                     //new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = ob.CREATED_BY},
                     //new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = ob.LAST_UPDATED_BY},
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

        public List<RF_MOUModel> SelectAll(String Default)
        {
            string sp = "pkg_common.rf_mou_select";
            try
            {
                var obList = new List<RF_MOUModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pRF_MOU_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =(Default=="Y")?3002:3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
              foreach (DataRow dr in ds.Tables[0].Rows)
               {
                            RF_MOUModel ob = new RF_MOUModel();
                            ob.RF_MOU_ID = (dr["RF_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_MOU_ID"]);
                            ob.MOU_CODE = (dr["MOU_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MOU_CODE"]);
                            ob.MOU_NAME_EN = (dr["MOU_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MOU_NAME_EN"]);
                            ob.MOU_NAME_BN = (dr["MOU_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MOU_NAME_BN"]);
                            ob.DECIMAL_VAL = (dr["DECIMAL_VAL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DECIMAL_VAL"]);
                            ob.PARAM_VAL_1 = (dr["PARAM_VAL_1"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PARAM_VAL_1"]);
                            ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);
                            //ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                            ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                            //ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
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

        public RF_MOUModel Select(long ID)
        {
            string sp = "Select_RF_MOU";
            try
            {
                var ob = new RF_MOUModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pRF_MOU_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
              foreach (DataRow dr in ds.Tables[0].Rows)
               {
                        ob.RF_MOU_ID = (dr["RF_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_MOU_ID"]);
                        ob.MOU_CODE = (dr["MOU_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MOU_CODE"]);
                        ob.MOU_NAME_EN = (dr["MOU_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MOU_NAME_EN"]);
                        ob.MOU_NAME_BN = (dr["MOU_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MOU_NAME_BN"]);
                        ob.DECIMAL_VAL = (dr["DECIMAL_VAL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DECIMAL_VAL"]);
                        ob.PARAM_VAL_1 = (dr["PARAM_VAL_1"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PARAM_VAL_1"]);
                        ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);
                        //ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                        ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                        //ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
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