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
using System.Globalization;
using SMS.Properties;

using System.IO;
using SMS.Data.Technical;

using System.Data.OleDb;
using SMS.Business.Inspection;
//using Exel = Microsoft.Office.Interop.Excel;



public partial class VesselGA : System.Web.UI.Page
{

    IFormatProvider iFormatProvider = CultureInfo.GetCultureInfo("en-GB");
    //BLL_Infra_Company objBLL = new BLL_Infra_Company();
    //BLL_Infra_Country objBLLCountry = new BLL_Infra_Country();
    //BLL_Infra_Currency objBLLCurrency = new BLL_Infra_Currency();
    SMS.Business.Infrastructure.BLL_Infra_MenuManagement objMenuBLL = new SMS.Business.Infrastructure.BLL_Infra_MenuManagement();
    UserAccess objUA = new UserAccess();


    BLL_Insp_VESSELGA objSURVBLL = new BLL_Insp_VESSELGA();
    SMS.Business.Infrastructure.BLL_Infra_VesselLib objBLLVessel = new SMS.Business.Infrastructure.BLL_Infra_VesselLib();



    public static Microsoft.Office.Interop.Excel.Application ExlApp;
    public static Microsoft.Office.Interop.Excel.Workbook ExlWrkBook;
    public static Microsoft.Office.Interop.Excel.Worksheet ExlWrkSheet;

    public string OperationMode = "";
    public Boolean uaEditFlag = false;
    public Boolean uaDeleteFlage = false;

    DropDownList DDLFleet = new DropDownList();
    DropDownList DDLVessel1 = new DropDownList();

    protected void Page_Load(object sender, EventArgs e)
    {
        UserAccessValidation();

        if (!IsPostBack)
        {
            ViewState["SORTDIRECTION"] = null;
            ViewState["SORTBYCOLOUMN"] = null;

            ucCustomPagerItems.PageSize = 15;

            // BindCompany();
            BindVesselGA();
            ViewState["OperationMode"] = "";
            //if (int.Parse(ddlFilterVesselManager.SelectedValue) == -1 || int.Parse(ddlFilterVesselManager.SelectedValue) == 0)
            //{
            //    Load_VesselList(int.Parse(Session["USERCOMPANYID"].ToString()));
            //}
            //else
            //{
            //    Load_VesselList(int.Parse(ddlFilterVesselManager.SelectedValue));
            //}
            //Load_VesselList();
            Load_VesselTypeList();
            Load_FleetList();
            // BindCountryDLL();
            //BindCurrencyDLL();

            //BindCompanyDLL();
            //BindCompanyTypeDLL();
            //BindCompanyRelationDLL();

            DDLVessel1.Items.Insert(0, new ListItem("-Select-", "0"));
        }
    }

    public void BindVesselGA()
    {
        int rowcount = ucCustomPagerItems.isCountRecord;

        int comID = UDFLib.ConvertToInteger(Session["USERCOMPANYID"]);

        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());


        DataTable dt = objSURVBLL.Get_VesselGASearch(txtfilter.Text != "" ? txtfilter.Text : null, null, UDFLib.ConvertIntegerToNull(DDLVessel.SelectedValue), UDFLib.ConvertIntegerToNull(DDLFleet.SelectedValue), sortbycoloumn, sortdirection
          , ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, Session["USERCOMPANYID"].ToString(), ref  rowcount);



        if (ucCustomPagerItems.isCountRecord == 1)
        {
            ucCustomPagerItems.CountTotalRec = rowcount.ToString();
            ucCustomPagerItems.BuildPager();
        }


