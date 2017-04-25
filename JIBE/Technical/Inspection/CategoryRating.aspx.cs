using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
//using SMS.Business.Technical;
using System.Data;
using System.Drawing;
using System.Collections;
using SMS.Properties;
using SMS.Business.Infrastructure;
using SMS.Business.Inspection;


public partial class Technical_Worklist_CategoryRating : System.Web.UI.Page
{
   // BLL_Tec_Worklist objWl = new BLL_Tec_Worklist();
    BLL_Tec_Inspection objInsp=new BLL_Tec_Inspection();
    DataSet dsCat = new DataSet();
    DataSet dsSubCat = new DataSet();
    DataSet dsRating = new DataSet();
    int dirtyCounter = 0;
    public UserAccess objUA = new UserAccess();
    BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
    BLL_INSP_Checklist objChecklist = new BLL_INSP_Checklist();
    protected void Page_Load(object sender, EventArgs e)
    {
       
        if (!IsPostBack)
        {

           // BindSubCategoryRating(null, null);
            if (Request.QueryString["InspID"] != null && Request.QueryString["ScheduleID"] != null)
            {
                if (Request.QueryString["InspID"].ToString().Trim() != "" && Request.QueryString["ScheduleID"] != "")
                {
                    int ScheduleID = UDFLib.ConvertToInteger(Request.QueryString["ScheduleID"].ToString());
                    string CheckListIDS = "";//GetChecklist(ScheduleID);
                    ViewState["CheckListIDs"] = CheckListIDS;
                    ViewState["InspectionID"] = Request.QueryString["InspID"].ToString();
                    BindCategoryRating(ViewState["InspectionID"].ToString(), CheckListIDS);
                 
                    // dirtyCounter = 0;
                    hdnDirtyCounter.Value = "0";
                    BindInspectionInfo();

                    if (Request.QueryString["rnd"] != null)
                    {
                        grdCatRating.Columns[7].Visible = false;
                        //grdCatRating.Columns[6].Visible = false;
                        grdCatRating.CellPadding = 2;

                        if (Request.QueryString["ReportType"]!= null)
                        {
                            if (Request.QueryString["ReportType"] == "C")
                            {

                                grdCatRating.Columns[3].Visible = false;
                            }
                        }
                   
                    }
                    if (Request.QueryString["SystemCode"] != null)
                    {

                        BindSubCategoryRating(Request.QueryString["SystemCode"], ViewState["InspectionID"].ToString());
                        DataRow[] dr;
                        dr=dsCat.Tables[0].Select("Code=" + Request.QueryString["SystemCode"]);
                        if (grdSubCatRating.Rows.Count >0 )
                        {
                            ((Label)grdSubCatRating.HeaderRow.Cells[0].FindControl("lblCatNo")).Text = dr[0].ItemArray[0].ToString();//((HiddenField)grdCatRating.Rows[rowindex].FindControl("hdnCatRNo")).Value;
                            ((Label)grdSubCatRating.HeaderRow.Cells[1].FindControl("lblCatDesc")).Text = dr[0].ItemArray[2].ToString();//((HiddenField)grdCatRating.Rows[rowindex].FindControl("hdnCatDesc")).Value;

                            grdSubCatRating.CellPadding = 2;

                            //grdSubCatRating.Columns[7].Visible = false;
                            grdSubCatRating.Columns[8].Visible = false;
                            grdSubCatRating.Columns[9].Visible = false;
                        }
                        

                        
                      //  grdSubCatRating.Enabled = false;

                        //for (int i = 0; i < grdSubCatRating.Rows.Count; i++)
                        //{
                        //    ((DropDownList)grdSubCatRating.Rows[i].Cells[5].FindControl("ddlRating")).Visible = false;
                        //    ((Label)grdSubCatRating.Rows[i].Cells[5].FindControl("lblSubCatValue")).Visible = true;
                        //    ((TextBox)grdSubCatRating.Rows[i].Cells[5].FindControl("txtRemarks")).Visible = false;
                        //    ((Label)grdSubCatRating.Rows[i].Cells[5].FindControl("lblRemarks")).Visible = true;
                        //    if (((DropDownList)grdSubCatRating.Rows[i].Cells[5].FindControl("ddlRating")).SelectedItem.Text != "--SELECT--")
                        //    {
                        //        ((Label)grdSubCatRating.Rows[i].Cells[5].FindControl("lblSubCatValue")).Text = ((DropDownList)grdSubCatRating.Rows[i].Cells[5].FindControl("ddlRating")).SelectedItem.Text;
                        //    }
                        //}

                        hdnrnd.Value = Request.QueryString["rnd"];
                //             string js4 = " setTimeout(ReplaceDropDownWithLabel, 10000);";
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "test", js4, true);
                   
                  }

                }
            }
        
          
        }
        string js4 = " onLoad();";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "test", js4, true);
        UserAccessValidation();
        
    }


    public string GetChecklist(int ScheduleID)
    {
        DataTable ds = objChecklist.Get_CheckListName(ScheduleID);
        string ChecklistID = string.Empty;
        if (ds.Rows.Count > 0)
        {

            for (int i = 0; i < ds.Rows.Count; i++)
            {
                if (ChecklistID != "")
                {
                    ChecklistID = ChecklistID + "," + ds.Rows[i]["ChecklistID"].ToString();
                }
                else
                {
                    ChecklistID = ds.Rows[i]["ChecklistID"].ToString();
                }


            }
        }

        return ChecklistID;
    }
    public void BindInspectionInfo()
    {
        if (Request.QueryString["VesselName"] != null && Request.QueryString["InspectType"] != null && Request.QueryString["InspectorName"] != null && Request.QueryString["From"] != null && Request.QueryString["To"] != null)
        {
            lblVesselName.Text = Request.QueryString["VesselName"].ToString();
            lblInspType.Text = Request.QueryString["InspectType"].ToString();
            lblInspectorName.Text = Request.QueryString["InspectorName"].ToString();

            lblFrom.Text = Convert.ToDateTime(Request.QueryString["From"].ToString()).ToString("dd MMM yyyy");
            if (Request.QueryString["To"].ToString() != "")
            {
                lblTo.Text = Convert.ToDateTime(Request.QueryString["To"].ToString()).ToString("dd MMM yyyy");
            }
            else
            {
                lblTo.Text = "";
            }
        }

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

        }
    }
    #region Merge Header
    [Serializable]
    private class MergedColumnsInfo
    {
        // indexes of merged columns
        public List<int> MergedColumns = new List<int>();
        // key-value pairs: key = the first column index, value = number of the merged columns
        public Hashtable StartColumns = new Hashtable();
        // key-value pairs: key = the first column index, value = common title of the merged columns 
        public Hashtable Titles = new Hashtable();

        //parameters: the merged columns indexes, common title of the merged columns 
        public void AddMergedColumns(int[] columnsIndexes, string title)
        {
            MergedColumns.AddRange(columnsIndexes);
            StartColumns.Add(columnsIndexes[0], columnsIndexes.Length);
            Titles.Add(columnsIndexes[0], title);
        }
    }

    private MergedColumnsInfo info
    {
        get
        {
            if (ViewState["info"] == null)
                ViewState["info"] = new MergedColumnsInfo();
            return (MergedColumnsInfo)ViewState["info"];
        }
    }

    private MergedColumnsInfo info1
    {
        get
        {
            if (ViewState["info1"] == null)
                ViewState["info1"] = new MergedColumnsInfo();
            return (MergedColumnsInfo)ViewState["info1"];
        }
    }

    private void RenderHeader(HtmlTextWriter output, Control container)
    {

        for (int i = 0; i < container.Controls.Count - 1; i++)
        {
            TableCell cell = (TableCell)container.Controls[i];
            //stretch non merged columns for two rows
            if (!info.MergedColumns.Contains(i))
            {
                cell.Attributes["rowspan"] = "2";


                cell.RenderControl(output);
            }
            else //render merged columns common title
                if (info.StartColumns.Contains(i))
                {


                    output.Write(string.Format("<th align='center' style='border:1px solid #000;border-collapse:collapse;background:url(../Images/gridheaderbg-image.png) left 0px repeat-x'  colspan='{0}'>{1}</th>",

                             info.StartColumns[i], info.Titles[i]));


                }
        }

        //close the first row	
        output.RenderEndTag();
        //set attributes for the second row
        //grid.HeaderStyle.AddAttributesToRender(output);
        //start the second row
        output.RenderBeginTag("tr");



        //render the second row (only the merged columns)
        for (int i = 0; i < info.MergedColumns.Count; i++)
        {
            //if qtn eval 

            TableCell cell = (TableCell)container.Controls[info.MergedColumns[i]];

            cell.CssClass = "Rating-HeaderStyle-css";
            cell.RenderControl(output);

        }


        info.MergedColumns.Clear();
        info.StartColumns.Clear();
        info.Titles.Clear();


    }
    private void RenderHeader1(HtmlTextWriter output, Control container)
    {

        for (int i = 0; i < container.Controls.Count - 1; i++)
        {
            TableCell cell = (TableCell)container.Controls[i];
            //stretch non merged columns for two rows
            if (!info1.MergedColumns.Contains(i))
            {
                cell.Attributes["rowspan"] = "2";


                cell.RenderControl(output);
            }
            else //render merged columns common title
                if (info1.StartColumns.Contains(i))
                {


                    output.Write(string.Format("<th align='center' style='border:1px solid #000;border-collapse:collapse;background:url(../Images/gridheaderbg-image.png) left 0px repeat-x'  colspan='{0}'>{1}</th>",
                             info1.StartColumns[i], info1.Titles[i]));


                }
        }

        //close the first row	
        output.RenderEndTag();
        //set attributes for the second row
        //grid.HeaderStyle.AddAttributesToRender(output);
        //start the second row
        output.RenderBeginTag("tr");



        //render the second row (only the merged columns)
        for (int i = 0; i < info1.MergedColumns.Count; i++)
        {
            //if qtn eval 

            TableCell cell = (TableCell)container.Controls[info1.MergedColumns[i]];

            cell.CssClass = "Rating-HeaderStyle-css";
            cell.RenderControl(output);

        }


        info1.MergedColumns.Clear();
        info1.StartColumns.Clear();
        info1.Titles.Clear();


    }
    #endregion
    public void BindCategoryRating(string InspectionID, string CheckListIDS)
    {
        try
        {
            ViewState["info"] = null;
            dsCat = objInsp.GetCategoryRating(InspectionID);
           
            if (dsCat.Tables[0].Rows.Count > 0)
            {
                grdCatRating.DataSource = dsCat.Tables[0];
                grdCatRating.DataBind();




                    //updSubCat.Update();



                    DataSet dsRatingByVal = new DataSet();
                    dsRatingByVal.Clear();
                    for (int i = 0; i < grdCatRating.Rows.Count; i++)
                    {
                        if (grdCatRating.Rows[i].Cells[4].Text != "&nbsp;" && grdCatRating.Rows[i].Cells[4].Text.Trim() != "")
                        {
                            dsRatingByVal = objInsp.GetRatingsByValue(Convert.ToString(Math.Round(Convert.ToDecimal(grdCatRating.Rows[i].Cells[4].Text))));
                            if (dsRatingByVal.Tables[0].Rows.Count > 0)
                            {
                                Label lblRate = (Label)grdCatRating.Rows[i].Cells[5].FindControl("lblCatRating");
                                if (lblRate.Text == dsRatingByVal.Tables[0].Rows[0][1].ToString())
                                {
                                    grdCatRating.Rows[i].Cells[5].Attributes.Add("style","background-color:"+dsRatingByVal.Tables[0].Rows[0][3].ToString());
                                }
                            }
                        }
                    }

                    grdCatRating.FooterRow.Cells[2].Text = dsCat.Tables[1].Rows[0][2].ToString();
                    grdCatRating.FooterRow.Cells[3].Text = dsCat.Tables[1].Rows[0][3].ToString();
                    grdCatRating.FooterRow.Cells[4].Text = dsCat.Tables[1].Rows[0][4].ToString();
                    grdCatRating.FooterRow.Cells[5].Text = dsCat.Tables[1].Rows[0][5].ToString();

                    grdCatRating.FooterRow.Cells[2].HorizontalAlign = HorizontalAlign.Left;
                    grdCatRating.FooterRow.Cells[3].HorizontalAlign = HorizontalAlign.Center;
                    grdCatRating.FooterRow.Cells[4].HorizontalAlign = HorizontalAlign.Center;
                    grdCatRating.FooterRow.Cells[5].HorizontalAlign = HorizontalAlign.Center;
                  
                    updCat.Update();


            }
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }
    public void BindSubCategoryRating(string ParentCode,string InspectionID)
    {

        try
        {
            ViewState["info1"] = null;
          
            dsSubCat = new DataSet();
            dsSubCat = objInsp.GetSubCategoryRating(ParentCode, InspectionID);





            if (dsSubCat.Tables[0].Rows.Count > 0)
            {

                grdSubCatRating.DataSource = dsSubCat.Tables[0];
                grdSubCatRating.DataBind();



                DataSet dsRatingByVal = new DataSet();
                dsRatingByVal.Clear();
                for (int i = 0; i < grdSubCatRating.Rows.Count; i++)
                {
                    DropDownList ddlRating = (DropDownList)grdSubCatRating.Rows[i].Cells[5].FindControl("ddlRating");
                    Label lblRating = (Label)grdSubCatRating.Rows[i].Cells[6].FindControl("lblSubCatRating");
                    if (ddlRating.SelectedItem.Text != "&nbsp;" && ddlRating.SelectedItem.Text.Trim() != "" && ddlRating.SelectedItem.Text.Trim() != "--SELECT--")
                    {
                        dsRatingByVal = objInsp.GetRatingsByValue(Convert.ToString(Math.Round(Convert.ToDecimal(ddlRating.SelectedItem.Text))));

                        if (dsRatingByVal.Tables[0].Rows.Count > 0)
                        {
                            if (lblRating.Text == dsRatingByVal.Tables[0].Rows[0][1].ToString())
                            {
                                grdSubCatRating.Rows[i].Cells[6].BackColor = Color.FromName(dsRatingByVal.Tables[0].Rows[0][3].ToString());

                            }
                        }
                    }
                    ddlRating.CssClass = "rpt" + ((HiddenField)grdSubCatRating.Rows[i].Cells[5].FindControl("hdnSubCatCode")).Value;
                }


            }
           
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }

    protected void grdSubCatRating_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DropDownList ddlRating = (e.Row.FindControl("ddlRating") as DropDownList);
                dsRating.Clear();
                dsRating = objInsp.GetRatings();
                ddlRating.DataSource = dsRating.Tables[0];
                ddlRating.DataTextField = "RatingValue";
                ddlRating.DataValueField = "Rating";
                ddlRating.DataBind();
                if ((e.Row.FindControl("hdnThisReport") as HiddenField).Value != "--SELECT--" && (e.Row.FindControl("hdnThisReport") as HiddenField).Value!="")
                {
                    decimal value = Math.Round(UDFLib.ConvertToDecimal((e.Row.FindControl("hdnThisReport") as HiddenField).Value), 0);
                    string FindVal = UDFLib.ConvertStringToNull(value)  ;
                    if (ddlRating.Items.FindByText(FindVal) != null)
                    {
                        ddlRating.Items.FindByText(FindVal).Selected = true;
                    }
                }
                if ((e.Row.FindControl("hdnThisReport") as HiddenField).Value == "--SELECT--" || (e.Row.FindControl("hdnThisReport") as HiddenField).Value == "")
                {
                   // decimal value = Math.Round(UDFLib.ConvertToDecimal((e.Row.FindControl("hdnThisReport") as HiddenField).Value), 0);
                    //string FindVal = UDFLib.ConvertStringToNull(value) + ".00";
                    ddlRating.Items.FindByText("--SELECT--").Selected = true;
                }
                int JobCount = objInsp.INSP_Get_WorklistJobsCountByLocationID(Convert.ToInt32(ViewState["InspectionID"].ToString()), Convert.ToInt32((e.Row.Cells[1].FindControl("hdnSubCatCode") as HiddenField).Value));

               //(e.Row.Cells[8].FindControl("lblJobCount") as Label).Attributes.Add("data-badge",Convert.ToString(JobCount));
               (e.Row.Cells[8].FindControl("lnkAssignChecklist") as LinkButton).Attributes.Add("data-badge", Convert.ToString(JobCount));
               
              
            }

            if (e.Row.RowType == DataControlRowType.Footer)
            {
               e.Row.Cells[2].Text = dsSubCat.Tables[1].Rows[0][2].ToString();
               e.Row.Cells[3].Text = dsSubCat.Tables[1].Rows[0][3].ToString();
               e.Row.Cells[4].Text = dsSubCat.Tables[1].Rows[0][4].ToString();
               e.Row.Cells[5].Text = dsSubCat.Tables[1].Rows[0][5].ToString();
               e.Row.Cells[6].Text = dsSubCat.Tables[1].Rows[0][6].ToString();
              
               e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Left;
               e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Center;
               e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Center;
               e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Center;
               e.Row.Cells[6].HorizontalAlign = HorizontalAlign.Center;

            }


          
            if (e.Row.RowType == DataControlRowType.Header)
            {
              //  info1.AddMergedColumns(new int[] { 3, 4, 5, 6,7,8,9}, "Rating");
            }

            if (e.Row.RowType == DataControlRowType.EmptyDataRow)
            {
               
            }
          
            
        }
        catch (Exception ex)
        {
            throw ex;
        }
       
       
    }
    protected void grdCatRating_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (Convert.ToInt32(hdnDirtyCounter.Value) > 0)
        {
           

            string confirmValue = hdnConfirm.Value;
            if (confirmValue == "Yes")
            {
                SaveRatings();
            }
            else
            {
                
            }
        }
        
        int rowindex= Convert.ToInt32(e.CommandArgument.ToString());
        ViewState["SelectedRowIndex"] = rowindex;
        grdCatRating.SelectRow(rowindex);

        //grdCatRating.Rows[rowindex].BackColor = Color.Coral;

        string SystemCode = ((HiddenField)grdCatRating.Rows[rowindex].FindControl("hdnCatCode")).Value;
        ViewState["CategoryCode"] = ((HiddenField)grdCatRating.Rows[rowindex].FindControl("hdnCatCode")).Value;

     
        BindSubCategoryRating(SystemCode, ViewState["InspectionID"].ToString());
        if (grdSubCatRating.Rows.Count > 0)
        {
            pnlAddRemark.Visible = true;
            pnlSubcategory.Visible = true;
            grdSubCatRating.Visible = true;
            txtAddRemarks.Visible = true;
            //grdSubCatRating.Style.Add("display", "inline");
            ((Label)grdSubCatRating.HeaderRow.Cells[0].FindControl("lblCatNo")).Text = ((HiddenField)grdCatRating.Rows[rowindex].FindControl("hdnCatRNo")).Value;
            ((Label)grdSubCatRating.HeaderRow.Cells[1].FindControl("lblCatDesc")).Text = ((HiddenField)grdCatRating.Rows[rowindex].FindControl("hdnCatDesc")).Value;
     
        }
        else
        {
           // grdSubCatRating.Dispose();
            grdSubCatRating.Visible = false;
            pnlAddRemark.Visible = false;
            pnlSubcategory.Visible = false;
      
         
            
        }



       

        
       
        hdnCurrentCatNo.Value = ((HiddenField)grdCatRating.Rows[rowindex].FindControl("hdnCatRNo")).Value;
        hdnCurrentCatDesc.Value = ((HiddenField)grdCatRating.Rows[rowindex].FindControl("hdnCatDesc")).Value;
           
        txtAddRemarks.Text = ((HiddenField)grdCatRating.Rows[rowindex].FindControl("hdnAddRemaks")).Value;
        
    }
    protected void grdSubCatRating_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (Convert.ToInt32(hdnDirtyCounter.Value) > 0)
        {

            SaveRatings();
         
        }
        if (e.CommandName == "AddDefect")
        {
            int LocationID = Convert.ToInt32(e.CommandArgument.ToString().Split(',')[0].ToString());
            int InspectionID = Convert.ToInt32(e.CommandArgument.ToString().Split(',')[1].ToString());
            int VesselID = Convert.ToInt32(e.CommandArgument.ToString().Split(',')[2].ToString());

            string js = "AddDefect(" + LocationID + "," + InspectionID + "," + VesselID + ");";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "AddDefect", js, true);
        }
        if (e.CommandName.ToUpper() == "ASSIGN")
        {
           
            grdJoblist.DataSource = null;
            grdJoblist.DataBind();

            int ScheduleID = UDFLib.ConvertToInteger(Request.QueryString["ScheduleID"].ToString());
            int Length = UDFLib.ConvertToInteger(Request.QueryString["To"].ToString().Length);
            int SchDetailId = UDFLib.ConvertToInteger(Request.QueryString["InspID"].ToString());
            int SubCatCode = Convert.ToInt32(e.CommandArgument.ToString().Trim());
            ViewState["SubCatCode"] = Convert.ToInt32(e.CommandArgument.ToString().Trim());
            DataSet ds = objInsp.Get_Schedule_Details(ScheduleID, Convert.ToInt32(Session["USERID"]), SchDetailId, SubCatCode);

            DataTable dtInpectionSchedule = ds.Tables[2];
            DataTable dtInspectSchOther = ds.Tables[3];
            ViewState["ScheduleID"] = ScheduleID;
            ViewState["SchDetailId"] = SchDetailId;
            ViewState["ShowImages"] = UDFLib.ConvertToInteger(Request.QueryString["ShowImages"].ToString());
            Session["dtInpectionSchedule"] = dtInpectionSchedule;
            Session["dtInspectSchOther"] = dtInspectSchOther;
            ViewState["VID"] = ds.Tables[0].Rows[0]["VESSEL_ID"];
            Search_Worklist();
            string js = " showModal('dvWorklist');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "hideModalJS", js, true);




            
            //if (Length == 0)
            //{
            //    btnAssign.Enabled = true;
            //    btnAssignandClose.Enabled = true;


            //}
            //else
            //{
            //    btnAssign.Enabled = false;
            //    btnAssignandClose.Enabled = false;
            //}

        }
    }
    protected void UpdateWorklistChecklist()
    {
        DataTable dtInpectionSchedule = ((DataTable)Session["dtInpectionSchedule"]);

        int InspDetId = 0;

        foreach (GridViewRow item in grdJoblist.Rows)
        {

            if (dtInpectionSchedule != null)
            {
                if (dtInpectionSchedule.Rows.Count > 0)
                {
                    DataRow[] dr = dtInpectionSchedule.Select("WORKLIST_ID='" + grdJoblist.DataKeys[item.RowIndex][0].ToString() + "' and VESSEL_ID='" + grdJoblist.DataKeys[item.RowIndex][1].ToString() + "' and OFFICE_ID='" + grdJoblist.DataKeys[item.RowIndex][2].ToString() + "'");
                    if (((CheckBox)grdJoblist.Rows[item.RowIndex].FindControl("checkRow")).Checked)
                    {
                        if (dr.Length > 0)
                        {


                            dr[0]["WORKLIST_ID"] = grdJoblist.DataKeys[item.RowIndex][0].ToString();
                            dr[0]["VESSEL_ID"] = grdJoblist.DataKeys[item.RowIndex][1].ToString();
                            dr[0]["OFFICE_ID"] = grdJoblist.DataKeys[item.RowIndex][2].ToString();
                            dr[0]["InspectionDetailId"] = ViewState["InspectionID"];
                            dr[0]["LocationID"] = ViewState["SubCatCode"];
                        }
                        else
                        {
                            dtInpectionSchedule.Rows.Add(grdJoblist.DataKeys[item.RowIndex][0], grdJoblist.DataKeys[item.RowIndex][1], grdJoblist.DataKeys[item.RowIndex][2], ViewState["InspectionID"], ViewState["SubCatCode"]);
                        }
                    }
                    else
                    {
                        if (dr.Length > 0)
                        {
                            dtInpectionSchedule.Rows.Remove(dr[0]);
                        }


                    }
                }
                else
                {
                    if (((CheckBox)grdJoblist.Rows[item.RowIndex].FindControl("checkRow")).Checked)
                    {
                        dtInpectionSchedule.Rows.Add(grdJoblist.DataKeys[item.RowIndex][0], grdJoblist.DataKeys[item.RowIndex][1], grdJoblist.DataKeys[item.RowIndex][2], ViewState["InspectionID"], ViewState["SubCatCode"]);
                    }

                }
            }

        }




        Session["dtInpectionSchedule"] = dtInpectionSchedule;
    }

    protected void Search_Worklist()
    {
        try
        {
            UpdateWorklistChecklist();
            DataTable dtFilter = new DataTable();
            dtFilter.Columns.Add("PRM_NAME", typeof(string));
            dtFilter.Columns.Add("PRM_VALUE", typeof(object));

            DataTable dtStatus = new DataTable();
            dtStatus.Columns.Add("All", typeof(int));
            dtStatus.Columns.Add("Pending", typeof(int));
            dtStatus.Columns.Add("Completed", typeof(int));
            dtStatus.Columns.Add("Reworked", typeof(int));
            dtStatus.Columns.Add("Verified", typeof(int));
            dtStatus.Columns.Add("Overdue", typeof(int));

            //string JobStaus ="";

            dtStatus.Rows.Add(rblJobStaus.Items[0].Selected == true ? 1 : 0,
                rblJobStaus.Items[1].Selected == true ? 1 : 0,
                rblJobStaus.Items[2].Selected == true ? 1 : 0,
                rblJobStaus.Items[3].Selected == true ? 1 : 0,
                rblJobStaus.Items[4].Selected == true ? 1 : 0,
                rblJobStaus.Items[5].Selected == true ? 1 : 0);


            dtFilter.Rows.Add(new object[] { "@FLEET_ID", UDFLib.ConvertIntegerToNull(null) });
            dtFilter.Rows.Add(new object[] { "@VESSEL_ID", UDFLib.ConvertIntegerToNull(Request.QueryString["VesselID"].ToString()) });
            dtFilter.Rows.Add(new object[] { "@ASSIGNOR", UDFLib.ConvertIntegerToNull(null) });
            dtFilter.Rows.Add(new object[] { "@DEPT_SHIP", UDFLib.ConvertIntegerToNull(null) });
            dtFilter.Rows.Add(new object[] { "@DEPT_OFFICE", UDFLib.ConvertIntegerToNull(null) });
            dtFilter.Rows.Add(new object[] { "@PRIORITY", UDFLib.ConvertIntegerToNull(null) });
            dtFilter.Rows.Add(new object[] { "@CATEGORY_NATURE", UDFLib.ConvertIntegerToNull(null) });
            dtFilter.Rows.Add(new object[] { "@CATEGORY_PRIMARY", UDFLib.ConvertIntegerToNull(null) });
            dtFilter.Rows.Add(new object[] { "@CATEGORY_SECONDARY", UDFLib.ConvertIntegerToNull(null) });
            dtFilter.Rows.Add(new object[] { "@CATEGORY_MINOR", null, });
            dtFilter.Rows.Add(new object[] { "@JOB_DESCRIPTION", UDFLib.ConvertStringToNull(null) });
            dtFilter.Rows.Add(new object[] { "@JOB_STATUS", UDFLib.ConvertStringToNull(rblJobStaus.SelectedValue) });

            dtFilter.Rows.Add(new object[] { "@dtJOB_Status", dtStatus });

            dtFilter.Rows.Add(new object[] { "@JOB_TYPE", UDFLib.ConvertIntegerToNull(null) });
          
            dtFilter.Rows.Add(new object[] { "@PIC", UDFLib.ConvertIntegerToNull(null) });
            dtFilter.Rows.Add(new object[] { "@JOB_MODIFIED_IN", UDFLib.ConvertIntegerToNull(null) });
            dtFilter.Rows.Add(new object[] { "@DATE_RAISED_FROM", UDFLib.ConvertDateToNull(null) });
            dtFilter.Rows.Add(new object[] { "@DATE_RAISED_TO", UDFLib.ConvertDateToNull(null) });
            dtFilter.Rows.Add(new object[] { "@DATE_CMPLTN_FROM", UDFLib.ConvertDateToNull(null) });
            dtFilter.Rows.Add(new object[] { "@DATE_CMPLTN_TO", UDFLib.ConvertDateToNull(null) });
            dtFilter.Rows.Add(new object[] { "@DEFER_TO_DD", (null) });
            dtFilter.Rows.Add(new object[] { "@SENT_TO_SHIP", (null) });
            dtFilter.Rows.Add(new object[] { "@HAVING_REQ_NO", (null) });
            dtFilter.Rows.Add(new object[] { "@FLAGGED_FOR_MEETING", (null) });
            dtFilter.Rows.Add(new object[] { "@INSPECTOR", UDFLib.ConvertIntegerToNull(null) });
            dtFilter.Rows.Add(new object[] { "@PAGE_INDEX", ucCustomPagerctp.CurrentPageIndex });
            dtFilter.Rows.Add(new object[] { "@PAGE_SIZE", ucCustomPagerctp.PageSize });

            int Record_Count = 0;

            DataTable taskTable = objInsp.Get_WorkList_Index(dtFilter, ref Record_Count);

            grdJoblist.DataSource = taskTable;
            grdJoblist.DataBind();

            ucCustomPagerctp.CountTotalRec = Record_Count.ToString();
            ucCustomPagerctp.BuildPager();

            DataTable dtPKIDs = taskTable.DefaultView.ToTable(true, new string[] { "WORKLIST_ID", "VESSEL_ID", "OFFICE_ID" });
            dtPKIDs.PrimaryKey = new DataColumn[] { dtPKIDs.Columns["WORKLIST_ID"], dtPKIDs.Columns["VESSEL_ID"], dtPKIDs.Columns["OFFICE_ID"] };
            Session["WORKLIST_PKID_NAV"] = dtPKIDs;

            lblRecordCount.Text = Record_Count.ToString();

        }
        catch (Exception ex)
        {
            ////.WriteError(this.GetType().Name.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), ex);
            string js = "alert('Error in loading data!! Error: " + UDFLib.ReplaceSpecialCharacter(ex.Message) + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", js, true);
        }
    }
    protected void ddlRating_SelectedIndexChanged(object sender, EventArgs e)
    {
        var ddlRate = (DropDownList)sender;
        var row = (GridViewRow)ddlRate.NamingContainer;
        //get the Id of the row
        //var Id = Convert.ToInt32(((DropDownList)row.FindControl("ddlRating")).Text);

        ((Label)row.FindControl("lblSubCatRating")).Text = ddlRate.SelectedValue.ToString().Split('_')[0];
      hdnDirtyCounter.Value = "1";

    }

    protected void BtnSaveRating_Click(object sender, EventArgs e)
    {

        SaveRatings();

    }

    public void SaveRatings()
    {
        try
        {
            bool indefect = false;
            for (int i = 0; i < grdCatRating.Rows.Count; i++)
            {
                string SystemCode = ((HiddenField)grdCatRating.Rows[i].FindControl("hdnCatCode")).Value;
                string SystemDescription = grdCatRating.Rows[i].Cells[2].Text;
                string SysLastReport = grdCatRating.Rows[i].Cells[3].Text;
                if (SysLastReport == "&nbsp;")
                {
                    SysLastReport = "";
                }
                string SysCurrentReport = grdCatRating.Rows[i].Cells[4].Text;
                if (SysCurrentReport == "&nbsp;")
                {
                    SysCurrentReport = "";

                }
                string SysRating = "";
                string ScheduleID = "";
                string InspectionID = ViewState["InspectionID"].ToString();

                string CreatedBy = "1";
                string ActiveStatus = "1";

                objInsp.InsertCategoryRating(SystemCode, SysLastReport, SysCurrentReport, SysRating, ScheduleID, InspectionID, CreatedBy, DateTime.Now, ActiveStatus);

            }

            for (int i = 0; i < grdSubCatRating.Rows.Count; i++)
            {
                string SystemCode = ViewState["CategoryCode"].ToString();
                string SubSystemCode = ((HiddenField)grdSubCatRating.Rows[i].FindControl("hdnSubCatCode")).Value;
                string SubSystemDescription = ((Label)grdSubCatRating.Rows[i].FindControl("lblSubCatDesc")).Text;
                string SubSysSecLastReport = grdSubCatRating.Rows[i].Cells[3].Text;
                if (SubSysSecLastReport == "&nbsp;")
                {
                    SubSysSecLastReport = "";
                }
                string SubSysLastReport = grdSubCatRating.Rows[i].Cells[4].Text;
                if (SubSysLastReport == "&nbsp;")
                {
                    SubSysLastReport = "";
                }
                string SubSysCurrentReport = ((DropDownList)grdSubCatRating.Rows[i].FindControl("ddlRating")).SelectedItem.Text;
                if (SubSysCurrentReport == "--SELECT--")
                {
                    SubSysCurrentReport = "";
                }
                string SubSysRating = ((DropDownList)grdSubCatRating.Rows[i].FindControl("ddlRating")).SelectedValue.ToString().Split('_')[0];
                string ScheduleID = "";
                string AdditionalRemarks = txtAddRemarks.Text;
                string InspectionID = ViewState["InspectionID"].ToString();
                string Remarks = ((TextBox)grdSubCatRating.Rows[i].FindControl("txtRemarks")).Text;
                string CreatedBy = "1";
                string ActiveStatus = "1";


                int inDefect = objInsp.INSP_Get_WorklistReportByLocationID(Convert.ToInt32(ViewState["InspectionID"].ToString()), Convert.ToInt32(SubSystemCode));
                if (inDefect == 1)
                {
                    int Rating = objInsp.INSP_Get_RatingsByRating("Fair");
                    if (SubSysCurrentReport != "")
                    {
                        if (Convert.ToInt32(SubSysCurrentReport) >= Rating)
                        {
                            string js1 = "alert('Item has defect can not be rated Fair and Above'); ";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert1", js1, true);


                            BindSubCategoryRating(ViewState["CategoryCode"].ToString(), ViewState["InspectionID"].ToString());
                            ((TextBox)grdSubCatRating.Rows[i].FindControl("txtRemarks")).Text = Remarks;
                            ((Label)grdSubCatRating.HeaderRow.Cells[0].FindControl("lblCatNo")).Text = hdnCurrentCatNo.Value;//((HiddenField)grdCatRating.Rows[rowindex].FindControl("hdnCatRNo")).Value;
                            ((Label)grdSubCatRating.HeaderRow.Cells[1].FindControl("lblCatDesc")).Text = hdnCurrentCatDesc.Value;//((HiddenField)grdCatRating.Rows[rowindex].FindControl("hdnCatDesc")).Value;

                            ((DropDownList)grdSubCatRating.Rows[i].FindControl("ddlRating")).Focus();


                            indefect = true;
                        }
                        else
                        {
                            objInsp.InsertSubCategoryRating(SystemCode, SubSystemCode, SubSysSecLastReport, SubSysLastReport, SubSysCurrentReport, SubSysRating, Remarks, AdditionalRemarks, ScheduleID, InspectionID, CreatedBy, DateTime.Now, ActiveStatus);


                        }
                    }
                    else
                    {
                        objInsp.InsertSubCategoryRating(SystemCode, SubSystemCode, SubSysSecLastReport, SubSysLastReport, SubSysCurrentReport, SubSysRating, Remarks, AdditionalRemarks, ScheduleID, InspectionID, CreatedBy, DateTime.Now, ActiveStatus);
                    }
                }
                else
                {
                    objInsp.InsertSubCategoryRating(SystemCode, SubSystemCode, SubSysSecLastReport, SubSysLastReport, SubSysCurrentReport, SubSysRating, Remarks, AdditionalRemarks, ScheduleID, InspectionID, CreatedBy, DateTime.Now, ActiveStatus);
                   
                
                }
            }

            if (indefect == false)
            {
                //pnlAddRemark.Visible = false;

               // grdSubCatRating.Visible = false;

                BindCategoryRating(ViewState["InspectionID"].ToString(), ViewState["CheckListIDs"].ToString());
                BindSubCategoryRating(ViewState["CategoryCode"].ToString(), ViewState["InspectionID"].ToString());

              
                updCat.Update();
                hdnDirtyCounter.Value = "0";
                //string js2 = "alert('Rating Saved Successfully');";
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "alert9", js2, true);
            }

            int rowindex = UDFLib.ConvertToInteger(ViewState["SelectedRowIndex"].ToString());
            grdCatRating.SelectRow(rowindex);


            if (grdSubCatRating.Rows.Count > 0)
            {
                pnlAddRemark.Visible = true;
                pnlSubcategory.Visible = true;
                grdSubCatRating.Visible = true;
                txtAddRemarks.Visible = true;
                //grdSubCatRating.Style.Add("display", "inline");
                ((Label)grdSubCatRating.HeaderRow.Cells[0].FindControl("lblCatNo")).Text = ((HiddenField)grdCatRating.Rows[rowindex].FindControl("hdnCatRNo")).Value;
                ((Label)grdSubCatRating.HeaderRow.Cells[1].FindControl("lblCatDesc")).Text = ((HiddenField)grdCatRating.Rows[rowindex].FindControl("hdnCatDesc")).Value;

            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void grdCatRating_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
              //  info.AddMergedColumns(new int[] { 3, 4, 5, 6, 7 }, "Rating");
            }


            Label lblCatRating = (Label)e.Row.FindControl("lblCatRating");
           // System.Web.UI.WebControls.Image ImbCOCModified = (System.Web.UI.WebControls.Image)e.Row.FindControl("ImbCOCModified");
            if (lblCatRating != null)
            {
                if (DataBinder.Eval(e.Row.DataItem, "ThisReport").ToString() != "")
                {
                    if (Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "ThisReport")) > 5)
                    {
                        lblCatRating.Attributes.Add("Title", "cssbody=[dvbdy1] cssheader=[dvhdr1] header=[Ratings] body=[Category average is above 5]");
                    }

                }
               
                    
                
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void grdCatRating_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
         //   e.Row.SetRenderMethodDelegate(RenderHeader);

        }
    }
    protected void grdSubCatRating_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
          //  e.Row.SetRenderMethodDelegate(RenderHeader1);

        }
    }

    protected void grdJoblist_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }
    protected void btnAssign_Click(object sender, EventArgs e)
    {

        UpdateWorklistChecklist();
        DataTable dtInpectionSchedule = ((DataTable)Session["dtInpectionSchedule"]);
        objInsp.Save_InspectionWorklist(dtInpectionSchedule, Convert.ToInt32(Session["USERID"]));
      //  Load_Current_Schedules();
     
        string js = "alert('Worklist Assigned Successfully!');";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertjs", js, true);
    }
    protected void btnAssignandClose_Click(object sender, EventArgs e)
    {

        UpdateWorklistChecklist();
        DataTable dtInpectionSchedule = ((DataTable)Session["dtInpectionSchedule"]);

        if (dtInpectionSchedule.Rows.Count == 0)
        {
            DataRow lRow = dtInpectionSchedule.NewRow();
            lRow["WORKLIST_ID"] = -1;
            lRow["InspectionDetailId"] = ViewState["InspectionID"];
            lRow["LocationID"] = ViewState["SubCatCode"];
            dtInpectionSchedule.Rows.Add(lRow);
        }

        objInsp.Save_InspectionWorklist(dtInpectionSchedule, Convert.ToInt32(Session["USERID"]));
        string js = "alert('Worklist Assigned Successfully!');";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertjs", js, true);
        //Load_Current_Schedules();
        BindCategoryRating(ViewState["InspectionID"].ToString(), ViewState["CheckListIDs"].ToString());
        BindSubCategoryRating(ViewState["CategoryCode"].ToString(), ViewState["InspectionID"].ToString());
        if (grdSubCatRating.Rows.Count > 0)
        {
            int rowindex = UDFLib.ConvertToInteger(ViewState["SelectedRowIndex"]);


            pnlAddRemark.Visible = true;
            pnlSubcategory.Visible = true;
            grdSubCatRating.Visible = true;
            txtAddRemarks.Visible = true;
            //grdSubCatRating.Style.Add("display", "inline");
            ((Label)grdSubCatRating.HeaderRow.Cells[0].FindControl("lblCatNo")).Text = ((HiddenField)grdCatRating.Rows[rowindex].FindControl("hdnCatRNo")).Value;
            ((Label)grdSubCatRating.HeaderRow.Cells[1].FindControl("lblCatDesc")).Text = ((HiddenField)grdCatRating.Rows[rowindex].FindControl("hdnCatDesc")).Value;

        }
        else
        {
            // grdSubCatRating.Dispose();
            grdSubCatRating.Visible = false;
            pnlAddRemark.Visible = false;
            pnlSubcategory.Visible = false;



        }
        updCat.Update();
       
        hdnDirtyCounter.Value = "0";
        js = "hideModal('dvWorklist');";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "hideModalJS", js, true);
    }
    protected void grdJoblist_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string AttachmentCount = DataBinder.Eval(e.Row.DataItem, "AttachmentCount").ToString();
            string Worklist_ID = DataBinder.Eval(e.Row.DataItem, "Worklist_ID").ToString();
            string Vessel_ID = DataBinder.Eval(e.Row.DataItem, "Vessel_ID").ToString();
            string WL_Office_ID = DataBinder.Eval(e.Row.DataItem, "Office_ID").ToString();

            ImageButton imgRemarks = (ImageButton)e.Row.FindControl("imgRemarks");
            if (imgRemarks != null)
            {
                imgRemarks.Attributes.Add("onmouseover", "showFollowups(" + Vessel_ID + "," + Worklist_ID + "," + WL_Office_ID + ",this)");
                imgRemarks.Attributes.Add("onmouseout", "closeDiv('dialog')");
            }

            System.Web.UI.WebControls.Image ImgAttachment = (System.Web.UI.WebControls.Image)(e.Row.FindControl("ImgAttachment"));
            if (ImgAttachment != null)
            {
                if (AttachmentCount == "0")
                    ImgAttachment.Visible = false;
                else
                    ImgAttachment.Attributes.Add("onclick", "showDialog('Attachments.aspx?vid=" + Vessel_ID + "&wlid=" + Worklist_ID + "&wl_off_id=" + WL_Office_ID + "');");

            }
            //if (!((CheckBox)e.Row.FindControl("checkRow")).Checked)
            //{
            //    e.Row.Visible = false;
            //}
        }

    }
    protected void grdJoblist_Sorting(object sender, GridViewSortEventArgs e)
    {
        try
        {
            hdnFlagCheck.Value = "false";
            //Retrieve the table from the session object.
            DataTable dt = Session["TaskTable"] as DataTable;
            if (dt != null)
            {
                //Sort the data.
                dt.DefaultView.Sort = e.SortExpression + " " + GetSortDirection(e.SortExpression);
                grdJoblist.DataSource = Session["TaskTable"];
                grdJoblist.DataBind();
            }
        }
        catch (Exception)
        {
            ////.WriteError(this.GetType().Name.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), ex);
        }
    }
    private string GetSortDirection(string column)
    {
        // By default, set the sort direction to ascending.
        string sortDirection = "ASC";

        // Retrieve the last column that was sorted.
        string sortExpression = ViewState["SortExpression"] as string;

        if (sortExpression != null)
        {
            // Check if the same column is being sorted.
            // Otherwise, the default value can be returned.
            if (sortExpression == column)
            {
                string lastDirection = ViewState["SortDirection"] as string;
                if ((lastDirection != null) && (lastDirection == "ASC"))
                {
                    sortDirection = "DESC";
                }
            }
        }

        // Save new values in ViewState.
        ViewState["SortDirection"] = sortDirection;
        ViewState["SortExpression"] = column;

        return sortDirection;
    }
    protected bool SelectCheckbox(string WORKLIST_ID, string VESSEL_ID, string OFFICE_ID)
    {
        DataTable dtInpectionSchedule = ((DataTable)Session["dtInpectionSchedule"]);
      

        if (dtInpectionSchedule != null)
            if (dtInpectionSchedule.Rows.Count > 0)
                if (dtInpectionSchedule.Select("WORKLIST_ID='" + WORKLIST_ID + "' and VESSEL_ID='" + VESSEL_ID + "' and OFFICE_ID='" + OFFICE_ID + "'").Length > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                   
                }
        return false;

    }
    protected bool EnableCheckbox(string WORKLIST_ID, string VESSEL_ID, string OFFICE_ID)
    {
      
        DataTable dtInspectSchOther = ((DataTable)Session["dtInspectSchOther"]);

        if (dtInspectSchOther != null)
            if (dtInspectSchOther.Rows.Count > 0)
                if (dtInspectSchOther.Select("WORKLIST_ID='" + WORKLIST_ID + "' and VESSEL_ID='" + VESSEL_ID + "' and OFFICE_ID='" + OFFICE_ID + "'").Length > 0)
                {
                    return false;
                }
                else
                {
                    return true;

                }
        return true;

    }
    protected void grdJoblist_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdJoblist.PageIndex = e.NewPageIndex;

        // Search_Worklist();
        //lblPageStatus.Text = (GridView1.PageIndex + 1).ToString() + " of " + GridView1.PageCount.ToString();
    }
    protected void btnGenerateReport_Click(object sender, EventArgs e)
    {
        // string js1 = "alert('../SupdtInspReport.aspx?ScheduleID=" + Convert.ToInt32(ViewState["ScheduleID"]) + ");";

        string url = "window.open('SupdtInspReport.aspx?SchDetailId=";
        url = url + Convert.ToInt32(ViewState["SchDetailId"]) + "&ShowImages=" + Convert.ToInt32(ViewState["ShowImages"]);
        url = url + "');";
        string js1 = url;
        ScriptManager.RegisterStartupScript(this, this.GetType(), "script1", js1, true);
    }
    protected void Filter_Changed(object sender, EventArgs e)
    {
        hdnFlagCheck.Value = "false";
        Search_Worklist();
    }
    protected void BtnTemp_Click(object sender, EventArgs e)
    {
       string InspectionID = UDFLib.ConvertStringToNull(ViewState["InspectionID"].ToString());
        string ParentCode= UDFLib.ConvertStringToNull(ViewState["CategoryCode"].ToString() );
        BindCategoryRating(InspectionID, ViewState["CheckListIDs"].ToString());
        BindSubCategoryRating(ParentCode, InspectionID);
        if (grdSubCatRating.Rows.Count > 0)
        {
            int rowindex = UDFLib.ConvertToInteger(ViewState["SelectedRowIndex"]);


            pnlAddRemark.Visible = true;
            pnlSubcategory.Visible = true;
            grdSubCatRating.Visible = true;
            txtAddRemarks.Visible = true;
            //grdSubCatRating.Style.Add("display", "inline");
            ((Label)grdSubCatRating.HeaderRow.Cells[0].FindControl("lblCatNo")).Text = ((HiddenField)grdCatRating.Rows[rowindex].FindControl("hdnCatRNo")).Value;
            ((Label)grdSubCatRating.HeaderRow.Cells[1].FindControl("lblCatDesc")).Text = ((HiddenField)grdCatRating.Rows[rowindex].FindControl("hdnCatDesc")).Value;

        }
        else
        {
            // grdSubCatRating.Dispose();
            grdSubCatRating.Visible = false;
            pnlAddRemark.Visible = false;
            pnlSubcategory.Visible = false;



        }
    }
}