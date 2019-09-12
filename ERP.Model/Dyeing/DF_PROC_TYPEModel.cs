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

    public class DF_PROC_TYPE_GROUP_VM
    {
        public Int64 LK_PROC_SUB_GRP_ID { get; set; }
        public string PROCESS_GROUP_NAME { get; set; }

        private List<DF_PROC_TYPEModel> _items = null;
        public List<DF_PROC_TYPEModel> items
        {
            get
            {
                if (_items == null)
                {
                    _items = new List<DF_PROC_TYPEModel>();
                }
                return _items;
            }
            set
            {
                _items = value;
            }
        }
    }
    public class DF_PROC_TYPEModel
    {
        public Int64 DF_PROC_TYPE_ID { get; set; }
        public string DF_PROC_TYPE_CODE { get; set; }
        public string DF_PROC_TYPE_NAME { get; set; }
        public string DF_PROC_TYPE_SNAME { get; set; }
        public string IS_ACTIVE { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }

        public string Save()
        {
            const string sp = "pkg_dye_common.df_proc_type_insert";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_PROC_TYPE_ID", Value = ob.DF_PROC_TYPE_ID},
                     new CommandParameter() {ParameterName = "pDF_PROC_TYPE_CODE", Value = ob.DF_PROC_TYPE_CODE},
                     new CommandParameter() {ParameterName = "pDF_PROC_TYPE_NAME", Value = ob.DF_PROC_TYPE_NAME},
                     new CommandParameter() {ParameterName = "pDF_PROC_TYPE_SNAME", Value = ob.DF_PROC_TYPE_SNAME},
                     new CommandParameter() {ParameterName = "pDISPLAY_ORDER", Value = ob.DISPLAY_ORDER},
                     new CommandParameter() {ParameterName = "pPROC_RATE", Value = ob.PROC_RATE},
                     new CommandParameter() {ParameterName = "pLK_PROC_SUB_GRP_ID", Value = ob.LK_PROC_SUB_GRP_ID},
                     new CommandParameter() {ParameterName = "pMC_FAB_PROC_GRP_ID", Value = ob.MC_FAB_PROC_GRP_ID},                     
                     new CommandParameter() {ParameterName = "pIS_ACTIVE", Value = ob.IS_ACTIVE},
                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
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
            const string sp = "df_proc_type_insert";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_PROC_TYPE_ID", Value = ob.DF_PROC_TYPE_ID},
                     new CommandParameter() {ParameterName = "pDF_PROC_TYPE_CODE", Value = ob.DF_PROC_TYPE_CODE},
                     new CommandParameter() {ParameterName = "pDF_PROC_TYPE_NAME", Value = ob.DF_PROC_TYPE_NAME},
                     new CommandParameter() {ParameterName = "pDF_PROC_TYPE_SNAME", Value = ob.DF_PROC_TYPE_SNAME},
                     new CommandParameter() {ParameterName = "pIS_ACTIVE", Value = ob.IS_ACTIVE},
                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = ob.CREATED_BY},
                     new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = ob.LAST_UPDATED_BY},
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

        public string Delete()
        {
            const string sp = "pkg_dye_common.df_proc_type_delete";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_PROC_TYPE_ID", Value = ob.DF_PROC_TYPE_ID},
                     new CommandParameter() {ParameterName = "pDF_PROC_TYPE_CODE", Value = ob.DF_PROC_TYPE_CODE},
                     new CommandParameter() {ParameterName = "pDF_PROC_TYPE_NAME", Value = ob.DF_PROC_TYPE_NAME},
                     new CommandParameter() {ParameterName = "pDF_PROC_TYPE_SNAME", Value = ob.DF_PROC_TYPE_SNAME},
                     new CommandParameter() {ParameterName = "pDISPLAY_ORDER", Value = ob.DISPLAY_ORDER},
                     new CommandParameter() {ParameterName = "pPROC_RATE", Value = ob.PROC_RATE},
                     new CommandParameter() {ParameterName = "pLK_PROC_SUB_GRP_ID", Value = ob.LK_PROC_SUB_GRP_ID},
                     new CommandParameter() {ParameterName = "pMC_FAB_PROC_GRP_ID", Value = ob.MC_FAB_PROC_GRP_ID},    
                     new CommandParameter() {ParameterName = "pIS_ACTIVE", Value = ob.IS_ACTIVE},
                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pOption", Value =4000},
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

        public List<DF_PROC_TYPEModel> SelectAll(Int64? pMC_FAB_PROC_GRP_ID = null)
        {
            string sp = "pkg_dye_common.df_proc_type_select";
            try
            {
                var obList = new List<DF_PROC_TYPEModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMC_FAB_PROC_GRP_ID", Value = pMC_FAB_PROC_GRP_ID},    
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DF_PROC_TYPEModel ob = new DF_PROC_TYPEModel();
                    ob.DF_PROC_TYPE_ID = (dr["DF_PROC_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_PROC_TYPE_ID"]);
                    ob.MC_FAB_PROC_GRP_ID = (dr["MC_FAB_PROC_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_FAB_PROC_GRP_ID"]);
                    ob.DF_PROC_TYPE_CODE = (dr["DF_PROC_TYPE_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DF_PROC_TYPE_CODE"]);
                    ob.DF_PROC_TYPE_NAME = (dr["DF_PROC_TYPE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DF_PROC_TYPE_NAME"]);
                    ob.DF_PROC_TYPE_SNAME = (dr["DF_PROC_TYPE_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DF_PROC_TYPE_SNAME"]);
                    ob.PROC_SUB_GRP_NAME = (dr["PROC_SUB_GRP_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PROC_SUB_GRP_NAME"]);
                    ob.DISPLAY_ORDER = (dr["DISPLAY_ORDER"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DISPLAY_ORDER"]);
                    ob.PROC_RATE = (dr["PROC_RATE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PROC_RATE"]);
                    ob.LK_PROC_SUB_GRP_ID = (dr["LK_PROC_SUB_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_PROC_SUB_GRP_ID"]);
                    ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);
                    obList.Add(ob);
                 
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DF_PROC_TYPEModel> QueryDataForBtPlan(Int64 pLK_PROC_SUB_GRP_ID, Int64? pDYE_BATCH_PLAN_ID)
        {
            string sp = "pkg_dye_common.df_proc_type_select";
            try
            {
                var obList = new List<DF_PROC_TYPEModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pLK_PROC_SUB_GRP_ID", Value = pLK_PROC_SUB_GRP_ID},
                     new CommandParameter() {ParameterName = "pDYE_BATCH_PLAN_ID", Value = pDYE_BATCH_PLAN_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3004},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DF_PROC_TYPEModel ob = new DF_PROC_TYPEModel();
                    ob.DF_PROC_TYPE_ID = (dr["DF_PROC_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_PROC_TYPE_ID"]);

                    ob.DYE_BT_RQD_PROC_ID = (dr["DYE_BT_RQD_PROC_ID"] == DBNull.Value) ? -1 : Convert.ToInt64(dr["DYE_BT_RQD_PROC_ID"]);
                    ob.DF_PROC_TYPE_CODE = (dr["DF_PROC_TYPE_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DF_PROC_TYPE_CODE"]);
                    ob.DF_PROC_TYPE_NAME = (dr["DF_PROC_TYPE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DF_PROC_TYPE_NAME"]);
                    ob.DF_PROC_TYPE_SNAME = (dr["DF_PROC_TYPE_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DF_PROC_TYPE_SNAME"]);
                    ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);
                    ob.IS_SELECTED = (dr["IS_SELECTED"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_SELECTED"]);
                    obList.Add(ob);

                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<DF_PROC_TYPEModel> SelectByID(Int64? pLK_DIA_TYPE_ID = null)
        {
            string sp = "pkg_dye_common.df_proc_type_select";
            try
            {
                var obList = new List<DF_PROC_TYPEModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pLK_DIA_TYPE_ID", Value =pLK_DIA_TYPE_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3002},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DF_PROC_TYPEModel ob = new DF_PROC_TYPEModel();
                    ob.DF_PROC_TYPE_ID = (dr["DF_PROC_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_PROC_TYPE_ID"]);
                    ob.DF_PROC_TYPE_CODE = (dr["DF_PROC_TYPE_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DF_PROC_TYPE_CODE"]);
                    ob.DF_PROC_TYPE_NAME = (dr["DF_PROC_TYPE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DF_PROC_TYPE_NAME"]);
                    ob.DF_PROC_TYPE_SNAME = (dr["DF_PROC_TYPE_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DF_PROC_TYPE_SNAME"]);
                    ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DF_PROC_TYPEModel Select(long ID)
        {
            string sp = "Select_DF_PROC_TYPE";
            try
            {
                var ob = new DF_PROC_TYPEModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_PROC_TYPE_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.DF_PROC_TYPE_ID = (dr["DF_PROC_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_PROC_TYPE_ID"]);
                    ob.DF_PROC_TYPE_CODE = (dr["DF_PROC_TYPE_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DF_PROC_TYPE_CODE"]);
                    ob.DF_PROC_TYPE_NAME = (dr["DF_PROC_TYPE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DF_PROC_TYPE_NAME"]);
                    ob.DF_PROC_TYPE_SNAME = (dr["DF_PROC_TYPE_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DF_PROC_TYPE_SNAME"]);
                    ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);

                }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DF_PROC_TYPE_GROUP_VM> getGroupData(Int64? pDYE_BATCH_PLAN_ID)
        {

            string sp = "PKG_DYE_COMMON.df_proc_type_select";
            try
            {
                var obList = new List<DF_PROC_TYPE_GROUP_VM>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pOption", Value =3003},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DF_PROC_TYPE_GROUP_VM ob = new DF_PROC_TYPE_GROUP_VM();
                    ob.LK_PROC_SUB_GRP_ID = (dr["LK_PROC_SUB_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_PROC_SUB_GRP_ID"]);
                    ob.PROCESS_GROUP_NAME = (dr["PROCESS_GROUP_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PROCESS_GROUP_NAME"]);
                    ob.items = this.QueryDataForBtPlan(ob.LK_PROC_SUB_GRP_ID, pDYE_BATCH_PLAN_ID);
                    obList.Add(ob);

                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public String IS_SELECTED { get; set; }

        public long DYE_BT_RQD_PROC_ID { get; set; }

        public long MC_FAB_PROC_GRP_ID { get; set; }

        public long DISPLAY_ORDER { get; set; }

        public decimal PROC_RATE { get; set; }

        public long LK_PROC_SUB_GRP_ID { get; set; }

        public string PROC_SUB_GRP_NAME { get; set; }
    }
}