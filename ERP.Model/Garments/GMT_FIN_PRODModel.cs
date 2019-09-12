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
    public class GMT_FIN_PRODModel
    {
        public Int64 GMT_FIN_PROD_ID { get; set; }
        public DateTime PROD_DT { get; set; }
        public Int64 HR_PROD_FLR_ID { get; set; }
        public Int64 RF_GMT_FP_TYPE_ID { get; set; }


        public Int64? HRLY_TGT_QTY { get; set; }
        public Int64? OT_TGT_HR { get; set; }
        public Int64? OT_TGT_QTY { get; set; }
        public Int64? QTY_MOU_ID { get; set; }
        public Int64? H1_PRD_QTY { get; set; }
        public Int64? H2_PRD_QTY { get; set; }
        public Int64? H3_PRD_QTY { get; set; }
        public Int64? H4_PRD_QTY { get; set; }
        public Int64? H5_PRD_QTY { get; set; }
        public Int64? H6_PRD_QTY { get; set; }
        public Int64? H7_PRD_QTY { get; set; }
        public Int64? H8_PRD_QTY { get; set; }
        public Int64? OT_ACT_PRD_QTY { get; set; }
        public string REMARKS { get; set; }
        public string XML { get; set; }

        public string Save()
        {
            const string sp = "pkg_gmt_fin.gmt_fin_prod_insert";
            string jsonStr="{";
            var ob = this;
            var i = 1;
            try
             {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pXML", Value = ob.XML},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
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
                         i++ ;
                 }
                 return jsonStr;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<GMT_FIN_PRODModel> SelectAll(string pHR_PROD_FLR_ID_LST, DateTime? pPROD_DT)
        {
            string sp = "pkg_gmt_fin.gmt_fin_prod_select";
            try
            {
                var obList = new List<GMT_FIN_PRODModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pHR_PROD_FLR_ID_LST", Value =pHR_PROD_FLR_ID_LST},
                     new CommandParameter() {ParameterName = "pPROD_DT", Value =pPROD_DT??DateTime.Now.Date},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
              foreach (DataRow dr in ds.Tables[0].Rows)
               {
                            GMT_FIN_PRODModel ob = new GMT_FIN_PRODModel();
                            ob.GMT_FIN_PROD_ID = (dr["GMT_FIN_PROD_ID"] == DBNull.Value) ? -1 : Convert.ToInt64(dr["GMT_FIN_PROD_ID"]);
                            ob.PROD_DT = (dr["PROD_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["PROD_DT"]);
                            ob.HR_PROD_FLR_ID = (dr["HR_PROD_FLR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_PROD_FLR_ID"]);
                            ob.RF_GMT_FP_TYPE_ID = (dr["RF_GMT_FP_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_GMT_FP_TYPE_ID"]);

                            ob.HR_PROD_FLR_ID_SPAN = (dr["HR_PROD_FLR_ID_SPAN"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_PROD_FLR_ID_SPAN"]);
                            ob.HR_PROD_FLR_ID_SL = (dr["HR_PROD_FLR_ID_SL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_PROD_FLR_ID_SL"]);

                            if (dr["HRLY_TGT_QTY"] != DBNull.Value)
                            {
                                ob.HRLY_TGT_QTY = Convert.ToInt64(dr["HRLY_TGT_QTY"]);
                            }

                            if (dr["OT_TGT_HR"] != DBNull.Value)
                            {
                                ob.OT_TGT_HR = Convert.ToInt64(dr["OT_TGT_HR"]);
                            }

                            if (dr["OT_TGT_QTY"] != DBNull.Value)
                            {
                                ob.OT_TGT_QTY = Convert.ToInt64(dr["OT_TGT_QTY"]);
                            }
                            if (dr["QTY_MOU_ID"] != DBNull.Value)
                            {
                                ob.QTY_MOU_ID = Convert.ToInt64(dr["QTY_MOU_ID"]);
                            }

                            if (dr["H1_PRD_QTY"] != DBNull.Value)
                            {
                                ob.H1_PRD_QTY = Convert.ToInt64(dr["H1_PRD_QTY"]);
                            }

                            if (dr["H2_PRD_QTY"] != DBNull.Value)
                            {
                                ob.H2_PRD_QTY = Convert.ToInt64(dr["H2_PRD_QTY"]);
                            }
                            if (dr["H3_PRD_QTY"] != DBNull.Value)
                            {
                                ob.H3_PRD_QTY = Convert.ToInt64(dr["H3_PRD_QTY"]);
                            }
                            if (dr["H4_PRD_QTY"] != DBNull.Value)
                            {
                                ob.H4_PRD_QTY = Convert.ToInt64(dr["H4_PRD_QTY"]);
                            }

                            if (dr["H5_PRD_QTY"] != DBNull.Value)
                            {
                                ob.H5_PRD_QTY = Convert.ToInt64(dr["H5_PRD_QTY"]);
                            }

                            if (dr["H6_PRD_QTY"] != DBNull.Value)
                            {
                                ob.H6_PRD_QTY = Convert.ToInt64(dr["H6_PRD_QTY"]);
                            }
                            if (dr["H7_PRD_QTY"] != DBNull.Value)
                            {
                                ob.H7_PRD_QTY = Convert.ToInt64(dr["H7_PRD_QTY"]);
                            }

                            if (dr["H8_PRD_QTY"] != DBNull.Value)
                            {
                                ob.H8_PRD_QTY = Convert.ToInt64(dr["H8_PRD_QTY"]);
                            }
                            if (dr["OT_ACT_PRD_QTY"] != DBNull.Value)
                            {
                                ob.OT_ACT_PRD_QTY = Convert.ToInt64(dr["OT_ACT_PRD_QTY"]);
                            }

                            ob.FLOOR_CODE = (dr["FLOOR_CODE"] == DBNull.Value) ? String.Empty : Convert.ToString(dr["FLOOR_CODE"]);
                            ob.FLOOR_DESC_EN = (dr["FLOOR_DESC_EN"] == DBNull.Value) ? String.Empty : Convert.ToString(dr["FLOOR_DESC_EN"]);
                            ob.FLOOR_DESC_BN = (dr["FLOOR_DESC_BN"] == DBNull.Value) ? String.Empty : Convert.ToString(dr["FLOOR_DESC_BN"]);
                            ob.FP_TYPE_CODE = (dr["FP_TYPE_CODE"] == DBNull.Value) ? String.Empty : Convert.ToString(dr["FP_TYPE_CODE"]);
                            ob.FP_TYPE_NAME_EN = (dr["FP_TYPE_NAME_EN"] == DBNull.Value) ? String.Empty : Convert.ToString(dr["FP_TYPE_NAME_EN"]);
                            ob.FP_TYPE_DESC = (dr["FP_TYPE_DESC"] == DBNull.Value) ? String.Empty : Convert.ToString(dr["FP_TYPE_DESC"]);

                            ob.CT_PRD_QTY = (dr["CT_PRD_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CT_PRD_QTY"]);
                            ob.CT_TGT_QTY = (dr["CT_TGT_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CT_TGT_QTY"]);

                            obList.Add(ob);
               }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public GMT_FIN_PRODModel Select(long ID)
        {
            string sp = "Select_GMT_FIN_PROD";
            try
            {
                var ob = new GMT_FIN_PRODModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pGMT_FIN_PROD_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
              foreach (DataRow dr in ds.Tables[0].Rows)
               {
                        ob.GMT_FIN_PROD_ID = (dr["GMT_FIN_PROD_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_FIN_PROD_ID"]);
                        ob.PROD_DT = (dr["PROD_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["PROD_DT"]);
                        ob.HR_PROD_FLR_ID = (dr["HR_PROD_FLR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_PROD_FLR_ID"]);
                        ob.RF_GMT_FP_TYPE_ID = (dr["RF_GMT_FP_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_GMT_FP_TYPE_ID"]);
                        ob.HRLY_TGT_QTY = (dr["HRLY_TGT_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HRLY_TGT_QTY"]);
                        ob.OT_TGT_HR = (dr["OT_TGT_HR"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["OT_TGT_HR"]);
                        ob.OT_TGT_QTY = (dr["OT_TGT_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["OT_TGT_QTY"]);
                        ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);
                        ob.H1_PRD_QTY = (dr["H1_PRD_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["H1_PRD_QTY"]);
                        ob.H2_PRD_QTY = (dr["H2_PRD_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["H2_PRD_QTY"]);
                        ob.H3_PRD_QTY = (dr["H3_PRD_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["H3_PRD_QTY"]);
                        ob.H4_PRD_QTY = (dr["H4_PRD_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["H4_PRD_QTY"]);
                        ob.H5_PRD_QTY = (dr["H5_PRD_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["H5_PRD_QTY"]);
                        ob.H6_PRD_QTY = (dr["H6_PRD_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["H6_PRD_QTY"]);
                        ob.H7_PRD_QTY = (dr["H7_PRD_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["H7_PRD_QTY"]);
                        ob.H8_PRD_QTY = (dr["H8_PRD_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["H8_PRD_QTY"]);
                        ob.OT_ACT_PRD_QTY = (dr["OT_ACT_PRD_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["OT_ACT_PRD_QTY"]);
                        ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);

               }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string FLOOR_CODE { get; set; }

        public string FLOOR_DESC_EN { get; set; }

        public string FLOOR_DESC_BN { get; set; }

        public string FP_TYPE_CODE { get; set; }

        public string FP_TYPE_NAME_EN { get; set; }

        public string FP_TYPE_DESC { get; set; }

        public long HR_PROD_FLR_ID_SL { get; set; }

        public long HR_PROD_FLR_ID_SPAN { get; set; }

        public long LN_TTL_TARGET_F { get; set; }

        public long LN_TTL_PROD_F { get; set; }

        public decimal LN_ACHV_F { get; set; }

        public int CUR_HOUR { get; set; }

        public long LN_TTL_TARGET_F_CUR { get; set; }

        public long CT_TGT_QTY { get; set; }

        public long CT_PRD_QTY { get; set; }

        public decimal CT_ACHV { get; set; }
    }
}