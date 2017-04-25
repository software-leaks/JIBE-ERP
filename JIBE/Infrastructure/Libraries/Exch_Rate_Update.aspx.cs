using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.Infrastructure;

public partial class Exch_Rate_Update : System.Web.UI.Page
{
    bool showall = false;
    //int currCodeHis = 0;
    protected void Page_Load(object sender, EventArgs e)
    {


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

    protected void btnupd_Click(object sender, EventArgs e)
    {
        System.Text.StringBuilder xmlexchrate = new System.Text.StringBuilder();
        xmlexchrate.Append("'<exchrate>");

        foreach (GridViewRow gr in GridViewExch.Rows)
        {
            double exchamount = 0;
            if (double.TryParse(((TextBox)gr.Cells[2].FindControl("txtrate")).Text, out exchamount))
            {

            }
            xmlexchrate.Append("<component currcode= \"" + Convert.ToInt32(GridViewExch.DataKeys[gr.RowIndex]["Curr_code"].ToString()) + "\"" + " ");
            xmlexchrate.Append(" Base_curr=\"" + Convert.ToInt32(GridViewExch.DataKeys[gr.RowIndex]["bcurr"].ToString()) + "\"" + " ");
            xmlexchrate.Append(" Exch_rate=\"" + exchamount + "\"" + " ");
            xmlexchrate.Append(" idloop=\"" + gr.RowIndex + "\"" + " />");



        }
        xmlexchrate.Append("</exchrate>'");

        BLL_Infra_ExchangeRate objExch = new BLL_Infra_ExchangeRate();

        try
        {
            objExch.ACC_Ins_Exchrate_XML(xmlexchrate.ToString());
        }
        catch
        {

        }


    }

    protected void btnsave_Click(object sender, EventArgs e)
    {


    }


}
