using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SMS.Business.PURC;
using System.IO;
using Telerik.Web.UI;
using SMS.Business.Infrastructure;
using AjaxControlToolkit4;

public partial class Purchase_ReqsnAttachment : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (AjaxFileUpload1.IsInFileUploadPostBack)
        {
            
        }
        else
        {

            if (!IsPostBack)
            {
                if (Request.QueryString["REQUISITION_CODE"] != null)
                {
                    lblReqsnNumber.Text = "Requisition Number : " + Request.QueryString["REQUISITION_CODE"].ToString();

                    BindReqsnAttchment();
                }
               
            }
            lblErrorMsg.Text = "";
        }
    }

    public void BindReqsnAttchment()
    {


        BLL_PURC_Purchase objPurch = new BLL_PURC_Purchase();
        DataTable dtAttachment = objPurch.Purc_Get_Reqsn_Attachments(Request.QueryString["REQUISITION_CODE"].ToString(), Convert.ToInt32(Request.QueryString["VESSEL_ID"].ToString()));

        gvReqsnAttachment.DataSource = dtAttachment;
        gvReqsnAttachment.DataBind();





    }

    public void imgbtnDelete_Click(object s, EventArgs e)
    {
        try
        {

            BLL_PURC_Purchase objAttch = new BLL_PURC_Purchase();

            string[] arg = (((ImageButton)s).CommandArgument.Split(new char[] { ',' }));
            int res = objAttch.Purc_Delete_Reqsn_Attachments(int.Parse(arg[0]), int.Parse(arg[1]), int.Parse(arg[2]));

            BindReqsnAttchment();


        }
        catch
        { }
    }

    protected void imgbtnAssignedToSupp_Click(object s, EventArgs e)
    {

        GridViewRow item = (GridViewRow)((ImageButton)s).Parent.Parent;
        lblFineName.Text = ((ImageButton)s).CommandArgument.Trim();
        gvSupplier.DataSource = BLL_PURC_Common.Get_AssignedAttach(Request.QueryString["REQUISITION_CODE"].ToString(), ((ImageButton)s).CommandArgument.Trim());
        gvSupplier.DataBind();

        ScriptManager.RegisterStartupScript(this, typeof(Page), "SuppAttachments", "showModal('dvAssignAttachment')", true);



    }

    protected void btnSave_Click(object s, EventArgs e)
    {
        foreach (GridViewRow gr in gvSupplier.Rows)
        {
            int Isassigned = ((CheckBox)gr.FindControl("chkIsSent")).Checked == true ? 1 : 0;
            BLL_PURC_Common.Update_AssignedAttachFile(Request.QueryString["REQUISITION_CODE"].ToString(), lblFineName.Text.Trim(), ((Label)gr.FindControl("lblSuppcode")).Text, Isassigned, UDFLib.ConvertToInteger(Session["USERID"]));//, Convert.ToInt32(Request.QueryString["VESSEL_ID"])
        }

        ScriptManager.RegisterStartupScript(this, typeof(Page), "SuppAttachmentshide", "hideModal('dvAssignAttachment')", true);
        BindReqsnAttchment();
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

            int sts = objTechService.SaveAttachedFileInfo(Request.QueryString["VESSEL_ID"].ToString(), Request.QueryString["REQUISITION_CODE"].ToString(), "0", Path.GetExtension(file.FileName), UDFLib.Remove_Special_Characters(Path.GetFileName(file.FileName)), Flag_Attach, Session["USERID"].ToString(), 0);
            string FullFilename = Path.Combine(sPath, GUID.ToString() + Path.GetExtension(file.FileName));
            FileStream fileStream = new FileStream(FullFilename, FileMode.Create, FileAccess.ReadWrite);
            fileStream.Write(fileBytes, 0, fileBytes.Length);
            fileStream.Close();
            if (Session["Supplier"] != null)
            {
                int IsAssign = 0;
                char[] delimiterChars = { ' ', ',', '.', ':', '\t', '/' };
                string[] Supplier = Session["Supplier"].ToString().Split(delimiterChars);
                for (int i = 0; i < Supplier.Length; i++)
                {
                    lblFineName.Text = UDFLib.Remove_Special_Characters(Path.GetFileName(Flag_Attach));
                    if (sts >= 1)
                    {
                        /*18082015_Pranali:Wrong filename is passing through this parameter lblFineName.Text.Trim() so changes to the new method */
                        /*Added new variable IsAssign=0 by default o\it will not assign any supplier. */

                        //BLL_PURC_Common.Update_AssignedAttachFile(Request.QueryString["REQUISITION_CODE"].ToString(), lblFineName.Text.Trim(), Supplier[i], 1, UDFLib.ConvertToInteger(Session["USERID"]));//,Convert.ToInt32(Request.QueryString["VESSEL_ID"]));
                        BLL_PURC_Common.Update_AssignedAttachFile(Request.QueryString["REQUISITION_CODE"].ToString(), UDFLib.Remove_Special_Characters(Path.GetFileName(file.FileName)), Supplier[i], IsAssign, UDFLib.ConvertToInteger(Session["USERID"]));//,Convert.ToInt32(Request.QueryString["VESSEL_ID"]));
                    }
                }
            }
            // Session["Supplier"] = null;
            ScriptManager.RegisterStartupScript(this, typeof(Page), "refresh", "fn_OnClose(a,b)", true);
        }
        #region Commenteed_18082015
        //try
        //{


        //    BLL_PURC_Purchase objTechService = new BLL_PURC_Purchase();

        //    Byte[] fileBytes = file.GetContents();
        //    string sPath = Server.MapPath("\\" + System.Configuration.ConfigurationManager.AppSettings["APP_NAME"].ToString() + "\\uploads\\Purchase");
        //    Guid GUID = Guid.NewGuid();
        //    string Flag_Attach = GUID.ToString() + Path.GetExtension(file.FileName);

           
        //   int sts = objTechService.SaveAttachedFileInfo(Request.QueryString["VESSEL_ID"].ToString(), Request.QueryString["REQUISITION_CODE"].ToString(), "0", Path.GetExtension(file.FileName), UDFLib.Remove_Special_Characters(Path.GetFileName(file.FileName)), Flag_Attach, Session["USERID"].ToString(), 0);

        //    string FullFilename = Path.Combine(sPath, GUID.ToString() + Path.GetExtension(file.FileName));
        //    FileStream fileStream = new FileStream(FullFilename, FileMode.Create, FileAccess.ReadWrite);
        //    fileStream.Write(fileBytes, 0, fileBytes.Length);
        //    fileStream.Close();

        //    lblFineName.Text = UDFLib.Remove_Special_Characters(Path.GetFileName(Flag_Attach));
        //    if (sts >= 1)
        //    {
        //        if (Session["Supplier"] != null)
        //        {
        //            char[] delimiterChars = { ' ', ',', '.', ':', '\t', '/' };
        //            string[] Supplier = Session["Supplier"].ToString().Split(delimiterChars);
        //            for (int i = 0; i < Supplier.Length; i++)
        //            {
        //                BLL_PURC_Common.Update_AssignedAttachFile(Request.QueryString["REQUISITION_CODE"].ToString(), lblFineName.Text.Trim(), Supplier[i], 1, UDFLib.ConvertToInteger(Session["USERID"]));//,Convert.ToInt32(Request.QueryString["VESSEL_ID"]));
        //            }
        //        }
        //    }
        //   // Session["Supplier"] = null;


        //    ScriptManager.RegisterStartupScript(this, typeof(Page), "refresh", "fn_OnClose(a,b)", true);
        //}
        #endregion
        catch (Exception ex)
        {

        }

    }

    protected void btnLoadFiles_Click(object sender, EventArgs e)
    {

        BindReqsnAttchment();
    }

}