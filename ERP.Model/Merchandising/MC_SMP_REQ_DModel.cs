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
    public class MC_SMP_REQ_DModel
    {
        public Int64? MC_SMP_REQ_D_ID { get; set; }
        public Int64? MC_SMP_REQ_H_ID { get; set; }
        public Int64? MC_SMP_REQ_STYL_ID { get; set; }
        public Int64? MC_TNA_TASK_STATUS_ID { get; set; }
        public Int64? MC_ORDER_H_ID { get; set; }
        public Int64? RF_SMPL_TYPE_ID { get; set; }
        public string IS_PRNTSO_REQ { get; set; }
        public Int64? LK_SMPL_SRC_TYPE_ID { get; set; }
        public DateTime? TARGET_DT { get; set; }
        public string REMARKS { get; set; }
        public string IS_NEED_TEST { get; set; }
        public string IS_ASSORT { get; set; }

        public Int64? MC_BUYER_ID { get; set; }
        public Int64 MC_STYLE_H_ID { get; set; }
        public Int64? MC_STYLE_H_EXT_ID { get; set; }
        public string HAS_SET { get; set; }
        public string STYLE_NO { get; set; }        
        public string ORDER_NO { get; set; }        
        public string SMPL_TYPE_NAME { get; set; }
        

        //public List<MC_WORK_STYLEModel> styleWiseOrderList { get; set; }
        //public List<RF_SMPL_TYPEModel> buyerWiseSampleTypeList { get; set; }
        //public List<SampChildItemVmModel> itemsChild { get; set; }
        //public List<MC_STYLE_H_EXTModel> buyerAccWiseStyleList { get; set; }
        public List<LookupDataModel> sampleSourceList { get; set; }
        public List<RF_TEST_INSTVm> testInstList { get; set; }
                      

        public string Save()
        {
            const string sp = "SP_MC_SMP_REQ_D";
            string jsonStr="{";
            var ob = this;
            var i = 1;
            try
             {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_SMP_REQ_D_ID", Value = ob.MC_SMP_REQ_D_ID},
                     new CommandParameter() {ParameterName = "pMC_SMP_REQ_H_ID", Value = ob.MC_SMP_REQ_H_ID},
                     new CommandParameter() {ParameterName = "pMC_ORDER_H_ID", Value = ob.MC_ORDER_H_ID},
                     new CommandParameter() {ParameterName = "pRF_SMPL_TYPE_ID", Value = ob.RF_SMPL_TYPE_ID},
                     new CommandParameter() {ParameterName = "pIS_PRNTSO_REQ", Value = ob.IS_PRNTSO_REQ},
                     new CommandParameter() {ParameterName = "pLK_SMPL_SRC_TYPE_ID", Value = ob.LK_SMPL_SRC_TYPE_ID},
                     new CommandParameter() {ParameterName = "pTARGET_DT", Value = ob.TARGET_DT},
                     new CommandParameter() {ParameterName = "pREMARKS", Value = ob.REMARKS},

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
            const string sp = "SP_MC_SMP_REQ_D";
            string jsonStr="{";
            var ob = this;
            var i = 1;
            try
             {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_SMP_REQ_D_ID", Value = ob.MC_SMP_REQ_D_ID},
                     new CommandParameter() {ParameterName = "pMC_SMP_REQ_H_ID", Value = ob.MC_SMP_REQ_H_ID},
                     new CommandParameter() {ParameterName = "pMC_ORDER_H_ID", Value = ob.MC_ORDER_H_ID},
                     new CommandParameter() {ParameterName = "pRF_SMPL_TYPE_ID", Value = ob.RF_SMPL_TYPE_ID},
                     new CommandParameter() {ParameterName = "pIS_PRNTSO_REQ", Value = ob.IS_PRNTSO_REQ},
                     new CommandParameter() {ParameterName = "pLK_SMPL_SRC_TYPE_ID", Value = ob.LK_SMPL_SRC_TYPE_ID},
                     new CommandParameter() {ParameterName = "pTARGET_DT", Value = ob.TARGET_DT},
                     new CommandParameter() {ParameterName = "pREMARKS", Value = ob.REMARKS},

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

        public string DeleteSampType()
        {
            const string sp = "pkg_mc_fab_booking.mc_smp_req_d_delete";
            string jsonStr="{";
            var ob = this;
            var i = 1;
            try
             {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_SMP_REQ_D_ID", Value = ob.MC_SMP_REQ_D_ID},
                     new CommandParameter() {ParameterName = "pOption", Value = 4000},
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

        public List<MC_SMP_REQ_DModel> SelectAll()
        {
            string sp = "Select_MC_SMP_REQ_D";
            try
            {
                var obList = new List<MC_SMP_REQ_DModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_SMP_REQ_D_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
              foreach (DataRow dr in ds.Tables[0].Rows)
               {
                   MC_SMP_REQ_DModel ob = new MC_SMP_REQ_DModel();
                   ob.MC_SMP_REQ_D_ID = (dr["MC_SMP_REQ_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_SMP_REQ_D_ID"]);
                   ob.MC_SMP_REQ_H_ID = (dr["MC_SMP_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_SMP_REQ_H_ID"]);
                   ob.MC_ORDER_H_ID = (dr["MC_ORDER_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_ORDER_H_ID"]);
                   ob.RF_SMPL_TYPE_ID = (dr["RF_SMPL_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_SMPL_TYPE_ID"]);
                   ob.IS_PRNTSO_REQ = (dr["IS_PRNTSO_REQ"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_PRNTSO_REQ"]);
                   ob.LK_SMPL_SRC_TYPE_ID = (dr["LK_SMPL_SRC_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_SMPL_SRC_TYPE_ID"]);
                   ob.TARGET_DT = (dr["TARGET_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["TARGET_DT"]);
                   ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                   
                   obList.Add(ob);
               }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public MC_SMP_REQ_DModel Select(long ID)
        {
            string sp = "Select_MC_SMP_REQ_D";
            try
            {
                var ob = new MC_SMP_REQ_DModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_SMP_REQ_D_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
              foreach (DataRow dr in ds.Tables[0].Rows)
               {
                   ob.MC_SMP_REQ_D_ID = (dr["MC_SMP_REQ_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_SMP_REQ_D_ID"]);
                   ob.MC_SMP_REQ_H_ID = (dr["MC_SMP_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_SMP_REQ_H_ID"]);
                   ob.MC_ORDER_H_ID = (dr["MC_ORDER_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_ORDER_H_ID"]);
                   ob.RF_SMPL_TYPE_ID = (dr["RF_SMPL_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_SMPL_TYPE_ID"]);
                   ob.IS_PRNTSO_REQ = (dr["IS_PRNTSO_REQ"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_PRNTSO_REQ"]);
                   ob.LK_SMPL_SRC_TYPE_ID = (dr["LK_SMPL_SRC_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_SMPL_SRC_TYPE_ID"]);
                   ob.TARGET_DT = (dr["TARGET_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["TARGET_DT"]);
                   ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);

               }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<MC_SMP_REQ_DModel> SampReqDtlListByHID(Int64 pMC_SMP_REQ_H_ID)
        {
            string sp = "pkg_mc_fab_booking.mc_smp_req_d_select";
            try
            {
                var obList = new List<MC_SMP_REQ_DModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_SMP_REQ_H_ID", Value = pMC_SMP_REQ_H_ID},
                     new CommandParameter() {ParameterName = "pOption", Value = 3002},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_SMP_REQ_DModel ob = new MC_SMP_REQ_DModel();
                    ob.MC_SMP_REQ_D_ID = (dr["MC_SMP_REQ_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_SMP_REQ_D_ID"]);
                    ob.MC_SMP_REQ_H_ID = (dr["MC_SMP_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_SMP_REQ_H_ID"]);
                    ob.MC_ORDER_H_ID = (dr["MC_ORDER_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_ORDER_H_ID"]);
                    ob.RF_SMPL_TYPE_ID = (dr["RF_SMPL_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_SMPL_TYPE_ID"]);
                    ob.IS_PRNTSO_REQ = (dr["IS_PRNTSO_REQ"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_PRNTSO_REQ"]);
                    ob.LK_SMPL_SRC_TYPE_ID = (dr["LK_SMPL_SRC_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_SMPL_SRC_TYPE_ID"]);
                    if (dr["TARGET_DT"] != DBNull.Value)
                    { ob.TARGET_DT = (dr["TARGET_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["TARGET_DT"]); }
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);

                    ob.MC_TNA_TASK_STATUS_ID = (dr["MC_TNA_TASK_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_TNA_TASK_STATUS_ID"]);
                    ob.MC_SMP_REQ_STYL_ID = (dr["MC_SMP_REQ_STYL_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_SMP_REQ_STYL_ID"]);

                    ob.MC_BUYER_ID = (dr["MC_BUYER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BUYER_ID"]);
                    ob.MC_STYLE_H_ID = (dr["MC_STYLE_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_H_ID"]);
                    ob.MC_STYLE_H_EXT_ID = (dr["MC_STYLE_H_EXT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_H_EXT_ID"]);
                    ob.STYLE_NO = (dr["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO"]);
                    ob.HAS_SET = (dr["HAS_SET"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["HAS_SET"]);

                    ob.ORDER_NO = (dr["ORDER_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_NO"]);
                    ob.STYLE_NO = (dr["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO"]);
                    ob.SMPL_TYPE_NAME = (dr["SMPL_TYPE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SMPL_TYPE_NAME"]);
                    ob.IS_NEED_TEST = (dr["IS_NEED_TEST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_NEED_TEST"]);
                    ob.IS_ASSORT = (dr["IS_ASSORT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ASSORT"]);
                    
                    var obSampSourceList = new LookupDataModel().LookupListData(75);
                    ob.sampleSourceList = (List<LookupDataModel>)obSampSourceList;
                    
                    //var obStyleWiseColorList = new MC_COLORModel().ColourMappDataByBuyerId(ob.MC_STYLE_H_ID);

                    //sp = "pkg_mc_fab_booking.mc_smp_req_d_cfg_select";
                    var obInsList = new List<RF_TEST_INSTVm>();
                    //var dsIns = db.ExecuteStoredProcedure(new List<CommandParameter>()
                    //{
                    //    new CommandParameter() {ParameterName = "pMC_SMP_REQ_D_ID", Value = ob.MC_SMP_REQ_D_ID},
                    //    new CommandParameter() {ParameterName = "pOption", Value = 3003},
                    //    new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                    //}, sp);

                    //var i = 0;
                    //if (ob.IS_NEED_TEST != "Y")
                    //    i = dsIns.Tables[0].Rows.Count;

                    //do
                    //{
                    //    RF_TEST_INSTVm obIns = new RF_TEST_INSTVm();

                    //    if (ob.IS_NEED_TEST == "Y")
                    //    {
                    //        if (dsIns.Tables[0].Rows[i]["RF_TEST_INST_ID"] != DBNull.Value)
                    //        { obIns.RF_TEST_INST_ID = (dsIns.Tables[0].Rows[i]["RF_TEST_INST_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dsIns.Tables[0].Rows[i]["RF_TEST_INST_ID"]); }
                    //        obIns.TEST_INST_NAME_EN = (dsIns.Tables[0].Rows[i]["TEST_INST_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dsIns.Tables[0].Rows[i]["TEST_INST_NAME_EN"]);
                    //    }

                    //    sp = "pkg_mc_fab_booking.mc_smp_req_d_cfg_select";
                    //    var obCfgList = new List<MC_SMP_REQ_D_CFGModel>();
                    //    var dsCfg = db.ExecuteStoredProcedure(new List<CommandParameter>()
                    //    {
                    //        new CommandParameter() {ParameterName = "pMC_SMP_REQ_D_ID", Value = ob.MC_SMP_REQ_D_ID},
                    //        new CommandParameter() {ParameterName = "pRF_TEST_INST_ID", Value = obIns.RF_TEST_INST_ID},
                    //        new CommandParameter() {ParameterName = "pOption", Value = 3002},
                    //        new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                    //    }, sp);
                    //    foreach (DataRow drCfg in dsCfg.Tables[0].Rows)
                    //    {
                    //        MC_SMP_REQ_D_CFGModel obCfg = new MC_SMP_REQ_D_CFGModel();
                    //        obCfg.MC_SMP_REQ_D_CFG_ID = (drCfg["MC_SMP_REQ_D_CFG_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(drCfg["MC_SMP_REQ_D_CFG_ID"]);
                    //        obCfg.MC_STYLE_D_ITEM_ID = (drCfg["MC_STYLE_D_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(drCfg["MC_STYLE_D_ITEM_ID"]);
                    //        obCfg.ITEM_SNAME = (drCfg["ITEM_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(drCfg["ITEM_SNAME"]);
                    //        obCfg.RQD_QTY = (drCfg["RQD_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(drCfg["RQD_QTY"]);
                    //        obCfg.QTY_MOU_ID = (drCfg["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(drCfg["QTY_MOU_ID"]);

                    //        obCfg.IS_ALL_COL = (drCfg["IS_ALL_COL"] == DBNull.Value) ? string.Empty : Convert.ToString(drCfg["IS_ALL_COL"]);
                    //        obCfg.MC_COLOR_LST = (drCfg["MC_COLOR_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(drCfg["MC_COLOR_LST"]);
                    //        obCfg.MC_SIZE_LST = (drCfg["MC_SIZE_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(drCfg["MC_SIZE_LST"]);

                    //        obCfg.styleWiseColorList = (List<MC_COLORModel>)obStyleWiseColorList;

                    //        if (drCfg["MC_COLOR_LST"] != DBNull.Value)
                    //        { obCfg.MC_COLOR_LIST = Convert.ToString(drCfg["MC_COLOR_LST"]).Split(','); }
                    //        if (drCfg["MC_SIZE_LST"] != DBNull.Value)
                    //        { obCfg.MC_SIZE_LIST = Convert.ToString(drCfg["MC_SIZE_LST"]).Split(','); }

                    //        obCfg.IS_SZ_TBC = (drCfg["IS_SZ_TBC"] == DBNull.Value) ? string.Empty : Convert.ToString(drCfg["IS_SZ_TBC"]);
                    //        if (drCfg["CONFIRM_DT"] != DBNull.Value)
                    //        { obCfg.CONFIRM_DT = Convert.ToDateTime(drCfg["CONFIRM_DT"]); }
                    //        obCfg.REMARKS = (drCfg["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(drCfg["REMARKS"]);

                    //        obCfgList.Add(obCfg);
                    //    }
                    //    obIns.styleItemsList = obCfgList;
                    //    obInsList.Add(obIns);

                    //    i++;
                    //} while (i < dsIns.Tables[0].Rows.Count);

                    
                    ob.testInstList = obInsList;
                    
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<RF_TEST_INSTVm> SampWiseItmCfgList(Int64 pMC_SMP_REQ_D_ID, string pIS_NEED_TEST, Int64 pMC_STYLE_H_ID, Int64 pMC_ORDER_H_ID)
        {
            var obChildItemList = new List<MC_STYLE_D_ITEMModel>();
            var obStyleWiseColorList = new MC_COLORModel().ColourMappDataByBuyerId(pMC_STYLE_H_ID);
            var obStyleWiseSizeList = new MC_SIZEModel().StyleWiseSizeList(pMC_STYLE_H_ID);
            string sp = "pkg_mc_fab_booking.mc_smp_req_d_cfg_select";
            try
            {
                var ob = new MC_SMP_REQ_DModel();
                OraDatabase db = new OraDatabase();
                var obInsList = new List<RF_TEST_INSTVm>();
                var dsIns = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                    new CommandParameter() {ParameterName = "pMC_SMP_REQ_D_ID", Value = pMC_SMP_REQ_D_ID},
                    new CommandParameter() {ParameterName = "pOption", Value = 3003},
                    new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                }, sp);
                var i = 0;
                if (pIS_NEED_TEST != "Y")
                    i = dsIns.Tables[0].Rows.Count;

                do
                {
                    RF_TEST_INSTVm obIns = new RF_TEST_INSTVm();

                    if (pIS_NEED_TEST == "Y")
                    {
                        if (dsIns.Tables[0].Rows[i]["RF_TEST_INST_ID"] != DBNull.Value)
                        { obIns.RF_TEST_INST_ID = (dsIns.Tables[0].Rows[i]["RF_TEST_INST_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dsIns.Tables[0].Rows[i]["RF_TEST_INST_ID"]); }
                        obIns.TEST_INST_NAME_EN = (dsIns.Tables[0].Rows[i]["TEST_INST_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dsIns.Tables[0].Rows[i]["TEST_INST_NAME_EN"]);
                    }

                    sp = "pkg_mc_fab_booking.mc_smp_req_d_cfg_select";
                    var obCfgList = new List<MC_SMP_REQ_D_CFGModel>();
                    var dsCfg = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                    new CommandParameter() {ParameterName = "pMC_SMP_REQ_D_ID", Value = pMC_SMP_REQ_D_ID},
                    new CommandParameter() {ParameterName = "pRF_TEST_INST_ID", Value = obIns.RF_TEST_INST_ID},
                    new CommandParameter() {ParameterName = "pOption", Value = 3002},
                    new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                }, sp);
                    foreach (DataRow drCfg in dsCfg.Tables[0].Rows)
                    {
                        MC_SMP_REQ_D_CFGModel obCfg = new MC_SMP_REQ_D_CFGModel();
                        obCfg.MC_SMP_REQ_D_CFG_ID = (drCfg["MC_SMP_REQ_D_CFG_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(drCfg["MC_SMP_REQ_D_CFG_ID"]);
                        obCfg.MC_SMP_REQ_D_ID = (drCfg["MC_SMP_REQ_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(drCfg["MC_SMP_REQ_D_ID"]);
                        obCfg.MC_STYLE_D_ITEM_ID = (drCfg["MC_STYLE_D_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(drCfg["MC_STYLE_D_ITEM_ID"]);
                        if (drCfg["PARENT_ID"] != DBNull.Value)
                        { obCfg.PARENT_ID = (drCfg["PARENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(drCfg["PARENT_ID"]); }
                        obCfg.ITEM_SNAME = (drCfg["ITEM_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(drCfg["ITEM_SNAME"]);
                        obCfg.RQD_QTY = (drCfg["RQD_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(drCfg["RQD_QTY"]);
                        obCfg.QTY_MOU_ID = (drCfg["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(drCfg["QTY_MOU_ID"]);

                        obCfg.IS_ALL_COL = (drCfg["IS_ALL_COL"] == DBNull.Value) ? string.Empty : Convert.ToString(drCfg["IS_ALL_COL"]);
                        obCfg.IS_ANY_COL = (drCfg["IS_ANY_COL"] == DBNull.Value) ? string.Empty : Convert.ToString(drCfg["IS_ANY_COL"]);
                        obCfg.IS_AVAILABLE_COL = (drCfg["IS_AVAILABLE_COL"] == DBNull.Value) ? string.Empty : Convert.ToString(drCfg["IS_AVAILABLE_COL"]);

                        obCfg.MC_COLOR_LST = (drCfg["MC_COLOR_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(drCfg["MC_COLOR_LST"]);
                        obCfg.MC_SIZE_LST = (drCfg["MC_SIZE_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(drCfg["MC_SIZE_LST"]);

                        obCfg.childItemList = (List<MC_STYLE_D_ITEMModel>)obChildItemList;

                        var obOrderItemWiseColorList = new MC_COLORModel().OrderItemWiseColorList(pMC_ORDER_H_ID, obCfg.MC_STYLE_D_ITEM_ID, "Y");
                        if (((List<MC_COLORModel>)obOrderItemWiseColorList).Count>2)
                        {
                            obCfg.styleWiseColorList = (List<MC_COLORModel>)obOrderItemWiseColorList;
                        }
                        else
                        {
                            obCfg.styleWiseColorList = (List<MC_COLORModel>)obStyleWiseColorList;
                            foreach (var col in (List<MC_COLORModel>)obOrderItemWiseColorList)
                            {
                                obCfg.styleWiseColorList.Add(col);
                            }
                        }

                        //obCfg.styleWiseColorList = (List<MC_COLORModel>)obStyleWiseColorList;
                        obCfg.styleWiseSizeList = (List<MC_SIZEModel>)obStyleWiseSizeList;

                        if (drCfg["MC_COLOR_LST"] != DBNull.Value)
                        { obCfg.MC_COLOR_LIST = Convert.ToString(drCfg["MC_COLOR_LST"]).Split(','); }
                        if (drCfg["MC_SIZE_LST"] != DBNull.Value)
                        { obCfg.MC_SIZE_LIST = Convert.ToString(drCfg["MC_SIZE_LST"]).Split(','); }

                        obCfg.IS_SZ_TBC = (drCfg["IS_SZ_TBC"] == DBNull.Value) ? string.Empty : Convert.ToString(drCfg["IS_SZ_TBC"]);
                        if (drCfg["CONFIRM_DT"] != DBNull.Value)
                        { obCfg.CONFIRM_DT = Convert.ToDateTime(drCfg["CONFIRM_DT"]); }
                        obCfg.REMARKS = (drCfg["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(drCfg["REMARKS"]);

                        obCfgList.Add(obCfg);
                    }
                    obIns.styleItemsList = obCfgList;
                    obInsList.Add(obIns);

                    i++;
                } while (i < dsIns.Tables[0].Rows.Count);

                return obInsList;
            }
            catch (Exception ex)
            {
                throw ex;
            }           
        }

        public List<MC_SMP_REQ_DModel> StyleListWiseSampList(string pMC_STYLE_H_EXT_IDS)
        {
            //var obAllSizeList = new MC_SIZEModel().SelectAll();

            string sp = "pkg_mc_fab_booking.mc_smp_req_d_select";
            try
            {
                var obList = new List<MC_SMP_REQ_DModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_STYLE_H_EXT_IDS", Value = pMC_STYLE_H_EXT_IDS},
                     new CommandParameter() {ParameterName = "pOption", Value = 3003},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_SMP_REQ_DModel ob = new MC_SMP_REQ_DModel();
                    ob.MC_SMP_REQ_D_ID = (dr["MC_SMP_REQ_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_SMP_REQ_D_ID"]);
                    ob.MC_SMP_REQ_H_ID = (dr["MC_SMP_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_SMP_REQ_H_ID"]);
                    ob.MC_ORDER_H_ID = (dr["MC_ORDER_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_ORDER_H_ID"]);
                    
                    ob.RF_SMPL_TYPE_ID = (dr["RF_SMPL_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_SMPL_TYPE_ID"]);
                    //ob.IS_PRNTSO_REQ = (dr["IS_PRNTSO_REQ"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_PRNTSO_REQ"]);
                    ob.LK_SMPL_SRC_TYPE_ID = (dr["LK_SMPL_SRC_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_SMPL_SRC_TYPE_ID"]);
                    //ob.TARGET_DT = (dr["TARGET_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["TARGET_DT"]);

                    ob.MC_BUYER_ID = (dr["MC_BUYER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BUYER_ID"]);
                    ob.MC_STYLE_H_ID = (dr["MC_STYLE_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_H_ID"]);
                    ob.MC_STYLE_H_EXT_ID = (dr["MC_STYLE_H_EXT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_H_EXT_ID"]);
                    ob.HAS_SET = (dr["HAS_SET"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["HAS_SET"]);

                    ob.ORDER_NO = (dr["ORDER_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_NO"]);
                    ob.STYLE_NO = (dr["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO"]);
                    ob.SMPL_TYPE_NAME = (dr["SMPL_TYPE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SMPL_TYPE_NAME"]);
                    ob.IS_NEED_TEST = (dr["IS_NEED_TEST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_NEED_TEST"]);
                    ob.IS_ASSORT = (dr["IS_ASSORT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ASSORT"]);

                    var obSampSourceList = new LookupDataModel().LookupListData(75);
                    ob.sampleSourceList = (List<LookupDataModel>)obSampSourceList;
                    
                    //var obStyleWiseColorList = new MC_COLORModel().ColourMappDataByBuyerId(ob.MC_STYLE_H_ID);
                    var obStyleWiseSizeList = new MC_SIZEModel().StyleWiseSizeList(ob.MC_STYLE_H_ID);
                    
                    sp = "pkg_common.rf_test_inst_select";
                    var obInsList = new List<RF_TEST_INSTVm>();
                    var dsIns = db.ExecuteStoredProcedure(new List<CommandParameter>()
                    {
                        new CommandParameter() {ParameterName = "pMC_BUYER_ID", Value = ob.MC_BUYER_ID},
                        new CommandParameter() {ParameterName = "pOption", Value = 3002},
                        new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                    }, sp);

                    var i = 0;
                    if (ob.IS_NEED_TEST != "Y")
                        i = dsIns.Tables[0].Rows.Count;

                    do
                    {
                        RF_TEST_INSTVm obIns = new RF_TEST_INSTVm();

                        if (ob.IS_NEED_TEST == "Y")// && i>0)
                        {
                            obIns.RF_TEST_INST_ID = (dsIns.Tables[0].Rows[i]["RF_TEST_INST_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dsIns.Tables[0].Rows[i]["RF_TEST_INST_ID"]);
                            obIns.TEST_INST_NAME_EN = (dsIns.Tables[0].Rows[i]["TEST_INST_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dsIns.Tables[0].Rows[i]["TEST_INST_NAME_EN"]);
                        }

                        if (ob.IS_ASSORT != "Y")
                        {
                            sp = "pkg_merchandising.mc_style_d_item_select";
                            var obCfgList = new List<MC_SMP_REQ_D_CFGModel>();
                            var obStyleWiseColorList = new MC_COLORModel().ColourMappDataByBuyerId(ob.MC_STYLE_H_ID);

                            var dsCfg = db.ExecuteStoredProcedure(new List<CommandParameter>()
                            {
                                new CommandParameter() {ParameterName = "pMC_STYLE_H_ID", Value = ob.MC_STYLE_H_ID},
                                new CommandParameter() {ParameterName = "pOption", Value = 3009},
                                new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                            }, sp);
                            foreach (DataRow drCfg in dsCfg.Tables[0].Rows)
                            {
                                MC_SMP_REQ_D_CFGModel obCfg = new MC_SMP_REQ_D_CFGModel();
                                obCfg.MC_SMP_REQ_D_CFG_ID = 0;
                                obCfg.MC_STYLE_D_ITEM_ID = (drCfg["MC_STYLE_D_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(drCfg["MC_STYLE_D_ITEM_ID"]);
                                if(drCfg["PARENT_ID"] != DBNull.Value)
                                { obCfg.PARENT_ID = (drCfg["PARENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(drCfg["PARENT_ID"]); }
                                obCfg.ITEM_SNAME = (drCfg["ITEM_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(drCfg["ITEM_SNAME"]);
                                obCfg.QTY_MOU_ID = 1;

                                if (dr["SZ_DEFA_QTY"] != DBNull.Value)
                                {
                                    obCfg.RQD_QTY = (dr["SZ_DEFA_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SZ_DEFA_QTY"]);
                                }

                                var obOrderItemWiseColorList = new MC_COLORModel().OrderItemWiseColorList(ob.MC_ORDER_H_ID, obCfg.MC_STYLE_D_ITEM_ID, "Y");                                
                                if (((List<MC_COLORModel>)obOrderItemWiseColorList).Count > 2)
                                {
                                    obCfg.styleWiseColorList = (List<MC_COLORModel>)obOrderItemWiseColorList;
                                }
                                else
                                {
                                    obCfg.styleWiseColorList = (List<MC_COLORModel>)obStyleWiseColorList;
                                    
                                    foreach (var col in (List<MC_COLORModel>)obOrderItemWiseColorList)
                                    {
                                        obCfg.styleWiseColorList.Add(col);
                                    }
                                    
                                }

                                //obCfg.styleWiseColorList = (List<MC_COLORModel>)obStyleWiseColorList;
                                obCfg.styleWiseSizeList = (List<MC_SIZEModel>)obStyleWiseSizeList;

                                //if (dr["DEFA_SIZE_LST"] != DBNull.Value)
                                //{ obCfg.MC_SIZE_LIST = Convert.ToString(dr["DEFA_SIZE_LST"]).Split(','); }

                                

                                obCfg.MC_SIZE_LIST = obCfg.styleWiseSizeList.ConvertAll(x => x.MC_SIZE_ID.ToString()).ToArray();// obCfg.styleWiseSizeList[0].MC_SIZE_ID.ToArray();

                                obCfgList.Add(obCfg);
                            }                        
                            obIns.styleItemsList = obCfgList;
                        }
                        obInsList.Add(obIns);

                        i++;
                    } while (i<dsIns.Tables[0].Rows.Count);

                    ob.testInstList = obInsList;

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<RF_TEST_INSTVm> GenTestInsList(Int64 pMC_BUYER_ID, Int64 pMC_STYLE_H_ID, string pIS_NEED_TEST, string pIS_ASSORT, Int64? pMC_ORDER_H_ID=null)
        {
            //var obAllSizeList = new MC_SIZEModel().SelectAll();

            string sp = "pkg_mc_fab_booking.mc_smp_req_d_select";
            try
            {                
                OraDatabase db = new OraDatabase();                                  
                //var obSampSourceList = new LookupDataModel().LookupListData(75);
                //ob.sampleSourceList = (List<LookupDataModel>)obSampSourceList;

                var obStyleWiseColorList = new MC_COLORModel().ColourMappDataByBuyerId(pMC_STYLE_H_ID);
                var obStyleWiseSizeList = new MC_SIZEModel().StyleWiseSizeList(pMC_STYLE_H_ID);

                sp = "pkg_common.rf_test_inst_select";
                var obInsList = new List<RF_TEST_INSTVm>();
                var dsIns = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                    new CommandParameter() {ParameterName = "pMC_BUYER_ID", Value = pMC_BUYER_ID},
                    new CommandParameter() {ParameterName = "pOption", Value = 3002},
                    new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                }, sp);

                var i = 0;
                if (pIS_NEED_TEST != "Y")
                    i = dsIns.Tables[0].Rows.Count;

                do
                {
                    RF_TEST_INSTVm obIns = new RF_TEST_INSTVm();

                    if (pIS_NEED_TEST == "Y")
                    {
                        obIns.RF_TEST_INST_ID = (dsIns.Tables[0].Rows[i]["RF_TEST_INST_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dsIns.Tables[0].Rows[i]["RF_TEST_INST_ID"]);
                        obIns.TEST_INST_NAME_EN = (dsIns.Tables[0].Rows[i]["TEST_INST_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dsIns.Tables[0].Rows[i]["TEST_INST_NAME_EN"]);
                    }

                    //if (pIS_ASSORT != "Y")
                    //{
                        sp = "pkg_merchandising.mc_style_d_item_select";
                        var obCfgList = new List<MC_SMP_REQ_D_CFGModel>();
                        var dsCfg = db.ExecuteStoredProcedure(new List<CommandParameter>()
                        {
                            new CommandParameter() {ParameterName = "pMC_STYLE_H_ID", Value = pMC_STYLE_H_ID},
                            new CommandParameter() {ParameterName = "pOption", Value = 3009},
                            new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                        }, sp);
                        foreach (DataRow drCfg in dsCfg.Tables[0].Rows)
                        {
                            MC_SMP_REQ_D_CFGModel obCfg = new MC_SMP_REQ_D_CFGModel();
                            obCfg.MC_SMP_REQ_D_CFG_ID = 0;
                            obCfg.MC_STYLE_D_ITEM_ID = (drCfg["MC_STYLE_D_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(drCfg["MC_STYLE_D_ITEM_ID"]);
                            if (drCfg["PARENT_ID"] != DBNull.Value)
                            { obCfg.PARENT_ID = (drCfg["PARENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(drCfg["PARENT_ID"]); }
                            obCfg.ITEM_SNAME = (drCfg["ITEM_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(drCfg["ITEM_SNAME"]);
                            obCfg.QTY_MOU_ID = 1;

                            //if (dr["SZ_DEFA_QTY"] != DBNull.Value)
                            //{
                            obCfg.RQD_QTY = 1;//(dr["SZ_DEFA_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SZ_DEFA_QTY"]);
                            //}

                            var obOrderItemWiseColorList = new MC_COLORModel().OrderItemWiseColorList(pMC_ORDER_H_ID, obCfg.MC_STYLE_D_ITEM_ID, "Y");
                            if (((List<MC_COLORModel>)obOrderItemWiseColorList).Count > 2)
                            {
                                obCfg.styleWiseColorList = (List<MC_COLORModel>)obOrderItemWiseColorList;
                            }
                            else
                            {
                                obCfg.styleWiseColorList = (List<MC_COLORModel>)obStyleWiseColorList;

                                foreach (var col in (List<MC_COLORModel>)obOrderItemWiseColorList)
                                {
                                    obCfg.styleWiseColorList.Add(col);
                                }

                            }

                            //var obStyleWiseColorList = new MC_COLORModel().OrderItemWiseColorList(pMC_ORDER_H_ID, obCfg.MC_STYLE_D_ITEM_ID, "Y");
                            obCfg.styleWiseColorList = (List<MC_COLORModel>)obStyleWiseColorList;
                            obCfg.styleWiseSizeList = (List<MC_SIZEModel>)obStyleWiseSizeList;

                            obCfg.MC_SIZE_LIST = obCfg.styleWiseSizeList.ConvertAll(x => x.MC_SIZE_ID.ToString()).ToArray();

                            //if (dr["DEFA_SIZE_LST"] != DBNull.Value)
                            //{ obCfg.MC_SIZE_LIST = Convert.ToString(dr["DEFA_SIZE_LST"]).Split(','); }

                            obCfgList.Add(obCfg);
                        }
                        obIns.styleItemsList = obCfgList;
                    //}
                    obInsList.Add(obIns);

                    i++;
                } while (i < dsIns.Tables[0].Rows.Count);


                return obInsList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        
    }



    public class RF_TEST_INSTVm
    {
        public Int64? RF_TEST_INST_ID { get; set; }
        public string TEST_INST_NAME_EN { get; set; }

        public List<MC_SMP_REQ_D_CFGModel> styleItemsList { get; set; }
    }

}