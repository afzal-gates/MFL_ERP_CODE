using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Data;

namespace ERP.Model
{
    public class HR_DESIG_PAYModel //: IHR_DESIG_PAYModel
    {
        public Int64 HR_DESIG_PAY_ID { get; set; }
        public Int64 HR_DESIGNATION_ID { get; set; }
        public Int64 HR_PAY_ELEMENT_ID { get; set; }
        public Decimal PAY_RATE { get; set; }
        public string IS_EFFECT_SALARY { get; set; }
        public string REMARKS { get; set; }
        public string IS_ACTIVE { get; set; }
        //public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        //public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }


        public Int64 HR_DEPARTMENT_ID { get; set; }
        public string DESIGNATION_NAME_EN { get; set; }

        public string BatchSave(List<HR_DESIG_PAYModel> obList)
        {
            string vMsg = "";
            OraDatabase db = new OraDatabase();
            try
            {
                Int32 vIsPost;
                //int vIsInsert;
                int vOption;                
                string sp;

                foreach (HR_DESIG_PAYModel item in obList)
                {
                    if (item.HR_DESIG_PAY_ID != null && item.HR_DESIG_PAY_ID > 0)
                    {
                        sp = "pkg_salary.hr_desig_pay_update";
                        vIsPost = 1;                        
                        vOption=2000;
                    }
                    else
                    {
                        sp = "pkg_salary.hr_desig_pay_insert";
                        vIsPost = 1;                        
                        vOption=1000;
                        if (item.PAY_RATE < 1)
                        { vIsPost = 0; }
                    }

                    if (vIsPost == 1)
                    {                                                
                        var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                        {                                     
                            new CommandParameter() {ParameterName = "pHR_DESIG_PAY_ID", Value = item.HR_DESIG_PAY_ID},
                            new CommandParameter() {ParameterName = "pHR_DESIGNATION_ID", Value = item.HR_DESIGNATION_ID},
                            new CommandParameter() {ParameterName = "pHR_PAY_ELEMENT_ID", Value = item.HR_PAY_ELEMENT_ID},
                            new CommandParameter() {ParameterName = "pPAY_RATE", Value = item.PAY_RATE},
                            new CommandParameter() {ParameterName = "pIS_EFFECT_SALARY", Value = item.IS_EFFECT_SALARY},
                            new CommandParameter() {ParameterName = "pREMARKS", Value = item.REMARKS},
                            new CommandParameter() {ParameterName = "pIS_ACTIVE", Value = item.IS_ACTIVE},                    
                            new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                            //new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},

                            new CommandParameter() {ParameterName = "pOption", Value = vOption},
                            new CommandParameter() {ParameterName = "pMsg", Value = 200, Direction = ParameterDirection.Output}
                        }, sp);

                        foreach (DataRow dr in ds.Tables["OUTPARAM"].Rows)
                        {
                            vMsg = dr["VALUE"].ToString();
                        }                        
                    }
                }
            }            
            catch (Exception ex)
            {
                vMsg = "MULTI-005" + ex.Message;
            }
            return vMsg;
        }

        public List<HR_DESIG_PAYModel> DesigPayListData(Int64? pHR_DEPARTMENT_ID, Int64? pHR_PAY_ELEMENT_ID)
        {
            string sp = "pkg_salary.hr_desig_pay_select";
            try
            {
                
                var obList = new List<HR_DESIG_PAYModel>();
                OraDatabase db = new OraDatabase();
                
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   new CommandParameter() {ParameterName = "pOption", Value = 3002},
                    new CommandParameter() {ParameterName = "pHR_PAY_ELEMENT_ID", Value = pHR_PAY_ELEMENT_ID},
                    new CommandParameter() {ParameterName = "pHR_DEPARTMENT_ID", Value = pHR_DEPARTMENT_ID},
                    new CommandParameter() {ParameterName = "pMsg", Value = 500, Direction = ParameterDirection.Output}
                }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    HR_DESIG_PAYModel ob = new HR_DESIG_PAYModel();

                    ob.HR_DESIG_PAY_ID = (dr["HR_DESIG_PAY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_DESIG_PAY_ID"]);
                    ob.HR_DESIGNATION_ID = (dr["HR_DESIGNATION_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_DESIGNATION_ID"]);
                    ob.DESIGNATION_NAME_EN = (dr["DESIGNATION_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DESIGNATION_NAME_EN"]);
                    ob.HR_PAY_ELEMENT_ID = (dr["HR_PAY_ELEMENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_PAY_ELEMENT_ID"]);
                    ob.PAY_RATE = (dr["PAY_RATE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PAY_RATE"]);
                    ob.IS_EFFECT_SALARY = (dr["IS_EFFECT_SALARY"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_EFFECT_SALARY"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                    ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);

                    ob.HR_DEPARTMENT_ID = (dr["HR_DEPARTMENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_DEPARTMENT_ID"]);

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