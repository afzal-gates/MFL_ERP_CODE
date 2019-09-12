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
    public class MC_SMP_FEEDBKModel
    {
        public Int64? MC_SMP_FEEDBK_ID { get; set; }
        public Int64? MC_SMP_REQ_D1_ID { get; set; }
        public Int64? REVISION_NO { get; set; }
        public Int64? MC_TNA_TASK_STATUS_ID { get; set; }
        public DateTime? COMMENTS_DT { get; set; }
        public string COMMENTS { get; set; }
        public string DTL_XML { get; set; }

        public Int64? MC_SMP_REQ_D_ID { get; set; }
        public string ITEM_SNAME { get; set; }
        public string COLOR_NAME_EN { get; set; }
        public string SIZE_CODE { get; set; }
        public string EVENT_NAME { get; set; }

        public string BatchSaveByrFeedback()
        {
            const string sp = "pkg_mc_fab_booking.mc_smp_feedbk_batch_save";
            string jsonStr="{";
            var ob = this;
            var i = 1;
            try
             {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                    new CommandParameter() {ParameterName = "pMC_SMP_REQ_D_ID", Value = ob.MC_SMP_REQ_D_ID}, 
                     new CommandParameter() {ParameterName = "pDTL_XML", Value = ob.DTL_XML}, 
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pOption", Value = 1000},
                     new CommandParameter() {ParameterName = "pMsg", Value = 500, Direction = ParameterDirection.Output}
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

        

        public List<MC_SMP_FEEDBKModel> SelectAll()
        {
            string sp = "Select_MC_SMP_FEEDBK";
            try
            {
                var obList = new List<MC_SMP_FEEDBKModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_SMP_FEEDBK_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
              foreach (DataRow dr in ds.Tables[0].Rows)
               {
                            MC_SMP_FEEDBKModel ob = new MC_SMP_FEEDBKModel();
                            ob.MC_SMP_FEEDBK_ID = (dr["MC_SMP_FEEDBK_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_SMP_FEEDBK_ID"]);
                            ob.MC_SMP_REQ_D1_ID = (dr["MC_SMP_REQ_D1_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_SMP_REQ_D1_ID"]);
                            ob.REVISION_NO = (dr["REVISION_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["REVISION_NO"]);
                            ob.MC_TNA_TASK_STATUS_ID = (dr["MC_TNA_TASK_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_TNA_TASK_STATUS_ID"]);
                            ob.COMMENTS_DT = (dr["COMMENTS_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["COMMENTS_DT"]);
                            ob.COMMENTS = (dr["COMMENTS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMMENTS"]);
                            
                            obList.Add(ob);
               }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public MC_SMP_FEEDBKModel Select(long ID)
        {
            string sp = "Select_MC_SMP_FEEDBK";
            try
            {
                var ob = new MC_SMP_FEEDBKModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_SMP_FEEDBK_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
              foreach (DataRow dr in ds.Tables[0].Rows)
               {
                        ob.MC_SMP_FEEDBK_ID = (dr["MC_SMP_FEEDBK_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_SMP_FEEDBK_ID"]);
                        ob.MC_SMP_REQ_D1_ID = (dr["MC_SMP_REQ_D1_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_SMP_REQ_D1_ID"]);
                        ob.REVISION_NO = (dr["REVISION_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["REVISION_NO"]);
                        ob.MC_TNA_TASK_STATUS_ID = (dr["MC_TNA_TASK_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_TNA_TASK_STATUS_ID"]);
                        ob.COMMENTS_DT = (dr["COMMENTS_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["COMMENTS_DT"]);
                        ob.COMMENTS = (dr["COMMENTS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMMENTS"]);
                        

               }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object SmplByrFeedbackStatus(long? pMC_SMP_REQ_D_ID = null)
        {
            string sp = "pkg_mc_fab_booking.mc_smp_feedbk_select";
            try
            {
                var obList = new List<MC_SMP_FEEDBKModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_SMP_REQ_D_ID", Value = pMC_SMP_REQ_D_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3003},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_SMP_FEEDBKModel ob = new MC_SMP_FEEDBKModel();

                    if (dr["REVISION_NO"] != DBNull.Value)
                        ob.REVISION_NO = (dr["REVISION_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["REVISION_NO"]);

                    //ob.MC_TNA_TASK_STATUS_ID = (dr["MC_TNA_TASK_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_TNA_TASK_STATUS_ID"]);
                    ob.COMMENTS_DT = (dr["COMMENTS_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["COMMENTS_DT"]);
                    ob.COMMENTS = (dr["COMMENTS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMMENTS"]);

                    ob.ITEM_SNAME = (dr["ITEM_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_SNAME"]);
                    ob.COLOR_NAME_EN = (dr["COLOR_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_NAME_EN"]);
                    ob.SIZE_CODE = (dr["SIZE_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SIZE_CODE"]);
                    ob.EVENT_NAME = (dr["EVENT_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EVENT_NAME"]);

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