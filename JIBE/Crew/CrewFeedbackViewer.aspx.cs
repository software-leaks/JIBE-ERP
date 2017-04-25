using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using SMS.Business.PURC;
using SMS.Business.Infrastructure;
using System.Data;
using System.Text;
using SMS.Business.Crew;
using System.IO;
public partial class CrewFeedbackViewer : System.Web.UI.Page
{

    public SMS.Properties.UserAccess objUA = new SMS.Properties.UserAccess();
    public BLL_Crew_CrewDetails objBLLCrew = new BLL_Crew_CrewDetails();
    public BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
    BLL_Infra_UploadFileSize objUploadFilesize = new BLL_Infra_UploadFileSize();
    public string TodayDateFormat = ""; 
    public Boolean uaEditFlag = false;

    protected void Page_Load(object sender, EventArgs e)
    {

        calFrom.Format = Convert.ToString(Session["User_DateFormat"]);
        calTo.Format = Convert.ToString(Session["User_DateFormat"]);
          
        TodayDateFormat = UDFLib.DateFormatMessage();
        UserAccessValidation();

        if (!IsPostBack)
        {
 
            BindFleetDLL();
            DDLFleet.SelectItems(new string[] { Session["USERFLEETID"].ToString() });
            BindVesselDDL();

            DDLStatus.SelectedValue = "1";

            Load_RankList();
            Load_CountryList();
            Load_ManningAgentList();

            BindUserList();
            BindGrid();

            lblMsg.Text = "";
            txtCrewRemarks.Text = "";
            
        }
        hdf_User_ID.Value = Session["userid"].ToString();
        //string msg1 = String.Format("$('.sailingInfo').SailingInfo();");
        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgshowdetails", msg1, true);   
    }

    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
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

    public void BindFleetDLL()
    {
        try
        {
            BLL_Infra_VesselLib objVsl = new BLL_Infra_VesselLib();

            DataTable FleetDT = objVsl.GetFleetList(Convert.ToInt32(Session["USERCOMPANYID"].ToString()));
            DDLFleet.DataSource = FleetDT;
            DDLFleet.DataTextField = "Name";
            DDLFleet.DataValueField = "code";
            DDLFleet.DataBind();
        }
        catch (Exception ex)
        {

        }
    }

    public void BindVesselDDL()
    {
        try
        {

            BLL_Infra_VesselLib objVsl = new BLL_Infra_VesselLib();
            StringBuilder sbFilterFlt = new StringBuilder();
            string VslFilter = "";
            foreach (DataRow dr in DDLFleet.SelectedValues.Rows)
            {
                sbFilterFlt.Append(dr[0]);
                sbFilterFlt.Append(",");
            }

            DataTable dtVessel = objVsl.Get_VesselList(0, 0, Convert.ToInt32(Session["USERCOMPANYID"].ToString()), "", Convert.ToInt32(Session["USERCOMPANYID"].ToString()));

            if (sbFilterFlt.Length > 1)
            {
                sbFilterFlt.Remove(sbFilterFlt.Length - 1, 1);
                VslFilter = string.Format("fleetCode in (" + sbFilterFlt.ToString() + ")");
                dtVessel.DefaultView.RowFilter = VslFilter;
            }

            DDLVessel.DataSource = dtVessel;
            DDLVessel.DataTextField = "Vessel_name";
            DDLVessel.DataValueField = "Vessel_id";
            DDLVessel.DataBind();
            Session["sVesselCode"] = DDLVessel.SelectedValues;
            Session["sFleet"] = DDLFleet.SelectedValues;

        }
        catch (Exception ex)
        {

        }
    }

    public void Load_RankList()
    {
        BLL_Crew_Admin objCrewAdmin = new BLL_Crew_Admin();
        DataTable dt = objCrewAdmin.Get_RankList();

        DDLRank.DataSource = dt;
        DDLRank.DataTextField = "Rank_Short_Name";
        DDLRank.DataValueField = "ID";
        DDLRank.DataBind();
    }

    private void Load_CountryList()
    {
        try
        {

            BLL_Infra_Country objCountry = new BLL_Infra_Country();

            DDLNationality.DataSource = objCountry.Get_CountryList();
            DDLNationality.DataTextField = "COUNTRY";
            DDLNationality.DataValueField = "ID";
            DDLNationality.DataBind();
      
        }
        catch { }
    }

    public void Load_ManningAgentList()
    {
        try
        {
             
                BLL_Infra_Company objComp = new BLL_Infra_Company();
                DDLManningOffice.DataSource = objBLLCrew.Get_ManningAgentList(UDFLib.ConvertToInteger(Session["USERCOMPANYID"].ToString()));
                DDLManningOffice.DataTextField = "COMPANY_NAME";
                DDLManningOffice.DataValueField = "ID";
                DDLManningOffice.DataBind();
         
        }
        catch { }
    }





