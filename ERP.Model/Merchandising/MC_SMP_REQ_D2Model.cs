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
    public class MC_SMP_REQ_D2Model
    {
        public Int64? MC_SMP_REQ_D2_ID { get; set; }
        public long MC_SMP_REQ_STYL_ID { get; set; }
        public Int64? MC_STYLE_D_FAB_ID { get; set; }
        public Int64? MC_COLOR_ID { get; set; }
        public Int64? GMT_COLOR_ID { get; set; }
        public string RF_GARM_PART_LST { get; set; }
        public string IS_CONTRAST { get; set; }
        public Int64? LK_FBR_GRP_ID { get; set; }
        public Int64? FIN_DIA { get; set; }
        public Int64? DIA_MOU_ID { get; set; }
        public Int64? LK_DIA_TYPE_ID { get; set; }
        public string FIN_DIA_DESC { get; set; }
        public string RF_GARM_PART_NAME_LST { get; set; }
        public string FAB_TYPE_DESC { get; set; }
        public string OTHER_SPEC { get; set; }
        public Decimal? RQD_QTY { get; set; }
        public Int64? QTY_MOU_ID { get; set; }

        public string STYLE_NO { get; set; }
        public List<MC_STYLE_D_FABModel> MC_STYLE_D_FAB_LIST { get; set; }
        public string[] RF_GARM_PART_LIST { get; set; }
        public List<RF_GARM_PARTModel> GMT_PART_LIST { get; set; }
        public List<LookupDataModel> DIA_TYPE_LIST { get; set; }
        public List<MC_SMP_REQ_D3Model> itemsColorQty { get; set; }
        public List<RF_MOUModel> QTY_MOU_LIST { get; set; }



        public List<MC_SMP_REQ_D2Model> SelectAll()
        {
            string sp = "Select_MC_SMP_REQ_D2";
            try
            {
                var obList = new List<MC_SMP_REQ_D2Model>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_SMP_REQ_D2_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value = 3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_SMP_REQ_D2Model ob = new MC_SMP_REQ_D2Model();
                    ob.MC_SMP_REQ_D2_ID = (dr["MC_SMP_REQ_D2_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_SMP_REQ_D2_ID"]);
                    //ob.MC_SMP_REQ_H_ID = (dr["MC_SMP_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_SMP_REQ_H_ID"]);
                    //ob.MC_ORDER_H_ID = (dr["MC_ORDER_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_ORDER_H_ID"]);
                    ob.MC_STYLE_D_FAB_ID = (dr["MC_STYLE_D_FAB_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_D_FAB_ID"]);
                    ob.RF_GARM_PART_LST = (dr["RF_GARM_PART_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["RF_GARM_PART_LST"]);
                    ob.IS_CONTRAST = (dr["IS_CONTRAST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_CONTRAST"]);
                    ob.LK_FBR_GRP_ID = (dr["LK_FBR_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_FBR_GRP_ID"]);
                    ob.FIN_DIA = (dr["FIN_DIA"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["FIN_DIA"]);
                    ob.DIA_MOU_ID = (dr["DIA_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DIA_MOU_ID"]);
                    ob.LK_DIA_TYPE_ID = (dr["LK_DIA_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_DIA_TYPE_ID"]);
                    ob.OTHER_SPEC = (dr["OTHER_SPEC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["OTHER_SPEC"]);
                    //ob.CONS_QTY = (dr["CONS_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["CONS_QTY"]);
                    //ob.CONS_MOU_ID = (dr["CONS_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CONS_MOU_ID"]);
                    ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public MC_SMP_REQ_D2Model Select(long ID)
        {
            string sp = "Select_MC_SMP_REQ_D2";
            try
            {
                var ob = new MC_SMP_REQ_D2Model();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_SMP_REQ_D2_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value = 3001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.MC_SMP_REQ_D2_ID = (dr["MC_SMP_REQ_D2_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_SMP_REQ_D2_ID"]);
                    //ob.MC_SMP_REQ_H_ID = (dr["MC_SMP_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_SMP_REQ_H_ID"]);
                    //ob.MC_ORDER_H_ID = (dr["MC_ORDER_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_ORDER_H_ID"]);
                    ob.MC_STYLE_D_FAB_ID = (dr["MC_STYLE_D_FAB_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_D_FAB_ID"]);
                    ob.RF_GARM_PART_LST = (dr["RF_GARM_PART_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["RF_GARM_PART_LST"]);
                    ob.IS_CONTRAST = (dr["IS_CONTRAST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_CONTRAST"]);
                    ob.LK_FBR_GRP_ID = (dr["LK_FBR_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_FBR_GRP_ID"]);
                    ob.FIN_DIA = (dr["FIN_DIA"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["FIN_DIA"]);
                    ob.DIA_MOU_ID = (dr["DIA_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DIA_MOU_ID"]);
                    ob.LK_DIA_TYPE_ID = (dr["LK_DIA_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_DIA_TYPE_ID"]);
                    ob.OTHER_SPEC = (dr["OTHER_SPEC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["OTHER_SPEC"]);
                    //ob.CONS_QTY = (dr["CONS_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["CONS_QTY"]);
                    //ob.CONS_MOU_ID = (dr["CONS_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CONS_MOU_ID"]);
                    ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);

                }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<MC_SMP_REQ_D2Model> SamFabQtyStyleListByHID(Int64 pMC_SMP_REQ_H_ID)
        {
            int vFirst = 0;
            string sp = "pkg_mc_fab_booking.mc_smp_req_d2_select";
            try
            {
                var obList = new List<MC_SMP_REQ_D2Model>();
                //OraDatabase db = new OraDatabase();
                //var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                //{
                //     new CommandParameter() {ParameterName = "pMC_SMP_REQ_H_ID", Value = pMC_SMP_REQ_H_ID},
                //     new CommandParameter() {ParameterName = "pOption", Value = 3002},
                //     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                // }, sp);

                var obGmtPartList = new RF_GARM_PARTModel().SelectAll();
                var obDiaTypeList = new LookupDataModel().LookupListData(67);

                //foreach (DataRow dr in ds.Tables[0].Rows)
                //{                    
                //    MC_SMP_REQ_D2Model ob = new MC_SMP_REQ_D2Model();
                //    ob.MC_SMP_REQ_D2_ID = (dr["MC_SMP_REQ_D2_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_SMP_REQ_D2_ID"]);
                //    ob.MC_SMP_REQ_H_ID = (dr["MC_SMP_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_SMP_REQ_H_ID"]);
                //    ob.MC_ORDER_H_ID = (dr["MC_ORDER_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_ORDER_H_ID"]);
                //    ob.MC_STYLE_D_FAB_ID = (dr["MC_STYLE_D_FAB_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_D_FAB_ID"]);
                //    ob.RF_GARM_PART_LST = (dr["RF_GARM_PART_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["RF_GARM_PART_LST"]);
                //    if (ob.RF_GARM_PART_LST != "")
                //    { ob.RF_GARM_PART_LIST = ob.RF_GARM_PART_LST.Split(','); }

                //    ob.IS_CONTRAST = (dr["IS_CONTRAST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_CONTRAST"]);
                //    if (dr["LK_FBR_GRP_ID"] != DBNull.Value)
                //    { ob.LK_FBR_GRP_ID = (dr["LK_FBR_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_FBR_GRP_ID"]); }
                //    ob.FIN_DIA = (dr["FIN_DIA"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["FIN_DIA"]);
                //    if (dr["DIA_MOU_ID"] != DBNull.Value)
                //    { ob.DIA_MOU_ID = (dr["DIA_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DIA_MOU_ID"]); }
                //    ob.LK_DIA_TYPE_ID = (dr["LK_DIA_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_DIA_TYPE_ID"]);
                //    ob.OTHER_SPEC = (dr["OTHER_SPEC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["OTHER_SPEC"]);
                //    ob.CONS_QTY = (dr["CONS_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["CONS_QTY"]);

                //    if (dr["CONS_MOU_ID"] != DBNull.Value)
                //    { ob.CONS_MOU_ID = (dr["CONS_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CONS_MOU_ID"]); }
                //    if (dr["QTY_MOU_ID"] != DBNull.Value)
                //    { ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]); }

                //    ob.STYLE_NO = (dr["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO"]);

                //    var obFabList = new MC_STYLE_D_FABModel().SelectFabByStyleID(Convert.ToInt64(dr["MC_STYLE_H_ID"]));
                //    ob.MC_STYLE_D_FAB_LIST = (List<MC_STYLE_D_FABModel>)obFabList;

                //    var obColorQtyList = new MC_SMP_REQ_D3Model().SampFabColorWiseQtyByStyle(Convert.ToInt64(dr["MC_STYLE_H_ID"]));
                //    ob.itemsColorQty = (List<MC_SMP_REQ_D3Model>)obColorQtyList;

                //    ob.GMT_PART_LIST = (List<RF_GARM_PARTModel>)obGmtPartList;
                //    ob.DIA_TYPE_LIST = (List<LookupDataModel>)obDiaTypeList;


                //    obList.Add(ob);
                //}
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object SamFabBookingQtyList(long pMC_SMP_REQ_H_ID)
        {
            int vFirst = 0;
            string sp = "pkg_mc_fab_booking.mc_smp_req_d2_select";
            try
            {
                var obList = new List<SMP_REQ_D2_STYLEVm>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_SMP_REQ_H_ID", Value = pMC_SMP_REQ_H_ID},
                     new CommandParameter() {ParameterName = "pOption", Value = 3002},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);

                //var obGmtPartList = new RF_GARM_PARTModel().SelectAll();
                //var obDiaTypeList = new LookupDataModel().LookupListData(67);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    SMP_REQ_D2_STYLEVm ob = new SMP_REQ_D2_STYLEVm();
                    ob.MC_SMP_REQ_STYL_ID = (dr["MC_SMP_REQ_STYL_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_SMP_REQ_STYL_ID"]);
                    ob.STYLE_NO = (dr["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO"]);
                    ob.MC_STYLE_H_EXT_ID = (dr["MC_STYLE_H_EXT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_H_EXT_ID"]);
                    ob.MC_ORDER_H_ID = (dr["MC_ORDER_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_ORDER_H_ID"]);

                    ob.MC_STYLE_H_ID = (dr["MC_STYLE_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_H_ID"]);

                    var obColorList = new SMP_REQ_D2_STYLE_COLORVm().SelectColorByStyleID(Convert.ToInt64(dr["MC_STYLE_H_ID"]), pMC_SMP_REQ_H_ID,
                        Convert.ToInt64(dr["MC_SMP_REQ_STYL_ID"]), Convert.ToInt64(dr["MC_STYLE_H_EXT_ID"]));
                    ob.STYLE_COLORS = (List<SMP_REQ_D2_STYLE_COLORVm>)obColorList;

                    //var obColorQtyList = new MC_SMP_REQ_D3Model().SampFabColorWiseQtyByStyle(Convert.ToInt64(dr["MC_STYLE_H_ID"]));
                    //ob.itemsColorQty = (List<MC_SMP_REQ_D3Model>)obColorQtyList;

                    //ob.GMT_PART_LIST = (List<RF_GARM_PARTModel>)obGmtPartList;
                    //ob.DIA_TYPE_LIST = (List<LookupDataModel>)obDiaTypeList;

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public string DeleteSampFabColor()
        {
            const string sp = "pkg_mc_fab_booking.mc_smp_req_d2_delete";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                    new CommandParameter() {ParameterName = "pMC_SMP_REQ_STYL_ID", Value = ob.MC_SMP_REQ_STYL_ID},
                    new CommandParameter() {ParameterName = "pMC_COLOR_ID", Value = ob.MC_COLOR_ID},
                    new CommandParameter() {ParameterName = "pOption", Value = 4000},
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


        public long MC_PROV_FAB_BK_D_ID { get; set; }

        public long RF_FIB_COMP_ID { get; set; }

        public long RF_FAB_TYPE_ID { get; set; }
    }





    public class SMP_REQ_D2_STYLEVm
    {
        public Int64? MC_SMP_REQ_STYL_ID { get; set; }
        public Int64? MC_STYLE_H_EXT_ID { get; set; }
        public Int64? MC_ORDER_H_ID { get; set; }
        public Int64? MC_STYLE_H_ID { get; set; }
        public string STYLE_NO { get; set; }
        public List<SMP_REQ_D2_STYLE_COLORVm> STYLE_COLORS { get; set; }


    }

    public class SMP_REQ_D2_STYLE_COLORVm
    {
        public Int64? MC_COLOR_ID { get; set; }
        public string COLOR_NAME_EN { get; set; }

        public Int64? GMT_COLOR_ID { get; set; }
        
        public Int64? MC_SMP_REQ_D2_ID { get; set; }
        public Int64? LK_COL_TYPE_ID { get; set; }
        public Int64? LK_YD_TYPE_ID { get; set; }
        public string COLOR_SPEC { get; set; }
        public string IS_CONTRAST { get; set; }
        public long MC_PROV_FAB_BK_D_ID { get; set; }
        public string SP_INSTRUCTION { get; set; }
        public List<MC_COLORModel> STYLE_COLORS_LIST { get; set; }
        public List<SMP_REQ_D2_GMT_PARTVm> GMT_PARTS { get; set; }

        public object SelectColorByStyleID(long pMC_STYLE_H_ID, long pMC_SMP_REQ_H_ID, long pMC_SMP_REQ_STYL_ID, long pMC_STYLE_H_EXT_ID)
        {
            string sp = "pkg_mc_fab_booking.mc_smp_req_d2_select";
            try
            {
                var obMouList = new RF_MOUModel().SelectAll("Y");


                var obList = new List<SMP_REQ_D2_STYLE_COLORVm>();
                //var colorList = new MC_COLORModel().ColourMappDataByBuyerId(pMC_STYLE_H_ID);
                var colorList = new MC_COLORModel().OrderItemWiseColorList(null, null, "Y", pMC_STYLE_H_ID);

                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pOption", Value = 3003},
                     new CommandParameter() {ParameterName = "pMC_SMP_REQ_H_ID", Value = pMC_SMP_REQ_H_ID},
                     new CommandParameter() {ParameterName = "pMC_STYLE_H_ID", Value = pMC_STYLE_H_ID},
                     new CommandParameter() {ParameterName = "pMC_SMP_REQ_STYL_ID", Value = pMC_SMP_REQ_STYL_ID},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);


                //sp = "pkg_mc_fab_booking.mc_smp_req_d2_select";
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    SMP_REQ_D2_STYLE_COLORVm ob = new SMP_REQ_D2_STYLE_COLORVm();
                    ob.MC_COLOR_ID = (dr["MC_COLOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_COLOR_ID"]);
                    ob.COLOR_NAME_EN = (dr["COLOR_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_NAME_EN"]);

                    ob.IS_CONTRAST = (dr["IS_CONTRAST"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_CONTRAST"]);
                    if (dr["GMT_COLOR_ID"] != DBNull.Value)
                        ob.GMT_COLOR_ID = Convert.ToInt64(dr["GMT_COLOR_ID"]);
                    else
                        ob.GMT_COLOR_ID = ob.MC_COLOR_ID;
                    
                    ob.MC_SMP_REQ_D2_ID = (dr["MC_SMP_REQ_D2_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_SMP_REQ_D2_ID"]);

                    ob.LK_COL_TYPE_ID = (dr["LK_COL_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_COL_TYPE_ID"]);
                    if (dr["LK_YD_TYPE_ID"] != DBNull.Value)
                    { ob.LK_YD_TYPE_ID = (dr["LK_YD_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_YD_TYPE_ID"]); }
                    ob.COLOR_SPEC = (dr["COLOR_SPEC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_SPEC"]);


                    var obRd2List = new List<SMP_REQ_D2_GMT_PARTVm>();
                    var dsRd2 = db.ExecuteStoredProcedure(new List<CommandParameter>()
                    {
                         new CommandParameter() {ParameterName = "pOption", Value = 3004},
                         new CommandParameter() {ParameterName = "pMC_SMP_REQ_H_ID", Value = pMC_SMP_REQ_H_ID},
                         new CommandParameter() {ParameterName = "pMC_STYLE_H_ID", Value = pMC_STYLE_H_ID},
                         new CommandParameter() {ParameterName = "pMC_STYLE_H_EXT_ID", Value = pMC_STYLE_H_EXT_ID},
                         new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                     }, sp);
                    foreach (DataRow drRd2 in dsRd2.Tables[0].Rows)
                    {
                        SMP_REQ_D2_GMT_PARTVm obRd2 = new SMP_REQ_D2_GMT_PARTVm();

                        obRd2.MC_SMP_REQ_STYL_ID = (drRd2["MC_SMP_REQ_STYL_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(drRd2["MC_SMP_REQ_STYL_ID"]);
                        obRd2.MC_STYLE_D_FAB_ID = (drRd2["MC_STYLE_D_FAB_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(drRd2["MC_STYLE_D_FAB_ID"]);
                        obRd2.RF_GARM_PART_LST = (drRd2["RF_GARM_PART_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(drRd2["RF_GARM_PART_LST"]);
                        obRd2.FIN_DIA = (drRd2["FIN_DIA"] == DBNull.Value) ? string.Empty : Convert.ToString(drRd2["FIN_DIA"]);

                        if (drRd2["DIA_MOU_ID"] != DBNull.Value)
                        { obRd2.DIA_MOU_ID = (drRd2["DIA_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(drRd2["DIA_MOU_ID"]); }
                        else { obRd2.DIA_MOU_ID = 23; }

                        if (drRd2["LK_DIA_TYPE_ID"] != DBNull.Value)
                        { obRd2.LK_DIA_TYPE_ID = (drRd2["LK_DIA_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(drRd2["LK_DIA_TYPE_ID"]); }
                        else { obRd2.LK_DIA_TYPE_ID = 327; }

                        obRd2.FIN_DIA_DESC = (drRd2["FIN_DIA_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(drRd2["FIN_DIA_DESC"]);
                        obRd2.FAB_TYPE_DESC = (drRd2["FAB_TYPE_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(drRd2["FAB_TYPE_DESC"]);
                        obRd2.RF_GARM_PART_NAME_LST = (drRd2["RF_GARM_PART_NAME_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(drRd2["RF_GARM_PART_NAME_LST"]);

                        if (obRd2.RF_GARM_PART_LST != "")
                        { obRd2.RF_GARM_PART_ID_LST = obRd2.RF_GARM_PART_LST.Split(','); }
                        else
                        {
                            obRd2.RF_GARM_PART_ID_LST = new string[] { "1" };
                        }

                        var obQtyList = new List<MC_SMP_REQ_D2Model>();
                        var dsQty = db.ExecuteStoredProcedure(new List<CommandParameter>()
                        {
                             new CommandParameter() {ParameterName = "pOption", Value = 3005},
                             new CommandParameter() {ParameterName = "pMC_SMP_REQ_H_ID", Value = pMC_SMP_REQ_H_ID},
                             new CommandParameter() {ParameterName = "pMC_STYLE_H_ID", Value = pMC_STYLE_H_ID},
                             new CommandParameter() {ParameterName = "pMC_SMP_REQ_STYL_ID", Value = obRd2.MC_SMP_REQ_STYL_ID},
                             new CommandParameter() {ParameterName = "pMC_COLOR_ID", Value = ob.MC_COLOR_ID},
                             new CommandParameter() {ParameterName = "pIS_CONTRAST", Value = ob.IS_CONTRAST},
                             new CommandParameter() {ParameterName = "pMC_STYLE_D_FAB_ID", Value = obRd2.MC_STYLE_D_FAB_ID},
                             new CommandParameter() {ParameterName = "pLK_YD_TYPE_ID", Value = ob.LK_YD_TYPE_ID},

                             new CommandParameter() {ParameterName = "pFIN_DIA", Value = obRd2.FIN_DIA},
                             new CommandParameter() {ParameterName = "pLK_DIA_TYPE_ID", Value = obRd2.LK_DIA_TYPE_ID},

                             new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                         }, sp);

                        if (dsQty.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow drQty in dsQty.Tables[0].Rows)
                            {
                                MC_SMP_REQ_D2Model obQty = new MC_SMP_REQ_D2Model();
                                obQty.MC_SMP_REQ_D2_ID = (drQty["MC_SMP_REQ_D2_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(drQty["MC_SMP_REQ_D2_ID"]);
                                if (drQty["QTY_MOU_ID"] != DBNull.Value)
                                { obQty.QTY_MOU_ID = (drQty["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(drQty["QTY_MOU_ID"]); }

                                obQty.QTY_MOU_LIST = (List<RF_MOUModel>)obMouList;

                                obQty.RQD_QTY = (drQty["RQD_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(drQty["RQD_QTY"]);
                                obQtyList.Add(obQty);
                            }
                        }
                        else
                        {
                            MC_SMP_REQ_D2Model obQty = new MC_SMP_REQ_D2Model();
                            obQty.MC_SMP_REQ_D2_ID = 0;
                            obQty.QTY_MOU_ID = 3;
                            //obQty.RQD_QTY = 0;
                            obQtyList.Add(obQty);
                        }

                        obRd2.GMT_PARTS_QTY = obQtyList;
                        obRd2List.Add(obRd2);
                    }
                    ob.GMT_PARTS = (List<SMP_REQ_D2_GMT_PARTVm>)obRd2List;
                    ob.STYLE_COLORS_LIST = (List<MC_COLORModel>)colorList;

                    obList.Add(ob);
                }


                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public object ProjectionColorByOrderID(long pMC_ORDER_H_ID, long pMC_STYLE_H_ID, Int64? pMC_PROV_FAB_BK_H_ID = null)
        {
            string sp = "pkg_mc_fab_booking.mc_prov_ord_det_select";
            try
            {
                var obMouList = new RF_MOUModel().SelectAll("Y");


                var obList = new List<SMP_REQ_D2_STYLE_COLORVm>();
                var colorList = new MC_COLORModel().ColourMappDataByBuyerId(pMC_STYLE_H_ID);

                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pOption", Value = 3003},
                     new CommandParameter() {ParameterName = "pMC_ORDER_H_ID", Value =pMC_ORDER_H_ID},
                     new CommandParameter() {ParameterName = "pMC_PROV_FAB_BK_H_ID", Value =pMC_PROV_FAB_BK_H_ID},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);


                //sp = "pkg_mc_fab_booking.mc_smp_req_d2_select";
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    SMP_REQ_D2_STYLE_COLORVm ob = new SMP_REQ_D2_STYLE_COLORVm();
                    ob.MC_COLOR_ID = (dr["MC_COLOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_COLOR_ID"]);
                    ob.COLOR_NAME_EN = (dr["COLOR_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_NAME_EN"]);

                    //ob.MC_PROV_FAB_BK_D_ID = (dr["MC_PROV_FAB_BK_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_PROV_FAB_BK_D_ID"]);

                    ob.LK_COL_TYPE_ID = (dr["LK_COL_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_COL_TYPE_ID"]);
                    if (dr["LK_YD_TYPE_ID"] != DBNull.Value)
                    { ob.LK_YD_TYPE_ID = (dr["LK_YD_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_YD_TYPE_ID"]); }
                    ob.COLOR_SPEC = (dr["COLOR_SPEC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_SPEC"]);
                    ob.IS_CONTRAST = (dr["IS_CONTRAST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_CONTRAST"]);
                    ob.SP_INSTRUCTION = (dr["SP_INSTRUCTION"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SP_INSTRUCTION"]);

                    var obRd2List = new List<SMP_REQ_D2_GMT_PARTVm>();
                    var dsRd2 = db.ExecuteStoredProcedure(new List<CommandParameter>()
                    {
                         new CommandParameter() {ParameterName = "pOption", Value = 3004},
                         new CommandParameter() {ParameterName = "pMC_ORDER_H_ID", Value = pMC_ORDER_H_ID},
                         new CommandParameter() {ParameterName = "pMC_PROV_FAB_BK_H_ID", Value =pMC_PROV_FAB_BK_H_ID},
                         new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                     }, sp);
                    foreach (DataRow drRd2 in dsRd2.Tables[0].Rows)
                    {
                        SMP_REQ_D2_GMT_PARTVm obRd2 = new SMP_REQ_D2_GMT_PARTVm();

                        //obRd2.MC_PROV_FAB_BK_D_ID = (drRd2["MC_PROV_FAB_BK_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(drRd2["MC_PROV_FAB_BK_D_ID"]);
                        obRd2.MC_STYLE_D_FAB_ID = (drRd2["MC_STYLE_D_FAB_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(drRd2["MC_STYLE_D_FAB_ID"]);
                        obRd2.RF_GARM_PART_LST = (drRd2["RF_GARM_PART_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(drRd2["RF_GARM_PART_LST"]);
                        obRd2.FIN_DIA = (drRd2["FIN_DIA"] == DBNull.Value) ? string.Empty : Convert.ToString(drRd2["FIN_DIA"]);
                        obRd2.FIN_GSM = (drRd2["FIN_GSM"] == DBNull.Value) ? string.Empty : Convert.ToString(drRd2["FIN_GSM"]);

                        if (drRd2["DIA_MOU_ID"] != DBNull.Value)
                        { obRd2.DIA_MOU_ID = (drRd2["DIA_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(drRd2["DIA_MOU_ID"]); }
                        else { obRd2.DIA_MOU_ID = 23; }

                        if (drRd2["LK_DIA_TYPE_ID"] != DBNull.Value)
                        { obRd2.LK_DIA_TYPE_ID = (drRd2["LK_DIA_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(drRd2["LK_DIA_TYPE_ID"]); }
                        else { obRd2.LK_DIA_TYPE_ID = 327; }

                        if (drRd2["RF_FIB_COMP_ID"] != DBNull.Value)
                        { obRd2.RF_FIB_COMP_ID = (drRd2["RF_FIB_COMP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(drRd2["RF_FIB_COMP_ID"]); }

                        if (drRd2["RF_FAB_TYPE_ID"] != DBNull.Value)
                        { obRd2.RF_FAB_TYPE_ID = (drRd2["RF_FAB_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(drRd2["RF_FAB_TYPE_ID"]); }

                        obRd2.FIN_DIA_DESC = (drRd2["FIN_DIA_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(drRd2["FIN_DIA_DESC"]);
                        obRd2.FAB_TYPE_DESC = (drRd2["FAB_TYPE_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(drRd2["FAB_TYPE_DESC"]);
                        obRd2.RF_GARM_PART_NAME_LST = (drRd2["RF_GARM_PART_NAME_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(drRd2["RF_GARM_PART_NAME_LST"]);

                        if (obRd2.RF_GARM_PART_LST != "")
                        { obRd2.RF_GARM_PART_ID_LST = obRd2.RF_GARM_PART_LST.Split(','); }
                        else
                        {
                            obRd2.RF_GARM_PART_ID_LST = new string[] { "1" };
                        }

                        var obQtyList = new List<MC_SMP_REQ_D2Model>();
                        var dsQty = db.ExecuteStoredProcedure(new List<CommandParameter>()
                        {
                             new CommandParameter() {ParameterName = "pOption", Value = 3005},
                             new CommandParameter() {ParameterName = "pMC_ORDER_H_ID", Value = pMC_ORDER_H_ID},
                             new CommandParameter() {ParameterName = "pMC_COLOR_ID", Value = ob.MC_COLOR_ID},
                             new CommandParameter() {ParameterName = "pMC_PROV_FAB_BK_D_ID", Value = obRd2.MC_PROV_FAB_BK_D_ID},
                             new CommandParameter() {ParameterName = "pMC_STYLE_D_FAB_ID", Value = obRd2.MC_STYLE_D_FAB_ID},
                             new CommandParameter() {ParameterName = "pMC_PROV_FAB_BK_H_ID", Value = pMC_PROV_FAB_BK_H_ID},
                             new CommandParameter() {ParameterName = "pFIN_DIA", Value = obRd2.FIN_DIA},
                             
                             new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                         }, sp);

                        if (dsQty.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow drQty in dsQty.Tables[0].Rows)
                            {
                                MC_SMP_REQ_D2Model obQty = new MC_SMP_REQ_D2Model();
                                obQty.MC_PROV_FAB_BK_D_ID = (drQty["MC_PROV_FAB_BK_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(drQty["MC_PROV_FAB_BK_D_ID"]);
                                if (drQty["QTY_MOU_ID"] != DBNull.Value)
                                { obQty.QTY_MOU_ID = (drQty["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(drQty["QTY_MOU_ID"]); }


                                if (drQty["RF_FIB_COMP_ID"] != DBNull.Value)
                                { obQty.RF_FIB_COMP_ID = (drQty["RF_FIB_COMP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(drQty["RF_FIB_COMP_ID"]); }

                                if (drQty["RF_FAB_TYPE_ID"] != DBNull.Value)
                                { obQty.RF_FAB_TYPE_ID = (drQty["RF_FAB_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(drQty["RF_FAB_TYPE_ID"]); }

                                obQty.QTY_MOU_LIST = (List<RF_MOUModel>)obMouList;

                                obQty.RQD_QTY = (drQty["RQD_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(drQty["RQD_QTY"]);
                                obQtyList.Add(obQty);
                            }
                        }
                        else
                        {
                            MC_SMP_REQ_D2Model obQty = new MC_SMP_REQ_D2Model();
                            obQty.MC_SMP_REQ_D2_ID = 0;
                            obQty.QTY_MOU_ID = 3;
                            //obQty.RQD_QTY = 0;
                            obQtyList.Add(obQty);
                        }

                        obRd2.GMT_PARTS_QTY = obQtyList;
                        obRd2List.Add(obRd2);
                    }
                    ob.GMT_PARTS = (List<SMP_REQ_D2_GMT_PARTVm>)obRd2List;
                    ob.STYLE_COLORS_LIST = (List<MC_COLORModel>)colorList;

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

    public class SMP_REQ_D2_GMT_PARTVm
    {
        public string FIN_DIA { get; set; }
        public Int64? DIA_MOU_ID { get; set; }
        public Int64? LK_DIA_TYPE_ID { get; set; }
        public string RF_GARM_PART_LST { get; set; }
        public string FIN_DIA_DESC { get; set; }
        public string RF_GARM_PART_NAME_LST { get; set; }
        public string FAB_TYPE_DESC { get; set; }
        public string[] RF_GARM_PART_ID_LST { get; set; }
        public List<MC_SMP_REQ_D2Model> GMT_PARTS_QTY { get; set; }

        public long MC_STYLE_D_FAB_ID { get; set; }

        public long MC_SMP_REQ_STYL_ID { get; set; }

        public long MC_PROV_FAB_BK_D_ID { get; set; }

        public Int64 RF_FIB_COMP_ID { get; set; }
        public Int64 RF_FAB_TYPE_ID { get; set; }

        public string FIN_GSM { get; set; }
    }
}