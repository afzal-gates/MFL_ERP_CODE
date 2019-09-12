using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Model
{
    public class ScSystemModel //: IScSystemModel
    {
        public Int64 SC_SYSTEM_MODULE_ID { get; set; }
        public Int64 PARENT_ID { get; set; }
        public string SYS_MODULE_NAME { get; set; }
        public Int64 MODULE_LEVEL { get; set; }
        public string IS_ACTIVE { get; set; }

        public DateTime? CREATION_DATE { get; set; }
        public int CREATED_BY { get; set; }
        public DateTime? LAST_UPDATE_DATE { get; set; }
        public int LAST_UPDATED_BY { get; set; }        
        public int LAST_UPDATE_LOGIN { get; set; }
        public int VERSION_NO { get; set; }


        public string PARENT_NAME { get; set; }
        public string text { get; set; }
        public bool expanded { get; set; }

        private List<ScSystemModel1> __subScSystemModel = null;
        public List<ScSystemModel1> items 
        {
            get
            {
                if (__subScSystemModel == null)
                {
                    __subScSystemModel = new List<ScSystemModel1>();
                }
                return __subScSystemModel;
            }
            set
            {
                __subScSystemModel = value;
            }
        }

        public int TranMode { get; set; }


        public List<ScSystemModel> ScSubModuleData()
        {
            string sp = "pkg_security.sc_system_select";
            try
            {
                var obList = new List<ScSystemModel>();

                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   new CommandParameter() {ParameterName = "pOption", Value = 3000},
                    new CommandParameter() {ParameterName = "pMsg", Value = 500, Direction = ParameterDirection.Output}
                }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ScSystemModel ob = new ScSystemModel();
                    ob.SC_SYSTEM_MODULE_ID = (dr["SC_SYSTEM_MODULE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SC_SYSTEM_MODULE_ID"]);
                    ob.SYS_MODULE_NAME = (dr["SYS_MODULE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SYS_MODULE_NAME"]);
                    ob.text = (dr["SYS_MODULE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SYS_MODULE_NAME"]);
                    ob.expanded = true;
                    ob.PARENT_ID = (dr["PARENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PARENT_ID"]);
                    ob.MODULE_LEVEL = (dr["MODULE_LEVEL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MODULE_LEVEL"]);
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
            const string sp = "pkg_menu.sc_system_module_insert";
            string vMsg = "";
            var ob = this;
            OraDatabase db = new OraDatabase();
            try
            {
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                    new CommandParameter() {ParameterName = "pSC_SYSTEM_MODULE_ID", Value = ob.SC_SYSTEM_MODULE_ID},
                    new CommandParameter() {ParameterName = "pPARENT_ID", Value = ob.PARENT_ID},
                    new CommandParameter() {ParameterName = "pSYS_MODULE_NAME", Value = ob.SYS_MODULE_NAME},
                    new CommandParameter() {ParameterName = "pMODULE_LEVEL", Value = ob.MODULE_LEVEL},
                    new CommandParameter() {ParameterName = "pCREATION_DATE", Value = DateTime.Now},
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
            const string sp = "pkg_menu.sc_system_module_update";
            string vMsg = "";
            var ob = this;

            OraDatabase db = new OraDatabase();
            try
            {
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                    new CommandParameter() {ParameterName = "pSC_SYSTEM_MODULE_ID", Value = ob.SC_SYSTEM_MODULE_ID},
                    new CommandParameter() {ParameterName = "pPARENT_ID", Value = ob.PARENT_ID},
                    new CommandParameter() {ParameterName = "pSYS_MODULE_NAME", Value = ob.SYS_MODULE_NAME},
                    new CommandParameter() {ParameterName = "pMODULE_LEVEL", Value = ob.MODULE_LEVEL},
                    new CommandParameter() {ParameterName = "pCREATION_DATE", Value = DateTime.Now},
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
        public List<ScSystemModel> ScSystemModuleData()
        {
            List<ScSystemModel> obList = new List<ScSystemModel>();
            OraDatabase db = new OraDatabase();
            try
            {
                var ds = db.ExecuteSQLStatement("select a.SYS_MODULE_NAME as PARENT_NAME, a.* from SC_SYSTEM_MODULE a where a.PARENT_ID is null order by a.SC_SYSTEM_MODULE_ID");

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ScSystemModel ob = new ScSystemModel();

                    ob.SC_SYSTEM_MODULE_ID = (dr["SC_SYSTEM_MODULE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SC_SYSTEM_MODULE_ID"]);
                    ob.SYS_MODULE_NAME = (dr["SYS_MODULE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SYS_MODULE_NAME"]);
                    ob.PARENT_NAME = (dr["PARENT_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PARENT_NAME"]);
                    ob.text = (dr["SYS_MODULE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SYS_MODULE_NAME"]);
                    ob.expanded = true;
                    ob.PARENT_ID = (dr["PARENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PARENT_ID"]);
                    ob.MODULE_LEVEL = (dr["MODULE_LEVEL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MODULE_LEVEL"]);
                    ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);

                    var ds1 = db.ExecuteSQLStatement("select b.SYS_MODULE_NAME as PARENT_NAME, a.* from SC_SYSTEM_MODULE a inner join SC_SYSTEM_MODULE b on b.SC_SYSTEM_MODULE_ID=a.PARENT_ID  where a.PARENT_ID='" + ob.SC_SYSTEM_MODULE_ID + "' order by a.SYS_MODULE_NAME");
                    foreach (DataRow dr1 in ds1.Tables[0].Rows)
                    {
                        ScSystemModel1 ob1 = new ScSystemModel1();

                        ob1.SC_SYSTEM_MODULE_ID = (dr1["SC_SYSTEM_MODULE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr1["SC_SYSTEM_MODULE_ID"]);
                        ob1.SYS_MODULE_NAME = (dr1["SYS_MODULE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["SYS_MODULE_NAME"]);
                        ob1.PARENT_NAME = (dr1["PARENT_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["PARENT_NAME"]);
                        ob1.text = (dr1["SYS_MODULE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["SYS_MODULE_NAME"]);
                        ob1.expanded = false;
                        ob1.PARENT_ID = (dr1["PARENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr1["PARENT_ID"]);
                        ob1.MODULE_LEVEL = (dr1["MODULE_LEVEL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr1["MODULE_LEVEL"]);
                        ob1.IS_ACTIVE = (dr1["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["IS_ACTIVE"]);

                        var ds2 = db.ExecuteSQLStatement("select b.SYS_MODULE_NAME as PARENT_NAME, a.* from SC_SYSTEM_MODULE a inner join SC_SYSTEM_MODULE b on b.SC_SYSTEM_MODULE_ID=a.PARENT_ID  where a.PARENT_ID='" + ob1.SC_SYSTEM_MODULE_ID + "' order by a.SYS_MODULE_NAME");
                        foreach (DataRow dr2 in ds2.Tables[0].Rows)
                        {
                            ScSystemModel2 ob2 = new ScSystemModel2();

                            ob2.SC_SYSTEM_MODULE_ID = (dr2["SC_SYSTEM_MODULE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr2["SC_SYSTEM_MODULE_ID"]);
                            ob2.SYS_MODULE_NAME = (dr2["SYS_MODULE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr2["SYS_MODULE_NAME"]);
                            ob2.PARENT_NAME = (dr2["PARENT_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr2["PARENT_NAME"]);
                            ob2.text = (dr2["SYS_MODULE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr2["SYS_MODULE_NAME"]);
                            ob2.expanded = false;
                            ob2.PARENT_ID = (dr2["PARENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr2["PARENT_ID"]);
                            ob2.MODULE_LEVEL = (dr2["MODULE_LEVEL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr2["MODULE_LEVEL"]);
                            ob2.IS_ACTIVE = (dr2["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr2["IS_ACTIVE"]);

                            var ds3 = db.ExecuteSQLStatement("select b.SYS_MODULE_NAME as PARENT_NAME, a.* from SC_SYSTEM_MODULE a inner join SC_SYSTEM_MODULE b on b.SC_SYSTEM_MODULE_ID=a.PARENT_ID  where a.PARENT_ID='" + ob2.SC_SYSTEM_MODULE_ID + "' order by a.SYS_MODULE_NAME");
                            foreach (DataRow dr3 in ds3.Tables[0].Rows)
                            {
                                ScSystemModel3 ob3 = new ScSystemModel3();

                                ob3.SC_SYSTEM_MODULE_ID = (dr3["SC_SYSTEM_MODULE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr3["SC_SYSTEM_MODULE_ID"]);
                                ob3.SYS_MODULE_NAME = (dr3["SYS_MODULE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr3["SYS_MODULE_NAME"]);
                                ob3.PARENT_NAME = (dr3["PARENT_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr3["PARENT_NAME"]);
                                ob3.text = (dr3["SYS_MODULE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr3["SYS_MODULE_NAME"]);
                                ob3.expanded = false;
                                ob3.PARENT_ID = (dr3["PARENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr3["PARENT_ID"]);
                                ob3.MODULE_LEVEL = (dr3["MODULE_LEVEL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr3["MODULE_LEVEL"]);
                                ob3.IS_ACTIVE = (dr3["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr3["IS_ACTIVE"]);

                                var ds4 = db.ExecuteSQLStatement("select b.SYS_MODULE_NAME as PARENT_NAME, a.* from SC_SYSTEM_MODULE a inner join SC_SYSTEM_MODULE b on b.SC_SYSTEM_MODULE_ID=a.PARENT_ID  where a.PARENT_ID='" + ob3.SC_SYSTEM_MODULE_ID + "' order by a.SYS_MODULE_NAME");
                                foreach (DataRow dr4 in ds4.Tables[0].Rows)
                                {
                                    ScSystemModel4 ob4 = new ScSystemModel4();

                                    ob4.SC_SYSTEM_MODULE_ID = (dr4["SC_SYSTEM_MODULE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr4["SC_SYSTEM_MODULE_ID"]);
                                    ob4.SYS_MODULE_NAME = (dr4["SYS_MODULE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr4["SYS_MODULE_NAME"]);
                                    ob4.PARENT_NAME = (dr4["PARENT_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr4["PARENT_NAME"]);
                                    ob4.text = (dr4["SYS_MODULE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr4["SYS_MODULE_NAME"]);
                                    ob4.expanded = false;
                                    ob4.PARENT_ID = (dr4["PARENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr4["PARENT_ID"]);
                                    ob4.MODULE_LEVEL = (dr4["MODULE_LEVEL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr4["MODULE_LEVEL"]);
                                    ob4.IS_ACTIVE = (dr4["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr4["IS_ACTIVE"]);

                                    var ds5 = db.ExecuteSQLStatement("select b.SYS_MODULE_NAME as PARENT_NAME, a.* from SC_SYSTEM_MODULE a inner join SC_SYSTEM_MODULE b on b.SC_SYSTEM_MODULE_ID=a.PARENT_ID  where a.PARENT_ID='" + ob4.SC_SYSTEM_MODULE_ID + "' order by a.SYS_MODULE_NAME");
                                    foreach (DataRow dr5 in ds5.Tables[0].Rows)
                                    {
                                        ScSystemModel5 ob5 = new ScSystemModel5();
                                        ob5.SC_SYSTEM_MODULE_ID = (dr5["SC_SYSTEM_MODULE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr5["SC_SYSTEM_MODULE_ID"]);
                                        ob5.SYS_MODULE_NAME = (dr5["SYS_MODULE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr5["SYS_MODULE_NAME"]);
                                        ob5.PARENT_NAME = (dr5["PARENT_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr5["PARENT_NAME"]);
                                        ob5.text = (dr5["SYS_MODULE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr5["SYS_MODULE_NAME"]);
                                        ob5.expanded = false;
                                        ob5.PARENT_ID = (dr5["PARENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr5["PARENT_ID"]);
                                        ob5.MODULE_LEVEL = (dr5["MODULE_LEVEL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr5["MODULE_LEVEL"]);
                                        ob5.IS_ACTIVE = (dr5["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr5["IS_ACTIVE"]);
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
            catch (Exception e)
            {
                throw e;
            }
        }

    }

    public class ScSystemModel1
    {
        public Int64 SC_SYSTEM_MODULE_ID { get; set; }
        public Int64 PARENT_ID { get; set; }
        public string SYS_MODULE_NAME { get; set; }
        public Int64 MODULE_LEVEL { get; set; }
        public string IS_ACTIVE { get; set; }

        public DateTime? CREATION_DATE { get; set; }
        public int CREATED_BY { get; set; }
        public DateTime? LAST_UPDATE_DATE { get; set; }
        public int LAST_UPDATED_BY { get; set; }
        public int LAST_UPDATE_LOGIN { get; set; }
        public int VERSION_NO { get; set; }


        public string PARENT_NAME { get; set; }
        public string text { get; set; }
        public bool expanded { get; set; }

        private List<ScSystemModel2> __subScSystemModel = null;
        public List<ScSystemModel2> items
        {
            get
            {
                if (__subScSystemModel == null)
                {
                    __subScSystemModel = new List<ScSystemModel2>();
                }
                return __subScSystemModel;
            }
            set
            {
                __subScSystemModel = value;
            }
        }

    }

    public class ScSystemModel2
    {
        public Int64 SC_SYSTEM_MODULE_ID { get; set; }
        public Int64 PARENT_ID { get; set; }
        public string SYS_MODULE_NAME { get; set; }
        public Int64 MODULE_LEVEL { get; set; }
        public string IS_ACTIVE { get; set; }

        public DateTime? CREATION_DATE { get; set; }
        public int CREATED_BY { get; set; }
        public DateTime? LAST_UPDATE_DATE { get; set; }
        public int LAST_UPDATED_BY { get; set; }
        public int LAST_UPDATE_LOGIN { get; set; }
        public int VERSION_NO { get; set; }


        public string PARENT_NAME { get; set; }
        public string text { get; set; }
        public bool expanded { get; set; }

        private List<ScSystemModel3> __subScSystemModel = null;
        public List<ScSystemModel3> items
        {
            get
            {
                if (__subScSystemModel == null)
                {
                    __subScSystemModel = new List<ScSystemModel3>();
                }
                return __subScSystemModel;
            }
            set
            {
                __subScSystemModel = value;
            }
        }

    }

    public class ScSystemModel3
    {
        public Int64 SC_SYSTEM_MODULE_ID { get; set; }
        public Int64 PARENT_ID { get; set; }
        public string SYS_MODULE_NAME { get; set; }
        public Int64 MODULE_LEVEL { get; set; }
        public string IS_ACTIVE { get; set; }

        public DateTime? CREATION_DATE { get; set; }
        public int CREATED_BY { get; set; }
        public DateTime? LAST_UPDATE_DATE { get; set; }
        public int LAST_UPDATED_BY { get; set; }
        public int LAST_UPDATE_LOGIN { get; set; }
        public int VERSION_NO { get; set; }


        public string PARENT_NAME { get; set; }
        public string text { get; set; }
        public bool expanded { get; set; }

        private List<ScSystemModel4> __subScSystemModel = null;
        public List<ScSystemModel4> items
        {
            get
            {
                if (__subScSystemModel == null)
                {
                    __subScSystemModel = new List<ScSystemModel4>();
                }
                return __subScSystemModel;
            }
            set
            {
                __subScSystemModel = value;
            }
        }

    }

    public class ScSystemModel4
    {
        public Int64 SC_SYSTEM_MODULE_ID { get; set; }
        public Int64 PARENT_ID { get; set; }
        public string SYS_MODULE_NAME { get; set; }
        public Int64 MODULE_LEVEL { get; set; }
        public string IS_ACTIVE { get; set; }

        public DateTime? CREATION_DATE { get; set; }
        public int CREATED_BY { get; set; }
        public DateTime? LAST_UPDATE_DATE { get; set; }
        public int LAST_UPDATED_BY { get; set; }
        public int LAST_UPDATE_LOGIN { get; set; }
        public int VERSION_NO { get; set; }


        public string PARENT_NAME { get; set; }
        public string text { get; set; }
        public bool expanded { get; set; }

        private List<ScSystemModel5> __subScSystemModel = null;
        public List<ScSystemModel5> items
        {
            get
            {
                if (__subScSystemModel == null)
                {
                    __subScSystemModel = new List<ScSystemModel5>();
                }
                return __subScSystemModel;
            }
            set
            {
                __subScSystemModel = value;
            }
        }

    }

    public class ScSystemModel5
    {
        public Int64 SC_SYSTEM_MODULE_ID { get; set; }
        public Int64 PARENT_ID { get; set; }
        public string SYS_MODULE_NAME { get; set; }
        public Int64 MODULE_LEVEL { get; set; }
        public string IS_ACTIVE { get; set; }

        public DateTime? CREATION_DATE { get; set; }
        public int CREATED_BY { get; set; }
        public DateTime? LAST_UPDATE_DATE { get; set; }
        public int LAST_UPDATED_BY { get; set; }
        public int LAST_UPDATE_LOGIN { get; set; }
        public int VERSION_NO { get; set; }


        public string PARENT_NAME { get; set; }
        public string text { get; set; }
        public bool expanded { get; set; }
        
        private List<ScSystemModel6> __subScSystemModel = null;
        public List<ScSystemModel6> items
        {
            get
            {
                if (__subScSystemModel == null)
                {
                    __subScSystemModel = new List<ScSystemModel6>();
                }
                return __subScSystemModel;
            }
            set
            {
                __subScSystemModel = value;
            }
        }
        

    }

    public class ScSystemModel6
    {
        public Int64 SC_SYSTEM_MODULE_ID { get; set; }
        public Int64 PARENT_ID { get; set; }
        public string SYS_MODULE_NAME { get; set; }
        public Int64 MODULE_LEVEL { get; set; }
        public string IS_ACTIVE { get; set; }

        public DateTime? CREATION_DATE { get; set; }
        public int CREATED_BY { get; set; }
        public DateTime? LAST_UPDATE_DATE { get; set; }
        public int LAST_UPDATED_BY { get; set; }
        public int LAST_UPDATE_LOGIN { get; set; }
        public int VERSION_NO { get; set; }


        public string PARENT_NAME { get; set; }
        public string text { get; set; }
        public bool expanded { get; set; }
        /*
        private List<ScSystemModel6> __subScSystemModel = null;
        public List<ScSystemModel6> items
        {
            get
            {
                if (__subScSystemModel == null)
                {
                    __subScSystemModel = new List<ScSystemModel6>();
                }
                return __subScSystemModel;
            }
            set
            {
                __subScSystemModel = value;
            }
        }
        */

    }

}
