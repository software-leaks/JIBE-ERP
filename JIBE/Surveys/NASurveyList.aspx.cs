using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SMS.Business.Survey;

public partial class Surveys_MakeAsNA : System.Web.UI.Page
{
    BLL_SURV_Survey objBLL = new BLL_SURV_Survey();

    //private static SortDirection GridViewSortDirection;

    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Request.QueryString["page"] != null)
            this.Page.MasterPageFile = "~/Surveys/SurveyMaster.master";
        else
            this.Page.MasterPageFile = "~/Site.master";
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        UserAccessValidation();
        if (MainDiv.Visible)
        {
            if (!IsPostBack)
            {
                SortGridView();
            }
        }
    }

    protected void UserAccessValidation()
    {
        int CurrentUserID = GetSessionUserID();
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

        SMS.Properties.UserAccess objUA = new SMS.Properties.UserAccess();
        SMS.Business.Infrastructure.BLL_Infra_UserCredentials objUser = new SMS.Business.Infrastructure.BLL_Infra_UserCredentials();
        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);


        if (objUA.View == 0)
        {
            MainDiv.Visible = false;
            AccessMsgDiv.Visible = true;
        }
        else
        {
            MainDiv.Visible = true;
            AccessMsgDiv.Visible = false;
        }
        if (objUA.Approve == 0)
        {
            grdSurveylist.Columns[grdSurveylist.Columns.Count - 3].Visible = false;
            grdSurveylist.Columns[grdSurveylist.Columns.Count - 4].Visible = false;
        }
    }

    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }

    protected void SortGridView()
    {
        DataTable dataTable = Get_GridData();
        string sSortDirection = "";

        grdSurveylist.PageSize = Convert.ToInt32(ddlPageSize.SelectedValue);

        if (dataTable != null)
        {
            if (ViewState["SortExpression"] != null)
            {
                if ((SortDirection)ViewState["SortDirection"] == SortDirection.Descending)
                    sSortDirection = " DESC";
                else
                    sSortDirection = "";

                DataView dataView = new DataView(dataTable);
                dataView.Sort = ViewState["SortExpression"].ToString() + sSortDirection;
                grdSurveylist.DataSource = dataView;
                grdSurveylist.DataBind();
            }
            else
            {
                grdSurveylist.DataSource = dataTable;
                grdSurveylist.DataBind();
            }
        }

        if (lblPageStatus.Text.Trim() != "1")
            ScriptManager.RegisterStartupScript(this, this.GetType(), "BindPage", "Pageing();", true);
    }

    protected DataTable Get_GridData()
    {
        int iFleetID = 0;
        int iVesselID = 0;
        int iSurv_Vessel_ID = 0;
        string Cat_ID = "";

        int Verified = UDFLib.ConvertToInteger(rdoVerified.SelectedValue);

        DataTable dt;

        if (GetQueryString("fid") != "")
            iFleetID = int.Parse(GetQueryString("fid"));

        if (GetQueryString("vid") != "")
            iVesselID = int.Parse(GetQueryString("vid"));

        if (GetQueryString("s_v_id") != "")
            iSurv_Vessel_ID = int.Parse(GetQueryString("s_v_id"));

        if (!string.IsNullOrEmpty(Request.QueryString["cat_id"]))
            Cat_ID = Convert.ToString(Request.QueryString["cat_id"]);

        if (Cat_ID != "0" && Cat_ID != "-1")
        {

            string[] CatList = Cat_ID.Split(',');

            DataTable dtCatList = new DataTable();
            dtCatList.Columns.Add("ID", typeof(string));

            for (int i = 0; i < CatList.Length; i++)
            {
                dtCatList.Rows.Add(CatList[i]);
            }

            dt = objBLL.Get_NASurvayList(iFleetID, iVesselID, dtCatList, iSurv_Vessel_ID, Verified);
        }
        else
        {
            dt = objBLL.Get_NASurvayList(iFleetID, iVesselID, UDFLib.ConvertToInteger(Cat_ID), iSurv_Vessel_ID, Verified);
        }

        lblRecordCount.Text = dt.Rows.Count.ToString();

        if (dt.Rows.Count > Convert.ToInt32(ddlPageSize.SelectedValue))
            lblPageStatus.Text = Math.Ceiling(Convert.ToDecimal(dt.Rows.Count) / Convert.ToDecimal(ddlPageSize.SelectedValue)).ToString();
        else
            lblPageStatus.Text = "1";

        return dt;
    }

    protected void grdSurveylist_Sorting(object sender, GridViewSortEventArgs e)
    {
        ViewState["SortExpression"] = e.SortExpression;

        if (ViewState["SortDirection"] == null)
            ViewState["SortDirection"] = SortDirection.Ascending;

        if ((SortDirection)ViewState["SortDirection"] == SortDirection.Ascending)
        {
            ViewState["SortDirection"] = SortDirection.Descending;
            SortGridView();
        }
        else
        {
            ViewState["SortDirection"] = SortDirection.Ascending;
            SortGridView();
        }
    }

    protected void grdSurveylist_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdSurveylist.PageIndex = e.NewPageIndex;
        SortGridView();
    }

    protected void grdSurveylist_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.ToUpper() == "MARKASACTIVE")
        {
            string args = e.CommandArgument.ToString();
            string[] arg = args.Split(',');

            int Vessel_ID = int.Parse(arg[0]);
            int Surv_Vessel_ID = int.Parse(arg[1]);

            objBLL.UPDATE_SurveyStatus(Vessel_ID, Surv_Vessel_ID, 1, GetSessionUserID());
            SortGridView();
        }
        if (e.CommandName.ToUpper() == "VERIFY")
        {
            string args = e.CommandArgument.ToString();
            string[] arg = args.Split(',');

            int Vessel_ID = int.Parse(arg[0]);
            int Surv_Vessel_ID = int.Parse(arg[1]);

            objBLL.Verify_NAMarked_Survey(Vessel_ID, Surv_Vessel_ID, 1, GetSessionUserID());
            SortGridView();
        }
        if (e.CommandName.ToUpper() == "UNDO_VERIFY")
        {
            string args = e.CommandArgument.ToString();
            string[] arg = args.Split(',');

            int Vessel_ID = int.Parse(arg[0]);
            int Surv_Vessel_ID = int.Parse(arg[1]);

            objBLL.Verify_NAMarked_Survey(Vessel_ID, Surv_Vessel_ID, 0, GetSessionUserID());
            SortGridView();
        }



    }

    public string GetQueryString(string Query)
    {
        try
        {
            if (Request.QueryString[Query] != null)
            {
                return Request.QueryString[Query].ToString();
            }
            else
                return "";
        }
        catch { return ""; }
    }

    protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
    {
        grdSurveylist.PageSize = int.Parse(ddlPageSize.SelectedValue);
        grdSurveylist.PageIndex = 0;
        SortGridView();
    }

    protected void rdoVerified_SelectedIndexChanged(object sender, EventArgs e)
    {
        grdSurveylist.PageIndex = 0;
        SortGridView();
    }
}