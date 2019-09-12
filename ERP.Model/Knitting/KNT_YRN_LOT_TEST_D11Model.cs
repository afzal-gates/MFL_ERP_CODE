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
    public class KNT_YRN_LOT_TEST_D11Model
    {
        public Int64 KNT_YRN_LOT_TEST_D11_ID { get; set; }
        public Int64 KNT_YRN_LOT_TEST_D1_ID { get; set; }
        public Int64 LK_COLR_GRP_ID { get; set; }
        public Int64 MC_COLOR_ID { get; set; }
        public Int64 LK_YRN_TST_STS_ID { get; set; }
        public string TEST_NOTE { get; set; }
        public string STATUS_NOTE { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }

        public string Save()
        {
            const string sp = "SP_KNT_YRN_LOT_TEST_D11";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_YRN_LOT_TEST_D11_ID", Value = ob.KNT_YRN_LOT_TEST_D11_ID},
                     new CommandParameter() {ParameterName = "pKNT_YRN_LOT_TEST_D1_ID", Value = ob.KNT_YRN_LOT_TEST_D1_ID},
                     new CommandParameter() {ParameterName = "pLK_COLR_GRP_ID", Value = ob.LK_COLR_GRP_ID},
                     new CommandParameter() {ParameterName = "pMC_COLOR_ID", Value = ob.MC_COLOR_ID},
                     new CommandParameter() {ParameterName = "pLK_YRN_TST_STS_ID", Value = ob.LK_YRN_TST_STS_ID},
                     new CommandParameter() {ParameterName = "pTEST_NOTE", Value = ob.TEST_NOTE},
                     new CommandParameter() {ParameterName = "pSTATUS_NOTE", Value = ob.STATUS_NOTE},
                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = ob.CREATED_BY},
                     new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = ob.LAST_UPDATED_BY},
                     new CommandParameter() {ParameterName = "pOption", Value =1000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);

                foreach (DataRow dr in ds.Tables["OUTPARAM"].Rows)
                {
                    jsonStr += dr["KEY"].ToString() + ":" + dr["VALUE"].ToString() + ",";
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
            const string sp = "SP_KNT_YRN_LOT_TEST_D11";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_YRN_LOT_TEST_D11_ID", Value = ob.KNT_YRN_LOT_TEST_D11_ID},
                     new CommandParameter() {ParameterName = "pKNT_YRN_LOT_TEST_D1_ID", Value = ob.KNT_YRN_LOT_TEST_D1_ID},
                     new CommandParameter() {ParameterName = "pLK_COLR_GRP_ID", Value = ob.LK_COLR_GRP_ID},
                     new CommandParameter() {ParameterName = "pMC_COLOR_ID", Value = ob.MC_COLOR_ID},
                     new CommandParameter() {ParameterName = "pLK_YRN_TST_STS_ID", Value = ob.LK_YRN_TST_STS_ID},
                     new CommandParameter() {ParameterName = "pTEST_NOTE", Value = ob.TEST_NOTE},
                     new CommandParameter() {ParameterName = "pSTATUS_NOTE", Value = ob.STATUS_NOTE},
                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = ob.CREATED_BY},
                     new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = ob.LAST_UPDATED_BY},
                     new CommandParameter() {ParameterName = "pOption", Value =2000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);

                foreach (DataRow dr in ds.Tables["OUTPARAM"].Rows)
                {
                    jsonStr += dr["KEY"].ToString() + ":" + dr["VALUE"].ToString() + ",";
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
            const string sp = "SP_KNT_YRN_LOT_TEST_D11";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_YRN_LOT_TEST_D11_ID", Value = ob.KNT_YRN_LOT_TEST_D11_ID},
                     new CommandParameter() {ParameterName = "pKNT_YRN_LOT_TEST_D1_ID", Value = ob.KNT_YRN_LOT_TEST_D1_ID},
                     new CommandParameter() {ParameterName = "pLK_COLR_GRP_ID", Value = ob.LK_COLR_GRP_ID},
                     new CommandParameter() {ParameterName = "pMC_COLOR_ID", Value = ob.MC_COLOR_ID},
                     new CommandParameter() {ParameterName = "pLK_YRN_TST_STS_ID", Value = ob.LK_YRN_TST_STS_ID},
                     new CommandParameter() {ParameterName = "pTEST_NOTE", Value = ob.TEST_NOTE},
                     new CommandParameter() {ParameterName = "pSTATUS_NOTE", Value = ob.STATUS_NOTE},
                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = ob.CREATED_BY},
                     new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = ob.LAST_UPDATED_BY},
                     new CommandParameter() {ParameterName = "pOption", Value =4000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);

                foreach (DataRow dr in ds.Tables["OUTPARAM"].Rows)
                {
                    jsonStr += dr["KEY"].ToString() + ":" + dr["VALUE"].ToString() + ",";
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

        public List<KNT_YRN_LOT_TEST_D11Model> SelectAll()
        {
            string sp = "pkg_knt_yarn_test.knt_yrn_lot_test_d11_select";
            try
            {
                var obList = new List<KNT_YRN_LOT_TEST_D11Model>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_YRN_LOT_TEST_D1_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    KNT_YRN_LOT_TEST_D11Model ob = new KNT_YRN_LOT_TEST_D11Model();
                    ob.KNT_YRN_LOT_TEST_D11_ID = (dr["KNT_YRN_LOT_TEST_D11_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_YRN_LOT_TEST_D11_ID"]);
                    ob.KNT_YRN_LOT_TEST_D1_ID = (dr["KNT_YRN_LOT_TEST_D1_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_YRN_LOT_TEST_D1_ID"]);
                    ob.LK_COLR_GRP_ID = (dr["LK_COLR_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_COLR_GRP_ID"]);
                    ob.MC_COLOR_ID = (dr["MC_COLOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_COLOR_ID"]);
                    ob.LK_YRN_TST_STS_ID = (dr["LK_YRN_TST_STS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_YRN_TST_STS_ID"]);
                    ob.TEST_NOTE = (dr["TEST_NOTE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TEST_NOTE"]);
                    ob.STATUS_NOTE = (dr["STATUS_NOTE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STATUS_NOTE"]);
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

        public List<KNT_YRN_LOT_TEST_D11Model> SelectByID(Int64? pKNT_YRN_LOT_TEST_D1_ID = null)
        {
            string sp = "pkg_knt_yarn_test.knt_yrn_lot_test_d11_select";
            try
            {
                var obList = new List<KNT_YRN_LOT_TEST_D11Model>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_YRN_LOT_TEST_D1_ID", Value =pKNT_YRN_LOT_TEST_D1_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    KNT_YRN_LOT_TEST_D11Model ob = new KNT_YRN_LOT_TEST_D11Model();
                    ob.KNT_YRN_LOT_TEST_D11_ID = (dr["KNT_YRN_LOT_TEST_D11_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_YRN_LOT_TEST_D11_ID"]);
                    ob.KNT_YRN_LOT_TEST_D1_ID = (dr["KNT_YRN_LOT_TEST_D1_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_YRN_LOT_TEST_D1_ID"]);
                    ob.LK_COLR_GRP_ID = (dr["LK_COLR_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_COLR_GRP_ID"]);
                    ob.MC_COLOR_ID = (dr["MC_COLOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_COLOR_ID"]);
                    ob.LK_YRN_TST_STS_ID = (dr["LK_YRN_TST_STS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_YRN_TST_STS_ID"]);
                    ob.TEST_NOTE = (dr["TEST_NOTE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TEST_NOTE"]);
                    ob.STATUS_NOTE = (dr["STATUS_NOTE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STATUS_NOTE"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);

                    ob.COLOR_NAME_EN = (dr["COLOR_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_NAME_EN"]);
                    ob.COLOR_GRP_NAME = (dr["COLOR_GRP_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_GRP_NAME"]);
                    
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<KNT_YRN_LOT_TEST_D11Model> SelectForDefectType(Int64? pKNT_YRN_LOT_TEST_D1_ID = null)
        {
            string sp = "pkg_knt_yarn_test.knt_yrn_lot_test_d11_select";
            try
            {
                var obList = new List<KNT_YRN_LOT_TEST_D11Model>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_YRN_LOT_TEST_D1_ID", Value =pKNT_YRN_LOT_TEST_D1_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3002},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    KNT_YRN_LOT_TEST_D11Model ob = new KNT_YRN_LOT_TEST_D11Model();
                    ob.KNT_YRN_LOT_TEST_D11_ID = (dr["KNT_YRN_LOT_TEST_D11_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_YRN_LOT_TEST_D11_ID"]);
                    ob.KNT_YRN_LOT_TEST_D1_ID = (dr["KNT_YRN_LOT_TEST_D1_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_YRN_LOT_TEST_D1_ID"]);
                    ob.RF_DY_DFCT_TYPE_ID = (dr["RF_DY_DFCT_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_DY_DFCT_TYPE_ID"]);
                    ob.DY_DFCT_TYPE_NAME = (dr["DY_DFCT_TYPE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DY_DFCT_TYPE_NAME"]);
                    ob.MC_COLOR_ID = (dr["MC_COLOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_COLOR_ID"]);
                    ob.COLOR_NAME_EN = (dr["COLOR_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_NAME_EN"]);
                    ob.KNT_YRN_LOT_TEST_R2_ID = (dr["KNT_YRN_LOT_TEST_R2_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_YRN_LOT_TEST_R2_ID"]);
                    ob.COMMENTS = (dr["COMMENTS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMMENTS"]);                    

                    obList.Add(ob);

                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public KNT_YRN_LOT_TEST_D11Model Select(long ID)
        {
            string sp = "Select_KNT_YRN_LOT_TEST_D11";
            try
            {
                var ob = new KNT_YRN_LOT_TEST_D11Model();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_YRN_LOT_TEST_D11_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.KNT_YRN_LOT_TEST_D11_ID = (dr["KNT_YRN_LOT_TEST_D11_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_YRN_LOT_TEST_D11_ID"]);
                    ob.KNT_YRN_LOT_TEST_D1_ID = (dr["KNT_YRN_LOT_TEST_D1_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_YRN_LOT_TEST_D1_ID"]);
                    ob.LK_COLR_GRP_ID = (dr["LK_COLR_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_COLR_GRP_ID"]);
                    ob.MC_COLOR_ID = (dr["MC_COLOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_COLOR_ID"]);
                    ob.LK_YRN_TST_STS_ID = (dr["LK_YRN_TST_STS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_YRN_TST_STS_ID"]);
                    ob.TEST_NOTE = (dr["TEST_NOTE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TEST_NOTE"]);
                    ob.STATUS_NOTE = (dr["STATUS_NOTE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STATUS_NOTE"]);
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


        public List<KNT_YRN_LOT_TEST_D11Model> KnitYarnTestRegister(Int64? pRF_YRN_CNT_ID = null, Int64? pRF_BRAND_ID = null, string pYRN_LOT_NO = null, Int64? pLK_YRN_TST_TYPE_ID = null)
        {
            string sp = "pkg_knt_yarn_test.knt_yrn_lot_test_register";
            try
            {
                var obList = new List<KNT_YRN_LOT_TEST_D11Model>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pRF_YRN_CNT_ID", Value =pRF_YRN_CNT_ID},
                     new CommandParameter() {ParameterName = "pRF_BRAND_ID", Value =pRF_BRAND_ID},
                     new CommandParameter() {ParameterName = "pYRN_LOT_NO", Value =pYRN_LOT_NO},
                     new CommandParameter() {ParameterName = "pLK_YRN_TST_TYPE_ID", Value =pLK_YRN_TST_TYPE_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    KNT_YRN_LOT_TEST_D11Model ob = new KNT_YRN_LOT_TEST_D11Model();
                    ob.SUP_TRD_NAME_EN = (dr["SUP_TRD_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SUP_TRD_NAME_EN"]);
                    ob.ITEM_NAME_EN = (dr["ITEM_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_NAME_EN"]);
                    ob.YRN_LOT_NO = (dr["YRN_LOT_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["YRN_LOT_NO"]);
                    ob.BRAND_NAME_EN = (dr["BRAND_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BRAND_NAME_EN"]);
                    ob.IMP_LC_NO = (dr["IMP_LC_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IMP_LC_NO"]);
                    ob.COLOR_NAME_EN = (dr["COLOR_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_NAME_EN"]);
                    ob.TEST_RESULT = (dr["TEST_RESULT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TEST_RESULT"]);
                    ob.TEST_NOTE = (dr["TEST_NOTE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TEST_NOTE"]);
                    ob.TEST_DT = (dr["TEST_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["TEST_DT"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string COLOR_NAME_EN { get; set; }

        public long RF_DY_DFCT_TYPE_ID { get; set; }

        public string DY_DFCT_TYPE_NAME { get; set; }

        public string COLOR_GRP_NAME { get; set; }

        public long KNT_YRN_LOT_TEST_R2_ID { get; set; }

        public string COMMENTS { get; set; }


        public string SUP_TRD_NAME_EN { get; set; }

        public string ITEM_NAME_EN { get; set; }

        public string YRN_LOT_NO { get; set; }

        public string BRAND_NAME_EN { get; set; }

        public string IMP_LC_NO { get; set; }

        public string TEST_RESULT { get; set; }

        public DateTime TEST_DT { get; set; }
    }
}