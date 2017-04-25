using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.Infrastructure;
using System.Web.UI.WebControls.WebParts;
using System.Data;
using SMS.Business.PURC;

using SMS.Business.Crew;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;



public partial class Infrastructure_DashBoard_Common : System.Web.UI.Page
{
    protected void Page_Init(object sender, EventArgs e)
    {
        //foreach (WebPartDisplayMode mode in WebPartManagerDB.SupportedDisplayModes)
        //{
        //    string modeName = mode.Name;
        //    if (mode.IsEnabled(WebPartManagerDB))
        //    {
        //        ListItem item = new ListItem(modeName, modeName);
        //        DisplayModeDropdown.Items.Add(item);
        //    }
        //}
    }
    WebPartManager _manager;
    protected void Page_Load(object sender, EventArgs e)
    {
        lblErrorMessage.Text = "";
        try
        {
            //UserAccessValidation();

            // web part
            _manager = WebPartManager.GetCurrentWebPartManager(Page);
            WebPartDisplayMode mode = _manager.SupportedDisplayModes["design"];
            _manager.DisplayMode = mode;

            CheckAccessRightsAndLoadSnippets();


            hdnUserID.Value = Session["USERID"].ToString();
            hdfUserdepartmentid.Value = Convert.ToString(Session["USERDEPARTMENTID"]);
            hdfUserCompanyID.Value = Convert.ToString(Session["USERCOMPANYID"]);


        }
        catch (Exception ex)
        {
            lblErrorMessage.Text += ex.Message + ex.StackTrace;
        }

    }

    public SMS.Properties.UserAccess objUA = new SMS.Properties.UserAccess();
    protected void UserAccessValidation()
    {
        int CurrentUserID = Convert.ToInt32(Session["userid"]);
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());
        SMS.Business.Infrastructure.BLL_Infra_UserCredentials objUser = new SMS.Business.Infrastructure.BLL_Infra_UserCredentials();

        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

        if (objUA.View == 0)
            Response.Redirect("~/default.aspx?msgid=1");

