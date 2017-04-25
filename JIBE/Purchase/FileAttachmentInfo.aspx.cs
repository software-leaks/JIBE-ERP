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
using System.IO;
using Telerik.Web.UI;
using SMS.Business.Infrastructure;
using AjaxControlToolkit4;

public partial class Technical_INV_FileAttachmentInfo : System.Web.UI.Page
{

    DataTable dtProItemsCons = new DataTable();
    BLL_Infra_UploadFileSize objUploadFilesize = new BLL_Infra_UploadFileSize();
    protected void Page_Load(object sender, EventArgs e)
    {
       
        if (AjaxFileUpload1.IsInFileUploadPostBack)
        {

        }
        else
        {
            
            dvAssigneToSupp.Visible = false;
            if (IsPostBack)
            {
                Session["PURCATTACHEDFILES"] = Session["Attached_Files"];

                //ucPurcAttachment1.Register_JS_Attach();
            }
            if (!IsPostBack)
            {
                btnLoadFiles.Attributes.Add("style", "visibility:hidden");
                Session["PURCATTACHEDFILES"] = "";
                Session["Attached_Files"] = "";
                FillDDLSupplier();
                BindFleetDLL();
                DDLFleet.SelectedValue = Session["USERFLEETID"].ToString();
                BindVesselDDL();
                Session["AttRequistionCode"] = null;


                if (!string.IsNullOrEmpty(Request.QueryString["AttVesselCode"]))
                {
                    DDLVessel.SelectedValue = Request.QueryString["AttVesselCode"].ToString();
                    Session["VesselCode"] = Request.QueryString["AttVesselCode"].ToString();
                    Session["AttRequistionCode"] = Request.QueryString["Requisitioncode"].ToString();
                    
                }
                //
                
                BindAttchmentInfo();
            }
            lblErrorMsg.Text = "";
        }
    }

    public void BindFleetDLL()
    {
        try
        {
            BLL_Infra_VesselLib objVsl = new BLL_Infra_VesselLib();

            DataTable FleetDT = objVsl.GetFleetList(Convert.ToInt32(Session["USERCOMPANYID"].ToString()));
            DDLFleet.Items.Clear();
            DDLFleet.DataSource = FleetDT;
            DDLFleet.DataTextField = "Name";
            DDLFleet.DataValueField = "code";
            DDLFleet.DataBind();
            ListItem li = new ListItem("--SELECT ALL--", "0");
            DDLFleet.Items.Insert(0, li);
        }
        catch (Exception ex)
        {

        }
    }

    public void BindVesselDDL()
    {
        try
        {

            BLL_Infra_VesselLib objVsl = new BLL_Infra_VesselLib();

            DataTable dtVessel = objVsl.Get_VesselList(UDFLib.ConvertToInteger(DDLFleet.SelectedValue), 0, Convert.ToInt32(Session["USERCOMPANYID"].ToString()), "", Convert.ToInt32(Session["USERCOMPANYID"].ToString()));
            DDLVessel.Items.Clear();
            DDLVessel.DataSource = dtVessel;
            DDLVessel.DataTextField = "Vessel_name";
            DDLVessel.DataValueField = "Vessel_id";
            DDLVessel.DataBind();
            ListItem li = new ListItem("--SELECT ALL--", "0");
            DDLVessel.Items.Insert(0, li);
        }
        catch (Exception ex)
        {

        }
    }

    public void FillDDLSupplier()
    {
        try
        {
            using (BLL_PURC_Purchase objTechService = new BLL_PURC_Purchase())
            {

                BLL_Infra_VesselLib objVsl = new BLL_Infra_VesselLib();
                string ReqCode = "";

                //if (!string.IsNullOrEmpty(Session["AttRequistionCode"].ToString()))
                //{
                //    ReqCode = Session["AttRequistionCode"].ToString();
                //}
                DataSet SupplDs = objTechService.GetSuupplierHavingAttachment(ReqCode);
                DDLSupplier.DataSource = SupplDs.Tables[0];
                DDLSupplier.DataTextField = "SHORT_NAME";
                DDLSupplier.DataValueField = "SUPPLIER";
                DDLSupplier.DataBind();

                DataTable CatDt = objTechService.GetCategory_FileType();
                DDLCategory.DataSource = CatDt;
                DDLCategory.DataTextField = "Description";
                DDLCategory.DataValueField = "code";
                DDLCategory.DataBind();
            }
        }
        catch (Exception ex)
        {

        }
    }

