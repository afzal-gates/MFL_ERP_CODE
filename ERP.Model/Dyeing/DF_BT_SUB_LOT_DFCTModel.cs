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
    public class DF_BT_SUB_LOT_DFCTModel
    {
        public Int64 DF_BT_SUB_LOT_DFCT_ID { get; set; }
        public Int64 DF_BT_SUB_LOT_ID { get; set; }
        public Int64 RF_FB_DFCT_TYPE_ID { get; set; }
        public Int64 RF_RESP_DEPT_ID { get; set; }

        public string Save()
        {
            const string sp = "SP_DF_BT_SUB_LOT_DFCT";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_BT_SUB_LOT_DFCT_ID", Value = ob.DF_BT_SUB_LOT_DFCT_ID},
                     new CommandParameter() {ParameterName = "pDF_BT_SUB_LOT_ID", Value = ob.DF_BT_SUB_LOT_ID},
                     new CommandParameter() {ParameterName = "pRF_FB_DFCT_TYPE_ID", Value = ob.RF_FB_DFCT_TYPE_ID},
                     new CommandParameter() {ParameterName = "pRF_RESP_DEPT_ID", Value = ob.RF_RESP_DEPT_ID},
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
            const string sp = "SP_DF_BT_SUB_LOT_DFCT";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_BT_SUB_LOT_DFCT_ID", Value = ob.DF_BT_SUB_LOT_DFCT_ID},
                     new CommandParameter() {ParameterName = "pDF_BT_SUB_LOT_ID", Value = ob.DF_BT_SUB_LOT_ID},
                     new CommandParameter() {ParameterName = "pRF_FB_DFCT_TYPE_ID", Value = ob.RF_FB_DFCT_TYPE_ID},
                     new CommandParameter() {ParameterName = "pRF_RESP_DEPT_ID", Value = ob.RF_RESP_DEPT_ID},
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
            const string sp = "SP_DF_BT_SUB_LOT_DFCT";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_BT_SUB_LOT_DFCT_ID", Value = ob.DF_BT_SUB_LOT_DFCT_ID},
                     new CommandParameter() {ParameterName = "pDF_BT_SUB_LOT_ID", Value = ob.DF_BT_SUB_LOT_ID},
                     new CommandParameter() {ParameterName = "pRF_FB_DFCT_TYPE_ID", Value = ob.RF_FB_DFCT_TYPE_ID},
                     new CommandParameter() {ParameterName = "pRF_RESP_DEPT_ID", Value = ob.RF_RESP_DEPT_ID},
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

        public List<DF_BT_SUB_LOT_DFCTModel> SelectAll()
        {
            string sp = "Select_DF_BT_SUB_LOT_DFCT";
            try
            {
                var obList = new List<DF_BT_SUB_LOT_DFCTModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_BT_SUB_LOT_DFCT_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DF_BT_SUB_LOT_DFCTModel ob = new DF_BT_SUB_LOT_DFCTModel();
                    ob.DF_BT_SUB_LOT_DFCT_ID = (dr["DF_BT_SUB_LOT_DFCT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_BT_SUB_LOT_DFCT_ID"]);
                    ob.DF_BT_SUB_LOT_ID = (dr["DF_BT_SUB_LOT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_BT_SUB_LOT_ID"]);
                    ob.RF_FB_DFCT_TYPE_ID = (dr["RF_FB_DFCT_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_FB_DFCT_TYPE_ID"]);
                    ob.RF_RESP_DEPT_ID = (dr["RF_RESP_DEPT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_RESP_DEPT_ID"]);
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<DF_BT_SUB_LOT_DFCTModel> SelectByLotID(Int64? pDF_BT_SUB_LOT_ID = null)
        {
            string sp = "PKG_DF_INSPECTION.DF_BT_SUB_LOT_DFCT_SELECT";
            try
            {
                var obList = new List<DF_BT_SUB_LOT_DFCTModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_BT_SUB_LOT_ID", Value =pDF_BT_SUB_LOT_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DF_BT_SUB_LOT_DFCTModel ob = new DF_BT_SUB_LOT_DFCTModel();
                    ob.DF_BT_SUB_LOT_DFCT_ID = (dr["DF_BT_SUB_LOT_DFCT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_BT_SUB_LOT_DFCT_ID"]);
                    ob.DF_BT_SUB_LOT_ID = (dr["DF_BT_SUB_LOT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_BT_SUB_LOT_ID"]);
                    ob.RF_FB_DFCT_TYPE_ID = (dr["RF_FB_DFCT_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_FB_DFCT_TYPE_ID"]);
                    ob.RF_RESP_DEPT_ID = (dr["RF_RESP_DEPT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_RESP_DEPT_ID"]);
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public DF_BT_SUB_LOT_DFCTModel Select(long ID)
        {
            string sp = "Select_DF_BT_SUB_LOT_DFCT";
            try
            {
                var ob = new DF_BT_SUB_LOT_DFCTModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_BT_SUB_LOT_DFCT_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.DF_BT_SUB_LOT_DFCT_ID = (dr["DF_BT_SUB_LOT_DFCT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_BT_SUB_LOT_DFCT_ID"]);
                    ob.DF_BT_SUB_LOT_ID = (dr["DF_BT_SUB_LOT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_BT_SUB_LOT_ID"]);
                    ob.RF_FB_DFCT_TYPE_ID = (dr["RF_FB_DFCT_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_FB_DFCT_TYPE_ID"]);
                    ob.RF_RESP_DEPT_ID = (dr["RF_RESP_DEPT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_RESP_DEPT_ID"]);

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