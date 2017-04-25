using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.INFRA;

public partial class Infrastructure_SynchronizerSettings_Sync_Vessel_Attachment_Rules : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            BindItems();
    }

    void BindItems()
    {
        gvAttachmentRule.DataSource = BLL_Infra_SynchronizerSettings.Get_Vessel_Import_Attachment_Rules();
        gvAttachmentRule.DataBind();
    }

    protected void btnSaveRule_Click(object s, EventArgs e)
    {
        BLL_Infra_SynchronizerSettings.Upd_Vessel_Import_Attachment_Rules(UDFLib.ConvertToInteger(hdfRule_ID.Value), txtAttachPreFix.Text, txtAttachPath.Text, Convert.ToInt32(Session["USERID"]));
        BindItems();
    }

    protected void btnDelete_Click(object s, EventArgs e)
    {
        BLL_Infra_SynchronizerSettings.Del_Vessel_Import_Attachment_Rules(int.Parse((s as ImageButton).CommandArgument), Convert.ToInt32(Session["USERID"]));
        BindItems();
    }
}