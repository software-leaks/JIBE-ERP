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
using SMS.Business.PURC;
using Telerik.Web.UI;
using SMS.Properties;
using SMS.Business.Infrastructure;
using System.Text;

public partial class Purchase_Add_EmailTemplate : System.Web.UI.Page
{
    BLL_PURC_Purchase objBLLDept = new BLL_PURC_Purchase();
    UserAccess objUA = new UserAccess();
    string OperationMode = "";
    string Flag = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["OperationMode"] != null)
        {
            OperationMode = Request.QueryString["OperationMode"];
        }
        if (Request.QueryString["Flag"] != null)
        {
            Flag = Request.QueryString["Flag"];
        }
        this.Title = OperationMode;
        if (!IsPostBack)
        {
            BindEmailType();

            if (Request.QueryString["command"] != null)
            {
                DataTable dt = new DataTable();
                dt = objBLLDept.PURC_Get_Email_Template(Convert.ToInt32(Request.QueryString["command"]));

                txtUserTypeID.Text = dt.Rows[0]["ID"].ToString();
                ddlEmailType.SelectedValue = dt.Rows[0]["Email_Type"].ToString();
                txtemailbody.Text = dt.Rows[0]["Email_Body"].ToString();
                txtemailsubject.Text = dt.Rows[0]["Email_Subject"].ToString();
            }

        }

    }

    /// <summary>
    /// This method is used to bind DDL of email template
    /// </summary>
    protected void BindEmailType()
    {
        try
        {
            DataSet dt = objBLLDept.Get_Email_System_Parameter("EMAIL", UDFLib.ConvertToInteger(Session["UserID"].ToString()));

            ddlEmailType.DataSource = dt;
            ddlEmailType.DataValueField = "Name";
            ddlEmailType.DataTextField = "Description";
            ddlEmailType.DataBind();
            ddlEmailType.Items.Insert(0, new ListItem("-Select-", "0"));

        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }

    }
    protected void btnsave_Click(object sender, EventArgs e)
    {
        try
        {
            int responseid=0;
            if (Flag == "Add")
            {
                responseid = objBLLDept.PURC_Ins_Email_Template(ddlEmailType.SelectedValue, txtemailbody.Text.Trim(), txtemailsubject.Text.Trim(), Convert.ToInt32(Session["USERID"]));

            }
            else
            {
                responseid = objBLLDept.PURC_UPD_Email_Template(Convert.ToInt32(txtUserTypeID.Text), ddlEmailType.SelectedValue, txtemailbody.Text.Trim(), txtemailsubject.Text.Trim(), Convert.ToInt32(Session["USERID"]));

            }
            if (responseid == 0)
            {

                string message = "alert('Email Type Already Existed.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
            }

            string jScript = "<script>window.close();</script>";
            ClientScript.RegisterClientScriptBlock(this.GetType(), "keyClientBlock", jScript);
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }

    }
}