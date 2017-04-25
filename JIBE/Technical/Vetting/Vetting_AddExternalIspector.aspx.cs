using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Properties;
using SMS.Business.Infrastructure;
using System.Data;
using SMS.Business.VET;
using System.IO;

public partial class Technical_Vetting_Vetting_AddExternalIspector : System.Web.UI.Page
{
    public int Result = 0;
    BLL_VET_VettingLib objBLL = new BLL_VET_VettingLib();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                BindVettingType();
                if (!string.IsNullOrWhiteSpace(Convert.ToString(Request.QueryString["Mode"])))
                {
                    string Mode = Request.QueryString["Mode"].ToString();
                    ViewState["Mode"] = Mode;
                    if (ViewState["Mode"].ToString() == "Add")
                    {
                        ViewState["InspectorID"] = "";
                        hdnInspectorId.Value = "";
                        UplImage.Enabled = false;                      
                        imgInspector.Attributes.Add("style", "display:none");
                    }
                }
                if (!string.IsNullOrWhiteSpace(Convert.ToString(Request.QueryString["Page"])))
                {
                    string Page = Request.QueryString["Page"].ToString();
                    ViewState["Page"] = Page;
                }
                if (!string.IsNullOrWhiteSpace(Convert.ToString(Request.QueryString["InspectorID"])))
                {
                    int InspectorID = UDFLib.ConvertToInteger(Request.QueryString["InspectorID"].ToString());
                    ViewState["InspectorID"] = InspectorID;
                    FillInspectoronEdit(InspectorID);
                    UplImage.Enabled = true;
                    imgInspector.Attributes.Add("style", "display:inline;max-width:80px;");
                }

            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            string FullName = txtFirstName.Text.Trim() + " " + txtLastName.Text.Trim();
            string Path = "";

