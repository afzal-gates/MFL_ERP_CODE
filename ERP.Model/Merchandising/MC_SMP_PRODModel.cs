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
    public class MC_SMP_PRODModel
    {
        public Int64 MC_SMP_PROD_ID { get; set; }
        public Int64 MC_SMP_REQ_D1_ID { get; set; }
        public DateTime PROD_DT { get; set; }
        public Int64 PROD_BATCH_NO { get; set; }
        public Int64? PRD_COLOR_ID { get; set; }
        public Int64? PRD_SIZE_ID { get; set; }
        public Int64 SEW_QTY { get; set; }
        public Int64 DELV_QTY { get; set; }
        public Int64 REJECT_QTY { get; set; }
        public Int64 QTY_MOU_ID { get; set; }
        public string REMARKS { get; set; }
        public string IS_SMP_FINALIZED { get; set; }

        
        public Int64 RQD_QTY { get; set; }
        public Int64 MC_SMP_REQ_H_ID { get; set; }
        public Int64 MC_SMP_REQ_D_ID { get; set; }
        public string SMP_REQ_NO { get; set; }        
        public Int64 MC_BYR_ACC_ID { get; set; }
        public string BYR_ACC_NAME_EN { get; set; }
        public string BYR_ACC_SNAME { get; set; }        
        public Int64 MC_STYLE_H_EXT_ID { get; set; }
        public string STYLE_NO { get; set; }
        public Int64 RF_SMPL_TYPE_ID { get; set; }
        public string SMPL_TYPE_NAME { get; set; }
        public Int64 MC_STYLE_D_ITEM_ID { get; set; }
        public string ITEM_SNAME { get; set; }
        public string COLOR_NAME_EN { get; set; }
        public string SIZE_CODE { get; set; }
        public string MOU_CODE { get; set; }
        public string IS_ANY_COL { get; set; }
        public string IS_AVAILABLE_COL { get; set; }
        public List<MC_COLORModel> itemWiseColorList { get; set; }
        


        public string Save()
        {
            const string sp = "SP_MC_SMP_PROD";
            string jsonStr="{";
            var ob = this;
            var i = 1;
            try
             {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_SMP_PROD_ID", Value = ob.MC_SMP_PROD_ID},
                     new CommandParameter() {ParameterName = "pMC_SMP_REQ_D1_ID", Value = ob.MC_SMP_REQ_D1_ID},
                     new CommandParameter() {ParameterName = "pPROD_DT", Value = ob.PROD_DT},
                     new CommandParameter() {ParameterName = "pPROD_BATCH_NO", Value = ob.PROD_BATCH_NO},
                     new CommandParameter() {ParameterName = "pPRD_COLOR_ID", Value = ob.PRD_COLOR_ID},
                     new CommandParameter() {ParameterName = "pPRD_SIZE_ID", Value = ob.PRD_SIZE_ID},
                     new CommandParameter() {ParameterName = "pSEW_QTY", Value = ob.SEW_QTY},
                     new CommandParameter() {ParameterName = "pDELV_QTY", Value = ob.DELV_QTY},
                     new CommandParameter() {ParameterName = "pREJECT_QTY", Value = ob.REJECT_QTY},
                     new CommandParameter() {ParameterName = "pQTY_MOU_ID", Value = ob.QTY_MOU_ID},
                     new CommandParameter() {ParameterName = "pREMARKS", Value = ob.REMARKS},
                     new CommandParameter() {ParameterName = "pIS_SMP_FINALIZED", Value = ob.IS_SMP_FINALIZED},                     
                     //new CommandParameter() {ParameterName = "pCREATED_BY", Value = ob.CREATED_BY},                    
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

        public List<MC_SMP_PRODModel> SelectAll()
        {
            string sp = "Select_MC_SMP_PROD";
            try
            {
                var obList = new List<MC_SMP_PRODModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_SMP_PROD_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
              foreach (DataRow dr in ds.Tables[0].Rows)
               {
                            MC_SMP_PRODModel ob = new MC_SMP_PRODModel();
                            ob.MC_SMP_PROD_ID = (dr["MC_SMP_PROD_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_SMP_PROD_ID"]);
                            ob.MC_SMP_REQ_D1_ID = (dr["MC_SMP_REQ_D1_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_SMP_REQ_D1_ID"]);
                            ob.PROD_DT = (dr["PROD_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["PROD_DT"]);
                            ob.PROD_BATCH_NO = (dr["PROD_BATCH_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PROD_BATCH_NO"]);
                            ob.PRD_COLOR_ID = (dr["PRD_COLOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PRD_COLOR_ID"]);
                            ob.PRD_SIZE_ID = (dr["PRD_SIZE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PRD_SIZE_ID"]);
                            ob.SEW_QTY = (dr["SEW_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SEW_QTY"]);
                            ob.DELV_QTY = (dr["DELV_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DELV_QTY"]);
                            ob.REJECT_QTY = (dr["REJECT_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["REJECT_QTY"]);
                            ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);
                            ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                            ob.IS_SMP_FINALIZED = (dr["IS_SMP_FINALIZED"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_SMP_FINALIZED"]);
                            
                            obList.Add(ob);
               }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public MC_SMP_PRODModel Select(long ID)
        {
            string sp = "Select_MC_SMP_PROD";
            try
            {
                var ob = new MC_SMP_PRODModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_SMP_PROD_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
              foreach (DataRow dr in ds.Tables[0].Rows)
               {
                        ob.MC_SMP_PROD_ID = (dr["MC_SMP_PROD_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_SMP_PROD_ID"]);
                        ob.MC_SMP_REQ_D1_ID = (dr["MC_SMP_REQ_D1_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_SMP_REQ_D1_ID"]);
                        ob.PROD_DT = (dr["PROD_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["PROD_DT"]);
                        ob.PROD_BATCH_NO = (dr["PROD_BATCH_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PROD_BATCH_NO"]);
                        ob.PRD_COLOR_ID = (dr["PRD_COLOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PRD_COLOR_ID"]);
                        ob.PRD_SIZE_ID = (dr["PRD_SIZE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PRD_SIZE_ID"]);
                        ob.SEW_QTY = (dr["SEW_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SEW_QTY"]);
                        ob.DELV_QTY = (dr["DELV_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DELV_QTY"]);
                        ob.REJECT_QTY = (dr["REJECT_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["REJECT_QTY"]);
                        ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);
                        ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                        ob.IS_SMP_FINALIZED = (dr["IS_SMP_FINALIZED"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_SMP_FINALIZED"]);                        
               }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public MC_SMP_PRODVM_Model getNewProdBatch(DateTime pPROD_DT, Int64? pPROD_BATCH_NO = null)
        {
            string sp = "pkg_mc_fab_booking.mc_smp_prod_select";
            try
            {
                MC_SMP_PRODVM_Model ob = new MC_SMP_PRODVM_Model();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {    
                     new CommandParameter() {ParameterName = "pPROD_DT", Value = pPROD_DT},
                     new CommandParameter() {ParameterName = "pPROD_BATCH_NO", Value = pPROD_BATCH_NO},
                     new CommandParameter() {ParameterName = "pOption", Value = 3005},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.IS_BATCH_OPEN = (dr["IS_BATCH_OPEN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_BATCH_OPEN"]);
                    ob.NEW_BATCH_NO = (dr["NEW_BATCH_NO"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["NEW_BATCH_NO"]);                                   
                }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<MC_SMP_PRODModel> getStyleSmpTypWiseProdQty(Int64 pMC_SMP_REQ_H_ID, Int64 pMC_STYLE_H_EXT_ID, Int64 pRF_SMPL_TYPE_ID,
            DateTime pPROD_DT, Int64 pPROD_BATCH_NO)
        {
            string sp = "pkg_mc_fab_booking.mc_smp_prod_select";
            try
            {
                var obList = new List<MC_SMP_PRODModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_SMP_REQ_H_ID", Value = pMC_SMP_REQ_H_ID},
                     new CommandParameter() {ParameterName = "pMC_STYLE_H_EXT_ID", Value = pMC_STYLE_H_EXT_ID},
                     new CommandParameter() {ParameterName = "pRF_SMPL_TYPE_ID", Value = pRF_SMPL_TYPE_ID},
                     new CommandParameter() {ParameterName = "pPROD_DT", Value = pPROD_DT},
                     new CommandParameter() {ParameterName = "pPROD_BATCH_NO", Value = pPROD_BATCH_NO},
                     new CommandParameter() {ParameterName = "pOption", Value = 3002},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_SMP_PRODModel ob = new MC_SMP_PRODModel();
                    ob.MC_SMP_PROD_ID = (dr["MC_SMP_PROD_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_SMP_PROD_ID"]);
                    ob.MC_SMP_REQ_D1_ID = (dr["MC_SMP_REQ_D1_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_SMP_REQ_D1_ID"]);
                    ob.MC_SMP_REQ_D_ID = (dr["MC_SMP_REQ_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_SMP_REQ_D_ID"]);
                    ob.MC_SMP_REQ_H_ID = (dr["MC_SMP_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_SMP_REQ_H_ID"]);
                    ob.PROD_DT = (dr["PROD_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["PROD_DT"]);
                    //ob.PROD_BATCH_NO = (dr["PROD_BATCH_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PROD_BATCH_NO"]);
                    ob.PRD_COLOR_ID = (dr["MC_COLOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_COLOR_ID"]);

                    if (dr["MC_SIZE_ID"] != DBNull.Value)
                        ob.PRD_SIZE_ID = (dr["MC_SIZE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_SIZE_ID"]);

                    ob.RQD_QTY = (dr["RQD_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RQD_QTY"]);
                    ob.SEW_QTY = (dr["SEW_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SEW_QTY"]);
                    ob.DELV_QTY = (dr["DELV_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DELV_QTY"]);
                    //ob.REJECT_QTY = (dr["REJECT_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["REJECT_QTY"]);
                    ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);
                    //ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                    //ob.IS_SMP_FINALIZED = (dr["IS_SMP_FINALIZED"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_SMP_FINALIZED"]);

                    ob.STYLE_NO = (dr["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO"]);
                    ob.BYR_ACC_SNAME = (dr["BYR_ACC_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BYR_ACC_SNAME"]);
                    ob.BYR_ACC_NAME_EN = (dr["BYR_ACC_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BYR_ACC_NAME_EN"]);
                    ob.SMPL_TYPE_NAME = (dr["SMPL_TYPE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SMPL_TYPE_NAME"]);
                    ob.MC_STYLE_D_ITEM_ID = (dr["MC_STYLE_D_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_D_ITEM_ID"]);
                    ob.ITEM_SNAME = (dr["ITEM_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_SNAME"]);
                    ob.COLOR_NAME_EN = (dr["COLOR_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_NAME_EN"]);
                    ob.SIZE_CODE = (dr["SIZE_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SIZE_CODE"]);
                    ob.MOU_CODE = (dr["MOU_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MOU_CODE"]);
                    ob.IS_ANY_COL = (dr["IS_ANY_COL"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ANY_COL"]);
                    ob.IS_AVAILABLE_COL = (dr["IS_AVAILABLE_COL"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_AVAILABLE_COL"]);

                    if (ob.IS_ANY_COL == "Y")
                    {
                        var obItemWiseColorList = new MC_SMP_REQ_D_CFGModel().smpCfgItemWiseColorList(Convert.ToString(dr["MC_COLOR_LST"]));
                        ob.itemWiseColorList = (List<MC_COLORModel>)obItemWiseColorList;
                    }
                    else if (ob.IS_AVAILABLE_COL == "Y")                    
                    {
                        var obItemWiseColorList = new MC_COLORModel().SelectAll();
                        ob.itemWiseColorList = (List<MC_COLORModel>)obItemWiseColorList;
                    }
                    
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public object getSmpTypList4Prod()
        {
            string sp = "pkg_mc_fab_booking.mc_smp_prod_select";
            try
            {
                var obList = new List<MC_SMP_PRODModel>();

                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   new CommandParameter() {ParameterName = "pOption", Value = 3003},
                    new CommandParameter() {ParameterName = "pSC_USER_ID", Value = Convert.ToInt64(HttpContext.Current.Session["multiScUserId"])},
                    new CommandParameter() {ParameterName = "pMsg", Value = 500, Direction = ParameterDirection.Output}
                }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_SMP_PRODModel ob = new MC_SMP_PRODModel();
                    ob.MC_SMP_REQ_H_ID = (dr["MC_SMP_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_SMP_REQ_H_ID"]);
                    ob.MC_BYR_ACC_ID = (dr["MC_BYR_ACC_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BYR_ACC_ID"]);
                    ob.BYR_ACC_NAME_EN = (dr["BYR_ACC_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BYR_ACC_NAME_EN"]);
                    ob.MC_STYLE_H_EXT_ID = (dr["MC_STYLE_H_EXT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_H_EXT_ID"]);
                    ob.STYLE_NO = (dr["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO"]);
                    ob.RF_SMPL_TYPE_ID = (dr["RF_SMPL_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_SMPL_TYPE_ID"]);
                    ob.SMPL_TYPE_NAME = (dr["SMPL_TYPE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SMPL_TYPE_NAME"]);                    
                    ob.SMP_REQ_NO = (dr["SMP_REQ_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SMP_REQ_NO"]);

                    //ob.SMP_REQ_DT = (dr["SMP_REQ_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["SMP_REQ_DT"]);
                    //ob.STYLE_NO_LST = (dr["STYLE_NO_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO_LST"]);
                    //ob.BYR_ACC_NAME_EN_LST = (dr["BYR_ACC_NAME_EN_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BYR_ACC_NAME_EN_LST"]);
                   
                    obList.Add(ob);
                }

                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object getSmplProdQty(DateTime? pPROD_DT = null, int? pPROD_BATCH_NO = null)
        {
            string sp = "pkg_mc_fab_booking.mc_smp_prod_select";
            try
            {
                var obList = new List<MC_SMP_PRODModel>();

                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   new CommandParameter() {ParameterName = "pOption", Value = 3004},
                    new CommandParameter() {ParameterName = "pPROD_DT", Value = pPROD_DT},
                    new CommandParameter() {ParameterName = "pPROD_BATCH_NO", Value = pPROD_BATCH_NO},
                    new CommandParameter() {ParameterName = "pMsg", Value = 500, Direction = ParameterDirection.Output}
                }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_SMP_PRODModel ob = new MC_SMP_PRODModel();
                    ob.MC_SMP_PROD_ID = (dr["MC_SMP_PROD_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_SMP_PROD_ID"]);
                    ob.MC_SMP_REQ_D1_ID = (dr["MC_SMP_REQ_D1_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_SMP_REQ_D1_ID"]);
                    ob.MC_SMP_REQ_D_ID = (dr["MC_SMP_REQ_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_SMP_REQ_D_ID"]);
                    ob.MC_SMP_REQ_H_ID = (dr["MC_SMP_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_SMP_REQ_H_ID"]);

                    ob.PROD_DT = (dr["PROD_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["PROD_DT"]);
                    //ob.PROD_BATCH_NO = (dr["PROD_BATCH_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PROD_BATCH_NO"]);

                    if (dr["MC_COLOR_ID"] != DBNull.Value)
                        ob.PRD_COLOR_ID = (dr["MC_COLOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_COLOR_ID"]);

                    if (dr["MC_SIZE_ID"] != DBNull.Value)
                        ob.PRD_SIZE_ID = (dr["MC_SIZE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_SIZE_ID"]);

                    ob.RQD_QTY = (dr["RQD_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RQD_QTY"]);
                    ob.SEW_QTY = (dr["SEW_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SEW_QTY"]);
                    ob.DELV_QTY = (dr["DELV_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DELV_QTY"]);
                    ob.REJECT_QTY = (dr["REJECT_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["REJECT_QTY"]);
                    ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                    ob.IS_SMP_FINALIZED = (dr["IS_SMP_FINALIZED"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_SMP_FINALIZED"]);

                    ob.STYLE_NO = (dr["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO"]);
                    ob.BYR_ACC_SNAME = (dr["BYR_ACC_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BYR_ACC_SNAME"]);
                    ob.BYR_ACC_NAME_EN = (dr["BYR_ACC_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BYR_ACC_NAME_EN"]);
                    ob.SMPL_TYPE_NAME = (dr["SMPL_TYPE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SMPL_TYPE_NAME"]);
                    ob.MC_STYLE_D_ITEM_ID = (dr["MC_STYLE_D_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_D_ITEM_ID"]);
                    ob.ITEM_SNAME = (dr["ITEM_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_SNAME"]);
                    ob.COLOR_NAME_EN = (dr["COLOR_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_NAME_EN"]);
                    ob.SIZE_CODE = (dr["SIZE_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SIZE_CODE"]);
                    ob.MOU_CODE = (dr["MOU_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MOU_CODE"]);

                    obList.Add(ob);
                }

                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object getSmplProdBatchListByDate(DateTime? pPROD_DT = null)
        {
            string sp = "pkg_mc_fab_booking.mc_smp_prod_select";
            try
            {
                var obList = new List<MC_SMP_PRODModel>();

                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   new CommandParameter() {ParameterName = "pOption", Value = 3006},
                    new CommandParameter() {ParameterName = "pPROD_DT", Value = pPROD_DT},                    
                    new CommandParameter() {ParameterName = "pMsg", Value = 500, Direction = ParameterDirection.Output}
                }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_SMP_PRODModel ob = new MC_SMP_PRODModel();
                    ob.PROD_BATCH_NO = (dr["PROD_BATCH_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PROD_BATCH_NO"]);

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


    public class MC_SMP_PRODVM_Model
    {
        public string DTL_XML { get; set; }
        public string IS_BATCH_OPEN { get; set; }
        public int? NEW_BATCH_NO { get; set; }


        public string BatchSaveSmplProd()
        {
            const string sp = "pkg_mc_fab_booking.mc_smp_prod_batch_save";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {                     
                     new CommandParameter() {ParameterName = "pDTL_XML", Value = ob.DTL_XML},
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

        public string BatchSaveSendToInhouse()
        {
            const string sp = "pkg_mc_fab_booking.mc_smp_prod_batch_save";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {                     
                     new CommandParameter() {ParameterName = "pDTL_XML", Value = ob.DTL_XML},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},                     
                     new CommandParameter() {ParameterName = "pOption", Value =1001},
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

        public string SampleSendToBuyer(MC_SMP_REQ_DModel ob)
        {
            const string sp = "pkg_mc_fab_booking.mc_smp_req_h_batch_save";
            string jsonStr = "{";
            //var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {                     
                     new CommandParameter() {ParameterName = "pMC_SMP_REQ_D_ID", Value = ob.MC_SMP_REQ_D_ID},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},                     
                     new CommandParameter() {ParameterName = "pOption", Value = 1003},
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
    }

}