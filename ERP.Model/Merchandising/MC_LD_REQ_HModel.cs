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
    public class MC_LD_REQ_HModel
    {
        public Int64 MC_LD_REQ_H_ID { get; set; }
        [Required(ErrorMessage = "Please select Buyer")]
        public Int64 MC_BYR_ACC_ID { get; set; }
        public Int64? MC_BUYER_ID { get; set; }
        public Int64 LK_ORD_TYPE_ID { get; set; }
        public string LD_REQ_NO { get; set; }
        [Required(ErrorMessage = "Please input Required Date")]
        public DateTime LD_REQ_DT { get; set; }
        public Int64 LD_REQ_BY { get; set; }
        [Required(ErrorMessage = "Please select Req. To")]
        public Int64 LD_REQ_TO { get; set; }
        public string LD_ATTN_MAIL { get; set; }
        public string REMARKS { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }

        public string ORDER_TYPE_NAME { get; set; }
        public Int64 MC_TNA_TASK_STATUS_ID { get; set; }

        public string STYLE_NO { get; set; }
        public DateTime TARGET_DT { get; set; }
        public string ORDER_NO { get; set; }
        public Int64? MC_ORDER_H_ID { get; set; }
        public string LOGIN_ID { get; set; }
        public Int64 SC_USER_ID { get; set; }
        public string TASK_STATUS_NAME { get; set; }
        public string COLOR_NAME_EN { get; set; }

        public List<MC_LD_REQ_DModel> items { get; set; }

        private List<APPROVED_LD> _approved_lds = null;
        public List<APPROVED_LD> approved_lds
        {
            get
            {
                if (_approved_lds == null)
                {
                    _approved_lds = new List<APPROVED_LD>();
                }
                return _approved_lds;
            }
            set
            {
                _approved_lds = value;
            }
        }

        public string BatchSaveLabdip()
        {
            const string sp = "PKG_Labdip_action.mc_ld_req_h_insert";
            string jsonStr = "{";
            var ob = this;
            var i = 1;

            var xml = new System.Text.StringBuilder();
            xml.Append("<trans>");
            foreach (var line in ob.items)
            {
                var test = line.TARGET_DT.ToShortDateString().Replace('/', '-');
                var spDate = test.Split('-')[1] + "-" + ConMonth(Convert.ToInt32(test.Split('-')[0])) + "-" + test.Split('-')[2];
                xml.AppendLine();
                xml.Append(" <row ");               
                xml.Append(" MC_LD_REQ_D_ID=\"" + line.MC_LD_REQ_D_ID + "\"");
                xml.Append(" MC_STYLE_H_ID=\"" + line.MC_STYLE_H_ID + "\"");
                xml.Append(" MC_LD_REQ_H_ID=\"" + line.MC_LD_REQ_H_ID + "\"");
                xml.Append(" MC_ORDER_H_ID=\"" + line.MC_ORDER_H_ID + "\"");
                xml.Append(" MC_COLOR_ID=\"" + line.MC_COLOR_ID + "\"");
                xml.Append(" MC_STYLE_D_FAB_ID=\"" + line.MC_STYLE_D_FAB_ID + "\"");
                xml.Append(" COLOR_REF=\"" + line.COLOR_REF + "\"");
                xml.Append(" LK_LTSRC_ID=\"" + line.LK_LTSRC_ID + "\"");
                xml.Append(" REQD_QTY=\"" + line.REQD_QTY + "\"");
                xml.Append(" PANTON_NO=\"" + line.PANTON_NO + "\"");

                xml.Append(" TARGET_DT=\"" + spDate + "\"");
                xml.Append(" LK_PRIORITY_ID=\"" + 304 + "\"");
                xml.Append(" MC_TNA_TASK_STATUS_ID=\"" + line.MC_TNA_TASK_STATUS_ID + "\"");
                xml.Append(" COMMENTS=\"" + line.COMMENTS + "\"");
                xml.Append(" APRV_LD_REF=\"" + line.APRV_LD_REF + "\"");
                xml.Append(" MC_STYLE_D_ITEM_LST=\"" + line.MC_STYLE_D_ITEM_LST + "\"");
                xml.Append(" CREATED_BY=\"" + HttpContext.Current.Session["multiScUserId"] + "\"");
                xml.Append(" />");
            }
            xml.AppendLine();
            xml.AppendLine("</trans>");


            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_LD_REQ_H_ID", Value = ob.MC_LD_REQ_H_ID},
                     new CommandParameter() {ParameterName = "pMC_BYR_ACC_ID", Value = ob.MC_BYR_ACC_ID},
                     new CommandParameter() {ParameterName = "pMC_BUYER_ID", Value =ob.MC_BUYER_ID},
                     new CommandParameter() {ParameterName = "pLK_ORD_TYPE_ID", Value = ob.LK_ORD_TYPE_ID},
                     new CommandParameter() {ParameterName = "pLD_REQ_NO", Value = ob.LD_REQ_NO},
                     new CommandParameter() {ParameterName = "pLD_REQ_DT", Value = ob.LD_REQ_DT},
                     new CommandParameter() {ParameterName = "pLD_REQ_BY", Value = ob.LD_REQ_BY},
                     new CommandParameter() {ParameterName = "pLD_REQ_TO", Value = ob.LD_REQ_TO},
                     new CommandParameter() {ParameterName = "pLD_ATTN_MAIL", Value = ob.LD_ATTN_MAIL},
                     new CommandParameter() {ParameterName = "pREMARKS", Value = ob.REMARKS},
                     //new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     //new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
                     //new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = ob.LAST_UPDATED_BY},
                     new CommandParameter() {ParameterName = "pOption", Value =1000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output},
                     new CommandParameter() {ParameterName = "pMC_LD_REQ_D", Value = xml.Replace("null","")},
                     new CommandParameter() {ParameterName = "v_mc_ld_req_h_id", Value =500, Direction = ParameterDirection.Output},
                     new CommandParameter() {ParameterName = "V_LD_REQ_NO", Value =500, Direction = ParameterDirection.Output},
                     new CommandParameter() {ParameterName = "V_LD_ATTN_MAIL", Value =500, Direction = ParameterDirection.Output}
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

        public List<MC_LD_REQ_HModel> SelectAll()
        {
            string sp = "PKG_Labdip_action.mc_LD_REQ_select";
            try
            {
                var obList = new List<MC_LD_REQ_HModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_LD_REQ_H_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_LD_REQ_HModel ob = new MC_LD_REQ_HModel();
                    ob.MC_LD_REQ_H_ID = (dr["MC_LD_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_LD_REQ_H_ID"]);
                    ob.MC_BYR_ACC_ID = (dr["MC_BYR_ACC_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BYR_ACC_ID"]);
                    ob.LD_REQ_NO = (dr["LD_REQ_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LD_REQ_NO"]);
                    ob.LD_REQ_DT = (dr["LD_REQ_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LD_REQ_DT"]);
                    ob.LD_REQ_BY = (dr["LD_REQ_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LD_REQ_BY"]);
                    ob.LD_REQ_TO = (dr["LD_REQ_TO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LD_REQ_TO"]);
                    ob.LD_ATTN_MAIL = (dr["LD_ATTN_MAIL"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LD_ATTN_MAIL"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public MC_LD_REQ_HModel Select(long ID)
        {
            string sp = "PKG_Labdip_action.mc_LD_REQ_select";
            try
            {
                var ob = new MC_LD_REQ_HModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_LD_REQ_H_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.MC_LD_REQ_H_ID = (dr["MC_LD_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_LD_REQ_H_ID"]);
                    ob.MC_BYR_ACC_ID = (dr["MC_BYR_ACC_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BYR_ACC_ID"]);
                    ob.LD_REQ_NO = (dr["LD_REQ_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LD_REQ_NO"]);
                    ob.LD_REQ_DT = (dr["LD_REQ_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LD_REQ_DT"]);
                    ob.LD_REQ_BY = (dr["LD_REQ_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LD_REQ_BY"]);
                    ob.LD_REQ_TO = (dr["LD_REQ_TO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LD_REQ_TO"]);
                    ob.LD_ATTN_MAIL = (dr["LD_ATTN_MAIL"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LD_ATTN_MAIL"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);

                }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<MC_LD_REQ_HModel> LabdipDataList(Int64 pMC_BUYER_ID)
        {
            string sp = "PKG_Labdip_action.mc_LD_REQ_select";
            try
            {
                var obList = new List<MC_LD_REQ_HModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_BUYER_ID", Value =pMC_BUYER_ID},
                     new CommandParameter() {ParameterName = "pOption", Value = 3002},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_LD_REQ_HModel ob = new MC_LD_REQ_HModel();
                    ob.MC_LD_REQ_H_ID = (dr["MC_LD_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_LD_REQ_H_ID"]);
                    ob.MC_BYR_ACC_ID = (dr["MC_BYR_ACC_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BYR_ACC_ID"]);
                    if (dr["MC_BUYER_ID"] != DBNull.Value)
                    {
                        ob.MC_BUYER_ID = Convert.ToInt64(dr["MC_BUYER_ID"]);
                    }
                    ob.LK_ORD_TYPE_ID = (dr["LK_ORD_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_ORD_TYPE_ID"]);
                    ob.LD_REQ_NO = (dr["LD_REQ_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LD_REQ_NO"]);
                    ob.LD_REQ_DT = (dr["LD_REQ_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LD_REQ_DT"]);
                    ob.LD_REQ_BY = (dr["LD_REQ_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LD_REQ_BY"]);
                    ob.LD_REQ_TO = (dr["LD_REQ_TO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LD_REQ_TO"]);
                    ob.LD_ATTN_MAIL = (dr["LD_ATTN_MAIL"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LD_ATTN_MAIL"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);

                    ob.ORDER_TYPE_NAME = (dr["ORDER_TYPE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_TYPE_NAME"]);
                    //ob.ACTN_TYPE_NAME = (dr["ACTN_TYPE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ACTN_TYPE_NAME"]);

                    sp = "PKG_Labdip_action.mc_LD_REQ_select";
                    List<MC_LD_REQ_DModel> obDList = new List<MC_LD_REQ_DModel>();
                    var StyleDs = db.ExecuteStoredProcedure(new List<CommandParameter>()
                    {
                         new CommandParameter() {ParameterName = "pMC_LD_REQ_H_ID", Value = ob.MC_LD_REQ_H_ID},
                         new CommandParameter() {ParameterName = "pOption", Value = 3003},
                         new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                    }, sp);
                    foreach (DataRow drDet in StyleDs.Tables[0].Rows)
                    {
                        MC_LD_REQ_DModel obD = new MC_LD_REQ_DModel();
                        obD.MC_LD_REQ_D_ID = (drDet["MC_LD_REQ_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(drDet["MC_LD_REQ_D_ID"]);
                        obD.MC_LD_REQ_H_ID = (drDet["MC_LD_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(drDet["MC_LD_REQ_H_ID"]);
                        //obD.MC_STYLE_H_ID = (drDet["MC_STYLE_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(drDet["MC_STYLE_H_ID"]);
                        //obD.STYLE_NO = (drDet["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(drDet["STYLE_NO"]);
                        if (drDet["MC_ORDER_H_ID"] != DBNull.Value)
                        {
                            obD.MC_ORDER_H_ID = Convert.ToInt64(drDet["MC_ORDER_H_ID"]);
                        }
                       
                        obD.MC_COLOR_ID = (drDet["MC_COLOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(drDet["MC_COLOR_ID"]);
                        obD.COLOR_REF = (drDet["COLOR_REF"] == DBNull.Value) ? string.Empty : Convert.ToString(drDet["COLOR_REF"]);

                        if (drDet["MC_STYLE_D_FAB_ID"] != DBNull.Value)
                        {
                            obD.MC_STYLE_D_FAB_ID = Convert.ToInt64(drDet["MC_STYLE_D_FAB_ID"]);
                        }

                        //obD.FABRIC_DESC = (drDet["FABRIC_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(drDet["FABRIC_DESC"]);
                        obD.REQD_QTY = (drDet["REQD_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(drDet["REQD_QTY"]);
                        obD.TARGET_DT = (drDet["TARGET_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(drDet["TARGET_DT"]);
                        obD.LK_PRIORITY_ID = (drDet["LK_PRIORITY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(drDet["LK_PRIORITY_ID"]);
                        obD.MC_TNA_TASK_STATUS_ID = (drDet["MC_TNA_TASK_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(drDet["MC_TNA_TASK_STATUS_ID"]);
                        obD.COMMENTS = (drDet["COMMENTS"] == DBNull.Value) ? string.Empty : Convert.ToString(drDet["COMMENTS"]);
                        obD.CREATION_DATE = (drDet["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(drDet["CREATION_DATE"]);
                        obD.CREATED_BY = (drDet["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(drDet["CREATED_BY"]);
                        obD.LAST_UPDATE_DATE = (drDet["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(drDet["LAST_UPDATE_DATE"]);
                        obD.LAST_UPDATED_BY = (drDet["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(drDet["LAST_UPDATED_BY"]);
                        obD.LK_LTSRC_ID = (drDet["LK_LTSRC_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(drDet["LK_LTSRC_ID"]);

                        obD.ORDER_NO = (drDet["ORDER_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(drDet["ORDER_NO"]);
                        obD.COLOR_NAME_EN = (drDet["COLOR_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(drDet["COLOR_NAME_EN"]);
                        obD.TASK_STATUS_NAME = (drDet["TASK_STATUS_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(drDet["TASK_STATUS_NAME"]);

                        obDList.Add(obD);
                    }
                    ob.items = obDList;
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<MC_LD_REQ_HModel> LabdipListByBuyerAcc(Int64 pMC_BYR_ACC_ID)
        {
            string sp = "PKG_Labdip_action.mc_LD_REQ_select";
            try
            {
                var obList = new List<MC_LD_REQ_HModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_BYR_ACC_ID", Value =pMC_BYR_ACC_ID},
                     new CommandParameter() {ParameterName = "pOption", Value = 3002},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_LD_REQ_HModel ob = new MC_LD_REQ_HModel();
                    ob.MC_LD_REQ_H_ID = (dr["MC_LD_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_LD_REQ_H_ID"]);
                    ob.MC_BYR_ACC_ID = (dr["MC_BYR_ACC_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BYR_ACC_ID"]);
                    if (dr["MC_BUYER_ID"] != DBNull.Value)
                    {
                        ob.MC_BUYER_ID = Convert.ToInt64(dr["MC_BUYER_ID"]);
                    }
                    ob.LD_REQ_NO = (dr["LD_REQ_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LD_REQ_NO"]);
                    ob.LD_REQ_DT = (dr["LD_REQ_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LD_REQ_DT"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                    ob.HAS_EXT = (dr["HAS_EXT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["HAS_EXT"]);
                    ob.MC_STYLE_H_EXT_LST = (dr["MC_STYLE_H_EXT_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MC_STYLE_H_EXT_LST"]);
                    ob.ORDER_LIST = (dr["ORDER_LIST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_LIST"]);

                    ob.STYLE_NO = (dr["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO"]);

                    ob.MC_STYLE_H_ID = (dr["MC_STYLE_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_H_ID"]);
                    //sp = "PKG_Labdip_action.mc_LD_REQ_select";
                    //List<MC_LD_REQ_DModel> obDList = new List<MC_LD_REQ_DModel>();
                    //var StyleDs = db.ExecuteStoredProcedure(new List<CommandParameter>()
                    //{
                    //     new CommandParameter() {ParameterName = "pMC_LD_REQ_H_ID", Value = ob.MC_LD_REQ_H_ID},
                    //     new CommandParameter() {ParameterName = "pOption", Value = 3003},
                    //     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                    //}, sp);
                    //foreach (DataRow drDet in StyleDs.Tables[0].Rows)
                    //{
                    //    MC_LD_REQ_DModel obD = new MC_LD_REQ_DModel();
                    //    obD.MC_LD_REQ_D_ID = (drDet["MC_LD_REQ_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(drDet["MC_LD_REQ_D_ID"]);
                    //    obD.MC_LD_REQ_H_ID = (drDet["MC_LD_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(drDet["MC_LD_REQ_H_ID"]);
                    //    obD.MC_STYLE_H_ID = (drDet["MC_STYLE_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(drDet["MC_STYLE_H_ID"]);
                    //    //obD.STYLE_NO = (drDet["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(drDet["STYLE_NO"]);
                    //    obD.MC_ORDER_H_ID = (drDet["MC_ORDER_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(drDet["MC_ORDER_H_ID"]);
                    //    obD.MC_COLOR_ID = (drDet["MC_COLOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(drDet["MC_COLOR_ID"]);
                    //    obD.COLOR_REF = (drDet["COLOR_REF"] == DBNull.Value) ? string.Empty : Convert.ToString(drDet["COLOR_REF"]);

                    //    if (drDet["MC_STYLE_D_FAB_ID"] != DBNull.Value)
                    //    {
                    //        obD.MC_STYLE_D_FAB_ID = Convert.ToInt64(drDet["MC_STYLE_D_FAB_ID"]);
                    //    }

                    //    obD.REQD_QTY = (drDet["REQD_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(drDet["REQD_QTY"]);
                    //    obD.TARGET_DT = (drDet["TARGET_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(drDet["TARGET_DT"]);
                    //    obD.LK_PRIORITY_ID = (drDet["LK_PRIORITY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(drDet["LK_PRIORITY_ID"]);
                    //    obD.MC_TNA_TASK_STATUS_ID = (drDet["MC_TNA_TASK_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(drDet["MC_TNA_TASK_STATUS_ID"]);
                    //    obD.COMMENTS = (drDet["COMMENTS"] == DBNull.Value) ? string.Empty : Convert.ToString(drDet["COMMENTS"]);
                    //    obD.CREATION_DATE = (drDet["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(drDet["CREATION_DATE"]);
                    //    obD.CREATED_BY = (drDet["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(drDet["CREATED_BY"]);
                    //    obD.LAST_UPDATE_DATE = (drDet["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(drDet["LAST_UPDATE_DATE"]);
                    //    obD.LAST_UPDATED_BY = (drDet["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(drDet["LAST_UPDATED_BY"]);
                    //    obD.LK_LTSRC_ID = (drDet["LK_LTSRC_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(drDet["LK_LTSRC_ID"]);

                    //    obD.ORDER_NO = (drDet["ORDER_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(drDet["ORDER_NO"]);
                    //    obD.COLOR_NAME_EN = (drDet["COLOR_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(drDet["COLOR_NAME_EN"]);
                    //    obD.TASK_STATUS_NAME = (drDet["TASK_STATUS_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(drDet["TASK_STATUS_NAME"]);
                    //    obD.SHIP_DT = (drDet["SHIP_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(drDet["SHIP_DT"]);

                    //    obD.LK_LTSRC_NAME = (drDet["LK_LTSRC_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(drDet["LK_LTSRC_NAME"]);
                    //    obD.FABRIC_DESC = (drDet["FABRIC_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(drDet["FABRIC_DESC"]);
                    //    obD.STYLE_NO = (drDet["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(drDet["STYLE_NO"]);
                    //    obD.PANTON_NO = (drDet["PANTON_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(drDet["PANTON_NO"]);

                    //    obDList.Add(obD);
                    //}
                    //ob.items = obDList;
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string ConMonth(Int64 mon)
        {
            string retMonth = string.Empty;
            switch (mon)
            {
                case 12: retMonth = "Dec";
                    break;
                case 11: retMonth = "Nov";
                    break;
                case 10: retMonth = "Oct";
                    break;
                case 9: retMonth = "Sep";
                    break;

                case 8: retMonth = "Aug";
                    break;
                case 7: retMonth = "Jul";
                    break;
                case 6: retMonth = "Jun";
                    break;
                case 5: retMonth = "May";
                    break;

                case 4: retMonth = "Apr";
                    break;
                case 3: retMonth = "Mar";
                    break;
                case 2: retMonth = "Feb";
                    break;
                case 1: retMonth = "Jan";
                    break;
                default: break;
            }
            return retMonth;
        }

        //public List<MC_LD_REQ_HModel> LabdipBuyerWiseList(Int64 pMC_BUYER_ID)
        //{
        //    string sp = "PKG_Labdip_action.mc_LD_REQ_select";
        //    try
        //    {
        //        var obList = new List<MC_LD_REQ_HModel>();
        //        OraDatabase db = new OraDatabase();
        //        var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
        //        {
        //             new CommandParameter() {ParameterName = "pMC_BUYER_ID", Value =pMC_BUYER_ID},
        //             new CommandParameter() {ParameterName = "pOption", Value = 3004},
        //             new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
        //         }, sp);
        //        foreach (DataRow dr in ds.Tables[0].Rows)
        //        {
        //            MC_LD_REQ_HModel ob = new MC_LD_REQ_HModel();
        //            ob.MC_LD_REQ_H_ID = (dr["MC_LD_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_LD_REQ_H_ID"]);
        //            ob.RF_ACTN_TYPE_ID = (dr["RF_ACTN_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_ACTN_TYPE_ID"]);
        //            ob.MC_BUYER_ID = (dr["MC_BUYER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BUYER_ID"]);
        //            ob.LK_ORD_TYPE_ID = (dr["LK_ORD_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_ORD_TYPE_ID"]);
        //            ob.LD_REQ_NO = (dr["LD_REQ_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LD_REQ_NO"]);
        //            ob.LD_REQ_DT = (dr["LD_REQ_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LD_REQ_DT"]);
        //            ob.LD_REQ_BY = (dr["LD_REQ_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LD_REQ_BY"]);
        //            ob.LD_REQ_TO = (dr["LD_REQ_TO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LD_REQ_TO"]);
        //            ob.LD_ATTN_MAIL = (dr["LD_ATTN_MAIL"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LD_ATTN_MAIL"]);
        //            ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
        //            ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
        //            ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
        //            ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
        //            ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);

        //            ob.STYLE_NO = (dr["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO"]);
        //            ob.ORDER_NO = (dr["ORDER_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_NO"]);
        //            ob.TARGET_DT = (dr["TARGET_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["TARGET_DT"]);

        //            obList.Add(ob);
        //        }
        //        return obList;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        public List<MC_LD_REQ_HModel> LabdipSubListByBuyerAcc(Int64? pMC_BYR_ACC_ID, Int64? pMC_LD_REQ_H_ID)
        {
            string sp = "PKG_Labdip_action.mc_LD_REQ_select";
            try
            {
                var obList = new List<MC_LD_REQ_HModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_BYR_ACC_ID", Value =pMC_BYR_ACC_ID},
                     new CommandParameter() {ParameterName = "pMC_LD_REQ_H_ID", Value =pMC_LD_REQ_H_ID},
                     new CommandParameter() {ParameterName = "pOption", Value = 3004},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_LD_REQ_HModel ob = new MC_LD_REQ_HModel();
                    ob.MC_LD_REQ_H_ID = (dr["MC_LD_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_LD_REQ_H_ID"]);
                    ob.MC_BYR_ACC_ID = (dr["MC_BYR_ACC_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BYR_ACC_ID"]);

                    if (dr["MC_BUYER_ID"] != DBNull.Value)
                    {
                        ob.MC_BUYER_ID = Convert.ToInt64(dr["MC_BUYER_ID"]);
                    }
                    ob.LK_ORD_TYPE_ID = (dr["LK_ORD_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_ORD_TYPE_ID"]);
                    ob.LD_REQ_NO = (dr["LD_REQ_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LD_REQ_NO"]);
                    ob.LD_REQ_DT = (dr["LD_REQ_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LD_REQ_DT"]);
                    ob.LD_REQ_BY = (dr["LD_REQ_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LD_REQ_BY"]);
                    ob.LD_REQ_TO = (dr["LD_REQ_TO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LD_REQ_TO"]);
                    ob.LD_ATTN_MAIL = (dr["LD_ATTN_MAIL"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LD_ATTN_MAIL"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);

                    ob.ORDER_NO = (dr["ORDER_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_NO"]);
                    ob.STYLE_NO = (dr["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO"]);
                    ob.MC_STYLE_H_ID = (dr["MC_STYLE_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_H_ID"]);
                    ob.TARGET_DT = (dr["TARGET_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["TARGET_DT"]);

                    if (dr["MC_ORDER_H_ID"] != DBNull.Value)
                    {
                        ob.MC_ORDER_H_ID = Convert.ToInt64(dr["MC_ORDER_H_ID"]);
                    }
                   

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<MC_LD_REQ_HModel> LabdipProgramForDB(Int64 pSC_USER_ID)
        {
            string sp = "PKG_Labdip_action.mc_LD_REQ_select";
            try
            {
                var obList = new List<MC_LD_REQ_HModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pSC_USER_ID", Value =pSC_USER_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3007},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_LD_REQ_HModel ob = new MC_LD_REQ_HModel();

                    ob.MC_LD_REQ_H_ID = (dr["MC_LD_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_LD_REQ_H_ID"]);
                    ob.LD_REQ_NO = (dr["LD_REQ_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LD_REQ_NO"]);
                    ob.TARGET_DT = (dr["TARGET_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["TARGET_DT"]);

                    if (dr["ACTION_DATE"] != DBNull.Value)
                    {
                        ob.ACTION_DATE = Convert.ToDateTime(dr["ACTION_DATE"]);
                    }
                    ob.IS_LD_REQ_BY = (dr["IS_LD_REQ_BY"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_LD_REQ_BY"]);
                   
                    ob.USER_NAME_EN = (dr["USER_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["USER_NAME_EN"]);
                    ob.MC_TNA_TASK_STATUS_ID = (dr["MC_TNA_TASK_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_TNA_TASK_STATUS_ID"]);

                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.TASK_STATUS_NAME = (dr["TASK_STATUS_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TASK_STATUS_NAME"]);
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public List<MC_LD_REQ_HModel> LabdipProgramForDBStyleWise(Int64 pSC_USER_ID, String pSearchParams)
        {
            string sp = "PKG_Labdip_action.mc_LD_REQ_select";
            try
            {
                var obList = new List<MC_LD_REQ_HModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pSC_USER_ID", Value =pSC_USER_ID},
                     new CommandParameter() {ParameterName = "pSEARCH_PARAMS", Value =pSearchParams},
                     new CommandParameter() {ParameterName = "pOption", Value =3008},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_LD_REQ_HModel ob = new MC_LD_REQ_HModel();

                    ob.MC_LD_REQ_H_ID = (dr["MC_LD_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_LD_REQ_H_ID"]);
                    ob.MC_STYLE_H_ID = (dr["MC_STYLE_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_H_ID"]);
                    ob.MC_BUYER_ID = (dr["MC_BUYER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BUYER_ID"]);

                    ob.HAS_EXT = (dr["HAS_EXT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["HAS_EXT"]);
                    ob.MC_STYLE_H_EXT_LST = (dr["MC_STYLE_H_EXT_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MC_STYLE_H_EXT_LST"]);


                    ob.MC_TNA_TASK_STATUS_ID = (dr["MC_TNA_TASK_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_TNA_TASK_STATUS_ID"]);
                    ob.MC_BYR_ACC_ID = (dr["MC_BYR_ACC_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BYR_ACC_ID"]);
                    ob.LD_REQ_NO = (dr["LD_REQ_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LD_REQ_NO"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.BYR_ACC_NAME_EN = (dr["BYR_ACC_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BYR_ACC_NAME_EN"]);
                    ob.STYLE_NO = (dr["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO"]);
                    ob.TASK_STATUS_NAME = (dr["TASK_STATUS_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TASK_STATUS_NAME"]);
                    ob.IS_ACT_ACTIVE = (dr["IS_ACT_ACTIVE"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_ACT_ACTIVE"]);
                    ob.LD_REQ_BY = (dr["LD_REQ_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LD_REQ_BY"]);
                    ob.LD_REQ_TO = (dr["LD_REQ_TO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LD_REQ_TO"]);

                    ob.IS_ACT_ACTIVE = (dr["IS_ACT_ACTIVE"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_ACT_ACTIVE"]);
                    ob.IS_ACT_ACTIVE = (dr["IS_ACT_ACTIVE"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_ACT_ACTIVE"]);

                        var ds1 = db.ExecuteStoredProcedure(new List<CommandParameter>()
                        {
                             new CommandParameter() {ParameterName = "pMC_LD_REQ_H_ID", Value = ob.MC_LD_REQ_H_ID},
                             new CommandParameter() {ParameterName = "pMC_STYLE_H_ID", Value = ob.MC_STYLE_H_ID},
                             new CommandParameter() {ParameterName = "pMC_TNA_TASK_STATUS_ID", Value = 10},
                             new CommandParameter() {ParameterName = "pOption", Value =3009},
                             new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                         }, sp);
                        foreach (DataRow dr1 in ds1.Tables[0].Rows)
                        {
                            APPROVED_LD ob2 = new APPROVED_LD();
                            ob2.COLOR_NAME_EN = (dr1["COLOR_NAME_EN"] == DBNull.Value) ? "N" : Convert.ToString(dr1["COLOR_NAME_EN"]);
                            ob2.MC_LD_REQ_D_ID = (dr1["MC_LD_REQ_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr1["MC_LD_REQ_D_ID"]);
                            ob.approved_lds.Add(ob2);
                        }
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public MC_LD_REQ_HModel LabdipDataListByHID(Int64 pMC_LD_REQ_H_ID)
        {
            string sp = "PKG_Labdip_action.mc_LD_REQ_select";
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_LD_REQ_H_ID", Value =pMC_LD_REQ_H_ID},
                     new CommandParameter() {ParameterName = "pOption", Value = 3001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_LD_REQ_HModel ob = new MC_LD_REQ_HModel();
                    ob.MC_LD_REQ_H_ID = (dr["MC_LD_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_LD_REQ_H_ID"]);
                    ob.MC_BYR_ACC_ID = (dr["MC_BYR_ACC_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BYR_ACC_ID"]);
                  
                    if (dr["MC_BUYER_ID"] != DBNull.Value)
                    {
                        ob.MC_BUYER_ID = Convert.ToInt64(dr["MC_BUYER_ID"]);
                    }

                    ob.LK_ORD_TYPE_ID = (dr["LK_ORD_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_ORD_TYPE_ID"]);
                    ob.LD_REQ_NO = (dr["LD_REQ_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LD_REQ_NO"]);
                    ob.LD_REQ_DT = (dr["LD_REQ_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LD_REQ_DT"]);
                    ob.LD_REQ_BY = (dr["LD_REQ_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LD_REQ_BY"]);
                    ob.LD_REQ_TO = (dr["LD_REQ_TO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LD_REQ_TO"]);
                    ob.LD_ATTN_MAIL = (dr["LD_ATTN_MAIL"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LD_ATTN_MAIL"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);

                    ob.ORDER_TYPE_NAME = (dr["ORDER_TYPE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_TYPE_NAME"]);
                    //ob.ACTN_TYPE_NAME = (dr["ACTN_TYPE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ACTN_TYPE_NAME"]);

                    sp = "PKG_Labdip_action.mc_LD_REQ_select";
                    List<MC_LD_REQ_DModel> obDList = new List<MC_LD_REQ_DModel>();
                    var StyleDs = db.ExecuteStoredProcedure(new List<CommandParameter>()
                    {
                         new CommandParameter() {ParameterName = "pMC_LD_REQ_H_ID", Value = ob.MC_LD_REQ_H_ID},
                         new CommandParameter() {ParameterName = "pOption", Value = 3003},
                         new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                    }, sp);
                    foreach (DataRow drDet in StyleDs.Tables[0].Rows)
                    {
                        MC_LD_REQ_DModel obD = new MC_LD_REQ_DModel();
                        obD.MC_LD_REQ_D_ID = (drDet["MC_LD_REQ_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(drDet["MC_LD_REQ_D_ID"]);
                        obD.MC_LD_REQ_H_ID = (drDet["MC_LD_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(drDet["MC_LD_REQ_H_ID"]);
                        obD.MC_STYLE_H_ID = (drDet["MC_STYLE_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(drDet["MC_STYLE_H_ID"]);
                      

                        if (drDet["MC_ORDER_H_ID"] != DBNull.Value)
                        {
                            obD.MC_ORDER_H_ID =  Convert.ToInt64(drDet["MC_ORDER_H_ID"]);
                        }

                        obD.MC_COLOR_ID = (drDet["MC_COLOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(drDet["MC_COLOR_ID"]);
                        obD.COLOR_REF = (drDet["COLOR_REF"] == DBNull.Value) ? string.Empty : Convert.ToString(drDet["COLOR_REF"]);

                        if (drDet["MC_STYLE_D_FAB_ID"] != DBNull.Value)
                        {
                            obD.MC_STYLE_D_FAB_ID = Convert.ToInt64(drDet["MC_STYLE_D_FAB_ID"]);
                        }

                        
                        obD.FABRIC_DESC = (drDet["FABRIC_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(drDet["FABRIC_DESC"]);


                        
                        obD.REQD_QTY = (drDet["REQD_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(drDet["REQD_QTY"]);
                        obD.TARGET_DT = (drDet["TARGET_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(drDet["TARGET_DT"]);
                        obD.LK_PRIORITY_ID = (drDet["LK_PRIORITY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(drDet["LK_PRIORITY_ID"]);
                        obD.MC_TNA_TASK_STATUS_ID = (drDet["MC_TNA_TASK_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(drDet["MC_TNA_TASK_STATUS_ID"]);
                        obD.COMMENTS = (drDet["COMMENTS"] == DBNull.Value) ? string.Empty : Convert.ToString(drDet["COMMENTS"]);
                        obD.CREATION_DATE = (drDet["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(drDet["CREATION_DATE"]);
                        obD.CREATED_BY = (drDet["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(drDet["CREATED_BY"]);
                        obD.LAST_UPDATE_DATE = (drDet["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(drDet["LAST_UPDATE_DATE"]);
                        obD.LAST_UPDATED_BY = (drDet["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(drDet["LAST_UPDATED_BY"]);
                        obD.LK_LTSRC_ID = (drDet["LK_LTSRC_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(drDet["LK_LTSRC_ID"]);
                        obD.LK_LTSRC_NAME = (drDet["LK_LTSRC_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(drDet["LK_LTSRC_NAME"]);
                        obD.PANTON_NO = (drDet["PANTON_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(drDet["PANTON_NO"]);
                        obD.SHIP_DT = (drDet["SHIP_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(drDet["SHIP_DT"]);


                        obD.ORDER_NO = (drDet["ORDER_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(drDet["ORDER_NO"]);
                        obD.COLOR_NAME_EN = (drDet["COLOR_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(drDet["COLOR_NAME_EN"]);
                        obD.TASK_STATUS_NAME = (drDet["TASK_STATUS_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(drDet["TASK_STATUS_NAME"]);
                        obD.APRV_LD_REF = (drDet["APRV_LD_REF"] == DBNull.Value) ? string.Empty : Convert.ToString(drDet["APRV_LD_REF"]);
                        obD.MC_STYLE_D_ITEM_LST = (drDet["MC_STYLE_D_ITEM_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(drDet["MC_STYLE_D_ITEM_LST"]);
                        obD.STYLE_NO = (drDet["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(drDet["STYLE_NO"]);

                        obDList.Add(obD);
                    }
                    ob.items = obDList;
                    return ob;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public MC_LD_REQ_HModel LabdipDataWOApprNo(Int64 pMC_LD_REQ_H_ID)
        {
            string sp = "PKG_Labdip_action.mc_LD_REQ_select";
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_LD_REQ_H_ID", Value =pMC_LD_REQ_H_ID},
                     new CommandParameter() {ParameterName = "pOption", Value = 3001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_LD_REQ_HModel ob = new MC_LD_REQ_HModel();
                    ob.MC_LD_REQ_H_ID = (dr["MC_LD_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_LD_REQ_H_ID"]);
                    ob.MC_BYR_ACC_ID = (dr["MC_BYR_ACC_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BYR_ACC_ID"]);
                    if (dr["MC_BUYER_ID"] != DBNull.Value)
                    {
                        ob.MC_BUYER_ID = Convert.ToInt64(dr["MC_BUYER_ID"]);
                    }
                    ob.LK_ORD_TYPE_ID = (dr["LK_ORD_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_ORD_TYPE_ID"]);
                    ob.LD_REQ_NO = (dr["LD_REQ_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LD_REQ_NO"]);
                    ob.LD_REQ_DT = (dr["LD_REQ_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LD_REQ_DT"]);
                    ob.LD_REQ_BY = (dr["LD_REQ_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LD_REQ_BY"]);
                    ob.LD_REQ_TO = (dr["LD_REQ_TO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LD_REQ_TO"]);
                    ob.LD_ATTN_MAIL = (dr["LD_ATTN_MAIL"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LD_ATTN_MAIL"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);

                    ob.ORDER_TYPE_NAME = (dr["ORDER_TYPE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_TYPE_NAME"]);
                    //ob.ACTN_TYPE_NAME = (dr["ACTN_TYPE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ACTN_TYPE_NAME"]);

                    sp = "PKG_Labdip_action.mc_LD_REQ_select";
                    List<MC_LD_REQ_DModel> obDList = new List<MC_LD_REQ_DModel>();
                    var StyleDs = db.ExecuteStoredProcedure(new List<CommandParameter>()
                    {
                         new CommandParameter() {ParameterName = "pMC_LD_REQ_H_ID", Value = ob.MC_LD_REQ_H_ID},
                         new CommandParameter() {ParameterName = "pOption", Value = 3003},
                         new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                    }, sp);
                    foreach (DataRow drDet in StyleDs.Tables[0].Rows)
                    {
                        MC_LD_REQ_DModel obD = new MC_LD_REQ_DModel();
                        obD.MC_LD_REQ_D_ID = (drDet["MC_LD_REQ_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(drDet["MC_LD_REQ_D_ID"]);
                        obD.MC_LD_REQ_H_ID = (drDet["MC_LD_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(drDet["MC_LD_REQ_H_ID"]);
                        obD.MC_STYLE_H_ID = (drDet["MC_STYLE_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(drDet["MC_STYLE_H_ID"]);
                        //obD.STYLE_NO = (drDet["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(drDet["STYLE_NO"]);
                        obD.MC_ORDER_H_ID = (drDet["MC_ORDER_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(drDet["MC_ORDER_H_ID"]);
                        obD.MC_COLOR_ID = (drDet["MC_COLOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(drDet["MC_COLOR_ID"]);
                        obD.COLOR_REF = (drDet["COLOR_REF"] == DBNull.Value) ? string.Empty : Convert.ToString(drDet["COLOR_REF"]);
                       
                        if (drDet["MC_STYLE_D_FAB_ID"] != DBNull.Value)
                        {
                            obD.MC_STYLE_D_FAB_ID = Convert.ToInt64(drDet["MC_STYLE_D_FAB_ID"]);
                        }
                        //obD.FABRIC_DESC = (drDet["FABRIC_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(drDet["FABRIC_DESC"]);
                        obD.REQD_QTY = (drDet["REQD_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(drDet["REQD_QTY"]);
                        obD.TARGET_DT = (drDet["TARGET_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(drDet["TARGET_DT"]);
                        obD.LK_PRIORITY_ID = (drDet["LK_PRIORITY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(drDet["LK_PRIORITY_ID"]);
                        obD.MC_TNA_TASK_STATUS_ID = (drDet["MC_TNA_TASK_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(drDet["MC_TNA_TASK_STATUS_ID"]);
                        obD.COMMENTS = (drDet["COMMENTS"] == DBNull.Value) ? string.Empty : Convert.ToString(drDet["COMMENTS"]);
                        obD.CREATION_DATE = (drDet["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(drDet["CREATION_DATE"]);
                        obD.CREATED_BY = (drDet["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(drDet["CREATED_BY"]);
                        obD.LAST_UPDATE_DATE = (drDet["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(drDet["LAST_UPDATE_DATE"]);
                        obD.LAST_UPDATED_BY = (drDet["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(drDet["LAST_UPDATED_BY"]);
                        obD.LK_LTSRC_ID = (drDet["LK_LTSRC_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(drDet["LK_LTSRC_ID"]);
                        obD.MC_STYLE_H_ID = (drDet["MC_STYLE_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(drDet["MC_STYLE_H_ID"]);
                        obD.ORDER_NO = (drDet["ORDER_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(drDet["ORDER_NO"]);
                        obD.COLOR_NAME_EN = (drDet["COLOR_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(drDet["COLOR_NAME_EN"]);
                        obD.TASK_STATUS_NAME = (drDet["TASK_STATUS_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(drDet["TASK_STATUS_NAME"]);
                        obD.APRV_LD_REF = (drDet["APRV_LD_REF"] == DBNull.Value) ? string.Empty : Convert.ToString(drDet["APRV_LD_REF"]);
                        obD.MC_STYLE_D_ITEM_LST = (drDet["MC_STYLE_D_ITEM_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(drDet["MC_STYLE_D_ITEM_LST"]);
                        //if (obD.APRV_LD_REF == "")
                        //{
                        obDList.Add(obD);
                        //}
                    }
                    ob.items = obDList;
                    return ob;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public DateTime? ACTION_DATE { get; set; }

        public string USER_NAME_EN { get; set; }

        public string IS_LD_REQ_BY { get; set; }

        public Int64? MC_LD_REQ_STATUS_ID { get; set; }

        public Int64? MC_LD_REQ_D_ID { get; set; }

        public string WORK_STYLE_NO { get; set; }

        public string JOB_TRAC_NO { get; set; }

        public string BYR_ACC_NAME_EN { get; set; }

        public long MC_STYLE_H_ID { get; set; }

        public string IS_ACT_ACTIVE { get; set; }

        public string HAS_EXT { get; set; }

        public string MC_STYLE_H_EXT_LST { get; set; }

        public string ORDER_LIST { get; set; }
    }

    public class APPROVED_LD
    {
        public string COLOR_NAME_EN { get; set; }
        public Int64 MC_LD_REQ_D_ID { get; set; }
    }
}