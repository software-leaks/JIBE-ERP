using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//using SMS.Business.PURC;
using SMS.Business.Infrastructure;
using SMS.Business.VM;
using System.Data;
using System.Web.UI.HtmlControls;
using SMS.Properties;


public partial class VesselMovement_Port_DPL : System.Web.UI.Page
{
    public string OperationMode = "";
    public Boolean uaEditFlag = false;
    public Boolean uaDeleteFlage = false;
    private long lCurrentRecord = 0;
    private long lRecordsPerRow = 200;
    public UserAccess objUA = new UserAccess();
    BLL_Infra_VesselLib objBLL = new BLL_Infra_VesselLib();
    BLL_VM_PortCall objPortCall = new BLL_VM_PortCall();
    BLL_Infra_Port objBLLPort = new BLL_Infra_Port();

    protected void Page_Load(object sender, EventArgs e)
    {
        UserAccessValidation();
        if (!IsPostBack)
        {
            //txtTo.Text = System.DateTime.Now.AddMonths(6).ToString("dd-MM-yyyy");
            Get_PortCall_Search_DPL();
        }
    }

    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }

    protected void UserAccessValidation()
    {
        int CurrentUserID = GetSessionUserID();
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

        BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

        if (CurrentUserID == 0)
            Response.Redirect("~/default.aspx?msgid=1");

    }

    public void Get_PortCall_Search_DPL()
    {
        string FromDate;
        try
        {
            if (Session["sFleet"] == null )
            {
                DataTable FleetDT = objBLL.GetFleetList(Convert.ToInt32(Session["USERCOMPANYID"].ToString()));
                FleetDT.Columns.RemoveAt(1);
                FleetDT.Columns.RemoveAt(1);
                FleetDT.Columns.RemoveAt(1);
                FleetDT.Columns.RemoveAt(1);
                FleetDT.Columns.RemoveAt(1);
                FleetDT.Columns.RemoveAt(1);
                FleetDT.Columns.RemoveAt(1);
                Session["sFleet"] = FleetDT;
                DataTable dtVessel = objPortCall.Get_PortCall_VesselList(FleetDT, 0, Convert.ToInt32(Session["USERCOMPANYID"].ToString()), "", Convert.ToInt32(Session["USERCOMPANYID"].ToString()));

                dtVessel.Columns.RemoveAt(1);
                dtVessel.Columns.RemoveAt(1);
                dtVessel.Columns.RemoveAt(1);
                dtVessel.Columns.RemoveAt(1);
                Session["sVesselCode"] = dtVessel;
            }

            String ToDate = System.DateTime.Now.AddMonths(6).ToString("dd-MM-yyyy");
            if (Session["FromDate"] != null)
            {
                FromDate = Session["FromDate"].ToString();
            }
            else
            {
                FromDate = System.DateTime.Now.ToString("dd-MM-yyyy");
            }
            if (Session["sFleet"] != null && Session["sVesselCode"] != null)
            {
                DataSet ds = objPortCall.Get_PortCall_Search_DPL((DataTable)Session["sFleet"], (DataTable)Session["sVesselCode"], Convert.ToDateTime(FromDate.ToString()));


                ds.Relations.Add(new DataRelation("NestedCat", ds.Tables[1].Columns["Vessel_ID"], ds.Tables[0].Columns["Vessel_ID"]));

                ds.Tables[1].TableName = "Members";

                rpt1.DataSource = ds.Tables[1];
                rpt1.DataBind();
            }
        }
        catch (Exception ex)
        {
        }
    }

    protected void rpt1_ItemDataBound(object sender, System.Web.UI.WebControls.RepeaterItemEventArgs e)
    {
        DataRowView dv = e.Item.DataItem as DataRowView;
        Image ImgCard = ((ImageButton)e.Item.FindControl("ImgView"));
        if (dv != null)
        {
            Repeater nestedRepeater = e.Item.FindControl("rpt2") as Repeater;
            if (nestedRepeater != null)
            {

                nestedRepeater.DataSource = dv.CreateChildView("NestedCat");
                nestedRepeater.DataBind();
            }
        }


    }
    protected void rpt2_ItemDataBound(Object Sender, RepeaterItemEventArgs e)
    {
        try
        {
            if (e.Item.ItemType != ListItemType.Header & e.Item.ItemType != ListItemType.Footer)
            {
                for (int i = 0; i <= e.Item.Controls.Count - 1; i++)
                {
                    System.Web.UI.WebControls.Literal obLiteral = e.Item.Controls[i] as System.Web.UI.WebControls.Literal;
                    if (obLiteral != null)
                    {
                        if (obLiteral.ID == "litRowStart")
                        {
                            lCurrentRecord += 1;
                            if ((lCurrentRecord == 1))
                            {
                                obLiteral.Text = "<tr>";
                            }
                            break;
                        }
                    }
                }
                for (int i = 0; i <= e.Item.Controls.Count - 1; i++)
                {
                    System.Web.UI.WebControls.Literal obLiteral = e.Item.Controls[i] as System.Web.UI.WebControls.Literal;
                    if (obLiteral != null)
                    {
                        if (obLiteral.ID == "litRowEnd")
                        {
                            if (lCurrentRecord % lRecordsPerRow == 0)
                            {
                                obLiteral.Text = "</tr>";
                                lCurrentRecord = 0;
                            }
                            break;
                        }
                    }
                }

                Image ImgCard = ((ImageButton)e.Item.FindControl("ImgView"));
                Image imgPurchasebtn = ((ImageButton)e.Item.FindControl("imgPurchasebtn"));
                Image imgPoAgencybtn = ((ImageButton)e.Item.FindControl("imgPoAgencybtn"));
                Image imgWorkList = ((ImageButton)e.Item.FindControl("imgWorkList"));
                Image imgAgencyWork = ((ImageButton)e.Item.FindControl("imgAgencyWork"));
                ImageButton ImgUpdate = ((ImageButton)e.Item.FindControl("ImgUpdate"));
                ImageButton ImgDelete = ((ImageButton)e.Item.FindControl("ImgDelete"));
                ImageButton Iswarrisk = ((ImageButton)e.Item.FindControl("Iswarrisk"));
                ImageButton IsShipCraneReq = ((ImageButton)e.Item.FindControl("IsShipCraneReq"));

                LinkButton lnkselect = (LinkButton)e.Item.FindControl("SelectButton");
                Label lblCharter = (Label)e.Item.FindControl("lblCharter");
                Label lblOwner = (Label)e.Item.FindControl("lblOwner");


               if (DataBinder.Eval(e.Item.DataItem, "IsWarRisk").ToString() == "1")
                {
                    Iswarrisk.Visible = true;

                }

                if (DataBinder.Eval(e.Item.DataItem, "IsShipCraneReq").ToString() == "1")
                {
                    IsShipCraneReq.Visible = true;

                }


                if (DataBinder.Eval(e.Item.DataItem, "CrewOn").ToString() != "0" || DataBinder.Eval(e.Item.DataItem, "CrewOffCount").ToString() != "0")
                {
                    ImgCard.Visible = true;

                }
                else
                {
                    ImgCard.Visible = false;
                }
                if (Convert.ToInt16(DataBinder.Eval(e.Item.DataItem, "AgencyPo").ToString()) > 0)
                {
                    imgAgencyWork.Visible = true;
                    imgAgencyWork.Style.Clear();
                    ImgDelete.Visible = false;
                }
                else
                {
                    imgAgencyWork.Visible = false;
                }
                if (Convert.ToInt16(DataBinder.Eval(e.Item.DataItem, "PO").ToString()) > 0)
                {
                    imgPurchasebtn.Visible = true;
                }
                else
                {
                    imgPurchasebtn.Visible = false;
                }
                if (Convert.ToInt16(DataBinder.Eval(e.Item.DataItem, "WorkList").ToString()) > 0)
                {
                    imgWorkList.Visible = true;
                }
                else
                {
                    imgWorkList.Visible = false;
                }
                if (Convert.ToInt16(DataBinder.Eval(e.Item.DataItem, "Port_ID").ToString()) == 0)
                {
                    lnkselect.BackColor = System.Drawing.Color.Yellow;
                }


            }
        }
        catch { }

    }

}