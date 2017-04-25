<%@ Page Title="Executive Summary" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="ExecutiveSummary.aspx.cs" Inherits="Technical_Worklist_ExecutiveSummary" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="../../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/jquery.min1.8.js" type="text/javascript"></script>
    <script src="../../Scripts/Common_Functions.js" type="text/javascript"></script>
    <style type="text/css">
        .page
        {
            width: 1200px;
            background-color: #fff;
            margin: 5px auto 0px auto;
            border: 1px solid #496077;
        }

    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div id="page-content" style="border: 1px solid #cccccc; z-index: -2; white-space: nowrap;">
        <asp:UpdateProgress ID="upUpdateProgress" runat="server">
            <ProgressTemplate>
                <div id="blur-on-updateprogress">
                    &nbsp;</div>
                <div id="divProgress" style="position: absolute; left: 49%; top: 30px; z-index: 201;
                    color: black">
                    <img src="../../Images/loaderbar.gif" alt="Please Wait" />
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" >
            <ContentTemplate>
               
                    <div id="page-header" class="page-title">
                        <b>Executive Summary</b>
                    </div>
                       <div id="dvSubHeader" >
                        <table  width="99%">
                          <tr>
                            <td align="center" style="color: Black; font-weight: bold; font-size: 14px; font:Tahoma ">
                               Vessel Name: <asp:Label ID="lblVesselName" runat="server" Text="Vessel  Name" ></asp:Label>
                        </tr>
                        <tr>
                            <td align="center" style="color: Black; font-weight: bold; font-size: 14px;font:Tahoma ">
                             <asp:Label ID="lblFromToDate" runat="server" Text="Date Range"></asp:Label>
                        </tr>
                        </table>
                    </div>
                <%--<div>
                    <table>
                        <tr>
                            <td>
                                <asp:Label ID="lblReportNo" runat="server" Text="Report No" Font-Bold="True"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtReportNo" runat="server" Width="200px" Font-Size="12px"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </div>--%>
                     <div id="dvExeSummary" style=" font-family:Tahoma; font-size: 12px; color:Black;">
                    <table width="99%">
                      
                       
                        <tr>
                            <td colspan="2">
                                <asp:DataList ID="dlSummary" runat="server" Width="100%">
                               
                              
                                    <ItemTemplate>
                                        <br />
                                        <br />
                                        <div style="width:100%; background-color:#666666; color:White;  ">
                                       
                                        &nbsp;<asp:Label ID="lblTopicName" runat="server"  Text='<%# Bind("TopicName") %>'  Font-Size="12px"  Font-Names="Tahoma"></asp:Label>
                                             </div>

                                      <div style="width:100%; white-space:normal; color:Black; ">
                                        <asp:HiddenField ID="hdnTopicKey" runat="server" Value='<%# Bind("TopicKey") %>' />
                                        <asp:TextBox ID="txtTopicDetails" runat="server"  Width="99.5%" 
                                           Height="100px" Text='<%# Bind("TopicDetail") %>' Font-Names="Tahoma" Font-Size="12px" ForeColor="Black" CssClass="rptSummary" TextMode="MultiLine"></asp:TextBox>
                                            </div>
                                    </ItemTemplate>
                                </asp:DataList>
                            </td>
                        </tr>
                     <tr>
                     <td>
                       <br />
                                        <br />
                     </td>
                     </tr>
                        <tr>
                        <td style="border:1px solid gray" width="100%">
                         
                        <div style="width:100%; background-color:#666666; color:Black; word-break:;">
                                       
                                       
                                             </div>
                                <table >
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblFoLog" runat="server" Text="Total FO As Per Log:" ForeColor="Black" Font-Names="Tahoma" Font-Size="12px"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtFOLog" runat="server" Font-Names="Tahoma" ForeColor="Black"  Font-Size="12px" CssClass="rptSummary" Width="80px"></asp:TextBox>
                                        </td>
                                        <td>
                                             <asp:Label ID="Label1" runat="server" Text="mt" ForeColor="Black" Font-Names="Tahoma" Font-Size="12px"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblFOMeasured" runat="server" Text="Total FO Measured:" ForeColor="Black" Font-Names="Tahoma" Font-Size="12px"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtFOMeasured" runat="server" Font-Names="Tahoma"  ForeColor="Black" Font-Size="12px" CssClass="rptSummary" Width="80px"></asp:TextBox>
                                        </td>
                                         <td>
                                             <asp:Label ID="Label2" runat="server" Text="mt" ForeColor="Black" Font-Names="Tahoma" Font-Size="12px"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblMDOLog" runat="server" Text="Total MDO As Per Log:" ForeColor="Black" Font-Names="Tahoma" Font-Size="12px"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtMDOLog" runat="server" Font-Names="Tahoma"  ForeColor="Black" Font-Size="12px" CssClass="rptSummary" Width="80px"></asp:TextBox>
                                        </td>
                                        <td>
                                             <asp:Label ID="Label3" runat="server" Text="mt" ForeColor="Black" Font-Names="Tahoma" Font-Size="12px"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblMDOMeasured" runat="server" Text="Total MDO Measured:" ForeColor="Black" Font-Names="Tahoma" Font-Size="12px"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtMDOMeasured" runat="server" Font-Names="Tahoma"  ForeColor="Black" Font-Size="12px" CssClass="rptSummary" Width="80px"></asp:TextBox>
                                        </td>
                                        <td>
                                             <asp:Label ID="Label4" runat="server" Text="mt" ForeColor="Black" Font-Names="Tahoma" Font-Size="12px"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblMGOLog" runat="server" Text="Total MGO As Per Log:" ForeColor="Black" Font-Names="Tahoma" Font-Size="12px"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtMGOLog" runat="server" Font-Names="Tahoma"  ForeColor="Black" Font-Size="12px" CssClass="rptSummary" Width="80px"></asp:TextBox>
                                        </td>
                                        <td>
                                             <asp:Label ID="Label5" runat="server" Text="mt" ForeColor="Black" Font-Names="Tahoma" Font-Size="12px"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblMGOMeasured" runat="server" Text="Total MGO Measured:" ForeColor="Black" Font-Names="Tahoma" Font-Size="12px"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtMGOMeasured" runat="server" Font-Names="Tahoma"  ForeColor="Black" Font-Size="12px" CssClass="rptSummary" Width="80px"></asp:TextBox>
                                        </td>
                                        <td>
                                             <asp:Label ID="Label6" runat="server" Text="mt" ForeColor="Black" Font-Names="Tahoma" Font-Size="12px"></asp:Label>
                                        </td>
                                    </tr>
                                
                                </table>
                        </td>
                        </tr>
                      <%--  <tr>
                            <td>
                                <asp:Button ID="BtnSaveSummary" runat="server" Text="Save" OnClick="BtnSaveSummary_Click" Width="100px" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                        </tr>--%>
                    </table>
                </div>
                <div style="width:100%; text-align:center;">
                   <asp:Button ID="Button1" runat="server" Text="Save" OnClick="BtnSaveSummary_Click" Width="100px" />
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
