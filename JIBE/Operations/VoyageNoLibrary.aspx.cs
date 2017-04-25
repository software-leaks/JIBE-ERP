using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Data;
using System.Reflection;
using System.Collections;
using SMS.Properties;
using SMS.Business.Infrastructure;
using SMS.Business.Operation;

public partial class Operations_VoyageNoLibrary : System.Web.UI.Page
{

    public UserAccess objUA = new UserAccess();
    BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
    public class ColorList
    {
        string ColorName;
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState["Mode"] = "";
            UserAccessValidation();
            BindVoyageNumber();
        }

      
            txtValue.Visible = false;
            btnVoyageIdSave.Visible = false;
            tdV_No.Visible = false;
       
      

    }

    protected void UserAccessValidation()
    {

        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

        objUA = objUser.Get_UserAccessForPage(Convert.ToInt32(Session["USERID"]), PageURL);

        if (objUA.View == 0)
        {
            Response.Redirect("~/crew/default.aspx?msgid=1");
        }

        if (objUA.Add == 0)
        {

        }
    }
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        txtfilter.Text = "";       
        BindVoyageNumber();

    }

    protected void btnFilter_Click(object sender, EventArgs e)
    {
        BindVoyageNumber();
    }
    public void BindVoyageId()
    {
        try
        {
            DataTable dt = new DataTable();
            int id = 0;
            dt = BLL_OPS_VoyageReports.Get_VoyageNo(id);
            grdVoyageId.DataSource = dt;
            grdVoyageId.DataBind();
            //  updCat.Update();
        }
        catch (Exception)
        {

            throw;
        }


    }



    public void BindVoyageNumber()
    {

        int rowcount = ucCustomPagerItems.isCountRecord;

        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());


        DataTable dt = BLL_OPS_VoyageReports.BindVoyageNumber(txtfilter.Text != "" ? txtfilter.Text : null, sortbycoloumn, sortdirection
             , ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref  rowcount);


        if (ucCustomPagerItems.isCountRecord == 1)
        {
            ucCustomPagerItems.CountTotalRec = rowcount.ToString();
            ucCustomPagerItems.BuildPager();
        }


        if (dt.Rows.Count > 0)
        {
            grdVoyageId.DataSource = dt;
            grdVoyageId.DataBind();
        }
        else
        {
            grdVoyageId.DataSource = dt;
            grdVoyageId.DataBind();
        }

    }

    protected void btnVoyageIdSave_Click(object sender, EventArgs e)
    {

        try
        {
            int CreatedBy, ModifiedBy;
            if (ViewState["Mode"].ToString() == "Add")
            {
                if (txtValue.Text.ToString().Trim() != "")
                {
                    CreatedBy = UDFLib.ConvertToInteger(Session["userid"].ToString());
                    int reslt = BLL_OPS_VoyageReports.Insert_VoyageNo(txtValue.Text.Trim(), CreatedBy);
                    if (reslt == 1)
                    {
                        string js2 = "alert('Voyage Number saved successfully..!');";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert9", js2, true);
                        ViewState["ID"] = "";
                        ViewState["Mode"] = "";
                    }
                    else if (reslt == 0)
                    {
                        string js2 = "alert('Voyage Number is already exist..!');";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert9", js2, true);
                    }
                    else 
                    {
                        string js2 = "alert('Voyage Number saving unsuccessful..!');";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert9", js2, true);
                    }

                }
                else
                {
                    string js2 = "alert('Voyage Number should not be blank...!');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert9", js2, true);
                }

            }
            else if (ViewState["Mode"].ToString() == "EDIT")
            {
                if (txtValue.Text.ToString().Trim() != "")
                {
                    int ID = Convert.ToInt32(ViewState["ID"].ToString());

                    ModifiedBy = UDFLib.ConvertToInteger(Session["userid"].ToString());
                    int reslt = BLL_OPS_VoyageReports.Update_VoyageNo(txtValue.Text.Trim(), ModifiedBy, ID);
                    if (reslt == 1)
                    {
                        string js2 = "alert('Voyage Number Updated Successfully');";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert9", js2, true);
                        ViewState["ID"] = "";
                        ViewState["Mode"] = "";
                    }
                    else if (reslt == 0)
                    {
                        string js2 = "alert('Voyage Number is already exist..!');";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert9", js2, true);
                    }
                    else
                    {
                        string js2 = "alert('Voyage Number Updated Unsuccessful');";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert9", js2, true);
                    }
                }
                else
                {
                    string js2 = "alert('Voyage Number should not be blank...!');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert9", js2, true);
                }
            }

         

            txtValue.Text = "";
            txtfilter.Text = "";

           BindVoyageNumber();
           updCat.Update();
        }
        catch (Exception)
        {

            throw;
        }

    }
    protected void btnRatingAdd_Click(object sender, EventArgs e)
    {

    }
    protected void ImgVoyageRestore_Click(object sender, CommandEventArgs e)
    {
        int ID = Convert.ToInt32(e.CommandArgument.ToString());
        BLL_OPS_VoyageReports.DeleteRestore_VoyageNo(UDFLib.ConvertToInteger(Session["userid"].ToString()), ID);
        txtValue.Text = "";
        BindVoyageNumber();

    }
    protected void ImgVoyageDelete_Click(object sender, CommandEventArgs e)
    {
        int ID = Convert.ToInt32(e.CommandArgument.ToString());
        BLL_OPS_VoyageReports.Delete_VoyageNo(UDFLib.ConvertToInteger(Session["userid"].ToString()), ID);
        txtValue.Text = "";
        BindVoyageNumber();


    }
    protected void ImgAdd_Click(object sender, EventArgs e)
    {
        ViewState["Mode"] = "Add";
        ViewState["ID"] = ""; 
        txtValue.Text = "";
        txtfilter.Text = "";
        txtValue.Visible = true;
        btnVoyageIdSave.Visible = true;
        tdV_No.Visible = true;
    }
    protected void ImgEdit_Click(object sender, CommandEventArgs e)
    {
        int ID = Convert.ToInt32(e.CommandArgument.ToString());

        DataTable dt = new DataTable();
        dt = BLL_OPS_VoyageReports.Get_VoyageNo(ID);

        if (dt.Rows.Count > 0)
        {
            txtValue.Text = dt.Rows[0]["Voyage_Number"].ToString();
            // ddlColor.SelectedItem.Text = ds.Tables[0].Rows[0][3].ToString();
        }
        ViewState["Mode"] = "EDIT";
        ViewState["ID"] = ID;
        txtValue.Visible = true;
        btnVoyageIdSave.Visible = true;
        tdV_No.Visible = true;
         updCat.Update();
    }
    protected void grdVoyageId_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HiddenField hdnActiveStatus = (HiddenField)e.Row.FindControl("hdnActiveStatus");
                if (hdnActiveStatus.Value == "Active")
                {
                    ImageButton ImgRestore = (ImageButton)e.Row.FindControl("ImgVoyageRestore");
                    ImageButton ImgEdit = (ImageButton)e.Row.FindControl("ImgEdit");
                    ImgEdit.Visible = true;
                    ImgRestore.Visible = false;

                    e.Row.Cells[1].ForeColor = Color.Green; // Color.FromName(e.Row.Cells[2].Text);

                }
                if (hdnActiveStatus.Value == "Deleted")
                {
                    ImageButton ImgDelete = (ImageButton)e.Row.FindControl("ImgVoyageDelete");
                    ImageButton ImgEdit = (ImageButton)e.Row.FindControl("ImgEdit");
                    e.Row.Cells[1].ForeColor = Color.Red;
                    ImgDelete.Visible = false;
                    ImgEdit.Visible = false;


                }
            }

        }
        catch (Exception)
        {

            throw;
        }
    }
 
}