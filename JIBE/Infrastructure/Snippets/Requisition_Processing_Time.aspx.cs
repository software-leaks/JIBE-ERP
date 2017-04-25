using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.Infrastructure;
using SMS.Business.PURC;
using System.Data;

public partial class Infrastructure_Snippets_Requisition_Processing_Time : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Requisition_Processing_Time();
    }

    protected void Requisition_Processing_Time()
    {
        BLL_Infra_VesselLib objVsl = new BLL_Infra_VesselLib();
        BLL_PURC_Purchase objTechService = new BLL_PURC_Purchase();
        BLL_Infra_UserCredentials objUserFlt = new BLL_Infra_UserCredentials();
        DataTable dtEmptyTable = new DataTable();

        DataTable FleetDT = objVsl.GetFleetList(Convert.ToInt32(Session["USERCOMPANYID"].ToString()));
        DataTable DeptDt = objTechService.GetDeptType();

        dtEmptyTable.Columns.Add("Message");
        DataRow drEmpty = dtEmptyTable.NewRow();
        drEmpty[0] = "No record found !";
        dtEmptyTable.Rows.Add(drEmpty);

        DataTable dtUserFleet = objUserFlt.Get_Fleet_By_UserID(Convert.ToInt32(Session["USERID"].ToString()));

        if (dtUserFleet.Rows.Count > 0)
        {

            Table tbl_Reqsn_Time = new Table();
            tbl_Reqsn_Time.CssClass = "tbl-css-dash";
            tbl_Reqsn_Time.CellPadding = 0;
            tbl_Reqsn_Time.CellSpacing = 1;


            foreach (DataRow drflt in FleetDT.Rows)
            {

                // get the fleets based on vessels assigned to loged in user
                DataRow[] drUserFleet = dtUserFleet.Select("fleetcode='" + drflt["code"].ToString() + "'");
                if (drUserFleet.Length > 0)
                {

                    TableRow tr_Reqsn_Time_HD_Fleet = new TableRow();
                    TableCell cl_Reqsn_Time_HD_Fleet = new TableCell();
                    TableRow tr_Reqsn_Time_HD_FormType = new TableRow();
                    Label lblHD = new Label();
                    TableRow tr_Reqsn_Time = new TableRow();
                    tr_Reqsn_Time.CssClass = "td-css-dash";
                    cl_Reqsn_Time_HD_Fleet.CssClass = "cell-HD-css";

                    lblHD.Text = drflt["Name"].ToString();

                    cl_Reqsn_Time_HD_Fleet.Controls.Add(lblHD);
                    tr_Reqsn_Time_HD_Fleet.Controls.Add(cl_Reqsn_Time_HD_Fleet);
                    tbl_Reqsn_Time.Controls.Add(tr_Reqsn_Time_HD_Fleet);


                    foreach (DataRow drDep in DeptDt.Rows)
                    {
                        if (drDep["Short_Code"].ToString() != "ALL")
                        {
                            DataTable dtReqsnCount = BLL_Infra_DashBoard.Get_Reqsn_Processing_Time(drDep["Short_Code"].ToString(), UDFLib.ConvertToInteger(drflt["code"].ToString()), Convert.ToInt32(Session["USERID"].ToString()));

                            TableCell cl_Reqsn_Time = new TableCell();
                            TableCell cl_Reqsn_Time_HD_FormType = new TableCell();
                            cl_Reqsn_Time_HD_FormType.Text = drDep["Description"].ToString();
                            cl_Reqsn_Time.CssClass = "td-css-dash";
                            cl_Reqsn_Time_HD_FormType.CssClass = "cell-HD-css";

                            GridView gvReqsnTime = new GridView();
                            gvReqsnTime.ID = "gvReqsnTime" + drflt["code"].ToString() + drDep["Short_Code"].ToString();
                            if (dtReqsnCount.Rows.Count > 0)
                            {
                                gvReqsnTime.DataSource = dtReqsnCount;
                            }
                            else
                            {
                                gvReqsnTime.DataSource = dtEmptyTable;
                            }
                            gvReqsnTime.DataBind();

                            gvReqsnTime.EmptyDataText = "No record found !";
                            gvReqsnTime.AutoGenerateColumns = true;
                            gvReqsnTime.RowStyle.CssClass = "RowStyle-css-dash";
                            gvReqsnTime.AlternatingRowStyle.CssClass = "AlternatingRowStyle-css-dash";
                            gvReqsnTime.HeaderStyle.CssClass = "HeaderStyle-css-dash";

                            tr_Reqsn_Time_HD_FormType.Controls.Add(cl_Reqsn_Time_HD_FormType);
                            cl_Reqsn_Time.Controls.Add(gvReqsnTime);
                            tr_Reqsn_Time.Controls.Add(cl_Reqsn_Time);
                        }
                    }
                    tbl_Reqsn_Time.Controls.Add(tr_Reqsn_Time_HD_FormType);
                    tbl_Reqsn_Time.Controls.Add(tr_Reqsn_Time);
                }
            }
            phReqsnProcessingTime.Controls.Add(tbl_Reqsn_Time);

        }
    }
}