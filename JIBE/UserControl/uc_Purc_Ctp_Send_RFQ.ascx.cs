using System;
using System.Web.UI.WebControls;
using System.Data;
using SMS.Business.PURC;
using System.Web.UI;
using SMS.Business.Infrastructure;
using SMS.Business.Crew;
using System.Configuration;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;

public partial class UserControl_uc_Purc_Ctp_Send_RFQ : System.Web.UI.UserControl
{
    #region property

    public int Contract_ID
    {
        get { return Int32.Parse(hdf_Contract_ID.Value); }
        set { hdf_Contract_ID.Value = value.ToString(); }
    }

    public bool Is_Reset_Values
    {
        get { return bool.Parse(hdf_Is_Reset_Values.Value); }
        set { hdf_Is_Reset_Values.Value = value.ToString(); }
    }

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        lblErrormsg.Text = "";
        if (!IsPostBack)
        {
            gvRFQList.DataSource = GetAddTable();
            gvRFQList.DataBind();
            ViewState["dtGridItems"] = GetAddTable();
        }

        if (Is_Reset_Values)
        {
            gvRFQList.DataSource = GetAddTable();
            gvRFQList.DataBind();
            ViewState["dtGridItems"] = GetAddTable();
        }
    }

    protected void imgbtnDelete_Click(object s, CommandEventArgs e)
    {
        DataTable dtGridItems = (DataTable)ViewState["dtGridItems"];
        DataRow dr = dtGridItems.Rows.Find(new object[] { e.CommandArgument.ToString().Split(',')[0], e.CommandArgument.ToString().Split(',')[1] });
        int rowIndex = dtGridItems.Rows.IndexOf(dr);
        if (rowIndex != dtGridItems.Rows.Count - 1)
        {
            dtGridItems.Rows.Remove(dr);

            gvRFQList.DataSource = dtGridItems;
            gvRFQList.DataBind();
            ViewState["dtGridItems"] = dtGridItems;
        }

        else
        {

        }
    }

    private DataTable GetAddTable()
    {
        DataTable dt = new DataTable();

        dt.Columns.Add("Supplier_Code", typeof(string));
        dt.Columns.Add("Port_ID", typeof(int));
        dt.Columns.Add("Remark", typeof(string));
        dt.PrimaryKey = new DataColumn[] { dt.Columns[0], dt.Columns[1], };

        DataRow dr = dt.NewRow();

        dr["Supplier_Code"] = "0";
        dr["port_id"] = "0";
        dt.Rows.Add(dr);

        dt.AcceptChanges();
        return dt;
    }

    private void AddNewRow()
    {
        DataTable dtGridItems = (DataTable)ViewState["dtGridItems"];
        int RowID = 0;

        try
        {
            foreach (GridViewRow gr in gvRFQList.Rows)
            {
                string Supp = ((UserControl_uc_SupplierList)gr.FindControl("uc_SupplierListRFQ")).SelectedValue;
                string port = ((UserControl_ctlPortList)gr.FindControl("ctlPortListRFQ")).SelectedValue;

                if (Supp.Trim() != "" && Supp.Trim() != "0" && port.Trim() != "" && port.Trim() != "0")
                {
                    DataRow drpk = dtGridItems.Rows.Find(new object[] { Supp, port });
                    if (drpk == null)
                    {

                        dtGridItems.Rows[RowID]["Port_ID"] = Int32.Parse(port);
                        dtGridItems.Rows[RowID]["Supplier_Code"] = Supp;
                    }
                    dtGridItems.Rows[RowID]["Remark"] = ((TextBox)gr.FindControl("txtRemark")).Text;
                }
                else
                {
                    if (RowID > 0)
                    {
                        bool isAdded = false;
                        if (port.Trim() == "0" && Supp.Trim() != "0")
                        {
                            dtGridItems.Rows[RowID]["Supplier_Code"] = Supp;
                            int portid = Int32.Parse(((UserControl_ctlPortList)gvRFQList.Rows[(gr.RowIndex - 1)].FindControl("ctlPortListRFQ")).SelectedValue);
                            DataRow drpk = dtGridItems.Rows.Find(new object[] { Supp, portid });
                            if (drpk == null)
                                dtGridItems.Rows[RowID]["Port_ID"] = portid;



                            isAdded = true;
                        }
                        else if (Supp.Trim() == "0" && port.Trim() != "0")
                        {
                            dtGridItems.Rows[RowID]["Port_ID"] = Int32.Parse(port);
                            string suppcode = ((UserControl_uc_SupplierList)gvRFQList.Rows[(gr.RowIndex - 1)].FindControl("uc_SupplierListRFQ")).SelectedValue;
                            DataRow drpk = dtGridItems.Rows.Find(new object[] { suppcode, port });
                            if (drpk == null)
                                dtGridItems.Rows[RowID]["Supplier_Code"] = suppcode;

                            isAdded = true;
                        }

                        if (isAdded)
                        {
                            DataRow dr = dtGridItems.NewRow();

                            dr["Supplier_Code"] = "0";
                            dr["port_id"] = "0";
                            dtGridItems.Rows.Add(dr);
                        }
                        gvRFQList.DataSource = dtGridItems;
                        gvRFQList.DataBind();
                        ViewState["dtGridItems"] = dtGridItems;

                    }
                    return;
                }
                RowID++;
            }

            if (RowID == 1)
            {
                DataRow dr = dtGridItems.NewRow();

                dr["Supplier_Code"] = "0";
                dr["port_id"] = "0";
                dtGridItems.Rows.Add(dr);

                gvRFQList.DataSource = dtGridItems;
                gvRFQList.DataBind();

                ViewState["dtGridItems"] = dtGridItems;
            }
        }
        catch (DataException ex)
        {


        }
    }

    protected void SupplierListRFQ_SelectedIndexChanged(ListBox sender)
    {

        AddNewRow();
    }
    protected void ctlPortListRFQ_SelectedIndexChanged()
    {
        AddNewRow();
    }


    protected void btnSaveSendToSippliers_Click(object s, EventArgs e)
    {
        try
        {
            DataTable dtGridItems = new DataTable();
            dtGridItems.Columns.Add("Supplier_code", typeof(string));
            dtGridItems.Columns.Add("Port_id", typeof(int));
            dtGridItems.Columns.Add("remark", typeof(string));
            dtGridItems.Columns.Add("RFQType");
            DataRow dr;

            foreach (GridViewRow gr in gvRFQList.Rows)
            {
                string Supp = ((UserControl_uc_SupplierList)gr.FindControl("uc_SupplierListRFQ")).SelectedValue;
                string port = ((UserControl_ctlPortList)gr.FindControl("ctlPortListRFQ")).SelectedValue;

                if (Supp != "0" && port != "0")
                {
                    dr = dtGridItems.NewRow();

                    dr["Supplier_Code"] = Supp;
                    dr["port_id"] = int.Parse(port);
                    dr["remark"] = ((TextBox)gr.FindControl("txtRemark")).Text;
                    dr["RFQType"] = Int32.Parse(((RadioButtonList)gr.FindControl("rbtnRfqType")).SelectedValue);
                    dtGridItems.Rows.Add(dr);
                }
            }

            DataTable dtQtnIdret = BLL_PURC_CTP.Ins_Ctp_SendRFQ(Contract_ID, dtGridItems, Convert.ToInt32(Session["USERID"]));

            if (dtQtnIdret.Rows.Count > 0)
            {
                btnSaveSendToSippliers.Enabled = false;

                BLL_PURC_Purchase objPurc = new BLL_PURC_Purchase();
                CTP_RFQ_Mail objmail = new CTP_RFQ_Mail();
                //if exists, Insert the Web Quotation supplier code in the PMS_Lib_Quotation_User and lib user 
                foreach (DataRow drsupp in dtGridItems.Rows)
                {
                    objPurc.GetSupplierUserDetails(drsupp["Supplier_Code"].ToString(), "S");
                }
                foreach (DataRow drqtn in dtQtnIdret.Rows)
                {
                    objmail.SendMailToSupplier(Convert.ToInt32(drqtn["QUOTATION_ID"].ToString()), Convert.ToInt32(drqtn["RFQType"].ToString()), this.Page);
                }

                String msgretv = String.Format("window.open('','_self','');window.close()");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgMail", msgretv, true);
            }
        }
        catch (Exception ex)
        {

            lblErrormsg.Text = ex.Message;
        }

    }


    public void SetAttribute_Refresh()
    {
        btnSaveSendToSippliers.Attributes.Add("onclick", "javascript:parent.ReloadParent_ByButtonID();");

    }




}



