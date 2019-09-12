using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace ERP.Model
{
    public class RF_CAL_MONTHModel 
    {
        public Int64 RF_CAL_MONTH_ID { get; set; }
        public string MONTH_NAME_EN { get; set; }
        public string MONTH_NAME_BN { get; set; }
        public Int64 MONTH_VALUE { get; set; }
        public DateTime MO_START_DATE { get; set; }
        public DateTime MO_END_DATE { get; set; }

        public List<RF_CAL_MONTHModel> MonthListData()
        {
            string sp = "pkg_common.rf_cal_month_select";
            try
            {
                var obList = new List<RF_CAL_MONTHModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   new CommandParameter() {ParameterName = "pOption", Value = 3000},
                    new CommandParameter() {ParameterName = "pRF_CAL_MONTH_ID", Value = 0},
                    new CommandParameter() {ParameterName = "pMsg", Value = 500, Direction = ParameterDirection.Output}
                }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    RF_CAL_MONTHModel ob = new RF_CAL_MONTHModel();
                    ob.RF_CAL_MONTH_ID = (dr["RF_CAL_MONTH_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_CAL_MONTH_ID"]);
                    ob.MONTH_NAME_EN = (dr["MONTH_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MONTH_NAME_EN"]);
                    ob.MONTH_NAME_BN = (dr["MONTH_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MONTH_NAME_BN"]);
                    ob.MONTH_VALUE = (dr["MONTH_VALUE"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MONTH_VALUE"]);
                    ob.MO_START_DATE = (dr["MO_START_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["MO_START_DATE"]);
                    ob.MO_END_DATE = (dr["MO_END_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["MO_END_DATE"]);
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