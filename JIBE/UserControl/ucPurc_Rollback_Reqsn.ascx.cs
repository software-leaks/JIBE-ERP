using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.PURC;
using System.Data;

public partial class UserControl_ucPurc_Rollback_Reqsn : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {


    }




    public string Reason
    {
        get { return txtReason.Text; }
        set { txtReason.Text = value; }
    }

    public string StageValue
    {
        get { return DDLReqStages.SelectedValue; }
        set { DDLReqStages.SelectedValue = value; }
    }

    public string StageText
    {
        get { return DDLReqStages.SelectedItem.Text; }
        set { DDLReqStages.SelectedItem.Text = value; }
    }

    public string RequisitionCode
    {
        get { return lblReqsnNoRollBack.Text; }
        set
        {
            lblReqsnNoRollBack.Text = value;
            btndivReqprioOK.Attributes.Add("onclick", " Async_Get_Reqsn_Validity('" + value + "')");
            if (DDLReqStages.Items.Count > 1)
                DDLReqStages.ClearSelection();

            txtReason.Text = "";
        }
    }

    public string Order_Code
    {
        get { return alinkOrder.InnerText; }
        set
        {
            alinkOrder.InnerText = value;

            if (value.Trim().Length < 1)
            {
                lblPOdtl.Visible = false;
                lblOrdCode.Visible = false;
                lblsupplName.Visible = false;
            }
        }
    }

    public string HRef
    {
        get { return alinkOrder.HRef; }
        set { alinkOrder.HRef = value; }
    }

    public string SupplierName
    {
        get { return lblsuppName.Text; }
        set { lblsuppName.Text = value; }
    }

    public delegate void SaveEventHaldeler(object s, EventArgs e);
    public event SaveEventHaldeler Save;

    public void BindRequisitionStatus(string sRequiPendingType)
    {

        using (BLL_PURC_Purchase objTechService = new BLL_PURC_Purchase())
        {
            DataTable dtREQStatus = objTechService.GetREQStatus();
            DataRow[] filterRows = dtREQStatus.Select("short_code ='" + sRequiPendingType + "'");
            dtREQStatus.DefaultView.RowFilter = "code < '" + filterRows[0][0].ToString() + "'";

            DDLReqStages.DataSource = dtREQStatus.DefaultView;
            DDLReqStages.DataTextField = "Description";
            DDLReqStages.DataValueField = "short_code";
            DDLReqStages.DataBind();

            if (DDLReqStages.Items.Count == 1)
                DDLReqStages.SelectedIndex = 0;

        }
    }


    public void BindGrid()
    {
        ucReqsncancelLog1.BindGrid();

    }

    protected void btndivReqprioOK_Click(object sender, EventArgs e)
    {
        Reason = txtReason.Text;
        StageValue = DDLReqStages.SelectedValue;
        StageText = DDLReqStages.SelectedItem.Text;

      Save(sender, e);
        
    }

}