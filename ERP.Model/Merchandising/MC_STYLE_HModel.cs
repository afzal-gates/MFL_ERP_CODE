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
    public class MC_STYLE_HModel
    {
        public Int64 MC_STYLE_H_ID { get; set; }
        public Int64? PARENT_ID { get; set; }
        public Int64 MC_BUYER_ID { get; set; }
        public Int64? MC_INQR_H_ID { get; set; }
        public string STYLE_NO { get; set; }
        public string STYL_EXT_NO { get; set; }
        public string REF_STYLE_NO { get; set; }
        public string COMP_STYLE_NO { get; set; }
        public string STYLE_DESC { get; set; }
        public Int64 ORIGIN_ID { get; set; }
        public Int64 MANUF_ID { get; set; }
        public string REVISION_NO { get; set; }
        public DateTime REVISION_DT { get; set; }
        public Int64? NO_OF_REV { get; set; }
        public Int64? LK_SEASON_ID { get; set; }
        public string SEASON_REF { get; set; }
        public string SZ_RANGE { get; set; }
        public string HAS_SET { get; set; }
        public string HAS_COMBO { get; set; }
        public string INSTRUCTION { get; set; }
        public string CONCEPT_NAME { get; set; }
        public string BRAND_NAME { get; set; }

        public string ITEM_LIST { get; set; }
        public Int64? LK_STYL_DEV_ID { get; set; }
        public string IS_ACTIVE { get; set; }
        public DateTime? CREATION_DATE { get; set; }
        public Int64? CREATED_BY { get; set; }
        public DateTime? LAST_UPDATE_DATE { get; set; }
        public Int64? LAST_UPDATED_BY { get; set; }
        public Int64 NO_OF_SET { get; set; }

        public Int64? QTY_MOU_ID { get; set; }

        public Int64? PCS_PER_PACK { get; set; }
        public Int64? MC_BYR_ACC_ID { get; set; }
        public Int64? MC_BYR_ACC_GRP_ID { get; set; }

        public Int64? MC_STYLE_H_EXT_ID { get; set; }
        public Int64? MC_ORDER_H_ID { get; set; }

        public string EXT_XML { get; set; }
        public string HAS_MULTI_COL_PACK { get; set; }
        public string ALL_ITEM_LIST { get; set; }
        public string APRV_LD_REF { get; set; }
        public Int64? LK_DYE_MTHD_ID { get; set; }
        public Int64? MC_COLOR_GRP_ID { get; set; }
        public string MATERIAL_DESC { get; set; }
        public string MC_SIZE_LST { get; set; }
        public string BUYER_NAME_EN { get; set; }
        public string SEASON { get; set; }
        public long? HR_COUNTRY_ID { get; set; }

        private List<MC_STYLE_D_ITEMModel> _items = null;
        public List<MC_STYLE_D_ITEMModel> items
        {
            get
            {
                if (_items == null)
                {
                    _items = new List<MC_STYLE_D_ITEMModel>();
                }
                return _items;
            }
            set
            {
                _items = value;
            }
        }

        public string HAS_MODEL { get; set; }


        public string Save()
        {
            const string sp = "pkg_merchandising.mc_style_h_insert";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            var xml = new System.Text.StringBuilder();

            if (ob.items.Count > 0)
            {
                xml.Append("<trans>");
                foreach (var line in ob.items)
                {
                    xml.AppendLine();
                    xml.Append(" <row ");
                    xml.Append(" MC_STYLE_D_ITEM_ID=\"" + line.MC_STYLE_D_ITEM_ID + "\"");
                    xml.Append(" ITEM_NAME_EN=\"" + line.ITEM_NAME_EN + "\"");
                    xml.Append(" COMBO_NO=\"" + line.COMBO_NO + "\"");
                    xml.Append(" MODEL_NO=\"" + line.MODEL_NO + "\"");
                    xml.Append(" SEGMENT_FLAG=\"" + line.SEGMENT_FLAG + "\"");
                    xml.Append(" />");
                }
                xml.AppendLine();
                xml.AppendLine("</trans>");
            }
            else
            {
                xml = null;
            }


            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_STYLE_H_ID", Value = ob.MC_STYLE_H_ID},
                     new CommandParameter() {ParameterName = "pPARENT_ID", Value = ob.PARENT_ID},
                     new CommandParameter() {ParameterName = "pMC_INQR_H_ID", Value = ob.MC_INQR_H_ID},
                     new CommandParameter() {ParameterName = "pSTYLE_NO", Value = ob.STYLE_NO},
                     new CommandParameter() {ParameterName = "pSTYL_EXT_NO", Value = ob.STYL_EXT_NO},
                     new CommandParameter() {ParameterName = "pREF_STYLE_NO", Value = ob.REF_STYLE_NO},
                     new CommandParameter() {ParameterName = "pCOMP_STYLE_NO", Value = ob.COMP_STYLE_NO},
                     new CommandParameter() {ParameterName = "pSTYLE_DESC", Value = ob.STYLE_DESC},
                     new CommandParameter() {ParameterName = "pORIGIN_ID", Value = ob.ORIGIN_ID},
                     new CommandParameter() {ParameterName = "pMANUF_ID", Value = ob.MANUF_ID},
                     new CommandParameter() {ParameterName = "pREVISION_NO", Value = ob.REVISION_NO},
                     new CommandParameter() {ParameterName = "pREVISION_DT", Value = ob.REVISION_DT},
                     new CommandParameter() {ParameterName = "pNO_OF_REV", Value = ob.NO_OF_REV},
                     new CommandParameter() {ParameterName = "pLK_SEASON_ID", Value = ob.LK_SEASON_ID},
                     new CommandParameter() {ParameterName = "pSEASON_REF", Value = ob.SEASON_REF},
                     new CommandParameter() {ParameterName = "pSZ_RANGE", Value = ob.SZ_RANGE},
                     new CommandParameter() {ParameterName = "pHAS_SET", Value = ob.HAS_SET},
                     new CommandParameter() {ParameterName = "pHAS_MULTI_COL_PACK", Value = ob.HAS_MULTI_COL_PACK},

                     new CommandParameter() {ParameterName = "pMC_BUYER_ID", Value = ob.MC_BUYER_ID},

                     new CommandParameter() {ParameterName = "pMC_BYR_ACC_ID", Value = ob.MC_BYR_ACC_ID},
                     
                     new CommandParameter() {ParameterName = "pHAS_COMBO", Value = ob.HAS_COMBO},
                     new CommandParameter() {ParameterName = "pHAS_MODEL", Value = ob.HAS_MODEL},
                     new CommandParameter() {ParameterName = "pINSTRUCTION", Value = ob.INSTRUCTION},
                     new CommandParameter() {ParameterName = "pCONCEPT_NAME", Value = ob.CONCEPT_NAME},
                     new CommandParameter() {ParameterName = "pBRAND_NAME", Value = ob.BRAND_NAME},
                     new CommandParameter() {ParameterName = "pLK_STYL_DEV_ID", Value = ob.LK_STYL_DEV_ID},
                     new CommandParameter() {ParameterName = "pIS_ACTIVE", Value = ob.IS_ACTIVE},
                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pXML", Value = xml},
                     new CommandParameter() {ParameterName = "pNO_OF_SET", Value = ob.NO_OF_SET},

                     new CommandParameter() {ParameterName = "pQTY_MOU_ID", Value = ob.QTY_MOU_ID},
                     new CommandParameter() {ParameterName = "pPCS_PER_PACK", Value = ob.PCS_PER_PACK},
                     new CommandParameter() {ParameterName = "pMATERIAL_DESC", Value = ob.MATERIAL_DESC},
                     new CommandParameter() {ParameterName = "pMC_SIZE_LST", Value = ob.MC_SIZE_LST},

                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pOption", Value =1000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output},
                     new CommandParameter() {ParameterName = "V_MC_STYLE_H_ID", Value =null, Direction = ParameterDirection.Output}
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

        public string Update()
        {
            const string sp = "pkg_merchandising.mc_style_h_update";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            var xml = new System.Text.StringBuilder();

            if (ob.items.Count > 0)
            {
                xml.Append("<trans>");
                foreach (var line in ob.items)
                {
                    xml.AppendLine();
                    xml.Append(" <row ");
                    xml.Append(" MC_STYLE_D_ITEM_ID=\"" + line.MC_STYLE_D_ITEM_ID + "\"");
                    xml.Append(" ITEM_NAME_EN=\"" + line.ITEM_NAME_EN + "\"");
                    xml.Append(" MODEL_NO=\"" + line.MODEL_NO + "\"");
                    xml.Append(" SEGMENT_FLAG=\"" + line.SEGMENT_FLAG + "\"");
                    xml.Append(" />");
                }
                xml.AppendLine();
                xml.AppendLine("</trans>");
            }
            else
            {
                xml = null;
            }





            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_STYLE_H_ID", Value = ob.MC_STYLE_H_ID},
                     new CommandParameter() {ParameterName = "pPARENT_ID", Value = ob.PARENT_ID},
                     new CommandParameter() {ParameterName = "pMC_INQR_H_ID", Value = ob.MC_INQR_H_ID},
                     new CommandParameter() {ParameterName = "pSTYLE_NO", Value = ob.STYLE_NO},
                     new CommandParameter() {ParameterName = "pSTYL_EXT_NO", Value = ob.STYL_EXT_NO},
                     new CommandParameter() {ParameterName = "pREF_STYLE_NO", Value = ob.REF_STYLE_NO},
                     new CommandParameter() {ParameterName = "pCOMP_STYLE_NO", Value = ob.COMP_STYLE_NO},
                     new CommandParameter() {ParameterName = "pSTYLE_DESC", Value = ob.STYLE_DESC},
                     new CommandParameter() {ParameterName = "pORIGIN_ID", Value = ob.ORIGIN_ID},
                     new CommandParameter() {ParameterName = "pMANUF_ID", Value = ob.MANUF_ID},
                     new CommandParameter() {ParameterName = "pREVISION_NO", Value = ob.REVISION_NO},
                     new CommandParameter() {ParameterName = "pREVISION_DT", Value = ob.REVISION_DT},
                     new CommandParameter() {ParameterName = "pNO_OF_REV", Value = ob.NO_OF_REV},
                     new CommandParameter() {ParameterName = "pLK_SEASON_ID", Value = ob.LK_SEASON_ID},
                     new CommandParameter() {ParameterName = "pSEASON_REF", Value = ob.SEASON_REF},
                     new CommandParameter() {ParameterName = "pSZ_RANGE", Value = ob.SZ_RANGE},
                     new CommandParameter() {ParameterName = "pHAS_SET", Value = ob.HAS_SET},
                     new CommandParameter() {ParameterName = "pHAS_MULTI_COL_PACK", Value = ob.HAS_MULTI_COL_PACK},
                     new CommandParameter() {ParameterName = "pHAS_COMBO", Value = ob.HAS_COMBO},
                     new CommandParameter() {ParameterName = "pHAS_MODEL", Value = ob.HAS_MODEL},
                     new CommandParameter() {ParameterName = "pINSTRUCTION", Value = ob.INSTRUCTION},
                     new CommandParameter() {ParameterName = "pCONCEPT_NAME", Value = ob.CONCEPT_NAME},
                     new CommandParameter() {ParameterName = "pBRAND_NAME", Value = ob.BRAND_NAME},
                     new CommandParameter() {ParameterName = "pLK_STYL_DEV_ID", Value = ob.LK_STYL_DEV_ID},
                     new CommandParameter() {ParameterName = "pIS_ACTIVE", Value = ob.IS_ACTIVE},
                     new CommandParameter() {ParameterName = "pMC_BUYER_ID", Value = ob.MC_BUYER_ID},
                     new CommandParameter() {ParameterName = "pXML", Value = xml},
                     new CommandParameter() {ParameterName = "pNO_OF_SET", Value = ob.NO_OF_SET},
                     new CommandParameter() {ParameterName = "pQTY_MOU_ID", Value = ob.QTY_MOU_ID},
                     new CommandParameter() {ParameterName = "pPCS_PER_PACK", Value = ob.PCS_PER_PACK},
                     new CommandParameter() {ParameterName = "pMC_BYR_ACC_ID", Value = ob.MC_BYR_ACC_ID},
                     new CommandParameter() {ParameterName = "pMC_STYLE_H_EXT_ID", Value = ob.MC_STYLE_H_EXT_ID},
                     new CommandParameter() {ParameterName = "pMATERIAL_DESC", Value = ob.MATERIAL_DESC},
                     new CommandParameter() {ParameterName = "pMC_SIZE_LST", Value = ob.MC_SIZE_LST},

                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value =HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pOption", Value =2000},
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
            const string sp = "pkg_merchandising.mc_style_h_delete";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_STYLE_H_ID", Value = ob.MC_STYLE_H_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =4000},
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

        public List<MC_STYLE_HModel> SelectAll()
        {
            string sp = "pkg_merchandising.mc_style_h_select";
            try
            {
                var obList = new List<MC_STYLE_HModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_STYLE_H_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_STYLE_HModel ob = new MC_STYLE_HModel();
                    ob.MC_STYLE_H_ID = (dr["MC_STYLE_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_H_ID"]);

                    if (dr["PARENT_ID"] != DBNull.Value)
                    {
                        ob.PARENT_ID = Convert.ToInt64(dr["PARENT_ID"]);
                    }

                    ob.MC_BUYER_ID = (dr["MC_BUYER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BUYER_ID"]);

                    ob.MC_INQR_H_ID = (dr["MC_INQR_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_INQR_H_ID"]);
                    ob.STYLE_NO = (dr["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO"]);
                    ob.STYL_EXT_NO = (dr["STYL_EXT_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYL_EXT_NO"]);
                    ob.REF_STYLE_NO = (dr["REF_STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REF_STYLE_NO"]);
                    ob.COMP_STYLE_NO = (dr["COMP_STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_STYLE_NO"]);
                    ob.STYLE_DESC = (dr["STYLE_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_DESC"]);
                    ob.ORIGIN_ID = (dr["ORIGIN_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ORIGIN_ID"]);
                    ob.MANUF_ID = (dr["MANUF_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MANUF_ID"]);
                    ob.REVISION_NO = (dr["REVISION_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REVISION_NO"]);
                    ob.REVISION_DT = (dr["REVISION_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["REVISION_DT"]);

                    if (dr["NO_OF_REV"] != DBNull.Value)
                    {
                        ob.NO_OF_REV = Convert.ToInt64(dr["NO_OF_REV"]);
                    }

                    ob.NO_OF_SET = (dr["NO_OF_SET"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["NO_OF_SET"]);

                    if (dr["QTY_MOU_ID"] != DBNull.Value)
                    {
                        ob.QTY_MOU_ID = Convert.ToInt64(dr["QTY_MOU_ID"]);
                    }

                    if (dr["MC_BYR_ACC_ID"] != DBNull.Value)
                    {
                        ob.MC_BYR_ACC_ID = Convert.ToInt64(dr["MC_BYR_ACC_ID"]);
                    }

                    if (dr["PCS_PER_PACK"] != DBNull.Value)
                    {
                        ob.PCS_PER_PACK = Convert.ToInt64(dr["PCS_PER_PACK"]);
                    }

                    if (dr["LK_SEASON_ID"] != DBNull.Value)
                    {
                        ob.LK_SEASON_ID = Convert.ToInt64(dr["LK_SEASON_ID"]);
                    }

                    ob.SEASON_REF = (dr["SEASON_REF"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SEASON_REF"]);
                    ob.SZ_RANGE = (dr["SZ_RANGE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SZ_RANGE"]);
                    ob.HAS_SET = (dr["HAS_SET"] == DBNull.Value) ? "N" : Convert.ToString(dr["HAS_SET"]);
                    ob.HAS_COMBO = (dr["HAS_COMBO"] == DBNull.Value) ? "N" : Convert.ToString(dr["HAS_COMBO"]);
                    ob.HAS_MULTI_COL_PACK = (dr["HAS_MULTI_COL_PACK"] == DBNull.Value) ? "N" : Convert.ToString(dr["HAS_MULTI_COL_PACK"]);
                    ob.HAS_MODEL = (dr["HAS_MODEL"] == DBNull.Value) ? "N" : Convert.ToString(dr["HAS_MODEL"]);
                    ob.INSTRUCTION = (dr["INSTRUCTION"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["INSTRUCTION"]);
                    ob.CONCEPT_NAME = (dr["CONCEPT_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CONCEPT_NAME"]);
                    ob.BRAND_NAME = (dr["BRAND_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BRAND_NAME"]);
                    ob.LK_STYL_DEV_ID = (dr["LK_STYL_DEV_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_STYL_DEV_ID"]);
                    ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);
                    ob.MC_STYLE_H_EXT_ID = (dr["MC_STYLE_H_EXT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_H_EXT_ID"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<MC_STYLE_HModel> StyleNoLabdipRefAuto(String STYLE_NO, Int64 MC_COLOR_ID)
        {
            string sp = "pkg_merchandising.mc_style_h_select";
            try
            {
                var obList = new List<MC_STYLE_HModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pSTYLE_NO", Value =STYLE_NO},
                     new CommandParameter() {ParameterName = "pMC_COLOR_ID", Value =MC_COLOR_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3011},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_STYLE_HModel ob = new MC_STYLE_HModel();

                    ob.STYLE_NO = (dr["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO"]);
                    ob.APRV_LD_REF = (dr["APRV_LD_REF"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["APRV_LD_REF"]);
                    ob.MC_COLOR_GRP_ID = (dr["MC_COLOR_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_COLOR_GRP_ID"]);
                    ob.LK_DYE_MTHD_ID = (dr["LK_DYE_MTHD_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_DYE_MTHD_ID"]);
                    ob.MC_STYLE_H_EXT_ID = (dr["MC_STYLE_H_EXT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_H_EXT_ID"]);
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public MC_STYLE_HModel Select(long ID, Int64? pLK_STYL_DEV_ID)
        {
            string sp = "pkg_merchandising.mc_style_h_select";
            try
            {
                var ob = new MC_STYLE_HModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_STYLE_H_ID", Value =ID},
                     new CommandParameter() {ParameterName = "pLK_STYL_DEV_ID", Value =pLK_STYL_DEV_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.MC_STYLE_H_ID = (dr["MC_STYLE_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_H_ID"]);

                    if (dr["PARENT_ID"] != DBNull.Value)
                    {
                        ob.PARENT_ID = Convert.ToInt64(dr["PARENT_ID"]);
                    }



                    ob.MC_INQR_H_ID = (dr["MC_INQR_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_INQR_H_ID"]);
                    ob.STYLE_NO = (dr["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO"]);
                    ob.STYL_EXT_NO = (dr["STYL_EXT_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYL_EXT_NO"]);
                    ob.REF_STYLE_NO = (dr["REF_STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REF_STYLE_NO"]);
                    ob.COMP_STYLE_NO = (dr["COMP_STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_STYLE_NO"]);
                    ob.STYLE_DESC = (dr["STYLE_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_DESC"]);
                    ob.ORIGIN_ID = (dr["ORIGIN_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ORIGIN_ID"]);
                    ob.MANUF_ID = (dr["MANUF_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MANUF_ID"]);
                    //ob.REVISION_NO = (dr["REVISION_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REVISION_NO"]);
                    //ob.REVISION_DT = (dr["REVISION_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["REVISION_DT"]);

                    if (dr["NO_OF_REV"] != DBNull.Value)
                    {
                        ob.NO_OF_REV = Convert.ToInt64(dr["NO_OF_REV"]);
                    }

                    if (dr["MC_BYR_ACC_ID"] != DBNull.Value)
                    {
                        ob.MC_BYR_ACC_ID = Convert.ToInt64(dr["MC_BYR_ACC_ID"]);
                    }

                    ob.NO_OF_SET = (dr["NO_OF_SET"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["NO_OF_SET"]);

                    if (dr["QTY_MOU_ID"] != DBNull.Value)
                    {
                        ob.QTY_MOU_ID = Convert.ToInt64(dr["QTY_MOU_ID"]);
                    }

                    if (dr["PCS_PER_PACK"] != DBNull.Value)
                    {
                        ob.PCS_PER_PACK = Convert.ToInt64(dr["PCS_PER_PACK"]);
                    }


                    if (dr["LK_SEASON_ID"] != DBNull.Value)
                    {
                        ob.LK_SEASON_ID = Convert.ToInt64(dr["LK_SEASON_ID"]);
                    }
                    ob.SEASON_REF = (dr["SEASON_REF"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SEASON_REF"]);
                    ob.SZ_RANGE = (dr["SZ_RANGE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SZ_RANGE"]);
                    ob.HAS_SET = (dr["HAS_SET"] == DBNull.Value) ? "N" : Convert.ToString(dr["HAS_SET"]);
                    ob.HAS_COMBO = (dr["HAS_COMBO"] == DBNull.Value) ? "N" : Convert.ToString(dr["HAS_COMBO"]);
                    ob.HAS_MODEL = (dr["HAS_MODEL"] == DBNull.Value) ? "N" : Convert.ToString(dr["HAS_MODEL"]);
                    ob.HAS_MULTI_COL_PACK = (dr["HAS_MULTI_COL_PACK"] == DBNull.Value) ? "N" : Convert.ToString(dr["HAS_MULTI_COL_PACK"]);

                    //ob.INSTRUCTION = (dr["INSTRUCTION"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["INSTRUCTION"]);
                    //ob.CONCEPT_NAME = (dr["CONCEPT_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CONCEPT_NAME"]);
                    //ob.BRAND_NAME = (dr["BRAND_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BRAND_NAME"]);
                    ob.LK_STYL_DEV_ID = (dr["LK_STYL_DEV_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_STYL_DEV_ID"]);
                    ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);
                    ob.MC_BUYER_ID = (dr["MC_BUYER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BUYER_ID"]);

                    ob.MC_STYLE_H_EXT_ID = (dr["MC_STYLE_H_EXT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_H_EXT_ID"]);
                    ob.MC_ORDER_H_ID = (dr["MC_ORDER_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_ORDER_H_ID"]);

                    ob.ITEM_LIST = (dr["ITEM_LIST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_LIST"]);
                    ob.ALL_ITEM_LIST = (dr["ALL_ITEM_LIST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ALL_ITEM_LIST"]);
                    //ob.EXT_XML = (dr["EXT_XML"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EXT_XML"]);
                    ob.MC_SIZE_LST = (dr["MC_SIZE_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MC_SIZE_LST"]);
                    ob.HR_COUNTRY_ID = (dr["HR_COUNTRY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COUNTRY_ID"]);

                    ob.CREATED_BY_TXT = (dr["CREATED_BY_TXT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CREATED_BY_TXT"]);
                    ob.CREATION_DATE_TXT = (dr["CREATION_DATE_TXT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CREATION_DATE_TXT"]);


                    var ds1 = db.ExecuteStoredProcedure(new List<CommandParameter>()
                                {
                                     new CommandParameter() {ParameterName = "pMC_STYLE_H_ID", Value =ob.MC_STYLE_H_ID},
                                     new CommandParameter() {ParameterName = "pOption", Value =3005},
                                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                                 }, sp);


                    foreach (DataRow dr1 in ds1.Tables[0].Rows)
                    {
                        MC_STYLE_D_ITEMModel ob1 = new MC_STYLE_D_ITEMModel();
                        ob1.MC_STYLE_D_ITEM_ID = (dr1["MC_STYLE_D_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr1["MC_STYLE_D_ITEM_ID"]);
                        ob1.ITEM_NAME_EN = (dr1["ITEM_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["ITEM_NAME_EN"]);
                        ob1.MODEL_NO = (dr1["MODEL_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["MODEL_NO"]);
                        ob1.COMBO_NO = (dr1["COMBO_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["COMBO_NO"]);
                        ob1.SEGMENT_FLAG = (dr1["SEGMENT_FLAG"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["SEGMENT_FLAG"]);
                        ob.items.Add(ob1);

                    }




                }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<MC_STYLE_HModel> BuyerWiseStyleList(Int64 pMC_BUYER_ID)
        {
            string sp = "pkg_merchandising.mc_style_h_select";
            try
            {
                var obList = new List<MC_STYLE_HModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_BUYER_ID", Value = pMC_BUYER_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3002},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_STYLE_HModel ob = new MC_STYLE_HModel();
                    ob.MC_STYLE_H_ID = (dr["MC_STYLE_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_H_ID"]);

                    if (dr["PARENT_ID"] != DBNull.Value)
                    {
                        ob.PARENT_ID = Convert.ToInt64(dr["PARENT_ID"]);
                    }

                    ob.MC_INQR_H_ID = (dr["MC_INQR_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_INQR_H_ID"]);
                    ob.STYLE_NO = (dr["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO"]);
                    ob.STYL_EXT_NO = (dr["STYL_EXT_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYL_EXT_NO"]);
                    ob.REF_STYLE_NO = (dr["REF_STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REF_STYLE_NO"]);
                    ob.COMP_STYLE_NO = (dr["COMP_STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_STYLE_NO"]);
                    ob.STYLE_DESC = (dr["STYLE_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_DESC"]);
                    ob.ORIGIN_ID = (dr["ORIGIN_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ORIGIN_ID"]);
                    ob.MANUF_ID = (dr["MANUF_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MANUF_ID"]);
                    //ob.REVISION_NO = (dr["REVISION_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REVISION_NO"]);
                    //ob.REVISION_DT = (dr["REVISION_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["REVISION_DT"]);
                    //ob.NO_OF_REV = (dr["NO_OF_REV"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["NO_OF_REV"]);
                    ob.LK_SEASON_ID = (dr["LK_SEASON_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_SEASON_ID"]);
                    ob.SEASON_REF = (dr["SEASON_REF"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SEASON_REF"]);
                    ob.SZ_RANGE = (dr["SZ_RANGE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SZ_RANGE"]);
                    ob.HAS_SET = (dr["HAS_SET"] == DBNull.Value) ? "N" : Convert.ToString(dr["HAS_SET"]);
                    ob.HAS_COMBO = (dr["HAS_COMBO"] == DBNull.Value) ? "N" : Convert.ToString(dr["HAS_COMBO"]);
                    ob.HAS_MODEL = (dr["HAS_MODEL"] == DBNull.Value) ? "N" : Convert.ToString(dr["HAS_MODEL"]);

                    ob.INSTRUCTION = (dr["INSTRUCTION"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["INSTRUCTION"]);
                    ob.CONCEPT_NAME = (dr["CONCEPT_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CONCEPT_NAME"]);
                    ob.BRAND_NAME = (dr["BRAND_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BRAND_NAME"]);
                    ob.LK_STYL_DEV_ID = (dr["LK_STYL_DEV_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_STYL_DEV_ID"]);
                    ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);
                    ob.MC_BUYER_ID = (dr["MC_BUYER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BUYER_ID"]);
                    //ob.MC_STYLE_H_EXT_ID = (dr["MC_STYLE_H_EXT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_H_EXT_ID"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<MC_STYLE_HModel> GetStyleList(Int64? pMC_BYR_ACC_GRP_ID = null, Int64? pMC_BYR_ACC_ID = null, Int64? pMC_STYLE_H_ID = null, string pSTYLE_NO = null)
        {
            string sp = "pkg_mc_style.mc_style_h_select";
            try
            {
                var obList = new List<MC_STYLE_HModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_BYR_ACC_GRP_ID", Value =pMC_BYR_ACC_GRP_ID},
                     new CommandParameter() {ParameterName = "pMC_BYR_ACC_ID", Value =pMC_BYR_ACC_ID},
                     new CommandParameter() {ParameterName = "pMC_STYLE_H_ID", Value =pMC_STYLE_H_ID},
                     new CommandParameter() {ParameterName = "pSTYLE_NO", Value =pSTYLE_NO},

                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_STYLE_HModel ob = new MC_STYLE_HModel();

                    ob.STYLE_NO = (dr["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO"]);
                    ob.STYLE_DESC = (dr["STYLE_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_DESC"]);

                    ob.MC_STYLE_H_ID = (dr["MC_STYLE_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_H_ID"]);
                    ob.MC_BUYER_ID = (dr["MC_BUYER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BUYER_ID"]);
                    ob.MC_BYR_ACC_ID = (dr["MC_BYR_ACC_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BYR_ACC_ID"]);
                    ob.MC_BYR_ACC_GRP_ID = (dr["MC_BYR_ACC_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BYR_ACC_GRP_ID"]);

                    ob.HAS_SET = (dr["HAS_SET"] == DBNull.Value) ? "N" : Convert.ToString(dr["HAS_SET"]);
                    ob.HAS_COMBO = (dr["HAS_COMBO"] == DBNull.Value) ? "N" : Convert.ToString(dr["HAS_COMBO"]);
                    ob.HAS_MULTI_COL_PACK = (dr["HAS_MULTI_COL_PACK"] == DBNull.Value) ? "N" : Convert.ToString(dr["HAS_MULTI_COL_PACK"]);

                    if (dr["PCS_PER_PACK"] != DBNull.Value)
                    {
                        ob.PCS_PER_PACK = Convert.ToInt64(dr["PCS_PER_PACK"]);
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

        public List<MC_STYLE_HModel> BuyerWiseStyleListData(Int64 pMC_BUYER_ID)
        {
            string sp = "pkg_merchandising.mc_style_h_select";
            try
            {
                var obList = new List<MC_STYLE_HModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_BUYER_ID", Value = pMC_BUYER_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3002},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_STYLE_HModel ob = new MC_STYLE_HModel();
                    ob.MC_STYLE_H_ID = (dr["MC_STYLE_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_H_ID"]);

                    if (dr["PARENT_ID"] != DBNull.Value)
                    {
                        ob.PARENT_ID = Convert.ToInt64(dr["PARENT_ID"]);
                    }
                    ob.MC_INQR_H_ID = (dr["MC_INQR_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_INQR_H_ID"]);
                    ob.STYLE_NO = (dr["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO"]);
                    ob.STYL_EXT_NO = (dr["STYL_EXT_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYL_EXT_NO"]);
                    ob.REF_STYLE_NO = (dr["REF_STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REF_STYLE_NO"]);
                    ob.COMP_STYLE_NO = (dr["COMP_STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_STYLE_NO"]);
                    ob.STYLE_DESC = (dr["STYLE_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_DESC"]);
                    ob.ORIGIN_ID = (dr["ORIGIN_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ORIGIN_ID"]);
                    ob.MANUF_ID = (dr["MANUF_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MANUF_ID"]);
                    ob.REVISION_NO = (dr["REVISION_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REVISION_NO"]);
                    ob.REVISION_DT = (dr["REVISION_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["REVISION_DT"]);

                    if (dr["NO_OF_REV"] != DBNull.Value)
                    {
                        ob.NO_OF_REV = Convert.ToInt64(dr["NO_OF_REV"]);
                    }

                    if (dr["MC_BYR_ACC_ID"] != DBNull.Value)
                    {
                        ob.MC_BYR_ACC_ID = Convert.ToInt64(dr["MC_BYR_ACC_ID"]);
                    }

                    ob.NO_OF_SET = (dr["NO_OF_SET"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["NO_OF_SET"]);

                    if (dr["QTY_MOU_ID"] != DBNull.Value)
                    {
                        ob.QTY_MOU_ID = Convert.ToInt64(dr["QTY_MOU_ID"]);
                    }

                    if (dr["PCS_PER_PACK"] != DBNull.Value)
                    {
                        ob.PCS_PER_PACK = Convert.ToInt64(dr["PCS_PER_PACK"]);
                    }


                    if (dr["LK_SEASON_ID"] != DBNull.Value)
                    {
                        ob.LK_SEASON_ID = Convert.ToInt64(dr["LK_SEASON_ID"]);
                    }
                    ob.SEASON_REF = (dr["SEASON_REF"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SEASON_REF"]);
                    ob.SZ_RANGE = (dr["SZ_RANGE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SZ_RANGE"]);
                    ob.HAS_SET = (dr["HAS_SET"] == DBNull.Value) ? "N" : Convert.ToString(dr["HAS_SET"]);
                    ob.HAS_COMBO = (dr["HAS_COMBO"] == DBNull.Value) ? "N" : Convert.ToString(dr["HAS_COMBO"]);
                    ob.HAS_MODEL = (dr["HAS_MODEL"] == DBNull.Value) ? "N" : Convert.ToString(dr["HAS_MODEL"]);
                    ob.HAS_MULTI_COL_PACK = (dr["HAS_MULTI_COL_PACK"] == DBNull.Value) ? "N" : Convert.ToString(dr["HAS_MULTI_COL_PACK"]);

                    ob.INSTRUCTION = (dr["INSTRUCTION"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["INSTRUCTION"]);
                    ob.CONCEPT_NAME = (dr["CONCEPT_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CONCEPT_NAME"]);
                    ob.BRAND_NAME = (dr["BRAND_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BRAND_NAME"]);
                    ob.LK_STYL_DEV_ID = (dr["LK_STYL_DEV_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_STYL_DEV_ID"]);
                    ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);
                    ob.MC_BUYER_ID = (dr["MC_BUYER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BUYER_ID"]);

                    //if (dr["MC_STYLE_H_EXT_ID"] != DBNull.Value)
                    //    ob.MC_STYLE_H_EXT_ID = (dr["MC_STYLE_H_EXT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_H_EXT_ID"]);


                    ob.ITEM_LIST = (dr["ITEM_LIST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_LIST"]);
                    ob.ALL_ITEM_LIST = (dr["ALL_ITEM_LIST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ALL_ITEM_LIST"]);

                    //ob.EXT_XML = (dr["EXT_XML"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EXT_XML"]);
                    ob.MC_SIZE_LST = (dr["MC_SIZE_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MC_SIZE_LST"]);

                    var ds1 = db.ExecuteStoredProcedure(new List<CommandParameter>()
                                {
                                     new CommandParameter() {ParameterName = "pMC_STYLE_H_ID", Value =ob.MC_STYLE_H_ID},
                                     new CommandParameter() {ParameterName = "pOption", Value =3005},
                                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                                 }, sp);


                    foreach (DataRow dr1 in ds1.Tables[0].Rows)
                    {
                        MC_STYLE_D_ITEMModel ob1 = new MC_STYLE_D_ITEMModel();
                        ob1.MC_STYLE_D_ITEM_ID = (dr1["MC_STYLE_D_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr1["MC_STYLE_D_ITEM_ID"]);
                        ob1.ITEM_NAME_EN = (dr1["ITEM_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["ITEM_NAME_EN"]);
                        ob1.MODEL_NO = (dr1["MODEL_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["MODEL_NO"]);
                        ob1.COMBO_NO = (dr1["COMBO_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["COMBO_NO"]);
                        ob1.SEGMENT_FLAG = (dr1["SEGMENT_FLAG"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["SEGMENT_FLAG"]);
                        ob.items.Add(ob1);

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

        public List<MC_STYLE_HModel> BuyerWiseStyleListDataByID(Int64 pMC_BUYER_ID)
        {
            string sp = "pkg_merchandising.mc_style_h_select";
            try
            {
                var obList = new List<MC_STYLE_HModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_BUYER_ID", Value = pMC_BUYER_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3013},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_STYLE_HModel ob = new MC_STYLE_HModel();
                    ob.MC_STYLE_H_ID = (dr["MC_STYLE_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_H_ID"]);

                    if (dr["PARENT_ID"] != DBNull.Value)
                    {
                        ob.PARENT_ID = Convert.ToInt64(dr["PARENT_ID"]);
                    }
                    ob.MC_INQR_H_ID = (dr["MC_INQR_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_INQR_H_ID"]);
                    ob.STYLE_NO = (dr["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO"]);
                    ob.STYL_EXT_NO = (dr["STYL_EXT_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYL_EXT_NO"]);
                    ob.REF_STYLE_NO = (dr["REF_STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REF_STYLE_NO"]);
                    ob.COMP_STYLE_NO = (dr["COMP_STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_STYLE_NO"]);
                    ob.STYLE_DESC = (dr["STYLE_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_DESC"]);
                    ob.ORIGIN_ID = (dr["ORIGIN_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ORIGIN_ID"]);
                    ob.MANUF_ID = (dr["MANUF_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MANUF_ID"]);
                    ob.REVISION_NO = (dr["REVISION_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REVISION_NO"]);
                    ob.REVISION_DT = (dr["REVISION_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["REVISION_DT"]);

                    if (dr["NO_OF_REV"] != DBNull.Value)
                    {
                        ob.NO_OF_REV = Convert.ToInt64(dr["NO_OF_REV"]);
                    }

                    if (dr["MC_BYR_ACC_ID"] != DBNull.Value)
                    {
                        ob.MC_BYR_ACC_ID = Convert.ToInt64(dr["MC_BYR_ACC_ID"]);
                    }

                    ob.NO_OF_SET = (dr["NO_OF_SET"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["NO_OF_SET"]);

                    if (dr["QTY_MOU_ID"] != DBNull.Value)
                    {
                        ob.QTY_MOU_ID = Convert.ToInt64(dr["QTY_MOU_ID"]);
                    }

                    if (dr["PCS_PER_PACK"] != DBNull.Value)
                    {
                        ob.PCS_PER_PACK = Convert.ToInt64(dr["PCS_PER_PACK"]);
                    }


                    if (dr["LK_SEASON_ID"] != DBNull.Value)
                    {
                        ob.LK_SEASON_ID = Convert.ToInt64(dr["LK_SEASON_ID"]);
                    }
                    ob.SEASON_REF = (dr["SEASON_REF"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SEASON_REF"]);
                    ob.SZ_RANGE = (dr["SZ_RANGE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SZ_RANGE"]);
                    ob.HAS_SET = (dr["HAS_SET"] == DBNull.Value) ? "N" : Convert.ToString(dr["HAS_SET"]);
                    ob.HAS_COMBO = (dr["HAS_COMBO"] == DBNull.Value) ? "N" : Convert.ToString(dr["HAS_COMBO"]);
                    ob.HAS_MODEL = (dr["HAS_MODEL"] == DBNull.Value) ? "N" : Convert.ToString(dr["HAS_MODEL"]);
                    ob.HAS_MULTI_COL_PACK = (dr["HAS_MULTI_COL_PACK"] == DBNull.Value) ? "N" : Convert.ToString(dr["HAS_MULTI_COL_PACK"]);

                    ob.INSTRUCTION = (dr["INSTRUCTION"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["INSTRUCTION"]);
                    ob.CONCEPT_NAME = (dr["CONCEPT_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CONCEPT_NAME"]);
                    ob.BRAND_NAME = (dr["BRAND_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BRAND_NAME"]);
                    ob.LK_STYL_DEV_ID = (dr["LK_STYL_DEV_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_STYL_DEV_ID"]);
                    ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);
                    ob.MC_BUYER_ID = (dr["MC_BUYER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BUYER_ID"]);

                    ob.MC_STYLE_H_EXT_ID = (dr["MC_STYLE_H_EXT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_H_EXT_ID"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<MC_STYLE_HModel> BuyerAccWiseStyleListData(Int64? pMC_BYR_ACC_ID)
        {
            string sp = "pkg_merchandising.mc_style_h_select";
            try
            {
                var obList = new List<MC_STYLE_HModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_BYR_ACC_ID", Value = pMC_BYR_ACC_ID},
                     new CommandParameter() {ParameterName = "pOption", Value = 3008},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_STYLE_HModel ob = new MC_STYLE_HModel();
                    ob.MC_STYLE_H_ID = (dr["MC_STYLE_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_H_ID"]);

                    if (dr["PARENT_ID"] != DBNull.Value)
                    {
                        ob.PARENT_ID = Convert.ToInt64(dr["PARENT_ID"]);
                    }

                    ob.MC_INQR_H_ID = (dr["MC_INQR_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_INQR_H_ID"]);
                    ob.STYLE_NO = (dr["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO"]);
                    ob.STYL_EXT_NO = (dr["STYL_EXT_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYL_EXT_NO"]);
                    ob.REF_STYLE_NO = (dr["REF_STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REF_STYLE_NO"]);
                    ob.COMP_STYLE_NO = (dr["COMP_STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_STYLE_NO"]);
                    ob.STYLE_DESC = (dr["STYLE_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_DESC"]);
                    ob.ORIGIN_ID = (dr["ORIGIN_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ORIGIN_ID"]);
                    ob.MANUF_ID = (dr["MANUF_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MANUF_ID"]);
                    //ob.REVISION_NO = (dr["REVISION_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REVISION_NO"]);
                    //ob.REVISION_DT = (dr["REVISION_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["REVISION_DT"]);

                    if (dr["NO_OF_REV"] != DBNull.Value)
                    {
                        ob.NO_OF_REV = Convert.ToInt64(dr["NO_OF_REV"]);
                    }

                    if (dr["MC_BYR_ACC_ID"] != DBNull.Value)
                    {
                        ob.MC_BYR_ACC_ID = Convert.ToInt64(dr["MC_BYR_ACC_ID"]);
                    }

                    ob.NO_OF_SET = (dr["NO_OF_SET"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["NO_OF_SET"]);

                    if (dr["QTY_MOU_ID"] != DBNull.Value)
                    {
                        ob.QTY_MOU_ID = Convert.ToInt64(dr["QTY_MOU_ID"]);
                    }

                    if (dr["PCS_PER_PACK"] != DBNull.Value)
                    {
                        ob.PCS_PER_PACK = Convert.ToInt64(dr["PCS_PER_PACK"]);
                    }


                    if (dr["LK_SEASON_ID"] != DBNull.Value)
                    {
                        ob.LK_SEASON_ID = Convert.ToInt64(dr["LK_SEASON_ID"]);
                    }
                    ob.SEASON_REF = (dr["SEASON_REF"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SEASON_REF"]);
                    ob.SZ_RANGE = (dr["SZ_RANGE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SZ_RANGE"]);
                    ob.HAS_SET = (dr["HAS_SET"] == DBNull.Value) ? "N" : Convert.ToString(dr["HAS_SET"]);
                    ob.HAS_COMBO = (dr["HAS_COMBO"] == DBNull.Value) ? "N" : Convert.ToString(dr["HAS_COMBO"]);
                    ob.HAS_MODEL = (dr["HAS_MODEL"] == DBNull.Value) ? "N" : Convert.ToString(dr["HAS_MODEL"]);
                    ob.HAS_MULTI_COL_PACK = (dr["HAS_MULTI_COL_PACK"] == DBNull.Value) ? "N" : Convert.ToString(dr["HAS_MULTI_COL_PACK"]);

                    //ob.INSTRUCTION = (dr["INSTRUCTION"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["INSTRUCTION"]);
                    //ob.CONCEPT_NAME = (dr["CONCEPT_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CONCEPT_NAME"]);
                    //ob.BRAND_NAME = (dr["BRAND_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BRAND_NAME"]);
                    ob.LK_STYL_DEV_ID = (dr["LK_STYL_DEV_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_STYL_DEV_ID"]);
                    ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);
                    ob.MC_BUYER_ID = (dr["MC_BUYER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BUYER_ID"]);

                    //ob.MC_STYLE_H_EXT_ID = (dr["MC_STYLE_H_EXT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_H_EXT_ID"]);


                    ob.ITEM_LIST = (dr["ITEM_LIST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_LIST"]);
                    ob.ALL_ITEM_LIST = (dr["ALL_ITEM_LIST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ALL_ITEM_LIST"]);

                    var ds1 = db.ExecuteStoredProcedure(new List<CommandParameter>()
                                {
                                     new CommandParameter() {ParameterName = "pMC_STYLE_H_ID", Value =ob.MC_STYLE_H_ID},
                                     new CommandParameter() {ParameterName = "pOption", Value =3005},
                                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                                 }, sp);


                    foreach (DataRow dr1 in ds1.Tables[0].Rows)
                    {
                        MC_STYLE_D_ITEMModel ob1 = new MC_STYLE_D_ITEMModel();
                        ob1.MC_STYLE_D_ITEM_ID = (dr1["MC_STYLE_D_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr1["MC_STYLE_D_ITEM_ID"]);
                        ob1.ITEM_NAME_EN = (dr1["ITEM_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["ITEM_NAME_EN"]);
                        ob1.MODEL_NO = (dr1["MODEL_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["MODEL_NO"]);
                        ob1.COMBO_NO = (dr1["COMBO_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["COMBO_NO"]);
                        ob1.SEGMENT_FLAG = (dr1["SEGMENT_FLAG"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["SEGMENT_FLAG"]);
                        ob.items.Add(ob1);

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

        public string getStyleExtAuto(Int64 MC_STYLE_H_ID)
        {
            string sp = "pkg_merchandising.mc_style_h_select";
            string ext = string.Empty;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pOption", Value = 3010},
                     new CommandParameter() {ParameterName = "pMC_STYLE_H_ID", Value = MC_STYLE_H_ID},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ext = (dr["EXT"] == DBNull.Value) ? String.Empty : Convert.ToString(dr["EXT"]);
                }

                return ext;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<MC_STYLE_HModel> getStyleDropDown(string pSTYLE_NO, Int64? pMC_BUYER_ID, Int64? pMC_STYLE_H_ID)
        {
            string sp = "pkg_merchandising.mc_style_h_select";
            try
            {
                var obList = new List<MC_STYLE_HModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pSTYLE_NO", Value = pSTYLE_NO},
                     new CommandParameter() {ParameterName = "pMC_BUYER_ID", Value = pMC_BUYER_ID},
                     new CommandParameter() {ParameterName = "pMC_STYLE_H_ID", Value = pMC_STYLE_H_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3012},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_STYLE_HModel ob = new MC_STYLE_HModel();
                    ob.STYLE_NO = (dr["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO"]);
                    ob.MC_STYLE_H_ID = (dr["MC_STYLE_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_H_ID"]);
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object BuyerAcWiseStyleList(Int64 pageNumber, Int64 pageSize, Int64? pMC_BYR_ACC_ID, Int64? pMC_STYLE_H_ID = null, String pSTYLE_NO = null)
        {
            string sp = "pkg_merchandising.mc_style_h_select";
            try
            {
                Int64 vTotalRec = 0;
                var obList = new List<MC_STYLE_HModel>();
                var obj = new RF_PAGERModel();

                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_BYR_ACC_ID", Value = pMC_BYR_ACC_ID},
                     new CommandParameter() {ParameterName = "pMC_STYLE_H_ID", Value = pMC_STYLE_H_ID},
                     new CommandParameter() {ParameterName = "pageNumber", Value = pageNumber},
                     new CommandParameter() {ParameterName = "pageSize", Value = pageSize},
                     new CommandParameter() {ParameterName = "pSTYLE_NO", Value = pSTYLE_NO},
                    
                     new CommandParameter() {ParameterName = "pOption", Value = 3014},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);

                foreach (DataRow dr in ds.Tables["OUTPARAM"].Rows)
                {
                    obj.total = (dr["VALUE"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["VALUE"]);

                }

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_STYLE_HModel ob = new MC_STYLE_HModel();
                    ob.MC_STYLE_H_ID = (dr["MC_STYLE_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_H_ID"]);

                    ob.STYLE_NO = (dr["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO"]);

                    ob.STYLE_DESC = (dr["STYLE_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_DESC"]);
                    ob.SEASON = (dr["SEASON"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SEASON"]);
                    ob.SZ_RANGE = (dr["SZ_RANGE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SZ_RANGE"]);
                    ob.MC_BUYER_ID = (dr["MC_BUYER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BUYER_ID"]);

                    ob.BUYER_NAME_EN = (dr["BUYER_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BUYER_NAME_EN"]);

                    ob.LK_STYL_DEV_ID = (dr["LK_STYL_DEV_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_STYL_DEV_ID"]);


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
        public dynamic GetOrderCofirmationMailData(Int64 pMC_ORDER_H_ID)
        {
            string sp = "pkg_merchandising.mc_style_h_select";
            try
            {
                dynamic ob = new System.Dynamic.ExpandoObject();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_ORDER_H_ID", Value = pMC_ORDER_H_ID},
                     new CommandParameter() {ParameterName = "pOption", Value = 3015},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.MAIL_LIST = (dr["MAIL_LIST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MAIL_LIST"]);
                    ob.DESC = (dr["DESCRIPTION"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DESCRIPTION"]);
                    ob.REDIRECT_URL = (dr["REDIRECT_URL"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REDIRECT_URL"]);
                }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public string CREATED_BY_TXT { get; set; }

        public string CREATION_DATE_TXT { get; set; }
    }
}