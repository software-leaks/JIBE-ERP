using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.JRA;
using SMS.Business.Infrastructure;
using System.Data;
using System.Drawing;
using SMS.Business.Crew;
using SMS.Properties;
public partial class JRA_Libraries_RiskAssessmentUserLevelConfig : System.Web.UI.Page
{

    BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
    protected void Page_Load(object sender, EventArgs e)
    {



        if (hfWC.Value != null && hfWC.Value.ToString() != "")
        {

        }
        if (hfType.Value != null && hfType.Value.ToString() != "")
        {

        }

        if (!this.IsPostBack)
        {
            LoadCombos();
            if (Request.QueryString["Work_Categ_ID"] != null)
            {
                hfWC.Value = Request.QueryString["Work_Category_Name"];
            }
            if (Request.QueryString["Work_Category_Name"] != null)
            {
                ViewState["Work_Category_Name"] = Request.QueryString["Work_Category_Name"];
            }

        }

        if (hfWC.Value != null && hfWC.Value.ToString() != "" && hfWC.Value.ToString() != "0")
        {
            if (hfSaveRank.Value.ToString() != "1")
            {
                GridViewHelper helper = new GridViewHelper(grdLevel);
                helper.RegisterGroup("Approval_Level", true, true);
                helper.GroupHeader += new GroupEvent(helper_GroupHeader);
                BindApprovar(UDFLib.ConvertToInteger(hfWC.Value));
            }
            hfSaveRank.Value = "";

        }
        else
        {
            pnlVessel.Visible = false;
            pnlOffice.Visible = true;
            rdblstAppType.SelectedIndex = 0;
            grdUser.DataSource = null;
            grdUser.DataBind();
        }



    }
    public void LoadCombos()
    {
        JRA_Lib lObjWC = new JRA_Lib();
        lObjWC.Work_Categ_Parent_ID = null;
        lObjWC.Mode = 3;
        DataTable dtW = BLL_JRA_Work_Category.JRA_GET_WORK_CATEGORY_LIST(lObjWC);
        ddlParentWorkCateg.DataSource = dtW;
        ddlParentWorkCateg.DataTextField = "Work_Category_Display";
        ddlParentWorkCateg.DataValueField = "Work_Categ_ID_TYPE";
        ddlParentWorkCateg.DataBind();
        ddlParentWorkCateg.Items.Insert(0, new ListItem("-SELECT ALL-", "0"));
        ddlCTWorkCategory.DataSource = dtW;
        ddlCTWorkCategory.DataTextField = "Work_Category_Display";
        ddlCTWorkCategory.DataValueField = "Work_Categ_ID";
        ddlCTWorkCategory.DataBind();
        ddlParentWorkCateg.SelectedIndex = 0;
    }
    public void BindApprovar(int Work_Categ_ID)
    {
        DataSet ds = new DataSet();
        ds = BLL_JRA_Hazards.Get_Approvar(Work_Categ_ID);

        lblWC.Text = hfIDName.Value;

        //if (ds.Tables[0].Rows.Count>0)
        //    lblWC.Text = ds.Tables[0].Rows[0]["Work_Categ_ID_Name"].ToString()+" :- ";


        if (ds.Tables[1].Rows.Count > 0)
        {
            ViewState["TotalApprovalLevel"] = ds.Tables[1].Rows[0]["LevelCount"].ToString();
        }
        else
        {
            ViewState["TotalApprovalLevel"] = 1;
        }



        if (hfType.Value == "0")
        {
            BLL_Crew_Admin objCrewAdmin = new BLL_Crew_Admin();
            DataTable dt = objCrewAdmin.Get_RankList();
            gvRankList.DataSource = dt;
            gvRankList.DataBind();
            pnlOffice.Visible = false;
            pnlVessel.Visible = true;
            rdblstAppType.SelectedIndex = 1;


        }
        else
        {
            grdLevel.DataSource = null;
            grdLevel.DataBind();
            grdLevel.DataSource = ds.Tables[0];
            grdLevel.DataBind();
            pnlOffice.Visible = true;
            pnlVessel.Visible = false;
            rdblstAppType.SelectedIndex = 0;

        }
        //if (ds.Tables[2].Rows[0]["Type"].ToString() == "1" && (hfType.Value == "0" ))
        //{

        //    BLL_Crew_Admin objCrewAdmin = new BLL_Crew_Admin();
        //    DataTable dt = objCrewAdmin.Get_RankList();
        //    gvRankList.DataSource = dt;
        //    gvRankList.DataBind();
        //    pnlOffice.Visible = true;
        //    pnlVessel.Visible = false;
        //}
        //else
        //{
        //    grdLevel.DataSource = null;
        //    grdLevel.DataBind();
        //    grdLevel.DataSource = ds.Tables[0];
        //    grdLevel.DataBind();
        //    pnlOffice.Visible = true;
        //    pnlVessel.Visible = false;
        //}





        //if (ds.Tables[2].Rows[0]["Type"].ToString() == "0")
        //{
        //    lblWorkCateg.Text = ds.Tables[2].Rows[0]["Work_Category_Name"].ToString();
        //    try
        //    {
        //        ddlRank.SelectedValue = ds.Tables[3].Rows[0]["Approvar_Detail_ID"].ToString();
        //    }
        //    catch (Exception)
        //    {

        //        ddlRank.SelectedIndex = 0;
        //    }
        //    pnlOffice.Visible = false;
        //    rdblstAppType.SelectedValue = "0";
        //}
        //else 
        //{

        //    //GridViewHelper helper = new GridViewHelper(grdLevel);
        //    //helper.RegisterGroup("Approval_Level", true, true);
        //    //helper.GroupHeader += new GroupEvent(helper_GroupHeader);
        //    grdLevel.DataSource = null;
        //    grdLevel.DataBind();
        //    grdLevel.DataSource = ds.Tables[0];
        //    grdLevel.DataBind();
        //    pnlOffice.Visible = true;
        //    pnlVessel.Visible = false;
        //    rdblstAppType.SelectedValue = "1";
        //}


    }
    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }


    public void BindUser()
    {
        DataTable ds = objUser.Get_UserList();
        ds.DefaultView.RowFilter = "User_Type='OFFICE USER' and Active_Status=1";
        DataTable dt = ds.DefaultView.ToTable();
        grdUser.DataSource = dt;
        grdUser.DataBind();
    }
    private void helper_GroupHeader(string groupName, object[] values, GridViewRow row)
    {
        if (groupName == "Approval_Level")
        {
            row.BackColor = System.Drawing.Color.LightGray;
            row.Cells[0].Attributes["colspan"] = "3";
            //row.Cells[0].Text = "&nbsp;&nbsp;" + row.Cells[0].Text;
            LinkButton lnk = new LinkButton();
            lnk.CssClass = "LinkButton";
            lnk.ForeColor = Color.Black;
            lnk.Text = row.Cells[0].Text;
            lnk.Font.Underline = true;
            lnk.Click += new EventHandler(lnk_click);
            //lnk.OnClientClick = "return AddApprovar();";
            row.Cells[0].Controls.Add(lnk);
            row.Cells[0].Font.Bold = true;

            //  row.Cells[0].CssClass = "Summary-SubHeaderStyle-css";


        }



    }

    protected void lnk_click(object sender, EventArgs e)
    {
        txtSearch.Text = "";
        int Work_Categ_ID = UDFLib.ConvertToInteger(hfWC.Value.ToString());

        BindApprovar(Work_Categ_ID);
        string Approval_Level = ((LinkButton)sender).Text;
        ViewState["ApprovalLevel"] = Approval_Level.Split('-')[1].ToString();
        string js1 = "showModal('dvAppLevelUser');";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "jsApprrovar", js1, true);
        BindUser();



    }

    protected bool SelectCheckbox(string UserID)
    {
        int Work_Categ_ID = UDFLib.ConvertToInteger(hfWC.Value.ToString());
        int ApprovalLevel = UDFLib.ConvertToInteger(ViewState["ApprovalLevel"].ToString());
        DataSet ds = BLL_JRA_Hazards.Get_ApprovarByLevel(Work_Categ_ID, ApprovalLevel);

        if (ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Select("Approvar_Detail_ID='" + UserID + "'").Length > 0)
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

    protected bool SelectRank(string Rank)
    {
        int Work_Categ_ID = UDFLib.ConvertToInteger(hfWC.Value.ToString());

        DataSet ds = BLL_JRA_Hazards.Get_ApprovarByLevel(Work_Categ_ID, 0);

        if (ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Select("Approvar_Detail_ID='" + Rank + "'").Length > 0)
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
        int Work_Categ_ID = UDFLib.ConvertToInteger(hfWC.Value.ToString());
        int ApprovalLevel = UDFLib.ConvertToInteger(ViewState["ApprovalLevel"].ToString());


        //DataSet ds = objFMS.FMS_Get_ApprovarByLevel(FileID, ApprovalLevel);
        DataSet ds = BLL_JRA_Hazards.Get_ApprovarByLevel(Work_Categ_ID, ApprovalLevel);
        if (ds.Tables.Count > 0)
        {
            if (ds.Tables[1].Select("Approvar_Detail_ID='" + UserID + "'").Length > 0)
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

    protected bool EnableCheckboxRank(string RankID)
    {
        int Work_Categ_ID = UDFLib.ConvertToInteger(hfWC.Value.ToString());



        //DataSet ds = objFMS.FMS_Get_ApprovarByLevel(FileID, ApprovalLevel);
        DataSet ds = BLL_JRA_Hazards.Get_ApprovarByLevel(Work_Categ_ID, 0);
        if (ds.Tables.Count > 0)
        {
            if (ds.Tables[1].Select("Approvar_Detail_ID='" + RankID + "'").Length > 0)
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
    protected void btnSearch_Click(object sender, EventArgs e)
    {


        DataTable ds = objUser.Get_UserList();
        ds.DefaultView.RowFilter = "User_Type='OFFICE USER' and Active_Status=1 and (USERNAME like '%" + txtSearch.Text + "%' or USERNAME like '%" + txtSearch.Text + "%') ";
        DataTable dt = ds.DefaultView.ToTable();
        grdUser.DataSource = dt;
        grdUser.DataBind();
        string js1 = "showModal('dvAppLevelUser');";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "jsApprrovar", js1, true);
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        int Work_Categ_ID = UDFLib.ConvertToInteger(hfWC.Value.ToString());
        int ApprovalLevel = UDFLib.ConvertToInteger(ViewState["ApprovalLevel"].ToString());
        DataSet ds = BLL_JRA_Hazards.Get_ApprovarByLevel(Work_Categ_ID, ApprovalLevel);
        int CreatedBy = GetSessionUserID();
        BLL_JRA_Hazards.UPD_Approvar(Work_Categ_ID, ApprovalLevel);
        for (int i = 0; i < grdUser.Rows.Count; i++)
        {
            if (((CheckBox)grdUser.Rows[i].Cells[2].FindControl("chkUser")).Checked == true)
            {
                DataTable table = new DataTable();
                table.Columns.Add("PID");



                BLL_JRA_Hazards.Insert_Approvar(Work_Categ_ID, ApprovalLevel, UDFLib.ConvertToInteger(grdUser.Rows[i].Cells[0].Text), CreatedBy, 1, table);
                //objFMS.FMS_Insert_FileApprovar(FileID, ApprovalLevel, UDFLib.ConvertToInteger(grdUser.Rows[i].Cells[0].Text), CreatedBy);
            }
        }
        if (!string.IsNullOrEmpty(txtSearch.Text))
        {
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {

                DataTable table = new DataTable();
                table.Columns.Add("PID");

                BLL_JRA_Hazards.Insert_Approvar(Work_Categ_ID, ApprovalLevel, UDFLib.ConvertToInteger(ds.Tables[0].Rows[i]["Approvar_Detail_ID"]), CreatedBy, 1, table);

            }
        }

            LoadCombos();
        string js2 = "SetWorkCategID(" + Work_Categ_ID.ToString() + ";" + "1);";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "jsMsg", js2, true);
        BindApprovar(Work_Categ_ID);
        string js0 = "alert('Approver Added Successfully and Approval has been configured to office user');";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "jsMsg", js0, true);

        string js1 = "hideModal('dvAppLevelUser');";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "jsApprrovar", js1, true);


        // BindApprovar(Work_Categ_ID);

    }
    protected void btnAddLevel_Click(object sender, EventArgs e)
    {
        if (hfWC.Value.ToString() == "")
        {
            string js0 = "alert('Select work category!');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "jsMsg", js0, true);
            return;
        }
        int Work_Categ_ID = UDFLib.ConvertToInteger(hfWC.Value.ToString());
        if (UDFLib.ConvertToInteger(ViewState["TotalApprovalLevel"].ToString()) == 5)
        {
            lblError.Visible = true;
            lblError.Text = "Can not Add More Approval Level. Max Approval Level Set!!";

        }
        else
        {

            int CreatedBy = GetSessionUserID();
            BLL_JRA_Hazards.Insert_ApprovalLevels(Work_Categ_ID, CreatedBy);

        }

        BindApprovar(Work_Categ_ID);



    }
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        //int FileID = UDFLib.ConvertToInteger(ViewState["FileID"].ToString());
        //BindApprovar(FileID);
    }
    protected void rdbOffice_CheckedChanged(object sender, EventArgs e)
    {

    }

    protected void btnSaveRank_Click(object sender, EventArgs e)
    {
        int Work_Categ_ID = UDFLib.ConvertToInteger(hfWC.Value.ToString());
        int k = 0;
        DataTable table = new DataTable();
        table.Columns.Add("PID");
        for (int i = 0; i < gvRankList.Rows.Count; i++)
        {
            if (((CheckBox)gvRankList.Rows[i].Cells[2].FindControl("chkRank")).Checked == true)
            {
                table.Rows.Add(UDFLib.ConvertToInteger(gvRankList.Rows[i].Cells[0].Text));
                k++;
            }
        }
        if (k == 0)
        {
            lblError.Visible = true;
            lblError.Text = "Rank not selected!";
        }
        else
        {

            BLL_JRA_Hazards.Insert_Approvar(Work_Categ_ID, 0, 0, GetSessionUserID(), 0, table);
            lblError.Visible = false;
            BLL_Crew_Admin objCrewAdmin = new BLL_Crew_Admin();
            LoadCombos();
            string js1 = "SetWorkCategID(" + Work_Categ_ID.ToString() + ";" + "0);";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "jsMsg", js1, true);
            BindApprovar(UDFLib.ConvertToInteger(hfWC.Value));
            string js0 = "alert('Approver rank added Successfully and Approval has been configured to Vessel Rank');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "jsMsg", js0, true);
        }
    }
    protected void Check_Clicked(Object sender, EventArgs e)
    {



    }
    protected void rdblstAppType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (hfType.Value != null && hfType.Value.ToString() != "")
        {
            if (hfType.Value.ToString() == "1")
            {
                pnlOffice.Visible = true;
                pnlVessel.Visible = false;
                rdblstAppType.SelectedIndex = 0;

            }
            else
            {
                pnlOffice.Visible = false;
                pnlVessel.Visible = true;
                rdblstAppType.SelectedIndex = 1;
                BLL_Crew_Admin objCrewAdmin = new BLL_Crew_Admin();
                DataTable dt = objCrewAdmin.Get_RankList();
                gvRankList.DataSource = dt;
                gvRankList.DataBind();
            }
        }
        //if (rdblstAppType.SelectedValue == "1")
        //{
        //    pnlOffice.Visible = true;
        //    pnlVessel.Visible = false;
        //    ViewState["Mode"] = "1";
        //}
        //else
        //{
        //    pnlOffice.Visible = false;
        //    pnlVessel.Visible = true;
        //    ViewState["Mode"] = "0";
        //}



        //BindApprovar(  Work_Categ_ID,"RD");
    }

    protected void btnCopyTo_Click(object sender, EventArgs e)
    {
        if (ddlParentWorkCateg.SelectedIndex <= 0)
            return;
        DataTable TO_Work_Categ_ID = new DataTable();
        TO_Work_Categ_ID.Columns.Add("PID");
        foreach (DataRow dr in ddlCTWorkCategory.SelectedValues.Rows)
        {
            if (hfWC.Value.ToString() != dr[0].ToString())
                TO_Work_Categ_ID.Rows.Add(dr[0].ToString());
        }
        if (TO_Work_Categ_ID.Rows.Count == 0)
            return;

        string msg = BLL_JRA_Hazards.COPY_APPROVAL(UDFLib.ConvertToInteger(ddlParentWorkCateg.SelectedValue.Split(';')[0]), TO_Work_Categ_ID, GetSessionUserID()).Tables[0].Rows[0][0].ToString();

        string js0 = "alert('" + msg + "');";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "jsMsg", js0, true);
        LoadCombos();
    }
}