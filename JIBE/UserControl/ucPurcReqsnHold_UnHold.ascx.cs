using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;


public partial class UserControl_ucPurcReqsnHold_UnHold : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {



    }


    protected void btndivSave_Click(object s, EventArgs e)
    {
        SaveClick(s, e);
    }

    protected void btndivCancel_Click(object s, EventArgs e)
    {
        CancelClick(s, e);
    }


    private string _ReqsnCode;
    private DataTable _dtLog;
    public string ReqsnCode
    {
        get { return _ReqsnCode; }
        set { _ReqsnCode = value; }
    }

    public DataTable DTLog
    {
        set { _dtLog = value; }
    }

    public string lblHeader
    {
        get { return lblUrgencyTitle.Text; }
        set { lblUrgencyTitle.Text = value; }
    }

    public string Remarks
    {
        get { return txtRemarks.Text; }
        set { txtRemarks.Text = value; }
    }

    public void BindLog()
    {
        rgdHoldLog.DataSource = _dtLog;
        rgdHoldLog.DataBind();
    }

    public delegate void ButtonCommandEventHandler(object s, EventArgs e);

    public event ButtonCommandEventHandler SaveClick;
    public event ButtonCommandEventHandler CancelClick;

}