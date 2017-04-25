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

using SMS.Business.Infrastructure;
using SMS.Properties;

public partial class AirPort : System.Web.UI.Page
{
    BLL_Infra_AirPort objBLLAirPort = new BLL_Infra_AirPort();
    BLL_Infra_Country objBLLCountry = new BLL_Infra_Country();
    UserAccess objUA = new UserAccess();

    public string OperationMode = "";

    public Boolean uaEditFlag = false;
    public Boolean uaDeleteFlage = false;

    protected void Page_Load(object sender, EventArgs e)
    {
        UserAccessValidation();
        if (!IsPostBack)
        {
            ViewState["SORTDIRECTION"] = null;
            ViewState["SORTBYCOLOUMN"] = null;

            ucCustomPagerItems.PageSize = 20;

            Load_CountryList(ddlAirportCountry);
            BindAirPortGrid();
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

        if (objUA.Add == 0)
        {
            ImgAdd.Visible = false;
        }
        if (objUA.Edit == 1)
            uaEditFlag = true;
        else
            btnsave.Visible = false;
        if (objUA.Delete == 1) uaDeleteFlage = true;

    }

    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }

    public void BindAirPortGrid()
    {

        int rowcount = ucCustomPagerItems.isCountRecord;

        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());


        DataTable dt = objBLLAirPort.Get_AirPort_Search(txtfilter.Text != "" ? txtfilter.Text : null, UDFLib.ConvertIntegerToNull(ddlPortCountryFilter.SelectedValue), sortbycoloumn, sortdirection
            , ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref  rowcount);


        if (ucCustomPagerItems.isCountRecord == 1)
        {
            ucCustomPagerItems.CountTotalRec = rowcount.ToString();
            ucCustomPagerItems.BuildPager();
        }

