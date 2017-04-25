using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.TMSA;
using System.Data;
using SMS.Properties;
using SMS.Business.Infrastructure;
public partial class TMSA_KPI_TMSA_Configure_KPI : System.Web.UI.Page
{
    BLL_TMSA_KPI objBLL = new BLL_TMSA_KPI();
    protected void Page_Load(object sender, EventArgs e)
    {
        UserAccessValidation();
        if (!IsPostBack)
        {
            BindCategory();
            BindInterval();
            ddlOperator.SelectedValue = "0";
            txtNumber.Text = "";
            LoadQueryList();
            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn("sno", typeof(int)));
            dt.Columns.Add(new DataColumn("PID", typeof(string)));
            dt.Columns.Add(new DataColumn("value", typeof(string)));
            dt.Columns.Add(new DataColumn("PIName", typeof(string)));

            // dt.PrimaryKey = new DataColumn[] { dt.Columns["sno"] };

            hdnKPIID.Value = Request.QueryString["KPI_ID"];//21
            if (hdnKPIID.Value == "0")
            {
                lblCode1.Visible = false;
              //  lblCode2.Visible = false;
                lblCode3.Visible = false;
                lblFormula1.Visible = false;
              //  lblFormula2.Visible = false;
                lblFormula3.Visible = false;
            }
            else
            {
                DataSet ds = BLL_TMSA_PI.Get_KPI_Detail(UDFLib.ConvertIntegerToNull(hdnKPIID.Value));
                DataTable dtDtl = ds.Tables[0];
                DataTable dtPIDtl = ds.Tables[1];

                txtKPIName.Text = dtDtl.Rows[0]["Name"].ToString();
                txtKPIDesc.Text = dtDtl.Rows[0]["Description"].ToString();
                txtTimePeriod.Text = dtDtl.Rows[0]["Time_Period"].ToString();
                txtMeasure.Text = dtDtl.Rows[0]["Measurement_Detail"].ToString();
                txtURL.Text = dtDtl.Rows[0]["URL"].ToString();
                lblCode3.Text = dtDtl.Rows[0]["Code"].ToString();

                if (ddlCategory.Items.FindByValue(dtDtl.Rows[0]["Category"].ToString()) != null)
                    ddlCategory.SelectedValue = dtDtl.Rows[0]["Category"].ToString() != "" ? dtDtl.Rows[0]["Category"].ToString() : "0";
                else
                    ddlCategory.SelectedValue = "0";

                //if (ddlListSource.Items.FindByValue(dtDtl.Rows[0]["DataSource"].ToString()) != null)
                //    ddlListSource.SelectedValue = dtDtl.Rows[0]["DataSource"].ToString() != "" ? dtDtl.Rows[0]["DataSource"].ToString() : "0";
                //else
                //    ddlListSource.SelectedValue = "0";


                //  ddlCategory.SelectedValue = dtDtl.Rows[0]["Category"].ToString();
                if (ddlInterval.Items.FindByValue(dtDtl.Rows[0]["Interval"].ToString()) != null)
                    ddlInterval.SelectedValue = dtDtl.Rows[0]["Interval"].ToString() != "" ? dtDtl.Rows[0]["Interval"].ToString() : "0";
                else
                    ddlInterval.SelectedValue = "0";

                if (ddlStatus.Items.FindByValue(dtDtl.Rows[0]["KPI_Status"].ToString()) != null)
                    ddlStatus.SelectedValue = dtDtl.Rows[0]["KPI_Status"].ToString() != "" ? dtDtl.Rows[0]["KPI_Status"].ToString() : "2";
                else
                    ddlStatus.SelectedValue = "2";

                if (ddlKPIApplicableFor.Items.FindByValue(dtDtl.Rows[0]["KPI_ApplicableFor"].ToString()) != null)   //Added to display the preselected value from database.
                    ddlKPIApplicableFor.SelectedValue = dtDtl.Rows[0]["KPI_ApplicableFor"].ToString() != "" ? dtDtl.Rows[0]["KPI_ApplicableFor"].ToString() : "0";

                else
                    ddlKPIApplicableFor.SelectedValue = "2";


                string exp = "";
                foreach (DataRow row in dtPIDtl.Rows)
                {
                    dt.Rows.Add(new object[] { Convert.ToInt32(row["SerialNumber"].ToString()), row["PI_ID"].ToString(), row["value"].ToString(), row["Name"].ToString() });
                    exp = exp + row["value"].ToString();
                }
                lblFormula3.Text = exp;
                dtlFormula.DataSource = dt;
                dtlFormula.DataBind();

                DataTable dt_PI = dt;
                dt_PI.DefaultView.RowFilter = "PID <> ''";
                DataList1.DataSource = dt_PI.DefaultView;
                DataList1.DataBind();


            }
            ViewState["dtFormula"] = dt;
            Bind_PI(ddlInterval.SelectedValue);

        }
    }

    public UserAccess objUA = new UserAccess();
    protected void UserAccessValidation()
    {
        int CurrentUserID = Convert.ToInt32(Session["userid"]);
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());
        BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();

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
    protected void LoadQueryList()
    {
        BLL_TMSA_PI objPIBLL = new BLL_TMSA_PI();
        ddlListSource.DataSource = objPIBLL.Get_SavedQuery("", "TMSA_KPI_Daemon_SP", GetSessionUserID());
        ddlListSource.DataTextField = "ObjectName";
        ddlListSource.DataValueField = "ObjectName";
        ddlListSource.DataBind();
        ddlListSource.Items.Insert(0, new ListItem("-SELECT-", "0"));
    }

    private void Bind_PI(string Interval)
    {
        ListItem liselect = new ListItem("--Select--", "0", true);
        ddlPIList.Items.Clear();
        ddlPIList.DataSource = objBLL.Get_AllPI_List(ddlInterval.SelectedValue);
        ddlPIList.DataTextField = "Name";
        ddlPIList.DataValueField = "ID";
        ddlPIList.DataBind();
        ddlPIList.Items.Insert(0, liselect);
        ddlPIList.SelectedValue = "0";
    }
    protected void Operator_Click(object s, EventArgs e)
    {
        if (ddlOperator.SelectedValue != "")
        {
            Rearrange();
            DataTable dtFormula = (DataTable)ViewState["dtFormula"];

            int sno = dtFormula.Rows.Count;
            //if (dtFormula.Rows.Count > 0 && ddlOperator.Items.FindByValue(dtFormula.Rows[sno - 1]["Value"].ToString()) != null && dtFormula.Rows[sno - 1]["Value"].ToString() != ")" && ddlOperator.SelectedValue !=")")
            //{
            //    string js = "alert('Invalid member sequence!');";
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "error", js, true);

            //}
            //else 
                if (dtFormula.Rows.Count == 0 && ddlOperator.SelectedValue != "(")
            {
                string js = "alert('Invalid member sequence!');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error", js, true);
            }
            else if (dtFormula.Rows.Count > 0 && ddlOperator.SelectedValue == ")")
            {
                DataRow[] filteredRows =
                dtFormula.Select("value='('");
                if (filteredRows.Count() == 0)
                {
                    string js = "alert('Invalid member sequence!');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "error", js, true);
                }

                sno++;
                dtFormula.Rows.Add(new object[] { sno, null, ddlOperator.SelectedValue, null });
                dtlFormula.DataSource = dtFormula;
                dtlFormula.DataBind();
                ViewState["dtFormula"] = dtFormula;
            }
            else
            {

                sno++;
                dtFormula.Rows.Add(new object[] { sno, null, ddlOperator.SelectedValue, null });
                dtlFormula.DataSource = dtFormula;
                dtlFormula.DataBind();
                ViewState["dtFormula"] = dtFormula;
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

    protected void PI_Click(object s, EventArgs e)
    {
        if (ddlInterval.SelectedValue == null || ddlInterval.SelectedValue == "0")
        {
            string js = "alert('Select a KPI interval!');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", js, true);

        }


        else if (ddlPIList.SelectedValue != "0")
        {
            Rearrange();
            DataTable dtFormula = (DataTable)ViewState["dtFormula"];

            int sno = dtFormula.Rows.Count;

            if (dtFormula.Rows.Count >0 && dtFormula.Rows[sno - 1]["PID"] != null && dtFormula.Rows[sno - 1]["PID"].ToString()!="")
            {
                string js = "alert('Invalid member sequence!');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error", js, true);

            }
            else
            {
                sno++;
                dtFormula.Rows.Add(new object[] { sno, ddlPIList.SelectedValue.Split(',')[0], ddlPIList.SelectedValue.Split(',')[1], ddlPIList.SelectedItem.Text });
                dtlFormula.DataSource = dtFormula;
                dtlFormula.DataBind();
                ViewState["dtFormula"] = dtFormula;

                DataTable dt = new DataTable();
                dt = dtFormula;
                dt.DefaultView.RowFilter = "PID <> ''";
                DataList1.DataSource = dt.DefaultView;
                DataList1.DataBind();
            }

        }


    }
    protected void Number_Click(object s, EventArgs e)
    {
        if (txtNumber.Text != "" && txtNumber.Text != "0")
        {
            Rearrange();
            DataTable dtFormula = (DataTable)ViewState["dtFormula"];



            int sno = dtFormula.Rows.Count;



            if (dtFormula.Rows.Count > 0 && (dtFormula.Rows[sno - 1]["PID"] != null || dtFormula.Rows[sno - 1]["Value"] != "("))
            {
                string js = "alert('Ivalid member sequence!');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error", js, true);

            }
            else
            {
                sno ++;
                dtFormula.Rows.Add(new object[] { sno, null, txtNumber.Text, "" });
                dtlFormula.DataSource = dtFormula;
                dtlFormula.DataBind();
                ViewState["dtFormula"] = dtFormula;
            }
        }
    }
    protected void imgbtnDeleteTask_Click(object s, CommandEventArgs e)
    {
        DataTable dtFormula = (DataTable)ViewState["dtFormula"];

        dtFormula.Columns.Add(new DataColumn("Position", typeof(decimal)));
        dtFormula.PrimaryKey = new DataColumn[] { dtFormula.Columns["sno"] };

        string[] posi = hdnPosition.Value.Split(',');

        for (int i = 0; i < posi.Length - 1; i++)
        {
            foreach (DataRow dr in dtFormula.Rows)
            {
                if (dr["sno"].ToString() == posi[i].Split('+')[1].ToString())
                {
                    dr["Position"] = Convert.ToDecimal(posi[i].Split('+')[0].ToString());
                    break;
                }
            }
        }
        dtFormula.Rows.Remove(dtFormula.Rows.Find(e.CommandArgument.ToString()));

        DataView dv = dtFormula.DefaultView;
        dv.Sort = "Position";
        DataTable sortedDT = dv.ToTable();
        int j=1;
        //for (int i = 1; i <= sortedDT.Rows.Count; i++)
        //{
        //    sortedDT.Rows[i - 1]["sno"] = i;
        //}

        foreach (DataRow dr in sortedDT.Rows)
        {
            dr["sno"]=j;
            j++;
        }

        sortedDT.Columns.Remove("Position");
        dtlFormula.DataSource = sortedDT;
        dtlFormula.DataBind();
        ViewState["dtFormula"] = sortedDT;

        DataTable dt1 = new DataTable();
        dt1 = sortedDT;
        dt1.DefaultView.RowFilter = "PID <> ''";
        DataList1.DataSource = dt1.DefaultView;
        DataList1.DataBind();

    }
    private void Rearrange()
    {
        DataTable dtFormula = (DataTable)ViewState["dtFormula"];

        dtFormula.Columns.Add(new DataColumn("Position", typeof(decimal)));

        string[] posi = hdnPosition.Value.Split(',');

        for (int i = 0; i < posi.Length - 1; i++)
        {
            foreach (DataRow dr in dtFormula.Rows)
            {
                 if (dr["sno"].ToString() == posi[i].Split('+')[1].ToString())
                {
                    dr["Position"] = Convert.ToDecimal(posi[i].Split('+')[0].ToString());
                    break;
                }
            }
        }

        DataView dv = dtFormula.DefaultView;
        dv.Sort = "Position";
        DataTable sortedDT = dv.ToTable();

        int j = 1;

        foreach (DataRow dr in sortedDT.Rows)
        {
            dr["sno"] = j;
            j++;
        }

        sortedDT.Columns.Remove("Position");
        ViewState["dtFormula"] = sortedDT;
    }
    protected void txtSave_Click(object s, EventArgs e)
    {
        try
        {
            Rearrange();
            if (ValidateFormula())
            {

                DataTable dtFormula = (DataTable)ViewState["dtFormula"];
                string exp = "";
                for (int i = 1; i <= dtFormula.Rows.Count; i++)
                {
                    exp = exp + dtFormula.Rows[i - 1]["value"].ToString();
                }

                if (exp != "")
                {
                    DataTable retval = objBLL.UPD_KPIDetails(UDFLib.ConvertIntegerToNull(hdnKPIID.Value), dtFormula, txtKPIName.Text.Trim(), txtKPIDesc.Text.Trim(), ddlInterval.SelectedValue, txtTimePeriod.Text.Trim(), txtMeasure.Text.Trim(),
                        ddlListSource.SelectedValue, Convert.ToInt32(Session["USERID"].ToString()), Convert.ToInt32(ddlStatus.SelectedValue), ddlCategory.SelectedValue, txtURL.Text, UDFLib.ConvertToInteger(ddlKPIApplicableFor.SelectedValue));
                    dtlFormula.DataSource = dtFormula;
                    dtlFormula.DataBind();

                    if (hdnKPIID.Value == "" || hdnKPIID.Value == "0")
                    {
                        lblCode1.Visible = true;
                        //lblCode2.Visible = true;
                        lblCode3.Visible = true;
                        lblFormula1.Visible = true;
                       // lblFormula2.Visible = true;
                        lblFormula3.Visible = true;
                        hdnKPIID.Value = retval.Rows[0]["KPI_ID"].ToString();
                        lblCode3.Text = retval.Rows[0]["KPI_Code"].ToString();
                        lblMessage.Text = "KPI created successfully!";
                    }
                    else
                        lblMessage.Text = "KPI updated successfully!";
                    lblFormula3.Text = exp;
                    clearControls();
                }
                else
                    lblMessage.Text = "Formula cannot be blank!";
            }
            else
            {
                lblMessage.Text = "Formula is not correct!";
            }
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.ToString();
        }
    }

    private void clearControls()
    {
        //txtKPIName.Text = "";
        //txtKPIDesc.Text = "";
        //txtMeasure.Text = "";
        //txtNumber.Text = "";
        //lblFormula3.Text = "";
        //ddlInterval.SelectedIndex = -1;
        //ddlOperator.SelectedIndex = -1;
        //ddlPIList.SelectedIndex = -1;
        //ddlStatus.SelectedIndex = -1;
        
        //DataList1.Visible = false;

    }



    private bool ValidateFormula()
    {
        try
        {
            DataTable dtFormula = (DataTable)ViewState["dtFormula"];
            string exp = "";
            bool ivalidate = true;
            int j = 1;
            for (int i = 1; i <= dtFormula.Rows.Count; i++)
            {
                int n;
                if (dtFormula.Rows[i - 1]["PID"].ToString() != "" || int.TryParse(dtFormula.Rows[i - 1]["value"].ToString(), out n))
                {
                    string chari = ((char)(96 + j)).ToString();
                    exp = exp + chari;
                    j = j + 1;
                }
                else if (i == 1 && (dtFormula.Rows[i - 1]["value"].ToString() == "+" || dtFormula.Rows[i - 1]["value"].ToString() == "-"))
                {

                }
                else
                    exp = exp + dtFormula.Rows[i - 1]["value"].ToString();
            }

            if (exp.IndexOf('^') > 0)
            {
                string res = validatePower(exp);
                ivalidate = Convert.ToBoolean(res.Split(',')[1]);
                exp = res.Split(',')[0];
            }

            if (ivalidate == true)
                ivalidate = validate(exp);

            return ivalidate;
        }
        catch (Exception ex)
        {
            return false;
        }
    }
    public static string validatePower(string exp)
    {
        bool ivalidate = true;
        string nexti = exp.Substring(exp.IndexOf('^') + 1, 1);
        if (nexti == "(")
        {
            int j = exp.IndexOf(')', exp.IndexOf('^'));
            if (j == -1)
                ivalidate = false;

            string substring = exp.Substring(exp.IndexOf('^') + 1, j - exp.IndexOf('^'));

            if (substring.IndexOf('^') > 0)
            {
                //validatePower(substring);
                string res = validatePower(substring);
                ivalidate = Convert.ToBoolean(res.Split(',')[1]);
                exp = exp.Replace(substring, res.Split(',')[0]);
                substring = res.Split(',')[0];
            }

            if (substring.Substring(1, 1) == "+" || substring.Substring(1, 1) == "-")
            {
                if (validate(substring.Substring(2, substring.Length - 3)) == true)
                    exp = exp.Replace("^" + substring, "");
                else
                    ivalidate = false;
            }
            else if (validate(substring) == true)
            {
                exp = exp.Replace("^" + substring, "");
            }
        }
        else if (nexti == ")" || nexti == "/" || nexti == "*" || nexti == "^")
            ivalidate = false;
        else if (nexti == "+" || nexti == "-")
        {
            string nextj = exp.Substring(exp.IndexOf('^') + 2, 1);
            if (nextj == "/" || nextj == "*" || nextj == "^" || nextj == "+" || nextj == "-" || nextj == ")")//
                ivalidate = false;
            else
                exp = exp.Replace(exp.Substring(exp.IndexOf('^'), 3), "");
        }
        else
        {
            exp = exp.Replace(exp.Substring(exp.IndexOf('^'), 2), "");
        }

        if (exp.IndexOf('^') > 0)
        {
            //validatePower(exp);
            string res = validatePower(exp);
            ivalidate = Convert.ToBoolean(res.Split(',')[1]);
            exp = res.Split(',')[0];
        }
        return exp + "," + ivalidate.ToString();

    }
    public static bool validate(string expression)
    {
        int previous = 0;
        int previous1 = 0;
        string expEvaluated = string.Empty;
        int operatorOperand = 1;

        for (int i = 0; i < expression.Length; i++)
        {
            char c = expression[i];
            if (c == ')')
            {
            }
            else if (c == '(')
            {
                int j = expression.IndexOf(')', i);
                if (j == -1)
                    return false;

                string substring = expression.Substring(i + 1, j - i - 1);
                while (getcharactercount(substring, '(') != getcharactercount(substring, ')'))
                {
                    if (j < expression.Length - 1)
                        j = expression.IndexOf(')', j + 1);
                    else
                        break;

                    substring = expression.Substring(i + 1, j - i - 1);
                } i = j - 1; //Changing the counter i to point to the next character                   
                //validating the sub expression                   

                if (validate(substring) == true)
                {
                    if (previous != 0 && previous1 != 0 && previous > previous1)
                    {
                        previous1 = operatorOperand;
                        operatorOperand++;
                        previous = 0;
                    }
                    else if (previous != 0 && previous1 != 0 && previous <= previous1)
                    {
                        return false;
                    }
                    else if (previous1 != 0)
                    {
                        return false;
                    }
                    else
                    {
                        previous1 = operatorOperand;
                        operatorOperand++;
                    }
                }
                else
                {
                    return false;
                }
            }
            else if (c == '+' || c == '-' || c == '*' || c == '/')
            {
                if (previous != 0)
                {
                    return false;
                }
                previous = operatorOperand;
                operatorOperand++;
            }
            else
            {
                if (previous != 0 && previous1 != 0 && previous > previous1)
                {
                    previous1 = operatorOperand;
                    operatorOperand++;
                    previous = 0;
                }
                else if (previous != 0 && previous1 != 0 && previous <= previous1)
                {
                    return false;
                }
                else if (previous1 != 0)
                {
                    return false;
                }
                else
                {
                    previous1 = operatorOperand;
                    operatorOperand++;
                }
            }
        }

        if (previous != 0)
            return false;

        return true;

    }
    public static int getcharactercount(string exp, char _c)
    {
        int count = 0;
        foreach (char c in exp)
        {
            if (c == _c)
                count++;
        }
        return count;
    }

    private void BindCategory()
    {
        ListItem liselect = new ListItem("--Select--", "0", true);
        DataTable dt = objBLL.Get_CategoryList("");
        ddlCategory.DataSource = dt;
        ddlCategory.DataTextField = "Category_Name";
        ddlCategory.DataValueField = "Category_Name";
        ddlCategory.DataBind();
        ddlCategory.Items.Insert(0, liselect);
        ddlCategory.SelectedValue = "0";
    }
    private void BindInterval()
    {
        ListItem liselect = new ListItem("--Select--", "0", true);
        DataTable dt = objBLL.Get_Intervals("");
        ddlInterval.DataSource = dt;
        ddlInterval.DataTextField = "Interval_Name";
        ddlInterval.DataValueField = "Interval_Name";
        ddlInterval.DataBind();
        ddlInterval.Items.Insert(0, liselect);
        ddlInterval.SelectedValue = "0";
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        string msg2 = "OpenGoal()";
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg2, true);
    }

    protected void ddlInterval_SelectedIndexChanged(object sender, EventArgs e)
    {
        Bind_PI(ddlInterval.SelectedValue);
    }
      
}