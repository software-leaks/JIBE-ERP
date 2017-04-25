using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit4;
using SMS.Business.LMS;
using System.Data;
using System.IO;
using SMS.Business.FAQ;

public partial class LMS_LMS_AttachItem_Details : System.Web.UI.Page
{
    public Boolean blnChapterItem=false;
    public Boolean blnTrainer = false;

    public SMS.Properties.UserAccess objUA = new SMS.Properties.UserAccess();

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
           
            UserAccessValidation();        
            ViewState["FAQ_ID"] = Request.QueryString["FAQ_ID"];
            BindChapterItem();
            
        }
    }

    protected void UserAccessValidation()
    {
        int CurrentUserID = Convert.ToInt32(Session["userid"]);
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());
        SMS.Business.Infrastructure.BLL_Infra_UserCredentials objUser = new SMS.Business.Infrastructure.BLL_Infra_UserCredentials();

        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

        if (objUA.View == 0)
            //Response.Redirect("~/default.aspx?msgid=1");

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

    }

    protected void BindChapterItem()
    {
        string Search_Text = txtSearchItemName.Text;
        DataSet ds = BLL_FAQ_Item.Get_FAQ_Items(Convert.ToInt32(ViewState["FAQ_ID"]), UDFLib.ConvertStringToNull(Search_Text));
        gvItemList.DataSource = ds;
        gvItemList.DataBind();
    }
   
    protected void btnSearchItem_Click(object sender, EventArgs e)
    {
        BindChapterItem();
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        string js = String.Format("parent.ReloadParent_ByButtonID();");
        ScriptManager.RegisterStartupScript(this, this.GetType(), "ReloadParent", js, true);
    }

   
    protected void btnSaveandClose_Click(object sender, EventArgs e)
    {
        DataTable dtFAQ_Items = new DataTable();
        dtFAQ_Items.Columns.Add("ID");
        DataRow dr;
        foreach (GridViewRow row in gvItemList.Rows)
        {
            CheckBox chkProjectAssigned = (row.Cells[0].FindControl("chkSelected") as CheckBox);
            if (chkProjectAssigned.Checked)
            {
                dr = dtFAQ_Items.NewRow();
                dr["ID"] = gvItemList.DataKeys[row.RowIndex].Value.ToString();
                dtFAQ_Items.Rows.Add(dr);
            }
        }
        int rs = BLL_FAQ_Item.Update_FAQ_Items(Convert.ToInt32(ViewState["FAQ_ID"]), dtFAQ_Items, UDFLib.ConvertIntegerToNull(Session["USERID"]));

        string js = String.Format("parent.ReloadParent_ByButtonID();");
        ScriptManager.RegisterStartupScript(this, this.GetType(), "ReloadParent", js, true);

   }

    

}



