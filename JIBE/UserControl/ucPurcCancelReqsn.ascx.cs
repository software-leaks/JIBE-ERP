using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClsBLLTechnical;
using SMS.Business.PURC;
using System.Data;

public partial class UserControl_ucPurcCancelReqsn : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            dlistPONumber.DataSource = BLL_PURC_Common.Get_PONumbers(ReqsnNumber);
            dlistPONumber.DataBind();
        }

    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try

        {
            String msgApp = "";

            if (dlistPONumber.Items.Count < 1)
            {

                DataTable dtQuotationList = new DataTable();
                dtQuotationList.Columns.Add("Qtncode");
                dtQuotationList.Columns.Add("amount");

                TechnicalBAL objPurc = new TechnicalBAL();
                BLL_PURC_Purchase objTechService = new BLL_PURC_Purchase();
                objPurc.Update_CancelReqsn(ReqsnNumber, DocCode, txtRemark.Text.Trim(), UDFLib.ConvertToInteger(Session["USERID"].ToString()));
                objTechService.InsertRequisitionStageStatus(ReqsnNumber, VesselCode, DocCode, "RCAN", " ", Convert.ToInt32(Session["USERID"]), dtQuotationList);
                BLL_PURC_Common.INS_Remarks(DocCode, Convert.ToInt32(Session["userid"].ToString()), txtRemark.Text.Trim(), 307);

                ReqsnNumber = "";
                DocCode = "0";
                VesselCode = "0";
            }
            else
            {
               msgApp =String.Format("alert('Delete operation can not be performed because this requisition has active PO.');");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msgApp, true);
            }
            //msgApp = String.Format("location.reload(true);");
             msgApp = String.Format("this.close();");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msgApp, true);

        }
        catch (Exception ex)
        {
            String msgApp = String.Format("alert('" + ex.Message + "');");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msgApp, true);
        }
    }



    public string ReqsnNumber
    {
        get { return HiddenField_ReqsnNumber.Value; }
        set { HiddenField_ReqsnNumber.Value = value; }
    }
    public string DocCode
    {
        get { return HiddenField_DocCode.Value; }
        set { HiddenField_DocCode.Value = value; }
    }

    public string VesselCode
    {
        get { return HiddenField_VesselCode.Value; }
        set { HiddenField_VesselCode.Value = value; }
    }


}