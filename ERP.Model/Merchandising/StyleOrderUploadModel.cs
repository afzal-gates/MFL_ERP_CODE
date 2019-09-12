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
    public class StyleOrderUploadModel
    {
        public HttpPostedFileBase ATT_FILE { get; set; }
        public Int32 UPLOAD_FORMAT_ID { get; set; }
        public Int64 SHOW_FROM_REC_NO { get; set; }
        public Int32 MAX_REC_SHOW { get; set; }
        

        public string BatchSave(string fileNameWithPath)
        {
            const string sp = "pkg_common.style_order_data_upload";
            string jsonStr = "{";
            var ob = this;
            var i = 1;

            string[] lines = System.IO.File.ReadAllLines(fileNameWithPath);

            var STYLE_ORDER_FORMAT01_XML = new System.Text.StringBuilder();
            var STYLE_ORDER_FORMAT02_XML = new System.Text.StringBuilder();

            STYLE_ORDER_FORMAT01_XML.Append("<trans>");
            STYLE_ORDER_FORMAT02_XML.Append("<trans>");

            string vShipDT = "";
            foreach (string line in lines)
            {
                string vLine = line.Replace("Null,", ",");
                string[] vResult = vLine.Split(',');

                if (ob.UPLOAD_FORMAT_ID == 1)
                {
                    STYLE_ORDER_FORMAT01_XML.AppendLine();
                    STYLE_ORDER_FORMAT01_XML.Append(" <row ");

                    if (vResult[0].ToUpper() != "NULL" && vResult[0]!="")
                        STYLE_ORDER_FORMAT01_XML.Append(" BYR_ACC_GRP_NAME_EN=\"" + vResult[0].Replace("&", "_And_") + "\"");
                    else if (vResult[1].ToUpper() != "NULL" && vResult[1] != "")
                        STYLE_ORDER_FORMAT01_XML.Append(" BYR_ACC_GRP_NAME_EN=\"" + vResult[1].Replace("&", "_And_") + "\"");
                    else
                        STYLE_ORDER_FORMAT01_XML.Append(" BYR_ACC_GRP_NAME_EN=\"" + vResult[2].Replace("&", "_And_") + "\"");

                    if (vResult[1].ToUpper() != "NULL" && vResult[1] != "")
                        STYLE_ORDER_FORMAT01_XML.Append(" BYR_ACC_NAME_EN=\"" + vResult[1].Replace("&", "_And_") + "\"");
                    else
                        STYLE_ORDER_FORMAT01_XML.Append(" BYR_ACC_NAME_EN=\"" + vResult[2].Replace("&", "_And_") + "\"");

                    STYLE_ORDER_FORMAT01_XML.Append(" BUYER_NAME_EN=\"" + vResult[2].Replace("&", "_And_") + "\"");
                    STYLE_ORDER_FORMAT01_XML.Append(" STYLE_NO=\"" + vResult[3].Replace("&", "_And_") + "\"");
                    STYLE_ORDER_FORMAT01_XML.Append(" STYLE_DESC=\"" + vResult[4].Replace("&", "_And_") + "\"");
                    STYLE_ORDER_FORMAT01_XML.Append(" STYLE_MOU_CODE=\"" + vResult[5] + "\"");
                    STYLE_ORDER_FORMAT01_XML.Append(" HAS_SET=\"" + vResult[6] + "\"");
                    STYLE_ORDER_FORMAT01_XML.Append(" PCS_PER_PACK=\"" + vResult[7] + "\"");
                    STYLE_ORDER_FORMAT01_XML.Append(" ORDER_NO=\"" + vResult[8].Replace("&", "_And_") + "\"");

                    vShipDT = string.Format("{0:yyyy-MMM-dd}", DateTime.Parse(vResult[10]));

                    STYLE_ORDER_FORMAT01_XML.Append(" ORD_CONF_DT=\"" + string.Format("{0:yyyy-MMM-dd}", DateTime.Parse(vResult[9])) + "\"");
                    STYLE_ORDER_FORMAT01_XML.Append(" SHIP_DT=\"" + string.Format("{0:yyyy-MMM-dd}", DateTime.Parse(vResult[10])) + "\"");
                    STYLE_ORDER_FORMAT01_XML.Append(" SHIP_COUNTRY=\"" + vResult[11] + "\"");
                    STYLE_ORDER_FORMAT01_XML.Append(" REVISION_NO=\"" + vResult[12] + "\"");

                    if (vResult[13].ToUpper() != "NULL" && vResult[13] != "")
                        STYLE_ORDER_FORMAT01_XML.Append(" REVISION_DT=\"" + string.Format("{0:yyyy-MMM-dd}", DateTime.Parse(vResult[13])) + "\"");
                    else
                        STYLE_ORDER_FORMAT01_XML.Append(" REVISION_DT=\"" + "" + "\"");

                    STYLE_ORDER_FORMAT01_XML.Append(" REV_REASON=\"" + vResult[14].Replace("&", "_And_") + "\"");
                    STYLE_ORDER_FORMAT01_XML.Append(" ITEM_CAT_NAME_EN=\"" + vResult[15].Replace("&", "_And_") + "\"");
                    STYLE_ORDER_FORMAT01_XML.Append(" ITEM_NAME_EN=\"" + vResult[16].Replace("&", "_And_") + "\"");
                    STYLE_ORDER_FORMAT01_XML.Append(" PARENT_ITEM_NAME_EN=\"" + vResult[17].Replace("&", "_And_") + "\"");
                    STYLE_ORDER_FORMAT01_XML.Append(" GMT_TYPE=\"" + vResult[18] + "\"");
                    STYLE_ORDER_FORMAT01_XML.Append(" COLOR_NAME_EN=\"" + vResult[19].Replace("&", "_And_") + "\"");
                    STYLE_ORDER_FORMAT01_XML.Append(" SIZE_CODE=\"" + vResult[20] + "\"");
                    STYLE_ORDER_FORMAT01_XML.Append(" SIZE_QTY=\"" + vResult[21] + "\"");
                    STYLE_ORDER_FORMAT01_XML.Append(" />");
                }
                else if (ob.UPLOAD_FORMAT_ID == 2)
                {
                    STYLE_ORDER_FORMAT02_XML.AppendLine();
                    STYLE_ORDER_FORMAT02_XML.Append(" <row ");

                    STYLE_ORDER_FORMAT02_XML.Append(" STYLE_NO=\"" + vResult[3].Replace("&", "_And_") + "\"");
                    STYLE_ORDER_FORMAT02_XML.Append(" ORDER_NO=\"" + vResult[4].Replace("&", "_And_") + "\"");
                    STYLE_ORDER_FORMAT02_XML.Append(" ITEM_CAT_NAME_EN=\"" + vResult[5].Replace("&", "_And_") + "\"");
                    STYLE_ORDER_FORMAT02_XML.Append(" FABRIC_DESC=\"" + vResult[6].Replace("&", "_And_") + "\"");
                    STYLE_ORDER_FORMAT02_XML.Append(" />");
                }
            }

            STYLE_ORDER_FORMAT01_XML.AppendLine();
            STYLE_ORDER_FORMAT02_XML.AppendLine();

            STYLE_ORDER_FORMAT01_XML.Append("</trans>");
            STYLE_ORDER_FORMAT02_XML.Append("</trans>");
            
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pUPLOAD_FORMAT_ID", Value = ob.UPLOAD_FORMAT_ID},
                     new CommandParameter() {ParameterName = "pSTYLE_ORDER_FORMAT01", Value = STYLE_ORDER_FORMAT01_XML},
                     new CommandParameter() {ParameterName = "pSTYLE_ORDER_FORMAT02", Value = STYLE_ORDER_FORMAT02_XML},                     
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

        //public List<StyleOrderUploadModel> SelectAll()
        //{
        //    string sp = "pkg_common.rf_actn_type_select";
        //    try
        //    {
        //        var obList = new List<RF_ACTN_TYPEModel>();
        //        OraDatabase db = new OraDatabase();
        //        var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
        //        {
        //             new CommandParameter() {ParameterName = "pRF_ACTN_TYPE_ID", Value =0},
        //             new CommandParameter() {ParameterName = "pOption", Value =3000},
        //             new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
        //         }, sp);
        //        foreach (DataRow dr in ds.Tables[0].Rows)
        //        {
        //            RF_ACTN_TYPEModel ob = new RF_ACTN_TYPEModel();
        //            ob.RF_ACTN_TYPE_ID = (dr["RF_ACTN_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_ACTN_TYPE_ID"]);
        //            ob.ACTN_TYPE_CODE = (dr["ACTN_TYPE_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ACTN_TYPE_CODE"]);
        //            ob.ACTN_TYPE_NAME = (dr["ACTN_TYPE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ACTN_TYPE_NAME"]);
        //            ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);
        //            ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
        //            ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
        //            ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
        //            ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);
        //            ob.ACTN_TYPE_SNAME = (dr["ACTN_TYPE_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ACTN_TYPE_SNAME"]);
        //            ob.PAGE_URL = (dr["PAGE_URL"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PAGE_URL"]);
        //            obList.Add(ob);
        //        }
        //        return obList;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        
    }
}