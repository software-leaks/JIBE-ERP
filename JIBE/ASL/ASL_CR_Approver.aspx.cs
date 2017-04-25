using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SMS.Business.Infrastructure;
using SMS.Business.ASL;
using System.IO;
using SMS.Properties;
using System.Web.UI.HtmlControls;
public partial class ASL_ASL_CR_Approver : System.Web.UI.Page
{
    BLL_ASL_Lib objBLL = new BLL_ASL_Lib();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindApproverGrid();
        }
    }
    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }
    protected void BindApproverGrid()
    {
        DataSet ds = objBLL.Get_SupplierColumnGroupDetails(GetSessionUserID());
        gvGroupColumn.DataSource = ds.Tables[0];
        gvGroupColumn.DataBind();

    }
}