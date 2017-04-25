using System;

using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.Infrastructure;

public partial class Infrastructure_DashBoard_SnippetAccess : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ViewState["ADMIN"] = null;
        UserAccessValidation();
        lblmsg.Text = "";
        if (!IsPostBack)
        {
            try
            {
                BLL_Infra_UserCredentials obj = new BLL_Infra_UserCredentials();
                if (Convert.ToInt32(ViewState["ADMIN"].ToString()) == 1)
                    lstUserList.DataSource = obj.Get_User_By_Manager(0);
                else
                    lstUserList.DataSource = obj.Get_User_By_Manager(Convert.ToInt32(Session["userid"].ToString()));
                lstUserList.DataBind();
            }
            catch (Exception ex)
            {
                lblmsg.Text = ex.Message;
            }
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


        }
        if (objUA.Approve == 0)
        {

        }
        if (objUA.Delete == 0)
        {


        }
        if (objUA.Admin == 1)
        {
            ViewState["ADMIN"] = 1;
        }
        else
        {
            ViewState["ADMIN"] = 0;
        }


    }
    protected void lstUserList_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            gvSnippets.DataSource = BLL_Infra_DashBoard.Get_Snippet_Access(Convert.ToInt32(lstUserList.SelectedValue),null);
            gvSnippets.DataBind();
        }
        catch (Exception ex)
        {
            lblmsg.Text = ex.Message;
        }

    }

    protected void btnSaveSnippets_Click(object s, EventArgs e)
    {
        try
        {
            DataTable dtsnippets = new DataTable();
            dtsnippets.Columns.Add("Snippet_ID");
            dtsnippets.Columns.Add("Access");
            foreach (GridViewRow rw in gvSnippets.Rows)
            {
                DataRow dr = dtsnippets.NewRow();
                dr["Snippet_ID"] = ((CheckBox)rw.FindControl("chksnippet")).ToolTip;
                dr["Access"] = ((CheckBox)rw.FindControl("chksnippet")).Checked == true ? 1 : 0;

                dtsnippets.Rows.Add(dr);
            }

            BLL_Infra_DashBoard.UPD_Snippet_Access(Convert.ToInt32(lstUserList.SelectedValue), dtsnippets, Convert.ToInt32(Session["userid"].ToString()));
            lblmsg.Text = "Saved Successfully";
        }
        catch (Exception ex)
        {
            lblmsg.Text = ex.Message;
        }
    }



}