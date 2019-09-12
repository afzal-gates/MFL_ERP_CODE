using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace ERP.Model
{
    public class HR_DSPLN_ACT_TYPEModel //: IHR_DSPLN_ACT_TYPEModel
    {
        public Int64 HR_DSPLN_ACT_TYPE_ID { get; set; }
        public string DA_TYPE_NAME_EN { get; set; }
        public string DA_TYPE_NAME_BN { get; set; }
        public string IS_EFFECT_SALARY { get; set; }
        public Int64 HR_PAY_ELEMENT_ID { get; set; }
        public Int64 DISPLAY_ORDER { get; set; }
        public Int64 LK_JOB_STATUS_ID { get; set; }
        public string IS_ACTIVE { get; set; }
        //public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        //public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }

        public List<HR_DSPLN_ACT_TYPEModel> ActionTypeListData()
        {
            string sp = "pkg_hr.hr_dspln_act_type_select";
            try
            {
                var obList = new List<HR_DSPLN_ACT_TYPEModel>();

                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
                    new CommandParameter() {ParameterName = "pHR_DSPLN_ACT_TYPE_ID", Value = 0},

                    new CommandParameter() {ParameterName = "pOption", Value = 3000},
                    new CommandParameter() {ParameterName = "pMsg", Value = 500, Direction = ParameterDirection.Output}
                }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    HR_DSPLN_ACT_TYPEModel ob = new HR_DSPLN_ACT_TYPEModel();
                    ob.HR_DSPLN_ACT_TYPE_ID = (dr["HR_DSPLN_ACT_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_DSPLN_ACT_TYPE_ID"]);
                    ob.DA_TYPE_NAME_EN = (dr["DA_TYPE_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DA_TYPE_NAME_EN"]);
                    ob.DA_TYPE_NAME_BN = (dr["DA_TYPE_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DA_TYPE_NAME_BN"]);
                    ob.IS_EFFECT_SALARY = (dr["IS_EFFECT_SALARY"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_EFFECT_SALARY"]);
                    ob.HR_PAY_ELEMENT_ID = (dr["HR_PAY_ELEMENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_PAY_ELEMENT_ID"]);
                    ob.DISPLAY_ORDER = (dr["DISPLAY_ORDER"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DISPLAY_ORDER"]);
                    ob.LK_JOB_STATUS_ID = (dr["LK_JOB_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_JOB_STATUS_ID"]);
                    ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);
                    //ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    //ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    //ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    //ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);
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