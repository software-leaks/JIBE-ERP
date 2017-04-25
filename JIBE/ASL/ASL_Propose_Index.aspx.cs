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
using SMS.Business.ASL;
 


public partial class ASL_Propose_Index : System.Web.UI.Page
{
    BLL_Infra_AirPort objBLLAirPort = new BLL_Infra_AirPort();
    BLL_Infra_Country objBLLCountry = new BLL_Infra_Country();
    UserAccess objUA = new UserAccess();

    public string OperationMode = "";

    public Boolean uaEditFlag = false;
    public Boolean uaDeleteFlage = false;
    public int Pageset;
    public string status; 
    protected void Page_Load(object sender, EventArgs e)
    {
        UserAccessValidation();
        if (!IsPostBack)
        {
            ViewState["SORTDIRECTION"] = null;
            ViewState["SORTBYCOLOUMN"] = null;

            ucCustomPagerItems.PageSize = 20;
            Pageset = 0;
            status = "0";
            BindGrid();
            BindProposedStatusDDL();
        }

    }
    private void BindProposedStatusDDL()
    {
        try
        {

            DataTable dt = BLL_ASL_Supplier.Get_ASL_System_SupplierProposed_Parameter(1, null, UDFLib.ConvertToInteger(Session["UserID"].ToString()));
            DataRow dr = dt.NewRow();

            dr[0] = "0";
            dr[1] = "All";
            dr[2] = "All";
            dr[3] = "1";
            dt.Rows.Add(dr);

            ddlStatus.DataSource = dt;
            ddlStatus.DataValueField = "Code";
            ddlStatus.DataTextField = "Name";
            ddlStatus.DataBind();
           
        }
        catch
        {
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

    public void BindGrid()
    {

        int rowcount = ucCustomPagerItems.isCountRecord;

        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());
        int? StatusID = UDFLib.ConvertIntegerToNull(status);

        DataTable dt = BLL_ASL_Supplier.Get_Proposed_Supplier_Search(txtfilter.Text != "" ? txtfilter.Text : null, StatusID, sortbycoloumn, sortdirection
            , ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref  rowcount, UDFLib.ConvertToInteger(Session["UserID"].ToString()), Pageset);


        if (ucCustomPagerItems.isCountRecord == 1)
        {
            ucCustomPagerItems.CountTotalRec = rowcount.ToString();
            ucCustomPagerItems.BuildPager();
        }

        if (dt.Rows.Count > 0)
        {
            gvProposedSupp.DataSource = dt;
            gvProposedSupp.DataBind();
        }
        else
        {
            gvProposedSupp.DataSource = dt;
            gvProposedSupp.DataBind();
        }

    }

    protected void btnsave_Click(object sender, EventArgs e)
    {

        if (HiddenFlag.Value == "Add")
        {
            //int retval = objBLLAirPort.Insert_AirPort(txtIndent.Text, UDFLib.ConvertStringToNull(ddlAirportType.SelectedValue), txtAirPortName.Text
            //    , txtLatDeg.Text, txtLonDeg.Text, UDFLib.ConvertIntegerToNull(txtElevation.Text), txtContinent.Text, txtISOCountry.Text
            //    , UDFLib.ConvertIntegerToNull(ddlAirportCountry.SelectedValue)
            //    , txtisoregion.Text, txtMunicipal.Text, "", txtGpsCode.Text, txtIataCode.Text
            //    , txtLocalCode.Text, Convert.ToInt32(Session["USERID"]));
        }
        else
        {
            //int retval = objBLLAirPort.Edit_AirPort(Convert.ToInt32(txtAirPortID.Text), txtIndent.Text, UDFLib.ConvertStringToNull(ddlAirportType.SelectedValue), txtAirPortName.Text
            //    , txtLatDeg.Text, txtLonDeg.Text, UDFLib.ConvertIntegerToNull(txtElevation.Text), txtContinent.Text, txtISOCountry.Text
            //    , UDFLib.ConvertIntegerToNull(ddlAirportCountry.SelectedValue)
            //    , txtisoregion.Text, txtMunicipal.Text, "", txtGpsCode.Text, txtIataCode.Text
            //    , txtLocalCode.Text, Convert.ToInt32(Session["USERID"]));
        }
        Pageset = 0;
        status = "0";
        BindGrid();

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
        int Pageset = 1;
        int rowcount = ucCustomPagerItems.isCountRecord;

        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());

        DataTable dt = BLL_ASL_Supplier.Get_Proposed_Supplier_Search(txtfilter.Text != "" ? txtfilter.Text : null, null, sortbycoloumn, sortdirection
            , ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref  rowcount, UDFLib.ConvertToInteger(Session["UserID"].ToString()), Pageset);

        string[] HeaderCaptions = { "Supplier Name", "ProPose Status", "Address", "Phone", "Email", "PIC Name",  };
        string[] DataColumnsName = { "Supplier_Name", "Propose_Status", "Address", "Phone", "Email", "PIC_NAME", };

        GridViewExportUtil.ShowExcel(dt, HeaderCaptions, DataColumnsName, "ProposedSupplier", "Proposed Supplier", "");
    }

