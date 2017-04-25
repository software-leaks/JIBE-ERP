using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using AjaxControlToolkit;
using System.Text;

using SMS.Business.Infrastructure;
using SMS.Properties;
using SMS.Business.CP;


public partial class CP_Openings : System.Web.UI.Page
{

    BLL_CP_Openings oBLL_Openings = new BLL_CP_Openings();
    UserAccess objUA = new UserAccess();
    public string Type = null;
    public string OperationMode = "";
    public string CurrStatus = null;
    public Boolean uaEditFlag = false;
    public Boolean uaDeleteFlage = false;
    private int Vessel_id = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        UserAccessValidation();


        if (!IsPostBack)
        {
            BindVessels();
            BindGrid();
        }

    }

   
    protected void BindCPStatus()
    {

        
    }

    protected void SetDefaultStatus()
    {

    }

    protected void BindVessels()
    {
        BLL_Infra_VesselLib objBLL = new BLL_Infra_VesselLib();
        int UserCompanyID = UDFLib.ConvertToInteger(Session["USERCOMPANYID"].ToString());

        DataTable dt = oBLL_Openings.GetVesselListAll(UserCompanyID);

        ddlvessel.DataSource = dt;
        ddlvessel.DataTextField = "VESSEL_NAME";
        ddlvessel.DataValueField = "VESSEL_ID";
        ddlvessel.DataBind();
        ddlvessel.Items.Insert(0, new ListItem("-Select-", "0"));
    }
    protected void BindCPSuppliers()
    {

    }

    public void BindGrid()
    {
        try
        {
            SetDefaultStatus();
            int rowcount = 0;

            DataTable dtStatus = new DataTable();
            dtStatus.Columns.Add("Status");
            foreach (ListItem chkitem in chkOpeningStatus.Items)
            {
                if (chkitem.Selected)
                {
                    DataRow dr = dtStatus.NewRow();

                    dr["Status"] = chkitem.Value;

                    dtStatus.Rows.Add(dr);
                }
            }
            if (Request.QueryString["vid"] != null)
            {
                Vessel_id = Convert.ToInt32(Request.QueryString[0]);
                if (ddlvessel.Items.FindByValue(Request.QueryString[0]) != null)
                    ddlvessel.SelectedValue = ddlvessel.Items.FindByValue(Request.QueryString[0]).Value;
                ddlvessel.Enabled = false;

            }
            else
            {
                Vessel_id = Convert.ToInt32(ddlvessel.SelectedValue);
            }
            DataTable dt = oBLL_Openings.GetOpeningList(dtStatus, ucCustomPager1.CurrentPageIndex, ucCustomPager1.PageSize, Vessel_id, ref  rowcount);


            if (ucCustomPager1.isCountRecord == 1)
            {
                ucCustomPager1.CountTotalRec = rowcount.ToString();
                ucCustomPager1.BuildPager();
            }


            gvOpenings.DataSource = dt;
            gvOpenings.DataBind();

        }
        catch { }
    }



    protected void UserAccessValidation()
    {
        int CurrentUserID = GetSessionUserID();
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

        BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

        if (objUA.View == 0)
            Response.Redirect("~/default.aspx?msgid=1");

        if (objUA.Add == 0)
        {
            
        }
        if (objUA.Edit == 1)
            uaEditFlag = true;
        else
            // btnsave.Visible = false;

            if (objUA.Delete == 1) uaDeleteFlage = true;

    }

    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }

    protected void gvNotices_Sorting(object sender, GridViewSortEventArgs se)
    {

        ViewState["SORTBYCOLOUMN"] = se.SortExpression;

        if (ViewState["SORTDIRECTION"] != null && ViewState["SORTDIRECTION"].ToString() == "0")
            ViewState["SORTDIRECTION"] = 1;
        else
            ViewState["SORTDIRECTION"] = 0;

        BindGrid();

    }

 
    protected void btnGet_Click(object sender, EventArgs e)
    {
        BindGrid();
    }


    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindGrid();
    }

    protected void btnRefresh_Click(object sender, ImageClickEventArgs e)
    {
        BindGrid();
    }
}