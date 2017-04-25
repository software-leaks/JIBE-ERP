using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.Infrastructure;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Collections;
using System.Text;
using SMS.Business.QMS;
using SMS.Properties;
using System.Data;

public partial class QMS_FBM_FBM_Main_Report : System.Web.UI.Page
{

    BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
    UserAccess objUA = new UserAccess();

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            ViewState["ID"] = null;
            ViewState["SORTDIRECTION"] = null;
            ViewState["SORTBYCOLOUMN"] = null;

            ViewState["ISDEPTSARCHONSENT"] = "N";

            ucCustomPagerItems.PageSize = 20;
            Get_UserDetails();

            ViewState["DeptID"] = "0";
            FillDDLOfficeDept();
            FillDDLPrimaryCategory();

            GetCountFBMOnCurrenUserDepartment();
            BindFBMReport();

            FillDDLYear();


        }

        UserAccessValidation();

    }



    protected void UserAccessValidation()
    {

        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

        objUA = objUser.Get_UserAccessForPage(Convert.ToInt32(Session["USERID"]), PageURL);

        if (objUA.View == 0)
        {

            Response.Redirect("~/crew/default.aspx?msgid=1");
        }

        if (objUA.Add == 0)
        {
            ImgFBMNewMsg.Enabled = false;

        }

    }




    public void FillDDLYear()
    {

        try
        {

            DataTable dtyears = BLL_FBM_Report.GetFBMYears();
            ddlYear.Items.Clear();
            ddlYear.DataSource = dtyears;
            ddlYear.DataTextField = "DATE_SENT";
            ddlYear.DataValueField = "DATE_SENT";
            ddlYear.DataBind();
            ListItem li = new ListItem("--SELECT ALL--", "0");
            ddlYear.Items.Insert(0, li);

        }
        catch (Exception ex)
        {


        }


    }

    public void Get_UserDetails()
    {
        BLL_Infra_UserCredentials ojbInfra = new BLL_Infra_UserCredentials();
        DataTable dtuser = ojbInfra.Get_UserDetails(Convert.ToInt32(Session["userid"].ToString()));

        ViewState["DeptID"] = dtuser.Rows[0]["Dep_Code"].ToString();
        ViewState["CurrDeptID"] = dtuser.Rows[0]["Dep_Code"].ToString();
        ViewState["USERTYPE"] = dtuser.Rows[0]["User_Type"].ToString();
    }

    public void GetCountFBMOnCurrenUserDepartment()
    {
        /*  On launching the FBM Module, if current user's having the FBM in draft then only Draft option would select
              Otherwise Sent option will select automatically. */

        int? deptid = null;
        if (ViewState["DeptID"] != null)
            deptid = Convert.ToInt32(ViewState["DeptID"].ToString());

        DataTable dt = BLL_FBM_Report.GetCountFBMOnCurrenUserDepartment(deptid);

        if (ViewState["USERTYPE"].ToString().ToUpper() == "ADMIN")
        {
            optMsgType.SelectedValue = "PENDINGAPPROVAL";
            DDLOfficeDept.SelectedValue = ViewState["DeptID"].ToString();
        }
        else
        {
            if (dt.Rows.Count > 0)
            {
                optMsgType.SelectedValue = "DRAFT";
                DDLOfficeDept.SelectedValue = ViewState["DeptID"].ToString();
            }
            else
            {
                optMsgType.SelectedValue = "SENT";
                DDLOfficeDept.SelectedValue = "0";
                ViewState["DeptID"] = "0";
            }
        }


    }

    public void BindFBMReport()
    {
        int rowcount = ucCustomPagerItems.isCountRecord;

        string deptid = (ViewState["DeptID"] == null || ViewState["DeptID"].ToString() == "0") ? null : (ViewState["DeptID"].ToString());



        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());


        DataSet ds = BLL_FBM_Report.FBMMessageSearch(UDFLib.ConvertIntegerToNull(Session["userid"].ToString()), null
            , UDFLib.ConvertIntegerToNull(deptid), optForUser.SelectedValue.ToString().Trim()
            , Convert.ToInt32(optMsgStatus.SelectedValue.ToString())
            , UDFLib.ConvertIntegerToNull(DDLPrimaryCategory.SelectedValue), UDFLib.ConvertIntegerToNull(DDLSecondryCategory.SelectedValue)
            , UDFLib.ConvertStringToNull(ddlYear.SelectedValue), UDFLib.ConvertDateToNull(txtFromDate.Text)
            , UDFLib.ConvertDateToNull(txtToDate.Text), txtSearch.Text, ViewState["ISDEPTSARCHONSENT"].ToString(), optMsgType.SelectedValue.ToString()
            , sortbycoloumn, sortdirection, ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref  rowcount);


        if (ucCustomPagerItems.isCountRecord == 1)
        {
            ucCustomPagerItems.CountTotalRec = rowcount.ToString();
            ucCustomPagerItems.BuildPager();
        }

        if (ds.Tables[0].Rows.Count > 0)
        {
            gvFBMReport.DataSource = ds.Tables[0];
            gvFBMReport.DataBind();

            if (ViewState["ID"] == null)
            {
                ViewState["ID"] = ds.Tables[0].Rows[0]["ID"].ToString();
                //gvFBMReport.SelectedIndex = 0;
            }

            // SetRowSelection();
        }
        else
        {
            gvFBMReport.DataSource = ds.Tables[0];
            gvFBMReport.DataBind();
        }
    }

    public void FillDDLOfficeDept()
    {
        try
        {

            int? companyid = null;
            if ((Session["USERCOMPANYID"].ToString() != "") || (Session["USERCOMPANYID"] == null))
                companyid = Convert.ToInt32(Session["USERCOMPANYID"].ToString());

            DataTable dtOfficeDept = BLL_FBM_Report.GetOfficeDepartment(companyid);
            DDLOfficeDept.Items.Clear();
            DDLOfficeDept.DataSource = dtOfficeDept;
            DDLOfficeDept.DataTextField = "VALUE";
            DDLOfficeDept.DataValueField = "ID";
            DDLOfficeDept.DataBind();
            ListItem li = new ListItem("--SELECT ALL--", "0");
            DDLOfficeDept.Items.Insert(0, li);



            FillDDLPrimaryCategory();



        }
        catch (Exception ex)
        {

        }

    }

    public void FillDDLPrimaryCategory()
    {
        try
        {
            int? deptid = null;
            if (DDLOfficeDept.SelectedValue != "0")
                deptid = Convert.ToInt32(DDLOfficeDept.SelectedValue);

            DataTable dtPrimaryCategory = BLL_FBM_Report.FBMGetSystemParameterList("1", "", deptid);
            DDLPrimaryCategory.Items.Clear();
            DDLPrimaryCategory.DataSource = dtPrimaryCategory;
            DDLPrimaryCategory.DataTextField = "NAME";
            DDLPrimaryCategory.DataValueField = "CODE";
            DDLPrimaryCategory.DataBind();
            ListItem li = new ListItem("--SELECT ALL--", "0");
            DDLPrimaryCategory.Items.Insert(0, li);
        }
        catch (Exception ex)
        {
        }

    }

    protected void DDLOfficeDept_SelectedIndexChanged(object sender, EventArgs e)
    {

        FillDDLPrimaryCategory();


        if (DDLOfficeDept.SelectedValue != "0")
            ViewState["DeptID"] = DDLOfficeDept.SelectedValue;
        else
            ViewState["DeptID"] = "0";





        BindFBMReport();
        UpdPnlGrid.Update();
    }

    protected void DDLPrimaryCategory_SelectedIndexChanged(object sender, EventArgs e)
    {


        DataTable dtSecondryCategory = BLL_FBM_Report.FBMGetSystemParameterList(DDLPrimaryCategory.SelectedValue.ToString(), "", null);
        DDLSecondryCategory.Items.Clear();
        DDLSecondryCategory.DataSource = dtSecondryCategory;
        DDLSecondryCategory.DataTextField = "NAME";
        DDLSecondryCategory.DataValueField = "CODE";
        DDLSecondryCategory.DataBind();
        ListItem li = new ListItem("--SELECT ALL--", "0");
        DDLSecondryCategory.Items.Insert(0, li);

        BindFBMReport();
        UpdPnlGrid.Update();
    }

    protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
    {

        BindFBMReport();
        UpdPnlGrid.Update();
    }

    protected void DDLSecondryCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindFBMReport();
        UpdPnlGrid.Update();
    }

    protected void gvFBMReport_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            e.Row.Attributes["onmouseover"] = "this.style.cursor='hand';this.style.color='blue';";
            e.Row.Attributes["onmouseout"] = "this.style.textDecoration='none';this.style.color='black';";

            // e.Row.Attributes["onmouseout"] = "";
            //e.Row.Attributes["onclick"] = ClientScript.GetPostBackEventReference(this.gvFBMReport, "Select$" + e.Row.RowIndex);
        }


      
    }

    protected void gvFBMReport_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblBody = (Label)e.Row.FindControl("lblBody");
            ImageButton ImgMsgBody = (ImageButton)e.Row.FindControl("ImgMsgBody");
            ImgMsgBody.Attributes.Add("Title", "cssbody=[dvbdy1] cssheader=[dvhdr1] header=none body=[" + lblBody.Text + "]");


            ImageButton ImgFBMAtt = (ImageButton)e.Row.FindControl("ImgFBMAtt");
            Label lblAttachment = (Label)e.Row.FindControl("lblAttachment");

            Label lblFBMID = (Label)e.Row.FindControl("lblFBMID");

            Label lblUserID = (Label)e.Row.FindControl("lblUserID");

            Label lblFilePathIfSingle = (Label)e.Row.FindControl("lblFilePathIfSingle");


            if (lblFilePathIfSingle.Text != "")
            {
                ImgFBMAtt.Attributes.Add("onclick", "DocOpen('" + lblFilePathIfSingle.Text + "'); return false;");
            }
            else
            {
                ImgFBMAtt.Attributes.Add("onclick", "javascript:window.open('../FBM/FBM_Main_Report_Details.aspx?FBMID=" + lblFBMID.Text.Trim() + "&UserID=" + lblUserID.Text + "'); return false;");
            }

            if (lblAttachment.Text == "N")
            {
                ImgFBMAtt.Visible = false;
            }

            Label lblPriority = (Label)e.Row.FindControl("lblPriority");
            ImageButton ImgPriority = (ImageButton)e.Row.FindControl("ImgPriority");


            if (lblPriority.Text == "1")
            {
                ImgPriority.Visible = true;
            }

            for (int ix = 1; ix < e.Row.Cells.Count - 3; ix++)
            {
                e.Row.Cells[ix].Attributes.Add("Onclick", "javascript:window.open('../FBM/FBM_Main_Report_Details.aspx?FBMID=" + lblFBMID.Text.Trim() + "&UserID=" + lblUserID.Text + "'); return false;");
                e.Row.Cells[ix].Attributes.Add("onMouseOver", "this.style.cursor='hand';");
                e.Row.Cells[7].Attributes.Add("Onclick", "javascript:window.open('../FBM/FBM_Main_Report_Details.aspx?FBMID=" + lblFBMID.Text.Trim() + "&UserID=" + lblUserID.Text + "'); return false;");
            }

        }

        if (e.Row.RowType == DataControlRowType.Header)
        {

            e.Row.Attributes["onmouseover"] = "this.style.cursor='default';";

            if (ViewState["SORTBYCOLOUMN"] != null)
            {
                HtmlImage img = (HtmlImage)e.Row.FindControl(ViewState["SORTBYCOLOUMN"].ToString());
                if (img != null)
                {
                    if (ViewState["SORTDIRECTION"] == null || ViewState["SORTDIRECTION"].ToString() == "0")
                        img.Src = "../../purchase/Image/arrowUp.png";

                    else
                        img.Src = "../../purchase/Image/arrowDown.png";

                    img.Visible = true;
                }
            }
        }

    }

    protected void btnClearFilter_Click(object sender, EventArgs e)
    {

        ViewState["SORTDIRECTION"] = null;
        ViewState["SORTBYCOLOUMN"] = null;

        DDLOfficeDept.SelectedValue = "0";


        txtFromDate.Text = "";
        txtToDate.Text = "";

        BindFBMReport();
        UpdPnlGrid.Update();
    }

    protected void gvFBMReport_Sorting(object sender, GridViewSortEventArgs se)
    {
        ViewState["SORTBYCOLOUMN"] = se.SortExpression;

        if (ViewState["SORTDIRECTION"] != null && ViewState["SORTDIRECTION"].ToString() == "0")
            ViewState["SORTDIRECTION"] = 1;
        else
            ViewState["SORTDIRECTION"] = 0;

        BindFBMReport();
        SetRowSelection();

    }

    private void SetRowSelection()
    {
        gvFBMReport.SelectedIndex = -1;
        for (int i = 0; i < gvFBMReport.Rows.Count; i++)
        {
            if (gvFBMReport.DataKeys[i].Value.ToString().Equals(ViewState["ID"].ToString()))
            {
                gvFBMReport.SelectedIndex = i;
            }
        }
    }

    protected void gvFBMReport_SelectedIndexChanging(object sender, GridViewSelectEventArgs se)
    {

        Label lblFBMID = (Label)gvFBMReport.Rows[se.NewSelectedIndex].FindControl("lblFBMID");
        Label lblActive = (Label)gvFBMReport.Rows[se.NewSelectedIndex].FindControl("lblActive");
        Label lblUserID = (Label)gvFBMReport.Rows[se.NewSelectedIndex].FindControl("lblUserID");


        ViewState["ID"] = ((Label)gvFBMReport.Rows[se.NewSelectedIndex].FindControl("lblFBMID")).Text;


        ResponseHelper.Redirect("../FBM/FBM_Main_Report_Details.aspx?FBMID=" + lblFBMID.Text.Trim() + "&UserID=" + lblUserID.Text, "Blank", "");

    }


    protected void btnRetrieve_Click(object sender, EventArgs e)
    {
        BindFBMReport();
        UpdPnlGrid.Update();
    }

    protected void optMsgType_SelectedIndexChanged(object sender, EventArgs e)
    {

        if ((optMsgType.SelectedValue == "SENT"))
        {
            DDLOfficeDept.SelectedValue = "0";
            ViewState["DeptID"] = "0";

        }
        else if ((optMsgType.SelectedValue == "PENDINGAPPROVAL"))
        {
            DDLOfficeDept.SelectedValue = "0";
            ViewState["DeptID"] = "0";
        }
        else
        {
            DDLOfficeDept.SelectedValue = ViewState["CurrDeptID"].ToString();
            ViewState["DeptID"] = ViewState["CurrDeptID"].ToString();
        }

        FillDDLPrimaryCategory();

        BindFBMReport();
        UpdPnlGrid.Update();
    }

    protected void optMsgStatus_SelectedIndexChanged(object sender, EventArgs e)
    {



        BindFBMReport();
        UpdPnlGrid.Update();
    }
    protected void optForUser_SelectedIndexChanged(object sender, EventArgs e)
    {

        BindFBMReport();
        UpdPnlGrid.Update();
    }
    protected void gvFBMReport_SelectedIndexChanged(object sender, EventArgs e)
    {

        // Get the currently selected row using the SelectedRow property.
        GridViewRow row = gvFBMReport.SelectedRow;

        string str = "You selected " + row.Cells[2].Text + ".";
        string str1 = "You selected " + row.Cells[2].Text + ".";



    }
}