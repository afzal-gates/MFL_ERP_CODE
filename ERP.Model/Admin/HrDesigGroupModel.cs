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
    public class HrDesigGroupModel 
    {
        public Int64 HR_DESIGNATION_GRP_ID { get; set; }
        public Int64 PARENT_ID { get; set; }
        public string DESIG_GRP_NAME_EN { get; set; }        
        public string DESIG_GRP_NAME_BN { get; set; }
        public string DESIG_GRP_SNAME { get; set; }
        public Int64 DESIG_GRP_LEVEL { get; set; }
        public Int64 DSG_GRP_ORDER { get; set; }
        public string IS_LEAF { get; set; }        
        public string IS_ACTIVE { get; set; }        

        public DateTime? CREATION_DATE { get; set; }
        public int CREATED_BY { get; set; }
        public DateTime? LAST_UPDATE_DATE { get; set; }
        public int LAST_UPDATED_BY { get; set; }


        public int TranMode { get; set; }
        public string PARENT_NAME { get; set; }
        public string text { get; set; }
        public string spriteCssClass { get; set; }
        public bool expanded { get; set; }
        public bool IS_PARAM_ACTIVE { get; set; }

        private List<HrDesigGroupModel1> _subHrDesigGroupModel = null;
        public List<HrDesigGroupModel1> items
        {
            get
            {
                if (_subHrDesigGroupModel == null)
                {
                    _subHrDesigGroupModel = new List<HrDesigGroupModel1>();
                }
                return _subHrDesigGroupModel;
            }
            set
            {
                _subHrDesigGroupModel = value;
            }
        }

        public List<HrDesigGroupModel> HrDesigGroupData()
        {
            string sp = "pkg_admin.hr_desig_grp_select";
            try
            {
                var obList = new List<HrDesigGroupModel>();

                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   new CommandParameter() {ParameterName = "pOption", Value = 3002},
                    new CommandParameter() {ParameterName = "pPARENT_ID", Value = null}
                }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    HrDesigGroupModel ob = new HrDesigGroupModel();

                    ob.HR_DESIGNATION_GRP_ID = (dr["HR_DESIGNATION_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_DESIGNATION_GRP_ID"]);
                    ob.PARENT_ID = (dr["PARENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PARENT_ID"]);
                    ob.DESIG_GRP_NAME_EN = (dr["DESIG_GRP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DESIG_GRP_NAME_EN"]);
                    ob.DESIG_GRP_NAME_BN = (dr["DESIG_GRP_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DESIG_GRP_NAME_BN"]);
                    ob.DESIG_GRP_SNAME = (dr["DESIG_GRP_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DESIG_GRP_SNAME"]);
                    ob.DESIG_GRP_LEVEL = (dr["DESIG_GRP_LEVEL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DESIG_GRP_LEVEL"]);
                    ob.DSG_GRP_ORDER = (dr["DSG_GRP_ORDER"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DSG_GRP_ORDER"]);
                    ob.IS_LEAF = (dr["IS_LEAF"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_LEAF"]);
                    ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);

                    ob.PARENT_NAME = (dr["PARENT_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PARENT_NAME"]);
                    ob.text = (dr["DESIG_GRP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DESIG_GRP_NAME_EN"]);
                    ob.expanded = true;


                    var ds1 = db.ExecuteStoredProcedure(new List<CommandParameter>()
                        {   new CommandParameter() {ParameterName = "pOption", Value = 3002},
                            new CommandParameter() {ParameterName = "pPARENT_ID", Value = ob.HR_DESIGNATION_GRP_ID}
                        }, sp);
                    foreach (DataRow dr1 in ds1.Tables[0].Rows)
                    {
                        HrDesigGroupModel1 ob1 = new HrDesigGroupModel1();

                        ob1.HR_DESIGNATION_GRP_ID = (dr1["HR_DESIGNATION_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr1["HR_DESIGNATION_GRP_ID"]);
                        ob1.PARENT_ID = (dr1["PARENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr1["PARENT_ID"]);
                        ob1.DESIG_GRP_NAME_EN = (dr1["DESIG_GRP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["DESIG_GRP_NAME_EN"]);
                        ob1.DESIG_GRP_NAME_BN = (dr1["DESIG_GRP_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["DESIG_GRP_NAME_BN"]);
                        ob1.DESIG_GRP_SNAME = (dr1["DESIG_GRP_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["DESIG_GRP_SNAME"]);
                        ob1.DESIG_GRP_LEVEL = (dr1["DESIG_GRP_LEVEL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr1["DESIG_GRP_LEVEL"]);
                        ob1.DSG_GRP_ORDER = (dr1["DSG_GRP_ORDER"] == DBNull.Value) ? 0 : Convert.ToInt64(dr1["DSG_GRP_ORDER"]);
                        ob1.IS_LEAF = (dr1["IS_LEAF"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["IS_LEAF"]);
                        ob1.IS_ACTIVE = (dr1["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["IS_ACTIVE"]);

                        ob1.PARENT_NAME = (dr1["PARENT_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["PARENT_NAME"]);
                        ob1.text = (dr1["DESIG_GRP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["DESIG_GRP_NAME_EN"]);
                        ob1.expanded = false;

                        var ds2 = db.ExecuteStoredProcedure(new List<CommandParameter>()
                        {   new CommandParameter() {ParameterName = "pOption", Value = 3002},
                            new CommandParameter() {ParameterName = "pPARENT_ID", Value = ob1.HR_DESIGNATION_GRP_ID}
                        }, sp);
                        foreach (DataRow dr2 in ds2.Tables[0].Rows)
                        {
                            HrDesigGroupModel2 ob2 = new HrDesigGroupModel2();

                            ob2.HR_DESIGNATION_GRP_ID = (dr2["HR_DESIGNATION_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr2["HR_DESIGNATION_GRP_ID"]);
                            ob2.PARENT_ID = (dr2["PARENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr2["PARENT_ID"]);
                            ob2.DESIG_GRP_NAME_EN = (dr2["DESIG_GRP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr2["DESIG_GRP_NAME_EN"]);
                            ob2.DESIG_GRP_NAME_BN = (dr2["DESIG_GRP_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr2["DESIG_GRP_NAME_BN"]);
                            ob2.DESIG_GRP_SNAME = (dr2["DESIG_GRP_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr2["DESIG_GRP_SNAME"]);
                            ob2.DESIG_GRP_LEVEL = (dr2["DESIG_GRP_LEVEL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr2["DESIG_GRP_LEVEL"]);
                            ob2.DSG_GRP_ORDER = (dr2["DSG_GRP_ORDER"] == DBNull.Value) ? 0 : Convert.ToInt64(dr2["DSG_GRP_ORDER"]);
                            ob2.IS_LEAF = (dr2["IS_LEAF"] == DBNull.Value) ? string.Empty : Convert.ToString(dr2["IS_LEAF"]);
                            ob2.IS_ACTIVE = (dr2["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr2["IS_ACTIVE"]);

                            ob2.PARENT_NAME = (dr2["PARENT_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr2["PARENT_NAME"]);
                            ob2.text = (dr2["DESIG_GRP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr2["DESIG_GRP_NAME_EN"]);
                            ob2.expanded = false;

                                var ds3 = db.ExecuteStoredProcedure(new List<CommandParameter>()
                                {   new CommandParameter() {ParameterName = "pOption", Value = 3002},
                                    new CommandParameter() {ParameterName = "pPARENT_ID", Value = ob2.HR_DESIGNATION_GRP_ID}
                                }, sp);
                                foreach (DataRow dr3 in ds3.Tables[0].Rows)
                                {
                                    HrDesigGroupModel3 ob3 = new HrDesigGroupModel3();

                                    ob3.HR_DESIGNATION_GRP_ID = (dr3["HR_DESIGNATION_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr3["HR_DESIGNATION_GRP_ID"]);
                                    ob3.PARENT_ID = (dr3["PARENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr3["PARENT_ID"]);
                                    ob3.DESIG_GRP_NAME_EN = (dr3["DESIG_GRP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr3["DESIG_GRP_NAME_EN"]);
                                    ob3.DESIG_GRP_NAME_BN = (dr3["DESIG_GRP_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr3["DESIG_GRP_NAME_BN"]);
                                    ob3.DESIG_GRP_SNAME = (dr3["DESIG_GRP_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr3["DESIG_GRP_SNAME"]);
                                    ob3.DESIG_GRP_LEVEL = (dr3["DESIG_GRP_LEVEL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr3["DESIG_GRP_LEVEL"]);
                                    ob3.DSG_GRP_ORDER = (dr3["DSG_GRP_ORDER"] == DBNull.Value) ? 0 : Convert.ToInt64(dr3["DSG_GRP_ORDER"]);
                                    ob3.IS_LEAF = (dr3["IS_LEAF"] == DBNull.Value) ? string.Empty : Convert.ToString(dr3["IS_LEAF"]);
                                    ob3.IS_ACTIVE = (dr3["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr3["IS_ACTIVE"]);

                                    ob3.PARENT_NAME = (dr3["PARENT_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr3["PARENT_NAME"]);
                                    ob3.text = (dr3["DESIG_GRP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr3["DESIG_GRP_NAME_EN"]);
                                    ob3.expanded = false;

                                    var ds4 = db.ExecuteStoredProcedure(new List<CommandParameter>()
                                        {   new CommandParameter() {ParameterName = "pOption", Value = 3002},
                                            new CommandParameter() {ParameterName = "pPARENT_ID", Value = ob3.HR_DESIGNATION_GRP_ID}
                                        }, sp);
                                    foreach (DataRow dr4 in ds4.Tables[0].Rows)
                                    {
                                        HrDesigGroupModel4 ob4 = new HrDesigGroupModel4();

                                        ob4.HR_DESIGNATION_GRP_ID = (dr4["HR_DESIGNATION_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr4["HR_DESIGNATION_GRP_ID"]);
                                        ob4.PARENT_ID = (dr4["PARENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr4["PARENT_ID"]);
                                        ob4.DESIG_GRP_NAME_EN = (dr4["DESIG_GRP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr4["DESIG_GRP_NAME_EN"]);
                                        ob4.DESIG_GRP_NAME_BN = (dr4["DESIG_GRP_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr4["DESIG_GRP_NAME_BN"]);
                                        ob4.DESIG_GRP_SNAME = (dr4["DESIG_GRP_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr4["DESIG_GRP_SNAME"]);
                                        ob4.DESIG_GRP_LEVEL = (dr4["DESIG_GRP_LEVEL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr4["DESIG_GRP_LEVEL"]);
                                        ob4.DSG_GRP_ORDER = (dr4["DSG_GRP_ORDER"] == DBNull.Value) ? 0 : Convert.ToInt64(dr4["DSG_GRP_ORDER"]);
                                        ob4.IS_LEAF = (dr4["IS_LEAF"] == DBNull.Value) ? string.Empty : Convert.ToString(dr4["IS_LEAF"]);
                                        ob4.IS_ACTIVE = (dr4["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr4["IS_ACTIVE"]);

                                        ob4.PARENT_NAME = (dr4["PARENT_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr4["PARENT_NAME"]);
                                        ob4.text = (dr4["DESIG_GRP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr4["DESIG_GRP_NAME_EN"]);
                                        ob4.expanded = false;

                                        var ds5 = db.ExecuteStoredProcedure(new List<CommandParameter>()
                                        {   new CommandParameter() {ParameterName = "pOption", Value = 3002},
                                            new CommandParameter() {ParameterName = "pPARENT_ID", Value = ob4.HR_DESIGNATION_GRP_ID}
                                        }, sp);
                                        foreach (DataRow dr5 in ds5.Tables[0].Rows)
                                        {
                                            HrDesigGroupModel5 ob5 = new HrDesigGroupModel5();

                                            ob5.HR_DESIGNATION_GRP_ID = (dr5["HR_DESIGNATION_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr5["HR_DESIGNATION_GRP_ID"]);
                                            ob5.PARENT_ID = (dr5["PARENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr5["PARENT_ID"]);
                                            ob5.DESIG_GRP_NAME_EN = (dr5["DESIG_GRP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr5["DESIG_GRP_NAME_EN"]);
                                            ob5.DESIG_GRP_NAME_BN = (dr5["DESIG_GRP_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr5["DESIG_GRP_NAME_BN"]);
                                            ob5.DESIG_GRP_SNAME = (dr5["DESIG_GRP_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr5["DESIG_GRP_SNAME"]);
                                            ob5.DESIG_GRP_LEVEL = (dr5["DESIG_GRP_LEVEL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr5["DESIG_GRP_LEVEL"]);
                                            ob5.DSG_GRP_ORDER = (dr5["DSG_GRP_ORDER"] == DBNull.Value) ? 0 : Convert.ToInt64(dr5["DSG_GRP_ORDER"]);
                                            ob5.IS_LEAF = (dr5["IS_LEAF"] == DBNull.Value) ? string.Empty : Convert.ToString(dr5["IS_LEAF"]);
                                            ob5.IS_ACTIVE = (dr5["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr5["IS_ACTIVE"]);
                                            ob5.PARENT_NAME = (dr5["PARENT_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr5["PARENT_NAME"]);
                                            ob5.text = (dr5["DESIG_GRP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr5["DESIG_GRP_NAME_EN"]);
                                            ob5.expanded = false;


                                            var ds6 = db.ExecuteStoredProcedure(new List<CommandParameter>()
                                            {   new CommandParameter() {ParameterName = "pOption", Value = 3002},
                                                new CommandParameter() {ParameterName = "pPARENT_ID", Value = ob5.HR_DESIGNATION_GRP_ID}
                                            }, sp);
                                            foreach (DataRow dr6 in ds6.Tables[0].Rows)
                                            {
                                                HrDesigGroupModel6 ob6 = new HrDesigGroupModel6();
                                                ob6.HR_DESIGNATION_GRP_ID = (dr6["HR_DESIGNATION_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr6["HR_DESIGNATION_GRP_ID"]);
                                                ob6.PARENT_ID = (dr6["PARENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr6["PARENT_ID"]);
                                                ob6.DESIG_GRP_NAME_EN = (dr6["DESIG_GRP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr6["DESIG_GRP_NAME_EN"]);
                                                ob6.DESIG_GRP_NAME_BN = (dr6["DESIG_GRP_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr6["DESIG_GRP_NAME_BN"]);
                                                ob6.DESIG_GRP_SNAME = (dr6["DESIG_GRP_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr6["DESIG_GRP_SNAME"]);
                                                ob6.DESIG_GRP_LEVEL = (dr6["DESIG_GRP_LEVEL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr6["DESIG_GRP_LEVEL"]);
                                                ob6.DSG_GRP_ORDER = (dr6["DSG_GRP_ORDER"] == DBNull.Value) ? 0 : Convert.ToInt64(dr6["DSG_GRP_ORDER"]);
                                                ob6.IS_LEAF = (dr6["IS_LEAF"] == DBNull.Value) ? string.Empty : Convert.ToString(dr6["IS_LEAF"]);
                                                ob6.IS_ACTIVE = (dr6["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr6["IS_ACTIVE"]);
                                                ob6.PARENT_NAME = (dr6["PARENT_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr6["PARENT_NAME"]);
                                                ob6.text = (dr6["DESIG_GRP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr6["DESIG_GRP_NAME_EN"]);
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
            const string sp = "pkg_admin.hr_desig_grp_insert";
            string vMsg = "";
            var ob = this;

            OraDatabase db = new OraDatabase();
            try
            {
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                    new CommandParameter() {ParameterName = "pHR_DESIGNATION_GRP_ID", Value = ob.HR_DESIGNATION_GRP_ID},
                    new CommandParameter() {ParameterName = "pPARENT_ID", Value = ob.PARENT_ID},
                    new CommandParameter() {ParameterName = "pDESIG_GRP_NAME_EN", Value = ob.DESIG_GRP_NAME_EN},
                    new CommandParameter() {ParameterName = "pDESIG_GRP_NAME_BN", Value = ob.DESIG_GRP_NAME_BN},
                    new CommandParameter() {ParameterName = "pDESIG_GRP_SNAME", Value = ob.DESIG_GRP_SNAME},
                    new CommandParameter() {ParameterName = "pDESIG_GRP_LEVEL", Value = ob.DESIG_GRP_LEVEL},
                    new CommandParameter() {ParameterName = "pDSG_GRP_ORDER", Value = ob.DSG_GRP_ORDER},
                    new CommandParameter() {ParameterName = "pIS_LEAF", Value = ob.IS_LEAF == null ? "N" : "Y"},
                    new CommandParameter() {ParameterName = "pIS_ACTIVE", Value = ob.IS_ACTIVE == null ? "N" : "Y"},
                    new CommandParameter() {ParameterName = "pCREATED_BY", Value = Convert.ToInt64(HttpContext.Current.Session["multiScUserId"])},
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
            const string sp = "pkg_admin.hr_desig_grp_update";
            string vMsg = "";
            var ob = this;

            OraDatabase db = new OraDatabase();
            try
            {
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                    new CommandParameter() {ParameterName = "pHR_DESIGNATION_GRP_ID", Value = ob.HR_DESIGNATION_GRP_ID},
                    new CommandParameter() {ParameterName = "pPARENT_ID", Value = ob.PARENT_ID},
                    new CommandParameter() {ParameterName = "pDESIG_GRP_NAME_EN", Value = ob.DESIG_GRP_NAME_EN},
                    new CommandParameter() {ParameterName = "pDESIG_GRP_NAME_BN", Value = ob.DESIG_GRP_NAME_BN},
                    new CommandParameter() {ParameterName = "pDESIG_GRP_SNAME", Value = ob.DESIG_GRP_SNAME},
                    new CommandParameter() {ParameterName = "pDESIG_GRP_LEVEL", Value = ob.DESIG_GRP_LEVEL},
                    new CommandParameter() {ParameterName = "pDSG_GRP_ORDER", Value = ob.DSG_GRP_ORDER},
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

        public List<HrDesigGroupModel> DesignationGrpListData(string pPARAM_TYPE)
        {
            try
            {
                string vSQL = "";
                if (pPARAM_TYPE == "PARTIAL SALARY PROCESS")
                    vSQL = "select d.*, (case when p.HR_DESIGNATION_GRP_ID is not null then 'Y' else 'N' end) as IS_PARAM_ACTIVE " +
                        " from HR_DESIGNATION_GRP d " +
                        " left join HR_DESIG_GRP_PARAM p on d.HR_DESIGNATION_GRP_ID=p.HR_DESIGNATION_GRP_ID and p.PARAM_TYPE='PARTIAL SALARY PROCESS' " +
                        " where PARENT_ID is not null " +
                        " order by d.DSG_GRP_ORDER";

                else if (pPARAM_TYPE == "EL ENCASHMENT PROCESS")
                    vSQL = "select d.*, (case when p.HR_DESIGNATION_GRP_ID is not null then 'Y' else 'N' end) as IS_PARAM_ACTIVE " +
                        " from HR_DESIGNATION_GRP d " +
                        " left join HR_DESIG_GRP_PARAM p on d.HR_DESIGNATION_GRP_ID=p.HR_DESIGNATION_GRP_ID and p.PARAM_TYPE='EL ENCASHMENT PROCESS' " +
                        " where PARENT_ID is not null " +
                        " order by d.DSG_GRP_ORDER";
                else
                    vSQL = "select d.*, 'N' as IS_PARAM_ACTIVE from HR_DESIGNATION_GRP d where PARENT_ID is not null order by d.DSG_GRP_ORDER";

                var obList = new List<HrDesigGroupModel>();

                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteSQLStatement(vSQL);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    HrDesigGroupModel ob = new HrDesigGroupModel();

                    ob.HR_DESIGNATION_GRP_ID = (dr["HR_DESIGNATION_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_DESIGNATION_GRP_ID"]);
                    ob.DESIG_GRP_NAME_EN = (dr["DESIG_GRP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DESIG_GRP_NAME_EN"]);
                    ob.PARENT_ID = (dr["PARENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PARENT_ID"]);

                    ob.DSG_GRP_ORDER = (dr["DSG_GRP_ORDER"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DSG_GRP_ORDER"]);

                    ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);
                    if (Convert.ToString(dr["IS_PARAM_ACTIVE"]) == "Y") 
                        ob.IS_PARAM_ACTIVE = true;

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

    public class HrDesigGroupModel1
    {
        public Int64 HR_DESIGNATION_GRP_ID { get; set; }
        public Int64 PARENT_ID { get; set; }
        public string DESIG_GRP_NAME_EN { get; set; }
        public string DESIG_GRP_NAME_BN { get; set; }
        public string DESIG_GRP_SNAME { get; set; }
        public Int64 DESIG_GRP_LEVEL { get; set; }
        public Int64 DSG_GRP_ORDER { get; set; }
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

        private List<HrDesigGroupModel2> _subHrDesigGroupModel = null;
        public List<HrDesigGroupModel2> items
        {
            get
            {
                if (_subHrDesigGroupModel == null)
                {
                    _subHrDesigGroupModel = new List<HrDesigGroupModel2>();
                }
                return _subHrDesigGroupModel;
            }
            set
            {
                _subHrDesigGroupModel = value;
            }
        }

    }

    public class HrDesigGroupModel2
    {
        public Int64 HR_DESIGNATION_GRP_ID { get; set; }
        public Int64 PARENT_ID { get; set; }
        public string DESIG_GRP_NAME_EN { get; set; }
        public string DESIG_GRP_NAME_BN { get; set; }
        public string DESIG_GRP_SNAME { get; set; }
        public Int64 DESIG_GRP_LEVEL { get; set; }
        public Int64 DSG_GRP_ORDER { get; set; }
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

        private List<HrDesigGroupModel3> _subHrDesigGroupModel = null;
        public List<HrDesigGroupModel3> items
        {
            get
            {
                if (_subHrDesigGroupModel == null)
                {
                    _subHrDesigGroupModel = new List<HrDesigGroupModel3>();
                }
                return _subHrDesigGroupModel;
            }
            set
            {
                _subHrDesigGroupModel = value;
            }
        }

    }

    public class HrDesigGroupModel3
    {
        public Int64 HR_DESIGNATION_GRP_ID { get; set; }
        public Int64 PARENT_ID { get; set; }
        public string DESIG_GRP_NAME_EN { get; set; }
        public string DESIG_GRP_NAME_BN { get; set; }
        public string DESIG_GRP_SNAME { get; set; }
        public Int64 DESIG_GRP_LEVEL { get; set; }
        public Int64 DSG_GRP_ORDER { get; set; }
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

        private List<HrDesigGroupModel4> _subHrDesigGroupModel = null;
        public List<HrDesigGroupModel4> items
        {
            get
            {
                if (_subHrDesigGroupModel == null)
                {
                    _subHrDesigGroupModel = new List<HrDesigGroupModel4>();
                }
                return _subHrDesigGroupModel;
            }
            set
            {
                _subHrDesigGroupModel = value;
            }
        }

    }

    public class HrDesigGroupModel4
    {
        public Int64 HR_DESIGNATION_GRP_ID { get; set; }
        public Int64 PARENT_ID { get; set; }
        public string DESIG_GRP_NAME_EN { get; set; }
        public string DESIG_GRP_NAME_BN { get; set; }
        public string DESIG_GRP_SNAME { get; set; }
        public Int64 DESIG_GRP_LEVEL { get; set; }
        public Int64 DSG_GRP_ORDER { get; set; }
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

        private List<HrDesigGroupModel5> _subHrDesigGroupModel = null;
        public List<HrDesigGroupModel5> items
        {
            get
            {
                if (_subHrDesigGroupModel == null)
                {
                    _subHrDesigGroupModel = new List<HrDesigGroupModel5>();
                }
                return _subHrDesigGroupModel;
            }
            set
            {
                _subHrDesigGroupModel = value;
            }
        }

    }

    public class HrDesigGroupModel5
    {
        public Int64 HR_DESIGNATION_GRP_ID { get; set; }
        public Int64 PARENT_ID { get; set; }
        public string DESIG_GRP_NAME_EN { get; set; }
        public string DESIG_GRP_NAME_BN { get; set; }
        public string DESIG_GRP_SNAME { get; set; }
        public Int64 DESIG_GRP_LEVEL { get; set; }
        public Int64 DSG_GRP_ORDER { get; set; }
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

        private List<HrDesigGroupModel6> _subHrDesigGroupModel = null;
        public List<HrDesigGroupModel6> items
        {
            get
            {
                if (_subHrDesigGroupModel == null)
                {
                    _subHrDesigGroupModel = new List<HrDesigGroupModel6>();
                }
                return _subHrDesigGroupModel;
            }
            set
            {
                _subHrDesigGroupModel = value;
            }
        }

    }

    public class HrDesigGroupModel6
    {
        public Int64 HR_DESIGNATION_GRP_ID { get; set; }
        public Int64 PARENT_ID { get; set; }
        public string DESIG_GRP_NAME_EN { get; set; }
        public string DESIG_GRP_NAME_BN { get; set; }
        public string DESIG_GRP_SNAME { get; set; }
        public Int64 DESIG_GRP_LEVEL { get; set; }
        public Int64 DSG_GRP_ORDER { get; set; }
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

        //private List<HrDesigGroupModel7> _subHrDesigGroupModel = null;
        //public List<HrDesigGroupModel7> items
        //{
        //    get
        //    {
        //        if (_subHrDesigGroupModel == null)
        //        {
        //            _subHrDesigGroupModel = new List<HrDesigGroupModel7>();
        //        }
        //        return _subHrDesigGroupModel;
        //    }
        //    set
        //    {
        //        _subHrDesigGroupModel = value;
        //    }
        //}

    }



}
