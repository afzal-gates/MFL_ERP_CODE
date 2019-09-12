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
    public class INV_ITEM_CATModel
    {
        public Int64 INV_ITEM_CAT_ID { get; set; }
        public Int64? PARENT_ID { get; set; }
        public string ITEM_CAT_PREFIX { get; set; }
        public string ITEM_CAT_CODE { get; set; }


        [Required(ErrorMessage = "Please insert category name [En]")]
        public string ITEM_CAT_NAME_EN { get; set; }
        [Required(ErrorMessage = "Please insert category name [Bn]")]
        public string ITEM_CAT_NAME_BN { get; set; }
        [Required(ErrorMessage = "Please insert category short name")]
        public string ITEM_CAT_SNAME { get; set; }
        public Int64 ITEM_CAT_LEVEL { get; set; }
        public Int64 ITEM_CAT_ORDER { get; set; }
        public string IS_LEAF { get; set; }
        public string IS_ACTIVE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }
        public Int64? INV_ITEM_CORE_CAT_ID { get; set; }
        public Int64? SC_USER_ID { get; set; }


        public string XML_STR { get; set; }
        public string PARENT_NAME { get; set; }
        public string text { get; set; }
        public string spriteCssClass { get; set; }
        public bool expanded { get; set; }
        public bool selected { get; set; }
        public string INV_CAT_IS_ACTIVE { get; set; }
        public bool IsChecked { get; set; }

        public int TranMode { get; set; }
        public Int64 TOT_OQ { get; set; }
        public String IS_M_P { get; set; }

        private List<INV_ITEM_CATModel> _SubDeptModel = null;

        private List<MC_STYLE_D_BOMModel> _inv_items = null;
        private List<MC_STYLE_BLK_BOMModel> _inv_items_blk = null;
        private List<MC_BUYER_BOMModel> _inv_itm_byr_bom = null;

        public List<INV_ITEM_CATModel> items
        {
            get
            {
                if (_SubDeptModel == null)
                {
                    _SubDeptModel = new List<INV_ITEM_CATModel>();
                }
                return _SubDeptModel;
            }
            set
            {
                _SubDeptModel = value;
            }
        }

        public List<MC_STYLE_D_BOMModel> inv_items
        {
            get
            {
                if (_inv_items == null)
                {
                    _inv_items = new List<MC_STYLE_D_BOMModel>();
                }
                return _inv_items;
            }
            set
            {
                _inv_items = value;
            }
        }

        public List<MC_BUYER_BOMModel> inv_itm_byr_bom
        {
            get
            {
                if (_inv_itm_byr_bom == null)
                {
                    _inv_itm_byr_bom = new List<MC_BUYER_BOMModel>();
                }
                return _inv_itm_byr_bom;
            }
            set
            {
                _inv_itm_byr_bom = value;
            }
        }


        public List<MC_STYLE_BLK_BOMModel> inv_items_blk
        {
            get
            {
                if (_inv_items_blk == null)
                {
                    _inv_items_blk = new List<MC_STYLE_BLK_BOMModel>();
                }
                return _inv_items_blk;
            }
            set
            {
                _inv_items_blk = value;
            }
        }



        public string Save()
        {
            const string sp = "pkg_inventory.inv_item_cat_insert";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pINV_ITEM_CAT_ID", Value = ob.INV_ITEM_CAT_ID},
                     new CommandParameter() {ParameterName = "pPARENT_ID", Value = ob.PARENT_ID},
                     new CommandParameter() {ParameterName = "pITEM_CAT_PREFIX", Value = ob.ITEM_CAT_PREFIX},
                     new CommandParameter() {ParameterName = "pITEM_CAT_CODE", Value = ob.ITEM_CAT_CODE},
                     new CommandParameter() {ParameterName = "pITEM_CAT_NAME_EN", Value = ob.ITEM_CAT_NAME_EN},
                     new CommandParameter() {ParameterName = "pITEM_CAT_NAME_BN", Value = ob.ITEM_CAT_NAME_BN},
                     new CommandParameter() {ParameterName = "pITEM_CAT_SNAME", Value = ob.ITEM_CAT_SNAME},
                     new CommandParameter() {ParameterName = "pITEM_CAT_LEVEL", Value = ob.ITEM_CAT_LEVEL},
                     new CommandParameter() {ParameterName = "pITEM_CAT_ORDER", Value = ob.ITEM_CAT_ORDER},
                     new CommandParameter() {ParameterName = "pIS_LEAF", Value = ob.IS_LEAF},
                     new CommandParameter() {ParameterName = "pIS_ACTIVE", Value = ob.IS_ACTIVE},  
                     new CommandParameter() {ParameterName = "pXML_STR", Value = ob.XML_STR},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]}, 
                     new CommandParameter() {ParameterName = "pOption", Value =1000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);

                foreach (DataRow dr in ds.Tables["OUTPARAM"].Rows)
                {
                    //jsonString += dr["KEY"].ToString() + ":"+ dr["VALUE"].ToString()+",";
                    jsonStr += Convert.ToString('"') + dr["KEY"].ToString() + Convert.ToString('"') + ":" + Convert.ToString('"') + (dr["VALUE"].ToString().Replace(@"""", @"\""")) + Convert.ToString('"');
                    if (i < ds.Tables["OUTPARAM"].Rows.Count)
                    {
                        jsonStr += ",";
                    }
                    else
                    {
                        jsonStr += "}";
                    }
                    i++;
                }
                return jsonStr;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string Update()
        {
            const string sp = "pkg_inventory.inv_item_cat_update";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pINV_ITEM_CAT_ID", Value = ob.INV_ITEM_CAT_ID},
                     new CommandParameter() {ParameterName = "pPARENT_ID", Value = ob.PARENT_ID},
                     new CommandParameter() {ParameterName = "pITEM_CAT_PREFIX", Value = ob.ITEM_CAT_PREFIX},
                     new CommandParameter() {ParameterName = "pITEM_CAT_CODE", Value = ob.ITEM_CAT_CODE},
                     new CommandParameter() {ParameterName = "pITEM_CAT_NAME_EN", Value = ob.ITEM_CAT_NAME_EN},
                     new CommandParameter() {ParameterName = "pITEM_CAT_NAME_BN", Value = ob.ITEM_CAT_NAME_BN},
                     new CommandParameter() {ParameterName = "pITEM_CAT_SNAME", Value = ob.ITEM_CAT_SNAME},
                     new CommandParameter() {ParameterName = "pITEM_CAT_LEVEL", Value = ob.ITEM_CAT_LEVEL},
                     new CommandParameter() {ParameterName = "pITEM_CAT_ORDER", Value = ob.ITEM_CAT_ORDER},
                     new CommandParameter() {ParameterName = "pIS_LEAF", Value = ob.IS_LEAF},
                     new CommandParameter() {ParameterName = "pIS_ACTIVE", Value = ob.IS_ACTIVE},
                     new CommandParameter() {ParameterName = "pXML_STR", Value = ob.XML_STR},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = ob.CREATED_BY},              
                     new CommandParameter() {ParameterName = "pOption", Value =2000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);

                foreach (DataRow dr in ds.Tables["OUTPARAM"].Rows)
                {
                    jsonStr += Convert.ToString('"') + dr["KEY"].ToString() + Convert.ToString('"') + ":" + Convert.ToString('"') + (dr["VALUE"].ToString().Replace(@"""", @"\""")) + Convert.ToString('"');
                    if (i < ds.Tables["OUTPARAM"].Rows.Count)
                    {
                        jsonStr += ",";
                    }
                    else
                    {
                        jsonStr += "}";
                    }
                    i++;
                }
                return jsonStr;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<INV_ITEM_CATModel> ItemCategTreeList(Int32? pSC_USER_ID = null)
        {
            string sp = "pkg_inventory.inv_item_cat_select";
            try
            {
                //var us = HttpContext.Current.Session["multiScUserId"].ToString();

                var obList = new List<INV_ITEM_CATModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pSC_USER_ID", Value = pSC_USER_ID},
                     new CommandParameter() {ParameterName = "pOption", Value = 3002},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                }, sp);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    INV_ITEM_CATModel root = new INV_ITEM_CATModel();
                    root.INV_ITEM_CAT_ID = (ds.Tables[0].Rows[i]["INV_ITEM_CAT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(ds.Tables[0].Rows[i]["INV_ITEM_CAT_ID"]);
                    root.PARENT_ID = (ds.Tables[0].Rows[i]["PARENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(ds.Tables[0].Rows[i]["PARENT_ID"]);
                    root.ITEM_CAT_PREFIX = (ds.Tables[0].Rows[i]["ITEM_CAT_PREFIX"] == DBNull.Value) ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["ITEM_CAT_PREFIX"]);
                    root.ITEM_CAT_CODE = (ds.Tables[0].Rows[i]["ITEM_CAT_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["ITEM_CAT_CODE"]);
                    root.ITEM_CAT_NAME_EN = (ds.Tables[0].Rows[i]["ITEM_CAT_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["ITEM_CAT_NAME_EN"]);
                    root.ITEM_CAT_NAME_BN = (ds.Tables[0].Rows[i]["ITEM_CAT_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["ITEM_CAT_NAME_BN"]);
                    root.ITEM_CAT_SNAME = (ds.Tables[0].Rows[i]["ITEM_CAT_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["ITEM_CAT_SNAME"]);
                    root.ITEM_CAT_LEVEL = (ds.Tables[0].Rows[i]["ITEM_CAT_LEVEL"] == DBNull.Value) ? 0 : Convert.ToInt64(ds.Tables[0].Rows[i]["ITEM_CAT_LEVEL"]);
                    root.ITEM_CAT_ORDER = (ds.Tables[0].Rows[i]["ITEM_CAT_ORDER"] == DBNull.Value) ? 0 : Convert.ToInt64(ds.Tables[0].Rows[i]["ITEM_CAT_ORDER"]);
                    root.IS_LEAF = (ds.Tables[0].Rows[i]["IS_LEAF"] == DBNull.Value) ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["IS_LEAF"]);
                    root.IS_ACTIVE = (ds.Tables[0].Rows[i]["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["IS_ACTIVE"]);
                    root.INV_ITEM_CORE_CAT_ID = (ds.Tables[0].Rows[i]["INV_ITEM_CORE_CAT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(ds.Tables[0].Rows[i]["INV_ITEM_CORE_CAT_ID"]);

                    root.PARENT_NAME = (ds.Tables[0].Rows[i]["PARENT_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["PARENT_NAME"]);
                    root.text = (ds.Tables[0].Rows[i]["ITEM_CAT_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["ITEM_CAT_NAME_EN"]);
                    root.expanded = true;
                    root.IsChecked = (Convert.ToString(ds.Tables[0].Rows[i]["INV_CAT_IS_ACTIVE"]) == "Y") ? true : false;


                    if (root.IS_LEAF == "Y")
                        root.spriteCssClass = "html";
                    else
                        root.spriteCssClass = "folder";

                    CreateNode(root, pSC_USER_ID);
                    obList.Add(root);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void CreateNode(INV_ITEM_CATModel node, Int32? pSC_USER_ID = null)
        {
            string sp = "pkg_inventory.inv_item_cat_select";
            var obList = new List<INV_ITEM_CATModel>();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                    new CommandParameter() {ParameterName = "pPARENT_ID", Value = node.INV_ITEM_CAT_ID},
                    new CommandParameter() {ParameterName = "pITEM_CAT_LEVEL", Value = node.ITEM_CAT_LEVEL},
                    new CommandParameter() {ParameterName = "pOption", Value = 3003},
                    new CommandParameter() {ParameterName = "pSC_USER_ID", Value = pSC_USER_ID},
                    new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);

            if (ds.Tables[0].Rows.Count == 0)
            {
                return;
            }
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                INV_ITEM_CATModel tnode = new INV_ITEM_CATModel();
                tnode.INV_ITEM_CAT_ID = (ds.Tables[0].Rows[i]["INV_ITEM_CAT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(ds.Tables[0].Rows[i]["INV_ITEM_CAT_ID"]);
                tnode.PARENT_ID = (ds.Tables[0].Rows[i]["PARENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(ds.Tables[0].Rows[i]["PARENT_ID"]);
                tnode.ITEM_CAT_PREFIX = (ds.Tables[0].Rows[i]["ITEM_CAT_PREFIX"] == DBNull.Value) ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["ITEM_CAT_PREFIX"]);
                tnode.ITEM_CAT_CODE = (ds.Tables[0].Rows[i]["ITEM_CAT_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["ITEM_CAT_CODE"]);
                tnode.ITEM_CAT_NAME_EN = (ds.Tables[0].Rows[i]["ITEM_CAT_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["ITEM_CAT_NAME_EN"]);
                tnode.ITEM_CAT_NAME_BN = (ds.Tables[0].Rows[i]["ITEM_CAT_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["ITEM_CAT_NAME_BN"]);
                tnode.ITEM_CAT_SNAME = (ds.Tables[0].Rows[i]["ITEM_CAT_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["ITEM_CAT_SNAME"]);
                tnode.ITEM_CAT_LEVEL = (ds.Tables[0].Rows[i]["ITEM_CAT_LEVEL"] == DBNull.Value) ? 0 : Convert.ToInt64(ds.Tables[0].Rows[i]["ITEM_CAT_LEVEL"]);
                tnode.ITEM_CAT_ORDER = (ds.Tables[0].Rows[i]["ITEM_CAT_ORDER"] == DBNull.Value) ? 0 : Convert.ToInt64(ds.Tables[0].Rows[i]["ITEM_CAT_ORDER"]);
                tnode.IS_LEAF = (ds.Tables[0].Rows[i]["IS_LEAF"] == DBNull.Value) ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["IS_LEAF"]);
                tnode.IS_ACTIVE = (ds.Tables[0].Rows[i]["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["IS_ACTIVE"]);
                tnode.INV_ITEM_CORE_CAT_ID = (ds.Tables[0].Rows[i]["INV_ITEM_CORE_CAT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(ds.Tables[0].Rows[i]["INV_ITEM_CORE_CAT_ID"]);

                tnode.PARENT_NAME = (ds.Tables[0].Rows[i]["PARENT_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["PARENT_NAME"]);
                tnode.text = (ds.Tables[0].Rows[i]["ITEM_CAT_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["ITEM_CAT_NAME_EN"]);
                tnode.expanded = false;
                tnode.IsChecked = (Convert.ToString(ds.Tables[0].Rows[i]["INV_CAT_IS_ACTIVE"]) == "Y") ? true : false;

                if (tnode.IS_LEAF == "Y")
                    tnode.spriteCssClass = "html";
                else
                    tnode.spriteCssClass = "folder";

                node.items.Add(tnode);
                CreateNode(tnode, pSC_USER_ID);
            }

        }

        public List<INV_ITEM_CATModel> SelectAll()
        {
            string sp = "pkg_inventory.inv_item_cat_select";
            try
            {
                var obList = new List<INV_ITEM_CATModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pINV_ITEM_CAT_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    INV_ITEM_CATModel ob = new INV_ITEM_CATModel();
                    ob.INV_ITEM_CAT_ID = (dr["INV_ITEM_CAT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_ITEM_CAT_ID"]);
                    ob.PARENT_ID = (dr["PARENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PARENT_ID"]);
                    ob.ITEM_CAT_PREFIX = (dr["ITEM_CAT_PREFIX"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_CAT_PREFIX"]);
                    ob.ITEM_CAT_CODE = (dr["ITEM_CAT_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_CAT_CODE"]);
                    ob.ITEM_CAT_NAME_EN = (dr["ITEM_CAT_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_CAT_NAME_EN"]);
                    ob.ITEM_CAT_NAME_BN = (dr["ITEM_CAT_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_CAT_NAME_BN"]);
                    ob.ITEM_CAT_SNAME = (dr["ITEM_CAT_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_CAT_SNAME"]);
                    ob.ITEM_CAT_LEVEL = (dr["ITEM_CAT_LEVEL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ITEM_CAT_LEVEL"]);
                    ob.ITEM_CAT_ORDER = (dr["ITEM_CAT_ORDER"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ITEM_CAT_ORDER"]);
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

        public INV_ITEM_CATModel Select(long ID)
        {
            string sp = "pkg_inventory.inv_item_cat_select";
            try
            {
                var ob = new INV_ITEM_CATModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pINV_ITEM_CAT_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.INV_ITEM_CAT_ID = (dr["INV_ITEM_CAT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_ITEM_CAT_ID"]);
                    ob.PARENT_ID = (dr["PARENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PARENT_ID"]);
                    ob.ITEM_CAT_PREFIX = (dr["ITEM_CAT_PREFIX"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_CAT_PREFIX"]);
                    ob.ITEM_CAT_CODE = (dr["ITEM_CAT_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_CAT_CODE"]);
                    ob.ITEM_CAT_NAME_EN = (dr["ITEM_CAT_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_CAT_NAME_EN"]);
                    ob.ITEM_CAT_NAME_BN = (dr["ITEM_CAT_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_CAT_NAME_BN"]);
                    ob.ITEM_CAT_SNAME = (dr["ITEM_CAT_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_CAT_SNAME"]);
                    ob.ITEM_CAT_LEVEL = (dr["ITEM_CAT_LEVEL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ITEM_CAT_LEVEL"]);
                    ob.ITEM_CAT_ORDER = (dr["ITEM_CAT_ORDER"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ITEM_CAT_ORDER"]);
                    ob.IS_LEAF = (dr["IS_LEAF"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_LEAF"]);
                    ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);

                }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public object ItemCategList()
        {
            string sp = "pkg_inventory.inv_item_cat_select";
            try
            {
                var obList = new List<INV_ITEM_CATModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pINV_ITEM_CAT_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3004},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    INV_ITEM_CATModel ob = new INV_ITEM_CATModel();
                    ob.INV_ITEM_CAT_ID = (dr["INV_ITEM_CAT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_ITEM_CAT_ID"]);
                    ob.PARENT_ID = (dr["PARENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PARENT_ID"]);
                    ob.ITEM_CAT_PREFIX = (dr["ITEM_CAT_PREFIX"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_CAT_PREFIX"]);
                    ob.ITEM_CAT_CODE = (dr["ITEM_CAT_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_CAT_CODE"]);
                    ob.ITEM_CAT_NAME_EN = (dr["ITEM_CAT_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_CAT_NAME_EN"]);
                    ob.ITEM_CAT_NAME_BN = (dr["ITEM_CAT_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_CAT_NAME_BN"]);
                    ob.ITEM_CAT_SNAME = (dr["ITEM_CAT_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_CAT_SNAME"]);
                    ob.ITEM_CAT_LEVEL = (dr["ITEM_CAT_LEVEL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ITEM_CAT_LEVEL"]);
                    ob.ITEM_CAT_ORDER = (dr["ITEM_CAT_ORDER"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ITEM_CAT_ORDER"]);
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


        public object ItemCategList4LastLevel(int? pPARENT_ID = null)
        {
            string sp = "pkg_inventory.inv_item_cat_select";
            try
            {
                var obList = new List<INV_ITEM_CATModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pPARENT_ID", Value = pPARENT_ID},
                     new CommandParameter() {ParameterName = "pOption", Value = 3009},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    INV_ITEM_CATModel ob = new INV_ITEM_CATModel();
                    ob.INV_ITEM_CAT_ID = (dr["INV_ITEM_CAT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_ITEM_CAT_ID"]);
                    ob.PARENT_ID = (dr["PARENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PARENT_ID"]);
                    ob.ITEM_CAT_PREFIX = (dr["ITEM_CAT_PREFIX"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_CAT_PREFIX"]);
                    ob.ITEM_CAT_CODE = (dr["ITEM_CAT_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_CAT_CODE"]);
                    ob.ITEM_CAT_NAME_EN = (dr["ITEM_CAT_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_CAT_NAME_EN"]);
                    ob.ITEM_CAT_NAME_BN = (dr["ITEM_CAT_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_CAT_NAME_BN"]);
                    ob.ITEM_CAT_SNAME = (dr["ITEM_CAT_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_CAT_SNAME"]);
                    ob.ITEM_CAT_LEVEL = (dr["ITEM_CAT_LEVEL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ITEM_CAT_LEVEL"]);
                    ob.ITEM_CAT_ORDER = (dr["ITEM_CAT_ORDER"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ITEM_CAT_ORDER"]);
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

        public List<INV_ITEM_CATModel> ScRoleMenuData(Int64 pSC_USER_ID)
        {
            string sp = "pkg_inventory.inv_item_cat_select";
            try
            {
                var obList = new List<INV_ITEM_CATModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     //new CommandParameter() {ParameterName = "pPARENT_ID", Value =0},
                     new CommandParameter() {ParameterName = "pSC_USER_ID", Value = pSC_USER_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3004},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                }, sp);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    INV_ITEM_CATModel root = new INV_ITEM_CATModel();
                    root.INV_ITEM_CAT_ID = (ds.Tables[0].Rows[i]["INV_ITEM_CAT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(ds.Tables[0].Rows[i]["INV_ITEM_CAT_ID"]);
                    root.PARENT_ID = (ds.Tables[0].Rows[i]["PARENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(ds.Tables[0].Rows[i]["PARENT_ID"]);
                    root.ITEM_CAT_PREFIX = (ds.Tables[0].Rows[i]["ITEM_CAT_PREFIX"] == DBNull.Value) ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["ITEM_CAT_PREFIX"]);
                    root.ITEM_CAT_CODE = (ds.Tables[0].Rows[i]["ITEM_CAT_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["ITEM_CAT_CODE"]);
                    root.ITEM_CAT_NAME_EN = (ds.Tables[0].Rows[i]["ITEM_CAT_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["ITEM_CAT_NAME_EN"]);
                    root.ITEM_CAT_NAME_BN = (ds.Tables[0].Rows[i]["ITEM_CAT_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["ITEM_CAT_NAME_BN"]);
                    root.ITEM_CAT_SNAME = (ds.Tables[0].Rows[i]["ITEM_CAT_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["ITEM_CAT_SNAME"]);
                    root.ITEM_CAT_LEVEL = (ds.Tables[0].Rows[i]["ITEM_CAT_LEVEL"] == DBNull.Value) ? 0 : Convert.ToInt64(ds.Tables[0].Rows[i]["ITEM_CAT_LEVEL"]);
                    root.ITEM_CAT_ORDER = (ds.Tables[0].Rows[i]["ITEM_CAT_ORDER"] == DBNull.Value) ? 0 : Convert.ToInt64(ds.Tables[0].Rows[i]["ITEM_CAT_ORDER"]);
                    root.IS_LEAF = (ds.Tables[0].Rows[i]["IS_LEAF"] == DBNull.Value) ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["IS_LEAF"]);
                    root.IS_ACTIVE = (ds.Tables[0].Rows[i]["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["IS_ACTIVE"]);

                    root.PARENT_NAME = (ds.Tables[0].Rows[i]["PARENT_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["PARENT_NAME"]);
                    root.text = (ds.Tables[0].Rows[i]["ITEM_CAT_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["ITEM_CAT_NAME_EN"]);
                    root.expanded = true;
                    root.IsChecked = (Convert.ToString(ds.Tables[0].Rows[i]["ROLE_MENU_IS_ACTIVE"]) == "Y") ? true : false;

                    if (root.IS_LEAF == "Y")
                        root.spriteCssClass = "html";
                    else
                        root.spriteCssClass = "folder";

                    CreateTreeNode(root, pSC_USER_ID);
                    obList.Add(root);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void CreateTreeNode(INV_ITEM_CATModel node, Int64 pSC_USER_ID)
        {
            string sp = "pkg_inventory.inv_item_cat_select";
            var obList = new List<INV_ITEM_CATModel>();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pPARENT_ID", Value = node.INV_ITEM_CAT_ID},
                     new CommandParameter() {ParameterName = "pSC_USER_ID", Value = pSC_USER_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3005},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);

            if (ds.Tables[0].Rows.Count == 0)
            {
                return;
            }
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                INV_ITEM_CATModel tnode = new INV_ITEM_CATModel();
                tnode.INV_ITEM_CAT_ID = (ds.Tables[0].Rows[i]["INV_ITEM_CAT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(ds.Tables[0].Rows[i]["INV_ITEM_CAT_ID"]);
                tnode.PARENT_ID = (ds.Tables[0].Rows[i]["PARENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(ds.Tables[0].Rows[i]["PARENT_ID"]);
                tnode.ITEM_CAT_PREFIX = (ds.Tables[0].Rows[i]["ITEM_CAT_PREFIX"] == DBNull.Value) ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["ITEM_CAT_PREFIX"]);
                tnode.ITEM_CAT_CODE = (ds.Tables[0].Rows[i]["ITEM_CAT_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["ITEM_CAT_CODE"]);
                tnode.ITEM_CAT_NAME_EN = (ds.Tables[0].Rows[i]["ITEM_CAT_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["ITEM_CAT_NAME_EN"]);
                tnode.ITEM_CAT_NAME_BN = (ds.Tables[0].Rows[i]["ITEM_CAT_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["ITEM_CAT_NAME_BN"]);
                tnode.ITEM_CAT_SNAME = (ds.Tables[0].Rows[i]["ITEM_CAT_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["ITEM_CAT_SNAME"]);
                tnode.ITEM_CAT_LEVEL = (ds.Tables[0].Rows[i]["ITEM_CAT_LEVEL"] == DBNull.Value) ? 0 : Convert.ToInt64(ds.Tables[0].Rows[i]["ITEM_CAT_LEVEL"]);
                tnode.ITEM_CAT_ORDER = (ds.Tables[0].Rows[i]["ITEM_CAT_ORDER"] == DBNull.Value) ? 0 : Convert.ToInt64(ds.Tables[0].Rows[i]["ITEM_CAT_ORDER"]);
                tnode.IS_LEAF = (ds.Tables[0].Rows[i]["IS_LEAF"] == DBNull.Value) ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["IS_LEAF"]);
                tnode.IS_ACTIVE = (ds.Tables[0].Rows[i]["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["IS_ACTIVE"]);

                tnode.PARENT_NAME = (ds.Tables[0].Rows[i]["PARENT_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["PARENT_NAME"]);
                tnode.text = (ds.Tables[0].Rows[i]["ITEM_CAT_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["ITEM_CAT_NAME_EN"]);
                tnode.expanded = true;
                tnode.IsChecked = (Convert.ToString(ds.Tables[0].Rows[i]["ROLE_MENU_IS_ACTIVE"]) == "Y") ? true : false;
                var x = Convert.ToString(ds.Tables[0].Rows[i]["ROLE_MENU_IS_ACTIVE"]);

                if (tnode.IS_LEAF == "Y")
                    tnode.spriteCssClass = "html";
                else
                    tnode.spriteCssClass = "folder";

                node.items.Add(tnode);
                CreateTreeNode(tnode, pSC_USER_ID);
            }

        }



        public List<INV_ITEM_CATModel> ItemCategListForDD(string ITEM_CAT_CODE)
        {
            string sp = "pkg_inventory.inv_item_cat_select";
            try
            {
                var obList = new List<INV_ITEM_CATModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pITEM_CAT_CODE", Value =ITEM_CAT_CODE},
                     new CommandParameter() {ParameterName = "pOption", Value =3007},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    INV_ITEM_CATModel ob = new INV_ITEM_CATModel();
                    ob.INV_ITEM_CAT_ID = (dr["INV_ITEM_CAT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_ITEM_CAT_ID"]);
                    ob.PARENT_ID = (dr["PARENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PARENT_ID"]);
                    ob.ITEM_CAT_PREFIX = (dr["ITEM_CAT_PREFIX"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_CAT_PREFIX"]);
                    ob.ITEM_CAT_CODE = (dr["ITEM_CAT_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_CAT_CODE"]);
                    ob.ITEM_CAT_NAME_EN = (dr["ITEM_CAT_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_CAT_NAME_EN"]);
                    ob.ITEM_CAT_NAME_BN = (dr["ITEM_CAT_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_CAT_NAME_BN"]);
                    ob.ITEM_CAT_SNAME = (dr["ITEM_CAT_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_CAT_SNAME"]);
                    ob.ITEM_CAT_LEVEL = (dr["ITEM_CAT_LEVEL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ITEM_CAT_LEVEL"]);
                    ob.ITEM_CAT_ORDER = (dr["ITEM_CAT_ORDER"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ITEM_CAT_ORDER"]);
                    ob.IS_LEAF = (dr["IS_LEAF"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_LEAF"]);
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<INV_ITEM_CATModel> ItemDataForStyleBOM(Int64 MC_STYLE_H_ID, Int64? MC_BUYER_ID, Int64? pMC_BYR_ACC_ID)
        {
            string sp = "pkg_budget_sheet.mc_style_d_bom_select";
            try
            {
                var obList = new List<INV_ITEM_CATModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pOption", Value =3002},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    INV_ITEM_CATModel ob = new INV_ITEM_CATModel();
                    ob.INV_ITEM_CAT_ID = (dr["INV_ITEM_CAT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_ITEM_CAT_ID"]);
                    ob.ITEM_CAT_CODE = (dr["ITEM_CAT_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_CAT_CODE"]);
                    ob.ITEM_CAT_NAME_EN = (dr["ITEM_CAT_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_CAT_NAME_EN"]);
                    ob.IS_LEAF = (dr["IS_LEAF"] == DBNull.Value) ? "Y" : Convert.ToString(dr["IS_LEAF"]);

                    var ds1 = db.ExecuteStoredProcedure(new List<CommandParameter>()
                             {
                                new CommandParameter() {ParameterName = "pINV_ITEM_CAT_ID", Value =ob.INV_ITEM_CAT_ID},
                                new CommandParameter() {ParameterName = "pIS_LEAF", Value =ob.IS_LEAF},
                                new CommandParameter() {ParameterName = "pMC_STYLE_H_ID", Value =MC_STYLE_H_ID}, 
                                new CommandParameter() {ParameterName = "pMC_BUYER_ID", Value =MC_BUYER_ID}, 
                                new CommandParameter() {ParameterName = "pMC_BYR_ACC_ID", Value =pMC_BYR_ACC_ID}, 
                                new CommandParameter() {ParameterName = "pOption", Value =3003},
                                new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                            }, sp);

                    foreach (DataRow dr1 in ds1.Tables[0].Rows)
                    {
                        MC_STYLE_D_BOMModel ob1 = new MC_STYLE_D_BOMModel();
                        ob1.INV_ITEM_ID = (dr1["INV_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr1["INV_ITEM_ID"]);
                        ob1.INV_ITEM_CAT_ID = (dr1["INV_ITEM_CAT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr1["INV_ITEM_CAT_ID"]);
                        ob1.MC_STYLE_D_BOM_ID = (dr1["MC_STYLE_D_BOM_ID"] == DBNull.Value) ? -1 : Convert.ToInt64(dr1["MC_STYLE_D_BOM_ID"]);
                        ob1.ITEM_NAME_EN = (dr1["ITEM_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["ITEM_NAME_EN"]);
                        ob1.PURCH_MOU_ID = (dr1["PURCH_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr1["PURCH_MOU_ID"]);
                        ob1.CONS_MOU_ID = (dr1["CONS_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr1["CONS_MOU_ID"]);
                        ob1.UNIT_QTY_REQ = (dr1["UNIT_QTY_REQ"] == DBNull.Value) ? 1 : Convert.ToInt64(dr1["UNIT_QTY_REQ"]);

                        ob1.IS_COMMON = (dr1["IS_COMMON"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["IS_COMMON"]);
                        ob1.POSITION_DESC = (dr1["POSITION_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["POSITION_DESC"]);

                        ob1.REMARKS = (dr1["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["REMARKS"]);
                        ob1.RATE_EST = (dr1["RATE_EST"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr1["RATE_EST"]);
                        ob1.PCT_ADD_QTY = (dr1["PCT_ADD_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr1["PCT_ADD_QTY"]);
                        ob1.IS_PCT = (dr1["IS_PCT"] == DBNull.Value) ? "N" : Convert.ToString(dr1["IS_PCT"]);
                        ob.inv_items.Add(ob1);
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

        public List<INV_ITEM_CATModel> ItemDataForStyleBlkBOM(Int64 MC_STYLE_H_ID, Int64 MC_BLK_FAB_REQ_H_ID, Int64 MC_STYL_BGT_H_ID)
        {
            string sp = "pkg_budget_sheet.mc_style_d_bom_select";
            try
            {
                var obList = new List<INV_ITEM_CATModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pOption", Value =3004},
                     new CommandParameter() {ParameterName = "pMC_BLK_FAB_REQ_H_ID", Value = MC_BLK_FAB_REQ_H_ID}, 
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    INV_ITEM_CATModel ob = new INV_ITEM_CATModel();
                    ob.INV_ITEM_CAT_ID = (dr["INV_ITEM_CAT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_ITEM_CAT_ID"]);
                    ob.ITEM_CAT_CODE = (dr["ITEM_CAT_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_CAT_CODE"]);
                    ob.ITEM_CAT_NAME_EN = (dr["ITEM_CAT_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_CAT_NAME_EN"]);
                    ob.IS_LEAF = (dr["IS_LEAF"] == DBNull.Value) ? "Y" : Convert.ToString(dr["IS_LEAF"]);
                    ob.TOT_OQ = (dr["TOT_OQ"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["TOT_OQ"]);

                    var ds1 = db.ExecuteStoredProcedure(new List<CommandParameter>()
                             {
                                new CommandParameter() {ParameterName = "pINV_ITEM_CAT_ID", Value =ob.INV_ITEM_CAT_ID},
                                new CommandParameter() {ParameterName = "pITEM_CAT_CODE", Value =ob.ITEM_CAT_CODE},
                                new CommandParameter() {ParameterName = "pIS_LEAF", Value =ob.IS_LEAF},
                                new CommandParameter() {ParameterName = "pMC_STYLE_H_ID", Value =MC_STYLE_H_ID},     
                                new CommandParameter() {ParameterName = "pMC_BLK_FAB_REQ_H_ID", Value =MC_BLK_FAB_REQ_H_ID},
                                new CommandParameter() {ParameterName = "pTOT_OQ", Value = ob.TOT_OQ},
                                new CommandParameter() {ParameterName = "pMC_STYL_BGT_H_ID", Value = MC_STYL_BGT_H_ID},
                                new CommandParameter() {ParameterName = "pOption", Value =3005},
                                new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                            }, sp);

                    foreach (DataRow dr1 in ds1.Tables[0].Rows)
                    {
                        MC_STYLE_BLK_BOMModel ob1 = new MC_STYLE_BLK_BOMModel();
                        ob1.MC_STYLE_BLK_BOM_ID = (dr1["MC_STYLE_BLK_BOM_ID"] == DBNull.Value) ? -1 : Convert.ToInt64(dr1["MC_STYLE_BLK_BOM_ID"]);
                        ob1.INV_ITEM_ID = (dr1["INV_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr1["INV_ITEM_ID"]);
                        ob1.INV_ITEM_CAT_ID = (dr1["INV_ITEM_CAT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr1["INV_ITEM_CAT_ID"]);
                        ob1.ITEM_NAME_EN = (dr1["ITEM_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["ITEM_NAME_EN"]);
                        ob1.CONV_FACTOR = (dr1["CONV_FACTOR"] == DBNull.Value) ? -1 : Convert.ToDecimal(dr1["CONV_FACTOR"]);
                        ob1.UNIT_QTY_REQ = (dr1["UNIT_QTY_REQ"] == DBNull.Value) ? 0 : Convert.ToInt64(dr1["UNIT_QTY_REQ"]);
                        ob1.PURCH_MOU_ID = (dr1["PURCH_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr1["PURCH_MOU_ID"]);
                        ob1.REMARKS = (dr1["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["REMARKS"]);
                        ob1.SUGGESTED = (dr1["SUGGESTED"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["SUGGESTED"]);

                        if (dr1["QTY_EST"] != DBNull.Value)
                        {
                            ob1.QTY_EST = Convert.ToDecimal(dr1["QTY_EST"]);
                        }
                        else
                        {
                            if (ob1.CONV_FACTOR > 0)
                            {
                                ob1.QTY_EST = Convert.ToInt64((ob1.UNIT_QTY_REQ * ob.TOT_OQ) / ob1.CONV_FACTOR);
                            }

                        }

                        if (dr1["PCT_ADD_QTY"] != DBNull.Value)
                        {
                            ob1.PCT_ADD_QTY = Convert.ToDecimal(dr1["PCT_ADD_QTY"]);
                        }

                        if (dr1["RATE_EST"] != DBNull.Value)
                        {
                            ob1.RATE_EST = Convert.ToDecimal(dr1["RATE_EST"]);
                        }

                        if (dr1["REVISION_NO"] != DBNull.Value)
                        {
                            ob1.REVISION_NO = Convert.ToInt64(dr1["REVISION_NO"]);
                        }

                        if (dr1["LK_FAB_QTY_SRC"] != DBNull.Value)
                        {
                            ob1.LK_FAB_QTY_SRC = Convert.ToInt64(dr1["LK_FAB_QTY_SRC"]);
                        }
                        ob1.QTY_ACT = (dr1["QTY_ACT"] == DBNull.Value) ? 0 : Convert.ToInt64(dr1["QTY_ACT"]);
                        ob1.RATE_ACT = (dr1["RATE_ACT"] == DBNull.Value) ? 0 : Convert.ToInt64(dr1["RATE_ACT"]);

                        ob1.PURCH_PRICE = (dr1["PURCH_PRICE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr1["PURCH_PRICE"]);
                        ob1.IMP_PRICE = (dr1["IMP_PRICE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr1["IMP_PRICE"]);

                        ob1.ACCT_MOU_ID = (dr1["ACCT_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr1["ACCT_MOU_ID"]);
                        ob1.IS_PO_CREATED = (dr1["IS_PO_CREATED"] == DBNull.Value) ? "N" : Convert.ToString(dr1["IS_PO_CREATED"]);
                        ob1.BOOKED_QTY = (dr1["BOOKED_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr1["BOOKED_QTY"]);

                        ob.inv_items_blk.Add(ob1);
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

        public List<INV_ITEM_CATModel> ItemDataForBuyerBOM(Int64 MC_BUYER_ID, Int64? pMC_BYR_ACC_ID)
        {
            string sp = "pkg_budget_sheet.mc_style_d_bom_select";
            try
            {
                var obList = new List<INV_ITEM_CATModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pOption", Value =3002},                   
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    INV_ITEM_CATModel ob = new INV_ITEM_CATModel();
                    ob.INV_ITEM_CAT_ID = (dr["INV_ITEM_CAT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_ITEM_CAT_ID"]);
                    ob.ITEM_CAT_CODE = (dr["ITEM_CAT_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_CAT_CODE"]);
                    ob.ITEM_CAT_NAME_EN = (dr["ITEM_CAT_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_CAT_NAME_EN"]);
                    ob.IS_LEAF = (dr["IS_LEAF"] == DBNull.Value) ? "Y" : Convert.ToString(dr["IS_LEAF"]);

                    var ds1 = db.ExecuteStoredProcedure(new List<CommandParameter>()
                             {
                                new CommandParameter() {ParameterName = "pINV_ITEM_CAT_ID", Value =ob.INV_ITEM_CAT_ID},
                                new CommandParameter() {ParameterName = "pIS_LEAF", Value =ob.IS_LEAF}, 
                                new CommandParameter() {ParameterName = "pMC_BUYER_ID", Value =MC_BUYER_ID}, 
                                new CommandParameter() {ParameterName = "pMC_BYR_ACC_ID", Value =pMC_BYR_ACC_ID},
                                new CommandParameter() {ParameterName = "pOption", Value =3008},
                                new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                            }, sp);

                    foreach (DataRow dr1 in ds1.Tables[0].Rows)
                    {
                        MC_BUYER_BOMModel ob1 = new MC_BUYER_BOMModel();
                        ob1.INV_ITEM_ID = (dr1["INV_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr1["INV_ITEM_ID"]);
                        ob1.MC_BUYER_BOM_ID = (dr1["MC_BUYER_BOM_ID"] == DBNull.Value) ? -1 : Convert.ToInt64(dr1["MC_BUYER_BOM_ID"]);
                        ob1.ITEM_NAME_EN = (dr1["ITEM_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["ITEM_NAME_EN"]);
                        ob1.PURCH_MOU_ID = (dr1["PURCH_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr1["PURCH_MOU_ID"]);
                        ob1.CONS_MOU_ID = (dr1["CONS_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr1["CONS_MOU_ID"]);
                        ob1.IS_COMMON = (dr1["IS_COMMON"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["IS_COMMON"]);
                        ob1.LK_ACS_CONS_TYPE_ID = (dr1["LK_ACS_CONS_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr1["LK_ACS_CONS_TYPE_ID"]);
                        ob1.IS_SUPL_NOM = (dr1["IS_SUPL_NOM"] == DBNull.Value) ? "N" : Convert.ToString(dr1["IS_SUPL_NOM"]);
                        ob1.REMARKS = (dr1["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["REMARKS"]);
                        ob1.RATE_EST = (dr1["RATE_EST"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr1["RATE_EST"]);
                        ob1.NOM_SUP_LST = new MC_BYR_NOM_SUPModel().SelectByID(pMC_BYR_ACC_ID, ob1.INV_ITEM_ID);
                        ob.inv_itm_byr_bom.Add(ob1);
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

        public string BatchSaveItemCatPermission()
        {
            const string sp = "pkg_inventory.inv_user_itmcat_insert";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {                     
                     new CommandParameter() {ParameterName = "pXML", Value = ob.XML_STR},                     
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]}, //ob.CREATED_BY},                     
                     new CommandParameter() {ParameterName = "pOption", Value =1000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);

                foreach (DataRow dr in ds.Tables["OUTPARAM"].Rows)
                {
                    jsonStr += Convert.ToString('"') + dr["KEY"].ToString() + Convert.ToString('"') + ":" + Convert.ToString('"') + (dr["VALUE"].ToString().Replace(@"""", @"\""")) + Convert.ToString('"');
                    if (i < ds.Tables["OUTPARAM"].Rows.Count)
                    {
                        jsonStr += ",";
                    }
                    else
                    {
                        jsonStr += "}";
                    }
                    i++;
                }
                return jsonStr;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<dynamic> getSalesStoreData()
        {
            string sp = "pkg_inventory.inv_item_cat_select";
            try
            {
                var obList = new List<dynamic>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pOption", Value = 3012},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    obList.Add(new
                    {
                        PS_STORE_ID = (dr["PS_STORE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PS_STORE_ID"]),
                        STORE_NAME_EN = (dr["STORE_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STORE_NAME_EN"])
                    });
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public List<dynamic> getSalesPointData()
        {
            string sp = "pkg_inventory.inv_item_cat_select";
            try
            {
                var obList = new List<dynamic>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pOption", Value = 3010},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    obList.Add(new
                    {
                        PS_COUNTR_ID = (dr["PS_COUNTR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PS_COUNTR_ID"]),
                        COUNTER_DESC = (dr["COUNTER_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COUNTER_DESC"])
                    });
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public object ItemCategListByUserID()
        {
            string sp = "pkg_inventory.inv_item_cat_select";
            try
            {
                var obList = new List<INV_ITEM_CATModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pSC_USER_ID", Value =HttpContext.Current.Session["multiScUserId"]}, 
                     new CommandParameter() {ParameterName = "pOption", Value =3011},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    INV_ITEM_CATModel ob = new INV_ITEM_CATModel();
                    ob.INV_ITEM_CAT_ID = (dr["INV_ITEM_CAT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_ITEM_CAT_ID"]);
                    ob.PARENT_ID = (dr["PARENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PARENT_ID"]);
                    ob.ITEM_CAT_PREFIX = (dr["ITEM_CAT_PREFIX"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_CAT_PREFIX"]);
                    ob.ITEM_CAT_CODE = (dr["ITEM_CAT_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_CAT_CODE"]);
                    ob.ITEM_CAT_NAME_EN = (dr["ITEM_CAT_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_CAT_NAME_EN"]);
                    ob.ITEM_CAT_NAME_BN = (dr["ITEM_CAT_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_CAT_NAME_BN"]);
                    ob.ITEM_CAT_SNAME = (dr["ITEM_CAT_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_CAT_SNAME"]);
                    ob.ITEM_CAT_LEVEL = (dr["ITEM_CAT_LEVEL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ITEM_CAT_LEVEL"]);
                    ob.ITEM_CAT_ORDER = (dr["ITEM_CAT_ORDER"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ITEM_CAT_ORDER"]);
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


    }
}