using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.Data;


namespace ERP.Model
{
    public class LookupTablesModel //: ILookupTablesModel
    {
        public Int64 LOOKUP_TABLE_ID { get; set; } 

        [Display(Name="Table Code")]
        //[Required(ErrorMessage="Please input table code")]
        public string LK_TABLE_CODE { get; set; }

        [Display(Name = "Table Name")]
        [Required(ErrorMessage = "Please input table name")]
        public string LK_TABLE_NAME_EN { get; set; }

        [Display(Name = "Table Name Bn")]
        public string LK_TABLE_NAME_BN { get; set; }

        [Display(Name="Description")]
        public string LK_TABLE_DESC { get; set; }

        [Display(Name = "Active?")]
        //[Required(ErrorMessage = "Please select an option")]
        public string IS_ACTIVE { get; set; }


        public DateTime? CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime? LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }
        public Int64 LAST_UPDATE_LOGIN { get; set; }
        public Int64 VERSION_NO { get; set; }

        public SelectList GetYesNoList()
        {
            //IEnumerable<SelectListItem> stateList = (from m in _db.mstrstates where m.bstatus == true select m).AsEnumerable().Select(m => new SelectListItem() { Text = m.vstate, Value = m.istateid.ToString() });

            List<SelectListItem> stateList = new List<SelectListItem>();
            
            stateList.Add(new SelectListItem { Text = "Yes", Value = "Y" });
            stateList.Add(new SelectListItem { Text = "No", Value = "N" });
            
            return new SelectList(stateList, "Value", "Text");
        }

        public List<LookupTablesModel> SelectAll()
        {
            string sp = "pkg_admin.lookup_table_select";

            try
            {
                var obList = new List<LookupTablesModel>();

                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   new CommandParameter() {ParameterName = "pOption", Value = 3000},
                    new CommandParameter() {ParameterName = "pLOOKUP_TABLE_ID", Value = null}
                    //new CommandParameter() {ParameterName = "pMsg", Value = 500, Direction = ParameterDirection.Output}
                }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    LookupTablesModel ob = new LookupTablesModel();
                    ob.LOOKUP_TABLE_ID = (dr["LOOKUP_TABLE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LOOKUP_TABLE_ID"]);
                    ob.LK_TABLE_CODE = (dr["LK_TABLE_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LK_TABLE_CODE"]);
                    ob.LK_TABLE_NAME_EN = (dr["LK_TABLE_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LK_TABLE_NAME_EN"]);
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
            const string sp = "pkg_admin.lookup_table_insert";
            string vMsg = "";
            var ob = this;

            OraDatabase db = new OraDatabase();
            try
            {
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                    new CommandParameter() {ParameterName = "pLOOKUP_TABLE_ID", Value = null},
                    new CommandParameter() {ParameterName = "pLK_TABLE_CODE", Value = ob.LK_TABLE_CODE == null ? "A" : ob.LK_TABLE_CODE},
                    new CommandParameter() {ParameterName = "pLK_TABLE_NAME_EN", Value = ob.LK_TABLE_NAME_EN},
                    new CommandParameter() {ParameterName = "pIS_ACTIVE", Value = ob.IS_ACTIVE},
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
            const string sp = "pkg_admin.lookup_table_update";
            string vMsg = "";
            var ob = this;

            OraDatabase db = new OraDatabase();
            try
            {
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                    new CommandParameter() {ParameterName = "pLOOKUP_TABLE_ID", Value = ob.LOOKUP_TABLE_ID},
                    new CommandParameter() {ParameterName = "pLK_TABLE_CODE", Value = ob.LK_TABLE_CODE == null ? "A" : ob.LK_TABLE_CODE},
                    new CommandParameter() {ParameterName = "pLK_TABLE_NAME_EN", Value = ob.LK_TABLE_NAME_EN},
                    new CommandParameter() {ParameterName = "pIS_ACTIVE", Value = ob.IS_ACTIVE},
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

        public LookupTablesModel Select(long ID)
        {
            const string sp = "pkg_admin.lookup_table_select";
            try
            {
                var ob = new LookupTablesModel();

                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   new CommandParameter() {ParameterName = "pOption", Value = 3001},
                    new CommandParameter() {ParameterName = "pLOOKUP_TABLE_ID", Value = ID}
                }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.LOOKUP_TABLE_ID = (dr["LOOKUP_TABLE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LOOKUP_TABLE_ID"]);
                    ob.LK_TABLE_CODE = (dr["LK_TABLE_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LK_TABLE_CODE"]);
                    ob.LK_TABLE_NAME_EN = (dr["LK_TABLE_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LK_TABLE_NAME_EN"]);
                    ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);
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
