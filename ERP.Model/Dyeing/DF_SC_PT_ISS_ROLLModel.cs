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
    public class DF_SC_PT_ISS_ROLLModel
    {
        public Int64 DF_SC_PT_ISS_ROLL_ID { get; set; }
        public Int64 DF_SC_PT_ISS_D1_ID { get; set; }
        public Int64 KNT_FAB_ROLL_ID { get; set; }
        public string IS_LNK_CHLN { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }



        public string TYPE { get; set; }

        public string Save()
        {
            const string sp = "PKG_DF_PT.DF_SC_PT_ISS_ROLL_INSERT";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_SC_PT_ISS_ROLL_ID", Value = ob.DF_SC_PT_ISS_ROLL_ID},
                     new CommandParameter() {ParameterName = "pDF_SC_PT_ISS_D1_ID", Value = ob.DF_SC_PT_ISS_D1_ID},
                     new CommandParameter() {ParameterName = "pKNT_FAB_ROLL_ID", Value = ob.KNT_FAB_ROLL_ID},
                     new CommandParameter() {ParameterName = "pTYPE", Value = ob.TYPE},
                     new CommandParameter() {ParameterName = "pIS_LNK_CHLN", Value = ob.IS_LNK_CHLN},                     
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pOption", Value = (ob.TYPE =="insert"||ob.TYPE =="delete") ? 1000 : 1002},

                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
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
            const string sp = "SP_DF_SC_PT_ISS_ROLL";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_SC_PT_ISS_ROLL_ID", Value = ob.DF_SC_PT_ISS_ROLL_ID},
                     new CommandParameter() {ParameterName = "pDF_SC_PT_ISS_D1_ID", Value = ob.DF_SC_PT_ISS_D1_ID},
                     new CommandParameter() {ParameterName = "pKNT_FAB_ROLL_ID", Value = ob.KNT_FAB_ROLL_ID},
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
            const string sp = "SP_DF_SC_PT_ISS_ROLL";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_SC_PT_ISS_ROLL_ID", Value = ob.DF_SC_PT_ISS_ROLL_ID},
                     new CommandParameter() {ParameterName = "pDF_SC_PT_ISS_D1_ID", Value = ob.DF_SC_PT_ISS_D1_ID},
                     new CommandParameter() {ParameterName = "pKNT_FAB_ROLL_ID", Value = ob.KNT_FAB_ROLL_ID},
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

        public List<DF_SC_PT_ISS_ROLLModel> SelectAll()
        {
            string sp = "Select_DF_SC_PT_ISS_ROLL";
            try
            {
                var obList = new List<DF_SC_PT_ISS_ROLLModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_SC_PT_ISS_ROLL_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DF_SC_PT_ISS_ROLLModel ob = new DF_SC_PT_ISS_ROLLModel();
                    ob.DF_SC_PT_ISS_ROLL_ID = (dr["DF_SC_PT_ISS_ROLL_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_SC_PT_ISS_ROLL_ID"]);
                    ob.DF_SC_PT_ISS_D1_ID = (dr["DF_SC_PT_ISS_D1_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_SC_PT_ISS_D1_ID"]);
                    ob.KNT_FAB_ROLL_ID = (dr["KNT_FAB_ROLL_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_FAB_ROLL_ID"]);
                    ob.IS_LNK_CHLN = (dr["IS_LNK_CHLN"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_LNK_CHLN"]);
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


        public List<DF_SC_PT_ISS_ROLLModel> SelectForScByID(Int64? pDF_SC_PT_ISS_D1_ID = null)
        {
            string sp = "PKG_DF_PT.DF_SC_PT_ISS_ROLL_SELECT";
            try
            {
                var obList = new List<DF_SC_PT_ISS_ROLLModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_SC_PT_ISS_D1_ID", Value =pDF_SC_PT_ISS_D1_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DF_SC_PT_ISS_ROLLModel ob = new DF_SC_PT_ISS_ROLLModel();
                    ob.DF_SC_PT_ISS_ROLL_ID = (dr["DF_SC_PT_ISS_ROLL_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_SC_PT_ISS_ROLL_ID"]);
                    ob.DF_SC_PT_ISS_D1_ID = (dr["DF_SC_PT_ISS_D1_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_SC_PT_ISS_D1_ID"]);
                    ob.KNT_FAB_ROLL_ID = (dr["KNT_FAB_ROLL_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_FAB_ROLL_ID"]);
                    ob.IS_LNK_CHLN = (dr["IS_LNK_CHLN"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_LNK_CHLN"]);
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

        public DF_SC_PT_ISS_ROLLModel Select(long ID)
        {
            string sp = "Select_DF_SC_PT_ISS_ROLL";
            try
            {
                var ob = new DF_SC_PT_ISS_ROLLModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_SC_PT_ISS_ROLL_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.DF_SC_PT_ISS_ROLL_ID = (dr["DF_SC_PT_ISS_ROLL_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_SC_PT_ISS_ROLL_ID"]);
                    ob.DF_SC_PT_ISS_D1_ID = (dr["DF_SC_PT_ISS_D1_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_SC_PT_ISS_D1_ID"]);
                    ob.KNT_FAB_ROLL_ID = (dr["KNT_FAB_ROLL_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_FAB_ROLL_ID"]);
                    ob.IS_LNK_CHLN = (dr["IS_LNK_CHLN"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_LNK_CHLN"]);
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


        public List<DF_SC_PT_ISS_ROLLModel> SelectForScRtn(Int64? pKNT_STYL_FAB_ITEM_ID = null, string pFAB_ROLL_NO = null)
        {
            string sp = "PKG_DF_PT.DF_SC_PT_ISS_ROLL_SELECT";
            try
            {
                var obList = new List<DF_SC_PT_ISS_ROLLModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_STYL_FAB_ITEM_ID", Value =pKNT_STYL_FAB_ITEM_ID},
                     new CommandParameter() {ParameterName = "pFAB_ROLL_NO", Value =pFAB_ROLL_NO},
                     new CommandParameter() {ParameterName = "pOption", Value =3002},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DF_SC_PT_ISS_ROLLModel ob = new DF_SC_PT_ISS_ROLLModel();
                    ob.DF_SC_PT_ISS_ROLL_ID = (dr["DF_SC_PT_ISS_ROLL_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_SC_PT_ISS_ROLL_ID"]);
                    ob.DF_SC_PT_ISS_D1_ID = (dr["DF_SC_PT_ISS_D1_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_SC_PT_ISS_D1_ID"]);
                    ob.KNT_FAB_ROLL_ID = (dr["KNT_FAB_ROLL_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_FAB_ROLL_ID"]);
                    ob.IS_LNK_CHLN = (dr["IS_LNK_CHLN"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_LNK_CHLN"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);

                    ob.FAB_ROLL_NO = (dr["FAB_ROLL_NO"] == DBNull.Value) ? String.Empty : Convert.ToString(dr["FAB_ROLL_NO"]);
                    ob.RCV_ROLL_WT = (dr["RCV_ROLL_WT"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RCV_ROLL_WT"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string FAB_ROLL_NO { get; set; }

        public decimal RCV_ROLL_WT { get; set; }
    }
}