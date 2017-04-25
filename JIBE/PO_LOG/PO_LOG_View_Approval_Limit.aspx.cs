using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SMS.Business.Infrastructure;
using SMS.Business.POLOG;
using System.IO;
using SMS.Properties;
using EO.Pdf;
using System.Drawing;
using ClsBLLTechnical;
using SMS.Business.Crew;
using SMS.Business.Infrastructure;
using System.Text;
using System.Xml.Linq;
using Telerik.Web.UI;
using System.Web.Caching;

public partial class PO_LOG_PO_LOG_View_Approval_Limit : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtSupplyID.Text = GetSupplyID();
            BindApprovalLimitGrid();
        }
    }
    public string GetSupplyID()
    {
        try
        {
             if (Request.QueryString["SUPPLY_ID"] != null)
            {
                return Request.QueryString["SUPPLY_ID"].ToString();
            }

            else
                return "0";
        }
        catch { return "0"; }
    }
    protected void BindApprovalLimitGrid()
    {
        try
        {
            DataSet ds = BLL_POLOG_Register.POLOG_Get_ApprovalDeatils(UDFLib.ConvertIntegerToNull(txtSupplyID.Text.ToString()));
            if (ds.Tables[1].Rows.Count > 0)
            {
                lblGroup.Text = ds.Tables[0].Rows[0]["Groupname"].ToString();
            }
            if (ds.Tables[0].Rows.Count > 0)
            {
                lblverifier.Text = ds.Tables[1].Rows[0]["Verifier"].ToString();
            }
            if (ds.Tables[2].Rows.Count > 0)
            {
                gvApprovalLimit.DataSource = ds.Tables[2];
                gvApprovalLimit.DataBind();
            }
            else
            {
                gvApprovalLimit.DataSource = null;
                gvApprovalLimit.DataBind();
            }
        }
        catch { }
        {
        }
    }
}