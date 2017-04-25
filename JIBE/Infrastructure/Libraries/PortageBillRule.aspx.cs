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


public partial class Infrastructure_Libraries_PortageBillRule : System.Web.UI.Page
{
    BLL_Infra_VesselLib objBLL = new BLL_Infra_VesselLib();
    BLL_Infra_PortageBillRule objBLLOps = new BLL_Infra_PortageBillRule();
    UserAccess objUA = new UserAccess();
    public string OperationMode = "";
    int Type;
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

          
            BindPortageBillRule();
        }
    }

    public void BindPortageBillRule()
    {

        int rowcount = ucCustomPagerItems.isCountRecord;
        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());

        DataTable dt = objBLLOps.Portage_Bill_Rule_Search(txtfilter.Text != "" ? txtfilter.Text : null
             , sortbycoloumn, sortdirection
             , ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref  rowcount);


        if (ucCustomPagerItems.isCountRecord == 1)
        {
            ucCustomPagerItems.CountTotalRec = rowcount.ToString();
            ucCustomPagerItems.BuildPager();
        }

        if (dt.Rows.Count > 0)
        {
            gvPortageBillRule.DataSource = dt;
            gvPortageBillRule.DataBind();
        }
        else
        {
            gvPortageBillRule.DataSource = dt;
            gvPortageBillRule.DataBind();
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
        this.SetFocus("ctl00_MainContent_DDLVessel");
        HiddenFlag.Value = "Add";
        OperationMode = "Add Prtage Bill Rule";

        ClearField();

        string AddHoldTank = String.Format("showModal('divadd',false);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddHoldTank", AddHoldTank, true);
    }
    protected void ClearField()
    {
        txtRuleName.Text = "";
        txtvalue.Text = "";
        //txtLashingGearInventoryID.Text = "";
        //txtcoargoMno.Text = "";
        //txtItemDes.Text = "";
        //txtitemModel.Text = "";
        ////txtHoldTankName.Text = "";
        ////ddlStructureType.SelectedValue = "0";
        //DDLVessel.SelectedValue = "0";
    }

    protected void onUpdate(object source, CommandEventArgs e)
    {

        HiddenFlag.Value = "Edit";
        OperationMode = "Edit Portage Bill Rule";


        string[] cmdargs = e.CommandArgument.ToString().Split(',');

        string ID = cmdargs[0].ToString();
        string Vessel_ID = cmdargs[1].ToString();

        DataTable dt = new DataTable();
        dt = objBLLOps.Get_Portage_Bill_Rule(Convert.ToInt32(ID));

        txtPortageBillruleID.Text = dt.Rows[0]["ID"].ToString();
        txtRuleName.Text = dt.Rows[0]["Name"].ToString();
        txtvalue.Text = dt.Rows[0]["Value"].ToString();   

        string HoldTankEdit = String.Format("showModal('divadd',false);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "RankCategory", HoldTankEdit, true);

    }
    protected void btnsave_Click(object sender, EventArgs e)
    {
       
         if (txtvalue.Text =="EOM")
         {
             Type=1;
         }

         else
             Type=2;
        if (HiddenFlag.Value == "Add")
        {
            int retval = objBLLOps.Insert_Portage_Bill_Rule(txtRuleName.Text, txtvalue.Text, Type);
        }
        else
        {
            int retval = objBLLOps.Edit_Portage_Bill_Rule(Convert.ToInt32(txtPortageBillruleID.Text), Convert.ToString(txtRuleName.Text), Convert.ToString(txtvalue.Text), Type);
        }

        BindPortageBillRule();

        string hidemodal = String.Format("hideModal('divadd')");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "hidemodal", hidemodal, true);

    }


    protected void btnFilter_Click(object sender, EventArgs e)
    {
        BindPortageBillRule();
    }
    protected void btnRefresh_Click(object sender, ImageClickEventArgs e)
    {

    }
    protected void ImgExpExcel_Click(object sender, ImageClickEventArgs e)
    {

    }

     protected void gvPortageBillRule_RowDataBound(object sender, GridViewRowEventArgs e)
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


        //ImageButton ImgUpdate = (ImageButton)e.Row.FindControl("ImgUpdate");


        //if (objUA.Delete == 1)
        //{ 

        //}

    }

    protected void gvPortageBillRule_Sorting(object sender, GridViewSortEventArgs se)
    {
        ViewState["SORTBYCOLOUMN"] = se.SortExpression;

        if (ViewState["SORTDIRECTION"] != null && ViewState["SORTDIRECTION"].ToString() == "0")
            ViewState["SORTDIRECTION"] = 1;
        else
            ViewState["SORTDIRECTION"] = 0;

        BindPortageBillRule();
    }
}
