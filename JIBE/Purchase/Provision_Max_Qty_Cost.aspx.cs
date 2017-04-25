using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.Infrastructure;
using System.Data;
using SMS.Business.PURC;
using System.Text;
using System.Collections;


public partial class Purchase_Provision_Max_Qty_Cost : System.Web.UI.Page
{
    int lastFixedColumnID = 3;

    public override void VerifyRenderingInServerForm(Control control)
    {
        //Empty Method
    }

    protected void Page_Load(object sender, EventArgs e)
    {

        UserAccessValidation();

        lblErrorMessage.Text = "";
        lblmsg.Text = "";

        if (!IsPostBack)
        {
            try
            {
                BindSubcatalogue();
                BLL_Infra_VesselLib objVSL = new BLL_Infra_VesselLib();
                DataTable dtVessel = objVSL.Get_VesselList(0, 0, Convert.ToInt32(Session["USERCOMPANYID"].ToString()), "", Convert.ToInt32(Session["USERCOMPANYID"].ToString()));
                chkVesselList.DataSource = dtVessel;
                chkVesselList.DataBind();

                SelectSavedVessels();

                BindSplittedItems();

                BLL_Infra_VesselLib objVsl = new BLL_Infra_VesselLib();

                DataTable FleetDT = objVsl.GetFleetList(Convert.ToInt32(Session["USERCOMPANYID"].ToString()));
                DataRow drselect = FleetDT.NewRow();
                drselect["code"] = "0";
                drselect["Name"] = "-- All Fleet --";

                FleetDT.Rows.InsertAt(drselect, 0);
                ddlFleet.DataSource = FleetDT;
                ddlFleet.DataTextField = "Name";
                ddlFleet.DataValueField = "code";
                ddlFleet.DataBind();
            }
            catch (Exception ex)
            {
                lblmsg.Text = ex.Message;
            }

        }

        if ((chkVesselList.Items.Cast<ListItem>().Count(li => li.Selected)) == 1)
        {
            btnAssign.Enabled = true;
            btnViewAssignment.Enabled = true;
        }
        else
        {
            btnAssign.Enabled = false;
            btnViewAssignment.Enabled = false;
        }


    }

    protected void BindSubcatalogue()
    {
        using (BLL_PURC_Purchase objTechService = new BLL_PURC_Purchase())
        {
            DataTable dtSubSystem = new DataTable();
            dtSubSystem = objTechService.SelectSubCatalogs();
            dtSubSystem.DefaultView.RowFilter = "System_Code ='PROVI'";

            ddlSubCatalogue.DataTextField = "subsystem_description";
            ddlSubCatalogue.DataValueField = "subsystem_code";
            DataTable dtsub = dtSubSystem.DefaultView.ToTable();
            DataRow drselect = dtsub.NewRow();
            drselect["subsystem_code"] = "0";
            drselect["subsystem_description"] = "-- All --";
            dtsub.Rows.InsertAt(drselect, 0);

            ddlSubCatalogue.DataSource = dtsub;
            ddlSubCatalogue.DataBind();

        }
    }

    public SMS.Properties.UserAccess objUA = new SMS.Properties.UserAccess();

    protected void UserAccessValidation()
    {
        int CurrentUserID = Convert.ToInt32(Session["userid"]);
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());
        SMS.Business.Infrastructure.BLL_Infra_UserCredentials objUser = new SMS.Business.Infrastructure.BLL_Infra_UserCredentials();

        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

        if (objUA.View == 0)
            Response.Redirect("~/default.aspx?msgid=1");

