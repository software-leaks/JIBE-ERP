using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.LMS;
using System.Data;
using AjaxControlToolkit4;
using System.IO;
using SMS.Business.FAQ;

public partial class LMS_Training_Item_List : System.Web.UI.Page
{
    public SMS.Properties.UserAccess objUA = new SMS.Properties.UserAccess();
    string msgmodal;
    protected void Page_Load(object sender, EventArgs e)
    {


        if (!IsPostBack)
        {
            FillItemType();
            UserAccessValidation();
            Session["vsAttachmentData"] = null;
            Session["vsAttachmentFileName"] = null;
            BindTrainingItems();
        }

    }

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
            btnNewItem.Visible = false;
        }
        if (objUA.Edit == 0)
        {


        }
        if (objUA.Approve == 0)
        {

        }
        if (objUA.Delete == 0)
        {
            ViewState["del"] = 0;
        }
        else
        {
            ViewState["del"] = 1;
        }
    }




    protected void onDelete(object source, CommandEventArgs e)
    {
        int Result = BLL_LMS_Training.Del_Training_Items(Convert.ToInt32(e.CommandArgument.ToString()), Convert.ToInt32(Session["USERID"]));
        if (Result == 1)
        {
            String msgretv = String.Format("alert('Item deleted successfully.')");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgret6v", msgretv, true);
            BindTrainingItems();
        }
        else
        {
            String msgretv = String.Format("alert('Item can not be deleted because it has been assigned to program.')");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgret6v", msgretv, true);

        }


    }
    


    public void BindTrainingItems()
    {

        int is_Fetch_Count = ucCustomPagerAllStatus.isCountRecord;
        DataTable dt = BLL_LMS_Training.Get_ChapterDetails_List(null, UDFLib.ConvertStringToNull(txtSearchItemName.Text.Trim()), UDFLib.ConvertStringToNull(ddlItemType.SelectedIndex == 0 ? null : ddlItemType.SelectedItem.Text), ucCustomPagerAllStatus.CurrentPageIndex, ucCustomPagerAllStatus.PageSize, ref is_Fetch_Count).Tables[0];
        gvTrainingItems.DataSource = dt;
        gvTrainingItems.DataBind();
        ucCustomPagerAllStatus.CountTotalRec = is_Fetch_Count.ToString();
        ucCustomPagerAllStatus.BuildPager();

    }

    protected void btnSearchItem_Click(object sender, EventArgs e)
    {
        BindTrainingItems();

    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        txtSearchItemName.Text = "";
        ddlItemType.SelectedIndex = 0;
        BindTrainingItems();

    }


    protected void btnNewItem_Click(object sender, EventArgs e)
    {

        String msgretv = String.Format("OpenPopupWindowBtnID('POP__ItemDetails', 'Add New Item','LMS_ItemDetails.aspx', 'popup', 400, 980, null, null, false, false, true, false,'" + btnSearchItem.ClientID + "')");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgret6v", msgretv, true);

    }
    protected void FillItemType()
    {
        ListItem li0 = new ListItem("-- Select --", null);
        ddlItemType.Items.Insert(0, li0);
        ListItem li1 = new ListItem("RESOURCE", "1");
        ddlItemType.Items.Insert(1, li1);
        ListItem li2 = new ListItem("FBM", "2");
        ddlItemType.Items.Insert(2, li2);
        ListItem li3 = new ListItem("ARTICLE", "3");
        ddlItemType.Items.Insert(3, li3);
        ListItem li4 = new ListItem("VIDEO MATERIALS", "4");
        ddlItemType.Items.Insert(4, li4);
        ddlMenuLink.Items.Clear();
        ddlMenuLink.DataTextField = "Menu_Link";
        ddlMenuLink.DataValueField = "Menu_Link";
        ddlMenuLink.DataSource = BLL_FAQ_Item.Get_Menu_Link().Tables[0];
        ddlMenuLink.DataBind();
        ListItem lim = new ListItem("--Select--", "0");
        ddlMenuLink.Items.Insert(0, lim);
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        BLL_LMS_Training.Update_Traing_ITem(Convert.ToInt32(ViewState["ItemID"]), ddlMenuLink.SelectedIndex < 0 ? null : ddlMenuLink.SelectedValue);
        String msgretv = String.Format("hideModal('divUrl');");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgret6v", msgretv, true);
        BindTrainingItems();
    }
}