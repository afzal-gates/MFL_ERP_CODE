using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace ERP.Model
{
    public class HrSectionModel
    {
        
        public Int64 HR_SECTION_ID { get; set; }
        public Int64 PARENT_HR_SECTION_ID { get; set; }

        public string SECTION_CODE { get; set; }
        public string SECTION_NAME_EN { get; set; }
        public string SECTION_NAME_BN { get; set; }
        public string SECTION_DESC { get; set; }
        public string SECTION_PREFIX { get; set; }
        public Int64 SECTION_LEVEL { get; set; }
        public string IS_LEAF { get; set; }
        public string IS_ACTIVE { get; set; }

        public DateTime? CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime? LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }
        public Int64 LAST_UPDATE_LOGIN { get; set; }
        public Int64 VERSION_NO { get; set; }

        public List<HrSectionModel> SelectAll()
        {
            const string sp = "pkg_admin.hr_section_select";

            try
            {
                var obList = new List<HrSectionModel>();

                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
                    new CommandParameter() {ParameterName = "pOption", Value = 3000}
                }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    HrSectionModel ob = new HrSectionModel();
                    ob.HR_SECTION_ID = (dr["HR_SECTION_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_SECTION_ID"]);
                    ob.PARENT_HR_SECTION_ID = (dr["PARENT_HR_SECTION_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PARENT_HR_SECTION_ID"]);
                    ob.SECTION_CODE = (dr["SECTION_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SECTION_CODE"]);
                    ob.SECTION_NAME_EN = (dr["SECTION_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SECTION_NAME_EN"]);
                    ob.SECTION_NAME_BN = (dr["SECTION_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SECTION_NAME_BN"]);
                    ob.SECTION_DESC = (dr["SECTION_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SECTION_DESC"]);
                    ob.SECTION_PREFIX = (dr["SECTION_PREFIX"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SECTION_PREFIX"]);
                    ob.SECTION_LEVEL = (dr["SECTION_LEVEL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SECTION_LEVEL"]);
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

        public SelectList getParentList()
        {
            try
            {
                var sectionSelectList = new List<SelectListItem>();

                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteSQLStatement("SELECT HR_SECTION_ID, SECTION_NAME_EN FROM HR_SECTION WHERE PARENT_HR_SECTION_ID=0");
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    sectionSelectList.Add(new SelectListItem { Text = dr["SECTION_NAME_EN"].ToString(), Value = dr["HR_SECTION_ID"].ToString() });

                }
                return new SelectList(sectionSelectList, "Value", "Text", "id");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public SelectList getChildList(string hr_section_id)
        {
            try
            {
                var sectionSelectList = new List<SelectListItem>();

                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteSQLStatement("SELECT HR_SECTION_ID, SECTION_NAME_EN FROM HR_SECTION WHERE PARENT_HR_SECTION_ID='" + hr_section_id + "'");
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    sectionSelectList.Add(new SelectListItem { Text = dr["SECTION_NAME_EN"].ToString(), Value = dr["HR_SECTION_ID"].ToString() });
                }
                return new SelectList(sectionSelectList, "Value", "Text", "id");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }




    }
}
