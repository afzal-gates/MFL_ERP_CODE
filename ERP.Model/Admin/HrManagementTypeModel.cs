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
    public class HrManagementTypeModel
    {
        public Int64 HR_MANAGEMENT_TYPE_ID { get; set; }                
        public Int64 PARENT_ID { get; set; }

        [Display(Name = "Type Name [E]")]
        [Required]
        public string MNG_TYPE_NAME_EN { get; set; }

        [Display(Name = "Type Name [B]")]
        [Required]
        public string MNG_TYPE_NAME_BN { get; set; }

        [Display(Name = "Short Name")]
        [Required]
        public string MNG_TYPE_SNAME { get; set; }

        [Display(Name = "Level")]
        [Required]
        public Int64 MNG_TYPE_LEVEL { get; set; }

        [Display(Name = "Is Leaf?")]        
        public string IS_LEAF { get; set; }

        [Display(Name = "Active?")]
        public string IS_ACTIVE { get; set; }


        public DateTime? CREATION_DATE { get; set; }
        public int CREATED_BY { get; set; }
        public DateTime? LAST_UPDATE_DATE { get; set; }
        public int LAST_UPDATED_BY { get; set; }


        public string PARENT_NAME { get; set; }
        public string text { get; set; }
        public string spriteCssClass { get; set; }
        public bool expanded { get; set; }

        private List<HrManagementTypeModel1> _subHrManagementTypeModel = null;
        public List<HrManagementTypeModel1> items
        {
            get
            {
                if (_subHrManagementTypeModel == null)
                {
                    _subHrManagementTypeModel = new List<HrManagementTypeModel1>();
                }
                return _subHrManagementTypeModel;
            }
            set
            {
                _subHrManagementTypeModel = value;
            }
        }

        public int TranMode { get; set; }

        public List<HrManagementTypeModel> ManagementTypeListData()
        {
            try
            {
                var obList = new List<HrManagementTypeModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteSQLStatement("select * from HR_MANAGEMENT_TYPE where PARENT_ID is not null order by MNG_TYPE_NAME_EN");
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    HrManagementTypeModel ob = new HrManagementTypeModel();
                    ob.HR_MANAGEMENT_TYPE_ID = (dr["HR_MANAGEMENT_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_MANAGEMENT_TYPE_ID"]);
                    ob.MNG_TYPE_NAME_EN = (dr["MNG_TYPE_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MNG_TYPE_NAME_EN"]);
                    ob.PARENT_ID = (dr["PARENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PARENT_ID"]);
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
        public List<HrManagementTypeModel> EmployeeTypeListData()
        {
            try
            {
                var obList = new List<HrManagementTypeModel>();

                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteSQLStatement("select * from HR_MANAGEMENT_TYPE where MNG_TYPE_LEVEL=2 order by MNG_TYPE_NAME_EN");
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    HrManagementTypeModel ob = new HrManagementTypeModel();
                    ob.HR_MANAGEMENT_TYPE_ID = (dr["HR_MANAGEMENT_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_MANAGEMENT_TYPE_ID"]);
                    ob.MNG_TYPE_NAME_EN = (dr["MNG_TYPE_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MNG_TYPE_NAME_EN"]);
                    ob.PARENT_ID = (dr["PARENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PARENT_ID"]);
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
        public List<HrManagementTypeModel> HrManagementTypeData()
        {
            string sp = "pkg_hr.hr_management_type_select";

            try
            {
                var obList = new List<HrManagementTypeModel>();

                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   new CommandParameter() {ParameterName = "pOption", Value = 3001},
                    new CommandParameter() {ParameterName = "pPARENT_ID", Value = null}
                }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    HrManagementTypeModel ob = new HrManagementTypeModel();

                    ob.HR_MANAGEMENT_TYPE_ID = (dr["HR_MANAGEMENT_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_MANAGEMENT_TYPE_ID"]);
                    ob.PARENT_ID = (dr["PARENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PARENT_ID"]);
                    ob.MNG_TYPE_NAME_EN = (dr["MNG_TYPE_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MNG_TYPE_NAME_EN"]);
                    ob.MNG_TYPE_NAME_BN = (dr["MNG_TYPE_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MNG_TYPE_NAME_BN"]);
                    ob.MNG_TYPE_SNAME = (dr["MNG_TYPE_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MNG_TYPE_SNAME"]);
                    ob.MNG_TYPE_LEVEL = (dr["MNG_TYPE_LEVEL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MNG_TYPE_LEVEL"]);
                    ob.IS_LEAF = (dr["IS_LEAF"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_LEAF"]);
                    ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);
                    ob.PARENT_NAME = (dr["PARENT_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PARENT_NAME"]);
                    ob.text = (dr["MNG_TYPE_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MNG_TYPE_NAME_EN"]);
                    ob.expanded = true;


                    var ds1 = db.ExecuteStoredProcedure(new List<CommandParameter>()
                        {   new CommandParameter() {ParameterName = "pOption", Value = 3002},
                            new CommandParameter() {ParameterName = "pPARENT_ID", Value = ob.HR_MANAGEMENT_TYPE_ID}
                        }, sp);
                    foreach (DataRow dr1 in ds1.Tables[0].Rows)
                    {
                        HrManagementTypeModel1 ob1 = new HrManagementTypeModel1();

                        ob1.HR_MANAGEMENT_TYPE_ID = (dr1["HR_MANAGEMENT_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr1["HR_MANAGEMENT_TYPE_ID"]);
                        ob1.PARENT_ID = (dr1["PARENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr1["PARENT_ID"]);
                        ob1.MNG_TYPE_NAME_EN = (dr1["MNG_TYPE_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["MNG_TYPE_NAME_EN"]);
                        ob1.MNG_TYPE_NAME_BN = (dr1["MNG_TYPE_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["MNG_TYPE_NAME_BN"]);
                        ob1.MNG_TYPE_SNAME = (dr1["MNG_TYPE_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["MNG_TYPE_SNAME"]);
                        ob1.MNG_TYPE_LEVEL = (dr1["MNG_TYPE_LEVEL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr1["MNG_TYPE_LEVEL"]);
                        ob1.IS_LEAF = (dr1["IS_LEAF"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["IS_LEAF"]);
                        ob1.IS_ACTIVE = (dr1["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["IS_ACTIVE"]);
                        ob1.PARENT_NAME = (dr1["PARENT_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["PARENT_NAME"]);
                        ob1.text = (dr1["MNG_TYPE_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["MNG_TYPE_NAME_EN"]);
                        ob1.expanded = false;

                        var ds2 = db.ExecuteStoredProcedure(new List<CommandParameter>()
                        {   new CommandParameter() {ParameterName = "pOption", Value = 3002},
                            new CommandParameter() {ParameterName = "pPARENT_ID", Value = ob1.HR_MANAGEMENT_TYPE_ID}
                        }, sp);
                        foreach (DataRow dr2 in ds2.Tables[0].Rows)
                        {
                            HrManagementTypeModel2 ob2 = new HrManagementTypeModel2();

                            ob2.HR_MANAGEMENT_TYPE_ID = (dr2["HR_MANAGEMENT_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr2["HR_MANAGEMENT_TYPE_ID"]);
                            ob2.PARENT_ID = (dr2["PARENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr2["PARENT_ID"]);
                            ob2.MNG_TYPE_NAME_EN = (dr2["MNG_TYPE_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr2["MNG_TYPE_NAME_EN"]);
                            ob2.MNG_TYPE_NAME_BN = (dr2["MNG_TYPE_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr2["MNG_TYPE_NAME_BN"]);
                            ob2.MNG_TYPE_SNAME = (dr2["MNG_TYPE_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr2["MNG_TYPE_SNAME"]);
                            ob2.MNG_TYPE_LEVEL = (dr2["MNG_TYPE_LEVEL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr2["MNG_TYPE_LEVEL"]);
                            ob2.IS_LEAF = (dr2["IS_LEAF"] == DBNull.Value) ? string.Empty : Convert.ToString(dr2["IS_LEAF"]);
                            ob2.IS_ACTIVE = (dr2["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr2["IS_ACTIVE"]);
                            ob2.PARENT_NAME = (dr2["PARENT_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr2["PARENT_NAME"]);
                            ob2.text = (dr2["MNG_TYPE_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr2["MNG_TYPE_NAME_EN"]);
                            ob2.expanded = false;

                            var ds3 = db.ExecuteStoredProcedure(new List<CommandParameter>()
                                {   new CommandParameter() {ParameterName = "pOption", Value = 3002},
                                    new CommandParameter() {ParameterName = "pPARENT_ID", Value = ob2.HR_MANAGEMENT_TYPE_ID}
                                }, sp);
                                foreach (DataRow dr3 in ds3.Tables[0].Rows)
                                {
                                    HrManagementTypeModel3 ob3 = new HrManagementTypeModel3();

                                    ob3.HR_MANAGEMENT_TYPE_ID = (dr3["HR_MANAGEMENT_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr3["HR_MANAGEMENT_TYPE_ID"]);
                                    ob3.PARENT_ID = (dr3["PARENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr3["PARENT_ID"]);
                                    ob3.MNG_TYPE_NAME_EN = (dr3["MNG_TYPE_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr3["MNG_TYPE_NAME_EN"]);
                                    ob3.MNG_TYPE_NAME_BN = (dr3["MNG_TYPE_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr3["MNG_TYPE_NAME_BN"]);
                                    ob3.MNG_TYPE_SNAME = (dr3["MNG_TYPE_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr3["MNG_TYPE_SNAME"]);
                                    ob3.MNG_TYPE_LEVEL = (dr3["MNG_TYPE_LEVEL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr3["MNG_TYPE_LEVEL"]);
                                    ob3.IS_LEAF = (dr3["IS_LEAF"] == DBNull.Value) ? string.Empty : Convert.ToString(dr3["IS_LEAF"]);
                                    ob3.IS_ACTIVE = (dr3["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr3["IS_ACTIVE"]);
                                    ob3.PARENT_NAME = (dr3["PARENT_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr3["PARENT_NAME"]);
                                    ob3.text = (dr3["MNG_TYPE_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr3["MNG_TYPE_NAME_EN"]);
                                    ob3.expanded = false;

                                    var ds4 = db.ExecuteStoredProcedure(new List<CommandParameter>()
                                    {   new CommandParameter() {ParameterName = "pOption", Value = 3002},
                                        new CommandParameter() {ParameterName = "pPARENT_ID", Value = ob3.HR_MANAGEMENT_TYPE_ID}
                                    }, sp);
                                    foreach (DataRow dr4 in ds4.Tables[0].Rows)
                                    {
                                        HrManagementTypeModel4 ob4 = new HrManagementTypeModel4();

                                        ob4.HR_MANAGEMENT_TYPE_ID = (dr4["HR_MANAGEMENT_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr4["HR_MANAGEMENT_TYPE_ID"]);
                                        ob4.PARENT_ID = (dr4["PARENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr4["PARENT_ID"]);
                                        ob4.MNG_TYPE_NAME_EN = (dr4["MNG_TYPE_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr4["MNG_TYPE_NAME_EN"]);
                                        ob4.MNG_TYPE_NAME_BN = (dr4["MNG_TYPE_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr4["MNG_TYPE_NAME_BN"]);
                                        ob4.MNG_TYPE_SNAME = (dr4["MNG_TYPE_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr4["MNG_TYPE_SNAME"]);
                                        ob4.MNG_TYPE_LEVEL = (dr4["MNG_TYPE_LEVEL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr4["MNG_TYPE_LEVEL"]);
                                        ob4.IS_LEAF = (dr4["IS_LEAF"] == DBNull.Value) ? string.Empty : Convert.ToString(dr4["IS_LEAF"]);
                                        ob4.IS_ACTIVE = (dr4["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr4["IS_ACTIVE"]);
                                        ob4.PARENT_NAME = (dr4["PARENT_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr4["PARENT_NAME"]);
                                        ob4.text = (dr4["MNG_TYPE_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr4["MNG_TYPE_NAME_EN"]);
                                        ob4.expanded = false;

                                        var ds5 = db.ExecuteStoredProcedure(new List<CommandParameter>()
                                        {   new CommandParameter() {ParameterName = "pOption", Value = 3002},
                                            new CommandParameter() {ParameterName = "pPARENT_ID", Value = ob4.HR_MANAGEMENT_TYPE_ID}
                                        }, sp);
                                        foreach (DataRow dr5 in ds5.Tables[0].Rows)
                                        {
                                            HrManagementTypeModel5 ob5 = new HrManagementTypeModel5();

                                            ob5.HR_MANAGEMENT_TYPE_ID = (dr5["HR_MANAGEMENT_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr5["HR_MANAGEMENT_TYPE_ID"]);
                                            ob5.PARENT_ID = (dr5["PARENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr5["PARENT_ID"]);
                                            ob5.MNG_TYPE_NAME_EN = (dr5["MNG_TYPE_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr5["MNG_TYPE_NAME_EN"]);
                                            ob5.MNG_TYPE_NAME_BN = (dr5["MNG_TYPE_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr5["MNG_TYPE_NAME_BN"]);
                                            ob5.MNG_TYPE_SNAME = (dr5["MNG_TYPE_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr5["MNG_TYPE_SNAME"]);
                                            ob5.MNG_TYPE_LEVEL = (dr5["MNG_TYPE_LEVEL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr5["MNG_TYPE_LEVEL"]);
                                            ob5.IS_LEAF = (dr5["IS_LEAF"] == DBNull.Value) ? string.Empty : Convert.ToString(dr5["IS_LEAF"]);
                                            ob5.IS_ACTIVE = (dr5["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr5["IS_ACTIVE"]);
                                            ob5.PARENT_NAME = (dr5["PARENT_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr5["PARENT_NAME"]);
                                            ob5.text = (dr5["MNG_TYPE_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr5["MNG_TYPE_NAME_EN"]);
                                            ob5.expanded = false;


                                            var ds6 = db.ExecuteStoredProcedure(new List<CommandParameter>()
                                            {   new CommandParameter() {ParameterName = "pOption", Value = 3002},
                                                new CommandParameter() {ParameterName = "pPARENT_ID", Value = ob5.HR_MANAGEMENT_TYPE_ID}
                                            }, sp);
                                            foreach (DataRow dr6 in ds6.Tables[0].Rows)
                                            {
                                                HrManagementTypeModel6 ob6 = new HrManagementTypeModel6();

                                                ob6.HR_MANAGEMENT_TYPE_ID = (dr6["HR_MANAGEMENT_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr6["HR_MANAGEMENT_TYPE_ID"]);
                                                ob6.PARENT_ID = (dr6["PARENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr6["PARENT_ID"]);
                                                ob6.MNG_TYPE_NAME_EN = (dr6["MNG_TYPE_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr6["MNG_TYPE_NAME_EN"]);
                                                ob6.MNG_TYPE_NAME_BN = (dr6["MNG_TYPE_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr6["MNG_TYPE_NAME_BN"]);
                                                ob6.MNG_TYPE_SNAME = (dr6["MNG_TYPE_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr6["MNG_TYPE_SNAME"]);
                                                ob6.MNG_TYPE_LEVEL = (dr6["MNG_TYPE_LEVEL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr6["MNG_TYPE_LEVEL"]);
                                                ob6.IS_LEAF = (dr6["IS_LEAF"] == DBNull.Value) ? string.Empty : Convert.ToString(dr6["IS_LEAF"]);
                                                ob6.IS_ACTIVE = (dr6["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr6["IS_ACTIVE"]);
                                                ob6.PARENT_NAME = (dr6["PARENT_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr6["PARENT_NAME"]);
                                                ob6.text = (dr6["MNG_TYPE_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr6["MNG_TYPE_NAME_EN"]);
                                                ob6.expanded = false;
                                                ob5.items.Add(ob6);
                                            }
                                            ob4.items.Add(ob5);
                                        }
                                        ob3.items.Add(ob4);
                                    }
                                    ob2.items.Add(ob3);
                                }
                            ob1.items.Add(ob2);
                        }
                        ob.items.Add(ob1);
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
            const string sp = "pkg_hr.hr_management_type_insert";
            string vMsg = "";
            var ob = this;

            OraDatabase db = new OraDatabase();
            try
            {
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                    new CommandParameter() {ParameterName = "pHR_MANAGEMENT_TYPE_ID", Value = ob.HR_MANAGEMENT_TYPE_ID},
                    new CommandParameter() {ParameterName = "pPARENT_ID", Value = ob.PARENT_ID},
                    new CommandParameter() {ParameterName = "pMNG_TYPE_NAME_EN", Value = ob.MNG_TYPE_NAME_EN},
                    new CommandParameter() {ParameterName = "pMNG_TYPE_NAME_BN", Value = ob.MNG_TYPE_NAME_BN},
                    new CommandParameter() {ParameterName = "pMNG_TYPE_SNAME", Value = ob.MNG_TYPE_SNAME},
                    new CommandParameter() {ParameterName = "pMNG_TYPE_LEVEL", Value = ob.MNG_TYPE_LEVEL},
                    new CommandParameter() {ParameterName = "pIS_LEAF", Value = ob.IS_LEAF == null ? "N" : "Y"},
                    new CommandParameter() {ParameterName = "pIS_ACTIVE", Value = ob.IS_ACTIVE == null ? "N" : "Y"},
                    new CommandParameter() {ParameterName = "pCREATED_BY", Value = Convert.ToInt64(HttpContext.Current.Session["multiScUserId"])},
                    new CommandParameter() {ParameterName = "pOption", Value = 1000},
                    new CommandParameter() {ParameterName = "pMsg", Value = 200, Direction = ParameterDirection.Output},
                    new CommandParameter() {ParameterName = "pMsgType", Value = 5, Direction = ParameterDirection.Output}
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
            const string sp = "pkg_hr.hr_management_type_update";
            var ob = this;
            string vMsg = "";

            OraDatabase db = new OraDatabase();
            try
            {
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                    new CommandParameter() {ParameterName = "pHR_MANAGEMENT_TYPE_ID", Value = ob.HR_MANAGEMENT_TYPE_ID},
                    new CommandParameter() {ParameterName = "pPARENT_ID", Value = ob.PARENT_ID},
                    new CommandParameter() {ParameterName = "pMNG_TYPE_NAME_EN", Value = ob.MNG_TYPE_NAME_EN},
                    new CommandParameter() {ParameterName = "pMNG_TYPE_NAME_BN", Value = ob.MNG_TYPE_NAME_BN},
                    new CommandParameter() {ParameterName = "pMNG_TYPE_SNAME", Value = ob.MNG_TYPE_SNAME},
                    new CommandParameter() {ParameterName = "pMNG_TYPE_LEVEL", Value = ob.MNG_TYPE_LEVEL},
                    new CommandParameter() {ParameterName = "pIS_LEAF", Value = ob.IS_LEAF == null ? "N" : "Y"},
                    new CommandParameter() {ParameterName = "pIS_ACTIVE", Value = ob.IS_ACTIVE == null ? "N" : "Y"},
                    new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = Convert.ToInt64(HttpContext.Current.Session["multiScUserId"])},
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

    }

    public class HrManagementTypeModel1
    {
        public Int64 HR_MANAGEMENT_TYPE_ID { get; set; }
        public Int64 PARENT_ID { get; set; }
        public string MNG_TYPE_NAME_EN { get; set; }
        public string MNG_TYPE_NAME_BN { get; set; }
        public string MNG_TYPE_SNAME { get; set; }
        public Int64 MNG_TYPE_LEVEL { get; set; }
        public string IS_LEAF { get; set; }
        public string IS_ACTIVE { get; set; }


        public DateTime? CREATION_DATE { get; set; }
        public int CREATED_BY { get; set; }
        public DateTime? LAST_UPDATE_DATE { get; set; }
        public int LAST_UPDATED_BY { get; set; }


        public string PARENT_NAME { get; set; }
        public string text { get; set; }
        public string spriteCssClass { get; set; }
        public bool expanded { get; set; }

        private List<HrManagementTypeModel2> _subHrManagementTypeModel = null;
        public List<HrManagementTypeModel2> items
        {
            get
            {
                if (_subHrManagementTypeModel == null)
                {
                    _subHrManagementTypeModel = new List<HrManagementTypeModel2>();
                }
                return _subHrManagementTypeModel;
            }
            set
            {
                _subHrManagementTypeModel = value;
            }
        }
    }

    public class HrManagementTypeModel2
    {
        public Int64 HR_MANAGEMENT_TYPE_ID { get; set; }
        public Int64 PARENT_ID { get; set; }
        public string MNG_TYPE_NAME_EN { get; set; }
        public string MNG_TYPE_NAME_BN { get; set; }
        public string MNG_TYPE_SNAME { get; set; }
        public Int64 MNG_TYPE_LEVEL { get; set; }
        public string IS_LEAF { get; set; }
        public string IS_ACTIVE { get; set; }


        public DateTime? CREATION_DATE { get; set; }
        public int CREATED_BY { get; set; }
        public DateTime? LAST_UPDATE_DATE { get; set; }
        public int LAST_UPDATED_BY { get; set; }


        public string PARENT_NAME { get; set; }
        public string text { get; set; }
        public string spriteCssClass { get; set; }
        public bool expanded { get; set; }

        private List<HrManagementTypeModel3> _subHrManagementTypeModel = null;
        public List<HrManagementTypeModel3> items
        {
            get
            {
                if (_subHrManagementTypeModel == null)
                {
                    _subHrManagementTypeModel = new List<HrManagementTypeModel3>();
                }
                return _subHrManagementTypeModel;
            }
            set
            {
                _subHrManagementTypeModel = value;
            }
        }
    }

    public class HrManagementTypeModel3
    {
        public Int64 HR_MANAGEMENT_TYPE_ID { get; set; }
        public Int64 PARENT_ID { get; set; }
        public string MNG_TYPE_NAME_EN { get; set; }
        public string MNG_TYPE_NAME_BN { get; set; }
        public string MNG_TYPE_SNAME { get; set; }
        public Int64 MNG_TYPE_LEVEL { get; set; }
        public string IS_LEAF { get; set; }
        public string IS_ACTIVE { get; set; }


        public DateTime? CREATION_DATE { get; set; }
        public int CREATED_BY { get; set; }
        public DateTime? LAST_UPDATE_DATE { get; set; }
        public int LAST_UPDATED_BY { get; set; }


        public string PARENT_NAME { get; set; }
        public string text { get; set; }
        public string spriteCssClass { get; set; }
        public bool expanded { get; set; }

        private List<HrManagementTypeModel4> _subHrManagementTypeModel = null;
        public List<HrManagementTypeModel4> items
        {
            get
            {
                if (_subHrManagementTypeModel == null)
                {
                    _subHrManagementTypeModel = new List<HrManagementTypeModel4>();
                }
                return _subHrManagementTypeModel;
            }
            set
            {
                _subHrManagementTypeModel = value;
            }
        }
    }

    public class HrManagementTypeModel4
    {
        public Int64 HR_MANAGEMENT_TYPE_ID { get; set; }
        public Int64 PARENT_ID { get; set; }
        public string MNG_TYPE_NAME_EN { get; set; }
        public string MNG_TYPE_NAME_BN { get; set; }
        public string MNG_TYPE_SNAME { get; set; }
        public Int64 MNG_TYPE_LEVEL { get; set; }
        public string IS_LEAF { get; set; }
        public string IS_ACTIVE { get; set; }


        public DateTime? CREATION_DATE { get; set; }
        public int CREATED_BY { get; set; }
        public DateTime? LAST_UPDATE_DATE { get; set; }
        public int LAST_UPDATED_BY { get; set; }


        public string PARENT_NAME { get; set; }
        public string text { get; set; }
        public string spriteCssClass { get; set; }
        public bool expanded { get; set; }

        private List<HrManagementTypeModel5> _subHrManagementTypeModel = null;
        public List<HrManagementTypeModel5> items
        {
            get
            {
                if (_subHrManagementTypeModel == null)
                {
                    _subHrManagementTypeModel = new List<HrManagementTypeModel5>();
                }
                return _subHrManagementTypeModel;
            }
            set
            {
                _subHrManagementTypeModel = value;
            }
        }
    }

    public class HrManagementTypeModel5
    {
        public Int64 HR_MANAGEMENT_TYPE_ID { get; set; }
        public Int64 PARENT_ID { get; set; }
        public string MNG_TYPE_NAME_EN { get; set; }
        public string MNG_TYPE_NAME_BN { get; set; }
        public string MNG_TYPE_SNAME { get; set; }
        public Int64 MNG_TYPE_LEVEL { get; set; }
        public string IS_LEAF { get; set; }
        public string IS_ACTIVE { get; set; }


        public DateTime? CREATION_DATE { get; set; }
        public int CREATED_BY { get; set; }
        public DateTime? LAST_UPDATE_DATE { get; set; }
        public int LAST_UPDATED_BY { get; set; }


        public string PARENT_NAME { get; set; }
        public string text { get; set; }
        public string spriteCssClass { get; set; }
        public bool expanded { get; set; }

        private List<HrManagementTypeModel6> _subHrManagementTypeModel = null;
        public List<HrManagementTypeModel6> items
        {
            get
            {
                if (_subHrManagementTypeModel == null)
                {
                    _subHrManagementTypeModel = new List<HrManagementTypeModel6>();
                }
                return _subHrManagementTypeModel;
            }
            set
            {
                _subHrManagementTypeModel = value;
            }
        }
    }

    public class HrManagementTypeModel6
    {
        public Int64 HR_MANAGEMENT_TYPE_ID { get; set; }
        public Int64 PARENT_ID { get; set; }
        public string MNG_TYPE_NAME_EN { get; set; }
        public string MNG_TYPE_NAME_BN { get; set; }
        public string MNG_TYPE_SNAME { get; set; }
        public Int64 MNG_TYPE_LEVEL { get; set; }
        public string IS_LEAF { get; set; }
        public string IS_ACTIVE { get; set; }


        public DateTime? CREATION_DATE { get; set; }
        public int CREATED_BY { get; set; }
        public DateTime? LAST_UPDATE_DATE { get; set; }
        public int LAST_UPDATED_BY { get; set; }


        public string PARENT_NAME { get; set; }
        public string text { get; set; }
        public string spriteCssClass { get; set; }
        public bool expanded { get; set; }

        //private List<HrManagementTypeModel7> _subHrManagementTypeModel = null;
        //public List<HrManagementTypeModel7> items
        //{
        //    get
        //    {
        //        if (_subHrManagementTypeModel == null)
        //        {
        //            _subHrManagementTypeModel = new List<HrManagementTypeModel7>();
        //        }
        //        return _subHrManagementTypeModel;
        //    }
        //    set
        //    {
        //        _subHrManagementTypeModel = value;
        //    }
        //}
    }


}
