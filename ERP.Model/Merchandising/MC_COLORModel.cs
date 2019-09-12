using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Web;
using System.ComponentModel;

namespace ERP.Model
{
    public class MC_COLORModel
    {
        public Int64 MC_COLOR_ID { get; set; }
        public string COLOR_CODE { get; set; }
        [DisplayName("Colour Name")]
        [Required(ErrorMessage = " The field {0} is required")]
        public string COLOR_NAME_EN { get; set; }
        public string COLOR_NAME_BN { get; set; }
        public string COLOR_EQUIV { get; set; }
        public string COLOR_SNAME { get; set; }
        public string PANTON_NO { get; set; }
        public Int64 PNT_VERSION_NO { get; set; }
        public string IS_SWATCH { get; set; }
        public string COL_IMAGE { get; set; }
        public string IS_ACTIVE { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }
        public string BUYER_NAME_EN { get; set; }
        public string COLOR_REF { get; set; }
        public string OTHER_REF { get; set; }
        public string IS_DUMMY_COLOR { get; set; }
        public string IS_ANY_COLOR { get; set; }
        public string IS_AVAILABLE_COLOR { get; set; }

        public Int64? MC_BUYER_ID { get; set; }

        public Int64? MC_STYLE_H_ID { get; set; }
        public Int64? MC_BU_COL_REF_ID { get; set; }
        public string XML { get; set; }
        public Int64 LK_COL_TYPE_ID { get; set; }
        public Int64? AOP_BASE_COL_ID { get; set; }
        public string AOP_PRNT_COL_LST { get; set; }
        public string YD_COL_LST { get; set; }

        public string COL_TYPE_NAME { get; set; }

        public Int64? MC_ORDER_H_ID { get; set; }
        public Int64? MC_BYR_ACC_ID { get; set; }
        public string APRV_LD_REF { get; set; }
        public Int64? MC_COLOR_GRP_ID { get; set; }
        public Int64? RF_FIB_COMP_ID { get; set; }
        public string IS_LD_WIP { get; set; }
        public Int64? LK_DYE_MTHD_ID { get; set; }
        public string MC_STYLE_D_ITEM_LST { get; set; }
        public string IS_GMT_COL { get; set; }
        public string IS_ADD_SD_COL { get; set; }
        public object OB_COLOUR_GROUP { get; set; }
        public object OB_COLOR { get; set; }
        public object OB_FIBER_COMP { get; set; }
        public object OB_DYE_METHOD { get; set; }





