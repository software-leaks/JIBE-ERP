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
using SMS.Business.Operation;

public partial class Operation_Lib_CPDataType : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnsave_Click(object sender, EventArgs e)
    {
        try
        {
            BLL_OPS_VoyageReports.OPS_Ins_CPDtaType(txtDataType.Text.Trim(), txtDataCode.Text.Trim());
        }
        catch { }
    }
   
    

    
}
