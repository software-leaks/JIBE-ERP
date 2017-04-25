using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Purchase_CTP_Quotation_ItemsPopup : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            uc_Purc_Ctp_AddItemDetails.SetAttribute_Refresh();
            uc_Purc_Ctp_AddItemDetails.Create_StoreinViewStateDatatables();
            uc_Purc_Ctp_AddItemDetails.Dept_ID = Convert.ToInt32(Request.QueryString["Dept_ID"]);
            uc_Purc_Ctp_AddItemDetails.Catalogue_code = Request.QueryString["Catalogue_code"];
            uc_Purc_Ctp_AddItemDetails.Catalogue_Name = Request.QueryString["Catalogue_Name"];
            uc_Purc_Ctp_AddItemDetails.DepartmentName = Request.QueryString["DepartmentName"];
            
            if(Convert.ToInt32(Request.QueryString["Quotation_ID"])>0)
            uc_Purc_Ctp_AddItemDetails.Quotation_ID = Convert.ToInt32(Request.QueryString["Quotation_ID"]);
            else
                uc_Purc_Ctp_AddItemDetails.Contract_ID = Convert.ToInt32(Request.QueryString["Contract_ID"]);

            uc_Purc_Ctp_AddItemDetails.BindData_UnSelected();
            uc_Purc_Ctp_AddItemDetails.BindData_Selected();
            uc_Purc_Ctp_AddItemDetails.BindSubCatalogue();
            uc_Purc_Ctp_AddItemDetails.SubCatalogue = "0";
            uc_Purc_Ctp_AddItemDetails.Is_Reset_Values = false;
        }
    }
   
}