using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.PortageBill;
using System.Data;

public partial class Infrastructure_AllotmentFlag_MailSetting : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindItems();
        }
        lblmsg.Text = "";
    }

    protected void btnSearch_Click(object s, EventArgs e)
    {
        BindItems();
    }

    protected void BindItems()
    {
        gvUserList.DataSource = BLL_PB_PortageBill.Allotment_Flag_Mail_Users(UDFLib.ConvertStringToNull(txtUsername.Text));
        gvUserList.DataBind();
    }


    protected void btnSave_Click(object s, EventArgs e)
    {
        DataTable dtList = new DataTable();
        dtList.Columns.Add("PID");
        dtList.Columns.Add("value");


        foreach (GridViewRow gr in gvUserList.Rows)
        {
            DataRow dr = dtList.NewRow();
            dr["PID"] = Convert.ToInt32(gvUserList.DataKeys[gr.RowIndex].Values["USERID"]);
            dr["value"] = ((gr.FindControl("chkUser") as CheckBox).Checked) == true ? "1" : "0";

            dtList.Rows.Add(dr);
        }

        if (BLL_PB_PortageBill.UPD_Allotment_Flag_Mail_Users(dtList, Convert.ToInt32(Session["userid"])) > 0)
        {
            lblmsg.Text = "Saved successfully";
        }
        


    }
}