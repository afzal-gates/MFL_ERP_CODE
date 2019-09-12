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
    public class MC_TNA_TMPLT_DModel
    {
        public Int64 MC_TNA_TMPLT_D_ID { get; set; }
        public Int64 MC_TNA_TMPLT_H_ID { get; set; }
        public Int64 TASK_ORD_NO { get; set; }
        public Int64 MC_TNA_TASK_ID { get; set; }
        public Int64? PARENT_TASK_ID { get; set; }
        public Int64 STD_DAYS { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public string PARENT_TASK_NAME { get; set; }

        public Int64? MC_ORD_TNA_ID { get; set; }

        public string IS_CHECKED { get; set; }

        public string IS_START_TASK { get; set; }

        public string IS_END_TASK { get; set; }
        public DateTime? PLAN_START_DT { get; set; }
        public DateTime? PLAN_END_DT { get; set; }
        public string REMARKS { get; set; }
        public string IS_PRODUCTION { get; set; }
        //public Int64? RQD_DAYS { get; set; }
        public Int64? MC_ORDER_H_ID { get; set; }
        public string XML { get; set; }
        public string IS_TNA_FINALIZED { get; set; }
        public string IS_ST_END { get; set; }
        public string IS_ST_END_BASE { get; set; }
        public string IS_ACTIVE { get; set; }
        public string IS_SMPL_TASK { get; set; }
        public int DayMulTi { get; set; }
        public string IS_PRODUCTION_N { get; set; }
        public string IS_PRODUCTION_B { get; set; }



        public string Save()
        {
            const string sp = "pkg_tna.mc_tna_tmplt_d_insert";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pXML", Value = ob.XML},
                     new CommandParameter() {ParameterName = "pMC_TNA_TMPLT_H_ID", Value = ob.MC_TNA_TMPLT_H_ID},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value =  HttpContext.Current.Session["multiScUserId"]},
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
            const string sp = "pkg_tna.mc_tna_tmplt_d_update";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_TNA_TMPLT_D_ID", Value = ob.MC_TNA_TMPLT_D_ID},
                     new CommandParameter() {ParameterName = "pMC_TNA_TMPLT_H_ID", Value = ob.MC_TNA_TMPLT_H_ID},
                     new CommandParameter() {ParameterName = "pTASK_ORD_NO", Value = ob.TASK_ORD_NO},
                     new CommandParameter() {ParameterName = "pMC_TNA_TASK_ID", Value = ob.MC_TNA_TASK_ID},
                     new CommandParameter() {ParameterName = "pPARENT_TASK_ID", Value = ob.PARENT_TASK_ID},
                     new CommandParameter() {ParameterName = "pSTD_DAYS", Value = ob.STD_DAYS},
                    // new CommandParameter() {ParameterName = "pRQD_DAYS", Value = ob.RQD_DAYS},
                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = ob.CREATED_BY},
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
            const string sp = "pkg_tna.mc_tna_tmplt_d_delete";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_TNA_TMPLT_D_ID", Value = ob.MC_TNA_TMPLT_D_ID},
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

        public List<MC_TNA_TMPLT_DModel> SelectAll()
        {
            string sp = "pkg_tna.mc_tna_tmplt_d_select";
            try
            {
                var obList = new List<MC_TNA_TMPLT_DModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_TNA_TMPLT_D_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_TNA_TMPLT_DModel ob = new MC_TNA_TMPLT_DModel();
                    ob.MC_TNA_TMPLT_D_ID = (dr["MC_TNA_TMPLT_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_TNA_TMPLT_D_ID"]);
                    ob.MC_TNA_TMPLT_H_ID = (dr["MC_TNA_TMPLT_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_TNA_TMPLT_H_ID"]);
                    ob.TASK_ORD_NO = (dr["TASK_ORD_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["TASK_ORD_NO"]);
                    ob.MC_TNA_TASK_ID = (dr["MC_TNA_TASK_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_TNA_TASK_ID"]);
                    ob.PARENT_TASK_ID = (dr["PARENT_TASK_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PARENT_TASK_ID"]);
                    ob.STD_DAYS = (dr["STD_DAYS"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["STD_DAYS"]);
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public MC_TNA_TMPLT_DModel Select(Int64 ID)
        {
            string sp = "pkg_tna.mc_tna_tmplt_d_select";
            try
            {
                var ob = new MC_TNA_TMPLT_DModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_TNA_TMPLT_D_ID", Value = ID },
                     new CommandParameter() {ParameterName = "pOption", Value =3001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.MC_TNA_TMPLT_D_ID = (dr["MC_TNA_TMPLT_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_TNA_TMPLT_D_ID"]);
                    ob.MC_TNA_TMPLT_H_ID = (dr["MC_TNA_TMPLT_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_TNA_TMPLT_H_ID"]);
                    ob.TASK_ORD_NO = (dr["TASK_ORD_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["TASK_ORD_NO"]);
                    ob.MC_TNA_TASK_ID = (dr["MC_TNA_TASK_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_TNA_TASK_ID"]);
                    ob.PARENT_TASK_ID = (dr["PARENT_TASK_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PARENT_TASK_ID"]);
                    ob.STD_DAYS = (dr["STD_DAYS"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["STD_DAYS"]);
                }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<MC_TNA_TMPLT_DModel> parentTasksList(Int64 MC_TNA_TMPLT_H_ID)
        {
            string sp = "pkg_tna.mc_tna_tmplt_d_select";
            try
            {
                var obList = new List<MC_TNA_TMPLT_DModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_TNA_TMPLT_H_ID", Value = MC_TNA_TMPLT_H_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3002},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_TNA_TMPLT_DModel ob = new MC_TNA_TMPLT_DModel();
                    ob.MC_TNA_TMPLT_D_ID = (dr["MC_TNA_TMPLT_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_TNA_TMPLT_D_ID"]);
                    ob.MC_TNA_TMPLT_H_ID = (dr["MC_TNA_TMPLT_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_TNA_TMPLT_H_ID"]);
                    ob.TASK_ORD_NO = (dr["TASK_ORD_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["TASK_ORD_NO"]);
                    ob.MC_TNA_TASK_ID = (dr["MC_TNA_TASK_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_TNA_TASK_ID"]);

                    if ((dr["PARENT_TASK_ID"] != DBNull.Value))
                    {
                        ob.PARENT_TASK_ID = Convert.ToInt64(dr["PARENT_TASK_ID"]);
                    }
                    ob.STD_DAYS = (dr["STD_DAYS"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["STD_DAYS"]);
                    ob.TA_TASK_NAME_EN = (dr["TA_TASK_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TA_TASK_NAME_EN"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string TA_TASK_NAME_EN { get; set; }

        public List<MC_TNA_TMPLT_DModel> treelistData(Int64 MC_TNA_TMPLT_H_ID)
        {
            string sp = "pkg_tna.mc_tna_tmplt_d_select";
            try
            {
                var obList = new List<MC_TNA_TMPLT_DModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_TNA_TMPLT_H_ID", Value = MC_TNA_TMPLT_H_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3002},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_TNA_TMPLT_DModel ob = new MC_TNA_TMPLT_DModel();
                    ob.MC_TNA_TMPLT_D_ID = (dr["MC_TNA_TMPLT_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_TNA_TMPLT_D_ID"]);
                    ob.MC_TNA_TMPLT_H_ID = (dr["MC_TNA_TMPLT_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_TNA_TMPLT_H_ID"]);
                    ob.TASK_ORD_NO = (dr["TASK_ORD_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["TASK_ORD_NO"]);
                    ob.MC_TNA_TASK_ID = (dr["MC_TNA_TASK_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_TNA_TASK_ID"]);

                    if ((dr["PARENT_TASK_ID"] != DBNull.Value))
                    {
                        ob.PARENT_TASK_ID = Convert.ToInt64(dr["PARENT_TASK_ID"]);
                    }
                    ob.STD_DAYS = (dr["STD_DAYS"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["STD_DAYS"]);

                    if (ob.STD_DAYS < 0)
                    {
                        ob.STD_DAYS = ob.STD_DAYS * -1;
                        ob.DayMulTi = -1;
                    }
                    else
                    {
                        ob.DayMulTi = 1;
                    }

                    ob.TA_TASK_NAME_EN = (dr["TA_TASK_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TA_TASK_NAME_EN"]);
                    ob.IS_END_TASK = (dr["IS_END_TASK"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_END_TASK"]);
                    ob.IS_START_TASK = (dr["IS_START_TASK"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_START_TASK"]);
                    ob.IS_PRODUCTION_N = (dr["IS_PRODUCTION_N"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_PRODUCTION_N"]);
                    ob.IS_PRODUCTION_B = (dr["IS_PRODUCTION_B"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_PRODUCTION_B"]);
                    ob.IS_ST_END_BASE = (dr["IS_ST_END_BASE"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_ST_END_BASE"]);
                    ob.IS_ST_END = (dr["IS_ST_END"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_ST_END"]);
                    ob.PARENT_TASK_NAME = (dr["PARENT_TASK_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PARENT_TASK_NAME"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<MC_TNA_TMPLT_DModel> TnAListByTemplate(Int64 MC_TNA_TMPLT_H_ID, Int64 MC_ORDER_H_ID, Int64 MC_BUYER_ID)
        {
            string sp = "pkg_tna.mc_tna_tmplt_h_select";
            try
            {
                var obList = new List<MC_TNA_TMPLT_DModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_TNA_TMPLT_H_ID", Value =MC_TNA_TMPLT_H_ID},
                     new CommandParameter() {ParameterName = "pMC_BUYER_ID", Value =MC_BUYER_ID},
                     new CommandParameter() {ParameterName = "pMC_ORDER_H_ID", Value =MC_ORDER_H_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3003},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_TNA_TMPLT_DModel ob = new MC_TNA_TMPLT_DModel();

                    ob.IS_ST_END_BASE = (dr["IS_ST_END_BASE"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_ST_END_BASE"]);
                    ob.IS_ST_END = (dr["IS_ST_END"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_ST_END"]);

                    ob.MC_TNA_TMPLT_D_ID = (dr["MC_TNA_TMPLT_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_TNA_TMPLT_D_ID"]);
                    ob.MC_TNA_TMPLT_H_ID = (dr["MC_TNA_TMPLT_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_TNA_TMPLT_H_ID"]);
                    ob.TASK_ORD_NO = (dr["TASK_ORDER"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["TASK_ORDER"]);
                    ob.MC_TNA_TASK_ID = (dr["MC_TNA_TASK_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_TNA_TASK_ID"]);

                    ob.MC_TNA_TASK_D_ID = (dr["MC_TNA_TASK_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_TNA_TASK_D_ID"]);


                    if (dr["PARENT_TASK_ID"] != DBNull.Value)
                    {
                        ob.PARENT_TASK_ID = Convert.ToInt64(dr["PARENT_TASK_ID"]);
                    }


                    ob.MC_ORD_TNA_D_ID = (dr["MC_ORD_TNA_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_ORD_TNA_D_ID"]);
                    ob.STD_DAYS = (dr["STD_DAYS"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["STD_DAYS"]);


                    ob.MC_ORD_TNA_ID = (dr["MC_ORD_TNA_ID"] == DBNull.Value) ? -1 : Convert.ToInt64(dr["MC_ORD_TNA_ID"]);
                    ob.IS_CHECKED = (dr["IS_CHECKED"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_CHECKED"]);
                    ob.IS_START_TASK = (dr["IS_START_TASK"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_START_TASK"]);
                    ob.IS_END_TASK = (dr["IS_END_TASK"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_END_TASK"]);

                    ob.TA_TASK_NAME_EN = (dr["TA_TASK_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TA_TASK_NAME_EN"]);
                    ob.PARENT_TASK_NAME = (dr["PARENT_TASK_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PARENT_TASK_NAME"]);

                    if (dr["PLAN_DT"] != DBNull.Value)
                    {
                        ob.PLAN_DT = Convert.ToDateTime(dr["PLAN_DT"]);
                    }

                    ob.IS_SMPL_TASK = (dr["IS_SMPL_TASK"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_SMPL_TASK"]);
                    ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);
                    ob.IS_PRODUCTION = (dr["IS_PRODUCTION"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_PRODUCTION"]);
                    ob.IS_MANDATORY = (dr["IS_MANDATORY"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_MANDATORY"]);
                    ob.TA_TASK_SL = (dr["TA_TASK_SL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["TA_TASK_SL"]);

                    ob.STD_DAYS_ORI = ob.STD_DAYS;

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<MC_TNA_TMPLT_DModel> TnAListByTemplateDev(Int64 MC_TNA_TMPLT_H_ID, Int64 MC_ORDER_H_ID, Int64 MC_BUYER_ID)
        {
            string sp = "pkg_tna.GET_TNA_GEN_DATA";
            try
            {
                var obList = new List<MC_TNA_TMPLT_DModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_TNA_TMPLT_H_ID", Value =MC_TNA_TMPLT_H_ID},
                     new CommandParameter() {ParameterName = "pMC_BUYER_ID", Value =MC_BUYER_ID},
                     new CommandParameter() {ParameterName = "pMC_ORDER_H_ID", Value =MC_ORDER_H_ID},
                     new CommandParameter() {ParameterName = "pSC_USER_ID", Value =  HttpContext.Current.Session["multiScUserId"]},

                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_TNA_TMPLT_DModel ob = new MC_TNA_TMPLT_DModel();

                    ob.IS_ST_END_BASE = (dr["IS_ST_END_BASE"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_ST_END_BASE"]);
                    ob.IS_ST_END = (dr["IS_ST_END"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_ST_END"]);

                    ob.MC_TNA_TMPLT_D_ID = (dr["MC_TNA_TMPLT_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_TNA_TMPLT_D_ID"]);
                    ob.MC_TNA_TMPLT_H_ID = (dr["MC_TNA_TMPLT_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_TNA_TMPLT_H_ID"]);
                    ob.TASK_ORD_NO = (dr["TASK_ORDER"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["TASK_ORDER"]);
                    ob.MC_TNA_TASK_ID = (dr["MC_TNA_TASK_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_TNA_TASK_ID"]);

                    if (dr["PARENT_TASK_ID"] != DBNull.Value)
                    {
                        ob.PARENT_TASK_ID = Convert.ToInt64(dr["PARENT_TASK_ID"]);
                    }

                    ob.MC_ORD_TNA_D_ID = (dr["MC_ORD_TNA_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_ORD_TNA_D_ID"]);
                    ob.STD_DAYS = (dr["STD_DAYS"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["STD_DAYS"]);

                    ob.MC_ORD_TNA_ID = (dr["MC_ORD_TNA_ID"] == DBNull.Value) ? -1 : Convert.ToInt64(dr["MC_ORD_TNA_ID"]);
                    ob.IS_CHECKED = (dr["IS_CHECKED"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_CHECKED"]);
                    ob.IS_START_TASK = (dr["IS_START_TASK"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_START_TASK"]);
                    ob.IS_END_TASK = (dr["IS_END_TASK"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_END_TASK"]);
                    ob.TA_TASK_NAME_EN = (dr["TA_TASK_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TA_TASK_NAME_EN"]);
                    ob.PARENT_TASK_NAME = (dr["PARENT_TASK_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PARENT_TASK_NAME"]);
                    ob.TA_TASK_NAME_EN += ob.IS_ST_END == "S" ? " Start" : ob.IS_ST_END == "E" ? " End" : "";
                    ob.PARENT_TASK_NAME += ob.IS_ST_END_BASE == "S" ? " Start" : ob.IS_ST_END_BASE == "E" ? " End" : "";
                    ob.IS_SMPL_TASK = (dr["IS_SMPL_TASK"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_SMPL_TASK"]);
                    ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);
                    ob.IS_PRODUCTION = (dr["IS_PRODUCTION"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_PRODUCTION"]);
                    ob.IS_MANDATORY = (dr["IS_MANDATORY"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_MANDATORY"]);
                    ob.TA_TASK_SL = (dr["TA_TASK_SL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["TA_TASK_SL"]);

                    if (dr["PLAN_START_DT"] != DBNull.Value)
                    {
                        ob.PLAN_START_DT = Convert.ToDateTime(dr["PLAN_START_DT"]);
                    }

                    if (dr["PLAN_END_DT"] != DBNull.Value)
                    {
                        ob.PLAN_END_DT = Convert.ToDateTime(dr["PLAN_END_DT"]);
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

        public List<MC_TNA_TMPLT_DModel> getSampleTasksByOrder(Int64 pMC_ORDER_H_ID)
        {
            string sp = "pkg_tna.mc_tna_task_select_for_rpt";
            try
            {
                var obList = new List<MC_TNA_TMPLT_DModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_ORDER_H_ID", Value = pMC_ORDER_H_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_TNA_TMPLT_DModel ob = new MC_TNA_TMPLT_DModel();

                    ob.TA_TASK_NAME_EN = (dr["TA_TASK_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TA_TASK_NAME_EN"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);


                    if (dr["PLAN_START_DT"] != DBNull.Value)
                    {
                        ob.PLAN_START_DT = Convert.ToDateTime(dr["PLAN_START_DT"]);
                    }

                    if (dr["PLAN_END_DT"] != DBNull.Value)
                    {
                        ob.PLAN_END_DT = Convert.ToDateTime(dr["PLAN_END_DT"]);
                    }


                    if (dr["ACT_START_DT"] != DBNull.Value)
                    {
                        ob.ACT_START_DT = Convert.ToDateTime(dr["ACT_START_DT"]);
                    }

                    if (dr["ACT_END_DT"] != DBNull.Value)
                    {
                        ob.ACT_END_DT = Convert.ToDateTime(dr["ACT_END_DT"]);
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

        public List<MC_TNA_TMPLT_DModel> getStrikeOffTasksByOrder(Int64 pMC_ORDER_H_ID)
        {
            string sp = "pkg_tna.mc_tna_task_select_for_rpt";
            try
            {
                var obList = new List<MC_TNA_TMPLT_DModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_ORDER_H_ID", Value = pMC_ORDER_H_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3002},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_TNA_TMPLT_DModel ob = new MC_TNA_TMPLT_DModel();

                    ob.TA_TASK_NAME_EN = (dr["TA_TASK_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TA_TASK_NAME_EN"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);


                    if (dr["PLAN_START_DT"] != DBNull.Value)
                    {
                        ob.PLAN_START_DT = Convert.ToDateTime(dr["PLAN_START_DT"]);
                    }

                    if (dr["PLAN_END_DT"] != DBNull.Value)
                    {
                        ob.PLAN_END_DT = Convert.ToDateTime(dr["PLAN_END_DT"]);
                    }


                    if (dr["ACT_START_DT"] != DBNull.Value)
                    {
                        ob.ACT_START_DT = Convert.ToDateTime(dr["ACT_START_DT"]);
                    }

                    if (dr["ACT_END_DT"] != DBNull.Value)
                    {
                        ob.ACT_END_DT = Convert.ToDateTime(dr["ACT_END_DT"]);
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



        public string SaveTnATemplateData()
        {
            const string sp = "pkg_tna.mc_tna_tmplt_d_update";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_ORDER_H_ID", Value = ob.MC_ORDER_H_ID},
                     new CommandParameter() {ParameterName = "pIS_TNA_FINALIZED", Value = ob.IS_TNA_FINALIZED},
                     new CommandParameter() {ParameterName = "pMC_TNA_TMPLT_H_ID", Value = ob.MC_TNA_TMPLT_H_ID},
                     new CommandParameter() {ParameterName = "pXML", Value = ob.XML},
                     new CommandParameter() {ParameterName = "pXMLD", Value = ob.XMLD},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pOption", Value =2001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output},
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

        public long TA_TASK_SL { get; set; }

        public string IS_MANDATORY { get; set; }

        public long MC_ORD_TNA_D_ID { get; set; }

        public string XMLD { get; set; }

        public DateTime? ACT_START_DT { get; set; }

        public DateTime? ACT_END_DT { get; set; }

        public DateTime? PLAN_DT { get; set; }

        public long STD_DAYS_ORI { get; set; }

        public long MC_TNA_TASK_D_ID { get; set; }
    }
}