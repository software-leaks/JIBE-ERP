using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.INFRA;

public partial class SynchronizerSettings_Import_From_Vessels_Attachment_Rules : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            BindItems();
    }

    void BindItems()
    {
        gvAttachmentRule.DataSource = BLL_Infra_SynchronizerSettings.Get_Office_Import_Attachment_Rules();
        gvAttachmentRule.DataBind();
    }

    protected void btnSaveRule_Click(object s, EventArgs e)
    {
        BLL_Infra_SynchronizerSettings.Upd_Office_Import_Attachment_Rules(UDFLib.ConvertToInteger(hdfRule_ID.Value), txtAttachPreFix.Text, txtAttachPath.Text, Convert.ToInt32(Session["USERID"]));
        BindItems();
    }

    protected void btnDelete_Click(object s, EventArgs e)
    {
        BLL_Infra_SynchronizerSettings.Del_Office_Import_Attachment_Rules(int.Parse((s as ImageButton).CommandArgument), Convert.ToInt32(Session["USERID"]));
        BindItems();
    }
}