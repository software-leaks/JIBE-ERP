using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.Crew;
using System.Data;

public partial class Crew_Crew_Details_Configuration : System.Web.UI.Page
{
    BLL_Crew_Admin obj = new BLL_Crew_Admin();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                UserAccessValidation();
                bindGridView();
            }
        }
        catch (Exception ex) { UDFLib.WriteExceptionLog(ex); }
    }

    /// <summary>
    /// User Access. Only user having Admin access will be able perform operations
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void UserAccessValidation()
    {
        int CurrentUserID = GetSessionUserID();

        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

        SMS.Properties.UserAccess objUA = new SMS.Properties.UserAccess();
        SMS.Business.Infrastructure.BLL_Infra_UserCredentials objUser = new SMS.Business.Infrastructure.BLL_Infra_UserCredentials();
        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

        if (objUA.View == 0)
            Response.Redirect("~/default.aspx?msgid=1");
        if (objUA.Admin == 1)
        {
            btnsave.Visible = true;
        }
        else
            btnsave.Visible = false;
       
    }
    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }

    private void bindGridView()
    {
        
       DataSet ds= obj.CRW_GetCDConfiguration(null);
       if (ds != null)
       {
           GridView1.DataSource = ds.Tables[0];
           GridView1.DataBind();
       }

    }


    /// <summary>
    ///GridView Row Data Bound.
    ///Code written for Configuration fields that are editable
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            SMS.Properties.UserAccess objUA = new SMS.Properties.UserAccess();
            SMS.Business.Infrastructure.BLL_Infra_UserCredentials objUser = new SMS.Business.Infrastructure.BLL_Infra_UserCredentials();
            int CurrentUserID = GetSessionUserID();

            string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());
            objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lbl = (Label)e.Row.FindControl("lblKey");
                Label lblD = (Label)e.Row.FindControl("lblDisplay");
                Label lblC = (Label)e.Row.FindControl("lblConfiguration");
                ImageButton imgButton = (ImageButton)e.Row.FindControl("ImgEdit");
                if (lbl.Text == "Addressformat")
                {
                  //  ((RadioButton)e.Row.FindControl("rdbDisplay")).Attributes.Add("onclick", "javascript:checkMutuallyExclusive('" + ((RadioButton)e.Row.FindControl("rdbConf")).ClientID + "','" + ((CheckBox)e.Row.FindControl("rdbDisplay")).ClientID + "')");
                    ((CheckBox)e.Row.FindControl("chkDisplay")).Visible = false;
                    ((RadioButton)e.Row.FindControl("rdbDisplay")).Visible = true;
                    ((RadioButton)e.Row.FindControl("rdbDisplay")).Text = "US";
                    //((RadioButton)e.Row.FindControl("rdbConf")).Attributes.Add("onclick", "javascript:checkMutuallyExclusive('" + ((RadioButton)e.Row.FindControl("rdbDisplay")).ClientID + "','" + ((CheckBox)e.Row.FindControl("rdbConf")).ClientID + "')");
                    ((CheckBox)e.Row.FindControl("chkConfidential")).Visible = false;
                    ((RadioButton)e.Row.FindControl("rdbConf")).Visible = true;
                    ((RadioButton)e.Row.FindControl("rdbConf")).Text = "International";
                }
                if (lbl.Text == "CF1" || lbl.Text == "CF2" || lbl.Text == "CF3")
                {
                    if (objUA.Admin == 1)
                    {
                        imgButton.Visible = true;
                    }
                    else
                        imgButton.Visible = false;
                    
                }
                else
                {
                    imgButton.Visible = false;
                }
            }
        }
        catch (Exception ex) { UDFLib.WriteExceptionLog(ex); }
    }
    protected void rdbDisplay_checked(object sender, EventArgs e)
    {
        RadioButton btn = (RadioButton)sender;
        GridViewRow gvr = (GridViewRow)btn.NamingContainer;
        RadioButton rdbConfidential = (RadioButton)gvr.FindControl("rdbConf");
        RadioButton rdbDisplay = (RadioButton)gvr.FindControl("rdbDisplay");
        if (rdbDisplay.Checked)
        {
            rdbConfidential.Checked = false;
        }

    }

    protected void rdbConf_checked(object sender, EventArgs e)
    {
        RadioButton btn = (RadioButton)sender;
        GridViewRow gvr = (GridViewRow)btn.NamingContainer;
        RadioButton rdbConfidential = (RadioButton)gvr.FindControl("rdbConf");
        RadioButton rdbDisplay = (RadioButton)gvr.FindControl("rdbDisplay");
        if (rdbConfidential.Checked)
        {
            rdbDisplay.Checked = false;
        }
    }
    
    /// <summary>
    /// Save Event. Datatable type parameter has been passed as argument
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnsave_Click(object sender, EventArgs e)
    {
        try
        {
            string confirmValue = Request.Form["confirm_value"];
            if (confirmValue == "Yes")
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("ID");
                dt.Columns.Add("Key");
                dt.Columns.Add("DisplayName");
                dt.Columns.Add("Display");
                dt.Columns.Add("Confidential");
                dt.Columns.Add("value");
                dt.Columns.Add("userid");
                for (int j = 0; j < GridView1.Rows.Count; j++)
                {
                    DataRow dr = dt.NewRow();
                    Label lbID = GridView1.Rows[j].FindControl("lblID") as Label;
                    Label lbKey = GridView1.Rows[j].FindControl("lblKey") as Label;
                    Label lbDN = GridView1.Rows[j].FindControl("lblDisplayName") as Label;
                    CheckBox lbDisplay = GridView1.Rows[j].FindControl("chkDisplay") as CheckBox;
                    CheckBox lbConf = GridView1.Rows[j].FindControl("chkConfidential") as CheckBox;
                    dr["ID"] = Convert.ToInt32(lbID.Text);
                    dr["Key"] = lbKey.Text;
                    dr["DisplayName"] = lbDN.Text;
                    dr["Display"] = lbDisplay.Checked ? 1 : 0;
                    dr["Confidential"] = lbConf.Checked ? 1 : 0;
                    if (lbKey.Text == "Addressformat")
                    {
                        RadioButton rdbDisplay = GridView1.Rows[j].FindControl("rdbDisplay") as RadioButton;
                        RadioButton rdbConf = GridView1.Rows[j].FindControl("rdbConf") as RadioButton;
                        if (rdbDisplay.Checked)
                        {
                            dr["value"] = 0;
                            dr["Display"] = 1;
                            dr["Confidential"] = 0;
                        }
                        else
                        {
                            dr["value"] = 1;
                            dr["Confidential"] = 1;
                            dr["Display"] = 0;
                        }
                    }
                    else
                    {
                        dr["value"] = null;
                    }
                    dr["userid"] = int.Parse(Session["USERID"].ToString());
                    dt.Rows.Add(dr);
                }

                int x = obj.CRW_UpdateConfig(dt);
                if (x > 0)
                {
                    bindGridView();
                    Response.Write("<script type='text/javascript'>");
                    Response.Write("alert('Crew details configuration has been updated successfully.');");
                    Response.Write("document.location.href='Crew_Details_Configuration.aspx';");
                    Response.Write("</script>");
                }
            }
            else
            {
                Response.Write("<script type='text/javascript'>");
                Response.Write("document.location.href='Crew_Details_Configuration.aspx';");
                Response.Write("</script>");
            }
          
        }
        catch (Exception ex) { UDFLib.WriteExceptionLog(ex); }

    }



    /// <summary>
    /// Edit/Save/cancel GridView Events for Configurable fields
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// 
    protected void ImgEdit_Click(object sender,EventArgs e)
    {
        try
        {
            ImageButton btn = (ImageButton)sender;
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;
            Label lbl = (Label)gvr.FindControl("lblDisplayName");
            TextBox txt = (TextBox)gvr.FindControl("txtDisplay");
            ImageButton imgSave = (ImageButton)gvr.FindControl("ImgSave");
            ImageButton imgCancel = (ImageButton)gvr.FindControl("ImgCancel");
            ImageButton imgEdit = (ImageButton)gvr.FindControl("ImgEdit");
            CheckBox chkConfidential = (CheckBox)gvr.FindControl("chkConfidential");
            CheckBox chkDisplay = (CheckBox)gvr.FindControl("chkDisplay");
            chkConfidential.Enabled = false;
            chkDisplay.Enabled = false;
            lbl.Visible = false;
            txt.Visible = true;
            imgEdit.Visible = false;
            imgSave.Visible = true;
            imgCancel.Visible = true;
        }
        catch (Exception ex) { UDFLib.WriteExceptionLog(ex); }

    }
    protected void ImgSave_Click(object sender,EventArgs e)
    {
        try
        {
            ImageButton btn = (ImageButton)sender;
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;
            Label ID = (Label)gvr.FindControl("lblID");
            Label Key = (Label)gvr.FindControl("lblKey");
            Label lbl = (Label)gvr.FindControl("lblDisplayName");
            TextBox txt = (TextBox)gvr.FindControl("txtDisplay");
            ImageButton imgSave = (ImageButton)gvr.FindControl("ImgSave");
            ImageButton imgCancel = (ImageButton)gvr.FindControl("ImgCancel");
            ImageButton imgEdit = (ImageButton)gvr.FindControl("ImgEdit");
            CheckBox chkConfidential = (CheckBox)gvr.FindControl("chkConfidential");
            CheckBox chkDisplay = (CheckBox)gvr.FindControl("chkDisplay");
            chkConfidential.Enabled = true;
            chkDisplay.Enabled = true;
   
            if (!string.IsNullOrEmpty(txt.Text.Trim()))
            {
                int i = obj.CRW_UpdateConfigFields(UDFLib.ConvertToInteger(ID.Text), Key.Text.Trim(), txt.Text.Trim());
                if (i > 0)
                {
                    bindGridView();
                    Response.Write("<script type='text/javascript'>");
                    Response.Write("alert('Field has been updated successfully.');");
                    Response.Write("document.location.href='Crew_Details_Configuration.aspx';");
                    Response.Write("</script>");
                    imgEdit.Visible = true;
                    imgSave.Visible = false;
                    imgCancel.Visible = false;
                    lbl.Visible = true;
                    txt.Visible = false;
                }
                else
                {
                 
                    Response.Write("<script type='text/javascript'>");
                    Response.Write("alert('Field name already exists.');");
                    Response.Write("document.location.href='Crew_Details_Configuration.aspx';");
                    Response.Write("</script>");
                    bindGridView();
                    lbl.Visible = false;
                    txt.Visible = true;
                    imgEdit.Visible = false;
                    imgSave.Visible = true;
                    imgCancel.Visible = true;

                }
            }
        }
        catch (Exception ex) { UDFLib.WriteExceptionLog(ex); }

    }

    protected void ImgCancel_Click(object sender,EventArgs e)
    {
        try
        {
            ImageButton btn = (ImageButton)sender;
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;
            Label lbl = (Label)gvr.FindControl("lblDisplayName");
            TextBox txt = (TextBox)gvr.FindControl("txtDisplay");
            ImageButton imgSave = (ImageButton)gvr.FindControl("ImgSave");
            ImageButton imgCancel = (ImageButton)gvr.FindControl("ImgCancel");
            ImageButton imgEdit = (ImageButton)gvr.FindControl("ImgEdit");
            imgEdit.Visible = true;
            imgSave.Visible = false;
            imgCancel.Visible = false;
            lbl.Visible = true;
            txt.Visible = false;
            CheckBox chkConfidential = (CheckBox)gvr.FindControl("chkConfidential");
            CheckBox chkDisplay = (CheckBox)gvr.FindControl("chkDisplay");
            chkConfidential.Enabled = true;
            chkDisplay.Enabled = true;
            Response.Write("<script type='text/javascript'>");
            Response.Write("document.location.href='Crew_Details_Configuration.aspx';");
            Response.Write("</script>");
        }
        catch (Exception ex) { UDFLib.WriteExceptionLog(ex); }
    }
}