        if (dt.Rows.Count > 0)
        {
            gvAirPort.DataSource = dt;
            gvAirPort.DataBind();
        }
        else
        {
            gvAirPort.DataSource = dt;
            gvAirPort.DataBind();
        }

    }

    protected void btnsave_Click(object sender, EventArgs e)
    {
       
        if (HiddenFlag.Value == "Add")
        {
            int retval = objBLLAirPort.Insert_AirPort(txtIndent.Text, UDFLib.ConvertStringToNull(ddlAirportType.SelectedValue),txtAirPortName.Text
                ,txtLatDeg.Text,txtLonDeg.Text,UDFLib.ConvertIntegerToNull(txtElevation.Text) ,txtContinent.Text,txtISOCountry.Text
                ,UDFLib.ConvertIntegerToNull(ddlAirportCountry.SelectedValue)
                , txtisoregion.Text, txtMunicipal.Text, rdoScheculeService.SelectedValue, txtGpsCode.Text, txtIataCode.Text
                ,txtLocalCode.Text,Convert.ToInt32(Session["USERID"])); 
        }
        else
        {
            int retval = objBLLAirPort.Edit_AirPort(Convert.ToInt32(txtAirPortID.Text), txtIndent.Text,UDFLib.ConvertStringToNull(ddlAirportType.SelectedValue), txtAirPortName.Text
                ,txtLatDeg.Text,txtLonDeg.Text,UDFLib.ConvertIntegerToNull(txtElevation.Text) ,txtContinent.Text,txtISOCountry.Text
                ,UDFLib.ConvertIntegerToNull(ddlAirportCountry.SelectedValue)
                , txtisoregion.Text, txtMunicipal.Text, rdoScheculeService.SelectedValue, txtGpsCode.Text, txtIataCode.Text
                ,txtLocalCode.Text,Convert.ToInt32(Session["USERID"])); 
        }

        BindAirPortGrid();

        string hidemodal = String.Format("hideModal('divadd')");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "hidemodal", hidemodal, true);
    }

    protected void ImgAdd_Click(object sender, EventArgs e)
    {
        this.SetFocus("ctl00_MainContent_txtPortName");
        HiddenFlag.Value = "Add";

        OperationMode = "Add Airport";

        ClearFields();

        string AddPort = String.Format("showModal('divadd',false);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddPort", AddPort, true);


    }

    protected void ImgExpExcel_Click(object sender, EventArgs e)
    {

        int rowcount = ucCustomPagerItems.isCountRecord;

        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());

        DataTable dt = objBLLAirPort.Get_AirPort_Search(txtfilter.Text != "" ? txtfilter.Text : null, UDFLib.ConvertIntegerToNull(ddlPortCountryFilter.SelectedValue), sortbycoloumn, sortdirection
            , null, null, ref  rowcount);

        string[] HeaderCaptions = { "Name", "IATA Code", "GPS Code", "Country", "Continent", "Municipality", "Scheduled Service"};
        string[] DataColumnsName = { "AirportName", "iata_code", "gps_code", "Country_Name", "Continent", "Municipality", "Scheduled_Service"};

        GridViewExportUtil.ShowExcel(dt, HeaderCaptions, DataColumnsName, "Airport", "Airport", "");
    }

    public void ClearFields()
    {

        txtAirPortID.Text = "";
        txtAirPortName.Text = "";

        txtLatDeg.Text = "";
        txtLonDeg.Text= "";

        txtIndent.Text = "";
        ddlAirportType.SelectedValue = "0";

        txtElevation.Text = "";
        txtContinent.Text = "";

        txtISOCountry.Text = "";
        ddlAirportCountry.SelectedValue = "0";

        txtisoregion.Text = "";
        txtMunicipal.Text = "";

        txtIataCode.Text = "";
        txtGpsCode.Text = "";
        txtLocalCode.Text = "";


    }

    protected void btnFilter_Click(object sender, EventArgs e)
    {

        BindAirPortGrid();

    }

    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        txtfilter.Text = "";
        ddlPortCountryFilter.SelectedValue = "0";

        BindAirPortGrid();
    }

    protected void Load_CountryList(DropDownList ddlCountry)
    {
        DataTable dt = objBLLCountry.Get_CountryList();


        ddlCountry.DataSource = dt;
        ddlCountry.DataTextField = "Country";
        ddlCountry.DataValueField = "ID";
        ddlCountry.DataBind();

        ddlCountry.Items.Insert(0, new ListItem("-Select-", "0"));

        ddlPortCountryFilter.DataSource = dt;
        ddlPortCountryFilter.DataTextField = "Country";
        ddlPortCountryFilter.DataValueField = "ID";
        ddlPortCountryFilter.DataBind();
        ddlPortCountryFilter.Items.Insert(0, new ListItem("-ALL-", "0"));

    }

    protected void onDelete(object source, CommandEventArgs e)
    {
        int retval = objBLLAirPort.Delete_AirPort(Convert.ToInt32(e.CommandArgument.ToString()),Convert.ToInt32(Session["USERID"].ToString()) );
        BindAirPortGrid();
    }

    protected void onUpdate(object source, CommandEventArgs e)
    {
        HiddenFlag.Value = "Edit";
        OperationMode = "Edit Airport";

        DataTable dt = new DataTable();
        dt = objBLLAirPort.Get_AirPortList((Convert.ToInt32(e.CommandArgument.ToString())));

        txtAirPortID.Text = dt.Rows[0]["ID"].ToString();
        txtAirPortName.Text = dt.Rows[0]["AirportName"].ToString();
        
        txtLatDeg.Text = dt.Rows[0]["Latitude_deg"].ToString();
        txtLonDeg.Text = dt.Rows[0]["Longitude_deg"].ToString();

        txtIndent.Text = dt.Rows[0]["Indent"].ToString();
        ddlAirportType.SelectedValue = dt.Rows[0]["Type"].ToString() != "" ? dt.Rows[0]["Type"].ToString() : "0";

        txtElevation.Text = dt.Rows[0]["Elevation_ft"].ToString();
        txtContinent.Text = dt.Rows[0]["Continent"].ToString();

        txtISOCountry.Text = dt.Rows[0]["ISO_Country"].ToString();
        ddlAirportCountry.SelectedValue = dt.Rows[0]["Country_ID"].ToString()!="" ? dt.Rows[0]["Country_ID"].ToString() : "0";

        txtisoregion.Text = dt.Rows[0]["iso_region"].ToString();
        txtMunicipal.Text = dt.Rows[0]["Municipality"].ToString();

        txtIataCode.Text = dt.Rows[0]["iata_code"].ToString();
        txtGpsCode.Text = dt.Rows[0]["gps_code"].ToString();
        txtLocalCode.Text = dt.Rows[0]["Local_Code"].ToString();
        rdoScheculeService.SelectedValue = dt.Rows[0]["Scheduled_Service"].ToString() !="" ?dt.Rows[0]["Scheduled_Service"].ToString() :"0";




        string InfoDiv = "Get_Record_Information_Details('LIB_AIRPORTS','ID="+ txtAirPortID.Text +"')";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "InfoDiv", InfoDiv, true);

 
        
        string AddAirPort = String.Format("showModal('divadd',false);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddAirPort", AddAirPort, true);

    }

    protected void gvAirPort_RowDataBound(object sender, GridViewRowEventArgs e)
    {

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

    protected void gvAirPort_Sorting(object sender, GridViewSortEventArgs se)
    {
        ViewState["SORTBYCOLOUMN"] = se.SortExpression;

        if (ViewState["SORTDIRECTION"] != null && ViewState["SORTDIRECTION"].ToString() == "0")
            ViewState["SORTDIRECTION"] = 1;
        else
            ViewState["SORTDIRECTION"] = 0;

        BindAirPortGrid();
    }

}