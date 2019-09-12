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
    public class LK_DYE_MTHDModel
    {
        public Int64 LK_DYE_MTHD_ID { get; set; }
        public string DYE_MTHD_NAME { get; set; }
        public string DYE_MTHD_SNAME { get; set; }
        public string IS_S_D_PART { get; set; }
        public string IS_DEFAULT { get; set; }
        public string IS_ACTIVE { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }

        public List<LK_DYE_MTHDModel> SelectAll()
        {
            string sp = "pkg_common.lk_dye_mthd_select";
            try
            {
                var obList = new List<LK_DYE_MTHDModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pLK_DYE_MTHD_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
              foreach (DataRow dr in ds.Tables[0].Rows)
               {
                            LK_DYE_MTHDModel ob = new LK_DYE_MTHDModel();
                            ob.LK_DYE_MTHD_ID = (dr["LK_DYE_MTHD_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_DYE_MTHD_ID"]);
                            ob.DYE_MTHD_NAME = (dr["DYE_MTHD_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DYE_MTHD_NAME"]);
                            ob.DYE_MTHD_SNAME = (dr["DYE_MTHD_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DYE_MTHD_SNAME"]);
                            ob.IS_S_D_PART = (dr["IS_S_D_PART"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_S_D_PART"]);
                            ob.IS_DEFAULT = (dr["IS_DEFAULT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_DEFAULT"]);
                            obList.Add(ob);
               }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public LK_DYE_MTHDModel Select(long ID)
        {
            string sp = "pkg_common.lk_dye_mthd_select";
            try
            {
                var ob = new LK_DYE_MTHDModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pLK_DYE_MTHD_ID", Value = ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3001 },
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
              foreach (DataRow dr in ds.Tables[0].Rows)
               {
                        ob.LK_DYE_MTHD_ID = (dr["LK_DYE_MTHD_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_DYE_MTHD_ID"]);
                        ob.DYE_MTHD_NAME = (dr["DYE_MTHD_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DYE_MTHD_NAME"]);
                        ob.DYE_MTHD_SNAME = (dr["DYE_MTHD_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DYE_MTHD_SNAME"]);
                        ob.IS_S_D_PART = (dr["IS_S_D_PART"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_S_D_PART"]);
                        ob.IS_DEFAULT = (dr["IS_DEFAULT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_DEFAULT"]);
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