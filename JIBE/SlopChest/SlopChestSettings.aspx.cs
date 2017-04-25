using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.SLC;
using System.Data;

public partial class Purchase_SlopChestSettings : System.Web.UI.Page
{
    BLL_SLC_Admin objBLL = new BLL_SLC_Admin();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bindSettings();
        }
    }

    protected void bindSettings()
    {
       rbtlstSettings.DataSource= objBLL.Get_SlopchestSettings();
       rbtlstSettings.DataTextField = "SC_Key";
       rbtlstSettings.DataValueField = "SC_Key";
       rbtlstSettings.DataBind();

       DataTable dt = objBLL.Get_SlopchestSettings();
       foreach (DataRow dr in dt.Rows)
       {
           ListItem li = rbtlstSettings.Items.FindByValue(dr["SC_Key"].ToString());
           if (li != null)
           {
               if (dr["SC_Value"].ToString() == "1")
               {
                   li.Selected = true;
               }
           }
       }

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string strKey = "";
        int val=0;
        //if (rbtAllItems.Checked)
        //{
        //    strKey = "Average all items";
        //    val=1;
        //}
        //else if (rbtRmaining.Checked)
        //{
        //    strKey = "Average remaining items";
        //    val=1;
        //}
        //else
        //{
        //    string js = "alert('Option not selected.');";
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", js, true);
        //    return ;
        //}

        if (rbtlstSettings.SelectedIndex == -1)
        {
            string js = "alert('Option not selected.');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", js, true);
            return;
        }

        strKey = rbtlstSettings.SelectedValue;

        int ret = objBLL.INSERT_SlopChestSettings(strKey, val, UDFLib.ConvertIntegerToNull(Session["USERID"]));


    }
}