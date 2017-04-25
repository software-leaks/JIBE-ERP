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
using SMS.Business.PURC;
using Telerik.Web.UI;
using SMS.Properties;
using SMS.Business.Infrastructure;

public partial class Location : System.Web.UI.Page
{
    public string LocationID;
    public BLL_PURC_Purchase objBLLLoc = new BLL_PURC_Purchase();
    public SMS.Properties.UserAccess objUA = new SMS.Properties.UserAccess();
    public string OperationMode = "";
    public Boolean uaEditFlag = false;
    public Boolean uaDeleteFlage = false;
    BLL_Infra_VesselLib objBLL = new BLL_Infra_VesselLib();
    protected void Page_Load(object sender, EventArgs e)
    {
        UserAccessValidation();

        if (!IsPostBack)
        {
            
            ViewState["SORTDIRECTION"] = null;
            ViewState["SORTBYCOLOUMN"] = null;

            ucCustomPagerItems.PageSize = 20;

            BindrgdLocation();
            BindLocationCombo();
            Load_VesselType();

            HiddenFlag.Value = "Add";

        }
    }

    protected void UserAccessValidation()
    {
        int CurrentUserID = Convert.ToInt32(Session["userid"]);
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());
        SMS.Business.Infrastructure.BLL_Infra_UserCredentials objUser = new SMS.Business.Infrastructure.BLL_Infra_UserCredentials();

        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

        if (objUA.View == 0)
            Response.Redirect("~/default.aspx");

