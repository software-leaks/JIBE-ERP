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
using ClsBLLTechnical;
using   SMS.Business.PURC ;

public partial class Technical_INV_ASLIndexscreen : System.Web.UI.Page
{

    TechnicalBAL techbal = new TechnicalBAL();
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            binddropdown();
            //BindSupplierType();
            BindSupplierCategory();
            filtergrid();
            ViewState["SortDirection1"] = "Descending";
            Session["filterby"] = "";
        }
    }


    protected void BindSupplierCategory()
    {
        try
        {
            using ( BLL_PURC_Purchase  objTechService = new  BLL_PURC_Purchase ())
            {
                DataSet dtsCategory = objTechService.GetSupplierCategory();
                if (dtsCategory.Tables[0].Rows.Count > 0)
                {
                    ddlsuppliercategory.DataTextField = "Description";
                    ddlsuppliercategory.DataValueField = "Code";
                    ddlsuppliercategory.DataSource = dtsCategory;
                    ddlsuppliercategory.DataBind();
                }
                ddlsuppliercategory.Items.Insert(0, new ListItem("Select", "0"));

            }
        }
        catch (Exception ex)
        {
            //.WriteError(this.GetType().Name.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), ex);
        }
    }
    //private void BindSupplierType()
    //{
    //    try
    //    {
    //        using ( BLL_PURC_Purchase  objTechService = new  BLL_PURC_Purchase ())
    //        {
    //            DataSet dtsSupType = objTechService.GetSupplierType();
    //            if (dtsSupType.Tables[0].Rows.Count > 0)
    //            {
    //                ddlSupplierType.DataTextField = "Description";
    //                ddlSupplierType.DataValueField = "Code";
    //                ddlSupplierType.DataSource = dtsSupType;
    //                ddlSupplierType.DataBind();
    //            }
    //            ddlSupplierType.Items.Insert(0, new ListItem("Select", "0"));

    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        //.WriteError(this.GetType().Name.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), ex);
    //    }
    //}
    protected void binddropdown()
    {


        ddlSupplierCountry.DataTextField = "COUNTRY";
        ddlSupplierCountry.DataValueField = "COUNTRY";
        DataSet ds = techbal.GetsupplierSummary("COUNTRY");
        ddlSupplierCountry.DataSource = techbal.GetsupplierSummary("COUNTRY");
        ddlSupplierCountry.DataBind();
        ddlSupplierCountry.Items.Insert(0, new ListItem("-Select-", "0"));
      //  ddlSupplierHQ.Enabled = false;



        //ddlSupplierScope.DataTextField = "Description";
        //ddlSupplierScope.DataValueField = "short_code";
        //ddlSupplierScope.DataSource = techbal.GetsupplierSummary("Scope");
        //ddlSupplierScope.DataBind();
        //ddlSupplierScope.Items.Insert(0, new ListItem("-Select-", "0"));





        ddlSupplierStatus.DataTextField = "Description";
        ddlSupplierStatus.DataValueField = "short_code";
        ddlSupplierStatus.DataSource = techbal.GetsupplierSummary("status");
        ddlSupplierStatus.DataBind();
        ddlSupplierStatus.Items.Insert(0, new ListItem("-Select-", "0"));




        //ddlSupplyPort.DataTextField = "PORT_NAME";
        //ddlSupplyPort.DataValueField = "ID";
        //ddlSupplyPort.DataSource = techbal.GetsupplierSummary("port");
        //ddlSupplyPort.DataBind();
        //ddlSupplyPort.Items.Insert(0, new ListItem("-Select-", "0"));
        //ddlSupplyPort.Enabled = false;
    }
    protected void filtergrid()
    {
        DataSet ds = new DataSet();
        ds = techbal.GetsupplierSummary("all");
        string filterby = "";
        if ((txtsuppliercode.Text != "") || (ddlsuppliercategory.SelectedIndex != 0) || (ddlSupplierCountry.SelectedIndex != 0) || (ddlSupplierStatus.SelectedIndex != 0))
        {

            if ((txtsuppliercode.Text != "") || (txtsuppliercode.Text != string.Empty))
            {
                if (filterby.Length != 0)
                {
                    filterby = filterby + " and (SUPPLIER like '%" + txtsuppliercode.Text.Trim() + "%'" + ") or  (SHORT_NAME like '" + "%" + txtsuppliercode.Text.Trim() + "%')";
                }
                else
                {
                    filterby = filterby + "  (SUPPLIER like '%" + txtsuppliercode.Text.Trim() + "%'" + ") or  (SHORT_NAME like '" + "%" + txtsuppliercode.Text.Trim() + "%')";
                }
            }
            if (ddlsuppliercategory.SelectedIndex != 0)
            {
                if (filterby.Length != 0)
                {
                    filterby = filterby + " and FLDCATEGORYID = '" + ddlsuppliercategory.SelectedValue + "'";
                }
                else
                {
                    filterby = filterby + "  FLDCATEGORYID = '" + ddlsuppliercategory.SelectedValue + "'";
                }
            }
            //if (ddlSupplierType.SelectedIndex != 0)
            //{
            //    if (filterby.Length != 0)
            //    {
            //        filterby = filterby + " and SUPPLIER_TYPE = '" + ddlSupplierType.SelectedItem.Text.Trim() + "'";
            //    }
            //    else
            //    {
            //        filterby = filterby + "  SUPPLIER_TYPE = '" + ddlSupplierType.SelectedItem.Text.Trim() + "'";
            //    }
            //}
            if (ddlSupplierCountry.SelectedIndex != 0)
            {
                if (filterby.Length != 0)
                {
                    filterby = filterby + " and COUNTRY='" + ddlSupplierCountry.SelectedItem.Text.Trim() +"'";
                }
                else
                {
                    filterby = filterby + "  COUNTRY='" + ddlSupplierCountry.SelectedItem.Text.Trim() + "'";
                }
            }
            if (ddlSupplierStatus.SelectedIndex!= 0)
            {
                if (filterby.Length != 0)
                {
                    filterby = filterby + " and Status = '" + ddlSupplierStatus.SelectedItem.Text.Trim() + "'";
                }
                else
                {
                    filterby = filterby + "  Status = '" + ddlSupplierStatus.SelectedItem.Text.Trim() + "'";
                }
            }
            //if (ddlSupplierScope.SelectedIndex != 0)
            //{
            //    if (filterby.Length != 0)
            //    {
            //        filterby = filterby + " and Scope like '" + ddlSupplierScope.SelectedItem.Text.Trim() + "%" + "'";
            //    }
            //    else
            //    {
            //        filterby = filterby + " Scope like '" + ddlSupplierScope.SelectedItem.Text.Trim() + "%" + "'";
            //    }
            //}
            //if (ddlSupplyPort.SelectedIndex != 0)
            //{
            //    if (filterby.Length != 0)
            //    {
            //        filterby = filterby + " and port like '" + ddlSupplyPort.SelectedItem.Text.Trim() + "%" + "'";
            //    }
            //    else
            //    {
            //        filterby = filterby + "  port like '" + ddlSupplyPort.SelectedItem.Text.Trim() + "%" + "'";
            //    }
            //}

            DataTable dt = new DataTable();
            dt = ds.Tables[0];
            dt.DefaultView.RowFilter = filterby;
            grdsupplier.DataSource = dt.DefaultView;
            grdsupplier.DataBind();
            lblcount.Text = Convert.ToString(dt.DefaultView.Count);
            Session["filterby"] = filterby;
        }
        else
        {
            Session["filterby"] = "";
            grdsupplier.DataSource = ds;
            grdsupplier.DataBind();
            lblcount.Text = Convert.ToString(ds.Tables[0].Rows.Count);
        }

    }
    protected void ddlsuppliercategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        filtergrid();
    }
    //protected void ddlSupplierType_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    filtergrid();
    //}
    protected void ddlSupplierHQ_SelectedIndexChanged(object sender, EventArgs e)
    {
        filtergrid();
    }
    protected void ddlSupplierStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        filtergrid();
    }
   
    //protected void ddlSupplyPort_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    filtergrid();
    //}
    //protected void ddlSupplierScope_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    filtergrid();
    //}
    protected void imgsuppliercode_Click(object sender, ImageClickEventArgs e)
    {
        filtergrid();
    }
    //protected void txtsuppliercode_TextChanged(object sender, EventArgs e)
    //{
    //    filtergrid();
    //}
    protected void btnclear_Click(object sender, EventArgs e)
    {
        ddlsuppliercategory.SelectedIndex = 0;
        //ddlSupplierType.SelectedIndex = 0;
        ddlSupplierCountry.SelectedIndex = 0;
        ddlSupplierStatus.SelectedIndex = 0;
        //ddlSupplierScope.SelectedIndex = 0;
        txtsuppliercode.Text = "";
        //ddlSupplyPort.SelectedIndex = 0;
        filtergrid();
        Session["filterby"] = "";
    }
    private string ConvertSortDirectionToSql(SortDirection sortDirection)
    {
        string newSortDirection = String.Empty;
        switch (sortDirection)
        {
            case (SortDirection.Ascending):
                if (ViewState["SortDirection1"].ToString() == "Descending")
                {
                    newSortDirection = "DESC";
                    ViewState["SortDirection1"] = "Ascending";
                    break;
                }

                if (ViewState["SortDirection1"].ToString() == "Ascending")
                {
                    newSortDirection = "ASC";
                    ViewState["SortDirection1"] = "Descending";
                    break;
                }
                break;

            case SortDirection.Descending:
                newSortDirection = "ASC";

                if (ViewState["SortDirection1"].ToString() == "Descending")
                {
                    newSortDirection = "DESC";
                    ViewState["SortDirection1"] = "Ascending";
                    break;
                }

                if (ViewState["SortDirection1"].ToString() == "Ascending")
                {
                    newSortDirection = "ASC";
                    ViewState["SortDirection1"] = "Descending";
                    break;
                }
                break;
        }
        return newSortDirection;
    }
    protected void grdsupplier_Sorting(object sender, GridViewSortEventArgs e)
    {
        DataSet ds = new DataSet();
        ds = techbal.GetsupplierSummary("all");
        DataTable dataTable = new DataTable();
        dataTable = ds.Tables[0];
        if (dataTable.Rows.Count != 0)
        {
              DataView dataView = new DataView(dataTable);
            if(Session["filterby"].ToString() !="")
            {
                dataView.RowFilter=Session["filterby"].ToString();
            }
          
            dataView.Sort = e.SortExpression + " " + ConvertSortDirectionToSql(e.SortDirection);
            grdsupplier.DataSource = dataView;
            grdsupplier.DataBind();
            lblcount.Text = Convert.ToString( dataView.Count);
        }
        if (dataTable.Rows.Count == 0)
        {
            filtergrid();
        }
    }
    protected void btnAddnewsupplier_Click(object sender, EventArgs e)
    {
        Response.Redirect("LibSupplierEntry.aspx");
    }
    protected void imgdetails_Click(object sender, ImageClickEventArgs e)
    {
        
       // Response.Redirect("ViewSupplierDetails.aspx?SupplierCode="+);
    }
    protected void Details(object sender, CommandEventArgs e)
    {
        string str = e.CommandArgument.ToString();
        Session["SupplierCode"] = e.CommandArgument.ToString();
        Response.Redirect("LibSupplierEntry.aspx?SupplierCode=" + str + "&Mode=View");
    }
    protected void imgedit_Command(object sender, CommandEventArgs e)
    {
        string str = e.CommandArgument.ToString();
        Session["SupplierCode"] = e.CommandArgument.ToString();
        Response.Redirect("LibSupplierEntry.aspx?SupplierCode=" + str + "&Mode=Edit");
    }
    protected void grdsupplier_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdsupplier.PageIndex = e.NewPageIndex;
        filtergrid();
    }
    
}