        if (objUA.Add == 0)
        {

        }
        if (objUA.Edit == 0)
        {


        }
        if (objUA.Approve == 0)
        {

        }
        if (objUA.Delete == 0)
        {


        }


    }


    protected void CheckAccessRightsAndLoadSnippets()
    {
        try
        {

            StringBuilder sbMyActionLabels = new StringBuilder();

            int? DepID = UDFLib.ConvertIntegerToNull((Request.QueryString["DepID"] == null ? "0" : Request.QueryString["DepID"].ToString()));

            DataTable dtSnippetAccess = BLL_Infra_DashBoard.Get_Snippet_Access_OnDashboard(Convert.ToInt32(Session["userid"]), DepID);


            if (DepID != null)
            {
                DataRow[] drValue = dtSnippetAccess.Select("Department_ID = " + DepID.ToString());
                if (drValue.Length > 0)
                {
                    string avalue = drValue[0]["VALUE"].ToString();
                    lblDashBoard.Text = avalue + " Dashboard";
                }

            }
            //if (DepID != null)
            //    lblDashBoard.Text = dtSnippetAccess.Rows[0]["VALUE"] + " Dashboard";
            StringBuilder jsFunctions = new StringBuilder();
            StringBuilder jsFunctions_Auto_Refresh = new StringBuilder();
            int counter = 0;

            if (dtSnippetAccess.Rows.Count > 0)
            {
                foreach (DataRow dr in dtSnippetAccess.Rows)
                {

                    try
                    {
                        WebPart wp = _manager.WebParts[Convert.ToString(dr["snippet_id"])];
                        if (wp != null)
                        {
                            //WebPartVerbCollection vcl = wp.Zone.WebParts[0].Verbs;

                            if (Convert.ToString(dr["Active_Status"]) != "1" && !wp.IsClosed)
                                _manager.CloseWebPart(wp);

                            else if (Convert.ToString(dr["access"]) != "0")
                            {
                                int i = 0;
                                foreach (System.Web.UI.WebControls.WebParts.GenericWebPart cl in wp.Parent.Controls)
                                {
                                    i++;
                                    if (cl.ChildControl.ID == Convert.ToString(dr["snippet_id"]))
                                    {
                                        if (Convert.ToString(dr["formyaction"]) == "1")
                                        {
                                            if (sbMyActionLabels.Length > 0)
                                                sbMyActionLabels.Append(",");

                                            sbMyActionLabels.Append(Convert.ToString(dr["snippet_id"]));

                                        }

                                        ((Label)cl.ChildControl).CssClass = Convert.ToString(dr["department_color"]);
                                    }
                                }

                                if (Convert.ToString(dr["Snippet_Function_Name"]).Length > 1)
                                {
                                    if (Convert.ToInt32(dr["Auto_Refresh"]) == 1)
                                    {

                                        if (Convert.ToInt32(dr["IsCountSpecific"]) == 1)
                                        {
                                            if (counter == 0)
                                            {
                                                hdnFromDays0.Value = Convert.ToString(dr["FromDay"]);
                                                hdnToDays7.Value = Convert.ToString(dr["ToDay"]);
                                                counter = counter + 1;
                                            }
                                            else if (counter == 1)
                                            {
                                                hdnFromDays8.Value = Convert.ToString(dr["FromDay"]);
                                                hdnToDays30.Value = Convert.ToString(dr["ToDay"]);
                                                counter = counter + 1;
                                            }
                                            else if (counter == 2)
                                            {
                                                hdnFromDays31.Value = Convert.ToString(dr["FromDay"]);
                                                hdnToDays90.Value = Convert.ToString(dr["ToDay"]);
                                                counter = counter + 1;
                                            }


                                            // jsFunctions_Auto_Refresh.Append(" setTimeout(" + Convert.ToString(dr["Snippet_Function_Name"]) + "(" + hdnFromDays0.Value.ToString() + "," + hdnToDays7.Value.ToString() + ")" + ",200); ");
                                            // jsFunctions_Auto_Refresh.Append(" setTimeout(" + Convert.ToString(dr["Snippet_Function_Name"]) + "(" + Convert.ToString(dr["FromDay"]) + "," + Convert.ToString(dr["ToDay"]) + ")" + ",200); ");
                                            jsFunctions_Auto_Refresh.Append(" setTimeout(" + Convert.ToString(dr["Snippet_Function_Name"]) + ",200); ");

                                        }
                                        else
                                        {
                                            jsFunctions_Auto_Refresh.Append(" setTimeout(" + Convert.ToString(dr["Snippet_Function_Name"]) + ",200); ");
                                        }
                                    }


                                    else
                                    {

                                        if (Convert.ToInt32(dr["IsCountSpecific"]) == 1)
                                        {
                                            if (counter == 0)
                                            {
                                                hdnFromDays31.Value = Convert.ToString(dr["FromDay"]);
                                                hdnToDays90.Value = Convert.ToString(dr["ToDay"]);
                                                counter = counter + 1;
                                            }
                                            else if (counter == 1)
                                            {
                                                hdnFromDays8.Value = Convert.ToString(dr["FromDay"]);
                                                hdnToDays30.Value = Convert.ToString(dr["ToDay"]);
                                                counter = counter + 1;
                                            }
                                            else if (counter == 2)
                                            {
                                                hdnFromDays0.Value = Convert.ToString(dr["FromDay"]);
                                                hdnToDays7.Value = Convert.ToString(dr["ToDay"]);
                                                counter = counter + 1;
                                            }
                                            jsFunctions_Auto_Refresh.Append(" setTimeout(" + Convert.ToString(dr["Snippet_Function_Name"]) + ",200); ");
                                        }
                                        else
                                        {
                                            jsFunctions.Append(" setTimeout(" + Convert.ToString(dr["Snippet_Function_Name"]) + ",200); ");

                                        }
                                    }
                                }

                            }

                            else if (Convert.ToString(dr["access"]) == "0" && !wp.IsClosed)
                            {
                                _manager.CloseWebPart(wp); //hide the web part based on user rights
                            }

                        }
                    }
                    catch (Exception ex) { lblErrorMessage.Text += ex.Message + ex.StackTrace; }

                }

                if (jsFunctions.Length > 0)
                {

                    String msgretv = " try { " + jsFunctions.ToString() + " } catch(ex){alert(ex._message);}  ";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Load_DashBoard", msgretv, true);
                }
                if (jsFunctions_Auto_Refresh.Length > 1)
                {
                    jsFunctions_Auto_Refresh.Insert(0, " function Refresh_Functions(){   ");
                    jsFunctions_Auto_Refresh.Append("  setTimeout('Refresh_Functions()',300000); } ");
                    jsFunctions_Auto_Refresh.Append("setTimeout(Refresh_Functions,200);");

                    String msgFunc = jsFunctions_Auto_Refresh.ToString();
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Load_DashBoard_Refresh_", msgFunc, true);
                }

                if (sbMyActionLabels.Length > 0)
                {
                    string slbl = "var ForMyActionLabels='" + sbMyActionLabels.ToString() + "';";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Load_MyActions", slbl, true);
                }
            }
        }
        catch (Exception ex) { lblErrorMessage.Text += ex.Message + ex.StackTrace; }

    }




    protected void DisplayModeDropdown_SelectedIndexChanged(object sender, EventArgs e)
    {
        //String selectedMode = DisplayModeDropdown.SelectedValue;
        //WebPartDisplayMode mode =
        //  WebPartManagerDB.SupportedDisplayModes[selectedMode];
        //if (mode != null)
        //    WebPartManagerDB.DisplayMode = mode;


    }

    protected void btnResetSnippets_Click(object sender, EventArgs e)
    {
        _manager = WebPartManager.GetCurrentWebPartManager(Page);
        _manager.Personalization.ResetPersonalizationState();
    }

    protected void btnMinimizeall_Click(object sender, EventArgs e)
    {
        foreach (WebPart wp in _manager.WebParts)
        {
            wp.ChromeState = PartChromeState.Minimized;
        }
    }

    protected void btnMaximizeall_Click(object sender, EventArgs e)
    {
        foreach (WebPart wp in _manager.WebParts)
        {
            wp.ChromeState = PartChromeState.Normal;
        }
    }



}
