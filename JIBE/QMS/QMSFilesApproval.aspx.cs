using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.Crew;
using SMS.Properties;
using System.Data;
using SMS.Business.Infrastructure;
using SMS.Business.Operations;
using SMS.Business.QMS;
using System.Web.UI.HtmlControls;
using System.IO;



public partial class QMSFilesApproval : System.Web.UI.Page
{
    BLL_QMS_Document objQMS = new BLL_QMS_Document();
    BLL_Infra_Company objCompBLL = new BLL_Infra_Company();

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
        
            BindGrid();
        }

    }

    public void BindGrid()
    {

        int rowcount = ucCustomPagerItems.isCountRecord;
        int ApproverId = 0;
        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());
        if (chkMyApproval.Checked == true)
            ApproverId = Int32.Parse(Session["USERID"].ToString());

        DataTable dt = objQMS.QMS_Files_Approval_Search(txtfilter.Text != "" ? txtfilter.Text : null, Convert.ToInt32(optApprove.SelectedValue), UDFLib.ConvertIntegerToNull(ApproverId)
            , sortbycoloumn, sortdirection
            , ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref  rowcount);


        if (ucCustomPagerItems.isCountRecord == 1)
        {
            ucCustomPagerItems.CountTotalRec = rowcount.ToString();
            ucCustomPagerItems.BuildPager();
        }

        if (dt.Rows.Count > 0)
        {
            gvQMSFile.DataSource = dt;
            gvQMSFile.DataBind();
        }
        else
        {
            gvQMSFile.DataSource = dt;
            gvQMSFile.DataBind();
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
       
        if (objUA.Edit == 1)uaEditFlag = true;
    
        if (objUA.Delete == 1) uaDeleteFlage = true;

        if (objUA.Approve == 1) btnApprove.Enabled = true;

        
    }

    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }

  

    protected void ClearField()
    {
        //txtHoldTankID.Text = "";
        //txtHoldTankName.Text = "";
        //ddlStructureType.SelectedValue = "0";
        //DDLVessel.SelectedValue = "0";
    }

    protected void lblLogFileID_onClick(object sender,EventArgs e)
    { 
    LinkButton btn = (LinkButton)sender;
    GridViewRow row = (GridViewRow)btn.NamingContainer;

    string filePath = gvQMSFile.DataKeys[row.RowIndex].Value.ToString();

    btn.PostBackUrl = "../QMS/" + filePath;
      
        
    }


    protected void btnApprove_Click(object sender, EventArgs e)
    {
        int isChecked = 0;

        foreach (GridViewRow row in gvQMSFile.Rows)
        {
            if (((CheckBox)row.FindControl("chkStatus")).Checked == true && ((CheckBox)row.FindControl("chkStatus")).Enabled == true)
            {
                isChecked = 1;
                Label lblID = (Label)row.FindControl("lblID");
                Label lblFilePath = (Label)row.FindControl("lblFilePath");
                Label lblVersion = (Label)row.FindControl("lblVersion");
                int Level_ID = UDFLib.ConvertToInteger(gvQMSFile.DataKeys[row.RowIndex].Values["LevelID"]);

                objQMS.QMS_Update_FileApproval(UDFLib.ConvertToInteger(lblID.Text),Level_ID);
               // int retval = objQMS.QMS_Files_Approved(UDFLib.ConvertToInteger(lblID.Text), UDFLib.ConvertToInteger(Session["USERID"].ToString()), null
               //                     , UDFLib.ConvertToInteger(Session["USERID"]), UDFLib.ConvertToInteger(lblVersion.Text.Trim()));

               // string SourcePath = Server.MapPath(lblFilePath.Text);
               // string DestinationPath = Server.MapPath("~/uploads/QMS");
                //File.Copy(SourcePath, Path.Combine(DestinationPath, Path.GetFileName(SourcePath)));

            }
        }
        if (isChecked ==1)
            BindGrid();

    }


    protected void btnFilter_Click(object sender, EventArgs e)
    {
        BindGrid();
    }

    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        txtfilter.Text = "";
        optApprove.SelectedValue = "0";
        chkMyApproval.Checked = true;
        BindGrid();
    }

    protected void ImgExpExcel_Click(object sender, EventArgs e)
    {
        int rowcount = ucCustomPagerItems.isCountRecord;

        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());

        DataTable dt = objQMS.QMS_Files_Approval_Search(txtfilter.Text != "" ? txtfilter.Text : null, Convert.ToInt32(optApprove.SelectedValue), null
            , sortbycoloumn, sortdirection
             , null, null, ref  rowcount);


        string[] HeaderCaptions = { "File Name" ,"Created Date" ,"Version" ,"Approval Name" , "Approval Date", "Created By" };
        string[] DataColumnsName = { "LogFileID", "LogDate", "Version", "ApproverName", "Date_Of_Approval", "CreatedBy" };

        GridViewExportUtil.ShowExcel(dt, HeaderCaptions, DataColumnsName, "QMSFileApproval", "QMS File Approval Status", "");

    }

    protected void gvQMSFile_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            LinkButton lblLogFileID = (LinkButton)e.Row.FindControl("lblLogFileID");

            Label lblFilePath = (Label)e.Row.FindControl("lblFilePath");

            lblLogFileID.Attributes.Add("onclick", "DocOpen('" + lblFilePath.Text + "'); return false;");

            if (Convert.ToInt32(Session["USERID"]) == UDFLib.ConvertToInteger(gvQMSFile.DataKeys[e.Row.RowIndex].Values["ApproverID"]))
            {
                ((CheckBox)e.Row.FindControl("chkStatus")).Enabled = true;
            }
            else
            {
                ((CheckBox)e.Row.FindControl("chkStatus")).Enabled = false;
            }

        }
        
        
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


        //ImageButton ImgUpdate = (ImageButton)e.Row.FindControl("ImgUpdate");


        //if (objUA.Delete == 1)
        //{ 
        
        //}

    }

    protected void gvQMSFile_Sorting(object sender, GridViewSortEventArgs se)
    {
        ViewState["SORTBYCOLOUMN"] = se.SortExpression;

        if (ViewState["SORTDIRECTION"] != null && ViewState["SORTDIRECTION"].ToString() == "0")
            ViewState["SORTDIRECTION"] = 1;
        else
            ViewState["SORTDIRECTION"] = 0;

        BindGrid();
    }
    protected void btnFilter_Click(object sender, ImageClickEventArgs e)
    {
        BindGrid();
    }
}