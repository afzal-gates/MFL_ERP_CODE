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
    public class KNT_CLCF_STR_RCVModel
    {
        public Int64 KNT_CLCF_STR_RCV_ID { get; set; }
        public Int64 SCM_STORE_ID { get; set; }
        public DateTime RCV_DT { get; set; }
        public Int64 PRD_QTY { get; set; }
        public Int64 RCV_QTY { get; set; }
        public Int64 REJ_QTY { get; set; }
        public string IS_G_F { get; set; }
        public Int64 KNT_CLCF_STYL_ITEM_ID { get; set; }
        public Int64 RCV_NO_ROLL { get; set; }
        public Decimal RCV_ROLL_WT { get; set; }
        public Int64 WT_MOU_ID { get; set; }
        public string REMARKS { get; set; }
        public string IS_FINALIZE { get; set; }
        public string XML_DTL { get; set; }
        

        public string BYR_ACC_NAME_EN { get; set; }
        public string COLOR_NAME_EN { get; set; }
        public string GARM_PART_NAME { get; set; }
        public string SIZE_CODE { get; set; }
        public string MESUREMENT { get; set; }
        

        

        public string BatchSave()
        {
            const string sp = "pkg_knit_collar_cuff.knt_clcf_str_rcv_save";
            string jsonStr="{";
            var ob = this;
            var i = 1;
            try
             {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     //new CommandParameter() {ParameterName = "pKNT_CLCF_STR_RCV_ID", Value = ob.KNT_CLCF_STR_RCV_ID},
                     new CommandParameter() {ParameterName = "pSCM_STORE_ID", Value = ob.SCM_STORE_ID},
                     new CommandParameter() {ParameterName = "pRCV_DT", Value = ob.RCV_DT},
                     //new CommandParameter() {ParameterName = "pIS_G_F", Value = ob.IS_G_F},
                     //new CommandParameter() {ParameterName = "pKNT_CLCF_STYL_ITEM_ID", Value = ob.KNT_CLCF_STYL_ITEM_ID},
                     //new CommandParameter() {ParameterName = "pRCV_NO_ROLL", Value = ob.RCV_NO_ROLL},
                     //new CommandParameter() {ParameterName = "pRCV_ROLL_WT", Value = ob.RCV_ROLL_WT},
                     //new CommandParameter() {ParameterName = "pWT_MOU_ID", Value = ob.WT_MOU_ID},
                     //new CommandParameter() {ParameterName = "pREMARKS", Value = ob.REMARKS},
                     new CommandParameter() {ParameterName = "pXML_DTL", Value = ob.XML_DTL},
                     new CommandParameter() {ParameterName = "pIS_FINALIZE", Value = ob.IS_FINALIZE},
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
                         i++ ;
                 }
                 return jsonStr;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public List<KNT_CLCF_STR_RCVModel> GetCollarCuff4StrRcv(DateTime? pRCV_DT, Int32? pSCM_STORE_ID, Int64? pMC_FAB_PROD_ORD_H_ID, Int64? pMC_STYLE_H_EXT_ID, Int64? pMC_COLOR_ID)
        {
            string sp = "pkg_knit_collar_cuff.knt_clcf_str_rcv_select";
            try
            {
                var obList = new List<KNT_CLCF_STR_RCVModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                    new CommandParameter() {ParameterName = "pRCV_DT", Value = pRCV_DT}, 
                    new CommandParameter() {ParameterName = "pSCM_STORE_ID", Value = pSCM_STORE_ID},
                    new CommandParameter() {ParameterName = "pMC_FAB_PROD_ORD_H_ID", Value = pMC_FAB_PROD_ORD_H_ID},
                     new CommandParameter() {ParameterName = "pMC_STYLE_H_EXT_ID", Value = pMC_STYLE_H_EXT_ID},
                     new CommandParameter() {ParameterName = "pMC_COLOR_ID", Value = pMC_COLOR_ID},
                     
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
              foreach (DataRow dr in ds.Tables[0].Rows)
              {
                  KNT_CLCF_STR_RCVModel ob = new KNT_CLCF_STR_RCVModel();

                  ob.KNT_CLCF_STR_RCV_ID = (dr["KNT_CLCF_STR_RCV_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_CLCF_STR_RCV_ID"]);

                  if (dr["SCM_STORE_ID"] != DBNull.Value)
                    ob.SCM_STORE_ID = (dr["SCM_STORE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_STORE_ID"]);

                  ob.RCV_DT = (dr["RCV_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["RCV_DT"]);
                  ob.IS_G_F = (dr["IS_G_F"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_G_F"]);
                  
                  ob.KNT_CLCF_STYL_ITEM_ID = (dr["KNT_CLCF_STYL_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_CLCF_STYL_ITEM_ID"]);
                  
                  ob.RCV_NO_ROLL = (dr["RCV_NO_ROLL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RCV_NO_ROLL"]);
                  ob.RCV_ROLL_WT = (dr["RCV_ROLL_WT"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RCV_ROLL_WT"]);

                  if (dr["WT_MOU_ID"] != DBNull.Value)
                    ob.WT_MOU_ID = (dr["WT_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["WT_MOU_ID"]);
                  
                  ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                  ob.IS_FINALIZE = (dr["IS_FINALIZE"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_FINALIZE"]);

                  ob.PRD_QTY = (dr["PRD_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PRD_QTY"]);
                  ob.RCV_QTY = (dr["RCV_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RCV_QTY"]);
                  ob.REJ_QTY = (dr["REJ_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["REJ_QTY"]);

                  ob.BYR_ACC_NAME_EN = (dr["BYR_ACC_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BYR_ACC_NAME_EN"]);
                  ob.COLOR_NAME_EN = (dr["COLOR_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_NAME_EN"]);
                  ob.GARM_PART_NAME = (dr["GARM_PART_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["GARM_PART_NAME"]);
                  ob.SIZE_CODE = (dr["SIZE_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SIZE_CODE"]);
                  ob.MESUREMENT = (dr["MESUREMENT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MESUREMENT"]);

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