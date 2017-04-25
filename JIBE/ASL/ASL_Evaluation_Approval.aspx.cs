using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using SMS.Business.Infrastructure;
using SMS.Properties;
using SMS.Business.ASL;

public partial class ASL_ASL_Evaluation_Approval : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
           
            Submit();
        }
        
    }
    //Approve And Reject Evaluation
    protected void Submit()
    {
        try
        {
            if(Request.QueryString["url"] != null)
            {
                String msg2;
                string Str = Request.QueryString["url"].ToString().Replace(" ", "+");
                string url = DMS.DES_Encrypt_Decrypt.Decrypt(Str);
               char[] delimiterChars = { '&',' ', ',', '.', ':', '\t','=' };

               string[] Arrayurl = url.Split(delimiterChars);

               string Evaluation_ID = Arrayurl[1].ToString();
               string User_ID = Arrayurl[3].ToString();
               string Evaluation_Status = Arrayurl[5].ToString();

               string IPAdd = string.Empty;
               IPAdd = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
               if (string.IsNullOrEmpty(IPAdd))
                   IPAdd = Request.ServerVariables["REMOTE_ADDR"];
              string Browser_Agent = Request.UserAgent;

               int RetValue = BLL_ASL_Supplier.Pending_Evaluation_Approve_Reject(UDFLib.ConvertIntegerToNull(Evaluation_ID.ToString()),null,Evaluation_Status,
                   UDFLib.ConvertIntegerToNull(User_ID), IPAdd, Browser_Agent);
               if (RetValue == 1)
               {
                   msg2 = String.Format("alert('Authentication is incorrect,Please check Authentication.')");
                   ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg2, true);
               }
               if (RetValue == 4)
               {
                   msg2 = String.Format("alert('Evaluation Approved Successfully.')");
                   ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg2, true);
               }
               if (RetValue == 2)
               {
                  msg2 = String.Format("alert('Evaluation Already Approved.')");
                   ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg2, true);
               }
              if (RetValue == 3)
                {
                   msg2 = String.Format("alert('Evaluation Already Rejected.')");
                   ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg2, true);
                }
              
          
               //string myclosescript = "<script language='javascript' type='text/javascript'>CloseWindow();</script>";
               string myclosescript = "<script language='javascript' type='text/javascript'>closeWin();</script>";
               Page.ClientScript.RegisterStartupScript(GetType(), "myclosescript", myclosescript);
            }
        }
        catch (Exception ex )
        {
            throw ex;
        }
    }

   
}