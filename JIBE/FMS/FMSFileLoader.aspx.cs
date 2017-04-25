using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using SMS.Business.FMS;
using SMS.Business.Infrastructure;
using SMS.Properties;
using System.IO;
using AjaxControlToolkit4;
using System.Drawing;
using System.Collections.Generic;
using System.Text;

public partial class FMSFileLoader : System.Web.UI.Page
{
    UserAccess objUA = new UserAccess();
    BLL_FMS_Document objFMS = new BLL_FMS_Document();
    BLL_Infra_Company objCompBLL = new BLL_Infra_Company();
    string LatestFilePath = "";
    string OppStatus = "";
    string OppUserID = "";
    string DocVersion = "0";
    string DocID = "";
    public Boolean uaEditFlag = false;
    BLL_Infra_VesselLib objVsl = new BLL_Infra_VesselLib();
    BLL_FMS_Document objInsp = new BLL_FMS_Document();
    public string Mode = "";
    TreeNode Tempnode = new TreeNode();
    /// <summary>
    /// This load event is Modified by Pranav Sakpal on 26-07-2016
    /// This event will load and bind all the controls as per conditions.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            //Added by Anjali DT:27-09-2016,To avoid page refresh if file not found.
            if (Request.QueryString["MethodName"] != null)
            {
                if (Request.QueryString["MethodName"] == "FileExists")
                {
                    FileExists(Request.QueryString["FileName"]);
                    return;
                }
            }

            if (GetSessionUserID() == 0)
                Response.Redirect("~/account/login.aspx");

            if (Request.QueryString["DocVer"] != null)
            {
                if (Request.QueryString["DocVer"].ToString() != "")
                {
                    string[] temp = Request.QueryString["DocVer"].ToString().Split('-');
                    DocID = temp[0];
                    DocVersion = temp[1];
                }
            }
            else
            {
                DocID = Request.QueryString["DocID"];
                if (DocID == null)
                    DocID = UDFLib.ConvertStringToNull(hdnFrmRecDocID.Value);
            }


            if (!IsPostBack)
            {
                UserAccessValidation();
                BindDepartment(); // Bind Department Dropdown.
                BindFormType();

                GetDocHistory();
                GetApprovalHiostory();

                BindOperationGrid(UDFLib.ConvertToInteger(DocID == null ? hdnFrmRecDocID.Value : DocID));
                BindRAFormList(UDFLib.ConvertToInteger(DocID == null ? hdnFrmRecDocID.Value : DocID));
                BindEditRAFormList(UDFLib.ConvertToInteger(DocID == null ? hdnFrmRecDocID.Value : DocID));
                LoadSearchData(); // Bind Fleet and Vessel dropdown which is inside the main page.
                BindStatus(); // Binds status into the dropdown .

                string originalPath = Request.UrlReferrer.AbsolutePath.ToString();
                string parentDirectory = UDFLib.GetPageURL(originalPath);
                DataSet ds = objInsp.FMS_Get_ParentFolders_SchTree(GetSessionUserID(), 0, parentDirectory);
                if (ds.Tables.Count > 0)
                {
                    if (DocID != null)
                    {
                        DataSet dsParentID = objInsp.FMS_Get_ParentFoldersID(Convert.ToInt32(DocID));
                        if (dsParentID.Tables.Count > 0)
                        {
                            if (dsParentID.Tables[0].Rows.Count > 0)
                            {
                                hdnParentID.Value = dsParentID.Tables[0].Rows[0]["ParentID"].ToString();
                            }
                        }
                        recursiveTree(ds.Tables[0], 0, null);
                    }

                }
            }

