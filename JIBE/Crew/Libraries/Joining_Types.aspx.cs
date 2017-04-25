using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI.HtmlControls;
using SMS.Business.Infrastructure;
using SMS.Properties;
using SMS.Business.Technical;
using SMS.Business.Crew;


public partial class Joining_Types : System.Web.UI.Page
{
    BLL_Crew_Admin objBLL = new BLL_Crew_Admin();

    UserAccess objUA = new UserAccess();

    public string OperationMode = "";

    public Boolean uaEditFlag = false;
    public Boolean uaDeleteFlage = false;


    protected void Page_Load(object sender, EventArgs e)
    {
        UserAccessValidation();
        if (!IsPostBack)
        {
            ViewState["SORTDIRECTION"] = null;
            ViewState["SORTBYCOLOUMN"] = null;

            ucCustomPagerItems.PageSize = 20;

            BindJoiningType();
            BindPermamnentStatus();
        }

    }



    public void BindJoiningType()
    {

        int rowcount = ucCustomPagerItems.isCountRecord;

        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());

        DataTable dt = objBLL.JoiningType_Search(txtfilter.Text != "" ? txtfilter.Text : null, sortbycoloumn, sortdirection
             , ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref  rowcount);


        if (ucCustomPagerItems.isCountRecord == 1)
        {
            ucCustomPagerItems.CountTotalRec = rowcount.ToString();
            ucCustomPagerItems.BuildPager();
        }

        if (dt.Rows.Count > 0)
        {
            gvJoiningType.DataSource = dt;
            gvJoiningType.DataBind();
        }
        else
        {
            gvJoiningType.DataSource = dt;
            gvJoiningType.DataBind();
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

        
        if (objUA.Edit == 1)
            uaEditFlag = true;
        else
            btnsave.Visible = false;

        if (objUA.Add == 0)
        {
            ImgbtnAddBranch.Visible = ImgAdd.Visible = false;
            btnSavePermanent.Visible = btnsave.Visible = false;
        }
        else
            btnSavePermanent.Visible= btnsave.Visible = true;

        if (objUA.Delete == 1) uaDeleteFlage = true;
        else uaDeleteFlage = false;
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
        this.SetFocus("ctl00_MainContent_txtJoiningType");
        HiddenFlag.Value = "Add";
        OperationMode = "Add Joining Type";

        ClearField();

        DataTable dtPBill = new DataTable();
        dtPBill = objBLL.Get_JoiningType_PBillComponent(null);
        GVOffPBill.DataSource = dtPBill;
        GVOffPBill.DataBind();

        string JoiningType = String.Format("showModal('divadd',false);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "JoiningType", JoiningType, true);
    }

    protected void ClearField()
    {
        txtJoiningTypeID.Text = "";
        txtJoiningType.Text = "";
        txtJoiningCode.Text = "";
        chkSeniorityConsidered.Checked = false;
        chkVessselPBill_Considered.Checked = false;
        chkServiceConsidered.Checked = false;
        chkPBillConsidered.Checked = false;
        chkOperatorExp.Checked = false;
        chkWatchKeeping.Checked = false;
    }

    protected void btnsave_Click(object sender, EventArgs e)
    {
        string msg;
        int flag = 0;
        DataTable dtJoining = new DataTable();
        dtJoining = objBLL.Get_JoiningType_List(null);
        dtJoining.DefaultView.RowFilter = HiddenFlag.Value != "Add" ? "Joining_Type= '" + txtJoiningType.Text.Trim() + "' AND ID<> '" + txtJoiningTypeID.Text.Trim() + "'" : "Joining_Type= '" + txtJoiningType.Text.Trim() + "'";
        if (dtJoining.DefaultView.Count > 0)
        {
            msg = String.Format("alert('Joining Type already exists!')");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg, true);
            flag = 1;
            string hidemodal = String.Format("showModal('divadd')");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "hidemodal", hidemodal, true);
        }

