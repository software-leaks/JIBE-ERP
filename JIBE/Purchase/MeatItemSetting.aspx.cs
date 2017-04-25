using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Properties;
using System.Web.UI.WebControls;
using SMS.Business.Infrastructure;
using SMS.Business.PURC;
using System.Data;


public partial class Purchase_MeatItemSetting : System.Web.UI.Page
{
    BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
    UserAccess objUserAcess = new UserAccess();
    protected void Page_Load(object sender, EventArgs e)
    {
        UserAccessValidation();

        if (!IsPostBack)
        {   
            BLL_PURC_Purchase objBLLPurc = new BLL_PURC_Purchase();
            DataTable ds = objBLLPurc.GetMeatItemSetting();
            for (int i = 0; i < ds.Rows.Count; i++)
            {
                if (ds.Rows[i]["Setting_Key"].ToString() == "MEAT_ITEM_ALLOWANCE")
                    txtMeatAllow.Text = ds.Rows[i]["Setting_Value"].ToString();
                else if (ds.Rows[i]["Setting_Key"].ToString() == "MEAT_ITEM_LIMIT")
                    txtMeatLimit.Text = ds.Rows[i]["Setting_Value"].ToString();
            }

        }
        lblError.Text = string.Empty;
    }
    protected void UserAccessValidation()
    {

        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

        objUserAcess = objUser.Get_UserAccessForPage(Convert.ToInt32(Session["USERID"]), PageURL);

        if (objUserAcess.View == 0)
        {

            Response.Redirect("~/crew/default.aspx?msgid=1");
        }

        if (objUserAcess.Add == 0)
        {
            //catalogue
            btnSave.Enabled = false;

        }

    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        BLL_PURC_Purchase objBLLPurc = new BLL_PURC_Purchase();
        objBLLPurc.UpdateMeatItemSetting(txtMeatAllow.Text, txtMeatLimit.Text, Convert.ToInt32(Session["USERID"]));
        lblError.Text = "Record Saved Successfully..";
    }
}