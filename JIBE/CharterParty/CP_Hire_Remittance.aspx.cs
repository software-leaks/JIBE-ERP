using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.CP;
using System.Data;
using SMS.Business.Infrastructure;
using System.Drawing;

public partial class CharterParty_CP_Hire_Remittance : System.Web.UI.Page
{
    BLL_CP_CharterParty oCP = new BLL_CP_CharterParty();
    BLL_Infra_VesselLib objBLL = new BLL_Infra_VesselLib();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            getVesselList();
            getChartererCode();
            getStatus();
            bindSupplier();
        }
    }

    protected void getVesselList()
    {
        DataTable dt = objBLL.Get_VesselList(0, 0, 0, "", Convert.ToInt32(Session["USERCOMPANYID"].ToString()));

        ddlVessel.DataSource = dt;
        ddlVessel.DataTextField = "VESSEL_NAME";
        ddlVessel.DataValueField = "VESSEL_ID";
        ddlVessel.DataBind();
        ddlVessel.Items.Insert(0, new ListItem("-All-", "0"));
        dt.Dispose();

    }
    protected void getChartererCode()
    {
        DataTable dtable = oCP.Get_Vessel_List().Tables[0];
        ddlCharterer.DataSource = dtable;
        ddlCharterer.DataTextField = "Supplier_Name";
        ddlCharterer.DataValueField = "Charterer_Code";
        ddlCharterer.DataBind();
        ddlCharterer.Items.Insert(0, new ListItem("-All-", "0"));
        dtable.Dispose();
    }

    protected void getStatus()
    {
        DataTable dtable = oCP.Get_Vessel_List().Tables[1];
        ddlCPStatus.DataSource = dtable;
        ddlCPStatus.DataTextField = "Variable_name";
        ddlCPStatus.DataValueField = "Variable_Code";
        ddlCPStatus.DataBind();
        ddlCPStatus.Items.Insert(0, new ListItem("-All-", "0"));
        dtable.Dispose();
    }

    protected void btnFilter_Click(object sender, EventArgs e)
    {
        divAdd.Visible = false;
        divGridRemarks.Visible = false;
      

            DataTable dtable = oCP.Get_Charter_Details_List(ddlVessel.SelectedValue, ddlCharterer.SelectedValue, ddlCPStatus.SelectedValue).Tables[0];
            gvSupplier.DataSource = dtable;
            gvSupplier.DataBind();
        
    }


    protected void bindSupplier()
    {
        DataTable dtable = oCP.Get_Charter_Details_List("0","0","0").Tables[0];
        gvSupplier.DataSource = dtable;
        gvSupplier.DataBind();
    }
    protected void gvSupplier_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvSupplier.PageIndex = e.NewPageIndex;
        divAdd.Visible = false;
        divGridRemarks.Visible = false;
        if (ddlCharterer.SelectedIndex == 0 && ddlCPStatus.SelectedIndex == 0 && ddlVessel.SelectedIndex == 0)
        {
            bindSupplier();
        }
        else
        {

            DataTable dtable = oCP.Get_Charter_Details_List(ddlVessel.SelectedValue, ddlCharterer.SelectedValue, ddlCPStatus.SelectedValue).Tables[1];
            gvSupplier.DataSource = dtable;
            gvSupplier.DataBind();
        }
    }

    protected void lblCharter_Click(object sender, EventArgs e)
    {
        divAdd.Visible = true;
        divGridRemarks.Visible = true;
        var closeLink = (Control)sender;
        GridViewRow row = (GridViewRow)closeLink.NamingContainer;
        LinkButton lblValue = (LinkButton)row.FindControl("lblCharter");
        Label lblVesselID = (Label)row.FindControl("lblVesselID");
        ViewState["Charter_Id"] = lblValue.Text;
        ViewState["Vessel_Id"] = lblVesselID.Text;
        Label txtAmount = (Label)row.FindControl("lblAmount");
     ViewState["Amount"] = txtAmount.Text;
       // row.BackColor = ColorTranslator.FromHtml("#A1DCF2");
   

        //gridbind

        DataTable dt = oCP.Get_Remittance_Details(lblValue.Text).Tables[0];
        gridRemarks.DataSource = dt;
        gridRemarks.DataBind();


    }
    protected void btnRemittance_Click(object sender, EventArgs e)
    {
        string curr="USD";

        oCP.INS_Charter_Receipts(ViewState["Charter_Id"].ToString(), Convert.ToInt32(ViewState["Vessel_Id"]), UDFLib.ConvertDateToNull(txtReceivedDate.Text), curr, Convert.ToDouble(txtAmount.Text), txtRemarks.Text, Convert.ToInt32(Session["userid"]));
        DataTable dt = oCP.Get_Remittance_Details(ViewState["Charter_Id"].ToString()).Tables[0];
        gridRemarks.DataSource = dt;
        gridRemarks.DataBind();
    }


    protected void gvSupplier_SelectedIndexChanged(object sender, EventArgs e)
    {
        gvSupplier.Rows[gvSupplier.SelectedIndex].BackColor = Color.Yellow;
    }
    protected void gvSupplier_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(gvSupplier, "Select$" + e.Row.RowIndex);
            e.Row.Attributes["style"] = "cursor:pointer";
            e.Row.Attributes.Add("onclick", "javascript:ChangeRowColor('" + e.Row.ClientID + "')");
        }

    }
    protected void gvSupplier_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "select")
        {
            GridViewRow gvr = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);

            foreach (GridViewRow row in gvSupplier.Rows)
            {
                row.BackColor = row.RowIndex.Equals(gvr.RowIndex) ? System.Drawing.Color.AliceBlue : System.Drawing.Color.White;
            }
        }

    }

    protected void gridRemarks_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView _gridView = (GridView)sender;

        _gridView.EditIndex = e.NewEditIndex;
        _gridView.SelectedIndex = e.NewEditIndex;
        DataTable dt = oCP.Get_Remittance_Details(ViewState["Charter_Id"].ToString()).Tables[0];
        gridRemarks.DataSource = dt;
        gridRemarks.DataBind();
    }
    protected void gridRemarks_RowCommand(object sender, GridViewCommandEventArgs e)
    {
       
        try
        {
            GridView _gridView = (GridView)sender;
            int nCurrentRow = Int32.Parse(e.CommandArgument.ToString());
            int id = Convert.ToInt16(_gridView.DataKeys[Convert.ToInt32(e.CommandArgument)].Value.ToString());
            if (e.CommandName.ToUpper().Equals("UPDATE"))
            {
                TextBox lblAmntRcv = (TextBox)_gridView.Rows[nCurrentRow].FindControl("lblAmntRcv");
                TextBox lblRIDate = (TextBox)_gridView.Rows[nCurrentRow].FindControl("lblRIDate");
                TextBox lblRemRemarks = (TextBox)_gridView.Rows[nCurrentRow].FindControl("lblRemRemarks");
                Label lblRemID = (Label)(_gridView.Rows[nCurrentRow].FindControl("lblRemID"));
                Label lblJID = (Label)(_gridView.Rows[nCurrentRow].FindControl("lblJID"));
               

                string seatime = lblAmntRcv.Text;
                string porttime = lblRIDate.Text;
               

               //Update command
                oCP.Update_Remittance_Details(UDFLib.ConvertDateToNull(lblRIDate.Text), Convert.ToDouble(lblAmntRcv.Text), lblRemRemarks.Text, lblRemID.Text);

            }
            if (e.CommandName.ToUpper().Equals("CANCEL"))
            {

                _gridView.EditIndex = -1;
                DataTable dt = oCP.Get_Remittance_Details(ViewState["Charter_Id"].ToString()).Tables[0];
                gridRemarks.DataSource = dt;
                gridRemarks.DataBind();


            }
            DataTable dt1 = oCP.Get_Remittance_Details(ViewState["Charter_Id"].ToString()).Tables[0];
            gridRemarks.DataSource = dt1;
            gridRemarks.DataBind();


        }
        catch (Exception ex)
        {
            string err = ex.ToString();
        }
    }
    protected void gridRemarks_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView _gridView = (GridView)sender;
        _gridView.EditIndex = -1;
        DataTable dt = oCP.Get_Remittance_Details(ViewState["Charter_Id"].ToString()).Tables[0];
        gridRemarks.DataSource = dt;
        gridRemarks.DataBind();
    }
    protected void gridRemarks_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        GridView _gridView = (GridView)sender;
        int nCurrentRow = e.RowIndex;
        _gridView.EditIndex = -1;
        DataTable dt = oCP.Get_Remittance_Details(ViewState["Charter_Id"].ToString()).Tables[0];
        gridRemarks.DataSource = dt;
        gridRemarks.DataBind();
    }
}