    public void BindUserList()
    {
        DataTable dt = objUser.Get_UserList();
        string filter = "User_type ='OFFICE USER' ";

         
        dt.DefaultView.RowFilter = filter;

        DDLCommentedBy.DataSource = dt.DefaultView.ToTable();
        DDLCommentedBy.DataTextField = "UserName";
        DDLCommentedBy.DataValueField = "UserID";
        DDLCommentedBy.DataBind();
 
    }



    protected void DDLFleet_SelectedIndexChanged()
    {
        BindVesselDDL();

        BindGrid();
    }

    public void BindGrid()
    {

        int is_Fetch_Count = ucCustomPager.isCountRecord;

        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());


        gvCrewFeedback.DataSource = objBLLCrew.Get_Crew_FeedBack_Viewer_Search((DataTable)DDLFleet.SelectedValues, (DataTable)DDLVessel.SelectedValues, (DataTable)DDLRank.SelectedValues
            , (DataTable)DDLNationality.SelectedValues, (DataTable)DDLManningOffice.SelectedValues, (DataTable)DDLCommentedBy.SelectedValues
            , UDFLib.ConvertDateToNull(UDFLib.ConvertToDefaultDt(Convert.ToString(txtCommentFromDate.Text))), UDFLib.ConvertDateToNull(UDFLib.ConvertToDefaultDt(Convert.ToString(txtCommentToDate.Text))), txtSearch.Text, UDFLib.ConvertIntegerToNull(DDLStatus.SelectedValue)
            , sortbycoloumn, sortdirection, ucCustomPager.CurrentPageIndex, ucCustomPager.PageSize, ref is_Fetch_Count);
        gvCrewFeedback.DataBind();

