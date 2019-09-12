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
    public class MC_BYR_ACC_GRPModel
    {
        public Int64 MC_BYR_ACC_GRP_ID { get; set; }
        public string BYR_ACC_GRP_NAME_EN { get; set; }
        public string BYR_ACC_GRP_NAME_BN { get; set; }
        public string BYR_ACC_GRP_SNAME { get; set; }
        public Int64 DISPLAY_ORDER { get; set; }
        public string IS_ACTIVE { get; set; }
        public string MC_BYR_ACC_ID_LST { get; set; }

        public string Save()
        {
            const string sp = "SP_MC_BYR_ACC_GRP";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_BYR_ACC_GRP_ID", Value = ob.MC_BYR_ACC_GRP_ID},
                     new CommandParameter() {ParameterName = "pBYR_ACC_GRP_NAME_EN", Value = ob.BYR_ACC_GRP_NAME_EN},
                     new CommandParameter() {ParameterName = "pBYR_ACC_GRP_NAME_BN", Value = ob.BYR_ACC_GRP_NAME_BN},
                     new CommandParameter() {ParameterName = "pBYR_ACC_GRP_SNAME", Value = ob.BYR_ACC_GRP_SNAME},
                     new CommandParameter() {ParameterName = "pDISPLAY_ORDER", Value = ob.DISPLAY_ORDER},
                     new CommandParameter() {ParameterName = "pIS_ACTIVE", Value = ob.IS_ACTIVE},
                     
                     //new CommandParameter() {ParameterName = "pCREATED_BY", Value = ob.CREATED_BY},
                     
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
            const string sp = "SP_MC_BYR_ACC_GRP";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_BYR_ACC_GRP_ID", Value = ob.MC_BYR_ACC_GRP_ID},
                     new CommandParameter() {ParameterName = "pBYR_ACC_GRP_NAME_EN", Value = ob.BYR_ACC_GRP_NAME_EN},
                     new CommandParameter() {ParameterName = "pBYR_ACC_GRP_NAME_BN", Value = ob.BYR_ACC_GRP_NAME_BN},
                     new CommandParameter() {ParameterName = "pBYR_ACC_GRP_SNAME", Value = ob.BYR_ACC_GRP_SNAME},
                     new CommandParameter() {ParameterName = "pDISPLAY_ORDER", Value = ob.DISPLAY_ORDER},
                     new CommandParameter() {ParameterName = "pIS_ACTIVE", Value = ob.IS_ACTIVE},                     
                     //new CommandParameter() {ParameterName = "pCREATED_BY", Value = ob.CREATED_BY},
     
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



        public List<MC_BYR_ACC_GRPModel> GetBuyerAccGrpList()
        {
            string sp = "PKG_MERCHANDISING.mc_byr_acc_grp_select";
            try
            {
                var obList = new List<MC_BYR_ACC_GRPModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_BYR_ACC_GRP_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_BYR_ACC_GRPModel ob = new MC_BYR_ACC_GRPModel();
                    ob.MC_BYR_ACC_GRP_ID = (dr["MC_BYR_ACC_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BYR_ACC_GRP_ID"]);
                    ob.BYR_ACC_GRP_NAME_EN = (dr["BYR_ACC_GRP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BYR_ACC_GRP_NAME_EN"]);
                    ob.BYR_ACC_GRP_NAME_BN = (dr["BYR_ACC_GRP_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BYR_ACC_GRP_NAME_BN"]);
                    ob.BYR_ACC_GRP_SNAME = (dr["BYR_ACC_GRP_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BYR_ACC_GRP_SNAME"]);
                    ob.DISPLAY_ORDER = (dr["DISPLAY_ORDER"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DISPLAY_ORDER"]);
                    ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);
                    ob.MC_BYR_ACC_ID_LST = (dr["MC_BYR_ACC_ID_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MC_BYR_ACC_ID_LST"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public MC_BYR_ACC_GRPModel Select(long ID)
        {
            string sp = "Select_MC_BYR_ACC_GRP";
            try
            {
                var ob = new MC_BYR_ACC_GRPModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_BYR_ACC_GRP_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.MC_BYR_ACC_GRP_ID = (dr["MC_BYR_ACC_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BYR_ACC_GRP_ID"]);
                    ob.BYR_ACC_GRP_NAME_EN = (dr["BYR_ACC_GRP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BYR_ACC_GRP_NAME_EN"]);
                    ob.BYR_ACC_GRP_NAME_BN = (dr["BYR_ACC_GRP_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BYR_ACC_GRP_NAME_BN"]);
                    ob.BYR_ACC_GRP_SNAME = (dr["BYR_ACC_GRP_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BYR_ACC_GRP_SNAME"]);
                    ob.DISPLAY_ORDER = (dr["DISPLAY_ORDER"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DISPLAY_ORDER"]);
                    ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);

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