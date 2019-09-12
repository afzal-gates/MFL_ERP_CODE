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
    public class MC_TNA_TMPLT_HModel
    {
        public Int64 MC_TNA_TMPLT_H_ID { get; set; }
        public string TNA_TMPLT_CODE { get; set; }
        public Int64 LK_ORDER_TYPE_ID { get; set; }
        public Int64? MC_BUYER_ID { get; set; }
        public Int64 LK_GARM_TYPE_ID { get; set; }
        public Int64 LEAD_TIME_FROM { get; set; }
        public Int64 LEAD_TIME_TO { get; set; }
        public Int64 PREPARED_BY { get; set; }
        public Int64 APPROVED_BY { get; set; }
        public string REMARKS { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }

        public string BUYER_NAME_EN { get; set; }

        public string LK_ORDER_TYPE { get; set; }

        public string LK_GARM_TYPE { get; set; }
        public string XML { get; set; }

        public Int64 STD_LEAD_TIME { get; set; }



        private List<GMT_PLN_TNA_TMPLTModel> _tasks = null;
        public List<GMT_PLN_TNA_TMPLTModel> tasks
        {
            get
            {
                if (_tasks == null)
                {
                    _tasks = new List<GMT_PLN_TNA_TMPLTModel>();
                }
                return _tasks;
            }
            set
            {
                _tasks = value;
            }
        }

        public string Save()
        {
            const string sp = "pkg_tna.mc_tna_tmplt_h_insert";
            string jsonStr="{";
            var ob = this;
            var i = 1;
            try
             {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_TNA_TMPLT_H_ID", Value = ob.MC_TNA_TMPLT_H_ID},
                     new CommandParameter() {ParameterName = "pTNA_TMPLT_CODE", Value = ob.TNA_TMPLT_CODE},
                     new CommandParameter() {ParameterName = "pLK_ORDER_TYPE_ID", Value = ob.LK_ORDER_TYPE_ID},
                     new CommandParameter() {ParameterName = "pMC_BUYER_ID", Value = ob.MC_BUYER_ID},
                     new CommandParameter() {ParameterName = "pLK_GARM_TYPE_ID", Value = ob.LK_GARM_TYPE_ID},
                     new CommandParameter() {ParameterName = "pLEAD_TIME_FROM", Value = ob.LEAD_TIME_FROM},
                     new CommandParameter() {ParameterName = "pLEAD_TIME_TO", Value = ob.LEAD_TIME_TO},
                     new CommandParameter() {ParameterName = "pPREPARED_BY", Value = ob.PREPARED_BY},
                     new CommandParameter() {ParameterName = "pAPPROVED_BY", Value = ob.APPROVED_BY},
                     new CommandParameter() {ParameterName = "pREMARKS", Value = ob.REMARKS},
                     new CommandParameter() {ParameterName = "pSTD_LEAD_TIME", Value = ob.STD_LEAD_TIME},
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

        public string Update()
        {
            const string sp = "pkg_tna.mc_tna_tmplt_h_update";
            string jsonStr="{";
            var ob = this;
            var i = 1;
            try
             {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_TNA_TMPLT_H_ID", Value = ob.MC_TNA_TMPLT_H_ID},
                     new CommandParameter() {ParameterName = "pTNA_TMPLT_CODE", Value = ob.TNA_TMPLT_CODE},
                     new CommandParameter() {ParameterName = "pLK_ORDER_TYPE_ID", Value = ob.LK_ORDER_TYPE_ID},
                     new CommandParameter() {ParameterName = "pMC_BUYER_ID", Value = ob.MC_BUYER_ID},
                     new CommandParameter() {ParameterName = "pLK_GARM_TYPE_ID", Value = ob.LK_GARM_TYPE_ID},
                     new CommandParameter() {ParameterName = "pLEAD_TIME_FROM", Value = ob.LEAD_TIME_FROM},
                     new CommandParameter() {ParameterName = "pLEAD_TIME_TO", Value = ob.LEAD_TIME_TO},
                     new CommandParameter() {ParameterName = "pPREPARED_BY", Value = ob.PREPARED_BY},
                     new CommandParameter() {ParameterName = "pAPPROVED_BY", Value = ob.APPROVED_BY},
                     new CommandParameter() {ParameterName = "pREMARKS", Value = ob.REMARKS},
                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = ob.CREATED_BY},
                     new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = ob.LAST_UPDATED_BY},
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
                    i++;  
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
            const string sp = "pkg_tna.mc_tna_tmplt_h_delete";
            string jsonStr="{";
            var ob = this;
            var i = 1;
            try
             {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_TNA_TMPLT_H_ID", Value = ob.MC_TNA_TMPLT_H_ID},
                     new CommandParameter() {ParameterName = "pTNA_TMPLT_CODE", Value = ob.TNA_TMPLT_CODE},
                     new CommandParameter() {ParameterName = "pLK_ORDER_TYPE_ID", Value = ob.LK_ORDER_TYPE_ID},
                     new CommandParameter() {ParameterName = "pMC_BUYER_ID", Value = ob.MC_BUYER_ID},
                     new CommandParameter() {ParameterName = "pLK_GARM_TYPE_ID", Value = ob.LK_GARM_TYPE_ID},
                     new CommandParameter() {ParameterName = "pLEAD_TIME_FROM", Value = ob.LEAD_TIME_FROM},
                     new CommandParameter() {ParameterName = "pLEAD_TIME_TO", Value = ob.LEAD_TIME_TO},
                     new CommandParameter() {ParameterName = "pPREPARED_BY", Value = ob.PREPARED_BY},
                     new CommandParameter() {ParameterName = "pAPPROVED_BY", Value = ob.APPROVED_BY},
                     new CommandParameter() {ParameterName = "pREMARKS", Value = ob.REMARKS},
                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = ob.CREATED_BY},
                     new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = ob.LAST_UPDATED_BY},
                     new CommandParameter() {ParameterName = "pOption", Value =4000},
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

        public List<MC_TNA_TMPLT_HModel> SelectAll()
        {
            string sp = "pkg_tna.mc_tna_tmplt_h_select";
            try
            {
                var obList = new List<MC_TNA_TMPLT_HModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_TNA_TMPLT_H_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
              foreach (DataRow dr in ds.Tables[0].Rows)
               {
                            MC_TNA_TMPLT_HModel ob = new MC_TNA_TMPLT_HModel();
                            ob.MC_TNA_TMPLT_H_ID = (dr["MC_TNA_TMPLT_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_TNA_TMPLT_H_ID"]);
                            ob.TNA_TMPLT_CODE = (dr["TNA_TMPLT_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TNA_TMPLT_CODE"]);
                            ob.LK_ORDER_TYPE_ID = (dr["LK_ORDER_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_ORDER_TYPE_ID"]);
                            ob.MC_BUYER_ID = (dr["MC_BUYER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BUYER_ID"]);
                            ob.LK_GARM_TYPE_ID = (dr["LK_GARM_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_GARM_TYPE_ID"]);
                            ob.LEAD_TIME_FROM = (dr["LEAD_TIME_FROM"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LEAD_TIME_FROM"]);
                            ob.LEAD_TIME_TO = (dr["LEAD_TIME_TO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LEAD_TIME_TO"]);
                            ob.PREPARED_BY = (dr["PREPARED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PREPARED_BY"]);
                            ob.APPROVED_BY = (dr["APPROVED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["APPROVED_BY"]);
                            ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                            ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);

                            ob.BUYER_NAME_EN = (dr["BUYER_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BUYER_NAME_EN"]);
                            ob.LK_ORDER_TYPE = (dr["LK_ORDER_TYPE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LK_ORDER_TYPE"]);
                            ob.LK_GARM_TYPE = (dr["LK_GARM_TYPE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LK_GARM_TYPE"]);
                            obList.Add(ob);
               }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<MC_TNA_TMPLT_HModel> getTemplatesByBuyer(Int32? pMC_BUYER_ID, Int32? pPARENT_ID, string pMC_TNA_TMPLT_H_LST)
        {
            string sp = "pkg_tna.mc_tna_tmplt_h_select";
            try
            {
                var obList = new List<MC_TNA_TMPLT_HModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_BUYER_ID", Value = pMC_BUYER_ID },
                     new CommandParameter() {ParameterName = "pPARENT_ID", Value = pPARENT_ID },
                     new CommandParameter() {ParameterName = "pMC_TNA_TMPLT_H_LST", Value = pMC_TNA_TMPLT_H_LST },
                     new CommandParameter() {ParameterName = "pOption", Value = pPARENT_ID.Equals(null)? 3006: 3007},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_TNA_TMPLT_HModel ob = new MC_TNA_TMPLT_HModel();
                    ob.MC_TNA_TMPLT_H_ID = (dr["MC_TNA_TMPLT_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_TNA_TMPLT_H_ID"]);
                    ob.TNA_TMPLT_CODE = (dr["TNA_TMPLT_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TNA_TMPLT_CODE"]);
                    ob.LK_ORDER_TYPE_ID = (dr["LK_ORDER_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_ORDER_TYPE_ID"]);
                    ob.MC_BUYER_ID = (dr["MC_BUYER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BUYER_ID"]);
                    ob.LK_GARM_TYPE_ID = (dr["LK_GARM_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_GARM_TYPE_ID"]);
                    ob.LEAD_TIME_FROM = (dr["LEAD_TIME_FROM"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LEAD_TIME_FROM"]);
                    ob.LEAD_TIME_TO = (dr["LEAD_TIME_TO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LEAD_TIME_TO"]);
                    ob.PREPARED_BY = (dr["PREPARED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PREPARED_BY"]);
                    ob.APPROVED_BY = (dr["APPROVED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["APPROVED_BY"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);

                    ob.BUYER_NAME_EN = (dr["BUYER_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BUYER_NAME_EN"]);
                    ob.LK_ORDER_TYPE = (dr["LK_ORDER_TYPE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LK_ORDER_TYPE"]);
                    ob.LK_GARM_TYPE = (dr["LK_GARM_TYPE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LK_GARM_TYPE"]);
                    
                    if (dr["PARENT_ID"] != DBNull.Value){
                        ob.PARENT_ID = Convert.ToInt64(dr["PARENT_ID"]);
                    }

                    ob.SHIP_AFTER_SEW = (dr["SHIP_AFTER_SEW"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["SHIP_AFTER_SEW"]);
                    ob.IN_BTWN_SEW = (dr["IN_BTWN_SEW"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["IN_BTWN_SEW"]);
                    ob.STD_LEAD_TIME = (dr["STD_LEAD_TIME"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["STD_LEAD_TIME"]);

                    if (!String.IsNullOrEmpty(pMC_TNA_TMPLT_H_LST))
                    {
                        ob.tasks = new GMT_PLN_TNA_TMPLTModel().Query(ob.MC_TNA_TMPLT_H_ID, ob.PARENT_ID);
                    }
                    

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<MC_TNA_TMPLT_HModel> GetTnAList(Int64? LK_ORDER_TYPE_ID, Int64? MC_BUYER_ID, Int64? LEAD_TIME)
        {
            string sp = "pkg_tna.mc_tna_tmplt_h_select";
            try
            {
                var obList = new List<MC_TNA_TMPLT_HModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pLK_ORDER_TYPE_ID", Value =LK_ORDER_TYPE_ID},
                     new CommandParameter() {ParameterName = "pMC_BUYER_ID", Value =MC_BUYER_ID},
                     new CommandParameter() {ParameterName = "pLEAD_TIME", Value =LEAD_TIME},
                     new CommandParameter() {ParameterName = "pOption", Value =3002},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_TNA_TMPLT_HModel ob = new MC_TNA_TMPLT_HModel();
                    ob.MC_TNA_TMPLT_H_ID = (dr["MC_TNA_TMPLT_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_TNA_TMPLT_H_ID"]);
                    ob.TNA_TMPLT_CODE = (dr["TNA_TMPLT_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TNA_TMPLT_CODE"]);
                    ob.MC_BUYER_ID = (dr["MC_BUYER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BUYER_ID"]);
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

        public MC_TNA_TMPLT_HModel Select(Int64 ID)
        {
            string sp = "pkg_tna.mc_tna_tmplt_h_select";
            try
            {
                var ob = new MC_TNA_TMPLT_HModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_TNA_TMPLT_H_ID", Value =ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
              foreach (DataRow dr in ds.Tables[0].Rows)
               {
                        ob.MC_TNA_TMPLT_H_ID = (dr["MC_TNA_TMPLT_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_TNA_TMPLT_H_ID"]);
                        ob.TNA_TMPLT_CODE = (dr["TNA_TMPLT_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TNA_TMPLT_CODE"]);
                        ob.LK_ORDER_TYPE_ID = (dr["LK_ORDER_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_ORDER_TYPE_ID"]);
                        ob.MC_BUYER_ID = (dr["MC_BUYER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BUYER_ID"]);
                        ob.LK_GARM_TYPE_ID = (dr["LK_GARM_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_GARM_TYPE_ID"]);
                        ob.LEAD_TIME_FROM = (dr["LEAD_TIME_FROM"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LEAD_TIME_FROM"]);
                        ob.LEAD_TIME_TO = (dr["LEAD_TIME_TO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LEAD_TIME_TO"]);
                        ob.PREPARED_BY = (dr["PREPARED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PREPARED_BY"]);
                        ob.APPROVED_BY = (dr["APPROVED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["APPROVED_BY"]);
                        ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                        ob.STD_LEAD_TIME = (dr["STD_LEAD_TIME"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["STD_LEAD_TIME"]);
               }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public String TnaRevise(Int64 MC_ORDER_H_ID, int REVISION_NO, DateTime REVISION_DT, string REV_REASON)
        {
            const string sp = "pkg_tna.mc_ord_tna_h_hist_insert";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_ORDER_H_ID", Value = MC_ORDER_H_ID},
                     new CommandParameter() {ParameterName = "pREVISION_NO", Value = REVISION_NO},
                     new CommandParameter() {ParameterName = "pREVISION_DT", Value = REVISION_DT},
                     new CommandParameter() {ParameterName = "pREV_REASON", Value = REV_REASON},
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
                    i++;
                }
                return jsonStr;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public long? PARENT_ID { get; set; }

        public int SHIP_AFTER_SEW { get; set; }

        public int IN_BTWN_SEW { get; set; }
    }
}