    protected void DDLFleet_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            BindVesselDDL();

        }
        catch (Exception ex)
        {
        }
    }

    public void BindAttchmentInfo()
    {
        string ddcat = null;
        if (DDLCategory.SelectedIndex != 0)
            ddcat = UDFLib.ConvertStringToNull(DDLCategory.SelectedValue.ToString());

        int rowcount = ucCustomPagerItems.isCountRecord;

        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());

        BLL_PURC_Purchase objTechService = new BLL_PURC_Purchase();

        DataTable dtAttFile = new DataTable();


        if (Session["AttRequistionCode"] != null)
        {
            txtRequisition.Text = Session["AttRequistionCode"].ToString();
        }


         dtAttFile = objTechService.GetAttachedFileInfo(UDFLib.ConvertIntegerToNull(DDLFleet.SelectedValue),UDFLib.ConvertIntegerToNull(DDLVessel.SelectedValue),
                      txtRequisition.Text.Trim() != "" ? txtRequisition.Text.Trim() : null,ddcat,UDFLib.ConvertIntegerToNull(DDLSupplier.SelectedValue),
                      sortbycoloumn, sortdirection
                     , ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref  rowcount);
         if (ucCustomPagerItems.isCountRecord == 1)
         {
             ucCustomPagerItems.CountTotalRec = rowcount.ToString();
             ucCustomPagerItems.BuildPager();
         }

         if (dtAttFile.Rows.Count > 0)
         {
             rgdFileInfo.DataSource = dtAttFile;
             rgdFileInfo.DataBind();
         }
         else
         {
             rgdFileInfo.DataSource = dtAttFile;
             rgdFileInfo.DataBind();
         }


    

    }

    protected void btnRetrieve_Click(object sender, EventArgs e)
    {
         BindAttchmentInfo();
    }

    protected void DDLVessel_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["VesselCode"] = DDLVessel.SelectedValue.ToString();

    }

    protected void btnRefresh_Click(object sender, EventArgs e)
    {

        DataTable dtAttItm = new DataTable();
        DDLSupplier.SelectedIndex = 0;
        DDLFleet.SelectedIndex = 0;
        BindVesselDDL();
        DDLVessel.SelectedValue = "0";
        DDLCategory.SelectedIndex = 0;
        txtRequisition.Text = "";

        Session["AttRequistionCode"] = null;
        BindAttchmentInfo();

    }

    protected void lbtnPreview_Click(object s, EventArgs e)
    {

        string crewDocPath = ((LinkButton)s).CommandArgument;
        crewDocPath = "../Uploads/Purchase/" + System.IO.Path.GetFileName(crewDocPath);
        string js = "previewDocument('" + crewDocPath + "');";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "loadfile", js, true);

    }

    protected void lbtnSelect_Click(object s, EventArgs e)
    {
        try
        {
            //ucPurcAttachment1.FileUploadPath = Server.MapPath("../Uploads/Purchase");
            //ucPurcAttachment1.VesselID = ((LinkButton)s).CommandArgument.Split(new char[] { ',' })[1];
            //ucPurcAttachment1.UserID = Session["USERID"].ToString();
            //ucPurcAttachment1.ReqsnNumber = ((LinkButton)s).CommandArgument.Split(new char[] { ',' })[0];

        }
        catch { }
    }

    protected void rgdFileInfo_DataBound(object sender, EventArgs e)
    {
        if (Session["PURCATTACHEDFILES"] != null && Session["PURCATTACHEDFILES"].ToString() != "")
        {
            string[] AttachedList = Session["PURCATTACHEDFILES"].ToString().Split(new char[] { ',' });

            for (int i = 0; i < rgdFileInfo.Rows.Count; i++)
            {
                foreach (string item in AttachedList)
                {
                    if (item != "" && item != " ")
                    {
                        if (((ImageButton)rgdFileInfo.Rows[i].FindControl("imgbtnDelete")).CommandArgument.Split(new char[] { ',' })[0].Contains(item))
                        {

                            ((ImageButton)rgdFileInfo.Rows[i].FindControl("imgbtnDelete")).Visible = true;
                        }
                    }
                }
            }
        }

    }

    public void imgbtnDelete_Click(object s, EventArgs e)
    {
        try
        {

            BLL_PURC_Purchase objAttch = new BLL_PURC_Purchase();

            int ID = int.Parse(((ImageButton)s).CommandArgument.Split(new char[] { ',' })[0]);
            int res = objAttch.Purc_Delete_Reqsn_Attachments(ID);
            if (res > 0)
            {
                File.Delete(Server.MapPath(((ImageButton)s).CommandArgument.Split(new char[] { ',' })[1]));
                BindAttchmentInfo();
            }

        }
        catch
        { }
    }

    protected void imgbtnAssignedToSupp_Click(object s, EventArgs e)
    {
        dvAssigneToSupp.Visible = true;
        GridViewRow item = (GridViewRow)((ImageButton)s).Parent.Parent;
        //var item =(rgdFileInfo.Rows[rowIndex].Cells[3].Text);
        //
        lblReqsn.Text = ((Label)item.FindControl("lblReq")).Text;
        lblFineName.Text = ((LinkButton)item.FindControl("lbtnPreview")).Text;
        lbldate.Text = ((Label)item.FindControl("lblDate")).Text;      
        gvSupplier.DataSource = BLL_PURC_Common.Get_AssignedAttach(((Label)item.FindControl("lblReq")).Text, ((ImageButton)s).CommandArgument.Trim());
        gvSupplier.DataBind();

    }

    protected void btnSave_Click(object s, EventArgs e)
    {
        foreach (GridViewRow gr in gvSupplier.Rows)
        {
            int Isassigned = ((CheckBox)gr.FindControl("chkIsSent")).Checked == true ? 1 : 0;
            BLL_PURC_Common.Update_AssignedAttachFile(lblReqsn.Text.Trim(), lblFineName.Text.Trim(), ((Label)gr.FindControl("lblSuppcode")).Text, Isassigned, UDFLib.ConvertToInteger(Session["USERID"].ToString()));
        }
    }

    protected void rgdFileInfo_ItemDataBound(object sender, GridItemEventArgs e)
    {
        if (e.Item is GridDataItem)
        {
            GridDataItem item = (GridDataItem)e.Item;

            ((Label)item.FindControl("lblSupp")).Attributes.Add("Title", "cssbody=[dvbdy1] cssheader=[dvhdr1] header=[File Sent To :] body=[" + BindToolTipsSuppNameOnQtnSent(item["Requisition_Code"].Text.ToString(), ((LinkButton)item.FindControl("lbtnPreview")).Text) + "]");


        }
    }

    protected string BindToolTipsSuppNameOnQtnSent(string Reqsn, string filename)
    {
        DataTable dtToolTipSuppname = BLL_PURC_Common.GET_SuppName_AttachedFile(Reqsn, filename);

        string strTootips = "";

            int i = 1;
        if (dtToolTipSuppname.Rows.Count > 0)
        {


            foreach (DataRow dr in dtToolTipSuppname.Rows)
            {
                strTootips = strTootips + i + ")" + dr[0].ToString() + " <br> ";
                i += 1;
            }
        }

        return strTootips;


    }

    protected void AjaxFileUpload1_OnUploadComplete(object sender, AjaxFileUploadEventArgs file)
    {
        try
        {

            BLL_PURC_Purchase objTechService = new BLL_PURC_Purchase();

            Byte[] fileBytes = file.GetContents();
            string sPath = Server.MapPath("\\" + System.Configuration.ConfigurationManager.AppSettings["APP_NAME"].ToString() + "\\uploads\\Purchase");
            Guid GUID = Guid.NewGuid();
            string Flag_Attach = GUID.ToString() + Path.GetExtension(file.FileName);

            int sts = objTechService.SaveAttachedFileInfo(Request.QueryString["AttVesselCode"].ToString(), Session["AttRequistionCode"].ToString(), "0", Path.GetExtension(file.FileName), UDFLib.Remove_Special_Characters(file.FileName), "../Uploads/Purchase/" + Flag_Attach, Session["USERID"].ToString(), 0);

            string FullFilename = Path.Combine(sPath, GUID.ToString() + Path.GetExtension(file.FileName));
            FileStream fileStream = new FileStream(FullFilename, FileMode.Create, FileAccess.ReadWrite);
            fileStream.Write(fileBytes, 0, fileBytes.Length);
            fileStream.Close();

        }
        catch (Exception ex)
        {

        }

    }

    protected void btnLoadFiles_Click(object sender, EventArgs e)
    {
         
        BindAttchmentInfo();
    }

   
}
