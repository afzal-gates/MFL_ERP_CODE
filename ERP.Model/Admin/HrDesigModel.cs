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
    public class HrDesigModel //: IHrCompany
    {
        public Int64 HR_DESIGNATION_ID { get; set; }
               
        public string DESIGNATION_CODE { get; set; }
        
        [Required(ErrorMessage="Please select department")]
        public Int64? HR_DEPARTMENT_ID { get; set; }
        [Required(ErrorMessage = "Please input designation name [E]")]
        [MaxLength(80,ErrorMessage="Sorry! You can input maximum 80 charecters")]
        public string DESIGNATION_NAME_EN { get; set; }
        [MaxLength(80, ErrorMessage = "Sorry! You can input maximum 80 charecters")]
        public string DESIGNATION_NAME_BN { get; set; }
        [MaxLength(300, ErrorMessage = "Sorry! You can input maximum 300 charecters")]
        public string DESIGNATION_DESC { get; set; }

        [Required(ErrorMessage = "Please input designation short name")]
        [MaxLength(20, ErrorMessage = "Sorry! You can input maximum 20 charecters")]
        public string DESIGNATION_SNAME { get; set; }

        [Required(ErrorMessage = "Please select designation group")]
        public Int64 HR_DESIGNATION_GRP_ID { get; set; }

        //[Required]
        public Int64? HR_MANAGEMENT_TYPE_ID { get; set; }   
     
        public Int64? HR_GRADE_ID { get; set; }
                
        [Range(1,99999,ErrorMessage="Sorry! You can input between 1 & 99999")]
        [Display(Name="designation order")]        
        [Required(ErrorMessage = "Please input display order")]
        public Int64? DESIG_ORDER { get; set; }    
    
        public string IS_GAJETED { get; set; }        
        public string IS_ACTIVE { get; set; }


        public Int64 TRAN_MODE { get; set; }
        public string DESIG_GRP_NAME_EN { get; set; }
        public string MNG_TYPE_NAME_EN { get; set; }
        public string DEPARTMENT_NAME_EN { get; set; }
        public string GRADE_NO_EN { get; set; }


        public DateTime? CREATION_DATE { get; set; }
        public int CREATED_BY { get; set; }
        public DateTime? LAST_UPDATE_DATE { get; set; }
        public int LAST_UPDATED_BY { get; set; }
        public int LAST_UPDATE_LOGIN { get; set; }
        public int VERSION_NO { get; set; }

        public string name { get; set; }

        public List<HrDesigModel> DesignationListData(Int64 showDepartment, int listReturnType)
        {
            string sp = "pkg_hr.hr_designation_select";
            try
            {
                var obList = new List<HrDesigModel>();

                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   new CommandParameter() {ParameterName = "pOption", Value = (listReturnType==1?3002:(listReturnType==2?3003:3004))},
                    new CommandParameter() {ParameterName = "pHR_DEPARTMENT_ID", Value = showDepartment},
                    new CommandParameter() {ParameterName = "pMsg", Value = 500, Direction = ParameterDirection.Output}
                }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    HrDesigModel ob = new HrDesigModel();
                    ob.HR_DESIGNATION_ID = (dr["HR_DESIGNATION_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_DESIGNATION_ID"]);
                    ob.DESIGNATION_CODE = (dr["DESIGNATION_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DESIGNATION_CODE"]);
                    ob.HR_DEPARTMENT_ID = (dr["HR_DEPARTMENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_DEPARTMENT_ID"]);
                    ob.DESIGNATION_NAME_EN = (dr["DESIGNATION_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DESIGNATION_NAME_EN"]);
                    ob.DESIGNATION_NAME_BN = (dr["DESIGNATION_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DESIGNATION_NAME_BN"]);
                    ob.DESIGNATION_DESC = (dr["DESIGNATION_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DESIGNATION_DESC"]);
                    ob.DESIGNATION_SNAME = (dr["DESIGNATION_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DESIGNATION_SNAME"]);
                    ob.HR_DESIGNATION_GRP_ID = (dr["HR_DESIGNATION_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_DESIGNATION_GRP_ID"]);
                    ob.HR_MANAGEMENT_TYPE_ID = (dr["HR_MANAGEMENT_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_MANAGEMENT_TYPE_ID"]);
                    ob.DESIG_GRP_NAME_EN = (dr["DESIG_GRP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DESIG_GRP_NAME_EN"]);
                    ob.MNG_TYPE_NAME_EN = (dr["MNG_TYPE_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MNG_TYPE_NAME_EN"]);
                    ob.DEPARTMENT_NAME_EN = (dr["DEPARTMENT_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DEPARTMENT_NAME_EN"]);
                    ob.GRADE_NO_EN = (dr["GRADE_NO_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["GRADE_NO_EN"]);
                    ob.HR_GRADE_ID = (dr["HR_GRADE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_GRADE_ID"]);
                    ob.IS_GAJETED = (dr["IS_GAJETED"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_GAJETED"]);
                    ob.DESIG_ORDER = (dr["DESIG_ORDER"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DESIG_ORDER"]);
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
            const string sp = "pkg_hr.hr_designation_insert";
            string vMsg = "";
            var ob = this;

            OraDatabase db = new OraDatabase();
            try
            {
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                    new CommandParameter() {ParameterName = "pHR_DESIGNATION_ID", Value = ob.HR_DESIGNATION_ID},
                    new CommandParameter() {ParameterName = "pDESIGNATION_CODE", Value = ob.DESIGNATION_CODE},
                    new CommandParameter() {ParameterName = "pHR_DEPARTMENT_ID", Value = (ob.HR_DEPARTMENT_ID != 0 || ob.HR_DEPARTMENT_ID == null)?ob.HR_DEPARTMENT_ID : null},
                    new CommandParameter() {ParameterName = "pDESIGNATION_NAME_EN", Value = ob.DESIGNATION_NAME_EN},
                    new CommandParameter() {ParameterName = "pDESIGNATION_NAME_BN", Value = ob.DESIGNATION_NAME_BN},
                    new CommandParameter() {ParameterName = "pDESIGNATION_DESC", Value = ob.DESIGNATION_DESC},
                    new CommandParameter() {ParameterName = "pDESIGNATION_SNAME", Value = ob.DESIGNATION_SNAME},
                    new CommandParameter() {ParameterName = "pHR_DESIGNATION_GRP_ID", Value = ob.HR_DESIGNATION_GRP_ID},
                    new CommandParameter() {ParameterName = "pHR_MANAGEMENT_TYPE_ID", Value =(ob.HR_MANAGEMENT_TYPE_ID != 0 || ob.HR_MANAGEMENT_TYPE_ID == null)? ob.HR_MANAGEMENT_TYPE_ID : null},
                    new CommandParameter() {ParameterName = "pHR_GRADE_ID", Value =(ob.HR_GRADE_ID != 0 || ob.HR_GRADE_ID == null)? ob.HR_GRADE_ID : null},
                    new CommandParameter() {ParameterName = "pIS_GAJETED", Value = ob.IS_GAJETED},
                    new CommandParameter() {ParameterName = "pDESIG_ORDER", Value = ob.DESIG_ORDER},
                    new CommandParameter() {ParameterName = "pCREATED_BY", Value = Convert.ToInt64(HttpContext.Current.Session["multiScUserId"])},
                    new CommandParameter() {ParameterName = "pIS_ACTIVE", Value = ob.IS_ACTIVE},
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
            const string sp = "pkg_hr.hr_designation_update";
            string vMsg = "";
            var ob = this;
            OraDatabase db = new OraDatabase();
            try
            {
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                    new CommandParameter() {ParameterName = "pHR_DESIGNATION_ID", Value = ob.HR_DESIGNATION_ID},
                    new CommandParameter() {ParameterName = "pDESIGNATION_CODE", Value = ob.DESIGNATION_CODE},
                    new CommandParameter() {ParameterName = "pHR_DEPARTMENT_ID", Value = (ob.HR_DEPARTMENT_ID != 0 || ob.HR_DEPARTMENT_ID == null)?ob.HR_DEPARTMENT_ID : null},
                    new CommandParameter() {ParameterName = "pDESIGNATION_NAME_EN", Value = ob.DESIGNATION_NAME_EN},
                    new CommandParameter() {ParameterName = "pDESIGNATION_NAME_BN", Value = ob.DESIGNATION_NAME_BN},
                    new CommandParameter() {ParameterName = "pDESIGNATION_DESC", Value = ob.DESIGNATION_DESC},
                    new CommandParameter() {ParameterName = "pDESIGNATION_SNAME", Value = ob.DESIGNATION_SNAME},
                    new CommandParameter() {ParameterName = "pHR_DESIGNATION_GRP_ID", Value = ob.HR_DESIGNATION_GRP_ID},
                    new CommandParameter() {ParameterName = "pHR_MANAGEMENT_TYPE_ID", Value =(ob.HR_MANAGEMENT_TYPE_ID != 0 || ob.HR_MANAGEMENT_TYPE_ID == null)? ob.HR_MANAGEMENT_TYPE_ID : null},
                    new CommandParameter() {ParameterName = "pHR_GRADE_ID", Value =(ob.HR_GRADE_ID != 0 || ob.HR_GRADE_ID == null)? ob.HR_GRADE_ID : null},
                    new CommandParameter() {ParameterName = "pIS_GAJETED", Value = ob.IS_GAJETED},
                    new CommandParameter() {ParameterName = "pDESIG_ORDER", Value = ob.DESIG_ORDER},
                    new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = Convert.ToInt64(HttpContext.Current.Session["multiScUserId"])},
                    new CommandParameter() {ParameterName = "pIS_ACTIVE", Value = ob.IS_ACTIVE},
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
        public HrDesigModel Select(long ID)
        {
            string sp = "pkg_hr.hr_designation_select";

            try
            {
                var ob = new HrDesigModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   new CommandParameter() {ParameterName = "pOption", Value = 3001},
                    new CommandParameter() {ParameterName = "pHR_DESIGNATION_ID", Value = ID},
                    new CommandParameter() {ParameterName = "pMsg", Value = 500, Direction = ParameterDirection.Output}
                }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.HR_DESIGNATION_ID = (dr["HR_DESIGNATION_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_DESIGNATION_ID"]);
                    ob.DESIGNATION_CODE = (dr["DESIGNATION_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DESIGNATION_CODE"]);
                    ob.HR_DEPARTMENT_ID = (dr["HR_DEPARTMENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_DEPARTMENT_ID"]);
                    ob.DESIGNATION_NAME_EN = (dr["DESIGNATION_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DESIGNATION_NAME_EN"]);
                    ob.DESIGNATION_NAME_BN = (dr["DESIGNATION_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DESIGNATION_NAME_BN"]);
                    ob.DESIGNATION_DESC = (dr["DESIGNATION_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DESIGNATION_DESC"]);
                    ob.DESIGNATION_SNAME = (dr["DESIGNATION_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DESIGNATION_SNAME"]);
                    ob.HR_DESIGNATION_GRP_ID = (dr["HR_DESIGNATION_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_DESIGNATION_GRP_ID"]);
                    ob.HR_MANAGEMENT_TYPE_ID = (dr["HR_MANAGEMENT_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_MANAGEMENT_TYPE_ID"]);
                    ob.HR_GRADE_ID = (dr["HR_GRADE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_GRADE_ID"]);
                    ob.IS_GAJETED = (dr["IS_GAJETED"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_GAJETED"]);
                    ob.DESIG_ORDER = (dr["DESIG_ORDER"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DESIG_ORDER"]);
                    ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);
                }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<HrDesigModel> NonEntitledDesigListData()
        {
            string sp = "pkg_hr.hr_designation_select";
            try
            {
                var obList = new List<HrDesigModel>();

                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
                    //new CommandParameter() {ParameterName = "pName", Value = 0},

                    new CommandParameter() {ParameterName = "pOption", Value = 3006},                    
                    new CommandParameter() {ParameterName = "pMsg", Value = 500, Direction = ParameterDirection.Output}
                }, sp);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    HrDesigModel ob = new HrDesigModel();

                    ob.HR_DESIGNATION_ID = (dr["HR_DESIGNATION_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_DESIGNATION_ID"]);
                    ob.DESIGNATION_CODE = (dr["DESIGNATION_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DESIGNATION_CODE"]);
                    ob.HR_DEPARTMENT_ID = (dr["HR_DEPARTMENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_DEPARTMENT_ID"]);
                    ob.DESIGNATION_NAME_EN = (dr["DESIGNATION_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DESIGNATION_NAME_EN"]);
                    ob.DESIGNATION_NAME_BN = (dr["DESIGNATION_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DESIGNATION_NAME_BN"]);
                    ob.DESIGNATION_DESC = (dr["DESIGNATION_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DESIGNATION_DESC"]);
                    ob.DESIGNATION_SNAME = (dr["DESIGNATION_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DESIGNATION_SNAME"]);
                    ob.HR_DESIGNATION_GRP_ID = (dr["HR_DESIGNATION_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_DESIGNATION_GRP_ID"]);
                    ob.HR_MANAGEMENT_TYPE_ID = (dr["HR_MANAGEMENT_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_MANAGEMENT_TYPE_ID"]);
                    ob.HR_GRADE_ID = (dr["HR_GRADE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_GRADE_ID"]);
                    ob.IS_GAJETED = (dr["IS_GAJETED"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_GAJETED"]);
                    ob.DESIG_ORDER = (dr["DESIG_ORDER"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DESIG_ORDER"]);
                    ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);

                    ob.name = ob.DESIGNATION_NAME_EN;

                    obList.Add(ob);
                }

                return obList;                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<HrDesigModel> EntitledDesigListData(Int64 pHR_OT_TEAM_ID)
        {
            string sp = "pkg_hr.hr_designation_select";
            try
            {
                var obList = new List<HrDesigModel>();

                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
                    new CommandParameter() {ParameterName = "pHR_OT_TEAM_ID", Value = pHR_OT_TEAM_ID},
                    new CommandParameter() {ParameterName = "pOption", Value = 3007},                    
                    new CommandParameter() {ParameterName = "pMsg", Value = 500, Direction = ParameterDirection.Output}
                }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    HrDesigModel ob = new HrDesigModel();

                    ob.HR_DESIGNATION_ID = (dr["HR_DESIGNATION_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_DESIGNATION_ID"]);
                    ob.DESIGNATION_CODE = (dr["DESIGNATION_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DESIGNATION_CODE"]);
                    ob.HR_DEPARTMENT_ID = (dr["HR_DEPARTMENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_DEPARTMENT_ID"]);
                    ob.DESIGNATION_NAME_EN = (dr["DESIGNATION_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DESIGNATION_NAME_EN"]);
                    ob.DESIGNATION_NAME_BN = (dr["DESIGNATION_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DESIGNATION_NAME_BN"]);
                    ob.DESIGNATION_DESC = (dr["DESIGNATION_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DESIGNATION_DESC"]);
                    ob.DESIGNATION_SNAME = (dr["DESIGNATION_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DESIGNATION_SNAME"]);
                    ob.HR_DESIGNATION_GRP_ID = (dr["HR_DESIGNATION_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_DESIGNATION_GRP_ID"]);
                    ob.HR_MANAGEMENT_TYPE_ID = (dr["HR_MANAGEMENT_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_MANAGEMENT_TYPE_ID"]);
                    ob.HR_GRADE_ID = (dr["HR_GRADE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_GRADE_ID"]);
                    ob.IS_GAJETED = (dr["IS_GAJETED"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_GAJETED"]);
                    ob.DESIG_ORDER = (dr["DESIG_ORDER"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DESIG_ORDER"]);
                    ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);

                    ob.name = ob.DESIGNATION_NAME_EN;

                    obList.Add(ob);
                }

                return obList;                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<HrDesigModel> DesigListData(Int64? pOrganoDeptId)
        {
            try
            {
                //string strSQL = "select a.* from HR_DESIGNATION a where HR_DEPARTMENT_ID like nvl('" + pOrganoDeptId + "','%') and IS_ACTIVE='Y' order by DESIGNATION_NAME_EN";
                string sp = "pkg_hr.hr_designation_select";
                var obList = new List<HrDesigModel>();

                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {  
                    new CommandParameter() {ParameterName = "pHR_DEPARTMENT_ID", Value = pOrganoDeptId},
                    new CommandParameter() {ParameterName = "pOption", Value = 3005},                    
                    new CommandParameter() {ParameterName = "pMsg", Value = 500, Direction = ParameterDirection.Output}
                }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    HrDesigModel ob = new HrDesigModel();

                    ob.HR_DEPARTMENT_ID = (dr["HR_DEPARTMENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_DEPARTMENT_ID"]);
                    ob.HR_DESIGNATION_ID = (dr["HR_DESIGNATION_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_DESIGNATION_ID"]);
                    ob.DESIGNATION_CODE = (dr["DESIGNATION_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DESIGNATION_CODE"]);
                    ob.DESIGNATION_NAME_EN = (dr["DESIGNATION_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DESIGNATION_NAME_EN"]);
                    ob.HR_GRADE_ID = (dr["HR_GRADE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_GRADE_ID"]);
                    ob.DESIG_ORDER = (dr["DESIG_ORDER"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DESIG_ORDER"]);

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
