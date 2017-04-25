using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.PURC;
using System.Data;

public partial class Purchase_CTP_New_Contract : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            mlvCTP.ActiveViewIndex = 0;
            uc_Purc_Ctp_Add_Item.Is_Reset_Values = true;
           
            uc_Purc_Ctp_Send_RFQSupp.Is_Reset_Values = true;
            bindDeptCatalogue();
        }
    }

    protected void btnNext_Prev_Click(object s, CommandEventArgs e)
    {
        int currentIndex = mlvCTP.ActiveViewIndex;
        mlvCTP.ActiveViewIndex = Convert.ToInt32(e.CommandName);

        if (e.CommandName == "1")
        {
            uc_Purc_Ctp_Add_Item.Dept_ID = Convert.ToInt32(cmbDept.SelectedValue);
            uc_Purc_Ctp_Add_Item.Catalogue_code = ddlCatalogue.SelectedValue;
            uc_Purc_Ctp_Add_Item.Catalogue_Name = ddlCatalogue.SelectedItem.Text;
            uc_Purc_Ctp_Add_Item.BindData_UnSelected();
            uc_Purc_Ctp_Add_Item.BindData_Selected();
            uc_Purc_Ctp_Add_Item.BindSubCatalogue();
            uc_Purc_Ctp_Add_Item.SubCatalogue = "0";
            uc_Purc_Ctp_Add_Item.DepartmentName = cmbDept.SelectedItem.Text;
            uc_Purc_Ctp_Add_Item.Is_Reset_Values = false;
        }

        if (e.CommandName == "2")
        {
            if (uc_Purc_Ctp_Add_Item.Contract_ID > -1)
            {
                uc_Purc_Ctp_Send_RFQSupp.Contract_ID = uc_Purc_Ctp_Add_Item.Contract_ID;
                uc_Purc_Ctp_Send_RFQSupp.Is_Reset_Values = false;
            }
            else
            {
                mlvCTP.ActiveViewIndex = currentIndex;
                String msgretv = String.Format("alert('please save the item');");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgret6v", msgretv, true);
            }
        }
        //if (e.CommandName == "0")
        //{
        //    if (uc_Purc_Ctp_Add_Item.AddItems_Saved_Status)
        //    {

        //    }
        //}

    }

    protected void bindDeptCatalogue()
    {
        using (BLL_PURC_Purchase objTechService = new BLL_PURC_Purchase())
        {
            DataTable DeptDt = objTechService.GetDeptType();
            optList.DataSource = DeptDt;
            optList.DataTextField = "Description";
            optList.DataValueField = "Short_Code";
            optList.DataBind();
            optList.SelectedIndex = 0;

            DataTable dtDepartment = new DataTable();
            dtDepartment = objTechService.SelectDepartment();
            cmbDept.Items.Clear();
            cmbDept.DataTextField = "Name_Dept";
            cmbDept.DataValueField = "ID";
            cmbDept.DataSource = dtDepartment;
            cmbDept.DataBind();
            ListItem li = new ListItem("SELECT", "0");
            cmbDept.Items.Insert(0, li);

        }
    }

    protected void optList_SelectedIndexChanged(object sender, EventArgs e)
    {

        try
        {
            uc_Purc_Ctp_Add_Item.Is_Reset_Values = true;
           
            uc_Purc_Ctp_Send_RFQSupp.Is_Reset_Values = true;
            using (BLL_PURC_Purchase objTechService = new BLL_PURC_Purchase())
            {
                DataTable dtDepartment = new DataTable();
                dtDepartment = objTechService.SelectDepartment();

                if (optList.SelectedItem.Text == "Spares")
                {
                    dtDepartment.DefaultView.RowFilter = "Form_Type='" + optList.SelectedValue + "'";


                }
                else if (optList.SelectedItem.Text == "Stores")
                {
                    dtDepartment.DefaultView.RowFilter = "Form_Type='" + optList.SelectedValue + "'";



                }
                else if (optList.SelectedItem.Text == "Repairs")
                {
                    dtDepartment.DefaultView.RowFilter = "Form_Type='" + optList.SelectedValue + "'";


                }
                cmbDept.Items.Clear();
                cmbDept.DataTextField = "Name_Dept";
                cmbDept.DataValueField = "ID";
                cmbDept.DataSource = dtDepartment;
                cmbDept.DataBind();
                ListItem li = new ListItem("SELECT ", "0");
                cmbDept.Items.Insert(0, li);
                ddlCatalogue.Items.Clear();
                ddlCatalogue.Items.Insert(0, li);

            }




        }
        catch (Exception ex)
        {

        }
        finally
        {

        }
    }

    protected void cmbDept_OnSelectedIndexChanged(object source, System.EventArgs e)
    {
        try
        {
            uc_Purc_Ctp_Add_Item.AddItems_Saved_Status = false;
            uc_Purc_Ctp_Add_Item.Is_Reset_Values = true;
         
            uc_Purc_Ctp_Send_RFQSupp.Is_Reset_Values = true;

            string filter;

            if (cmbDept.SelectedIndex != 0)
            {
                using (BLL_PURC_Purchase objTechService = new BLL_PURC_Purchase())
                {
                    DataTable dtCatalog = objTechService.SelectCatalog();



                    string department = cmbDept.SelectedValue.ToString();

                    filter = "1=1";

                    if (department != "0")
                        filter += "  and  id='" + department + "'";


                    dtCatalog.DefaultView.RowFilter = filter;

                    if (dtCatalog.DefaultView.Count > 0)
                    {
                        ddlCatalogue.Items.Clear();
                        ddlCatalogue.DataTextField = "system_description";
                        ddlCatalogue.DataValueField = "system_code";
                        ddlCatalogue.DataSource = dtCatalog.DefaultView.ToTable();
                        ddlCatalogue.DataBind();
                        ListItem li = new ListItem("SELECT", "0");
                        ddlCatalogue.Items.Insert(0, li);

                    }
                    else
                    {

                    }

                }
            }
            else
            {
                ddlCatalogue.Items.Clear();
                ListItem li = new ListItem("SELECT", "0");
                ddlCatalogue.Items.Insert(0, li);
            }
        }
        catch (Exception ex)
        {

        }
        finally
        {

        }

    }

    protected void ddlCatalogue_SelectedIndexChanged(object sender, EventArgs e)
    {
        uc_Purc_Ctp_Add_Item.AddItems_Saved_Status = false;
        uc_Purc_Ctp_Add_Item.Is_Reset_Values = true;
     
        uc_Purc_Ctp_Send_RFQSupp.Is_Reset_Values = true;
    }
}