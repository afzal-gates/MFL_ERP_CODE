using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ERP.Model.Purchase
{
    public class SCM_SUP_CERTModel
    {
        public Int64 SCM_SUP_CERT_ID { get; set; }
        public Int64 SCM_SUPPLIER_ID { get; set; }
        public Int64 RF_AUD_CERT_TYPE_ID { get; set; }

        public string CERT_TYPE_TITLE { get; set; }
        public string DOC_PATH { get; set; }
        public DateTime EFFECT_FROM { get; set; }
        public DateTime EXPIRED_ON { get; set; }
        public string IS_ACTIVE { get; set; }
        public string IS_EXISTS { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }

        public HttpPostedFileBase DOC_PATH_FILE { get; set; }

        public List<SCM_SUP_CERTModel> Select(Int64? ID)
        {
            string sp = "pkg_pur_supplier.scm_sup_certificate_select";
            try
            {
                var ob = new SCM_SUP_CERTModel();
                var List = new List<SCM_SUP_CERTModel>();

                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pSCM_SUPPLIER_ID", Value =ID==null?0:ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob = new SCM_SUP_CERTModel();
                    ob.SCM_SUPPLIER_ID = (dr["SCM_SUPPLIER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_SUPPLIER_ID"]);
                    ob.RF_AUD_CERT_TYPE_ID = (dr["RF_AUD_CERT_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_AUD_CERT_TYPE_ID"]);
                    ob.SCM_SUP_CERT_ID = (dr["SCM_SUP_CERT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_SUP_CERT_ID"]);
                    ob.CERT_TYPE_TITLE = (dr["CERT_TYPE_TITLE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CERT_TYPE_TITLE"]);
                    ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);
                    ob.DOC_PATH = (dr["DOC_PATH"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DOC_PATH"]);
                    ob.IS_EXISTS = (dr["IS_EXISTS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_EXISTS"]);
                    ob.EFFECT_FROM = (dr["EFFECT_FROM"] == DBNull.Value) ? DateTime.MinValue : Convert.ToDateTime(dr["EFFECT_FROM"]);
                    ob.EXPIRED_ON = (dr["EXPIRED_ON"] == DBNull.Value) ? DateTime.MinValue : Convert.ToDateTime(dr["EXPIRED_ON"]);
                    List.Add(ob);
                }
                return List;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
