using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Security.Cryptography;
using System.IO;
using System.Text;
using System.Configuration;
using System.Data.SqlTypes;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;
using System.Collections;
using System.Collections.ObjectModel;
using System.Xml;

/// <summary>
/// Summary description for UserDefinedFunction
/// </summary>
public class UDFLib
{
    private const string PARAMETER_NAME = "enc=";
    private const string ENCRYPTION_KEY = "key";
    private readonly static byte[] SALT = Encoding.ASCII.GetBytes(ENCRYPTION_KEY.Length.ToString());

    public UDFLib()
    {
        //
        // TODO: Add constructor logic here
        //
    }



    public static decimal ConvertToDecimal(decimal? value)
    {

        decimal retValue = 0;
        if (decimal.TryParse(value.ToString(), out retValue))
        {

        }

        return retValue;
    }

    public static decimal ConvertToDecimal(string value)
    {

        decimal retValue = 0;
        if (decimal.TryParse(value, out retValue))
        {

        }

        return retValue;
    }

    public static int ConvertToInteger(string value)
    {

        int retValue = 0;
        if (int.TryParse(value, out retValue))
        {

        }

        return retValue;
    }



    public static decimal ConvertToDecimal(dynamic value)
    {

        decimal retValue = 0;
        decimal.TryParse(Convert.ToString(value), out retValue);

        return retValue;
    }

    public static int ConvertToInteger(dynamic value)
    {

        int retValue = 0;
        int.TryParse(Convert.ToString(value), out retValue);
        return retValue;
    }

    public static int? ConvertIntegerToNull(object value)
    {
        int retValue = 0;
        int.TryParse(Convert.ToString(value), out retValue);

        if (retValue == 0)
            return null;
        else
            return retValue;
    }

    public static decimal? ConvertDecimalToNull(object value)
    {
        decimal? retVal = 0;
        decimal outValue = 0;
        if (decimal.TryParse(Convert.ToString(value), out outValue))
        {
            retVal = outValue;
        }
        else
            retVal = null;

        return retVal;

    }


    public static DateTime? ConvertDateToNull(object value)
    {
        DateTime? retValue = new DateTime();
        DateTime outValue = new DateTime();
        if (DateTime.TryParse(Convert.ToString(value), out outValue))
        {
            retValue = outValue;
        }
        else
            retValue = null;

        return retValue;

    }

    public static double ConvertToDouble(string value)
    {
        double retValue = 0;
        if (double.TryParse(value, out retValue))
        {

        }
        return retValue;
    }



    public static string ConvertStringToNull(object value)
    {
        if (Convert.ToString(value) == "0" || Convert.ToString(value) == "")
            return null;
        else
            return Convert.ToString(value);
    }


    public static string ReplaceSpecialCharacter(string strContent)
    {

        return strContent.Replace("\r\n", "<br>").Replace("'", " ").Replace("\r", "<br>").Replace("\t", "").Replace("(", "[").Replace(")", "]").Replace("\n", "<br>").Replace(@"\", " ").Replace("/", " ");
    }

    public static string EscapeLikeValue(string valueWithoutWildcards)
    {
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < valueWithoutWildcards.Length; i++)
        {
            char c = valueWithoutWildcards[i];
            if (c == '*' || c == '%' || c == '[' || c == ']')
                sb.Append("[").Append(c).Append("]");
            else if (c == '\'')
                sb.Append("''");
            else
                sb.Append(c);
        }
        return sb.ToString();
    }

