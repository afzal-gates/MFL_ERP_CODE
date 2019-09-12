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
    public class KNT_YRN_LOTModel
    {
        public Int64 KNT_YRN_LOT_ID { get; set; }
        public Int64 RF_BRAND_ID { get; set; }
        public string YRN_LOT_NO { get; set; }
        public Int64 LK_YRN_COLR_GRP_ID { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public decimal STK_BALANCE { get; set; }
        public string MOU_CODE { get; set; }
        public string BRAND_NAME_EN { get; set; }
        public long YARN_ITEM_ID { get; set; }

        public Decimal QTY_PER_PACK { get; set; }
        public Int64 PACK_MOU_ID { get; set; }
        public Int64? LK_YRN_TST_TYPE_ID { get; set; }
        public string IS_USABLE { get; set; }
        public Int64 KNT_YRN_ISS_D_ID { get; set; }
        public Int64? KNT_YRN_ISS_H_ID { get; set; }
        public string IS_SOLID { get; set; }
        public string XML_ISS_D { get; set; }
        
        public Int64 KNT_YRN_STR_REQ_H_ID { get; set; }


        public List<KNT_YRN_LOTModel> SelectAll(String pKNT_YRN_LOT_ID_LST, Int64? pRF_BRAND_ID, Int64? pYARN_ITEM_ID, string pIS_SOLID, Int64? pMC_FAB_PROD_ORD_H_ID,Int64? pSCM_SUPPLIER_ID)
        {
            string sp = "pkg_knit_plan.knt_yrn_lot_select";
            try
            {
                var obList = new List<KNT_YRN_LOTModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_YRN_LOT_ID_LST", Value = pKNT_YRN_LOT_ID_LST},
                     new CommandParameter() {ParameterName = "pRF_BRAND_ID", Value = pRF_BRAND_ID},
                     new CommandParameter() {ParameterName = "pYARN_ITEM_ID", Value = pYARN_ITEM_ID},
                     new CommandParameter() {ParameterName = "pIS_SOLID", Value = pIS_SOLID},

                     new CommandParameter() {ParameterName = "pMC_FAB_PROD_ORD_H_ID", Value = pMC_FAB_PROD_ORD_H_ID},
                     new CommandParameter() {ParameterName = "pSCM_SUPPLIER_ID", Value = pSCM_SUPPLIER_ID},
                     
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
              foreach (DataRow dr in ds.Tables[0].Rows)
               {
                            KNT_YRN_LOTModel ob = new KNT_YRN_LOTModel();
                            ob.KNT_YRN_LOT_ID = (dr["KNT_YRN_LOT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_YRN_LOT_ID"]);
                            ob.RF_BRAND_ID = (dr["RF_BRAND_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_BRAND_ID"]);
                            ob.KNT_YRN_LOT_STK_ID = (dr["KNT_YRN_LOT_STK_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_YRN_LOT_STK_ID"]);
                            ob.YRN_LOT_NO = (dr["YRN_LOT_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["YRN_LOT_NO"]);
                            ob.LK_YRN_COLR_GRP_ID = (dr["LK_YRN_COLR_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_YRN_COLR_GRP_ID"]);
                            ob.STK_BALANCE = (dr["STK_BALANCE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["STK_BALANCE"]);
                            ob.MOU_CODE = (dr["MOU_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MOU_CODE"]);
                            //ob.IS_SOLID = (dr["IS_SOLID"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_SOLID"]);
                  
                            obList.Add(ob);
               }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public List<KNT_YRN_LOTModel> YarnLotForJobCard(Int64 pKNT_PLAN_H_ID, string pIS_YD = "N")
        {
            string sp = "pkg_knit_plan.knt_yrn_lot_select";
            try
            {
                var obList = new List<KNT_YRN_LOTModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_PLAN_H_ID", Value =pKNT_PLAN_H_ID},
                     new CommandParameter() {ParameterName = "pIS_YD", Value =pIS_YD},
                     new CommandParameter() {ParameterName = "pOption", Value =3002},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    KNT_YRN_LOTModel ob = new KNT_YRN_LOTModel();
                    ob.KNT_YRN_LOT_ID = (dr["KNT_YRN_LOT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_YRN_LOT_ID"]);
                    ob.KNT_PLAN_D_ID = (dr["KNT_PLAN_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_PLAN_D_ID"]);
                    ob.YARN_ITEM_ID = (dr["YARN_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["YARN_ITEM_ID"]);                    
                    ob.RF_BRAND_ID = (dr["RF_BRAND_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_BRAND_ID"]);
                    ob.YRN_LOT_NO = (dr["YRN_LOT_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["YRN_LOT_NO"]);
                    ob.LK_YRN_COLR_GRP_ID = (dr["LK_YRN_COLR_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_YRN_COLR_GRP_ID"]);
                    ob.STK_BALANCE = (dr["STK_BALANCE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["STK_BALANCE"]);
                    ob.STITCH_LEN = (dr["STITCH_LEN"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["STITCH_LEN"]);
                    ob.MOU_CODE = (dr["MOU_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MOU_CODE"]);
                    ob.BRAND_NAME_EN = (dr["BRAND_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BRAND_NAME_EN"]);
                    //ob.IS_SOLID = (dr["IS_SOLID"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_SOLID"]);


                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<KNT_YRN_LOTModel> GetBrandWiseYarnLotList(Int64 pRF_BRAND_ID, string pYRN_LOT_NO = null)
        {
            string sp = "pkg_knit_plan.knt_yrn_lot_select";
            try
            {
                var obList = new List<KNT_YRN_LOTModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pRF_BRAND_ID", Value = pRF_BRAND_ID},
                     new CommandParameter() {ParameterName = "pYRN_LOT_NO", Value = pYRN_LOT_NO},
                     new CommandParameter() {ParameterName = "pOption", Value = 3003},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    KNT_YRN_LOTModel ob = new KNT_YRN_LOTModel();
                    ob.KNT_YRN_LOT_ID = (dr["KNT_YRN_LOT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_YRN_LOT_ID"]);                    
                    ob.RF_BRAND_ID = (dr["RF_BRAND_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_BRAND_ID"]);
                    ob.YRN_LOT_NO = (dr["YRN_LOT_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["YRN_LOT_NO"]);
                    //ob.IS_SOLID = (dr["IS_SOLID"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_SOLID"]);
                    
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public long? KNT_YRN_LOT_STK_ID { get; set; }

        public string Update()
        {
            const string sp = "pkg_knit_yarn_issue.knt_yrn_lot_update";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {

                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},

                     //new CommandParameter() {ParameterName = "pKNT_YRN_ISS_H_ID", Value = ob.KNT_YRN_ISS_H_ID},
                     new CommandParameter() {ParameterName = "pKNT_YRN_STR_REQ_H_ID", Value = ob.KNT_YRN_STR_REQ_H_ID},
                     new CommandParameter() {ParameterName = "pXML_ISS_D", Value = ob.XML_ISS_D},
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

        public long KNT_PLAN_D_ID { get; set; }
        public decimal STITCH_LEN { get; set; }
    }
}