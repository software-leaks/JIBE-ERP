using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SMS.Business.Crew;
using System.ComponentModel;

public partial class UserControl_ucCardAttachments : System.Web.UI.UserControl
{
    BLL_Crew_CrewDetails objBLLCrew = new BLL_Crew_CrewDetails();
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void ImgAttachments_Click(object sender, EventArgs e)
    {
        pnl_uc_popup.Visible = true;
        int CardID = UDFLib.ConvertToInteger(hdnCardID.Value);
        int UserID = UDFLib.ConvertToInteger(hdnUserID.Value);

        Load_Attachments(CardID, UserID);
    }

    protected void ImgClose_Click(object sender, EventArgs e)
    {
        pnl_uc_popup.Visible = false;
    }
        

    protected void Load_Attachments(int CardID, int UserID)
    {
        DataTable dt = objBLLCrew.Get_CardAttachments(CardID, UserID);
        rptAttachments.DataSource = dt;
        rptAttachments.DataBind();
    }

    [BindableAttribute(true)]
    public int CardID
    {

        get { return UDFLib.ConvertToInteger(hdnCardID.Value); }

        set
        {
            try
            {
                hdnCardID.Value = value.ToString();
                DataTable dt = objBLLCrew.Get_CardAttachments(CardID, 0);
                if (dt.Rows.Count == 0)
                    ImgAttachments.Visible = false;
            }
            catch { }
        }
    }
    public int UserID
    {

        get { return UDFLib.ConvertToInteger(hdnUserID.Value); }

        set
        {
            try
            {
                hdnUserID.Value = value.ToString();
            }
            catch { }
        }
    }

}