    public void ClearFields()
    {

        //txtAirPortID.Text = "";
        //txtAirPortName.Text = "";

        //txtLatDeg.Text = "";
        //txtLonDeg.Text = "";

        //txtIndent.Text = "";
        //ddlAirportType.SelectedValue = "0";

        //txtElevation.Text = "";
        //txtContinent.Text = "";

        //txtISOCountry.Text = "";
        //ddlAirportCountry.SelectedValue = "0";

        //txtisoregion.Text = "";
        //txtMunicipal.Text = "";

        //txtIataCode.Text = "";
        //txtGpsCode.Text = "";
        //txtLocalCode.Text = "";


    }

    protected void btnFilter_Click(object sender, EventArgs e)
    {
        Pageset = 1;
        status = ddlStatus.SelectedValue;
        BindGrid();

    }

    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        txtfilter.Text = "";
        Pageset = 1;
        ddlStatus.SelectedValue = "21";
        status = ddlStatus.SelectedValue;
        BindGrid();
    }

    protected void Load_CountryList(DropDownList ddlCountry)
    {
        DataTable dt = objBLLCountry.Get_CountryList();


        ddlCountry.DataSource = dt;
        ddlCountry.DataTextField = "Country";
        ddlCountry.DataValueField = "ID";
        ddlCountry.DataBind();
        ddlCountry.Items.Insert(0, new ListItem("-Select-", "0"));

   

    }

    protected void onDelete(object source, CommandEventArgs e)
    {
        int retval = objBLLAirPort.Delete_AirPort(Convert.ToInt32(e.CommandArgument.ToString()), Convert.ToInt32(Session["USERID"].ToString()));
        Pageset = 1;
        status = ddlStatus.SelectedValue;
        BindGrid();
    }

    protected void onUpdate(object source, CommandEventArgs e)
    {
        HiddenFlag.Value = "Edit";
        OperationMode = "Edit Airport";

        DataTable dt = new DataTable();
        dt = objBLLAirPort.Get_AirPortList((Convert.ToInt32(e.CommandArgument.ToString())));

      

  
        string AddAirPort = String.Format("showModal('divadd',false);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddAirPort", AddAirPort, true);

    }

    protected void gvProposedSupp_RowDataBound(object sender, GridViewRowEventArgs e)
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

    protected void gvProposedSupp_Sorting(object sender, GridViewSortEventArgs se)
    {
        ViewState["SORTBYCOLOUMN"] = se.SortExpression;

        if (ViewState["SORTDIRECTION"] != null && ViewState["SORTDIRECTION"].ToString() == "0")
            ViewState["SORTDIRECTION"] = 1;
        else
            ViewState["SORTDIRECTION"] = 0;

        Pageset = 1;
        status = ddlStatus.SelectedValue;
        BindGrid();
    }
}