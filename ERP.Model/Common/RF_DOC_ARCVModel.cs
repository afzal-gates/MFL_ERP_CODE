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
    public class RF_DOC_ARCVModel
    {
        public Int64? RF_DOC_ARCV_ID { get; set; }
        public Int64? LK_DOC_TYPE_ID { get; set; }
        public string DOC_TYPE_NAME_EN { get; set; }
        public string DOC_REF_NO { get; set; }
        public string DOC_DESC { get; set; }
        public string DOC_PATH_URL { get; set; }
        public Int64? DISPLAY_ORDER { get; set; }
        public Int64? MC_BYR_ACC_ID { get; set; }
        public Int64? MC_BUYER_ID { get; set; }
        public Int64? MC_STYLE_H_EXT_ID { get; set; }
        public string MC_ORDER_H_ID_LST { get; set; }
        public Int64? CM_IMP_PI_H_ID { get; set; }
        public string STYLE_NO { get; set; }
        public string ORDER_NO { get; set; }
        
        public HttpPostedFileBase ATT_FILE { get; set; }


        //private List<RF_DOC_ARCVModel> _SlDocsModel = null;
        //public List<RF_DOC_ARCVModel> items
        //{
        //    get
        //    {
        //        if (_SlDocsModel == null)
        //        {
        //            _SlDocsModel = new List<RF_DOC_ARCVModel>();
        //        }
        //        return _SlDocsModel;
        //    }
        //    set
        //    {
        //        _SlDocsModel = value;
        //    }
        //}

        //public string FILE_TYPE { get; set; }
        //public string NEXT_SL_FILE { get; set; }
        //public string DOC_PATH_REF { get; set; }



        public string Save()
        {
            const string sp = "pkg_common.rf_doc_arcv_save";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pRF_DOC_ARCV_ID", Value = ob.RF_DOC_ARCV_ID},
                     new CommandParameter() {ParameterName = "pLK_DOC_TYPE_ID", Value = ob.LK_DOC_TYPE_ID},
                     new CommandParameter() {ParameterName = "pDOC_REF_NO", Value = ob.DOC_REF_NO},
                     new CommandParameter() {ParameterName = "pDOC_DESC", Value = ob.DOC_DESC},
                     new CommandParameter() {ParameterName = "pDOC_PATH_URL", Value = ob.DOC_PATH_URL},
                     new CommandParameter() {ParameterName = "pDISPLAY_ORDER", Value = ob.DISPLAY_ORDER},
                     new CommandParameter() {ParameterName = "pMC_BYR_ACC_ID", Value = ob.MC_BYR_ACC_ID},
                     new CommandParameter() {ParameterName = "pMC_BUYER_ID", Value = ob.MC_BUYER_ID},
                     new CommandParameter() {ParameterName = "pMC_STYLE_H_EXT_ID", Value = ob.MC_STYLE_H_EXT_ID},
                     new CommandParameter() {ParameterName = "pMC_ORDER_H_ID_LST", Value = ob.MC_ORDER_H_ID_LST},
                     new CommandParameter() {ParameterName = "pCM_IMP_PI_H_ID", Value = ob.CM_IMP_PI_H_ID},
                     
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     
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

        public string Delete()
        {
            const string sp = "pkg_common.rf_doc_arcv_delete";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pRF_DOC_ARCV_ID", Value = ob.RF_DOC_ARCV_ID},
                     new CommandParameter() {ParameterName = "pLK_DOC_TYPE_ID", Value = ob.LK_DOC_TYPE_ID},
                     new CommandParameter() {ParameterName = "pDOC_REF_NO", Value = ob.DOC_REF_NO},
                     new CommandParameter() {ParameterName = "pDOC_DESC", Value = ob.DOC_DESC},
                     new CommandParameter() {ParameterName = "pDOC_PATH_URL", Value = ob.DOC_PATH_URL},
                     new CommandParameter() {ParameterName = "pDISPLAY_ORDER", Value = ob.DISPLAY_ORDER},
                     new CommandParameter() {ParameterName = "pMC_BUYER_ID", Value = ob.MC_BUYER_ID},
                     new CommandParameter() {ParameterName = "pMC_STYLE_H_EXT_ID", Value = ob.MC_STYLE_H_EXT_ID},
                     new CommandParameter() {ParameterName = "pMC_ORDER_H_ID_LST", Value = ob.MC_ORDER_H_ID_LST},
                     new CommandParameter() {ParameterName = "pCM_IMP_PI_H_ID", Value = ob.CM_IMP_PI_H_ID},
                     
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     
                     new CommandParameter() {ParameterName = "pOption", Value = 4000},
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

        public object SelectAll(Int64 pageNumber, Int64 pageSize, Int64? pMC_BYR_ACC_ID, Int64? pMC_STYLE_H_EXT_ID, string pDOC_REF_NO, string pSTYLE_NO, string pORDER_NO)
        {
            string sp = "pkg_common.rf_doc_arcv_select";
            try
            {
                Int64 vTotalRec = 0;
                var obList = new List<RF_DOC_ARCVModel>();
                var obj = new RF_PAGERModel();

                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_STYLE_H_EXT_ID", Value = pMC_STYLE_H_EXT_ID},
                     new CommandParameter() {ParameterName = "pMC_BYR_ACC_ID", Value = pMC_BYR_ACC_ID},
                     new CommandParameter() {ParameterName = "pDOC_REF_NO", Value =pDOC_REF_NO},
                     new CommandParameter() {ParameterName = "pSTYLE_NO", Value =pSTYLE_NO},
                     new CommandParameter() {ParameterName = "pORDER_NO", Value =pORDER_NO},

                     new CommandParameter() {ParameterName = "pageNumber", Value =pageNumber},
                     new CommandParameter() {ParameterName = "pageSize", Value =pageSize},
                     
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    RF_DOC_ARCVModel ob = new RF_DOC_ARCVModel();
                    ob.RF_DOC_ARCV_ID = (dr["RF_DOC_ARCV_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_DOC_ARCV_ID"]);
                    ob.LK_DOC_TYPE_ID = (dr["LK_DOC_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_DOC_TYPE_ID"]);

                    ob.DOC_TYPE_NAME_EN = (dr["DOC_TYPE_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DOC_TYPE_NAME_EN"]);
                    ob.STYLE_NO = (dr["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO"]);
                    ob.ORDER_NO = (dr["ORDER_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_NO"]);

                    ob.DOC_REF_NO = (dr["DOC_REF_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DOC_REF_NO"]);
                    ob.DOC_DESC = (dr["DOC_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DOC_DESC"]);
                    ob.DOC_PATH_URL = (dr["DOC_PATH_URL"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DOC_PATH_URL"]);
                    ob.DISPLAY_ORDER = (dr["DISPLAY_ORDER"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DISPLAY_ORDER"]);
                    ob.MC_BYR_ACC_ID = (dr["MC_BYR_ACC_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BYR_ACC_ID"]);
                    ob.MC_BUYER_ID = (dr["MC_BUYER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BUYER_ID"]);
                    ob.MC_STYLE_H_EXT_ID = (dr["MC_STYLE_H_EXT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_H_EXT_ID"]);
                    ob.MC_ORDER_H_ID_LST = (dr["MC_ORDER_H_ID_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MC_ORDER_H_ID_LST"]);
                    ob.CM_IMP_PI_H_ID = (dr["CM_IMP_PI_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CM_IMP_PI_H_ID"]);

                    if (vTotalRec < 1)
                    {
                        vTotalRec = (dr["TOTAL_REC"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["TOTAL_REC"]);
                    }

                    obList.Add(ob);
                }

                obj.total = vTotalRec;
                obj.data = obList;
                return obj;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public RF_DOC_ARCVModel Select(long ID)
        {
            string sp = "Select_RF_DOC_ARCV";
            try
            {
                var ob = new RF_DOC_ARCVModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pRF_DOC_ARCV_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.RF_DOC_ARCV_ID = (dr["RF_DOC_ARCV_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_DOC_ARCV_ID"]);
                    ob.LK_DOC_TYPE_ID = (dr["LK_DOC_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_DOC_TYPE_ID"]);
                    ob.DOC_REF_NO = (dr["DOC_REF_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DOC_REF_NO"]);
                    ob.DOC_DESC = (dr["DOC_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DOC_DESC"]);
                    ob.DOC_PATH_URL = (dr["DOC_PATH_URL"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DOC_PATH_URL"]);
                    ob.DISPLAY_ORDER = (dr["DISPLAY_ORDER"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DISPLAY_ORDER"]);
                    ob.MC_BUYER_ID = (dr["MC_BUYER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BUYER_ID"]);
                    ob.MC_STYLE_H_EXT_ID = (dr["MC_STYLE_H_EXT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_H_EXT_ID"]);
                    ob.MC_ORDER_H_ID_LST = (dr["MC_ORDER_H_ID_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MC_ORDER_H_ID_LST"]);
                    ob.CM_IMP_PI_H_ID = (dr["CM_IMP_PI_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CM_IMP_PI_H_ID"]);
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