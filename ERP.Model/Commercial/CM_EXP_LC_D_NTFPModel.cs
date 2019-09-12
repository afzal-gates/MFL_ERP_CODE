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
    public class CM_EXP_LC_D_NTFPModel
    {
        public Int64 CM_EXP_LC_D_NTFP_ID { get; set; }
        public Int64 CM_EXP_LC_H_ID { get; set; }
        public Int64 CM_NOTIFY_PARTY_ID { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }

        public string Save()
        {
            const string sp = "SP_CM_EXP_LC_D_NTFP";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pCM_EXP_LC_D_NTFP_ID", Value = ob.CM_EXP_LC_D_NTFP_ID},
                     new CommandParameter() {ParameterName = "pCM_EXP_LC_H_ID", Value = ob.CM_EXP_LC_H_ID},
                     new CommandParameter() {ParameterName = "pCM_NOTIFY_PARTY_ID", Value = ob.CM_NOTIFY_PARTY_ID},
                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = ob.CREATED_BY},
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
            const string sp = "SP_CM_EXP_LC_D_NTFP";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pCM_EXP_LC_D_NTFP_ID", Value = ob.CM_EXP_LC_D_NTFP_ID},
                     new CommandParameter() {ParameterName = "pCM_EXP_LC_H_ID", Value = ob.CM_EXP_LC_H_ID},
                     new CommandParameter() {ParameterName = "pCM_NOTIFY_PARTY_ID", Value = ob.CM_NOTIFY_PARTY_ID},
                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = ob.CREATED_BY},
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
            const string sp = "SP_CM_EXP_LC_D_NTFP";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pCM_EXP_LC_D_NTFP_ID", Value = ob.CM_EXP_LC_D_NTFP_ID},
                     new CommandParameter() {ParameterName = "pCM_EXP_LC_H_ID", Value = ob.CM_EXP_LC_H_ID},
                     new CommandParameter() {ParameterName = "pCM_NOTIFY_PARTY_ID", Value = ob.CM_NOTIFY_PARTY_ID},
                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = ob.CREATED_BY},
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

        public List<CM_EXP_LC_D_NTFPModel> SelectAll()
        {
            string sp = "Select_CM_EXP_LC_D_NTFP";
            try
            {
                var obList = new List<CM_EXP_LC_D_NTFPModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pCM_EXP_LC_D_NTFP_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    CM_EXP_LC_D_NTFPModel ob = new CM_EXP_LC_D_NTFPModel();
                    ob.CM_EXP_LC_D_NTFP_ID = (dr["CM_EXP_LC_D_NTFP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CM_EXP_LC_D_NTFP_ID"]);
                    ob.CM_EXP_LC_H_ID = (dr["CM_EXP_LC_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CM_EXP_LC_H_ID"]);
                    ob.CM_NOTIFY_PARTY_ID = (dr["CM_NOTIFY_PARTY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CM_NOTIFY_PARTY_ID"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<CM_EXP_LC_D_NTFPModel> SelectByID(Int64? pCM_EXP_LC_H_ID = null)
        {
            string sp = "Select_CM_EXP_LC_D_NTFP";
            try
            {
                var obList = new List<CM_EXP_LC_D_NTFPModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pCM_EXP_LC_H_ID", Value =pCM_EXP_LC_H_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    CM_EXP_LC_D_NTFPModel ob = new CM_EXP_LC_D_NTFPModel();
                    ob.CM_EXP_LC_D_NTFP_ID = (dr["CM_EXP_LC_D_NTFP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CM_EXP_LC_D_NTFP_ID"]);
                    ob.CM_EXP_LC_H_ID = (dr["CM_EXP_LC_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CM_EXP_LC_H_ID"]);
                    ob.CM_NOTIFY_PARTY_ID = (dr["CM_NOTIFY_PARTY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CM_NOTIFY_PARTY_ID"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CM_EXP_LC_D_NTFPModel Select(long ID)
        {
            string sp = "Select_CM_EXP_LC_D_NTFP";
            try
            {
                var ob = new CM_EXP_LC_D_NTFPModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pCM_EXP_LC_D_NTFP_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.CM_EXP_LC_D_NTFP_ID = (dr["CM_EXP_LC_D_NTFP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CM_EXP_LC_D_NTFP_ID"]);
                    ob.CM_EXP_LC_H_ID = (dr["CM_EXP_LC_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CM_EXP_LC_H_ID"]);
                    ob.CM_NOTIFY_PARTY_ID = (dr["CM_NOTIFY_PARTY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CM_NOTIFY_PARTY_ID"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);

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