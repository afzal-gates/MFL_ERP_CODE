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
    public class CM_IMP_PO_D_YRN_HISTModel
    {
        public Int64 CM_IMP_PO_D_YRN_HIST_ID { get; set; }
        public Int64 CM_IMP_PO_H_HIST_ID { get; set; }
        public Int64 CM_IMP_PO_D_YRN_ID { get; set; }
        public Int64 CM_IMP_PO_H_ID { get; set; }
        public Int64 SCM_PURC_REQ_D_YRN_ID { get; set; }
        public Int64 INV_ITEM_ID { get; set; }
        public Int64 LK_YRN_COLR_GRP_ID { get; set; }
        public Int64 RF_BRAND_ID { get; set; }
        public Decimal PO_QTY { get; set; }
        public Int64 QTY_MOU_ID { get; set; }
        public Decimal UNIT_PRICE { get; set; }
        public string LOT_REF_NO { get; set; }
        public DateTime DELV_TGT_DT { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }

        public string Save()
        {
            const string sp = "SP_CM_IMP_PO_D_YRN_HIST";
            string jsonStr="{";
            var ob = this;
            var i = 1;
            try
             {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pCM_IMP_PO_D_YRN_HIST_ID", Value = ob.CM_IMP_PO_D_YRN_HIST_ID},
                     new CommandParameter() {ParameterName = "pCM_IMP_PO_H_HIST_ID", Value = ob.CM_IMP_PO_H_HIST_ID},
                     new CommandParameter() {ParameterName = "pCM_IMP_PO_D_YRN_ID", Value = ob.CM_IMP_PO_D_YRN_ID},
                     new CommandParameter() {ParameterName = "pCM_IMP_PO_H_ID", Value = ob.CM_IMP_PO_H_ID},
                     new CommandParameter() {ParameterName = "pSCM_PURC_REQ_D_YRN_ID", Value = ob.SCM_PURC_REQ_D_YRN_ID},
                     new CommandParameter() {ParameterName = "pINV_ITEM_ID", Value = ob.INV_ITEM_ID},
                     new CommandParameter() {ParameterName = "pLK_YRN_COLR_GRP_ID", Value = ob.LK_YRN_COLR_GRP_ID},
                     new CommandParameter() {ParameterName = "pRF_BRAND_ID", Value = ob.RF_BRAND_ID},
                     new CommandParameter() {ParameterName = "pPO_QTY", Value = ob.PO_QTY},
                     new CommandParameter() {ParameterName = "pQTY_MOU_ID", Value = ob.QTY_MOU_ID},
                     new CommandParameter() {ParameterName = "pUNIT_PRICE", Value = ob.UNIT_PRICE},
                     new CommandParameter() {ParameterName = "pLOT_REF_NO", Value = ob.LOT_REF_NO},
                     new CommandParameter() {ParameterName = "pDELV_TGT_DT", Value = ob.DELV_TGT_DT},
                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = ob.CREATED_BY},
                     new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = ob.LAST_UPDATED_BY},
                     new CommandParameter() {ParameterName = "pOption", Value =1000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);

                 foreach (DataRow dr in ds.Tables["OUTPARAM"].Rows)
                 {
                    jsonStr += dr["KEY"].ToString() + ":"+ dr["VALUE"].ToString()+",";
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
            const string sp = "SP_CM_IMP_PO_D_YRN_HIST";
            string jsonStr="{";
            var ob = this;
            var i = 1;
            try
             {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pCM_IMP_PO_D_YRN_HIST_ID", Value = ob.CM_IMP_PO_D_YRN_HIST_ID},
                     new CommandParameter() {ParameterName = "pCM_IMP_PO_H_HIST_ID", Value = ob.CM_IMP_PO_H_HIST_ID},
                     new CommandParameter() {ParameterName = "pCM_IMP_PO_D_YRN_ID", Value = ob.CM_IMP_PO_D_YRN_ID},
                     new CommandParameter() {ParameterName = "pCM_IMP_PO_H_ID", Value = ob.CM_IMP_PO_H_ID},
                     new CommandParameter() {ParameterName = "pSCM_PURC_REQ_D_YRN_ID", Value = ob.SCM_PURC_REQ_D_YRN_ID},
                     new CommandParameter() {ParameterName = "pINV_ITEM_ID", Value = ob.INV_ITEM_ID},
                     new CommandParameter() {ParameterName = "pLK_YRN_COLR_GRP_ID", Value = ob.LK_YRN_COLR_GRP_ID},
                     new CommandParameter() {ParameterName = "pRF_BRAND_ID", Value = ob.RF_BRAND_ID},
                     new CommandParameter() {ParameterName = "pPO_QTY", Value = ob.PO_QTY},
                     new CommandParameter() {ParameterName = "pQTY_MOU_ID", Value = ob.QTY_MOU_ID},
                     new CommandParameter() {ParameterName = "pUNIT_PRICE", Value = ob.UNIT_PRICE},
                     new CommandParameter() {ParameterName = "pLOT_REF_NO", Value = ob.LOT_REF_NO},
                     new CommandParameter() {ParameterName = "pDELV_TGT_DT", Value = ob.DELV_TGT_DT},
                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = ob.CREATED_BY},
                     new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = ob.LAST_UPDATED_BY},
                     new CommandParameter() {ParameterName = "pOption", Value =2000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);

                 foreach (DataRow dr in ds.Tables["OUTPARAM"].Rows)
                 {
                    jsonStr += dr["KEY"].ToString() + ":"+ dr["VALUE"].ToString()+",";
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
            const string sp = "SP_CM_IMP_PO_D_YRN_HIST";
            string jsonStr="{";
            var ob = this;
            var i = 1;
            try
             {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pCM_IMP_PO_D_YRN_HIST_ID", Value = ob.CM_IMP_PO_D_YRN_HIST_ID},
                     new CommandParameter() {ParameterName = "pCM_IMP_PO_H_HIST_ID", Value = ob.CM_IMP_PO_H_HIST_ID},
                     new CommandParameter() {ParameterName = "pCM_IMP_PO_D_YRN_ID", Value = ob.CM_IMP_PO_D_YRN_ID},
                     new CommandParameter() {ParameterName = "pCM_IMP_PO_H_ID", Value = ob.CM_IMP_PO_H_ID},
                     new CommandParameter() {ParameterName = "pSCM_PURC_REQ_D_YRN_ID", Value = ob.SCM_PURC_REQ_D_YRN_ID},
                     new CommandParameter() {ParameterName = "pINV_ITEM_ID", Value = ob.INV_ITEM_ID},
                     new CommandParameter() {ParameterName = "pLK_YRN_COLR_GRP_ID", Value = ob.LK_YRN_COLR_GRP_ID},
                     new CommandParameter() {ParameterName = "pRF_BRAND_ID", Value = ob.RF_BRAND_ID},
                     new CommandParameter() {ParameterName = "pPO_QTY", Value = ob.PO_QTY},
                     new CommandParameter() {ParameterName = "pQTY_MOU_ID", Value = ob.QTY_MOU_ID},
                     new CommandParameter() {ParameterName = "pUNIT_PRICE", Value = ob.UNIT_PRICE},
                     new CommandParameter() {ParameterName = "pLOT_REF_NO", Value = ob.LOT_REF_NO},
                     new CommandParameter() {ParameterName = "pDELV_TGT_DT", Value = ob.DELV_TGT_DT},
                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = ob.CREATED_BY},
                     new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = ob.LAST_UPDATED_BY},
                     new CommandParameter() {ParameterName = "pOption", Value =4000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);

                 foreach (DataRow dr in ds.Tables["OUTPARAM"].Rows)
                 {
                    jsonStr += dr["KEY"].ToString() + ":"+ dr["VALUE"].ToString()+",";
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

        public List<CM_IMP_PO_D_YRN_HISTModel> SelectAll()
        {
            string sp = "Select_CM_IMP_PO_D_YRN_HIST";
            try
            {
                var obList = new List<CM_IMP_PO_D_YRN_HISTModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pCM_IMP_PO_D_YRN_HIST_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
              foreach (DataRow dr in ds.Tables[0].Rows)
               {
                            CM_IMP_PO_D_YRN_HISTModel ob = new CM_IMP_PO_D_YRN_HISTModel();
                            ob.CM_IMP_PO_D_YRN_HIST_ID = (dr["CM_IMP_PO_D_YRN_HIST_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CM_IMP_PO_D_YRN_HIST_ID"]);
                            ob.CM_IMP_PO_H_HIST_ID = (dr["CM_IMP_PO_H_HIST_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CM_IMP_PO_H_HIST_ID"]);
                            ob.CM_IMP_PO_D_YRN_ID = (dr["CM_IMP_PO_D_YRN_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CM_IMP_PO_D_YRN_ID"]);
                            ob.CM_IMP_PO_H_ID = (dr["CM_IMP_PO_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CM_IMP_PO_H_ID"]);
                            ob.SCM_PURC_REQ_D_YRN_ID = (dr["SCM_PURC_REQ_D_YRN_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_PURC_REQ_D_YRN_ID"]);
                            ob.INV_ITEM_ID = (dr["INV_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_ITEM_ID"]);
                            ob.LK_YRN_COLR_GRP_ID = (dr["LK_YRN_COLR_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_YRN_COLR_GRP_ID"]);
                            ob.RF_BRAND_ID = (dr["RF_BRAND_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_BRAND_ID"]);
                            ob.PO_QTY = (dr["PO_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PO_QTY"]);
                            ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);
                            ob.UNIT_PRICE = (dr["UNIT_PRICE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["UNIT_PRICE"]);
                            ob.LOT_REF_NO = (dr["LOT_REF_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LOT_REF_NO"]);
                            ob.DELV_TGT_DT = (dr["DELV_TGT_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["DELV_TGT_DT"]);
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

        public CM_IMP_PO_D_YRN_HISTModel Select(long ID)
        {
            string sp = "Select_CM_IMP_PO_D_YRN_HIST";
            try
            {
                var ob = new CM_IMP_PO_D_YRN_HISTModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pCM_IMP_PO_D_YRN_HIST_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
              foreach (DataRow dr in ds.Tables[0].Rows)
               {
                        ob.CM_IMP_PO_D_YRN_HIST_ID = (dr["CM_IMP_PO_D_YRN_HIST_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CM_IMP_PO_D_YRN_HIST_ID"]);
                        ob.CM_IMP_PO_H_HIST_ID = (dr["CM_IMP_PO_H_HIST_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CM_IMP_PO_H_HIST_ID"]);
                        ob.CM_IMP_PO_D_YRN_ID = (dr["CM_IMP_PO_D_YRN_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CM_IMP_PO_D_YRN_ID"]);
                        ob.CM_IMP_PO_H_ID = (dr["CM_IMP_PO_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CM_IMP_PO_H_ID"]);
                        ob.SCM_PURC_REQ_D_YRN_ID = (dr["SCM_PURC_REQ_D_YRN_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_PURC_REQ_D_YRN_ID"]);
                        ob.INV_ITEM_ID = (dr["INV_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_ITEM_ID"]);
                        ob.LK_YRN_COLR_GRP_ID = (dr["LK_YRN_COLR_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_YRN_COLR_GRP_ID"]);
                        ob.RF_BRAND_ID = (dr["RF_BRAND_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_BRAND_ID"]);
                        ob.PO_QTY = (dr["PO_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PO_QTY"]);
                        ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);
                        ob.UNIT_PRICE = (dr["UNIT_PRICE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["UNIT_PRICE"]);
                        ob.LOT_REF_NO = (dr["LOT_REF_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LOT_REF_NO"]);
                        ob.DELV_TGT_DT = (dr["DELV_TGT_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["DELV_TGT_DT"]);
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
    }
}