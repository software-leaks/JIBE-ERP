using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.FMS;
using SMS.Business.Infrastructure;
using System.Data;
using System.Drawing;
using SMS.Properties;
public partial class FMS_FMSSchFileApprovarConfig : System.Web.UI.Page
{
    BLL_FMS_Document objFMS = new BLL_FMS_Document();
    UserAccess objUserAcess = new UserAccess();
    BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
    DataTable dtTemp = new DataTable();
   
    protected void Page_Load(object sender, EventArgs e)
    {
        if (GetSessionUserID() == 0)
            Response.Redirect("~/account/login.aspx");
        else
            UserAccessValidation();
        if (!IsPostBack)
        {
            dtTemp.Columns.Add("UserID");
            dtTemp.Columns.Add("UserName");
            dtTemp.Columns.Add("Status");
            ViewState["APList"] = dtTemp;
            int FileID = UDFLib.ConvertToInteger(Request.QueryString["FileID"].ToString());
            BindApprovalLevel(FileID);
            BindApprovar(FileID, 1);
            HighlightSelectedRow(0);
        }


    }

    /// <summary>
    /// Getting Login User Access Details
    /// </summary>
    protected void UserAccessValidation()
    {

        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());
        objUserAcess = objUser.Get_UserAccessForPage(Convert.ToInt32(Session["USERID"]), PageURL);

    }

    protected void onLevelClick(object source, CommandEventArgs e)
    {

        try
        {
            int Level = UDFLib.ConvertToInteger(e.CommandArgument.ToString().Split(',')[0]);
            int RowIndex = UDFLib.ConvertToInteger(e.CommandArgument.ToString().Split(',')[1]);
            int FileID = UDFLib.ConvertToInteger(Request.QueryString["FileID"].ToString());
            HighlightSelectedRow(RowIndex);
            ViewState["RowIndex"] = RowIndex;
            BindApprovar(FileID, Level);
          

        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string jsSqlError2 = "alert('Error while processing.Please contact admin');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "jsSqlError2", jsSqlError2, true);
        }
    }

    protected void HighlightSelectedRow(int RowIndex)
    {
        for (int i = 0; i < grdLevel.Rows.Count; i++)
        {
            GridViewRow gvr = grdLevel.Rows[i];
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
    /// <summary>
    /// Event is used to delete a particular level
    /// </summary>
     protected void onDeleteLevelClick(object source, CommandEventArgs e)
    {
        try
        {
            int Approvalcnt = 0;
            int Level = UDFLib.ConvertToInteger(e.CommandArgument.ToString());
            int FileID = UDFLib.ConvertToInteger(Request.QueryString["FileID"].ToString());
            DataSet  dsApprvercount = objFMS.FMS_Get_FileApprovar(FileID, Level);
            if (dsApprvercount.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < dsApprvercount.Tables[0].Rows.Count; i++)
                {
                    if (dsApprvercount.Tables[0].Rows[i]["UserID"].ToString() != "")
                    {
                        Approvalcnt++;
                    }
                }
            }
            //int ApprovalCount = objFMS.FMS_Get_ApproverInLevel(FileID, Level); // Commented DT 05-08-2016 ApprovalCount variable Not used anywhere.
            objFMS.FMS_Delete_ApprovalLevel(FileID, UDFLib.ConvertToInteger(Session["USERID"].ToString()), Level);
            BindApprovalLevel(FileID);
            BindApprovar(FileID, 1);
            if (Level != 1 && Approvalcnt > 0)
                {
                    string jsAlert = "alert('All approvers in this level will be deleted.');";
                     ScriptManager.RegisterStartupScript(this, this.GetType(), "msg1", jsAlert, true);
                }

        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string jsSqlError2 = "alert('Error while processing.Please contact admin');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "jsSqlError2", jsSqlError2, true);    

        }
    }
   /// <summary>
   /// Bind Approval Levels
   /// </summary>
   /// <param name="FileID">Selected File ID.</param> 
    public void BindApprovalLevel(int FileID)
    {
        DataSet ds = new DataSet();
        ds = objFMS.FMS_Get_FileApprovalLevel(FileID);

        if (ds.Tables[0].Rows.Count > 0)
        {
            ViewState["TotalApprovalLevel"] = ds.Tables[0].Rows.Count;

        }
        else
        {
            ViewState["TotalApprovalLevel"] = 1;
        }


        grdLevel.DataSource = ds.Tables[0];
        grdLevel.DataBind();


        if (objUserAcess.Delete == 1)
        {
            for (int i = 0; i < grdLevel.Rows.Count; i++)
            {
                ImageButton img = (ImageButton)grdLevel.Rows[i].Cells[1].FindControl("ImgDelete");
                if (i == grdLevel.Rows.Count - 1)
                {
                    img.Visible = true;
                }
                else
                {
                    img.Visible = false;
                }
            }
        }

    }
    /// <summary>
    /// Bind approvar list according to level
    /// </summary>
    /// <param name="FileID">Selected File ID.</param>
    /// <param name="Level">Selected Level.</param>
    public void BindApprovar(int FileID, int Level)
    {
        try
        {
            DataSet ds = new DataSet();
            ds = objFMS.FMS_Get_FileApprovar(FileID, Level);           
                grdLevelUser.DataSource = ds.Tables[0];
                grdLevelUser.DataBind();
               if (ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["ID"].ToString() == "")
                    {
                        System.Web.UI.WebControls.Image imgApprover = (System.Web.UI.WebControls.Image)grdLevelUser.Rows[0].Cells[1].FindControl("imgApproverRecordInfo");
                        imgApprover.Visible = false;

                    }
                }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string jsSqlError2 = "alert('Error while processing.Please contact admin');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "jsSqlError2", jsSqlError2, true);
        }

    }
    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }

    /// <summary>
    /// Bind User Grid 
    /// </summary>
    public void BindUser()
    {
        DataSet ds = new DataSet();
        ds = objFMS.FMS_Get_UserList();

        grdUser.DataSource = ds.Tables[0];
        grdUser.DataBind();

        dtTemp = (DataTable)ViewState["APList"];
        dtTemp.Rows.Clear();
        for (int i = 0; i < grdUser.Rows.Count; i++)
        {
            string UserID = grdUser.DataKeys[i].Value.ToString(); 
            if (((CheckBox)grdUser.Rows[i].Cells[2].FindControl("chkUser")).Checked == true)
            {
                dtTemp.Rows.Add(UserID, grdUser.Rows[i].Cells[1].Text, true);
            }
            else
            {
                dtTemp.Rows.Add(UserID, grdUser.Rows[i].Cells[1].Text, false);
            }
        }

        ViewState["APList"] = dtTemp;
    }
    protected void onchkUser_CheckChanged(object sender, EventArgs e)
    {
        dtTemp = (DataTable)ViewState["APList"];
        for (int i = 0; i < grdUser.Rows.Count; i++)
        {
            if (((CheckBox)grdUser.Rows[i].Cells[2].FindControl("chkUser")).Checked == true)
            {
                dtTemp.Rows[i]["Status"] = true;
            }
            else
            {
                dtTemp.Rows[i]["Status"] = false;
            }
        }
        ViewState["APList"] = dtTemp;
    }

    protected void onchkUser_Check_Changed(object sender, EventArgs e)
    {
        dtTemp = (DataTable)ViewState["APList"];
        for (int i = 0; i < grdUser.Rows.Count; i++)
        {
            if (((CheckBox)grdUser.Rows[i].Cells[2].FindControl("chkUser")).Checked == true)
            {
                dtTemp.Rows[i]["Status"] = true;
            }
            else
            {
                dtTemp.Rows[i]["Status"] = false;
            }
        }
        ViewState["APList"] = dtTemp;
    }

    protected void lnklevel_click(object sender, EventArgs e)
    {
        int FileID = UDFLib.ConvertToInteger(Request.QueryString["FileID"].ToString());
        LinkButton btn = (LinkButton)(sender);
        int Level = UDFLib.ConvertToInteger(btn.CommandArgument.ToString().Split(',')[0]);
        int RowIndex = UDFLib.ConvertToInteger(btn.CommandArgument.ToString().Split(',')[1]);
        BindApprovalLevel(FileID);
        string Approval_Level = ((LinkButton)sender).Text;
        BindApprovar(FileID, UDFLib.ConvertToInteger(Approval_Level.Split('-')[1].ToString()));
        ViewState["ApprovalLevel"] = Approval_Level.Split('-')[1].ToString();
        string js1 = "$('#dvAppLevelUser').prop('title', 'Approver List');$('#txtSearch').val(''); showModal('dvAppLevelUser');    ";/* Code to Set Title, Clear Search Textbox and open Popup of Approver List  */
        ScriptManager.RegisterStartupScript(this, this.GetType(), "jsApprrovar", js1, true);
        BindUser();
        HighlightSelectedRow(RowIndex);
        BindApprovar(FileID, Level);
      }

    protected bool SelectCheckbox(string UserID)
    {
        int FileID = UDFLib.ConvertToInteger(Request.QueryString["FileID"].ToString());
        int ApprovalLevel = UDFLib.ConvertToInteger(ViewState["ApprovalLevel"].ToString());
        DataSet ds = objFMS.FMS_Get_ApprovarByLevel(FileID, ApprovalLevel);
        if (ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Select("User_ID='" + UserID + "'").Length > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {

            return false;

        }
        
    }
    protected bool EnableCheckbox(string UserID)
    {
        int FileID = UDFLib.ConvertToInteger(Request.QueryString["FileID"].ToString());
        int ApprovalLevel = UDFLib.ConvertToInteger(ViewState["ApprovalLevel"].ToString());
        DataSet ds = objFMS.FMS_Get_ApprovarByLevel(FileID, ApprovalLevel);
        if (ds.Tables.Count > 0)
        {
            if (ds.Tables[1].Select("User_ID='" + UserID + "'").Length > 0)
            {

                return false;
            }
            else
            {
                return true;
            }
        }
        else
        {

            return true;

        }
    }
    /// <summary>
    /// Search Users by Name
    /// </summary>
       protected void btnSearch_Click(object sender, EventArgs e)
    {
        dtTemp = (DataTable)ViewState["APList"];
        DataTable dtTemp1 = new DataTable();
        if (dtTemp.Select("UserID LIKE '%" + txtSearch.Text + "%' or UserName LIKE '%" + txtSearch.Text + "%'  ").Length > 0)
        {
            dtTemp1 = dtTemp.Select("UserID LIKE '%" + txtSearch.Text + "%' or UserName LIKE '%" + txtSearch.Text + "%'  ").CopyToDataTable();

            DataView dv = dtTemp1.DefaultView;
            dv.Sort = "UserName ASC";
            DataTable sortedDT = dv.ToTable();

            grdUser.DataSource = sortedDT;
            grdUser.DataBind();

            for (int i = 0; i < grdUser.Rows.Count; i++)
            {
                string UserID = grdUser.DataKeys[i].Value.ToString(); 
                for (int j = 0; j < sortedDT.Rows.Count; j++)
                {
                    if (UserID == sortedDT.Rows[j]["UserID"].ToString())
                    {
                        if (sortedDT.Rows[j]["Status"].ToString() == "True")
                        {
                            CheckBox chk = (CheckBox)grdUser.Rows[i].Cells[2].FindControl("chkUser");
                            chk.Checked = true;
                        }

                    }
                }
            }
        }
        else
        {
            grdUser.DataSource = null;
            grdUser.DataBind();
        }

    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        Save(true);
    }

    /// <summary>
    /// This function saves the approver check in user list under selected level Created By Someshwar Mandre
    /// </summary>
    /// <param name="Flag">To confirm if data to be saved or not</param>
    protected void Save(Boolean Flag)
    {
        try
        {

            if (Flag == true)
            {
                int FirstitmeCounter = 0;//hdnCheckboxValues.value;

                for (int i = 0; i < grdUser.Rows.Count; i++)
                {   
                    CheckBox chk = (CheckBox)grdUser.Rows[i].Cells[2].FindControl("chkUser");
                    HiddenField hdn = (HiddenField)grdUser.Rows[i].Cells[2].FindControl("hdnCheckboxValues");

                    int checkCurrVal = 0;
                    if (chk.Checked == true)
                    {
                        checkCurrVal = 1;
                    }

                    if (checkCurrVal != Convert.ToInt32(hdn.Value.ToString()))
                    {
                        FirstitmeCounter++;
                        break;
                    }
                   
                }

                bool flaguser = true;
                Random r = new Random();
                 dtTemp = (DataTable)ViewState["APList"];           
                int FileID = UDFLib.ConvertToInteger(Request.QueryString["FileID"].ToString());
                int TotalApprovalLevel = UDFLib.ConvertToInteger(objFMS.FMS_Get_FileApprovalLevel(FileID));
                int ApprovalLevel = UDFLib.ConvertToInteger(ViewState["ApprovalLevel"].ToString());
                int CreatedBy = GetSessionUserID();
                int userCount = 0;
                for (int i = 0; i < grdUser.Rows.Count; i++) /* Loop to update checked and unchecked status in data table from Approver List Grid */
                {
                    string UserID = grdUser.DataKeys[i].Value.ToString();
                    for (int j = 0;  j < dtTemp.Rows.Count;  j++)
                    {
                        if (UserID == dtTemp.Rows[j]["UserID"].ToString())
                        {
                            dtTemp.Rows[j]["Status"] = ((CheckBox)grdUser.Rows[i].Cells[2].FindControl("chkUser")).Checked;
                           
                        }
                    }
                }
                //ViewState["APList"] = dtTemp;
                for (int i = 0; i < dtTemp.Rows.Count; i++)
                {
                    if (dtTemp.Rows[i]["Status"].ToString() == "True")
                    {

                        userCount++;
                    }
                 
                }

                if (userCount > 0)
                {
                    for (int i = 0; i < grdUser.Rows.Count; i++)
                    {
                        string UserID = grdUser.DataKeys[i].Value.ToString();
                        if (((CheckBox)grdUser.Rows[i].Cells[2].FindControl("chkUser")).Checked == true)
                        {
                            objFMS.FMS_Insert_FileApprovar(FileID, ApprovalLevel, UDFLib.ConvertToInteger(UserID), CreatedBy);
                            // userCount++;
                        }
                        else
                        {                          
                            /* Update approvers in level */
                            objFMS.FMS_Update_FileApprovarById(FileID, ApprovalLevel, UDFLib.ConvertToInteger(UserID), CreatedBy);
                         
                        }
                    }
                }
             
                else
                {
                    if (FirstitmeCounter > 0)
                    {
                        string jsErr = "alert('This level has only one approver, please add new approver before deleting this approver');";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "jsErr" + r.ToString(), jsErr, true);
                        flaguser = false;
                    }
                    else
                    {
                        string jsErr = "alert('Please select at least one approver');";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "jsErr" + r.ToString(), jsErr, true);
                        flaguser = false;
                    }
                }
                
                BindApprovar(FileID, ApprovalLevel);
                if (userCount > 0)
                {
                    BindApprovalLevel(FileID);
                    if (flaguser)
                    {
                        string js0 = "alert('Approver List Updated Successfully');";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "jsMsg", js0, true);

                        string js1 = "hideModal('dvAppLevelUser');";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "jsApprrovar", js1, true);
                        lblError.Text = "";
                        lblError.Visible = false;

                    }
                }
                HighlightSelectedRow(ApprovalLevel - 1);
            }
            else
            {
                DataTable dt = (DataTable)ViewState["APList"];

            }
        
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string jsSqlError2 = "alert('Error while processing.Please contact admin');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "jsSqlError2", jsSqlError2, true);
        }
    }

    protected void btnAddLevel_Click(object sender, EventArgs e)
    {
        lblError.Visible = false;
        lblError.Text = "";
        int FileID = UDFLib.ConvertToInteger(Request.QueryString["FileID"].ToString());
        if (UDFLib.ConvertToInteger(ViewState["TotalApprovalLevel"].ToString()) == 5)
        {
            lblError.Visible = true;
            lblError.Text = "Cannot Add More Approval Level. Max Approval Level Set!!";

        }
        else
        {
            int maxLevelCount = grdLevel.Rows.Count;
            int count = UDFLib.ConvertToInteger(objFMS.FMS_Get_ApprovarByLevel(FileID, maxLevelCount).Tables[0].Rows.Count);
            if (maxLevelCount >= 1)
            {
                if (count > 0)
                {
                    int CreatedBy = GetSessionUserID();
                    objFMS.FMS_Insert_ApprovalLevels(FileID, CreatedBy);
                }
                else
                {
                    lblError.Visible = true;
                    lblError.Text = "Cannot Add New Level. <br>Last level does not contain any user<br> Add User to Last level then add new level  !";
                }
            }
            else
            {
                int CreatedBy = GetSessionUserID();
                objFMS.FMS_Insert_ApprovalLevels(FileID, CreatedBy);
            }
        }

        BindApprovalLevel(FileID);


    }
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        int FileID = UDFLib.ConvertToInteger(Request.QueryString["FileID"].ToString());
        BindApprovalLevel(FileID);
        HighlightSelectedRow(0);
        BindApprovar(FileID, 1);
        lblError.Text = "";
    }
    protected void grdLevel_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            if (objUserAcess.Delete == 0)
            {
                ImageButton img = (ImageButton)e.Row.Cells[1].FindControl("ImgDelete");
                img.Visible = false;
            }

        }
    }
}