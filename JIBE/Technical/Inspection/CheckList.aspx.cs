using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.Infrastructure;
using System.Data;
//using SMS.Business.Technical;
using SMS.Properties;
using SMS.Business.Inspection;
public partial class Technical_Inspection_CheckList : System.Web.UI.Page
{
    BLL_Infra_VesselLib objBLLVesslType = new BLL_Infra_VesselLib();
    BLL_INSP_Checklist objBLLCheckList = new BLL_INSP_Checklist();

    UserAccess objUA = new UserAccess();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            hdnfldUserID.Value = Convert.ToString(Session["USERID"]);


            UserAccessValidation();

            string id = "";
            if (Request.QueryString["CHKID"] != null)
            {
                id = Request.QueryString["CHKID"];
            }

            if (id != "")
            {
                hdnQuerystring.Value = id;
            }

            string Status = "";
            if (Request.QueryString["Status"] != null)
            {
                Status = Request.QueryString["Status"];
            }

            if (Status != "")
            {
                hdnStatus.Value = Status;
            }

            string ParentID = "";
            if (Request.QueryString["ParentID"] != null)
            {
                ParentID = Request.QueryString["ParentID"];
            }

            if (ParentID != "")
            {
                hdnQuerystringPID.Value = ParentID;
            }

            if (hdnQuerystringPID.Value != "")
            {

                bindGrid(hdnQuerystring.Value);

            }
            else
            {
                grvChecklist.DataSource = null;
                grvChecklist.DataBind();

                btnUpdateShedules.Visible = false;
            }

            FillDDLVesselTYPE();
            FillDDLChecklistTYPE(UDFLib.ConvertIntegerToNull(id));
            FillDDLGrades(UDFLib.ConvertIntegerToNull(id));
            Load_CategoryList();
            Load_Gradings(UDFLib.ConvertIntegerToNull(id));

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

       

    }

    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }

    protected void Load_Gradings(int? CheckList_ID)
    {
        DataTable dt = objBLLCheckList.Get_Grades(null);
        ddlGradingType.DataSource = dt;
        ddlGradingType.DataBind();
        ddlGradingType.Items.Insert(0, new ListItem("-Select-", "0"));
    }

    protected void Load_CategoryList()
    {
        //DataTable dt = objBLLCheckList.Get_Search_CheckListType("");
        DataTable dt = objBLLCheckList.Get_Search_CheckListCategory("");

        ddlCatName.DataSource = dt;
        ddlCatName.DataBind();

    }


    public void FillDDLVesselTYPE()
    {
        try
        {

            DataTable dtVesselType = objBLLVesslType.Get_VesselType();

            ddlvesselType.DataSource = dtVesselType;
            ddlvesselType.DataTextField = "VesselTypes";
            ddlvesselType.DataValueField = "ID";
            ddlvesselType.DataBind();
            ddlvesselType.Items.Insert(0, new ListItem("-SELECT-", "0"));


        }
        catch (Exception ex)
        {
        }
    }

    public void FillDDLChecklistTYPE(int? chkid)
    {
        try
        {


            DataTable dtCheckListType = objBLLCheckList.Get_CheckListType(chkid);


            ddlchecklistType.DataSource = dtCheckListType;
            ddlchecklistType.DataTextField = "Category_Name";
            ddlchecklistType.DataValueField = "ID";
            ddlchecklistType.DataBind();
            ddlchecklistType.Items.Insert(0, new ListItem("-SELECT-", "0"));


        }
        catch (Exception ex)
        {
        }
    }

    public void FillDDLGrades(int? CheckList_ID)
    {
        try
        {


            DataTable dtGrades = objBLLCheckList.Get_Grades(null);


            ddlGrades.DataSource = dtGrades;
            ddlGrades.DataTextField = "Grade_Name";
            ddlGrades.DataValueField = "ID";
            ddlGrades.DataBind();
            ddlGrades.Items.Insert(0, new ListItem("-SELECT-", "0"));


        }
        catch (Exception ex)
        {
        }
    }



    protected void btnPublish_Click(object sender, EventArgs e)
    {
        //string js = "PublishClick();";
        //ScriptManager.RegisterStartupScript(this, this.GetType(), "hideModalJS", js, true);

        dvAddNew.Style.Add("display", "none");


        string chkID = hdnQuerystring.Value;

        if (chkID != "")
        {
            bindGrid(chkID);
            hdnQuerystring.Value = chkID;

            string js = "GetCheckList('" + chkID + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "hideModalJS", js, true);
        }
        else
        {
            chkID = hdnfldChecklstID.Value;
            bindGrid(chkID);
            hdnQuerystring.Value = chkID;

            string js = "GetCheckList('" + chkID + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "hideModalJS", js, true);
        }

    }

    public void bindGrid(string chkID)
    {
        DataTable dt = objBLLCheckList.Get_SheduleCheckList(Convert.ToInt32(chkID));

        if (dt.Rows.Count > 0)
        {
            grvChecklist.DataSource = dt;
            grvChecklist.DataBind();
           
            grvChecklist.Visible = true;
            btnUpdateShedules.Visible = true;
        }
        else
        {
            grvChecklist.DataSource = null;
            grvChecklist.DataBind();
            btnUpdateShedules.Visible = false;
            grvChecklist.Visible = false;
            

        }



    }

    protected void btnUpdateShedules_Click(object sender, EventArgs e)
    {
        DataTable dt = new DataTable("LIB_UDTT_ID_VALUE");
        dt.Columns.Add("ID", typeof(string));
        dt.Columns.Add("VALUE", typeof(string));


        foreach (GridViewRow row in grvChecklist.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                CheckBox chkRow = (row.Cells[0].FindControl("chkRow") as CheckBox);
                if (chkRow.Checked)
                {
                    string SHID = (row.Cells[1].FindControl("lblSCHID") as Label).Text;
                    string CHKID = (row.Cells[1].FindControl("lblCHKID") as Label).Text;
                    //int chID = Convert.ToInt32(CHKID);
                    if (CHKID != "" && SHID != "")
                    {
                        dt.Rows.Add(SHID, CHKID);
                        //objBLLCheckList.Save_ScheduleAndCheckList(chID, Res, GetSessionUserID(), 1);
                    }
                }
                
            }
        }

        string chID = "";
        if (hdnQuerystring.Value == "")
        {
            chID = hdnfldChecklstID.Value;
        }
        else
        {
            chID = hdnQuerystring.Value;
        }

        objBLLCheckList.UpdateS_SheduleAndChecklist(UDFLib.ConvertIntegerToNull(chID), dt, UDFLib.ConvertIntegerToNull(Session["USERID"]));
        //objBLLCheckList.UpdateS_SheduleAndChecklist(UDFLib.ConvertIntegerToNull(hdnQuerystring.Value), dt, UDFLib.ConvertIntegerToNull(Session["USERID"]));

        string chkID = hdnQuerystring.Value;
        bindGrid(chkID);

        string js = "GetCheckList('" + chkID + "');";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "hideModalJS", js, true);

        dvAddNew.Style.Add("display", "none");
    }
    protected void btnGetSchedule_Click(object sender, EventArgs e)
    {
        string chID = "";
        if (hdnQuerystring.Value == "")
        {
            chID = hdnfldChecklstID.Value;
        }
        else
        {
            chID = hdnQuerystring.Value;
        }

        bindGrid(chID);
    }
}