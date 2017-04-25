using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SMS.Business.INFRA;

public partial class ASL_Libraries_Approval_Group_Department : System.Web.UI.Page
{
    BLL_Infra_Approval_Group_Department objBllGroup = new BLL_Infra_Approval_Group_Department();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!String.IsNullOrEmpty(Request.QueryString["ID"].ToString()))
            {
                BindGroupDeatils(Convert.ToInt16(Request.QueryString["ID"].ToString()));
            }
        }
    }
    void BindGroupDeatils(int GroupID)
    {
        try
        {
            DataSet ds = objBllGroup.Get_Approval_Group(GroupID);

            if (ds.Tables[0].Rows.Count > 0)
            {
                txtApprovalGroup.Text = ds.Tables[0].Rows[0]["Group_Name"].ToString();
                txtGroupID.Text = ds.Tables[0].Rows[0]["Group_ID"].ToString();
            }
            BindDepartment();
        }
        catch { }
        {
        }
    }
    void BindDepartment()
    {
        try
        {

            DataSet ds = objBllGroup.Get_DepartmentList(UDFLib.ConvertStringToNull(rdbDepartment.SelectedValue),UDFLib.ConvertIntegerToNull(txtGroupID.Text));
            rdbDepartment.DataSource = ds.Tables[0];
            rdbDepartment.DataTextField = "FormName";
            rdbDepartment.DataValueField = "Form_Type";
            rdbDepartment.DataBind();
            if (ds.Tables[2].Rows.Count > 0)
            {
                rdbDepartment.SelectedValue = ds.Tables[2].Rows[0]["Department_Type"].ToString();
            }
            else
            {
                rdbDepartment.SelectedIndex = 0;
            }
            BindDepartmentList();
            
        }
        catch { }
        {
        }
    }
    protected void BindDepartmentList()
    {
        try
        {
            DataSet ds_Depart = objBllGroup.Get_DepartmentList(UDFLib.ConvertStringToNull(rdbDepartment.SelectedValue), UDFLib.ConvertIntegerToNull(txtGroupID.Text));
            chkDepartment.DataSource = ds_Depart.Tables[1];
            chkDepartment.DataTextField = "name_dept";
            chkDepartment.DataValueField = "ID";
            chkDepartment.DataBind();
            int i = 0;
            foreach (ListItem chkitem in chkDepartment.Items)
            {
                if (ds_Depart.Tables[1].Rows[i]["Selected"].ToString() == "1")
                {
                    chkitem.Enabled = false;
                }
                if (ds_Depart.Tables[1].Rows[i]["Active_Status"].ToString() == "1")
                {
                    chkitem.Selected = true;
                    chkitem.Enabled = true;
                }
                i++;
            }
        }
        catch { }
        {
        }
    }
       
    protected void btnDraft_Click(object sender, EventArgs e)
    {
        try
        {
            int responseid = objBllGroup.INS_Approval_Group(UDFLib.ConvertIntegerToNull(txtGroupID.Text), rdbDepartment.SelectedValue, txtApprovalGroup.Text, Convert.ToInt32(Session["USERID"]));
            txtGroupID.Text = Convert.ToString(responseid);
            SaveDepartmentList();
        
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

        foreach (ListItem chkitem in chkDepartment.Items)
        {
            DataRow dr = dt.NewRow();
            dr["FKID"] = chkitem.Value;
            dr["Value"] = chkitem.Selected == true ? 1 : 0;
            if ( chkitem.Selected == true )
                dt.Rows.Add(dr);
        }
        objBllGroup.INS_Approval_Group_Department(UDFLib.ConvertIntegerToNull(txtGroupID.Text.ToString()), dt, rdbDepartment.SelectedValue, UDFLib.ConvertToInteger(Session["UserID"].ToString()));
    }
    protected void rdbDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindDepartmentList();
    }
}