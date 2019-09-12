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
    public class GMT_BNDL_CRD_PDATAModel
    {
        public Int64? GMT_BNDL_CRD_PDATA_ID { get; set; }
        public Int64? GMT_CUT_INFO_ID { get; set; }        
        public Int32? BUNDLE_NO { get; set; }
        public string BUNDLE_BARCODE { get; set; }
        public Int64? MC_ORDER_SIZE_ID { get; set; }
        public string RATIO_GRP_TEXT { get; set; }
        public string DYE_LOT_NO { get; set; }
        public Int64? GRD_SL1 { get; set; }
        public Int64? GRD_SL2 { get; set; }
        public Int64? QTY_IN_BNDL { get; set; }

        public Int64? BUNDLE_CARD_LIST_SL { get; set; }
        public string SIZE_CODE { get; set; }
        public Int64? NO_OF_BUNDLE { get; set; }



        public List<GMT_BNDL_CRD_PDATAModel> GetBundleCardList(Int64 pGMT_CUT_INFO_ID)
        {
            string sp = "pkg_cut_lay_chart.gmt_cut_info_select";
            try
            {
                var obList = new List<GMT_BNDL_CRD_PDATAModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pGMT_CUT_INFO_ID", Value =pGMT_CUT_INFO_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3005},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    GMT_BNDL_CRD_PDATAModel ob = new GMT_BNDL_CRD_PDATAModel();
                    
                    //ob.BUNDLE_NO = (dr["BUNDLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BUNDLE_NO"]);
                    //ob.MC_ORDER_SIZE_ID = (dr["MC_ORDER_SIZE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_ORDER_SIZE_ID"]);
                    //ob.RATIO_GRP_TEXT = (dr["RATIO_GRP_TEXT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["RATIO_GRP_TEXT"]);
                    
                    ob.BUNDLE_CARD_LIST_SL = (dr["BUNDLE_CARD_LIST_SL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["BUNDLE_CARD_LIST_SL"]);
                    ob.SIZE_CODE = (dr["SIZE_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SIZE_CODE"]);
                    ob.NO_OF_BUNDLE = (dr["NO_OF_BUNDLE"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["NO_OF_BUNDLE"]);
                    ob.QTY_IN_BNDL = (dr["QTY_IN_BNDL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_IN_BNDL"]);
                    
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public GMT_BNDL_CRD_PDATAModel GetBndlCardInfo(string pBUNDLE_BARCODE)
        {
            string sp = "pkg_cut_lay_chart.gmt_cut_info_select";
            try
            {
                var ob = new GMT_BNDL_CRD_PDATAModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pBUNDLE_BARCODE", Value =pBUNDLE_BARCODE},
                     new CommandParameter() {ParameterName = "pOption", Value =3008},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.GMT_BNDL_CRD_PDATA_ID = (dr["GMT_BNDL_CRD_PDATA_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_BNDL_CRD_PDATA_ID"]);
                    ob.BUNDLE_BARCODE = (dr["BUNDLE_BARCODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BUNDLE_BARCODE"]);
                    ob.BUNDLE_NO = (dr["BUNDLE_NO"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["BUNDLE_NO"]);                    
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