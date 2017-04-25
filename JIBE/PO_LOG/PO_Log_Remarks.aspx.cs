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

public partial class PO_LOG_PO_Log_Remarks : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["ID"] != null)
            {
                txtRemarksID.Text = Request.QueryString["ID"].ToString();
                txtPOCode.Text = Request.QueryString["POID"].ToString();
                BindRemarksGrid();
            }
        }
    }
    protected void btnAddRemarks_Click(object sender, EventArgs e)
    {
        String Remarks_Type = Request.QueryString["Type"].ToString();
        int RetValue = BLL_POLOG_Register.POLOG_Insert_Remarks(UDFLib.ConvertIntegerToNull(txtRemarksID.Text), UDFLib.ConvertIntegerToNull(txtPOCode.Text),
            txtAddRemarks.Text.Trim(), Remarks_Type,UDFLib.ConvertToInteger(Session["UserID"].ToString()));

        txtAddRemarks.Text = "";
        BindRemarksGrid();
        //string msgDraft = String.Format("parent.ReloadParent_ByButtonID();");
        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgDraft", msgDraft, true);
        //ClearControl();
    }
    protected void BindRemarksGrid()
    {
        int Type = 0;
        String Remarks_Type = Request.QueryString["Type"].ToString();
        DataTable dt = BLL_POLOG_Register.POLOG_Get_Remarks(UDFLib.ConvertIntegerToNull(txtRemarksID.Text), UDFLib.ConvertIntegerToNull(txtPOCode.Text.ToString()), Type, Remarks_Type);

        if (dt.Rows.Count > 0)
        {
            gvRemarks.DataSource = dt;
            gvRemarks.DataBind();
        }
    }
    protected void lbtnEdit_Click(object s, CommandEventArgs e)
    {
        string[] arg = e.CommandArgument.ToString().Split(',');
        int? RemarksID = UDFLib.ConvertIntegerToNull(arg[0]);
        txtRemarksID.Text = UDFLib.ConvertStringToNull(arg[0]);
        DataTable dt = new DataTable();
        int Type = 1;
        String Remarks_Type = Request.QueryString["Type"].ToString();
        dt = BLL_POLOG_Register.POLOG_Get_Remarks(UDFLib.ConvertIntegerToNull(txtRemarksID.Text), UDFLib.ConvertIntegerToNull(txtPOCode.Text.ToString()), Type, Remarks_Type);
        if (dt.Rows.Count > 0)
        {
            DataRow dr = dt.Rows[0];
            txtAddRemarks.Text = dr["Remarks"].ToString();
            btnAddRemarks.Text = "Edit Remarks";
        }

    }
    protected void lbtnDelete_Click(object s, CommandEventArgs e)
    {
        string[] arg = e.CommandArgument.ToString().Split(',');
        int? RemarksID = UDFLib.ConvertIntegerToNull(arg[0]);

        int RetValue = BLL_POLOG_Register.POLOG_Delete_Remarks(RemarksID, UDFLib.ConvertToInteger(Session["UserID"].ToString()));
        BindRemarksGrid();
        //string AddUserTypemodal = String.Format("showModal('divadd',false);");
        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddUserTypemodal", AddUserTypemodal, true);
    }
}