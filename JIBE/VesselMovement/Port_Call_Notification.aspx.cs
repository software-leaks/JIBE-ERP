using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using SMS.Business.Infrastructure;
using SMS.Business.VM;
using SMS.Properties;
using System.Data;

public partial class VesselMovement_Port_Call_Notification : System.Web.UI.Page
{
    public string OperationMode = "";
    public Boolean uaEditFlag = false;
    public Boolean uaDeleteFlag = false;
    public Boolean uaAddFlag = false;
    private long lCurrentRecord = 0;
    private long lRecordsPerRow = 200;
    public UserAccess objUA = new UserAccess();
    BLL_VM_PortCall objPortCall = new BLL_VM_PortCall();
    MergeGridviewHeader_Info objContractList = new MergeGridviewHeader_Info();

    protected void Page_Load(object sender, EventArgs e)
    {
        UserAccessValidation();
        if (!IsPostBack)
        {
            Session["Notification_ID"] = null;
            ViewState["SORTDIRECTION"] = null;
            ViewState["SORTBYCOLOUMN"] = null;

            BindNotificationList();

            //objContractList.AddMergedColumns(new int[] { 2, 3 }, "Arrival Period", "");
        }
    }

    protected void gvNotification_RowCreated(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                MergeGridviewHeader.SetProperty(objContractList);

                e.Row.SetRenderMethodDelegate(MergeGridviewHeader.RenderHeader);
                ViewState["DynamicHeaderCSS"] = "PMSGridItemStyle-css";
            }

        }
        catch { }
    }

    protected void UserAccessValidation()
    {
        int CurrentUserID = GetSessionUserID();
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

        BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

        if (objUA.View == 0)
            Response.Redirect("~/default.aspx?msgid=1");

        if (objUA.Add == 1)
        {
            uaAddFlag = true;
            ImgAdd.Visible = true;
        }
        if (objUA.Edit == 1)
            uaEditFlag = true;
        if (objUA.Delete == 1) uaDeleteFlag = true;

    }
    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }


    public void BindNotificationList()
    {

        int rowcount = ucCustomPagerItems.isCountRecord;

        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());

        DataTable dt = objPortCall.Get_PortCall_Notification_List(null, sortbycoloumn, sortdirection, ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref  rowcount);


        if (ucCustomPagerItems.isCountRecord == 1)
        {
            ucCustomPagerItems.CountTotalRec = rowcount.ToString();
            ucCustomPagerItems.BuildPager();
        }

        if (dt.Rows.Count > 0)
        {
            gvNotification.DataSource = dt;
            gvNotification.DataBind();
        }
        else
        {
            gvNotification.DataSource = dt;
            gvNotification.DataBind();
        }

    }

    protected void gvNotification_RowDataBound(object sender, GridViewRowEventArgs e)
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
        }
    }

    protected void gvNotification_Sorting(object sender, GridViewSortEventArgs se)
    {
        ViewState["SORTBYCOLOUMN"] = se.SortExpression;

        if (ViewState["SORTDIRECTION"] != null && ViewState["SORTDIRECTION"].ToString() == "0")
            ViewState["SORTDIRECTION"] = 1;
        else
            ViewState["SORTDIRECTION"] = 0;

        BindNotificationList();
    }

    protected void onDelete(object source, CommandEventArgs e)
    {
        int retval = objPortCall.Del_PortCall_Notification(Convert.ToInt32(e.CommandArgument.ToString()), Convert.ToInt32(Session["USERID"]));

        BindNotificationList();
    }

    protected void onView(object source, CommandEventArgs e)
    {
        Session["NotificationID"] = e.CommandArgument.ToString();
        LoadNotificationReport();
    }

    protected void btnRefresh_Click(object sender, ImageClickEventArgs e)
    {

        BindNotificationList();

    }
    protected void btnBackToVessel_Click(object sender, EventArgs e)
    {
        Response.Redirect("../VesselMovement/PortCallVessel.aspx");
    }
    protected void gvNotificationReport_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvNotificationReport.PageIndex = e.NewPageIndex;
        LoadNotificationReport();
    }
    /// <summary>
    /// Description: Method is used to load list of port call notification for a partcular
    /// </summary>
    public void LoadNotificationReport()
    {
        if (Session["NotificationID"] != null)
        {
            DataTable dt = objPortCall.Get_Port_Call_Notification_Report(Session["NotificationID"].ToString());
            gvNotificationReport.DataSource = dt;
            gvNotificationReport.DataBind();
            gvNotificationReport.Visible = true;

        }

    }

}