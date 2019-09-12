using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Web;

namespace ERP.Model
{
    public class HrGradeModel
    {
        public Int64 HR_GRADE_ID { get; set; }

        [Display(Name="Grade# [E]")]
        [Required(ErrorMessage="Input Grade Number")]
        public string GRADE_NO_EN { get; set; }

        [Display(Name = "Grade# [B]")]
        [Required]
        public string GRADE_NO_BN { get; set; }
        [Display(Name = "Active?")]
        public string IS_ACTIVE { get; set; }


        public DateTime? CREATION_DATE { get; set; }
        public int CREATED_BY { get; set; }
        public DateTime? LAST_UPDATE_DATE { get; set; }
        public int LAST_UPDATED_BY { get; set; }

        public List<HrGradeModel> GradeListData()
        {
            //string sp = "pkg_hr.hr_designation_select";
            try
            {
                var obList = new List<HrGradeModel>();

                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteSQLStatement("select * from HR_GRADE order by GRADE_NO_EN");
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    HrGradeModel ob = new HrGradeModel();
                    ob.HR_GRADE_ID = (dr["HR_GRADE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_GRADE_ID"]);
                    ob.GRADE_NO_EN = (dr["GRADE_NO_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["GRADE_NO_EN"]);
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
        public List<HrGradeModel> SelectAll()
        {
            string sp = "pkg_hr.hr_grade_select";

            try
            {
                var obList = new List<HrGradeModel>();

                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   new CommandParameter() {ParameterName = "pOption", Value = 3000}
                }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    HrGradeModel ob = new HrGradeModel();
                    ob.HR_GRADE_ID = (dr["HR_GRADE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_GRADE_ID"]);
                    ob.GRADE_NO_EN = (dr["GRADE_NO_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["GRADE_NO_EN"]);
                    ob.GRADE_NO_BN = (dr["GRADE_NO_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["GRADE_NO_BN"]);
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
        public string Save()
        {
            const string sp = "pkg_hr.hr_grade_insert";
            string vMsg = "";
            var ob = this;

            OraDatabase db = new OraDatabase();
            try
            {
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                    new CommandParameter() {ParameterName = "pHR_GRADE_ID", Value = ob.HR_GRADE_ID},
                    new CommandParameter() {ParameterName = "pGRADE_NO_EN", Value = ob.GRADE_NO_EN},
                    new CommandParameter() {ParameterName = "pGRADE_NO_BN", Value = ob.GRADE_NO_BN},
                    new CommandParameter() {ParameterName = "pCREATED_BY", Value = Convert.ToInt64(HttpContext.Current.Session["multiScUserId"])},
                    new CommandParameter() {ParameterName = "pIS_ACTIVE", Value = ob.IS_ACTIVE == null ? "N" : "Y"},
                    new CommandParameter() {ParameterName = "pOption", Value = 1000},
                    new CommandParameter() {ParameterName = "pMsg", Value = 200, Direction = ParameterDirection.Output}
                }, sp);

                foreach (DataRow dr in ds.Tables["OUTPARAM"].Rows)
                {
                    vMsg = dr["VALUE"].ToString();
                }
            }
            catch (Exception ex)
            {
                vMsg = ex.ToString();
            }
            return vMsg;
        }

        public string Update()
        {
            const string sp = "pkg_hr.hr_grade_update";
            string vMsg = "";
            var ob = this;

            OraDatabase db = new OraDatabase();
            try
            {
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                    new CommandParameter() {ParameterName = "pHR_GRADE_ID", Value = ob.HR_GRADE_ID},
                    new CommandParameter() {ParameterName = "pGRADE_NO_EN", Value = ob.GRADE_NO_EN},
                    new CommandParameter() {ParameterName = "pGRADE_NO_BN", Value = ob.GRADE_NO_BN},
                    new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = Convert.ToInt64(HttpContext.Current.Session["multiScUserId"])},
                    new CommandParameter() {ParameterName = "pIS_ACTIVE", Value = ob.IS_ACTIVE == null ? "N" : "Y"},
                    new CommandParameter() {ParameterName = "pOption", Value = 2000},
                    new CommandParameter() {ParameterName = "pMsg", Value = 200, Direction = ParameterDirection.Output}
                }, sp);

                foreach (DataRow dr in ds.Tables["OUTPARAM"].Rows)
                {
                    vMsg = dr["VALUE"].ToString();
                }
            }
            catch (Exception ex)
            {
                vMsg = ex.ToString();
            }
            return vMsg;
        }

        public HrGradeModel Select(long ID)
        {
            string sp = "pkg_hr.hr_grade_select";

            try
            {
                var ob = new HrGradeModel();
                OraDatabase db = new OraDatabase();

                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   new CommandParameter() {ParameterName = "pOption", Value = 3001},
                    new CommandParameter() {ParameterName = "pHR_GRADE_ID", Value = ID}
                }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.HR_GRADE_ID = (dr["HR_GRADE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_GRADE_ID"]);
                    ob.GRADE_NO_EN = (dr["GRADE_NO_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["GRADE_NO_EN"]);
                    ob.GRADE_NO_BN = (dr["GRADE_NO_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["GRADE_NO_BN"]);
                    ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);
                }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