        ucCustomPager.CountTotalRec = is_Fetch_Count.ToString();
        ucCustomPager.BuildPager();

    }



    protected void Load_CrewVoyages(int CrewID)
    {
        DataTable dt = objBLLCrew.Get_CrewVoyages(CrewID);
        ddlVoyages.DataSource = dt;
        ddlVoyages.DataBind();

        updDDlVoyages.Update();
    }



    protected void OnAddComment(object source, CommandEventArgs e)
    {
        txtCrewRemarks.Text = "";lblMsg.Text = "";
        UpdatePanel1.Update();        

        Load_CrewVoyages(Convert.ToInt32(e.CommandArgument.ToString()));
        ViewState["CrewID"] = e.CommandArgument.ToString();

        string AddPort = String.Format("showModal('dvPopupAddFeedback',false);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddPort", AddPort, true);
    }

    protected void btnSearchPO_Click(object sender, EventArgs e)
    {
        if (txtCommentFromDate.Text != "")
        {
            if (!UDFLib.DateCheck(txtCommentFromDate.Text))
            {
                string js = "alert('Enter Valid From Date"+TodayDateFormat+"');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", js, true);
                return;
            }
        }

        if (txtCommentToDate.Text != "")
        {
            if (!UDFLib.DateCheck(txtCommentToDate.Text))
            {
                string js = "alert('Enter Valid To Date" + TodayDateFormat + "');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", js, true);
                return;
            }
        }


        BindGrid();
    }

    protected void btnClearFilter_Click(object sender, EventArgs e)
    {

        DDLFleet.ClearSelection();
        DDLVessel.ClearSelection();

        DDLRank.ClearSelection();
        DDLNationality.ClearSelection();
        DDLManningOffice.ClearSelection();
        DDLCommentedBy.ClearSelection();


        DDLStatus.SelectedValue = "1";

        txtSearch.Text = "";
        txtCommentFromDate.Text = "";
        txtCommentToDate.Text = "";

        BindFleetDLL();
        BindVesselDDL();

        Load_RankList();
        Load_CountryList();
        Load_ManningAgentList();

        BindUserList();
     
        BindGrid();

    }


    protected void ImgExpExcel_Click(object sender, EventArgs e)
    {

        int is_Fetch_Count = ucCustomPager.isCountRecord;

        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());

        DataTable dt = objBLLCrew.Get_Crew_FeedBack_Viewer_Search((DataTable)DDLFleet.SelectedValues, (DataTable)DDLVessel.SelectedValues, (DataTable)DDLRank.SelectedValues
            , (DataTable)DDLNationality.SelectedValues, (DataTable)DDLManningOffice.SelectedValues, (DataTable)DDLCommentedBy.SelectedValues
            , UDFLib.ConvertDateToNull(txtCommentFromDate.Text), UDFLib.ConvertDateToNull(txtCommentToDate.Text), txtSearch.Text, UDFLib.ConvertIntegerToNull(DDLStatus.SelectedValue)
            , sortbycoloumn, sortdirection, null, null, ref is_Fetch_Count);

        gvCrewFeedback.DataSource = dt;
        gvCrewFeedback.DataBind();


        ChangeColumnDataType(dt, "Comment_Date", typeof(string));

        foreach (DataRow item in dt.Rows)
        {
            if (!string.IsNullOrEmpty(item["Comment_Date"].ToString()))
                item["Comment_Date"] ="&nbsp;" + UDFLib.ConvertUserDateFormat(Convert.ToString(item["Comment_Date"]), UDFLib.GetDateFormat());
        }
      
        string[] HeaderCaptions = { "Staff Code", "Name", "Rank", "Comment Date", "Comment By", "Comment Vessel", "Comment" };
        string[] DataColumnsName = { "Staff_Code", "Staff_Name", "Rank_Name", "Comment_Date", "Comment_By", "Comment_Vessel", "Remark" };

        GridViewExportUtil.ShowExcel(dt, HeaderCaptions, DataColumnsName, "CrewFeedbackViewer", "Crew Feedback Viewer", "");

    }

    public static bool ChangeColumnDataType(DataTable table, string columnname, Type newtype)
    {
        if (table.Columns.Contains(columnname) == false)
            return false;

        DataColumn column = table.Columns[columnname];
        if (column.DataType == newtype)
            return true;

        try
        {
            DataColumn newcolumn = new DataColumn("temporary", newtype);
            table.Columns.Add(newcolumn);
            foreach (DataRow row in table.Rows)
            {
                try
                {
                    row["temporary"] = Convert.ChangeType(row[columnname], newtype);
                }
                catch
                {
                }
            }
            table.Columns.Remove(columnname);
            newcolumn.ColumnName = columnname;
        }
        catch (Exception)
        {
            return false;
        }

        return true;
    }

    protected void DDLVessel_SelectedIndexChanged()
    {
        BindGrid();
    }

    protected void DDLNationality_SelectedIndexChanged()
    {
        BindGrid();
    }

    protected void DDLCommentedBy_SelectedIndexChanged()
    {
        BindGrid();
    }

    protected void DDLRank_SelectedIndexChanged()
    {
        BindGrid();
    }

    protected void DDLManningOffice_SelectedIndexChanged()
    {
        BindGrid();
    }



    protected void btnSaveRemarks_Click(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        try
        {
            if (txtCrewRemarks.Text != "")
            {
                string FileName = "";
                DataTable dt = new DataTable();
                dt = objUploadFilesize.Get_Module_FileUpload("CWF_");
                if (dt.Rows.Count > 0)
                {
                    string datasize = dt.Rows[0]["Size_KB"].ToString();
                    if (CrewRemarks_FileUploader.HasFile)
                    {
                        if (CrewRemarks_FileUploader.PostedFile.ContentLength < Int32.Parse(dt.Rows[0]["Size_KB"].ToString()) * 1024)
                        {
                            FileName = Path.GetFileName(CrewRemarks_FileUploader.FileName);
                            string strPath = MapPath("~/Uploads/CrewDocuments/") + FileName;
                            CrewRemarks_FileUploader.SaveAs(strPath);
                        }
                        else
                        {
                            lblMsg.Text = datasize + " KB File size exceeds maximum limit";
                            string AddPort1 = String.Format("showModal('dvPopupAddFeedback',false);");
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddPort", AddPort1, true);
                            return;
                        }
                    }
                    lblMsg.Text = "";
                    int VoyageID = UDFLib.ConvertToInteger(ddlVoyages.SelectedValue);
                    objBLLCrew.INS_CrewRemarks(UDFLib.ConvertToInteger(ViewState["CrewID"].ToString()), txtCrewRemarks.Text, FileName, GetSessionUserID(), VoyageID);
                    lblMsg.Text = "Feedback Saved.";
                    txtCrewRemarks.Text = "";
                    BindGrid();
                }
                else
                {
                    string js2 = "alert('Upload size not set!');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert9", js2, true);
                }
            }
            else

                lblMsg.Text = "Please enter your Feedback and Save";
                string AddPort = String.Format("showModal('dvPopupAddFeedback',false);");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddPort", AddPort, true);
           }
        catch (Exception ex)
        {

        }
    }
    protected void btnSaveAndCloseRemarks_Click(object sender, EventArgs e)
    {
        btnSaveRemarks_Click(null, null);
        BindGrid();

        string js = "parent.hideModal('dvPopupAddFeedback');";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", js, true);

    }


    protected void gvCrewFeedback_Sorting(object sender, GridViewSortEventArgs se)
    {
        ViewState["SORTBYCOLOUMN"] = se.SortExpression;

        if (ViewState["SORTDIRECTION"] != null && ViewState["SORTDIRECTION"].ToString() == "0")
            ViewState["SORTDIRECTION"] = 1;
        else
            ViewState["SORTDIRECTION"] = 0;

        BindGrid();
    }
    protected void gvCrewFeedback_RowDataBound(object sender, GridViewRowEventArgs e)
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
    }

}