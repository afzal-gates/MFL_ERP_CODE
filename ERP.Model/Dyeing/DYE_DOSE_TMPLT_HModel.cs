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
    public class DYE_DOSE_TMPLT_HModel
    {
        public Int64 DYE_DOSE_TMPLT_H_ID { get; set; }
        public string DOSE_TMPLT_CODE { get; set; }
        public string DOSE_TMPLT_NAME_EN { get; set; }
        public string DOSE_TMPLT_NAME_BN { get; set; }
        public string DOSE_TMPLT_SNAME { get; set; }
        public Int64 LK_DYE_MTHD_ID { get; set; }
        public string MC_COLOR_GRP_LST { get; set; }
        public string IS_FINALIZED { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }
        public Int64 LK_DC_TMPLT_TYPE_ID { get; set; }
        
        public string XML_DOS_D { get; set; }

        public string Save()
        {
            const string sp = "pkg_mc_ld_recipe.dye_dose_tmplt_h_insert";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_DOSE_TMPLT_H_ID", Value = ob.DYE_DOSE_TMPLT_H_ID},
                     new CommandParameter() {ParameterName = "pDOSE_TMPLT_CODE", Value = ob.DOSE_TMPLT_CODE},
                     new CommandParameter() {ParameterName = "pDOSE_TMPLT_NAME_EN", Value = ob.DOSE_TMPLT_NAME_EN},
                     new CommandParameter() {ParameterName = "pDOSE_TMPLT_NAME_BN", Value = ob.DOSE_TMPLT_NAME_BN},
                     new CommandParameter() {ParameterName = "pDOSE_TMPLT_SNAME", Value = ob.DOSE_TMPLT_SNAME},
                     new CommandParameter() {ParameterName = "pLK_DYE_MTHD_ID", Value = ob.LK_DYE_MTHD_ID},
                     new CommandParameter() {ParameterName = "pMC_COLOR_GRP_LST", Value = ob.MC_COLOR_GRP_LST},
                     new CommandParameter() {ParameterName = "pIS_FINALIZED", Value = ob.IS_FINALIZED},
                     new CommandParameter() {ParameterName = "pLK_DC_TMPLT_TYPE_ID", Value = ob.LK_DC_TMPLT_TYPE_ID},
                     new CommandParameter() {ParameterName = "pXML_DOS_D", Value = ob.XML_DOS_D},
                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
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
            const string sp = "SP_DYE_DOSE_TMPLT_H";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_DOSE_TMPLT_H_ID", Value = ob.DYE_DOSE_TMPLT_H_ID},
                     new CommandParameter() {ParameterName = "pDOSE_TMPLT_CODE", Value = ob.DOSE_TMPLT_CODE},
                     new CommandParameter() {ParameterName = "pDOSE_TMPLT_NAME_EN", Value = ob.DOSE_TMPLT_NAME_EN},
                     new CommandParameter() {ParameterName = "pDOSE_TMPLT_NAME_BN", Value = ob.DOSE_TMPLT_NAME_BN},
                     new CommandParameter() {ParameterName = "pDOSE_TMPLT_SNAME", Value = ob.DOSE_TMPLT_SNAME},
                     new CommandParameter() {ParameterName = "pLK_DYE_MTHD_ID", Value = ob.LK_DYE_MTHD_ID},
                     new CommandParameter() {ParameterName = "pMC_COLOR_GRP_LST", Value = ob.MC_COLOR_GRP_LST},
                     new CommandParameter() {ParameterName = "pIS_FINALIZED", Value = ob.IS_FINALIZED},
                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = ob.CREATED_BY},
                     new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = ob.LAST_UPDATED_BY},
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
            const string sp = "SP_DYE_DOSE_TMPLT_H";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_DOSE_TMPLT_H_ID", Value = ob.DYE_DOSE_TMPLT_H_ID},
                     new CommandParameter() {ParameterName = "pDOSE_TMPLT_CODE", Value = ob.DOSE_TMPLT_CODE},
                     new CommandParameter() {ParameterName = "pDOSE_TMPLT_NAME_EN", Value = ob.DOSE_TMPLT_NAME_EN},
                     new CommandParameter() {ParameterName = "pDOSE_TMPLT_NAME_BN", Value = ob.DOSE_TMPLT_NAME_BN},
                     new CommandParameter() {ParameterName = "pDOSE_TMPLT_SNAME", Value = ob.DOSE_TMPLT_SNAME},
                     new CommandParameter() {ParameterName = "pLK_DYE_MTHD_ID", Value = ob.LK_DYE_MTHD_ID},
                     new CommandParameter() {ParameterName = "pMC_COLOR_GRP_LST", Value = ob.MC_COLOR_GRP_LST},
                     new CommandParameter() {ParameterName = "pIS_FINALIZED", Value = ob.IS_FINALIZED},
                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = ob.CREATED_BY},
                     new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = ob.LAST_UPDATED_BY},
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

        public List<DYE_DOSE_TMPLT_HModel> SelectAll()
        {
            string sp = "pkg_mc_ld_recipe.dye_dose_tmplt_h_select";
            try
            {
                var obList = new List<DYE_DOSE_TMPLT_HModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_DOSE_TMPLT_H_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DYE_DOSE_TMPLT_HModel ob = new DYE_DOSE_TMPLT_HModel();
                    ob.DYE_DOSE_TMPLT_H_ID = (dr["DYE_DOSE_TMPLT_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_DOSE_TMPLT_H_ID"]);
                    ob.DOSE_TMPLT_CODE = (dr["DOSE_TMPLT_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DOSE_TMPLT_CODE"]);
                    ob.DOSE_TMPLT_NAME_EN = (dr["DOSE_TMPLT_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DOSE_TMPLT_NAME_EN"]);
                    ob.DOSE_TMPLT_NAME_BN = (dr["DOSE_TMPLT_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DOSE_TMPLT_NAME_BN"]);
                    ob.DOSE_TMPLT_SNAME = (dr["DOSE_TMPLT_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DOSE_TMPLT_SNAME"]);
                    ob.LK_DYE_MTHD_ID = (dr["LK_DYE_MTHD_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_DYE_MTHD_ID"]);
                    ob.LK_DC_TMPLT_TYPE_ID = (dr["LK_DC_TMPLT_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_DC_TMPLT_TYPE_ID"]);
                    ob.MC_COLOR_GRP_LST = (dr["MC_COLOR_GRP_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MC_COLOR_GRP_LST"]);
                    ob.IS_FINALIZED = (dr["IS_FINALIZED"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_FINALIZED"]);
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

        public DYE_DOSE_TMPLT_HModel Select(Int64? pDYE_DOSE_TMPLT_H_ID = null)
        {
            string sp = "pkg_mc_ld_recipe.dye_dose_tmplt_h_select";
            try
            {
                var ob = new DYE_DOSE_TMPLT_HModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_DOSE_TMPLT_H_ID", Value =pDYE_DOSE_TMPLT_H_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.DYE_DOSE_TMPLT_H_ID = (dr["DYE_DOSE_TMPLT_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_DOSE_TMPLT_H_ID"]);
                    ob.DOSE_TMPLT_CODE = (dr["DOSE_TMPLT_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DOSE_TMPLT_CODE"]);
                    ob.DOSE_TMPLT_NAME_EN = (dr["DOSE_TMPLT_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DOSE_TMPLT_NAME_EN"]);
                    ob.DOSE_TMPLT_NAME_BN = (dr["DOSE_TMPLT_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DOSE_TMPLT_NAME_BN"]);
                    ob.DOSE_TMPLT_SNAME = (dr["DOSE_TMPLT_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DOSE_TMPLT_SNAME"]);
                    ob.LK_DYE_MTHD_ID = (dr["LK_DYE_MTHD_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_DYE_MTHD_ID"]);
                    ob.LK_DC_TMPLT_TYPE_ID = (dr["LK_DC_TMPLT_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_DC_TMPLT_TYPE_ID"]);
                    ob.MC_COLOR_GRP_LST = (dr["MC_COLOR_GRP_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MC_COLOR_GRP_LST"]);
                    ob.IS_FINALIZED = (dr["IS_FINALIZED"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_FINALIZED"]);
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


        public List<DYE_DOSE_TMPLT_HModel> SelectByColorGrpID(Int64? pMC_COLOR_GRP_ID = null, Int64? pLK_DYE_MTHD_ID=null)
        {
            string sp = "pkg_mc_ld_recipe.dye_dose_tmplt_h_select";
            try
            {
                var list = new List<DYE_DOSE_TMPLT_HModel>();
                
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_COLOR_GRP_ID", Value =pMC_COLOR_GRP_ID},
                     new CommandParameter() {ParameterName = "pLK_DYE_MTHD_ID", Value =pLK_DYE_MTHD_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3002},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    var ob = new DYE_DOSE_TMPLT_HModel();
                    ob.DYE_DOSE_TMPLT_H_ID = (dr["DYE_DOSE_TMPLT_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_DOSE_TMPLT_H_ID"]);
                    ob.DOSE_TMPLT_CODE = (dr["DOSE_TMPLT_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DOSE_TMPLT_CODE"]);
                    ob.DOSE_TMPLT_NAME_EN = (dr["DOSE_TMPLT_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DOSE_TMPLT_NAME_EN"]);
                    ob.DOSE_TMPLT_NAME_BN = (dr["DOSE_TMPLT_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DOSE_TMPLT_NAME_BN"]);
                    ob.DOSE_TMPLT_SNAME = (dr["DOSE_TMPLT_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DOSE_TMPLT_SNAME"]);
                    ob.LK_DYE_MTHD_ID = (dr["LK_DYE_MTHD_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_DYE_MTHD_ID"]);
                    ob.LK_DC_TMPLT_TYPE_ID = (dr["LK_DC_TMPLT_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_DC_TMPLT_TYPE_ID"]);
                    ob.MC_COLOR_GRP_LST = (dr["MC_COLOR_GRP_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MC_COLOR_GRP_LST"]);
                    ob.IS_FINALIZED = (dr["IS_FINALIZED"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_FINALIZED"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);

                    if (dr["MC_COLOR_GRP_ID"] == DBNull.Value)
                        ob.MC_COLOR_GRP_ID = (dr["MC_COLOR_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_COLOR_GRP_ID"]);

                    list.Add(ob);
                }
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public long MC_COLOR_GRP_ID { get; set; }
    }
}