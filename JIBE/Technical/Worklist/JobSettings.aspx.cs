using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.Technical;
using System.Data;
using System.Text;
using System.Data.SqlClient;

public partial class Technical_Worklist_JobSettings : System.Web.UI.Page
{
    BLL_Tec_Worklist objBLL = new BLL_Tec_Worklist();

    public string OperationMode = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            UserAccessValidation();
            BindItems();
        }
    }

    protected void UserAccessValidation()
    {
        int CurrentUserID = GetSessionUserID();
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

        SMS.Properties.UserAccess objUA = new SMS.Properties.UserAccess();
        SMS.Business.Infrastructure.BLL_Infra_UserCredentials objUser = new SMS.Business.Infrastructure.BLL_Infra_UserCredentials();
        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

        if (objUA.Admin == 0)
        {
            addnewSetting.Visible = false;

        }
        if (objUA.View == 0)
        {
            Response.Redirect("~/default.aspx?msgid=1");
        }

    }

    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
        {
            Response.Redirect("~/account/Login.aspx");
            return 0;
        }
    }
    public void BindItems()
    {
        DataTable _dtfunctionTable;

        try
        {
            string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
            int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());

            _dtfunctionTable = objBLL.GetSettingforFunctions();
            if (_dtfunctionTable.Rows.Count > 0)
            {
                for (int i = 0; i < _dtfunctionTable.Rows.Count; i++)
                {
                    if (_dtfunctionTable.Rows[i]["Settings_Key"].ToString() == "View Functions To Jobs")
                    {
                        trVisible1.Visible = true;
                        lblDescriptionJob.Text = _dtfunctionTable.Rows[i]["Descriptions"].ToString().Replace("_", " ");
                        if (_dtfunctionTable.Rows[i]["Settings_Value"].ToString().Trim() == "1")
                        {
                            chkbSettingValue.Checked = true;
                        }
                        else
                        {
                            chkbSettingValue.Checked = false;
                        }
                    }
                    else if (_dtfunctionTable.Rows[i]["Settings_Key"].ToString() == "JOB_DUE_IN_7_DAYS")
                    {
                        trVisible2.Visible = true;
                        lblDescription7Day.Text = _dtfunctionTable.Rows[i]["Settings_Key"].ToString().Replace("_", " ");
                        txtSettingValue7Day.Text = _dtfunctionTable.Rows[i]["Settings_Value"].ToString();

                    }
                    else if (_dtfunctionTable.Rows[i]["Settings_Key"].ToString() == "JOB_DUE_THIS_MONTH")
                    {
                        trVisible3.Visible = true;
                        lblDescriptionMonth.Text = _dtfunctionTable.Rows[i]["Settings_Key"].ToString().Replace("_", " ");
                        txtSettingValueMonth.Text = _dtfunctionTable.Rows[i]["Settings_Value"].ToString();
                    }


                }

            }
            if (_dtfunctionTable.Rows.Count == 0)
            {
                divAddSetting.Visible = false;
            }

        }

        catch (Exception ex)
        {
            throw ex;
        }


    }


    protected void addnewSetting_Click(object sender, EventArgs e)
    {
        try
        {
            if (ValidateControls())
            {
                DataTable BindTable = new DataTable();
                DataTable dt = new DataTable();
                string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
                int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());

                string ChkboxValue = "";


                BindTable = objBLL.GetSettingforFunctions();
                dt.Columns.AddRange(new DataColumn[2] { new DataColumn("Code"), new DataColumn("Value") });

                if (BindTable.Rows.Count > 0)
                {
                    for (int i = 0; i < BindTable.Rows.Count; i++)
                    {
                        if (BindTable.Rows[i]["Settings_Key"].ToString() == "View Functions To Jobs")
                        {
                            if (chkbSettingValue.Checked)
                            {
                                ChkboxValue = "1";
                                int Code1 = Convert.ToInt32(BindTable.Rows[i]["TEC_Lib_Settings_CODE"].ToString());
                                int value1 = Convert.ToInt32(ChkboxValue);
                                dt.Rows.Add(Code1, value1);
                            }
                            else
                            {
                                ChkboxValue = "0";
                                int Code1 = Convert.ToInt32(BindTable.Rows[i]["TEC_Lib_Settings_CODE"].ToString());
                                int value1 = Convert.ToInt32(ChkboxValue);
                                dt.Rows.Add(Code1, value1);
                            }
                        }
                        else if (BindTable.Rows[i]["Settings_Key"].ToString() == "JOB_DUE_IN_7_DAYS")
                        {
                            int Code2 = Convert.ToInt32(BindTable.Rows[i]["TEC_Lib_Settings_CODE"].ToString());
                            int value2 = Convert.ToInt32(txtSettingValue7Day.Text.Trim());
                            dt.Rows.Add(Code2, value2);
                        }

                        else if (BindTable.Rows[i]["Settings_Key"].ToString() == "JOB_DUE_THIS_MONTH")
                        {
                            int Code3 = Convert.ToInt32(BindTable.Rows[i]["TEC_Lib_Settings_CODE"].ToString());
                            int value3 = Convert.ToInt32(txtSettingValueMonth.Text.Trim());
                            dt.Rows.Add(Code3, value3);
                        }
                    }
                }
               
                int result = objBLL.SaveJobFunctionSetting(dt, Convert.ToInt32(Session["USERID"]));

                if (result > 0)
                {
                    string JoiningType = String.Format("hideModal('divadd',false);alert('" + UDFLib.GetException("SuccessMessage/UpdateMessage") + "');");
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "JoiningType", JoiningType, true);
                }
                // BindItems();
            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string js = "alert('" + UDFLib.GetException("SystemError/ GeneralMessage") + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", js, true);
        }

    }

    private bool ValidateControls()
    {
        try
        {
            string strSettingValue1 = txtSettingValue7Day.Text.Trim();
            string strSettingValue2 = txtSettingValueMonth.Text.Trim();
            if (trVisible2.Visible == true)
            {

                if (strSettingValue1 == "")
                {
                    string js1 = "alert('Please enter settings value.');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertFun", js1, true);
                    return false;
                }
            }
            if (trVisible3.Visible == true)
            {
                if (strSettingValue2 == "")
                {
                    string js2 = "alert('Please enter settings value.');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertFun", js2, true);
                    return false;
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return true;
    }


}