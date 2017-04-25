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
using Telerik.Web.UI;

public partial class ASL_ASL_InvoiceStatus : System.Web.UI.Page
{
    UserAccess objUA = new UserAccess();
    public Boolean uaEditFlag = true;
    public Boolean uaDeleteFlage = true;
    protected void Page_Load(object sender, EventArgs e)
    {
        UserAccessValidation();
        if (!IsPostBack)
        {
            //Load_VesselList();
            BindGrid();
        }
    }
    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }
    protected void UserAccessValidation()
    {
        int CurrentUserID = GetSessionUserID();
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

        BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

        if (objUA.View == 0)
        {
            pnl.Visible = false;
            lblMsg.Text = "You don't have sufficient previlege to access the requested information.";
        }
        else
        {
            pnl.Visible = true;
        }

        if (objUA.Add == 0)
        {
            //ImgAdd.Visible = false;
        }
        if (objUA.Edit == 1)
            uaEditFlag = true;
        //else
        //    btnsave.Visible = false;

        if (objUA.Delete == 1) uaDeleteFlage = true;

    }
    public void Load_VesselList()
    {
        BLL_Infra_VesselLib objBLL = new BLL_Infra_VesselLib();
        DataTable dt = objBLL.Get_VesselList(0, 0, 0, "", 1);

        ddlVessel.DataSource = dt;
        ddlVessel.DataTextField = "VESSEL_NAME";
        ddlVessel.DataValueField = "VESSEL_ID";
        ddlVessel.DataBind();
        ddlVessel.Items.Insert(0, new ListItem("All Vessel", "0"));

    }
    protected void BindGrid()
    {
        string SupplierID = GetSuppID();

        DataSet ds = BLL_ASL_Supplier.Get_Supplier_General_Data_List(UDFLib.ConvertStringToNull(SupplierID));
        if (ds.Tables[0].Rows.Count > 0)
        {
            DataRow dr = ds.Tables[0].Rows[0];

            lblSuppliername.Text = dr["Register_Name"].ToString();
            if (dr["Invoice_Status_Enabled"].ToString() == "NO")
            {
                div1.Visible = true;
            }
            else
            {
                div1.Visible = false;
            }
            if (dr["Payment_History_Enabled"].ToString() == "YES")
            {
                div2.Visible = true;
            }
            else
            {
                div2.Visible = false;
            }

        }
        if (ds.Tables[1].Rows.Count > 0)
        {
            tr1.Visible = true;
            ddlVessel.DataSource = ds.Tables[1];
            ddlVessel.DataTextField = "VESSEL_NAME";
            ddlVessel.DataValueField = "VESSEL_ID";
            ddlVessel.DataBind();
            ddlVessel.Items.Insert(0, new ListItem("All Vessel", "0"));
        }
        else
        {
            ddlVessel.Items.Insert(0, new ListItem("All Vessel", "0"));
        }
        BindPendingInvoiceGrid();
        BindInvoiceStatusGrid();
    }
    public string GetSuppID()
    {
        try
        {
            if (Request.QueryString["Supp_ID"] != null)
            {
                return Request.QueryString["Supp_ID"].ToString();
            }

            else
                return "0";
        }
        catch { return "0"; }
    }
    protected void BindPendingInvoiceGrid()
    {
        string SupplierID = GetSuppID();

        DataSet ds = BLL_ASL_Supplier.Get_Supplier_PO_PendingInvoice(SupplierID, UDFLib.ConvertIntegerToNull(ddlVessel.SelectedValue), UDFLib.ConvertStringToNull(ddlVessel.SelectedItem.Text));
        if (ds.Tables[0].Rows.Count > 0)
        {
            divPendingInvoice.Visible = true;
            tdPendingInvoice.Visible = true;
            gvPOPendingInvoice.DataSource = ds;
            gvPOPendingInvoice.DataBind();
        }
        else
        {
            divPendingInvoice.Visible = false;
            tdPendingInvoice.Visible = false;
            gvPOPendingInvoice.DataSource = ds;
            gvPOPendingInvoice.DataBind();
        }
    }

    protected void btnfilter_Click(object sender, EventArgs e)
    {
        BindPendingInvoiceGrid();
        BindInvoiceStatusGrid();
    }

    protected void BindInvoiceStatusGrid()
    {
        string SupplierID = GetSuppID();

        DataSet ds = BLL_ASL_Supplier.ASL_GET_POInvoice_Status(SupplierID, UDFLib.ConvertIntegerToNull(ddlVessel.SelectedValue), UDFLib.ConvertStringToNull(ddlVessel.SelectedItem.Text));
        if (ds.Tables[0].Rows.Count > 0)
        {
            divInvoiceStatus.Visible = true;
            divStatus.Visible = true;
            gvInvoiceStatus.DataSource = ds.Tables[0];
            gvInvoiceStatus.DataBind();
        }
        else
        {
            divInvoiceStatus.Visible = false;
            divStatus.Visible = false;
            gvInvoiceStatus.DataSource = ds.Tables[0];
            gvInvoiceStatus.DataBind();
        }
        if (ds.Tables[1].Rows.Count > 0)
        {
            divStatus1.Visible = true;
            divInvoiceStatus1.Visible = true;
            gvInvoiceStatus1.DataSource = ds.Tables[1];
            gvInvoiceStatus1.DataBind();
        }
        else
        {
            divStatus1.Visible = false;
            divInvoiceStatus1.Visible = false;
            gvInvoiceStatus1.DataSource = ds.Tables[1];
            gvInvoiceStatus1.DataBind();
        }
        if (ds.Tables[2].Rows.Count > 0)
        {
            divStatus2.Visible = true;
            divInvoiceStatus2.Visible = true;
            gvInvoiceStatus2.DataSource = ds.Tables[2];
            gvInvoiceStatus2.DataBind();
        }
        else
        {
            divStatus2.Visible = false;
            divInvoiceStatus2.Visible = false;
            gvInvoiceStatus2.DataSource = ds.Tables[2];
            gvInvoiceStatus2.DataBind();
        }
        if (ds.Tables[3].Rows.Count > 0)
        {
            divStatus3.Visible = true;
            divInvoiceStatus3.Visible = true;
            gvInvoiceStatus3.DataSource = ds.Tables[3];
            gvInvoiceStatus3.DataBind();
        }
        else
        {
            divStatus3.Visible = false;
            divInvoiceStatus3.Visible = false;
            gvInvoiceStatus3.DataSource = ds.Tables[3];
            gvInvoiceStatus3.DataBind();
        }

    }





    protected void gvInvoiceStatus1_RowDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {
        if (e.Item is GridDataItem)
        {
            GridDataItem item = (GridDataItem)e.Item;
            //Label lblInvoice_Type = (Label)e.Row.FindControl("lblInvoiceReference");
            //Button ImgCreditView = (Button)e.Row.FindControl("ImgCrView");
            Button ImgInvoiceView = (Button)e.Item.FindControl("ImgInvView");

            string str = DataBinder.Eval(e.Item.DataItem, "PO_Closed_Date").ToString();
            if (str == "")
            {
                ImgInvoiceView.Enabled = true;
            }
            else
            {
                ImgInvoiceView.Enabled = false;
            }

        }
    }



    protected void gvInvoiceStatus1_ItemCommand(object source, GridCommandEventArgs e)
    {
        if (e.CommandName == "INVOICE")
        {
            string[] arg = e.CommandArgument.ToString().Split(',');
            string File_ID = UDFLib.ConvertStringToNull(arg[2]);
            string Supplier_Code = UDFLib.ConvertStringToNull(arg[0]);
            string Supply_ID = UDFLib.ConvertStringToNull(arg[1]);
            string Type = "0";
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('../ASL/ASL_Supplier_Upload.aspx?SupplierCode=" + Supplier_Code + "&Supply_ID=" + Supply_ID + "&File_ID=" + File_ID + "&Type=" + Type + "', '_blank');", true);
        }
        if (e.CommandName == "CREDITNOTE")
        {
            string[] arg = e.CommandArgument.ToString().Split(',');
            string File_ID = UDFLib.ConvertStringToNull(arg[2]);
            string Supplier_Code = UDFLib.ConvertStringToNull(arg[0]);
            string Supply_ID = UDFLib.ConvertStringToNull(arg[1]);
            string Type = "0";
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('../ASL/ASL_Supplier_CreditNote_Upload.aspx?SupplierCode=" + Supplier_Code + "&Supply_ID=" + Supply_ID + "&File_ID=" + File_ID + "&Type=" + Type + "', '_blank');", true);
        }
    }
    protected void gvInvoiceStatus2_RowDataBound(object sender, GridItemEventArgs e)
    {
        if (e.Item is GridDataItem)
        {
            GridDataItem item = (GridDataItem)e.Item;
            //Label lblInvoice_Type = (Label)e.Row.FindControl("lblInvoiceReference");
            //Button ImgCreditView = (Button)e.Row.FindControl("ImgCrView");
            Button ImgInvoiceView = (Button)e.Item.FindControl("ImgInvView");

            string str = DataBinder.Eval(e.Item.DataItem, "PO_Closed_Date").ToString();
            if (str == "")
            {
                ImgInvoiceView.Enabled = true;
            }
            else
            {
                ImgInvoiceView.Enabled = false;
            }

        }
    }
    protected void gvInvoiceStatus2_ItemCommand(object source, GridCommandEventArgs e)
    {
        if (e.CommandName == "INVOICE")
        {
            string[] arg = e.CommandArgument.ToString().Split(',');
            string File_ID = UDFLib.ConvertStringToNull(arg[2]);
            string Supplier_Code = UDFLib.ConvertStringToNull(arg[0]);
            string Supply_ID = UDFLib.ConvertStringToNull(arg[1]);
            string Type = "0";
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('../ASL/ASL_Supplier_Upload.aspx?SupplierCode=" + Supplier_Code + "&Supply_ID=" + Supply_ID + "&File_ID=" + File_ID + "&Type=" + Type + "', '_blank');", true);
        }
        if (e.CommandName == "CREDITNOTE")
        {
            string[] arg = e.CommandArgument.ToString().Split(',');
            string File_ID = UDFLib.ConvertStringToNull(arg[2]);
            string Supplier_Code = UDFLib.ConvertStringToNull(arg[0]);
            string Supply_ID = UDFLib.ConvertStringToNull(arg[1]);
            string Type = "0";
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('../ASL/ASL_Supplier_CreditNote_Upload.aspx?SupplierCode=" + Supplier_Code + "&Supply_ID=" + Supply_ID + "&File_ID=" + File_ID + "&Type=" + Type + "', '_blank');", true);
        }
    }
    protected void gvInvoiceStatus3_RowDataBound(object sender, GridItemEventArgs e)
    {
        if (e.Item is GridDataItem)
        {
            GridDataItem item = (GridDataItem)e.Item;
            //Label lblInvoice_Type = (Label)e.Row.FindControl("lblInvoiceReference");
            //Button ImgCreditView = (Button)e.Row.FindControl("ImgCrView");
            Button ImgInvoiceView = (Button)e.Item.FindControl("ImgInvView");

            string str = DataBinder.Eval(e.Item.DataItem, "PO_Closed_Date").ToString();
            if (str == "")
            {
                ImgInvoiceView.Enabled = true;
            }
            else
            {
                ImgInvoiceView.Enabled = false;
            }

        }
    }
    protected void gvInvoiceStatus3_ItemCommand(object source, GridCommandEventArgs e)
    {
        if (e.CommandName == "INVOICE")
        {
            string[] arg = e.CommandArgument.ToString().Split(',');
            string File_ID = UDFLib.ConvertStringToNull(arg[2]);
            string Supplier_Code = UDFLib.ConvertStringToNull(arg[0]);
            string Supply_ID = UDFLib.ConvertStringToNull(arg[1]);
            string Type = "0";
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('../ASL/ASL_Supplier_Upload.aspx?SupplierCode=" + Supplier_Code + "&Supply_ID=" + Supply_ID + "&File_ID=" + File_ID + "&Type=" + Type + "', '_blank');", true);
        }
        if (e.CommandName == "CREDITNOTE")
        {
            string[] arg = e.CommandArgument.ToString().Split(',');
            string File_ID = UDFLib.ConvertStringToNull(arg[2]);
            string Supplier_Code = UDFLib.ConvertStringToNull(arg[0]);
            string Supply_ID = UDFLib.ConvertStringToNull(arg[1]);
            string Type = "0";
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('../ASL/ASL_Supplier_CreditNote_Upload.aspx?SupplierCode=" + Supplier_Code + "&Supply_ID=" + Supply_ID + "&File_ID=" + File_ID + "&Type=" + Type + "', '_blank');", true);
        }
    }
    protected void gvInvoiceStatus_ItemCommand(object source, GridCommandEventArgs e)
    {
        if (e.CommandName == "INVOICE")
        {
            string[] arg = e.CommandArgument.ToString().Split(',');
            string File_ID = UDFLib.ConvertStringToNull(arg[2]);
            string Supplier_Code = UDFLib.ConvertStringToNull(arg[0]);
            string Supply_ID = UDFLib.ConvertStringToNull(arg[1]);
            string Type = "0";
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('../ASL/ASL_Supplier_Upload.aspx?SupplierCode=" + Supplier_Code + "&Supply_ID=" + Supply_ID + "&File_ID=" + File_ID + "&Type=" + Type + "', '_blank');", true);
        }
        if (e.CommandName == "CREDITNOTE")
        {
            string[] arg = e.CommandArgument.ToString().Split(',');
            string File_ID = UDFLib.ConvertStringToNull(arg[2]);
            string Supplier_Code = UDFLib.ConvertStringToNull(arg[0]);
            string Supply_ID = UDFLib.ConvertStringToNull(arg[1]);
            string Type = "0";
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('../ASL/ASL_Supplier_CreditNote_Upload.aspx?SupplierCode=" + Supplier_Code + "&Supply_ID=" + Supply_ID + "&File_ID=" + File_ID + "&Type=" + Type + "', '_blank');", true);
        }
    }
    protected void gvInvoiceStatus_RowDataBound(object sender, GridItemEventArgs e)
    {
        if (e.Item is GridDataItem)
        {
            //Label lblInvoice_Type = (Label)e.Row.FindControl("lblInvoiceReference");
            //Button ImgCreditView = (Button)e.Row.FindControl("ImgCrView");
            Button ImgInvoiceView = (Button)e.Item.FindControl("ImgInvView");

            string str = DataBinder.Eval(e.Item.DataItem, "PO_Closed_Date").ToString();
            if (str == "")
            {
                ImgInvoiceView.Enabled = true;
            }
            else
            {
                ImgInvoiceView.Enabled = false;
            }

        }
    }
    protected void gvPOPendingInvoice_ItemCommand(object source, GridCommandEventArgs e)
    {
        if (e.CommandName == "INVOICE")
        {
            string[] arg = e.CommandArgument.ToString().Split(',');

            string Supplier_Code = UDFLib.ConvertStringToNull(arg[0]);
            string Supply_ID = UDFLib.ConvertStringToNull(arg[1]);
            string File_ID = UDFLib.ConvertStringToNull(arg[2]);
            string Type = "1";
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('../ASL/ASL_Supplier_Upload.aspx?SupplierCode=" + Supplier_Code + "&Supply_ID=" + Supply_ID + "&File_ID=" + File_ID + "&Type=" + Type + "', '_blank');", true);
        }
        if (e.CommandName == "CREDITNOTE")
        {
            string[] arg = e.CommandArgument.ToString().Split(',');

            string Supplier_Code = UDFLib.ConvertStringToNull(arg[0]);
            string Supply_ID = UDFLib.ConvertStringToNull(arg[1]);
            string File_ID = UDFLib.ConvertStringToNull(arg[2]);
            string Type = "1";
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('../ASL/ASL_Supplier_CreditNote_Upload.aspx?SupplierCode=" + Supplier_Code + "&Supply_ID=" + Supply_ID + "&File_ID=" + File_ID + "&Type=" + Type + "', '_blank');", true);
        }
    }
    protected void gvPOPendingInvoice_RowDataBound(object sender, GridItemEventArgs e)
    {
        if (e.Item is GridDataItem)
        {
            Label lblInvoice_Type = (Label)e.Item.FindControl("lblInvoice_Type");
            Button ImgCreditView = (Button)e.Item.FindControl("ImgCrViewStatus");
            Button ImgInvoiceView = (Button)e.Item.FindControl("ImgInvViewStatus");
            string str = lblInvoice_Type.Text;
            if (str == "CREDIT")
            {
                //ImgCreditView.Visible = true;
            }
            else
            {
                //ImgInvoiceView.Visible = true;
            }
        }
    }
}