using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Web;
namespace ERP.Model
{
    public class ScMenuModel //: IHrCompany
    {
        public Int64 SC_MENU_ID { get; set; }
        public Int64 PARENT_ID { get; set; }
        public Int64 SC_SYSTEM_MODULE_ID { get; set; }
        public string MENU_NAME_EN { get; set; }
        public string MENU_NAME_BN { get; set; }
        public string MENU_DESC { get; set; }
        public string MENU_URL { get; set; }
        public Int64 MENU_ORDER { get; set; }
        public Int64 MENU_LEVEL { get; set; }
        public string IS_OPEN_NEW_WINDOW { get; set; }
        public string IS_LEAF { get; set; }
        public string IS_REPORT { get; set; }
        public Int64 SC_SECURITY_LEVEL_ID { get; set; }
        public string ICON_PATH { get; set; }
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
        public Int64 id { get; set; }
        //public Int64 SC_ROLE_ID { get; set; }
        public bool IsChecked { get; set; }


        private List<ScMenuModel1> _subScMenuModel = null;
        public List<ScMenuModel1> items
        {
            get
            {
                if (_subScMenuModel == null)
                {
                    _subScMenuModel = new List<ScMenuModel1>();
                }
                return _subScMenuModel;
            }
            set
            {
                _subScMenuModel = value;
            }
        }

        public int TranMode { get; set; }

