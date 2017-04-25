using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.Infrastructure;
using System.Web.Security;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Data.Sql;
using System.Collections.Generic;

public partial class Infrastructure_DaemonSettings_QueryBuilder : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["smsconn"].ToString());

            lblLoginInfo.Text = "User not logged in ...";
            txtQuery.Enabled = false;

            ddlServer.Items.Add(new ListItem(conn.DataSource, conn.DataSource));
            ddlCatalog.Items.Add(new ListItem(conn.Database, conn.Database));
            lblConnect.Text = "";            
            btnConnect.Visible = false;
            
        }
        string js = "initScripts();";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "script1", js, true);
    }
    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }
    
    protected void btnSearchServer_Click(object sender, EventArgs e)
    {
        Load_Servers();
        Load_Catalogs();

        lblConnect.Text = "";
        btnConnect.Visible = true;

    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        if (DBLogin_Check() == true)
        {
            string js = "alert('Login Successful!!');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "script1", js, true);

            Load_SavedQueries();
        }
        else
        {
            ddlSavedQuery.Items.Clear();
        }
    }

    protected void btnSaveQuery_Click(object sender, EventArgs e)
    {
        if (txtQuery.Text != "")
        {
            if (hdnQueryID.Value == "")
            {
                string js = "showModal('dvSaveCommand');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "script2", js, true);
            }
            else
            {
                btnSaveCommand_Click(null, null);
            }
        }
    }
    protected void btnSaveQueryAs_Click(object sender, EventArgs e)
    {
        if (txtQuery.Text != "")
        {
            string js = "showModal('dvSaveCommand');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "script2", js, true);
        }
    }
    protected void btnPreview_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtQuery.Text != "")
            {
                DataTable dt = BLL_Infra_DaemonSettings.ExecuteQuery(GetConnectionString(), txtQuery.Text, 0);
                gvResult.DataSource = dt;
                gvResult.DataBind();
            }
        }
        catch(Exception ex)
        {
            string js = "alert('" + ex.Message.Replace("'","") + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "script1", js, true);
        }
    }

    protected void btnSaveCommand_Click(object sender, EventArgs e)
    {
        string DBServer = ddlServer.SelectedValue;
        string Catalog = ddlCatalog.SelectedValue;
        
        int Res = BLL_Infra_DaemonSettings.SaveQuery(txtQueryName.Text, ddlCommandType.SelectedValue, txtQuery.Text, ddlResultType.SelectedValue, DBServer, Catalog, txtLogin.Text, hdnPassword.Value, UDFLib.ConvertToInteger(Session["USERID"]));
        if (Res > 0)
        {
            string js = "alert('Query Saved !!'); hideModal('dvSaveCommand');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "script2", js, true);

            Load_SavedQueries();
        }
    }

    protected bool DBLogin_Check()
    {
        string DBServer = ddlServer.SelectedValue;
        string Catalog = ddlCatalog.SelectedValue;

        string password = txtPassword.Text == "" ? hdnPassword.Value : txtPassword.Text;
        string LoginConnection = "Data Source=" + DBServer + ";DATABASE=" + Catalog + ";UID=" + txtLogin.Text + ";PWD=" + password;

        try
        {
            SqlConnection conn = new SqlConnection(LoginConnection);
            conn.Open();

            lblLoginInfo.Text = "Login Successful.<br><br>" +
                                "Server: " + ddlServer.SelectedValue + "<br>" +
                                "Catalog: " + ddlCatalog.SelectedValue;
            txtQuery.Enabled = true;
            pnlCtl.Visible = true;
            pnlSavedProc.Enabled = true;
            pnlDBProcedures.Enabled = true;

            hdnServer.Value = DBServer;
            hdnCatalog.Value = ddlCatalog.SelectedValue;
            hdnUsername.Value = txtLogin.Text;
            hdnPassword.Value = password;
            
            return true;
        }
        catch (Exception ex)
        {
            LogOut_DB();            
            return false;
        }
    }

    protected void ddlSavedQuery_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSavedQuery.SelectedValue == "0")
        {
            txtQuery.Text = "";
            txtQueryName.Text = "";
            lblQueryDetails.Text = "";
            lblCreatedBy.Text = "";
        }
        else
        {
            DataTable dt = BLL_Infra_DaemonSettings.Get_SavedQuery(UDFLib.ConvertToInteger(ddlSavedQuery.SelectedValue), GetSessionUserID());
            if (dt.Rows.Count > 0)
            {
                txtQuery.Text = dt.Rows[0]["Command_SQL"].ToString();
                txtQueryName.Text = dt.Rows[0]["Query_Name"].ToString();
                lblQueryDetails.Text = "Server: " + dt.Rows[0]["DBServer"].ToString() +
                    "/ (" + dt.Rows[0]["DatabaseName"].ToString() + ")" +
                    "<br>Query Name: " + dt.Rows[0]["Query_Name"].ToString();

                lblCreatedBy.Text = "Created By: " + dt.Rows[0]["CreatedBy"].ToString() + " on " + dt.Rows[0]["Date_Of_Creation"].ToString();
                if (dt.Rows[0]["ModifiedBy"].ToString() != "")
                    lblCreatedBy.Text += ", Modified By: " + dt.Rows[0]["ModifiedBy"].ToString() + " on " + dt.Rows[0]["Date_Of_Modification"].ToString();
            }
            else
            {
                txtQuery.Text = "";
                txtQueryName.Text = "";
                lblQueryDetails.Text = "";
                lblCreatedBy.Text = "";
            }
        }
    }

    protected void ddlServer_SelectedIndexChanged(object sender, EventArgs e)
    {
        LogOut_DB();        
    }

    protected void btnConnect_Click(object sender, EventArgs e)
    {
        Load_Catalogs();
    }

    protected void Load_SavedQueries()
    {
        DataTable dt = BLL_Infra_DaemonSettings.Get_SavedQuery(hdnServer.Value,hdnCatalog.Value,hdnUsername.Value, 0, GetSessionUserID());
        ddlSavedQuery.Items.Clear();
        ddlSavedQuery.DataSource = dt;
        ddlSavedQuery.DataTextField = "Query_Name";
        ddlSavedQuery.DataValueField = "ID";
        ddlSavedQuery.DataBind();
        ddlSavedQuery.Items.Insert(0, new ListItem("Select Procedure", "0"));
    }

    protected void Load_DatabaseProcedures()
    {
        DataTable dt = BLL_Infra_DaemonSettings.Get_DatabaseProcedures();
        lstDBProcedures.Items.Clear();
        lstDBProcedures.DataSource = dt;
        lstDBProcedures.DataTextField = "objectname";
        lstDBProcedures.DataValueField = "objectname";
        lstDBProcedures.DataBind();
        lstDBProcedures.Items.Insert(0, new ListItem("Select Procedure", "0"));
    }

    protected void Load_Servers()
    {
        // Retrieve the enumerator instance and then the data.
        SqlDataSourceEnumerator instance = SqlDataSourceEnumerator.Instance;
        System.Data.DataTable tableServers = instance.GetDataSources();

        List<string> listServers = new List<string>();
        foreach (DataRow rowServer in tableServers.Rows)
        {
            // Server instance could have instace name or only server name,            
            // check this for show the name            
            if (String.IsNullOrEmpty(rowServer["InstanceName"].ToString()))
                listServers.Add(rowServer["ServerName"].ToString());
            else
                listServers.Add(rowServer["ServerName"] + "\\" + rowServer["InstanceName"]);
        }


        ddlServer.DataSource = listServers;
        //ddlServer.DataTextField = "ServerName";
        //ddlServer.DataValueField = "ServerName";
        ddlServer.DataBind();

        DBLogin_Check();
    }
       
    protected void Load_Catalogs()
    {
        ddlCatalog.Items.Clear();

        string ServerName = ddlServer.SelectedValue;
        string username = txtLogin.Text;
        string password = txtPassword.Text == "" ? hdnPassword.Value : txtPassword.Text;
        string LoginConnection = "Data Source=" + ServerName + ";DATABASE=MASTER;UID=" + txtLogin.Text + ";PWD=" + password;

        if (username != "" && password != "")
        {
            try
            {
                SqlConnection conn = new SqlConnection(LoginConnection);
                conn.Open();

                SqlCommand sqlCmd = new SqlCommand("sp_databases", conn);
                sqlCmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                ddlCatalog.DataSource = dt;
                ddlCatalog.DataTextField = "DATABASE_NAME";
                ddlCatalog.DataValueField = "DATABASE_NAME";
                ddlCatalog.DataBind();

                lblConnect.Text = "Connected!!";
                hdnPassword.Value = password;
                btnConnect.Visible = false;
            }
            catch(Exception ex)
            {
                lblLoginInfo.Text = "User not logged in ...<br><br>" + ex.Message; ;
                
            }
        }
        else
        {
            string js = "alert('Please provide username and password.');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "script1", js, true);

        }

    }

    protected void LogOut_DB()
    {
        lblLoginInfo.Text = "User not logged in ...";
        txtQuery.Enabled = false;
        pnlCtl.Visible = false;
        pnlSavedProc.Enabled = false;
        pnlDBProcedures.Enabled = false;

        btnConnect.Visible = true;
        lblConnect.Text = "";
        ddlCatalog.Items.Clear();
        lblQueryDetails.Text = "";
        lblCreatedBy.Text = "";

        hdnServer.Value = "";
        hdnCatalog.Value = "";
        hdnUsername.Value = "";
        hdnPassword.Value = "";
    }

    protected string GetConnectionString()
    {
        string DBServer = hdnServer.Value;
        string Catalog = hdnCatalog.Value;
        string password = hdnPassword.Value;
        string LoginConnection = "Data Source=" + DBServer + ";DATABASE=" + Catalog + ";UID=" + txtLogin.Text + ";PWD=" + password;

        return LoginConnection;
    }

    protected void btnReloadSavedProcedures_Click(object sender, EventArgs e)
    {
        Load_SavedQueries();

    }
    protected void btnReloadDBProcedures_Click(object sender, EventArgs e)
    {
        Load_DatabaseProcedures();

    }
    protected void lstDBProcedures_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtQuery.Text = "";
        txtQueryName.Text = "";
        lblQueryDetails.Text = "";
        lblCreatedBy.Text = "";

        DataTable dt = BLL_Infra_DaemonSettings.Get_DatabaseProcedureSQL(lstDBProcedures.SelectedValue);
        if (dt.Rows.Count > 0)
        {
            txtQuery.Text = dt.Rows[0]["sqlcommand"].ToString();
        }
    }
   
    
}