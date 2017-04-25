using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserControl_ucAsyncPager : System.Web.UI.UserControl
{
    public string CountTotalRecClientID
    {
        get { return hdfcountTotalRec.ClientID; }

    }

    public string PageSizeClientID
    {
        get { return hdfPageSize.ClientID; }

    }

    public string CurrentPageIndexClientID
    {
        get { return hdfPageIndex.ClientID; }

    }

    public string BindMethodName
    {
        set { hdfBindMethodname.Value = value; }
        get { return hdfBindMethodname.Value; }

    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ListItem item = ddlPageSize.Items.FindByValue(hdfPageSize.Value);
            if (item != null)
                item.Selected = true;

            ddlPageSize.Attributes.Add("onchange", "Asyncpager_ChangePageSize('" + ddlPageSize.ClientID + "','" + hdfPageSize.ClientID + "'," + BindMethodName + ");return false;");
            first.Attributes.Add("onclick", "Asyncpager_Navigate_Sections('" + hdfPageIndex.ClientID + "','" + hdfPageSize.ClientID + "','" + hdfcountTotalRec.ClientID + "','" + hdfCurrentPageSection.ClientID + "','first'," + BindMethodName + ");return false;");
            prev.Attributes.Add("onclick", "Asyncpager_Navigate_Sections('" + hdfPageIndex.ClientID + "','" + hdfPageSize.ClientID + "','" + hdfcountTotalRec.ClientID + "','" + hdfCurrentPageSection.ClientID + "','prev'," + BindMethodName + ");return false;");
            next.Attributes.Add("onclick", "Asyncpager_Navigate_Sections('" + hdfPageIndex.ClientID + "','" + hdfPageSize.ClientID + "','" + hdfcountTotalRec.ClientID + "','" + hdfCurrentPageSection.ClientID + "','next'," + BindMethodName + ");return false;");
            last.Attributes.Add("onclick", "Asyncpager_Navigate_Sections('" + hdfPageIndex.ClientID + "','" + hdfPageSize.ClientID + "','" + hdfcountTotalRec.ClientID + "','" + hdfCurrentPageSection.ClientID + "','last'," + BindMethodName + ");return false;");
        }
    }
}