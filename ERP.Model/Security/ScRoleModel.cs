using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.Data;
using System.Web;
//using ERP.Interface;

namespace ERP.Model
{
    public class ScRoleModel //: ISC_ROLEModel
    {
        public Int64 SC_ROLE_ID { get; set; }
        
        
        [Display(Name = "Role Name [E]")]
        [Required]
        [Remote("IsUniqueNameE", "ScRole", ErrorMessage = "Role name [E] already exists")]
        public string ROLE_NAME_EN { get; set; }

        [Display(Name = "Role Name [B]")]
        public string ROLE_NAME_BN { get; set; }
        [Display(Name = "Description")]
        public string ROLE_DESC { get; set; }

        [Required]
        [Display(Name = "Short Name")]
        public string ROLE_SNAME { get; set; }
        [Display(Name = "Admin?")]
        [Required]
        public string IS_ADMIN { get; set; }
        [Display(Name = "Active?")]
        [Required]
        public string IS_ACTIVE { get;  set; }


        
        //public bool VIEW_IS_ADMIN
        //{
        //    get
        //    {
        //        //return Boolean.Parse(IS_ADMIN);
        //        bool x=false;
        //        if(IS_ADMIN!=null)
        //            x = Boolean.Parse(IS_ADMIN);

        //        if (x)
        //            return true;
        //        else
        //            return false;

        //    }
        //    set
        //    {
        //        if (value)
        //            IS_ADMIN = "Y";
        //        else
        //            IS_ADMIN = "N";
        //    }
        //}

        
        //public bool VIEW_IS_ACTIVE
        //{
        //    get
        //    {
        //        //return Boolean.Parse(IS_ACTIVE);
        //        bool x = false;
        //        if (IS_ACTIVE != null)
        //            x = Boolean.Parse(IS_ACTIVE);

        //        if (x)
        //            return true;
        //        else
        //            return false;
        //    }
        //    set
        //    {
        //        if (value)
        //            IS_ACTIVE = "Y";
        //        else
        //            IS_ACTIVE = "N";
        //    }
        //}

        //public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        //public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }
        public Int64 LAST_UPDATE_LOGIN { get; set; }
        public Int64 VERSION_NO { get; set; }

