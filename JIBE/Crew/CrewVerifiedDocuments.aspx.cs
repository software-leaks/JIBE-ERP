using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.Crew;
using SMS.Business.Infrastructure;
using SMS.Business.DMS;
using System.Data;
using System.IO;



public partial class Crew_CrewVerifiedDocuments : System.Web.UI.Page
{
    BLL_Crew_CrewDetails objCrew = new BLL_Crew_CrewDetails();
    BLL_Infra_UploadFileSize objUploadFilesize = new BLL_Infra_UploadFileSize();
    BLL_DMS_Admin objDMS = new BLL_DMS_Admin();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (getQueryString("CrewID") == null)
            Response.Redirect("CrewList.aspx");

        UserAccessValidation();

        if (!IsPostBack)
        {
            try
            {
                int CrewID = Convert.ToInt32(getQueryString("CrewID"));
                BindData();

                DataTable dt = objCrew.Get_CrewPersonalDetailsByID(CrewID);
                if (dt.Rows.Count > 0)
                {
                    lblCrewName.Text = dt.Rows[0]["staff_fullname"].ToString();
                    lblCurrentRank.Text = dt.Rows[0]["CurrentRank"].ToString();
                    lblStaffCode.Text = dt.Rows[0]["Staff_Code"].ToString();
                    lblNationality.Text = dt.Rows[0]["Country_Name"].ToString();
                    lblVessel.Text = dt.Rows[0]["Vessel_Short_Name"].ToString();
                }
            }
            catch
            {
            }
        }
    }
    protected void UserAccessValidation()
    {
        int CurrentUserID = GetSessionUserID();
        SMS.Properties.UserAccess objUA = new SMS.Properties.UserAccess();
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

        SMS.Business.Infrastructure.BLL_Infra_UserCredentials objUser = new SMS.Business.Infrastructure.BLL_Infra_UserCredentials();
        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

        if (objUA.View == 0)
            Response.Redirect("~/default.aspx?msgid=1");

        if (objUA.Add == 0)
        {
            GridView1.Columns[GridView1.Columns.Count - 1].Visible = false;
        }
        if (objUA.Edit == 0)
        {
        }
        if (objUA.Delete == 0)
        {
        }

        if (objUA.Approve == 0)
        {
        }
    }


    private string getQueryString(string QueryField)
    {
        try
        {
            if (Request.QueryString[QueryField] != null && Request.QueryString[QueryField].ToString() != "")
            {
                return Request.QueryString[QueryField].ToString();
            }
            else
                return "";
        }
        catch
        {
            return "";
        }
    }

    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }

    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            GridView1.EditIndex = e.NewEditIndex;
            BindData();

            DropDownList ddlYesNo = ((DropDownList)(GridView1.Rows[e.NewEditIndex].FindControl("ddlYN")));
            if (ddlYesNo != null)
            {
                ddlYesNo.SelectedValue = "1";
            }
        }
        catch
        {
        }


    }

    protected void GridView1_RowCancelEdit(object sender, GridViewCancelEditEventArgs e)
    {
        try
        {
            GridView1.EditIndex = -1;
            BindData();

        }
        catch
        {
        }


    }  

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string DocTypeID = DataBinder.Eval(e.Row.DataItem, "DocTypeID").ToString();
            string DocFileName = DataBinder.Eval(e.Row.DataItem, "DocFileName").ToString();

            HyperLink img = (HyperLink)e.Row.FindControl("ImgAttachment");
            if (img != null)
            {
                if (DocFileName != "")
                {
                    if (File.Exists(Server.MapPath("~/Uploads/CrewDocuments/") + DocFileName))
                    {
                        img.NavigateUrl = "~/Uploads/CrewDocuments/" + DocFileName;
                    }
                    else
                    {
                        img.NavigateUrl = "~/FileNotFound.aspx";

                    }
                    img.Target = "_blank";
                }
                else
                    img.Visible = false;
            }

            if (DocTypeID == "10")
            {
                int iCrewID = UDFLib.ConvertToInteger(getQueryString("CrewID"));
                int iVoyID = UDFLib.ConvertToInteger(getQueryString("VoyID"));

                System.Data.DataTable dt = objCrew.Get_CrewAgreementStatus(iVoyID, GetSessionUserID());

                DataRow[] dr = dt.Select("StepText like '%Contract Signed by Office%'");
                if (dr.Length == 0)
                {
                    ImageButton LinkButton2 = (ImageButton)e.Row.FindControl("LinkButton2");
                    if (LinkButton2 != null)
                    {
                        LinkButton2.Visible = false;
                        Image imgInfo = (Image)e.Row.FindControl("imgInfo");
                        if (imgInfo != null)
                        {
                            imgInfo.Visible = true;
                            imgInfo.Attributes.Add("Title", "cssbody=[dvbdy1] cssheader=[dvhdr1] header=[Contract not signed by office] body=[Contract is not yet signed by office. Please wait for the office to sign the contract.]");
                        }
                    }
                }
            }
        }
    }

    protected void BindData()
    {
        int CrewID = int.Parse(getQueryString("CrewID"));
        int VoyageID = int.Parse(getQueryString("VoyID"));

        DataTable dtDocs = objCrew.Get_Crew_VoyageVerifiedDocuments(CrewID, VoyageID);

        GridView1.DataSource = dtDocs;
        GridView1.DataBind();
    }
}