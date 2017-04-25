using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.Infrastructure;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Collections;
using System.Text;
using System.IO;
using SMS.Business.QMS;
using System.Configuration;
using SMS.Properties;



public partial class QMS_FBM_MsgConfirm : System.Web.UI.Page
{
    BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
    UserAccess objUA = new UserAccess();
    
    
    protected void Page_Load(object sender, EventArgs e)
    {

        if (IsPostBack)
        {
         

        }

         

    }
}