        public string Save()
        {
            const string sp = "pkg_merchandising.mc_color_insert";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_COLOR_ID", Value = ob.MC_COLOR_ID},
                     new CommandParameter() {ParameterName = "pCOLOR_CODE", Value = ob.COLOR_CODE},
                     new CommandParameter() {ParameterName = "pCOLOR_NAME_EN", Value = ob.COLOR_NAME_EN},
                     new CommandParameter() {ParameterName = "pCOLOR_NAME_BN", Value = ob.COLOR_NAME_BN},
                     new CommandParameter() {ParameterName = "pCOLOR_EQUIV", Value = ob.COLOR_EQUIV},
                     new CommandParameter() {ParameterName = "pCOLOR_SNAME", Value = ob.COLOR_SNAME},
                     new CommandParameter() {ParameterName = "pPANTON_NO", Value = ob.PANTON_NO},
                     new CommandParameter() {ParameterName = "pPNT_VERSION_NO", Value = ob.PNT_VERSION_NO},
                     new CommandParameter() {ParameterName = "pIS_SWATCH", Value = ob.IS_SWATCH},
                     new CommandParameter() {ParameterName = "pCOL_IMAGE", Value = ob.COL_IMAGE},
                     new CommandParameter() {ParameterName = "pIS_ACTIVE", Value = ob.IS_ACTIVE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pLK_COL_TYPE_ID", Value =ob.LK_COL_TYPE_ID},
                     new CommandParameter() {ParameterName = "pAOP_BASE_COL_ID", Value =ob.AOP_BASE_COL_ID},
                     new CommandParameter() {ParameterName = "pAOP_PRNT_COL_LST", Value =ob.AOP_PRNT_COL_LST},
                     new CommandParameter() {ParameterName = "pYD_COL_LST", Value =ob.YD_COL_LST},
                     new CommandParameter() {ParameterName = "pMC_COLOR_GRP_ID", Value =ob.MC_COLOR_GRP_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =1000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output},
                     new CommandParameter() {ParameterName = "V_MC_COLOR_ID", Value =null, Direction = ParameterDirection.Output}
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
            const string sp = "pkg_merchandising.mc_color_update";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_COLOR_ID", Value = ob.MC_COLOR_ID},
                     new CommandParameter() {ParameterName = "pCOLOR_CODE", Value = ob.COLOR_CODE},
                     new CommandParameter() {ParameterName = "pCOLOR_NAME_EN", Value = ob.COLOR_NAME_EN},
                     new CommandParameter() {ParameterName = "pCOLOR_NAME_BN", Value = ob.COLOR_NAME_BN},
                     new CommandParameter() {ParameterName = "pCOLOR_EQUIV", Value = ob.COLOR_EQUIV},
                     new CommandParameter() {ParameterName = "pCOLOR_SNAME", Value = ob.COLOR_SNAME},
                     new CommandParameter() {ParameterName = "pPANTON_NO", Value = ob.PANTON_NO},
                     new CommandParameter() {ParameterName = "pMC_COLOR_GRP_ID", Value =ob.MC_COLOR_GRP_ID},
                     new CommandParameter() {ParameterName = "pPNT_VERSION_NO", Value = ob.PNT_VERSION_NO},
                     new CommandParameter() {ParameterName = "pIS_SWATCH", Value = ob.IS_SWATCH},
                     new CommandParameter() {ParameterName = "pCOL_IMAGE", Value = ob.COL_IMAGE},
                     new CommandParameter() {ParameterName = "pIS_ACTIVE", Value = ob.IS_ACTIVE},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pLK_COL_TYPE_ID", Value =ob.LK_COL_TYPE_ID},
                     new CommandParameter() {ParameterName = "pAOP_BASE_COL_ID", Value =ob.AOP_BASE_COL_ID},
                     new CommandParameter() {ParameterName = "pAOP_PRNT_COL_LST", Value =ob.AOP_PRNT_COL_LST},
                     new CommandParameter() {ParameterName = "pYD_COL_LST", Value =ob.YD_COL_LST},
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


        public string UpdateWithXML()
        {
            const string sp = "pkg_merchandising.mc_color_update";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {

                     new CommandParameter() {ParameterName = "pXML", Value = ob.XML},
                     new CommandParameter() {ParameterName = "pMC_STYLE_H_ID", Value = ob.MC_STYLE_H_ID},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pOption", Value =2001},
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
            const string sp = "pkg_merchandising.mc_color_delete";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_COLOR_ID", Value = ob.MC_COLOR_ID},
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

        public List<MC_COLORModel> SelectAll(string pCOLOR_NAME_EN = null, int pOption = 3000)
        {
            string sp = "pkg_merchandising.mc_color_select";
            try
            {
                var obList = new List<MC_COLORModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_COLOR_ID", Value =0},
                     new CommandParameter() {ParameterName = "pCOLOR_NAME_EN", Value =pCOLOR_NAME_EN},
                     new CommandParameter() {ParameterName = "pOption", Value =pOption},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_COLORModel ob = new MC_COLORModel();
                    ob.MC_COLOR_ID = (dr["MC_COLOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_COLOR_ID"]);

                    if (dr["MC_COLOR_GRP_ID"] != DBNull.Value)
                    {
                        ob.MC_COLOR_GRP_ID = Convert.ToInt64(dr["MC_COLOR_GRP_ID"]);
                    }
                    ob.COLOR_CODE = (dr["COLOR_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_CODE"]);
                    ob.COLOR_NAME_EN = (dr["COLOR_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_NAME_EN"]);
                    ob.COLOR_NAME_BN = (dr["COLOR_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_NAME_BN"]);
                    ob.COLOR_EQUIV = (dr["COLOR_EQUIV"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_EQUIV"]);
                    ob.COLOR_SNAME = (dr["COLOR_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_SNAME"]);
                    ob.PANTON_NO = (dr["PANTON_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PANTON_NO"]);
                    ob.PNT_VERSION_NO = (dr["PNT_VERSION_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PNT_VERSION_NO"]);
                    ob.IS_SWATCH = (dr["IS_SWATCH"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_SWATCH"]);
                    ob.COL_IMAGE = (dr["COL_IMAGE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COL_IMAGE"]);
                    ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);
                    ob.LK_COL_TYPE_ID = (dr["LK_COL_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_COL_TYPE_ID"]);
                    ob.COL_TYPE_NAME = (dr["COL_TYPE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COL_TYPE_NAME"]);

                    if (dr["AOP_BASE_COL_ID"] != DBNull.Value)
                    {
                        ob.AOP_BASE_COL_ID = Convert.ToInt64(dr["AOP_BASE_COL_ID"]);
                    }
                    ob.AOP_PRNT_COL_LST = (dr["AOP_PRNT_COL_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["AOP_PRNT_COL_LST"]);
                    ob.YD_COL_LST = (dr["YD_COL_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["YD_COL_LST"]);

                    ob.IS_DUMMY_COLOR = (dr["IS_DUMMY_COLOR"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_DUMMY_COLOR"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public MC_COLORModel Select(long ID)
        {
            string sp = "pkg_merchandising.mc_color_select";
            try
            {
                var ob = new MC_COLORModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_COLOR_ID", Value =ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.MC_COLOR_ID = (dr["MC_COLOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_COLOR_ID"]);
                    ob.COLOR_CODE = (dr["COLOR_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_CODE"]);
                    ob.COLOR_NAME_EN = (dr["COLOR_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_NAME_EN"]);
                    ob.COLOR_NAME_BN = (dr["COLOR_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_NAME_BN"]);
                    ob.COLOR_EQUIV = (dr["COLOR_EQUIV"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_EQUIV"]);
                    ob.COLOR_SNAME = (dr["COLOR_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_SNAME"]);
                    ob.PANTON_NO = (dr["PANTON_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PANTON_NO"]);
                    ob.PNT_VERSION_NO = (dr["PNT_VERSION_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PNT_VERSION_NO"]);
                    ob.IS_SWATCH = (dr["IS_SWATCH"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_SWATCH"]);
                    ob.COL_IMAGE = (dr["COL_IMAGE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COL_IMAGE"]);
                    ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);
                    ob.LK_COL_TYPE_ID = (dr["LK_COL_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_COL_TYPE_ID"]);

                    if (dr["MC_COLOR_GRP_ID"] != DBNull.Value)
                    {
                        ob.MC_COLOR_GRP_ID = Convert.ToInt64(dr["MC_COLOR_GRP_ID"]);
                    }

                    if (dr["AOP_BASE_COL_ID"] != DBNull.Value)
                    {
                        ob.AOP_BASE_COL_ID = Convert.ToInt64(dr["AOP_BASE_COL_ID"]);
                    }
                    ob.AOP_PRNT_COL_LST = (dr["AOP_PRNT_COL_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["AOP_PRNT_COL_LST"]);
                    ob.YD_COL_LST = (dr["YD_COL_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["YD_COL_LST"]);
                }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object ColourBuyerListData()
        {
            string sp = "pkg_merchandising.mc_color_select";
            try
            {
                var obList = new List<MC_COLORModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pOption", Value =3002},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_COLORModel ob = new MC_COLORModel();
                    ob.COLOR_NAME_EN = (dr["COLOR_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_NAME_EN"]);
                    ob.BUYER_NAME_EN = (dr["BUYER_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BUYER_NAME_EN"]);
                    ob.MC_COLOR_ID = (dr["MC_COLOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_COLOR_ID"]);
                    ob.MC_BUYER_ID = (dr["MC_BUYER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BUYER_ID"]);
                    ob.COLOR_REF = (dr["COLOR_REF"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_REF"]);
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object ColourListByBuyerId(Int64 MC_BUYER_ID)
        {
            string sp = "pkg_merchandising.mc_color_select";
            try
            {
                var obList = new List<MC_COLORModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pOption", Value =3003},
                     new CommandParameter() {ParameterName = "pMC_BUYER_ID", Value =MC_BUYER_ID},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_COLORModel ob = new MC_COLORModel();

                    ob.MC_COLOR_ID = (dr["MC_COLOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_COLOR_ID"]);
                    ob.COLOR_CODE = (dr["COLOR_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_CODE"]);
                    ob.COLOR_NAME_EN = (dr["COLOR_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_NAME_EN"]);
                    ob.COLOR_NAME_BN = (dr["COLOR_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_NAME_BN"]);
                    ob.COLOR_EQUIV = (dr["COLOR_EQUIV"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_EQUIV"]);
                    ob.COLOR_SNAME = (dr["COLOR_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_SNAME"]);
                    ob.PANTON_NO = (dr["PANTON_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PANTON_NO"]);
                    ob.PNT_VERSION_NO = (dr["PNT_VERSION_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PNT_VERSION_NO"]);
                    ob.IS_SWATCH = (dr["IS_SWATCH"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_SWATCH"]);
                    ob.COL_IMAGE = (dr["COL_IMAGE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COL_IMAGE"]);
                    ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);

                    ob.LK_COL_TYPE_ID = (dr["LK_COL_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_COL_TYPE_ID"]);

                    if (dr["AOP_BASE_COL_ID"] != DBNull.Value)
                    {
                        ob.AOP_BASE_COL_ID = Convert.ToInt64(dr["AOP_BASE_COL_ID"]);
                    }
                    ob.AOP_PRNT_COL_LST = (dr["AOP_PRNT_COL_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["AOP_PRNT_COL_LST"]);
                    ob.YD_COL_LST = (dr["YD_COL_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["YD_COL_LST"]);


                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public object ColourMappDataByBuyerId(Int64 MC_STYLE_H_ID, String pLK_COL_TYPE_LIST = null, string pIS_DUMMY_COLOR = null)
        {
            string sp = "pkg_merchandising.mc_color_select";
            try
            {
                var obList = new List<MC_COLORModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pOption", Value =3004},
                     new CommandParameter() {ParameterName = "pMC_STYLE_H_ID", Value =MC_STYLE_H_ID},
                     new CommandParameter() {ParameterName = "pLK_COL_TYPE_LIST", Value =pLK_COL_TYPE_LIST},
                     new CommandParameter() {ParameterName = "pIS_DUMMY_COLOR", Value =pIS_DUMMY_COLOR},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        MC_COLORModel ob = new MC_COLORModel();

                        ob.MC_BU_COL_REF_ID = (dr["MC_BU_COL_REF_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BU_COL_REF_ID"]);
                        ob.MC_COLOR_ID = (dr["MC_COLOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_COLOR_ID"]);
                        ob.COLOR_REF = (dr["COLOR_REF"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_REF"]);
                        ob.OTHER_REF = (dr["OTHER_REF"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["OTHER_REF"]);
                        ob.IS_SWATCH = (dr["IS_SWATCH"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_SWATCH"]);
                        ob.IS_GMT_COL = (dr["IS_GMT_COL"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_GMT_COL"]);
                        ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);
                        ob.MC_STYLE_D_ITEM_LST = (dr["MC_STYLE_D_ITEM_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MC_STYLE_D_ITEM_LST"]);

                        ob.COLOR_NAME_EN = (dr["COLOR_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_NAME_EN"]);

                        ob.COL_TYPE_NAME = (dr["COL_TYPE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COL_TYPE_NAME"]);
                        ob.PANTON_NO = (dr["PANTON_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PANTON_NO"]);

                        ob.APRV_LD_REF = (dr["APRV_LD_REF"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["APRV_LD_REF"]);

                        ob.MC_BUYER_ID = (dr["MC_BUYER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BUYER_ID"]);
                        ob.LK_COL_TYPE_ID = (dr["LK_COL_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_COL_TYPE_ID"]);

                        if (dr["AOP_BASE_COL_ID"] != DBNull.Value)
                        {
                            ob.AOP_BASE_COL_ID = Convert.ToInt64(dr["AOP_BASE_COL_ID"]);
                        }

                        if (dr["RF_FIB_COMP_ID"] != DBNull.Value)
                        {
                            ob.RF_FIB_COMP_ID = Convert.ToInt64(dr["RF_FIB_COMP_ID"]);
                        }


                        if (dr["MC_COLOR_GRP_ID"] != DBNull.Value)
                        {
                            ob.MC_COLOR_GRP_ID = Convert.ToInt64(dr["MC_COLOR_GRP_ID"]);
                        }
                        ob.IS_LD_WIP = (dr["IS_LD_WIP"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_LD_WIP"]);
                        ob.AOP_PRNT_COL_LST = (dr["AOP_PRNT_COL_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["AOP_PRNT_COL_LST"]);
                        ob.YD_COL_LST = (dr["YD_COL_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["YD_COL_LST"]);

                        if (dr["LK_DYE_MTHD_ID"] != DBNull.Value)
                        {
                            ob.LK_DYE_MTHD_ID = Convert.ToInt64(dr["LK_DYE_MTHD_ID"]);
                        }



                        ob.IS_ADD_SD_COL = (dr["IS_ADD_SD_COL"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_ADD_SD_COL"]);

                        ob.OB_COLOUR_GROUP = new
                        {
                            MC_COLOR_GRP_ID = ob.MC_COLOR_GRP_ID,
                            COLOR_GRP_NAME_EN = (dr["COLOR_GRP_NAME_EN"] == DBNull.Value) ? "--Select--" : Convert.ToString(dr["COLOR_GRP_NAME_EN"])
                        };

                        ob.OB_DYE_METHOD = new
                        {
                            LK_DYE_MTHD_ID = ob.LK_DYE_MTHD_ID,
                            DYE_MTHD_NAME = (dr["DYE_MTHD_NAME"] == DBNull.Value) ? "--Select-" : Convert.ToString(dr["DYE_MTHD_NAME"])
                        };

                        ob.OB_FIBER_COMP = new
                        {
                            RF_FIB_COMP_ID = ob.RF_FIB_COMP_ID,
                            FIB_COMP_NAME = (dr["FIB_COMP_NAME"] == DBNull.Value) ? "--Select--" : Convert.ToString(dr["FIB_COMP_NAME"])
                        };

                        ob.OB_COLOR = new
                        {
                            MC_COLOR_ID = ob.MC_COLOR_ID,
                            COLOR_NAME_EN = (dr["COLOR_NAME_EN"] == DBNull.Value) ? "--N/A--" : Convert.ToString(dr["COLOR_NAME_EN"])
                        };

                        ob.IS_USED = (dr["IS_USED"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_USED"]);


                        obList.Add(ob);
                    }
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<MC_COLORModel> OrderOrByerAccWiseColorList(Int64? pMC_ORDER_H_ID, Int64? pMC_BYR_ACC_ID, Int64? pGMT_MRKR_ID = null)
        {
            string sp = "pkg_merchandising.mc_color_select";
            try
            {
                var obList = new List<MC_COLORModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_ORDER_H_ID", Value = pMC_ORDER_H_ID},
                     new CommandParameter() {ParameterName = "pMC_BYR_ACC_ID", Value = pMC_BYR_ACC_ID},
                     new CommandParameter() {ParameterName = "pGMT_MRKR_ID", Value = pGMT_MRKR_ID},
                     new CommandParameter() {ParameterName = "pOption", Value = 3005},
                     new CommandParameter() {ParameterName = "pMsg", Value = 500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_COLORModel ob = new MC_COLORModel();
                    ob.MC_COLOR_ID = (dr["MC_COLOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_COLOR_ID"]);
                    ob.COLOR_CODE = (dr["COLOR_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_CODE"]);
                    ob.COLOR_NAME_EN = (dr["COLOR_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_NAME_EN"]);
                    //ob.COLOR_NAME_BN = (dr["COLOR_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_NAME_BN"]);
                    //ob.COLOR_EQUIV = (dr["COLOR_EQUIV"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_EQUIV"]);
                    //ob.COLOR_SNAME = (dr["COLOR_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_SNAME"]);
                    //ob.PANTON_NO = (dr["PANTON_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PANTON_NO"]);
                    //ob.PNT_VERSION_NO = (dr["PNT_VERSION_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PNT_VERSION_NO"]);
                    //ob.IS_SWATCH = (dr["IS_SWATCH"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_SWATCH"]);
                    //ob.COL_IMAGE = (dr["COL_IMAGE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COL_IMAGE"]);
                    //ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);

                    if (dr["MC_BYR_ACC_ID"] != DBNull.Value)
                        ob.MC_BYR_ACC_ID = (dr["MC_BYR_ACC_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BYR_ACC_ID"]);

                    if (dr["MC_ORDER_H_ID"] != DBNull.Value)
                        ob.MC_ORDER_H_ID = (dr["MC_ORDER_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_ORDER_H_ID"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object MultiOrderWiseColorList(string pMC_ORDER_H_IDS)
        {
            string sp = "pkg_merchandising.mc_color_select";
            try
            {
                var obList = new List<MC_COLORModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_ORDER_H_IDS", Value = pMC_ORDER_H_IDS},                     
                     new CommandParameter() {ParameterName = "pOption", Value = 3006},
                     new CommandParameter() {ParameterName = "pMsg", Value = 500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_COLORModel ob = new MC_COLORModel();
                    ob.MC_COLOR_ID = (dr["MC_COLOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_COLOR_ID"]);
                    ob.COLOR_CODE = (dr["COLOR_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_CODE"]);
                    ob.COLOR_NAME_EN = (dr["COLOR_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_NAME_EN"]);
                    //ob.COLOR_NAME_BN = (dr["COLOR_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_NAME_BN"]);
                    //ob.COLOR_EQUIV = (dr["COLOR_EQUIV"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_EQUIV"]);
                    //ob.COLOR_SNAME = (dr["COLOR_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_SNAME"]);
                    //ob.PANTON_NO = (dr["PANTON_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PANTON_NO"]);
                    //ob.PNT_VERSION_NO = (dr["PNT_VERSION_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PNT_VERSION_NO"]);
                    //ob.IS_SWATCH = (dr["IS_SWATCH"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_SWATCH"]);
                    //ob.COL_IMAGE = (dr["COL_IMAGE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COL_IMAGE"]);
                    //ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);

                    //if (dr["MC_BYR_ACC_ID"] != DBNull.Value)
                    //    ob.MC_BYR_ACC_ID = (dr["MC_BYR_ACC_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BYR_ACC_ID"]);

                    //if (dr["MC_ORDER_H_ID"] != DBNull.Value)
                    //    ob.MC_ORDER_H_ID = (dr["MC_ORDER_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_ORDER_H_ID"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object OrderItemWiseColorList(Int64? pMC_ORDER_H_ID=null, Int64? pMC_STYLE_D_ITEM_ID=null, string pIS_DUMMY_COLOR = null, Int64? pMC_STYLE_H_ID=null)
        {
            string sp = "pkg_merchandising.mc_color_select";
            try
            {
                var obList = new List<MC_COLORModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_ORDER_H_ID", Value = pMC_ORDER_H_ID},                 
                     new CommandParameter() {ParameterName = "pMC_STYLE_H_ID", Value = pMC_STYLE_H_ID}, 
                     new CommandParameter() {ParameterName = "pMC_STYLE_D_ITEM_ID", Value = pMC_STYLE_D_ITEM_ID},
                     new CommandParameter() {ParameterName = "pIS_DUMMY_COLOR", Value = pIS_DUMMY_COLOR},
                     new CommandParameter() {ParameterName = "pOption", Value = 3007},
                     new CommandParameter() {ParameterName = "pMsg", Value = 500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_COLORModel ob = new MC_COLORModel();
                    ob.MC_COLOR_ID = (dr["MC_COLOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_COLOR_ID"]);
                    ob.COLOR_CODE = (dr["COLOR_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_CODE"]);
                    ob.COLOR_NAME_EN = (dr["COLOR_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_NAME_EN"]);
                    ob.IS_DUMMY_COLOR = (dr["IS_DUMMY_COLOR"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_DUMMY_COLOR"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public object GetAnyAvailColorList()
        {
            string sp = "pkg_merchandising.mc_color_select";
            try
            {
                var obList = new List<MC_COLORModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {                    
                     new CommandParameter() {ParameterName = "pOption", Value = 3008},
                     new CommandParameter() {ParameterName = "pMsg", Value = 500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_COLORModel ob = new MC_COLORModel();
                    ob.MC_COLOR_ID = (dr["MC_COLOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_COLOR_ID"]);
                    ob.COLOR_CODE = (dr["COLOR_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_CODE"]);
                    ob.COLOR_NAME_EN = (dr["COLOR_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_NAME_EN"]);
                    ob.IS_DUMMY_COLOR = (dr["IS_DUMMY_COLOR"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_DUMMY_COLOR"]);
                    ob.IS_ANY_COLOR = (dr["IS_ANY_COLOR"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ANY_COLOR"]);
                    ob.IS_AVAILABLE_COLOR = (dr["IS_AVAILABLE_COLOR"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_AVAILABLE_COLOR"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object GetColorListByProdOrdID(Int64 pMC_FAB_PROD_ORD_H_ID)
        {
            string sp = "pkg_merchandising.mc_color_select";
            try
            {
                var obList = new List<MC_COLORModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_FAB_PROD_ORD_H_ID", Value = pMC_FAB_PROD_ORD_H_ID},                     
                     new CommandParameter() {ParameterName = "pOption", Value = 3011},
                     new CommandParameter() {ParameterName = "pMsg", Value = 500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_COLORModel ob = new MC_COLORModel();
                    ob.MC_COLOR_ID = (dr["MC_COLOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_COLOR_ID"]);
                    ob.COLOR_CODE = (dr["COLOR_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_CODE"]);
                    ob.COLOR_NAME_EN = (dr["COLOR_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_NAME_EN"]);
                    //ob.COLOR_NAME_BN = (dr["COLOR_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_NAME_BN"]);
                    //ob.COLOR_EQUIV = (dr["COLOR_EQUIV"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_EQUIV"]);
                    //ob.COLOR_SNAME = (dr["COLOR_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_SNAME"]);
                    //ob.PANTON_NO = (dr["PANTON_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PANTON_NO"]);
                    //ob.PNT_VERSION_NO = (dr["PNT_VERSION_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PNT_VERSION_NO"]);
                    //ob.IS_SWATCH = (dr["IS_SWATCH"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_SWATCH"]);
                    //ob.COL_IMAGE = (dr["COL_IMAGE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COL_IMAGE"]);
                    //ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);

                    //if (dr["MC_BYR_ACC_ID"] != DBNull.Value)
                    //    ob.MC_BYR_ACC_ID = (dr["MC_BYR_ACC_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BYR_ACC_ID"]);

                    //if (dr["MC_ORDER_H_ID"] != DBNull.Value)
                    //    ob.MC_ORDER_H_ID = (dr["MC_ORDER_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_ORDER_H_ID"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object getColorListByDyeBatch(string pMC_FAB_PROD_ORD_H_LST, Int64 pINV_ITEM_CAT_ID)
        {
            string sp = "pkg_dye_batch_plan.mc_item_dy_prod_select";
            try
            {
                var obList = new List<MC_COLORModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                    
                     new CommandParameter() {ParameterName = "pMC_FAB_PROD_ORD_H_LST", Value =pMC_FAB_PROD_ORD_H_LST},
                     new CommandParameter() {ParameterName = "pINV_ITEM_CAT_ID", Value =pINV_ITEM_CAT_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3002},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_COLORModel ob = new MC_COLORModel();
                    ob.COLOR_NAME_EN = (dr["COLOR_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_NAME_EN"]);
                    ob.MC_COLOR_ID = (dr["MC_COLOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_COLOR_ID"]);
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public object GetColorListAfterDyeByID(Int64? pMC_FAB_PROD_ORD_H_ID = null, Int64? pMC_BYR_ACC_GRP_ID = null, Int64? pMC_STYLE_H_EXT_ID = null)
        {
            string sp = "pkg_merchandising.mc_color_select";
            try
            {
                var obList = new List<MC_COLORModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_FAB_PROD_ORD_H_ID", Value = pMC_FAB_PROD_ORD_H_ID},                     
                     new CommandParameter() {ParameterName = "pMC_STYLE_H_EXT_ID", Value = pMC_STYLE_H_EXT_ID},                     
                     new CommandParameter() {ParameterName = "pMC_BYR_ACC_GRP_ID", Value = pMC_BYR_ACC_GRP_ID},                     
                     new CommandParameter() {ParameterName = "pOption", Value = 3012},
                     new CommandParameter() {ParameterName = "pMsg", Value = 500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_COLORModel ob = new MC_COLORModel();
                    ob.MC_COLOR_ID = (dr["MC_COLOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_COLOR_ID"]);
                    ob.COLOR_CODE = (dr["COLOR_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_CODE"]);
                    ob.COLOR_NAME_EN = (dr["COLOR_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_NAME_EN"]);
                    ob.COLOR_NAME_BN = (dr["COLOR_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_NAME_BN"]);
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<MC_COLORModel> StyleExtOrByerAccWiseColorList(Int64? pMC_BYR_ACC_ID, Int64? pMC_STYLE_H_EXT_ID = null)
        {
            string sp = "pkg_merchandising.mc_color_select";
            try
            {
                var obList = new List<MC_COLORModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_STYLE_H_EXT_ID", Value = pMC_STYLE_H_EXT_ID},
                     new CommandParameter() {ParameterName = "pMC_BYR_ACC_ID", Value = pMC_BYR_ACC_ID},
                     new CommandParameter() {ParameterName = "pOption", Value = 3013},
                     new CommandParameter() {ParameterName = "pMsg", Value = 500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_COLORModel ob = new MC_COLORModel();
                    ob.MC_COLOR_ID = (dr["MC_COLOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_COLOR_ID"]);
                    ob.COLOR_CODE = (dr["COLOR_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_CODE"]);
                    ob.COLOR_NAME_EN = (dr["COLOR_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_NAME_EN"]);
                    if (dr["MC_BYR_ACC_ID"] != DBNull.Value)
                        ob.MC_BYR_ACC_ID = (dr["MC_BYR_ACC_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BYR_ACC_ID"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string IS_USED { get; set; }
    }
}