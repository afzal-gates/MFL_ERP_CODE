using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace ERP.Model
{
    public class HR_SAL_TRAN_DModel //: IHR_SAL_TRAN_DModel
    {
        public Int64 HR_SAL_TRAN_D_ID { get; set; }
        public Int64 HR_SAL_TRAN_H_ID { get; set; }
        public Int64 HR_PAY_ELEMENT_ID { get; set; }
        public Int64 PAY_QTY_ACT { get; set; }
        public Int64 PAY_QTY_EXM { get; set; }
        public Int64 PAY_QTY { get; set; }
        public Decimal PAY_RATE { get; set; }
        public string PAY_UNIT_FLAG { get; set; }
        public Decimal PAY_AMT { get; set; }
        public string IS_ARREAR { get; set; }
        public string REMARKS { get; set; }
        public string IS_PROCESSED { get; set; }


        public string PAY_ELEMENT_NAME_EN { get; set; }
        public string PAY_ELM_TYPE_NAME_EN { get; set; }

        public List<HR_SAL_TRAN_DModel> SalaryPayListData(Int64 pHR_SAL_TRAN_H_ID)
        {
            string sp = "pkg_salary.hr_sal_tran_d_select";
            try
            {
                var obList = new List<HR_SAL_TRAN_DModel>();

                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
                    new CommandParameter() {ParameterName = "pHR_SAL_TRAN_H_ID", Value = pHR_SAL_TRAN_H_ID},
                    new CommandParameter() {ParameterName = "pOption", Value = 3002},                    
                    new CommandParameter() {ParameterName = "pMsg", Value = 500, Direction = ParameterDirection.Output}
                }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    HR_SAL_TRAN_DModel ob = new HR_SAL_TRAN_DModel();
                    ob.HR_SAL_TRAN_D_ID = (dr["HR_SAL_TRAN_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_SAL_TRAN_D_ID"]);
                    ob.HR_SAL_TRAN_H_ID = (dr["HR_SAL_TRAN_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_SAL_TRAN_H_ID"]);
                    ob.HR_PAY_ELEMENT_ID = (dr["HR_PAY_ELEMENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_PAY_ELEMENT_ID"]);
                    ob.PAY_QTY_ACT = (dr["PAY_QTY_ACT"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PAY_QTY_ACT"]);
                    ob.PAY_QTY_EXM = (dr["PAY_QTY_EXM"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PAY_QTY_EXM"]);
                    ob.PAY_QTY = (dr["PAY_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PAY_QTY"]);
                    ob.PAY_RATE = (dr["PAY_RATE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PAY_RATE"]);
                    ob.PAY_UNIT_FLAG = (dr["PAY_UNIT_FLAG"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PAY_UNIT_FLAG"]);
                    ob.PAY_AMT = (dr["PAY_AMT"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PAY_AMT"]);
                    ob.IS_ARREAR = (dr["IS_ARREAR"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ARREAR"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                    ob.PAY_ELEMENT_NAME_EN = (dr["PAY_ELEMENT_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PAY_ELEMENT_NAME_EN"]);
                    ob.PAY_ELM_TYPE_NAME_EN = (dr["PAY_ELM_TYPE_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PAY_ELM_TYPE_NAME_EN"]);
                    ob.IS_PROCESSED = (dr["IS_PROCESSED"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_PROCESSED"]);

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