        if (objUA.Add == 0)
        {


        }
        if (objUA.Edit == 0)
        {

            btnViewAssignment.Visible = false;
        }
        if (objUA.Approve == 0)
        {

        }
        if (objUA.Delete == 0)
        {


        }


    }

    protected void btnSplitItems_Click(object s, EventArgs e)
    {
        try
        {
            ViewState["info"] = null;
            DataTable dtVessls = (DataTable)ViewState["vessellist"];
            dtVessls.Rows.Clear(); ;

            foreach (ListItem item in chkVesselList.Items)
            {
                if (item.Selected == true)
                {
                    DataRow dtRow = dtVessls.Rows.Find(item.Value);
                    if (dtRow == null)
                    {
                        dtRow = dtVessls.NewRow();
                        dtRow[0] = item.Value;
                        dtRow[1] = item.Text;
                        dtVessls.Rows.Add(dtRow);

                    }
                }

            }
            ViewState["vessellist"] = dtVessls;
            BindSplittedItems();

        }
        catch (Exception ex)
        {
            lblmsg.Text = ex.Message;
        }


    }

    protected void BindSplittedItems()
    {
        ViewState["info"] = null;


        int? maxqty = chkMaxQty.Checked ? UDFLib.ConvertIntegerToNull(1) : null;
        DataSet dsitems = BLL_PURC_Common.Get_Provisions_Approval_Limit((DataTable)ViewState["vessellist"], CustomPager.CurrentPageIndex, CustomPager.PageSize, UDFLib.ConvertStringToNull(ddlSubCatalogue.SelectedValue), UDFLib.ConvertStringToNull(txtSearchItems.Text), maxqty);
        if (dsitems.Tables.Count > 0)
        {
            dsitems.Tables[0].Columns.RemoveAt(0);
            gvItemsSplit.DataSource = dsitems.Tables[0];
            gvItemsSplit.DataBind();

            CustomPager.CountTotalRec = dsitems.Tables[2].Rows[0][0].ToString();
            CustomPager.BuildPager();

            dsitems.Tables[1].PrimaryKey = new DataColumn[] { dsitems.Tables[1].Columns["vessel_id"] };
            ViewState["vessellist"] = dsitems.Tables[1];
        }

    }

    public void SelectSavedVessels()
    {
        DataTable dtVessls = (DataTable)ViewState["vessellist"];
        if (dtVessls == null)
        {
            dtVessls = new DataTable();
            dtVessls.Columns.Add("VESSEL_ID");
            dtVessls.Columns.Add("VESSEL_SHORTNAME");
            dtVessls.PrimaryKey = new DataColumn[] { dtVessls.Columns["VESSEL_ID"] };
        }


        dtVessls.Rows.Clear();
        chkVesselList.ClearSelection();
        DataTable dtSavedVSL = BLL_PURC_Common.Get_Provision_Limit_Vessels();
        foreach (DataRow dr in dtSavedVSL.Rows)
        {
            DataRow dtRow = dtVessls.NewRow();
            dtRow[0] = dr["vessel_id"].ToString();
            dtRow[1] = dr["Vessel_Short_Name"];
            dtVessls.Rows.Add(dtRow);
        }
        ViewState["vessellist"] = dtVessls;
        foreach (ListItem li in chkVesselList.Items)
        {
            foreach (DataRow dr in dtSavedVSL.Rows)
            {
                if (dr["vessel_id"].ToString() == li.Value)
                {
                    chkVesselList.Items.FindByValue(li.Value).Selected = true;
                }
            }

        }

    }

    protected void gvItemsSplit_DataBound(object sender, EventArgs e)
    {
        try
        {
            if (gvItemsSplit.HeaderRow != null)
            {
                gvItemsSplit.HeaderRow.Cells[0].Visible = false;

                GridViewRow gvheader = gvItemsSplit.HeaderRow;

                int hi = 0;
                foreach (TableCell hcell in gvheader.Cells)
                {
                    if (hi > lastFixedColumnID)
                    {
                        if ((hi % 2) == 0)
                        {
                            string headername = hcell.Text.Split('_')[1];
                            info.AddMergedColumns(new int[] { hi, hi + 1 }, headername);
                        }

                        HiddenField hdf = new HiddenField();
                        hdf.ID = "hdf" + hi.ToString();
                        Label lblvsl = new Label();
                        lblvsl.ID = "lblvsl" + hi.ToString();
                        hdf.Value = hcell.Text.Split('_')[2];
                        lblvsl.Text = hcell.Text.Split('_')[0];
                        hcell.Controls.Add(hdf);
                        hcell.Controls.Add(lblvsl);
                    }

                    hi++;
                }
            }




            #region datarow

            foreach (GridViewRow gr in gvItemsSplit.Rows)
            {
                gr.Cells[0].Visible = false;
                gr.Cells[3].HorizontalAlign = HorizontalAlign.Center;
                int icell = 0;
                foreach (TableCell cell in gr.Cells)
                {
                    if (icell > lastFixedColumnID)
                    {
                        Label lbl = new Label();
                        lbl.ID = icell.ToString();
                        lbl.Text = cell.Text;

                        string Vessel_id = (gvItemsSplit.HeaderRow.Cells[icell].FindControl("hdf" + icell.ToString()) as HiddenField).Value;
                        string Item_ref_code = gr.Cells[0].Text;

                        cell.Controls.Add(lbl);
                        cell.Attributes.Add("onclick", "EditCostAmount(" + Vessel_id + "," + Item_ref_code + ",'" + lbl.ClientID + "'," + Session["userid"].ToString() + ",'" + gr.Cells[1].Text.Replace("'", "") + "',event,this)");
                        cell.CssClass = "maxlimit-css-number";

                    }

                    icell++;

                }
            }


            #endregion

        }
        catch (Exception ex)
        {
            lblmsg.Text = ex.Message;
        }
    }
    protected void gvItemsSplit_RowCreated(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.SetRenderMethodDelegate(RenderHeader);

            }
        }
        catch (Exception ex)
        {
            lblmsg.Text = ex.Message;
        }
    }

    private void RenderHeader(HtmlTextWriter output, Control container)
    {

        for (int i = 0; i < container.Controls.Count; i++)
        {
            TableCell cell = (TableCell)container.Controls[i];
            //stretch non merged columns for two rows
            if (!info.MergedColumns.Contains(i))
            {
                cell.Attributes["rowspan"] = "2";


                cell.RenderControl(output);
            }
            else //render merged columns common title
                if (info.StartColumns.Contains(i))
                {


                    output.Write(string.Format("<th align='center' style='background-color:#DFBFDF;border:1px solid #cccccc;border-collapse:collapse;background:url(../Images/gridheaderbg-image.png) left 0px repeat-x'  colspan='{0}'>{1}</th>",
                             info.StartColumns[i], info.Titles[i]));


                }
        }

        //close the first row	
        output.RenderEndTag();
        //set attributes for the second row
        //grid.HeaderStyle.AddAttributesToRender(output);
        //start the second row
        output.RenderBeginTag("tr");



        //render the second row (only the merged columns)
        for (int i = 0; i < info.MergedColumns.Count; i++)
        {
            //if qtn eval 

            TableCell cell = (TableCell)container.Controls[info.MergedColumns[i]];

            cell.CssClass = "HeaderStyle-css-bulkreport";
            cell.RenderControl(output);

        }

        info.MergedColumns.Clear();
        info.StartColumns.Clear();
        info.Titles.Clear();
    }

    [Serializable]
    private class MergedColumnsInfo
    {
        // indexes of merged columns
        public List<int> MergedColumns = new List<int>();
        // key-value pairs: key = the first column index, value = number of the merged columns
        public Hashtable StartColumns = new Hashtable();
        // key-value pairs: key = the first column index, value = common title of the merged columns 
        public Hashtable Titles = new Hashtable();

        //parameters: the merged columns indexes, common title of the merged columns 
        public void AddMergedColumns(int[] columnsIndexes, string title)
        {
            MergedColumns.AddRange(columnsIndexes);
            StartColumns.Add(columnsIndexes[0], columnsIndexes.Length);
            Titles.Add(columnsIndexes[0], title);
        }
    }

    private MergedColumnsInfo info
    {
        get
        {
            if (ViewState["info"] == null)
                ViewState["info"] = new MergedColumnsInfo();
            return (MergedColumnsInfo)ViewState["info"];
        }
    }
    protected void btnExport_Click(object s, EventArgs e)
    {
        gvItemsSplit.GridLines = GridLines.Both;
        ViewState["info"] = null;

        CustomPager.CurrentPageIndex = 1;
        CustomPager.PageSize = 5000;
        BindSplittedItems();


        GridViewExportUtil obj = new GridViewExportUtil();
        GridViewExportUtil.Export("ProvisionsLimit.xls", gvItemsSplit, "");

        gvItemsSplit.GridLines = GridLines.None;
    }
    protected void ddlFleet_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            BLL_Infra_VesselLib objVSL = new BLL_Infra_VesselLib();
            DataTable dtVessel = objVSL.Get_VesselList(UDFLib.ConvertToInteger(ddlFleet.SelectedValue), 0, Convert.ToInt32(Session["USERCOMPANYID"].ToString()), "", Convert.ToInt32(Session["USERCOMPANYID"].ToString()));
            chkVesselList.DataSource = dtVessel;
            chkVesselList.DataBind();

            SelectSavedVessels();
        }
        catch (Exception ex)
        {
            lblmsg.Text = ex.Message;
        }
    }

    protected void btnViewAssignment_Click(object s, EventArgs e)
    {
        try
        {

            btnSplitItems_Click(null, null);

            if ((chkVesselList.Items.Cast<ListItem>().Count(li => li.Selected)) == 1)
            {
                BLL_Infra_VesselLib objVSL = new BLL_Infra_VesselLib();
                DataTable dtVessel = objVSL.Get_VesselList(0, 0, Convert.ToInt32(Session["USERCOMPANYID"].ToString()), "", Convert.ToInt32(Session["USERCOMPANYID"].ToString()));
                dtVessel.PrimaryKey = new DataColumn[] { dtVessel.Columns["Vessel_ID"] };


                dtVessel.Rows.Remove(dtVessel.Rows.Find(chkVesselList.SelectedValue));

                chkNotAssignedVessels.DataSource = dtVessel;
                chkNotAssignedVessels.DataTextField = "Vessel_Short_Name";
                chkNotAssignedVessels.DataValueField = "Vessel_ID";
                chkNotAssignedVessels.DataBind();

                String msgmodal = String.Format("showModal('dvCopyfromVessels',false);");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgonFinalmodal", msgmodal, true);


            }
            else
            {
                

                String msgmodal = String.Format("alert('Please select only one vessel in vessel list');");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgonFinalmodal", msgmodal, true);
            }

        }
        catch (Exception ex)
        {
            lblmsg.Text = ex.Message;
        }
    }

    protected void btnAssign_Click(object s, EventArgs e)
    {
        try
        {

            if ((chkVesselList.Items.Cast<ListItem>().Count(li => li.Selected)) == 1)
            {
                DataTable dtVsl = new DataTable();
                dtVsl.Columns.Add("pid");

                DataRow dr = null;
                foreach (ListItem li in chkNotAssignedVessels.Items)
                {
                    if (li.Selected)
                    {
                        dr = dtVsl.NewRow();
                        dr["pid"] = li.Value;
                        dtVsl.Rows.Add(dr);
                    }
                }

                if (dtVsl.Rows.Count > 0)
                {
                    BLL_PURC_Common.Upd_Copy_Provisions_Approval_Limit(Convert.ToInt32(chkVesselList.SelectedValue), dtVsl, Convert.ToInt32(Session["userid"]));
                    SelectSavedVessels();
                    btnSplitItems_Click(null, null);

                }
                else
                {
                    lblErrorMessage.Text = "Please select vessel to copy !";
                }
            }
            else
            {
                lblErrorMessage.Text = "Please select source vessel !";
            }

        }
        catch (Exception ex)
        {
            lblErrorMessage.Text = ex.Message;
        }

        btnSplitItems_Click(null, null);
        String msgmodal = String.Format("hideModal('dvCopyfromVessels');");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "hidemodal", msgmodal, true);
    }

    protected void chkSelectAll_CheckedChanged(object sender, EventArgs e)
    {
        foreach (ListItem li in chkNotAssignedVessels.Items)
        {
            li.Selected = chkSelectAll.Checked;
        }
    }
    protected void chkVesselListSelectAll_CheckedChanged(object sender, EventArgs e)
    {
        foreach (ListItem li in chkVesselList.Items)
        {
            li.Selected = chkVesselListSelectAll.Checked;
        }
    }




}