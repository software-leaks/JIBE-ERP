<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Crew_Performance_SnippetSettings.aspx.cs" Inherits="Infrastructure_Libraries_Crew_Performance_SnippetSettings" MasterPageFile="~/Site.master" Title="Crew Performance Snippet Settings"%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">  
 <center  style="height:850px">
<div style="border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px; width: auto;
            height: 100%;">    
            <div style="margin-left: auto; height: auto; width: auto; margin-right: auto; text-align: center;" >
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <table width="40%" >           
                        <tr>
                            <td  >
                                <fieldset style="text-align: left; margin: 0px; padding: 2px;width:250px">
                                    <legend>Crew Performance Snippet :-</legend>                               
                                    <table  cellpadding="5" cellspacing="1"  width="100%">
                                      <tr>
                                        <td>
                                        <div style="width:100%; height:600px; overflow:auto;" >
                                              <asp:GridView ID="gvRankList" runat="server" AutoGenerateColumns="False" EmptyDataText="No Record Found"
                                            AllowSorting="True" CaptionAlign="Bottom" CellPadding="4"
                                            Font-Size="14px" GridLines="None" Width="100%" CssClass="gridmain-css" Height="600px" >
                                          <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                          <RowStyle CssClass="RowStyle-css" />
                                          <HeaderStyle CssClass="HeaderStyle-css" Height="35px" />
                                            <Columns>
                                                <asp:TemplateField  HeaderText="Select Rank"  >
                                                      <ItemTemplate>
                                                        <asp:CheckBox  ID="chkSelected" Checked='<%# Eval("Status").ToString()=="1"?true:false%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>                                             
                                                <asp:TemplateField  HeaderText="Rank" >
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRankId" runat="server" Text='<%# Eval("Id")%>'  Visible="false"></asp:Label>
                                                        <asp:Label ID="lblRank" runat="server" Text='<%# Eval("Rank_Short_Name")%>' ></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>                                                
                                            </Columns>
                                            <AlternatingRowStyle CssClass="crew-interview-grid-alternaterow" />
                                        </asp:GridView>
                                        </div>
                                        </td>
                                     </tr>
                                     <tr>
                                        <td colspan="2" align="center">
                                            <asp:Button ID="btnSaveRank" runat="server" Text=" Save " onclick="btnSaveRank_Click" />
                                        </td>
                                    </tr>
                                </table>
                                 </fieldset>
                            </td>
                             <td valign="top">
                                <table>
                                </table>
                                
                            </td>
                        </tr>
                        </table>
                        </div>
                    </ContentTemplate> 
                 </asp:UpdatePanel> 
             </div>
 </div>     
       </center>
   </asp:Content>