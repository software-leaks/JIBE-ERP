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
using SMS.Business.TMSA;


public partial class VesselEEDI : System.Web.UI.Page
{


    BLL_Infra_VesselLib objBLL = new BLL_Infra_VesselLib();
    BLL_TMSA_EEDI objEEDIBLL = new BLL_TMSA_EEDI();
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
            Load_Vessel();

          


            BindVesselEEDI();
        }

    }

    public void Load_Vessel()
    {
       
        DataTable dt = objBLL.Get_UserVesselList_DL(0,0, Convert.ToInt32(Session["USERCOMPANYID"].ToString()),null, Convert.ToInt32(Session["USERCOMPANYID"].ToString()), GetSessionUserID());

        ddlVessel.DataSource = dt;
        ddlVessel.DataTextField = "Vessel_Name";
        ddlVessel.DataValueField = "Vessel_ID";
        ddlVessel.DataBind();
        ddlVessel.Items.Insert(0, new ListItem("-Select-", "0"));

    }

    public void BindVesselEEDI()
    {

        int rowcount = ucCustomPagerItems.isCountRecord;

        //string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        //int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());


        DataTable dt = objEEDIBLL.SearchEEDI((Convert.ToInt32(ddlVessel.SelectedIndex)==0) ? 0 : Convert.ToInt32(ddlVessel.SelectedValue), ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref  rowcount);


        if (ucCustomPagerItems.isCountRecord == 1)
        {
            ucCustomPagerItems.CountTotalRec = rowcount.ToString();
            ucCustomPagerItems.BuildPager();
        }

            gvEEDI.DataSource = dt;
            gvEEDI.DataBind();
        

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
         txtEEDI.Text = "";
        txtRemarks.Text = "";
        this.SetFocus("ctl00_MainContent_ddlVessel");
        HiddenFlag.Value = "Add";
        OperationMode = "Add Vessel EEDI";

        ddlVesselList.Enabled = true;
        DataTable dt = objBLL.Get_UserVesselList_DL(0, 0, Convert.ToInt32(Session["USERCOMPANYID"].ToString()), null, Convert.ToInt32(Session["USERCOMPANYID"].ToString()), GetSessionUserID());

        ddlVesselList.DataSource = dt;
        ddlVesselList.DataTextField = "Vessel_Name";
        ddlVesselList.DataValueField = "Vessel_ID";
        ddlVesselList.DataBind();
        ddlVesselList.Items.Insert(0, new ListItem("-Select-", "0"));
        string AddUserTypemodal = String.Format("showModal('divadd',false);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddUserTypemodal", AddUserTypemodal, true);
    }

    protected void ClearField()
    {
        ddlVessel.SelectedValue = "0";
        


    }

    protected void btnsave_Click(object sender, EventArgs e)
    {

        
        if (HiddenFlag.Value == "Add")
        {
            OperationMode = "Add Vessel EEDI";

            if(ddlVesselList.SelectedIndex>0)
            {
            if (!string.IsNullOrEmpty(txtEEDI.Text))
            {
               
                    if (check(Convert.ToInt32(ddlVesselList.SelectedValue)))
                    {
                        objEEDIBLL.INSERT_EEDI_BL(Convert.ToInt32(ddlVesselList.SelectedValue), Convert.ToDouble(txtEEDI.Text), txtRemarks.Text, Convert.ToInt32(Session["USERID"]));

                        string js = "New EEDI added successfully...!";
                        string msgdivResponseShow = string.Format("alert('" + js + "');hideModal('divadd',false);");
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgdivResponseShow", msgdivResponseShow, true);

                    }
                    else
                    {
                        string js = "alert('EEDI AllReady Exists!');";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert9", js, true);
                        string Countrymodal = String.Format("showModal('divadd',false);");
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Countrymodal", Countrymodal, true);
                    }
               
               
            }
            else
            {
                string js = "alert('EEDI is blank!');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert9", js, true);
                string EEDImodal = String.Format("showModal('divadd');");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Countrymodal", EEDImodal, true);
            }
        }
         else
                {
                    string js = "alert('Please select vessel!');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert9", js, true);
                    string Countrymodal = String.Format("showModal('divadd');");
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Countrymodal", Countrymodal, true);
                }
        }
        else
            if (HiddenFlag.Value == "Edit")
            {
                OperationMode = "Update Vessel EEDI";
                if (ddlVesselList.SelectedValue !="0")
                {
                    if (!string.IsNullOrEmpty(txtEEDI.Text))
                    {
                        objEEDIBLL.Edit_EEDI(Convert.ToInt32(ddlVesselList.SelectedValue), Convert.ToDecimal(txtEEDI.Text), txtRemarks.Text, Convert.ToInt32(Session["USERID"]));
                        string js = " EEDI updated successfully...!";
                        string msgdivResponseShow = string.Format("alert('" + js + "');hideModal('divadd',false);");
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgdivResponseShow", msgdivResponseShow, true);
                    }
                    else
                    {
                        string js = "alert('EEDI is blank!');";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert9", js, true);
                        string EEDImodal = String.Format("showModal('divadd');");
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Countrymodal", EEDImodal, true);
                    }
                }
                else
                {
                
                    string js = "alert('Please select vessel!');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert9", js, true);
                    string Countrymodal = String.Format("showModal('divadd');");
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Countrymodal", Countrymodal, true);
                }


            }
  

        //string hidemodal = String.Format("hideModal('divadd')");
        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "hidemodal", hidemodal, true);
        BindVesselEEDI();
        txtEEDI.Text = "";
        txtRemarks.Text = "";
    }

    protected void onUpdate(object source, CommandEventArgs e)
    {

        HiddenFlag.Value = "Edit";
        DataTable dtt = objBLL.Get_UserVesselList_DL(0, 0, Convert.ToInt32(Session["USERCOMPANYID"].ToString()), null, Convert.ToInt32(Session["USERCOMPANYID"].ToString()), GetSessionUserID());

        ddlVesselList.DataSource = dtt;
        ddlVesselList.DataTextField = "Vessel_Name";
        ddlVesselList.DataValueField = "Vessel_ID";
        ddlVesselList.DataBind();
        

        ddlVesselList.Enabled = false;
       
        string[] arg = e.CommandArgument.ToString().Split(',');
        string vid = UDFLib.ConvertStringToNull(arg[0]);
        string eedival = UDFLib.ConvertStringToNull(arg[1]);


        DataTable dt = new DataTable();
        dt = objEEDIBLL.Get_EEDI(Convert.ToInt32(vid));

        ddlVesselList.SelectedValue = dt.Rows[0]["VesselId"].ToString();
        txtEEDI.Text =Math.Round(Convert.ToDecimal( dt.Rows[0]["EEDI_Value"].ToString()),2).ToString();

        OperationMode = "Update Vessel EEDI";

        string AddUserTypemodal = String.Format("showModal('divadd',false);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddUserTypemodal", AddUserTypemodal, true);

    }

    protected void onDelete(object source, CommandEventArgs e)
    {

        string arg = e.CommandArgument.ToString();
        
        int retval = objEEDIBLL.Delete_EEDI(Convert.ToInt32(arg), Convert.ToInt32(Session["USERID"]));

        BindVesselEEDI();

    }

    protected void btnFilter_Click(object sender, EventArgs e)
    {
        BindVesselEEDI();
    }

    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        ddlVessel.SelectedValue = "0";
       
        BindVesselEEDI();

    }

    

    protected void gvEEDI_Sorting(object sender, GridViewSortEventArgs se)
    {
        ViewState["SORTBYCOLOUMN"] = se.SortExpression;

        if (ViewState["SORTDIRECTION"] != null && ViewState["SORTDIRECTION"].ToString() == "0")
            ViewState["SORTDIRECTION"] = 1;
        else
            ViewState["SORTDIRECTION"] = 0;

        BindVesselEEDI();
    }

    public bool check(int s)
    {
        
            DataTable dt = objEEDIBLL.Check_EEDI(s);
            if (dt.Rows.Count > 0 )
            {
                return false;
            }
            else
            {
                return true;
            }
        
    }
 
}
