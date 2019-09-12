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
    public class VM_TNA_TASK_FAILURE_MAILModel
    {
        //public Int64 MC_ORD_TNA_ID { get; set; }
        //public Int64 MC_ORDER_H_ID { get; set; }
        //public Int64 MC_TNA_TASK_ID { get; set; }
        public string IS_ST_END { get; set; }
        public DateTime PLAN_ST_END { get; set; }
        public String ACT_ST_END { get; set; }
        public string REMARKS { get; set; }
        public string TA_TASK_NAME_EN { get; set; }
        public DateTime ORD_CONF_DT { get; set; }
        public DateTime SHIP_DT { get; set; }
        public Int64 LEAD_TIME { get; set; }
        public string STYLE_NO { get; set; }
        public string BYR_ACC_NAME_EN { get; set; }
        public string STYLE_DESC { get; set; }

        public List<VM_TNA_TASK_FAILURE_MAILModel> SelectAll(int pOption)
        {
            string sp = "PKG_TNA.tna_matrix_grid_select";
            try
            {
                var obList = new List<VM_TNA_TASK_FAILURE_MAILModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pOption", Value =pOption},
                     //3004 => Sample/Development
                     //3005 => Bulk
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
              foreach (DataRow dr in ds.Tables[0].Rows)
               {
                            VM_TNA_TASK_FAILURE_MAILModel ob = new VM_TNA_TASK_FAILURE_MAILModel();
                            //ob.MC_ORD_TNA_ID = (dr["MC_ORD_TNA_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_ORD_TNA_ID"]);
                            //ob.MC_ORDER_H_ID = (dr["MC_ORDER_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_ORDER_H_ID"]);
                            //ob.MC_TNA_TASK_ID = (dr["MC_TNA_TASK_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_TNA_TASK_ID"]);
                            //ob.IS_ST_END = (dr["IS_ST_END"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ST_END"]);
                            ob.PLAN_ST_END = (dr["PLAN_ST_END"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["PLAN_ST_END"]);
                            ob.ORDER_NO = (dr["ORDER_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_NO"]);
                            if (dr["ACT_ST_END"] != DBNull.Value)
                            {
                                ob.ACT_ST_END = Convert.ToDateTime(dr["ACT_ST_END"]).ToString("dd-MMM-yyyy");
                            }

                            ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                            ob.TA_TASK_NAME_EN = (dr["TA_TASK_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TA_TASK_NAME_EN"]);
                            ob.ORD_CONF_DT = (dr["ORD_CONF_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["ORD_CONF_DT"]);
                            ob.SHIP_DT = (dr["SHIP_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["SHIP_DT"]);
                            ob.LEAD_TIME = (dr["LEAD_TIME"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LEAD_TIME"]);
                            ob.STYLE_NO = (dr["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO"]);
                            ob.BYR_ACC_NAME_EN = (dr["BYR_ACC_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BYR_ACC_NAME_EN"]);
                            ob.STYLE_DESC = (dr["STYLE_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_DESC"]);
                            obList.Add(ob);
               }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string ORDER_NO { get; set; }
    }
}