using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Model.Purchase
{
    public class SCM_SUP_ITEMModel
    {
        public Int64 SCM_SUP_ITEM_ID { get; set; }
        public Int64 SCM_SUPPLIER_ID { get; set; }
        public Int64 INV_ITEM_CAT_ID { get; set; }
        public Int64 INV_ITEM_SUB_CAT_ID { get; set; }

        public string IS_ACTIVE { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }
        public string SUP_TRD_NAME_EN { get; set; }


        public List<SCM_SUP_ITEMModel> SelectAll(Int64? pINV_ITEM_CAT_ID)
        {
            string sp = "pkg_pur_supplier.scm_supplier_select";
            try
            {
                var obList = new List<SCM_SUP_ITEMModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pSCM_SUPPLIER_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    SCM_SUP_ITEMModel ob = new SCM_SUP_ITEMModel();
                    ob.SCM_SUPPLIER_ID = (dr["SCM_SUPPLIER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_SUPPLIER_ID"]);
                    ob.SUP_TRD_NAME_EN = (dr["SUP_TRD_NAME_EN"] == DBNull.Value) ? String.Empty : Convert.ToString(dr["SUP_TRD_NAME_EN"]);
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }        
    }
}
