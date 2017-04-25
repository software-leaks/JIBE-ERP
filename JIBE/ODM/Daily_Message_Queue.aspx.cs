using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using SMS.Business.Infrastructure;
using SMS.Business.ODM;
using SMS.Properties;
using System.Data;

public partial class ODM_Daily_Message_Queue : System.Web.UI.Page
{
    public string OperationMode = "";
    public Boolean uaEditFlag = false;
    public Boolean uaDeleteFlage = false;
    private long lCurrentRecord = 0;
    private long lRecordsPerRow = 200;
    public UserAccess objUA = new UserAccess();
    BLL_ODM objODM = new BLL_ODM();
    protected void Page_Load(object sender, EventArgs e)
    {
        UserAccessValidation();
        if (!IsPostBack)
        {
            Session["ODM_ID"] = null;
            ViewState["SORTDIRECTION"] = null;
            ViewState["SORTBYCOLOUMN"] = null;

            BindODMQueue();
        }
    }


    protected void UserAccessValidation()
    {
        int CurrentUserID = GetSessionUserID();
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

        BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

        if (objUA.View == 0)
            Response.Redirect("~/default.aspx?msgid=1");

        // if (objUA.Add == 0) ImgAdd.Visible = false;
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


    public void BindODMQueue()
    {

        int rowcount = ucCustomPagerItems.isCountRecord;

        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());

        DataTable dt = objODM.Get_ODM_QueueList(null, sortbycoloumn, sortdirection, ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref  rowcount);


        if (ucCustomPagerItems.isCountRecord == 1)
        {
            ucCustomPagerItems.CountTotalRec = rowcount.ToString();
            ucCustomPagerItems.BuildPager();
        }

        if (dt.Rows.Count > 0)
        {
            gvODM.DataSource = dt;
            gvODM.DataBind();
        }
        else
        {
            gvODM.DataSource = dt;
            gvODM.DataBind();
        }

    }

    protected void gvODM_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.Header)
        {
            if (ViewState["SORTBYCOLOUMN"] != null)
            {
                HtmlImage img = (HtmlImage)e.Row.FindControl(ViewState["SORTBYCOLOUMN"].ToString());
                if (img != null)
                {
                    if (ViewState["SORTDIRECTION"] == null || ViewState["SORTDIRECTION"].ToString() == "0")
                        img.Src = "~/purchase/Image/arrowUp.png";
                    else
                        img.Src = "~/purchase/Image/arrowDown.png";

                    img.Visible = true;
                }
            }
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes["onmouseover"] = "this.style.cursor='hand';this.style.backgroundColor='#FEECEC';";
            e.Row.Attributes["onmouseout"] = "this.style.textDecoration='none';this.style.color='black';this.style.backgroundColor=''";
            GridViewRow gvr = e.Row; 
            Label lblUserList = (Label)e.Row.FindControl("lblUserList");
            DataRow drv = ((DataRowView)e.Row.DataItem).Row;

            string vesselList = drv["VesselList"].ToString();

            if (vesselList != null && vesselList != "")
            {
                if (vesselList.Length > 100)
                {
                    lblUserList.Text = vesselList.Substring(0, 99) + "...";

                }
                else
                    lblUserList.Text = vesselList.ToString();

                lblUserList.ToolTip = vesselList.ToString();
            }
        }
    }

    protected void gvODM_Sorting(object sender, GridViewSortEventArgs se)
    {
        ViewState["SORTBYCOLOUMN"] = se.SortExpression;

        if (ViewState["SORTDIRECTION"] != null && ViewState["SORTDIRECTION"].ToString() == "0")
            ViewState["SORTDIRECTION"] = 1;
        else
            ViewState["SORTDIRECTION"] = 0;

        BindODMQueue();
    }

    protected void onDelete(object source, CommandEventArgs e)
    {
       // int retval = objPortCall.Del_PortCall_Notification(Convert.ToInt32(e.CommandArgument.ToString()), Convert.ToInt32(Session["USERID"]));

        BindODMQueue();
    }

    protected void btnRefresh_Click(object sender, ImageClickEventArgs e)
    {

        BindODMQueue();

    }
    protected void btnBackToVessel_Click(object sender, EventArgs e)
    {
      
    }
    protected void btnHistory_Click(object sender, EventArgs e)
    {
        Response.Redirect("../ODM/ODM_History.aspx");
    }
    protected void btnAttachments_Click(object sender, EventArgs e)
    {
        Response.Redirect("../ODM/Pending_Attachments.aspx");
    }
}