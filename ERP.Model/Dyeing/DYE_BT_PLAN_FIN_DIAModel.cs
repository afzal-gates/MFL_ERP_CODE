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
    public class DYE_BT_PLAN_FIN_DIAModel
    {
        public Int64 DYE_BT_PLAN_FIN_DIA_ID { get; set; }
        public Int64 MC_FAB_PROD_ORD_D_ID { get; set; }
        public Int64 DYE_BATCH_PLAN_ID { get; set; }
        public Decimal RQD_PRD_QTY { get; set; }

        public string FIN_DIA { get; set; }
        public string DIA_TYPE_NAME { get; set; }
        public string MOU_CODE { get; set; }


        public string Save()
        {
            const string sp ="PKG_DYE_GFAB_REQ.dye_bt_plan_fin_dia_save";
            string jsonStr="{";
            var ob = this;
            var i = 1;
            try
             {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_BT_PLAN_FIN_DIA_ID", Value = ob.DYE_BT_PLAN_FIN_DIA_ID},
                     new CommandParameter() {ParameterName = "pMC_FAB_PROD_ORD_D_ID", Value = ob.MC_FAB_PROD_ORD_D_ID},
                     new CommandParameter() {ParameterName = "pDYE_BATCH_PLAN_ID", Value = ob.DYE_BATCH_PLAN_ID},
                     new CommandParameter() {ParameterName = "pRQD_PRD_QTY", Value = ob.RQD_PRD_QTY},
                     new CommandParameter() {ParameterName = "pOption", Value =1000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output},
                     new CommandParameter() {ParameterName = "OP_DYE_BT_PLAN_FIN_DIA_ID", Value =0, Direction = ParameterDirection.Output}
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

        public List<DYE_BT_PLAN_FIN_DIAModel> Query(
            int pOption,
            Int64? pINV_ITEM_CAT_ID,
            Int64 pRF_FAB_TYPE_ID,
            Int64 pRF_FIB_COMP_ID,
            Int64 pFAB_COLOR_ID,
            Int64 pMC_FAB_PROD_ORD_H_ID,
            Int64 pDYE_BATCH_PLAN_ID
        )
        {
            string sp = "PKG_DYE_GFAB_REQ.dye_bt_plan_fin_dia_select";
            //pOption=3000=>Select All Data
            try
            {
                var obList = new List<DYE_BT_PLAN_FIN_DIAModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pINV_ITEM_CAT_ID", Value =pINV_ITEM_CAT_ID },
                     new CommandParameter() {ParameterName = "pMC_FAB_PROD_ORD_H_ID", Value =pMC_FAB_PROD_ORD_H_ID },
                     new CommandParameter() {ParameterName = "pRF_FAB_TYPE_ID", Value = pRF_FAB_TYPE_ID },
                     new CommandParameter() {ParameterName = "pRF_FIB_COMP_ID", Value = pRF_FIB_COMP_ID },
                     new CommandParameter() {ParameterName = "pFAB_COLOR_ID", Value = pFAB_COLOR_ID },
                     new CommandParameter() {ParameterName = "pDYE_BATCH_PLAN_ID", Value = pDYE_BATCH_PLAN_ID },

                     new CommandParameter() {ParameterName = "pOption", Value =pOption},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
              foreach (DataRow dr in ds.Tables[0].Rows)
               {
                            DYE_BT_PLAN_FIN_DIAModel ob = new DYE_BT_PLAN_FIN_DIAModel();
                            ob.DYE_BT_PLAN_FIN_DIA_ID = (dr["DYE_BT_PLAN_FIN_DIA_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_BT_PLAN_FIN_DIA_ID"]);
                            ob.MC_FAB_PROD_ORD_D_ID = (dr["MC_FAB_PROD_ORD_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_FAB_PROD_ORD_D_ID"]);
                            ob.DYE_BATCH_PLAN_ID = (dr["DYE_BATCH_PLAN_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_BATCH_PLAN_ID"]);
                            ob.RQD_PRD_QTY = (dr["RQD_PRD_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RQD_PRD_QTY"]);

                            ob.FIN_DIA = (dr["FIN_DIA"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FIN_DIA"]);
                            ob.DIA_TYPE_NAME = (dr["DIA_TYPE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DIA_TYPE_NAME"]);
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


        public List<DYE_BT_PLAN_FIN_DIAModel> getBtDiaFinByPlanId(
                int pOption,
                Int64 pDYE_BATCH_PLAN_ID
        )
        {
            string sp = "PKG_DYE_GFAB_REQ.dye_bt_plan_fin_dia_select";
            //pOption=3000=>Select All Data
            //pOption=3003=>Select All Data By batch Plan Id
            try
            {
                var obList = new List<DYE_BT_PLAN_FIN_DIAModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_BATCH_PLAN_ID", Value =pDYE_BATCH_PLAN_ID },
                     new CommandParameter() {ParameterName = "pOption", Value =pOption},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DYE_BT_PLAN_FIN_DIAModel ob = new DYE_BT_PLAN_FIN_DIAModel();
                    ob.DYE_BT_PLAN_FIN_DIA_ID = (dr["DYE_BT_PLAN_FIN_DIA_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_BT_PLAN_FIN_DIA_ID"]);
                    ob.MC_FAB_PROD_ORD_D_ID = (dr["MC_FAB_PROD_ORD_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_FAB_PROD_ORD_D_ID"]);
                    ob.DYE_BATCH_PLAN_ID = (dr["DYE_BATCH_PLAN_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_BATCH_PLAN_ID"]);
                    ob.RQD_PRD_QTY = (dr["RQD_PRD_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RQD_PRD_QTY"]);

                    ob.FIN_DIA = (dr["FIN_DIA"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FIN_DIA"]);
                    ob.DIA_TYPE_NAME = (dr["DIA_TYPE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DIA_TYPE_NAME"]);
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

        public DYE_BT_PLAN_FIN_DIAModel Select(Int64 pDYE_BT_PLAN_FIN_DIA_ID, int pOption)
        {
            string sp = "PKG_DYE_GFAB_REQ.dye_bt_plan_fin_dia_select";
            //pOption=3001=>Select Single Data
            try
            {
                var ob = new DYE_BT_PLAN_FIN_DIAModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_BT_PLAN_FIN_DIA_ID", Value = pDYE_BT_PLAN_FIN_DIA_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =pOption},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
              foreach (DataRow dr in ds.Tables[0].Rows)
               {
                        ob.DYE_BT_PLAN_FIN_DIA_ID = (dr["DYE_BT_PLAN_FIN_DIA_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_BT_PLAN_FIN_DIA_ID"]);
                        ob.MC_FAB_PROD_ORD_D_ID = (dr["MC_FAB_PROD_ORD_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_FAB_PROD_ORD_D_ID"]);
                        ob.DYE_BATCH_PLAN_ID = (dr["DYE_BATCH_PLAN_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_BATCH_PLAN_ID"]);
                        ob.RQD_PRD_QTY = (dr["RQD_PRD_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RQD_PRD_QTY"]);

                        ob.FIN_DIA = (dr["FIN_DIA"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FIN_DIA"]);
                        ob.DIA_TYPE_NAME = (dr["DIA_TYPE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DIA_TYPE_NAME"]);
                        ob.MOU_CODE = (dr["MOU_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MOU_CODE"]);

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