using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class Exch_Rate : System.Web.UI.Page
{
    bool showall = false;
    int currCodeHis = 0;
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            history.Visible = false;
        }

    }
    protected void edit_click(object sender, EventArgs e)
    {

    }
    protected void btnfilter_Click(object sender, EventArgs e)
    {
        filter_grid(DropDownListfilter.SelectedValue.Trim());
        showall = chkshowall.Checked;
    }
    protected void filter_grid(string fltby)
    {
        if (txtfilter.Text != "")
        {
            ControlParameter pr = new ControlParameter();
            pr.ControlID = "txtfilter";
            pr.DefaultValue = "eas";
            pr.Name = fltby;
            pr.PropertyName = "Text";

            ObjectDataSource1.FilterExpression = fltby + " LIKE '%{0}%'";
            ObjectDataSource1.FilterParameters.Add(pr);
        }
    }
    protected void GridViewExch_Sorting(object sender, GridViewSortEventArgs e)
    {
        if (txtfilter.Text != "")
        {
            filter_grid(DropDownListfilter.SelectedValue.Trim());
        }
        showall = chkshowall.Checked;
    }
    protected void GridViewExch_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

        Session["pageindex"] = GridViewExch.PageIndex;
        if (txtfilter.Text != "")
        {
            filter_grid(DropDownListfilter.SelectedValue.Trim());
        }

    }
    //protected void btnsave_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        using (IeLogService_INFClient obj = new IeLogService_INFClient())
    //        {
    //            int responseid = obj.OCAP_Exch_Rate_INS(Convert.ToInt32(DropDownListcurrency.SelectedValue), float.Parse(txtexchrate.Text), txtdate.Text.Trim(), Convert.ToInt32(DropDownListbase.SelectedValue));

    //            if (responseid != 0)
    //            {
    //                Log.Common.LogForAll objins = new Log.Common.LogForAll();
    //                objins.insertLog(responseid.ToString(), "OCAP", "Exch_Rate.aspx", "insert", Session["username"].ToString(), "Acc_Exch_Rate");

    //            }
    //            obj.Close();
    //        }
    //    }
    //    catch { }
    //    Response.Redirect("../AccessPanel/Exch_Rate.aspx");
    //}
    protected void btnExporttoexcel_Click(object sender, EventArgs e)
    {
        //GridViewExch.AllowPaging = false;
        //ObjectDataSource1.Select();
        //GridViewExch.DataBind();
        //clsExporttoExel objexp = new clsExporttoExel();
        //objexp.PrepareGridViewForExport(GridViewExch);
        //objexp.ExportGridView(GridViewExch, "ECHANGE");



    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
           server control at run time. */
        //EnableEventValidation = false;
    }
    protected void GridViewExch_Sorted(object sender, EventArgs e)
    {
        GridViewExch.PageIndex = Convert.ToInt32(Session["pageindex"]);
    }
    protected void ObjectDataSource1_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        e.InputParameters.Add("showall", showall);
    }
    protected void chkshowall_CheckedChanged(object sender, EventArgs e)
    {
        if (chkshowall.Checked)
        {
            showall = true;
            ObjectDataSource1.Select();
            GridViewExch.DataBind();
        }
        else
        {
            showall = false;
            ObjectDataSource1.Select();
            GridViewExch.DataBind();
        }

    }
    protected void lbtncurcode_Click(object sender, EventArgs e)
    {
        LinkButton btncode = (LinkButton)sender;
        currCodeHis = Convert.ToInt32(btncode.CommandArgument);
        history.Visible = true;
        lblcurrhis.Text = "Update history for :" + btncode.Text; ;
        ObjectDataSourceHistory.Select();
        GridViewExchHistory.DataBind();


    }
    protected void ObjectDataSourceHistory_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        e.InputParameters.Add("currCode", currCodeHis);
    }
    protected void btlclose_Click(object sender, EventArgs e)
    {
        history.Visible = false;
    }
    protected void GridViewExchHistory_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }
}
