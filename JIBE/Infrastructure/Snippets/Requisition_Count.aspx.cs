using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.Infrastructure;
using SMS.Business.PURC;
using System.Data;

public partial class Infrastructure_Snippets_Requisition_Count : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Requisition_Count();
    }

    protected void Requisition_Count()
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

            Table tbl_Reqsn_Count = new Table();
            tbl_Reqsn_Count.CssClass = "tbl-css-dash";
            tbl_Reqsn_Count.CellPadding = 0;
            tbl_Reqsn_Count.CellSpacing = 1;


            foreach (DataRow drflt in FleetDT.Rows)
            {

                // get the fleets based on vessels assigned to loged in user
                DataRow[] drUserFleet = dtUserFleet.Select("fleetcode='" + drflt["code"].ToString() + "'");
                if (drUserFleet.Length > 0)
                {

                    TableRow tr_Reqsn_Count_HD_Fleet = new TableRow();
                    TableCell cl_Reqsn_Count_HD_Fleet = new TableCell();
                    TableRow tr_Reqsn_Count_HD_FormType = new TableRow();
                    Label lblHD = new Label();
                    TableRow tr_Reqsn_Count = new TableRow();
                    tr_Reqsn_Count.CssClass = "td-css-dash";
                    cl_Reqsn_Count_HD_Fleet.CssClass = "cell-HD-css";

                    lblHD.Text = drflt["Name"].ToString();

                    cl_Reqsn_Count_HD_Fleet.Controls.Add(lblHD);
                    tr_Reqsn_Count_HD_Fleet.Controls.Add(cl_Reqsn_Count_HD_Fleet);
                    tbl_Reqsn_Count.Controls.Add(tr_Reqsn_Count_HD_Fleet);


                    foreach (DataRow drDep in DeptDt.Rows)
                    {
                        if (drDep["Short_Code"].ToString() != "ALL")
                        {
                            DataTable dtReqsnCount = BLL_Infra_DashBoard.Get_Rreqsn_Count(drDep["Short_Code"].ToString(), UDFLib.ConvertToInteger(drflt["code"].ToString()), Session["USERID"].ToString()).Tables[0];

                            TableCell cl_Reqsn_Count = new TableCell();
                            TableCell cl_Reqsn_Count_HD_FormType = new TableCell();
                            cl_Reqsn_Count_HD_FormType.Text = drDep["Description"].ToString();
                            cl_Reqsn_Count.CssClass = "td-css-dash";
                            cl_Reqsn_Count_HD_FormType.CssClass = "cell-HD-css";

                            GridView gvReqsnCount = new GridView();
                            gvReqsnCount.ID = "gvReqsnCount" + drflt["code"].ToString() + drDep["Short_Code"].ToString();
                            if (dtReqsnCount.Rows.Count > 0)
                            {
                                gvReqsnCount.DataSource = dtReqsnCount;
                            }
                            else
                            {
                                gvReqsnCount.DataSource = dtEmptyTable;
                            }
                            gvReqsnCount.DataBind();

                            gvReqsnCount.EmptyDataText = "No record found !";
                            gvReqsnCount.AutoGenerateColumns = true;
                            gvReqsnCount.RowStyle.CssClass = "RowStyle-css-dash";
                            gvReqsnCount.AlternatingRowStyle.CssClass = "AlternatingRowStyle-css-dash";
                            gvReqsnCount.HeaderStyle.CssClass = "HeaderStyle-css-dash";

                            tr_Reqsn_Count_HD_FormType.Controls.Add(cl_Reqsn_Count_HD_FormType);
                            cl_Reqsn_Count.Controls.Add(gvReqsnCount);
                            tr_Reqsn_Count.Controls.Add(cl_Reqsn_Count);
                        }
                    }
                    tbl_Reqsn_Count.Controls.Add(tr_Reqsn_Count_HD_FormType);
                    tbl_Reqsn_Count.Controls.Add(tr_Reqsn_Count);
                }
            }

            phReqsnCount.Controls.Add(tbl_Reqsn_Count);


        }
    }
}