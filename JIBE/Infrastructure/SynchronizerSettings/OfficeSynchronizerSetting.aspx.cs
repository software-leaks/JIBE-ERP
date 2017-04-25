using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.INFRA;

public partial class Infrastructure_OfficeSync_OfficeSynchronizerSetting : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            BindItems();
    }
  public void BindItems()
    {
        int RecordCount=0;

        gvSetting.DataSource = BLL_Infra_SynchronizerSettings.Get_Office_Sync_Setting(null, CustomPagerSetting.CurrentPageIndex, CustomPagerSetting.PageSize, ref RecordCount);
        gvSetting.DataBind();

        CustomPagerSetting.CountTotalRec = RecordCount.ToString();
        CustomPagerSetting.BuildPager();
    }

    protected void btnSaveSetting_Click(object s, EventArgs e)
    {
        BLL_Infra_SynchronizerSettings.Upd_Office_Sync_Setting(UDFLib.ConvertToInteger(hdfSetting_ID.Value), txtKey.Text,txtValue.Text, Convert.ToInt32(Session["USERID"]));
        BindItems();
    }

    protected void btnDelete_Click(object s, EventArgs e)
    {
        BLL_Infra_SynchronizerSettings.Del_Office_Sync_Setting(int.Parse((s as ImageButton).CommandArgument), Convert.ToInt32(Session["USERID"]));
        BindItems();
    }
}