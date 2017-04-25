using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.Infrastructure;
using System.Data;
using SMS.Business.VET;
using System.Collections;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Text;
using SMS.Properties;

public partial class Technical_Vetting_Vetting_Index : System.Web.UI.Page
{  
    public Boolean uaEditFlag = true;
    public Boolean uaDeleteFlage = true;
    int Vetting_Id = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            MergeGridviewHeader_Info objVetMerge = new MergeGridviewHeader_Info();
        if (GetSessionUserID() == 0)
            Response.Redirect("~/account/login.aspx");
        String msgretv = String.Format("setTimeout(getOperatingSystem,500);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgret6v", msgretv, true);
        UserAccessValidation();
        setDateFormat();
        if (!IsPostBack)
        {            
            BindVesselDDL();
            VET_Get_VettingTypeList();
            Add_VettingStatus();
            VET_Get_OilMajorList();
            Add_JobStatus();
            Bind_VettingIndex();
            VET_Get_InspectorList();
        }

        objVetMerge.AddMergedColumns(new int[] { 9, 10, 11, 12 }, "Report", "GroupHeaderStyle-css HeaderStyle-css");
        objVetMerge.AddMergedColumns(new int[] { 13, 14, 15, 16 }, "Jobs", "GroupHeaderStyle-css HeaderStyle-css");
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }


    #region Vetting Index

    /// <summary>
    /// Method is used to set date format
    /// </summary>
    public void setDateFormat()
    {
        cexLVetFromDate.Format = UDFLib.GetDateFormat();
        cexLVetToDate.Format = UDFLib.GetDateFormat();       

    }
    /// <summary>
    /// To check access rights of user for requested page
    /// </summary>
    protected void UserAccessValidation()
    {
        try
        {
            BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
            UserAccess objUA = new UserAccess();
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

        objUA = objUser.Get_UserAccessForPage(Convert.ToInt32(Session["USERID"]), PageURL);

        if (objUA.View == 0)
        {
            Response.Redirect("~/default.aspx?msgid=1");
        }

        if (objUA.Add == 0)
        {

        }
        if (objUA.Edit == 1)
            uaEditFlag = true;

        if (objUA.Delete == 1) uaDeleteFlage = true;
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    /// <summary>
    /// Method is used to get login user id
    /// </summary>
    /// <returns>retrun user id</returns>
    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;

    }
   
    /// <summary>
    /// Bind Vessel dropdown as per vessel assign to the specific user
    /// </summary>
    public void BindVesselDDL()
    {
        try
        {
            BLL_Infra_VesselLib objVsl = new BLL_Infra_VesselLib();
            BLL_VET_VettingLib objBLLVetLib = new BLL_VET_VettingLib();
            if (chkVesselAssign.Checked == true)
            {
                DataTable dtVessel = objVsl.Get_VesselList(0, 0, Convert.ToInt32(Session["USERCOMPANYID"].ToString()), "", Convert.ToInt32(Session["USERCOMPANYID"].ToString()));

                DDLVessel.DataTextField = "Vessel_name";
                DDLVessel.DataValueField = "Vessel_id";
                DDLVessel.DataSource = dtVessel;
                DDLVessel.DataBind();


                DataTable dtUserVessel = objBLLVetLib.VET_Get_UserVesselList(0, 0, Convert.ToInt32(Session["USERCOMPANYID"].ToString()), "", Convert.ToInt32(Session["USERCOMPANYID"].ToString()), GetSessionUserID());
                if (dtUserVessel.Rows.Count > 0)
                {
                    CheckBoxList chk = (CheckBoxList)DDLVessel.Controls[0].Controls[0].FindControl("CheckBoxListItems");
                    for (int j = 0; j < chk.Items.Count; j++)
                    {
                        for (int i = 0; i < dtUserVessel.Rows.Count; i++)
                        {
                            if (chk.Items[j].Value == dtUserVessel.Rows[i]["Vessel_ID"].ToString())
                            {
                                ((CheckBoxList)DDLVessel.Controls[0].Controls[0].FindControl("CheckBoxListItems")).Items[j].Selected = true;
                                break;
                            }
                        }
                    }
                }
            }
            else
            {
                DataTable dtVessel = objVsl.Get_VesselList(0, 0, Convert.ToInt32(Session["USERCOMPANYID"].ToString()), "", Convert.ToInt32(Session["USERCOMPANYID"].ToString()));

                DDLVessel.DataTextField = "Vessel_name";
                DDLVessel.DataValueField = "Vessel_id";
                DDLVessel.DataSource = dtVessel;
                DDLVessel.DataBind();

            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string js = "alert('" + UDFLib.GetException("SystemError/ GeneralMessage") + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", js, true);
        }
    }

    /// <summary>
    /// To display Vetting Type
    /// </summary>
    public void VET_Get_VettingTypeList()
    {
        try
        {
            BLL_VET_VettingLib objBLLVetLib = new BLL_VET_VettingLib();
            DataTable dtVetType = objBLLVetLib.VET_Get_VettingTypeList();          
            DDLVetType.DataTextField = "Vetting_Type_Name";
            DDLVetType.DataValueField = "Vetting_Type_ID";
            DDLVetType.DataSource = dtVetType;
            DDLVetType.DataBind();
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string js = "alert('" + UDFLib.GetException("SystemError/ GeneralMessage") + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", js, true);
        }
    }

    /// <summary>
    /// Bind status to status dropdown
    /// </summary>
    public void Add_VettingStatus()
    {
        try
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Value");

            dt.Rows.Add("Planned");
            dt.Rows.Add("In-Progress");
            dt.Rows.Add("Completed");
            dt.Rows.Add("Failed");

            DDLStatus.DataSource = dt;
            DDLStatus.DataTextField = "Value";
            DDLStatus.DataValueField = "Value";
            DDLStatus.DataBind();
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string js = "alert('" + UDFLib.GetException("SystemError/ GeneralMessage") + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", js, true);
        }

    }

    /// <summary>
    /// Bind oil major list
    /// </summary>
    public void VET_Get_OilMajorList()
    {
        try
        {
            BLL_VET_VettingLib objBLLVetLib = new BLL_VET_VettingLib();
            DataTable dtOilMajorNames = objBLLVetLib.VET_Get_OilMajorList();
            DDLOilMajor.DataSource = dtOilMajorNames;
            DDLOilMajor.DataTextField = "Display_Name";
            DDLOilMajor.DataValueField = "ID";
            DDLOilMajor.DataBind();
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string js = "alert('" + UDFLib.GetException("SystemError/ GeneralMessage") + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", js, true);
        }
    }

    /// <summary>
    /// Bind all inspector list
    /// </summary>
    public void VET_Get_InspectorList()
    {
        try
        {
            BLL_VET_VettingLib objBLLVetLib = new BLL_VET_VettingLib();
            DataTable dt = objBLLVetLib.VET_Get_InspectorList();
            DDLInspector.DataSource = dt;
            DDLInspector.DataTextField = "NAME";
            DDLInspector.DataValueField = "UserID";
            DDLInspector.DataBind();

        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string js = "alert('" + UDFLib.GetException("SystemError/ GeneralMessage") + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", js, true);
        }
    }

    /// <summary>
    /// Bind Job status
    /// </summary>
    public void Add_JobStatus()
    {
        try
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Value");

            dt.Rows.Add("Pending");
            dt.Rows.Add("Completed");
            dt.Rows.Add("Verify");
            dt.Rows.Add("Deferred");

            DDLJobStatus.DataSource = dt;
            DDLJobStatus.DataTextField = "Value";
            DDLJobStatus.DataValueField = "Value";
            DDLJobStatus.DataBind();
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string js = "alert('" + UDFLib.GetException("SystemError/ GeneralMessage") + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", js, true);
        }

    }

    /// <summary>
    /// Bind all records of vetting
    /// </summary>
    public void Bind_VettingIndex()
    {
        try
        {
            BLL_VET_Index objBLLIndx = new BLL_VET_Index();    
            DataSet ds = new DataSet();
            int rowcount = ucCustomPagerItems.isCountRecord;
            string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
            int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());
            int IsValid = 0;
            DataTable dtEXInspector = new DataTable();
            dtEXInspector.Columns.Add("ID");
            DataTable dtInspector = new DataTable();
            dtInspector.Columns.Add("ID");
            foreach (DataRow dr in DDLInspector.SelectedValues.Rows)
            {
                if (dr[0].ToString().Split('_')[1] == "In")
                {
                    dtInspector.Rows.Add(dr[0].ToString().Split('_')[0]);
                }
                if (dr[0].ToString().Split('_')[1] == "Ex")
                {
                    dtEXInspector.Rows.Add(dr[0].ToString().Split('_')[0]);
                }
            }

            DateTime? VetFromDate, VetToDate;
            VetFromDate = txtLVetFromDate.Text.Trim() == "" ? null : UDFLib.ConvertDateToNull(UDFLib.ConvertToDate(txtLVetFromDate.Text));
            VetToDate = txtLVetToDate.Text.Trim() == "" ? null : UDFLib.ConvertDateToNull(UDFLib.ConvertToDate(txtLVetToDate.Text));
            
            if (rbtnValid.Checked == true)
            {
                IsValid = 1;
            }
            ds = objBLLIndx.VET_Get_VettingIndex(DDLVessel.SelectedValues, DDLVetType.SelectedValues, DDLStatus.SelectedValues, txtDueDays.Text == "0" ? UDFLib.ConvertToInteger(txtDueDays.Text.Trim()) : UDFLib.ConvertIntegerToNull(txtDueDays.Text.Trim()), IsValid, UDFLib.ConvertToInteger(rbtnObservation.SelectedValue), DDLOilMajor.SelectedValues, dtInspector, dtEXInspector, VetFromDate, VetToDate, DDLJobStatus.SelectedValues, txtVessel.Text != "" ? txtVessel.Text.Trim() : null, sortbycoloumn, sortdirection, ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref rowcount);

            if (ds.Tables.Count > 0)
            {             

                ViewState["dtObservationCount"] = ds.Tables[1];
                ViewState["dtNotesCount"] = ds.Tables[2];
                ViewState["dtResponse"] = ds.Tables[3];
                ViewState["dtJobsCount"] = ds.Tables[4];
            }
                
            if (ucCustomPagerItems.isCountRecord == 1)
                {
                    ucCustomPagerItems.CountTotalRec = rowcount.ToString();
                    ucCustomPagerItems.BuildPager();
                }
            gvVettingIndex.DataSource = ds.Tables[0];
            gvVettingIndex.DataBind();
            if (ds.Tables[0].Rows.Count>0)
            {
                btnExport.Enabled = true;
            }
            else
            {
                btnExport.Enabled = false;
            }

            UpdPnlGrid.Update();
            UpdPnlFilter.Update();

        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string js = "alert('" + UDFLib.GetException("SystemError/ GeneralMessage") + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", js, true);
        }
    }
    protected void btnRetrieve_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            Bind_VettingIndex();
            if (hfAdv.Value == "o")
            {
                String tgladvsearch = String.Format("toggleOnSearchClearFilter(advText,'" + hfAdv.Value + "');");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tgladvsearch", tgladvsearch, true);
            }
            else
            {
                String tgladvsearch1 = String.Format("toggleOnSearchClearFilter(advText,'" + hfAdv.Value + "');");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tgladvsearch1", tgladvsearch1, true);
            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string js = "alert('" + UDFLib.GetException("SystemError/ GeneralMessage") + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", js, true);
        }
    }
    protected void btnClearFilter_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            DDLVessel.ClearSelection();
            DDLVetType.ClearSelection();
            DDLStatus.ClearSelection();
            DDLJobStatus.ClearSelection();
            txtDueDays.Text = "";
            rbtnValid.Checked=false;
            rbtnObservation.SelectedValue = "0";
            DDLOilMajor.ClearSelection();
            DDLInspector.ClearSelection();
            txtLVetFromDate.Text = "";
            txtLVetToDate.Text = "";
            txtVessel.Text = "";
            chkVesselAssign.Checked = true;
            BindVesselDDL();
            UpdAdvFltr.Update();
            Bind_VettingIndex();
            if (hfAdv.Value == "o")
            {
                String tgladvsearchClr = String.Format("toggleOnSearchClearFilter(advText,'" + hfAdv.Value + "');");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tgladvsearchClr", tgladvsearchClr, true);
            }
            else
            {
                String tgladvsearchClr1 = String.Format("toggleOnSearchClearFilter(advText,'" + hfAdv.Value + "');");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tgladvsearchClr1", tgladvsearchClr1, true);
            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string js = "alert('" + UDFLib.GetException("SystemError/ GeneralMessage") + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", js, true);
        }

    }
    protected void gvVettingIndex_RowCreated(object sender, GridViewRowEventArgs e)
    {
        try
        {
            MergeGridviewHeader_Info objVetMerge = new MergeGridviewHeader_Info();
            if (e.Row.RowType == DataControlRowType.Header)
            {
                MergeGridviewHeader.SetProperty(objVetMerge);
                e.Row.SetRenderMethodDelegate(MergeGridviewHeader.RenderHeader);

            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string js = "alert('" + UDFLib.GetException("SystemError/ GeneralMessage") + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", js, true);
        }
    }

    /// <summary>
    /// To Export gridview data to excel
    /// </summary>
    /// <param name="GridViewexp"> Grid view name </param>
    /// <param name="ExportHeader"> Dislay Header in excel </param>
    public void ExportGridviewToExcel(GridView GridViewexp, string ExportHeader)
    {   
        try
        {

        string filename = String.Format("Vetting_{0}_{1}_{2}.xls", DateTime.Today.Day.ToString(),
            DateTime.Today.Month.ToString(), DateTime.Today.Year.ToString());

        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + filename);
        HttpContext.Current.Response.Charset = "";

        HttpContext.Current.Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);

        HttpContext.Current.Response.ContentType = "application/vnd.xls";

        System.IO.StringWriter stringWriter = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter htmlWriter = new HtmlTextWriter(stringWriter);

        // Replace all gridview controls with literals

        GridViewRow grh = GridViewexp.HeaderRow;
        grh.ForeColor = System.Drawing.Color.Black;
        grh.HorizontalAlign = HorizontalAlign.Left;
        GridViewexp.GridLines = GridLines.Both;

        grh.Cells[17].Visible = false;
        foreach (TableCell cl in grh.Cells)
        {
            cl.HorizontalAlign = HorizontalAlign.Left;
            cl.Attributes.Add("class", "text");
            PrepareControlForExport_GridView(cl);

        }
        foreach (GridViewRow gr in GridViewexp.Rows)
        {
            gr.Cells[17].Visible = false;
            foreach (TableCell cl in gr.Cells)
            {
                cl.HorizontalAlign = HorizontalAlign.Left;
                cl.Attributes.Add("class", "text");
                PrepareControlForExport_GridView(cl);

            }
        }

        System.Web.UI.HtmlControls.HtmlForm form
            = new System.Web.UI.HtmlControls.HtmlForm();
        Controls.Add(form);

        Label lbl = new Label();
        lbl.Text = ExportHeader;
        lbl.Font.Size = 14;
        lbl.Font.Bold = true;
        form.Controls.Add(lbl);
        form.Controls.Add(GridViewexp);
        form.RenderControl(htmlWriter);
        string style = @"<style> .text { mso-number-format:\@; } </style> ";
        HttpContext.Current.Response.Write(style);
        HttpContext.Current.Response.Write(stringWriter.ToString());
        HttpContext.Current.Response.End();
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }

    }

    public static void PrepareControlForExport_GridView(Control control)
    {
        try
        {
            for (int i = 0; i < control.Controls.Count; i++)
            {
                Control current = control.Controls[i];

                if (current is LinkButton)
                {
                    TableCell cl = control as TableCell;
                    cl.Text = (current as LinkButton).Text;

                }
                else if (current is ImageButton)
                {
                    TableCell cl = control as TableCell;
                    cl.Text = (current as ImageButton).ToolTip;
                }
                else if (current is HyperLink)
                {
                    TableCell cl = control as TableCell;
                    cl.Text = (current as HyperLink).Text;
                }
                else if (current is DropDownList)
                {
                    TableCell cl = control as TableCell;
                    cl.Text = (current as DropDownList).Items.Count > 0 ? (current as DropDownList).SelectedItem.Text : ""; ;
                }
                else if (current is CheckBox)
                {

                    current.Visible = false;
                }
                else if (current is TextBox)
                {
                    TableCell cl = control as TableCell;
                    cl.Text = (current as TextBox).Text;

                }
                else if (current is Image)
                {
                    TableCell cl = control as TableCell;

                    cl.Text = (current as Image).AlternateText;

                }
                else if (current is Label)
                {
                    TableCell cl = control as TableCell;
                    cl.Text = (current as Label).Text;

                }

            }
        
         }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }

    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        //required to avoid the run time error "  
        //Control 'GridView1' of type 'Grid View' must be placed inside a form tag with runat=server."  
    }
    protected void btnExport_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            Bind_VettingIndex();
            ExportGridviewToExcel(gvVettingIndex, "Vetting Index");
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string js = "alert('" + UDFLib.GetException("SystemError/ GeneralMessage") + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", js, true);
        }
    }
    protected void gvVettingIndex_Sorting(object sender, GridViewSortEventArgs e)
    {
        try
        {
            ViewState["SORTBYCOLOUMN"] = e.SortExpression;

            if (ViewState["SORTDIRECTION"] != null && ViewState["SORTDIRECTION"].ToString() == "0")
                ViewState["SORTDIRECTION"] = 1;
            else
                ViewState["SORTDIRECTION"] = 0;

            Bind_VettingIndex();
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string js = "alert('" + UDFLib.GetException("SystemError/ GeneralMessage") + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", js, true);
        }

    }
    protected void gvVettingIndex_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            MergeGridviewHeader_Info objVetMerge = new MergeGridviewHeader_Info();
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblVettingId = (Label)e.Row.FindControl("lblVetting_Id");
                Label lblOverDueRespondDate = (Label)e.Row.FindControl("lblOverDueRespondDate");
                Label lblOverDueValidDate = (Label)e.Row.FindControl("lblOverDueValidDate");
                Label lblVettingStatus = (Label)e.Row.FindControl("lblVetStatus");
                Label lblOpenCloseResponse = (Label)e.Row.FindControl("lblResponse");       

                Label lblVettingDate = (Label)e.Row.FindControl("lblVetDate");
                Label lblRespondDate = (Label)e.Row.FindControl("lblRespondDate");
                Label lblValidDate = (Label)e.Row.FindControl("lblValidDate");

                HyperLink hplinkPendingJob = (HyperLink)e.Row.FindControl("hplnkJobPending");
                HyperLink hplinkJobCompleted = (HyperLink)e.Row.FindControl("hplnkJobCompleted");
                HyperLink hplinkJobVerified = (HyperLink)e.Row.FindControl("hplnkJobVerified");
                HyperLink hplinkJobDeffered = (HyperLink)e.Row.FindControl("hplnkJobDeffered");
                HyperLink hplinkOpenJob = (HyperLink)e.Row.FindControl("hplnkOpenObs");
                HyperLink hplinkCloseJob = (HyperLink)e.Row.FindControl("hplnkClosedObs");
                HyperLink hplinkNote = (HyperLink)e.Row.FindControl("hplnkNote");               
               
                
                if (lblOverDueRespondDate.Text == "Y")
                {
                    e.Row.Cells[5].BackColor = System.Drawing.Color.Red;
                    e.Row.Cells[5].ForeColor = System.Drawing.Color.White;
                    e.Row.Cells[5].Font.Bold = true;
                }
                if (lblOverDueValidDate.Text == "Y")
                {
                    e.Row.Cells[6].BackColor = System.Drawing.Color.Red;
                    e.Row.Cells[6].ForeColor = System.Drawing.Color.White;
                    e.Row.Cells[6].Font.Bold = true;
                }

                ImageButton ImgDelete = (ImageButton)e.Row.FindControl("ImgDelete");
                HyperLink ImgReport = (HyperLink)e.Row.FindControl("ImgReport");

                if (lblVettingStatus.Text == "Planned")
                {
                    ImgDelete.Visible = true;
                    ImgReport.Visible = false;
                }
                else
                {
                    ImgDelete.Visible = false;
                    ImgReport.Visible = true;
                }

                ImageButton ImgRework = (ImageButton)e.Row.FindControl("ImgRework");
                if (lblVettingStatus.Text == "Completed" || lblVettingStatus.Text == "Failed")
                {
                    ImgRework.Visible = true;
                }
                else
                {
                    ImgRework.Visible = false;
                }

                HyperLink ImgDetail = (HyperLink)e.Row.FindControl("ImgDetails");             
                ImgDetail.NavigateUrl = "../Vetting/Vetting_Details.aspx?Vetting_Id=" + DataBinder.Eval(e.Row.DataItem, "Vetting_ID").ToString();     


                if (lblVettingDate.Text != "")
                {
                    lblVettingDate.Text = UDFLib.ConvertUserDateFormat(lblVettingDate.Text, UDFLib.GetDateFormat());
                }
                if (lblRespondDate.Text != "")
                {
                    lblRespondDate.Text = UDFLib.ConvertUserDateFormat(lblRespondDate.Text, UDFLib.GetDateFormat());
                }
                if (lblValidDate.Text != "")
                {
                    lblValidDate.Text = UDFLib.ConvertUserDateFormat(lblValidDate.Text, UDFLib.GetDateFormat());
                }
                // if extra column is added need to changed in this condition
                for (int j = 0; j < 9; j++)
                {
                    // Row should be clickable 
                    e.Row.Cells[j].Attributes.Add("onclick", "window.open('Vetting_Details.aspx?Vetting_Id=" + DataBinder.Eval(e.Row.DataItem, "Vetting_ID").ToString() + "','_blank');");
                    e.Row.Cells[j].Attributes.Add("style", "cursor:pointer;");
                    e.Row.Cells[j].Attributes["onmousedown"] = ClientScript.GetPostBackClientHyperlink(this.gvVettingIndex, "Select$" + e.Row.RowIndex);            
                }

                // added hyperlink for response column 
                e.Row.Cells[12].Attributes.Add("onclick", "window.open('Vetting_Details.aspx?Vetting_Id=" + DataBinder.Eval(e.Row.DataItem, "Vetting_ID").ToString() + "','_blank');");
                e.Row.Cells[12].Attributes.Add("style", "cursor:pointer;");
                e.Row.Cells[12].Attributes["onmousedown"] = ClientScript.GetPostBackClientHyperlink(this.gvVettingIndex, "Select$" + e.Row.RowIndex);       
                for (int i = 9; i < e.Row.Controls.Count; i++)
                {
                  
                    string ObsOpenCount, ObsCloseCount, NotesCount, ResponseCount, PendJobCount, CompJobCount, VerifiedJobCount, DefferedJobCount;

                    DataTable dtObservationCount = (DataTable)ViewState["dtObservationCount"];
                    string filteropnobsexpression = " Observation_Type_ID=2 and Status='Open' and Vetting_ID=" + lblVettingId.Text;
                    DataRow[] drObsOpenCount = dtObservationCount.Select(filteropnobsexpression);
                    if (drObsOpenCount.Length <= 0)
                    {
                        ObsOpenCount = "0";
                        hplinkOpenJob.Text = ObsOpenCount;

                    }
                    else
                    {
                        ObsOpenCount = drObsOpenCount[0][0].ToString();
                        hplinkOpenJob.Text = ObsOpenCount;
                    }
                    string filterclosedobsexpression = " Observation_Type_ID=2 and Status='Closed' and Vetting_ID=" + lblVettingId.Text;
                    DataRow[] drObsCloseCount = dtObservationCount.Select(filterclosedobsexpression);
                    if (drObsCloseCount.Length <= 0)
                    {
                        ObsCloseCount = "0";
                        hplinkCloseJob.Text = ObsCloseCount;
                    }
                    else
                    {
                        ObsCloseCount = drObsCloseCount[0][0].ToString();
                        hplinkCloseJob.Text = ObsCloseCount;
                    }
                    DataTable dtNotesCount = (DataTable)ViewState["dtNotesCount"];
                    string filternoteexpression = " Observation_Type_ID=1 and Vetting_ID=" + lblVettingId.Text;
                    DataRow[] drNotesCount = dtNotesCount.Select(filternoteexpression);
                    if (drNotesCount.Length <= 0)
                    {
                        NotesCount = "0";
                        hplinkNote.Text = NotesCount;
                    }
                    else
                    {
                        NotesCount = drNotesCount[0][0].ToString();
                        hplinkNote.Text = NotesCount;
                    }
                    DataTable dtResponseCount = (DataTable)ViewState["dtResponse"];
                    string filterresponseexpression = "Vetting_ID=" + lblVettingId.Text;
                    DataRow[] drResponseCount = dtResponseCount.Select(filterresponseexpression);
                    if (drResponseCount.Length <= 0)
                    {
                        ResponseCount = "0";
                        lblOpenCloseResponse.Text = ResponseCount;
                    }
                    else
                    {
                        ResponseCount = drResponseCount[0][0].ToString();
                        lblOpenCloseResponse.Text = ResponseCount;
                    }
                    DataTable dtJobCount = (DataTable)ViewState["dtJobsCount"];
                    string filterpendjobexpression = " Vetting_ID=" + lblVettingId.Text + " and Job_Status='PENDING'";
                    DataRow[] drPendJobCount = dtJobCount.Select(filterpendjobexpression);
                    if (drPendJobCount.Length <= 0)
                    {
                        PendJobCount = "0";
                        hplinkPendingJob.Text = PendJobCount;
                    }
                    else
                    {
                        PendJobCount = drPendJobCount[0][0].ToString();
                        hplinkPendingJob.Text = PendJobCount;
                    }
                    string filtercompletedjobexpression = " Vetting_ID=" + lblVettingId.Text + " and Job_Status='COMPLETED'";
                    DataRow[] drComplJobCount = dtJobCount.Select(filtercompletedjobexpression);
                    if (drComplJobCount.Length <= 0)
                    {
                        CompJobCount = "0";
                        hplinkJobCompleted.Text = CompJobCount;
                    }
                    else
                    {
                        CompJobCount = drComplJobCount[0][0].ToString();
                        hplinkJobCompleted.Text = CompJobCount;
                    }
                    string filterverifiedjobexpression = " Vetting_ID=" + lblVettingId.Text + " and Job_Status='VERIFY'";
                    DataRow[] drVerifiedJobCount = dtJobCount.Select(filterverifiedjobexpression);
                    if (drVerifiedJobCount.Length <= 0)
                    {
                        VerifiedJobCount = "0";
                        hplinkJobVerified.Text = VerifiedJobCount;
                    }
                    else
                    {
                        VerifiedJobCount = drVerifiedJobCount[0][0].ToString();
                        hplinkJobVerified.Text = VerifiedJobCount;
                    }
                    string filterdefferedjobexpression = " Vetting_ID=" + lblVettingId.Text + " and Job_Status='DEFERRED'";
                    DataRow[] drDefferedJobCount = dtJobCount.Select(filterdefferedjobexpression);
                    if (drDefferedJobCount.Length <= 0)
                    {
                        DefferedJobCount = "0";
                        hplinkJobDeffered.Text = DefferedJobCount;
                    }
                    else
                    {
                        DefferedJobCount = drDefferedJobCount[0][0].ToString();
                        hplinkJobDeffered.Text = DefferedJobCount;
                    }
                }

            }
            if (e.Row.RowType == DataControlRowType.Header)
            {
                MergeGridviewHeader.SetProperty(objVetMerge);

                e.Row.SetRenderMethodDelegate(MergeGridviewHeader.RenderHeader);
                ViewState["DynamicHeaderCSS"] = "GroupHeaderStyle-css HeaderStyle-css";

            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string js = "alert('" + UDFLib.GetException("SystemError/ GeneralMessage") + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", js, true);
        }
    }

    protected void onDelete(object source, CommandEventArgs e)
    {
        BLL_VET_Index objBLLIndx = new BLL_VET_Index();    
        Vetting_Id = Convert.ToInt32(e.CommandArgument);
        try
        {
            int res = objBLLIndx.VET_Del_PlannedVetting(Vetting_Id, Convert.ToInt32(Session["USERID"]));

            if (res < 0)
            {
                Bind_VettingIndex();
            }

        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string js = "alert('" + UDFLib.GetException("SystemError/ GeneralMessage") + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", js, true);
        }
    }

    protected void onRework(object source, CommandEventArgs e)
    {
        BLL_VET_Index objBLLIndx = new BLL_VET_Index();    
        Vetting_Id = Convert.ToInt32(e.CommandArgument);
        try
        {
            objBLLIndx.VET_Upd_VettingReworkStatus(Vetting_Id, Convert.ToInt32(Session["USERID"]));

            Bind_VettingIndex();
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string js = "alert('" + UDFLib.GetException("SystemError/ GeneralMessage") + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", js, true);
        }
    }

    /// <summary>
    /// Event is use to call function that retrive forms details according to vessels assigned to login user.
    /// </summary>
    protected void chkVesselAssign_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            BindVesselDDL();
            UpdPnlFilter.Update();

        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string js = "alert('" + UDFLib.GetException("SystemError/ GeneralMessage") + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", js, true);
        }

    }

    #endregion
  }