            if (DocID != null)
            {
                if (!IsPostBack)
                {

                    ViewState["SchStatusID"] = "";


                    DataSet dsFileDetails = objFMS.getFileDetailsByID(int.Parse(DocID), int.Parse(DocVersion));

                    if (dsFileDetails.Tables[0].Rows.Count > 0)
                    {
                        DataRow dr = dsFileDetails.Tables[0].Rows[0];
                        lblDocName.Text = dr["LogFileID"].ToString();
                        hdnDocName.Value = dr["LogFileID"].ToString();
                        if (DocVersion == "0")
                        {
                            DocVersion = dr["Version"].ToString();
                            if (dr["Created_By"].ToString() == "0")
                                lblOppStatus.Text = dr["Operation_Type"].ToString() == "" ? "CREATED by Office on " + ConvertDateToString(dr["Date_Of_Creatation"].ToString(), "dd-MMM-yy HH:mm") : dr["Operation_Type"].ToString() + " by " + dr["first_name"].ToString().ToUpper() + " on " + ConvertDateToString(dr["Operation_Date"].ToString(), "dd-MMM-yy HH:mm");
                            else
                                lblOppStatus.Text = dr["Operation_Type"].ToString() == "" ? "CREATED by " + dr["CreatedBYFirstName"].ToString() + " on " + ConvertDateToString(dr["Date_Of_Creatation"].ToString(), "dd-MMM-yy HH:mm") : dr["Operation_Type"].ToString() + " by " + dr["first_name"].ToString().ToUpper() + " on " + ConvertDateToString(dr["Operation_Date"].ToString(), "dd-MMM-yy HH:mm");
                        }
                        else
                        {
                            if (DocVersion == dr["Version"].ToString())
                                lblOppStatus.Text = dr["Operation_Type"].ToString() == "" ? "CREATED by " + dr["first_name"].ToString() + " on " + ConvertDateToString(dr["Date_Of_Creatation"].ToString(), "dd-MMM-yy HH:mm") : dr["Operation_Type"].ToString() + " by " + dr["first_name"].ToString().ToUpper() + " on " + ConvertDateToString(dr["Operation_Date"].ToString(), "dd-MMM-yy HH:mm");
                            else
                                lblOppStatus.Text = "<b>You are viewing older version of the form.</b>";

                        }
                        lblCurrentVersion.Text = DocVersion;

                        OppStatus = dr["Operation_Type"].ToString();
                        OppUserID = dr["userid"].ToString();
                        string VersionFilePath = "";

                        //for file Approval History
                        int rowCount = 0;
                        DataTable dtApproval_Level = objFMS.FMS_Get_FileApprovalExists(UDFLib.ConvertToInteger(DocID), null, null, null, null, null, null, ref rowCount);
                        if (dtApproval_Level.Rows.Count > 0 && dtApproval_Level.Rows[dtApproval_Level.Rows.Count - 1]["Approval_Status"].ToString() == "0")
                        {
                            int index = 0;
                            foreach (DataRow row in dtApproval_Level.Rows)
                            {
                                if (row["Approval_Status"].ToString() == "0")
                                {
                                    index = Convert.ToInt32(row["LevelID"].ToString());

                                    break;

                                }

                            }
                            int User_ID = Convert.ToInt32(dtApproval_Level.Rows[index - 1]["ApproverID"].ToString());
                            BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();

                            DataTable dtUser = objUser.Get_UserDetails(User_ID);
                            if (dtUser.Rows.Count > 0)
                            {
                                lblUser.Text = dtUser.Rows[0]["User_name"].ToString();
                            }
                            dvMain.Visible = false;

                            divApprovalMessage.Visible = true;
                            lblDocName1.Text = lblDocName.Text;
                            lblLevel.Text = UDFLib.ConvertStringToNull(index);
                            lblCurrentVersion1.Text = lblCurrentVersion.Text;
                            lblOppStatus1.Text = lblOppStatus.Text;

                        }
                        else if (dtApproval_Level.Rows.Count > 0 && dtApproval_Level.Rows[dtApproval_Level.Rows.Count - 1]["Approval_Status"].ToString() == "-1")
                        {
                            DataSet ds = objFMS.FMS_Get_FileRejectionInfo(UDFLib.ConvertToInteger(DocID));

                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                lblDocName1.Text = ds.Tables[0].Rows[0]["LogFileID"].ToString();
                                lblCurrentVersion1.Text = ds.Tables[0].Rows[0]["Version"].ToString();
                                lblOppStatus1.Text = "Rejected";
                                lblApprovalPendingBy.Text = "Rejected By:";
                                lblUser.Text = ds.Tables[0].Rows[0]["User_Name"].ToString();
                                lblLevel.Text = ds.Tables[0].Rows[0]["LevelID"].ToString();
                                lblComment.Text = "Comment:" + ds.Tables[0].Rows[0]["Remark"].ToString();
                                dvMain.Visible = false;
                                divApprovalMessage.Visible = true;
                            }


                        }
                        else
                        {

                            LatestFilePath = "../" + dr["FilePath"].ToString();
                            dvMain.Visible = true;
                            divApprovalMessage.Visible = false;
                            divApprovalMessage.Attributes.Add("display", "none");
                            LoadData();
                            LoadDocScheduleGrid();
                            btnLoadFiles.Attributes.Add("style", "visibility:hidden");


                        }

                    }
                }

            }
            else
            {

                if (!IsPostBack)
                {
                    // To retain previous selection of 'Pending for Approval' tab when after user approve forms and page refresh.

                    if (Session["MODEFMS"] != null)
                    {
                        if (Session["MODEFMS"].ToString() == "Pending")
                        {
                            FormsPendingApprover(sender, e);
                            Session["MODEFMS"] = null;

                        }
                    }
                    else
                    {
                        btnReceived.CssClass = "FormsReceived";
                        btnReceived.Enabled = false;
                        btnPending.CssClass = "FormsNotReceived";
                        btnPendingApp.CssClass = "FormsNotReceived";
                        lblFormsStatus.Text = "Forms received in the last";
                        Mode = "Received";
                        BindFormsReceived("7");
                        rblPending.Visible = false;
                        chkUser.Visible = false;
                        lblhdrStatus.Visible = false;
                        lblMyApprovals.Visible = false;
                        tblApp.Visible = false;
                    }
                }
                btnLoadFiles.Attributes.Add("style", "visibility:hidden");

            }
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "Confirm();", true);

        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }


    }

    /// <summary>
    /// This function is created by Pranav Sakpal on 26-07-2016.
    /// recursiveTree This function will forms tree and bind it to control 
    /// This will operate dtmenu parameter table to fetch tree node values in it.
    /// ParentId check ie. selected document folder selection will be maintained in (hdnParentID) hiddenfeild.
    /// </summary>
    /// <param name="dtmenu">Data table with tree elements</param>
    /// <param name="ID">valid ID 0 OR int type value</param>
    /// <param name="meni">this will be null or may contain TreeNode object. </param>
    protected void recursiveTree(DataTable dtmenu, int ID, TreeNode meni)
    {
        try
        {
            string SelectedDocID = Request.QueryString["DocID"].ToString();

            DataRow[] drInners;
            if (ID.ToString() == "0")
                drInners = dtmenu.Select("ParentID =0");
            else
                drInners = dtmenu.Select("ParentID ='" + ID.ToString() + "' ");

            if (drInners.Length != 0)
            {

                foreach (DataRow drInner in drInners)
                {
                    TreeNode miner;
                    miner = new TreeNode(drInner["LogFileID"].ToString().Trim(), drInner["ID"].ToString());
                    //if (Convert.ToString(drInner["NodeType"]) == "1")
                    //{
                    //    miner.ShowCheckBox = false;
                    //}
                    //miner.SelectAction = TreeNodeSelectAction.None;
                    if (meni == null)
                    {
                        trvFile.Nodes.Add(miner);
                    }
                    else
                    {
                        meni.ChildNodes.Add(miner);
                        meni.CollapseAll();
                    }

                    int ID1 = Convert.ToInt32(drInner["ID"].ToString());

                    //if (miner.Value == SelectedDocID)
                    if (miner.Value == hdnParentID.Value)
                    {
                        Tempnode = miner;
                        lblSelectedFolderName.Text = drInner["LogFileID"].ToString().Trim();
                    }

                    recursiveTree(dtmenu, ID1, miner);


                }

            }

            ExpandToRoot(Tempnode);
            Tempnode.Selected = true;
        }
        catch (Exception ex)
        {
            string Error3 = "alert('" + ex.Message.ToString() + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Error3", Error3, true);
        }

    }
    /// <summary>
    /// This function is created by Pranav Sakpal on 26-07-2016
    /// This function will expand the treenode comes as parameter.
    /// </summary>
    /// <param name="node">Tree node object to perform epantion on it.</param>
    private void ExpandToRoot(TreeNode node)
    {
        try
        {
            node.Expand();
            if (node.Parent != null)
            {

                ExpandToRoot(node.Parent);
            }
        }
        catch (Exception ex)
        {
            string Error4 = "alert('" + ex.Message.Replace("'", "") + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Error4", Error4, true);
        }


    }



    /// <summary>
    /// Get File Approval History
    /// </summary>
    public void GetApprovalHiostory()
    {
        int FileID = 0;
        FileID = UDFLib.ConvertToInteger(GetDocID());
        if (FileID == 0 && hdnFrmRecDocID.Value != "")
            FileID = UDFLib.ConvertToInteger(hdnFrmRecDocID.Value);

        DataSet ds = objFMS.FMS_Get_ScheduleApprovalHistory(FileID);
        if (ds != null)
        {
            grdSchAppHistory.DataSource = ds.Tables[0];
            grdSchAppHistory.DataBind();
        }
    }
    /// <summary>
    /// Modified on DT:21-06-2016
    /// To display information related to selected file.
    /// </summary>
    public void GetDocHistory()
    {
        try
        {
            int FileID = 0;
            int showArchivedForms;

            //To visible or not archived files
            if (Session["ShowArchivedForms"] == null)
            {
                showArchivedForms = 0;
            }
            else
            {
                showArchivedForms = Convert.ToInt32(Session["ShowArchivedForms"]);
            }

            FileID = UDFLib.ConvertToInteger(GetDocID());

            if (FileID == 0 && hdnFrmRecDocID.Value != "")
                FileID = UDFLib.ConvertToInteger(hdnFrmRecDocID.Value);

            //Get details of selected file in tree.
            DataSet dsGetLastestFileInfoByID = objFMS.GetLastestFileInfoByID(FileID, showArchivedForms);

            if (dsGetLastestFileInfoByID.Tables[0].Rows.Count > 0)
            {
                DataRow dr = dsGetLastestFileInfoByID.Tables[0].Rows[0];

                string FileExtension = new FileInfo(dr["LogFileID"].ToString()).Extension;

                lnkFileName1.Visible = true;
                lnkFileName.Text = dr["LogFileID"].ToString();
                lnkFileName1.Text = dr["LogFileID"].ToString();
                lnkFileName1.NavigateUrl = "../" + dr["FilePath"].ToString();
                hdnFilePath.Value = "../" + dr["FilePath"].ToString();
                lblFormType.Text = dr["Form_Type"].ToString();
                lblDepartment.Text = dr["Department"].ToString();

                if (dr["Created_User"].ToString() != "")
                    lblCreatedBy.Text = dr["Created_User"].ToString();
                else
                    lblCreatedBy.Text = "Office";

                lblCreationDate.Text = dr["Date_Of_Creatation"].ToString();

                if (dr["Opp_User"].ToString() != "")
                {
                    lblLastOperation.Text = dr["Operation_Type"].ToString() + " by " + dr["Opp_User"].ToString();
                    lblLastOperationDt.Text = dr["Operation_date"].ToString();
                }

                if (dr["Version"].ToString() != "")
                {
                    lblLatestVersion.Text = dr["Version"].ToString();
                }
                lblRemark.Text = dr["Remark"].ToString();

                if (dr["ForwardAttchment"].ToString() == "True")
                {
                    chkautoforwardMail.Checked = true;
                }
                else
                {
                    chkautoforwardMail.Checked = false; ;
                }

                lblArchivedTitle.Text = "";

                //only for archived files header text will be visible.
                Session["IsFileArchived"] = dr["Active_Status"].ToString();

                if (dr["Active_Status"].ToString() == "0")
                {
                    lblArchivedTitle.Text = "Archived on : " + dr["ArchivedOn_Date"].ToString() + " by : " + dr["ArchivedBy_User"].ToString() + "";
                }
                // To show hide controls conditionally for archive and un-archive
                Show_Hide_Controls(dr["Active_Status"].ToString());
                LoadDataForEdit(dr);

            }

            DataSet dsOperationDetailsByID = objFMS.GetOprationDetailsByID(FileID);
        }
        catch (Exception ex)
        {
            string jsSqlError2 = "alert('" + ex.Message + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "jsSqlError2", jsSqlError2, true);
        }
    }
    /// <summary>
    /// Modified on DT:04-07-2016
    /// To check acces for logged in user.
    /// like Add,Edit ,delete access for requested page.
    /// </summary>
    /// <returns></returns>
    protected bool UserAccessValidation()
    {
        int CurrentUserID = GetSessionUserID();
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

        try
        {
            BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
            objUA = objUser.Get_UserAccessForPage(CurrentUserID, Session["Pageurl"].ToString());

            if (objUA != null)
            {
                if (objUA.View == 0)
                {
                    Response.Write("<center><br><br><h2><font color=gray>You do not have enough priviledge to access this page.</font></h2></center>");
                    Response.End();
                }

                if (objUA.Edit == 1)
                {
                    uaEditFlag = true;
                    ImgSubCheckIn.Visible = true;
                    ImgCheckOut.Visible = true;
                    ImgBtnDocSchedule.Visible = true;
                    btnRework.Visible = true;
                    ImgCheckIn.Visible = true;

                }
                if (objUA.Edit == 0)
                {
                    uaEditFlag = true;
                    ImgSubCheckIn.Visible = false;
                    ImgCheckOut.Visible = false;
                    ImgBtnDocSchedule.Visible = false;
                    btnRework.Visible = false;
                    ImgCheckIn.Visible = false;

                }
                if (objUA.Approve == 1)
                {
                    ImgSetApproval.Visible = true;
                    BtnApprove.Visible = true;
                }
                if (objUA.Approve == 0)
                {
                    ImgSetApproval.Visible = false;
                    BtnApprove.Visible = false;
                }

                if (objUA.Delete == 0)
                {
                    btnDelete.Visible = false;
                }
                if (objUA.Add == 1)
                {
                    imgAddFollowup.Visible = true;
                    ImgAddAttachment.Visible = true;
                }
                if (objUA.Add == 0)
                {
                    imgAddFollowup.Visible = false;
                    ImgAddAttachment.Visible = false;
                }
                // Only Admin cane archived /delete forms.
                if (objUA.Admin == 1)
                {
                    btnDelete.Visible = true;
                }
                else
                {
                    btnDelete.Visible = false;
                }

                if (Session["IsFileArchived"] != null)
                {
                    Show_Hide_Controls(Session["IsFileArchived"].ToString());
                }
            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }

        return uaEditFlag;
    }

    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Convert.ToString(Session["USERID"]));
        else
            return 0;
    }

    private string getVirtualPath(string docPath)
    {
        string[] arPath = docPath.Split('\\');
        string url = "";

        for (int i = 3; i < arPath.Length; i++)
        {
            if (url != "")
                url += "/";

            url += arPath[i];
        }
        return url;
    }
    /// <summary>
    /// Get File Version (operation) history
    /// </summary>
    /// <param name="FileID">Selected File ID</param>
    protected void BindOperationGrid(int FileID)
    {
        DataSet dsOperationDetailsByID = objFMS.GetOprationDetailsByID(FileID);
        if (dsOperationDetailsByID != null)
        {
            dtrOppGrid.DataSource = dsOperationDetailsByID.Tables[0];
            dtrOppGrid.DataBind();
        }
    }

    protected void GetFile(object sender, CommandEventArgs e)
    {

        string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
        string FileID = commandArgs[0].ToString();
        string Version = commandArgs[1].ToString();
        string FilePath = commandArgs[2].ToString();
    }
    /// <summary>
    /// Function is used to retrive forms scheduled details and bind into grid.
    /// </summary>
    protected void LoadDocScheduleGrid()
    {

        ucCustomPagerSch.Visible = true;
        int rowcount = ucCustomPagerSch.isCountRecord;

        if (ValidateAll() == true)
        {
            DataTable dt = objFMS.Get_DocScheduleList(Convert.ToInt32(GetDocID()), UDFLib.ConvertIntegerToNull(ddlVessel.SelectedValue), UDFLib.ConvertIntegerToNull(ddlFleet.SelectedValue), UDFLib.ConvertStringToNull(ddlStatus.SelectedValue), ucCustomPagerSch.CurrentPageIndex, ucCustomPagerSch.PageSize, ref  rowcount, UDFLib.ConvertDateToNull(txtFromDate.Text.Trim()), UDFLib.ConvertDateToNull(txtTillDate.Text.Trim())); // (Modified By : kavita)Passed From date and Till Date 
            if (ucCustomPagerSch.isCountRecord == 1)
            {

                ucCustomPagerSch.CountTotalRec = rowcount.ToString();
                ucCustomPagerSch.BuildPager();
            }

            gvVesselDocHistory.DataSource = dt;
            gvVesselDocHistory.DataBind();

            string js = "   document.getElementById('dvDocDetails').style.display='none';";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "hiderightpanel", js, true);
            if (ViewState["FMS_CompletionDate"] != null && ViewState["FMS_ApprovedStatus"] != null)
            {
                if (ViewState["FMS_CompletionDate"].ToString() != "" && ViewState["FMS_ApprovedStatus"].ToString() == "")
                {
                    imgAddFollowup.Visible = true;
                    ImgAddAttachment.Visible = true;
                    dlApprovalLevel.Visible = true;
                    lblApl.Visible = true;
                }
                else
                {

                    imgAddFollowup.Visible = false;
                    ImgAddAttachment.Visible = false;
                    dlApprovalLevel.Visible = false;
                    lblApl.Visible = false;
                }
            }
            else
            {
                imgAddFollowup.Visible = false;
                ImgAddAttachment.Visible = false;
                dlApprovalLevel.Visible = false;
                lblApl.Visible = false;
            }


        }


    }
    /// <summary>
    /// LoadData() Funtion Bind Vessel and Fleet dropdown
    /// </summary>
    protected void LoadData()
    {
        DataTable FleetDT = objVsl.GetFleetList(Convert.ToInt32(Session["USERCOMPANYID"].ToString()));
        ddlFleet.DataSource = FleetDT;
        ddlFleet.DataTextField = "Name";
        ddlFleet.DataValueField = "code";
        ddlFleet.DataBind();
        ddlFleet.Items.Insert(0, new ListItem("- ALL -", "0"));

        DataTable dtVessel = objVsl.Get_VesselList(0, 0, Convert.ToInt32(Session["USERCOMPANYID"].ToString()), "", Convert.ToInt32(Session["USERCOMPANYID"].ToString()));/*In This function call only Company ID is pass . Previously "0" was pass which preven from binding default all vessel*/
        ddlVessel.DataSource = dtVessel;
        ddlVessel.DataTextField = "Vessel_name";
        ddlVessel.DataValueField = "Vessel_id";
        ddlVessel.DataBind();
        ddlVessel.Items.Insert(0, new ListItem("- ALL -", "0"));
    }
    /// <summary>
    /// LoadSearchData() Funtion Bind Vessel and Fleet dropdown in Dashboard
    /// </summary>
    protected void LoadSearchData()
    {
        DataTable FleetDTSearch = objVsl.GetFleetList(Convert.ToInt32(Session["USERCOMPANYID"].ToString()));
        if (FleetDTSearch.Rows.Count > 0)
        {
            ddlFleetSearch.DataSource = FleetDTSearch;
            ddlFleetSearch.DataTextField = "Name";
            ddlFleetSearch.DataValueField = "code";
            ddlFleetSearch.DataBind();
            ddlFleetSearch.Items.Insert(0, new ListItem("- ALL -", "0"));
            BindVesselList();
        }
    }

    protected void ddlFleet_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DataTable dtVessel = objVsl.Get_VesselList(UDFLib.ConvertToInteger(ddlFleet.SelectedValue), 0, Convert.ToInt32(Session["USERCOMPANYID"].ToString()), "", Convert.ToInt32(Session["USERCOMPANYID"].ToString()));
            if (dtVessel.Rows.Count > 0)
            {
                ddlVessel.Items.Clear();
                ddlVessel.DataSource = dtVessel;
                ddlVessel.DataTextField = "Vessel_name";
                ddlVessel.DataValueField = "Vessel_id";
                ddlVessel.DataBind();
                ListItem li = new ListItem("- ALL -", "0");
                ddlVessel.Items.Insert(0, li);
            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string jsSqlError2 = "alert('" + ex.Message + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "jsSqlError2", jsSqlError2, true);
        }
    }
    /// <summary>
    /// Event use to bind vessel list according to Fleet Selection.
    /// </summary>
    protected void ddlFleetSearch_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            BindVesselList();
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string jsSqlError2 = "alert('" + ex.Message + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "jsSqlError2", jsSqlError2, true);

        }
    }

    /// <summary>
    /// To display schedule data.
    /// </summary>
    /// <param name="source"></param>
    /// <param name="e"></param>
    protected void onDocumentClick(object source, CommandEventArgs e)
    {

        try
        {

            int VesselID = UDFLib.ConvertToInteger(e.CommandArgument.ToString().Split(',')[0]);
            int ScheduleID = UDFLib.ConvertToInteger(e.CommandArgument.ToString().Split(',')[1]);
            int StatusID = UDFLib.ConvertToInteger(e.CommandArgument.ToString().Split(',')[3]);
            int OfficeID = UDFLib.ConvertToInteger(e.CommandArgument.ToString().Split(',')[2]);
            hdnSchOfficeID.Value = e.CommandArgument.ToString().Split(',')[2];
            hdnSchVesselID.Value = e.CommandArgument.ToString().Split(',')[0];
            int RowIndex = -1;
            ViewState["FMSRow_Index"] = -1;
            if (e.CommandArgument.ToString().Split(',').Length == 5)
            {
                RowIndex = UDFLib.ConvertToInteger(e.CommandArgument.ToString().Split(',')[4]);
                ViewState["FMSRow_Index"] = RowIndex;
            }

            // Added on DT:06-07-2016.
            // If status is 'Un-assigned' and 'Re-schedule' then 
            if (gvVesselDocHistory.Rows.Count > 0)
            {
                Label txtStatus = (Label)gvVesselDocHistory.Rows[RowIndex].FindControl("lblStatus");
                if ((txtStatus.Text == "Un-assigned") || (txtStatus.Text == "Re-schedule"))
                {

                    if (txtStatus.Text == "Un-assigned")
                    {
                        lblstatus1.Text = "Un-assigned form";
                        lblRe_ScheduledBy.Text = "Un-assigned By :";
                    }
                    if (txtStatus.Text == "Re-schedule")
                    {
                        lblstatus1.Text = "Re-schedule form";
                        lblRe_ScheduledBy.Text = "Re-schedule By :";

                    }
                    DataTable dtformDetails = objFMS.FMS_Get_Reschedule_FormInfo(StatusID);
                    if (dtformDetails != null)
                    {
                        if (dtformDetails.Rows.Count > 0)
                        {
                            lblReScheduledBy.Text = dtformDetails.Rows[0]["Rescheduled_by"].ToString();
                            lblDate.Text = dtformDetails.Rows[0]["ArchivedOn_Date"].ToString();
                            lblFileName.Text = dtformDetails.Rows[0]["FormName"].ToString();
                            lblVessel.Text = dtformDetails.Rows[0]["Vessel_Name"].ToString();
                        }
                    }

                }
                else
                {
                    DisplayData(VesselID, StatusID, OfficeID, RowIndex);
                }
            }
            else
            {
                DisplayData(VesselID, StatusID, OfficeID, RowIndex);

            }

        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string jsSqlError2 = "alert('" + ex.Message + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "jsSqlError2", jsSqlError2, true);
        }

    }

    private void DisplayData(int VesselID, int StatusID, int OfficeID, int RowIndex)
    {
        try
        {
            DataTable dtReports = objFMS.Get_VesselDocHistroy(Convert.ToInt32(GetDocID()), VesselID, StatusID, OfficeID);


            if (dtReports.Rows.Count > 0)
            {
                dtReports.PrimaryKey = new DataColumn[] { dtReports.Columns["ID"] };
                DataRow dr = dtReports.Rows[0];
                BindVesselDocHistroy(dr);
                Bind_ScheduleRAFormList(StatusID, VesselID);

            }
            else
            {
                btnRework.Visible = false;
                BtnApprove.Visible = false;
                updVesselHist.Update();
            }

            HighlightSelectedRow(RowIndex);
            string strRow = " if( $('[id$=itemRow]').length>0) { $('[id$=itemRow]')[" + RowIndex + "].className='highlightRow';}";

            //string strRow = "if( $('[id$=itemRow]').length>0) { $('[id$=itemRow]')[11].className='highlightRow';}";

            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "strRow" + RowIndex, strRow, true);
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string jsSqlError2 = "alert('" + ex.Message + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "jsSqlError2", jsSqlError2, true);
        }
    }

    public void BindApprovalLevel()
    {

        DataSet dsAppLevel = objFMS.FMS_Get_FileApprovalLevelStatus(Convert.ToInt32(GetDocID()), UDFLib.ConvertToInteger(Session["FMS_SchStatusID"]), UDFLib.ConvertToInteger(Session["FMS_SchOfficeID"]), UDFLib.ConvertToInteger(Session["FMS_SchVesselID"]));
        if (dsAppLevel.Tables.Count > 0)
        {
            if (dsAppLevel.Tables[0].Rows.Count > 0)
            {
                dlApprovalLevel.DataSource = dsAppLevel.Tables[0];
                dlApprovalLevel.DataBind();

                dlApprovalLevel.Visible = true;
                lblApl.Visible = true;
            }
            else
            {
                dlApprovalLevel.Visible = false;
                lblApl.Visible = false;
            }
        }

    }

    public void clearFields()
    {
        lblCompletionDate.Text = "";
        lblDueDate.Text = "";
        lblVoyageNum.Text = "";
        lblVesselAt.Text = "";
        lblInspectionStatus.Text = "";
        lblVessName.Text = "";
    }

    /// <summary>
    /// This Method is edited by Pranav Sakpal on 27-07-2016 
    /// This method is edited for showing Document name as per schedule selection
    /// </summary>
    /// <param name="dr">Valid </param>
    public void BindVesselDocHistroy(DataRow dr)
    {
        try
        {

            clearFields();
            UserAccessValidation();
            string FileExtension = string.Empty;
            if (dr["DocumentPath"].ToString() != "")
            {
                FileExtension = new FileInfo(dr["DocumentPath"].ToString()).Extension;
                if (FileExtension == ".xsl" || FileExtension == ".xml")/* As told by Bikash Sir For xml and xsl file when User click on link then those document should be force download and rest of documents should open or download as per support in browser  */
                {
                    lnkDocID.Visible = true;
                    lnkDocID1.Visible = false;
                }
                else
                {
                    lnkDocID.Visible = false;
                    lnkDocID1.Visible = true;
                }

            }
            else
            {
                lnkDocID.Visible = false;
                lnkDocID1.Visible = false;
            }

            hdnDocName.Value = dr["File_Name"].ToString();

            lnkDocID.Text = hdnDocName.Value.ToString() == "" ? hdnFrmRecDocName.Value.ToString() : hdnDocName.Value.ToString();// +"_" + Vessel_Name

            lnkDocID1.Text = hdnDocName.Value.ToString() == "" ? hdnFrmRecDocName.Value.ToString() : hdnDocName.Value.ToString();// +"_" + Vessel_Name
            lnkDocID1.NavigateUrl = "../Uploads/FMS/" + dr["DocumentPath"].ToString();
            hdnDocPath.Value = "../Uploads/FMS/" + dr["DocumentPath"].ToString();
            lnkDocID.Visible = true;
            if (dr["DocumentPath"].ToString() == "")
            {
                lnkDocID.Enabled = false;
                btnForce_Download_Doc.Visible = false;
            }
            else
            {
                lnkDocID.Enabled = true;
                btnForce_Download_Doc.Visible = true;
            }
            DataSet ds = objFMS.GetDocSchedule_Details(UDFLib.ConvertToInteger(dr["ID"]), UDFLib.ConvertToInteger(dr["Office_ID"]), UDFLib.ConvertToInteger(dr["Vessel_ID"]));
            if (ds.Tables.Count >= 2)
            {
                gvFollowup.DataSource = ds.Tables[0];
                gvFollowup.DataBind();
                gvAttachment.DataSource = ds.Tables[1];
                gvAttachment.DataBind();

                if (ds.Tables[2].Rows.Count > 0)
                {
                    lblCompletionDate.Text = ds.Tables[2].Rows[0]["Completion_Date"].ToString();
                    lblDueDate.Text = ds.Tables[2].Rows[0]["Schedule_Date"].ToString();
                    lblVoyageNum.Text = ds.Tables[2].Rows[0]["Voyage_Number"].ToString();
                    lblVesselAt.Text = ds.Tables[2].Rows[0]["Vessel_At"].ToString();
                    lblInspectionStatus.Text = ds.Tables[2].Rows[0]["Status"].ToString();
                    lblVessName.Text = ds.Tables[2].Rows[0]["Vessel_Name"].ToString();
                }
                string DocName = hdnDocName.Value.ToString() == "" ? hdnFrmRecDocName.Value.ToString() : hdnDocName.Value.ToString();
                if (lblCompletionDate.Text != "" && dr["Approved_Status"].ToString() == "")
                {
                    imgAddFollowup.Visible = true;
                    ImgAddAttachment.Visible = true;
                    dlApprovalLevel.Visible = true;
                    lblApl.Visible = true;
                    for (int i = 0; i < gvAttachment.Rows.Count; i++)
                    {
                        ((ImageButton)gvAttachment.Rows[i].Cells[2].FindControl("imgbtnDelete")).Visible = true;
                    }
                    if (dr["DocumentPath"].ToString() != "")
                    {
                        FileExtension = new FileInfo(dr["DocumentPath"].ToString()).Extension;
                        if (FileExtension == ".xsl" || FileExtension == ".xml")/* As told by Bikash Sir For xml and xsl file when User click on link then those document should be force download and rest of documents should open or download as per support in browser  */
                        {
                            lnkDocID.Visible = true;
                            lnkDocID1.Visible = false;
                        }
                        else
                        {
                            lnkDocID.Visible = false;
                            lnkDocID1.Visible = true;
                        }

                        lnkDocID.Text = DocName.Split('.')[0] + "_" + lblVessName.Text + "_" + lblCompletionDate.Text + "." + DocName.Split('.')[1]; // +"_" + Vessel_Name

                        lnkDocID1.Text = DocName.Split('.')[0] + "_" + lblVessName.Text + "_" + lblCompletionDate.Text + "." + DocName.Split('.')[1];// +"_" + Vessel_Name
                        lnkDocID1.NavigateUrl = "../Uploads/FMS/" + dr["DocumentPath"].ToString();
                        hdnDocPath.Value = "../Uploads/FMS/" + dr["DocumentPath"].ToString();
                    }
                }
                else if (lblCompletionDate.Text != "" && dr["Approved_Status"].ToString() != "")
                {
                    imgAddFollowup.Visible = false;
                    ImgAddAttachment.Visible = false;
                    dlApprovalLevel.Visible = true;
                    lblApl.Visible = true;
                    for (int i = 0; i < gvAttachment.Rows.Count; i++)
                    {
                        ((ImageButton)gvAttachment.Rows[i].Cells[2].FindControl("imgbtnDelete")).Visible = false;
                    }
                    if (dr["DocumentPath"].ToString() != "")
                    {

                        FileExtension = new FileInfo(dr["DocumentPath"].ToString()).Extension;
                        if (FileExtension == ".xsl" || FileExtension == ".xml")/* As told by Bikash Sir For xml and xsl file when User click on link then those document should be force download and rest of documents should open or download as per support in browser  */
                        {
                            lnkDocID.Visible = true;
                            lnkDocID1.Visible = false;
                        }
                        else
                        {
                            lnkDocID.Visible = false;
                            lnkDocID1.Visible = true;
                        }
                        lnkDocID.Text = DocName.Split('.')[0] + "_" + lblVessName.Text + "_" + lblCompletionDate.Text + "." + DocName.Split('.')[1];

                        lnkDocID1.Text = DocName.Split('.')[0] + "_" + lblVessName.Text + "_" + lblCompletionDate.Text + "." + DocName.Split('.')[1];// +"_" + Vessel_Name
                        lnkDocID1.NavigateUrl = "../Uploads/FMS/" + dr["DocumentPath"].ToString();

                        hdnDocPath.Value = "../Uploads/FMS/" + dr["DocumentPath"].ToString();
                    }
                }
                else
                {
                    imgAddFollowup.Visible = false;
                    ImgAddAttachment.Visible = false;
                    dlApprovalLevel.Visible = false;
                    lblApl.Visible = false;

                    for (int i = 0; i < gvAttachment.Rows.Count; i++)
                    {
                        ((ImageButton)gvAttachment.Rows[i].Cells[2].FindControl("imgbtnDelete")).Visible = true;
                    }
                }

            }
            hdnScheduleStatusID.Value = dr["ID"].ToString();
            Session["FMS_SchStatusID"] = dr["ID"].ToString();

            Session["FMS_SchOfficeID"] = dr["Office_ID"].ToString();
            Session["FMS_SchVesselID"] = dr["Vessel_ID"].ToString();
            ViewState["FMS_CompletionDate"] = dr["Completion_Date"].ToString();
            ViewState["FMS_ApprovedStatus"] = dr["Approved_Status"].ToString();
            DataSet ds1 = new DataSet();
            ds1 = objFMS.FMS_Get_ScheduleStatusVoyageInfo(UDFLib.ConvertToInteger(dr["ID"].ToString()), UDFLib.ConvertToInteger(dr["Office_ID"].ToString()), UDFLib.ConvertToInteger(dr["Vessel_ID"].ToString()));

            if (ds1.Tables[0].Rows.Count > 0)
            {
                frmVoyage.DataSource = ds1.Tables[0];
                frmVoyage.DataBind();
                /* Added condition to check wheather Master/Chief Engineer Photo Exists or Not. */
                if (ds1.Tables[0].Rows[0]["VSLCE_PHOTOURL"].ToString() == "")
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "HidePhotoURL", "HideCEImage();", true);
                }
                if (ds1.Tables[0].Rows[0]["VSL2E_PHOTOURL"].ToString() == "")
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "HidePhotoURL1", "HideMasterImage();", true);
                }

            }
            if (btnRework.Visible == true)
            {
                if (dr["Completion_Date"].ToString() != "" && dr["Approved_Status"].ToString() == "")
                {
                    btnRework.Visible = true;

                }
                else
                {
                    btnRework.Visible = false;
                }

            }


            int rowcount = 0;
            int FileID = UDFLib.ConvertToInteger(Request.QueryString["DocID"] == null ? hdnFrmRecDocID.Value.ToString() : Request.QueryString["DocID"].ToString());
            DataSet ds2 = objFMS.FMS_Get_ScheduleFileApprovalByFileID(UDFLib.ConvertToInteger(dr["ID"].ToString()), UDFLib.ConvertToInteger(dr["Vessel_ID"].ToString()), UDFLib.ConvertToInteger(dr["Office_ID"].ToString()), FileID, GetSessionUserID(), ref rowcount);

            if (BtnApprove.Visible == true)
            {
                if (rowcount > 0)
                {
                    ViewState["FMS_ApprovalLevel"] = ds2.Tables[0].Rows[0]["Approval_Level"].ToString();
                    BtnApprove.Visible = true;
                    btnRework.Visible = true;


                }
                else
                {
                    BtnApprove.Visible = false;
                    btnRework.Visible = false;

                }
                // Added By kavita : For "Completed" status rework option should be available.
                // if (ds2.Tables[1].Rows.Count>0)  
                if (ds2.Tables[1].Rows[0][0].ToString() == "1")
                {
                    ViewState["FMS_ApprovalLevel"] = 0;
                    btnRework.Visible = true;
                    BtnApprove.Visible = false;
                }
            }
            if (ViewState["FMS_CompletionDate"].ToString() != "" && ViewState["FMS_ApprovedStatus"].ToString() == "")
            {
                imgAddFollowup.Visible = true;
                ImgAddAttachment.Visible = true;
            }
            else
            {
                imgAddFollowup.Visible = false;
                ImgAddAttachment.Visible = false;
            }
            updVesselHist.Update();
            BindApprovalLevel();

        }

        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string jsSqlError2 = "alert('" + ex.Message + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "jsSqlError2", jsSqlError2, true);
        }
    }

    /// <summary>
    /// this is a method for the convert the date in givien format by the user.
    /// </summary>
    /// <param name="strDT"></param>
    /// <param name="format"></param>
    /// <returns></returns>
    public string ConvertDateToString(string strDT, string format)
    {
        string strRetDt = strDT;
        if (strDT != "")
        {
            DateTime dt = Convert.ToDateTime(strDT);
            strRetDt = dt.ToString(format);
        }
        return strRetDt;
    }

    /// <summary>
    /// get the latest file path for the particular file id.
    /// </summary>
    /// <returns></returns>
    public string GetDocPath()
    {
        return LatestFilePath;
    }

    /// <summary>
    /// get the latest file ID for the particular file id.
    /// </summary>
    /// <returns></returns>
    public string GetDocID()
    {
        return DocID;
    }

    /// <summary>
    /// get the latest file status for the latest file id.
    /// </summary>
    /// <returns></returns>
    public string GetStatus()
    {
        return OppStatus;
    }

    /// <summary>
    /// get the latest User ID for the particular check out file id.
    /// </summary>
    /// <returns></returns>
    public string CheckOutUserID()
    {
        return OppUserID;
    }

    protected void btnDeleteE_Click(object sender, EventArgs e)
    {
        try
        {

            objFMS.Delete_DMSFile_Folder(UDFLib.ConvertToInteger(Request.QueryString["DocID"] == null ? hdnFrmRecDocID.Value : Request.QueryString["DocID"]), GetSessionUserID());

            string jsFile = "alert('Form archived successfully.');parent.ChildCallBackDelete();";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", jsFile, true);
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string jsSqlError2 = "alert('" + ex.Message + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "jsSqlError2", jsSqlError2, true);
        }

    }

    protected void btnLoadFiles_Click(object sender, EventArgs e)
    {
        try
        {
            LoadFiles(null, null);

            if (ViewState["FMS_CompletionDate"].ToString() != "" && ViewState["FMS_ApprovedStatus"].ToString() == "")
            {
                imgAddFollowup.Visible = true;
                ImgAddAttachment.Visible = true;
            }
            else
            {

                imgAddFollowup.Visible = false;
                ImgAddAttachment.Visible = false;
            }
            int RowIndex = UDFLib.ConvertToInteger(ViewState["FMSRow_Index"].ToString());
            string strRRow = " if( $('[id$=itemRow]').length>0) { $('[id$=itemRow]')[" + RowIndex + "].className='highlightRow';}";
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "strRRow" + RowIndex, strRRow, true);
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string jsSqlError2 = "alert('" + ex.Message + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "jsSqlError2", jsSqlError2, true);
        }

    }

    public void LoadFiles(object s, EventArgs e)
    {
        try
        {
            int ScheduleStatusID = UDFLib.ConvertToInteger(Session["FMS_SchStatusID"].ToString());
            int SchOfficeID = UDFLib.ConvertToInteger(Session["FMS_SchOfficeID"].ToString());
            int SchVesselID = UDFLib.ConvertToInteger(Session["FMS_SchVesselID"].ToString());


            gvAttachment.DataSource = objFMS.Get_DocSchedule_Attachments(ScheduleStatusID, SchOfficeID, SchVesselID);
            gvAttachment.DataBind();




            DataSet ds1 = new DataSet();
            ds1 = objFMS.FMS_Get_ScheduleStatusVoyageInfo(UDFLib.ConvertToInteger(Session["FMS_SchStatusID"].ToString()), UDFLib.ConvertToInteger(Session["FMS_SchOfficeID"].ToString()), UDFLib.ConvertToInteger(Session["FMS_SchVesselID"].ToString()));

            frmVoyage.DataSource = ds1.Tables[0];
            frmVoyage.DataBind();

            if (btnRework.Visible == true)
            {
                if (ViewState["FMS_CompletionDate"].ToString() != "" && ViewState["FMS_ApprovedStatus"].ToString() == "")
                {
                    btnRework.Visible = true;

                }
                else
                {
                    btnRework.Visible = false;

                }

            }


            int rowcount = 0;
            int FileID = UDFLib.ConvertToInteger((Request.QueryString["DocID"] == null ? hdnFrmRecDocID.Value : Request.QueryString["DocID"]).ToString());
            DataSet ds2 = objFMS.FMS_Get_ScheduleFileApprovalByScheduleID(UDFLib.ConvertToInteger(Session["FMS_SchStatusID"].ToString()), UDFLib.ConvertToInteger(Session["FMS_SchVesselID"].ToString()), UDFLib.ConvertToInteger(Session["FMS_SchOfficeID"].ToString()), FileID, GetSessionUserID(), ref rowcount);

            if (BtnApprove.Visible == true)
            {
                if (rowcount > 0)
                {
                    ViewState["FMS_ApprovalLevel"] = ds2.Tables[0].Rows[0]["Approval_Level"].ToString();
                    BtnApprove.Visible = true;

                }
                else
                {
                    BtnApprove.Visible = false;

                }
            }


        }
        catch { }

    }

    public void LoadFollowup()
    {

    }

    public void imgbtnDelete_Click(object s, EventArgs e)
    {
        try
        {

            int Code = int.Parse(((ImageButton)s).CommandArgument.Split(new char[] { ',' })[0]);
            int OfficeID = int.Parse(((ImageButton)s).CommandArgument.Split(new char[] { ',' })[2]);
            int VesselID = int.Parse(((ImageButton)s).CommandArgument.Split(new char[] { ',' })[3]);
            int res = objFMS.Delete_DocSchedule_Attachments(Code, GetSessionUserID(), OfficeID, VesselID);
            if (res > 0)
            {
                File.Delete(Server.MapPath(((ImageButton)s).CommandArgument.Split(new char[] { ',' })[1]));
            }
            LoadFiles(null, null);
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string jsSqlError2 = "alert('" + ex.Message + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "jsSqlError2", jsSqlError2, true);
        }

    }

    protected void AjaxFileUpload1_OnUploadComplete(object sender, AjaxFileUploadEventArgs file)
    {
        try
        {
            int ScheduleStatusID = UDFLib.ConvertToInteger(Session["FMS_SchStatusID"].ToString());
            int SchOfficeID = UDFLib.ConvertToInteger(Session["FMS_SchOfficeID"]);
            int UserID = GetSessionUserID();

            Byte[] fileBytes = file.GetContents();
            string sPath = Server.MapPath("\\" + System.Configuration.ConfigurationManager.AppSettings["APP_NAME"].ToString() + "\\Uploads\\FMS");
            Guid GUID = Guid.NewGuid();

            string Flag_Attach = "FMS_" + GUID.ToString() + Path.GetExtension(file.FileName);
            int sts = objFMS.Insert_DocSchedule_Attachment(ScheduleStatusID, Path.GetFileName(file.FileName), Flag_Attach, UserID, SchOfficeID, UDFLib.ConvertToInteger(Session["FMS_SchVesselID"].ToString()));
            string FullFilename = Path.Combine(sPath, "FMS_" + GUID.ToString() + Path.GetExtension(file.FileName));

            FileStream fileStream = new FileStream(FullFilename, FileMode.Create, FileAccess.ReadWrite);
            fileStream.Write(fileBytes, 0, fileBytes.Length);
            fileStream.Close();

        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string jsSqlError2 = "alert('" + ex.Message + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "jsSqlError2", jsSqlError2, true);
        }

    }

    protected void btnSaveRemark_Click(object s, EventArgs e)
    {


        if (txtNewRemark.Text.Trim() != "")
        {
            try
            {
                int ScheduleStatusID = UDFLib.ConvertToInteger(hdnScheduleStatusID.Value);
                int SchOfficeID = UDFLib.ConvertToInteger(hdnSchOfficeID.Value);
                int SchVesselID = UDFLib.ConvertToInteger(Session["FMS_SchVesselID"].ToString());
                gvFollowup.DataSource = objFMS.Insert_DocSchedule_Remark(txtNewRemark.Text, ScheduleStatusID, GetSessionUserID(), SchOfficeID, SchVesselID);
                gvFollowup.DataBind();
                txtNewRemark.Text = "";
                string js1 = " hideModal('dvInsRemark');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "hdFwp", js1, true);
            }
            catch (Exception ex)
            {
                UDFLib.WriteExceptionLog(ex);
                string jsSqlError2 = "alert('" + ex.Message + "');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "jsSqlError2", jsSqlError2, true);
            }
        }
        else
        {
            string js = " alert('Please Enter Followup.');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msgApp", js, true);
        }
    }

    protected void BtnAppRemark_Click(object s, EventArgs e)
    {
        try
        {

            int FileID = UDFLib.ConvertToInteger((Request.QueryString["DocID"] == null ? hdnFrmRecDocID.Value : Request.QueryString["DocID"]).ToString());

            int FormStatus = Convert.ToInt32(objFMS.FMS_Insert_ScheduleFileApproval(UDFLib.ConvertToInteger(Session["FMS_SchStatusID"].ToString()), UDFLib.ConvertToInteger(Session["FMS_SchOfficeID"].ToString()), UDFLib.ConvertToInteger(Session["FMS_SchVesselID"].ToString()), FileID, txtAppRemark.Text, GetSessionUserID(), GetSessionUserID(), UDFLib.ConvertToInteger(lblLatestVersion.Text), UDFLib.ConvertToInteger(ViewState["FMS_ApprovalLevel"].ToString()), 1));

            if (FormStatus != 0)
            {
                if (FormStatus == 1)
                {
                    string js = " alert('Form already has been approved.');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msgApp", js, true);
                }
                else if (FormStatus == 2)
                {
                    string js = " alert('Form already has been reworked.');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msgApp", js, true);
                }
            }
            else
            {

                if (Session["FMSMODE"] != null)
                {
                    if (Session["FMSMODE"].ToString() == "Pending")
                    {
                        Session["MODEFMS"] = "Pending";
                    }
                    else
                    {
                        Session["MODEFMS"] = null;
                    }
                }
                string js = " alert('Form approved successfully.');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msgApp", js, true);

            }
            string js1 = " hideModal('dvAppRemark');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "hdAppModal", js1, true);
            LoadDocScheduleGrid();
            GetApprovalHiostory();
            BindOperationGrid(UDFLib.ConvertToInteger(DocID == null ? hdnFrmRecDocID.Value : DocID));
            txtAppRemark.Text = "";
            btnMainsearch_Click(s, e);// Added on DT:09-07-2016 || To retain previous selection of 'Pending for Approval' tab when after user approve forms and page refresh.
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string jsSqlError2 = "alert('" + ex.Message + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "jsSqlError2", jsSqlError2, true);
        }

    }

    protected void btnRefresh_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ddlStatus.SelectedValue = "0";
            ddlVessel.SelectedValue = "0";
            ddlFleet.SelectedValue = "0";
            updRAForms.Update();
            txtFromDate.Text = "";
            txtTillDate.Text = "";
            LoadDocScheduleGrid();
            if (ViewState["FMS_CompletionDate"] != null && ViewState["FMS_ApprovedStatus"] != null)
            {
                if (ViewState["FMS_CompletionDate"].ToString() != "" && ViewState["FMS_ApprovedStatus"].ToString() == "")
                {
                    imgAddFollowup.Visible = true;
                    ImgAddAttachment.Visible = true;
                }
                else
                {
                    imgAddFollowup.Visible = false;
                    ImgAddAttachment.Visible = false;
                }
            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string jsSqlError2 = "alert('" + ex.Message + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "jsSqlError2", jsSqlError2, true);
        }
    }

    protected void btnFilter_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            LoadDocScheduleGrid();
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string jsSqlError2 = "alert('" + ex.Message + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "jsSqlError2", jsSqlError2, true);
        }

    }

    protected void btnRework_Click(object sender, EventArgs e)
    {

        string js = "$('#dvReworkRemark').prop('title', 'Rework Schedule'); showModal('dvReworkRemark');";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "handleModal", js, true);

        string strRow = " if( $('[id$=itemRow]').length>0) { $('[id$=itemRow]')[" + ViewState["FMSRow_Index"] + "].className='highlightRow';}";
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "strRow" + ViewState["FMSRow_Index"], strRow, true);

    }

    protected void btnSaveRRemark_Click(object sender, EventArgs e)
    {
        if (txtRRemark.Text.Trim() != "")
        {
            try
            {

                int FileID = UDFLib.ConvertToInteger((Request.QueryString["DocID"] == null ? hdnFrmRecDocID.Value : Request.QueryString["DocID"]).ToString());

                int FormStatus = Convert.ToInt32(objFMS.FMS_Update_ScheduleStatusForRework(GetSessionUserID(), UDFLib.ConvertToInteger(Session["FMS_SchStatusID"].ToString()), UDFLib.ConvertToInteger(Session["FMS_SchOfficeID"].ToString()), UDFLib.ConvertToInteger(Session["FMS_SchVesselID"].ToString()), txtRRemark.Text, UDFLib.ConvertToInteger(lblLatestVersion.Text), UDFLib.ConvertToInteger(ViewState["FMS_ApprovalLevel"].ToString()), FileID));
                if (FormStatus != 0)
                {
                    if (FormStatus == 1)
                    {
                        string js = " alert('Form already has been approved.');";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "msgApp", js, true);
                    }
                    else if (FormStatus == 2)
                    {
                        string js = " alert('Form already has been reworked.');";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "msgApp", js, true);
                    }
                }
                else
                {

                    if (Session["FMSMODE"] != null)
                    {
                        if (Session["FMSMODE"].ToString() == "Pending")
                        {
                            Session["MODEFMS"] = "Pending";

                        }
                        else
                        {
                            Session["MODEFMS"] = null;
                        }
                    }

                    string js = " alert('Form Re-work successfully.');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", js, true);

                }
                string js1 = " hideModal('dvReworkRemark');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "hdModal", js1, true);
                LoadDocScheduleGrid();
                GetApprovalHiostory();
                BindOperationGrid(UDFLib.ConvertToInteger(DocID == null ? hdnFrmRecDocID.Value : DocID));
                btnMainsearch_Click(sender, e); // Added on DT:09-07-2016 || To retain previous selection of 'Pending for Approval' tab when after user approve forms and page refresh.
                txtRRemark.Text = "";
            }
            catch (Exception ex)
            {
                UDFLib.WriteExceptionLog(ex);
                string jsSqlError2 = "alert('" + ex.Message + "');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "jsSqlError2", jsSqlError2, true);
            }
        }
        else
        {
            string js = " alert('Please Enter Remark.');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", js, true);
        }
    }

    protected void frmVoyage_DataBound(object sender, EventArgs e)
    {

    }

    protected void btnApprove_Click(object sender, EventArgs e)
    {
        string js = "   $('#dvAppRemark').prop('title', 'Approve Schedule'); showModal('dvAppRemark');";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "handleModal", js, true);

        string strRow = " if( $('[id$=itemRow]').length>0) { $('[id$=itemRow]')[" + ViewState["FMSRow_Index"] + "].className='highlightRow';}";
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "strRow" + ViewState["FMSRow_Index"], strRow, true);
    }

    protected void dlApprovalLevel_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        DataSet dsTemp = objFMS.FMS_Get_ApprovedSchedule(UDFLib.ConvertToInteger(Session["FMS_SchStatusID"].ToString()), UDFLib.ConvertToInteger(Session["FMS_SchOfficeID"].ToString()), UDFLib.ConvertToInteger(Session["FMS_SchVesselID"].ToString()), UDFLib.ConvertToInteger((Request.QueryString["DocID"] == null ? hdnFrmRecDocID.Value : Request.QueryString["DocID"]).ToString()));
        Label lbllevels = (Label)e.Item.FindControl("lbllevel");
        Label lblApproverRemark = (Label)e.Item.FindControl("lblApproverRemark");
        Panel pnlApp = (Panel)e.Item.FindControl("pnlApp");
        Label lblHeader1 = (Label)e.Item.FindControl("lblHeader1");
        Label lblHeader3 = (Label)e.Item.FindControl("lblHeader3");
        Label lblHeader5 = (Label)e.Item.FindControl("lblHeader5");
        Label lblHeader2 = (Label)e.Item.FindControl("lblHeader2");
        Label lblHeader4 = (Label)e.Item.FindControl("lblHeader4");
        TextBox lblHeader6 = (TextBox)e.Item.FindControl("lblHeader6");
        HtmlTable tblpnlApp = (HtmlTable)e.Item.FindControl("tbl");

        for (int i = 0; i < dsTemp.Tables[0].Rows.Count; i++)
        {

            if ((lbllevels.Text == "Level " + dsTemp.Tables[0].Rows[i]["Approval_Level"].ToString()) && (dsTemp.Tables[0].Rows[i]["Approval_Status"].ToString() == "1"))
            {
                lbllevels.Visible = true;
                tblpnlApp.Visible = true;

                lbllevels.BackColor = ColorTranslator.FromHtml("#66FF99");

                lblHeader6.Text = dsTemp.Tables[0].Rows[i]["Remark"].ToString();
                lblHeader2.Text = dsTemp.Tables[0].Rows[i]["APPROVEDBY"].ToString();
                lblHeader4.Text = dsTemp.Tables[0].Rows[i]["Date_Of_Approval"].ToString();
                lblHeader6.Attributes.Add("onMouseOver", "javascript:js_ShowToolTip('" + dsTemp.Tables[0].Rows[i]["Remark"].ToString() + "',event,this)");

            }
            else if ((lbllevels.Text == "Level " + dsTemp.Tables[0].Rows[i]["Approval_Level"].ToString()) && (dsTemp.Tables[0].Rows[i]["Approval_Status"].ToString() == "0"))
            {
                lbllevels.BackColor = Color.Red;
                lbllevels.Attributes.Add("onMouseOver", "javascript:js_ShowToolTip('" + dsTemp.Tables[0].Rows[i]["Status"].ToString() + "',event,this)");
            }

        }
    }

    protected void BindRAFormList(int FileID)
    {
        DataTable dt = objFMS.Get_RAFormsByDocID(FileID);
        if (dt.Rows.Count == 0)
        {
            if (pnlRAForms.Visible == true)
            {
                pnlRAForms.Visible = false;
            }
        }
        else
        {
            if (pnlRAForms.Visible == false)
            {
                pnlRAForms.Visible = true;
            }
        }

        dlRAForms.DataSource = dt;
        dlRAForms.DataBind();
        updRAForms.Update();
    }
    /// <summary>
    /// Bind RA forms in Edit form pop window
    /// </summary>
    /// <param name="FileID">Selected file id</param>
    protected void BindEditRAFormList(int FileID)
    {
        DataTable dt = objFMS.Get_RAFormsByDocID(FileID);
        if (dt.Rows.Count > 0)
        {

            dlRAFormsEdit.DataSource = dt;
            dlRAFormsEdit.DataBind();
            ViewState["Cat_Table"] = dt;
            dvRAEditForms.Visible = true;
        }
        else
        {            
            dvRAEditForms.Visible = false;
        }
        updAttachedRAF.Update();
    }
    /// <summary>
    /// Method used to retrived RA forms list
    /// </summary>
    /// <param name="schduleID">selected form scheduled ID</param>
    /// <param name="vesselID">Vessel ID on which form is schedule</param>
    protected void Bind_ScheduleRAFormList(int schduleID, int vesselID)
    {
        try
        {
            DataTable dt = objFMS.Get_RAFormsBy_ScheduleID(schduleID, vesselID);
            if (dt.Rows.Count == 0)
            {
                pnlRAForms.Visible = false;
                divRA.Visible = false;
            }
            else
            {
                pnlRAForms.Visible = true;
                divRA.Visible = true;
            }
            dlVessel_RAForms.DataSource = dt;
            dlVessel_RAForms.DataBind();
            upVessel_RAForms.Update();
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string jsSqlError2 = "alert('" + ex.Message + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "jsSqlError2", jsSqlError2, true);
        }
    }

    protected void btnGetFileDtl_Click(object sender, EventArgs e)
    {
        CommandEventArgs a = new CommandEventArgs("onDocumentClick", hdnVSL.Value + "," + hdnSchID.Value + "," + hdnStaID.Value + "," + hdnOffID.Value + "," + hdnIndex.Value);
        onDocumentClick(this, a);
    }
    /// <summary>
    /// Set filter Mode and Period value according to the selection and pass Period parameter to another method.
    /// </summary>

    protected void btnGetFormsReceived_Click(object sender, EventArgs e)
    {
        try
        {
            string Period = "";
            if (btnReceived.Enabled == false)
            {
                Mode = "Received";
                Period = hdnPeriodRec.Value;
            }
            else if (btnPendingApp.Enabled == false)
            {
                Mode = "Pending";
                Period = hdnPeriodRec.Value;
                //Session[FormReceivedPeriod] = Period;
            }
            else
            {
                Mode = "Due";
                Period = hdnPeriodDue.Value;
            }

            BindFormsReceived(Period);

            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "Confirm();", true);
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string jsFError = " alert('" + ex + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "jsFError", jsFError, true);
        }

    }

    /// <summary>
    /// This method edited by Pranav Sakpal on 29-07-2016  for handling datatable null.
    /// Method is use to retrive forms details and bind details to the control.
    /// </summary>
    /// <param name="Period">Selected No .of Days where form is pending for approval/Due,Overdue/Received.</param>
    protected void BindFormsReceived(string Period)
    {
        try
        {
            divApprovalMessage.Visible = false;
            int? IsApproved = Convert.ToInt32(rblPending.SelectedItem.Value);
            int? ApprovedID = (GetCheckboxItems());
            DataTable dtVessel = new DataTable();
            dtVessel.Columns.Add("VID");
            if (ddlVesselSearch.SelectedValues.Rows.Count > 0)
            {
                foreach (DataRow dr in ddlVesselSearch.SelectedValues.Rows)
                {
                    dtVessel.Rows.Add(dr[0]);
                }

            }
            else
            {
                DataTable dtTemp = (DataTable)ViewState["dtVesselSearch"];
                if (dtTemp != null)
                {
                    foreach (DataRow dr in dtTemp.Rows)
                    {
                        dtVessel.Rows.Add(dr[0]);
                    }
                }

            }
            // Added By Kavita : 22-06-2016 , Added new filter parameters.

            int? VesselAssignUserID = (GetUserVesselAssignmetItem());
            int? FleetID = UDFLib.ConvertIntegerToNull(ddlFleetSearch.SelectedValue);
            int? StatusID = UDFLib.ConvertIntegerToNull(ddlStatusSearch.SelectedValue);
            int? DepartmentID = UDFLib.ConvertIntegerToNull(ddlDepartmentSearch.SelectedValue);
            string SearchBy = UDFLib.ConvertStringToNull(txtMainSearch.Text.Trim());
            int? FormType = UDFLib.ConvertIntegerToNull(ddlFormTypeSearch.SelectedValue);
            DataSet ds = objFMS.FMS_Get_FormReceived_ByDate(Period, Mode, IsApproved, ApprovedID, VesselAssignUserID, FleetID, dtVessel, StatusID, DepartmentID, SearchBy, FormType);  // Passing five extra parameters VesselAssignUserID ,Fleet,Vessel,Status and Department and serach by form name. 

            if (Mode == "Received" || Mode == "Pending")
            {
                UDFLib.AddParentTable(ds.Tables[0], "Events", new string[] { "Completion_Date" },
                new string[] { }, "EventMembers");
            }
            else
            {
                UDFLib.AddParentTable(ds.Tables[0], "Events", new string[] { "Schedule_Date" },
                    new string[] { }, "EventMembers");
            }
            ds.Tables[0].Columns.Add("Index");
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                ds.Tables[0].Rows[i]["Index"] = i;
            }

            MainRepeater.DataSource = ds;
            MainRepeater.DataMember = "Events";
            MainRepeater.DataBind();
            lnkPeriod.InnerText = Period;

        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string jsFError = " alert('" + ex + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "jsFError", jsFError, true);
        }
    }

    /// <summary>
    /// When Forms Received tab is selected.|| To display related information
    /// </summary>
    /// <param name="s"></param>
    /// <param name="e"></param>
    protected void FormsReceived(object s, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "FReceived", " document.getElementById('dvDocDetails').style.display = 'none';", true);
        btnReceived.CssClass = "FormsReceived";
        btnReceived.Enabled = false;
        btnPending.CssClass = "FormsNotReceived";
        btnPendingApp.CssClass = "FormsNotReceived";
        lblFormsStatus.Text = "Forms received in the last";
        btnPending.Enabled = true;
        btnPendingApp.Enabled = true;
        if (hdnPeriodRec.Value == "")
            hdnPeriodRec.Value = "7";
        Mode = "Received";
        BindFormsReceived(hdnPeriodRec.Value);
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "Confirm();", true);
        //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "GridColour();", true);
        rblPending.Visible = false;
        chkUser.Visible = false;
        lblhdrStatus.Visible = false;
        lblMyApprovals.Visible = false;
        tblApp.Visible = false;

        Session["FMSMODE"] = "Received";
    }

    /// <summary>
    /// When 'Forms Due/Overdue' tab is selected.|| To display related information.
    /// </summary>
    /// <param name="s"></param>
    /// <param name="e"></param>
    protected void FormsPending(object s, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "FPending", " document.getElementById('dvDocDetails').style.display = 'none';", true);
        btnPending.CssClass = "FormsReceived";
        btnPending.Enabled = false;
        btnReceived.CssClass = "FormsNotReceived";
        btnPendingApp.CssClass = "FormsNotReceived";
        lblFormsStatus.Text = "Overdue Forms and Forms Due in the Next";
        btnReceived.Enabled = true;
        btnPendingApp.Enabled = true;
        if (hdnPeriodDue.Value == "")
            hdnPeriodDue.Value = "7";
        Mode = "Due";
        BindFormsReceived(hdnPeriodDue.Value);
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "Confirm();", true);
        // ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "GridColour();", true);
        rblPending.Visible = false;
        chkUser.Visible = false;
        lblhdrStatus.Visible = false;
        lblMyApprovals.Visible = false;
        tblApp.Visible = false;

        Session["FMSMODE"] = "Due";
    }

    /// <summary>
    /// When 'Pending For Approval' tab is selected.|| To display related information.
    /// </summary>
    /// <param name="s"></param>
    /// <param name="e"></param>
    protected void FormsPendingApprover(object s, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "FPendingAppr", " document.getElementById('dvDocDetails').style.display = 'none';", true);
        btnPendingApp.CssClass = "FormsReceived";
        btnPendingApp.Enabled = false;
        btnReceived.CssClass = "FormsNotReceived";
        btnPending.CssClass = "FormsNotReceived";
        lblFormsStatus.Text = "Forms Pending for Approval in the Next ";
        btnReceived.Enabled = true;
        btnPending.Enabled = true;
        if (hdnPeriodRec.Value == "")
            hdnPeriodRec.Value = "7";
        Mode = "Pending";
        BindFormsReceived(hdnPeriodRec.Value);
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "Confirm();", true);
        //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "GridColour();", true);
        rblPending.Visible = true;
        chkUser.Visible = true;
        lblhdrStatus.Visible = true;
        lblMyApprovals.Visible = true;
        tblApp.Visible = true;

        Session["FMSMODE"] = "Pending";
    }

    private int? GetCheckboxItems()
    {
        int? retval = null;
        List<string> selectedValues = chkUser.Items.Cast<ListItem>()
                    .Where(li => li.Selected)
                    .Select(li => li.Value)
                    .ToList();
        if (selectedValues.Contains("1"))
        {

            retval = GetSessionUserID();
        }
        else
        {
            retval = null;
        }
        return retval;
    }

    /// <summary>
    /// Added by Kavita : DT 22-06-2016
    /// Get user Id of a login User for Vessel Assignment Filter.
    /// </summary>
    /// <returns>Return User ID</returns>
    private int? GetUserVesselAssignmetItem()
    {
        int? retUserID = null;

        if (chkVesselAssign.Checked == true)
        {

            retUserID = GetSessionUserID();

        }
        else
        {
            retUserID = null;
        }
        return retUserID;

    }

    protected void rblPending_SelectedIndexChanged(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "FPending", " document.getElementById('dvDocDetails').style.display = 'none';", true);
        btnGetFormsReceived_Click(null, null);
    }

    protected void chkUser_SelectedIndexChanged(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "FPending", " document.getElementById('dvDocDetails').style.display = 'none';", true);
        btnGetFormsReceived_Click(null, null);


    }

    /// <summary>
    /// Event is use to call function that retrive forms details according to vessels assigned to login user.
    /// </summary>
    protected void chkVesselAssign_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            BindVesselList();

            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "FPending", " document.getElementById('dvDocDetails').style.display = 'none';", true);
            hdnPeriodRec.Value = lnkPeriod.InnerText; // Reset hdnPeriodRec because first time on check "By Vessel Assignment" hdnPeriodRec value is getting null.
            btnGetFormsReceived_Click(null, null);

        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string jsSqlError2 = "alert('" + ex.Message + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "jsSqlError2", jsSqlError2, true);
        }

    }
   protected void cdcatalog_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        try
        {
            int StatusID, OfficeID, VesselID;
            string FormStatus = "";
            string tooltip = "";
            if (e.Item.ItemType == ListItemType.Header)
            {
                HtmlTableCell tdUser = (HtmlTableCell)e.Item.FindControl("tdUser");
                e.Item.FindControl("tdUser");
                if (Mode == "Pending")
                {
                    tdUser.Visible = true;
                }
            }
            if (e.Item.ItemType != ListItemType.Item && e.Item.ItemType != ListItemType.AlternatingItem)
                return;

            DataRow dr = (DataRow)e.Item.DataItem;
            StatusID = Convert.ToInt32(dr["ID"]);
            OfficeID = Convert.ToInt32(dr["Office_ID"]);
            VesselID = Convert.ToInt32(dr["Vessel_ID"]);
            FormStatus = dr["FormStatus"].ToString();

            Label lblID = (Label)e.Item.FindControl("lblID");

            if (FormStatus != "6")
            {
                tooltip = ShowTooltip(Convert.ToString(dr["DocID"]), "", StatusID, OfficeID, VesselID);
            }
            else
            {
                tooltip = ShowTooltip(Convert.ToString(dr["DocID"]), "");
            }

            lblID.Attributes.Add("onMouseOver", "javascript:js_ShowToolTip('" + tooltip + "',event,this)");
            HtmlTableCell tdApp = (HtmlTableCell)e.Item.FindControl("tdApp");

            if (Mode == "Pending")
            {
                var col = e.Item.FindControl("lblrApprover");
                col.Visible = true;
                tdApp.Visible = true;
            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string jsSqlError2 = "alert('" + ex.Message + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "jsSqlError2", jsSqlError2, true);
        }


    }

    protected void HighlightSelectedRow(int RowIndex)
    {
        for (int i = 0; i < gvVesselDocHistory.Rows.Count; i++)
        {
            GridViewRow gvr = gvVesselDocHistory.Rows[i];
            if (RowIndex == i)
            {
                //Apply Yellow color to selected Row
                gvr.BackColor = ColorTranslator.FromHtml("#FFFFCC");
            }
            else
            {
                //Apply White color to rest of rows
                gvr.BackColor = ColorTranslator.FromHtml("#FFFFFF");
            }
        }


    }
    protected void HighlightSelectedItem(int ItemIndex, int ParentItemIndex)
    {

        {

            Repeater rptCat = (Repeater)MainRepeater.Items[ParentItemIndex].FindControl("cdcatalog");
            for (int j = 0; j < rptCat.Items.Count; j++)
            {
                if (ItemIndex == j)
                {


                    HtmlTableRow tr = (HtmlTableRow)rptCat.Items[j].FindControl("itemRow");



                    tr.Attributes.Add("style", "background-color:#FFFFCC;");


                }
                else
                {
                    HtmlTableRow tr = (HtmlTableRow)rptCat.Items[j].FindControl("itemRow");



                    tr.Attributes.Add("style", "background-color:#FFFFFF;");

                }
            }
        }


    }

    #region Show File Details on Tooltip

    /// <summary>
    /// Modified on DT:22-06-2016
    /// </summary>
    /// <param name="FileID">ID of selected file.</param>
    /// <param name="filename">Name of selected file.</param>
    /// <returns>Information related to selected file in HTML format.</returns>
    private string ShowTooltip(string FileID, string filename)
    {
        string ext = "";
        string FileInfo = string.Empty;
        int showArchivedForms;

        try
        {
            if (Path.GetExtension(filename).Length > 1)
                ext = Path.GetExtension(filename).Substring(1).ToLower();

            //To visible or not archived files
            if (Session["ShowArchivedForms"] == null)
            {
                showArchivedForms = 0;
            }
            else
            {
                showArchivedForms = Convert.ToInt32(Session["ShowArchivedForms"]);
            }

            DataSet dsGetLastestFileInfoByID = objFMS.GetLastestFileInfoByID(UDFLib.ConvertToInteger(FileID), showArchivedForms);

            if (dsGetLastestFileInfoByID != null)
            {
                if (dsGetLastestFileInfoByID.Tables[0].Rows.Count > 0)
                {
                    DataRow dr = dsGetLastestFileInfoByID.Tables[0].Rows[0];

                    string FileName = "<td><b>Form Name:</b></td><td> " + dr["LogFileID"].ToString() + "</td>";
                    string FormType = "<td><b>Form Type:</b></td><td> " + dr["Form_Type"].ToString() + "</td>";
                    string Department = "<td><b> Department:</b> </td><td> " + dr["Department"].ToString() + "</td>";
                    string CreatedBy = string.Empty;

                    if (dr["Created_User"].ToString() != "")
                        CreatedBy = "<td><b>Created By:</b></td><td> " + dr["Created_User"].ToString() + "</td>";
                    else
                        CreatedBy = "<td><b>Created By:</b></td><td> Office</td>";

                    string CreationDate = "<td><b>Creation Date:</b></td><td> " + dr["Date_Of_Creatation"].ToString() + "</td>";
                    string LastOperation = "<td><b>Last action:</b></td><td> ";
                    string LastOperationDate = "<td><b>Last action date:</b></td><td> ";

                    if (dr["Opp_User"].ToString() != "")
                    {
                        LastOperation = "<td><b>Last action:</b></td><td> " + dr["Operation_Type"].ToString() + " by " + dr["Opp_User"].ToString() + "</td>";
                        LastOperationDate = "<td><b>Last action date:</b></td><td> " + dr["Operation_date"].ToString() + "</td>";
                    }
                    string LatestVersion = string.Empty;

                    if (dr["Version"].ToString() != "")
                    {
                        LatestVersion = "<td><b>Version:</b></td><td> " + dr["Version"].ToString() + "</td>";
                    }

                    string Remark = "<td><b>Remarks:</b></td><td colspan=3> " + dsGetLastestFileInfoByID.Tables[0].Rows[0]["Remark"].ToString() + "</td>";
                    FileInfo = "<table><tr>" + FileName + " " + LatestVersion + "</tr><tr>" + FormType + " " + Department + "</tr><tr>" + CreatedBy + " " + LastOperation + "</tr><tr>" + CreationDate + " " + LastOperationDate + "</tr>" + Remark + "</table>";
                }
            }

        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string jsFError = " alert('" + ex + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "jsFError", jsFError, true);
        }
        return FileInfo;
    }
    #endregion

    #region Overloaded method that Show File Details on Tooltip based on status Id,office Id ,vessel Id 

    /// <summary>
    /// Created On : 29 Sep 2016
    /// </summary>
    /// <param name="FileID">ID of selected file.</param>
    /// <param name="filename">Name of selected file.</param>
    /// <param name="StatusID">It is a File scheduled status ID.</param>
    /// <param name="OfficeID">It is a file scheduled office ID</param>
    /// <param name="VesselID">It is a vessel id on which file are scheduled</param>
    /// <returns>Information related to selected file in HTML format.</returns>
    private string ShowTooltip(string FileID, string filename, int StatusID, int OfficeID, int VesselID)
    {
        string ext = "";
        string FileInfo = string.Empty;
        int showArchivedForms;

        try
        {
            if (Path.GetExtension(filename).Length > 1)
                ext = Path.GetExtension(filename).Substring(1).ToLower();

            //To visible or not archived files
            if (Session["ShowArchivedForms"] == null)
            {
                showArchivedForms = 0;
            }
            else
            {
                showArchivedForms = Convert.ToInt32(Session["ShowArchivedForms"]);
            }

            DataSet dsGetLastestFileInfoByID = objFMS.GetLastestFileInfoByID(UDFLib.ConvertToInteger(FileID), showArchivedForms,StatusID,OfficeID,VesselID);

            if (dsGetLastestFileInfoByID != null)
            {
                if (dsGetLastestFileInfoByID.Tables[0].Rows.Count > 0)
                {
                    DataRow dr = dsGetLastestFileInfoByID.Tables[0].Rows[0];

                    string FileName = "<td><b>Form Name:</b></td><td> " + dr["LogFileID"].ToString() + "</td>";
                    string FormType = "<td><b>Form Type:</b></td><td> " + dr["Form_Type"].ToString() + "</td>";
                    string Department = "<td><b> Department:</b> </td><td> " + dr["Department"].ToString() + "</td>";
                    string CreatedBy = string.Empty;

                    if (dr["Created_User"].ToString() != "")
                        CreatedBy = "<td><b>Created By:</b></td><td> " + dr["Created_User"].ToString() + "</td>";
                    else
                        CreatedBy = "<td><b>Created By:</b></td><td> Office</td>";

                    string CreationDate = "<td><b>Creation Date:</b></td><td> " + dr["Date_Of_Creatation"].ToString() + "</td>";
                    string LastOperation = "<td><b>Last action:</b></td><td> ";
                    string LastOperationDate = "<td><b>Last action date:</b></td><td> ";

                    if (dr["Opp_User"].ToString() != "")
                    {
                        LastOperation = "<td><b>Last action:</b></td><td> " + dr["Operation_Type"].ToString() + " by " + dr["Opp_User"].ToString() + "</td>";
                        LastOperationDate = "<td><b>Last action date:</b></td><td> " + dr["Operation_date"].ToString() + "</td>";
                    }
                    string LatestVersion = string.Empty;

                    if (dr["Version"].ToString() != "")
                    {
                        LatestVersion = "<td><b>Version:</b></td><td> " + dr["Version"].ToString() + "</td>";
                    }

                    string Remark = "<td><b>Remarks:</b></td><td colspan=3> " + dsGetLastestFileInfoByID.Tables[0].Rows[0]["Remark"].ToString() + "</td>";
                    FileInfo = "<table><tr>" + FileName + " " + LatestVersion + "</tr><tr>" + FormType + " " + Department + "</tr><tr>" + CreatedBy + " " + LastOperation + "</tr><tr>" + CreationDate + " " + LastOperationDate + "</tr>" + Remark + "</table>";
                }
            }

        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string jsFError = " alert('" + ex + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "jsFError", jsFError, true);
        }
        return FileInfo;
    }
    #endregion
    



    public string ReturnExtension(string fileExtension)
    {
        switch (fileExtension)
        {
            case ".mdi": return "";
            case ".xml": return "application/rdf+xml";
            case ".323": return "text/h323";
            case ".acx": return "application/internet-property-stream";
            case ".ai": return "application/postscript";
            case ".aif": return "audio/x-aiff";
            case ".aifc": return "audio/x-aiff";
            case ".aiff": return "audio/x-aiff";
            case ".asf": return "video/x-ms-asf";
            case ".asr": return "video/x-ms-asf";
            case ".asx": return "video/x-ms-asf";
            case ".au": return "audio/basic";
            case ".avi": return "video/x-msvideo";
            case ".axs": return "application/olescript";
            case ".bas": return "text/plain";
            case ".bcpio": return "application/x-bcpio";
            case ".bin": return "application/octet-stream";
            case ".bmp": return "image/bmp";
            case ".c": return "text/plain";
            case ".cat": return "application/vnd.ms-pkiseccat";
            case ".cdf": return "application/x-cdf";
            case ".cer": return "application/x-x509-ca-cert";
            case ".class": return "application/octet-stream";
            case ".clp": return "application/x-msclip";
            case ".cmx": return "image/x-cmx";
            case ".cod": return "image/cis-cod";
            case ".cpio": return "application/x-cpio";
            case ".crd": return "application/x-mscardfile";
            case ".crl": return "application/pkix-crl";
            case ".crt": return "application/x-x509-ca-cert";
            case ".csh": return "application/x-csh";
            case ".css": return "text/css";
            case ".dcr": return "application/x-director";
            case ".der": return "application/x-x509-ca-cert";
            case ".dir": return "application/x-director";
            case ".dll": return "application/x-msdownload";
            case ".dms": return "application/octet-stream";
            case ".doc": return "application/msword";
            case ".dot": return "application/msword";
            case ".dvi": return "application/x-dvi";
            case ".dxr": return "application/x-director";
            case ".eps": return "application/postscript";
            case ".etx": return "text/x-setext";
            case ".evy": return "application/envoy";
            case ".exe": return "application/octet-stream";
            case ".flr": return "x-world/x-vrml";
            case ".gif": return "image/gif";
            case ".gtar": return "application/x-gtar";
            case ".gz": return "application/x-gzip";
            case ".h": return "text/plain";
            case ".hdf": return "application/x-hdf";
            case ".hlp": return "application/winhlp";
            case ".hqx": return "application/mac-binhex40";
            case ".hta": return "application/hta";
            case ".htc": return "text/x-component";
            case ".htm": return "text/html";
            case ".html": return "text/html";
            case ".htt": return "text/webviewhtml";
            case ".ico": return "image/x-icon";
            case ".ief": return "image/ief";
            case ".iii": return "application/x-iphone";
            case ".ins": return "application/x-internet-signup";
            case ".isp": return "application/x-internet-signup";
            case ".jfif": return "image/pipeg";
            case ".jpe": return "image/jpeg";
            case ".jpeg": return "image/jpeg";
            case ".jpg": return "image/jpeg";
            case ".js": return "application/x-javascript";
            case ".latex": return "application/x-latex";
            case ".lha": return "application/octet-stream";
            case ".lsf": return "video/x-la-asf";
            case ".lsx": return "video/x-la-asf";
            case ".lzh": return "application/octet-stream";
            case ".m13": return "application/x-msmediaview";
            case ".m14": return "application/x-msmediaview";
            case ".m3u": return "audio/x-mpegurl";
            case ".man": return "application/x-troff-man";
            case ".mdb": return "application/x-msaccess";
            case ".me": return "application/x-troff-me";
            case ".mht": return "message/rfc822";
            case ".mhtml": return "message/rfc822";
            case ".mid": return "audio/mid";
            case ".mny": return "application/x-msmoney";
            case ".mov": return "video/quicktime";
            case ".movie": return "video/x-sgi-movie";
            case ".mp2": return "video/mpeg";
            case ".mp3": return "audio/mpeg";
            case ".mp4": return "video/mp4";
            case ".mpa": return "video/mpeg";
            case ".mpe": return "video/mpeg";
            case ".mpeg": return "video/mpeg";
            case ".mpg": return "video/mpeg";
            case ".mpp": return "application/vnd.ms-project";
            case ".mpv2": return "video/mpeg";
            case ".ms": return "application/x-troff-ms";
            case ".mvb": return "application/x-msmediaview";
            case ".nws": return "message/rfc822";
            case ".oda": return "application/oda";
            case ".p10": return "application/pkcs10";
            case ".p12": return "application/x-pkcs12";
            case ".p7b": return "application/x-pkcs7-certificates";
            case ".p7c": return "application/x-pkcs7-mime";
            case ".p7m": return "application/x-pkcs7-mime";
            case ".p7r": return "application/x-pkcs7-certreqresp";
            case ".p7s": return "application/x-pkcs7-signature";
            case ".pbm": return "image/x-portable-bitmap";
            case ".pdf": return "application/pdf";
            case ".pfx": return "application/x-pkcs12";
            case ".pgm": return "image/x-portable-graymap";
            case ".pko": return "application/ynd.ms-pkipko";
            case ".pma": return "application/x-perfmon";
            case ".pmc": return "application/x-perfmon";
            case ".pml": return "application/x-perfmon";
            case ".pmr": return "application/x-perfmon";
            case ".pmw": return "application/x-perfmon";
            case ".pnm": return "image/x-portable-anymap";
            case ".pot": return "application/vnd.ms-powerpoint";
            case ".ppm": return "image/x-portable-pixmap";
            case ".pps": return "application/vnd.ms-powerpoint";
            case ".ppt": return "application/vnd.ms-powerpoint";
            case ".prf": return "application/pics-rules";
            case ".ps": return "application/postscript";
            case ".pub": return "application/x-mspublisher";
            case ".qt": return "video/quicktime";
            case ".ra": return "audio/x-pn-realaudio";
            case ".ram": return "audio/x-pn-realaudio";
            case ".ras": return "image/x-cmu-raster";
            case ".rgb": return "image/x-rgb";
            case ".rmi": return "audio/mid";
            case ".roff": return "application/x-troff";
            case ".rtf": return "application/rtf";
            case ".rtx": return "text/richtext";
            case ".scd": return "application/x-msschedule";
            case ".sct": return "text/scriptlet";
            case ".setpay": return "application/set-payment-initiation";
            case ".setreg": return "application/set-registration-initiation";
            case ".sh": return "application/x-sh";
            case ".shar": return "application/x-shar";
            case ".sit": return "application/x-stuffit";
            case ".snd": return "audio/basic";
            case ".spc": return "application/x-pkcs7-certificates";
            case ".spl": return "application/futuresplash";
            case ".src": return "application/x-wais-source";
            case ".sst": return "application/vnd.ms-pkicertstore";
            case ".stl": return "application/vnd.ms-pkistl";
            case ".stm": return "text/html";
            case ".svg": return "image/svg+xml";
            case ".sv4cpio": return "application/x-sv4cpio";
            case ".sv4crc": return "application/x-sv4crc";
            case ".t": return "application/x-troff";
            case ".tar": return "application/x-tar";
            case ".tcl": return "application/x-tcl";
            case ".tex": return "application/x-tex";
            case ".texi": return "application/x-texinfo";
            case ".texinfo": return "application/x-texinfo";
            case ".tgz": return "application/x-compressed";
            case ".tif": return "image/tiff";
            case ".tiff": return "image/tiff";
            case ".tr": return "application/x-troff";
            case ".trm": return "application/x-msterminal";
            case ".tsv": return "text/tab-separated-values";
            case ".txt": return "text/plain";
            case ".uls": return "text/iuls";
            case ".ustar": return "application/x-ustar";
            case ".vcf": return "text/x-vcard";
            case ".vrml": return "x-world/x-vrml";
            case ".wav": return "audio/x-wav";
            case ".wcm": return "application/vnd.ms-works";
            case ".wdb": return "application/vnd.ms-works";
            case ".wks": return "application/vnd.ms-works";
            case ".wmf": return "application/x-msmetafile";
            case ".wps": return "application/vnd.ms-works";
            case ".wri": return "application/x-mswrite";
            case ".wrl": return "x-world/x-vrml";
            case ".wrz": return "x-world/x-vrml";
            case ".xaf": return "x-world/x-vrml";
            case ".xbm": return "image/x-xbitmap";
            case ".xla": return "application/vnd.ms-excel";
            case ".xlc": return "application/vnd.ms-excel";
            case ".xlm": return "application/vnd.ms-excel";
            case ".xls": return "application/vnd.ms-excel";
            case ".xlt": return "application/vnd.ms-excel";
            case ".xlw": return "application/vnd.ms-excel";
            case ".xof": return "x-world/x-vrml";
            case ".xpm": return "image/x-xpixmap";
            case ".xwd": return "image/x-xwindowdump";
            case ".z": return "application/x-compress";
            case ".zip": return "application/zip";
            default: return "application/octet-stream";


        }
    }



    protected void OnClick_lbtnRAForms(object sender, EventArgs e)
    {
        try
        {

            Bind_RAFormCategory("");

            string AddSalStrmodal = String.Format("showModal('divRACategory',false);");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddSalStrmodal", AddSalStrmodal, true);
            udpRACategory.Update();
        }
        catch (Exception)
        {

            throw;
        }
    }

    private void Bind_RAFormCategory(string Category_Name)
    {
        try
        {

            DataTable dt = objFMS.Get_WorkCategoryList(Category_Name);
            chklRAF_Category.DataSource = dt;
            chklRAF_Category.DataTextField = "Work_Category_Name";
            chklRAF_Category.DataValueField = "Work_Categ_ID";
            chklRAF_Category.DataBind();


            for (int i = 0; i < dlRAFormsEdit.Items.Count; i++)
            {
                HiddenField hlRaForm = (HiddenField)dlRAFormsEdit.Items[i].FindControl("hdnRAFrm");
                for (int j = 0; j < chklRAF_Category.Items.Count; j++)
                {
                    if (hlRaForm.Value == chklRAF_Category.Items[j].Value)
                    {
                        chklRAF_Category.Items[j].Selected = true;
                    }
                }
            }


        }
        catch (System.Exception)
        {

        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {

        string selected = string.Empty;
        DataTable dt = new DataTable();
        dt.Columns.Add("ID");
        dt.Columns.Add("Work_Categ_ID");
        dt.Columns.Add("Work_Category_Name");
        try
        {
            int i = 1;
            ViewState["Cat_Table"] = null;
            foreach (ListItem li in chklRAF_Category.Items)
            {
                if (li.Selected == true)
                {
                    dt.Rows.Add(i, li.Value, li.Text);
                    i++;
                }
            }
            dlRAFormsEdit.DataSource = dt;
            dlRAFormsEdit.DataBind();
            ViewState["Cat_Table"] = dt;
            updAttachedRAF.Update();
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string jsSqlError2 = "alert('" + ex.Message + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "jsSqlError2", jsSqlError2, true);
        }
        string AddSalStrmodal = String.Format("hideModal('divRACategory',false);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddSalStrmodal", AddSalStrmodal, true);
    }

    protected void imgbtnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            Bind_RAFormCategory(txtCatSearch.Text.Trim());
            string AddSalStrmodal = String.Format("showModal('divRACategory',false);");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddSalStrmodal", AddSalStrmodal, true);
            udpRACategory.Update();
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string jsSqlError2 = "alert('" + ex.Message + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "jsSqlError2", jsSqlError2, true);
        }

    }
    /// <summary>
    /// Bind Form type dropdown
    /// </summary>
    public void BindFormType()
    {
        DataSet ds = objFMS.FMS_Get_FormType();
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlFormType.DataSource = ds.Tables[0];
            ddlFormType.DataTextField = "FormType";
            ddlFormType.DataValueField = "ID";
            ddlFormType.DataBind();
            ddlFormType.Items.Insert(0, new ListItem("-- Select --", "0"));

            // Added by Kavita : Bind Search Form type 
            ddlFormTypeSearch.DataSource = ds.Tables[0];
            ddlFormTypeSearch.DataTextField = "FormType";
            ddlFormTypeSearch.DataValueField = "ID";
            ddlFormTypeSearch.DataBind();
            ddlFormTypeSearch.Items.Insert(0, new ListItem("-- Select --", "0"));
        }

    }

    /// <summary>
    /// function is use to Bind Department Dropdown.
    /// </summary>
    public void BindDepartment()
    {
        DataTable dt = objCompBLL.Get_CompanyDepartmentList(UDFLib.ConvertToInteger(Session["APPCOMPANYID"].ToString()));
        if (dt.Rows.Count > 0)
        {
            ddlDepartments.DataSource = dt;
            ddlDepartments.DataTextField = "VALUE";
            ddlDepartments.DataValueField = "ID";
            ddlDepartments.DataBind();
            ddlDepartments.Items.Insert(0, new ListItem("-- Select --", "0"));


            ddlDepartmentSearch.DataSource = dt;
            ddlDepartmentSearch.DataTextField = "VALUE";
            ddlDepartmentSearch.DataValueField = "ID";
            ddlDepartmentSearch.DataBind();
            ddlDepartmentSearch.Items.Insert(0, new ListItem("-- Select --", "0"));
        }

    }

    /// <summary>
    /// Bind Status Dropdown.
    /// </summary>
    public void BindStatus()
    {
        DataTable dt = objFMS.FMS_Get_StatusList();
        if (dt.Rows.Count > 0)
        {
            ddlStatusSearch.DataSource = dt;
            ddlStatusSearch.DataTextField = "Status";
            ddlStatusSearch.DataValueField = "ID";
            ddlStatusSearch.DataBind();
            ddlStatusSearch.Items.Insert(0, new ListItem("-ALL-", "0"));
            ddlStatusSearch.Items.Remove(ddlStatusSearch.Items.FindByValue("7"));
            ddlStatusSearch.Items.Remove(ddlStatusSearch.Items.FindByValue("8"));

            ddlStatus.DataSource = dt;
            ddlStatus.DataTextField = "Status";
            ddlStatus.DataValueField = "ID";
            ddlStatus.DataBind();
            ddlStatus.Items.Insert(0, new ListItem("-ALL-", "0"));

        }
    }

    public void LoadDataForEdit(DataRow dr)
    {

        string[] str = dr["LogFileID"].ToString().Split('.');
        string newstring = string.Empty;
        for (int i = 0; i < str.Length - 1; i++)
        {
            if (newstring == "")
            {
                newstring = str[i];
            }
            else
            {
                newstring = newstring + "." + str[i];
            }

        }
        txtFormName.Text = newstring;
        txtExtension.Text = str[str.Length - 1];
        ddlFormType.SelectedValue = dr["FormTypeID"].ToString();
        ddlDepartments.SelectedValue = dr["DeptID"].ToString();
        txtRemarks.Text = dr["Remark"].ToString();

    }

    protected void BtnSaveForm_Click(object sender, EventArgs e)
    {
        try
        {
            int ParentID = 0;
            if (hdnChangedParentID.Value != "")
            {
                ParentID = Convert.ToInt32(hdnChangedParentID.Value.ToString());
            }
            int DocumentID = UDFLib.ConvertToInteger(Request.QueryString["DocID"].ToString());
            string FileName = txtFormName.Text + "." + txtExtension.Text;
            int FormType = UDFLib.ConvertToInteger(ddlFormType.SelectedValue);
            int Department = UDFLib.ConvertToInteger(ddlDepartments.SelectedValue);
            int UserID = GetSessionUserID();
            string Remarks = txtRemarks.Text;
            if (ViewState["Cat_Table"] == null)
            {
                DataTable dtemt = new DataTable();
                dtemt.Columns.Add("ID");
                dtemt.Columns.Add("Work_Categ_ID");
                dtemt.Columns.Add("Work_Category_Name");
                ViewState["Cat_Table"] = dtemt;
            }
            /* check is added for wheather seleted form is present on current foler or location of same form is changed by other user */
            DataTable dtExistsFolder = objFMS.FMS_Get_Exits_Folder(DocumentID, Convert.ToInt32(hdnParentID.Value));

            if (dtExistsFolder.Rows.Count > 0)
            {
                if (dtExistsFolder.Rows[0]["Return"].ToString() == "0")
                {
                    string editFormAlert = "alert('This file no more exist in current folder.This file is moved to - " + dtExistsFolder.Rows[0]["LogFileID"].ToString() + "');parent.ChildCallBackDelete();";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "editFormAlert", editFormAlert, true);

                }

                else
                {
                    DataTable dt = (DataTable)ViewState["Cat_Table"];
                    //objFMS.FMS_Update_Document(DocumentID, FileName, FormType, Department, UserID, Remarks, dt);
                    objFMS.FMS_Update_Document(DocumentID, FileName, FormType, Department, UserID, Remarks, ParentID, dt);
                    string editFormAlert = "alert('File updated successfully.');parent.ChildCallBackDelete();";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "editFormAlert", editFormAlert, true);
                    string editForm = "hideModal('dvEditForm');";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "editForm", editForm, true);
                    lnkFileName1.Text = txtFormName.Text;
                    lnkFileName.Text = txtFormName.Text;
                    lblFormType.Text = ddlFormType.SelectedItem.Text;
                    lblDepartment.Text = ddlDepartments.SelectedItem.Text;
                    lblRemark.Text = txtRemarks.Text;

                    BindRAFormList(DocumentID);
                }
            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string jsSqlError2 = "alert('" + ex.Message + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "jsSqlError2", jsSqlError2, true);
        }
    }

    /// <summary>
    /// Added on DT:22-06-2016
    /// Call a method to unArchive forms.
    /// </summary>
    /// <param name="s"></param>
    /// <param name="e"></param>
    public void btnUnarchive_Click(object s, EventArgs e)
    {
        try
        {
            UnArchiveForms();
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);

        }
    }

    /// <summary>
    /// Added By Kavita : 23-06-2016
    /// From and Till date Date Validation
    /// </summary>
    /// <returns></returns>
    protected bool ValidateAll()
    {
        try
        {
            string js = "";

            if (UDFLib.ConvertDateToNull(txtTillDate.Text) != null)
            {
                if (UDFLib.ConvertDateToNull(txtFromDate.Text) != null)
                {

                    if (UDFLib.ConvertDateToNull(txtTillDate.Text) < UDFLib.ConvertDateToNull(txtFromDate.Text))
                    {
                        js = "alert(' Till date cannot be less than from date.');";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "script1", js, true);
                        return false;
                    }
                }
            }

        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string Error10 = "alert('" + ex.Message.Replace("'", "") + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Error10", Error10, true);
            return false;
        }
        return true;

    }

    /// <summary>
    /// Added on DT:22-06-2016
    /// To unArchive forms.
    /// </summary>
    private void UnArchiveForms()
    {
        try
        {
            objFMS.UnArchiveForms(UDFLib.ConvertToInteger(Request.QueryString["DocID"] == null ? hdnFrmRecDocID.Value : Request.QueryString["DocID"]), GetSessionUserID());
            string jsFile = "alert('Form un-archived successfully.');parent.ChildCallBackDelete();";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", jsFile, true);
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    private bool ValidateMailID()
    {
        try
        {
            if (objFMS.GetMailID() == string.Empty)
            {
                string Error10 = "alert('Email can not be blank.Please contact your administrator.');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Error10", Error10, true);
                return false;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return true;
    }

    /// <summary>
    /// Added on DT:24-06-2016.To save configuration settings of ,attchment of selected form is forward via mail or not.
    /// </summary>
    protected void chkautoforwardMail_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            if (chkautoforwardMail.Checked == true)
            {
                if (ValidateMailID())
                {

                    SaveForwardAttchmentToForms();
                }
                else
                {
                    chkautoforwardMail.Checked = false;
                }
            }
            else
            {
                SaveForwardAttchmentToForms();
            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);

        }
    }

    /// <summary>
    /// Added on DT:24-06-2016.To save configuration settings of  ,attchment of selected form is forward via mail or not.
    /// </summary>
    private void SaveForwardAttchmentToForms()
    {
        int _forwardAttchment = 0;
        try
        {
            if (chkautoforwardMail.Checked)
            {
                _forwardAttchment = 1;

            }

            objFMS.SaveForwardAttchmentToForms(UDFLib.ConvertToInteger(GetDocID()), GetSessionUserID(), _forwardAttchment);
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }




    /// <summary>
    /// Added on DT:07-07-2016
    /// To hide ans show controls conditionally as follows
    /// 1.User is admin , user can add ,edit archive ,un-archive forms.
    /// 2. Ifuser is not admin archive form will not be visible.
    /// 3.If forms is archive only that form un-archive will be visible.
    /// 3.
    /// 
    /// </summary>
    /// <param name="Isformarchived"></param>
    private void Show_Hide_Controls(string Isformarchived)
    {
        try
        {

            btnUnarchive.Visible = false;
            chkautoforwardMail.Visible = false;
            btnDelete.Visible = false;
            btnUnarchive.Visible = false;

            // only admin can do following things: 
            //   1. add /update mail id.
            //   2. Archive and ur-archive forms.
            if (objUA.Admin == 1)
            {
                chkautoforwardMail.Visible = true;
                btnDelete.Visible = true;
                btnUnarchive.Visible = true;

            }
            // If Form is archived then ..
            if (Isformarchived == "0")
            {
                if (objUA.Admin == 1)
                {
                    btnUnarchive.Visible = true;
                }
                //Disable Mail Forward Controls for archived files.
                chkautoforwardMail.Visible = false;
                btnDelete.Visible = false;

                ImgBtnDocSchedule.Visible = false;
                ImgSetApproval.Visible = false;
                ImgSubCheckIn.Visible = false;
                ImgCheckOut.Visible = false;
                ImgCheckIn.Visible = false;
                ImgViewHistory.Visible = false;
                ImgSchAppHistory.Visible = false;
                ImgGetLatest.Visible = false;
                ImgEditForm.Visible = false;
            }
            else // If form is not archived.
            {
                if (objUA.Admin == 1)
                {
                    btnDelete.Visible = true;
                }
                btnUnarchive.Visible = false;   // for active forms un-archive will not be visible.

                ImgBtnDocSchedule.Visible = true;
                ImgSetApproval.Visible = true;
                ImgSubCheckIn.Visible = true;
                ImgCheckOut.Visible = true;
                ImgCheckIn.Visible = true;
                ImgViewHistory.Visible = true;
                ImgSchAppHistory.Visible = true;
                ImgGetLatest.Visible = true;
                ImgEditForm.Visible = true; ;

            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    /// <summary>
    /// Added by Kavita DT:07-07-08-2016. Bind Vessel dropdown in search.
    /// </summary>
    /// <param name="s"></param>
    /// <param name="e"></param>
    public void BindVesselList()
    {
        try
        {
            DataTable dtVesselSearch = null;
            if (chkVesselAssign.Checked == true)
            {

                dtVesselSearch = objVsl.Get_UserVesselList_Search(UDFLib.ConvertToInteger(ddlFleetSearch.SelectedValue), 0, Convert.ToInt32(Session["USERCOMPANYID"].ToString()), "", Convert.ToInt32(Session["USERCOMPANYID"].ToString()), Convert.ToInt32(Session["USERID"].ToString())); /*In This function call only Company ID is pass . Previously "0" was pass which preven from binding default all vessel*/

            }
            else
            {
                dtVesselSearch = objVsl.Get_VesselList(UDFLib.ConvertToInteger(ddlFleetSearch.SelectedValue), 0, Convert.ToInt32(Session["USERCOMPANYID"].ToString()), "", Convert.ToInt32(Session["USERCOMPANYID"].ToString()));

            }

            ddlVesselSearch.DataSource = dtVesselSearch;
            ddlVesselSearch.DataTextField = "Vessel_name";
            ddlVesselSearch.DataValueField = "Vessel_id";
            ddlVesselSearch.DataBind();

            string[] strArr = new string[dtVesselSearch.Rows.Count];
            int i = 0;
            foreach (DataRow r in dtVesselSearch.Rows)
            {
                strArr[i] = r["Vessel_id"].ToString();
                i++;
            }
            ddlVesselSearch.SelectItems(strArr);
            ViewState["dtVesselSearch"] = dtVesselSearch;

        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string Error10 = "alert('" + ex.Message.Replace("'", "") + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Error10", Error10, true);
        }
    }
    protected void btnMainsearch_Click(object sender, EventArgs e)
    {
        try
        {

            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "FPending", " document.getElementById('dvDocDetails').style.display = 'none';", true);
            hdnPeriodRec.Value = lnkPeriod.InnerText;
            btnGetFormsReceived_Click(null, null);
            txtMainSearch.Focus();



        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string Error10 = "alert('" + ex.Message.Replace("'", "") + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Error10", Error10, true);

        }
    }
    protected void btnClearSearch_Click(object sender, EventArgs e)
    {
        try
        {
            txtMainSearch.Text = "";
            chkVesselAssign.Checked = true;
            ddlFleetSearch.SelectedValue = "0";
            ddlStatusSearch.SelectedValue = "0";
            ddlDepartmentSearch.SelectedValue = "0";
            ddlFormTypeSearch.SelectedValue = "0";
            ddlVesselSearch.ClearSelection();
            BindVesselList();
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "FPending", " document.getElementById('dvDocDetails').style.display = 'none';", true);
            hdnPeriodRec.Value = lnkPeriod.InnerText;
            btnGetFormsReceived_Click(null, null);

        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string Error10 = "alert('" + ex.Message.Replace("'", "") + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Error10", Error10, true);

        }
    }
    protected void trvFile_SelectedNodeChanged(object sender, EventArgs e)
    {
        lblSelectedFolderName.Text = trvFile.SelectedNode.Text;
    }

    #region Download Attachments

    /// <summary>
    /// Modified on DT :02-07-2016 .Code moved to new method. To download all type of attchment. 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lnkFileName_Click(object sender, EventArgs e)
    {
        try
        {
            DownloadAllAttchment(hdnFilePath.Value.ToString());
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    //Modified on 23-09-2016 || Existing functionality, moved to new function only. || To download all type of attchment.
    protected void lnkDocID_Click(object sender, EventArgs e)
    {
        try
        {
            DownloadAllAttchment(hdnDocPath.Value);
        }

        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string jsFError = " alert('" + ex + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "jsFError", jsFError, true);
        }

    }

    /// <summary>
    /// Added on DT:02-07-2016.|| Existing functionality moved to new function only. || To download all type of attchment.
    /// </summary>
    private void DownloadAllAttchment(string _filePath)
    {
        try
        {
            if (File.Exists(Server.MapPath(_filePath)) == true)
            {
                string FileName = new FileInfo(_filePath).Name;
                string FileExtension = new FileInfo(_filePath).Extension;
                Response.Clear();
                Response.ClearContent();
                Response.ClearHeaders();
                Response.ContentType = ReturnExtension(FileExtension);
                Response.AppendHeader("content-disposition", "attachment;filename=" + FileName);

                Response.TransmitFile(Server.MapPath(_filePath));
                Response.End();
            }
            else
            {

                string jsNoFile = " alert('File not found.'); ";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "jsNoFileerror", jsNoFile, true);
                LoadFiles(null, null);

            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    /// <summary>
    /// Added on DT:02-07-2016.To force download all attchment.
    /// </summary>
    /// <param name="s"></param>
    /// <param name="e"></param>
    public void btnForce_Download_Click(object s, EventArgs e)
    {
        try
        {
            DownloadAllAttchment(hdnFilePath.Value.ToString());
        }

        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);

        }
    }

    //Added by Anjali DT:23-09-2016|| To download all type of attchment.
    protected void btnForce_Download_Doc_Click(object sender, ImageClickEventArgs e)
    {
        DownloadAllAttchment(hdnDocPath.Value.ToString());
    }

    //Added by Anjali DT:23-09-2016|| To download all type of attchment.
    protected void btnForce_Download_Attachmen_Click(object s, EventArgs e)
    {
        try
        {
            string path = ((ImageButton)s).CommandArgument.ToString();
            path = "../Uploads/FMS/" + path;
            DownloadAllAttchment(path);
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    ///Added by Anjali DT:23-09-2016. 
    ///To Registers a control as a trigger for a postback. This method is used to configure ,for controls inside an UpdatePanel.
    protected void gvAttachment_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        string _filePath = string.Empty;
        try
        {
            ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ImageButton imgForceDownload = (ImageButton)e.Row.FindControl("btnForce_Download_Attachmen");
                if (imgForceDownload != null)
                {
                    _filePath = "../Uploads/FMS/" + imgForceDownload.CommandArgument.ToString();
                    if (IsFileExists(_filePath))
                    {
                        scriptManager.RegisterPostBackControl(imgForceDownload);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }

    }
    /// <summary>
    /// To check file exists or not
    /// </summary>
    /// <param name="_filePath">Path of file</param>
    /// <returns>True:file exists || False: Fle not found</returns>
    private bool IsFileExists(string _filePath)
    {
        try
        {
            if (File.Exists(Server.MapPath(_filePath)) == true)
            {
                return true;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return false;
    }

    /// <summary>
    /// To check file exists or not and send respond to javascript function,to avoid page refresh, if file not found.
    /// </summary>
    /// <param name="filePath">Path of file</param>
    public void FileExists(string filePath)
    {
        try
        {
            if (File.Exists(Server.MapPath(filePath.Replace("..", "~"))))
            {
                Response.Clear();
                Response.Write("1");
                Response.End();
            }
            else
            {
                Response.Clear();
                Response.Write("0");
                Response.End();
            }
        }
        catch (Exception ex)
        {
        }

    }
    #endregion

}