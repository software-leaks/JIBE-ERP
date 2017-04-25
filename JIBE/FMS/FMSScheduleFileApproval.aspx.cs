using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.FMS;
using System.Data;
using System.Web.UI.HtmlControls;
using System.IO;
public partial class FMS_FMSScheduleFileApproval : System.Web.UI.Page
{
    BLL_FMS_Document objFMS = new BLL_FMS_Document();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState["SORTDIRECTION"] = null;
            ViewState["SORTBYCOLOUMN"] = null;

            ucCustomPagerItems.PageSize = 20;
            ucCustomPagerApprovedList.PageSize = 20;
            BindGrid();
        }
    }

    public void BindGrid()
    {
        grdApprovalList.Visible = true;
        grdApprovedList.Visible = false;
        ucCustomPagerItems.Visible = true;
        ucCustomPagerApprovedList.Visible = false;
        int rowcount = ucCustomPagerItems.isCountRecord;
        int? ApproverId =null;
        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());
        if (chkMyApproval.Checked == true)
            ApproverId = Int32.Parse(Session["USERID"].ToString());



    
        DataSet ds = new DataSet();

        ds = objFMS.FMS_Get_ScheduleFileApproval(ApproverId, sortbycoloumn, sortdirection, ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, txtfilter.Text != "" ? txtfilter.Text : null, ref  rowcount);

        



        if (ucCustomPagerItems.isCountRecord == 1)
        {

            ucCustomPagerItems.CountTotalRec =rowcount.ToString();
            ucCustomPagerItems.BuildPager();
        }
       // ds.Tables[0].Columns["Schedule_Date"].DataType = System.Type.GetType("System.DateTime");
         ds.Tables[0].DefaultView.Sort="Schedule_Date DESC";

        grdApprovalList.DataSource = ds.Tables[0].DefaultView.ToTable();
        grdApprovalList.DataBind();

    }


    public void ApprovedBindGrid()
    {
        grdApprovalList.Visible = false;
        grdApprovedList.Visible = true;
        ucCustomPagerItems.Visible = false;
        ucCustomPagerApprovedList.Visible = true;

        int rowcount = ucCustomPagerApprovedList.isCountRecord;
        int ApproverId = 0;
        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());
        if (chkMyApproval.Checked == true)
            ApproverId = Int32.Parse(Session["USERID"].ToString());




        DataSet ds = new DataSet();

        ds = objFMS.FMS_Get_ApprovedScheduleStatus(ApproverId, sortbycoloumn, sortdirection, ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, txtfilter.Text != "" ? txtfilter.Text : null, ref  rowcount);



        if (ucCustomPagerApprovedList.isCountRecord == 1)
        {

            ucCustomPagerApprovedList.CountTotalRec = rowcount.ToString();
            ucCustomPagerApprovedList.BuildPager();
        }



        ds.Tables[0].DefaultView.Sort = "Schedule_Date DESC";

        grdApprovedList.DataSource = ds.Tables[0].DefaultView.ToTable();
      
        grdApprovedList.DataBind();

    }
    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Convert.ToString(Session["USERID"]));
        else
            return 0;
    }
    protected void grdApprovalList_RowDataBound(object sender, GridViewRowEventArgs e)
    {



        if (e.Row.RowType == DataControlRowType.DataRow)
        {


            int res = 0;
            Label lblStatus = (Label)e.Row.Cells[6].FindControl("lblStatusID");
            Label lblAppLevel = (Label)e.Row.Cells[6].FindControl("lblApprovalLevel");

         
            LinkButton lblLogFileID = (LinkButton)e.Row.FindControl("lblLogFileID");

            Label lblFilePath = (Label)e.Row.FindControl("lblFilePath");

            lblLogFileID.Attributes.Add("onclick", "DocOpen('../" + lblFilePath.Text + "'); return false;");

            Label lblSchDate = (Label)e.Row.Cells[1].FindControl("lblSchDate");
            string strSchDate=lblSchDate.Text;
            lblSchDate.Text = Convert.ToDateTime(strSchDate).ToString("dd/MM/yyyy") ;

            if (Convert.ToInt32(Session["USERID"]) == UDFLib.ConvertToInteger(grdApprovalList.DataKeys[e.Row.RowIndex].Values["ApprovarID"]))
            {
                ((Button)e.Row.FindControl("BtnApprove")).Enabled = true;
                ((Button)e.Row.FindControl("BtnRework")).Enabled = true;
            }
            else
            {
                ((Button)e.Row.FindControl("BtnApprove")).Enabled = false;
                ((Button)e.Row.FindControl("BtnRework")).Enabled = false;
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

       
    }
    protected void grdApprovalList_Sorting(object sender, GridViewSortEventArgs e)
    {
        ViewState["SORTBYCOLOUMN"] = e.SortExpression;

        if (ViewState["SORTDIRECTION"] != null && ViewState["SORTDIRECTION"].ToString() == "0")
            ViewState["SORTDIRECTION"] = 1;
        else
            ViewState["SORTDIRECTION"] = 0;

        BindGrid();
    }
    protected void btnApprove_Click(object sender, EventArgs e)
    {
        string js = " showModal('dvRemark');";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "handleModal", js, true);

        

    }
    protected void ImgExpExcel_Click(object sender, EventArgs e)
    {
        int rowcount = ucCustomPagerItems.isCountRecord;

        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
       /* Need To Pull Data Schedule wise*/
        //DataTable dt = objFMS.FMS_Files_Approval_Search(txtfilter.Text != "" ? txtfilter.Text : null, Convert.ToInt32(optApprove.SelectedValue), null, sortbycoloumn, sortdirection , null, null, ref  rowcount);
        if (optApprove.SelectedValue == "0")
        {

             rowcount = ucCustomPagerItems.isCountRecord;
            int ApproverId = 0;
            string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
            int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());
            if (chkMyApproval.Checked == true)
                ApproverId = Int32.Parse(Session["USERID"].ToString());




            if (chkMyApproval.Checked == true)
            {

                ds = objFMS.FMS_Get_ScheduleFileApproval(ApproverId, sortbycoloumn, sortdirection, ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, txtfilter.Text != "" ? txtfilter.Text : null, ref  rowcount);
            }
            else
            {
                ds = objFMS.FMS_Get_ScheduleFileApproval(null, sortbycoloumn, sortdirection, ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, txtfilter.Text != "" ? txtfilter.Text : null, ref  rowcount);
            }

        }
        else if (optApprove.SelectedValue == "1")
        {
            rowcount = ucCustomPagerApprovedList.isCountRecord;
            int ApproverId = 0;
            string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
            int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());
            if (chkMyApproval.Checked == true)
                ApproverId = Int32.Parse(Session["USERID"].ToString());






            ds = objFMS.FMS_Get_ApprovedScheduleStatus(ApproverId, sortbycoloumn, sortdirection, ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, txtfilter.Text != "" ? txtfilter.Text : null, ref  rowcount);


        }

        string[] HeaderCaptions = { "File Name", "Due Date", "Version", "Approvar Name", "Approval Date","Status" };
        string[] DataColumnsName = { "FileName", "Schedule_Date", "Version", "ApproverName", "Date_Of_Approval", "App_Status" };

        GridViewExportUtil.ShowExcel(ds.Tables[0], HeaderCaptions, DataColumnsName, "Office Schedule Approval", "Office Schedule Approval Status", "");

    }

    protected void btnRefresh_Click(object sender, ImageClickEventArgs e)
    {
        txtfilter.Text = "";
        chkMyApproval.Checked = true;
        optApprove.SelectedValue = "0";
        if (optApprove.SelectedValue == "0")
        {

            BindGrid();
            grdApprovalList.Visible = true;
            grdApprovedList.Visible = false;

            if (grdApprovalList.Rows.Count > 0)
            {
                ucCustomPagerItems.Visible = true;
            }
            else
            {
                ucCustomPagerItems.Visible = false;
            }

          
            ucCustomPagerApprovedList.Visible = false;
        }
        else if (optApprove.SelectedValue == "1")
        {
            ApprovedBindGrid();
            grdApprovalList.Visible = false;
            grdApprovedList.Visible = true;
            ucCustomPagerItems.Visible = false;
            if (grdApprovedList.Rows.Count > 0)
            {
                ucCustomPagerApprovedList.Visible = true;
            }
            else
            {
                ucCustomPagerApprovedList.Visible = false;
            }
          
        }
    }
    protected void btnFilter_Click(object sender, ImageClickEventArgs e)
    {
        if (optApprove.SelectedValue == "0")
        {
            BindGrid();
            grdApprovalList.Visible = true;
            grdApprovedList.Visible = false;

            if (grdApprovalList.Rows.Count > 0)
            {
                ucCustomPagerItems.Visible = true;
            }
            else
            {
                ucCustomPagerItems.Visible = false;
            }


            ucCustomPagerApprovedList.Visible = false;
        }
        else if (optApprove.SelectedValue == "1")
        {
            ApprovedBindGrid();
            grdApprovalList.Visible = false;
            grdApprovedList.Visible = true;
            ucCustomPagerItems.Visible = false;
            if (grdApprovedList.Rows.Count > 0)
            {
                ucCustomPagerApprovedList.Visible = true;
            }
            else
            {
                ucCustomPagerApprovedList.Visible = false;
            }

        }
    }
    protected void btnSaveRemark_Click(object sender, EventArgs e)
    {
       
  
        int RowIndex = UDFLib.ConvertToInteger(ViewState["ApproveRowIndex"].ToString());
        Label lblFileID = (Label)grdApprovalList.Rows[RowIndex].FindControl("lblFileID");
        Label lblFilePath = (Label)grdApprovalList.Rows[RowIndex].FindControl("lblFilePath");
        Label lblVersion = (Label)grdApprovalList.Rows[RowIndex].FindControl("lblVersion");
        Label lblStatusID = (Label)grdApprovalList.Rows[RowIndex].FindControl("lblStatusID");
        Label lblOfficeID = (Label)grdApprovalList.Rows[RowIndex].FindControl("lblOfficeID");
        Label lblVesselID = (Label)grdApprovalList.Rows[RowIndex].FindControl("lblVesselID");
        int Approval_Level = UDFLib.ConvertToInteger(grdApprovalList.DataKeys[RowIndex].Values["Approval_Level"]);
        int ApprovedBy = GetSessionUserID();
        int CreatedBy = GetSessionUserID();
        objFMS.FMS_Insert_ScheduleFileApproval(UDFLib.ConvertToInteger(lblStatusID.Text), UDFLib.ConvertToInteger(lblOfficeID.Text), UDFLib.ConvertToInteger(lblVesselID.Text), UDFLib.ConvertToInteger(lblFileID.Text), txtRemark.Text, ApprovedBy, CreatedBy, UDFLib.ConvertToInteger(lblVersion.Text), Approval_Level, 1);


        DataSet ds = new DataSet();
        ds = objFMS.FMS_Get_ScheduleFileApprovalByStatus(UDFLib.ConvertToInteger(lblStatusID.Text), UDFLib.ConvertToInteger(lblOfficeID.Text), UDFLib.ConvertToInteger(lblVesselID.Text));
        if (ds.Tables[0].Rows.Count == 0)
        {
            objFMS.FMS_Update_ScheduleStatus(UDFLib.ConvertToInteger(lblStatusID.Text), UDFLib.ConvertToInteger(lblOfficeID.Text), UDFLib.ConvertToInteger(lblVesselID.Text), GetSessionUserID());

        }

      

        string js = " alert('Form Approved Successfully');";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", js, true);

        string js1 = " hideModal('dvRemark');";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "hdModal", js1, true);
        txtRemark.Text = "";
        BindGrid();
    }



    protected void btnSaveRRemark_Click(object sender, EventArgs e)
    {
        int RowIndex = UDFLib.ConvertToInteger(ViewState["ReworkRowIndex"].ToString());
        Label lblFileID = (Label)grdApprovalList.Rows[RowIndex].FindControl("lblFileID");
        Label lblStatusID = (Label)grdApprovalList.Rows[RowIndex].FindControl("lblStatusID");
        Label lblOfficeID = (Label)grdApprovalList.Rows[RowIndex].FindControl("lblOfficeID");
        Label lblVesselID = (Label)grdApprovalList.Rows[RowIndex].FindControl("lblVesselID");
        int Approval_Level = UDFLib.ConvertToInteger(grdApprovalList.DataKeys[RowIndex].Values["Approval_Level"]);
        Label lblVersion = (Label)grdApprovalList.Rows[RowIndex].FindControl("lblVersion");
        objFMS.FMS_Update_ScheduleStatusForRework(GetSessionUserID(), UDFLib.ConvertToInteger(lblStatusID.Text), UDFLib.ConvertToInteger(lblOfficeID.Text), UDFLib.ConvertToInteger(lblVesselID.Text), txtRRemark.Text, UDFLib.ConvertToInteger(lblVersion.Text),Approval_Level, UDFLib.ConvertToInteger(lblFileID.Text));

        string js = " alert('Form Re-work Successfully');";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", js, true);
        string js1 = " hideModal('dvReworkRemark');";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "hdModal", js1, true);
        txtRRemark.Text = "";
        BindGrid();
       
    }
    protected void grdApprovedList_Sorting(object sender, GridViewSortEventArgs e)
    {
        ViewState["SORTBYCOLOUMN"] = e.SortExpression;

        if (ViewState["SORTDIRECTION"] != null && ViewState["SORTDIRECTION"].ToString() == "0")
            ViewState["SORTDIRECTION"] = 1;
        else
            ViewState["SORTDIRECTION"] = 0;

        ApprovedBindGrid();
    }
  
    protected void grdApprovalList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if(e.CommandName=="Approve")
        {
            int RowIndex = UDFLib.ConvertToInteger(e.CommandArgument.ToString());

            ViewState["ApproveRowIndex"]=RowIndex;
            string js = " showModal('dvRemark');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "handleModal", js, true);
        }
        if (e.CommandName == "Rework")
        {
            int RowIndex = UDFLib.ConvertToInteger(e.CommandArgument.ToString());

            ViewState["ReworkRowIndex"] = RowIndex;
            string js = " showModal('dvRRemark');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "handleModal", js, true);
        }
    }

    protected void grdApprovedList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {


            int res = 0;
            Label lblStatus = (Label)e.Row.Cells[6].FindControl("lblStatusID");
            Label lblAppLevel = (Label)e.Row.Cells[6].FindControl("lblApprovalLevel");


            LinkButton lblLogFileID = (LinkButton)e.Row.FindControl("lblLogFileID");

            Label lblFilePath = (Label)e.Row.FindControl("lblFilePath");

            lblLogFileID.Attributes.Add("onclick", "DocOpen('../" + lblFilePath.Text + "'); return false;");

            Label lblSchDate = (Label)e.Row.Cells[1].FindControl("lblSchDate");
            string strSchDate = lblSchDate.Text;
            lblSchDate.Text = Convert.ToDateTime(strSchDate).ToString("dd/MM/yyyy");
          


        }
    }
}