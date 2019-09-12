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
    public class MC_PLAN_EVENTModel
    {

        public Int64 id {get;set;}
        public string barColor { get; set; }
        public string start { get; set; }
        public string end { get; set; }
        public decimal pct_completion { get; set; }
        public Int64  resource { get; set; }
        public string status { get; set; }
        public string text {get;set;}

        //public string backColor  { get; set; }
        //public string barBackColor { get; set; }
        public bool clickDisabled { get; set; }
        public bool resizeDisabled { get; set; }
        public bool moveDisabled { get; set; }
        public bool rightClickDisabled { get; set; }

        public string COLOR_NAME_EN { get; set; }

        public string FAB_TYPE_NAME { get; set; }

        public string WORK_STYLE_NO { get; set; }

        public string BYR_ACC_NAME_EN { get; set; }

        public string ORDER_NO_LIST { get; set; }

        public string JOB_CRD_NO { get; set; }

        public int KNT_JC_STS_TYPE_ID { get; set; }











        public string Save()
        {
            const string sp = "pkg_knit_plan.knt_fab_roll_insert";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {


                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pOption", Value =1000},
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

        public List<MC_PLAN_EVENTModel> QueryData(DateTime? pSTART_DT, DateTime? pEND_DT)
        {
            string sp = "pkg_mc_load_plan.EVENT_SELECT";
            try
            {
                var obList = new List<MC_PLAN_EVENTModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pSTART_DT", Value =pSTART_DT},
                     new CommandParameter() {ParameterName = "pEND_DT", Value =pEND_DT},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_PLAN_EVENTModel ob = new MC_PLAN_EVENTModel();
                    ob.id = (dr["KNT_JOB_CRD_H_ID"] == DBNull.Value) ? 0: Convert.ToInt64(dr["KNT_JOB_CRD_H_ID"]);
                    ob.barColor = (dr["RGB_COL_CODE"] == DBNull.Value) ? String.Empty : Convert.ToString(dr["RGB_COL_CODE"]);
                    ob.start = (dr["START_DT_STR"] == DBNull.Value) ? String.Empty : Convert.ToString(dr["START_DT_STR"]);
                    ob.end = (dr["END_DT_STR"] == DBNull.Value) ? String.Empty : Convert.ToString(dr["END_DT_STR"]);
                    ob.pct_completion = (dr["PCT_COMPLETE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PCT_COMPLETE"]);
                    ob.status = (dr["JC_STS_TYP_NAME"] == DBNull.Value) ? String.Empty : Convert.ToString(dr["JC_STS_TYP_NAME"]);
                    ob.text = (dr["TEXT"] == DBNull.Value) ? String.Empty : Convert.ToString(dr["TEXT"]);
                    ob.resource = (dr["KNT_MACHINE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_MACHINE_ID"]);
                    ob.clickDisabled = (dr["IS_EVENT_CLICK"] == DBNull.Value) ? true : Convert.ToString(dr["IS_EVENT_CLICK"]) != "Y";
                    ob.moveDisabled = (dr["IS_EVENT_MOVE"] == DBNull.Value) ? true : Convert.ToString(dr["IS_EVENT_MOVE"]) != "Y";
                    ob.rightClickDisabled = (dr["IS_EVENT_RIGHT_CLICK"] == DBNull.Value) ? true : Convert.ToString(dr["IS_EVENT_RIGHT_CLICK"]) != "Y";
                    ob.resizeDisabled = (dr["IS_EVENT_RESIZE"] == DBNull.Value) ? true : Convert.ToString(dr["IS_EVENT_RESIZE"]) != "Y";
                    ob.KNT_JC_STS_TYPE_ID = (dr["KNT_JC_STS_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt32 (dr["KNT_JC_STS_TYPE_ID"]);

                    ob.COLOR_NAME_EN = (dr["COLOR_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_NAME_EN"]);
                    ob.FAB_TYPE_NAME = (dr["FAB_TYPE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FAB_TYPE_NAME"]);
                    ob.WORK_STYLE_NO = (dr["WORK_STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["WORK_STYLE_NO"]);
                    ob.BYR_ACC_NAME_EN = (dr["BYR_ACC_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BYR_ACC_NAME_EN"]);
                    ob.ORDER_NO_LIST = (dr["ORDER_NO_LIST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_NO_LIST"]);
                    ob.JOB_CRD_NO = (dr["JOB_CRD_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["JOB_CRD_NO"]);

                    ob.PROD_QTY = (dr["PROD_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PROD_QTY"]);
                    ob.ASIGN_QTY = (dr["ASIGN_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["ASIGN_QTY"]);
                    ob.UN_ASIGN_QTY = (dr["UN_ASIGN_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["UN_ASIGN_QTY"]);
                    ob.TG_D_PROD_QTY = (dr["TG_D_PROD_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["TG_D_PROD_QTY"]);
                    ob.KNT_PLAN_H_ID = (dr["KNT_PLAN_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_PLAN_H_ID"]);
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public string saveEventMovedData(string pXML, Int64? pOption)
        {
            const string sp = "pkg_mc_load_plan.SaveEventMovedData";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pXML", Value = pXML},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pOption", Value = pOption},
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



        public decimal PROD_QTY { get; set; }

        public decimal ASIGN_QTY { get; set; }

        public decimal TG_D_PROD_QTY { get; set; }

        public decimal UN_ASIGN_QTY { get; set; }

        public long KNT_PLAN_H_ID { get; set; }
    }
}