using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//using SMS.Business.PURC;
using SMS.Business.Infrastructure;
using SMS.Business.CP;
using System.Data;
using System.Web.UI.HtmlControls;
using SMS.Properties;

public partial class CP_Remarks : System.Web.UI.Page
{
    UserAccess objUA = new UserAccess();
    public int Remarks_ID = 0;
    public int CPID = 0;
    public Boolean uaEditFlag = true;//Test default true
    public Boolean uaDeleteFlage = true;
    BLL_CP_CharterParty objCP = new BLL_CP_CharterParty();
    BLL_Infra_UserCredentials objUserBLL = new BLL_Infra_UserCredentials();
    protected void Page_Load(object sender, EventArgs e)
    {
       // UserAccessValidation();
        if (!IsPostBack)
        {

            BindUser();
            BindRemarks();

        }
    }

    protected void BindUser()
    {
        DataTable dt = objUserBLL.Get_OfficeUser_List();
        ddlUser.DataSource = dt;
        ddlUser.DataTextField = "USERNAME";
        ddlUser.DataValueField = "USERID";
        ddlUser.DataBind();
        ddlUser.Items.Insert(0, new ListItem("-Select-", "0"));
    }


    protected void UserAccessValidation()
    {
        int CurrentUserID = GetSessionUserID();
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

        BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

        if (objUA.View == 0)
            Response.Redirect("~/default.aspx?msgid=1");

        if (objUA.Add == 0)
        {
            btnSave.Enabled = false;
        }
        if (objUA.Edit == 1)
            uaEditFlag = true;
        else
            // btnsave.Visible = false;
            if (objUA.Delete == 1) uaDeleteFlage = true;

    }
    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }

    protected void BindRemarks()
    {
        if (Session["CPID"] != null)
        {
            DataTable dt = objCP.Get_RemarksAll(UDFLib.ConvertIntegerToNull(Session["CPID"]));

            gvRemarks.DataSource = dt;
            gvRemarks.DataBind();
        }
    }

    protected void ClearData()
    {

        txtRemarks.Text = "";
        ddlUser.SelectedValue = "0";
        ltmessage.Text = "";
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        SaveData();
        BindRemarks();
    }
    protected void btnSaveClose_Click(object sender, EventArgs e)
    {
        SaveData();
        this.ClientScript.RegisterClientScriptBlock(this.GetType(), "Close", "window.close();", true);
    }

    protected void SaveData()
    {
        int res = 1;
        try
        {
            CPID = UDFLib.ConvertToInteger(Session["CPID"]);
            if (CPID != 0)
            {
                res = objCP.INS_Remarks(CPID, UDFLib.ConvertIntegerToNull(ddlUser.SelectedValue),
                    txtRemarks.Text, GetSessionUserID());
                ClearData();
                if (res == 1)
                    ltmessage.Text = "Remark added successfully.";
            }

        }
        catch { }
    }

    protected void ibtnMarkRead_Click(object source, CommandEventArgs e)
    {
        DataTable dt = new DataTable();
        try
        {
            Remarks_ID = Convert.ToInt32(e.CommandArgument);
            ViewState["Remarks_Id"] = Remarks_ID;
            objCP.UPD_Remarks(Remarks_ID, "READ", GetSessionUserID());
            BindRemarks();


        }
        catch { }

    }

    protected void ibtnClose_Click(object source, CommandEventArgs e)
    {
        DataTable dt = new DataTable();
        try
        {
            Remarks_ID = Convert.ToInt32(e.CommandArgument);
            ViewState["Remarks_Id"] = Remarks_ID;
            objCP.UPD_Remarks(Remarks_ID, "CLOSE", GetSessionUserID());
            BindRemarks();
        }
        catch { }

    }

    protected void ibtnDelete_Click(object source, CommandEventArgs e)
    {
        DataTable dt = new DataTable();
        ClearData();
        try
        {
            Remarks_ID = Convert.ToInt32(e.CommandArgument);
            objCP.UPD_Remarks(Remarks_ID,"DELETE", GetSessionUserID());
            BindRemarks();
        }
        catch { }

    }



    protected void gvRemarks_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                GridViewRow gvr = e.Row;

                ImageButton ibtnMarkRead = (ImageButton)gvr.FindControl("ibtnMarkRead");
                ImageButton ibtnClose = (ImageButton)gvr.FindControl("ibtnClose");
                ImageButton ibtnDelete = (ImageButton)gvr.FindControl("ibtnDelete");
                int CreatedUserId = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "CreatedUserId"));
                int For_Action_By = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "For_Action_By"));

                string status = DataBinder.Eval(e.Row.DataItem, "Status").ToString();
                if (GetSessionUserID() == For_Action_By)
                    ibtnMarkRead.Visible = true;
                if (GetSessionUserID() == CreatedUserId)
                    ibtnClose.Visible = true;

                if (status.ToUpper() == "CLOSE")
                {
                    ibtnDelete.Visible = false;
                    ibtnMarkRead.Visible = false;
                    ibtnClose.Visible = false;
                    //ibtnClose.Enabled = false;

                }
                if (status.ToUpper() == "READ")
                {
                    ibtnMarkRead.Visible = false;
                    ibtnClose.Visible = true;
                    ibtnDelete.Visible = false;
                }

            }
        }
        catch { }
    }
}
   
