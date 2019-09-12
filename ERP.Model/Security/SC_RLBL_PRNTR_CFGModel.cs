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
    public class SC_RLBL_PRNTR_CFGModel
    {
        public Int64 SC_RLBL_PRNTR_CFG_ID { get; set; }
        public string RLBL_PRNTR_NAME { get; set; }
        public string PRNTR_ADDRESS { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }

        public string Save()
        {
            const string sp = "SP_SC_RLBL_PRNTR_CFG";
            string jsonStr="{";
            var ob = this;
            var i = 1;
            try
             {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pSC_RLBL_PRNTR_CFG_ID", Value = ob.SC_RLBL_PRNTR_CFG_ID},
                     new CommandParameter() {ParameterName = "pRLBL_PRNTR_NAME", Value = ob.RLBL_PRNTR_NAME},
                     new CommandParameter() {ParameterName = "pPRNTR_ADDRESS", Value = ob.PRNTR_ADDRESS},
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
        public List<SC_RLBL_PRNTR_CFGModel> QueryDatas()
        {
            string sp = "pkg_common.sc_rlbl_prntr_cfg_select";
            try
            {
                var obList = new List<SC_RLBL_PRNTR_CFGModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
              foreach (DataRow dr in ds.Tables[0].Rows)
               {
                            SC_RLBL_PRNTR_CFGModel ob = new SC_RLBL_PRNTR_CFGModel();
                            ob.SC_RLBL_PRNTR_CFG_ID = (dr["SC_RLBL_PRNTR_CFG_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SC_RLBL_PRNTR_CFG_ID"]);
                            ob.RLBL_PRNTR_NAME = (dr["RLBL_PRNTR_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["RLBL_PRNTR_NAME"]);
                            ob.PRNTR_ADDRESS = (dr["PRNTR_ADDRESS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PRNTR_ADDRESS"]);
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