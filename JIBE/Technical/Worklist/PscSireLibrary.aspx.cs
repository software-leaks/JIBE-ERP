using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SMS.Business.Technical;
using SMS.Business.Infrastructure;
using SMS.Properties;
public partial class Technical_Worklist_PscSireLibrary : System.Web.UI.Page
{
    BLL_Tec_Worklist objWL = new BLL_Tec_Worklist();
    UserAccess objUA = new UserAccess();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindPSCSIRE();
            ViewState["Mode"] = "ADD";

        }
        UserAccessValidation();
    }
    protected void UserAccessValidation()
    {
        int CurrentUserID = GetSessionUserID();
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

        BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);


        if (objUA.View == 0)
        {
            //  Response.Redirect("~/default.aspx?msgid=1");
            lblError.Text = "You don't have sufficient privilege to view this page";
            lblError.Visible = true;
            grdPSCSIRE.Visible = false;
            lblPSCSIRE.Visible = false;
            txtPSCSIRECode.Visible = false;
            btnPSCSIRESave.Visible = false;
        }
        if (objUA.Add == 0)
        {
            // Response.Redirect("~/default.aspx?msgid=2");
            //pnlAddAttachment.Enabled = false;
            btnPSCSIRESave.Enabled = false;
        }
        if (objUA.Edit == 0)
        {
            //Response.Redirect("~/default.aspx?msgid=3");


            for (int i = 0; i < grdPSCSIRE.Rows.Count; i++)
            {
                ImageButton imgb = ((ImageButton)grdPSCSIRE.Rows[i].Cells[1].FindControl("ImgEditPSCSIRE"));
                imgb.Visible = false;
            }
            btnPSCSIRESave.Enabled = false;

        }
        if (objUA.Delete == 0)
        {
            for (int i = 0; i < grdPSCSIRE.Rows.Count; i++)
            {
                ImageButton imgb = ((ImageButton)grdPSCSIRE.Rows[i].Cells[1].FindControl("ImgPSCSIREDelete"));
                imgb.Visible = false;
            }
        }
        if (objUA.Approve == 0)
        {

        }

    }

    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }
    protected void BindPSCSIRE()
    {
        DataSet ds = objWL.TEC_WL_Get_PSCSIRE();
        if (ds.Tables.Count > 0)
        {
            grdPSCSIRE.DataSource = ds.Tables[0];
            grdPSCSIRE.DataBind();
        }
    }
    protected void btnPSCSIRESave_Click(object sender, EventArgs e)
    {
        string PSCSIRE;

        PSCSIRE = txtPSCSIRECode.Text;
       
        if (ViewState["Mode"].ToString() == "ADD")
        {
            objWL.TEC_WL_Insert_PSCSIRE(PSCSIRE, UDFLib.ConvertToInteger(Session["userid"].ToString()));
            string js2 = "alert('PSC/SIRE Saved Successfully');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert10", js2, true);
        }
        else if (ViewState["Mode"].ToString() == "EDIT")
        {
            int ID = Convert.ToInt32(ViewState["ID"].ToString());
            objWL.TEC_WL_Update_PSCSIRE(ID, PSCSIRE, UDFLib.ConvertToInteger(Session["userid"].ToString()));
            string js2 = "alert('PSC/SIRE Updated Successfully');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert10", js2, true);
        }

        BindPSCSIRE();
        ViewState["ID"] = "";
        ViewState["Mode"] = "ADD";
        txtPSCSIRECode.Text = "";
        updCat.Update();
        //  btnCategorySave.Enabled = false;

    }

    //protected void btnCategoryAdd_Click(object sender, EventArgs e)
    //{
    //    txtPSCSIRECode.Text = "";
    
    //    ViewState["Mode"] = "ADD";
    //    // btnCategorySave.Enabled = true;

    //}
    protected void ImgPSCSIRERestore_Click(object sender, CommandEventArgs e)
    {
        int ID = Convert.ToInt32(e.CommandArgument.ToString());
        objWL.TEC_WL_Restore_PSCSIRE(ID, UDFLib.ConvertToInteger(Session["userid"].ToString()));
        BindPSCSIRE();
    }
    protected void ImgPSCSIREDelete_Click(object sender, CommandEventArgs e)
    {

        int ID = Convert.ToInt32(e.CommandArgument.ToString());
        objWL.TEC_WL_Delete_PSCSIRE(ID, UDFLib.ConvertToInteger(Session["userid"].ToString()));
        BindPSCSIRE();

    }
    protected void ImgEditPSCSIRE_Click(object sender, CommandEventArgs e)
    {
        int ID = Convert.ToInt32(e.CommandArgument.ToString());

        DataSet ds = new DataSet();
        ds = objWL.TEC_WL_Get_PSCSIREByID(ID);

        if (ds.Tables[0].Rows.Count > 0)
        {
            txtPSCSIRECode.Text = ds.Tables[0].Rows[0][1].ToString();
           

        }

        ViewState["Mode"] = "EDIT";
        ViewState["ID"] = ID;
        //   btnCategorySave.Enabled = true;
    }
    protected void lnkCategory_Click(object sender, CommandEventArgs e)
    {
        int ID = Convert.ToInt32(e.CommandArgument.ToString());

        DataSet ds = new DataSet();
        ds = objWL.TEC_WL_Get_PSCSIREByID(ID);

        if (ds.Tables[0].Rows.Count > 0)
        {
            txtPSCSIRECode.Text = ds.Tables[0].Rows[0][1].ToString();

            
        }


    }
}