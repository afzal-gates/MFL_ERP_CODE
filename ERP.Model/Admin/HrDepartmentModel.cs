using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Web;

namespace ERP.Model
{
    public class HrDepartmentModel //: IHrDepartmentModel
    {
        public Int64 HR_DEPARTMENT_ID { get; set; }
        public Int64 PARENT_ID { get; set; }
        public string DEPARTMENT_CODE { get; set; }

        [Required]        
        public string DEPARTMENT_NAME_EN { get; set; }
        public string DEPARTMENT_NAME_BN { get; set; }
        public string DEPARTMENT_DESC { get; set; }
        public Int64 DEPT_ORDER { get; set; }
        public string DEPARTMENT_PREFIX { get; set; }
        public int DEPARTMENT_LEVEL { get; set; }
        public string IS_CORE_DEPT { get; set; }
        public string IS_ORGANO_LEVEL { get; set; }
        public string IS_LEAF { get; set; }
        public string IS_ACTIVE { get; set; }

        public DateTime? CREATION_DATE { get; set; }
        public int CREATED_BY { get; set; }
        public DateTime? LAST_UPDATE_DATE { get; set; }
        public int LAST_UPDATED_BY { get; set; }
        public int LAST_UPDATE_LOGIN { get; set; }
        public int VERSION_NO { get; set; }


        public long? CORE_DEPT_ID { get; set; }
        public string PARENT_NAME { get; set; }
        public string text { get; set; }
        public string spriteCssClass { get; set; }
        public bool expanded { get; set; }
        public bool selected { get; set; }

        private List<DeptLevel1Model> _SubDeptModel = null;
        public List<DeptLevel1Model> items 
        {
            get
            {
                if (_SubDeptModel == null)
                {
                    _SubDeptModel = new List<DeptLevel1Model>();
                }
                return _SubDeptModel;
            }
            set
            {
                _SubDeptModel = value;
            }
        }

        public int TranMode {get;set;}




