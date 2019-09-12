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
    public class INV_USER_ITMCATModel
    {
        public Int64 INV_USER_ITMCAT_ID { get; set; }
        public Int64 SC_USER_ID { get; set; }
        public Int64 INV_ITEM_CAT_ID { get; set; }
        public string IS_ACTIVE { get; set; }
        //public DateTime CREATION_DATE { get; set; }
        //public Int64 CREATED_BY { get; set; }
        //public DateTime LAST_UPDATE_DATE { get; set; }
        //public Int64 LAST_UPDATED_BY { get; set; }
        public bool IsChecked { get; set; }
        public string ITEM_CAT_NAME_EN { get; set; }
        public string text { get; set; }
        public string spriteCssClass { get; set; }
        public bool expanded { get; set; }
        public string IS_LEAF { get; set; }
        public List<INV_USER_ITMCATModel> _SubDeptModel = null;
        public List<INV_USER_ITMCATModel> items
        {
            get
            {
                if (_SubDeptModel == null)
                {
                    _SubDeptModel = new List<INV_USER_ITMCATModel>();
                }
                return _SubDeptModel;
            }
            set
            {
                _SubDeptModel = value;
            }
        }
        public Int64? INV_ITEM_CORE_CAT_ID { get; set; }
        
        


        

        public List<INV_USER_ITMCATModel> SelectAll()
        {
            const string sp = "pkg_inventory.inv_user_itmcat_select";
            try
            {
                var obList = new List<INV_USER_ITMCATModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pINV_USER_ITMCAT_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
              foreach (DataRow dr in ds.Tables[0].Rows)
               {
                            INV_USER_ITMCATModel ob = new INV_USER_ITMCATModel();
                            ob.INV_USER_ITMCAT_ID = (dr["INV_USER_ITMCAT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_USER_ITMCAT_ID"]);
                            ob.SC_USER_ID = (dr["SC_USER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SC_USER_ID"]);
                            ob.INV_ITEM_CAT_ID = (dr["INV_ITEM_CAT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_ITEM_CAT_ID"]);
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

        public INV_USER_ITMCATModel Select(long ID)
        {
            const string sp = "pkg_inventory.inv_user_itmcat_select";
            try
            {
                var ob = new INV_USER_ITMCATModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pINV_USER_ITMCAT_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
              foreach (DataRow dr in ds.Tables[0].Rows)
               {
                        ob.INV_USER_ITMCAT_ID = (dr["INV_USER_ITMCAT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_USER_ITMCAT_ID"]);
                        ob.SC_USER_ID = (dr["SC_USER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SC_USER_ID"]);
                        ob.INV_ITEM_CAT_ID = (dr["INV_ITEM_CAT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_ITEM_CAT_ID"]);
                        ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);
                        
               }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<INV_USER_ITMCATModel> LoginUserWiseItemCatList()
        {
            string vUserType = HttpContext.Current.Session["multiUserType"].ToString();
            Int64 vLoginUserId = Convert.ToInt64(HttpContext.Current.Session["multiScUserId"]);
            int vOption = 3002;

            string sp = "pkg_inventory.inv_user_itmcat_select";
            try
            {
                if (vUserType == "B")
                    vOption = 3006;
                else
                    vOption = 3007;


                var obList = new List<INV_USER_ITMCATModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pSC_USER_ID", Value = vLoginUserId},
                     new CommandParameter() {ParameterName = "pOption", Value = vOption},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                }, sp);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    INV_USER_ITMCATModel ob = new INV_USER_ITMCATModel();
                    ob.INV_ITEM_CAT_ID = (ds.Tables[0].Rows[i]["INV_ITEM_CAT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(ds.Tables[0].Rows[i]["INV_ITEM_CAT_ID"]);
                    ob.ITEM_CAT_NAME_EN = (ds.Tables[0].Rows[i]["ITEM_CAT_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["ITEM_CAT_NAME_EN"]);
                    ob.IS_LEAF = (ds.Tables[0].Rows[i]["IS_LEAF"] == DBNull.Value) ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["IS_LEAF"]);
                    ob.IS_ACTIVE = (ds.Tables[0].Rows[i]["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["IS_ACTIVE"]);
                                        
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<INV_USER_ITMCATModel> LoginUserWiseItemCatTreeList()
        {
            string vUserType = HttpContext.Current.Session["multiUserType"].ToString();
            Int64 vLoginUserId= Convert.ToInt64(HttpContext.Current.Session["multiScUserId"]);
            int vOption=3002;

            string sp = "pkg_inventory.inv_user_itmcat_select";
            try
            {
                if (vUserType == "B")
                    vOption = 3004;
                else
                    vOption = 3002;


                var obList = new List<INV_USER_ITMCATModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pSC_USER_ID", Value = vLoginUserId},
                     new CommandParameter() {ParameterName = "pOption", Value = vOption},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                }, sp);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    INV_USER_ITMCATModel root = new INV_USER_ITMCATModel();
                    root.INV_ITEM_CAT_ID = (ds.Tables[0].Rows[i]["INV_ITEM_CAT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(ds.Tables[0].Rows[i]["INV_ITEM_CAT_ID"]);                    
                    root.ITEM_CAT_NAME_EN = (ds.Tables[0].Rows[i]["ITEM_CAT_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["ITEM_CAT_NAME_EN"]);
                    root.IS_LEAF = (ds.Tables[0].Rows[i]["IS_LEAF"] == DBNull.Value) ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["IS_LEAF"]);
                    root.IS_ACTIVE = (ds.Tables[0].Rows[i]["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["IS_ACTIVE"]);
                    root.INV_ITEM_CORE_CAT_ID = (ds.Tables[0].Rows[i]["INV_ITEM_CORE_CAT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(ds.Tables[0].Rows[i]["INV_ITEM_CORE_CAT_ID"]);
                    root.text = (ds.Tables[0].Rows[i]["ITEM_CAT_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["ITEM_CAT_NAME_EN"]);
                    root.expanded = true;
                    //root.IsChecked = (Convert.ToString(ds.Tables[0].Rows[i]["INV_CAT_IS_ACTIVE"]) == "Y") ? true : false;


                    if (root.IS_LEAF == "Y")
                        root.spriteCssClass = "html";
                    else
                        root.spriteCssClass = "folder";

                    CreateNode(root, vLoginUserId, vUserType);
                    obList.Add(root);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void CreateNode(INV_USER_ITMCATModel node, Int64? pSC_USER_ID, string pUSER_TYPE)
        {
            int vOption = 3003;
            if (pUSER_TYPE == "B")
                vOption = 3005;
            else
                vOption = 3003;

            string sp = "pkg_inventory.inv_user_itmcat_select";
            var obList = new List<INV_USER_ITMCATModel>();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                    new CommandParameter() {ParameterName = "pPARENT_ID", Value = node.INV_ITEM_CAT_ID},                   
                    new CommandParameter() {ParameterName = "pOption", Value = vOption},
                    new CommandParameter() {ParameterName = "pSC_USER_ID", Value = pSC_USER_ID},
                    new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);

            if (ds.Tables[0].Rows.Count == 0)
            {
                return;
            }
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                INV_USER_ITMCATModel tnode = new INV_USER_ITMCATModel();
                tnode.INV_ITEM_CAT_ID = (ds.Tables[0].Rows[i]["INV_ITEM_CAT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(ds.Tables[0].Rows[i]["INV_ITEM_CAT_ID"]);                
                tnode.ITEM_CAT_NAME_EN = (ds.Tables[0].Rows[i]["ITEM_CAT_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["ITEM_CAT_NAME_EN"]);
                tnode.IS_LEAF = (ds.Tables[0].Rows[i]["IS_LEAF"] == DBNull.Value) ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["IS_LEAF"]);
                tnode.IS_ACTIVE = (ds.Tables[0].Rows[i]["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["IS_ACTIVE"]);
                tnode.INV_ITEM_CORE_CAT_ID = (ds.Tables[0].Rows[i]["INV_ITEM_CORE_CAT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(ds.Tables[0].Rows[i]["INV_ITEM_CORE_CAT_ID"]);
                tnode.text = (ds.Tables[0].Rows[i]["ITEM_CAT_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["ITEM_CAT_NAME_EN"]);
                tnode.expanded = false;
                //tnode.IsChecked = (Convert.ToString(ds.Tables[0].Rows[i]["INV_CAT_IS_ACTIVE"]) == "Y") ? true : false;

                if (tnode.IS_LEAF == "Y")
                    tnode.spriteCssClass = "html";
                else
                    tnode.spriteCssClass = "folder";

                node.items.Add(tnode);
                CreateNode(tnode, pSC_USER_ID, pUSER_TYPE);
            }

        }


    }
}