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
using System.Collections.Generic;
using SMS.Business.Infrastructure;
using SMS.Properties;
using SMS.Business.Crew;

public partial class Crew_Libraries_CrewCardApproval : System.Web.UI.Page
{
    BLL_Crew_CardApproval objBLL = new BLL_Crew_CardApproval();
    BLL_Infra_UserCredentials objBLLUser = new BLL_Infra_UserCredentials();
    UserAccess objUA = new UserAccess();
    
    DataTable dtUserApprovalList = new DataTable();
    public string OperationMode = "";
    public Boolean uaEditFlag = false;
    public Boolean uaDeleteFlage = false;


    protected void Page_Load(object sender, EventArgs e)
    {
        UserAccessValidation();
        if (!IsPostBack)
        {
            BindUserApproval();
        }
    }

    public void BindUserApproval()
    {
        DataTable dt = objBLL.Get_CrewCardApprovalDetails(ddlApprovalType.SelectedValue != "0" ? ddlApprovalType.SelectedValue : null,txtfilter.Text != "" ? txtfilter.Text : null);

        gvUserType.DataSource = dt;
        gvUserType.DataBind();
    }

    protected void UserAccessValidation()
    {
        int CurrentUserID = GetSessionUserID();
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

        BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

        if (objUA.View == 0)
            Response.Redirect("~/default.aspx?msgid=1");

        if (objUA.Add == 0) ImgAdd.Visible = false;
        if (objUA.Edit == 1)
            uaEditFlag = true;
        else
            btnsave.Visible = false;
        if (objUA.Delete == 1) uaDeleteFlage = true;
    }

    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }


    protected void ImgAdd_Click(object sender, EventArgs e)
    {
        try
        {
            HiddenFlag.Value = "Add";

            OperationMode = "Add Approver";
            clearcontrol();
            BindUserApprovalList(null);

            string AddUserTypemodal = String.Format("showModal('divadd',false);");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddUserTypemodal", AddUserTypemodal, true);
        }
        catch
        {
        }
    }
    protected void clearcontrol()
    {
        ddlType.Enabled = true;

        ddlType.SelectedValue = "0";
        chkUserList.DataSource = null;
        chkUserList.DataBind();
    }
    protected void btnsave_Click(object sender, EventArgs e)
    {
        int UserSelectedCount = 0;
        string js = "";
        string ApproverType = ddlType.SelectedValue;

        if (ApproverType == "0")
        {
            js = "alert('Select Approver Type');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "status", js, true);
            string AddUserTypemodal = String.Format("showModal('divadd',false);");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddUserTypemodal", AddUserTypemodal, true);
        }
        else
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("PKID");
            dt.Columns.Add("ID");
            dt.Columns.Add("VALUE");

            foreach (ListItem chkitem in chkUserList.Items)
            {
                DataRow dr = dt.NewRow();
                dr["PKID"] = 0;
                dr["ID"] = chkitem.Value;
                dr["VALUE"] = chkitem.Selected == true ? 1 : 0;
                if (chkitem.Selected == true)
                {
                    UserSelectedCount++;
                    dt.Rows.Add(dr);
                }
            }

            if (UserSelectedCount == 0)
            {
                js = "alert('Select user for approver');";
            }
            else
            {
                objBLL.INS_CardApproval(ApproverType, dt, UDFLib.ConvertToInteger(Session["UserID"].ToString()));

            }
            if (js == "")
            {
                BindUserApproval();
                js = "alert('User updated for Approver Type');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "status", js, true);
                string hidemodal = String.Format("hideModal('divadd')");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "hidemodal", hidemodal, true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "status", js, true);
                string AddUserTypemodal = String.Format("showModal('divadd',false);");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddUserTypemodal", AddUserTypemodal, true);
            }
        }
    }

    protected void onUpdate(object source, CommandEventArgs e)
    {
        HiddenFlag.Value = "Edit";
        OperationMode = HiddenFlag.Value + " Approver";

        string[] arg = e.CommandArgument.ToString().Split(',');

        ddlType.SelectedValue = UDFLib.ConvertStringToNull(arg[0]);
        ddlType.Enabled = false;

        BindUserApprovalList(ddlType.SelectedValue);

        string AddUserTypemodal = String.Format("showModal('divadd',false);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddUserTypemodal", AddUserTypemodal, true);

    }

    protected void onDelete(object source, CommandEventArgs e)
    {
        string[] DelParms = Convert.ToString(e.CommandArgument).Split(',');

        objBLL.Del_CardApprover(UDFLib.ConvertToInteger(DelParms[0]), Convert.ToInt32(Session["USERID"]));
        string js = "User removed from approval rights";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "msgNoFile", "alert('" + js + "');", true);
        BindUserApproval();
    }

    protected void btnFilter_Click(object sender, EventArgs e)
    {
        BindUserApproval();
    }

    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        txtfilter.Text = "";
        txtApprovarID.Text = "";
        BindUserApproval();
    }


    protected void ImgExpExcel_Click(object sender, EventArgs e)
    {
        DataTable dt = objBLL.Get_CrewCardApprovalDetails(ddlApprovalType.SelectedValue != "0" ? ddlApprovalType.SelectedValue : null, txtfilter.Text != "" ? txtfilter.Text : null);

        string[] HeaderCaptions = { "Approver Type", "User Name" };
        string[] DataColumnsName = { "ApproverType", "UserName" };

        GridViewExportUtil.ShowExcel(dt, HeaderCaptions, DataColumnsName, "Card Approver", "Card Approver", "");
    }

    protected void BindUserApprovalList(string ApprovalType)
    {
        try
        {
            dtUserApprovalList = objBLL.Get_CardApprovalUserList(ApprovalType);
            chkUserList.DataSource = dtUserApprovalList;
            chkUserList.DataTextField = "USERNAME";
            chkUserList.DataValueField = "USERID";
            chkUserList.DataBind();

            int i = 0;
            foreach (ListItem chkitem in chkUserList.Items)
            {
                if (dtUserApprovalList.Rows[i]["UserSelected"].ToString() == "1")
                {
                    chkitem.Selected = true;
                }
                i++;
            }

            string AddUserTypemodal = String.Format("showModal('divadd',false);");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddUserTypemodal", AddUserTypemodal, true);
        }
        catch
        {
        }
    }

    protected void ddlType_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        OperationMode =  HiddenFlag.Value + " Approver";
        BindUserApprovalList(ddlType.SelectedValue.ToString());
        string AddUserTypemodal = String.Format("showModal('divadd',false);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddUserTypemodal", AddUserTypemodal, true);
    }   
   
}