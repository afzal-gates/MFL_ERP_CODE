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
    public class KNT_MC_TYPEModel
    {
        public Int64 KNT_MC_TYPE_ID { get; set; }
        public Int64 PARENT_ID { get; set; }
        public string MC_TYPE_CODE { get; set; }
        public string MC_TYPE_NAME_EN { get; set; }
        public string MC_TYPE_NAME_BN { get; set; }
        public string IS_LEAF { get; set; }
        public string IS_ACTIVE { get; set; }

        public string PARENT_NAME { get; set; }
        public string text { get; set; }
        public string spriteCssClass { get; set; }
        public bool expanded { get; set; }
        public bool selected { get; set; }
        public bool IsChecked { get; set; }
        private List<KNT_MC_TYPEModel> _SubMcTypeModel = null;
        public List<KNT_MC_TYPEModel> items
        {
            get
            {
                if (_SubMcTypeModel == null)
                {
                    _SubMcTypeModel = new List<KNT_MC_TYPEModel>();
                }
                return _SubMcTypeModel;
            }
            set
            {
                _SubMcTypeModel = value;
            }
        }




        public string Save()
        {
            const string sp = "SP_KNT_MC_TYPE";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_MC_TYPE_ID", Value = ob.KNT_MC_TYPE_ID},
                     new CommandParameter() {ParameterName = "pPARENT_ID", Value = ob.PARENT_ID},
                     new CommandParameter() {ParameterName = "pMC_TYPE_CODE", Value = ob.MC_TYPE_CODE},
                     new CommandParameter() {ParameterName = "pMC_TYPE_NAME_EN", Value = ob.MC_TYPE_NAME_EN},
                     new CommandParameter() {ParameterName = "pMC_TYPE_NAME_BN", Value = ob.MC_TYPE_NAME_BN},
                     new CommandParameter() {ParameterName = "pIS_LEAF", Value = ob.IS_LEAF},
                     new CommandParameter() {ParameterName = "pIS_ACTIVE", Value = ob.IS_ACTIVE},
       
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

        public string Update()
        {
            const string sp = "SP_KNT_MC_TYPE";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_MC_TYPE_ID", Value = ob.KNT_MC_TYPE_ID},
                     new CommandParameter() {ParameterName = "pPARENT_ID", Value = ob.PARENT_ID},
                     new CommandParameter() {ParameterName = "pMC_TYPE_CODE", Value = ob.MC_TYPE_CODE},
                     new CommandParameter() {ParameterName = "pMC_TYPE_NAME_EN", Value = ob.MC_TYPE_NAME_EN},
                     new CommandParameter() {ParameterName = "pMC_TYPE_NAME_BN", Value = ob.MC_TYPE_NAME_BN},
                     new CommandParameter() {ParameterName = "pIS_LEAF", Value = ob.IS_LEAF},
                     new CommandParameter() {ParameterName = "pIS_ACTIVE", Value = ob.IS_ACTIVE},
        
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

        public List<KNT_MC_TYPEModel> KntMcTypeListByUser()
        {
            string sp = "pkg_knit_common.knt_mc_type_select";
            try
            {
                var obList = new List<KNT_MC_TYPEModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pSC_USER_ID", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pOption", Value = 3005},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    KNT_MC_TYPEModel ob = new KNT_MC_TYPEModel();
                    ob.KNT_MC_TYPE_ID = (dr["KNT_MC_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_MC_TYPE_ID"]);
                    ob.PARENT_ID = (dr["PARENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PARENT_ID"]);
                    ob.MC_TYPE_CODE = (dr["MC_TYPE_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MC_TYPE_CODE"]);
                    ob.MC_TYPE_NAME_EN = (dr["MC_TYPE_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MC_TYPE_NAME_EN"]);
                    ob.MC_TYPE_NAME_BN = (dr["MC_TYPE_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MC_TYPE_NAME_BN"]);
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

        public KNT_MC_TYPEModel Select(long ID)
        {
            string sp = "Select_KNT_MC_TYPE";
            try
            {
                var ob = new KNT_MC_TYPEModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_MC_TYPE_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.KNT_MC_TYPE_ID = (dr["KNT_MC_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_MC_TYPE_ID"]);
                    ob.PARENT_ID = (dr["PARENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PARENT_ID"]);
                    ob.MC_TYPE_CODE = (dr["MC_TYPE_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MC_TYPE_CODE"]);
                    ob.MC_TYPE_NAME_EN = (dr["MC_TYPE_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MC_TYPE_NAME_EN"]);
                    ob.MC_TYPE_NAME_BN = (dr["MC_TYPE_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MC_TYPE_NAME_BN"]);
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

        public List<KNT_MC_TYPEModel> KntMcTypeList()
        {
            string sp = "pkg_knit_common.knt_mc_type_select";
            try
            {
                var obList = new List<KNT_MC_TYPEModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_MC_TYPE_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3004},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    KNT_MC_TYPEModel ob = new KNT_MC_TYPEModel();
                    ob.KNT_MC_TYPE_ID = (dr["KNT_MC_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_MC_TYPE_ID"]);
                    ob.PARENT_ID = (dr["PARENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PARENT_ID"]);
                    ob.MC_TYPE_CODE = (dr["MC_TYPE_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MC_TYPE_CODE"]);
                    ob.MC_TYPE_NAME_EN = (dr["MC_TYPE_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MC_TYPE_NAME_EN"]);
                    ob.MC_TYPE_NAME_BN = (dr["MC_TYPE_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MC_TYPE_NAME_BN"]);
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

        public List<KNT_MC_TYPEModel> KntMcTypeTreeList()
        {
            string sp = "pkg_knit_common.knt_mc_type_select";
            try
            {
                //var us = HttpContext.Current.Session["multiScUserId"].ToString();

                var obList = new List<KNT_MC_TYPEModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {                     
                     new CommandParameter() {ParameterName = "pOption", Value = 3002},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                }, sp);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    KNT_MC_TYPEModel root = new KNT_MC_TYPEModel();
                    root.KNT_MC_TYPE_ID = (ds.Tables[0].Rows[i]["KNT_MC_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(ds.Tables[0].Rows[i]["KNT_MC_TYPE_ID"]);
                    root.PARENT_ID = (ds.Tables[0].Rows[i]["PARENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(ds.Tables[0].Rows[i]["PARENT_ID"]);
                    root.MC_TYPE_CODE = (ds.Tables[0].Rows[i]["MC_TYPE_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["MC_TYPE_CODE"]);
                    root.MC_TYPE_NAME_EN = (ds.Tables[0].Rows[i]["MC_TYPE_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["MC_TYPE_NAME_EN"]);
                    root.MC_TYPE_NAME_BN = (ds.Tables[0].Rows[i]["MC_TYPE_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["MC_TYPE_NAME_BN"]);
                    root.IS_LEAF = (ds.Tables[0].Rows[i]["IS_LEAF"] == DBNull.Value) ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["IS_LEAF"]);
                    root.IS_ACTIVE = (ds.Tables[0].Rows[i]["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["IS_ACTIVE"]);

                    root.PARENT_NAME = (ds.Tables[0].Rows[i]["PARENT_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["PARENT_NAME"]);
                    root.text = (ds.Tables[0].Rows[i]["MC_TYPE_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["MC_TYPE_NAME_EN"]);
                    root.expanded = true;
                    root.IsChecked = (Convert.ToString(ds.Tables[0].Rows[i]["KNT_MC_TYPE_IS_ACTIVE"]) == "Y") ? true : false;


                    if (root.IS_LEAF == "Y")
                        root.spriteCssClass = "html";
                    else
                        root.spriteCssClass = "folder";

                    CreateNode(root);
                    obList.Add(root);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void CreateNode(KNT_MC_TYPEModel node)
        {
            string sp = "pkg_knit_common.knt_mc_type_select";
            var obList = new List<KNT_MC_TYPEModel>();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                    new CommandParameter() {ParameterName = "pPARENT_ID", Value = node.KNT_MC_TYPE_ID},                    
                    new CommandParameter() {ParameterName = "pOption", Value = 3003},                    
                    new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);

            if (ds.Tables[0].Rows.Count == 0)
            {
                return;
            }
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                KNT_MC_TYPEModel tnode = new KNT_MC_TYPEModel();
                tnode.KNT_MC_TYPE_ID = (ds.Tables[0].Rows[i]["KNT_MC_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(ds.Tables[0].Rows[i]["KNT_MC_TYPE_ID"]);
                tnode.PARENT_ID = (ds.Tables[0].Rows[i]["PARENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(ds.Tables[0].Rows[i]["PARENT_ID"]);
                tnode.MC_TYPE_CODE = (ds.Tables[0].Rows[i]["MC_TYPE_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["MC_TYPE_CODE"]);
                tnode.MC_TYPE_NAME_EN = (ds.Tables[0].Rows[i]["MC_TYPE_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["MC_TYPE_NAME_EN"]);
                tnode.MC_TYPE_NAME_BN = (ds.Tables[0].Rows[i]["MC_TYPE_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["MC_TYPE_NAME_BN"]);
                tnode.IS_LEAF = (ds.Tables[0].Rows[i]["IS_LEAF"] == DBNull.Value) ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["IS_LEAF"]);
                tnode.IS_ACTIVE = (ds.Tables[0].Rows[i]["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["IS_ACTIVE"]);

                tnode.PARENT_NAME = (ds.Tables[0].Rows[i]["PARENT_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["PARENT_NAME"]);
                tnode.text = (ds.Tables[0].Rows[i]["MC_TYPE_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["MC_TYPE_NAME_EN"]);
                tnode.expanded = false;
                tnode.IsChecked = (Convert.ToString(ds.Tables[0].Rows[i]["KNT_MC_TYPE_IS_ACTIVE"]) == "Y") ? true : false;

                if (tnode.IS_LEAF == "Y")
                    tnode.spriteCssClass = "html";
                else
                    tnode.spriteCssClass = "folder";

                node.items.Add(tnode);
                CreateNode(tnode);
            }
        }




    }
}