        public List<ScMenuModel> ScMenuData()
        {
            string sp = "pkg_menu.sc_menu_select";
            var obList = new List<ScMenuModel>();

            OraDatabase db = new OraDatabase();
            try
            {

                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   new CommandParameter() {ParameterName = "pOption", Value = 3001},
                    new CommandParameter() {ParameterName = "pPARENT_ID", Value = null}
                }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ScMenuModel ob = new ScMenuModel();
                    ob.SC_MENU_ID = (dr["SC_MENU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SC_MENU_ID"]);
                    ob.PARENT_ID = (dr["PARENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PARENT_ID"]);
                    ob.PARENT_NAME = (dr["PARENT_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PARENT_NAME"]);
                    ob.text = (dr["MENU_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MENU_NAME_EN"]);
                    ob.expanded = false;
                    ob.SC_SYSTEM_MODULE_ID = (dr["SC_SYSTEM_MODULE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SC_SYSTEM_MODULE_ID"]);
                    ob.MENU_NAME_EN = (dr["MENU_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MENU_NAME_EN"]);
                    ob.MENU_NAME_BN = (dr["MENU_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MENU_NAME_BN"]);
                    ob.MENU_DESC = (dr["MENU_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MENU_DESC"]);
                    ob.MENU_URL = (dr["MENU_URL"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MENU_URL"]);
                    ob.MENU_ORDER = (dr["MENU_ORDER"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MENU_ORDER"]);
                    ob.MENU_LEVEL = (dr["MENU_LEVEL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MENU_LEVEL"]);
                    ob.IS_OPEN_NEW_WINDOW = (dr["IS_OPEN_NEW_WINDOW"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_OPEN_NEW_WINDOW"]);
                    ob.IS_LEAF = (dr["IS_LEAF"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_LEAF"]);
                    ob.IS_REPORT = (dr["IS_REPORT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_REPORT"]);
                    ob.SC_SECURITY_LEVEL_ID = (dr["SC_SECURITY_LEVEL_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SC_SECURITY_LEVEL_ID"]);
                    ob.ICON_PATH = (dr["ICON_PATH"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ICON_PATH"]);
                    ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);
                    ob.spriteCssClass = ob.IS_LEAF == "Y" ? "html" : "folder";

                    var ds1 = db.ExecuteStoredProcedure(new List<CommandParameter>()
                    {   new CommandParameter() {ParameterName = "pOption", Value = 3002},
                        new CommandParameter() {ParameterName = "pPARENT_ID", Value = ob.SC_MENU_ID}
                       
                    }, sp);

                    foreach (DataRow dr1 in ds1.Tables[0].Rows)
                    {
                        ScMenuModel1 ob1 = new ScMenuModel1();

                        ob1.SC_MENU_ID = (dr1["SC_MENU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr1["SC_MENU_ID"]);
                        ob1.PARENT_ID = (dr1["PARENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr1["PARENT_ID"]);
                        ob1.PARENT_NAME = (dr1["PARENT_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["PARENT_NAME"]);
                        ob1.text = (dr1["MENU_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["MENU_NAME_EN"]);
                        ob1.expanded = false;
                        ob1.SC_SYSTEM_MODULE_ID = (dr1["SC_SYSTEM_MODULE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr1["SC_SYSTEM_MODULE_ID"]);
                        ob1.MENU_NAME_EN = (dr1["MENU_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["MENU_NAME_EN"]);
                        ob1.MENU_NAME_BN = (dr1["MENU_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["MENU_NAME_BN"]);
                        ob1.MENU_DESC = (dr1["MENU_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["MENU_DESC"]);
                        ob1.MENU_URL = (dr1["MENU_URL"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["MENU_URL"]);
                        ob1.MENU_ORDER = (dr1["MENU_ORDER"] == DBNull.Value) ? 0 : Convert.ToInt64(dr1["MENU_ORDER"]);
                        ob1.MENU_LEVEL = (dr1["MENU_LEVEL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr1["MENU_LEVEL"]);
                        ob1.IS_OPEN_NEW_WINDOW = (dr1["IS_OPEN_NEW_WINDOW"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["IS_OPEN_NEW_WINDOW"]);
                        ob1.IS_LEAF = (dr1["IS_LEAF"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["IS_LEAF"]);
                        ob1.IS_REPORT = (dr1["IS_REPORT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["IS_REPORT"]);
                        ob1.SC_SECURITY_LEVEL_ID = (dr1["SC_SECURITY_LEVEL_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr1["SC_SECURITY_LEVEL_ID"]);
                        ob1.ICON_PATH = (dr1["ICON_PATH"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["ICON_PATH"]);
                        ob1.IS_ACTIVE = (dr1["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["IS_ACTIVE"]);
                        if (ob1.IS_LEAF == "Y")
                            ob1.spriteCssClass = "html";
                        else
                            ob1.spriteCssClass = "folder";


                        var ds2 = db.ExecuteStoredProcedure(new List<CommandParameter>()
                        {   new CommandParameter() {ParameterName = "pOption", Value = 3002},
                            new CommandParameter() {ParameterName = "pPARENT_ID", Value = ob1.SC_MENU_ID}
                           
                        }, sp);
                        foreach (DataRow dr2 in ds2.Tables[0].Rows)
                        {
                            ScMenuModel2 ob2 = new ScMenuModel2();

                            ob2.SC_MENU_ID = (dr2["SC_MENU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr2["SC_MENU_ID"]);
                            ob2.PARENT_ID = (dr2["PARENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr2["PARENT_ID"]);
                            ob2.PARENT_NAME = (dr2["PARENT_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr2["PARENT_NAME"]);
                            ob2.text = (dr2["MENU_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr2["MENU_NAME_EN"]);
                            ob2.expanded = false;
                            ob2.SC_SYSTEM_MODULE_ID = (dr2["SC_SYSTEM_MODULE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr2["SC_SYSTEM_MODULE_ID"]);
                            ob2.MENU_NAME_EN = (dr2["MENU_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr2["MENU_NAME_EN"]);
                            ob2.MENU_NAME_BN = (dr2["MENU_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr2["MENU_NAME_BN"]);
                            ob2.MENU_DESC = (dr2["MENU_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr2["MENU_DESC"]);
                            ob2.MENU_URL = (dr2["MENU_URL"] == DBNull.Value) ? string.Empty : Convert.ToString(dr2["MENU_URL"]);
                            ob2.MENU_ORDER = (dr2["MENU_ORDER"] == DBNull.Value) ? 0 : Convert.ToInt64(dr2["MENU_ORDER"]);
                            ob2.MENU_LEVEL = (dr2["MENU_LEVEL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr2["MENU_LEVEL"]);
                            ob2.IS_OPEN_NEW_WINDOW = (dr2["IS_OPEN_NEW_WINDOW"] == DBNull.Value) ? string.Empty : Convert.ToString(dr2["IS_OPEN_NEW_WINDOW"]);
                            ob2.IS_LEAF = (dr2["IS_LEAF"] == DBNull.Value) ? string.Empty : Convert.ToString(dr2["IS_LEAF"]);
                            ob2.IS_REPORT = (dr2["IS_REPORT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr2["IS_REPORT"]);
                            ob2.SC_SECURITY_LEVEL_ID = (dr2["SC_SECURITY_LEVEL_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr2["SC_SECURITY_LEVEL_ID"]);
                            ob2.ICON_PATH = (dr2["ICON_PATH"] == DBNull.Value) ? string.Empty : Convert.ToString(dr2["ICON_PATH"]);
                            ob2.IS_ACTIVE = (dr2["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr2["IS_ACTIVE"]);
                            if (ob2.IS_LEAF == "Y")
                                ob2.spriteCssClass = "html";
                            else
                                ob2.spriteCssClass = "folder";

                            var ds3 = db.ExecuteStoredProcedure(new List<CommandParameter>()
                        {   new CommandParameter() {ParameterName = "pOption", Value = 3002},
                            new CommandParameter() {ParameterName = "pPARENT_ID", Value = ob2.SC_MENU_ID}
                           
                        }, sp);
                            foreach (DataRow dr3 in ds3.Tables[0].Rows)
                            {
                                ScMenuModel3 ob3 = new ScMenuModel3();
                                ob3.SC_MENU_ID = (dr3["SC_MENU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr3["SC_MENU_ID"]);
                                ob3.PARENT_ID = (dr3["PARENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr3["PARENT_ID"]);
                                ob3.PARENT_NAME = (dr3["PARENT_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr3["PARENT_NAME"]);
                                ob3.text = (dr3["MENU_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr3["MENU_NAME_EN"]);
                                ob3.expanded = false;
                                ob3.SC_SYSTEM_MODULE_ID = (dr3["SC_SYSTEM_MODULE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr3["SC_SYSTEM_MODULE_ID"]);
                                ob3.MENU_NAME_EN = (dr3["MENU_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr3["MENU_NAME_EN"]);
                                ob3.MENU_NAME_BN = (dr3["MENU_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr3["MENU_NAME_BN"]);
                                ob3.MENU_DESC = (dr3["MENU_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr3["MENU_DESC"]);
                                ob3.MENU_URL = (dr3["MENU_URL"] == DBNull.Value) ? string.Empty : Convert.ToString(dr3["MENU_URL"]);
                                ob3.MENU_ORDER = (dr3["MENU_ORDER"] == DBNull.Value) ? 0 : Convert.ToInt64(dr3["MENU_ORDER"]);
                                ob3.MENU_LEVEL = (dr3["MENU_LEVEL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr3["MENU_LEVEL"]);
                                ob3.IS_OPEN_NEW_WINDOW = (dr3["IS_OPEN_NEW_WINDOW"] == DBNull.Value) ? string.Empty : Convert.ToString(dr3["IS_OPEN_NEW_WINDOW"]);
                                ob3.IS_LEAF = (dr3["IS_LEAF"] == DBNull.Value) ? string.Empty : Convert.ToString(dr3["IS_LEAF"]);
                                ob3.IS_REPORT = (dr3["IS_REPORT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr3["IS_REPORT"]);
                                ob3.SC_SECURITY_LEVEL_ID = (dr3["SC_SECURITY_LEVEL_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr3["SC_SECURITY_LEVEL_ID"]);
                                ob3.ICON_PATH = (dr3["ICON_PATH"] == DBNull.Value) ? string.Empty : Convert.ToString(dr3["ICON_PATH"]);
                                ob3.IS_ACTIVE = (dr3["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr3["IS_ACTIVE"]);
                                if (ob3.IS_LEAF == "Y")
                                    ob3.spriteCssClass = "html";
                                else
                                    ob3.spriteCssClass = "folder";

                                var ds4 = db.ExecuteStoredProcedure(new List<CommandParameter>()
                            {   new CommandParameter() {ParameterName = "pOption", Value = 3002},
                                new CommandParameter() {ParameterName = "pPARENT_ID", Value = ob3.SC_MENU_ID}
                               
                            }, sp);
                                foreach (DataRow dr4 in ds4.Tables[0].Rows)
                                {
                                    ScMenuModel4 ob4 = new ScMenuModel4();
                                    ob4.SC_MENU_ID = (dr4["SC_MENU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr4["SC_MENU_ID"]);
                                    ob4.PARENT_ID = (dr4["PARENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr4["PARENT_ID"]);
                                    ob4.PARENT_NAME = (dr4["PARENT_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr4["PARENT_NAME"]);
                                    ob4.text = (dr4["MENU_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr4["MENU_NAME_EN"]);
                                    ob4.expanded = false;
                                    ob4.SC_SYSTEM_MODULE_ID = (dr4["SC_SYSTEM_MODULE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr4["SC_SYSTEM_MODULE_ID"]);
                                    ob4.MENU_NAME_EN = (dr4["MENU_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr4["MENU_NAME_EN"]);
                                    ob4.MENU_NAME_BN = (dr4["MENU_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr4["MENU_NAME_BN"]);
                                    ob4.MENU_DESC = (dr4["MENU_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr4["MENU_DESC"]);
                                    ob4.MENU_URL = (dr4["MENU_URL"] == DBNull.Value) ? string.Empty : Convert.ToString(dr4["MENU_URL"]);
                                    ob4.MENU_ORDER = (dr4["MENU_ORDER"] == DBNull.Value) ? 0 : Convert.ToInt64(dr4["MENU_ORDER"]);
                                    ob4.MENU_LEVEL = (dr4["MENU_LEVEL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr4["MENU_LEVEL"]);
                                    ob4.IS_OPEN_NEW_WINDOW = (dr4["IS_OPEN_NEW_WINDOW"] == DBNull.Value) ? string.Empty : Convert.ToString(dr4["IS_OPEN_NEW_WINDOW"]);
                                    ob4.IS_LEAF = (dr4["IS_LEAF"] == DBNull.Value) ? string.Empty : Convert.ToString(dr4["IS_LEAF"]);
                                    ob4.IS_REPORT = (dr4["IS_REPORT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr4["IS_REPORT"]);
                                    ob4.SC_SECURITY_LEVEL_ID = (dr4["SC_SECURITY_LEVEL_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr4["SC_SECURITY_LEVEL_ID"]);
                                    ob4.ICON_PATH = (dr4["ICON_PATH"] == DBNull.Value) ? string.Empty : Convert.ToString(dr4["ICON_PATH"]);
                                    ob4.IS_ACTIVE = (dr4["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr4["IS_ACTIVE"]);
                                    if (ob4.IS_LEAF == "Y")
                                        ob4.spriteCssClass = "html";
                                    else
                                        ob4.spriteCssClass = "folder";

                                    var ds5 = db.ExecuteStoredProcedure(new List<CommandParameter>()
                                    {   new CommandParameter() {ParameterName = "pOption", Value = 3002},
                                        new CommandParameter() {ParameterName = "pPARENT_ID", Value = ob4.SC_MENU_ID}
                                       
                                    }, sp);
                                    foreach (DataRow dr5 in ds5.Tables[0].Rows)
                                    {
                                        ScMenuModel5 ob5 = new ScMenuModel5();
                                        ob5.SC_MENU_ID = (dr5["SC_MENU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr5["SC_MENU_ID"]);
                                        ob5.PARENT_ID = (dr5["PARENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr5["PARENT_ID"]);
                                        ob5.PARENT_NAME = (dr5["PARENT_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr5["PARENT_NAME"]);
                                        ob5.text = (dr5["MENU_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr5["MENU_NAME_EN"]);
                                        ob5.expanded = false;
                                        ob5.SC_SYSTEM_MODULE_ID = (dr5["SC_SYSTEM_MODULE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr5["SC_SYSTEM_MODULE_ID"]);
                                        ob5.MENU_NAME_EN = (dr5["MENU_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr5["MENU_NAME_EN"]);
                                        ob5.MENU_NAME_BN = (dr5["MENU_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr5["MENU_NAME_BN"]);
                                        ob5.MENU_DESC = (dr5["MENU_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr5["MENU_DESC"]);
                                        ob5.MENU_URL = (dr5["MENU_URL"] == DBNull.Value) ? string.Empty : Convert.ToString(dr5["MENU_URL"]);
                                        ob5.MENU_ORDER = (dr5["MENU_ORDER"] == DBNull.Value) ? 0 : Convert.ToInt64(dr5["MENU_ORDER"]);
                                        ob5.MENU_LEVEL = (dr5["MENU_LEVEL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr5["MENU_LEVEL"]);
                                        ob5.IS_OPEN_NEW_WINDOW = (dr5["IS_OPEN_NEW_WINDOW"] == DBNull.Value) ? string.Empty : Convert.ToString(dr5["IS_OPEN_NEW_WINDOW"]);
                                        ob5.IS_LEAF = (dr5["IS_LEAF"] == DBNull.Value) ? string.Empty : Convert.ToString(dr5["IS_LEAF"]);
                                        ob5.IS_REPORT = (dr5["IS_REPORT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr5["IS_REPORT"]);
                                        ob5.SC_SECURITY_LEVEL_ID = (dr5["SC_SECURITY_LEVEL_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr5["SC_SECURITY_LEVEL_ID"]);
                                        ob5.ICON_PATH = (dr5["ICON_PATH"] == DBNull.Value) ? string.Empty : Convert.ToString(dr5["ICON_PATH"]);
                                        ob5.IS_ACTIVE = (dr5["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr5["IS_ACTIVE"]);
                                        if (ob5.IS_LEAF == "Y")
                                            ob5.spriteCssClass = "html";
                                        else
                                            ob5.spriteCssClass = "folder";

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

        public List<ScMenuModel> ScSideMenuData(Int64 pSC_USER_ID)
        {
            string sp = "pkg_menu.sc_menu_select";

            //List<ScMenuModel> obUserMenuList = new List<ScMenuModel>();

            //List<ScMenuModel> obList = new List<ScMenuModel>();

            try
            {

                var obList = new List<ScMenuModel>();
                var obUserMenuList = new List<ScMenuModel>();

                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   new CommandParameter() {ParameterName = "pOption", Value = 3006},
                    new CommandParameter() {ParameterName = "pPARENT_ID", Value = null},
                    new CommandParameter() {ParameterName = "pSC_USER_ID", Value = pSC_USER_ID},
                }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ScMenuModel obUserMenu = new ScMenuModel();
                    ScMenuModel ob = new ScMenuModel();

                    ob.SC_MENU_ID = (dr["SC_MENU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SC_MENU_ID"]);
                    ob.PARENT_ID = (dr["PARENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PARENT_ID"]);
                    ob.PARENT_NAME = (dr["PARENT_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PARENT_NAME"]);
                    ob.text = (dr["MENU_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MENU_NAME_EN"]);
                    ob.expanded = true;
                    ob.SC_SYSTEM_MODULE_ID = (dr["SC_SYSTEM_MODULE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SC_SYSTEM_MODULE_ID"]);
                    ob.MENU_NAME_EN = (dr["MENU_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MENU_NAME_EN"]);
                    ob.MENU_NAME_BN = (dr["MENU_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MENU_NAME_BN"]);
                    ob.MENU_DESC = (dr["MENU_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MENU_DESC"]);
                    ob.MENU_URL = (dr["MENU_URL"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MENU_URL"]);
                    ob.MENU_ORDER = (dr["MENU_ORDER"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MENU_ORDER"]);
                    ob.MENU_LEVEL = (dr["MENU_LEVEL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MENU_LEVEL"]);
                    ob.IS_OPEN_NEW_WINDOW = (dr["IS_OPEN_NEW_WINDOW"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_OPEN_NEW_WINDOW"]);
                    ob.IS_LEAF = (dr["IS_LEAF"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_LEAF"]);
                    ob.IS_REPORT = (dr["IS_REPORT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_REPORT"]);
                    ob.SC_SECURITY_LEVEL_ID = (dr["SC_SECURITY_LEVEL_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SC_SECURITY_LEVEL_ID"]);
                    ob.ICON_PATH = (dr["ICON_PATH"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ICON_PATH"]);
                    ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);
                    //ob.id = ob.SC_MENU_ID;
                    //ob.Checked = false; //(dr["Checked"] == DBNull.Value) ? dr["Checked"];


                    if (ob.IS_LEAF == "Y")
                    {
                        obUserMenu.SC_MENU_ID = ob.SC_MENU_ID;
                        obUserMenu.MENU_NAME_EN = ob.MENU_NAME_EN;
                        obUserMenuList.Add(obUserMenu);

                        ob.spriteCssClass = "html";
                    }
                    else
                        ob.spriteCssClass = "folder";




                    var ds1 = db.ExecuteStoredProcedure(new List<CommandParameter>()
                        {   new CommandParameter() {ParameterName = "pOption", Value = 3005},
                            new CommandParameter() {ParameterName = "pPARENT_ID", Value = ob.SC_MENU_ID},
                            new CommandParameter() {ParameterName = "pSC_USER_ID", Value = pSC_USER_ID}
                        }, sp);
                    foreach (DataRow dr1 in ds1.Tables[0].Rows)
                    {
                        ScMenuModel obUserMenu1 = new ScMenuModel();
                        ScMenuModel1 ob1 = new ScMenuModel1();

                        ob1.SC_MENU_ID = (dr1["SC_MENU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr1["SC_MENU_ID"]);
                        ob1.PARENT_ID = (dr1["PARENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr1["PARENT_ID"]);
                        ob1.PARENT_NAME = (dr1["PARENT_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["PARENT_NAME"]);
                        ob1.text = (dr1["MENU_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["MENU_NAME_EN"]);
                        ob1.expanded = true;
                        ob1.SC_SYSTEM_MODULE_ID = (dr1["SC_SYSTEM_MODULE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr1["SC_SYSTEM_MODULE_ID"]);
                        ob1.MENU_NAME_EN = (dr1["MENU_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["MENU_NAME_EN"]);
                        ob1.MENU_NAME_BN = (dr1["MENU_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["MENU_NAME_BN"]);
                        ob1.MENU_DESC = (dr1["MENU_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["MENU_DESC"]);
                        ob1.MENU_URL = (dr1["MENU_URL"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["MENU_URL"]);
                        ob1.MENU_ORDER = (dr1["MENU_ORDER"] == DBNull.Value) ? 0 : Convert.ToInt64(dr1["MENU_ORDER"]);
                        ob1.MENU_LEVEL = (dr1["MENU_LEVEL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr1["MENU_LEVEL"]);
                        ob1.IS_OPEN_NEW_WINDOW = (dr1["IS_OPEN_NEW_WINDOW"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["IS_OPEN_NEW_WINDOW"]);
                        ob1.IS_LEAF = (dr1["IS_LEAF"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["IS_LEAF"]);
                        ob1.IS_REPORT = (dr1["IS_REPORT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["IS_REPORT"]);
                        ob1.SC_SECURITY_LEVEL_ID = (dr1["SC_SECURITY_LEVEL_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr1["SC_SECURITY_LEVEL_ID"]);
                        ob1.ICON_PATH = (dr1["ICON_PATH"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["ICON_PATH"]);
                        ob1.IS_ACTIVE = (dr1["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["IS_ACTIVE"]);
                        //ob1.id = ob1.SC_MENU_ID;
                        //ob1.Checked = true; 

                        if (ob1.IS_LEAF == "Y")
                        {
                            obUserMenu1.SC_MENU_ID = ob1.SC_MENU_ID;
                            obUserMenu1.MENU_NAME_EN = ob1.MENU_NAME_EN;
                            obUserMenuList.Add(obUserMenu1);
                            ob1.spriteCssClass = "html";
                        }
                        else
                            ob1.spriteCssClass = "folder";

                        var ds2 = db.ExecuteStoredProcedure(new List<CommandParameter>()
                                {   new CommandParameter() {ParameterName = "pOption", Value = 3005},
                                    new CommandParameter() {ParameterName = "pPARENT_ID", Value = ob1.SC_MENU_ID},
                                    new CommandParameter() {ParameterName = "pSC_USER_ID", Value = pSC_USER_ID}
                                }, sp);
                        foreach (DataRow dr2 in ds2.Tables[0].Rows)
                        {
                            ScMenuModel obUserMenu2 = new ScMenuModel();
                            ScMenuModel2 ob2 = new ScMenuModel2();

                            ob2.SC_MENU_ID = (dr2["SC_MENU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr2["SC_MENU_ID"]);
                            ob2.PARENT_ID = (dr2["PARENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr2["PARENT_ID"]);
                            ob2.PARENT_NAME = (dr2["PARENT_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr2["PARENT_NAME"]);
                            ob2.text = (dr2["MENU_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr2["MENU_NAME_EN"]);
                            ob2.expanded = true;
                            ob2.SC_SYSTEM_MODULE_ID = (dr2["SC_SYSTEM_MODULE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr2["SC_SYSTEM_MODULE_ID"]);
                            ob2.MENU_NAME_EN = (dr2["MENU_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr2["MENU_NAME_EN"]);
                            ob2.MENU_NAME_BN = (dr2["MENU_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr2["MENU_NAME_BN"]);
                            ob2.MENU_DESC = (dr2["MENU_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr2["MENU_DESC"]);
                            ob2.MENU_URL = (dr2["MENU_URL"] == DBNull.Value) ? string.Empty : Convert.ToString(dr2["MENU_URL"]);
                            ob2.MENU_ORDER = (dr2["MENU_ORDER"] == DBNull.Value) ? 0 : Convert.ToInt64(dr2["MENU_ORDER"]);
                            ob2.MENU_LEVEL = (dr2["MENU_LEVEL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr2["MENU_LEVEL"]);
                            ob2.IS_OPEN_NEW_WINDOW = (dr2["IS_OPEN_NEW_WINDOW"] == DBNull.Value) ? string.Empty : Convert.ToString(dr2["IS_OPEN_NEW_WINDOW"]);
                            ob2.IS_LEAF = (dr2["IS_LEAF"] == DBNull.Value) ? string.Empty : Convert.ToString(dr2["IS_LEAF"]);
                            ob2.IS_REPORT = (dr2["IS_REPORT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr2["IS_REPORT"]);
                            ob2.SC_SECURITY_LEVEL_ID = (dr2["SC_SECURITY_LEVEL_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr2["SC_SECURITY_LEVEL_ID"]);
                            ob2.ICON_PATH = (dr2["ICON_PATH"] == DBNull.Value) ? string.Empty : Convert.ToString(dr2["ICON_PATH"]);
                            ob2.IS_ACTIVE = (dr2["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr2["IS_ACTIVE"]);
                            //ob2.id = ob2.SC_MENU_ID;
                            //ob2.Checked = false;
                            if (ob2.IS_LEAF == "Y")
                            {
                                obUserMenu2.SC_MENU_ID = ob2.SC_MENU_ID;
                                obUserMenu2.MENU_NAME_EN = ob2.MENU_NAME_EN;
                                obUserMenuList.Add(obUserMenu2);
                                ob2.spriteCssClass = "html";
                            }
                            else
                                ob2.spriteCssClass = "folder";


                            var ds3 = db.ExecuteStoredProcedure(new List<CommandParameter>()
                                {   new CommandParameter() {ParameterName = "pOption", Value = 3005},
                                    new CommandParameter() {ParameterName = "pPARENT_ID", Value = ob2.SC_MENU_ID},
                                    new CommandParameter() {ParameterName = "pSC_USER_ID", Value = pSC_USER_ID}
                                }, sp);
                            foreach (DataRow dr3 in ds3.Tables[0].Rows)
                            {
                                ScMenuModel obUserMenu3 = new ScMenuModel();
                                ScMenuModel3 ob3 = new ScMenuModel3();

                                ob3.SC_MENU_ID = (dr3["SC_MENU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr3["SC_MENU_ID"]);
                                ob3.PARENT_ID = (dr3["PARENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr3["PARENT_ID"]);
                                ob3.PARENT_NAME = (dr3["PARENT_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr3["PARENT_NAME"]);
                                ob3.text = (dr3["MENU_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr3["MENU_NAME_EN"]);
                                ob3.expanded = true;
                                ob3.SC_SYSTEM_MODULE_ID = (dr3["SC_SYSTEM_MODULE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr3["SC_SYSTEM_MODULE_ID"]);
                                ob3.MENU_NAME_EN = (dr3["MENU_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr3["MENU_NAME_EN"]);
                                ob3.MENU_NAME_BN = (dr3["MENU_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr3["MENU_NAME_BN"]);
                                ob3.MENU_DESC = (dr3["MENU_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr3["MENU_DESC"]);
                                ob3.MENU_URL = (dr3["MENU_URL"] == DBNull.Value) ? string.Empty : Convert.ToString(dr3["MENU_URL"]);
                                ob3.MENU_ORDER = (dr3["MENU_ORDER"] == DBNull.Value) ? 0 : Convert.ToInt64(dr3["MENU_ORDER"]);
                                ob3.MENU_LEVEL = (dr3["MENU_LEVEL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr3["MENU_LEVEL"]);
                                ob3.IS_OPEN_NEW_WINDOW = (dr3["IS_OPEN_NEW_WINDOW"] == DBNull.Value) ? string.Empty : Convert.ToString(dr3["IS_OPEN_NEW_WINDOW"]);
                                ob3.IS_LEAF = (dr3["IS_LEAF"] == DBNull.Value) ? string.Empty : Convert.ToString(dr3["IS_LEAF"]);
                                ob3.IS_REPORT = (dr3["IS_REPORT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr3["IS_REPORT"]);
                                ob3.SC_SECURITY_LEVEL_ID = (dr3["SC_SECURITY_LEVEL_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr3["SC_SECURITY_LEVEL_ID"]);
                                ob3.ICON_PATH = (dr3["ICON_PATH"] == DBNull.Value) ? string.Empty : Convert.ToString(dr3["ICON_PATH"]);
                                ob3.IS_ACTIVE = (dr3["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr3["IS_ACTIVE"]);
                                //ob3.id = ob3.SC_MENU_ID;

                                if (ob3.IS_LEAF == "Y")
                                {
                                    obUserMenu3.SC_MENU_ID = ob3.SC_MENU_ID;
                                    obUserMenu3.MENU_NAME_EN = ob3.MENU_NAME_EN;
                                    obUserMenuList.Add(obUserMenu3);
                                    ob3.spriteCssClass = "html";
                                }
                                else
                                    ob3.spriteCssClass = "folder";



                                var ds4 = db.ExecuteStoredProcedure(new List<CommandParameter>()
                                    {   new CommandParameter() {ParameterName = "pOption", Value = 3005},
                                        new CommandParameter() {ParameterName = "pPARENT_ID", Value = ob3.SC_MENU_ID},
                                        new CommandParameter() {ParameterName = "pSC_USER_ID", Value = pSC_USER_ID}
                                    }, sp);
                                foreach (DataRow dr4 in ds4.Tables[0].Rows)
                                {
                                    ScMenuModel obUserMenu4 = new ScMenuModel();
                                    ScMenuModel4 ob4 = new ScMenuModel4();

                                    ob4.SC_MENU_ID = (dr4["SC_MENU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr4["SC_MENU_ID"]);
                                    ob4.PARENT_ID = (dr4["PARENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr4["PARENT_ID"]);
                                    ob4.PARENT_NAME = (dr4["PARENT_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr4["PARENT_NAME"]);
                                    ob4.text = (dr4["MENU_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr4["MENU_NAME_EN"]);
                                    ob4.expanded = true;
                                    ob4.SC_SYSTEM_MODULE_ID = (dr4["SC_SYSTEM_MODULE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr4["SC_SYSTEM_MODULE_ID"]);
                                    ob4.MENU_NAME_EN = (dr4["MENU_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr4["MENU_NAME_EN"]);
                                    ob4.MENU_NAME_BN = (dr4["MENU_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr4["MENU_NAME_BN"]);
                                    ob4.MENU_DESC = (dr4["MENU_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr4["MENU_DESC"]);
                                    ob4.MENU_URL = (dr4["MENU_URL"] == DBNull.Value) ? string.Empty : Convert.ToString(dr4["MENU_URL"]);
                                    ob4.MENU_ORDER = (dr4["MENU_ORDER"] == DBNull.Value) ? 0 : Convert.ToInt64(dr4["MENU_ORDER"]);
                                    ob4.MENU_LEVEL = (dr4["MENU_LEVEL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr4["MENU_LEVEL"]);
                                    ob4.IS_OPEN_NEW_WINDOW = (dr4["IS_OPEN_NEW_WINDOW"] == DBNull.Value) ? string.Empty : Convert.ToString(dr4["IS_OPEN_NEW_WINDOW"]);
                                    ob4.IS_LEAF = (dr4["IS_LEAF"] == DBNull.Value) ? string.Empty : Convert.ToString(dr4["IS_LEAF"]);
                                    ob4.IS_REPORT = (dr4["IS_REPORT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr4["IS_REPORT"]);
                                    ob4.SC_SECURITY_LEVEL_ID = (dr4["SC_SECURITY_LEVEL_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr4["SC_SECURITY_LEVEL_ID"]);
                                    ob4.ICON_PATH = (dr4["ICON_PATH"] == DBNull.Value) ? string.Empty : Convert.ToString(dr4["ICON_PATH"]);
                                    ob4.IS_ACTIVE = (dr4["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr4["IS_ACTIVE"]);
                                    //ob4.id = ob4.SC_MENU_ID;

                                    if (ob4.IS_LEAF == "Y")
                                    {
                                        obUserMenu4.SC_MENU_ID = ob4.SC_MENU_ID;
                                        obUserMenu4.MENU_NAME_EN = ob4.MENU_NAME_EN;
                                        obUserMenuList.Add(obUserMenu4);
                                        ob4.spriteCssClass = "html";
                                    }
                                    else
                                        ob4.spriteCssClass = "folder";


                                    var ds5 = db.ExecuteStoredProcedure(new List<CommandParameter>()
                                        {   new CommandParameter() {ParameterName = "pOption", Value = 3005},
                                            new CommandParameter() {ParameterName = "pPARENT_ID", Value = ob4.SC_MENU_ID},
                                            new CommandParameter() {ParameterName = "pSC_USER_ID", Value = pSC_USER_ID}
                                        }, sp);
                                    foreach (DataRow dr5 in ds5.Tables[0].Rows)
                                    {
                                        ScMenuModel obUserMenu5 = new ScMenuModel();
                                        ScMenuModel5 ob5 = new ScMenuModel5();

                                        ob5.SC_MENU_ID = (dr5["SC_MENU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr5["SC_MENU_ID"]);
                                        ob5.PARENT_ID = (dr5["PARENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr5["PARENT_ID"]);
                                        ob5.PARENT_NAME = (dr5["PARENT_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr5["PARENT_NAME"]);
                                        ob5.text = (dr5["MENU_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr5["MENU_NAME_EN"]);
                                        ob5.expanded = true;
                                        ob5.SC_SYSTEM_MODULE_ID = (dr5["SC_SYSTEM_MODULE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr5["SC_SYSTEM_MODULE_ID"]);
                                        ob5.MENU_NAME_EN = (dr5["MENU_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr5["MENU_NAME_EN"]);
                                        ob5.MENU_NAME_BN = (dr5["MENU_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr5["MENU_NAME_BN"]);
                                        ob5.MENU_DESC = (dr5["MENU_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr5["MENU_DESC"]);
                                        ob5.MENU_URL = (dr5["MENU_URL"] == DBNull.Value) ? string.Empty : Convert.ToString(dr5["MENU_URL"]);
                                        ob5.MENU_ORDER = (dr5["MENU_ORDER"] == DBNull.Value) ? 0 : Convert.ToInt64(dr5["MENU_ORDER"]);
                                        ob5.MENU_LEVEL = (dr5["MENU_LEVEL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr5["MENU_LEVEL"]);
                                        ob5.IS_OPEN_NEW_WINDOW = (dr5["IS_OPEN_NEW_WINDOW"] == DBNull.Value) ? string.Empty : Convert.ToString(dr5["IS_OPEN_NEW_WINDOW"]);
                                        ob5.IS_LEAF = (dr5["IS_LEAF"] == DBNull.Value) ? string.Empty : Convert.ToString(dr5["IS_LEAF"]);
                                        ob5.IS_REPORT = (dr5["IS_REPORT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr5["IS_REPORT"]);
                                        ob5.SC_SECURITY_LEVEL_ID = (dr5["SC_SECURITY_LEVEL_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr5["SC_SECURITY_LEVEL_ID"]);
                                        ob5.ICON_PATH = (dr5["ICON_PATH"] == DBNull.Value) ? string.Empty : Convert.ToString(dr5["ICON_PATH"]);
                                        ob5.IS_ACTIVE = (dr5["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr5["IS_ACTIVE"]);
                                        //ob5.id = ob5.SC_MENU_ID;

                                        if (ob5.IS_LEAF == "Y")
                                        {
                                            obUserMenu5.SC_MENU_ID = ob5.SC_MENU_ID;
                                            obUserMenu5.MENU_NAME_EN = ob5.MENU_NAME_EN;
                                            obUserMenuList.Add(obUserMenu5);
                                            ob5.spriteCssClass = "html";
                                        }
                                        else
                                            ob5.spriteCssClass = "folder";

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



                HttpContext.Current.Session["multiUserMenuList"] = obUserMenuList;

                //List<ScMenuModel> obUserMenuList1 = new List<ScMenuModel>();
                //obUserMenuList1 = (List<ScMenuModel>)HttpContext.Current.Session["multiUserMenuList"];

                return obList;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public string Save()
        {
            const string sp = "pkg_menu.sc_menu_insert";
            string vMsg = "";
            var ob = this;

            OraDatabase db = new OraDatabase();
            try
            {
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                    new CommandParameter() {ParameterName = "pSC_MENU_ID", Value = ob.SC_MENU_ID},
                    new CommandParameter() {ParameterName = "pPARENT_ID", Value = ob.PARENT_ID},
                    new CommandParameter() {ParameterName = "pSC_SYSTEM_MODULE_ID", Value = ob.SC_SYSTEM_MODULE_ID},
                    new CommandParameter() {ParameterName = "pMENU_NAME_EN", Value = ob.MENU_NAME_EN},
                    new CommandParameter() {ParameterName = "pMENU_DESC", Value = ob.MENU_DESC},
                    new CommandParameter() {ParameterName = "pMENU_URL", Value = ob.MENU_URL},
                    new CommandParameter() {ParameterName = "pMENU_ORDER", Value = ob.MENU_ORDER},
                    new CommandParameter() {ParameterName = "pMENU_LEVEL", Value = ob.MENU_LEVEL},
                    new CommandParameter() {ParameterName = "pIS_OPEN_NEW_WINDOW", Value = ob.IS_OPEN_NEW_WINDOW == null ? "N" : "Y"},
                    new CommandParameter() {ParameterName = "pIS_LEAF", Value = ob.IS_LEAF == null ? "N" : "Y"},
                    new CommandParameter() {ParameterName = "pIS_REPORT", Value = ob.IS_REPORT == null ? "N" : "Y"},
                    new CommandParameter() {ParameterName = "pSC_SECURITY_LEVEL_ID", Value = ob.SC_SECURITY_LEVEL_ID},
                    new CommandParameter() {ParameterName = "pICON_PATH", Value = ob.ICON_PATH},
                    new CommandParameter() {ParameterName = "pIS_ACTIVE", Value = ob.IS_ACTIVE == null ? "N" : "Y"},
                    new CommandParameter() {ParameterName = "pCREATION_DATE", Value = DateTime.Now},
                    new CommandParameter() {ParameterName = "pVERSION_NO", Value = 1},                   
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
            const string sp = "pkg_menu.sc_menu_update";
            string vMsg = "";
            var ob = this;


            OraDatabase db = new OraDatabase();
            try
            {
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                    new CommandParameter() {ParameterName = "pSC_MENU_ID", Value = ob.SC_MENU_ID},
                    new CommandParameter() {ParameterName = "pPARENT_ID", Value = ob.PARENT_ID},
                    new CommandParameter() {ParameterName = "pSC_SYSTEM_MODULE_ID", Value = ob.SC_SYSTEM_MODULE_ID},
                    new CommandParameter() {ParameterName = "pMENU_NAME_EN", Value = ob.MENU_NAME_EN},
                    new CommandParameter() {ParameterName = "pMENU_DESC", Value = ob.MENU_DESC},
                    new CommandParameter() {ParameterName = "pMENU_URL", Value = ob.MENU_URL},
                    new CommandParameter() {ParameterName = "pMENU_ORDER", Value = ob.MENU_ORDER},
                    new CommandParameter() {ParameterName = "pMENU_LEVEL", Value = ob.MENU_LEVEL},
                    new CommandParameter() {ParameterName = "pIS_OPEN_NEW_WINDOW", Value = ob.IS_OPEN_NEW_WINDOW == null ? "N" : "Y"},
                    new CommandParameter() {ParameterName = "pIS_LEAF", Value = ob.IS_LEAF == null ? "N" : "Y"},
                    new CommandParameter() {ParameterName = "pIS_REPORT", Value = ob.IS_REPORT == null ? "N" : "Y"},
                    new CommandParameter() {ParameterName = "pSC_SECURITY_LEVEL_ID", Value = ob.SC_SECURITY_LEVEL_ID},
                    new CommandParameter() {ParameterName = "pICON_PATH", Value = ob.ICON_PATH},
                    new CommandParameter() {ParameterName = "pIS_ACTIVE", Value = ob.IS_ACTIVE == null ? "N" : "Y"},
                    new CommandParameter() {ParameterName = "pCREATION_DATE", Value = DateTime.Now},
                    new CommandParameter() {ParameterName = "pVERSION_NO", Value = 1},                   
                    new CommandParameter() {ParameterName = "pOption", Value = 2000},
                    new CommandParameter() {ParameterName = "pMsg", Value = 200, Direction = ParameterDirection.Output}
                }, sp);

                foreach (DataRow dr in ds.Tables["OUTPARAM"].Rows)
                {
                    vMsg = dr["VALUE"].ToString();
                }

            }
            catch (Exception e)
            {
                vMsg = e.ToString();
            }

            return vMsg;
        }

        public List<ScMenuModel> ScRoleMenuData(Int64 ModuleId, Int64 pSC_ROLE_ID)
        {
            string sp = "pkg_menu.sc_menu_select";
            var obList = new List<ScMenuModel>();
            OraDatabase db = new OraDatabase();
            try
            {

                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                    new CommandParameter() {ParameterName = "pSC_ROLE_ID", Value = pSC_ROLE_ID},
                    new CommandParameter() {ParameterName = "pPARENT_ID", Value = null},
                    new CommandParameter() {ParameterName = "pSC_SYSTEM_MODULE_ID", Value = ModuleId},
                    new CommandParameter() {ParameterName = "pOption", Value = 3003}
                    //new CommandParameter() {ParameterName = "pMsg", Value = 200, Direction = ParameterDirection.Output}
                }, sp);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ScMenuModel ob = new ScMenuModel();

                    ob.SC_MENU_ID = (dr["SC_MENU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SC_MENU_ID"]);
                    ob.PARENT_ID = (dr["PARENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PARENT_ID"]);
                    ob.PARENT_NAME = (dr["PARENT_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PARENT_NAME"]);
                    ob.text = (dr["MENU_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MENU_NAME_EN"]);
                    ob.expanded = true;
                    ob.SC_SYSTEM_MODULE_ID = (dr["SC_SYSTEM_MODULE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SC_SYSTEM_MODULE_ID"]);
                    ob.MENU_NAME_EN = (dr["MENU_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MENU_NAME_EN"]);
                    ob.MENU_NAME_BN = (dr["MENU_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MENU_NAME_BN"]);
                    ob.MENU_DESC = (dr["MENU_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MENU_DESC"]);
                    ob.MENU_URL = (dr["MENU_URL"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MENU_URL"]);
                    ob.MENU_ORDER = (dr["MENU_ORDER"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MENU_ORDER"]);
                    ob.MENU_LEVEL = (dr["MENU_LEVEL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MENU_LEVEL"]);
                    ob.IS_OPEN_NEW_WINDOW = (dr["IS_OPEN_NEW_WINDOW"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_OPEN_NEW_WINDOW"]);
                    ob.IS_LEAF = (dr["IS_LEAF"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_LEAF"]);
                    ob.IS_REPORT = (dr["IS_REPORT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_REPORT"]);
                    ob.SC_SECURITY_LEVEL_ID = (dr["SC_SECURITY_LEVEL_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SC_SECURITY_LEVEL_ID"]);
                    ob.ICON_PATH = (dr["ICON_PATH"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ICON_PATH"]);
                    ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);
                    ob.id = ob.SC_MENU_ID;
                    //ob.SC_ROLE_ID = (dr["SC_ROLE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SC_ROLE_ID"]);
                    ob.IsChecked = (Convert.ToString(dr["ROLE_MENU_IS_ACTIVE"]) == "Y") ? true : false;


                    if (ob.IS_LEAF == "Y")
                        ob.spriteCssClass = "html";
                    else
                        ob.spriteCssClass = "folder";


                    var ds1 = db.ExecuteStoredProcedure(new List<CommandParameter>()
                        {   
                            new CommandParameter() {ParameterName = "pPARENT_ID", Value = ob.SC_MENU_ID},
                            new CommandParameter() {ParameterName = "pOption", Value = 3004},
                            new CommandParameter() {ParameterName = "pSC_ROLE_ID", Value =pSC_ROLE_ID}
                        }, sp);
                    foreach (DataRow dr1 in ds1.Tables[0].Rows)
                    {
                        ScMenuModel1 ob1 = new ScMenuModel1();

                        ob1.SC_MENU_ID = (dr1["SC_MENU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr1["SC_MENU_ID"]);
                        ob1.PARENT_ID = (dr1["PARENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr1["PARENT_ID"]);
                        ob1.PARENT_NAME = (dr1["PARENT_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["PARENT_NAME"]);
                        ob1.text = (dr1["MENU_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["MENU_NAME_EN"]);
                        ob1.expanded = true;
                        ob1.SC_SYSTEM_MODULE_ID = (dr1["SC_SYSTEM_MODULE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr1["SC_SYSTEM_MODULE_ID"]);
                        ob1.MENU_NAME_EN = (dr1["MENU_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["MENU_NAME_EN"]);
                        ob1.MENU_NAME_BN = (dr1["MENU_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["MENU_NAME_BN"]);
                        ob1.MENU_DESC = (dr1["MENU_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["MENU_DESC"]);
                        ob1.MENU_URL = (dr1["MENU_URL"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["MENU_URL"]);
                        ob1.MENU_ORDER = (dr1["MENU_ORDER"] == DBNull.Value) ? 0 : Convert.ToInt64(dr1["MENU_ORDER"]);
                        ob1.MENU_LEVEL = (dr1["MENU_LEVEL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr1["MENU_LEVEL"]);
                        ob1.IS_OPEN_NEW_WINDOW = (dr1["IS_OPEN_NEW_WINDOW"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["IS_OPEN_NEW_WINDOW"]);
                        ob1.IS_LEAF = (dr1["IS_LEAF"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["IS_LEAF"]);
                        ob1.IS_REPORT = (dr1["IS_REPORT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["IS_REPORT"]);
                        ob1.SC_SECURITY_LEVEL_ID = (dr1["SC_SECURITY_LEVEL_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr1["SC_SECURITY_LEVEL_ID"]);
                        ob1.ICON_PATH = (dr1["ICON_PATH"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["ICON_PATH"]);
                        ob1.IS_ACTIVE = (dr1["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["IS_ACTIVE"]);
                        ob1.id = ob1.SC_MENU_ID;
                        //ob1.SC_ROLE_ID = (dr1["SC_ROLE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr1["SC_ROLE_ID"]);
                        //string a = (dr1["ROLE_MENU_IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["ROLE_MENU_IS_ACTIVE"]);
                        ob1.IsChecked = (Convert.ToString(dr1["ROLE_MENU_IS_ACTIVE"]) == "Y") ? true : false;


                        if (ob1.IS_LEAF == "Y")
                            ob1.spriteCssClass = "html";
                        else
                            ob1.spriteCssClass = "folder";


                        var ds2 = db.ExecuteStoredProcedure(new List<CommandParameter>()
                                {   
                                    new CommandParameter() {ParameterName = "pPARENT_ID", Value = ob1.SC_MENU_ID},
                                    new CommandParameter() {ParameterName = "pOption", Value = 3004},
                                    new CommandParameter() {ParameterName = "pSC_ROLE_ID", Value =pSC_ROLE_ID}
                                }, sp);
                        foreach (DataRow dr2 in ds2.Tables[0].Rows)
                        {
                            ScMenuModel2 ob2 = new ScMenuModel2();

                            ob2.SC_MENU_ID = (dr2["SC_MENU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr2["SC_MENU_ID"]);
                            ob2.PARENT_ID = (dr2["PARENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr2["PARENT_ID"]);
                            ob2.PARENT_NAME = (dr2["PARENT_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr2["PARENT_NAME"]);
                            ob2.text = (dr2["MENU_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr2["MENU_NAME_EN"]);
                            ob2.expanded = true;
                            ob2.SC_SYSTEM_MODULE_ID = (dr2["SC_SYSTEM_MODULE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr2["SC_SYSTEM_MODULE_ID"]);
                            ob2.MENU_NAME_EN = (dr2["MENU_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr2["MENU_NAME_EN"]);
                            ob2.MENU_NAME_BN = (dr2["MENU_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr2["MENU_NAME_BN"]);
                            ob2.MENU_DESC = (dr2["MENU_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr2["MENU_DESC"]);
                            ob2.MENU_URL = (dr2["MENU_URL"] == DBNull.Value) ? string.Empty : Convert.ToString(dr2["MENU_URL"]);
                            ob2.MENU_ORDER = (dr2["MENU_ORDER"] == DBNull.Value) ? 0 : Convert.ToInt64(dr2["MENU_ORDER"]);
                            ob2.MENU_LEVEL = (dr2["MENU_LEVEL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr2["MENU_LEVEL"]);
                            ob2.IS_OPEN_NEW_WINDOW = (dr2["IS_OPEN_NEW_WINDOW"] == DBNull.Value) ? string.Empty : Convert.ToString(dr2["IS_OPEN_NEW_WINDOW"]);
                            ob2.IS_LEAF = (dr2["IS_LEAF"] == DBNull.Value) ? string.Empty : Convert.ToString(dr2["IS_LEAF"]);
                            ob2.IS_REPORT = (dr2["IS_REPORT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr2["IS_REPORT"]);
                            ob2.SC_SECURITY_LEVEL_ID = (dr2["SC_SECURITY_LEVEL_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr2["SC_SECURITY_LEVEL_ID"]);
                            ob2.ICON_PATH = (dr2["ICON_PATH"] == DBNull.Value) ? string.Empty : Convert.ToString(dr2["ICON_PATH"]);
                            ob2.IS_ACTIVE = (dr2["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr2["IS_ACTIVE"]);
                            ob2.id = ob2.SC_MENU_ID;
                            //ob2.SC_ROLE_ID = (dr2["SC_ROLE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr2["SC_ROLE_ID"]);
                            ob2.IsChecked = (Convert.ToString(dr2["ROLE_MENU_IS_ACTIVE"]) == "Y") ? true : false;

                            if (ob2.IS_LEAF == "Y")
                                ob2.spriteCssClass = "html";
                            else
                                ob2.spriteCssClass = "folder";

                            var ds3 = db.ExecuteStoredProcedure(new List<CommandParameter>()
                                {   
                                    new CommandParameter() {ParameterName = "pPARENT_ID", Value = ob2.SC_MENU_ID},
                                    new CommandParameter() {ParameterName = "pOption", Value = 3004},
                                    new CommandParameter() {ParameterName = "pSC_ROLE_ID", Value =pSC_ROLE_ID}
                                }, sp);
                            foreach (DataRow dr3 in ds3.Tables[0].Rows)
                            {
                                ScMenuModel3 ob3 = new ScMenuModel3();

                                ob3.SC_MENU_ID = (dr3["SC_MENU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr3["SC_MENU_ID"]);
                                ob3.PARENT_ID = (dr3["PARENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr3["PARENT_ID"]);
                                ob3.PARENT_NAME = (dr3["PARENT_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr3["PARENT_NAME"]);
                                ob3.text = (dr3["MENU_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr3["MENU_NAME_EN"]);
                                ob3.expanded = true;
                                ob3.SC_SYSTEM_MODULE_ID = (dr3["SC_SYSTEM_MODULE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr3["SC_SYSTEM_MODULE_ID"]);
                                ob3.MENU_NAME_EN = (dr3["MENU_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr3["MENU_NAME_EN"]);
                                ob3.MENU_NAME_BN = (dr3["MENU_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr3["MENU_NAME_BN"]);
                                ob3.MENU_DESC = (dr3["MENU_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr3["MENU_DESC"]);
                                ob3.MENU_URL = (dr3["MENU_URL"] == DBNull.Value) ? string.Empty : Convert.ToString(dr3["MENU_URL"]);
                                ob3.MENU_ORDER = (dr3["MENU_ORDER"] == DBNull.Value) ? 0 : Convert.ToInt64(dr3["MENU_ORDER"]);
                                ob3.MENU_LEVEL = (dr3["MENU_LEVEL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr3["MENU_LEVEL"]);
                                ob3.IS_OPEN_NEW_WINDOW = (dr3["IS_OPEN_NEW_WINDOW"] == DBNull.Value) ? string.Empty : Convert.ToString(dr3["IS_OPEN_NEW_WINDOW"]);
                                ob3.IS_LEAF = (dr3["IS_LEAF"] == DBNull.Value) ? string.Empty : Convert.ToString(dr3["IS_LEAF"]);
                                ob3.IS_REPORT = (dr3["IS_REPORT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr3["IS_REPORT"]);
                                ob3.SC_SECURITY_LEVEL_ID = (dr3["SC_SECURITY_LEVEL_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr3["SC_SECURITY_LEVEL_ID"]);
                                ob3.ICON_PATH = (dr3["ICON_PATH"] == DBNull.Value) ? string.Empty : Convert.ToString(dr3["ICON_PATH"]);
                                ob3.IS_ACTIVE = (dr3["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr3["IS_ACTIVE"]);
                                ob3.id = ob3.SC_MENU_ID;
                                //ob3.SC_ROLE_ID = (dr3["SC_ROLE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr3["SC_ROLE_ID"]);
                                ob3.IsChecked = (Convert.ToString(dr3["ROLE_MENU_IS_ACTIVE"]) == "Y") ? true : false;

                                if (ob3.IS_LEAF == "Y")
                                    ob3.spriteCssClass = "html";
                                else
                                    ob3.spriteCssClass = "folder";


                                var ds4 = db.ExecuteStoredProcedure(new List<CommandParameter>()
                                    {   
                                        new CommandParameter() {ParameterName = "pPARENT_ID", Value = ob3.SC_MENU_ID},
                                        new CommandParameter() {ParameterName = "pOption", Value = 3004},
                                        new CommandParameter() {ParameterName = "pSC_ROLE_ID", Value =pSC_ROLE_ID}
                                    }, sp);
                                foreach (DataRow dr4 in ds4.Tables[0].Rows)
                                {
                                    ScMenuModel4 ob4 = new ScMenuModel4();

                                    ob4.SC_MENU_ID = (dr4["SC_MENU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr4["SC_MENU_ID"]);
                                    ob4.PARENT_ID = (dr4["PARENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr4["PARENT_ID"]);
                                    ob4.PARENT_NAME = (dr4["PARENT_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr4["PARENT_NAME"]);
                                    ob4.text = (dr4["MENU_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr4["MENU_NAME_EN"]);
                                    ob4.expanded = true;
                                    ob4.SC_SYSTEM_MODULE_ID = (dr4["SC_SYSTEM_MODULE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr4["SC_SYSTEM_MODULE_ID"]);
                                    ob4.MENU_NAME_EN = (dr4["MENU_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr4["MENU_NAME_EN"]);
                                    ob4.MENU_NAME_BN = (dr4["MENU_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr4["MENU_NAME_BN"]);
                                    ob4.MENU_DESC = (dr4["MENU_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr4["MENU_DESC"]);
                                    ob4.MENU_URL = (dr4["MENU_URL"] == DBNull.Value) ? string.Empty : Convert.ToString(dr4["MENU_URL"]);
                                    ob4.MENU_ORDER = (dr4["MENU_ORDER"] == DBNull.Value) ? 0 : Convert.ToInt64(dr4["MENU_ORDER"]);
                                    ob4.MENU_LEVEL = (dr4["MENU_LEVEL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr4["MENU_LEVEL"]);
                                    ob4.IS_OPEN_NEW_WINDOW = (dr4["IS_OPEN_NEW_WINDOW"] == DBNull.Value) ? string.Empty : Convert.ToString(dr4["IS_OPEN_NEW_WINDOW"]);
                                    ob4.IS_LEAF = (dr4["IS_LEAF"] == DBNull.Value) ? string.Empty : Convert.ToString(dr4["IS_LEAF"]);
                                    ob4.IS_REPORT = (dr4["IS_REPORT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr4["IS_REPORT"]);
                                    ob4.SC_SECURITY_LEVEL_ID = (dr4["SC_SECURITY_LEVEL_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr4["SC_SECURITY_LEVEL_ID"]);
                                    ob4.ICON_PATH = (dr4["ICON_PATH"] == DBNull.Value) ? string.Empty : Convert.ToString(dr4["ICON_PATH"]);
                                    ob4.IS_ACTIVE = (dr4["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr4["IS_ACTIVE"]);
                                    ob4.id = ob4.SC_MENU_ID;
                                    //ob4.SC_ROLE_ID = (dr4["SC_ROLE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr4["SC_ROLE_ID"]);
                                    ob4.IsChecked = (Convert.ToString(dr4["ROLE_MENU_IS_ACTIVE"]) == "Y") ? true : false;

                                    if (ob4.IS_LEAF == "Y")
                                        ob4.spriteCssClass = "html";
                                    else
                                        ob4.spriteCssClass = "folder";


                                    var ds5 = db.ExecuteStoredProcedure(new List<CommandParameter>()
                                            {   
                                                new CommandParameter() {ParameterName = "pPARENT_ID", Value = ob4.SC_MENU_ID},
                                                new CommandParameter() {ParameterName = "pOption", Value = 3004},
                                                new CommandParameter() {ParameterName = "pSC_ROLE_ID", Value =pSC_ROLE_ID}
                                            }, sp);
                                    foreach (DataRow dr5 in ds5.Tables[0].Rows)
                                    {
                                        ScMenuModel5 ob5 = new ScMenuModel5();
                                        ob5.SC_MENU_ID = (dr5["SC_MENU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr5["SC_MENU_ID"]);
                                        ob5.PARENT_ID = (dr5["PARENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr5["PARENT_ID"]);
                                        ob5.PARENT_NAME = (dr5["PARENT_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr5["PARENT_NAME"]);
                                        ob5.text = (dr5["MENU_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr5["MENU_NAME_EN"]);
                                        ob5.expanded = true;
                                        ob5.SC_SYSTEM_MODULE_ID = (dr5["SC_SYSTEM_MODULE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr5["SC_SYSTEM_MODULE_ID"]);
                                        ob5.MENU_NAME_EN = (dr5["MENU_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr5["MENU_NAME_EN"]);
                                        ob5.MENU_NAME_BN = (dr5["MENU_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr5["MENU_NAME_BN"]);
                                        ob5.MENU_DESC = (dr5["MENU_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr5["MENU_DESC"]);
                                        ob5.MENU_URL = (dr5["MENU_URL"] == DBNull.Value) ? string.Empty : Convert.ToString(dr5["MENU_URL"]);
                                        ob5.MENU_ORDER = (dr5["MENU_ORDER"] == DBNull.Value) ? 0 : Convert.ToInt64(dr5["MENU_ORDER"]);
                                        ob5.MENU_LEVEL = (dr5["MENU_LEVEL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr5["MENU_LEVEL"]);
                                        ob5.IS_OPEN_NEW_WINDOW = (dr5["IS_OPEN_NEW_WINDOW"] == DBNull.Value) ? string.Empty : Convert.ToString(dr5["IS_OPEN_NEW_WINDOW"]);
                                        ob5.IS_LEAF = (dr5["IS_LEAF"] == DBNull.Value) ? string.Empty : Convert.ToString(dr5["IS_LEAF"]);
                                        ob5.IS_REPORT = (dr5["IS_REPORT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr5["IS_REPORT"]);
                                        ob5.SC_SECURITY_LEVEL_ID = (dr5["SC_SECURITY_LEVEL_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr5["SC_SECURITY_LEVEL_ID"]);
                                        ob5.ICON_PATH = (dr5["ICON_PATH"] == DBNull.Value) ? string.Empty : Convert.ToString(dr5["ICON_PATH"]);
                                        ob5.IS_ACTIVE = (dr5["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr5["IS_ACTIVE"]);
                                        ob5.id = ob5.SC_MENU_ID;
                                        //ob5.SC_ROLE_ID = (dr5["SC_ROLE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr5["SC_ROLE_ID"]);
                                        ob5.IsChecked = (Convert.ToString(dr5["ROLE_MENU_IS_ACTIVE"]) == "Y") ? true : false;

                                        if (ob5.IS_LEAF == "Y")
                                            ob5.spriteCssClass = "html";
                                        else
                                            ob5.spriteCssClass = "folder";
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



        public List<ScMenuModel> checkMenuPermission(Int64 pSC_USER_ID, string pCONTROLLER_NAME, string pMENU_URL)
        {
            string sp = "pkg_menu.sc_menu_select";

            try
            {

                var obList = new List<ScMenuModel>();

                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
                    new CommandParameter() {ParameterName = "pOption", Value = 3007},
                    new CommandParameter() {ParameterName = "pCONTROLLER_NAME", Value = pCONTROLLER_NAME},
                    new CommandParameter() {ParameterName = "pMENU_URL", Value = pMENU_URL},
                    new CommandParameter() {ParameterName = "pSC_USER_ID", Value = pSC_USER_ID},
                }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ScMenuModel ob = new ScMenuModel();

                    ob.SC_MENU_ID = (dr["SC_MENU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SC_MENU_ID"]);
                    ob.PARENT_ID = (dr["PARENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PARENT_ID"]);
                    //ob.PARENT_NAME = (dr["PARENT_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PARENT_NAME"]);
                    ob.text = (dr["MENU_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MENU_NAME_EN"]);
                    ob.SC_SYSTEM_MODULE_ID = (dr["SC_SYSTEM_MODULE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SC_SYSTEM_MODULE_ID"]);
                    ob.MENU_NAME_EN = (dr["MENU_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MENU_NAME_EN"]);
                    ob.MENU_NAME_BN = (dr["MENU_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MENU_NAME_BN"]);
                    ob.MENU_DESC = (dr["MENU_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MENU_DESC"]);
                    ob.MENU_URL = (dr["MENU_URL"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MENU_URL"]);
                    ob.MENU_ORDER = (dr["MENU_ORDER"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MENU_ORDER"]);
                    ob.MENU_LEVEL = (dr["MENU_LEVEL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MENU_LEVEL"]);
                    ob.IS_OPEN_NEW_WINDOW = (dr["IS_OPEN_NEW_WINDOW"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_OPEN_NEW_WINDOW"]);
                    ob.IS_LEAF = (dr["IS_LEAF"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_LEAF"]);
                    ob.IS_REPORT = (dr["IS_REPORT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_REPORT"]);
                    ob.SC_SECURITY_LEVEL_ID = (dr["SC_SECURITY_LEVEL_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SC_SECURITY_LEVEL_ID"]);
                    ob.ICON_PATH = (dr["ICON_PATH"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ICON_PATH"]);
                    ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);
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




    public class ScMenuModel1
    {
        public Int64 SC_MENU_ID { get; set; }
        public Int64 PARENT_ID { get; set; }
        public Int64 SC_SYSTEM_MODULE_ID { get; set; }
        public string MENU_NAME_EN { get; set; }
        public string MENU_NAME_BN { get; set; }
        public string MENU_DESC { get; set; }
        public string MENU_URL { get; set; }
        public Int64 MENU_ORDER { get; set; }
        public Int64 MENU_LEVEL { get; set; }
        public string IS_OPEN_NEW_WINDOW { get; set; }
        public string IS_LEAF { get; set; }
        public string IS_REPORT { get; set; }
        public Int64 SC_SECURITY_LEVEL_ID { get; set; }
        public string ICON_PATH { get; set; }
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
        public Int64 id { get; set; }
        //public Int64 SC_ROLE_ID { get; set; }
        public bool IsChecked { get; set; }


        private List<ScMenuModel2> _subScMenuModel = null;
        public List<ScMenuModel2> items
        {
            get
            {
                if (_subScMenuModel == null)
                {
                    _subScMenuModel = new List<ScMenuModel2>();
                }
                return _subScMenuModel;
            }
            set
            {
                _subScMenuModel = value;
            }
        }

    }

    public class ScMenuModel2
    {
        public Int64 SC_MENU_ID { get; set; }
        public Int64 PARENT_ID { get; set; }
        public Int64 SC_SYSTEM_MODULE_ID { get; set; }
        public string MENU_NAME_EN { get; set; }
        public string MENU_NAME_BN { get; set; }
        public string MENU_DESC { get; set; }
        public string MENU_URL { get; set; }
        public Int64 MENU_ORDER { get; set; }
        public Int64 MENU_LEVEL { get; set; }
        public string IS_OPEN_NEW_WINDOW { get; set; }
        public string IS_LEAF { get; set; }
        public string IS_REPORT { get; set; }
        public Int64 SC_SECURITY_LEVEL_ID { get; set; }
        public string ICON_PATH { get; set; }
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
        public Int64 id { get; set; }
        //public Int64 SC_ROLE_ID { get; set; }
        public bool IsChecked { get; set; }


        private List<ScMenuModel3> _subScMenuModel = null;
        public List<ScMenuModel3> items
        {
            get
            {
                if (_subScMenuModel == null)
                {
                    _subScMenuModel = new List<ScMenuModel3>();
                }
                return _subScMenuModel;
            }
            set
            {
                _subScMenuModel = value;
            }
        }

    }

    public class ScMenuModel3
    {
        public Int64 SC_MENU_ID { get; set; }
        public Int64 PARENT_ID { get; set; }
        public Int64 SC_SYSTEM_MODULE_ID { get; set; }
        public string MENU_NAME_EN { get; set; }
        public string MENU_NAME_BN { get; set; }
        public string MENU_DESC { get; set; }
        public string MENU_URL { get; set; }
        public Int64 MENU_ORDER { get; set; }
        public Int64 MENU_LEVEL { get; set; }
        public string IS_OPEN_NEW_WINDOW { get; set; }
        public string IS_LEAF { get; set; }
        public string IS_REPORT { get; set; }
        public Int64 SC_SECURITY_LEVEL_ID { get; set; }
        public string ICON_PATH { get; set; }
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
        public Int64 id { get; set; }
        //public Int64 SC_ROLE_ID { get; set; }
        public bool IsChecked { get; set; }


        private List<ScMenuModel4> _subScMenuModel = null;
        public List<ScMenuModel4> items
        {
            get
            {
                if (_subScMenuModel == null)
                {
                    _subScMenuModel = new List<ScMenuModel4>();
                }
                return _subScMenuModel;
            }
            set
            {
                _subScMenuModel = value;
            }
        }

    }

    public class ScMenuModel4
    {
        public Int64 SC_MENU_ID { get; set; }
        public Int64 PARENT_ID { get; set; }
        public Int64 SC_SYSTEM_MODULE_ID { get; set; }
        public string MENU_NAME_EN { get; set; }
        public string MENU_NAME_BN { get; set; }
        public string MENU_DESC { get; set; }
        public string MENU_URL { get; set; }
        public Int64 MENU_ORDER { get; set; }
        public Int64 MENU_LEVEL { get; set; }
        public string IS_OPEN_NEW_WINDOW { get; set; }
        public string IS_LEAF { get; set; }
        public string IS_REPORT { get; set; }
        public Int64 SC_SECURITY_LEVEL_ID { get; set; }
        public string ICON_PATH { get; set; }
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
        public Int64 id { get; set; }
        //public Int64 SC_ROLE_ID { get; set; }
        public bool IsChecked { get; set; }


        private List<ScMenuModel5> _subScMenuModel = null;
        public List<ScMenuModel5> items
        {
            get
            {
                if (_subScMenuModel == null)
                {
                    _subScMenuModel = new List<ScMenuModel5>();
                }
                return _subScMenuModel;
            }
            set
            {
                _subScMenuModel = value;
            }
        }

    }

    public class ScMenuModel5
    {
        public Int64 SC_MENU_ID { get; set; }
        public Int64 PARENT_ID { get; set; }
        public Int64 SC_SYSTEM_MODULE_ID { get; set; }
        public string MENU_NAME_EN { get; set; }
        public string MENU_NAME_BN { get; set; }
        public string MENU_DESC { get; set; }
        public string MENU_URL { get; set; }
        public Int64 MENU_ORDER { get; set; }
        public Int64 MENU_LEVEL { get; set; }
        public string IS_OPEN_NEW_WINDOW { get; set; }
        public string IS_LEAF { get; set; }
        public string IS_REPORT { get; set; }
        public Int64 SC_SECURITY_LEVEL_ID { get; set; }
        public string ICON_PATH { get; set; }
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
        public Int64 id { get; set; }
        //public Int64 SC_ROLE_ID { get; set; }
        public bool IsChecked { get; set; }


        private List<ScMenuModel6> _subScMenuModel = null;
        public List<ScMenuModel6> items
        {
            get
            {
                if (_subScMenuModel == null)
                {
                    _subScMenuModel = new List<ScMenuModel6>();
                }
                return _subScMenuModel;
            }
            set
            {
                _subScMenuModel = value;
            }
        }

    }

    public class ScMenuModel6
    {
        public Int64 SC_MENU_ID { get; set; }
        public Int64 PARENT_ID { get; set; }
        public Int64 SC_SYSTEM_MODULE_ID { get; set; }
        public string MENU_NAME_EN { get; set; }
        public string MENU_NAME_BN { get; set; }
        public string MENU_DESC { get; set; }
        public string MENU_URL { get; set; }
        public Int64 MENU_ORDER { get; set; }
        public Int64 MENU_LEVEL { get; set; }
        public string IS_OPEN_NEW_WINDOW { get; set; }
        public string IS_LEAF { get; set; }
        public string IS_REPORT { get; set; }
        public Int64 SC_SECURITY_LEVEL_ID { get; set; }
        public string ICON_PATH { get; set; }
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
        public Int64 id { get; set; }
        //public Int64 SC_ROLE_ID { get; set; }
        public bool IsChecked { get; set; }

        //private List<ScMenuModel3> _subScMenuModel = null;
        //public List<ScMenuModel3> items
        //{
        //    get
        //    {
        //        if (_subScMenuModel == null)
        //        {
        //            _subScMenuModel = new List<ScMenuModel3>();
        //        }
        //        return _subScMenuModel;
        //    }
        //    set
        //    {
        //        _subScMenuModel = value;
        //    }
        //}

    }


}
