using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace ERP.Model
{
    public class HrShiftTypeModel //: IHrCompanyModel
    {
        public Int64 HR_SHIFT_TYPE_ID { get; set; }
        public string SHIFT_TYPE_CODE { get; set; }

        [Required]
        public string SHIFT_TYPE_NAME_EN { get; set; }
        [Required]
        public string SHIFT_TYPE_NAME_BN { get; set; }

        public string SHIFT_TYPE_DESC { get; set; }
        public string COLOR_CODE { get; set; }

        public string IS_ACTIVE { get; set; }


        public DateTime? CREATION_DATE { get; set; }
        public int CREATED_BY { get; set; }
        public DateTime? LAST_UPDATE_DATE { get; set; }
        public int LAST_UPDATED_BY { get; set; }

        public List<HrShiftTypeModel> ShiftTypeListData()
        {
            string sp = "pkg_hr.hr_shift_type_select";
            var obList = new List<HrShiftTypeModel>();

            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
            {   
                new CommandParameter() {ParameterName = "pOption", Value = 3000},                    
                new CommandParameter() {ParameterName = "pMsg", Value = 500, Direction = ParameterDirection.Output}
            }, sp);
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                HrShiftTypeModel ob = new HrShiftTypeModel();

                ob.HR_SHIFT_TYPE_ID = (dr["HR_SHIFT_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_SHIFT_TYPE_ID"]);

                ob.SHIFT_TYPE_CODE = (dr["SHIFT_TYPE_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SHIFT_TYPE_CODE"]);
                ob.SHIFT_TYPE_NAME_EN = (dr["SHIFT_TYPE_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SHIFT_TYPE_NAME_EN"]);
                ob.SHIFT_TYPE_NAME_BN = (dr["SHIFT_TYPE_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SHIFT_TYPE_NAME_BN"]);
                ob.SHIFT_TYPE_DESC = (dr["SHIFT_TYPE_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SHIFT_TYPE_DESC"]);
                ob.COLOR_CODE = (dr["COLOR_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_CODE"]);
                ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);

                obList.Add(ob);
            }

            return obList;            
        }


    }
}
