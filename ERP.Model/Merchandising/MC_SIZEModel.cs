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
    public class MC_SIZEModel
    {
        public Int64? MC_SIZE_ID { get; set; }
        [DisplayName("Size Code")]
        [Required(ErrorMessage = " The field {0} is required")]
        public string SIZE_CODE { get; set; }

        [DisplayName("Size Name")]
        [Required(ErrorMessage = " The field {0} is required")]
        public string SIZE_NAME_EN { get; set; }
        public string SIZE_NAME_BN { get; set; }
        public string IS_ACTIVE { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }

        public Int64? MC_SIZE_GRP_ID { get; set; }
        public Int64? DISPLAY_ORDER { get; set; }

        public Int64? MC_STYLE_H_ID { get; set; }
        public Int64? MEAS_LENGTH { get; set; }
        public Int64? MEAS_WIDTH { get; set; }
        public long RF_GARM_PART_ID { get; set; }
        public string IS_FINALIZED { get; set; }
        public Int64? MC_ORDER_SIZE_ID { get; set; }

        public string Save()
        {
            const string sp = "pkg_merchandising.mc_size_insert";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_SIZE_ID", Value = ob.MC_SIZE_ID},
                     new CommandParameter() {ParameterName = "pSIZE_CODE", Value = ob.SIZE_CODE},
                     new CommandParameter() {ParameterName = "pSIZE_NAME_EN", Value = ob.SIZE_NAME_EN},
                     new CommandParameter() {ParameterName = "pSIZE_NAME_BN", Value = ob.SIZE_NAME_BN},
                     new CommandParameter() {ParameterName = "pIS_ACTIVE", Value = ob.IS_ACTIVE},
                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = ob.CREATED_BY},
                     new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = ob.LAST_UPDATED_BY},
                     new CommandParameter() {ParameterName = "pOption", Value =1000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output},
                     new CommandParameter() {ParameterName = "V_MC_SIZE_ID", Value =null, Direction = ParameterDirection.Output}
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
            const string sp = "pkg_merchandising.mc_size_update";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_SIZE_ID", Value = ob.MC_SIZE_ID},
                     new CommandParameter() {ParameterName = "pSIZE_CODE", Value = ob.SIZE_CODE},
                     new CommandParameter() {ParameterName = "pSIZE_NAME_EN", Value = ob.SIZE_NAME_EN},
                     new CommandParameter() {ParameterName = "pSIZE_NAME_BN", Value = ob.SIZE_NAME_BN},
                     new CommandParameter() {ParameterName = "pIS_ACTIVE", Value = ob.IS_ACTIVE},
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
            const string sp = "pkg_merchandising.mc_size_delete";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_SIZE_ID", Value = ob.MC_SIZE_ID},
                     new CommandParameter() {ParameterName = "pSIZE_CODE", Value = ob.SIZE_CODE},
                     new CommandParameter() {ParameterName = "pSIZE_NAME_EN", Value = ob.SIZE_NAME_EN},
                     new CommandParameter() {ParameterName = "pSIZE_NAME_BN", Value = ob.SIZE_NAME_BN},
                     new CommandParameter() {ParameterName = "pIS_ACTIVE", Value = ob.IS_ACTIVE},
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

        public List<MC_SIZEModel> SelectAll()
        {
            string sp = "pkg_merchandising.mc_size_select";
            try
            {
                var obList = new List<MC_SIZEModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_SIZE_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_SIZEModel ob = new MC_SIZEModel();
                    ob.MC_SIZE_ID = (dr["MC_SIZE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_SIZE_ID"]);
                    ob.SIZE_CODE = (dr["SIZE_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SIZE_CODE"]);
                    ob.SIZE_NAME_EN = (dr["SIZE_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SIZE_NAME_EN"]);
                    ob.SIZE_NAME_BN = (dr["SIZE_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SIZE_NAME_BN"]);
                    ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);

                    if (dr["MC_SIZE_GRP_ID"] != null)
                    { ob.MC_SIZE_GRP_ID = (dr["MC_SIZE_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_SIZE_GRP_ID"]); }
                    ob.DISPLAY_ORDER = (dr["DISPLAY_ORDER"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DISPLAY_ORDER"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public MC_SIZEModel Select(long ID)
        {
            string sp = "pkg_merchandising.mc_size_select";
            try
            {
                var ob = new MC_SIZEModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_SIZE_ID", Value =ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.MC_SIZE_ID = (dr["MC_SIZE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_SIZE_ID"]);
                    ob.SIZE_CODE = (dr["SIZE_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SIZE_CODE"]);
                    ob.SIZE_NAME_EN = (dr["SIZE_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SIZE_NAME_EN"]);
                    ob.SIZE_NAME_BN = (dr["SIZE_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SIZE_NAME_BN"]);
                    ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);

                }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        


        public List<MC_SIZEModel> OrderWiseSizeList(long? pMC_ORDER_H_ID)
        {
            string sp = "pkg_mc_order.mc_order_h_select";
            try
            {
                var obList = new List<MC_SIZEModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_ORDER_H_ID", Value = pMC_ORDER_H_ID},
                     new CommandParameter() {ParameterName = "pOption", Value = 3008},
                     new CommandParameter() {ParameterName = "pMsg", Value = 500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_SIZEModel ob = new MC_SIZEModel();
                    ob.MC_SIZE_ID = (dr["MC_SIZE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_SIZE_ID"]);
                    ob.SIZE_CODE = (dr["SIZE_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SIZE_CODE"]);
                    ob.SIZE_NAME_EN = (dr["SIZE_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SIZE_NAME_EN"]);
                    ob.SIZE_NAME_BN = (dr["SIZE_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SIZE_NAME_BN"]);
                    ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);

                    //ob.MC_ORDER_SIZE_ID = (dr["MC_ORDER_SIZE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_ORDER_SIZE_ID"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<MC_SIZEModel> MultiOrderWiseSizeList(string pMC_ORDER_H_IDS)
        {
            string sp = "pkg_mc_order.mc_order_h_select";
            try
            {
                var obList = new List<MC_SIZEModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_ORDER_H_IDS", Value = pMC_ORDER_H_IDS},
                     new CommandParameter() {ParameterName = "pOption", Value = 3009},
                     new CommandParameter() {ParameterName = "pMsg", Value = 500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_SIZEModel ob = new MC_SIZEModel();
                    ob.MC_SIZE_ID = (dr["MC_SIZE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_SIZE_ID"]);

                    if (dr["MC_SIZE_GRP_ID"] != null)
                    { ob.MC_SIZE_GRP_ID = (dr["MC_SIZE_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_SIZE_GRP_ID"]); }
                    ob.DISPLAY_ORDER = (dr["DISPLAY_ORDER"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DISPLAY_ORDER"]);

                    ob.SIZE_CODE = (dr["SIZE_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SIZE_CODE"]);
                    ob.SIZE_NAME_EN = (dr["SIZE_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SIZE_NAME_EN"]);
                    ob.SIZE_NAME_BN = (dr["SIZE_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SIZE_NAME_BN"]);
                    ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public List<MC_SIZEModel> StyleWiseSizeList(Int64 pMC_STYLE_H_ID)
        {
            string sp = "pkg_mc_order.mc_order_h_select";
            try
            {
                var obList = new List<MC_SIZEModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_STYLE_H_ID", Value = pMC_STYLE_H_ID},
                     new CommandParameter() {ParameterName = "pOption", Value = 3013},
                     new CommandParameter() {ParameterName = "pMsg", Value = 500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_SIZEModel ob = new MC_SIZEModel();
                    ob.MC_SIZE_ID = (dr["MC_SIZE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_SIZE_ID"]);

                    if (dr["MC_SIZE_GRP_ID"] != null)
                    { ob.MC_SIZE_GRP_ID = (dr["MC_SIZE_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_SIZE_GRP_ID"]); }
                    ob.DISPLAY_ORDER = (dr["DISPLAY_ORDER"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DISPLAY_ORDER"]);

                    ob.SIZE_CODE = (dr["SIZE_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SIZE_CODE"]);
                    ob.SIZE_NAME_EN = (dr["SIZE_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SIZE_NAME_EN"]);
                    ob.SIZE_NAME_BN = (dr["SIZE_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SIZE_NAME_BN"]);
                    ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}