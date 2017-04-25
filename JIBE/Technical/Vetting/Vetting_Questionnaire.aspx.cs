using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;
using System.Text;
using SMS.Business.Infrastructure;
using SMS.Business.PURC;
using System.Web.UI.HtmlControls;
using SMS.Business.PMS;
using SMS.Business.Crew;
using SMS.Business.VET;
using SMS.Properties;
using AjaxControlToolkit4;
using System.IO;


public partial class Technical_Vetting_Vetting_Questionnaire : System.Web.UI.Page
{
   
    public Boolean uaEditFlag = true;
    public Boolean uaDeleteFlage = true;
    int Questionnaire_ID;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (GetSessionUserID() == 0)
                Response.Redirect("~/account/login.aspx");
          
            UserAccessValidation();          

            if (!IsPostBack)
            {
                ViewState["SORTDIRECTION"] = null;
                ViewState["SORTBYCOLOUMN"] = null;
                Get_VesselType();            
                VET_Get_Module();
                VET_Get_VettingTypeList();
                Add_Status();
                BindQuestionnaire();
            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string js = "alert('" + UDFLib.GetException("SystemError/ GeneralMessage") + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", js, true);
        }
    }

    /// <summary>
    /// To check access rights of user for requested page
    /// </summary>
    protected void UserAccessValidation()
    {
        try
        {
            BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
            UserAccess objUA = new UserAccess();
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

        objUA = objUser.Get_UserAccessForPage(Convert.ToInt32(Session["USERID"]), PageURL);

        if (objUA.View == 0)
        {
            Response.Redirect("~/default.aspx?msgid=1");
        }

        if (objUA.Add == 0)
        {

        }
        if (objUA.Edit == 1)
            uaEditFlag = true;

        if (objUA.Delete == 1) uaDeleteFlage = true;
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }
    /// <summary>
    /// To bind Vessel type
    /// </summary>
    public void Get_VesselType()
    {
        try
        {
            BLL_Infra_VesselLib objBLL = new BLL_Infra_VesselLib();
            DataTable dtVesselType = objBLL.Get_VesselType();
            DDLVeseelType.DataSource = dtVesselType;
            DDLVeseelType.DataTextField = "VesselTypes";
            DDLVeseelType.DataValueField = "ID";
            DDLVeseelType.DataBind();           
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string js = "alert('" + UDFLib.GetException("SystemError/ GeneralMessage") + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", js, true);
        }
    }
       
    
    /// <summary>
    /// To display Module
    /// </summary>
    public void VET_Get_Module()
    {
        try
        {
            BLL_VET_VettingLib objBLLVetLib = new BLL_VET_VettingLib();
            DataTable dtModule = objBLLVetLib.VET_Get_Module();
            DDLModule.DataSource = dtModule;
            DDLModule.DataTextField = "Name";
            DDLModule.DataValueField = "Module_ID";
            DDLModule.DataBind();
            DDLModule.Items.Insert(0, new ListItem("-Select-", "0"));
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string js = "alert('" + UDFLib.GetException("SystemError/ GeneralMessage") + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", js, true);
        }
    }
    /// <summary>
    /// To display Vetting Type
    /// </summary>
    public void VET_Get_VettingTypeList()
    {
        try
        {
            BLL_VET_VettingLib objBLLVetLib = new BLL_VET_VettingLib();
            DataTable dtVetType = objBLLVetLib.VET_Get_VettingTypeList();
            DDLVetType.DataTextField = "Vetting_Type_Name";
            DDLVetType.DataValueField = "Vetting_Type_ID";
            DDLVetType.DataSource = dtVetType;
            DDLVetType.DataBind();
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string js = "alert('" + UDFLib.GetException("SystemError/ GeneralMessage") + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", js, true);
        }
    }
    /// <summary>
    /// Bind status to status dropdown
    /// </summary>
    public void Add_Status()
    {
        try
        {
            DataTable dt = new DataTable();          
            dt.Columns.Add("Value");

            dt.Rows.Add("Published");
            dt.Rows.Add("Draft");
            dt.Rows.Add("Archived");

            DDLStatus.DataSource = dt;
            DDLStatus.DataTextField = "Value";
            DDLStatus.DataValueField = "Value";
            DDLStatus.DataBind();
            DDLStatus.Select("Published");
            DDLStatus.Select("Draft");
           
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string js = "alert('" + UDFLib.GetException("SystemError/ GeneralMessage") + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", js, true);
        }

    }

    /// <summary>
    /// Bind all vetting Questionnaire
    /// </summary>
    public void BindQuestionnaire()
    {
        try
        {
            BLL_VET_Questionnaire objBLLQuest = new BLL_VET_Questionnaire();
            DataSet ds = new DataSet();
            int rowcount = ucCustomPagerItems.isCountRecord;           
            string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
            int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());           
            
            ds = objBLLQuest.VET_Get_Questionnaire(UDFLib.ConvertIntegerToNull(DDLModule.SelectedValue), DDLVeseelType.SelectedValues,DDLVetType.SelectedValues, DDLStatus.SelectedValues, txtNumber.Text != "" ? txtNumber.Text.Trim() : null, txtVersion.Text != "" ? txtVersion.Text.Trim() : null, txtQuestionnaire.Text != "" ? txtQuestionnaire.Text.Trim() : null, sortbycoloumn, sortdirection, ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref rowcount);

            if (ucCustomPagerItems.isCountRecord == 1)
            {
                ucCustomPagerItems.CountTotalRec = rowcount.ToString();
                ucCustomPagerItems.BuildPager();
            }

            gvQuestionnaire.DataSource = ds.Tables[0];
            gvQuestionnaire.DataBind();
            if (ds.Tables[0].Rows.Count>0)
            {                
                btnExport.Enabled = true;
            }
            else
            {
                btnExport.Enabled = false;
            }
            UpdPnlGrid.Update();
            UpdPnlFilter.Update();
        }         
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string js = "alert('" + UDFLib.GetException("SystemError/ GeneralMessage") + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", js, true);
        }
       
    }
    protected void gvQuestionnaire_Sorting(object sender, GridViewSortEventArgs se)
    {
        try
        {
            ViewState["SORTBYCOLOUMN"] = se.SortExpression;

            if (ViewState["SORTDIRECTION"] != null && ViewState["SORTDIRECTION"].ToString() == "0")
                ViewState["SORTDIRECTION"] = 1;
            else
                ViewState["SORTDIRECTION"] = 0;

            BindQuestionnaire();
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string js = "alert('" + UDFLib.GetException("SystemError/ GeneralMessage") + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", js, true);
        }
    }
        
    protected void btnClearFilter_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            DDLModule.SelectedValue = "0";
            DDLVeseelType.ClearSelection();
            DDLVetType.ClearSelection();
            DDLStatus.ClearSelection();
            Add_Status();
            txtNumber.Text = "";
            txtVersion.Text = "";
            txtQuestionnaire.Text = "";
            UpdAdvFltr.Update();
            BindQuestionnaire();

            if (hfAdv.Value == "o")
            {
                String tgladvsearchClr = String.Format("toggleOnSearchClearFilter(advText,'" + hfAdv.Value + "');");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tgladvsearchClr", tgladvsearchClr, true);
            }
            else
            {
                String tgladvsearchClr1 = String.Format("toggleOnSearchClearFilter(advText,'" + hfAdv.Value + "');");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tgladvsearchClr1", tgladvsearchClr1, true);
            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string js = "alert('" + UDFLib.GetException("SystemError/ GeneralMessage") + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", js, true);
        }
    }
    /// <summary>
    /// Export to excel
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnExport_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            BLL_VET_Questionnaire objBLLQuest = new BLL_VET_Questionnaire();
            DataSet ds = new DataSet();
            int rowcount = ucCustomPagerItems.isCountRecord;
            string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
            int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());

            ds = objBLLQuest.VET_Get_Questionnaire(UDFLib.ConvertIntegerToNull(DDLModule.SelectedValue), DDLVeseelType.SelectedValues, DDLVetType.SelectedValues, DDLStatus.SelectedValues, txtNumber.Text != "" ? txtNumber.Text.Trim() : null, txtVersion.Text != "" ? txtVersion.Text.Trim() : null, txtQuestionnaire.Text != "" ? txtQuestionnaire.Text.Trim() : null, sortbycoloumn, sortdirection, null,null, ref rowcount);

            string[] HeaderCaptions = { "Module", "Vetting Type", "Vessel Type", "Questionnaire Name", "Number", "Version", "Status" };
            string[] DataColumnsName = { "Module", "Vetting_Type_Name", "Vessel_Type", "Questionnaire_Name", "Number", "Version", "Status" };

            string FileName = "VettingQuestionnaire" + "_" + DateTime.Now.ToString("yyyy-MM-dd HH.mm.ss") ;
            string FilePath = Server.MapPath(@"~/Uploads\\Temp\\"); string ExportTempFilePath = Server.MapPath(@"~/Uploads\\ExportTemplete\\");

            if ((sender as ImageButton).CommandArgument == "ExportFrom_IE")
            {
                GridViewExportUtil.ShowExcel(ds.Tables[0], HeaderCaptions, DataColumnsName, FileName, "Vetting Questionnaire Index", "");
            }
            else
            {
                GridViewExportUtil.Export_To_Excel_Interop(ds.Tables[0], HeaderCaptions, DataColumnsName, "Vetting Questionnaire", FilePath + FileName, ExportTempFilePath, @"~\\Uploads\\Temp\\" + FileName);
                
            }

        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string js = "alert('" + UDFLib.GetException("SystemError/ GeneralMessage") + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", js, true);
        }
    }
    protected void btnRetrieve_Click(object sender, ImageClickEventArgs e)
    {
        try
        {           
            ucCustomPagerItems.CurrentPageIndex = 1;
            BindQuestionnaire();
            if (hfAdv.Value == "o")
            {
                String tgladvsearch = String.Format("toggleOnSearchClearFilter(advText,'" + hfAdv.Value + "');");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tgladvsearch", tgladvsearch, true);
            }
            else
            {
                String tgladvsearch1 = String.Format("toggleOnSearchClearFilter(advText,'" + hfAdv.Value + "');");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tgladvsearch1", tgladvsearch1, true);
            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string js = "alert('" + UDFLib.GetException("SystemError/ GeneralMessage") + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", js, true);
        }
    }
    /// <summary>
    /// To change Questionnire status to 'Archived'
    /// </summary>
    /// <param name="source"></param>
    /// <param name="e"></param>
    protected void onDelete(object source, CommandEventArgs e)
    {        
         Questionnaire_ID = Convert.ToInt32(e.CommandArgument);
        try
        {
            BLL_VET_Questionnaire objBLLQuest = new BLL_VET_Questionnaire();
           int res = objBLLQuest.VET_Del_Questionnaire(Questionnaire_ID, Convert.ToInt32(Session["USERID"]));
           if (res > 0)
           {
               BindQuestionnaire();
           }
           
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string js = "alert('" + UDFLib.GetException("SystemError/ GeneralMessage") + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", js, true);
        }
    }     
    
    protected void gvQuestionnaire_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblStatus = (Label)e.Row.FindControl("lblStatus");
                HyperLink ImgEdit = (HyperLink)e.Row.FindControl("ImgUpdate");
                ImageButton ImgAttachment = (ImageButton)e.Row.FindControl("ImgAttatch");

                if (lblStatus.Text == "Archived")
                {

                    ImgEdit.Visible = false;
                    ImgAttachment.Visible = false;

                }

                ImgEdit.NavigateUrl = "../Vetting/Vetting_QuestionnaireDetails.aspx?Questionnaire_ID=" + DataBinder.Eval(e.Row.DataItem, "Questionnaire_ID").ToString() + "&Status=" + lblStatus.Text.ToString();
            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }
}