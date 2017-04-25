using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using SMS.Business.Infrastructure;
using SMS.Business.TMSA;
using System.Data;
using System.Web.UI.HtmlControls;
using SMS.Properties;
using System.Text;

public partial class TMSA_KPI_TMSA_KPI_Details : System.Web.UI.Page
{
    UserAccess objUA = new UserAccess();
    protected DataTable dtGridItems;
    public int CPID = 0;
    protected int Billing_Item_Id = 0;
    public Boolean uaEditFlag = true;//Test default true
    public Boolean uaDeleteFlage = true;
    BLL_TMSA_KPI objCP = new BLL_TMSA_KPI();

    BLL_Infra_UserCredentials objUserBLL = new BLL_Infra_UserCredentials();
    protected void Page_Load(object sender, EventArgs e)
    {
        // UserAccessValidation();
        if (!IsPostBack)
        {

            ViewState["PI_Id"] = "0";
            if (Convert.ToInt32(ViewState["PI_Id"]) != 0)
            {
             //   btnSave.Text = "Update";
            }


            BindGrid();
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
          ///  btnSave.Enabled = false;
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

    protected void BindGrid()
    {
        try
        {

            //int rowcount = ucCustomPager1.isCountRecord;
            //DataTable dt = BLL_TMSA_KPI.Get_PI_List(null, ucCustomPager1.CurrentPageIndex, ucCustomPager1.PageSize, ref  rowcount).Tables[0];


            //if (ucCustomPager1.isCountRecord == 1)
            //{
            //    ucCustomPager1.CountTotalRec = rowcount.ToString();
            //    ucCustomPager1.BuildPager();
            //}


            //gvPIList.DataSource = dt;
            //gvPIList.DataBind();



        }
        catch (Exception ex)
        {

        }
    }








    protected void btnSaveItem_Click(object sender, EventArgs e)
    {

        if (Session["CPID"] != null)
        {
            SaveData();
            BindGrid();

        }


    }


    protected void ClearData()
    {
        //ltmessage.Text = "";
        //txtEffectivedate.Text = "";
        //txtPICode.Text = "";

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        SaveData();

    }


    protected void SaveData()
    {
       // ltmessage.Text = "";
        try
        {


            // int result=  BLL_TMSA_KPI.INS_PI_Details(PI_Name, PICode ,Interval, Description,null,Context, UDFLib.ConvertToInteger(Session["UserID"].ToString()));
            ClearData();
        }
        catch (Exception ex)
        {
        //    ltmessage.Text = ex.ToString();
        }
    }




    protected void btnClear_Click(object sender, EventArgs e)
    {

        ClearData();

    }

}