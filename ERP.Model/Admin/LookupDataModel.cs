using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.Data;
using System.Web;

namespace ERP.Model
{
    public class LookupDataModel //: ILookupDataModel
    {
        public Int64 LOOKUP_DATA_ID { get; set; }
        public Int64 LOOKUP_TABLE_ID { get; set; }

        [Display(Name="Data Code")]
        //[Required(ErrorMessage="Please input data code")]
        public string LK_DATA_CODE { get; set; }

        [Display(Name = "Data Name")]
        [Required(ErrorMessage = "Please input data name")]
        public string LK_DATA_NAME_EN { get; set; }
        public string LK_DATA_NAME_BN { get; set; }
        
        [Display(Name = "Description")]
        public string LK_DATA_DESC { get; set; }

        [Display(Name = "Param. Value")]
        [Required(ErrorMessage = "Please input parameter value")]
        public Decimal LK_DATA_PARAM_VALUE { get; set; }

        [Display(Name = "Active")]
        [Required(ErrorMessage = "Please select an option")]
        public string IS_ACTIVE { get; set; }
        public Int64? SC_USER_ID { get; set; }


        public DateTime? CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime? LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }
        public Int64 LAST_UPDATE_LOGIN { get; set; }
        public Int64 VERSION_NO { get; set; }
        public string DATA_PREFIX { get; set; }
        
        
        public List<LookupDataModel> LookupListData(Int64? lookupTableId)
        {
            string sp = "pkg_admin.lookup_data_select";
            try
            {
                var obList = new List<LookupDataModel>();

                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   new CommandParameter() {ParameterName = "pOption", Value = 3001},
                    new CommandParameter() {ParameterName = "pLOOKUP_TABLE_ID", Value = lookupTableId}
                }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    LookupDataModel ob = new LookupDataModel();
                    ob.LOOKUP_DATA_ID = (dr["LOOKUP_DATA_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LOOKUP_DATA_ID"]);
                    ob.LOOKUP_TABLE_ID = (dr["LOOKUP_TABLE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LOOKUP_TABLE_ID"]);
                    ob.LK_DATA_CODE = (dr["LK_DATA_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LK_DATA_CODE"]);
                    ob.LK_DATA_NAME_EN = (dr["LK_DATA_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LK_DATA_NAME_EN"]);
                    ob.LK_DATA_NAME_BN = (dr["LK_DATA_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LK_DATA_NAME_BN"]);
                    ob.LK_DATA_PARAM_VALUE = (dr["LK_DATA_PARAM_VALUE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["LK_DATA_PARAM_VALUE"]);
                    ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);
                    ob.DATA_PREFIX = (dr["DATA_PREFIX"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DATA_PREFIX"]);
                    ob.VERSION_NO = (dr["VERSION_NO"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["VERSION_NO"]);
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<LookupDataModel> SelectLookupDatas(long lookupTableID)
        {
            string sp = "pkg_admin.lookup_data_select";
            try
            {
                var obList = new List<LookupDataModel>();

                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   new CommandParameter() {ParameterName = "pOption", Value = 3001},
                    new CommandParameter() {ParameterName = "pLOOKUP_TABLE_ID", Value = lookupTableID}
                }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    LookupDataModel ob = new LookupDataModel();
                    ob.LOOKUP_DATA_ID = (dr["LOOKUP_DATA_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LOOKUP_DATA_ID"]);
                    ob.LOOKUP_TABLE_ID = (dr["LOOKUP_TABLE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LOOKUP_TABLE_ID"]);
                    ob.LK_DATA_CODE = (dr["LK_DATA_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LK_DATA_CODE"]);
                    ob.LK_DATA_NAME_EN = (dr["LK_DATA_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LK_DATA_NAME_EN"]);
                    ob.LK_DATA_NAME_BN = (dr["LK_DATA_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LK_DATA_NAME_BN"]);
                    ob.LK_DATA_PARAM_VALUE = (dr["LK_DATA_PARAM_VALUE"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_DATA_PARAM_VALUE"]);
                    ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);
                    ob.DATA_PREFIX = (dr["DATA_PREFIX"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DATA_PREFIX"]);
                    
                    obList.Add(ob);
                }

                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public LookupDataModel Select(long ID)
        {
            const string sp = "pkg_admin.lookup_data_select";
            try
            {
                var ob = new LookupDataModel();

                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   new CommandParameter() {ParameterName = "pOption", Value = 3002},
                    new CommandParameter() {ParameterName = "pLOOKUP_DATA_ID", Value = ID},
                    new CommandParameter() {ParameterName = "pLOOKUP_TABLE_ID", Value = 0}
                    //new CommandParameter() {ParameterName = "pMsg", Value = 500, Direction = ParameterDirection.Output}
                }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.LOOKUP_DATA_ID = (dr["LOOKUP_DATA_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LOOKUP_DATA_ID"]);
                    ob.LOOKUP_TABLE_ID = (dr["LOOKUP_TABLE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LOOKUP_TABLE_ID"]);
                    ob.LK_DATA_CODE = (dr["LK_DATA_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LK_DATA_CODE"]);
                    ob.LK_DATA_NAME_EN = (dr["LK_DATA_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LK_DATA_NAME_EN"]);
                    ob.LK_DATA_NAME_BN = (dr["LK_DATA_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LK_DATA_NAME_BN"]);
                    ob.LK_DATA_PARAM_VALUE = (dr["LK_DATA_PARAM_VALUE"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_DATA_PARAM_VALUE"]);
                    ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);
                }

                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<LookupDataModel> GetClcfChlnType()
        {
            string sp = "pkg_knit_collar_cuff.knt_clcf_str_rcv_select";
            try
            {
                var obList = new List<LookupDataModel>();

                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
                    new CommandParameter() {ParameterName = "pSC_USER_ID", Value = HttpContext.Current.Session["multiScUserId"]}, 
                    new CommandParameter() {ParameterName = "pOption", Value = 3004}
                }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    LookupDataModel ob = new LookupDataModel();
                    ob.LOOKUP_DATA_ID = (dr["LOOKUP_DATA_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LOOKUP_DATA_ID"]);
                    ob.LOOKUP_TABLE_ID = (dr["LOOKUP_TABLE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LOOKUP_TABLE_ID"]);
                    ob.LK_DATA_CODE = (dr["LK_DATA_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LK_DATA_CODE"]);
                    ob.LK_DATA_NAME_EN = (dr["LK_DATA_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LK_DATA_NAME_EN"]);
                    ob.LK_DATA_PARAM_VALUE = (dr["LK_DATA_PARAM_VALUE"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_DATA_PARAM_VALUE"]);
                    ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);

                    ob.SC_USER_ID = (dr["SC_USER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SC_USER_ID"]);
                    
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
            const string sp = "pkg_admin.lookup_data_insert";
            string vMsg = "";

            OraDatabase db = new OraDatabase();
            try
            {
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                    new CommandParameter() {ParameterName = "pLOOKUP_TABLE_ID", Value = this.LOOKUP_TABLE_ID},
                    new CommandParameter() {ParameterName = "pLK_DATA_CODE", Value = this.LK_DATA_CODE},
                    new CommandParameter() {ParameterName = "pLK_DATA_NAME_EN", Value = this.LK_DATA_NAME_EN},
                    new CommandParameter() {ParameterName = "pLK_DATA_NAME_BN", Value = this.LK_DATA_NAME_BN},
                    new CommandParameter() {ParameterName = "pLK_DATA_PARAM_VALUE", Value = this.LK_DATA_PARAM_VALUE},
                    new CommandParameter() {ParameterName = "pIS_ACTIVE", Value = this.IS_ACTIVE == null ? "N" : "Y"},
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
            const string sp = "pkg_admin.lookup_data_update";
            string vMsg = "";
            OraDatabase db = new OraDatabase();
            try
            {
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                    new CommandParameter() {ParameterName = "pLOOKUP_DATA_ID", Value = this.LOOKUP_DATA_ID},
                    new CommandParameter() {ParameterName = "pLOOKUP_TABLE_ID", Value = this.LOOKUP_TABLE_ID},
                    new CommandParameter() {ParameterName = "pLK_DATA_CODE", Value = this.LK_DATA_CODE==null? "A" : this.LK_DATA_CODE},
                    new CommandParameter() {ParameterName = "pLK_DATA_NAME_EN", Value = this.LK_DATA_NAME_EN},
                    new CommandParameter() {ParameterName = "pLK_DATA_NAME_BN", Value = this.LK_DATA_NAME_BN},
                    new CommandParameter() {ParameterName = "pLK_DATA_PARAM_VALUE", Value = this.LK_DATA_PARAM_VALUE},
                    new CommandParameter() {ParameterName = "pIS_ACTIVE", Value = this.IS_ACTIVE == null ? "N" : "Y"},
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


        public int? MC_BYR_ACC_GRP_ID { get; set; }
    }

   

     //public SelectList GetYesNoList()
     //   {
     //       //IEnumerable<SelectListItem> stateList = (from m in _db.mstrstates where m.bstatus == true select m).AsEnumerable().Select(m => new SelectListItem() { Text = m.vstate, Value = m.istateid.ToString() });

     //       List<SelectListItem> stateList = new List<SelectListItem>();
            
     //       stateList.Add(new SelectListItem { Text = "Yes", Value = "Y" });
     //       stateList.Add(new SelectListItem { Text = "No", Value = "N" });
            
     //       return new SelectList(stateList, "Value", "Text");
     //   }
}
