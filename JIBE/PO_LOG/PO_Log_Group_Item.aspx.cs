using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SMS.Business.INFRA;
using SMS.Business.POLOG;

public partial class PO_LOG_PO_Log_Group_Item : System.Web.UI.Page
{
    BLL_Infra_Approval_Group_Department objBllGroup = new BLL_Infra_Approval_Group_Department();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindPOType();
            if (!String.IsNullOrEmpty(Request.QueryString["Group_ID"].ToString()))
            {
                BindGroupDeatils(UDFLib.ConvertStringToNull(Request.QueryString["Group_ID"].ToString()));
            }
        }
    }
    protected void BindPOType()
    {
        try
        {
            DataSet ds = BLL_POLOG_Register.POLOG_Get_Type(UDFLib.ConvertToInteger(Session["UserID"].ToString()),"PO_TYPE");
            if (ds.Tables[1].Rows.Count > 0)
            {
                ddlPOType.DataSource = ds.Tables[1];
                ddlPOType.DataTextField = "VARIABLE_NAME";
                ddlPOType.DataValueField = "VARIABLE_CODE";
                ddlPOType.DataBind();
                ddlPOType.Items.Insert(0, new ListItem("-Select-", "0"));
            }
            else
            {
                ddlPOType.Items.Insert(0, new ListItem("-Select-", "0"));
            }
            //
        }
        catch { }
        {
        }
    }
    void BindGroupDeatils(string GroupID)
    {
        try
        {
            //Change And Enter One Type
            int Type = 1;
            //DataSet ds = BLL_POLOG_Lib.Get_Approval_Group(GroupID, Type);
            DataSet ds = BLL_POLOG_Register.POLOG_Get_AccountClassification(UDFLib.ConvertIntegerToNull(GetSessionUserID()), null);
            if (ds.Tables[1].Rows.Count > 0)
            {
                txtApprovalGroup.Text = ds.Tables[0].Rows[0]["Approval_Group_Name"].ToString();
                txtGroupID.Text = ds.Tables[0].Rows[0]["Approval_Group_Code"].ToString();
                ddlPOType.SelectedValue = ds.Tables[0].Rows[0]["Req_type"].ToString();
            }
            BindDepartmentList(txtGroupID.Text, ddlPOType.SelectedValue);
        }
        catch { }
        {
        }
    }
    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }
    protected void BindDepartmentList(string GroupID,string PO_Type)
    {
        try
        {
            DataSet ds_Depart = BLL_POLOG_Lib.Get_Approval_Group_Item(UDFLib.ConvertStringToNull(txtGroupID.Text),UDFLib.ConvertStringToNull(PO_Type));
            chkAccClasification.DataSource = ds_Depart.Tables[0];
            chkAccClasification.DataTextField = "Variable_Name";
            chkAccClasification.DataValueField = "Variable_Code";
            chkAccClasification.DataBind();
            int i = 0;
            foreach (ListItem chkitem in chkAccClasification.Items)
            {
                if (ds_Depart.Tables[0].Rows[i]["Selected"].ToString() == "1")
                {
                    chkitem.Enabled = false;
                }
                if (ds_Depart.Tables[0].Rows[i]["Active_Status"].ToString() == "1")
                {
                    chkitem.Selected = true;
                    chkitem.Enabled = true;
                }
                i++;
            }
            String User_Type = "OFFICE USER";
            DataSet ds = BLL_POLOG_Lib.POLOG_Get_Approval_Group_User(UDFLib.ConvertStringToNull(GroupID), User_Type);


            gvUser.DataSource = ds.Tables[0];
            gvUser.DataBind();
            //chkInvoiceCreater.DataTextField = "User_Name";
            //chkInvoiceCreater.DataValueField = "UserID";
            //chkInvoiceCreater.DataBind();
            //int j = 0;
            //foreach (ListItem chkitem in chkInvoiceCreater.Items)
            //{
            //    if (ds.Tables[0].Rows[j]["Invoice_Creator"].ToString() == "1")
            //    {
            //        chkitem.Selected = true;
            //    }
            //    j++;
            //}
            //foreach (ListItem chkitem in chkInvoiceApprover.Items)
            //{
            //    if (ds.Tables[0].Rows[j]["Invoice_Approver"].ToString() == "1")
            //    {
            //        chkitem.Selected = true;
            //    }
            //    j++;
            //}
           
           
        }
        catch { }
        {
        }
    }

    protected void btnDraft_Click(object sender, EventArgs e)
    {
        try
        {
            string responseid = BLL_POLOG_Lib.INS_Approval_Group(UDFLib.ConvertStringToNull(txtGroupID.Text), txtApprovalGroup.Text,ddlPOType.SelectedValue, UDFLib.ConvertIntegerToNull(Session["USERID"]));
            txtGroupID.Text = Convert.ToString(responseid);
            SaveDepartmentList();
            //SaveUserList();
            string msgDraft = String.Format("parent.ReloadParent_ByButtonID();");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgDraft", msgDraft, true);
        }
        catch { }
        {
        }
    }
  
    protected void SaveDepartmentList()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("PKID");
        dt.Columns.Add("FKID");
        dt.Columns.Add("Value");
        //int iInvoiceCreator = 0;
        //int iInvoiceApprover = 0;
        //int? iUserID = 0;
        foreach (ListItem chkitem in chkAccClasification.Items)
        {
            DataRow dr = dt.NewRow();
            dr["FKID"] = chkitem.Value;
            dr["Value"] = chkitem.Selected == true ? 1 : 0;
            if (chkitem.Selected == true)
                dt.Rows.Add(dr);
        }
        DataTable dtUser = new DataTable();
        dtUser.Columns.Add("PKID");
        dtUser.Columns.Add("UserID");
        dtUser.Columns.Add("InvoiceCreator");
        dtUser.Columns.Add("InvoiceApprover");
        foreach (GridViewRow row in gvUser.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                if (((CheckBox)row.FindControl("chkInvoice_Creator")).Checked == true || ((CheckBox)row.FindControl("chkInvoice_Approver")).Checked == true)
                {
                    DataRow dr = dtUser.NewRow();
                    dr["UserID"] = UDFLib.ConvertIntegerToNull(gvUser.DataKeys[row.RowIndex].Value.ToString());
                    dr["InvoiceCreator"] = (((CheckBox)row.FindControl("chkInvoice_Creator")).Checked == true) ? 1 : 0;
                    dr["InvoiceApprover"] = (((CheckBox)row.FindControl("chkInvoice_Approver")).Checked == true) ? 1 : 0;
                    dtUser.Rows.Add(dr);
                }
            }

        }
        BLL_POLOG_Lib.INS_Approval_Group_Item(UDFLib.ConvertStringToNull(txtGroupID.Text.ToString()), dt, dtUser, UDFLib.ConvertToInteger(Session["UserID"].ToString()));
    }

    protected void gvUser_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    protected void ddlPOType_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindDepartmentList(UDFLib.ConvertStringToNull(txtGroupID.Text), UDFLib.ConvertStringToNull(ddlPOType.SelectedValue));
    }
}