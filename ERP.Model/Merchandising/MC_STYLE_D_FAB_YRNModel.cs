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
    public class MC_STYLE_D_FAB_YRNModel
    {
        public Int64 MC_STYLE_D_FAB_YRN_ID { get; set; }
        public Int64 MC_STYLE_D_FAB_ID { get; set; }
        public Int64 YARN_ITEM_ID { get; set; }
        public string LK_YFAB_PART_LST { get; set; }
        public Decimal PCT_RATIO { get; set; }
        public Int64 RF_YRN_CNT_ID { get; set; }
        public string ITEM_NAME_EN { get; set; }

        public string Save()
        {
            const string sp = "SP_MC_STYLE_D_FAB_YRN";
            string jsonStr="{";
            var ob = this;
            var i = 1;
            try
             {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_STYLE_D_FAB_YRN_ID", Value = ob.MC_STYLE_D_FAB_YRN_ID},
                     new CommandParameter() {ParameterName = "pMC_STYLE_D_FAB_ID", Value = ob.MC_STYLE_D_FAB_ID},
                     new CommandParameter() {ParameterName = "pYARN_ITEM_ID", Value = ob.YARN_ITEM_ID},
                     new CommandParameter() {ParameterName = "pLK_YFAB_PART_LST", Value = ob.LK_YFAB_PART_LST},
                     new CommandParameter() {ParameterName = "pPCT_RATIO", Value = ob.PCT_RATIO},
                     new CommandParameter() {ParameterName = "pRF_YRN_CNT_ID", Value = ob.RF_YRN_CNT_ID},
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
        public List<MC_STYLE_D_FAB_YRNModel> queryData(Int64? pMC_STYLE_H_ID = null, Int64? pMC_STYLE_D_FAB_ID = null, Int64? pRF_FAB_PROD_CAT_ID = null, Int64? pKNT_SC_PRG_RCV_ID =null)
        {
            string sp = "pkg_fabrication.mc_style_d_fab_yrn_select";
            try
            {
                var obList = new List<MC_STYLE_D_FAB_YRNModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_STYLE_H_ID", Value = pMC_STYLE_H_ID},
                     new CommandParameter() {ParameterName = "pMC_STYLE_D_FAB_ID", Value = pMC_STYLE_D_FAB_ID},
                     new CommandParameter() {ParameterName = "pRF_FAB_PROD_CAT_ID", Value = pRF_FAB_PROD_CAT_ID},
                     new CommandParameter() {ParameterName = "pKNT_SC_PRG_RCV_ID", Value = pKNT_SC_PRG_RCV_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
              foreach (DataRow dr in ds.Tables[0].Rows)
               {
                            MC_STYLE_D_FAB_YRNModel ob = new MC_STYLE_D_FAB_YRNModel();
                            ob.ITEM_NAME_EN = (dr["ITEM_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_NAME_EN"]);
                            ob.YARN_ITEM_ID = (dr["YARN_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["YARN_ITEM_ID"]);
                            ob.PCT_RATIO = (dr["PCT_RATIO"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PCT_RATIO"]);
                            ob.RF_YRN_CNT_ID = (dr["RF_YRN_CNT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_YRN_CNT_ID"]);
                            ob.KNT_YRN_LOT_ID_LST = (dr["KNT_YRN_LOT_ID_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["KNT_YRN_LOT_ID_LST"]);
                            obList.Add(ob);
               }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<MC_STYLE_D_FAB_YRNModel> getDyedYarnListForKp(Int64? pMC_FAB_PROD_ORD_H_ID, Int64? pSCM_SUPPLIER_ID)
        {
            string sp = "pkg_fabrication.mc_style_d_fab_yrn_select";
            try
            {
                var obList = new List<MC_STYLE_D_FAB_YRNModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_FAB_PROD_ORD_H_ID", Value = pMC_FAB_PROD_ORD_H_ID},
                     new CommandParameter() {ParameterName = "pSCM_SUPPLIER_ID", Value = pSCM_SUPPLIER_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3003},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_STYLE_D_FAB_YRNModel ob = new MC_STYLE_D_FAB_YRNModel();
                    ob.ITEM_NAME_EN = (dr["ITEM_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_NAME_EN"]);
                    ob.YARN_ITEM_ID = (dr["YARN_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["YARN_ITEM_ID"]);
                    ob.PCT_RATIO = (dr["PCT_RATIO"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PCT_RATIO"]);
                    ob.RF_YRN_CNT_ID = (dr["RF_YRN_CNT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_YRN_CNT_ID"]);
                    ob.KNT_YRN_LOT_ID_LST = (dr["KNT_YRN_LOT_ID_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["KNT_YRN_LOT_ID_LST"]);
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public List<MC_STYLE_D_FAB_YRNModel> queryDataFabScreen(Int64? pMC_STYLE_H_ID = null)
        {
            string sp = "pkg_fabrication.mc_style_d_fab_yrn_select";
            try
            {
                var obList = new List<MC_STYLE_D_FAB_YRNModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_STYLE_H_ID", Value = pMC_STYLE_H_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3002},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_STYLE_D_FAB_YRNModel ob = new MC_STYLE_D_FAB_YRNModel();
                    ob.ITEM_NAME_EN = (dr["ITEM_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_NAME_EN"]);
                    ob.YARN_ITEM_ID = (dr["YARN_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["YARN_ITEM_ID"]);
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public string KNT_YRN_LOT_ID_LST { get; set; }
    }
}