        if (dt.Rows.Count > 0)
        {
            GridViewCompany.DataSource = dt;
            GridViewCompany.DataBind();
        }
        else
        {
            GridViewCompany.DataSource = dt;
            GridViewCompany.DataBind();
        }


    }



    public void Load_FleetList()
    {

        DataTable dtfleet = objBLLVessel.GetFleetList_ByID(null, UDFLib.ConvertIntegerToNull(Session["USERCOMPANYID"].ToString()));

        DDLFleet.DataSource = dtfleet;
        DDLFleet.DataTextField = "FleetName";
        DDLFleet.DataValueField = "FleetCode";
        DDLFleet.DataBind();
        DDLFleet.Items.Insert(0, new ListItem("-Select-", "0"));

    }

    public void Load_VesselTypeList()
    {

        DataTable dtvslType = objBLLVessel.Get_VesselType();

        ddlVesselType.DataSource = dtvslType;
        ddlVesselType.DataTextField = "VesselTypes";
        ddlVesselType.DataValueField = "ID";
        ddlVesselType.DataBind();
        ddlVesselType.Items.Insert(0, new ListItem("-Select-", "0"));

        DDLVessel.DataSource = dtvslType;
        DDLVessel.DataTextField = "VesselTypes";
        DDLVessel.DataValueField = "ID";
        DDLVessel.DataBind();
        DDLVessel.Items.Insert(0, new ListItem("-Select-", "0"));

    }

    public void Load_VesselList()
    {
        int Fleet_ID = 0;
        if (DDLFleet.SelectedValue != "")
            Fleet_ID = int.Parse(DDLFleet.SelectedValue);


        // //int UserCompanyID = int.Parse(ddlFilterVesselManager.SelectedValue);
        //// int UserCompanyID = int.Parse(Session["USERCOMPANYID"].ToString());
        int Vessel_Manager = int.Parse(Session["USERCOMPANYID"].ToString());
        // //int Vessel_Manager = VMID;

        // //DDLVessel.DataSource = objBLL.Get_VesselList(Fleet_ID, 0, Vessel_Manager, "", UserCompanyID);
        // DDLVessel.DataSource = objBLLVessel.Get_SURVEY_VesselList(Fleet_ID, 0, Vessel_Manager, "", 0);

        // DDLVessel.DataTextField = "VESSEL_NAME";
        // DDLVessel.DataValueField = "VESSEL_ID";
        // DDLVessel.DataBind();
        // DDLVessel.Items.Insert(0, new ListItem("-SELECT ALL-", "0"));


        DDLVessel1.DataSource = objBLLVessel.Get_SURVEY_VesselList(Fleet_ID, 0, Vessel_Manager, "", 0);

        DDLVessel1.DataTextField = "VESSEL_NAME";
        DDLVessel1.DataValueField = "VESSEL_ID";
        DDLVessel1.DataBind();
        DDLVessel1.Items.Insert(0, new ListItem("-SELECT ALL-", "0"));
        //// DDLVessel.SelectedIndex = 0;
    }
    protected void UserAccessValidation()
    {
        int CurrentUserID = GetSessionUserID();
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

        SMS.Business.Infrastructure.BLL_Infra_UserCredentials objUser = new SMS.Business.Infrastructure.BLL_Infra_UserCredentials();
        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

        if (objUA.View == 0)
            Response.Redirect("~/default.aspx?msgid=1");

        if (objUA.Add == 0) ImgAdd.Visible = false;
        if (objUA.Edit == 1)
            uaEditFlag = true;
        //else
        //   // btnsave.Visible = false;
        //if (objUA.Delete == 1) uaDeleteFlage = true;

    }

    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }



    protected void GridViewCompany_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //System.Data.DataRowView dr = (System.Data.DataRowView)e.Row.DataItem;
            //bool aVar = Convert.ToBoolean( dr["Verifiedval"].ToString());
            ////ImgNotVerify

            //ImageButton img = (ImageButton)e.Row.FindControl("ImgVerified");
            //ImageButton img1 = (ImageButton)e.Row.FindControl("ImgUnverified");
            //if (img != null && img1!=null)
            //{
            //    if (aVar == true)
            //    {   //img.ImageUrl = "~/Images/Allot-Flag-Completed.PNG";
            //        img.Visible = true;
            //        img1.Visible =false ;
            //    }
            //    else
            //    {   // img.ImageUrl = "~/Images/Allot-Flag-Active.PNG";
            //        img.Visible = false;
            //        img1.Visible =true ;
            //    }

            //}
        }

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

    protected void GridViewCompany_Sorting(object sender, GridViewSortEventArgs se)
    {

        ViewState["SORTBYCOLOUMN"] = se.SortExpression;

        if (ViewState["SORTDIRECTION"] != null && ViewState["SORTDIRECTION"].ToString() == "0")
            ViewState["SORTDIRECTION"] = 1;
        else
            ViewState["SORTDIRECTION"] = 0;

        BindVesselGA();


    }

    protected void ImgAdd_Click(object sender, EventArgs e)
    {
        HiddenFlag.Value = "Add";

        OperationMode = "Add Vessel GA";
        ViewState["OperationMode"] = "Add New Vessel GA";
        DDLVessel1.SelectedValue = "0";
        ddlVesselType.SelectedValue = "0";
        txtPathID.Text = "";
        txtPathName.Text = "";
        ddlParentID.SelectedValue = "0";

        gvPMSJobAttachment.DataSource = null;
        gvPMSJobAttachment.DataBind();
        chkIsGA.Checked = false;

        string AddCompmodal = String.Format("showModal('divadd',false);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddCompmodal", AddCompmodal, true);
    }

    protected void onUpdate(object source, CommandEventArgs e)
    {

        HiddenFlag.Value = "Edit";
        OperationMode = "Edit Vessel GA";
        ViewState["OperationMode"] = "Edit Vessel GA";
        DataTable dt = new DataTable();
        dt = objSURVBLL.Get_VesselGA();
        dt.DefaultView.RowFilter = "ID= '" + e.CommandArgument.ToString() + "'";


        DDLVessel1.SelectedValue = dt.DefaultView[0]["Object_ID"].ToString() != "" ? dt.DefaultView[0]["Object_ID"].ToString() : "0";
        ddlVesselType.SelectedValue = dt.DefaultView[0]["Vessel_TypeID"].ToString() != "" ? dt.DefaultView[0]["Vessel_TypeID"].ToString() : "0";
        bindParentID();
        txtPathID.Text = dt.DefaultView[0]["Path_ID"].ToString();
        txtPathName.Text = dt.DefaultView[0]["Path_Name"].ToString();
        ddlParentID.SelectedValue = dt.DefaultView[0]["Parent_Path"].ToString() != "" ? dt.DefaultView[0]["Parent_Path"].ToString() : "0";
        //ddlParentID.SelectedValue = "-Select-";

        // chkIsGA.Checked = Convert.ToBoolean(dt.DefaultView[0]["Is_GA"].ToString());

        if (dt.DefaultView[0]["Is_GA"].ToString() == "1")
            chkIsGA.Checked = true;
        else
            chkIsGA.Checked = false;

        gvPMSJobAttachment.DataSource = dt.DefaultView;
        gvPMSJobAttachment.DataBind();

        //string InfoDiv = "Get_Record_Information_Details('LIB_COMPANY','ID=" + txtCompanyID.Text + "')";
        //ScriptManager.RegisterStartupScript(this, this.GetType(), "InfoDiv", InfoDiv, true);

        string Companymodal = String.Format("showModal('divadd',false);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Companymodal", Companymodal, true);

    }
    //imgbtnDeleteSVG_Click

    protected void imgbtnDeleteSVG_Click(object s, CommandEventArgs e)
    {
        // this for SVG


        //Label lblImg = new Label();
        ImageButton imgSVG = new ImageButton();
        Label lblSvg = new Label();
        HyperLink hlSVG = new HyperLink();
        if (gvPMSJobAttachment.Items.Count > 0)
        {
            for (int i = 0; i < gvPMSJobAttachment.Items.Count; i++)
            {
                hlSVG = gvPMSJobAttachment.Items[i].FindControl("lblSVG") as HyperLink;
                lblSvg = gvPMSJobAttachment.Items[i].FindControl("lblATTACHMENTSVG_PATH") as Label;
                imgSVG = gvPMSJobAttachment.Items[i].FindControl("imgbtnDeleteSVG") as ImageButton;
            }
        }

        lblSvg.Text = "";
        hlSVG.Visible = false;
        lblSvg.Visible = false;
        imgSVG.Visible = false;

        //int indel = objSURVBLL.DeleteVesselGA("SVG", txtPathID.Text);

        //if (indel == 1)
        //{
        //}

        //DataTable dt = new DataTable();
        //dt = objSURVBLL.Get_VesselGA();
        //dt.DefaultView.RowFilter = "ID= '" + txtPathID.Text + "'";
        // //dt.DefaultView[0]["SVG"]="";

        //gvPMSJobAttachment.DataSource = dt.DefaultView;
        //gvPMSJobAttachment.DataBind();

        string Companymodal = String.Format("showModal('divadd',false);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Companymodal", Companymodal, true);
    }
    protected void imgbtnDeleteAssembly_Click(object s, CommandEventArgs e)
    {
        // this for IMG


        Label lblImg = new Label();
        ImageButton imgImg = new ImageButton();
        HyperLink hlIMG = new HyperLink();
        //Label lblSvg = new Label();
        if (gvPMSJobAttachment.Items.Count > 0)
        {
            for (int i = 0; i < gvPMSJobAttachment.Items.Count; i++)
            {
                lblImg = gvPMSJobAttachment.Items[i].FindControl("lblATTACHMENTIMG_PATH") as Label;
                hlIMG = gvPMSJobAttachment.Items[i].FindControl("lblIMG") as HyperLink;
                imgImg = gvPMSJobAttachment.Items[i].FindControl("imgbtnDeleteAssembly") as ImageButton;
            }
        }

        lblImg.Text = "";
        hlIMG.Visible = false;
        lblImg.Visible = false;
        imgImg.Visible = false;

        //int indel = objSURVBLL.DeleteVesselGA("IMG", txtPathID.Text);

        //if (indel == 1)
        //{
        //}

        //DataTable dt = new DataTable();
        //dt = objSURVBLL.Get_VesselGA();
        //dt.DefaultView.RowFilter = "ID= '" + txtPathID.Text + "'";
        //dt.DefaultView


        //gvPMSJobAttachment.DataSource = dt.DefaultView;
        //gvPMSJobAttachment.DataBind();

        string Companymodal = String.Format("showModal('divadd',false);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Companymodal", Companymodal, true);

    }

    protected void onVerified(object source, CommandEventArgs e)
    {

        //HiddenFlag.Value = "Verify";
        //OperationMode = "Verify Company";

        //DataTable dt = new DataTable();
        //// dt = objBLL.Get_CompanyList();
        //dt = objBLL.Get_CompanyListVerified();
        //dt.DefaultView.RowFilter = "ID= '" + e.CommandArgument.ToString() + "'";

        ////txtCompanyID.Text = dt.DefaultView[0]["ID"].ToString();

        //ddlCompanyType1.SelectedValue = dt.DefaultView[0]["Company_TypeID"].ToString() != "" ? dt.DefaultView[0]["Company_TypeID"].ToString() : "0";
        //ddlAddressCountry1.SelectedValue = dt.DefaultView[0]["Country"].ToString() != "" ? dt.DefaultView[0]["Country"].ToString() : "0";
        //ddlCurrency1.SelectedValue = dt.DefaultView[0]["Base_Curr"].ToString() != "" ? dt.DefaultView[0]["Base_Curr"].ToString() : "0";
        //ddlCountryIncorp1.SelectedValue = dt.DefaultView[0]["Country_Of_Incorp"].ToString() != "" ? dt.DefaultView[0]["Country_Of_Incorp"].ToString() : "0";


        //// if company type = Manning Agent then as per Bikash

        ////if (dt.DefaultView[0]["Company_TypeID"].ToString() == "3")
        ////{
        //    ddlParentCompany1.SelectedValue = dt.DefaultView[0]["Parent_Company_ID"].ToString() != "" ? dt.DefaultView[0]["Parent_Company_ID"].ToString() : "0";
        //    ddlRelation1.SelectedValue = dt.DefaultView[0]["Relation"].ToString() != "" ? dt.DefaultView[0]["Relation"].ToString() : "0";
        ////}

        //ddlCompanyType1.Enabled = false;
        //ddlParentCompany1.Enabled = false;
        //ddlRelation1.Enabled = false;

        ////td_Relation1.Visible = false;
        ////td_ParentCompany1.Visible = false;


        //txtCompCode1.Text = dt.DefaultView[0]["Company_Code"].ToString();
        //txtCompCode1.Enabled = false;
        //txtCompName1.Text = dt.DefaultView[0]["Company_Name"].ToString();
        //txtCompName1.Enabled = false;
        //txtShortName1.Text = dt.DefaultView[0]["Short_Name"].ToString();
        //txtShortName1.Enabled = false;
        //txtRegNo1.Text = dt.DefaultView[0]["Reg_Number"].ToString();
        //txtRegNo1.Enabled = false;
        //txtDtIncorp1.Text = dt.DefaultView[0]["Date_Of_Incorp"].ToString();
        //txtDtIncorp1.Enabled = false;
        //txtAddrerss1.Text = dt.DefaultView[0]["Address"].ToString();
        //txtAddrerss1.Enabled = false;
        //txtEmail1.Text = dt.DefaultView[0]["Email1"].ToString();
        //txtEmail1.Enabled = false;
        //txtPhone1.Text = dt.DefaultView[0]["phone1"].ToString();
        //txtPhone1.Enabled = false;
        //txtEmail21.Text = dt.DefaultView[0]["Email2"].ToString();
        //txtPhone21.Text = dt.DefaultView[0]["Phone2"].ToString();

        //txtFax11.Text = dt.DefaultView[0]["Fax1"].ToString();
        //txtFax21.Text = dt.DefaultView[0]["Fax2"].ToString();

        //chkVerify.Checked = Convert.ToBoolean(dt.DefaultView[0]["Verifiedval"].ToString());

        ////string InfoDiv = "Get_Record_Information_Details('LIB_COMPANY','ID=" + txtCompanyID.Text + "')";
        ////ScriptManager.RegisterStartupScript(this, this.GetType(), "InfoDiv", InfoDiv, true);


        //string Companymodal = String.Format("showModal('divVerify',false);");
        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Companymodal", Companymodal, true);



    }

    protected void onDelete(object source, CommandEventArgs e)
    {

        //int retval = objBLL.DeleteCompany_DL(Convert.ToInt32(e.CommandArgument.ToString()), UDFLib.ConvertToInteger(Session["UserID"]));
        //BindCompany();
    }

    protected void btnFilter_Click(object sender, EventArgs e)
    {
        BindVesselGA();
        //BindCompany();

    }

    protected void btnRefresh_Click(object sender, EventArgs e)
    {


        txtfilter.Text = "";
        DDLVessel.SelectedValue = "0";

        BindVesselGA();
        //ddlCompanyTypeFilter.SelectedValue = "0";
        //ddlCountryIncorpFilter.SelectedValue = "0";
        //ddlCurrencyFilter.SelectedValue = "0";
        //ddlCountryFilter.SelectedValue = "0";

        //BindCompany();

    }

    protected void btnsave_Click(object sender, EventArgs e)
    {

        string strLocalImagePath = FileUploaderIMG.PostedFile.FileName;
        string strLocalSVGPath = FileUploadSVG.PostedFile.FileName;
        DDLVessel1.SelectedValue = "0";

        string FileNameIMG = "";
        string FileNameSVG = "";
        int ISGAVAL = 0;
        int responseid = -1;

        if (HiddenFlag.Value == "Add")
        {
            if (FileUploaderIMG.HasFile)
            {
                if (FileUploadSVG.HasFile)
                {
                    FileNameIMG = Path.GetFileName(strLocalImagePath);
                    FileNameSVG = Path.GetFileName(strLocalSVGPath);
                }
                else
                {
                    FileNameIMG = Path.GetFileName(strLocalImagePath);
                }
            }
            else
            {
                if (FileUploadSVG.HasFile)
                {

                    FileNameSVG = Path.GetFileName(strLocalSVGPath);
                }
            }

            if (chkIsGA.Checked == true)
                ISGAVAL = 1;
            else
                ISGAVAL = 0;

            if (FileNameIMG != "" || FileNameSVG != "")
            {
                responseid = objSURVBLL.InsertVesselGA(txtPathID.Text, 0, FileNameIMG, FileNameSVG, ISGAVAL, ddlParentID.SelectedValue == "0" ? "" : ddlParentID.SelectedValue, UDFLib.ConvertIntegerToNull(ddlVesselType.SelectedValue), txtPathName.Text);//int.Parse(DDLVessel1.SelectedValue)
            }

            if (FileNameIMG != "")
            {
                FileUploaderIMG.PostedFile.SaveAs(Server.MapPath("~/Uploads/Inspection/" + FileNameIMG));
            }

            if (FileNameSVG != "")
            {
                FileUploadSVG.PostedFile.SaveAs(Server.MapPath("~/Uploads/Inspection/" + FileNameSVG));
            }


            if (responseid == -1)
            {
                string js1 = "";
                if (FileNameIMG == "" && FileNameSVG == "")
                {
                    js1 = "alert('Upload atleast one file. ');";
                }
                else
                {
                    js1 = "alert('Record Not Saved');";
                }
                OperationMode = ViewState["OperationMode"].ToString();
                string AddCompmodal = String.Format("showModal('divadd',false); ");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddCompmodal", AddCompmodal, true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertUser", js1, true);


            }
            else
            {

                string js1 = "alert('Record Saved Successfully');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertUser", js1, true);
                //string hidemodal = String.Format("hideModal('divadd')");

                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "hidemodal", hidemodal, true);
                BindVesselGA();
                // BindCompany();
                HiddenFlag.Value = "";

            }

            //BindVesselGA();

        }
        else
        {
            Label lblImg = new Label();
            Label lblSvg = new Label();
            if (gvPMSJobAttachment.Items.Count > 0)
            {
                for (int i = 0; i < gvPMSJobAttachment.Items.Count; i++)
                {
                    lblImg = gvPMSJobAttachment.Items[i].FindControl("lblATTACHMENTIMG_PATH") as Label;
                    lblSvg = gvPMSJobAttachment.Items[i].FindControl("lblATTACHMENTSVG_PATH") as Label;
                }
            }

            if (lblImg.Text != "")
            {
                FileNameIMG = lblImg.Text;
            }
            else
            {
                if (FileUploaderIMG.HasFile)
                {
                    FileNameIMG = Path.GetFileName(strLocalImagePath);
                }
            }

            if (lblSvg.Text != "")
            {
                FileNameSVG = lblSvg.Text;
            }
            else
            {
                if (FileUploadSVG.HasFile)
                {
                    FileNameSVG = Path.GetFileName(strLocalSVGPath);
                }
            }


            if (chkIsGA.Checked == true)
                ISGAVAL = 1;
            else
                ISGAVAL = 0;

            if (FileNameIMG != "" || FileNameSVG != "")
            {
                responseid = objSURVBLL.EditVesselGA(txtPathID.Text, 0, FileNameIMG, FileNameSVG, ISGAVAL, ddlParentID.SelectedValue == "0" ? "" : ddlParentID.SelectedValue, UDFLib.ConvertIntegerToNull(ddlVesselType.SelectedValue), txtPathName.Text);//int.Parse(DDLVessel1.SelectedValue)
            }


            if (lblImg.Text == "")
            {
                if (FileUploaderIMG.HasFile)
                {
                    FileUploaderIMG.PostedFile.SaveAs(Server.MapPath("~/Uploads/Inspection/" + FileNameIMG));
                }
            }

            if (lblSvg.Text == "")
            {
                if (FileUploadSVG.HasFile)
                {
                    FileUploadSVG.PostedFile.SaveAs(Server.MapPath("~/Uploads/Inspection/" + FileNameSVG));
                }
            }
            // if (FileNameIMG != "")
            // {
            //     FileUploaderIMG.PostedFile.SaveAs(Server.MapPath("~/Uploads/VesselGA/" + FileNameIMG));
            // }

            // if (FileNameSVG != "")
            // {
            //     FileUploadSVG.PostedFile.SaveAs(Server.MapPath("~/Uploads/VesselGA/" + FileNameSVG));
            // }

            if (responseid == -1)
            {
                string js1 = "";
                if (FileNameIMG == "" && FileNameSVG == "")
                {
                    js1 = "alert('Upload atleast one file. '); ";
                }
                else
                {
                    js1 = "alert('Record Not Saved');";
                }
                OperationMode = ViewState["OperationMode"].ToString();
                string AddCompmodal = String.Format("showModal('divadd',false);");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddCompmodal", AddCompmodal, true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertUser", js1, true);

                //string AddCompmodal = String.Format("showModal('divadd',false);");
                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddCompmodal", AddCompmodal, true);
            }
            else
            {
                string js1 = "alert('Record Updated Successfully');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertUser", js1, true);
                BindVesselGA();
                // BindCompany();
                HiddenFlag.Value = "";

                //string hidemodal = String.Format("hideModal('divadd')");
                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "hidemodal", hidemodal, true);
            }

        }



    }

    protected void ImgExpExcel_Click(object sender, EventArgs e)
    {


        //int rowcount = ucCustomPagerItems.isCountRecord;

        //string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        //int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());

        ////DataTable dt = objBLL.SearchCompany(txtfilter.Text != "" ? txtfilter.Text : null, UDFLib.ConvertIntegerToNull(ddlCompanyTypeFilter.SelectedValue), UDFLib.ConvertIntegerToNull(ddlCountryIncorpFilter.SelectedValue)
        ////    , UDFLib.ConvertIntegerToNull(ddlCurrencyFilter.SelectedValue), UDFLib.ConvertIntegerToNull(ddlCountryFilter.SelectedValue), sortbycoloumn, sortdirection
        ////  , null, null, ref  rowcount);


        //string[] HeaderCaptions = { "Code", "Company Type", "Name", "Short Name", "Reg No", "Date Of Incorp", "Country of Incorp", "Currency", "Email", "Phone" };
        //string[] DataColumnsName = { "Company_Code", "Company_Type", "Company_Name", "Short_Name", "Reg_Number", "Date_Of_Incorp", "COUNTRY_INCORP", "Currency_code", "Email1", "Phone1" };

        //GridViewExportUtil.ShowExcel(dt, HeaderCaptions, DataColumnsName, "Company", "Company", "");

    }


    public void btnHiddenSubmit_Click(object sender, EventArgs e)
    {
        //BindCompanyTypeDLL();
    }

    protected void btnVerify_Click(object sender, EventArgs e)
    {
        //if (HiddenFlag.Value == "Verify")
        //{
        //       int responseid = objBLL.VerifyCompany(Convert.ToInt32(txtCompanyID.Text), Convert.ToInt32(txtCompCode1.Text), Convert.ToInt32(ddlCompanyType1.SelectedValue)
        //        , txtCompName1.Text, txtShortName1.Text, txtRegNo1.Text, txtDtIncorp1.Text, UDFLib.ConvertIntegerToNull(ddlCountryIncorp1.SelectedValue), UDFLib.ConvertIntegerToNull(ddlCurrency1.SelectedValue)
        //        , txtAddrerss1.Text, UDFLib.ConvertIntegerToNull(ddlAddressCountry1.SelectedValue), txtEmail1.Text, txtPhone1.Text, txtEmail21.Text, txtPhone21.Text, txtFax11.Text, txtFax21.Text, UDFLib.ConvertToInteger(Session["UserID"]),chkVerify.Checked);

        //       string emailid = "";
        //       if (txtEmail21.Text != "")
        //       {
        //           emailid = txtEmail1.Text.Trim() + "," + txtEmail21.Text;
        //       }
        //       else
        //       {
        //           emailid = txtEmail1.Text.Trim();
        //       }

        //       if (responseid != -1)
        //       {
        //           if (chkVerify.Checked == true)
        //           {
        //               DataTable dtVerifyComp= objBLL.SendSurveyCompanyVerification(emailid, txtShortName1.Text.Trim(), txtCompName1.Text.Trim(), Convert.ToInt32(txtCompanyID.Text), UDFLib.ConvertToInteger(Session["UserID"]),"");

        //               if (dtVerifyComp.Rows.Count > 0)
        //               {
        //                   if (dtVerifyComp.Rows[0][0].ToString() == "Surveyor")
        //                   {
        //                       int iUserID = 460;
        //                       objMenuBLL.Update_User_Menu_Access(iUserID, 1, 1, 1, 1, 1, 1, 1, 1, UDFLib.ConvertToInteger(Session["UserID"]));

        //                       iUserID = 12;
        //                       objMenuBLL.Update_User_Menu_Access(iUserID, 1, 1, 1, 1, 1, 1, 1, 1, UDFLib.ConvertToInteger(Session["UserID"]));
        //                   }
        //               }


        //           }
        //       }
        //}

        // BindCompany();

        string hidemodal = String.Format("hideModal('divVerify')");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "hidemodal", hidemodal, true);
    }
    protected void DDLVessel1_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindParentID();
        OperationMode = ViewState["OperationMode"].ToString();
        string AddCompmodal = String.Format("showModal('divadd',false); ");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddCompmodal", AddCompmodal, true);
        //string AddCompmodal = String.Format("showModal('divadd',false);");
        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddCompmodal", AddCompmodal, true);
    }

    protected void ddlVesselType_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindParentID();
        OperationMode = ViewState["OperationMode"].ToString();
        string AddCompmodal = String.Format("showModal('divadd',false);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddCompmodal", AddCompmodal, true);
        //string AddCompmodal = String.Format("showModal('divadd',false);");
        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddCompmodal", AddCompmodal, true);
    }


    //[System.Web.Services.WebMethod]
    protected void bindParentID()//string vesselID
    {
        //ddlParentID.DataSource = objSURVBLL.Get_ParentID(int.Parse(DDLVessel1.SelectedValue));
        ddlParentID.DataSource = objSURVBLL.Get_ParentID_VT(int.Parse(ddlVesselType.SelectedValue));
        //ddlParentID.DataSource = objSURVBLL.Get_ParentID(int.Parse(vesselID));
        ddlParentID.DataTextField = "ParentID";
        ddlParentID.DataValueField = "ParentID";
        ddlParentID.DataBind();
        ddlParentID.Items.Insert(0, new ListItem("-SELECT ALL-", "0"));


        //Get_ParentID()
        //ddlParentID.DataSource=""
    }


    protected void btnUpload_Click(object sender, EventArgs e)
    {

        try
        {

            string _QuotationCode = "";//DDLSupplier.SelectedValue.Split(new char[] { '~' })[1];
            if (FileUpload1.HasFile)
            {
                //FileUpload1.
                string strLocalPath = FileUpload1.PostedFile.FileName;

                string FileName = Path.GetFileName(strLocalPath);
                //FileUpload1.PostedFile.SaveAs(Server.MapPath("TempUpload\\" + FileName));

                string strPath = strLocalPath;
                //string strPath = Server.MapPath("TempUpload\\" + FileName).ToString();


                //   ViewState["strPath"] = FileUpload1.PostedFile.FileName;
                ViewState["strPath"] = strPath;


                //string[] arrfn = FileName.Split('\\');
                // string strPath = Server.MapPath("SendRFQ") + "\\" + arrfn[arrfn.Length - 1];

                ExlApp = new Microsoft.Office.Interop.Excel.Application();
                ExlWrkBook = ExlApp.Workbooks.Open(strPath,
                                                          0,
                                                          true,
                                                          5,
                                                          "", "",
                                                          true,
                                                          Microsoft.Office.Interop.Excel.XlPlatform.xlWindows,
                                                          "\t",
                                                          false,
                                                          false,
                                                          0,
                                                          true,
                                                          1,
                                                          0);
                ExlWrkSheet = (Microsoft.Office.Interop.Excel.Worksheet)ExlWrkBook.ActiveSheet;

                //DataSet ds = new DataSet();
                System.Data.DataTable dt = new System.Data.DataTable("LIB_VesselGA");
                dt.Columns.Add("Path_ID", typeof(string));
                dt.Columns.Add("Object_ID", typeof(string));
                dt.Columns.Add("Image_Path", typeof(string));
                dt.Columns.Add("SVG_Path", typeof(string));
                dt.Columns.Add("Is_GA", typeof(string));
                dt.Columns.Add("Parent_Path_ID", typeof(string));
                dt.Columns.Add("Path_Name", typeof(string));
                dt.Columns.Add("Active_Status", typeof(string));                
                dt.Columns.Add("Vessel_TypeID", typeof(string));

                dt.AcceptChanges();
                DataRow dr = dt.NewRow();


                int i = 2;
                while (((Microsoft.Office.Interop.Excel.Range)ExlWrkSheet.Cells[i, 1]).Value2 != null)
                {
                    DataRow drNew = dt.NewRow();

                    drNew["Path_ID"] = Convert.ToString(((Microsoft.Office.Interop.Excel.Range)ExlWrkSheet.Cells[i, 1]).Value2);
                    drNew["Object_ID"] = Convert.ToString(((Microsoft.Office.Interop.Excel.Range)ExlWrkSheet.Cells[i, 2]).Value2);
                    drNew["Image_Path"] = Convert.ToString(((Microsoft.Office.Interop.Excel.Range)ExlWrkSheet.Cells[i, 3]).Value2);
                    drNew["SVG_Path"] = Convert.ToString(((Microsoft.Office.Interop.Excel.Range)ExlWrkSheet.Cells[i, 4]).Value2);
                    drNew["Is_GA"] = Convert.ToString(((Microsoft.Office.Interop.Excel.Range)ExlWrkSheet.Cells[i, 5]).Value2);
                    drNew["Parent_Path_ID"] = Convert.ToString(((Microsoft.Office.Interop.Excel.Range)ExlWrkSheet.Cells[i, 6]).Value2);
                    drNew["Path_Name"] = Convert.ToString(((Microsoft.Office.Interop.Excel.Range)ExlWrkSheet.Cells[i, 11]).Value2);
                    drNew["Active_Status"] = Convert.ToString(((Microsoft.Office.Interop.Excel.Range)ExlWrkSheet.Cells[i, 10]).Value2);                    
                    drNew["Vessel_TypeID"] = Convert.ToString(((Microsoft.Office.Interop.Excel.Range)ExlWrkSheet.Cells[i, 12]).Value2);

                    dt.Rows.Add(drNew);
                    i = i+1;
                }
                dt.AcceptChanges();

                if (dt.Rows.Count > 0)
                {
                    int k = objSURVBLL.InsertImport_VesselGA(dt);
                }
            }
            else
            {
                String msg = String.Format("alert('Please select file to import.');");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg, true);
                //lblErrorMsg.Text = "There is no file to upload.";
            }

        }
        catch (Exception ex)
        {
            String msg = String.Format("alert('The uploaded file do not belong to the selected supplier');");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg, true);

            //lblErrorMsg.Text = ex.ToString();
        }
        finally
        {

        }
    }
}
