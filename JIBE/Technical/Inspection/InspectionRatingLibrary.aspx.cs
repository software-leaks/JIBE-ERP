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
using SMS.Business.Inspection;
public partial class Technical_Worklist_InspectionRatingLibrary : System.Web.UI.Page
{

    BLL_Tec_Inspection objInsp = new BLL_Tec_Inspection();
    DataSet dsRating = new DataSet();
    DataSet dsColor = new DataSet();
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
            BindRating();
            BindColor();
            ViewState["Mode"] = "";
            btnRatingSave.Enabled = false;
        }
        UserAccessValidation();
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
    public void BindRating()
    {
        dsRating.Clear();
        dsRating = objInsp.GetRatings();


        if (dsRating.Tables[1].Rows.Count > 0)
        {
            grdRating.DataSource = dsRating.Tables[1];
            grdRating.DataBind();
        }
    }

    public void BindColor()
    {
       //dsColor.Clear();
        ddlColor.DataSource = finalColorList();
        ddlColor.DataBind();
    }

    private List<string> finalColorList()
    {
       
        string[] allColors = Enum.GetNames(typeof(System.Drawing.KnownColor));
        string[] systemEnvironmentColors =
            new string[(
            typeof(System.Drawing.SystemColors)).GetProperties().Length];

        int index = 0;
        
        foreach (MemberInfo member in (
            typeof(System.Drawing.SystemColors)).GetProperties())
        {
            systemEnvironmentColors[index++] = member.Name;
        }

        List<string> finalColorList = new List<string>();
        finalColorList.Clear();
        finalColorList.Add("--SELECT--");
        foreach (string color in allColors)
        {
            if (Array.IndexOf(systemEnvironmentColors, color) < 0)
            {
                finalColorList.Add(color);
            }
        }
        return finalColorList;
    }
    protected void btnRatingSave_Click(object sender, EventArgs e)
    {
        string RatingValue, Rating, RatingColor, CreatedBy,ModifiedBy;
        int ActiveStatus;
        DateTime DateOfCreation,DateOfModification;
        


   
            Rating = txtRating.Text;
            RatingValue = txtValue.Text;
            RatingColor = ddlColor.SelectedItem.Text;
           
            ActiveStatus = 1;
        
            if (ViewState["Mode"].ToString() == "ADD")
            {
                DateOfCreation = DateTime.Now;
                CreatedBy =Session["userid"].ToString();
                objInsp.InsertRating(Rating, RatingValue, RatingColor, CreatedBy, DateOfCreation, ActiveStatus);
                string js2 = "alert('Rating Saved Successfully');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert9", js2, true);
            }
            else if (ViewState["Mode"].ToString() == "EDIT")
            {
                int ID = Convert.ToInt32(ViewState["RatingID"].ToString());
                DateOfModification = DateTime.Now;
                ModifiedBy = Session["userid"].ToString();
                objInsp.UpdateRating(ID, Rating, Convert.ToInt32(RatingValue), RatingColor, ModifiedBy, DateOfModification, ActiveStatus);
                string js2 = "alert('Rating Updated Successfully');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert9", js2, true);
            }
           
            BindRating();
            ViewState["RatingID"] = "";
            ViewState["Mode"] = "";
            txtRating.Text = "";
            txtValue.Text = "";

           // ddlColor.SelectedItem.Text = "--SELECT--";
            BindColor();    
        updCat.Update();
        btnRatingSave.Enabled = false;
    }
    protected void btnRatingAdd_Click(object sender, EventArgs e)
    {
        txtRating.Text = "";
        txtValue.Text = "";
        ddlColor.SelectedItem.Text = "--SELECT--";
        ViewState["Mode"] = "ADD";
        btnRatingSave.Enabled = true;

    }
    protected void ImgRatingRestore_Click(object sender, CommandEventArgs e)
    {
        int ID = Convert.ToInt32(e.CommandArgument.ToString());
        objInsp.DeleteRestoreRating(ID, true, Session["userid"].ToString(), DateTime.Now);
        BindRating();
    }
    protected void ImgRatingDelete_Click(object sender, CommandEventArgs e)
    {

        int ID = Convert.ToInt32(e.CommandArgument.ToString());
        objInsp.DeleteRestoreRating(ID, false, Session["userid"].ToString(), DateTime.Now);
        BindRating();

    }
    protected void ImgEditRating_Click(object sender, CommandEventArgs e)
    {
        int ID = Convert.ToInt32(e.CommandArgument.ToString());

        DataSet ds = new DataSet();
        ds = objInsp.GetRatingsByID(ID);

        if (ds.Tables[0].Rows.Count > 0)
        {
            txtRating.Text = ds.Tables[0].Rows[0][1].ToString();
            txtValue.Text = ds.Tables[0].Rows[0][2].ToString();
           // ddlColor.SelectedItem.Text = ds.Tables[0].Rows[0][3].ToString();
            ddlColor.SelectedIndex = ddlColor.Items.IndexOf(ddlColor.Items.FindByText(ds.Tables[0].Rows[0][3].ToString()));
        }

        ViewState["Mode"] = "EDIT";
        ViewState["RatingID"] = ID;
        btnRatingSave.Enabled = true;
    }
    protected void lnkRating_Click(object sender, CommandEventArgs e)
    {
        int ID = Convert.ToInt32(e.CommandArgument.ToString());

        DataSet ds = new DataSet();
        ds = objInsp.GetRatingsByID(ID);

        if (ds.Tables[0].Rows.Count > 0)
        {
            txtRating.Text = ds.Tables[0].Rows[0][1].ToString();
            txtValue.Text = ds.Tables[0].Rows[0][2].ToString();
            //ddlColor.SelectedItem.Text = ds.Tables[0].Rows[0][3].ToString();
           ddlColor.SelectedIndex= ddlColor.Items.IndexOf(ddlColor.Items.FindByText(ds.Tables[0].Rows[0][3].ToString()));
        }


    }
    protected void grdRating_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            HiddenField hdnActiveStatus = (HiddenField)e.Row.FindControl("hdnActiveStatus");
            if (hdnActiveStatus.Value == "1")
            {
                ImageButton ImgRestore = (ImageButton)e.Row.FindControl("ImgRatingRestore");
                ImageButton ImgEdit = (ImageButton)e.Row.FindControl("ImgEditRating");
                ImgEdit.Visible = true;
                ImgRestore.Visible = false;
                LinkButton lnk = (LinkButton)e.Row.FindControl("lnkRating");
                lnk.Enabled = true;

                e.Row.Cells[1].BackColor =  Color.FromName(e.Row.Cells[2].Text);


            }
            if (hdnActiveStatus.Value == "0")
            {
                ImageButton ImgDelete = (ImageButton)e.Row.FindControl("ImgRatingDelete");
                ImageButton ImgEdit = (ImageButton)e.Row.FindControl("ImgEditRating");
                ImgDelete.Visible = false;
                ImgEdit.Visible = false;
                LinkButton lnk = (LinkButton)e.Row.FindControl("lnkRating");
                lnk.Enabled = false;

            }
        }
    }
}