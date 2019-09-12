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

    public class KNT_COMBO_ColModel
    {
        public Int64 YD_COMBO_ID { get; set; }
        public string COLOR_NAME_EN { get; set; }

        public string ORDER_NO_LIST { get; set; }
        public Int64 MC_FAB_PROD_ORD_H_ID { get; set; }
        public string STYLE_NO { get; set; }
        public decimal NET_GFAB_QTY { get; set; }

        private List<KNT_YDP_D_FDRModel> _details = null;
        public List<KNT_YDP_D_FDRModel> details
        {
            get
            {
                if (_details == null)
                {
                    _details = new List<KNT_YDP_D_FDRModel>();
                }
                return _details;
            }
            set
            {
                _details = value;
            }
        }

        public List<KNT_COMBO_ColModel> Query(Int64 pKNT_YD_PRG_H_ID)
        {
            string sp = "Select_KNT_YDP_D_FDR";
            try
            {
                var obList = new List<KNT_COMBO_ColModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_YDP_D_FDR_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    KNT_COMBO_ColModel ob = new KNT_COMBO_ColModel();
                    ob.YD_COMBO_ID = (dr["YD_COMBO_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["YD_COMBO_ID"]);
                    ob.COLOR_NAME_EN = (dr["COLOR_NAME_EN"] == DBNull.Value) ? String.Empty : Convert.ToString(dr["COLOR_NAME_EN"]);
                    ob.details = new KNT_YDP_D_FDRModel().Query(pKNT_YD_PRG_H_ID, ob.YD_COMBO_ID);
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<KNT_COMBO_ColModel> getColorList(String pMC_FAB_PROD_ORD_H_LST, Int64? pKNT_YD_PRG_H_ID, Int64? pPARENT_ID=null)
        {
            string sp = "PKG_KNT_YD_PRG.knt_yd_prg_h_select";
            try
            {
                var obList = new List<KNT_COMBO_ColModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_FAB_PROD_ORD_H_LST", Value = pMC_FAB_PROD_ORD_H_LST},
                     new CommandParameter() {ParameterName = "pKNT_YD_PRG_H_ID", Value = pKNT_YD_PRG_H_ID??pPARENT_ID},
                     new CommandParameter() {ParameterName = "pOption", Value = pPARENT_ID.Equals(null)?3002:3006},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    KNT_COMBO_ColModel ob = new KNT_COMBO_ColModel();
                    ob.YD_COMBO_ID = (dr["YD_COMBO_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["YD_COMBO_ID"]);
                    ob.MC_FAB_PROD_ORD_H_ID = (dr["MC_FAB_PROD_ORD_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_FAB_PROD_ORD_H_ID"]);
                    ob.COLOR_NAME_EN = (dr["COLOR_NAME_EN"] == DBNull.Value) ? String.Empty : Convert.ToString(dr["COLOR_NAME_EN"]);
                    ob.ORDER_NO_LIST = (dr["ORDER_NO_LIST"] == DBNull.Value) ? String.Empty : Convert.ToString(dr["ORDER_NO_LIST"]);
                    ob.STYLE_NO = (dr["STYLE_NO"] == DBNull.Value) ? String.Empty : Convert.ToString(dr["STYLE_NO"]);
                    ob.MC_FAB_PROD_ORD_H_ID = (dr["MC_FAB_PROD_ORD_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_FAB_PROD_ORD_H_ID"]);
                    ob.NET_GFAB_QTY = (dr["NET_GFAB_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["NET_GFAB_QTY"]);
                    ob.details = new KNT_YDP_D_FDRModel().getBaseColorListByYd(ob.YD_COMBO_ID, pKNT_YD_PRG_H_ID, ob.MC_FAB_PROD_ORD_H_ID, pPARENT_ID);
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
    public class KNT_YDP_D_FDRModel
    {
        public Int64 KNT_YDP_D_FDR_ID { get; set; }
        public Int64 KNT_YD_PRG_H_ID { get; set; }
        public Int64 YD_COMBO_ID { get; set; }
        public Int64 YRN_COLOR_ID { get; set; }
        public Int64 RQD_FDR_QTY { get; set; }
        public Int64 LK_YD_APV_STS_ID { get; set; }

        public Int64 CAL_YRN_QTY { get; set; }
        
        public string COLOR_NAME_EN { get; set; }
        public string Save()
        {
            const string sp = "SP_KNT_YDP_D_FDR";
            string jsonStr="{";
            var ob = this;
            var i = 1;
            try
             {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_YDP_D_FDR_ID", Value = ob.KNT_YDP_D_FDR_ID},
                     new CommandParameter() {ParameterName = "pKNT_YD_PRG_H_ID", Value = ob.KNT_YD_PRG_H_ID},
                     new CommandParameter() {ParameterName = "pYD_COMBO_ID", Value = ob.YD_COMBO_ID},
                     new CommandParameter() {ParameterName = "pYRN_COLOR_ID", Value = ob.YRN_COLOR_ID},
                     new CommandParameter() {ParameterName = "pCAL_YRN_QTY", Value = ob.CAL_YRN_QTY},
                     new CommandParameter() {ParameterName = "pRQD_FDR_QTY", Value = ob.RQD_FDR_QTY},
                     new CommandParameter() {ParameterName = "pLK_YD_APV_STS_ID", Value = ob.LK_YD_APV_STS_ID},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
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
        public List<KNT_YDP_D_FDRModel> Query(Int64 pKNT_YD_PRG_H_ID, Int64 YD_COMBO_ID)
        {
            string sp = "Select_KNT_YDP_D_FDR";
            try
            {
                var obList = new List<KNT_YDP_D_FDRModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_YDP_D_FDR_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
              foreach (DataRow dr in ds.Tables[0].Rows)
               {
                            KNT_YDP_D_FDRModel ob = new KNT_YDP_D_FDRModel();
                            ob.KNT_YDP_D_FDR_ID = (dr["KNT_YDP_D_FDR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_YDP_D_FDR_ID"]);
                            ob.KNT_YD_PRG_H_ID = (dr["KNT_YD_PRG_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_YD_PRG_H_ID"]);
                            ob.YD_COMBO_ID = (dr["YD_COMBO_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["YD_COMBO_ID"]);
                            ob.YRN_COLOR_ID = (dr["YRN_COLOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["YRN_COLOR_ID"]);
                            ob.RQD_FDR_QTY = (dr["RQD_FDR_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RQD_FDR_QTY"]);

                            ob.CAL_YRN_QTY = (dr["CAL_YRN_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CAL_YRN_QTY"]);
                  
                            ob.LK_YD_APV_STS_ID = (dr["LK_YD_APV_STS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_YD_APV_STS_ID"]);
                            obList.Add(ob);
               }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public KNT_YDP_D_FDRModel Select(long ID)
        {
            string sp = "Select_KNT_YDP_D_FDR";
            try
            {
                var ob = new KNT_YDP_D_FDRModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_YDP_D_FDR_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
              foreach (DataRow dr in ds.Tables[0].Rows)
               {
                        ob.KNT_YDP_D_FDR_ID = (dr["KNT_YDP_D_FDR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_YDP_D_FDR_ID"]);
                        ob.KNT_YD_PRG_H_ID = (dr["KNT_YD_PRG_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_YD_PRG_H_ID"]);
                        ob.YD_COMBO_ID = (dr["YD_COMBO_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["YD_COMBO_ID"]);
                        ob.YRN_COLOR_ID = (dr["YRN_COLOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["YRN_COLOR_ID"]);
                        ob.RQD_FDR_QTY = (dr["RQD_FDR_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RQD_FDR_QTY"]);
                        ob.LK_YD_APV_STS_ID = (dr["LK_YD_APV_STS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_YD_APV_STS_ID"]);
               }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<KNT_YDP_D_FDRModel> getBaseColorListByYd(Int64 pYD_COMBO_ID, Int64? pKNT_YD_PRG_H_ID, Int64 pMC_FAB_PROD_ORD_H_ID, Int64? pPARENT_ID)
        {
            string sp = "PKG_KNT_YD_PRG.knt_yd_prg_h_select";
            try
            {
                var obList = new List<KNT_YDP_D_FDRModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pYD_COMBO_ID", Value = pYD_COMBO_ID},
                     new CommandParameter() {ParameterName = "pKNT_YD_PRG_H_ID", Value = pKNT_YD_PRG_H_ID??pPARENT_ID},
                     new CommandParameter() {ParameterName = "pMC_FAB_PROD_ORD_H_ID", Value = pMC_FAB_PROD_ORD_H_ID},
                     new CommandParameter() {ParameterName = "pOption", Value = pPARENT_ID.Equals(null)?3003:3007},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    KNT_YDP_D_FDRModel ob = new KNT_YDP_D_FDRModel();
                    ob.KNT_YDP_D_FDR_ID = (dr["KNT_YDP_D_FDR_ID"] == DBNull.Value) ? -1 : Convert.ToInt64(dr["KNT_YDP_D_FDR_ID"]);
                    ob.KNT_YD_PRG_H_ID = (dr["KNT_YD_PRG_H_ID"] == DBNull.Value) ? -1 : Convert.ToInt64(dr["KNT_YD_PRG_H_ID"]);
                    ob.YD_COMBO_ID = pYD_COMBO_ID;
                    ob.YRN_COLOR_ID = (dr["YRN_COLOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["YRN_COLOR_ID"]);
                    ob.RQD_FDR_QTY = (dr["RQD_FDR_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RQD_FDR_QTY"]);
                    ob.CAL_YRN_QTY = (dr["CAL_YRN_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CAL_YRN_QTY"]);
                
                    if (dr["LK_YD_APV_STS_ID"] != DBNull.Value)
                    {
                        ob.LK_YD_APV_STS_ID = Convert.ToInt64(dr["LK_YD_APV_STS_ID"]);
                    }

                    ob.COLOR_NAME_EN = (dr["COLOR_NAME_EN"] == DBNull.Value) ? String.Empty : Convert.ToString(dr["COLOR_NAME_EN"]);
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