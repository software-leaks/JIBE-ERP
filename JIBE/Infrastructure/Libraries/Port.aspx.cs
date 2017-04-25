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

public partial class Port : System.Web.UI.Page
{

    BLL_Infra_Port objBLLPort = new BLL_Infra_Port();
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

            Load_CountryList(ddlPortCountry);
            BindPortGrid();
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

        if (objUA.Add == 0)ImgAdd.Visible = false;
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

    public void BindPortGrid()
    {

        int rowcount = ucCustomPagerItems.isCountRecord;

        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());


        DataTable dt = objBLLPort.Get_PortList_Search(txtfilter.Text != "" ? txtfilter.Text : null, UDFLib.ConvertIntegerToNull(ddlPortCountryFilter.SelectedValue), sortbycoloumn, sortdirection
            , ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref  rowcount);


        if (ucCustomPagerItems.isCountRecord == 1)
        {
            ucCustomPagerItems.CountTotalRec = rowcount.ToString();
            ucCustomPagerItems.BuildPager();
        }


        if (dt.Rows.Count > 0)
        {
            GridViewPort.DataSource = dt;
            GridViewPort.DataBind();
        }
        else
        {
            GridViewPort.DataSource = dt;
            GridViewPort.DataBind();
        }

    }

    protected void btnsave_Click(object sender, EventArgs e)
    {
        string Port_Lat = txtLatDeg.Text + "-" + txtLatMin.Text + "'" + rdoNorthSouth.SelectedValue;
        string Port_Lon = txtLonDeg.Text + "-" + txtLonMin.Text + "'" + rdoEastWest.SelectedValue;


        if (HiddenFlag.Value == "Add")
        {
            int retval = objBLLPort.InsertPort(txtPortName.Text, int.Parse(ddlPortCountry.SelectedValue), txtBPCode.Text, Port_Lat, Port_Lon, txtOcean.Text
                , UDFLib.ConvertIntegerToNull(txtUTC.Text), ddlPortCountry.SelectedItem.Text, chkWarRisk.Checked, Convert.ToInt32(Session["USERID"]));
        }
        else
        {
            int retval = objBLLPort.EditPort(Convert.ToInt32(txtPortID.Text), txtPortName.Text, int.Parse(ddlPortCountry.SelectedValue), txtBPCode.Text, Port_Lat, Port_Lon, txtOcean.Text
                , UDFLib.ConvertIntegerToNull(txtUTC.Text), ddlPortCountry.SelectedItem.Text, chkWarRisk.Checked, Convert.ToInt32(Session["USERID"]));
        }

        BindPortGrid();

        string hidemodal = String.Format("hideModal('divadd')");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "hidemodal", hidemodal, true);
    }

    protected void ImgAdd_Click(object sender, EventArgs e)
    {
        this.SetFocus("ctl00_MainContent_txtPortName");
        HiddenFlag.Value = "Add";

        OperationMode = "Add Port";

        ClearFields();

        string AddPort = String.Format("showModal('divadd',false);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddPort", AddPort, true);


    }




    protected void ImgExpExcel_Click(object sender, EventArgs e)
    {
        int rowcount = ucCustomPagerItems.isCountRecord;

        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());


        DataTable dt = objBLLPort.Get_PortList_Search(txtfilter.Text != "" ? txtfilter.Text : null, UDFLib.ConvertIntegerToNull(ddlPortCountryFilter.SelectedValue), sortbycoloumn, sortdirection
            , null, null, ref  rowcount);


        string[] HeaderCaptions = { "Port Name", "Country", "BP Code", "Lattitude", "Longitude", "Ocean", "UTC", "War Risk" };
        string[] DataColumnsName = { "PORT_NAME", "COUNTRY_NAME", "BP_CODE", "PORT_LAT", "PORT_LON", "OCEAN", "UTC", "WarRisk" };

        GridViewExportUtil.ShowExcel(dt, HeaderCaptions, DataColumnsName, "Port", "Port", "");

    }

    public void ClearFields()
    {
        txtPortID.Text = "";
        txtPortName.Text = "";
        txtBPCode.Text = "";
        ddlPortCountry.SelectedValue = "0";
        txtOcean.Text = "";
        txtUTC.Text = "";
        txtLatMin.Text = "";
        txtLatDeg.Text = "";
        txtLonMin.Text = "";
        txtLonDeg.Text = "";
        chkWarRisk.Checked = false;
    }

    protected void btnFilter_Click(object sender, EventArgs e)
    {

        BindPortGrid();

    }

    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        txtfilter.Text = "";
        ddlPortCountryFilter.SelectedValue = "0";

        BindPortGrid();
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
        int retval = objBLLPort.DeletePort_DL(Convert.ToInt32(e.CommandArgument.ToString()), Convert.ToInt32(Session["USERID"]));
        BindPortGrid();
    }

    protected void onUpdate(object source, CommandEventArgs e)
    {
        HiddenFlag.Value = "Edit";
        OperationMode = "Edit Port";

        DataTable dt = new DataTable();
        dt = objBLLPort.Get_PortDetailsByID(Convert.ToInt32(e.CommandArgument.ToString()));

        txtPortID.Text = dt.Rows[0]["PORT_ID"].ToString();
        txtPortName.Text = dt.Rows[0]["PORT_NAME"].ToString();
        txtBPCode.Text = dt.Rows[0]["BP_CODE"].ToString();
        ddlPortCountry.SelectedValue = dt.Rows[0]["Country_ID"].ToString() !="" ? dt.Rows[0]["Country_ID"].ToString() : "0" ;
        txtOcean.Text = dt.Rows[0]["OCEAN"].ToString();
        txtUTC.Text = dt.Rows[0]["UTC"].ToString();
        chkWarRisk.Checked = Convert.ToBoolean(dt.Rows[0]["WarRisk"]);
        if (dt.Rows[0]["LA_LP"].ToString() == "N")
            rdoNorthSouth.SelectedValue = "N";
        else
            rdoNorthSouth.SelectedValue = "S";
        txtLatMin.Text = dt.Rows[0]["LA_MP"].ToString();
        txtLatDeg.Text = dt.Rows[0]["LA_FP"].ToString();

        if (dt.Rows[0]["LA_LP"].ToString() == "E")
            rdoEastWest.SelectedValue = "E";
        else
            rdoEastWest.SelectedValue = "W";

        txtLonMin.Text = dt.Rows[0]["LO_MP"].ToString();
        txtLonDeg.Text = dt.Rows[0]["LO_FP"].ToString();



        string InfoDiv = "Get_Record_Information_Details('Lib_Ports','PORT_ID=" + txtPortID.Text + "')";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "InfoDiv", InfoDiv, true);



        string AddPort = String.Format("showModal('divadd',false);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddPort", AddPort, true);

    }

    protected void GridViewPort_RowDataBound(object sender, GridViewRowEventArgs e)
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

    protected void GridViewPort_Sorting(object sender, GridViewSortEventArgs se)
    {
        ViewState["SORTBYCOLOUMN"] = se.SortExpression;

        if (ViewState["SORTDIRECTION"] != null && ViewState["SORTDIRECTION"].ToString() == "0")
            ViewState["SORTDIRECTION"] = 1;
        else
            ViewState["SORTDIRECTION"] = 0;

        BindPortGrid();
    }

}
