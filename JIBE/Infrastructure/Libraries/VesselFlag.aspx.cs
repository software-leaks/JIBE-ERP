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
using System.Text.RegularExpressions;
using SMS.Business.Infrastructure;
using SMS.Properties;


public partial class VesselFlag : System.Web.UI.Page
{


    BLL_Infra_VesselFlag objBLL = new BLL_Infra_VesselFlag();
    BLL_Infra_Country objBLLCountry = new BLL_Infra_Country();
    BLL_Infra_Company objBLLComp = new BLL_Infra_Company();
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
            BindVesselFlag();
        }

    }

    

    public void BindVesselFlag()
    {

        int rowcount = ucCustomPagerItems.isCountRecord;

        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());


        DataTable dt = objBLL.SearchVesselFlag(txtfilter.Text != "" ? txtfilter.Text : null,sortbycoloumn, sortdirection
             , ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref  rowcount);


        if (ucCustomPagerItems.isCountRecord == 1)
        {
            ucCustomPagerItems.CountTotalRec = rowcount.ToString();
            ucCustomPagerItems.BuildPager();
        }


        if (dt.Rows.Count > 0)
        {
            gvVesselFlag.DataSource = dt;
            gvVesselFlag.DataBind();
        }
        else
        {
            gvVesselFlag.DataSource = dt;
            gvVesselFlag.DataBind();
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

        if (objUA.Add == 0) ImgAdd.Visible = false;
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

    protected void ImgAdd_Click(object sender, EventArgs e)
    {
        this.SetFocus("ctl00_MainContent_txtFlagName");
        HiddenFlag.Value = "Add";
        OperationMode = "Add Vessel Flag";

        ClearField();

        string AddUserTypemodal = String.Format("showModal('divadd',false);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddUserTypemodal", AddUserTypemodal, true);
    }

    protected void ClearField()
    {

        txtFlagName.Text = "";
        txtVesselFlag.Text = "";
        txtEmail.Text = "";
    }

    protected void btnsave_Click(object sender, EventArgs e)
    {
        int responseid = 0;
        string js = "";        

        if (txtEmail.Text.Trim() != "" && !checkEmail(txtEmail.Text))
        {
            js = String.Format("alert('Please enter Valid E-mail');showModal('divadd',false);");
            if (HiddenFlag.Value == "Edit")
            {
                string InfoDiv = "Get_Record_Information_Details('LIB_VESSEL_FLAGS','Vessel_Flag=" + txtVesselFlag.Text + "')";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "InfoDiv", InfoDiv, true);
            }
        }
        else
        {
            if (HiddenFlag.Value == "Add")
            {
                responseid = objBLL.InsertVesselFlag(txtFlagName.Text, txtEmail.Text, Convert.ToInt32(Session["USERID"]));
            }
            else
            {
                responseid = objBLL.EditVesselFlag(Convert.ToInt32(txtVesselFlag.Text.Trim()), txtFlagName.Text, txtEmail.Text, Convert.ToInt32(Session["USERID"]));
            }
            if (responseid == -1)
            {
                js = String.Format("alert('Vessel Flag with same name already exists');showModal('divadd',false);");
            }
            else
            {
                BindVesselFlag();
                js = String.Format("hideModal('divadd')");
            }
        }
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "js", js, true);
        if ( HiddenFlag.Value.Equals("Edit"))
            OperationMode = "Edit Vessel Flag";
        else
            OperationMode = "Add Vessel Flag";
    }

    protected void onUpdate(object source, CommandEventArgs e)
    {

        HiddenFlag.Value = "Edit";
        OperationMode = "Edit Vessel Flag";

        DataTable dt = new DataTable();
        dt = objBLL.Get_VesselFlage_List(Convert.ToInt32(e.CommandArgument.ToString()));

        txtVesselFlag.Text = dt.Rows[0]["Vessel_Flag"].ToString();
        txtFlagName.Text = dt.Rows[0]["Flag_Name"].ToString();
        txtEmail.Text = dt.Rows[0]["mailid"].ToString();
        
        string InfoDiv = "Get_Record_Information_Details('LIB_VESSEL_FLAGS','Vessel_Flag=" + txtVesselFlag.Text + "')";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "InfoDiv", InfoDiv, true);


        string AddUserTypemodal = String.Format("showModal('divadd',false);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddUserTypemodal", AddUserTypemodal, true);

    }
    protected bool checkEmail(string Value_)
    {
        Regex reg = new Regex(@"^([\w-]+(?:\.[\w-]+)*)@((?:[\w-]+\.)*\w[\w-]{0,66})\.([a-z]{2,6}(?:\.[a-z][A-Z]{2})?)$", RegexOptions.IgnoreCase);

        if (reg.IsMatch(Value_))
            return true;
        else
            return false;
    }
    protected void onDelete(object source, CommandEventArgs e)
    {
        int retval = objBLL.DeleteVesselFlag(Convert.ToInt32(e.CommandArgument.ToString()), Convert.ToInt32(Session["USERID"]));
        BindVesselFlag();
    }

    protected void btnFilter_Click(object sender, EventArgs e)
    {
        BindVesselFlag();
    }

    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        txtfilter.Text = "";
       // ddl_Vessel_Manager_Filter.SelectedValue = "0";

        BindVesselFlag();

    }

    protected void ImgExpExcel_Click(object sender, EventArgs e)
    {

        int rowcount = ucCustomPagerItems.isCountRecord;

        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());

        DataTable dt = objBLL.SearchVesselFlag(txtfilter.Text != "" ? txtfilter.Text : null, sortbycoloumn, sortdirection
                  , null, null, ref  rowcount);

        string[] HeaderCaptions = { "Flag Name", "Email" };
        string[] DataColumnsName = { "Flag_Name", "mailid" };

        GridViewExportUtil.ShowExcel(dt, HeaderCaptions, DataColumnsName, "VesselFlag", "Vessel Flag", "");
    }

    protected void gvVesselFlag_RowDataBound(object sender, GridViewRowEventArgs e)
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

    protected void gvVesselFlag_Sorting(object sender, GridViewSortEventArgs se)
    {
        ViewState["SORTBYCOLOUMN"] = se.SortExpression;

        if (ViewState["SORTDIRECTION"] != null && ViewState["SORTDIRECTION"].ToString() == "0")
            ViewState["SORTDIRECTION"] = 1;
        else
            ViewState["SORTDIRECTION"] = 0;

        BindVesselFlag();
    }

}
