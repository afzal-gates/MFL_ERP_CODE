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
    public class MC_LD_SUBMITModel
    {
        public Int64 MC_LD_SUBMIT_ID { get; set; }
        public Int64 MC_LD_REQ_D_ID { get; set; }
        public Int64? RF_FIB_COMP_ID { get; set; }
        public DateTime SUBMIT_DT { get; set; }
        public Int64 REVISION_NO { get; set; }
        public string LD_REF1 { get; set; }
        public string LD_REF2 { get; set; }
        public string LD_REF3 { get; set; }
        public DateTime COMMENTS_DT { get; set; }
        public string COMMENTS { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }
        public string COLOR_NAME_EN { get; set; }
        public string FABRIC_DESC { get; set; }
        public string IS_ACTIVE { get; set; }

        public string APRV_LD_REF { get; set; }
        public Int64 MC_TNA_TASK_STATUS_ID { get; set; }

        public string IS_LD_REF1 { get; set; }
        public string IS_LD_REF2 { get; set; }
        public string IS_LD_REF3 { get; set; }
        public Int64 MC_ORDER_H_ID { get; set; }
        public string TASK_STATUS_NAME { get; set; }
        public Int64 MC_STYLE_H_ID { get; set; }
        public Int64 MC_LD_REQ_H_ID { get; set; }
        public Int64? MC_COLOR_ID { get; set; }
        public Int64 MC_COLOR_GRP_ID { get; set; }
        public Int64 LK_DYE_MTHD_ID { get; set; }
        public String IS_REPEAT { get; set; }
        public Int64? OLD_MC_STYLE_H_EXT_ID { get; set; }
        public string XML { get; set; }


        private List<MC_LD_SUBMIT_DModel> _ld_ref_list = null;

        public List<MC_LD_SUBMIT_DModel> ld_ref_list
        {
            get
            {
                if (_ld_ref_list == null)
                {
                    _ld_ref_list = new List<MC_LD_SUBMIT_DModel>();
                }
                return _ld_ref_list;
            }
            set
            {
                _ld_ref_list = value;
            }
        }

        public string submitLabdipData()
        {
            const string sp = "PKG_Labdip_action.mc_ld_submit_insert";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_LD_SUBMIT_ID", Value = ob.MC_LD_SUBMIT_ID},
                     new CommandParameter() {ParameterName = "pMC_LD_REQ_D_ID", Value = ob.MC_LD_REQ_D_ID},
                     new CommandParameter() {ParameterName = "pSUBMIT_DT", Value = ob.SUBMIT_DT},
                     new CommandParameter() {ParameterName = "pREVISION_NO", Value = ob.REVISION_NO},
                     new CommandParameter() {ParameterName = "pLD_REF1", Value = ob.LD_REF1},
                     new CommandParameter() {ParameterName = "pLD_REF2", Value = ob.LD_REF2},
                     new CommandParameter() {ParameterName = "pLD_REF3", Value = ob.LD_REF3},
                     new CommandParameter() {ParameterName = "pCOMMENTS_DT", Value = ob.COMMENTS_DT },
                     new CommandParameter() {ParameterName = "pCOMMENTS", Value = ob.COMMENTS},
                     new CommandParameter() {ParameterName = "pRF_FIB_COMP_ID", Value = ob.RF_FIB_COMP_ID},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pOption", Value =1000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output},

                     new CommandParameter() {ParameterName = "pMC_COLOR_GRP_ID", Value = ob.MC_COLOR_GRP_ID},
                     new CommandParameter() {ParameterName = "pLK_DYE_MTHD_ID", Value = ob.LK_DYE_MTHD_ID},
                     new CommandParameter() {ParameterName = "pIS_REPEAT", Value = ob.IS_REPEAT},
                     new CommandParameter() {ParameterName = "pOLD_MC_STYLE_H_EXT_ID", Value = ob.OLD_MC_STYLE_H_EXT_ID},

                     new CommandParameter() {ParameterName = "pAPRV_LD_REF", Value = ob.APRV_LD_REF},
                     new CommandParameter() {ParameterName = "pMC_TNA_TASK_STATUS_ID", Value = ob.MC_TNA_TASK_STATUS_ID},
                      new CommandParameter() {ParameterName = "V_MC_LD_SUBMIT_ID", Value =500, Direction = ParameterDirection.Output},
                      new CommandParameter() {ParameterName = "pXML", Value = ob.XML},
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
        public string updateLabdipData()
        {
            const string sp = "PKG_Labdip_action.mc_ld_submit_update";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_LD_SUBMIT_ID", Value = ob.MC_LD_SUBMIT_ID},
                     new CommandParameter() {ParameterName = "pMC_LD_REQ_D_ID", Value = ob.MC_LD_REQ_D_ID},
                     new CommandParameter() {ParameterName = "pSUBMIT_DT", Value = ob.SUBMIT_DT},
                     new CommandParameter() {ParameterName = "pREVISION_NO", Value = ob.REVISION_NO},
                     new CommandParameter() {ParameterName = "pLD_REF1", Value = ob.LD_REF1},
                     new CommandParameter() {ParameterName = "pLD_REF2", Value = ob.LD_REF2},
                     new CommandParameter() {ParameterName = "pLD_REF3", Value = ob.LD_REF3},
                     new CommandParameter() {ParameterName = "pCOMMENTS_DT", Value = ob.COMMENTS_DT },
                     new CommandParameter() {ParameterName = "pCOMMENTS", Value = ob.COMMENTS},
                     new CommandParameter() {ParameterName = "pRF_FIB_COMP_ID", Value = ob.RF_FIB_COMP_ID},
                     
                     new CommandParameter() {ParameterName = "pMC_COLOR_GRP_ID", Value = ob.MC_COLOR_GRP_ID},
                     new CommandParameter() {ParameterName = "pLK_DYE_MTHD_ID", Value = ob.LK_DYE_MTHD_ID},
                     new CommandParameter() {ParameterName = "pIS_REPEAT", Value = ob.IS_REPEAT},
                     new CommandParameter() {ParameterName = "pOLD_MC_STYLE_H_EXT_ID", Value = ob.OLD_MC_STYLE_H_EXT_ID},

                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pOption", Value =2000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output},
                     new CommandParameter() {ParameterName = "pAPRV_LD_REF", Value = ob.APRV_LD_REF},
                     new CommandParameter() {ParameterName = "pMC_TNA_TASK_STATUS_ID", Value = ob.MC_TNA_TASK_STATUS_ID},
                     new CommandParameter() {ParameterName = "pXML", Value = ob.XML}
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

        public string Save()
        {
            const string sp = "PKG_Labdip_action.mc_ld_submit_insert";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_LD_SUBMIT_ID", Value = ob.MC_LD_SUBMIT_ID},
                     new CommandParameter() {ParameterName = "pMC_LD_REQ_D_ID", Value = ob.MC_LD_REQ_D_ID},
                     new CommandParameter() {ParameterName = "pSUBMIT_DT", Value = ob.SUBMIT_DT},
                     new CommandParameter() {ParameterName = "pREVISION_NO", Value = ob.REVISION_NO},
                     new CommandParameter() {ParameterName = "pLD_REF1", Value = ob.LD_REF1},
                     new CommandParameter() {ParameterName = "pLD_REF2", Value = ob.LD_REF2},
                     new CommandParameter() {ParameterName = "pLD_REF3", Value = ob.LD_REF3},
                     new CommandParameter() {ParameterName = "pRF_FIB_COMP_ID", Value = ob.RF_FIB_COMP_ID},
                     new CommandParameter() {ParameterName = "pCOMMENTS_DT", Value = ob.COMMENTS_DT},
                     new CommandParameter() {ParameterName = "pCOMMENTS", Value = ob.COMMENTS},
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
            const string sp = "PKG_Labdip_action.mc_ld_submit_update";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_LD_SUBMIT_ID", Value = ob.MC_LD_SUBMIT_ID},
                     new CommandParameter() {ParameterName = "pMC_LD_REQ_D_ID", Value = ob.MC_LD_REQ_D_ID},
                     new CommandParameter() {ParameterName = "pSUBMIT_DT", Value = ob.SUBMIT_DT},
                     new CommandParameter() {ParameterName = "pREVISION_NO", Value = ob.REVISION_NO},
                     new CommandParameter() {ParameterName = "pLD_REF1", Value = ob.LD_REF1},
                     new CommandParameter() {ParameterName = "pLD_REF2", Value = ob.LD_REF2},
                     new CommandParameter() {ParameterName = "pLD_REF3", Value = ob.LD_REF3},
                     new CommandParameter() {ParameterName = "pCOMMENTS_DT", Value = ob.COMMENTS_DT},
                     new CommandParameter() {ParameterName = "pCOMMENTS", Value = ob.COMMENTS},
                     new CommandParameter() {ParameterName = "pRF_FIB_COMP_ID", Value = ob.RF_FIB_COMP_ID},

                     new CommandParameter() {ParameterName = "pMC_COLOR_GRP_ID", Value = ob.MC_COLOR_GRP_ID},
                     new CommandParameter() {ParameterName = "pLK_DYE_MTHD_ID", Value = ob.LK_DYE_MTHD_ID},
                     new CommandParameter() {ParameterName = "pIS_REPEAT", Value = ob.IS_REPEAT},
                     new CommandParameter() {ParameterName = "pOLD_MC_STYLE_H_EXT_ID", Value = ob.OLD_MC_STYLE_H_EXT_ID},

                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
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
            const string sp = "PKG_Labdip_action.mc_ld_submit_delete";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_LD_SUBMIT_ID", Value = ob.MC_LD_SUBMIT_ID},
                     new CommandParameter() {ParameterName = "pMC_LD_REQ_D_ID", Value = ob.MC_LD_REQ_D_ID},
                     new CommandParameter() {ParameterName = "pSUBMIT_DT", Value = ob.SUBMIT_DT},
                     new CommandParameter() {ParameterName = "pREVISION_NO", Value = ob.REVISION_NO},
                     new CommandParameter() {ParameterName = "pLD_REF1", Value = ob.LD_REF1},
                     new CommandParameter() {ParameterName = "pLD_REF2", Value = ob.LD_REF2},
                     new CommandParameter() {ParameterName = "pLD_REF3", Value = ob.LD_REF3},
                     new CommandParameter() {ParameterName = "pCOMMENTS_DT", Value = ob.COMMENTS_DT},
                     new CommandParameter() {ParameterName = "pCOMMENTS", Value = ob.COMMENTS},
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

        public List<MC_LD_SUBMITModel> SelectAll()
        {
            const string sp = "PKG_Labdip_action.mc_ld_submit_select";
            try
            {
                var obList = new List<MC_LD_SUBMITModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_LD_SUBMIT_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_LD_SUBMITModel ob = new MC_LD_SUBMITModel();
                    ob.MC_LD_SUBMIT_ID = (dr["MC_LD_SUBMIT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_LD_SUBMIT_ID"]);
                    ob.MC_LD_REQ_D_ID = (dr["MC_LD_REQ_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_LD_REQ_D_ID"]);


                    if (dr["RF_FIB_COMP_ID"] != DBNull.Value)
                    {
                        ob.RF_FIB_COMP_ID = Convert.ToInt64(dr["RF_FIB_COMP_ID"]);
                    }

                    ob.SUBMIT_DT = (dr["SUBMIT_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["SUBMIT_DT"]);
                    ob.REVISION_NO = (dr["REVISION_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["REVISION_NO"]);
                    ob.LD_REF1 = (dr["LD_REF1"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LD_REF1"]);
                    ob.LD_REF2 = (dr["LD_REF2"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LD_REF2"]);
                    ob.LD_REF3 = (dr["LD_REF3"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LD_REF3"]);
                    ob.COMMENTS_DT = (dr["COMMENTS_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["COMMENTS_DT"]);
                    ob.COMMENTS = (dr["COMMENTS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMMENTS"]);
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

        public MC_LD_SUBMITModel Select(long ID)
        {
            const string sp = "PKG_Labdip_action.mc_ld_submit_select";
            try
            {
                var ob = new MC_LD_SUBMITModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_LD_SUBMIT_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.MC_LD_SUBMIT_ID = (dr["MC_LD_SUBMIT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_LD_SUBMIT_ID"]);
                    ob.MC_LD_REQ_D_ID = (dr["MC_LD_REQ_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_LD_REQ_D_ID"]);

                    if (dr["RF_FIB_COMP_ID"] != DBNull.Value)
                    {
                        ob.RF_FIB_COMP_ID = Convert.ToInt64(dr["RF_FIB_COMP_ID"]);
                    }

                    ob.SUBMIT_DT = (dr["SUBMIT_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["SUBMIT_DT"]);
                    ob.REVISION_NO = (dr["REVISION_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["REVISION_NO"]);
                    ob.LD_REF1 = (dr["LD_REF1"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LD_REF1"]);
                    ob.LD_REF2 = (dr["LD_REF2"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LD_REF2"]);
                    ob.LD_REF3 = (dr["LD_REF3"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LD_REF3"]);
                    ob.COMMENTS_DT = (dr["COMMENTS_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["COMMENTS_DT"]);
                    ob.COMMENTS = (dr["COMMENTS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMMENTS"]);
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

        public List<MC_LD_SUBMITModel> LabdipColorStyleWise(Int64 MC_STYLE_H_ID, Int64 pMC_LD_REQ_H_ID)
        {
            string sp = "PKG_Labdip_action.mc_LD_REQ_select";
            try
            {
                var obList = new List<MC_LD_SUBMITModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_STYLE_H_ID", Value =MC_STYLE_H_ID},
                      new CommandParameter() {ParameterName = "pMC_LD_REQ_H_ID", Value =pMC_LD_REQ_H_ID},
                     new CommandParameter() {ParameterName = "pOption", Value = 3005},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_LD_SUBMITModel ob = new MC_LD_SUBMITModel();
                    ob.MC_LD_SUBMIT_ID = (dr["MC_LD_SUBMIT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_LD_SUBMIT_ID"]);
                    ob.MC_LD_REQ_D_ID = (dr["MC_LD_REQ_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_LD_REQ_D_ID"]);

                    ob.MC_LD_REQ_H_ID = pMC_LD_REQ_H_ID;
                    ob.MC_STYLE_H_ID = MC_STYLE_H_ID;
                    ob.SUBMIT_DT = (dr["SUBMIT_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["SUBMIT_DT"]);
                    if (ob.SUBMIT_DT.ToShortDateString() == "1/1/1900")
                    {
                        ob.SUBMIT_DT = DateTime.Now;
                    }

                    if (dr["RF_FIB_COMP_ID"] != DBNull.Value)
                    {
                        ob.RF_FIB_COMP_ID = Convert.ToInt64(dr["RF_FIB_COMP_ID"]);
                    }

                    if (dr["OLD_MC_STYLE_H_EXT_ID"] != DBNull.Value)
                    {
                        ob.OLD_MC_STYLE_H_EXT_ID = Convert.ToInt64(dr["OLD_MC_STYLE_H_EXT_ID"]);
                    }
                    ob.MC_COLOR_GRP_ID = (dr["MC_COLOR_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_COLOR_GRP_ID"]);
                    ob.LK_DYE_MTHD_ID = (dr["LK_DYE_MTHD_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_DYE_MTHD_ID"]);
                    ob.IS_REPEAT = (dr["IS_REPEAT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_REPEAT"]);

                    ob.REVISION_NO = (dr["REVISION_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["REVISION_NO"]);
                    ob.APRV_LD_REF = (dr["APRV_LD_REF"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["APRV_LD_REF"]);
                    ob.LD_REF1 = (dr["LD_REF1"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LD_REF1"]);
                    ob.LD_REF2 = (dr["LD_REF2"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LD_REF2"]);
                    ob.LD_REF3 = (dr["LD_REF3"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LD_REF3"]);
                    if (ob.LD_REF1 != "")
                    {
                        ob.IS_ACTIVE = "Y";
                    }
                    if (ob.LD_REF1 == ob.APRV_LD_REF && ob.APRV_LD_REF != "")
                    {
                        ob.IS_LD_REF1 = "Y";
                    }
                    if (ob.LD_REF2 == ob.APRV_LD_REF && ob.APRV_LD_REF != "")
                    {
                        ob.IS_LD_REF2 = "Y";
                    }
                    if (ob.LD_REF3 == ob.APRV_LD_REF && ob.APRV_LD_REF != "")
                    {
                        ob.IS_LD_REF3 = "Y";
                    }
                    ob.COMMENTS_DT = (dr["COMMENTS_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["COMMENTS_DT"]);
                    if (ob.COMMENTS_DT.ToShortDateString() == "1/1/1900")
                    {
                        ob.COMMENTS_DT = DateTime.Now;
                    }
                    ob.COMMENTS = (dr["COMMENTS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMMENTS"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);
                    ob.FABRIC_DESC = (dr["FABRIC_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FABRIC_DESC"]);
                    ob.COLOR_NAME_EN = (dr["COLOR_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_NAME_EN"]);
                    ob.TASK_STATUS_NAME = (dr["TASK_STATUS_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TASK_STATUS_NAME"]);
                    ob.MC_ORDER_H_ID = (dr["MC_ORDER_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_ORDER_H_ID"]);

                    if (ob.IS_REPEAT == "Y")
                    {
                        ob.MC_TNA_TASK_STATUS_ID = 10;
                    }
                    else
                    {
                        ob.MC_TNA_TASK_STATUS_ID = (dr["MC_TNA_TASK_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_TNA_TASK_STATUS_ID"]);
                    }

                   
                    ob.MC_COLOR_ID = (dr["MC_COLOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_COLOR_ID"]);
                    ob.ld_ref_list = new MC_LD_SUBMIT_DModel().QueryData(ob.MC_LD_SUBMIT_ID); 

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<MC_LD_SUBMITModel> LabdipColorStyleWiseHistory(string pSTYLE_NO, Int64 pMC_LD_REQ_H_ID)
        {
            string sp = "PKG_Labdip_action.mc_LD_REQ_select";
            try
            {
                var obList = new List<MC_LD_SUBMITModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_LD_REQ_H_ID", Value =pMC_LD_REQ_H_ID},
                     new CommandParameter() {ParameterName = "pSTYLE_NO", Value =pSTYLE_NO},
                     new CommandParameter() {ParameterName = "pOption", Value = 3006},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_LD_SUBMITModel ob = new MC_LD_SUBMITModel();
                    ob.MC_LD_SUBMIT_ID = (dr["MC_LD_SUBMIT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_LD_SUBMIT_ID"]);
                    ob.MC_LD_REQ_D_ID = (dr["MC_LD_REQ_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_LD_REQ_D_ID"]);

                    if (dr["RF_FIB_COMP_ID"] != DBNull.Value)
                    {
                        ob.RF_FIB_COMP_ID = Convert.ToInt64(dr["RF_FIB_COMP_ID"]);
                    }

                    ob.SUBMIT_DT = (dr["SUBMIT_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["SUBMIT_DT"]);
                    ob.REVISION_NO = (dr["REVISION_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["REVISION_NO"]);
                    ob.LD_REF1 = (dr["LD_REF1"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LD_REF1"]);
                    ob.LD_REF2 = (dr["LD_REF2"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LD_REF2"]);
                    ob.LD_REF3 = (dr["LD_REF3"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LD_REF3"]);
                    ob.APRV_LD_REF = (dr["APRV_LD_REF"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["APRV_LD_REF"]);
                    ob.COMMENTS_DT = (dr["Only_Comments_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["Only_Comments_DT"]);
                    ob.COMMENTS = (dr["Only_Comments"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["Only_Comments"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);
                    ob.FABRIC_DESC = (dr["FABRIC_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FABRIC_DESC"]);
                    ob.COLOR_NAME_EN = (dr["COLOR_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_NAME_EN"]);
                    ob.TASK_STATUS_NAME = (dr["TASK_STATUS_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TASK_STATUS_NAME"]);
                    ob.MC_ORDER_H_ID = (dr["MC_ORDER_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_ORDER_H_ID"]);
                    ob.MC_TNA_TASK_STATUS_ID = (dr["MC_TNA_TASK_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_TNA_TASK_STATUS_ID"]);
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