    public static string CreateHtmlTableFromDataTable(DataTable dtTable, string[] HeaderCaptions, string[] DataColumnsName, string PageHeader)
    {
        StringBuilder strTable = new StringBuilder();

        try
        {

            if (dtTable.Rows.Count > 0)
            {
                strTable.Append("<table id='__tbl_remark' class='CreateHtmlTableFromDataTable-table' CELLPADDING='2' CELLSPACING='0'  style='border-collapse:collapse'  >");
                if (PageHeader.Length > 1)
                    strTable.Append("<tr> <td class='CreateHtmlTableFromDataTable-PageHeader'  colspan='" + dtTable.Columns.Count + "' > <b>" + PageHeader + "</b> </td></tr>");

                if (HeaderCaptions.Length > 0)
                {
                    strTable.Append("<tr >");
                    for (int i = 0; i < HeaderCaptions.Length; i++)
                    {
                        strTable.Append("<td class='CreateHtmlTableFromDataTable-DataHedaer' width='auto'>");
                        strTable.Append("<b>" + HeaderCaptions[i] + "</b>");
                        strTable.Append("</td>");
                    }
                    strTable.Append("</tr>");
                }
                foreach (DataRow dr in dtTable.Rows)
                {
                    strTable.Append("<tr>");
                    for (int i = 0; i < DataColumnsName.Length; i++)
                    {
                        strTable.Append("<td class='CreateHtmlTableFromDataTable-Data'>");
                        strTable.Append(dr[DataColumnsName[i]].ToString().Replace("\n", "<br>"));
                        strTable.Append("</td>");
                    }
                    strTable.Append("</tr>");
                }
                strTable.Append("</table>");
            }
            else
                strTable.Append("<span style='color:maroon;padding:2px'> No record found !</span>");

            return strTable.ToString();
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {

        }
    }

    public static string CreateHtmlTableFromDataTable(DataTable dtTable, string[] HeaderCaptions, string[] DataColumnsName, string[] DataColumnsAlignment, string PageHeader)
    {
        StringBuilder strTable = new StringBuilder();

        try
        {

            if (dtTable.Rows.Count > 0)
            {
                strTable.Append("<table id='__tbl_remark'  class='CreateHtmlTableFromDataTable-table' CELLPADDING='2' CELLSPACING='0'  style='border-collapse:collapse'  >");
                if (!string.IsNullOrWhiteSpace(PageHeader) && PageHeader.Length > 1)
                    strTable.Append("<tr> <td class='CreateHtmlTableFromDataTable-PageHeader'  colspan='" + dtTable.Columns.Count + "' > <b>" + PageHeader + "</b> </td></tr>");

                if (HeaderCaptions.Length > 0)
                {
                    strTable.Append("<tr >");
                    for (int i = 0; i < HeaderCaptions.Length; i++)
                    {
                        strTable.Append("<th class='CreateHtmlTableFromDataTable-DataHedaer' width='auto' >");
                        strTable.Append("<b>" + HeaderCaptions[i] + "</b>");
                        strTable.Append("</th>");
                    }
                    strTable.Append("</tr>");
                }
                foreach (DataRow dr in dtTable.Rows)
                {
                    strTable.Append("<tr>");
                    for (int i = 0; i < DataColumnsName.Length; i++)
                    {
                        strTable.Append("<td class='CreateHtmlTableFromDataTable-Data'  align='" + DataColumnsAlignment[i] + "'>");
                        strTable.Append(dr[DataColumnsName[i]].ToString().Replace("\n", "<br>"));
                        strTable.Append("</td>");
                    }
                    strTable.Append("</tr>");
                }
                strTable.Append("</table>");
            }
            else
                strTable.Append("<span style='color:maroon;padding:2px'> No record found !</span>");

            return strTable.ToString();
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {

        }
    }

    public static string CreateHtmlCheckBoxList(string Prefix_ID, string HDFitemCount_ID, DataTable dtItems, string DataTextField, string DataValueField, string RepeatDirection, int RepeatColumn = 0, string css = "")
    {
        StringBuilder ChkList = new StringBuilder();

        if (dtItems.Rows.Count > 0)
        {
            ChkList.Append(" <input type='hidden' id='" + HDFitemCount_ID + "' value='" + dtItems.Rows.Count.ToString() + "'  />");
            ChkList.Append("<table ");

            if (css != "")
                ChkList.Append(" class='" + css + "'");
            ChkList.Append(" > ");

            if (RepeatDirection.ToLower() == "horizontal")
            {

                ChkList.Append("<tr>");

                int i = 1;
                int j = 0;
                if (RepeatColumn != 0)
                    j = RepeatColumn;

                foreach (DataRow dritem in dtItems.Rows)
                {


                    ChkList.Append("<td>");

                    ChkList.Append(" <input type='checkbox' id='" + Prefix_ID + "_" + i.ToString() + "' value='" + dritem[DataValueField].ToString() + "'  />" + dritem[DataTextField].ToString());


                    ChkList.Append("</td>");

                    if (RepeatColumn != 0 && i == j)
                    {
                        ChkList.Append("</tr>");
                        ChkList.Append("<tr>");
                        j = j + RepeatColumn;
                    }

                    i++;
                }

                ChkList.Append("</tr>");
            }
            else if (RepeatDirection.ToLower() == "vertical")
            {
                int i = 1;
                foreach (DataRow dritem in dtItems.Rows)
                {
                    ChkList.Append("<tr>");
                    ChkList.Append("<td>");

                    ChkList.Append(" <input type='checkbox' id='" + Prefix_ID + "_" + i.ToString() + "' value='" + dritem[DataValueField].ToString() + "'  />" + dritem[DataTextField].ToString());


                    ChkList.Append("</td>");
                    ChkList.Append("</tr>");
                    i++;
                }

            }
            ChkList.Append("</table>");
        }

        return ChkList.ToString();
    }

    public static string CreateHtmlTableFromDataTable(DataTable dtTable, string[] HeaderCaptions, string[] DataColumnsName, Dictionary<string, UDCHyperLink> DicLink, Dictionary<string, UDCToolTip> DicToolTip, string[] DataColumnsAlignment, string TableCss, string HeaderCss, string RowStyleCss)
    {
        StringBuilder strTable = new StringBuilder();

        try
        {

            if (dtTable.Rows.Count > 0)
            {
                //ArrayList arrSrcToolTipColumn = new ArrayList(); // store the all sourcetooltip column name in aaray list
                //if (DicToolTip.Keys.Count > 0)
                //{
                //    foreach (UDCToolTip objTP in DicToolTip.Values)
                //    {
                //        arrSrcToolTipColumn.Add(objTP.ToolTipSourceColumn);
                //    }

                //}


                strTable.Append("<table id='__tbl_remark' CELLPADDING='2' CELLSPACING='0'  style='border-collapse:collapse' ");
                if (!string.IsNullOrWhiteSpace(TableCss))
                    strTable.Append(" class='" + TableCss + "' ");

                strTable.Append(" > ");

                if (HeaderCaptions.Length > 0)
                {
                    strTable.Append("<tr >");
                    for (int i = 0; i < HeaderCaptions.Length; i++)
                    {
                        strTable.Append("<td  width='auto'");
                        if (!string.IsNullOrWhiteSpace(HeaderCss))
                            strTable.Append(" class='" + HeaderCss + "' ");
                        strTable.Append(" > ");


                        strTable.Append(HeaderCaptions[i]);
                        strTable.Append("</td>");
                    }
                    strTable.Append("</tr>");
                }
                foreach (DataRow dr in dtTable.Rows)
                {
                    strTable.Append("<tr>");
                    for (int i = 0; i < DataColumnsName.Length; i++)
                    {

                        strTable.Append("<td ");
                        if (!string.IsNullOrWhiteSpace(RowStyleCss))
                            strTable.Append(" class='" + RowStyleCss + "' ");

                        if (DataColumnsAlignment.Length > i)
                            strTable.Append("  align='" + DataColumnsAlignment[i] + "' ");

                        UDCToolTip objToolTip;
                        if (DicToolTip.TryGetValue(DataColumnsName[i], out objToolTip))// check for tool tip
                        {
                            if (objToolTip.UseControlToolTip)
                                strTable.Append(" title='" + ReplaceSpecialCharacter(Convert.ToString(dr[objToolTip.ToolTipSourceColumn])) + "'");
                            else
                            {
                                if (Convert.ToString(dr[objToolTip.ToolTipSourceColumn]).Contains("To Attend TAROKO this call Singapore for"))
                                {
                                    string s = "";
                                }
                                strTable.Append(" onmouseover='js_ShowToolTip(&#39;" + ReplaceSpecialCharacter(Convert.ToString(dr[objToolTip.ToolTipSourceColumn])) + "&#39;,event,this)'");
                            }
                        }

                        strTable.Append(" > ");
                        UDCHyperLink objA;
                        if (DicLink.TryGetValue(DataColumnsName[i], out objA)) //check for hyperlink
                        {
                            if (string.IsNullOrWhiteSpace(objA.HyperLinkColumnName))//link is fixed
                                strTable.Append("<a href='" + objA.NaviagteURL);
                            else
                                strTable.Append("<a  href='../" + Convert.ToString(dr[objA.HyperLinkColumnName]));// link can be different for each row

                            for (int iQs = 0; iQs < objA.QueryStringDataColumn.Length; iQs++)// create querystring
                            {
                                if (iQs == 0)
                                    strTable.Append("?");

                                strTable.Append(objA.QueryStringText[iQs] + "=" + dr[objA.QueryStringDataColumn[iQs]].ToString());

                                if (iQs < objA.QueryStringDataColumn.Length - 1)
                                    strTable.Append("&");
                            }

                            strTable.Append("'");

                            if (!string.IsNullOrWhiteSpace(objA.Target))
                                strTable.Append(" target='" + objA.Target + "' ");
                            else
                                strTable.Append(" target='_blank'");


                            strTable.Append(" > ");

                            strTable.Append(dr[DataColumnsName[i]].ToString().Replace("\n", "<br>"));




                            strTable.Append("</a>");

                        }

                        else
                        {
                            strTable.Append(dr[DataColumnsName[i]].ToString().Replace("\n", "<br>"));
                        }


                        strTable.Append("</td>");
                        // }
                    }
                    strTable.Append("</tr>");
                }
                strTable.Append("</table>");
            }
            else
                strTable.Append("<span style='color:maroon;padding:2px'> No record found !</span>");

            return strTable.ToString();
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {

        }
    }
    /// <summary>
    /// Method is used to bnd vetting snippet
    /// </summary>
    public static string CreateHtmlTableFromDataTable(DataTable dtTable, string[] HeaderCaptions, string[] DataColumnsName, Dictionary<string, UDCHyperLink> DicLink, List<UDCAction> dicAction, Dictionary<string, UDCToolTip> DicToolTip, string[] DataColumnsAlignment, string TableCss, string HeaderCss, string RowStyleCss, string AlternateRowStyleCss)
    {
        StringBuilder strTable = new StringBuilder();

        try
        {

            if (dtTable.Rows.Count > 0)
            {
                strTable.Append("<table id='__tbl_remark' CELLPADDING='2' CELLSPACING='0'  style='border-collapse:collapse' ");
                if (!string.IsNullOrWhiteSpace(TableCss))
                    strTable.Append(" class='" + TableCss + "' ");

                strTable.Append(" > ");

                if (HeaderCaptions.Length > 0)
                {
                    strTable.Append("<tr >");
                    for (int i = 0; i < HeaderCaptions.Length; i++)
                    {
                        strTable.Append("<td  width='auto'");
                        if (!string.IsNullOrWhiteSpace(HeaderCss))
                            strTable.Append(" class='" + HeaderCss + "' ");
                        strTable.Append(" > ");


                        strTable.Append(HeaderCaptions[i]);
                        strTable.Append("</td>");
                    }
                    strTable.Append("</tr>");
                }
                int icol = 0;
                foreach (DataRow dr in dtTable.Rows)
                {
                    strTable.Append("<tr>");
                    for (int i = 0; i < DataColumnsName.Length; i++)
                    {

                        strTable.Append("<td ");
                        if (!string.IsNullOrWhiteSpace(RowStyleCss))
                            strTable.Append(" class='" + RowStyleCss + "' ");

                        if (DataColumnsName[i] == "PLANNED_VETTING")
                            if (dr["PLANNED_VETTING"].ToString() == "")
                                strTable.Append(" style='background-color :Red;'  ");

                        if (DataColumnsAlignment.Length > i)
                            strTable.Append("  align='" + DataColumnsAlignment[i] + "' ");

                        UDCToolTip objToolTip;
                        if (DicToolTip.TryGetValue(DataColumnsName[i], out objToolTip))// check for tool tip
                        {
                            if (objToolTip.UseControlToolTip)
                                strTable.Append(" title='" + ReplaceSpecialCharacter(Convert.ToString(dr[objToolTip.ToolTipSourceColumn])) + "'");
                            else
                            {
                                if (Convert.ToString(dr[objToolTip.ToolTipSourceColumn]).Contains("To Attend TAROKO this call Singapore for"))
                                {
                                    string s = "";
                                }
                                strTable.Append(" onmouseover='js_ShowToolTip(&#39;" + ReplaceSpecialCharacter(Convert.ToString(dr[objToolTip.ToolTipSourceColumn])) + "&#39;,event,this)'");
                            }
                        }

                        strTable.Append(" > ");
                        UDCHyperLink objA;
                     
                        if (DicLink.TryGetValue(DataColumnsName[i], out objA)) //check for hyperlink
                        {
                            if (string.IsNullOrWhiteSpace(objA.HyperLinkColumnName))//link is fixed
                                strTable.Append("<a href='" + objA.NaviagteURL);
                            else
                                strTable.Append("<a  href='../" + Convert.ToString(dr[objA.HyperLinkColumnName]));// link can be different for each row
                              
                            for (int iQs = 0; iQs < objA.QueryStringDataColumn.Length; iQs++)// create querystring
                            {
                                if (iQs == 0)
                                    strTable.Append("?");

                                strTable.Append(objA.QueryStringText[iQs] + "=" + dr[objA.QueryStringDataColumn[iQs]].ToString());

                                if (iQs < objA.QueryStringDataColumn.Length - 1)
                                    strTable.Append("&");
                            }

                            strTable.Append("'");

                            if (!string.IsNullOrWhiteSpace(objA.Target))
                                strTable.Append(" target='" + objA.Target + "' ");
                            else
                                strTable.Append(" target='_blank'");


                            strTable.Append(" > ");

                            strTable.Append(dr[DataColumnsName[i]].ToString().Replace("\n", "<br>"));

                            strTable.Append("</a>");

                        }                
                        else
                        {
                            strTable.Append(dr[DataColumnsName[i]].ToString().Replace("\n", "<br>"));
                        }


                        strTable.Append("</td>");
                        // }
                    }

                    //add action columns
                    if (dicAction.Count > 0)
                    {
                        strTable.Append("<td width='auto'");

                        if (!string.IsNullOrWhiteSpace(RowStyleCss) && (icol % 2) == 0)
                            strTable.Append(" class='" + RowStyleCss + "' ");
                        else if (!string.IsNullOrWhiteSpace(AlternateRowStyleCss))
                            strTable.Append(" class='" + AlternateRowStyleCss + "' ");
                        strTable.Append(">");

                        strTable.Append("<table>");
                        strTable.Append("<tr>");
                        foreach (UDCAction iAction in dicAction)
                        {
                            strTable.Append("<td> <img src='" + iAction.ImageURL + "' width='18' height='18' ");
                            string funparam = "";
                            foreach (string iprm in iAction.ParamDataColumn)
                            {
                                if (funparam.Length > 0)
                                    funparam += ",";

                                funparam += dr[iprm];
                            }

                            funparam += ",event,this";

                            foreach (string[] fun in iAction.FunctionName)
                            {
                                strTable.Append("  " + fun[1] + "='" + fun[0] + "(" + funparam + ")'");
                            }

                            strTable.Append("/>");
                            strTable.Append("</td>");

                        }
                        strTable.Append("</tr>");
                        strTable.Append("</table>");
                        strTable.Append("</td>");
                    }

                    strTable.Append("</tr>");
                    icol++;
                }
                strTable.Append("</table>");
            }
            else
                strTable.Append("<span style='color:maroon;padding:2px'> No record found !</span>");

            return strTable.ToString();
        }
        catch (Exception ex)
        {
            WriteExceptionLog(ex);
            return "";
        }

    }

    public static string CreateHtmlTableFromDataTableNew(DataTable dtTable, string[] HeaderCaptions, string[] DataColumnsName, Dictionary<string, UDCHyperLink> DicLink, Dictionary<string, UDCToolTip> DicToolTip, string[] DataColumnsAlignment, string TableCss, string HeaderCss, string RowStyleCss)
    {
        StringBuilder strTable = new StringBuilder();

        try
        {

            if (dtTable.Rows.Count > 0)
            {
                //ArrayList arrSrcToolTipColumn = new ArrayList(); // store the all sourcetooltip column name in aaray list
                //if (DicToolTip.Keys.Count > 0)
                //{
                //    foreach (UDCToolTip objTP in DicToolTip.Values)
                //    {
                //        arrSrcToolTipColumn.Add(objTP.ToolTipSourceColumn);
                //    }

                //}


                strTable.Append("<table id='__tbl_remark' CELLPADDING='2' CELLSPACING='0'  style='border-collapse:collapse' ");
                if (!string.IsNullOrWhiteSpace(TableCss))
                    strTable.Append(" class='" + TableCss + "' ");

                strTable.Append(" > ");

                if (HeaderCaptions.Length > 0)
                {
                    strTable.Append("<tr >");
                    for (int i = 0; i < HeaderCaptions.Length; i++)
                    {
                        strTable.Append("<td  width='auto'");
                        if (!string.IsNullOrWhiteSpace(HeaderCss))
                            strTable.Append(" class='" + HeaderCss + "' ");
                        strTable.Append(" > ");


                        strTable.Append(HeaderCaptions[i]);
                        strTable.Append("</td>");
                    }
                    strTable.Append("</tr>");
                }
                foreach (DataRow dr in dtTable.Rows)
                {
                    strTable.Append("<tr>");
                    for (int i = 0; i < DataColumnsName.Length; i++)
                    {

                        strTable.Append("<td ");
                        if (!string.IsNullOrWhiteSpace(RowStyleCss))
                            strTable.Append(" class='" + RowStyleCss + "' ");

                        if (DataColumnsAlignment.Length > i)
                            strTable.Append("  align='" + DataColumnsAlignment[i] + "' ");

                        UDCToolTip objToolTip;
                        if (DicToolTip.TryGetValue(DataColumnsName[i], out objToolTip))// check for tool tip
                        {
                            if (objToolTip.UseControlToolTip)
                                strTable.Append(" title='" + ReplaceSpecialCharacter(Convert.ToString(dr[objToolTip.ToolTipSourceColumn])) + "'");
                            else
                            {
                                if (Convert.ToString(dr[objToolTip.ToolTipSourceColumn]).Contains("To Attend TAROKO this call Singapore for"))
                                {
                                    string s = "";
                                }
                                strTable.Append(" onmouseover='js_ShowToolTip(&#39;" + ReplaceSpecialCharacter(Convert.ToString(dr[objToolTip.ToolTipSourceColumn])) + "&#39;,event,this)'");
                            }
                        }

                        strTable.Append(" > ");
                        UDCHyperLink objA;
                        if (DicLink.TryGetValue(DataColumnsName[i], out objA)) //check for hyperlink
                        {
                            if (string.IsNullOrWhiteSpace(objA.HyperLinkColumnName))//link is fixed
                                strTable.Append("<a href='" + objA.NaviagteURL);
                            else
                                strTable.Append("<a  href='" + Convert.ToString(dr[objA.HyperLinkColumnName]));// link can be different for each row

                            for (int iQs = 0; iQs < objA.QueryStringDataColumn.Length; iQs++)// create querystring
                            {
                                if (iQs == 0)
                                    strTable.Append("?");

                                strTable.Append(objA.QueryStringText[iQs] + "=" + dr[objA.QueryStringDataColumn[iQs]].ToString());

                                if (iQs < objA.QueryStringDataColumn.Length - 1)
                                    strTable.Append("&");
                            }

                            strTable.Append("'");

                            if (!string.IsNullOrWhiteSpace(objA.Target))
                                strTable.Append(" target='" + objA.Target + "' ");
                            else
                                strTable.Append(" target='_blank'");


                            strTable.Append(" > ");

                            strTable.Append(dr[DataColumnsName[i]].ToString().Replace("\n", "<br>"));




                            strTable.Append("</a>");

                        }

                        else
                        {
                            strTable.Append(dr[DataColumnsName[i]].ToString().Replace("\n", "<br>"));
                        }


                        strTable.Append("</td>");
                        // }
                    }
                    strTable.Append("</tr>");
                }
                strTable.Append("</table>");
            }
            else
                strTable.Append("<span style='color:maroon;padding:2px'> No record found !</span>");

            return strTable.ToString();
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {

        }
    }

    public static string CreateHtmlTableFromDataTable(DataTable dtTable, string[] HeaderCaptions, string[] DataColumnsName, string[] DataColumnsAlignment, string TableCss = "", string HeaderCss = "", string RowStyleCss = "", UDTRepeatDirection RepeatDirection = UDTRepeatDirection.Horizontal, string PageHeader = "")
    {
        StringBuilder strTable = new StringBuilder();

        try
        {

            if (dtTable.Rows.Count > 0)
            {
                strTable.Append("<table id='__tbl_remark' CELLPADDING='2' CELLSPACING='0'  style='border-collapse:collapse' ");
                if (!string.IsNullOrWhiteSpace(TableCss))
                    strTable.Append(" class='" + TableCss + "' ");

                strTable.Append(" > ");



                #region ----Repeat Horizontal----

                if (RepeatDirection == UDTRepeatDirection.Horizontal)
                {
                    if (PageHeader.Length > 1)
                        strTable.Append("<tr> <td class='CreateHtmlTableFromDataTable-PageHeader'  colspan='" + dtTable.Columns.Count + "' > <b>" + PageHeader + "</b> </td></tr>");


                    if (HeaderCaptions.Length > 0)
                    {
                        strTable.Append("<tr >");
                        for (int i = 0; i < HeaderCaptions.Length; i++)
                        {
                            strTable.Append("<td  width='auto'");
                            if (!string.IsNullOrWhiteSpace(HeaderCss))
                                strTable.Append(" class='" + HeaderCss + "' ");
                            strTable.Append(" > ");

                            strTable.Append(HeaderCaptions[i]);
                            strTable.Append("</td>");
                        }
                        strTable.Append("</tr>");
                    }
                    foreach (DataRow dr in dtTable.Rows)
                    {
                        strTable.Append("<tr>");
                        for (int i = 0; i < DataColumnsName.Length; i++)
                        {
                            strTable.Append("<td ");
                            if (!string.IsNullOrWhiteSpace(RowStyleCss))
                                strTable.Append(" class='" + RowStyleCss + "' ");
                            if (DataColumnsAlignment.Length > 0)
                                strTable.Append("  align='" + DataColumnsAlignment[i] + "' ");

                            strTable.Append(" > ");

                            strTable.Append(dr[DataColumnsName[i]].ToString().Replace("\n", "<br>"));
                            strTable.Append("</td>");
                        }
                        strTable.Append("</tr>");
                    }
                }
                #endregion

                #region ----Repeat Vertical----
                else if (RepeatDirection == UDTRepeatDirection.Vertical)
                {
                    if (PageHeader.Length > 1)
                        strTable.Append("<tr> <td class='CreateHtmlTableFromDataTable-PageHeader'  colspan='2' > <b>" + PageHeader + "</b> </td></tr>");



                    foreach (DataRow dr in dtTable.Rows)
                    {
                        strTable.Append("<tr >");

                        for (int i = 0; i < dtTable.Columns.Count; i++)
                        {

                            strTable.Append("<td  width='auto'");

                            if (!string.IsNullOrWhiteSpace(HeaderCss))
                                strTable.Append(" class='" + HeaderCss + "' ");

                            strTable.Append(" > ");

                            if (HeaderCaptions.Length > i)
                                strTable.Append(HeaderCaptions[i]);

                            strTable.Append("</td>");


                            strTable.Append("<td ");

                            if (!string.IsNullOrWhiteSpace(RowStyleCss))
                                strTable.Append(" class='" + RowStyleCss + "' ");

                            if (DataColumnsAlignment.Length > i)
                                strTable.Append("  align='" + DataColumnsAlignment[i] + "' ");

                            strTable.Append(" > ");

                            if (DataColumnsName.Length > i)
                                strTable.Append(dr[DataColumnsName[i]].ToString().Replace("\n", "<br>"));

                            strTable.Append("</td>");



                            strTable.Append("</tr>");

                        }
                    }
                }
                #endregion


                strTable.Append("</table>");
            }
            else
                strTable.Append("<span style='color:maroon;padding:2px'> No record found !</span>");

            return strTable.ToString();
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {

        }
    }
    public static string CreateHtmlTableFromDataTable(DataTable dtTable, string[] HeaderCaptions, string[] DataColumnsName, Dictionary<string, UDCHyperLink> DicLink, Dictionary<string, UDCToolTip> DicToolTip, string[] DataColumnsAlignment, string TableCss, string HeaderCss, string RowStyleCss, string AlternateRowStyleCss)
    {
        StringBuilder strTable = new StringBuilder();

        try
        {

            if (dtTable.Rows.Count > 0)
            {

                strTable.Append("<table id='__tbl_remark' CELLPADDING='2' CELLSPACING='0'  style='border-collapse:collapse' ");
                if (!string.IsNullOrWhiteSpace(TableCss))
                    strTable.Append(" class='" + TableCss + "' ");

                strTable.Append(" > ");

                if (HeaderCaptions.Length > 0)
                {
                    strTable.Append("<tr >");
                    for (int i = 0; i < HeaderCaptions.Length; i++)
                    {
                        strTable.Append("<td  width='auto'");
                        if (!string.IsNullOrWhiteSpace(HeaderCss))
                            strTable.Append(" class='" + HeaderCss + "' ");

                        if (HeaderCaptions[i].Split('=').Length == 2)
                        {
                            strTable.Append(" onclick='AsyncLoadDataOnSort(&#39;" + HeaderCaptions[i].Split('=')[0] + "&#39;)'");
                            strTable.Append(" style='cursor:default;color:yellow' ");
                        }

                        strTable.Append(" > ");

                        if (HeaderCaptions[i].Split('=').Length == 2)
                            strTable.Append(HeaderCaptions[i].Split('=')[0]);
                        else
                            strTable.Append(HeaderCaptions[i]);

                        strTable.Append("</td>");
                    }
                    //strTable.Append("<td  width='auto' style='text-align:center'  class='" + HeaderCss + "' >");
                    //strTable.Append("Action");
                    //strTable.Append("</td>");

                    strTable.Append("</tr>");
                }

                int icol = 0;

                foreach (DataRow dr in dtTable.Rows)
                {
                    strTable.Append("<tr>");
                    for (int i = 0; i < DataColumnsName.Length; i++)
                    {

                        strTable.Append("<td ");
                        if (!string.IsNullOrWhiteSpace(RowStyleCss) && (icol % 2) == 0)
                            strTable.Append(" class='" + RowStyleCss + "' ");
                        else if (!string.IsNullOrWhiteSpace(AlternateRowStyleCss))
                            strTable.Append(" class='" + AlternateRowStyleCss + "' ");

                        if (DataColumnsAlignment.Length > i)
                            strTable.Append("  align='" + DataColumnsAlignment[i] + "' ");

                        #region . tooltip and  javascript event

                        UDCToolTip objToolTip;
                        if (DicToolTip.TryGetValue(DataColumnsName[i], out objToolTip))// check for tool tip
                        {
                            if (objToolTip.UseControlToolTip)
                                strTable.Append(" title='" + ReplaceSpecialCharacter(Convert.ToString(dr[objToolTip.ToolTipSourceColumn])) + "'");
                            else
                            {
                                if (Convert.ToString(dr[objToolTip.ToolTipSourceColumn]).Contains("To Attend TAROKO this call Singapore for"))
                                {
                                    string s = "";
                                }
                                strTable.Append(" onmouseover='js_ShowToolTip(&#39;" + ReplaceSpecialCharacter(Convert.ToString(dr[objToolTip.ToolTipSourceColumn])) + "&#39;,event,this)'");
                                strTable.Append(" style='cursor:default' ");
                            }
                        }

                        #endregion

                        strTable.Append(" > ");

                        #region . column data

                        UDCHyperLink objA;
                        if (DicLink.TryGetValue(DataColumnsName[i], out objA)) //check for hyperlink
                        {


                            if (string.IsNullOrWhiteSpace(objA.HyperLinkColumnName))//link is fixed
                                strTable.Append("<a href='" + objA.NaviagteURL);
                            else
                                strTable.Append("<a  href='../" + Convert.ToString(dr[objA.HyperLinkColumnName]));// link can be different for each row

                            for (int iQs = 0; iQs < objA.QueryStringDataColumn.Length; iQs++)// create querystring
                            {
                                if (iQs == 0)
                                    strTable.Append("?");

                                strTable.Append(objA.QueryStringText[iQs] + "=" + dr[objA.QueryStringDataColumn[iQs]].ToString());

                                if (iQs < objA.QueryStringDataColumn.Length - 1)
                                    strTable.Append("&");
                            }
                            strTable.Append("'");

                            if (!string.IsNullOrWhiteSpace(objA.Target))
                                strTable.Append(" target='" + objA.Target + "' ");
                            else
                                strTable.Append(" target='_blank'");


                            strTable.Append(" > ");

                            strTable.Append(dr[DataColumnsName[i]].ToString().Replace("\n", "<br>"));
                            strTable.Append("</a>");

                        }

                        else
                        {
                            strTable.Append(dr[DataColumnsName[i]].ToString().Replace("\n", "<br>"));
                        }

                        #endregion

                        strTable.Append("</td>");
                        // }
                    }
                    strTable.Append("</tr>");

                    icol++;
                }
                strTable.Append("</table>");
            }
            else
                strTable.Append("<span style='color:maroon;padding:2px'> No record found !</span>");

            return strTable.ToString();
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {

        }
    }


    public static string CreateHtmlTableFromDataTable(DataTable dtTable, string[] HeaderCaptions, string[] DataColumnsName, Dictionary<string, UDCHyperLink> DicLink, Dictionary<string, UDCToolTip> DicToolTip, string[] DataColumnsAlignment, string TableCss, string HeaderCss, string RowStyleCss, string AlternateRowStyleCss, List<UDCAction> dicAction)
    {
        StringBuilder strTable = new StringBuilder();

        try
        {

            if (dtTable.Rows.Count > 0)
            {

                strTable.Append("<table id='__tbl_remark' CELLPADDING='2' CELLSPACING='0'  style='border-collapse:collapse' ");
                if (!string.IsNullOrWhiteSpace(TableCss))
                    strTable.Append(" class='" + TableCss + "' ");

                strTable.Append(" > ");

                if (HeaderCaptions.Length > 0)
                {
                    strTable.Append("<tr >");
                    for (int i = 0; i < HeaderCaptions.Length; i++)
                    {
                        strTable.Append("<td  width='auto'");
                        if (!string.IsNullOrWhiteSpace(HeaderCss))
                            strTable.Append(" class='" + HeaderCss + "' ");

                        if (HeaderCaptions[i].Split('=').Length == 2)
                        {
                            strTable.Append(" onclick='AsyncLoadDataOnSort(&#39;" + HeaderCaptions[i].Split('=')[1] + "&#39;)'");
                            strTable.Append(" style='cursor:default;color:yellow' ");
                        }

                        strTable.Append(" > ");

                        if (HeaderCaptions[i].Split('=').Length == 2)
                            strTable.Append(HeaderCaptions[i].Split('=')[0]);
                        else
                            strTable.Append(HeaderCaptions[i]);

                        strTable.Append("</td>");
                    }
                    strTable.Append("<td  width='auto' style='text-align:center'  class='" + HeaderCss + "' >");
                    strTable.Append("Action");
                    strTable.Append("</td>");

                    strTable.Append("</tr>");
                }

                int icol = 0;

                foreach (DataRow dr in dtTable.Rows)
                {
                    strTable.Append("<tr>");
                    for (int i = 0; i < DataColumnsName.Length; i++)
                    {

                        strTable.Append("<td ");
                        if (!string.IsNullOrWhiteSpace(RowStyleCss) && (icol % 2) == 0)
                            strTable.Append(" class='" + RowStyleCss + "' ");
                        else if (!string.IsNullOrWhiteSpace(AlternateRowStyleCss))
                            strTable.Append(" class='" + AlternateRowStyleCss + "' ");

                        if (DataColumnsAlignment.Length > i)
                            strTable.Append("  align='" + DataColumnsAlignment[i] + "' ");

                        #region . tooltip and  javascript event

                        UDCToolTip objToolTip;
                        if (DicToolTip.TryGetValue(DataColumnsName[i], out objToolTip))// check for tool tip
                        {
                            if (objToolTip.UseControlToolTip)
                                strTable.Append(" title='" + ReplaceSpecialCharacter(Convert.ToString(dr[objToolTip.ToolTipSourceColumn])) + "'");
                            else
                            {
                                if (Convert.ToString(dr[objToolTip.ToolTipSourceColumn]).Contains("To Attend TAROKO this call Singapore for"))
                                {
                                    string s = "";
                                }
                                strTable.Append(" onmouseover='js_ShowToolTip(&#39;" + ReplaceSpecialCharacter(Convert.ToString(dr[objToolTip.ToolTipSourceColumn])) + "&#39;,event,this)'");
                                strTable.Append(" style='cursor:default' ");
                            }
                        }



                        //if (dicJSEvent.Count > 0)
                        //{
                        //    foreach (UDCJSEvent ievt in dicJSEvent)
                        //    {
                        //        if (ievt.EventColumnName.ToUpper() == DataColumnsName[i].ToUpper())
                        //        {
                        //            string funparam = "";
                        //            foreach (string iprm in ievt.ParamDataColumn)
                        //            {
                        //                if (funparam.Length > 0)
                        //                    funparam += ",";

                        //                funparam += dr[iprm];
                        //            }

                        //            funparam += ",event,this";

                        //            foreach (string[] fun in ievt.FunctionName)
                        //            {
                        //                strTable.Append("  " + fun[1] + "='" + fun[0] + "(" + funparam + ")'");
                        //            }

                        //            strTable.Append(" style='cursor:default' ");
                        //        }
                        //    }
                        //}

                        #endregion

                        strTable.Append(" > ");

                        #region . column data

                        UDCHyperLink objA;
                        if (DicLink.TryGetValue(DataColumnsName[i], out objA)) //check for hyperlink
                        {


                            if (string.IsNullOrWhiteSpace(objA.HyperLinkColumnName))//link is fixed
                                strTable.Append("<a href='" + objA.NaviagteURL);
                            else
                                strTable.Append("<a  href='../" + Convert.ToString(dr[objA.HyperLinkColumnName]));// link can be different for each row

                            for (int iQs = 0; iQs < objA.QueryStringDataColumn.Length; iQs++)// create querystring
                            {
                                if (iQs == 0)
                                    strTable.Append("?");

                                strTable.Append(objA.QueryStringText[iQs] + "=" + dr[objA.QueryStringDataColumn[iQs]].ToString());

                                if (iQs < objA.QueryStringDataColumn.Length - 1)
                                    strTable.Append("&");
                            }
                            strTable.Append("'");

                            if (!string.IsNullOrWhiteSpace(objA.Target))
                                strTable.Append(" target='" + objA.Target + "' ");
                            else
                                strTable.Append(" target='_blank'");


                            strTable.Append(" onclick=onlinkClick(");
                            for (int iQs = 0; iQs < objA.QueryStringDataColumn.Length; iQs++)// create querystring
                            {
                                if (iQs == 0)
                                    strTable.Append("");

                                strTable.Append(dr[objA.QueryStringDataColumn[iQs]].ToString());

                                if (iQs < objA.QueryStringDataColumn.Length - 1)
                                    strTable.Append(",");


                            }
                            strTable.Append(")");

                            strTable.Append(" > ");

                            strTable.Append(dr[DataColumnsName[i]].ToString().Replace("\n", "<br>"));
                            strTable.Append("</a>");

                        }

                        else
                        {
                            strTable.Append(dr[DataColumnsName[i]].ToString().Replace("\n", "<br>"));
                        }

                        #endregion

                        strTable.Append("</td>");
                        // }
                    }




                    //add action columns
                    if (dicAction.Count > 0)
                    {
                        strTable.Append("<td width='auto'");

                        if (!string.IsNullOrWhiteSpace(RowStyleCss) && (icol % 2) == 0)
                            strTable.Append(" class='" + RowStyleCss + "' ");
                        else if (!string.IsNullOrWhiteSpace(AlternateRowStyleCss))
                            strTable.Append(" class='" + AlternateRowStyleCss + "' ");
                        strTable.Append(">");

                        strTable.Append("<table>");
                        strTable.Append("<tr>");
                        foreach (UDCAction iAction in dicAction)
                        {
                            strTable.Append("<td> <img src='" + iAction.ImageURL + "' width='18' height='18' ");
                            string funparam = "";
                            foreach (string iprm in iAction.ParamDataColumn)
                            {
                                if (funparam.Length > 0)
                                    funparam += ",";

                                funparam += dr[iprm];
                            }

                            funparam += ",event,this";

                            foreach (string[] fun in iAction.FunctionName)
                            {
                                strTable.Append("  " + fun[1] + "='" + fun[0] + "(" + funparam + ")'");
                            }

                            strTable.Append("/>");
                            strTable.Append("</td>");

                        }
                        strTable.Append("</tr>");
                        strTable.Append("</table>");
                        strTable.Append("</td>");
                    }


                    strTable.Append("</tr>");

                    icol++;
                }
                strTable.Append("</table>");
            }
            else
                strTable.Append("<span style='color:maroon;padding:2px'> No record found !</span>");

            return strTable.ToString();
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {

        }
    }

    public static string CreateHtmlTableFromDataTable(DataTable dtTable, string[] HeaderCaptions, string[] DataColumnsName, Dictionary<string, UDCHyperLink> DicLink, Dictionary<string, UDCToolTip> DicToolTip, string[] DataColumnsAlignment, string TableCss, string HeaderCss, string RowStyleCss, string AlternateRowStyleCss, List<UDCAction> dicAction, List<UDCJSEvent> dicJSEvent)
    {
        StringBuilder strTable = new StringBuilder();

        try
        {

            if (dtTable.Rows.Count > 0)
            {

                strTable.Append("<table id='__tbl_remark' CELLPADDING='2' CELLSPACING='0'  style='border-collapse:collapse' ");
                if (!string.IsNullOrWhiteSpace(TableCss))
                    strTable.Append(" class='" + TableCss + "' ");

                strTable.Append(" > ");

                if (HeaderCaptions.Length > 0)
                {
                    strTable.Append("<tr >");
                    for (int i = 0; i < HeaderCaptions.Length; i++)
                    {
                        strTable.Append("<td  width='auto'");
                        if (!string.IsNullOrWhiteSpace(HeaderCss))
                            strTable.Append(" class='" + HeaderCss + "' ");

                        if (HeaderCaptions[i].Split('=').Length == 2)
                        {
                            strTable.Append(" onclick='AsyncLoadDataOnSort(&#39;" + HeaderCaptions[i].Split('=')[1] + "&#39;)'");
                            strTable.Append(" style='cursor:default;color:yellow' ");
                        }

                        strTable.Append(" > ");

                        if (HeaderCaptions[i].Split('=').Length == 2)
                            strTable.Append(HeaderCaptions[i].Split('=')[0]);
                        else
                            strTable.Append(HeaderCaptions[i]);

                        strTable.Append("</td>");
                    }

                    if (dicAction.Count > 0)
                    {
                        strTable.Append("<td  width='auto' style='text-align:center'  class='" + HeaderCss + "' >");
                        strTable.Append("Action");
                        strTable.Append("</td>");
                    }

                    strTable.Append("</tr>");
                }

                int icol = 0;

                foreach (DataRow dr in dtTable.Rows)
                {
                    strTable.Append("<tr>");
                    for (int i = 0; i < DataColumnsName.Length; i++)
                    {

                        strTable.Append("<td ");
                        if (!string.IsNullOrWhiteSpace(RowStyleCss) && (icol % 2) == 0)
                            strTable.Append(" class='" + RowStyleCss + "' ");
                        else if (!string.IsNullOrWhiteSpace(AlternateRowStyleCss))
                            strTable.Append(" class='" + AlternateRowStyleCss + "' ");

                        if (DataColumnsAlignment.Length > i)
                            strTable.Append("  align='" + DataColumnsAlignment[i] + "' ");

                        #region . tooltip and  javascript event

                        UDCToolTip objToolTip;
                        if (DicToolTip.TryGetValue(DataColumnsName[i], out objToolTip))// check for tool tip
                        {
                            if (objToolTip.UseControlToolTip)
                                strTable.Append(" title='" + ReplaceSpecialCharacter(Convert.ToString(dr[objToolTip.ToolTipSourceColumn])) + "'");
                            else
                            {
                                if (Convert.ToString(dr[objToolTip.ToolTipSourceColumn]).Contains("To Attend TAROKO this call Singapore for"))
                                {
                                    string s = "";
                                }
                                strTable.Append(" onmouseover='js_ShowToolTip(&#39;" + ReplaceSpecialCharacter(Convert.ToString(dr[objToolTip.ToolTipSourceColumn])) + "&#39;,event,this)'");
                                strTable.Append(" style='cursor:default' ");
                            }
                        }



                        if (dicJSEvent.Count > 0)
                        {
                            foreach (UDCJSEvent ievt in dicJSEvent)
                            {
                                if (ievt.EventColumnName.ToUpper() == DataColumnsName[i].ToUpper())
                                {
                                    string funparam = "";
                                    foreach (string iprm in ievt.ParamDataColumn)
                                    {
                                        if (funparam.Length > 0)
                                            funparam += ",";

                                        funparam += dr[iprm];
                                    }

                                    funparam += ",event,this";

                                    foreach (string[] fun in ievt.FunctionName)
                                    {
                                        strTable.Append("  " + fun[1] + "='" + fun[0] + "(" + funparam + ")'");
                                    }

                                    strTable.Append(" style='cursor:default' ");
                                }
                            }
                        }

                        #endregion

                        strTable.Append(" > ");

                        #region . column data

                        UDCHyperLink objA;
                        if (DicLink.TryGetValue(DataColumnsName[i], out objA)) //check for hyperlink
                        {


                            if (string.IsNullOrWhiteSpace(objA.HyperLinkColumnName))//link is fixed
                                strTable.Append("<a href='" + objA.NaviagteURL);
                            else
                                strTable.Append("<a  href='../" + Convert.ToString(dr[objA.HyperLinkColumnName]));// link can be different for each row

                            for (int iQs = 0; iQs < objA.QueryStringDataColumn.Length; iQs++)// create querystring
                            {
                                if (iQs == 0)
                                    strTable.Append("?");

                                strTable.Append(objA.QueryStringText[iQs] + "=" + dr[objA.QueryStringDataColumn[iQs]].ToString());

                                if (iQs < objA.QueryStringDataColumn.Length - 1)
                                    strTable.Append("&");
                            }
                            strTable.Append("'");

                            if (!string.IsNullOrWhiteSpace(objA.Target))
                                strTable.Append(" target='" + objA.Target + "' ");
                            else
                                strTable.Append(" target='_blank'");


                            strTable.Append(" > ");

                            strTable.Append(dr[DataColumnsName[i]].ToString().Replace("\n", "<br>"));
                            strTable.Append("</a>");

                        }

                        else
                        {
                            strTable.Append(dr[DataColumnsName[i]].ToString().Replace("\n", "<br>"));
                        }

                        #endregion

                        strTable.Append("</td>");
                        // }
                    }




                    //add action columns
                    if (dicAction.Count > 0)
                    {
                        strTable.Append("<td width='auto'");

                        if (!string.IsNullOrWhiteSpace(RowStyleCss) && (icol % 2) == 0)
                            strTable.Append(" class='" + RowStyleCss + "' ");
                        else if (!string.IsNullOrWhiteSpace(AlternateRowStyleCss))
                            strTable.Append(" class='" + AlternateRowStyleCss + "' ");
                        strTable.Append(">");

                        strTable.Append("<table>");
                        strTable.Append("<tr>");
                        foreach (UDCAction iAction in dicAction)
                        {
                            strTable.Append("<td> <img src='" + iAction.ImageURL + "' width='18' height='18' ");
                            string funparam = "";
                            foreach (string iprm in iAction.ParamDataColumn)
                            {
                                if (funparam.Length > 0)
                                    funparam += ",";

                                funparam += "'" + dr[iprm] + "'";
                            }

                            funparam += ",event,this";

                            foreach (string[] fun in iAction.FunctionName)
                            {
                                strTable.Append(fun[1] + "=\"" + fun[0] + "(" + funparam + ")\"");
                            }

                            strTable.Append("/>");
                            strTable.Append("</td>");

                        }
                        strTable.Append("</tr>");
                        strTable.Append("</table>");
                        strTable.Append("</td>");
                    }


                    strTable.Append("</tr>");

                    icol++;
                }
                strTable.Append("</table>");
            }
            else
                strTable.Append("<span style='color:maroon;padding:2px'> No record found !</span>");

            return strTable.ToString();
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {

        }
    }

    /* CreateHtmlTableFromDataTableWithCheckBox Created By Someshwar on 21-11-2014 */
    public static string CreateHtmlTableFromDataTableWithCheckBox(DataTable dtTable, string[] HeaderCaptions, string[] DataColumnsName, Dictionary<string, UDCHyperLinkImage> DicLink, Dictionary<string, UDCToolTip> DicToolTip, string[] DataColumnsAlignment, string TableID, string TableCss, string HeaderCss, string RowStyleCss, string AlternateRowStyleCss, List<UDCActionNew> dicAction, List<UDCJSEvent> dicJSEvent, Dictionary<string, UDCCheckBox> dicCheckBox)
    {
        StringBuilder strTable = new StringBuilder();

        try
        {

            if (dtTable.Rows.Count > 0)
            {

                strTable.Append("<table id='" + TableID + "' CELLPADDING='2' CELLSPACING='0'  style='border-collapse:collapse' ");
                if (!string.IsNullOrWhiteSpace(TableCss))
                    strTable.Append(" class='" + TableCss + "' ");

                strTable.Append(" > ");

                if (HeaderCaptions.Length > 0)
                {
                    strTable.Append("<tr >");
                    for (int i = 0; i < HeaderCaptions.Length; i++)
                    {
                        strTable.Append("<th  width='auto' ");
                        if (!string.IsNullOrWhiteSpace(HeaderCss))
                            strTable.Append(" class='" + HeaderCss + "' ");

                        if (HeaderCaptions[i].Split('=').Length == 2)
                        {
                            if (TableID.Contains("Location"))
                            {
                                strTable.Append(" onclick='AsyncLoadLocationDataOnSort(&#39;" + HeaderCaptions[i].Split('=')[0] + "&#39;)'");
                                strTable.Append(" style='cursor:default;color: #0071BC;text-align:left;' ");
                            }
                            if (TableID.Contains("Spare"))
                            {
                                //if ((HeaderCaptions[i] == "Part Number=") || HeaderCaptions[i] == "Name=")
                                //{
                                //    strTable.Append(" onclick='AsyncLoadSpareDataOnSort(&#39;" + HeaderCaptions[i].Split('=')[0] + "&#39;)'");
                                //    strTable.Append(" style='cursor:default;text-align:left;' ");
                                //}
                                //else
                                {
                                    strTable.Append(" onclick='AsyncLoadSpareDataOnSort(&#39;" + HeaderCaptions[i].Split('=')[0] + "&#39;)'");
                                    strTable.Append(" style='cursor:default;color: #0071BC;text-align:left;' ");
                                }
                            }
                            if (TableID.Contains("Job"))
                            {
                                strTable.Append(" onclick='AsyncLoadJobDataOnSort(&#39;" + HeaderCaptions[i].Split('=')[0] + "&#39;)'");
                                strTable.Append(" style='cursor:default;color: #0071BC;text-align:left;' ");
                            }
                        }

                        if (DataColumnsAlignment.Length > i)
                            strTable.Append("  align='" + DataColumnsAlignment[i] + "' ");

                        strTable.Append(" > ");

                        if (HeaderCaptions[i].Split('=').Length == 2)
                            strTable.Append(HeaderCaptions[i].Split('=')[0]);
                        else
                            strTable.Append(HeaderCaptions[i]);

                        strTable.Append("</th>");
                    }

                    if (dicAction.Count > 0)
                    {
                        strTable.Append("<th  width='auto' style='text-align:left'  class='" + HeaderCss + "' >");
                        strTable.Append("Action");
                        strTable.Append("</th>");
                    }

                    strTable.Append("</tr>");

                }

                int icol = 0;
                int rownum = 1;
                Boolean columnadded = false;
                foreach (DataRow dr in dtTable.Rows)
                {
                    strTable.Append("<tr>");

                    for (int i = 0; i < DataColumnsName.Length; i++)
                    {
                        columnadded = false;

                        if (dtTable.Columns.Contains("Active_Status"))
                        {
                            if (dr["Active_Status"].ToString() == "0")
                            {
                                strTable.Append("<td style=color:red;");
                                columnadded = true;
                            }

                        }

                        if (dtTable.Columns.Contains("Critical"))
                        {
                            if (DataColumnsName[i].ToString() == "Critical")
                            {
                                if (dr["Critical"].ToString() == "Y")
                                {

                                    strTable.Append("<td style=background-color:red;color:white;font-weight:bold;text-align:" + DataColumnsAlignment[i] + ";");
                                    columnadded = true;
                                }

                            }
                        }
                        if (dtTable.Columns.Contains("CMS"))
                        {
                            if (DataColumnsName[i].ToString() == "CMS")
                            {
                                if (dr["CMS"].ToString() == "Y")
                                {


                                    strTable.Append("<td style=background-color:green;color:white;font-weight:bold;text-align:" + DataColumnsAlignment[i] + ";");
                                    columnadded = true;
                                }

                            }
                        }
                        if (columnadded == false)
                        {
                            strTable.Append("<td");
                        }
                        if (!string.IsNullOrWhiteSpace(RowStyleCss) && (icol % 2) == 0)
                            strTable.Append(" class='" + RowStyleCss + "' ");
                        else if (!string.IsNullOrWhiteSpace(AlternateRowStyleCss))
                            strTable.Append(" class='" + AlternateRowStyleCss + "' ");

                        if (DataColumnsAlignment.Length > i)
                            strTable.Append("  align='" + DataColumnsAlignment[i] + "' ");

                        #region . tooltip and  javascript event

                        UDCToolTip objToolTip;
                        if (DicToolTip.TryGetValue(DataColumnsName[i], out objToolTip))// check for tool tip
                        {
                            if (objToolTip.UseControlToolTip)
                                strTable.Append(" title='" + ReplaceSpecialCharacter(Convert.ToString(dr[objToolTip.ToolTipSourceColumn])) + "'");
                            else
                            {
                                if (Convert.ToString(dr[objToolTip.ToolTipSourceColumn]).Contains("To Attend TAROKO this call Singapore for"))
                                {
                                    string s = "";
                                }
                                strTable.Append(" onmouseover='js_ShowToolTip(&#39; " + ReplaceSpecialCharacter(" <div style='width:400px; white-space:normal'>" + Convert.ToString(dr[objToolTip.ToolTipSourceColumn])) + " &#39;,event,this)'");
                                strTable.Append(" style='cursor:default; white-space:normal' ");
                            }
                        }



                        if (dicJSEvent.Count > 0)
                        {
                            foreach (UDCJSEvent ievt in dicJSEvent)
                            {
                                if (ievt.EventColumnName.ToUpper() == DataColumnsName[i].ToUpper())
                                {
                                    string funparam = "";
                                    foreach (string iprm in ievt.ParamDataColumn)
                                    {
                                        if (funparam.Length > 0)
                                            funparam += ",";

                                        funparam += dr[iprm];
                                    }

                                    funparam += ",event,this";

                                    foreach (string[] fun in ievt.FunctionName)
                                    {
                                        strTable.Append("  " + fun[1] + "='" + fun[0] + "(" + funparam + ")'");
                                    }

                                    strTable.Append(" style='cursor:default' ");
                                }
                            }
                        }

                        #endregion

                        strTable.Append(" > ");

                        #region . column data

                        UDCHyperLinkImage objA;
                        UDCCheckBox objChk;
                        if (DicLink.TryGetValue(DataColumnsName[i], out objA)) //check for hyperlink
                        {

                            if (dr[objA.QueryStringDataColumn[0]] != null && dr[objA.QueryStringDataColumn[0]].ToString() != "")
                            {
                                if (string.IsNullOrWhiteSpace(objA.HyperLinkColumnName))//link is fixed
                                    strTable.Append("<a href='" + objA.NaviagteURL);
                                else
                                    strTable.Append("<a  href='../" + objA.NaviagteURL + Convert.ToString(dr[objA.QueryStringDataColumn[0]]));// link can be different for each row

                                for (int iQs = 0; iQs < objA.QueryStringDataColumn.Length; iQs++)// create querystring
                                {
                                    if (iQs == 0)
                                        strTable.Append("?");

                                    strTable.Append(objA.QueryStringText[iQs] + "=" + dr[objA.QueryStringDataColumn[iQs]].ToString());

                                    if (iQs < objA.QueryStringDataColumn.Length - 1)
                                        strTable.Append("&");
                                }
                                strTable.Append("'");

                                if (!string.IsNullOrWhiteSpace(objA.Target))
                                    strTable.Append(" target='" + objA.Target + "' ");
                                else
                                    strTable.Append(" target='_blank'");


                                strTable.Append(" > ");
                                //  if (!string.IsNullOrWhiteSpace(objA.HyperLinkColumnName))
                                // {
                                if (dr[objA.HyperLinkColumnName].ToString() != "")
                                {
                                    strTable.Append("<img src='" + objA.ImageURL + "' width='18' height='18' />");
                                }

                                //  }
                                // strTable.Append(dr[DataColumnsName[i]].ToString().Replace("\n", "<br>"));
                                strTable.Append("</a>");
                            }

                        }
                        else if (dicCheckBox.TryGetValue(DataColumnsName[i], out objChk)) //check for CheckBox
                        {
                            string funparam = "";

                            if (dr[DataColumnsName[i]].ToString() == "1" || dr[DataColumnsName[i]].ToString() == "True")
                            {

                                strTable.Append("<input type='checkbox' id=" + TableID + "_" + DataColumnsName[i] + rownum + " checked=true disabled=false  ");
                                if (objChk.FunctionName != null)
                                {
                                    foreach (string[] fun in objChk.FunctionName)
                                    {
                                        // funparam = TableID + "_" + DataColumnsName[i] + rownum;
                                        //strTable.Append(fun[1] + "=\"" + fun[0] + "(this)\"");
                                        //strTable.Append(fun[1] + "=\"" + fun[0] + "(" + dr[DataColumnsName[1]].ToString() + ",this)\"");
                                        strTable.Append(fun[1] + "=\"" + fun[0] + "('" + dr[objChk.ParamDataColumn[0]] + "',this)\"");
                                    }
                                }
                                strTable.Append(" />");


                            }
                            else
                            {
                                strTable.Append("<input type='checkbox' id=" + TableID + "_" + DataColumnsName[i] + rownum + " ");
                                if (objChk.FunctionName != null)
                                {
                                    foreach (string[] fun in objChk.FunctionName)
                                    {
                                        //funparam = TableID + "_" + DataColumnsName[i] + rownum;
                                        strTable.Append(fun[1] + "=\"" + fun[0] + "('" + dr[objChk.ParamDataColumn[0]] + "',this)\"");
                                    }
                                }
                                strTable.Append(" />");
                            }

                        }

                        else
                        {


                            strTable.Append(dr[DataColumnsName[i]].ToString().Replace("\n", "<br>"));


                        }

                        #endregion








                        strTable.Append("</td>");

                        // }
                    }




                    //add action columns
                    if (dicAction.Count > 0)
                    {
                        strTable.Append("<td width='auto'");

                        if (!string.IsNullOrWhiteSpace(RowStyleCss) && (icol % 2) == 0)
                            strTable.Append(" class='" + RowStyleCss + "' ");
                        else if (!string.IsNullOrWhiteSpace(AlternateRowStyleCss))
                            strTable.Append(" class='" + AlternateRowStyleCss + "' ");
                        strTable.Append(">");

                        strTable.Append("<table>");
                        strTable.Append("<tr>");
                        foreach (UDCActionNew iAction in dicAction)
                        {
                            if (dr["Active_Status"].ToString() == "0")
                            {
                                if (iAction.ActionName == "Restore")
                                {
                                    strTable.Append("<td style='cursor:pointer;' onmouseover='js_ShowToolTip(&#39;" + iAction.ActionName + "&#39;,event,this)'> <img src='" + iAction.ImageURL + "' width='18' height='18' ");
                                    string funparam = "";
                                    foreach (string iprm in iAction.ParamDataColumn)
                                    {
                                        if (funparam.Length > 0)
                                            funparam += ",";

                                        funparam += "'" + dr[iprm] + "'";
                                    }

                                    funparam += ",event,this";

                                    foreach (string[] fun in iAction.FunctionName)
                                    {
                                        strTable.Append(fun[1] + "=\"" + fun[0] + "(" + funparam + ")\"");
                                    }

                                    strTable.Append("/>");
                                    strTable.Append("</td>");
                                }
                            }
                            else
                            {
                                if (iAction.ActionName != "Restore")
                                {
                                    strTable.Append("<td style='cursor:pointer;' onmouseover='js_ShowToolTip(&#39;" + iAction.ActionName + "&#39;,event,this)'> <img src='" + iAction.ImageURL + "' width='18' height='18' ");
                                    string funparam = "";
                                    foreach (string iprm in iAction.ParamDataColumn)
                                    {
                                        if (funparam.Length > 0)
                                            funparam += ",";

                                        funparam += "'" + dr[iprm] + "'";
                                    }

                                    funparam += ",event,this";

                                    foreach (string[] fun in iAction.FunctionName)
                                    {
                                        strTable.Append(fun[1] + "=\"" + fun[0] + "(" + funparam + ")\"");
                                    }

                                    strTable.Append("/>");
                                    strTable.Append("</td>");
                                }
                            }

                        }
                        strTable.Append("</tr>");
                        strTable.Append("</table>");
                        strTable.Append("</td>");
                    }


                    strTable.Append("</tr>");

                    icol++;
                    rownum += 1;
                }
                strTable.Append("</table>");
            }
            else
                strTable.Append("<span style='color:maroon;padding:2px'> No record found !</span>");

            return strTable.ToString();
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {

        }
    }

    /* CreateHtmlTableFromDataTableWithCustomizeError Created By Someshwar on 24-11-2014 */
    public static string CreateHtmlTableFromDataTableWithCustomizeError(DataTable dtTable, string[] HeaderCaptions, string[] DataColumnsName, string[] DataColumnsAlignment, string NoRowError, string TableCss = "", string HeaderCss = "", string RowStyleCss = "", UDTRepeatDirection RepeatDirection = UDTRepeatDirection.Horizontal, string PageHeader = "")
    {
        StringBuilder strTable = new StringBuilder();

        try
        {

            if (dtTable.Rows.Count > 0)
            {
                strTable.Append("<table id='__tbl_remark' CELLPADDING='2' CELLSPACING='0'  style='border-collapse:collapse' ");
                if (!string.IsNullOrWhiteSpace(TableCss))
                    strTable.Append(" class='" + TableCss + "' ");

                strTable.Append(" > ");



                #region ----Repeat Horizontal----

                if (RepeatDirection == UDTRepeatDirection.Horizontal)
                {
                    if (PageHeader.Length > 1)
                        strTable.Append("<tr> <td class='CreateHtmlTableFromDataTable-PageHeader'  colspan='" + dtTable.Columns.Count + "' > <b>" + PageHeader + "</b> </td></tr>");


                    if (HeaderCaptions.Length > 0)
                    {
                        strTable.Append("<tr >");
                        for (int i = 0; i < HeaderCaptions.Length; i++)
                        {
                            strTable.Append("<td  width='auto'");
                            if (!string.IsNullOrWhiteSpace(HeaderCss))
                                strTable.Append(" class='" + HeaderCss + "' ");
                            strTable.Append(" > ");

                            strTable.Append(HeaderCaptions[i]);
                            strTable.Append("</td>");
                        }
                        strTable.Append("</tr>");
                    }
                    foreach (DataRow dr in dtTable.Rows)
                    {
                        strTable.Append("<tr>");
                        for (int i = 0; i < DataColumnsName.Length; i++)
                        {
                            strTable.Append("<td ");
                            if (!string.IsNullOrWhiteSpace(RowStyleCss))
                                strTable.Append(" class='" + RowStyleCss + "' ");
                            if (DataColumnsAlignment.Length > 0)
                                strTable.Append("  align='" + DataColumnsAlignment[i] + "' ");

                            strTable.Append(" > ");

                            strTable.Append(dr[DataColumnsName[i]].ToString().Replace("\n", "<br>"));
                            strTable.Append("</td>");
                        }
                        strTable.Append("</tr>");
                    }
                }
                #endregion

                #region ----Repeat Vertical----
                else if (RepeatDirection == UDTRepeatDirection.Vertical)
                {
                    if (PageHeader.Length > 1)
                        strTable.Append("<tr> <td class='CreateHtmlTableFromDataTable-PageHeader'  colspan='2' > <b>" + PageHeader + "</b> </td></tr>");



                    foreach (DataRow dr in dtTable.Rows)
                    {
                        strTable.Append("<tr >");

                        for (int i = 0; i < dtTable.Columns.Count; i++)
                        {

                            strTable.Append("<td  width='auto'");

                            if (!string.IsNullOrWhiteSpace(HeaderCss))
                                strTable.Append(" class='" + HeaderCss + "' ");

                            strTable.Append(" > ");

                            if (HeaderCaptions.Length > i)
                                strTable.Append(HeaderCaptions[i]);

                            strTable.Append("</td>");


                            strTable.Append("<td ");

                            if (!string.IsNullOrWhiteSpace(RowStyleCss))
                                strTable.Append(" class='" + RowStyleCss + "' ");

                            if (DataColumnsAlignment.Length > i)
                                strTable.Append("  align='" + DataColumnsAlignment[i] + "' ");

                            strTable.Append(" > ");

                            if (DataColumnsName.Length > i)
                                strTable.Append(dr[DataColumnsName[i]].ToString().Replace("\n", "<br>"));

                            strTable.Append("</td>");



                            strTable.Append("</tr>");

                        }
                    }
                }
                #endregion


                strTable.Append("</table>");
            }
            else
                strTable.Append("<span style='color:maroon;padding:2px'>" + NoRowError + "</span>");

            return strTable.ToString();
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {

        }
    }
    /* CreateHtmlDataListFromDataTable Created By Someshwar on 27-11-2014 */
    public static string CreateHtmlDataListFromDataTable(DataTable dtTable, string[] HeaderCaptions, string[] DataColumnsName, Dictionary<string, UDCHyperLink> DicLink, Dictionary<string, UDCToolTip> DicToolTip, string[] DataColumnsAlignment, string TableID, string TableCss, string HeaderCss, string RowStyleCss, string AlternateRowStyleCss, List<UDCActionNew> dicAction, List<UDCJSEvent> dicJSEvent, Dictionary<string, UDCCheckBox> dicCheckBox)
    {
        StringBuilder strTable = new StringBuilder();



        try
        {

            if (dtTable.Rows.Count > 0)
            {

                strTable.Append("<table id='" + TableID + "' CELLPADDING='2' CELLSPACING='0' ");
                if (!string.IsNullOrWhiteSpace(TableCss))
                    strTable.Append(" class='" + TableCss + "' ");

                strTable.Append(" > ");

                //  strTable.Append(" <tr><td> ");

                int icol = 0;
                int rownum = 1;
                foreach (DataRow dr in dtTable.Rows)
                {
                    if (Convert.ToDouble(rownum % 6) == 0)
                    {
                        strTable.Append("<tr>");
                    }
                    strTable.Append("<td>");
                    double num = Convert.ToDouble(rownum % 5);
                    strTable.Append("<table id='" + TableID + "_Sub' CELLPADDING='2' CELLSPACING='0'  style='background-color: #C3EBFF; border-radius: 2px; padding: 1px; border: 1px solid #ACC9C9' >");
                    if (Convert.ToDouble(rownum % 6) == 0)
                    {
                        strTable.Append("<tr>");
                    }
                    for (int i = 0; i < DataColumnsName.Length; i++)
                    {
                        //if (dtTable.Columns.Contains("Active_Status"))
                        //{
                        //    if (dr["Active_Status"].ToString() == "0")
                        //    {
                        //        strTable.Append("<td style=color:red;");

                        //    }
                        //    else
                        //    {
                        //        strTable.Append("<td width='auto'");
                        //    }
                        //}
                        //else
                        //{
                        strTable.Append("<td ");
                        //}
                        if (!string.IsNullOrWhiteSpace(RowStyleCss) && (icol % 2) == 0)
                            strTable.Append(" class='" + RowStyleCss + "' ");
                        else if (!string.IsNullOrWhiteSpace(AlternateRowStyleCss))
                            strTable.Append(" class='" + AlternateRowStyleCss + "' ");

                        if (DataColumnsAlignment.Length > i)
                            strTable.Append("  align='" + DataColumnsAlignment[i] + "' ");

                        #region . tooltip and  javascript event

                        UDCToolTip objToolTip;
                        if (DicToolTip.TryGetValue(DataColumnsName[i], out objToolTip))// check for tool tip
                        {
                            if (objToolTip.UseControlToolTip)
                                strTable.Append(" title='" + ReplaceSpecialCharacter(Convert.ToString(dr[objToolTip.ToolTipSourceColumn])) + "'");
                            else
                            {
                                if (Convert.ToString(dr[objToolTip.ToolTipSourceColumn]).Contains("To Attend TAROKO this call Singapore for"))
                                {
                                    string s = "";
                                }
                                strTable.Append(" onmouseover='js_ShowToolTip(&#39;" + ReplaceSpecialCharacter(Convert.ToString(dr[objToolTip.ToolTipSourceColumn])) + "&#39;,event,this)'");
                                strTable.Append(" style='cursor:default' ");
                            }
                        }



                        if (dicJSEvent.Count > 0)
                        {
                            foreach (UDCJSEvent ievt in dicJSEvent)
                            {
                                if (ievt.EventColumnName.ToUpper() == DataColumnsName[i].ToUpper())
                                {
                                    string funparam = "";
                                    foreach (string iprm in ievt.ParamDataColumn)
                                    {
                                        if (funparam.Length > 0)
                                            funparam += ",";

                                        funparam += dr[iprm];
                                    }

                                    funparam += ",event,this";

                                    foreach (string[] fun in ievt.FunctionName)
                                    {
                                        strTable.Append("  " + fun[1] + "='" + fun[0] + "(" + funparam + ")'");
                                    }

                                    strTable.Append(" style='cursor:default' ");
                                }
                            }
                        }

                        #endregion

                        strTable.Append(" > ");

                        #region . column data

                        UDCHyperLink objA;
                        UDCCheckBox objChk;
                        if (DicLink.TryGetValue(DataColumnsName[i], out objA)) //check for hyperlink
                        {


                            if (string.IsNullOrWhiteSpace(objA.HyperLinkColumnName))//link is fixed
                                strTable.Append("<a href='" + objA.NaviagteURL);
                            else
                                strTable.Append("<a  href='../" + objA.NaviagteURL + Convert.ToString(dr[objA.QueryStringDataColumn[0]]));// link can be different for each row

                            for (int iQs = 0; iQs < objA.QueryStringDataColumn.Length; iQs++)// create querystring
                            {
                                if (iQs == 0)
                                    strTable.Append("?");

                                strTable.Append(objA.QueryStringText[iQs] + "=" + dr[objA.QueryStringDataColumn[iQs]].ToString());

                                if (iQs < objA.QueryStringDataColumn.Length - 1)
                                    strTable.Append("&");
                            }
                            strTable.Append("'");

                            if (!string.IsNullOrWhiteSpace(objA.Target))
                                strTable.Append(" target='" + objA.Target + "' ");
                            else
                                strTable.Append(" target='_blank'");


                            strTable.Append(" > ");

                            strTable.Append(dr[DataColumnsName[i]].ToString().Replace("\n", "<br>"));
                            strTable.Append("</a>");

                        }
                        else if (dicCheckBox.TryGetValue(DataColumnsName[i], out objChk)) //check for CheckBox
                        {
                            if (dr[DataColumnsName[i]].ToString() == "1")
                            {
                                strTable.Append("<input type='checkbox' id=" + TableID + "_" + DataColumnsName[i] + rownum + " checked=true disabled=true />");
                            }
                            else
                            {
                                strTable.Append("<input type='checkbox' id=" + TableID + "_" + DataColumnsName[i] + rownum + " />");
                            }
                        }

                        else
                        {
                            strTable.Append(dr[DataColumnsName[i]].ToString().Replace("\n", "<br>"));
                        }

                        #endregion








                        strTable.Append("</td>");

                        // }
                    }




                    //add action columns
                    if (dicAction.Count > 0)
                    {
                        strTable.Append("<td width='auto'");

                        if (!string.IsNullOrWhiteSpace(RowStyleCss) && (icol % 2) == 0)
                            strTable.Append(" class='" + RowStyleCss + "' ");
                        else if (!string.IsNullOrWhiteSpace(AlternateRowStyleCss))
                            strTable.Append(" class='" + AlternateRowStyleCss + "' ");
                        strTable.Append(">");

                        strTable.Append("<table>");
                        strTable.Append("<tr>");
                        foreach (UDCActionNew iAction in dicAction)
                        {
                            if (dr["Active_Status"].ToString() == "0")
                            {
                                if (iAction.ActionName == "Restore")
                                {
                                    strTable.Append("<td style='cursor:pointer;' onmouseover='js_ShowToolTip(&#39;" + iAction.ActionName + "&#39;,event,this)'> <img src='" + iAction.ImageURL + "' width='18' height='18' ");
                                    string funparam = "";
                                    foreach (string iprm in iAction.ParamDataColumn)
                                    {
                                        if (funparam.Length > 0)
                                            funparam += ",";

                                        funparam += "'" + dr[iprm] + "'";
                                    }

                                    funparam += ",event,this";

                                    foreach (string[] fun in iAction.FunctionName)
                                    {
                                        strTable.Append(fun[1] + "=\"" + fun[0] + "(" + funparam + ")\"");
                                    }

                                    strTable.Append("/>");
                                    strTable.Append("</td>");
                                }
                            }
                            else
                            {
                                if (iAction.ActionName != "Restore")
                                {
                                    strTable.Append("<td style='cursor:pointer;' onmouseover='js_ShowToolTip(&#39;" + iAction.ActionName + "&#39;,event,this)'> <img src='" + iAction.ImageURL + "' width='18' height='18' ");
                                    string funparam = "";
                                    foreach (string iprm in iAction.ParamDataColumn)
                                    {
                                        if (funparam.Length > 0)
                                            funparam += ",";

                                        funparam += "'" + dr[iprm] + "'";
                                    }

                                    funparam += ",event,this";

                                    foreach (string[] fun in iAction.FunctionName)
                                    {
                                        strTable.Append(fun[1] + "=\"" + fun[0] + "(" + funparam + ")\"");
                                    }

                                    strTable.Append("/>");
                                    strTable.Append("</td>");
                                }
                            }


                        }
                        strTable.Append("</tr>");
                        strTable.Append("</table>");
                        strTable.Append("</td>");
                    }
                    if (Convert.ToDouble(rownum % 5) == 0)
                    {
                        strTable.Append("</tr>");
                    }
                    icol++;
                    rownum += 1;
                    strTable.Append("</table>");
                    strTable.Append("</td>");
                    if (Convert.ToDouble(rownum % 6) == 0)
                    {
                        strTable.Append("</tr>");
                    }
                }
                // strTable.Append(" </td></tr> ");
                strTable.Append("</table>");
            }
            else
                strTable.Append("<span style='color:maroon;padding:2px'> </span>");

            return strTable.ToString();
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {

        }
    }

    public static DataTable PivotTable(string PivotColumnName, string PivotValueColumnName, string PivotColumnOrder, string[] PrimaryKeyColumns, string[] HideColumns, DataTable dtTableToPivot)
    {
        StringBuilder sbPKs = new StringBuilder();

        DataTable dtFinalResult = new DataTable();
        DataView dvPivotColumnNames = dtTableToPivot.DefaultView.ToTable(true, new string[] { PivotColumnName, PivotColumnOrder }).DefaultView;
        dvPivotColumnNames.Sort = PivotColumnOrder;

        DataTable dtPivotPrimaryKeys = dtTableToPivot.DefaultView.ToTable(true, PrimaryKeyColumns);


        foreach (DataColumn dcol in dtTableToPivot.Columns)
        {
            if (dcol.ColumnName != PivotColumnName && dcol.ColumnName != PivotValueColumnName && dcol.ColumnName != PivotColumnOrder)
                dtFinalResult.Columns.Add(dcol.ColumnName);
        }

        foreach (DataRow drCol in dvPivotColumnNames.ToTable().Rows)
        {
            dtFinalResult.Columns.Add(drCol[0].ToString());
        }


        foreach (DataRow drPK in dtPivotPrimaryKeys.Rows)
        {
            DataRow drNew = dtFinalResult.NewRow();

            foreach (DataColumn dcol in dtTableToPivot.Columns)
            {
                if (dcol.ColumnName != PivotColumnName && dcol.ColumnName != PivotValueColumnName && dcol.ColumnName != PivotColumnOrder)
                {
                    sbPKs.Clear();
                    foreach (string pk in PrimaryKeyColumns)
                    {
                        sbPKs.Append(pk + " = " + drPK[pk].ToString() + " and ");

                    }
                    sbPKs.Append(" 1=1  ");

                    DataRow[] drcoll = dtTableToPivot.Select(sbPKs.ToString());//[0][dcol.ColumnName];
                    drNew[dcol.ColumnName] = drcoll[0][dcol.ColumnName];
                }
            }

            foreach (DataRow drCol in dvPivotColumnNames.ToTable().Rows)
            {
                sbPKs.Clear();
                foreach (string pk in PrimaryKeyColumns)
                {
                    sbPKs.Append(pk + " = " + drPK[pk].ToString() + " and ");
                }


                DataRow[] drValue = dtTableToPivot.Select(sbPKs.ToString() + PivotColumnName + " = '" + drCol[0].ToString() + "' ");
                if (drValue.Length > 0)
                    drNew[drCol[0].ToString()] = drValue[0][PivotValueColumnName];
                else
                    drNew[drCol[0].ToString()] = null;
            }

            dtFinalResult.Rows.Add(drNew);
        }


        foreach (string strColToremove in HideColumns)
        {
            if (dtFinalResult.Columns.IndexOf(strColToremove) > -1)
                dtFinalResult.Columns.Remove(strColToremove);
        }

        return dtFinalResult;

    }

    public static DataTable PivotTable(string PivotColumnName, string PivotValueColumnName, string PivotColumnOrder, string[] PrimaryKeyColumns, string[] HideColumns, string SortOrderColumns, Dictionary<string, UDCHyperLink> DicLink, DataTable dtTableToPivot)
    {
        StringBuilder sbPKs = new StringBuilder();

        DataTable dtFinalResult = new DataTable();
        DataView dvPivotColumnNames = dtTableToPivot.DefaultView.ToTable(true, new string[] { PivotColumnName, PivotColumnOrder }).DefaultView;
        dvPivotColumnNames.Sort = PivotColumnOrder;

        DataTable dtPivotPrimaryKeys = dtTableToPivot.DefaultView.ToTable(true, PrimaryKeyColumns);


        foreach (DataColumn dcol in dtTableToPivot.Columns)
        {
            if (dcol.ColumnName != PivotColumnName && dcol.ColumnName != PivotValueColumnName && dcol.ColumnName != PivotColumnOrder)
            {
                if (!string.IsNullOrWhiteSpace(SortOrderColumns) && SortOrderColumns.Contains(dcol.ColumnName))
                    dtFinalResult.Columns.Add(dcol.ColumnName, dcol.DataType);
                else
                    dtFinalResult.Columns.Add(dcol.ColumnName);

            }
        }

        foreach (DataRow drCol in dvPivotColumnNames.ToTable().Rows)
        {
            dtFinalResult.Columns.Add(drCol[0].ToString());
        }


        foreach (DataRow drPK in dtPivotPrimaryKeys.Rows)
        {
            DataRow drNew = dtFinalResult.NewRow();

            foreach (DataColumn dcol in dtTableToPivot.Columns)
            {
                if (dcol.ColumnName != PivotColumnName && dcol.ColumnName != PivotValueColumnName && dcol.ColumnName != PivotColumnOrder)
                {
                    sbPKs.Clear();
                    foreach (string pk in PrimaryKeyColumns)
                    {
                        sbPKs.Append(pk + " = " + drPK[pk].ToString() + " and ");

                    }
                    sbPKs.Append(" 1=1  ");

                    DataRow[] drcoll = dtTableToPivot.Select(sbPKs.ToString());//[0][dcol.ColumnName];
                    drNew[dcol.ColumnName] = drcoll[0][dcol.ColumnName];
                }
            }

            foreach (DataRow drCol in dvPivotColumnNames.ToTable().Rows)
            {
                sbPKs.Clear();
                foreach (string pk in PrimaryKeyColumns)
                {
                    sbPKs.Append(pk + " = " + drPK[pk].ToString() + " and ");

                }




                DataRow[] drValue = dtTableToPivot.Select(sbPKs.ToString() + PivotColumnName + " = '" + drCol[0].ToString() + "' ");
                if (drValue.Length > 0)
                    drNew[drCol[0].ToString()] = drValue[0][PivotValueColumnName];
                else
                    drNew[drCol[0].ToString()] = null;
            }

            dtFinalResult.Rows.Add(drNew);
        }


        if (!string.IsNullOrWhiteSpace(SortOrderColumns))
        {
            try
            {
                dtFinalResult.DefaultView.Sort = SortOrderColumns;
                dtFinalResult = dtFinalResult.DefaultView.ToTable();
            }
            catch { }
        }

        #region . hyperlink

        foreach (DataRow dr in dtFinalResult.Rows)
        {
            if (DicLink != null)
            {
                foreach (string HyperLinkColumn in DicLink.Keys)
                {

                    UDCHyperLink objA;
                    StringBuilder StrLink = new StringBuilder();

                    if (DicLink.TryGetValue(HyperLinkColumn, out objA)) //check for hyperlink
                    {
                        if (string.IsNullOrWhiteSpace(objA.HyperLinkColumnName))//link is fixed
                            StrLink.Append("<a href='" + objA.NaviagteURL);
                        else
                            StrLink.Append("<a  href='../" + Convert.ToString(dr[objA.HyperLinkColumnName]));// link can be different for each row

                        for (int iQs = 0; iQs < objA.QueryStringDataColumn.Length; iQs++)// create querystring
                        {
                            if (iQs == 0)
                                StrLink.Append("?");

                            StrLink.Append(objA.QueryStringText[iQs] + "=" + dr[objA.QueryStringDataColumn[iQs]].ToString());

                            if (iQs < objA.QueryStringDataColumn.Length - 1)
                                StrLink.Append("&");
                        }
                        StrLink.Append("'");

                        if (!string.IsNullOrWhiteSpace(objA.Target))
                            StrLink.Append(" target='" + objA.Target + "' ");
                        else
                            StrLink.Append(" target='_blank'");


                        StrLink.Append(" > ");

                        StrLink.Append(dr[HyperLinkColumn].ToString().Replace("\n", "<br>"));
                        StrLink.Append("</a>");


                        dr[HyperLinkColumn] = StrLink.ToString();
                    }
                }
            }

        }
        #endregion




        if (HideColumns != null)
        {
            foreach (string strColToremove in HideColumns)
            {
                if (dtFinalResult.Columns.IndexOf(strColToremove) > -1)
                    dtFinalResult.Columns.Remove(strColToremove);
            }
        }
        return dtFinalResult;

    }


    public static int FindGridColumnIndex(DataControlFieldCollection Columns, string AccessibleHeaderText)
    {
        int index = -1;
        foreach (DataControlField field in Columns)
        {
            index++;

            if (field.AccessibleHeaderText.Trim() == AccessibleHeaderText.Trim())
                break;

        }
        return index;
    }

    // Inspection check list

    /// <summary>
    /// Creates a parent DataTable within the DataSet using distinct rows from
    /// an existing "source" DataTable, based on the column(s) specified.
    /// The source table then becomes the "child" table of the newly created 
    /// parent. A DataRelation is also created between the parent table and the 
    /// child table, using the column(s) specified.
    /// </summary>
    /// <param name="sourceTable">
    /// The source DataTable, which must be within a DataSet. 
    /// This source table will become the "child" table.
    /// </param>
    /// <param name="parentTableName">
    /// This name will be assigned to the parent table once it is created.
    /// </param>
    /// <param name="relationColumns">
    /// Specify the columns used to relate the parent table to the child table.
    /// </param>
    /// <param name="additionalColumns">
    /// Any additional column(s) in the source table that will be extracted to the 
    /// parent table. These columns will be removed from the source table.
    /// </param>
    /// <param name="relationName">
    /// The name of the relation that will be created between the parent and 
    /// the child table.
    /// </param>
    /// 



    public static DataTable MonthDays()
    {
        DataTable dtDays = new DataTable();
        dtDays.Columns.Add("DaysText");
        dtDays.Columns.Add("DaysValue");
        DataRow dr = null;
        for (int mins = 1; mins <= 30; mins++)
        {
            dr = dtDays.NewRow();
            dr["DaysText"] = mins.ToString();
            dr["DaysValue"] = mins.ToString();
            dtDays.Rows.Add(dr);
        }

        return dtDays;
    }


    public static void AddParentTable(DataTable sourceTable, string parentTableName,
    string[] relationColumns, string[] additionalColumns, string relationName)
    {
        DataSet dataSet = sourceTable.DataSet;

        if (dataSet == null)
        {
            throw new Exception("The source DataTable must be contained in a DataSet");
        }

        // generate the set of columns to use to create the Parent table:
        string[] cols = new string[relationColumns.Length + additionalColumns.Length];
        relationColumns.CopyTo(cols, 0);
        additionalColumns.CopyTo(cols, relationColumns.Length);

        // create the parent table, copying unique rows from the Child table:
        DataTable parent = sourceTable.DefaultView.ToTable(parentTableName, true, cols);

        // add the parent table to the DataSet:
        dataSet.Tables.Add(parent);

        // remove the additional columns from the child table that were 
        // copied to the parent table:
        foreach (string s in additionalColumns)
            sourceTable.Columns.Remove(s);

        // create the relation between the new parent table and the child table:
        DataColumn[] parentColumns = new DataColumn[relationColumns.Length];
        DataColumn[] childColumns = new DataColumn[relationColumns.Length];

        for (int i = 0; i < relationColumns.Length; i++)
        {
            parentColumns[i] = parent.Columns[relationColumns[i]];
            childColumns[i] = sourceTable.Columns[relationColumns[i]];
        }

        // And, finally, add the relation to the parent table:
        parent.ChildRelations.Add(relationName, parentColumns, childColumns);
    }


    // this method not in use.

    public static void SendMail(string MailTo, string Subject, string Body)
    {
        try
        {
            if (MailTo != "")
            {
                System.Net.Mail.MailMessage Message = new System.Net.Mail.MailMessage();

                Message.To.Add(MailTo);
                Message.From = new System.Net.Mail.MailAddress("");
                Message.Subject = Subject;

                Message.Body = Body;
                System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient();
                //smtp.Port = 587;
                //smtp.Host = "localhost";
                smtp.EnableSsl = true;
                //ServicePointManager.ServerCertificateValidationCallback = TrustAllCertificatesCallback;
                //smtp.Credentials = new System.Net.NetworkCredential("sep", "Cricko7");
                smtp.Send(Message);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static string GetPageURL(string URL)
    {
        try
        {
            string APP_NAME = ConfigurationManager.AppSettings["APP_NAME"].ToUpper();
            return URL.ToUpper().Replace("/" + APP_NAME + "/", "");
        }
        catch
        {
            return URL;
        }
    }

    // encrypt the query string 
    public static string Encrypt(string inputText)
    {
        RijndaelManaged rijndaelCipher = new RijndaelManaged();
        byte[] plainText = Encoding.Unicode.GetBytes(inputText);
        PasswordDeriveBytes SecretKey = new PasswordDeriveBytes(ENCRYPTION_KEY, SALT);

        using (ICryptoTransform encryptor = rijndaelCipher.CreateEncryptor(SecretKey.GetBytes(32), SecretKey.GetBytes(16)))
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                {
                    cryptoStream.Write(plainText, 0, plainText.Length);
                    cryptoStream.FlushFinalBlock();
                    return "?" + PARAMETER_NAME + Convert.ToBase64String(memoryStream.ToArray());
                }
            }
        }
    }

    public static string Remove_Special_Characters(string Input_)
    {
        return Regex.Replace(Input_, @"[^A-Za-z0-9-_.$ ]", "");
    }


    /// <summary>
    /// Method to write application wide exception log. 
    /// </summary>
    /// <param name="ex">Object of Class Exception</param>
    public static void WriteExceptionLog(Exception ex)
    {
        System.Threading.ThreadAbortException exception = ex as System.Threading.ThreadAbortException;
        if (exception == null)
        {
            string ExceptionLogFolderPath = System.Web.HttpContext.Current.Request.PhysicalApplicationPath + "\\ExceptionLogFolder";
            try
            {
                if (!Directory.Exists(ExceptionLogFolderPath))
                    Directory.CreateDirectory(ExceptionLogFolderPath);

                if (Directory.Exists(ExceptionLogFolderPath))
                {
                    //Create month wise exception log file.
                    string date = string.Format("{0:dd}", DateTime.Now);
                    string month = string.Format("{0:MMM}", DateTime.Now);
                    string year = string.Format("{0:yyyy}", DateTime.Now);

                    string folderName = month + year; //Feb2013
                    string monthFolder = ExceptionLogFolderPath + "\\" + folderName;
                    if (!Directory.Exists(monthFolder))
                        Directory.CreateDirectory(monthFolder);

                    string ExceptionLogFileName = monthFolder +
                        "\\ExceptionLog_" + date + month + ".txt"; //ExceptionLog_04Feb.txt

                    using (System.IO.StreamWriter strmWriter = new System.IO.StreamWriter(ExceptionLogFileName, true))
                    {
                        strmWriter.WriteLine("On " + DateTime.Now.ToString() +
                            ", following error occured in the application:");
                        strmWriter.WriteLine("Message: " + ex.Message);

                        strmWriter.WriteLine("Source: " + ex.Source);
                        strmWriter.WriteLine("Stack Trace: " + ex.StackTrace);
                        strmWriter.WriteLine("HelpLink: " + ex.HelpLink);
                        strmWriter.WriteLine("-------------------------------------------------------------------------------");
                    }
                }
                else
                    throw new DirectoryNotFoundException("Exception log folder not found in the specified path");
            }
            catch
            {
                throw;
            }
        }
    }


    /// <summary>
    /// Method to get messages from exceptionmessages.xml. 
    /// </summary>
    /// <param name="Key">Key</param>
    public static string GetException(string Key)
    {
        string message = "";
        try
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(System.Web.HttpContext.Current.Request.PhysicalApplicationPath + "ExceptionMessages.xml");
            XmlNodeList docNodeList = doc.SelectNodes("/Exception/" + Key);
            message = docNodeList.Item(0).InnerText;
            return message;
        }
        catch (Exception ex)
        {
            WriteExceptionLog(ex);
            return message;
        }
    }

    public static string ConvertUserDateFormat(string Date)
    {
        if (Date != "")
        {
            try
            {
                DateTime.Parse(Date);
                DateTime dt = Convert.ToDateTime(Date);
                return String.Format("{0:" + GetDateFormat() + "}", dt);
            }
            catch (Exception)
            {
                return "";
            }
        }
        else
            return "";
    }

    public static string ConvertUserDateFormat(string Date, string DateFormat)
    {
        if (Date != "")
        {
            try
            {
                DateTime.Parse(Date);

                DateTime dt = Convert.ToDateTime(Date);
                return String.Format("{0:" + DateFormat + "}", dt);
            }
            catch (Exception)
            {
                return "";
            }
        }
        else
            return "";
    }
    public static string ConvertUserDateFormatTime(string Date, string DateFormat)
    {
        if (Date != "")
        {
            try
            {
                DateTime.Parse(Date);
                DateTime dt = Convert.ToDateTime(Date);
                return String.Format("{0:" + DateFormat + " HH:mm}", dt);
            }
            catch (Exception)
            {
                return "";
            }
        }
        else
            return "";
    }
    public static string ConvertUserDateFormatTime(string Date)
    {
        if (Date != "")
        {
            try
            {
                DateTime.Parse(Date);

                DateTime dt = Convert.ToDateTime(Date);

                if (Convert.ToString(HttpContext.Current.Session["User_DateFormat"]) != "")
                {
                    return String.Format("{0:" + Convert.ToString(HttpContext.Current.Session["User_DateFormat"]) + " HH:mm}", dt);
                }
                else
                    return String.Format("{0:dd-MM-yyyy HH:mm}", dt);
            }
            catch (Exception)
            {
                return "";
            }
        }
        else
            return "";
    }


    public static string GetDateFormat()
    {
        if (HttpContext.Current.Session["User_DateFormat"] != null)
            return Convert.ToString(HttpContext.Current.Session["User_DateFormat"]);
        else
        {
            HttpContext.Current.Session["User_DateFormat"] = "dd-MM-yyyy";
            return Convert.ToString(HttpContext.Current.Session["User_DateFormat"]);
        }
    }

    public static Boolean DateCheck(string Date)
    {
        try
        {
            string[] formats = { "DD-MM-YYYY", "MM-DD-YYYY", "DD-MMM-YYYY", "YYYY-MM-DD" };

            if (HttpContext.Current.Session["User_DateFormat"] != null)
            {
                formats[0] = Convert.ToString(HttpContext.Current.Session["User_DateFormat"]);
            }

            DateTime expectedDate;
            if (!DateTime.TryParseExact(Date, formats, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out expectedDate))
            {
                //   MessageBox.MessageBox.Show("Invalid Date Entry");
                return false;
            }
            return true;
        }
        catch (Exception)
        {
            // MessageBox.MessageBox.Show("Exception Invalid Date Entry");
            return false;
        }

    }

    public static string ConvertToDefaultDt(string strDate)
    {
        if (strDate != "")
        {
            try
            {
                DateTime dt;
                if (GetDateFormat().ToLower() == "mm-dd-yyyy")
                {
                    dt = DateTime.Parse(strDate.Split('-')[2] + "-" + strDate.Split('-')[0] + "-" + strDate.Split('-')[1]);
                }
                else
                {
                    //DateTime.Parse(Date);
                    dt = Convert.ToDateTime(strDate);
                }

                return String.Format("{0:dd-MM-yyyy}", dt);


            }
            catch (Exception)
            {
                return "";
            }
        }
        else
            return "";
    }

    public static int ConvertMonthToDigit(string month)
    {
        int retVal = 0;
        switch (month.ToLower())
        {
            case "jan":
                retVal = 01;
                break;
            case "feb":
                retVal = 02;
                break;
            case "mar":
                retVal = 03;
                break;
            case "apr":
                retVal = 04;
                break;
            case "may":
                retVal = 05;
                break;
            case "jun":
                retVal = 06;
                break;
            case "jul":
                retVal = 07;
                break;
            case "aug":
                retVal = 08;
                break;
            case "sep":
                retVal = 09;
                break;
            case "oct":
                retVal = 10;
                break;
            case "nov":
                retVal = 11;
                break;
            case "dec":
                retVal = 12;
                break;
        }
        return retVal;
    }


    public static DateTime ConvertToDate(string Date)
    {
        return ConvertToDate(Date, GetDateFormat());
    }

    public static string DateFormatMessage()
    {
        return " in " + GetDateFormat().ToUpper() + " format. (e.g "+ ConvertUserDateFormat(DateTime.Today.ToShortDateString())+")";
    }

    public static string DateFormatTimeMessage()
    {
        return " in " + GetDateFormat().ToUpper() + " format. (e.g " + ConvertUserDateFormatTime(DateTime.Today.ToString()) + ")";
    }

    public static DateTime ConvertToDate(string Date, string DFormat)
    {
        DateTime date = new DateTime();
        if (Date != "")
        {
            if (DFormat.ToLower() == "dd-mm-yyyy")
                date = new DateTime(Convert.ToInt32(Date.Split('-')[2]), Convert.ToInt32(Date.Split('-')[1]), Convert.ToInt32(Date.Split('-')[0]));
            if (DFormat.ToLower() == "mm-dd-yyyy")
                date = new DateTime(Convert.ToInt32(Date.Split('-')[2]), Convert.ToInt32(Date.Split('-')[0]), Convert.ToInt32(Date.Split('-')[1]));
            else if (DFormat.ToLower() == "dd-mmm-yyyy")
                date = new DateTime(Convert.ToInt32(Date.Split('-')[2]), ConvertMonthToDigit(Date.Split('-')[1].ToString()), Convert.ToInt32(Date.Split('-')[0]));
            else if (DFormat.ToLower() == "yyyy-mm-dd")
                date = new DateTime(Convert.ToInt32(Date.Split('-')[0]), Convert.ToInt32(Date.Split('-')[1]), Convert.ToInt32(Date.Split('-')[2]));
            else if (DFormat.ToLower() == "mm/dd/yyyy")
                date = new DateTime(Convert.ToInt32(Date.Split('/')[2]), Convert.ToInt32(Date.Split('/')[0]), Convert.ToInt32(Date.Split('/')[1]));
            else if (DFormat.ToLower() == "dd/mm/yyyy")
                date = new DateTime(Convert.ToInt32(Date.Split('/')[2]), Convert.ToInt32(Date.Split('/')[1]), Convert.ToInt32(Date.Split('/')[0]));
        }
        else
        {
            date = DateTime.Parse("1900/01/01");
        }
        return date;
    }

    public DateTime? ConvertDate(string date)
    {
        if (date == "" || date == null)
        {
            return null;
        }
        else
        {
            return UDFLib.ConvertToDate(date);
        }
    }

    // Method to change datatype of a column in datatable
    public static bool ChangeColumnDataType(DataTable table, string columnname, Type newtype)
    {
        if (table.Columns.Contains(columnname) == false)
            return false;

        DataColumn column = table.Columns[columnname];
        if (column.DataType == newtype)
            return true;

        try
        {
            DataColumn newcolumn = new DataColumn("temporary", newtype);
            table.Columns.Add(newcolumn);
            foreach (DataRow row in table.Rows)
            {
                try
                {
                    row["temporary"] = Convert.ChangeType(row[columnname], newtype);
                }
                catch
                {
                }
            }
            table.Columns.Remove(columnname);
            newcolumn.ColumnName = columnname;
        }
        catch (Exception)
        {
            return false;
        }

        return true;
    }

    /// <summary>
    /// Get JSON from datatable
    /// </summary>
    /// <param name="dt"></param>
    /// <returns></returns>
    public static string DataTableToJsonObj(DataTable dt)
    {
        DataSet ds = new DataSet();
        ds.Merge(dt);
        StringBuilder JsonString = new StringBuilder();
        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {
            JsonString.Append("[");
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                JsonString.Append("{");
                for (int j = 0; j < ds.Tables[0].Columns.Count; j++)
                {
                    if (j < ds.Tables[0].Columns.Count - 1)
                    {
                        JsonString.Append("\"" + ds.Tables[0].Columns[j].ColumnName.ToString() + "\":" + "\"" + ds.Tables[0].Rows[i][j].ToString() + "\",");
                    }
                    else if (j == ds.Tables[0].Columns.Count - 1)
                    {
                        JsonString.Append("\"" + ds.Tables[0].Columns[j].ColumnName.ToString() + "\":" + "\"" + ds.Tables[0].Rows[i][j].ToString() + "\"");
                    }
                }
                if (i == ds.Tables[0].Rows.Count - 1)
                {
                    JsonString.Append("}");
                }
                else
                {
                    JsonString.Append("},");
                }
            }
            JsonString.Append("]");
            return JsonString.ToString();
        }
        else
        {
            return null;
        }
    }

    /// <summary>
    /// Get logged in UserID
    /// </summary>
    /// <returns></returns>
    public static int GetSessionUserID()
    {
        if (HttpContext.Current.Session["USERID"] != null)
            return int.Parse(HttpContext.Current.Session["USERID"].ToString());
        else
            return 0;
    }
}


#region -------User defined DataType---------

public enum UDTRepeatDirection { Horizontal = 1, Vertical };

#endregion

#region--------User Defined Class-----

public class UDCHyperLink
{
    public string HyperLinkColumnName;
    public string NaviagteURL;
    public string[] QueryStringText;
    public string[] QueryStringDataColumn;
    public string Target;


    public UDCHyperLink()
    {
    }
    public UDCHyperLink(string HyperLinkColumnName_, string NaviagteURL_, string[] QueryStringText_, string[] QueryStringDataColumn_, string Target_)
    {
        HyperLinkColumnName = HyperLinkColumnName_;
        NaviagteURL = NaviagteURL_;
        QueryStringDataColumn = QueryStringDataColumn_;
        QueryStringText = QueryStringText_;
        Target = Target_;
    }

}

public class UDCHyperLinkImage
{
    public string HyperLinkColumnName;
    public string NaviagteURL;
    public string[] QueryStringText;
    public string[] QueryStringDataColumn;
    public string Target;
    public string ImageURL;

    public UDCHyperLinkImage()
    {
    }
    public UDCHyperLinkImage(string HyperLinkColumnName_, string NaviagteURL_, string[] QueryStringText_, string[] QueryStringDataColumn_, string Target_, string ImageURL_)
    {
        HyperLinkColumnName = HyperLinkColumnName_;
        NaviagteURL = NaviagteURL_;
        QueryStringDataColumn = QueryStringDataColumn_;
        QueryStringText = QueryStringText_;
        Target = Target_;
        ImageURL = ImageURL_;
    }

}
public class UDCCheckBox
{
    public string CheckBoxColumnName;
    public string CheckBoxColumnValue;
    public string[][] FunctionName;
    public string[] ParamDataColumn;

    public UDCCheckBox()
    {
    }
    public UDCCheckBox(string _CheckBoxColumnName, string _CheckBoxColumnValue)
    {
        CheckBoxColumnName = _CheckBoxColumnName;
        CheckBoxColumnValue = _CheckBoxColumnValue;

    }
    public UDCCheckBox(string _CheckBoxColumnName, string _CheckBoxColumnValue, string[][] _FunctionName)
    {
        CheckBoxColumnName = _CheckBoxColumnName;
        CheckBoxColumnValue = _CheckBoxColumnValue;
        FunctionName = _FunctionName;

    }
    public UDCCheckBox(string _CheckBoxColumnName, string _CheckBoxColumnValue, string[][] _FunctionName, string[] _ParamDataColumn)
    {
        CheckBoxColumnName = _CheckBoxColumnName;
        CheckBoxColumnValue = _CheckBoxColumnValue;
        FunctionName = _FunctionName;
        ParamDataColumn = _ParamDataColumn;
    }
}

public class UDCToolTip
{
    public string ToolTipTargetColumn;
    public string ToolTipSourceColumn;
    public bool UseControlToolTip = false;

    public UDCToolTip()
    {
    }
    public UDCToolTip(string ToolTipTargetColumn_, string ToolTipSourceColumn_, bool UseControlToolTip_)
    {
        ToolTipTargetColumn = ToolTipTargetColumn_;
        ToolTipSourceColumn = ToolTipSourceColumn_;
        UseControlToolTip = UseControlToolTip_;
    }

}
public class UDCAction
{
    public string[][] FunctionName;
    public string ImageURL;
    public string[] ParamDataColumn;


    public UDCAction()
    {
    }
    public UDCAction(string[][] _FunctionName, string _ImageURL, string[] _ParamDataColumn)
    {
        FunctionName = _FunctionName;
        ImageURL = _ImageURL;
        ParamDataColumn = _ParamDataColumn;


    }

}
public class UDCActionNew
{
    public string[][] FunctionName;
    public string ImageURL;
    public string[] ParamDataColumn;
    public string ActionName;

    public UDCActionNew()
    {
    }
    public UDCActionNew(string[][] _FunctionName, string _ImageURL, string[] _ParamDataColumn, string _ActionName)
    {
        FunctionName = _FunctionName;
        ImageURL = _ImageURL;
        ParamDataColumn = _ParamDataColumn;
        ActionName = _ActionName;

    }

}

public class UDCJSEvent
{
    public string[][] FunctionName;

    public string[] ParamDataColumn;

    public string EventColumnName;


    public UDCJSEvent()
    {
    }
    public UDCJSEvent(string[][] _FunctionName, string[] _ParamDataColumn, string _EventColumnName)
    {
        FunctionName = _FunctionName;
        EventColumnName = _EventColumnName;
        ParamDataColumn = _ParamDataColumn;

    }
}


#endregion 