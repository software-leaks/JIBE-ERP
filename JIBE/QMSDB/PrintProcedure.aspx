<%@ Page Language="C#" EnableViewState="false" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="System.Reflection" %>
<%@ Import Namespace="SMS.Business.QMSDB" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">

    DataSet ds = new DataSet();
    DataTable dt = new DataTable(); 
    string Procedure_ID = "";
    //string[] alColumns;
    //string[] alCaptions;
    string printheader;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Request.QueryString["Procedure_ID"]!= null)
                Procedure_ID = Request.QueryString["Procedure_ID"].ToString();
            dt = BLL_QMSDB_Procedures.QMSDBProcedures_Edit(int.Parse(Procedure_ID));
        }
        catch (Exception ex)
        {
            Response.Write("Refresh the screen and try again!<br/>" + ex.Message );
        }
        
    //    'build the content for the dynamic Word document
    //'in HTML alongwith some Office specific style properties. 
     System.Text.StringBuilder strBody = new  System.Text.StringBuilder("");

     strBody.Append("<html xmlns:o='urn:schemas-microsoft-com:office:office' xmlns:w='urn:schemas-microsoft-com:office:word'  xmlns='http://www.w3.org/TR/REC-html40'><head><title>Time</title>") ;

    //'The setting specifies document's view after it is downloaded as Print
    //'instead of the default Web Layout
    strBody.Append("<!--[if gte mso 9]> <xml> <w:WordDocument> <w:View>Print</w:View><w:Zoom>90</w:Zoom><w:DoNotOptimizeForBrowser/></w:WordDocument></xml><![endif]-->");

    strBody.Append(@"<style><!-- /* Style Definitions */ @page Section1   {size:8.5in 11.0in;  margin:1.0in 1.25in 1.0in 1.25in ;  mso-header-margin:.5in;  mso-footer-margin:.5in; mso-paper-source:0;}
                             div.Section1
                           {page:Section1;}
                           --></style></head>") ;

    strBody.Append(@"<body lang=EN-US style='tab-interval:.5in'>
                            <div>
                            <h1>Time and tide wait for none</h1>
                            <p style='color:red'><I>
                            DateTime.Now </I></p> " + dt.Rows[0][9].ToString() +" </div></body></html>") ;

    //'Force this content to be downloaded 
    //'as a Word document with the name of your choice
    Response.AppendHeader("",dt.Rows[0]["HeaderDetails"].ToString());
    Response.AppendHeader("Content-disposition", "attachment; filename=myword.doc");
    Response.Write(strBody);

        
    }  
</script>

