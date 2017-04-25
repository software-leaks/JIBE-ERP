using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.Crew;
using System.Data;


public partial class Crew_PreJoiningChecklist : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Load_ChecklistForEdit();
        }
    }

    protected void Load_ChecklistForEdit()
    {
        BLL_Crew_CrewDetails objCrewBLL = new BLL_Crew_CrewDetails();
        DataTable dt = objCrewBLL.Get_CrewJoiningChecklist(int.Parse(getQueryString("CrewID")));
        pnlEditCheckList.Visible = true;
        pnlViewCheckList.Visible = false;
        btnSave.Visible = true;

        if (dt.Rows.Count > 0)
        {
            foreach (DataRow dr in dt.Rows)
            {
                switch (int.Parse(dr["QuestionID"].ToString()))
                {
                    case 1:
                        rdoQ1.SelectedValue = dr["Answer"].ToString();
                        txtRemark1.Text = dr["Remark"].ToString();
                        break;
                    case 2:
                        rdoQ2.SelectedValue = dr["Answer"].ToString();
                        txtRemark2.Text = dr["Remark"].ToString();
                        break;
                    case 3:
                        rdoQ3.SelectedValue = dr["Answer"].ToString();
                        txtRemark3.Text = dr["Remark"].ToString();
                        break;
                    case 4:
                        rdoQ4.SelectedValue = dr["Answer"].ToString();
                        txtRemark4.Text = dr["Remark"].ToString();
                        break;
                    case 5:
                        rdoQ5.SelectedValue = dr["Answer"].ToString();
                        txtRemark5.Text = dr["Remark"].ToString();
                        break;
                    case 6:
                        rdoQ6.SelectedValue = dr["Answer"].ToString();
                        txtRemark6.Text = dr["Remark"].ToString();

                        break;
                    case 7:
                        rdoQ7.SelectedValue = dr["Answer"].ToString();
                        txtRemark7.Text = dr["Remark"].ToString();

                        break;
                    case 8:
                        rdoQ8.SelectedValue = dr["Answer"].ToString();
                        txtRemark8.Text = dr["Remark"].ToString();

                        break;
                    case 9:
                        rdoQ9.SelectedValue = dr["Answer"].ToString();
                        txtRemark9.Text = dr["Remark"].ToString();

                        break;
                    case 10:
                        rdoQ10.SelectedValue = dr["Answer"].ToString();
                        txtRemark10.Text = dr["Remark"].ToString();

                        break;
                    case 11:
                        rdoQ11.SelectedValue = dr["Answer"].ToString();
                        txtRemark11.Text = dr["Remark"].ToString();

                        break;
                    case 12:
                        rdoQ12.SelectedValue = dr["Answer"].ToString();
                        txtRemark12.Text = dr["Remark"].ToString();

                        break;
                    case 13:
                        rdoQ13.SelectedValue = dr["Answer"].ToString();
                        txtRemark13.Text = dr["Remark"].ToString();

                        break;
                }
            }
        }
    }

    protected void Load_ChecklistForPrinting()
    {
        BLL_Crew_CrewDetails objCrewBLL = new BLL_Crew_CrewDetails();
        DataTable dt = objCrewBLL.Get_CrewJoiningChecklist(int.Parse(getQueryString("CrewID")));
        pnlEditCheckList.Visible = false;
        pnlViewCheckList.Visible = true;
        btnSave.Visible = false;

        if (dt.Rows.Count > 0)
        {
            foreach (DataRow dr in dt.Rows)
            {
                switch (int.Parse(dr["QuestionID"].ToString()))
                {
                    case 1:
                        lblAns1.Text = (dr["Answer"].ToString()=="1")?"Yes":"No";
                        lblRemarks1.Text = dr["Remark"].ToString();
                        break;
                    case 2:
                        lblAns2.Text = (dr["Answer"].ToString() == "1") ? "Yes" : "No";
                        lblRemarks2.Text = dr["Remark"].ToString();
                        break;
                    case 3:
                        lblAns3.Text = (dr["Answer"].ToString() == "1") ? "Yes" : "No";
                        lblRemarks3.Text = dr["Remark"].ToString();
                        break;
                    case 4:
                        lblAns4.Text = (dr["Answer"].ToString() == "1") ? "Yes" : "No";
                        lblRemarks4.Text = dr["Remark"].ToString();
                        break;
                    case 5:
                        lblAns4.Text = (dr["Answer"].ToString() == "1") ? "Yes" : "No";
                        lblRemarks5.Text = dr["Remark"].ToString();
                        break;
                    case 6:
                        lblAns5.Text = (dr["Answer"].ToString() == "1") ? "Yes" : "No";
                        lblRemarks6.Text = dr["Remark"].ToString();
                        break;
                    case 7:
                        lblAns6.Text = (dr["Answer"].ToString() == "1") ? "Yes" : "No";
                        lblRemarks7.Text = dr["Remark"].ToString();
                        break;
                    case 8:
                        lblAns8.Text = (dr["Answer"].ToString() == "1") ? "Yes" : "No";
                        lblRemarks8.Text = dr["Remark"].ToString();
                        break;
                    case 9:
                        lblAns9.Text = (dr["Answer"].ToString() == "1") ? "Yes" : "No";
                        lblRemarks9.Text = dr["Remark"].ToString();
                        break;
                    case 10:
                        lblAns10.Text = (dr["Answer"].ToString() == "1") ? "Yes" : "No";
                        lblRemarks10.Text = dr["Remark"].ToString();
                        break;
                    case 11:
                        lblAns11.Text = (dr["Answer"].ToString() == "1") ? "Yes" : "No";
                        lblRemarks11.Text = dr["Remark"].ToString();
                        break;
                    case 12:
                        lblAns12.Text = (dr["Answer"].ToString() == "1") ? "Yes" : "No";
                        lblRemarks12.Text = dr["Remark"].ToString();
                        break;
                    case 13:
                        lblAns13.Text = (dr["Answer"].ToString() == "1") ? "Yes" : "No";
                        lblRemarks13.Text = dr["Remark"].ToString();
                        break;
                }
            }
        }
    }

    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }

    private string getQueryString(string QueryField)
    {
        try
        {
            if (Request.QueryString[QueryField] != null && Request.QueryString[QueryField].ToString() != "")
            {
                return Request.QueryString[QueryField].ToString();
            }
            else
                return "";
        }
        catch
        {
            return "";
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            BLL_Crew_CrewDetails objCrewBLL = new BLL_Crew_CrewDetails();

            objCrewBLL.UPDATE_CrewJoiningChecklist(int.Parse(getQueryString("CrewID")), 1, int.Parse(rdoQ1.SelectedValue), txtRemark1.Text, GetSessionUserID());
            objCrewBLL.UPDATE_CrewJoiningChecklist(int.Parse(getQueryString("CrewID")), 2, int.Parse(rdoQ2.SelectedValue), txtRemark2.Text, GetSessionUserID());
            objCrewBLL.UPDATE_CrewJoiningChecklist(int.Parse(getQueryString("CrewID")), 3, int.Parse(rdoQ3.SelectedValue), txtRemark3.Text, GetSessionUserID());
            objCrewBLL.UPDATE_CrewJoiningChecklist(int.Parse(getQueryString("CrewID")), 4, int.Parse(rdoQ4.SelectedValue), txtRemark4.Text, GetSessionUserID());
            objCrewBLL.UPDATE_CrewJoiningChecklist(int.Parse(getQueryString("CrewID")), 5, int.Parse(rdoQ5.SelectedValue), txtRemark5.Text, GetSessionUserID());
            objCrewBLL.UPDATE_CrewJoiningChecklist(int.Parse(getQueryString("CrewID")), 6, int.Parse(rdoQ6.SelectedValue), txtRemark6.Text, GetSessionUserID());
            objCrewBLL.UPDATE_CrewJoiningChecklist(int.Parse(getQueryString("CrewID")), 7, int.Parse(rdoQ7.SelectedValue), txtRemark7.Text, GetSessionUserID());
            objCrewBLL.UPDATE_CrewJoiningChecklist(int.Parse(getQueryString("CrewID")), 8, int.Parse(rdoQ8.SelectedValue), txtRemark8.Text, GetSessionUserID());
            objCrewBLL.UPDATE_CrewJoiningChecklist(int.Parse(getQueryString("CrewID")), 9, int.Parse(rdoQ9.SelectedValue), txtRemark9.Text, GetSessionUserID());
            objCrewBLL.UPDATE_CrewJoiningChecklist(int.Parse(getQueryString("CrewID")), 10, int.Parse(rdoQ10.SelectedValue), txtRemark10.Text, GetSessionUserID());
            objCrewBLL.UPDATE_CrewJoiningChecklist(int.Parse(getQueryString("CrewID")), 11, int.Parse(rdoQ11.SelectedValue), txtRemark11.Text, GetSessionUserID());
            objCrewBLL.UPDATE_CrewJoiningChecklist(int.Parse(getQueryString("CrewID")), 12, int.Parse(rdoQ12.SelectedValue), txtRemark12.Text, GetSessionUserID());
            objCrewBLL.UPDATE_CrewJoiningChecklist(int.Parse(getQueryString("CrewID")), 13, int.Parse(rdoQ13.SelectedValue), txtRemark13.Text, GetSessionUserID());

            string js = "alert('Checklist saved successfully');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertUser", js, true);

        }
        catch(Exception ex)
        {
            string js = "alert('Error in saving checklist. Error: " + ex.Message.Replace("'", "") + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertUser", js, true);
        }



    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Load_ChecklistForEdit();
    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        Load_ChecklistForPrinting();
        string js = "PrintDiv('page-content');";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "print", js, true);
    }

}