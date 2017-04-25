using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.Infrastructure;
using SMS.Business.PURC;
using System.Data;
using ClsBLLTechnical;


public partial class UserControl_ucPurcQuotationApproval : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnSendForApproval_Click(object s, EventArgs e)
    {
        if (lstUserList.SelectedValue != "0")
        {
            ApproverID = lstUserList.SelectedValue;
            stsSaved(null, null);
        }
        else
        {
            String msg = String.Format("alert('Please select user');");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg, true);
        }
    }
    private string _ApproverID;

    public string ApproverID
    {
        get { return _ApproverID; }
        set { _ApproverID = value; }
    }

    public string ReqsnCode
    {
        get { return hdfReqsnCode.Value; }
        set { hdfReqsnCode.Value = value; }
    }
    public string DocumentCode
    {
        get { return hdfDocumentCode.Value; }
        set { hdfDocumentCode.Value = value; }
    }
    public string VesselCode
    {
        get { return hdfVesselCode.Value; }
        set { hdfVesselCode.Value = value; }
    }

    public string Remark
    {
        get { return txtRemark.Text; }
        set { txtRemark.Text = value; }
    }

    public string CallSaved
    {
        get { return hdfCallstsSaved.Value; }
        set { hdfCallstsSaved.Value = value; }
    }

    public void FillUser()
    {
        DataSet dsUsers = BLL_PURC_Common.Get_QTN_Approver(hdfReqsnCode.Value);

        lstUserList.DataSource = dsUsers.Tables[0];
        lstUserList.DataBind();
        lstUserList.Items.Insert(0, new ListItem("SELECT", "0"));
        lstUserList.SelectedIndex = 0;
        lblreqsnCode.Text = "Reqsn No.: " + ReqsnCode;
        ListItem itemrmv = lstUserList.Items.FindByValue(Session["userid"].ToString());
        lstUserList.Items.Remove(itemrmv);
    }

    public delegate void SavedCommandEventHandler(object s, EventArgs e);

    public event SavedCommandEventHandler stsSaved;

}