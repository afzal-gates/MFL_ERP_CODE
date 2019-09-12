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

    public class OrderTnAViewModelGrid
    {
        public Int64 total { get; set; }
        private List<OrderTnAViewModel> _data = null;
        public List<OrderTnAViewModel> data
        {
            get
            {
                if (_data == null)
                {
                    _data = new List<OrderTnAViewModel>();
                }
                return _data;
            }
            set
            {
                _data = value;
            }
        }
    }
    public class OrderTnAViewModel
    {
        public Int64 MC_ORDER_H_ID { get; set; }
        public string ORDER_NO { get; set; }
        public Int64 LK_ORD_TYPE_ID { get; set; }
        public DateTime ORD_CONF_DT { get; set; }
        public DateTime SHIP_DT { get; set; }
        public Int64 LEAD_TIME { get; set; }
        public Int64 TOT_ORD_QTY { get; set; }
        public Int64 MC_STYLE_H_ID { get; set; }
        public string JOB_TRAC_NO { get; set; }
        public string WORK_STYLE_NO { get; set; }
        public string ORD_TYPE_NAME { get; set; }

        public DateTime CURR_DATE { get; set; }

        public string BYR_ACC_NAME_EN { get; set; }

        public string STYLE_DESC { get; set; }

        public OrderTnAViewModelGrid OrderTnaData(Int64 pMC_BYR_ACC_ID, Int64 pageNumber, Int64 pageSize, string pORDER_NO, string pWORK_STYLE_NO, string pORD_TYPE_NAME, Int64? pMC_ORDER_H_ID)
        {
            string sp = "pkg_tna.mc_order_tna_select_grid";
            try
            {
                var obList = new OrderTnAViewModelGrid();
                int total_rec = 0;
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {

                     new CommandParameter() {ParameterName = "pMC_BYR_ACC_ID", Value =pMC_BYR_ACC_ID},
                     new CommandParameter() {ParameterName = "pageNumber", Value =pageNumber},
                     new CommandParameter() {ParameterName = "pageSize", Value =pageSize},
                     new CommandParameter() {ParameterName = "pORDER_NO", Value =pORDER_NO},
                     new CommandParameter() {ParameterName = "pSTYLE_NO", Value =pWORK_STYLE_NO},
                     new CommandParameter() {ParameterName = "pORD_TYPE_NAME", Value =pORD_TYPE_NAME},
                     new CommandParameter() {ParameterName = "pMC_ORDER_H_ID", Value =pMC_ORDER_H_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "total", Value =null, Direction = ParameterDirection.Output},
                     new CommandParameter() {ParameterName = "pMsg", Value =null, Direction = ParameterDirection.Output}
                 }, sp);
              foreach (DataRow dr in ds.Tables[0].Rows)
               {
                            OrderTnAViewModel ob = new OrderTnAViewModel();
                            ob.MC_ORDER_H_ID = (dr["MC_ORDER_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_ORDER_H_ID"]);
                            ob.ORDER_NO = (dr["ORDER_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_NO"]);
                            ob.LK_ORD_TYPE_ID = (dr["LK_ORD_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_ORD_TYPE_ID"]);

                            if (dr["ORD_CONF_DT"] != DBNull.Value)
                            {
                                ob.ORD_CONF_DT = Convert.ToDateTime(dr["ORD_CONF_DT"]);
                            }

                            if (dr["SHIP_DT"] != DBNull.Value)
                            {
                                ob.SHIP_DT = Convert.ToDateTime(dr["SHIP_DT"]);
                            }
                                                    
                            ob.LEAD_TIME = (dr["LEAD_TIME"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LEAD_TIME"]);
                            ob.TOT_ORD_QTY = (dr["TOT_ORD_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["TOT_ORD_QTY"]);
                            ob.MC_STYLE_H_ID = (dr["MC_STYLE_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_H_ID"]);
                            ob.JOB_TRAC_NO = (dr["JOB_TRAC_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["JOB_TRAC_NO"]);
                            ob.WORK_STYLE_NO = (dr["WORK_STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["WORK_STYLE_NO"]);
                            ob.ORD_TYPE_NAME = (dr["ORD_TYPE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORD_TYPE_NAME"]);

                            ob.STYLE_DESC = (dr["STYLE_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_DESC"]);
                            ob.TOT_ORD_QTY = (dr["TOT_ORD_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["TOT_ORD_QTY"]);
                            ob.MOU_CODE = (dr["MOU_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MOU_CODE"]);
                  

                            ob.CURR_DATE = DateTime.Now.Date;
                            if (total_rec < 1)
                            {
                                obList.total = Convert.ToInt64(dr["TOTAL_REC"]);
                            }

                      obList.data.Add(ob);
               }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public MC_ORD_TNA_GRID OrderTnADataTask(
            Int64 pMC_ORDER_H_ID,
            Int64 pageNumber,
            Int64 pageSize,
            string pTA_TASK_NAME_EN,
            string DpD2GList,
            string pPLAN_START_DT,
            string pACT_START_DT
            )
        {
            string sp = "pkg_tna.mc_order_tna_select_grid";
            try
            {
                    var obList = new MC_ORD_TNA_GRID();
                    int total_rec = 0;
                    OraDatabase db = new OraDatabase();
                    var ds1 = db.ExecuteStoredProcedure(new List<CommandParameter>()
                                {
                                     new CommandParameter() {ParameterName = "pMC_ORDER_H_ID", Value =pMC_ORDER_H_ID},
                                     new CommandParameter() {ParameterName = "pageNumber", Value =pageNumber},
                                     new CommandParameter() {ParameterName = "pageSize", Value =pageSize},
                                     new CommandParameter() {ParameterName = "pTA_TASK_NAME_EN", Value =pTA_TASK_NAME_EN},
                                     new CommandParameter() {ParameterName = "pDpD2GList", Value =DpD2GList},
                                     new CommandParameter() {ParameterName = "pPLAN_START_DT", Value =pPLAN_START_DT},
                                     new CommandParameter() {ParameterName = "pACT_START_DT", Value =pACT_START_DT},
                                     new CommandParameter() {ParameterName = "pSC_USER_ID", Value =HttpContext.Current.Session["multiScUserId"]},
                                     new CommandParameter() {ParameterName = "pOption", Value =3001},
                                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                                 }, sp);

                    foreach (DataRow dr1 in ds1.Tables[0].Rows)
                    {
                        MC_ORD_TNAModel ob1 = new MC_ORD_TNAModel();
                        ob1.IS_ST_END = (dr1["IS_ST_END"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["IS_ST_END"]);
                        ob1.IS_ST_END_BASE = (dr1["IS_ST_END_BASE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["IS_ST_END_BASE"]);

                        ob1.MC_ORD_TNA_ID = (dr1["MC_ORD_TNA_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr1["MC_ORD_TNA_ID"]);
                        ob1.MC_ORDER_H_ID = (dr1["MC_ORDER_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr1["MC_ORDER_H_ID"]);
                        ob1.MC_TNA_TASK_ID = (dr1["MC_TNA_TASK_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr1["MC_TNA_TASK_ID"]);
                        ob1.PARENT_TASK_ID = (dr1["PARENT_TASK_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr1["PARENT_TASK_ID"]);
                        ob1.TASK_ORDER = (dr1["TA_TASK_SL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr1["TA_TASK_SL"]);
                        ob1.STD_DAYS = (dr1["STD_DAYS"] == DBNull.Value) ? 0 : Convert.ToInt64(dr1["STD_DAYS"]);

                        ob1.TA_TASK_NAME_EN = (dr1["TA_TASK_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["TA_TASK_NAME_EN"]);

                        ob1.SHOULD_U = (dr1["SHOULD_U"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["SHOULD_U"]);
                        ob1.IS_PRODUCTION_N = (dr1["IS_PRODUCTION_N"] == DBNull.Value) ? "N" : Convert.ToString(dr1["IS_PRODUCTION_N"]);
                        ob1.IS_PRODUCTION_B = (dr1["IS_PRODUCTION_B"] == DBNull.Value) ? "N" : Convert.ToString(dr1["IS_PRODUCTION_B"]);

                        ob1.IS_AUTO_UPDATE = (dr1["IS_AUTO_UPDATE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["IS_AUTO_UPDATE"]);
                        ob1.REMARKS = (dr1["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["REMARKS"]);
                        ob1.CRUD_FLAG = (dr1["CRUD_FLAG"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["CRUD_FLAG"]);
                        ob1.IS_START_TASK = (dr1["IS_START_TASK"] == DBNull.Value) ? "N" : Convert.ToString(dr1["IS_START_TASK"]);
                        ob1.IS_N_A = (dr1["IS_N_A"] == DBNull.Value) ? "N" : Convert.ToString(dr1["IS_N_A"]);


                        if (total_rec < 1)
                        {
                            obList.total = Convert.ToInt64(dr1["TOTAL_REC"]);
                        }

                        if (dr1["PLAN_START_DT"] != DBNull.Value)
                        {
                            ob1.PLAN_START_DT = Convert.ToDateTime(dr1["PLAN_START_DT"]);
                        }

                        if (dr1["ACT_START_DT"] != DBNull.Value)
                        {
                            ob1.ACT_START_DT = Convert.ToDateTime(dr1["ACT_START_DT"]);
                        }

                        if ((Convert.ToDateTime(ob1.PLAN_START_DT) - (ob1.ACT_START_DT ?? DateTime.Now.Date)).Days < 0 && ob1.IS_N_A=="N")
                        {
                            ob1.LAG_DAYS = (Convert.ToDateTime(ob1.PLAN_START_DT) - (ob1.ACT_START_DT ?? DateTime.Now.Date)).Days;
                        }

                        if ((Convert.ToDateTime(ob1.PLAN_START_DT) - DateTime.Now.Date).Days >= 0)
                        {
                            ob1.DAYS_TO_GO = (Convert.ToDateTime(ob1.PLAN_START_DT) - DateTime.Now.Date).Days;
                        }


                        obList.data.Add(ob1);
                    }

                    return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


            public MC_ORD_TNAModel OrderTnATaskByCode(
                Int64 pMC_ORDER_H_ID,
                string pTA_TASK_CODE
            )
            {
                string sp = "pkg_tna.mc_order_tna_select_grid";
                try
                {

                    OraDatabase db = new OraDatabase();
                    MC_ORD_TNAModel ob1 = new MC_ORD_TNAModel();

                    var ds1 = db.ExecuteStoredProcedure(new List<CommandParameter>()
                                    {
                                         new CommandParameter() {ParameterName = "pMC_ORDER_H_ID", Value =pMC_ORDER_H_ID},
                                         new CommandParameter() {ParameterName = "pTA_TASK_CODE", Value = pTA_TASK_CODE},
                                         new CommandParameter() {ParameterName = "pSC_USER_ID", Value =HttpContext.Current.Session["multiScUserId"]},
                                         new CommandParameter() {ParameterName = "pOption", Value =3005},
                                         new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                                     }, sp);

                    foreach (DataRow dr1 in ds1.Tables[0].Rows)
                    {
                        
                        ob1.IS_ST_END = (dr1["IS_ST_END"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["IS_ST_END"]);
                        ob1.IS_ST_END_BASE = (dr1["IS_ST_END_BASE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["IS_ST_END_BASE"]);

                        ob1.MC_ORD_TNA_ID = (dr1["MC_ORD_TNA_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr1["MC_ORD_TNA_ID"]);
                        ob1.MC_ORDER_H_ID = (dr1["MC_ORDER_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr1["MC_ORDER_H_ID"]);
                        ob1.MC_TNA_TASK_ID = (dr1["MC_TNA_TASK_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr1["MC_TNA_TASK_ID"]);
                        ob1.PARENT_TASK_ID = (dr1["PARENT_TASK_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr1["PARENT_TASK_ID"]);
                        ob1.TASK_ORDER = (dr1["TA_TASK_SL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr1["TA_TASK_SL"]);
                        ob1.STD_DAYS = (dr1["STD_DAYS"] == DBNull.Value) ? 0 : Convert.ToInt64(dr1["STD_DAYS"]);

                        ob1.TA_TASK_NAME_EN = (dr1["TA_TASK_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["TA_TASK_NAME_EN"]);

                        ob1.SHOULD_U = (dr1["SHOULD_U"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["SHOULD_U"]);
                        ob1.IS_PRODUCTION_N = (dr1["IS_PRODUCTION_N"] == DBNull.Value) ? "N" : Convert.ToString(dr1["IS_PRODUCTION_N"]);
                        ob1.IS_PRODUCTION_B = (dr1["IS_PRODUCTION_B"] == DBNull.Value) ? "N" : Convert.ToString(dr1["IS_PRODUCTION_B"]);

                        ob1.IS_AUTO_UPDATE = (dr1["IS_AUTO_UPDATE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["IS_AUTO_UPDATE"]);
                        ob1.REMARKS = (dr1["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["REMARKS"]);
                        ob1.CRUD_FLAG = (dr1["CRUD_FLAG"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["CRUD_FLAG"]);
                        ob1.IS_START_TASK = (dr1["IS_START_TASK"] == DBNull.Value) ? "N" : Convert.ToString(dr1["IS_START_TASK"]);
                        ob1.IS_N_A = (dr1["IS_N_A"] == DBNull.Value) ? "N" : Convert.ToString(dr1["IS_N_A"]);



                        if (dr1["PLAN_START_DT"] != DBNull.Value)
                        {
                            ob1.PLAN_START_DT = Convert.ToDateTime(dr1["PLAN_START_DT"]);
                        }

                        if (dr1["ACT_START_DT"] != DBNull.Value)
                        {
                            ob1.ACT_START_DT = Convert.ToDateTime(dr1["ACT_START_DT"]);
                        }

                        if ((Convert.ToDateTime(ob1.PLAN_START_DT) - (ob1.ACT_START_DT ?? DateTime.Now.Date)).Days < 0 && ob1.IS_N_A == "N")
                        {
                            ob1.LAG_DAYS = (Convert.ToDateTime(ob1.PLAN_START_DT) - (ob1.ACT_START_DT ?? DateTime.Now.Date)).Days;
                        }

                        if ((Convert.ToDateTime(ob1.PLAN_START_DT) - DateTime.Now.Date).Days >= 0)
                        {
                            ob1.DAYS_TO_GO = (Convert.ToDateTime(ob1.PLAN_START_DT) - DateTime.Now.Date).Days;
                        }
                    }

                    return ob1;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }


        public MC_ORD_TNA_GRID OrderTnADataTaskDashBord(long pageNumber, long pageSize, string pTA_TASK_NAME_EN, string pPLAN_START_DT, string pWORK_STYLE, string pBASE_STYLE, string pORDER_NO)
        {
            string sp = "pkg_tna.mc_order_tna_select_grid";
            try
            {
                var obList = new MC_ORD_TNA_GRID();
                int total_rec = 0;
                OraDatabase db = new OraDatabase();
                var ds1 = db.ExecuteStoredProcedure(new List<CommandParameter>()
                                {
                                     new CommandParameter() {ParameterName = "pageNumber", Value =pageNumber},
                                     new CommandParameter() {ParameterName = "pageSize", Value =pageSize},
                                     new CommandParameter() {ParameterName = "pTA_TASK_NAME_EN", Value =pTA_TASK_NAME_EN},
                                     new CommandParameter() {ParameterName = "pPLAN_START_DT", Value =pPLAN_START_DT},
                                     new CommandParameter() {ParameterName = "pWORK_STYLE", Value =pWORK_STYLE},
                                     new CommandParameter() {ParameterName = "pBASE_STYLE", Value =pBASE_STYLE},
                                     new CommandParameter() {ParameterName = "pORDER_NO", Value =pORDER_NO},
                                     new CommandParameter() {ParameterName = "pSC_USER_ID", Value =HttpContext.Current.Session["multiScUserId"]},
                                     new CommandParameter() {ParameterName = "pOption", Value =3002},
                                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                                 }, sp);

                foreach (DataRow dr1 in ds1.Tables[0].Rows)
                {
                    MC_ORD_TNAModel ob1 = new MC_ORD_TNAModel();
                    ob1.IS_ST_END = (dr1["IS_ST_END"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["IS_ST_END"]);
                    ob1.IS_ST_END_BASE = (dr1["IS_ST_END_BASE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["IS_ST_END_BASE"]);

                    ob1.MC_ORD_TNA_ID = (dr1["MC_ORD_TNA_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr1["MC_ORD_TNA_ID"]);
                    ob1.MC_ORDER_H_ID = (dr1["MC_ORDER_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr1["MC_ORDER_H_ID"]);
                    ob1.MC_TNA_TASK_ID = (dr1["MC_TNA_TASK_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr1["MC_TNA_TASK_ID"]);
                    ob1.PARENT_TASK_ID = (dr1["PARENT_TASK_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr1["PARENT_TASK_ID"]);
                    ob1.TASK_ORDER = (dr1["TASK_ORDER"] == DBNull.Value) ? 0 : Convert.ToInt64(dr1["TASK_ORDER"]);
                    ob1.STD_DAYS = (dr1["STD_DAYS"] == DBNull.Value) ? 0 : Convert.ToInt64(dr1["STD_DAYS"]);

                    ob1.TA_TASK_NAME_EN = (dr1["TA_TASK_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["TA_TASK_NAME_EN"]);

                    ob1.SHOULD_U = (dr1["SHOULD_U"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["SHOULD_U"]);
                    ob1.IS_PRODUCTION_N = (dr1["IS_PRODUCTION_N"] == DBNull.Value) ? "N" : Convert.ToString(dr1["IS_PRODUCTION_N"]);
                    ob1.IS_PRODUCTION_B = (dr1["IS_PRODUCTION_B"] == DBNull.Value) ? "N" : Convert.ToString(dr1["IS_PRODUCTION_B"]);

                    ob1.IS_AUTO_UPDATE = (dr1["IS_AUTO_UPDATE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["IS_AUTO_UPDATE"]);
                    ob1.REMARKS = (dr1["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["REMARKS"]);
                    ob1.CRUD_FLAG = (dr1["CRUD_FLAG"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["CRUD_FLAG"]);
                    ob1.U = ob1.CRUD_FLAG.IndexOf("U") >= 0 ? "Y" : "N";
                    ob1.IS_START_TASK = (dr1["IS_START_TASK"] == DBNull.Value) ? "N" : Convert.ToString(dr1["IS_START_TASK"]);


                    if (dr1["PLAN_START_DT"] != DBNull.Value)
                    {
                        ob1.PLAN_START_DT = Convert.ToDateTime(dr1["PLAN_START_DT"]);
                    }
                    if ((Convert.ToDateTime(ob1.PLAN_START_DT) - DateTime.Now.Date).Days >= 0)
                    {
                        ob1.DAYS_TO_GO = (Convert.ToDateTime(ob1.PLAN_START_DT) - DateTime.Now.Date).Days;
                    }

                    if (total_rec < 1)
                    {
                        obList.total = Convert.ToInt64(dr1["TOTAL_REC"]);
                    }

                    if (dr1["EXCEPTED_DATE"] != DBNull.Value)
                    {
                        ob1.EXCEPTED_DATE = Convert.ToDateTime(dr1["EXCEPTED_DATE"]);
                    }
                    ob1.IS_UPD_BY_PLND = (dr1["IS_UPD_BY_PLND"] == DBNull.Value) ? "N" : Convert.ToString(dr1["IS_UPD_BY_PLND"]);



                    ob1.ORDER_NO = (dr1["ORDER_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["ORDER_NO"]);
                    ob1.WORK_STYLE = (dr1["WORK_STYLE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["WORK_STYLE"]);
                    ob1.BASE_STYLE = (dr1["BASE_STYLE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["BASE_STYLE"]);
                    ob1.MC_BYR_ACC_ID = (dr1["MC_BYR_ACC_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr1["MC_BYR_ACC_ID"]);
                    ob1.BYR_ACC_NAME_EN = (dr1["BYR_ACC_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["BYR_ACC_NAME_EN"]);
                    

                    obList.data.Add(ob1);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string MOU_CODE { get; set; }
    }
}