            if (DDLVetType.SelectedValues.Rows.Count > 0)
            {

                if (hdnInspectorId.Value != "")
                {
                    if (UplImage.HasFile == true)
                    {
                        string extnsion = System.IO.Path.GetExtension(UplImage.FileName);
                        if (extnsion == ".jpg" || extnsion == ".jpeg" || extnsion == ".gif" || extnsion == ".png")
                        {
                            FileInfo fn = new FileInfo(UplImage.PostedFile.FileName);
                            Guid gid = Guid.NewGuid();
                            string filename = "EXI_" + gid + fn.Extension;
                            Path = "~/Uploads/Vetting/VETExeInsp/" + filename;
                            UplImage.SaveAs(Server.MapPath("~/Uploads/Vetting/VETExeInsp/" + filename));

                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AlreadyExists", "alert('Invalid File');showModal('dvAddExternalInspector', false);", true);
                            return;
                        }
                    }

                    objBLL.VET_UPD_ExternalInspector(UDFLib.ConvertToInteger(hdnInspectorId.Value), txtFirstName.Text.Trim(), txtLastName.Text.Trim(), txtCompany.Text.Trim(), txtDocumentType.Text.Trim(), txtDocumentNumber.Text.Trim(),
                        DDLVetType.SelectedValues, Path, GetSessionUserID(), ref Result);

                    if (Result < 0)
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AlreadyExists", "alert('" + UDFLib.GetException("InformationMessage/DataExists") + "');showModal('dvAddExternalInspector', false);", true);
                    else if (Result > 0)
                    {
                        FillInspectoronEdit(UDFLib.ConvertToInteger(hdnInspectorId.Value));
                        if (ViewState["Page"] != null)
                        {
                            string js22 = "parent.UpdatePage();";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "UpdatePage", js22, true);
                        }
                        else
                        {
                            string js22 = "parent.SetNewInspector('" + hdnInspectorId.Value + "');";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "SetNewInspector", js22, true);
                        }


                    }

                }
                else
                {
                    objBLL.VET_INS_ExternalInspector(txtFirstName.Text.Trim(), txtLastName.Text.Trim(), txtCompany.Text.Trim(), txtDocumentType.Text.Trim(), txtDocumentNumber.Text.Trim(), DDLVetType.SelectedValues, GetSessionUserID(), ref Result);

                    if (Result < 0)
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AlreadyExists", "alert('" + UDFLib.GetException("InformationMessage/DataExists") + "');showModal('dvAddExternalInspector', false);", true);
                    else if (Result > 0)
                    {

                        hdnInspectorId.Value = Result.ToString();
                        UplImage.Enabled = true;                   
                        if (ViewState["Page"] != null)
                        {
                            string js22 = "parent.UpdatePage();";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "UpdatePage", js22, true);
                        }
                        else
                        {
                            string js22 = "parent.SetNewInspector('" + hdnInspectorId.Value + "');";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "SetNewInspectorDLL", js22, true);
                        }

                      

                    }

                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error", "alert('Please select at least one vetting type.');", true);
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
    /// Method is used to get login user id
    /// </summary>
    /// <returns>retrun user id</returns>
    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }
    /// <summary>
    /// Method is used to bind vetting type dropdown
    /// </summary>
    private void BindVettingType()
    {
        try
        {
            DataTable dtVtTypes = new DataTable();
            dtVtTypes = objBLL.VET_Get_VettingTypeList_Insp();
            if (dtVtTypes.Rows.Count > 0)
            {              
                DataTable dtVetType = objBLL.VET_Get_VettingTypeList_Insp();
                if (dtVetType!=null)
                {
                    if (dtVetType.Rows.Count>0)
                    {                 
             
                        dtVetType.DefaultView.RowFilter = "IsInternal=0";
                        DDLVetType.DataSource = dtVetType.DefaultView.ToTable();
                        DDLVetType.DataTextField = "Vetting_Type_Name";
                        DDLVetType.DataValueField = "Vetting_Type_ID";
                        DDLVetType.DataBind();
                    }
                }
                
            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }
    /// <summary>
    /// Method is used to bind inspector details
    /// </summary>
    private void FillInspectoronEdit(int InspectorId)
    {
        try
        {
            DataSet dtInsp = new DataSet();
            dtInsp = objBLL.VET_Get_ExternalInspectorbyID(InspectorId);
            if (dtInsp.Tables.Count > 0)
            {

                txtFirstName.Text = dtInsp.Tables[0].Rows[0][0].ToString();
                txtLastName.Text = dtInsp.Tables[0].Rows[0][1].ToString();
                txtCompany.Text = dtInsp.Tables[0].Rows[0][2].ToString();
                txtDocumentType.Text = dtInsp.Tables[0].Rows[0][3].ToString();
                txtDocumentNumber.Text = dtInsp.Tables[0].Rows[0][4].ToString();
                imgInspector.Attributes.Add("style", "display:inline;max-width:80px;");
                imgInspector.ImageUrl = dtInsp.Tables[0].Rows[0][5].ToString();
                hdnInspectorId.Value = InspectorId.ToString();
                if (dtInsp.Tables[1].Rows.Count > 0)
                {
                    for (int i = 0; i < dtInsp.Tables[1].Rows.Count; i++)
                    {
                        DDLVetType.Select(dtInsp.Tables[1].Rows[i][0].ToString());
                    }
                }

            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }

    }    

    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> VET_Get_AutoComplete_ExtInspectorFNList(string prefixText, int count)
    {
        SMS.Business.VET.BLL_VET_VettingLib objBl = new SMS.Business.VET.BLL_VET_VettingLib();
        DataTable dt;
        List<string> RetVal = new List<string>();

        try
        {
            dt = objBl.VET_Get_AutoComplete_ExtInspectorFNList(prefixText).Tables[0];
            dt.Rows.Cast<System.Data.DataRow>().Take(count);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                RetVal.Add(dt.Rows[i]["First_Name"].ToString());
            }

            return RetVal;
        }
        catch { throw; }
        finally { objBl = null; }

    }
    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> VET_Get_AutoComplete_ExtInspectorLNList(string prefixText, int count)
    {
        SMS.Business.VET.BLL_VET_VettingLib objBl = new SMS.Business.VET.BLL_VET_VettingLib();
        DataTable dt;
        List<string> RetVal = new List<string>();

        try
        {
            dt = objBl.VET_Get_AutoComplete_ExtInspectorLNList(prefixText).Tables[0];
            dt.Rows.Cast<System.Data.DataRow>().Take(count);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                RetVal.Add(dt.Rows[i]["Last_Name"].ToString());
            }

            return RetVal;
        }
        catch { throw; }
        finally { objBl = null; }

    }

}