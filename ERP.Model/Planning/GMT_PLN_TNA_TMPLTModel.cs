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

    public class GMT_PLN_TNA_BUYER
    {

        public string BUYER_NAME_EN { get; set; }
        public Int32 MC_BUYER_ID { get; set; }

        private List<MC_TNA_TMPLT_HModel> _templates = null;
        public List<MC_TNA_TMPLT_HModel> templates
        {
            get
            {
                if (_templates == null)
                {
                    _templates = new List<MC_TNA_TMPLT_HModel>();
                }
                return _templates;
            }
            set
            {
                _templates = value;
            }
        }
        public string XML { get; set; }

        public GMT_PLN_TNA_BUYER getGmtPlanCpm(int? pMC_BUYER_ID, Int32? pPARENT_ID,string pMC_TNA_TMPLT_H_LST )
        {
            string sp = "PKG_GMT_PLN_TNA.gmt_pln_tna_tmplt_select";
            //pOption=3000=>Select All Data
            try
            {
                OraDatabase db = new OraDatabase();
                GMT_PLN_TNA_BUYER ob = new GMT_PLN_TNA_BUYER();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_BUYER_ID", Value =pMC_BUYER_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =  3001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {

                    ob.MC_BUYER_ID = (dr["MC_BUYER_ID"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["MC_BUYER_ID"]);
                    ob.BUYER_NAME_EN = (dr["BUYER_NAME_EN"] == DBNull.Value) ? String.Empty : Convert.ToString(dr["BUYER_NAME_EN"]);
                    ob.templates = new MC_TNA_TMPLT_HModel().getTemplatesByBuyer(pMC_BUYER_ID, pPARENT_ID, pMC_TNA_TMPLT_H_LST);
                }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string Save()
        {
            const string sp = "PKG_GMT_PLN_TNA.gmt_pln_tna_tmplt_save";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_BUYER_ID", Value = ob.MC_BUYER_ID},
                     new CommandParameter() {ParameterName = "pXML", Value = ob.XML},

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
    public class GMT_PLN_TNA_TMPLTModel
    {
        public Int64 GMT_PLN_TNA_TMPLT_ID { get; set; }
        public Int64 MC_TNA_TMPLT_H_ID { get; set; }
        public Int64 SERIAL_NO { get; set; }
        public Int64 MC_TNA_TASK_D_ID { get; set; }
        public Int64? PARENT_TNA_TASK_D_ID { get; set; }
        public Int64? STD_DAYS { get; set; }
        public Int64 MAX_DLAY_ALOW { get; set; }

        public string IS_ST_END { get; set; }
        public string IS_CRITICAL_PROC { get; set; }
        public string TA_TASK_NAME_EN { get; set; }
        public Int32 DISPLAY_ORDER { get; set; }



        public List<GMT_PLN_TNA_TMPLTModel> Query(Int64 pMC_TNA_TMPLT_H_ID, Int64? pPARENT_ID)
        {
            string sp = "PKG_GMT_PLN_TNA.gmt_pln_tna_tmplt_select";
            //pOption=3000=>Select All Data
            try
            {
                var obList = new List<GMT_PLN_TNA_TMPLTModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_TNA_TMPLT_H_ID", Value = pMC_TNA_TMPLT_H_ID},
                     new CommandParameter() {ParameterName = "pPARENT_ID", Value = pPARENT_ID},
                     new CommandParameter() {ParameterName = "pOption", Value = pPARENT_ID.Equals(null)? 3000 : 3003},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    GMT_PLN_TNA_TMPLTModel ob = new GMT_PLN_TNA_TMPLTModel();
                    ob.GMT_PLN_TNA_TMPLT_ID = (dr["GMT_PLN_TNA_TMPLT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_PLN_TNA_TMPLT_ID"]);
                    ob.SERIAL_NO = (dr["SERIAL_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SERIAL_NO"]);
                    ob.MC_TNA_TMPLT_H_ID = (dr["MC_TNA_TMPLT_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_TNA_TMPLT_H_ID"]);
                    ob.MC_TNA_TASK_D_ID = (dr["MC_TNA_TASK_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_TNA_TASK_D_ID"]);
                    if (dr["PARENT_TNA_TASK_D_ID"] != DBNull.Value)
                    {
                        ob.PARENT_TNA_TASK_D_ID = Convert.ToInt64(dr["PARENT_TNA_TASK_D_ID"]);
                    }

                    if (dr["PRIOR_TNA_TASK_D_ID"] != DBNull.Value)
                    {
                        ob.PRIOR_TNA_TASK_D_ID = Convert.ToInt64(dr["PRIOR_TNA_TASK_D_ID"]);
                    }

                    if (dr["STD_DAYS"] != DBNull.Value)
                    {
                        ob.STD_DAYS = Convert.ToInt64(dr["STD_DAYS"]);
                    }

                    ob.IS_ST_END = (dr["IS_ST_END"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_ST_END"]);

                    ob.IS_CRITICAL_PROC = (dr["IS_CRITICAL_PROC"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_CRITICAL_PROC"]);

                    ob.TA_TASK_NAME_EN = (dr["TA_TASK_NAME_EN"] == DBNull.Value) ? "N/A" : Convert.ToString(dr["TA_TASK_NAME_EN"]);
                    ob.MAX_DLAY_ALOW = (dr["MAX_DLAY_ALOW"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MAX_DLAY_ALOW"]);
                    ob.DISPLAY_ORDER = (dr["DISPLAY_ORDER"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["DISPLAY_ORDER"]);

                    obList.Add(ob);
                }

                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<dynamic> getTnaTasksDs()
        {
            string sp = "PKG_GMT_PLN_TNA.gmt_pln_tna_tmplt_select";
            try
            {
                var obList = new List<dynamic>();

                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                 {
                     new CommandParameter() {ParameterName = "pOption", Value = 3002},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);

                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        obList.Add(new
                        {
                            MC_TNA_TASK_D_ID = Convert.ToInt64(dr["MC_TNA_TASK_D_ID"]),
                            TA_TASK_NAME_EN = Convert.ToString(dr["TA_TASK_NAME_EN"])
                        });
                    }
               
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public long? PRIOR_TNA_TASK_D_ID { get; set; }
    }
}