        if (flag != 1)
        {
            dtJoining.DefaultView.RowFilter = HiddenFlag.Value != "Add" ? "JCode= '" + txtJoiningCode.Text.Trim() + "' AND ID<> '" + txtJoiningTypeID.Text.Trim() + "'" : "JCode= '" + txtJoiningCode.Text.Trim() + "'";
            if (dtJoining.DefaultView.Count > 0)
            {
                msg = String.Format("alert('Joining code already exists!')");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg, true);
                flag = 1;
                string hidemodal = String.Format("showModal('divadd')");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "hidemodal", hidemodal, true);
            }
            else
            {
                DataTable dt = new DataTable();
                dt.Columns.Add(new DataColumn("JCode", typeof(Int32)));
                DataRow dr;
                if (chkPBillConsidered.Checked == true)
                {
                    foreach (GridViewRow row in GVOffPBill.Rows)
                    {
                        if ((row.Cells[1].FindControl("chkPbillAssigned") as CheckBox).Checked == true)
                        {
                            dr = dt.NewRow();
                            dr["JCode"] = GVOffPBill.DataKeys[row.RowIndex].Value;
                            dt.Rows.Add(dr);
                        }

                    }
                }

                if (HiddenFlag.Value == "Add")
                {
                    int retval = objBLL.InsertJoiningType(txtJoiningType.Text, txtJoiningCode.Text, chkSeniorityConsidered.Checked, chkVessselPBill_Considered.Checked, chkServiceConsidered.Checked, chkPBillConsidered.Checked, dt, Convert.ToInt32(Session["USERID"]), chkOperatorExp.Checked, chkWatchKeeping.Checked);
                }
                else
                {
                    int retval = objBLL.EditJoiningType(Convert.ToInt32(txtJoiningTypeID.Text.Trim()), txtJoiningType.Text, txtJoiningCode.Text, chkSeniorityConsidered.Checked, chkVessselPBill_Considered.Checked, chkServiceConsidered.Checked, chkPBillConsidered.Checked, dt, Convert.ToInt32(Session["USERID"]), chkOperatorExp.Checked, chkWatchKeeping.Checked);
                }

                BindJoiningType();
                string hidemodal = String.Format("hideModal('divadd')");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "hidemodal", hidemodal, true);
            }
        }



    }

    protected void onUpdate(object source, CommandEventArgs e)
    {

        HiddenFlag.Value = "Edit";
        OperationMode = "Edit Joining Type";

        DataTable dt = new DataTable();
        dt = objBLL.Get_JoiningType_List(Convert.ToInt32(e.CommandArgument.ToString()));


        txtJoiningTypeID.Text = dt.Rows[0]["ID"].ToString();
        txtJoiningType.Text = dt.Rows[0]["Joining_Type"].ToString();
        txtJoiningCode.Text = dt.Rows[0]["JCode"].ToString();
        chkSeniorityConsidered.Checked = dt.Rows[0]["SeniorityConsidered"].ToString() == "True" ? true : false;
        chkVessselPBill_Considered.Checked = dt.Rows[0]["VesselPortageBillConsidered"].ToString() == "True" ? true : false;
        chkServiceConsidered.Checked = dt.Rows[0]["ServiceConsidered"].ToString() == "True" ? true : false;
        chkPBillConsidered.Checked = dt.Rows[0]["OfficePortageBillConsidered"].ToString() == "True" ? true : false;
        chkOperatorExp.Checked = dt.Rows[0]["OperatorExpConsidered"].ToString() == "True" ? true : false;
        chkWatchKeeping.Checked = dt.Rows[0]["WatchKeepingConsidered"].ToString() == "True" ? true : false;



        DataTable dtPBill = new DataTable();
        dtPBill = objBLL.Get_JoiningType_PBillComponent(Convert.ToInt32(e.CommandArgument.ToString()));
        GVOffPBill.DataSource = dtPBill;
        GVOffPBill.DataBind();

        string InfoDiv = "Get_Record_Information_Details('CRW_LIB_JOININGTYPES','ID=" + txtJoiningTypeID.Text + "')";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "InfoDiv", InfoDiv, true);


        string AddSingoffReasonmodal = String.Format("showModal('divadd',false);" + (chkPBillConsidered.Checked == true ? "$('[id$=dvPBill]').toggle();" : ""));
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddSingoffReasonmodal", AddSingoffReasonmodal, true);

    }

    protected void onDelete(object source, CommandEventArgs e)
    {

        int retval = objBLL.DeleteJoiningType(Convert.ToInt32(e.CommandArgument.ToString()), Convert.ToInt32(Session["USERID"]));

        BindJoiningType();

    }

    protected void btnFilter_Click(object sender, EventArgs e)
    {
        BindJoiningType();
    }

    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        txtfilter.Text = "";
        BindJoiningType();
    }

    protected void ImgExpExcel_Click(object sender, EventArgs e)
    {
        int rowcount = ucCustomPagerItems.isCountRecord;

        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());

        DataTable dt = objBLL.JoiningType_Search(txtfilter.Text != "" ? txtfilter.Text : null, sortbycoloumn, sortdirection
            , null, null, ref  rowcount);



        string[] HeaderCaptions = { "Joining Type", "Joining Code", "Seniority Considered", "Vessel PBill Considered", "Office PBill Considered", "Service Considered", "Operator Experience Considered", "Watch Keeping Considered" };
        string[] DataColumnsName = { "Joining_Type", "JCode", "SeniorityConsidered", "VesselPortageBillConsidered", "OfficePortageBillConsidered", "ServiceConsidered", "OperatorExpConsidered", "WatchKeepingConsidered" };

        GridViewExportUtil.ShowExcel(dt, HeaderCaptions, DataColumnsName, "JoiningType", "Joining Type", "");

    }

    protected void gvJoiningType_RowDataBound(object sender, GridViewRowEventArgs e)
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

    protected void gvJoiningType_Sorting(object sender, GridViewSortEventArgs se)
    {
        ViewState["SORTBYCOLOUMN"] = se.SortExpression;

        if (ViewState["SORTDIRECTION"] != null && ViewState["SORTDIRECTION"].ToString() == "0")
            ViewState["SORTDIRECTION"] = 1;
        else
            ViewState["SORTDIRECTION"] = 0;

        BindJoiningType();
    }

    protected void BindPermamnentStatus()
    {
        try
        {
            int rowcount = ucCustomPagerPermanent.isCountRecord;
            int Result = 1;
            string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
            int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());

            BLL_Crew_Admin objBLL = new BLL_Crew_Admin();
            DataSet dt = objBLL.CRUD_PermanentStatus("", "R", 0, 0, txtPermanentFilter.Text != "" ? txtPermanentFilter.Text : null, sortbycoloumn, sortdirection
                 , ucCustomPagerPermanent.CurrentPageIndex, ucCustomPagerPermanent.PageSize, ref  rowcount, ref Result);

            if (dt != null)
            {
                if (ucCustomPagerPermanent.isCountRecord == 1)
                {
                    ucCustomPagerPermanent.CountTotalRec = rowcount.ToString();
                    ucCustomPagerPermanent.BuildPager();
                }
                if (dt.Tables[0].Rows.Count > 0)
                {
                    gvPermanentStatus.DataSource = dt.Tables[0];
                    gvPermanentStatus.DataBind();
                }
                else
                {
                    gvPermanentStatus.DataSource = null;
                    gvPermanentStatus.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }
    protected void ImgbtnRefreshBranch_Click(object sender, ImageClickEventArgs e)
    {
        txtPermanentFilter.Text = "";
        BindPermamnentStatus();
    }
    protected void btnFilter_Click(object sender, ImageClickEventArgs e)
    {
        txtPermanent.Text = "";
        BindPermamnentStatus();
    }

    protected void onDeleteStatus(object source, CommandEventArgs e)
    {
        try
        {
            int rowcount = 0; int Result = 1;
            BLL_Crew_Admin objBLL = new BLL_Crew_Admin();
            DataTable dt = objBLL.CRUD_PermanentStatus("", "D", GetSessionUserID(), Convert.ToInt32(e.CommandArgument.ToString()), "", "", null, null, null, ref rowcount, ref Result).Tables[0];
            if (Result > 0)
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ShowMessage", "alert('Record deleted successfully')", true);

            BindPermamnentStatus();
        }
        catch (Exception ex) { UDFLib.WriteExceptionLog(ex); }
    }

    protected void btnSavePermanent_Click(object sender, EventArgs e)
    {
        int Result = 1;
        try
        {
            if (Convert.ToInt32(hdnStatusID.Value) > 0)
            {
                int rowcount = 0;
                BLL_Crew_Admin objBLL = new BLL_Crew_Admin();
                DataTable dt = objBLL.CRUD_PermanentStatus(txtPermanent.Text.Trim(), "U", GetSessionUserID(), Convert.ToInt32(hdnStatusID.Value), "", "", null, null, null, ref rowcount, ref Result).Tables[0];

                ///Result == 2 Already exists
                if (Result < 0)
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AlreadyExists", "alert('The selected status already exist in the system');showModal('divAddStatus', false);", true);
                else if (Result > 0)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AlreadyExists", "alert('" + UDFLib.GetException("SuccessMessage/UpdateMessage") + "');hideModal('divAddStatus');", true);
                    BindPermamnentStatus();
                }
            }
            else
            {
                int rowcount = 0;
                BLL_Crew_Admin objBLL = new BLL_Crew_Admin();
                DataTable dt = objBLL.CRUD_PermanentStatus(txtPermanent.Text.Trim(), "I", GetSessionUserID(), 0, "", "", null, null, null, ref rowcount, ref Result).Tables[0];

                ///Result == 2 Already exists
                if (Result < 0)
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AlreadyExists", "alert('The selected status already exist in the system');showModal('divAddStatus', false);", true);
                else if (Result > 0)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AlreadyExists", "alert('" + UDFLib.GetException("SuccessMessage/SaveMessage") + "');hideModal('divAddStatus');", true);
                    BindPermamnentStatus();
                }
            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }
    protected void btnFilterJoining_Click(object sender, ImageClickEventArgs e)
    {
        BindJoiningType();
    }
}