        public object GetSection4IncrProp()
        {
            string sp = "pkg_admin.hr_department_select";
            try
            {
                int vOption = 3003;
                if(Convert.ToString(HttpContext.Current.Session["multiUserType"])=="B")
                    vOption = 3004;
                

                var obList = new List<HrDepartmentModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
                    new CommandParameter() {ParameterName = "pSC_USER_ID", Value = Convert.ToInt64(HttpContext.Current.Session["multiScUserId"])},
                    new CommandParameter() {ParameterName = "pOption", Value = vOption}
                }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    HrDepartmentModel ob = new HrDepartmentModel();
                    ob.HR_DEPARTMENT_ID = (dr["HR_DEPARTMENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_DEPARTMENT_ID"]);
                    //ob.PARENT_ID = (dr["PARENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PARENT_ID"]);
                    ob.DEPARTMENT_CODE = (dr["DEPARTMENT_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DEPARTMENT_CODE"]);
                    //ob.PARENT_NAME = (dr["PARENT_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PARENT_NAME"]);
                    //ob.text = (dr["DEPARTMENT_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DEPARTMENT_NAME_EN"]);
                    //ob.expanded = true;
                    //ob.selected = false;
                    ob.DEPARTMENT_NAME_EN = (dr["SECTION_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SECTION_NAME"]);
                    ob.DEPARTMENT_NAME_BN = (dr["DEPARTMENT_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DEPARTMENT_NAME_BN"]);
                    ob.DEPARTMENT_LEVEL = (dr["DEPARTMENT_LEVEL"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["DEPARTMENT_LEVEL"]);
                    ob.DEPARTMENT_PREFIX = (dr["DEPARTMENT_PREFIX"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DEPARTMENT_PREFIX"]);
                    ob.DEPARTMENT_DESC = (dr["DEPARTMENT_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DEPARTMENT_DESC"]);
                    ob.DEPT_ORDER = (dr["DEPT_ORDER"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DEPT_ORDER"]);
                    ob.IS_LEAF = (dr["IS_LEAF"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_LEAF"]);
                    ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);
                    ob.CORE_DEPT_ID = (dr["CORE_DEPT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CORE_DEPT_ID"]);
                    
                    obList.Add(ob);
                }

                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public List<HrDepartmentModel> ParentDeptListData()
        {
            string sp = "pkg_admin.hr_department_select";
            try
            {
                var obList = new List<HrDepartmentModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
                    //new CommandParameter() {ParameterName = "pSC_USER_ID", Value = pSC_USER_ID},
                    new CommandParameter() {ParameterName = "pOption", Value = 3001}
                }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    HrDepartmentModel ob = new HrDepartmentModel();
                    ob.HR_DEPARTMENT_ID = (dr["HR_DEPARTMENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_DEPARTMENT_ID"]);
                    //ob.PARENT_ID = (dr["PARENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PARENT_ID"]);
                    ob.DEPARTMENT_CODE = (dr["DEPARTMENT_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DEPARTMENT_CODE"]);
                    //ob.PARENT_NAME = (dr["PARENT_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PARENT_NAME"]);
                    //ob.text = (dr["DEPARTMENT_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DEPARTMENT_NAME_EN"]);
                    //ob.expanded = true;
                    //ob.selected = false;
                    ob.DEPARTMENT_NAME_EN = (dr["DEPARTMENT_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DEPARTMENT_NAME_EN"]);
                    ob.DEPARTMENT_NAME_BN = (dr["DEPARTMENT_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DEPARTMENT_NAME_BN"]);
                    ob.DEPARTMENT_LEVEL = (dr["DEPARTMENT_LEVEL"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["DEPARTMENT_LEVEL"]);
                    ob.DEPARTMENT_PREFIX = (dr["DEPARTMENT_PREFIX"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DEPARTMENT_PREFIX"]);
                    ob.DEPARTMENT_DESC = (dr["DEPARTMENT_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DEPARTMENT_DESC"]);
                    ob.DEPT_ORDER = (dr["DEPT_ORDER"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DEPT_ORDER"]);
                    ob.IS_LEAF = (dr["IS_LEAF"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_LEAF"]);
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
        public List<HrDepartmentModel> DeptListData(Int64? pPARENT_ID)
        {
            string sp = "pkg_admin.hr_department_select";
            try
            {
                var obList = new List<HrDepartmentModel>();

                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   new CommandParameter() {ParameterName = "pOption", Value = 3002},
                    new CommandParameter() {ParameterName = "pPARENT_ID", Value = pPARENT_ID}
                }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    HrDepartmentModel ob = new HrDepartmentModel();
                    ob.HR_DEPARTMENT_ID = (dr["HR_DEPARTMENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_DEPARTMENT_ID"]);
                    ob.PARENT_ID = (dr["PARENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PARENT_ID"]);
                    ob.DEPARTMENT_CODE = (dr["DEPARTMENT_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DEPARTMENT_CODE"]);
                    //ob.PARENT_NAME = (dr["PARENT_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PARENT_NAME"]);
                    //ob.text = (dr["DEPARTMENT_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DEPARTMENT_NAME_EN"]);
                    //ob.expanded = true;
                    //ob.selected = false;
                    ob.DEPARTMENT_NAME_EN = (dr["SECTION_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SECTION_NAME"]);
                    ob.DEPARTMENT_NAME_BN = (dr["DEPARTMENT_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DEPARTMENT_NAME_BN"]);
                    ob.DEPARTMENT_LEVEL = (dr["DEPARTMENT_LEVEL"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["DEPARTMENT_LEVEL"]);
                    //ob.DEPARTMENT_PREFIX = (dr["DEPARTMENT_PREFIX"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DEPARTMENT_PREFIX"]);
                    //ob.DEPARTMENT_DESC = (dr["DEPARTMENT_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DEPARTMENT_DESC"]);
                    ob.DEPT_ORDER = (dr["DEPT_ORDER"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DEPT_ORDER"]);
                    ob.IS_LEAF = (dr["IS_LEAF"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_LEAF"]);
                    ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);

                    ob.CORE_DEPT_ID = (dr["CORE_DEPT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CORE_DEPT_ID"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<HrDepartmentModel> getHrDeptsForTna()
        {
            string sp = "pkg_admin.hr_department_select";
            try
            {
                var obList = new List<HrDepartmentModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
                    new CommandParameter() {ParameterName = "pOption", Value = 3006}
                }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    HrDepartmentModel ob = new HrDepartmentModel();
                    ob.HR_DEPARTMENT_ID = (dr["HR_DEPARTMENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_DEPARTMENT_ID"]);
                    ob.DEPARTMENT_NAME_EN = (dr["DEPARTMENT_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DEPARTMENT_NAME_EN"]);
                    obList.Add(ob);

                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<HrDepartmentModel> getDeptByCompanyOffice(Int64? pHR_COMPANY_ID, Int64? pHR_OFFICE_ID)
        {
            string sp = "pkg_admin.hr_department_select";
            try
            {
                var obList = new List<HrDepartmentModel>();

                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
                    new CommandParameter() {ParameterName = "pOption", Value = 3005},
                    new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = pHR_COMPANY_ID},
                    new CommandParameter() {ParameterName = "pHR_OFFICE_ID", Value = pHR_OFFICE_ID}

                }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    HrDepartmentModel ob = new HrDepartmentModel();
                    ob.HR_DEPARTMENT_ID = (dr["HR_DEPARTMENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_DEPARTMENT_ID"]);
                    ob.DEPARTMENT_NAME_EN = (dr["DEPARTMENT_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DEPARTMENT_NAME_EN"]);   
                    obList.Add(ob);

                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<HrDepartmentModel> SelectAll()
        {
            string sp = "pkg_admin.hr_department_select";
            try
            {
                var obList = new List<HrDepartmentModel>();

                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   new CommandParameter() {ParameterName = "pOption", Value = 3000},
                    new CommandParameter() {ParameterName = "pHR_DEPARTMENT_ID", Value = 0}
                }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    HrDepartmentModel ob = new HrDepartmentModel();
                    ob.HR_DEPARTMENT_ID = (dr["HR_DEPARTMENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_DEPARTMENT_ID"]);
                    ob.PARENT_ID = (dr["PARENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PARENT_ID"]);
                    ob.DEPARTMENT_CODE = (dr["DEPARTMENT_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DEPARTMENT_CODE"]);
                    ob.DEPARTMENT_NAME_EN = (dr["DEPARTMENT_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DEPARTMENT_NAME_EN"]);
                    ob.DEPARTMENT_NAME_BN = (dr["DEPARTMENT_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DEPARTMENT_NAME_BN"]);
                    ob.DEPARTMENT_DESC = (dr["DEPARTMENT_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DEPARTMENT_DESC"]);
                    ob.DEPARTMENT_PREFIX = (dr["DEPARTMENT_PREFIX"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DEPARTMENT_PREFIX"]);
                    ob.IS_LEAF = (dr["IS_LEAF"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_LEAF"]);
                    ob.DEPT_ORDER = (dr["DEPT_ORDER"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DEPT_ORDER"]);
                    ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.VERSION_NO = (dr["VERSION_NO"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["VERSION_NO"]);
                    ob.IS_ORGANO_LEVEL = (dr["IS_ORGANO_LEVEL"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ORGANO_LEVEL"]);
                    ob.IS_CORE_DEPT = (dr["IS_CORE_DEPT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_CORE_DEPT"]);
                    ob.PARENT_NAME = (dr["PARENT_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PARENT_NAME"]);
                    obList.Add(ob);

                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<HrDepartmentModel> DeptListData()
        {
            try
            {
                Int64 V_COUNT = 0;
                Int64 V_PARENT_ID = 0;
                Int64 V_CAT_ID = 0;

                List<HrDepartmentModel> obList = new List<HrDepartmentModel>();
                OraDatabase db = new OraDatabase();

                var ds = db.ExecuteSQLStatement("select a.DEPARTMENT_NAME_EN as PARENT_NAME, a.* from HR_DEPARTMENT a where PARENT_ID is null order by DEPT_ORDER");
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    V_COUNT = (dr["PARENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PARENT_ID"]);
                    V_PARENT_ID = (dr["PARENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PARENT_ID"]);
                    V_CAT_ID = Convert.ToInt64(dr["HR_DEPARTMENT_ID"]);

                    HrDepartmentModel ob = new HrDepartmentModel();

                    ob.HR_DEPARTMENT_ID = (dr["HR_DEPARTMENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_DEPARTMENT_ID"]);
                    ob.PARENT_ID = (dr["PARENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PARENT_ID"]);
                    ob.DEPARTMENT_CODE = (dr["DEPARTMENT_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DEPARTMENT_CODE"]);
                    ob.PARENT_NAME = (dr["PARENT_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PARENT_NAME"]);
                    ob.text = (dr["DEPARTMENT_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DEPARTMENT_NAME_EN"]);
                    ob.expanded = true;
                    ob.selected = false;
                    ob.DEPARTMENT_NAME_EN = (dr["DEPARTMENT_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DEPARTMENT_NAME_EN"]);
                    ob.DEPARTMENT_NAME_BN = (dr["DEPARTMENT_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DEPARTMENT_NAME_BN"]);
                    ob.DEPARTMENT_LEVEL = (dr["DEPARTMENT_LEVEL"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["DEPARTMENT_LEVEL"]);
                    ob.DEPARTMENT_PREFIX = (dr["DEPARTMENT_PREFIX"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DEPARTMENT_PREFIX"]);
                    ob.DEPARTMENT_DESC = (dr["DEPARTMENT_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DEPARTMENT_DESC"]);
                    ob.DEPT_ORDER = (dr["DEPT_ORDER"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DEPT_ORDER"]);
                    ob.IS_CORE_DEPT = (dr["IS_CORE_DEPT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_CORE_DEPT"]);
                    ob.IS_ORGANO_LEVEL = (dr["IS_ORGANO_LEVEL"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ORGANO_LEVEL"]);
                    ob.IS_LEAF = (dr["IS_LEAF"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_LEAF"]);
                    ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);

                    if (ob.IS_LEAF == "Y")
                        ob.spriteCssClass = "html";
                    else
                        ob.spriteCssClass = "folder";

                    var ds1 = db.ExecuteSQLStatement("select a.DEPARTMENT_NAME_EN as PARENT_NAME, b.* from HR_DEPARTMENT a inner join HR_DEPARTMENT b on a.HR_DEPARTMENT_ID=B.PARENT_ID where b.PARENT_ID = '" + dr["HR_DEPARTMENT_ID"] + "' order by b.DEPT_ORDER");
                    foreach (DataRow dr1 in ds1.Tables[0].Rows)
                    {
                        DeptLevel1Model deptLevel1 = new DeptLevel1Model();
                        deptLevel1.HR_DEPARTMENT_ID = (dr1["HR_DEPARTMENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr1["HR_DEPARTMENT_ID"]);
                        deptLevel1.PARENT_ID = (dr1["PARENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr1["PARENT_ID"]);
                        deptLevel1.DEPARTMENT_CODE = (dr1["DEPARTMENT_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["DEPARTMENT_CODE"]);
                        deptLevel1.PARENT_NAME = (dr1["PARENT_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["PARENT_NAME"]);
                        deptLevel1.text = (dr1["DEPARTMENT_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["DEPARTMENT_NAME_EN"]);
                        deptLevel1.expanded = false;
                        deptLevel1.selected = false;
                        deptLevel1.DEPARTMENT_NAME_EN = (dr1["DEPARTMENT_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["DEPARTMENT_NAME_EN"]);
                        deptLevel1.DEPARTMENT_NAME_BN = (dr1["DEPARTMENT_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["DEPARTMENT_NAME_BN"]);
                        deptLevel1.DEPARTMENT_LEVEL = (dr1["DEPARTMENT_LEVEL"] == DBNull.Value) ? 0 : Convert.ToInt32(dr1["DEPARTMENT_LEVEL"]);
                        deptLevel1.DEPARTMENT_PREFIX = (dr1["DEPARTMENT_PREFIX"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["DEPARTMENT_PREFIX"]);
                        deptLevel1.DEPARTMENT_DESC = (dr1["DEPARTMENT_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["DEPARTMENT_DESC"]);
                        deptLevel1.DEPT_ORDER = (dr1["DEPT_ORDER"] == DBNull.Value) ? 0 : Convert.ToInt64(dr1["DEPT_ORDER"]);
                        deptLevel1.IS_CORE_DEPT = (dr1["IS_CORE_DEPT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["IS_CORE_DEPT"]);
                        deptLevel1.IS_ORGANO_LEVEL = (dr1["IS_ORGANO_LEVEL"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["IS_ORGANO_LEVEL"]);
                        deptLevel1.IS_LEAF = (dr1["IS_LEAF"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["IS_LEAF"]);
                        deptLevel1.IS_ACTIVE = (dr1["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["IS_ACTIVE"]);
                        if (deptLevel1.IS_LEAF == "Y")
                            deptLevel1.spriteCssClass = "html";
                        else
                            deptLevel1.spriteCssClass = "folder";

                        var ds2 = db.ExecuteSQLStatement("select a.DEPARTMENT_NAME_EN as PARENT_NAME, b.* from HR_DEPARTMENT a inner join HR_DEPARTMENT b on a.HR_DEPARTMENT_ID=B.PARENT_ID where b.PARENT_ID = '" + dr1["HR_DEPARTMENT_ID"] + "' order by b.DEPT_ORDER");
                        foreach (DataRow dr2 in ds2.Tables[0].Rows)
                        {
                            DeptLevel2Model deptLevel2 = new DeptLevel2Model();
                            deptLevel2.HR_DEPARTMENT_ID = (dr2["HR_DEPARTMENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr2["HR_DEPARTMENT_ID"]);
                            deptLevel2.PARENT_ID = (dr2["PARENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr2["PARENT_ID"]);
                            deptLevel2.DEPARTMENT_CODE = (dr2["DEPARTMENT_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr2["DEPARTMENT_CODE"]);
                            deptLevel2.PARENT_NAME = (dr2["PARENT_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr2["PARENT_NAME"]);
                            deptLevel2.text = (dr2["DEPARTMENT_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr2["DEPARTMENT_NAME_EN"]);
                            deptLevel2.expanded = false;
                            deptLevel2.selected = false;
                            deptLevel2.DEPARTMENT_NAME_EN = (dr2["DEPARTMENT_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr2["DEPARTMENT_NAME_EN"]);
                            deptLevel2.DEPARTMENT_NAME_BN = (dr2["DEPARTMENT_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr2["DEPARTMENT_NAME_BN"]);
                            deptLevel2.DEPARTMENT_LEVEL = (dr2["DEPARTMENT_LEVEL"] == DBNull.Value) ? 0 : Convert.ToInt32(dr2["DEPARTMENT_LEVEL"]);
                            deptLevel2.DEPARTMENT_PREFIX = (dr2["DEPARTMENT_PREFIX"] == DBNull.Value) ? string.Empty : Convert.ToString(dr2["DEPARTMENT_PREFIX"]);
                            deptLevel2.DEPARTMENT_DESC = (dr2["DEPARTMENT_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr2["DEPARTMENT_DESC"]);
                            deptLevel2.DEPT_ORDER = (dr2["DEPT_ORDER"] == DBNull.Value) ? 0 : Convert.ToInt64(dr2["DEPT_ORDER"]);
                            deptLevel2.IS_CORE_DEPT = (dr2["IS_CORE_DEPT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr2["IS_CORE_DEPT"]);
                            deptLevel2.IS_ORGANO_LEVEL = (dr2["IS_ORGANO_LEVEL"] == DBNull.Value) ? string.Empty : Convert.ToString(dr2["IS_ORGANO_LEVEL"]);
                            deptLevel2.IS_LEAF = (dr2["IS_LEAF"] == DBNull.Value) ? string.Empty : Convert.ToString(dr2["IS_LEAF"]);
                            deptLevel2.IS_ACTIVE = (dr2["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr2["IS_ACTIVE"]);
                            if (deptLevel2.IS_LEAF == "Y")
                                deptLevel2.spriteCssClass = "html";
                            else
                                deptLevel2.spriteCssClass = "folder";


                            var ds3 = db.ExecuteSQLStatement("select a.DEPARTMENT_NAME_EN as PARENT_NAME, b.* from HR_DEPARTMENT a inner join HR_DEPARTMENT b on a.HR_DEPARTMENT_ID=B.PARENT_ID where b.PARENT_ID = '" + dr2["HR_DEPARTMENT_ID"] + "' order by b.DEPT_ORDER");
                            foreach (DataRow dr3 in ds3.Tables[0].Rows)
                            {
                                DeptLevel3Model deptLevel3 = new DeptLevel3Model();
                                deptLevel3.HR_DEPARTMENT_ID = (dr3["HR_DEPARTMENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr3["HR_DEPARTMENT_ID"]);
                                deptLevel3.PARENT_ID = (dr3["PARENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr3["PARENT_ID"]);
                                deptLevel3.DEPARTMENT_CODE = (dr3["DEPARTMENT_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr3["DEPARTMENT_CODE"]);
                                deptLevel3.PARENT_NAME = (dr3["PARENT_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr3["PARENT_NAME"]);
                                deptLevel3.text = (dr3["DEPARTMENT_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr3["DEPARTMENT_NAME_EN"]);
                                deptLevel3.expanded = false;
                                deptLevel3.selected = false;
                                deptLevel3.DEPARTMENT_NAME_EN = (dr3["DEPARTMENT_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr3["DEPARTMENT_NAME_EN"]);
                                deptLevel3.DEPARTMENT_NAME_BN = (dr3["DEPARTMENT_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr3["DEPARTMENT_NAME_BN"]);
                                deptLevel3.DEPARTMENT_LEVEL = (dr3["DEPARTMENT_LEVEL"] == DBNull.Value) ? 0 : Convert.ToInt32(dr3["DEPARTMENT_LEVEL"]);
                                deptLevel3.DEPARTMENT_PREFIX = (dr3["DEPARTMENT_PREFIX"] == DBNull.Value) ? string.Empty : Convert.ToString(dr3["DEPARTMENT_PREFIX"]);
                                deptLevel3.DEPARTMENT_DESC = (dr3["DEPARTMENT_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr3["DEPARTMENT_DESC"]);
                                deptLevel3.DEPT_ORDER = (dr3["DEPT_ORDER"] == DBNull.Value) ? 0 : Convert.ToInt64(dr3["DEPT_ORDER"]);
                                deptLevel3.IS_CORE_DEPT = (dr3["IS_CORE_DEPT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr3["IS_CORE_DEPT"]);
                                deptLevel3.IS_ORGANO_LEVEL = (dr3["IS_ORGANO_LEVEL"] == DBNull.Value) ? string.Empty : Convert.ToString(dr3["IS_ORGANO_LEVEL"]);
                                deptLevel3.IS_LEAF = (dr3["IS_LEAF"] == DBNull.Value) ? string.Empty : Convert.ToString(dr3["IS_LEAF"]);
                                deptLevel3.IS_ACTIVE = (dr3["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr3["IS_ACTIVE"]);
                                if (deptLevel3.IS_LEAF == "Y")
                                    deptLevel3.spriteCssClass = "html";
                                else
                                    deptLevel3.spriteCssClass = "folder";

                                var ds4 = db.ExecuteSQLStatement("select a.DEPARTMENT_NAME_EN as PARENT_NAME, b.* from HR_DEPARTMENT a inner join HR_DEPARTMENT b on a.HR_DEPARTMENT_ID=B.PARENT_ID where b.PARENT_ID = '" + dr3["HR_DEPARTMENT_ID"] + "' order by b.DEPT_ORDER");
                                foreach (DataRow dr4 in ds4.Tables[0].Rows)
                                {
                                    DeptLevel4Model deptLevel4 = new DeptLevel4Model();
                                    deptLevel4.HR_DEPARTMENT_ID = (dr4["HR_DEPARTMENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr4["HR_DEPARTMENT_ID"]);
                                    deptLevel4.PARENT_ID = (dr4["PARENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr4["PARENT_ID"]);
                                    deptLevel4.DEPARTMENT_CODE = (dr4["DEPARTMENT_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr4["DEPARTMENT_CODE"]);
                                    deptLevel4.PARENT_NAME = (dr4["PARENT_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr4["PARENT_NAME"]);
                                    deptLevel4.text = (dr4["DEPARTMENT_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr4["DEPARTMENT_NAME_EN"]);
                                    deptLevel4.expanded = false;
                                    deptLevel4.selected = false;
                                    deptLevel4.DEPARTMENT_NAME_EN = (dr4["DEPARTMENT_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr4["DEPARTMENT_NAME_EN"]);
                                    deptLevel4.DEPARTMENT_NAME_BN = (dr4["DEPARTMENT_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr4["DEPARTMENT_NAME_BN"]);
                                    deptLevel4.DEPARTMENT_LEVEL = (dr4["DEPARTMENT_LEVEL"] == DBNull.Value) ? 0 : Convert.ToInt32(dr4["DEPARTMENT_LEVEL"]);
                                    deptLevel4.DEPARTMENT_PREFIX = (dr4["DEPARTMENT_PREFIX"] == DBNull.Value) ? string.Empty : Convert.ToString(dr4["DEPARTMENT_PREFIX"]);
                                    deptLevel4.DEPARTMENT_DESC = (dr4["DEPARTMENT_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr4["DEPARTMENT_DESC"]);
                                    deptLevel4.DEPT_ORDER = (dr4["DEPT_ORDER"] == DBNull.Value) ? 0 : Convert.ToInt64(dr4["DEPT_ORDER"]);
                                    deptLevel4.IS_CORE_DEPT = (dr4["IS_CORE_DEPT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr4["IS_CORE_DEPT"]);
                                    deptLevel4.IS_ORGANO_LEVEL = (dr4["IS_ORGANO_LEVEL"] == DBNull.Value) ? string.Empty : Convert.ToString(dr4["IS_ORGANO_LEVEL"]);
                                    deptLevel4.IS_LEAF = (dr4["IS_LEAF"] == DBNull.Value) ? string.Empty : Convert.ToString(dr4["IS_LEAF"]);
                                    deptLevel4.IS_ACTIVE = (dr4["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr4["IS_ACTIVE"]);
                                    if (deptLevel4.IS_LEAF == "Y")
                                        deptLevel4.spriteCssClass = "html";
                                    else
                                        deptLevel4.spriteCssClass = "folder";

                                    deptLevel3.items.Add(deptLevel4);
                                }
                                deptLevel2.items.Add(deptLevel3);
                            }
                            deptLevel1.items.Add(deptLevel2);
                        }
                        ob.items.Add(deptLevel1);
                    }
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
            const string sp = "pkg_admin.hr_department_insert";
            string vMsg = "";
            var ob = this;
            OraDatabase db = new OraDatabase();
            try
            {
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                    new CommandParameter() {ParameterName = "pHR_DEPARTMENT_ID", Value = ob.HR_DEPARTMENT_ID},
                    new CommandParameter() {ParameterName = "pPARENT_ID", Value = ob.PARENT_ID},
                    new CommandParameter() {ParameterName = "pDEPARTMENT_CODE", Value = ob.DEPARTMENT_CODE},
                    new CommandParameter() {ParameterName = "pDEPARTMENT_NAME_EN", Value = ob.DEPARTMENT_NAME_EN},
                    new CommandParameter() {ParameterName = "pDEPARTMENT_NAME_BN", Value = ob.DEPARTMENT_NAME_BN},
                    new CommandParameter() {ParameterName = "pDEPARTMENT_DESC", Value = ob.DEPARTMENT_DESC},
                    new CommandParameter() {ParameterName = "pDEPT_ORDER", Value = ob.DEPT_ORDER},
                    new CommandParameter() {ParameterName = "pDEPARTMENT_PREFIX", Value = ob.DEPARTMENT_PREFIX},
                    new CommandParameter() {ParameterName = "pDEPARTMENT_LEVEL", Value = ob.DEPARTMENT_LEVEL},
                    new CommandParameter() {ParameterName = "pIS_CORE_DEPT", Value = ob.IS_CORE_DEPT == null ? "N" : "Y"},
                    new CommandParameter() {ParameterName = "pIS_ORGANO_LEVEL", Value = ob.IS_ORGANO_LEVEL == null ? "N" : "Y"},
                    new CommandParameter() {ParameterName = "pIS_LEAF", Value = ob.IS_LEAF == null ? "N" : "Y"},
                    new CommandParameter() {ParameterName = "pCREATED_BY", Value =Convert.ToInt64(HttpContext.Current.Session["multiScUserId"])},
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
            const string sp = "pkg_admin.hr_department_update";
            string vMsg = "";
            var ob = this;

            OraDatabase db = new OraDatabase();
            try
            {
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                    new CommandParameter() {ParameterName = "pHR_DEPARTMENT_ID", Value = ob.HR_DEPARTMENT_ID},
                    new CommandParameter() {ParameterName = "pPARENT_ID", Value = ob.PARENT_ID},
                    new CommandParameter() {ParameterName = "pDEPARTMENT_CODE", Value = ob.DEPARTMENT_CODE},
                    new CommandParameter() {ParameterName = "pDEPARTMENT_NAME_EN", Value = ob.DEPARTMENT_NAME_EN},
                    new CommandParameter() {ParameterName = "pDEPARTMENT_NAME_BN", Value = ob.DEPARTMENT_NAME_BN},
                    new CommandParameter() {ParameterName = "pDEPARTMENT_DESC", Value = ob.DEPARTMENT_DESC},
                    new CommandParameter() {ParameterName = "pDEPT_ORDER", Value = ob.DEPT_ORDER},
                    new CommandParameter() {ParameterName = "pDEPARTMENT_PREFIX", Value = ob.DEPARTMENT_PREFIX},
                    new CommandParameter() {ParameterName = "pDEPARTMENT_LEVEL", Value = ob.DEPARTMENT_LEVEL},
                    new CommandParameter() {ParameterName = "pIS_CORE_DEPT", Value = ob.IS_CORE_DEPT == null ? "N" : "Y"},
                    new CommandParameter() {ParameterName = "pIS_ORGANO_LEVEL", Value = ob.IS_ORGANO_LEVEL == null ? "N" : "Y"},
                    new CommandParameter() {ParameterName = "pIS_LEAF", Value = ob.IS_LEAF == null ? "N" : "Y"},
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
        public List<HrDepartmentModel> DepartmentListData()
        {
            try
            {
                var obList = new List<HrDepartmentModel>();

                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteSQLStatement("select * from HR_DEPARTMENT where PARENT_ID is not null " +
                                                                    " and IS_CORE_DEPT='Y' " +
                                                                    " and IS_ACTIVE='Y' " +
                                                                    " ORDER BY PARENT_ID, DEPT_ORDER");
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    HrDepartmentModel ob = new HrDepartmentModel();
                    ob.HR_DEPARTMENT_ID = (dr["HR_DEPARTMENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_DEPARTMENT_ID"]);
                    ob.DEPARTMENT_CODE = (dr["DEPARTMENT_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DEPARTMENT_CODE"]);
                    ob.PARENT_ID = (dr["PARENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PARENT_ID"]);
                    ob.DEPARTMENT_NAME_EN = (dr["DEPARTMENT_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DEPARTMENT_NAME_EN"]);
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
    public class DeptLevel1Model
    {
        public Int64 HR_DEPARTMENT_ID { get; set; }
        public Int64 PARENT_ID { get; set; }
        public string DEPARTMENT_CODE { get; set; }
        public string DEPARTMENT_NAME_EN { get; set; }
        public string DEPARTMENT_NAME_BN { get; set; }
        public string DEPARTMENT_DESC { get; set; }
        public Int64 DEPT_ORDER { get; set; }
        public string DEPARTMENT_PREFIX { get; set; }
        public int DEPARTMENT_LEVEL { get; set; }
        public string IS_CORE_DEPT { get; set; }
        public string IS_ORGANO_LEVEL { get; set; }
        public string IS_LEAF { get; set; }
        public string IS_ACTIVE { get; set; }

        public DateTime? CREATION_DATE { get; set; }
        public int CREATED_BY { get; set; }
        public DateTime? LAST_UPDATE_DATE { get; set; }
        public int LAST_UPDATED_BY { get; set; }
        public int LAST_UPDATE_LOGIN { get; set; }
        public int VERSION_NO { get; set; }


        public string PARENT_NAME { get; set; }
        public string text { get; set; }
        public string spriteCssClass { get; set; }
        public bool expanded { get; set; }
        public bool selected { get; set; }

        private List<DeptLevel2Model> _SubDeptModel = null;
        public List<DeptLevel2Model> items
        {
            get
            {
                if (_SubDeptModel == null)
                {
                    _SubDeptModel = new List<DeptLevel2Model>();
                }
                return _SubDeptModel;
            }
            set
            {
                _SubDeptModel = value;
            }
        }
    }
    public class DeptLevel2Model
    {
        public Int64 HR_DEPARTMENT_ID { get; set; }
        public Int64 PARENT_ID { get; set; }
        public string DEPARTMENT_CODE { get; set; }
        public string DEPARTMENT_NAME_EN { get; set; }
        public string DEPARTMENT_NAME_BN { get; set; }
        public string DEPARTMENT_DESC { get; set; }
        public Int64 DEPT_ORDER { get; set; }
        public string DEPARTMENT_PREFIX { get; set; }
        public int DEPARTMENT_LEVEL { get; set; }
        public string IS_CORE_DEPT { get; set; }
        public string IS_ORGANO_LEVEL { get; set; }
        public string IS_LEAF { get; set; }
        public string IS_ACTIVE { get; set; }

        public DateTime? CREATION_DATE { get; set; }
        public int CREATED_BY { get; set; }
        public DateTime? LAST_UPDATE_DATE { get; set; }
        public int LAST_UPDATED_BY { get; set; }
        public int LAST_UPDATE_LOGIN { get; set; }
        public int VERSION_NO { get; set; }


        public string PARENT_NAME { get; set; }
        public string text { get; set; }
        public string spriteCssClass { get; set; }
        public bool expanded { get; set; }
        public bool selected { get; set; }

        private List<DeptLevel3Model> _SubDeptModel = null;
        public List<DeptLevel3Model> items
        {
            get
            {
                if (_SubDeptModel == null)
                {
                    _SubDeptModel = new List<DeptLevel3Model>();
                }
                return _SubDeptModel;
            }
            set
            {
                _SubDeptModel = value;
            }
        }

    }
    public class DeptLevel3Model
    {
        public Int64 HR_DEPARTMENT_ID { get; set; }
        public Int64 PARENT_ID { get; set; }
        public string DEPARTMENT_CODE { get; set; }
        public string DEPARTMENT_NAME_EN { get; set; }
        public string DEPARTMENT_NAME_BN { get; set; }
        public string DEPARTMENT_DESC { get; set; }
        public Int64 DEPT_ORDER { get; set; }
        public string DEPARTMENT_PREFIX { get; set; }
        public int DEPARTMENT_LEVEL { get; set; }
        public string IS_CORE_DEPT { get; set; }
        public string IS_ORGANO_LEVEL { get; set; }
        public string IS_LEAF { get; set; }
        public string IS_ACTIVE { get; set; }

        public DateTime? CREATION_DATE { get; set; }
        public int CREATED_BY { get; set; }
        public DateTime? LAST_UPDATE_DATE { get; set; }
        public int LAST_UPDATED_BY { get; set; }
        public int LAST_UPDATE_LOGIN { get; set; }
        public int VERSION_NO { get; set; }


        public string PARENT_NAME { get; set; }
        public string text { get; set; }
        public string spriteCssClass { get; set; }
        public bool expanded { get; set; }
        public bool selected { get; set; }

        private List<DeptLevel4Model> _SubDeptModel = null;
        public List<DeptLevel4Model> items
        {
            get
            {
                if (_SubDeptModel == null)
                {
                    _SubDeptModel = new List<DeptLevel4Model>();
                }
                return _SubDeptModel;
            }
            set
            {
                _SubDeptModel = value;
            }
        }

    }
    public class DeptLevel4Model
    {
        public Int64 HR_DEPARTMENT_ID { get; set; }
        public Int64 PARENT_ID { get; set; }
        public string DEPARTMENT_CODE { get; set; }
        public string DEPARTMENT_NAME_EN { get; set; }
        public string DEPARTMENT_NAME_BN { get; set; }
        public string DEPARTMENT_DESC { get; set; }
        public Int64 DEPT_ORDER { get; set; }
        public string DEPARTMENT_PREFIX { get; set; }
        public int DEPARTMENT_LEVEL { get; set; }
        public string IS_CORE_DEPT { get; set; }
        public string IS_ORGANO_LEVEL { get; set; }
        public string IS_LEAF { get; set; }
        public string IS_ACTIVE { get; set; }

        public DateTime? CREATION_DATE { get; set; }
        public int CREATED_BY { get; set; }
        public DateTime? LAST_UPDATE_DATE { get; set; }
        public int LAST_UPDATED_BY { get; set; }
        public int LAST_UPDATE_LOGIN { get; set; }
        public int VERSION_NO { get; set; }


        public string PARENT_NAME { get; set; }
        public string text { get; set; }
        public string spriteCssClass { get; set; }
        public bool expanded { get; set; }
        public bool selected { get; set; }

        //private List<DeptLevel5Model> _SubDeptModel = null;
        //public List<DeptLevel5Model> items
        //{
        //    get
        //    {
        //        if (_SubDeptModel == null)
        //        {
        //            _SubDeptModel = new List<DeptLevel5Model>();
        //        }
        //        return _SubDeptModel;
        //    }
        //    set
        //    {
        //        _SubDeptModel = value;
        //    }
        //}

    }

   
}
