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
    public class HR_OT_ENTITLEDModel //: IHR_OT_ENTITLEDModel
    {
        public Int64 HR_OT_ENTITLED_ID { get; set; }
        public Int64 HR_OT_TEAM_ID { get; set; }
        public Int64 HR_DESIGNATION_ID { get; set; }
        //public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }

        public string BatchSave(List<HrDesigModel> ob1)
        {
            string vMsg = "";
            var ob = this;

            OraDatabase db = new OraDatabase();
            try
            {
                string sp = "pkg_hr.hr_ot_entitled_delete";
               
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                    new CommandParameter() {ParameterName = "pHR_OT_TEAM_ID", Value = ob.HR_OT_TEAM_ID},
                    new CommandParameter() {ParameterName = "pOption", Value = 4001},
                    new CommandParameter() {ParameterName = "pMsg", Value = 200, Direction = ParameterDirection.Output}
                }, sp);
                                
                sp = "pkg_hr.hr_ot_entitled_insert";
                foreach (HrDesigModel item in ob1)
                {
                    var ds1 = db.ExecuteStoredProcedure(new List<CommandParameter>()
                    {
                        new CommandParameter() {ParameterName = "pHR_OT_ENTITLED_ID", Value = ob.HR_OT_ENTITLED_ID},
                        new CommandParameter() {ParameterName = "pHR_OT_TEAM_ID", Value = ob.HR_OT_TEAM_ID},
                        new CommandParameter() {ParameterName = "pHR_DESIGNATION_ID", Value = item.HR_DESIGNATION_ID},
                        new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                        
                        new CommandParameter() {ParameterName = "pOption", Value = 1000},
                        new CommandParameter() {ParameterName = "pMsg", Value = 200, Direction = ParameterDirection.Output}
                    }, sp);

                    foreach (DataRow dr in ds1.Tables["OUTPARAM"].Rows)
                    {
                        vMsg = dr["VALUE"].ToString();
                    }                   
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return vMsg;
        }



    }
}