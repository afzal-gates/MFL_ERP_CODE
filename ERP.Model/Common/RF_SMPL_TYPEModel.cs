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
    public class RF_SMPL_TYPEModel
    {
        public Int64 RF_SMPL_TYPE_ID { get; set; }
        public string SMPL_TYPE_CODE { get; set; }
        public string SMPL_TYPE_NAME { get; set; }
        public string IS_ACTIVE { get; set; }

        public Int64? MC_BU_SMPL_CFG_ID { get; set;}
        public Int64? LK_SMPL_SRC_TYPE_ID {get;set;}
        public Int64? SMPL_ORDER { get; set;}
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }
        public Int64 SZ_DEFA_QTY { get; set; }
        public string DEFA_SIZE_LST { get; set; }
        public Int64? LK_ORD_TYPE_ID { get; set; }
        public string IS_NEED_TEST { get; set; }
        public string IS_ASSORT { get; set; }

        public string Save()
        {
            const string sp = "SP_RF_SMPL_TYPE";
            string jsonStr="{";
            var ob = this;
            var i = 1;
            try
             {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pRF_SMPL_TYPE_ID", Value = ob.RF_SMPL_TYPE_ID},
                     new CommandParameter() {ParameterName = "pSMPL_TYPE_CODE", Value = ob.SMPL_TYPE_CODE},
                     new CommandParameter() {ParameterName = "pSMPL_TYPE_NAME", Value = ob.SMPL_TYPE_NAME},
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

        public string Update()
        {
            const string sp = "SP_RF_SMPL_TYPE";
            string jsonStr="{";
            var ob = this;
            var i = 1;
            try
             {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pRF_SMPL_TYPE_ID", Value = ob.RF_SMPL_TYPE_ID},
                     new CommandParameter() {ParameterName = "pSMPL_TYPE_CODE", Value = ob.SMPL_TYPE_CODE},
                     new CommandParameter() {ParameterName = "pSMPL_TYPE_NAME", Value = ob.SMPL_TYPE_NAME},
                     new CommandParameter() {ParameterName = "pIS_ACTIVE", Value = ob.IS_ACTIVE},
                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = ob.CREATED_BY},
                     new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
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
            const string sp = "SP_RF_SMPL_TYPE";
            string jsonStr="{";
            var ob = this;
            var i = 1;
            try
             {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pRF_SMPL_TYPE_ID", Value = ob.RF_SMPL_TYPE_ID},
                     new CommandParameter() {ParameterName = "pSMPL_TYPE_CODE", Value = ob.SMPL_TYPE_CODE},
                     new CommandParameter() {ParameterName = "pSMPL_TYPE_NAME", Value = ob.SMPL_TYPE_NAME},
                     new CommandParameter() {ParameterName = "pIS_ACTIVE", Value = ob.IS_ACTIVE},
                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = ob.CREATED_BY},
                     new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
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

        public List<RF_SMPL_TYPEModel> SelectAll()
        {
            string sp = "pkg_common.rf_smpl_type_select";
            try
            {
                var obList = new List<RF_SMPL_TYPEModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     //new CommandParameter() {ParameterName = "pRF_SMPL_TYPE_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
              foreach (DataRow dr in ds.Tables[0].Rows)
               {
                            RF_SMPL_TYPEModel ob = new RF_SMPL_TYPEModel();
                            ob.RF_SMPL_TYPE_ID = (dr["RF_SMPL_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_SMPL_TYPE_ID"]);
                            ob.SMPL_TYPE_CODE = (dr["SMPL_TYPE_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SMPL_TYPE_CODE"]);
                            ob.SMPL_TYPE_NAME = (dr["SMPL_TYPE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SMPL_TYPE_NAME"]);
                            ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);
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

        public RF_SMPL_TYPEModel Select(long ID)
        {
            string sp = "Select_RF_SMPL_TYPE";
            try
            {
                var ob = new RF_SMPL_TYPEModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pRF_SMPL_TYPE_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
              foreach (DataRow dr in ds.Tables[0].Rows)
               {
                        ob.RF_SMPL_TYPE_ID = (dr["RF_SMPL_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_SMPL_TYPE_ID"]);
                        ob.SMPL_TYPE_CODE = (dr["SMPL_TYPE_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SMPL_TYPE_CODE"]);
                        ob.SMPL_TYPE_NAME = (dr["SMPL_TYPE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SMPL_TYPE_NAME"]);
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

        public List<RF_SMPL_TYPEModel> SampleListByBuyer(Int64 MC_BUYER_ID)
        {
            string sp = "pkg_common.rf_smpl_type_select";
            try
            {
                var obList = new List<RF_SMPL_TYPEModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_BUYER_ID", Value =MC_BUYER_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3002},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    RF_SMPL_TYPEModel ob = new RF_SMPL_TYPEModel();
                    ob.RF_SMPL_TYPE_ID = (dr["RF_SMPL_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_SMPL_TYPE_ID"]);
                    ob.SMPL_TYPE_CODE = (dr["SMPL_TYPE_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SMPL_TYPE_CODE"]);
                    ob.SMPL_TYPE_NAME = (dr["SMPL_TYPE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SMPL_TYPE_NAME"]);
                    ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);
                    ob.MC_BU_SMPL_CFG_ID = (dr["MC_BU_SMPL_CFG_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BU_SMPL_CFG_ID"]);
                    ob.LK_SMPL_SRC_TYPE_ID = (dr["LK_SMPL_SRC_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_SMPL_SRC_TYPE_ID"]);
                    ob.SMPL_ORDER = (dr["SMPL_ORDER"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SMPL_ORDER"]);
                    ob.DEFA_SIZE_LST = (dr["DEFA_SIZE_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DEFA_SIZE_LST"]);
                    ob.SZ_DEFA_QTY = (dr["SZ_DEFA_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SZ_DEFA_QTY"]);                    

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<RF_SMPL_TYPEModel> SampleListByBuyerID(Int64 MC_BUYER_ID)
        {
            string sp = "pkg_common.rf_smpl_type_select";
            try
            {
                var obList = new List<RF_SMPL_TYPEModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_BUYER_ID", Value = MC_BUYER_ID},
                     new CommandParameter() {ParameterName = "pOption", Value = 3003},
                     new CommandParameter() {ParameterName = "pMsg", Value = 500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    RF_SMPL_TYPEModel ob = new RF_SMPL_TYPEModel();
                    ob.RF_SMPL_TYPE_ID = (dr["RF_SMPL_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_SMPL_TYPE_ID"]);
                    ob.SMPL_TYPE_CODE = (dr["SMPL_TYPE_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SMPL_TYPE_CODE"]);
                    ob.SMPL_TYPE_NAME = (dr["SMPL_TYPE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SMPL_TYPE_NAME"]);
                    ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);
                    ob.MC_BU_SMPL_CFG_ID = (dr["MC_BU_SMPL_CFG_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BU_SMPL_CFG_ID"]);
                    ob.LK_SMPL_SRC_TYPE_ID = (dr["LK_SMPL_SRC_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_SMPL_SRC_TYPE_ID"]);
                    ob.SMPL_ORDER = (dr["SMPL_ORDER"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SMPL_ORDER"]);
                    ob.LK_ORD_TYPE_ID = (dr["LK_ORD_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_ORD_TYPE_ID"]);
                    ob.IS_NEED_TEST = (dr["IS_NEED_TEST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_NEED_TEST"]);
                    ob.IS_ASSORT = (dr["IS_ASSORT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ASSORT"]);

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