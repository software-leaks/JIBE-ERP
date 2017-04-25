using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;
using SMS.Properties;
using SMS.Business.Infrastructure;
using SMS.Business.Inspection;
using SMS.Business.VET;
using System.IO;
using System.Configuration;

public partial class Technical_Vetting_Vetting_AssignJobs : System.Web.UI.Page
{
    DataSet dsCat = new DataSet();
    DataSet dsSubCat = new DataSet();
    DataSet dsRating = new DataSet();
    BLL_VET_Index objBLLIndx = new BLL_VET_Index();
    BLL_Tec_Inspection objInsp = new BLL_Tec_Inspection();
    UserAccess objUA = new UserAccess();
    BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
     string Observation_ID = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
        if (!IsPostBack)
        {
            hdnfldUserID.Value = Convert.ToString(Session["USERID"]);
            UserAccessValidation();           
        
            if (Request.QueryString["Observation_ID"] != null)
            {
                Observation_ID = Request.QueryString["Observation_ID"];
                ViewState["Observation_ID"] = Observation_ID;
            }

     
            string VesselID = "";
            if (Request.QueryString["Vessel_ID"] != null)
            {
                VesselID = Request.QueryString["Vessel_ID"];
            }
            if (VesselID != "")
            {
                hdnVesselID.Value = VesselID;
            }

            DataTable dtSelected = new DataTable();
            DataTable dtAllSelected = new DataTable();
            DataColumn columnwl = new DataColumn("WORKLIST_ID", typeof(int));
            DataColumn colVESSEL_ID = new DataColumn("VESSEL_ID", typeof(int));
            DataColumn columnof = new DataColumn("OFFICE_ID", typeof(int));
            DataColumn columnob = new DataColumn("Observation_ID", typeof(int));

            DataColumn columnwl1 = new DataColumn("WORKLIST_ID", typeof(int));
            DataColumn colVESSEL_ID1 = new DataColumn("VESSEL_ID", typeof(int));
            DataColumn columnof1 = new DataColumn("OFFICE_ID", typeof(int));
            DataColumn columnob1 = new DataColumn("Observation_ID", typeof(int));

            dtSelected.Columns.Add(columnwl);
            dtSelected.Columns.Add(colVESSEL_ID);
            dtSelected.Columns.Add(columnof);
            dtSelected.Columns.Add(columnob);
            dtSelected.PrimaryKey = new DataColumn[] { columnwl, columnof };



            dtAllSelected.Columns.Add(columnwl1);
            dtAllSelected.Columns.Add(colVESSEL_ID1);
            dtAllSelected.Columns.Add(columnof1);
            dtAllSelected.Columns.Add(columnob1);
            dtAllSelected.PrimaryKey = new DataColumn[] { columnwl1, columnof1 };

            ViewState["vsdtSelected_Items"] = dtSelected;
            ViewState["vsdtSelected_ALL_Items"] = dtAllSelected;

            Search_Worklist();
            SaveItemsSelection();
            Search_AllChecked_Worklist();

        }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }
    /// <summary>
    /// Method is used to save selected worklist job 
    /// </summary>
    protected void SaveItemsSelection()
    {
        try
        {
        DataTable dtSelected = (DataTable)ViewState["vsdtSelected_Items"];
        bool chk;
        int WORKLIST_ID = 0;
        int OFFICE_ID = 0;
        int VESSEL_ID = 0;
        int Observation_ID = 0;
        object[] arrKeys = new object[2];
        DataRow drItem;
        foreach (GridViewRow gr in grdJoblist.Rows)
        {
            WORKLIST_ID = Convert.ToInt32(grdJoblist.DataKeys[gr.RowIndex].Values["WORKLIST_ID"]);
            OFFICE_ID = Convert.ToInt32(grdJoblist.DataKeys[gr.RowIndex].Values["OFFICE_ID"]);
            VESSEL_ID = Convert.ToInt32(grdJoblist.DataKeys[gr.RowIndex].Values["VESSEL_ID"]);
            Observation_ID = Convert.ToInt32(ViewState["Observation_ID"].ToString());

            arrKeys[0] = WORKLIST_ID;
            arrKeys[1] = OFFICE_ID;


            chk = ((CheckBox)gr.FindControl("checkRow")).Checked;


            if (dtSelected.Rows.Contains(arrKeys))
            {
                if (!chk)
                {

                    dtSelected.Rows.Find(arrKeys).Delete();

                }
            }
            else
            {
                if (chk && ((CheckBox)gr.FindControl("checkRow")).Enabled == true)
                {


                    drItem = dtSelected.NewRow();
                    drItem["WORKLIST_ID"] = WORKLIST_ID;
                    drItem["OFFICE_ID"] = OFFICE_ID;
                    drItem["VESSEL_ID"] = VESSEL_ID;
                    drItem["Observation_ID"] = Observation_ID;              

                    dtSelected.Rows.Add(drItem);
                }
            }

        }
        if (dtSelected != null)
        {
            dtSelected.AcceptChanges();
        }
            
        ViewState["vsdtSelected_Items"] = dtSelected;
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    /// <summary>
    /// Method is used to check wheather user have access to this page or not.
    /// </summary>
    protected void UserAccessValidation()
    {
        int CurrentUserID = GetSessionUserID();
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);
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
    /// Purpose: To make search functionality available
    /// </summary>
    protected void Search_Worklist()
    {
        try
        {

            SaveItemsSelection();
            DataTable dtStatus = new DataTable();
            dtStatus.Columns.Add("All", typeof(int));
            dtStatus.Columns.Add("Pending", typeof(int));
            dtStatus.Columns.Add("Completed", typeof(int));
            dtStatus.Columns.Add("Reworked", typeof(int));
            dtStatus.Columns.Add("Verified", typeof(int));
            dtStatus.Columns.Add("Overdue", typeof(int));



            dtStatus.Rows.Add(rblJobStaus.Items[0].Selected == true ? 1 : 0,
                rblJobStaus.Items[1].Selected == true ? 1 : 0,
                0,

                rblJobStaus.Items[2].Selected == true ? 1 : 0,
                rblJobStaus.Items[3].Selected == true ? 1 : 0,
                rblJobStaus.Items[4].Selected == true ? 1 : 0);

            DataTable dtFilter = new DataTable();
            dtFilter.Columns.Add("PRM_NAME", typeof(string));
            dtFilter.Columns.Add("PRM_VALUE", typeof(object));

            dtFilter.Rows.Add(new object[] { "@FLEET_ID", UDFLib.ConvertIntegerToNull(null) });
            dtFilter.Rows.Add(new object[] { "@VESSEL_ID", UDFLib.ConvertIntegerToNull(hdnVesselID.Value) });
            dtFilter.Rows.Add(new object[] { "@ASSIGNOR", UDFLib.ConvertIntegerToNull(null) });
            dtFilter.Rows.Add(new object[] { "@DEPT_SHIP", UDFLib.ConvertIntegerToNull(null) });
            dtFilter.Rows.Add(new object[] { "@DEPT_OFFICE", UDFLib.ConvertIntegerToNull(null) });
            dtFilter.Rows.Add(new object[] { "@PRIORITY", UDFLib.ConvertIntegerToNull(null) });
            dtFilter.Rows.Add(new object[] { "@CATEGORY_NATURE", UDFLib.ConvertIntegerToNull(null) });
            dtFilter.Rows.Add(new object[] { "@CATEGORY_PRIMARY", UDFLib.ConvertIntegerToNull(null) });
            dtFilter.Rows.Add(new object[] { "@CATEGORY_SECONDARY", UDFLib.ConvertIntegerToNull(null) });
            dtFilter.Rows.Add(new object[] { "@CATEGORY_MINOR", null, });
            dtFilter.Rows.Add(new object[] { "@JOB_DESCRIPTION", UDFLib.ConvertStringToNull(txtDescription.Text.Trim()) });
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

            DataTable taskTable = objInsp.INSP_GET_WORKLIST_JOB(dtFilter, ref Record_Count);

            grdJoblist.DataSource = taskTable;
            grdJoblist.DataBind();

            ucCustomPagerctp.CountTotalRec = Record_Count.ToString();
            ucCustomPagerctp.BuildPager();

            DataTable dtPKIDs = taskTable.DefaultView.ToTable(true, new string[] { "WORKLIST_ID", "VESSEL_ID", "OFFICE_ID" });
            dtPKIDs.PrimaryKey = new DataColumn[] { dtPKIDs.Columns["WORKLIST_ID"], dtPKIDs.Columns["VESSEL_ID"], dtPKIDs.Columns["OFFICE_ID"] };
            Session["WORKLIST_PKID_NAV"] = dtPKIDs;

            lblRecordCount.Text = Record_Count.ToString();

            DataSet dsCheckListWorklist;
         
            dsCheckListWorklist = objBLLIndx.VET_Get_ObservationWorklistJobs(UDFLib.ConvertToInteger(Request.QueryString["Vessel_ID"]), UDFLib.ConvertToInteger(ViewState["Observation_ID"].ToString()));

            foreach (GridViewRow gr in grdJoblist.Rows)
            {

                string WORKLIST_ID = Convert.ToString(grdJoblist.DataKeys[gr.RowIndex]["WORKLIST_ID"]);
                string OFFICE_ID = Convert.ToString(grdJoblist.DataKeys[gr.RowIndex]["OFFICE_ID"]);

                if (dsCheckListWorklist.Tables.Count > 0)
                {
                    DataRow[] drchk = dsCheckListWorklist.Tables[0].Select("WORKLIST_ID=" + WORKLIST_ID + " and OFFICE_ID=" + OFFICE_ID);

                    if (drchk.Length > 0)
                    {
                        if (drchk[0]["Observation_ID"].ToString() != ViewState["Observation_ID"].ToString())
                        {
                            (gr.FindControl("checkRow") as CheckBox).Checked = false;
                            (gr.FindControl("checkRow") as CheckBox).Enabled = false;

                        }
                        else
                        {
                            if (drchk[0]["Observation_ID"].ToString() != "0" && drchk[0]["Observation_ID"].ToString() != "")
                            {
                                (gr.FindControl("checkRow") as CheckBox).Checked = true;

                                if (Convert.ToInt32(drchk[0]["ISCURRENT"]) == 0)
                                    (gr.FindControl("checkRow") as CheckBox).Enabled = false;
                            }

                        }


                    }
                }

            }

            SaveItemsSelection();

            string js = "GridWorkFlow();";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error1", js, true);

        }
        catch (Exception ex)
        {

            string js = "alert('Error in loading data!! Error: " + UDFLib.ReplaceSpecialCharacter(ex.Message) + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error2", js, true);
        }
    }

    /// <summary>
    /// Method is used to get assign job and mark as a check
    /// </summary>
    protected void Search_AllChecked_Worklist()
    {
        try
        {
          
            SaveItemsSelection();
            DataTable dtStatus = new DataTable();
            dtStatus.Columns.Add("All", typeof(int));
            dtStatus.Columns.Add("Pending", typeof(int));
            dtStatus.Columns.Add("Completed", typeof(int));
            dtStatus.Columns.Add("Reworked", typeof(int));
            dtStatus.Columns.Add("Verified", typeof(int));
            dtStatus.Columns.Add("Overdue", typeof(int));

            dtStatus.Rows.Add(1, 0, 0, 0, 0, 0);

            DataTable dtFilter = new DataTable();
            dtFilter.Columns.Add("PRM_NAME", typeof(string));
            dtFilter.Columns.Add("PRM_VALUE", typeof(object));

            dtFilter.Rows.Add(new object[] { "@FLEET_ID", UDFLib.ConvertIntegerToNull(null) });
            dtFilter.Rows.Add(new object[] { "@VESSEL_ID", UDFLib.ConvertIntegerToNull(hdnVesselID.Value) });
            dtFilter.Rows.Add(new object[] { "@ASSIGNOR", UDFLib.ConvertIntegerToNull(null) });
            dtFilter.Rows.Add(new object[] { "@DEPT_SHIP", UDFLib.ConvertIntegerToNull(null) });
            dtFilter.Rows.Add(new object[] { "@DEPT_OFFICE", UDFLib.ConvertIntegerToNull(null) });
            dtFilter.Rows.Add(new object[] { "@PRIORITY", UDFLib.ConvertIntegerToNull(null) });
            dtFilter.Rows.Add(new object[] { "@CATEGORY_NATURE", UDFLib.ConvertIntegerToNull(null) });
            dtFilter.Rows.Add(new object[] { "@CATEGORY_PRIMARY", UDFLib.ConvertIntegerToNull(null) });
            dtFilter.Rows.Add(new object[] { "@CATEGORY_SECONDARY", UDFLib.ConvertIntegerToNull(null) });
            dtFilter.Rows.Add(new object[] { "@CATEGORY_MINOR", null, });
            dtFilter.Rows.Add(new object[] { "@JOB_DESCRIPTION", UDFLib.ConvertStringToNull(null) });
            dtFilter.Rows.Add(new object[] { "@JOB_STATUS", UDFLib.ConvertStringToNull(0) });
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
            dtFilter.Rows.Add(new object[] { "@PAGE_INDEX", null });
            dtFilter.Rows.Add(new object[] { "@PAGE_SIZE", null });          

            int Record_Count = 0;

            DataTable taskTable = objInsp.INSP_GET_WORKLIST_JOB(dtFilter, ref Record_Count);

            DataTable dtPKIDs = taskTable.DefaultView.ToTable(true, new string[] { "WORKLIST_ID", "VESSEL_ID", "OFFICE_ID" });
            dtPKIDs.PrimaryKey = new DataColumn[] { dtPKIDs.Columns["WORKLIST_ID"], dtPKIDs.Columns["VESSEL_ID"], dtPKIDs.Columns["OFFICE_ID"] };
            Session["WORKLIST_PKID_NAV"] = dtPKIDs;         

            DataSet dsCheckListWorklist;
          
            dsCheckListWorklist = objBLLIndx.VET_Get_ObservationWorklistJobs(UDFLib.ConvertToInteger(Request.QueryString["Vessel_ID"]), UDFLib.ConvertToInteger(ViewState["Observation_ID"].ToString()));
                        
            DataTable dtSelectedALL = (DataTable)ViewState["vsdtSelected_Items"];

            for (int i = 0; i < taskTable.Rows.Count; i++)
            {
                string WORKLIST_ID = Convert.ToString(taskTable.Rows[i]["WORKLIST_ID"]);
                string OFFICE_ID = Convert.ToString(taskTable.Rows[i]["OFFICE_ID"]);
                string VESSEL_ID = Convert.ToString(taskTable.Rows[i]["VESSEL_ID"]);

                object[] arrKeys = new object[2];
                arrKeys[0] = WORKLIST_ID;
                arrKeys[1] = OFFICE_ID;
                DataRow drItem;
                if(dsCheckListWorklist.Tables.Count > 0)
                {
                DataRow[] drchk = dsCheckListWorklist.Tables[0].Select("WORKLIST_ID=" + WORKLIST_ID + " and OFFICE_ID=" + OFFICE_ID);

                if (drchk.Length > 0)
                {
                    drItem = dtSelectedALL.NewRow();
                    drItem["WORKLIST_ID"] = WORKLIST_ID;
                    drItem["OFFICE_ID"] = OFFICE_ID;
                    drItem["VESSEL_ID"] = VESSEL_ID;
                    drItem["Observation_ID"] = Convert.ToInt32(ViewState["Observation_ID"].ToString());
                    
                                if (drchk[0]["Observation_ID"].ToString() != "0" && drchk[0]["Observation_ID"].ToString() != "")
                                {
                                 
                                    if (dtSelectedALL.Rows.Contains(arrKeys))
                                    {
                                        

                                    }
                                    else
                                    {
                                        drItem = dtSelectedALL.NewRow();
                                        drItem["WORKLIST_ID"] = WORKLIST_ID;
                                        drItem["OFFICE_ID"] = OFFICE_ID;
                                        drItem["VESSEL_ID"] = Convert.ToInt32(Request.QueryString["VesselID"]);
                                        drItem["Observation_ID"] = Convert.ToInt32(ViewState["Observation_ID"].ToString());

                                        if (Request.QueryString["Observation_ID"] != "")
                                        {
                                            drItem["Observation_ID"] = Convert.ToInt32(Request.QueryString["Observation_ID"].ToString());
                                        }
                                        else
                                        {
                                            drItem["Observation_ID"] = DBNull.Value;
                                        }                               

                                        if (Convert.ToInt32(drchk[0]["ISCURRENT"]) != 0)
                                        {
                                            dtSelectedALL.Rows.Add(drItem);
                                        }

                                      
                                    }
                                }
                         
                }
            }
            }
            if (dtSelectedALL != null)
            {
                dtSelectedALL.AcceptChanges();
            }
            ViewState["vsdtSelected_Items"] = dtSelectedALL;

            SaveItemsSelection();

        }
        catch (Exception ex)
        {

            string js = "alert('Error in loading data!! Error: " + UDFLib.ReplaceSpecialCharacter(ex.Message) + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", js, true);
        }
    }

    /// <summary>
    /// Method is used to update assign job details
    /// </summary>
    protected void UpdateWorklistChecklist()
    {
         try
        {
        DataTable dtInpectionSchedule = ((DataTable)Session["dtInpectionSchedule"]);

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
                            dr[0]["LocationID"] = ViewState["SubCatCode"] == null ? DBNull.Value : ViewState["SubCatCode"];
                            dr[0]["LocationNodeID"] = ViewState["LocationNodeID"];
                        }
                        else
                        {
                            dtInpectionSchedule.Rows.Add(grdJoblist.DataKeys[item.RowIndex][0], grdJoblist.DataKeys[item.RowIndex][1], grdJoblist.DataKeys[item.RowIndex][2], ViewState["InspectionID"], ViewState["SubCatCode"] == null ? DBNull.Value : ViewState["SubCatCode"], ViewState["LocationNodeID"]);
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
                        dtInpectionSchedule.Rows.Add(grdJoblist.DataKeys[item.RowIndex][0], grdJoblist.DataKeys[item.RowIndex][1], grdJoblist.DataKeys[item.RowIndex][2], ViewState["InspectionID"], ViewState["SubCatCode"] == null ? DBNull.Value : ViewState["SubCatCode"], ViewState["LocationNodeID"]);
                    }

                }
            }

        }
             
        Session["dtInpectionSchedule"] = dtInpectionSchedule;
        }
         catch (Exception ex)
         {
             UDFLib.WriteExceptionLog(ex);
         }

    }


    protected void btnAssign_Click(object sender, EventArgs e)
    {
        try
        {

            if (Session["dtInpectionSchedule"] != null)
            {
                DataTable dtInpectionSchedule = ((DataTable)Session["dtInpectionSchedule"]);
                objInsp.Save_InspectionWorklist(dtInpectionSchedule, Convert.ToInt32(Session["USERID"]));

                string js = "alert('Worklist Assigned Successfully!');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertjs", js, true);
            }
            else
            {
                string js = "alert('Worklist Assignment failed!');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertjs", js, true);
            }

        }
        catch (Exception)
        {

            string js = "alert('Worklist Assignment failed!');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertjs", js, true);
        }

    }

    protected void btnAssignandClose_Click(object sender, EventArgs e)
    { 
         try
        {

        SaveItemsSelection();

       DataTable dtObs = (DataTable)ViewState["vsdtSelected_Items"];

       if (dtObs.Rows.Count > 0)       {

           objBLLIndx.VET_INS_Obs_Assign_Jobs(dtObs, UDFLib.ConvertToInteger(ViewState["Observation_ID"].ToString()), Convert.ToInt32(Session["USERID"]));
           string js = "alert('Worklist Assigned Successfully!');";
           ScriptManager.RegisterStartupScript(this, this.GetType(), "alertjs", js, true);

       }
       else
       {
           string js1 = "alert('Please select job!');";
           ScriptManager.RegisterStartupScript(this, this.GetType(), "alertjs1", js1, true);
       }
     
       string js12 = "parent.saveCloseChild();";
       ScriptManager.RegisterStartupScript(this, this.GetType(), "hideModalJS", js12, true);
        }
         catch (Exception ex)
         {
             UDFLib.WriteExceptionLog(ex);
         }


    }


    protected void btnGenerateReport_Click(object sender, EventArgs e)
    {
        try
        {

        string url = "window.open('SupdtInspReport.aspx?SchDetailId=";
        url = url + Convert.ToInt32(ViewState["SchDetailId"]) + "&ShowImages=" + Convert.ToInt32(ViewState["ShowImages"]);
        url = url + "');";
        string js1 = url;
        ScriptManager.RegisterStartupScript(this, this.GetType(), "script1", js1, true);
        }
     catch (Exception ex)
     {
         UDFLib.WriteExceptionLog(ex);
     }
    }

    protected void Filter_Changed(object sender, EventArgs e)
    {
         try
        {

        hdnFlagCheck.Value = "false";
        Search_Worklist();
        Search_AllChecked_Worklist();
        }
         catch (Exception ex)
         {
             UDFLib.WriteExceptionLog(ex);
         }
    }

    protected void grdJoblist_RowDataBound(object sender, GridViewRowEventArgs e)
    {
         try
        {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string AttachmentCount = DataBinder.Eval(e.Row.DataItem, "AttachmentCount").ToString();
            string Worklist_ID = DataBinder.Eval(e.Row.DataItem, "Worklist_ID").ToString();
            string Vessel_ID = DataBinder.Eval(e.Row.DataItem, "Vessel_ID").ToString();
            string WL_Office_ID = DataBinder.Eval(e.Row.DataItem, "Office_ID").ToString();
            Label lblRaisedDate = (Label)e.Row.FindControl("lblgrdRaisedDate");
            Label lblDATE_ESTMTD_CMPLTN = (Label)e.Row.FindControl("lblDATE_ESTMTD_CMPLTN");
            Label lblgrdCompletedOn = (Label)e.Row.FindControl("lblgrdCompletedOn");
            ImageButton imgRemarks = (ImageButton)e.Row.FindControl("imgRemarks");
            if (imgRemarks != null)
            {
                imgRemarks.Attributes.Add("onmouseover", "showFollowups(" + Vessel_ID + "," + Worklist_ID + "," + WL_Office_ID + ",this)");
                imgRemarks.Attributes.Add("onmouseout", "closeDiv('dialog')");
            }

            if (lblRaisedDate.Text != "")
            {
                lblRaisedDate.Text = UDFLib.ConvertUserDateFormat(Convert.ToDateTime(lblRaisedDate.Text).ToString("dd/MM/yyyy"), UDFLib.GetDateFormat());
            }
            if (lblDATE_ESTMTD_CMPLTN.Text != "")
            {
                lblDATE_ESTMTD_CMPLTN.Text = UDFLib.ConvertUserDateFormat(Convert.ToDateTime(lblDATE_ESTMTD_CMPLTN.Text).ToString("dd/MM/yyyy"), UDFLib.GetDateFormat());
            }
            if (lblgrdCompletedOn.Text != "")
            {
                lblgrdCompletedOn.Text = UDFLib.ConvertUserDateFormat(Convert.ToDateTime(lblgrdCompletedOn.Text).ToString("dd/MM/yyyy"), UDFLib.GetDateFormat());
            }

            System.Web.UI.WebControls.Image ImgAttachment = (System.Web.UI.WebControls.Image)(e.Row.FindControl("ImgAttachment"));
            if (ImgAttachment != null)
            {
                if (AttachmentCount == "0")
                    ImgAttachment.Visible = false;
                else
                    ImgAttachment.Attributes.Add("onclick", "showDialog('../Worklist/Attachments.aspx?vid=" + Vessel_ID + "&wlid=" + Worklist_ID + "&wl_off_id=" + WL_Office_ID + "');");

            }



        }
        }
         catch (Exception ex)
         {
             UDFLib.WriteExceptionLog(ex);
         }

    }
    protected void grdJoblist_Sorting(object sender, GridViewSortEventArgs e)
    {
        try
        {
            hdnFlagCheck.Value = "false";

            DataTable dt = Session["TaskTable"] as DataTable;
            if (dt != null)
            {
                //Sort the data.
                dt.DefaultView.Sort = e.SortExpression + " " + GetSortDirection(e.SortExpression);
                grdJoblist.DataSource = Session["TaskTable"];
                grdJoblist.DataBind();
            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    protected void grdJoblist_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }

    private string GetSortDirection(string column)
    {    
        // By default, set the sort direction to ascending.
        string sortDirection = "ASC";
        try
        {    

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
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }

        return sortDirection;
    }

    protected bool SelectCheckbox(string WORKLIST_ID, string VESSEL_ID, string OFFICE_ID)
    { 
        try
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
       
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
        return false;
    }
    protected bool EnableCheckbox(string WORKLIST_ID, string VESSEL_ID, string OFFICE_ID)
    {
         try
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
        }
         catch (Exception ex)
         {
             UDFLib.WriteExceptionLog(ex);
         }

        return true;

    }

    //This functionality is added by Hadish on 27-10-2016
    protected void ImgBtnSearch_Click(object sender, ImageClickEventArgs e)
    {
        Search_Worklist();
    }

    //This functionality is added by Hadish on 27-10-2016
    protected void ImgBtnClearFilter_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
        txtDescription.Text = string.Empty;
        ImgBtnSearch_Click(null, null);
         }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }

    }
}