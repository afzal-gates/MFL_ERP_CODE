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
    public class DYE_BT_DC_REQ_D2Model
    {
        public Int64 DYE_BT_DC_REQ_D2_ID { get; set; }
        public Int64 DYE_STR_REQ_H_ID { get; set; }
        public Int64 DYE_PRD_PHASE_TYPE_ID { get; set; }
        public Int64 INV_ITEM_ID { get; set; }
        public Decimal DOSE_QTY { get; set; }
        public Int64 DOSE_MOU_ID { get; set; }
        public Int64 RQD_QTY_K { get; set; }
        public Int64 RQD_QTY_G { get; set; }
        public Int64 RQD_QTY_M { get; set; }
        public Decimal RQD_QTY { get; set; }
        public Int64 QTY_MOU_ID { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }

        public Int64 STEP_NO { get; set; }
        public Int64? ALT_BT_DC_REQ_D2_ID { get; set; }
        public string IS_FINALIZED { get; set; }
        public Int64? NO_EDIT { get; set; }

        public string IS_WAIT { get; set; }
        public bool IS_LAST { get; set; }

        public Int64? ALT_ITEM_ID { get; set; }
        public string IS_ALT_ITEM { get; set; }


        public List<INV_ITEMModel> ItemList { get; set; }

        public string Save()
        {
            const string sp = "pkg_dye_batch_program.dye_bt_dc_req_d2_insert";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_BT_DC_REQ_D2_ID", Value = ob.DYE_BT_DC_REQ_D2_ID},
                     new CommandParameter() {ParameterName = "pDYE_STR_REQ_H_ID", Value = ob.DYE_STR_REQ_H_ID},
                     new CommandParameter() {ParameterName = "pDYE_PRD_PHASE_TYPE_ID", Value = ob.DYE_PRD_PHASE_TYPE_ID},
                     new CommandParameter() {ParameterName = "pINV_ITEM_ID", Value = ob.INV_ITEM_ID},
                     new CommandParameter() {ParameterName = "pDOSE_QTY", Value = ob.DOSE_QTY},
                     new CommandParameter() {ParameterName = "pDOSE_MOU_ID", Value = ob.DOSE_MOU_ID},
                     new CommandParameter() {ParameterName = "pRQD_QTY_K", Value = ob.RQD_QTY_K},
                     new CommandParameter() {ParameterName = "pRQD_QTY_G", Value = ob.RQD_QTY_G},
                     new CommandParameter() {ParameterName = "pRQD_QTY_M", Value = ob.RQD_QTY_M},
                     new CommandParameter() {ParameterName = "pRQD_QTY", Value = ob.RQD_QTY},
                     new CommandParameter() {ParameterName = "pQTY_MOU_ID", Value = ob.QTY_MOU_ID},
                     new CommandParameter() {ParameterName = "pSTEP_NO", Value = ob.STEP_NO},
                     new CommandParameter() {ParameterName = "pALT_BT_DC_REQ_D2_ID", Value = ob.ALT_BT_DC_REQ_D2_ID},                     
                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
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
            const string sp = "SP_DYE_BT_DC_REQ_D2";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_BT_DC_REQ_D2_ID", Value = ob.DYE_BT_DC_REQ_D2_ID},
                     new CommandParameter() {ParameterName = "pDYE_STR_REQ_H_ID", Value = ob.DYE_STR_REQ_H_ID},
                     new CommandParameter() {ParameterName = "pDYE_PRD_PHASE_TYPE_ID", Value = ob.DYE_PRD_PHASE_TYPE_ID},
                     new CommandParameter() {ParameterName = "pINV_ITEM_ID", Value = ob.INV_ITEM_ID},
                     new CommandParameter() {ParameterName = "pDOSE_QTY", Value = ob.DOSE_QTY},
                     new CommandParameter() {ParameterName = "pDOSE_MOU_ID", Value = ob.DOSE_MOU_ID},
                     new CommandParameter() {ParameterName = "pRQD_QTY_K", Value = ob.RQD_QTY_K},
                     new CommandParameter() {ParameterName = "pRQD_QTY_G", Value = ob.RQD_QTY_G},
                     new CommandParameter() {ParameterName = "pRQD_QTY_M", Value = ob.RQD_QTY_M},
                     new CommandParameter() {ParameterName = "pRQD_QTY", Value = ob.RQD_QTY},
                     new CommandParameter() {ParameterName = "pQTY_MOU_ID", Value = ob.QTY_MOU_ID},
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
            const string sp = "SP_DYE_BT_DC_REQ_D2";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_BT_DC_REQ_D2_ID", Value = ob.DYE_BT_DC_REQ_D2_ID},
                     new CommandParameter() {ParameterName = "pDYE_STR_REQ_H_ID", Value = ob.DYE_STR_REQ_H_ID},
                     new CommandParameter() {ParameterName = "pDYE_PRD_PHASE_TYPE_ID", Value = ob.DYE_PRD_PHASE_TYPE_ID},
                     new CommandParameter() {ParameterName = "pINV_ITEM_ID", Value = ob.INV_ITEM_ID},
                     new CommandParameter() {ParameterName = "pDOSE_QTY", Value = ob.DOSE_QTY},
                     new CommandParameter() {ParameterName = "pDOSE_MOU_ID", Value = ob.DOSE_MOU_ID},
                     new CommandParameter() {ParameterName = "pRQD_QTY_K", Value = ob.RQD_QTY_K},
                     new CommandParameter() {ParameterName = "pRQD_QTY_G", Value = ob.RQD_QTY_G},
                     new CommandParameter() {ParameterName = "pRQD_QTY_M", Value = ob.RQD_QTY_M},
                     new CommandParameter() {ParameterName = "pRQD_QTY", Value = ob.RQD_QTY},
                     new CommandParameter() {ParameterName = "pQTY_MOU_ID", Value = ob.QTY_MOU_ID},
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

        public List<DYE_BT_DC_REQ_D2Model> SelectAll()
        {
            string sp = "Select_DYE_BT_DC_REQ_D2";
            try
            {
                var obList = new List<DYE_BT_DC_REQ_D2Model>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_BT_DC_REQ_D2_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DYE_BT_DC_REQ_D2Model ob = new DYE_BT_DC_REQ_D2Model();
                    ob.DYE_BT_DC_REQ_D2_ID = (dr["DYE_BT_DC_REQ_D2_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_BT_DC_REQ_D2_ID"]);
                    ob.DYE_STR_REQ_H_ID = (dr["DYE_STR_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_STR_REQ_H_ID"]);
                    ob.DYE_PRD_PHASE_TYPE_ID = (dr["DYE_PRD_PHASE_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_PRD_PHASE_TYPE_ID"]);
                    ob.INV_ITEM_ID = (dr["INV_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_ITEM_ID"]);
                    ob.DOSE_QTY = (dr["DOSE_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["DOSE_QTY"]);
                    ob.DOSE_MOU_ID = (dr["DOSE_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DOSE_MOU_ID"]);
                    ob.RQD_QTY_K = (dr["RQD_QTY_K"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RQD_QTY_K"]);
                    ob.RQD_QTY_G = (dr["RQD_QTY_G"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RQD_QTY_G"]);
                    ob.RQD_QTY_M = (dr["RQD_QTY_M"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RQD_QTY_M"]);
                    ob.RQD_QTY = (dr["RQD_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RQD_QTY"]);
                    ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);
                    ob.ALT_BT_DC_REQ_D2_ID = (dr["ALT_BT_DC_REQ_D2_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ALT_BT_DC_REQ_D2_ID"]);
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

        public List<DYE_BT_DC_REQ_D2Model> Select(Int64? ID, Int64? pQUERY_TYPE_ID = null)
        {
            string sp = "PKG_DYE_BATCH_PROGRAM.DYE_BT_DC_REQ_D2_SELECT";
            try
            {
                var obList = new List<DYE_BT_DC_REQ_D2Model>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_STR_REQ_H_ID", Value = ID},
                     new CommandParameter() {ParameterName = "pQUERY_TYPE_ID", Value = pQUERY_TYPE_ID},
                     
                     new CommandParameter() {ParameterName = "pOption", Value =3001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DYE_BT_DC_REQ_D2Model ob = new DYE_BT_DC_REQ_D2Model();
                    ob.DYE_BT_DC_REQ_D2_ID = (dr["DYE_BT_DC_REQ_D2_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_BT_DC_REQ_D2_ID"]);
                    ob.DYE_STR_REQ_H_ID = (dr["DYE_STR_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_STR_REQ_H_ID"]);
                    ob.DYE_PRD_PHASE_TYPE_ID = (dr["DYE_PRD_PHASE_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_PRD_PHASE_TYPE_ID"]);
                    ob.INV_ITEM_ID = (dr["INV_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_ITEM_ID"]);
                    ob.DOSE_QTY = (dr["DOSE_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["DOSE_QTY"]);
                    ob.DOSE_MOU_ID = (dr["DOSE_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DOSE_MOU_ID"]);
                    ob.RQD_QTY_K = (dr["RQD_QTY_K"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RQD_QTY_K"]);
                    ob.RQD_QTY_G = (dr["RQD_QTY_G"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RQD_QTY_G"]);
                    ob.RQD_QTY_M = (dr["RQD_QTY_M"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RQD_QTY_M"]);
                    ob.RQD_QTY = (dr["RQD_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RQD_QTY"]);
                    ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);
                    ob.ALT_BT_DC_REQ_D2_ID = (dr["ALT_BT_DC_REQ_D2_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ALT_BT_DC_REQ_D2_ID"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);
                    ob.STEP_NO = (dr["STEP_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["STEP_NO"]);
                    ob.NO_EDIT = (dr["NO_EDIT"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["NO_EDIT"]);

                    ob.IS_FINALIZED = (dr["IS_FINALIZED"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_FINALIZED"]);
                    ob.ITEM_NAME_EN = (dr["ITEM_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_NAME_EN"]);
                    ob.ITEM_CAT_NAME_EN = (dr["ITEM_CAT_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_CAT_NAME_EN"]);
                    ob.QTY_MOU_ST = (dr["QTY_MOU_ST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["QTY_MOU_ST"]);
                    ob.ISS_QTY = (dr["ISS_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["ISS_QTY"]);
                    ob.SUB_STK = (dr["SUB_STK"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["SUB_STK"]);

                    ob.PRD_PHASE_NAME = (dr["PRD_PHASE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PRD_PHASE_NAME"]);
                    ob.MOU_CODE = (dr["MOU_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MOU_CODE"]);
                    ob.DC_AGENT_ID = (dr["DC_AGENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DC_AGENT_ID"]);
                    ob.DYE_BT_DC_ISS_D_ID = (dr["DYE_BT_DC_ISS_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_BT_DC_ISS_D_ID"]);

                    ob.ALT_ITEM_ID = (dr["ALT_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ALT_ITEM_ID"]);
                    ob.IS_ALT_ITEM = (dr["IS_ALT_ITEM"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_ALT_ITEM"]);
                    
                    ob.IS_WAIT = ob.IS_FINALIZED == "W" ? "W" : "X";
                    ob.IS_FINALIZED = ob.IS_FINALIZED == "W" ? "N" : ob.IS_FINALIZED;

                    if (pQUERY_TYPE_ID == 1)
                    {
                        if (ob.IS_WAIT != "W")
                            obList.Add(ob);
                    }
                    else
                        obList.Add(ob);


                    //ob.IS_WAIT = ob.IS_FINALIZED == "W" ? "W" : "X";
                    //ob.IS_FINALIZED = ob.IS_FINALIZED;

                    //if (pQUERY_TYPE_ID == 1)
                    //{
                    //    if (ob.IS_WAIT == "W")
                    //        obList.Add(ob);
                    //}
                    //else
                    //    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DYE_BT_DC_REQ_D2Model> SelectForAR(Int64? pDYE_STR_REQ_H_ID)
        {
            string sp = "PKG_DYE_BATCH_PROGRAM.DYE_BT_DC_REQ_D2_SELECT";
            try
            {
                var obList = new List<DYE_BT_DC_REQ_D2Model>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_STR_REQ_H_ID", Value = pDYE_STR_REQ_H_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3002},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DYE_BT_DC_REQ_D2Model ob = new DYE_BT_DC_REQ_D2Model();
                    ob.DYE_BT_DC_REQ_D2_ID = (dr["DYE_BT_DC_REQ_D2_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_BT_DC_REQ_D2_ID"]);
                    ob.DYE_STR_REQ_H_ID = (dr["DYE_STR_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_STR_REQ_H_ID"]);
                    ob.DYE_PRD_PHASE_TYPE_ID = (dr["DYE_PRD_PHASE_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_PRD_PHASE_TYPE_ID"]);
                    ob.INV_ITEM_ID = (dr["INV_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_ITEM_ID"]);
                    ob.DOSE_QTY = (dr["DOSE_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["DOSE_QTY"]);
                    ob.DOSE_MOU_ID = (dr["DOSE_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DOSE_MOU_ID"]);
                    ob.RQD_QTY_K = (dr["RQD_QTY_K"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RQD_QTY_K"]);
                    ob.RQD_QTY_G = (dr["RQD_QTY_G"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RQD_QTY_G"]);
                    ob.RQD_QTY_M = (dr["RQD_QTY_M"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RQD_QTY_M"]);
                    ob.RQD_QTY = (dr["RQD_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RQD_QTY"]);
                    ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);
                    ob.ALT_BT_DC_REQ_D2_ID = (dr["ALT_BT_DC_REQ_D2_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ALT_BT_DC_REQ_D2_ID"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);
                    ob.STEP_NO = (dr["STEP_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["STEP_NO"]);
                    ob.NO_EDIT = (dr["NO_EDIT"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["NO_EDIT"]);

                    ob.IS_ALT_ITEM = "N";
                    ob.IS_FINALIZED = "N";
                    //ob.IS_FINALIZED = (dr["IS_FINALIZED"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_FINALIZED"]);

                    ob.ITEM_NAME_EN = (dr["ITEM_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_NAME_EN"]);
                    ob.ITEM_CAT_NAME_EN = (dr["ITEM_CAT_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_CAT_NAME_EN"]);
                    ob.QTY_MOU_ST = (dr["QTY_MOU_ST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["QTY_MOU_ST"]);
                    ob.ISS_QTY = (dr["ISS_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["ISS_QTY"]);
                    ob.SUB_STK = (dr["SUB_STK"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["SUB_STK"]);

                    ob.PRD_PHASE_NAME = (dr["PRD_PHASE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PRD_PHASE_NAME"]);
                    ob.MOU_CODE = (dr["MOU_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MOU_CODE"]);
                    ob.DC_AGENT_ID = (dr["DC_AGENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DC_AGENT_ID"]);

                    ob.IS_WAIT = ob.IS_FINALIZED == "W" ? "W" : "X";
                    ob.IS_FINALIZED = ob.IS_FINALIZED == "W" ? "N" : ob.IS_FINALIZED;

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DYE_BT_DC_REQ_D2Model> SelectForAddition(Int64? pDYE_STR_REQ_H_ID)
        {
            string sp = "PKG_DYE_BATCH_PROGRAM.DYE_BT_DC_REQ_D2_SELECT";
            try
            {
                var obList = new List<DYE_BT_DC_REQ_D2Model>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_STR_REQ_H_ID", Value = pDYE_STR_REQ_H_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3003},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DYE_BT_DC_REQ_D2Model ob = new DYE_BT_DC_REQ_D2Model();
                    ob.DYE_BT_DC_REQ_D2_ID = (dr["DYE_BT_DC_REQ_D2_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_BT_DC_REQ_D2_ID"]);
                    ob.DYE_STR_REQ_H_ID = (dr["DYE_STR_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_STR_REQ_H_ID"]);
                    ob.DYE_PRD_PHASE_TYPE_ID = (dr["DYE_PRD_PHASE_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_PRD_PHASE_TYPE_ID"]);
                    ob.INV_ITEM_ID = (dr["INV_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_ITEM_ID"]);
                    ob.DOSE_QTY = (dr["DOSE_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["DOSE_QTY"]);
                    ob.DOSE_MOU_ID = (dr["DOSE_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DOSE_MOU_ID"]);
                    ob.RQD_QTY_K = (dr["RQD_QTY_K"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RQD_QTY_K"]);
                    ob.RQD_QTY_G = (dr["RQD_QTY_G"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RQD_QTY_G"]);
                    ob.RQD_QTY_M = (dr["RQD_QTY_M"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RQD_QTY_M"]);
                    ob.RQD_QTY = (dr["RQD_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RQD_QTY"]);
                    ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);
                    ob.ALT_BT_DC_REQ_D2_ID = (dr["ALT_BT_DC_REQ_D2_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ALT_BT_DC_REQ_D2_ID"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);
                    ob.STEP_NO = (dr["STEP_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["STEP_NO"]);
                    ob.NO_EDIT = (dr["NO_EDIT"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["NO_EDIT"]);

                    ob.IS_ALT_ITEM = "N";
                    ob.IS_FINALIZED = "N";
                    //ob.IS_FINALIZED = (dr["IS_FINALIZED"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_FINALIZED"]);

                    ob.ITEM_NAME_EN = (dr["ITEM_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_NAME_EN"]);
                    ob.ITEM_CAT_NAME_EN = (dr["ITEM_CAT_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_CAT_NAME_EN"]);
                    ob.QTY_MOU_ST = (dr["QTY_MOU_ST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["QTY_MOU_ST"]);
                    ob.ISS_QTY = (dr["ISS_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["ISS_QTY"]);
                    ob.SUB_STK = (dr["SUB_STK"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["SUB_STK"]);

                    ob.PRD_PHASE_NAME = (dr["PRD_PHASE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PRD_PHASE_NAME"]);
                    ob.MOU_CODE = (dr["MOU_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MOU_CODE"]);
                    ob.DC_AGENT_ID = (dr["DC_AGENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DC_AGENT_ID"]);

                    ob.IS_WAIT = ob.IS_FINALIZED == "W" ? "W" : "X";
                    ob.IS_FINALIZED = ob.IS_FINALIZED == "W" ? "N" : ob.IS_FINALIZED;

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<DYE_BT_DC_REQ_D2Model> SelectAllAddition(Int64? pDYE_STR_REQ_H_ID)
        {
            string sp = "PKG_DYE_BATCH_PROGRAM.DYE_BT_DC_REQ_D2_SELECT";
            try
            {
                var obList = new List<DYE_BT_DC_REQ_D2Model>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_STR_REQ_H_ID", Value = pDYE_STR_REQ_H_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3004},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DYE_BT_DC_REQ_D2Model ob = new DYE_BT_DC_REQ_D2Model();
                    ob.DYE_BT_DC_REQ_D2_ID = (dr["DYE_BT_DC_REQ_D2_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_BT_DC_REQ_D2_ID"]);
                    ob.DYE_STR_REQ_H_ID = (dr["DYE_STR_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_STR_REQ_H_ID"]);
                    ob.DYE_PRD_PHASE_TYPE_ID = (dr["DYE_PRD_PHASE_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_PRD_PHASE_TYPE_ID"]);
                    ob.INV_ITEM_ID = (dr["INV_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_ITEM_ID"]);
                    ob.DOSE_QTY = (dr["DOSE_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["DOSE_QTY"]);
                    ob.DOSE_MOU_ID = (dr["DOSE_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DOSE_MOU_ID"]);
                    ob.RQD_QTY_K = (dr["RQD_QTY_K"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RQD_QTY_K"]);
                    ob.RQD_QTY_G = (dr["RQD_QTY_G"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RQD_QTY_G"]);
                    ob.RQD_QTY_M = (dr["RQD_QTY_M"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RQD_QTY_M"]);
                    ob.RQD_QTY = (dr["RQD_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RQD_QTY"]);
                    ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);
                    ob.ALT_BT_DC_REQ_D2_ID = (dr["ALT_BT_DC_REQ_D2_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ALT_BT_DC_REQ_D2_ID"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);
                    ob.STEP_NO = (dr["STEP_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["STEP_NO"]);
                    ob.NO_EDIT = (dr["NO_EDIT"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["NO_EDIT"]);

                    ob.IS_ALT_ITEM = "N";
                    ob.IS_FINALIZED = "N";
                    //ob.IS_FINALIZED = (dr["IS_FINALIZED"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_FINALIZED"]);

                    ob.ITEM_NAME_EN = (dr["ITEM_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_NAME_EN"]);
                    ob.ITEM_CAT_NAME_EN = (dr["ITEM_CAT_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_CAT_NAME_EN"]);
                    ob.QTY_MOU_ST = (dr["QTY_MOU_ST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["QTY_MOU_ST"]);
                    ob.ISS_QTY = (dr["ISS_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["ISS_QTY"]);
                    ob.SUB_STK = (dr["SUB_STK"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["SUB_STK"]);

                    ob.PRD_PHASE_NAME = (dr["PRD_PHASE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PRD_PHASE_NAME"]);
                    ob.MOU_CODE = (dr["MOU_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MOU_CODE"]);
                    ob.DC_AGENT_ID = (dr["DC_AGENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DC_AGENT_ID"]);

                    ob.IS_WAIT = ob.IS_FINALIZED == "W" ? "W" : "X";
                    ob.IS_FINALIZED = ob.IS_FINALIZED == "W" ? "N" : ob.IS_FINALIZED;

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string ITEM_NAME_EN { get; set; }

        public string ITEM_CAT_NAME_EN { get; set; }

        public string QTY_MOU_ST { get; set; }

        public decimal ISS_QTY { get; set; }

        public decimal SUB_STK { get; set; }

        public string PRD_PHASE_NAME { get; set; }

        public string MOU_CODE { get; set; }

        public long DC_AGENT_ID { get; set; }

        public Int64? DYE_BT_DC_ISS_D_ID { get; set; }
    }
}