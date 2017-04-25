using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SMS.Business.Crew;


public partial class Crew_Libraries__CrewStatusLibrary : System.Web.UI.Page
{
    public string OperationMode = "";
    BLL_Crew_Admin objCrewAdmin = new BLL_Crew_Admin();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            UserAccessValidation();
            Load_Status ();
            Load_Calc_Status();            
        }
    }

    protected void UserAccessValidation()
    {
        SMS.Properties.UserAccess objUA = new SMS.Properties.UserAccess();

        int CurrentUserID = GetSessionUserID();
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

        SMS.Business.Infrastructure.BLL_Infra_UserCredentials objUser = new SMS.Business.Infrastructure.BLL_Infra_UserCredentials();
        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

        if (objUA.View == 0)
            Response.Redirect("~/default.aspx?msgid=1");

        if (objUA.Add == 0)
        {
            ImgAdd_C_Status.Visible = false;
            ImgAddStatus.Visible = false;
          
        }
        if (objUA.Edit == 0)
        {
            GridView_Status.Columns[2].Visible = false;
            GridView_Calc_Status.Columns[2].Visible = false;                    
        }
        if (objUA.Delete == 0)
        {
            GridView_Status.Columns[GridView_Status.Columns.Count - 1].Visible = false;
            GridView_Calc_Status.Columns[GridView_Calc_Status.Columns.Count - 1].Visible = false;
            
        }
        if (objUA.Approve == 0)
        {
        }

    }
    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }

    protected void Load_Status()
    {
      
        DataTable dt = objCrewAdmin.Get_CrewMainStatus_Search(txtfilter.Text);
        GridView_Status.DataSource = dt;
        GridView_Status.DataBind();
    }

    protected void GridView_Status_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        int ID = UDFLib.ConvertToInteger(GridView_Status.DataKeys[e.RowIndex].Value.ToString());
        string Name = e.NewValues["Name"].ToString();
        //string Value = e.NewValues["Value"].ToString();
       int reslt= objCrewAdmin.UPDATE_Status(ID, Name, GetSessionUserID());
        GridView_Status.EditIndex = -1;

        if (reslt == 2)
        {
            string str = String.Format("alert('Status already exists ...! ');");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "EditCertificatemodal", str, true);
        }
        else if (reslt == 3)
        {

            string str = String.Format("alert('Value already exists ..!');");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "EditCertificatemodal", str, true);
        }
        else if (reslt == 1)
        {

            string str = String.Format("alert('Status is updated Successfully ..!');");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "EditCertificatemodal", str, true);

            txtStatus.Text = "";
            txtValue.Text = "";
            txtfilterCal_Status.Text = "";
        }   
        Load_Status();
    }
    protected void GridView_Status_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            GridView_Status.EditIndex = e.NewEditIndex;
            Load_Status(); 
        }
        catch
        {
        }
    }
    protected void GridView_Status_RowCancelEdit(object sender, GridViewCancelEditEventArgs e)
    {
        try
        {
            GridView_Status.EditIndex = -1;
            Load_Status();
        }
        catch
        {
        }
    }
    protected void txtfilter_TextChanged(object sender, EventArgs e)
    {
        Load_Status();
    }

    protected void txtfilterCal_Status_TextChanged(object sender, EventArgs e)
    {
        Load_Calc_Status();
    }  
    protected void btnSaveAndClose_Click(object sender, EventArgs e)
    {
        if (txtStatus.Text!="" )
        {
            if (txtValue.Text!="")
            {
                if ( "AddStatus"==HiddenFlag.Value.ToString())
                {
                    int reslt = objCrewAdmin.Insert_Status(txtStatus.Text.Trim(), txtValue.Text.Trim(), GetSessionUserID());
                    if (reslt == 2)
                    {
                        string str = String.Format("alert('Status already exists ...! ');showModal('divadd',false);");
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "EditCertificatemodal", str, true);
                        string hidemodal = String.Format("showModal('divadd')");
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "hidemodal", hidemodal, true);
                    }
                    else if (reslt == 3)
                    {
                        string str = String.Format("alert('value already exists ...! ');showModal('divadd',false);");
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "EditCertificatemodal", str, true);
                        string hidemodal = String.Format("showModal('divadd')");
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "hidemodal", hidemodal, true);
                    }
                    else if (reslt == 1)
                    {

                        string str = String.Format("alert('Status is saved Successfully ..!');hideModal('divadd',false);");
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "EditCertificatemodal", str, true);
                        string hidemodal = String.Format("hideModal('divadd')");
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "hidemodal", hidemodal, true);
                        txtStatus.Text = "";
                        txtValue.Text = "";
                        txtfilter.Text = "";
                    }
                }
                else if (HiddenFlag.Value == "AddCalculatedStatus")
                {
                    int reslt = objCrewAdmin.Insert_Calc_Status(txtStatus.Text.Trim(), txtValue.Text.Trim(), GetSessionUserID());
                    if (reslt ==2)
                    {
                        string str = String.Format("alert('Sub Status already exists ...! ');showModal('divadd',false);");
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "EditCertificatemodal", str, true);
                    string hidemodal = String.Format("showModal('divadd')");
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "hidemodal", hidemodal, true);
                    }
                    else if (reslt == 3)
                    {
                        string str = String.Format("alert('value already exists ...! ');showModal('divadd',false);");
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "EditCertificatemodal", str, true);
                        string hidemodal = String.Format("showModal('divadd')");
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "hidemodal", hidemodal, true);
                    }
                    else if (reslt ==1)
                    {

                        string str = String.Format("alert('Sub Status is saved Successfully ..!');showModal('divadd',false);");
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "EditCertificatemodal", str, true);
                        string hidemodal = String.Format("hideModal('divadd')");
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "hidemodal", hidemodal, true);
                        txtStatus.Text = "";
                        txtValue.Text = "";
                        txtfilterCal_Status.Text = "";
                    }                      }
            }
           
         
        }
       
        Load_Status();        
        UpdatePanelcurr.Update();
        Load_Calc_Status();       
        UpdatePanel4.Update();

            //string js = "closeDiv('dvAddNewCategory');";
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", js, true);

    }

    protected void Load_Calc_Status()
    {
        DataTable dt = objCrewAdmin.Get_Calc_Status_Search(txtfilterCal_Status.Text);
        GridView_Calc_Status.DataSource = dt;
        GridView_Calc_Status.DataBind();
    }
    //protected void GridView_Calc_Status_RowDeleting(object sender, GridViewDeleteEventArgs e)
    //{
    //    int ID = UDFLib.ConvertToInteger(GridView_Calc_Status.DataKeys[e.RowIndex].Value.ToString());

    //    BLL_Crew_Evaluation.DELETE_EvaluationType(ID, GetSessionUserID());
    //    Load_Calc_Status();
    //}
    protected void GridView_Calc_Status_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        int ID = UDFLib.ConvertToInteger(GridView_Calc_Status.DataKeys[e.RowIndex].Value.ToString());  
        string Name = e.NewValues["Name"].ToString();        
        int reslt = objCrewAdmin.UPDATE_Calc_Status(ID, Name, GetSessionUserID());
        GridView_Calc_Status.EditIndex = -1;
        if (reslt == 2)
        {
            string str = String.Format("alert('Sub Status already exists ...! ');");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "EditCertificatemodal", str, true); 
        }
        else if (reslt == 3)
        {

            string str = String.Format("alert('Value already exists ..!');");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "EditCertificatemodal", str, true);
          
        } 
        else if (reslt == 1)
        {

            string str = String.Format("alert('Sub Status is updated Successfully ..!');");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "EditCertificatemodal", str, true);     
            txtStatus.Text = "";
            txtValue.Text = "";
            txtfilterCal_Status.Text = "";
        }       
        Load_Calc_Status();
    }
    protected void GridView_Calc_Status_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            GridView_Calc_Status.EditIndex = e.NewEditIndex;
            Load_Calc_Status();
        }
        catch
        {
        }
    }
    protected void GridView_Calc_Status_RowCancelEdit(object sender, GridViewCancelEditEventArgs e)
    {
        try
        {
            GridView_Calc_Status.EditIndex = -1;
            Load_Calc_Status();
        }
        catch
        {
        }
    }

    protected void btnSaveEvaType_Click(object sender, EventArgs e)
    {     
         Load_Calc_Status();
    }
    protected void btnSaveAndCloseEvaType_Click(object sender, EventArgs e)
    {

        btnSaveEvaType_Click(null, null);
        string js = "closeDiv('dvAddNewType');";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", js, true);

    }


    protected void ImgAddStatus_Click(object sender, ImageClickEventArgs e)
    {
        this.SetFocus("ctl00_MainContent_txtStatus");
        HiddenFlag.Value = "AddStatus";
        OperationMode = "Add Status";
        txtStatus.Text = "";
        txtValue.Text = "";

        string AddUserTypemodal = String.Format("showModal('divadd',false);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddUserTypemodal", AddUserTypemodal, true);
    }

  

    protected void btncancel_Click(object sender, EventArgs e)
    {

        txtStatus.Text = "";
        txtValue.Text = "";
        string AddUserTypemodal = String.Format("hideModel('divadd',false);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddUserTypemodal", AddUserTypemodal, true);
    }
    protected void ImgAdd_C_Status_Click(object sender, ImageClickEventArgs e)
    {
        this.SetFocus("ctl00_MainContent_txtStatus");
        HiddenFlag.Value = "AddCalculatedStatus";
        OperationMode = "Add Sub Status";
        txtStatus.Text = "";
        txtValue.Text = "";
        string AddUserTypemodal = String.Format("showModal('divadd',false);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddUserTypemodal", AddUserTypemodal, true);
    }

    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        txtfilterCal_Status.Text = "";
        Load_Calc_Status();
    }

    protected void ibtnRefStatus_Click(object sender, EventArgs e)
    {
        txtfilter.Text = "";
        Load_Status();
    }
}