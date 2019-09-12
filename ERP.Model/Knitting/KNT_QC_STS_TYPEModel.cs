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
    public class KNT_QC_STS_TYPEModel
    {
        public Int64 KNT_QC_STS_TYPE_ID { get; set; }
        public string QC_STS_TYP_CODE { get; set; }
        public string QC_STS_TYP_NAME { get; set; }
        public string RGB_COL_CODE { get; set; }
        public string IS_ACTIVE { get; set; }



        public List<KNT_QC_STS_TYPEModel> GetQcStatusTypeList(string pKNT_QC_STS_TYPE_ID_LST)
        {
            string sp = "pkg_knit_common.knt_qc_sts_type_select";
            try
            {
                var obList = new List<KNT_QC_STS_TYPEModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_QC_STS_TYPE_ID_LST", Value = pKNT_QC_STS_TYPE_ID_LST},
                     new CommandParameter() {ParameterName = "pOption", Value = 3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    KNT_QC_STS_TYPEModel ob = new KNT_QC_STS_TYPEModel();
                    ob.KNT_QC_STS_TYPE_ID = (dr["KNT_QC_STS_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_QC_STS_TYPE_ID"]);
                    ob.QC_STS_TYP_CODE = (dr["QC_STS_TYP_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["QC_STS_TYP_CODE"]);
                    ob.QC_STS_TYP_NAME = (dr["QC_STS_TYP_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["QC_STS_TYP_NAME"]);
                    ob.RGB_COL_CODE = (dr["RGB_COL_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["RGB_COL_CODE"]);
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

        public KNT_QC_STS_TYPEModel Select(long ID)
        {
            string sp = "Select_KNT_QC_STS_TYPE";
            try
            {
                var ob = new KNT_QC_STS_TYPEModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_QC_STS_TYPE_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.KNT_QC_STS_TYPE_ID = (dr["KNT_QC_STS_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_QC_STS_TYPE_ID"]);
                    ob.QC_STS_TYP_CODE = (dr["QC_STS_TYP_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["QC_STS_TYP_CODE"]);
                    ob.QC_STS_TYP_NAME = (dr["QC_STS_TYP_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["QC_STS_TYP_NAME"]);
                    ob.RGB_COL_CODE = (dr["RGB_COL_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["RGB_COL_CODE"]);
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