        if (objUA.Add == 0) ImgAdd.Visible = false;
        if (objUA.Edit == 1)
            uaEditFlag = true;
        else
            btnSaveLocation.Visible = false;
        if (objUA.Delete == 1) uaDeleteFlage = true;


    }


    protected void onUpdate(object source, CommandEventArgs e)
    {
        using (BLL_PURC_Purchase objTechService = new BLL_PURC_Purchase())
        {
            HiddenFlag.Value = "Edit";
            OperationMode = "Edit Location";

            TxtShotCode.Enabled = false;
            DataTable dtItems = new DataTable();
            dtItems = objTechService.SelectLocation();
            dtItems.DefaultView.RowFilter = "Code= '" + e.CommandArgument.ToString() + "'";
            txtCode.Text = dtItems.DefaultView[0]["Code"].ToString();

            txtParentType.Text = dtItems.DefaultView[0]["Parent_Type"].ToString();
            TxtShotCode.Text = dtItems.DefaultView[0]["Short_Code"].ToString();
            txtShortDesc.Text = dtItems.DefaultView[0]["Description"].ToString();
            txtLongDesc.Text = dtItems.DefaultView[0]["Long_Discription"].ToString();
            if (dtItems.DefaultView[0]["Vessel_Type"] == DBNull.Value)
                ddlvessel_AddType.SelectedIndex = 0;
            else
            ddlvessel_AddType.SelectedValue = dtItems.DefaultView[0]["Vessel_Type"].ToString();
            dvLoca.Visible = false;

            string AssginLocmodal = String.Format("showModal('divaddLocation',false);");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ApprmodalUser", AssginLocmodal, true);
 
        }

    }

    protected void onDelete(object source, CommandEventArgs e)
    {
        string[] strIds = e.CommandArgument.ToString().Split(',');
        string sCode = strIds[0];
        string sParentType = strIds[1];

        using (BLL_PURC_Purchase objTechService = new BLL_PURC_Purchase())
        {
            LocationData objLocationDO = new LocationData();

            LocationID = sCode;
            objLocationDO.Code = sCode;

            objLocationDO.CurrentUser = Session["userid"].ToString();
            int count = objTechService.DeleteLocation(objLocationDO);
            BindrgdLocation();

        }
    }



    public void BindrgdLocation()
    {


        int rowcount = ucCustomPagerItems.isCountRecord;

        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());


        DataTable dt = objBLLLoc.Location_Search(txtSearch.Text != "" ? txtSearch.Text : null, sortbycoloumn, sortdirection
        , ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref  rowcount);


        if (ucCustomPagerItems.isCountRecord == 1)
        {
            ucCustomPagerItems.CountTotalRec = rowcount.ToString();
            ucCustomPagerItems.BuildPager();
        }


        if (dt.Rows.Count > 0)
        {
            rgdLocation.DataSource = dt;
            rgdLocation.DataBind();
        }
        else
        {
            rgdLocation.DataSource = dt;
            rgdLocation.DataBind();
        }
        
        
        
        //string filter = "";
        //filter = "Description Like '%" + txtSearch.Text.Trim() + "%'  OR  Short_Code Like '%" + txtSearch.Text.Trim() + "%' ";

        //using (BLL_PURC_Purchase objTechService = new BLL_PURC_Purchase())
        //{
        //    DataTable dtLocation = new DataTable();
        //    dtLocation = objTechService.SelectLocation();
        //    dtLocation.DefaultView.RowFilter = filter;
        //    rgdLocation.DataSource = dtLocation.DefaultView;
        //    rgdLocation.DataBind();

        //}
    }

    private void BindLocationCombo()
    {

        cmbParent.Items.Clear();

        using (BLL_PURC_Purchase objTechService = new BLL_PURC_Purchase())
        {
            cmbParent.DataSource = objTechService.BindLocationCombo();
            cmbParent.DataTextField = "Short_Code";
            cmbParent.DataValueField = "code";
            cmbParent.DataBind();
            cmbParent.Items.FindByValue("1").Selected = true;
            cmbParent.Enabled = false;
        }

    }




    protected void DivbtnSave_Click(object sender, EventArgs e)
    {


        using (BLL_PURC_Purchase objTechService = new BLL_PURC_Purchase())
        {
            LocationData objLocDo = new LocationData();
            objLocDo.Code = txtCode.Text;
            objLocDo.ParentType = cmbParent.SelectedValue;
            objLocDo.ShortCode = TxtShotCode.Text;
            objLocDo.ShortDiscription = txtShortDesc.Text;
            objLocDo.LongDiscription = txtLongDesc.Text;
            objLocDo.CurrentUser = Session["userid"].ToString();
            objLocDo.VesselType = ddlvessel_AddType.SelectedValue;

            if (HiddenFlag.Value == "Add")
            {
                objLocDo.NoOfLoc = txtNoLoc.Text;
                int retVal = objTechService.SaveLocation(objLocDo);
            }
            else
            {
                int retVal = objTechService.EditLocation(objLocDo);
            }

            BindrgdLocation();
            BindLocationCombo();

            string hidemodal = String.Format("hideModal('divaddLocation')");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "hidemodal", hidemodal, true);

        }


    }

     

    protected void btnFilter_Click(object sender, EventArgs e)
    {
        BindrgdLocation();

    }

    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        txtSearch.Text = "";
        BindrgdLocation();
    }


    protected void ImgAdd_Click(object sender, EventArgs e)
    {
        this.SetFocus("ctl00_MainContent_TxtShotCode");
        HiddenFlag.Value = "Add";
        OperationMode = "Add Location";
        txtNoLoc.Text = "1";
        TxtShotCode.Text = "";
        txtShortDesc.Text = "";
        txtLongDesc.Text = "";
        ddlvessel_AddType.SelectedIndex = 0;
        TxtShotCode.Enabled = true;
        dvLoca.Visible = true;
        string AddLocmodal = String.Format("showModal('divaddLocation',false);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddLocmodal", AddLocmodal, true);

    }


    protected void ImgExpExcel_Click(object sender, EventArgs e)
    {
        int rowcount = ucCustomPagerItems.isCountRecord;

        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());


        DataTable dt = objBLLLoc.Location_Search(txtSearch.Text != "" ? txtSearch.Text : null, sortbycoloumn, sortdirection
                                                 , null, null, ref  rowcount);


        string[] HeaderCaptions = { "Short Code", "Description", "  Long Description." };
        string[] DataColumnsName = { "Short_code", "Description", "Long_Discription" };

        GridViewExportUtil.ShowExcel(dt, HeaderCaptions, DataColumnsName, "Location", "Location", "");

    }


    protected void rgdLocation_RowDataBound(object sender, GridViewRowEventArgs e)
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

   


    protected void rgdLocation_Sorting(object sender, GridViewSortEventArgs se)
    {


        ViewState["SORTBYCOLOUMN"] = se.SortExpression;

        if (ViewState["SORTDIRECTION"] != null && ViewState["SORTDIRECTION"].ToString() == "0")
            ViewState["SORTDIRECTION"] = 1;
        else
            ViewState["SORTDIRECTION"] = 0;

        BindrgdLocation();
    }


    public void Load_VesselType()
    {

        DataTable dtVesselType = objBLL.Get_VesselType();


        ddlvessel_AddType.DataSource = dtVesselType;
        ddlvessel_AddType.DataTextField = "VesselTypes";
        ddlvessel_AddType.DataValueField = "ID";
        ddlvessel_AddType.DataBind();
        ddlvessel_AddType.Items.Insert(0, new ListItem("-Select-", "0"));
    }
    
}
