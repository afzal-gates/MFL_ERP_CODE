using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace ERP.Model
{
    public class HrPeriodTypeModel
    {
        public Int64 HR_PERIOD_TYPE_ID { get; set; }
       
        [Required]
        public string PERIOD_TYPE_NAME_EN { get; set; }
        [Required]
        public string PERIOD_TYPE_NAME_BN { get; set; }
        [Required]
        public Int64 CONV_IN_DAYS { get; set; }

        public string IS_ACTIVE { get; set; }


        public DateTime? CREATION_DATE { get; set; }
        public int CREATED_BY { get; set; }
        public DateTime? LAST_UPDATE_DATE { get; set; }
        public int LAST_UPDATED_BY { get; set; }


        public string PERIOD_TYPE_NAME_EN_UNIT { get; set; }

        public List<HrPeriodTypeModel> PeriodTypeListData()
        {
            string sp = "pkg_hr.hr_period_type_select";
            try
            {
                var obList = new List<HrPeriodTypeModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   new CommandParameter() {ParameterName = "pOption", Value = 3000},
                    new CommandParameter() {ParameterName = "pMsg", Value = 500, Direction = ParameterDirection.Output}
                }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    HrPeriodTypeModel ob = new HrPeriodTypeModel();
                    ob.HR_PERIOD_TYPE_ID = (dr["HR_PERIOD_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_PERIOD_TYPE_ID"]);
                    ob.PERIOD_TYPE_NAME_EN = (dr["PERIOD_TYPE_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PERIOD_TYPE_NAME_EN"]);
                    if (ob.HR_PERIOD_TYPE_ID == 1)
                        ob.PERIOD_TYPE_NAME_EN_UNIT = "Daily";
                    else if (ob.HR_PERIOD_TYPE_ID == 2)
                        ob.PERIOD_TYPE_NAME_EN_UNIT = "Weekly";
                    else if (ob.HR_PERIOD_TYPE_ID == 3)
                        ob.PERIOD_TYPE_NAME_EN_UNIT = "Monthly";
                    else if (ob.HR_PERIOD_TYPE_ID == 4)
                        ob.PERIOD_TYPE_NAME_EN_UNIT = "Yearly";

                    ob.PERIOD_TYPE_NAME_BN = (dr["PERIOD_TYPE_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PERIOD_TYPE_NAME_BN"]);
                    ob.CONV_IN_DAYS = (dr["CONV_IN_DAYS"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CONV_IN_DAYS"]);
                    ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);
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