        public string Save()
        {
            const string sp = "pkg_security.sc_role_insert";
            string vMsg = "";
            var ob = this;

            OraDatabase db = new OraDatabase();
            try
            {
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                    new CommandParameter() {ParameterName = "pSC_ROLE_ID", Value = ob.SC_ROLE_ID},
                    new CommandParameter() {ParameterName = "pROLE_NAME_EN", Value = ob.ROLE_NAME_EN},
                    new CommandParameter() {ParameterName = "pROLE_NAME_BN", Value = ob.ROLE_NAME_BN},
                    new CommandParameter() {ParameterName = "pROLE_DESC", Value = ob.ROLE_DESC},
                    new CommandParameter() {ParameterName = "pROLE_SNAME", Value = ob.ROLE_SNAME},
                    new CommandParameter() {ParameterName = "pIS_ADMIN", Value = ob.IS_ADMIN},
                    new CommandParameter() {ParameterName = "pIS_ACTIVE", Value = ob.IS_ACTIVE},
                    new CommandParameter() {ParameterName = "pCREATED_BY", Value = Convert.ToInt64(HttpContext.Current.Session["multiScUserId"])},
                    new CommandParameter() {ParameterName = "pVERSION_NO", Value = ob.VERSION_NO},
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
            const string sp = "pkg_security.sc_role_update";
            var ob = this;
            string vMsg = "";

            OraDatabase db = new OraDatabase();
            try
            {
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                    new CommandParameter() {ParameterName = "pSC_ROLE_ID", Value = ob.SC_ROLE_ID},
                    new CommandParameter() {ParameterName = "pROLE_NAME_EN", Value = ob.ROLE_NAME_EN},
                    new CommandParameter() {ParameterName = "pROLE_NAME_BN", Value = ob.ROLE_NAME_BN},
                    new CommandParameter() {ParameterName = "pROLE_DESC", Value = ob.ROLE_DESC},
                    new CommandParameter() {ParameterName = "pROLE_SNAME", Value = ob.ROLE_SNAME},
                    new CommandParameter() {ParameterName = "pIS_ADMIN", Value = ob.IS_ADMIN},
                    new CommandParameter() {ParameterName = "pIS_ACTIVE", Value = ob.IS_ACTIVE},
                    new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = Convert.ToInt64(HttpContext.Current.Session["multiScUserId"])},
                    new CommandParameter() {ParameterName = "pLAST_UPDATE_LOGIN", Value = Convert.ToInt64(HttpContext.Current.Session["multiScUserId"])},
                    new CommandParameter() {ParameterName = "pVERSION_NO", Value = ob.VERSION_NO},
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

        public string Delete()
        {
            const string sp = "pkg_security.sc_role_delete";
            string vMsg = "";
            var ob = this;
            OraDatabase db = new OraDatabase();
            try
            {
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                    new CommandParameter() {ParameterName = "pSC_ROLE_ID", Value = ob.SC_ROLE_ID},
                    new CommandParameter() {ParameterName = "pROLE_NAME_EN", Value = ob.ROLE_NAME_EN},
                    new CommandParameter() {ParameterName = "pROLE_NAME_BN", Value = ob.ROLE_NAME_BN},
                    new CommandParameter() {ParameterName = "pROLE_DESC", Value = ob.ROLE_DESC},
                    new CommandParameter() {ParameterName = "pROLE_SNAME", Value = ob.ROLE_SNAME},
                    new CommandParameter() {ParameterName = "pIS_ADMIN", Value = ob.IS_ADMIN},
                    new CommandParameter() {ParameterName = "pIS_ACTIVE", Value = ob.IS_ACTIVE},
                    new CommandParameter() {ParameterName = "pOption", Value = 4000},
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

        public List<ScRoleModel> SelectAll()
        {
            string sp = "pkg_security.sc_role_select";

            try
            {
                var obList = new List<ScRoleModel>();

                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   new CommandParameter() {ParameterName = "pOption", Value = 3000},
                    new CommandParameter() {ParameterName = "pSC_ROLE_ID", Value = 0},
                    new CommandParameter() {ParameterName = "pMsg", Value = 500, Direction = ParameterDirection.Output}
                }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ScRoleModel ob = new ScRoleModel();
                    ob.SC_ROLE_ID = (dr["SC_ROLE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SC_ROLE_ID"]);
                    ob.ROLE_NAME_EN = (dr["ROLE_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ROLE_NAME_EN"]);
                    ob.ROLE_NAME_BN = (dr["ROLE_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ROLE_NAME_BN"]);
                    ob.ROLE_DESC = (dr["ROLE_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ROLE_DESC"]);
                    ob.ROLE_SNAME = (dr["ROLE_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ROLE_SNAME"]);
                    ob.IS_ADMIN = (dr["IS_ADMIN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ADMIN"]);
                    ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);
                    //ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);
                    ob.LAST_UPDATE_LOGIN = (dr["LAST_UPDATE_LOGIN"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATE_LOGIN"]);
                    ob.VERSION_NO = (dr["VERSION_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["VERSION_NO"]);
                    obList.Add(ob);
                }

                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ScRoleModel Select(long ID)
        {
            string sp = "pkg_security.sc_role_select";
            try
            {
                var ob = new ScRoleModel();

                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
                    new CommandParameter() {ParameterName = "pSC_ROLE_ID", Value = ID},
                    new CommandParameter() {ParameterName = "pOption", Value = 3001},
                    new CommandParameter() {ParameterName = "pMsg", Value = 500, Direction = ParameterDirection.Output}
                }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.SC_ROLE_ID = (dr["SC_ROLE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SC_ROLE_ID"]);
                    ob.ROLE_NAME_EN = (dr["ROLE_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ROLE_NAME_EN"]);
                    ob.ROLE_NAME_BN = (dr["ROLE_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ROLE_NAME_BN"]);
                    ob.ROLE_DESC = (dr["ROLE_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ROLE_DESC"]);
                    ob.ROLE_SNAME = (dr["ROLE_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ROLE_SNAME"]);
                    ob.IS_ADMIN = (dr["IS_ADMIN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ADMIN"]);
                    ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);
                    ob.LAST_UPDATE_LOGIN = (dr["LAST_UPDATE_LOGIN"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATE_LOGIN"]);
                    ob.VERSION_NO = (dr["VERSION_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["VERSION_NO"]);
                }

                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        
    }
}