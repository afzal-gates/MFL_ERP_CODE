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

    public class MC_ORD_TNA_GRID
    {
        public Int64 total { get; set; }
        private List<MC_ORD_TNAModel> _data = null;
        public List<MC_ORD_TNAModel> data
        {
            get
            {
                if (_data == null)
                {
                    _data = new List<MC_ORD_TNAModel>();
                }
                return _data;
            }
            set
            {
                _data = value;
            }
        }
    }
    public class MC_ORD_TNAModel
    {
        public Int64 MC_ORD_TNA_ID { get; set; }
        public Int64 MC_ORDER_H_ID { get; set; }
        public Int64 MC_TNA_TASK_ID { get; set; }
        public Int64 PARENT_TASK_ID { get; set; }
        public Int64 TASK_ORDER { get; set; }
        public Int64 STD_DAYS { get; set; }
        public DateTime PLAN_START_DT { get; set; }
        public DateTime PLAN_END_DT { get; set; }
        public DateTime? ACT_START_DT { get; set; }
        public DateTime? ACT_END_DT { get; set; }
        public string TA_TASK_NAME_EN { get; set; }
        public string IS_PRODUCTION_N { get; set; }
        public string IS_PRODUCTION_B { get; set; }
        public string IS_AUTO_UPDATE { get; set; }
        public Int64? LAG_DAYS { get; set; }
        public Int64? DAYS_TO_GO { get; set; }
        public string REMARKS { get; set; }
        public string CRUD_FLAG { get; set; }
        public string IS_ST_END { get; set; }
        public string IS_ST_END_BASE { get; set; }
        public string IS_START_TASK { get; set; }
        public string SHOULD_U { get; set; }
        public string ORDER_NO { get; set; }
        public string WORK_STYLE { get; set; }
        public string BASE_STYLE { get; set; }
        public long MC_BYR_ACC_ID { get; set; }
        public string BYR_ACC_NAME_EN { get; set; }
        public string IS_ONLY_COMMNENT { get; set; }
        public string U { get; set; }
        public string IS_N_A { get; set; }
        public string IS_HS_SINZING { get; set; }


        public string Save()
        {
            const string sp = "pkg_tna.mc_ord_tna_update";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_ORD_TNA_ID", Value = ob.MC_ORD_TNA_ID},
                     new CommandParameter() {ParameterName = "pACT_START_DT", Value = ob.ACT_START_DT},
                     new CommandParameter() {ParameterName = "pEXCEPTED_DATE", Value = ob.EXCEPTED_DATE},
                     new CommandParameter() {ParameterName = "pREMARKS", Value = ob.REMARKS},
                     new CommandParameter() {ParameterName = "pIS_ONLY_COMMNENT", Value = ob.IS_ONLY_COMMNENT},
                     new CommandParameter() {ParameterName = "pIS_ST_END", Value = ob.IS_ST_END},                     
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
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


        public List<MC_ORD_TNAModel> SelectByOrderList(string pMC_ORDER_H_ID_LST = null)
        {
            string sp = "pkg_tna.mc_order_tna_select";
            try
            {
                var obList = new List<MC_ORD_TNAModel>();
                OraDatabase db = new OraDatabase();
                var ds1 = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                    new CommandParameter() {ParameterName = "pMC_ORDER_H_ID_LST", Value =pMC_ORDER_H_ID_LST},
                    new CommandParameter() {ParameterName = "pOption", Value =3002},
                    new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                }, sp);

                foreach (DataRow dr1 in ds1.Tables[0].Rows)
                {
                    MC_ORD_TNAModel ob1 = new MC_ORD_TNAModel();

                    ob1.MC_ORD_TNA_ID = (dr1["MC_ORD_TNA_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr1["MC_ORD_TNA_ID"]);
                    ob1.MC_ORDER_H_ID = (dr1["MC_ORDER_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr1["MC_ORDER_H_ID"]);
                    ob1.MC_TNA_TASK_ID = (dr1["MC_TNA_TASK_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr1["MC_TNA_TASK_ID"]);
                    //ob1.PARENT_TASK_ID = (dr1["PARENT_TASK_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr1["PARENT_TASK_ID"]);
                    ob1.REMARKS = (dr1["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["REMARKS"]);

                    if (dr1["PLAN_START_DT"] != DBNull.Value)
                    {
                        ob1.PLAN_START_DT = Convert.ToDateTime(dr1["PLAN_START_DT"]);
                    }
                    if (dr1["PLAN_END_DT"] != DBNull.Value)
                    {
                        ob1.PLAN_END_DT = Convert.ToDateTime(dr1["PLAN_END_DT"]);
                    }

                    if ((Convert.ToDateTime(ob1.PLAN_START_DT) - DateTime.Now.Date).Days >= 0)
                    {
                        ob1.DAYS_TO_GO = (Convert.ToDateTime(ob1.PLAN_START_DT) - DateTime.Now.Date).Days;
                    }

                    ob1.ORDER_NO = (dr1["ORDER_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["ORDER_NO"]);
                    ob1.TA_TASK_NAME_EN = (dr1["TA_TASK_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["TA_TASK_NAME_EN"]);

                    //ob1.IS_ST_END = (dr1["IS_ST_END"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["IS_ST_END"]);
                    //ob1.IS_ST_END_BASE = (dr1["IS_ST_END_BASE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["IS_ST_END_BASE"]);
                    //ob1.TASK_ORDER = (dr1["TASK_ORDER"] == DBNull.Value) ? 0 : Convert.ToInt64(dr1["TASK_ORDER"]);
                    //ob1.STD_DAYS = (dr1["STD_DAYS"] == DBNull.Value) ? 0 : Convert.ToInt64(dr1["STD_DAYS"]);
                    //ob1.SHOULD_U = (dr1["SHOULD_U"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["SHOULD_U"]);
                    //ob1.IS_PRODUCTION_N = (dr1["IS_PRODUCTION_N"] == DBNull.Value) ? "N" : Convert.ToString(dr1["IS_PRODUCTION_N"]);
                    //ob1.IS_PRODUCTION_B = (dr1["IS_PRODUCTION_B"] == DBNull.Value) ? "N" : Convert.ToString(dr1["IS_PRODUCTION_B"]);

                    //ob1.IS_AUTO_UPDATE = (dr1["IS_AUTO_UPDATE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["IS_AUTO_UPDATE"]);
                    //ob1.CRUD_FLAG = (dr1["CRUD_FLAG"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["CRUD_FLAG"]);
                    //ob1.U = ob1.CRUD_FLAG.IndexOf("U") >= 0 ? "Y" : "N";
                    //ob1.IS_START_TASK = (dr1["IS_START_TASK"] == DBNull.Value) ? "N" : Convert.ToString(dr1["IS_START_TASK"]);

                    //ob1.WORK_STYLE = (dr1["WORK_STYLE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["WORK_STYLE"]);
                    //ob1.BASE_STYLE = (dr1["BASE_STYLE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["BASE_STYLE"]);
                    //ob1.MC_BYR_ACC_ID = (dr1["MC_BYR_ACC_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr1["MC_BYR_ACC_ID"]);
                    //ob1.BYR_ACC_NAME_EN = (dr1["BYR_ACC_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["BYR_ACC_NAME_EN"]);


                    obList.Add(ob1);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public DateTime? EXCEPTED_DATE { get; set; }
        public string IS_UPD_BY_PLND { get; set; }
    }
}