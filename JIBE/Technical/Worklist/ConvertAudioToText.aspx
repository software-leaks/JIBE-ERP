<%@ Page Title="Convert audio to text" Language="C#" MasterPageFile="~/Site.master"
    AutoEventWireup="true" CodeFile="ConvertAudioToText.aspx.cs" Inherits="Technical_Worklist_ConvertAudioToText"
    EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="../../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">

        var _prevControl = null;
        var _PrevImg = null;

        function PlayAudio(actl) {

            var ctlaudio = actl.getElementsByTagName("audio");
            var CurrentctlImg = actl.getElementsByTagName("img")[0];


            currentctlid = ctlaudio[0];

            if (_prevControl != null && _prevControl != currentctlid) {
                _PrevImg.src = "../../images/AquaPlayicon.png";
                CurrentctlImg.src = "../../images/AquaPauseicon.png";

                _prevControl.pause();
                currentctlid.play();
            }
            else if (_prevControl == currentctlid) {

                if (_prevControl.paused == false && _prevControl.ended == false) {
                    _prevControl.pause();
                    _PrevImg.src = "../../images/AquaPlayicon.png";
                }
                else {
                    currentctlid.play();
                    CurrentctlImg.src = "../../images/AquaPauseicon.png";

                }
            }
            else {
                currentctlid.play();
                CurrentctlImg.src = "../../images/AquaPauseicon.png";
            }

            _prevControl = currentctlid;
            _PrevImg = CurrentctlImg;

        }
        function DocOpen(docpath) {
            window.open(docpath);
        }

        function AudioOnEnded(acurrentctl) {

            _PrevImg.src = "../../images/AquaPlayicon.png";
        }


        var lastexect = null;
        function Upd_Convert_Audio_To_Text(Vessel_ID, Worklist_ID, Followup_ID, WL_Office_ID, UserID, AttachPath, Action, objsrc) {

            var txtAudText = "";

            if (Action == "DRAFT")
                txtAudText = objsrc.id.replace("imgSave", "txtAudText");
            else
                txtAudText = objsrc.id.replace("imgConfirm", "txtAudText");


            var Followup = document.getElementById(txtAudText).value;
            if (Followup.trim() == "") {
                alert('Audio text can not be blank.');
                return false;
            }


            if (lastexect != null)
                lastexect.abort();

            var service = Sys.Net.WebServiceProxy.invoke('../../JibeWebService.asmx', 'Upd_Convert_Audio_To_Text', false, { "Vessel_ID": Vessel_ID, "Worklist_ID": Worklist_ID, "Followup_ID": Followup_ID, "WL_Office_ID": WL_Office_ID, "UserID": UserID, "Followup": Followup, "AttachPath": AttachPath, "Action": Action }, onSucc_Audio_To_Text, Onfail, new Array(objsrc.id, Action));
            lastexect = service.get_executor();


        }

        function onSucc_Audio_To_Text(retval, prm) {


            if ((retval.toString() == "1" && prm[1].toString() == "FINALIZED") || retval.toString() == "2") {

                if (prm[1].toString() == "DRAFT") {
                    document.getElementById(prm[0]).style.display = "none";
                    document.getElementById(prm[0].replace("imgSave", "imgConfirm")).style.display = "none";

                }
                else {
                    document.getElementById(prm[0]).style.display = "none";
                    document.getElementById(prm[0].replace("imgConfirm", "imgSave")).style.display = "none";
                    alert("Confirm successfully.");
                }

                if (retval.toString() == "2") {

                    alert("This audio has been already converted to text.Please refresh the page.");
                }
            }
            else if (retval.toString() == "1") {

                alert("Saved successfully.");
            }


        }


        function Onfail(retval) {

            alert("failed");
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="page-title" style="text-align: center; font-weight: bold">
        Convert Audio To Text
    </div>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server">
        <ProgressTemplate>
            <div id="blur-on-updateprogress">
                &nbsp;</div>
            <div id="divProgress" style="position: absolute; left: 49%; top: 30px; z-index: 2;
                color: black">
                <img src="../../Images/loaderbar.gif" alt="Please Wait" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div style="min-height: 0px; max-height: 800px; overflow: auto;">
                <asp:Button ID="btnrefresh" runat="server" Text="Refresh" Style="margin: 2px" OnClick="btnrefresh_Click" />
                <asp:GridView ID="gvAttachments" runat="server" AutoGenerateColumns="false" CellPadding="2"
                    ShowHeaderWhenEmpty="true" EmptyDataText="No record found !" GridLines="None"
                    Width="100%" DataKeyNames="WL_OFFICE_ID,WORKLIST_ID,VESSEL_ID,FOLLOWUP_ID,Attach_Path,FL_OFFICE_ID"
                    CssClass="gridmain-css">
                    <Columns>
                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                            HeaderText="Play">
                            <ItemTemplate>
                                <a id="A1" onmousedown="PlayAudio(this);" runat="server">
                                    <img src="../../Images/AquaPlayicon.png" alt="play" height="25px" title="Play" />
                                    <audio onended="AudioOnEnded(this)" preload="none" style="width: 60px; background-color: transparent;
                                        padding: 0px; margin: 0px" src='<%# "http://" +HttpContext.Current.Request.Url.Host + HttpContext.Current.Request.ApplicationPath+"/uploads/Technical/"+Eval("Attach_Path").ToString()%>'>
                                    </audio>
                                </a>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Audio-Text" HeaderStyle-Width="800px" ItemStyle-Width="800px">
                            <ItemTemplate>
                                <table style="width: 100%">
                                    <tr>
                                        <td style="padding-right: 10px">
                                            <asp:Label ID="lbljobtitle" Font-Size="9px" Font-Names="verdana" ForeColor="#2E4A9E"
                                                runat="server" Text='<%#Eval("JOB_DESCRIPTION") %>'></asp:Label>
                                        </td>
                                        <td style="border-right: 1px solid #702495; width: 1px">
                                        </td>
                                        <td style="padding-left: 10px; width: 200px">
                                            <asp:Label ID="lblAttachName" runat="server" Font-Size="10px" ForeColor="#2E4A9E"
                                                Text='<%#Eval("ATTACH_NAME") %>'></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                            <asp:TextBox ID="txtAudText" runat="server" TextMode="MultiLine" Height="60px" Width="800px"
                                                Text='<%#Bind("FOLLOWUP") %>'></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Date" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lblAttach_Date" runat="server" Text='<%#Eval("DATE_OF_CREATION","{0:dd/MMM/yy}") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                            HeaderText="Download">
                            <ItemTemplate>
                                <asp:Image ID="imgFlagOFF" Height="22px" runat="server" ImageUrl="~/Images/Download-icon.png"
                                    ToolTip="Download" onclick='<%#"DocOpen(&#39;../../Uploads/Technical/" + Eval("Attach_Path") +"&#39;)" %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                            HeaderText="Save">
                            <ItemTemplate>
                                <asp:ImageButton ID="imgSave" Height="22px" Style="display: block" runat="server"
                                    ImageUrl="~/Images/Save.png" OnClick='<%#"Upd_Convert_Audio_To_Text("+Eval("VESSEL_ID")+","+Eval("WORKLIST_ID")+","+Eval("FOLLOWUP_ID")+","+Eval("WL_OFFICE_ID")+","+Session["USERID"].ToString()+",&#39;"+Eval("Attach_Path").ToString()+"&#39;,&#39;DRAFT&#39;,this);return false;" %>'
                                    ToolTip="Save" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                            HeaderText="Confirm">
                            <ItemTemplate>
                                <asp:ImageButton ID="imgConfirm" Height="22px" Style="display: block" runat="server"
                                    ImageUrl="~/Images/Ok-icon.png" OnClick='<%#"Upd_Convert_Audio_To_Text("+Eval("VESSEL_ID")+","+Eval("WORKLIST_ID")+","+Eval("FOLLOWUP_ID")+","+Eval("WL_OFFICE_ID")+","+Session["USERID"].ToString()+",&#39;"+Eval("Attach_Path").ToString()+"&#39;,&#39;FINALIZED&#39;,this);return false;" %>'
                                    ToolTip="Confirm" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <HeaderStyle CssClass="HeaderStyle-css" />
                    <RowStyle CssClass="RowStyle-css" />
                    <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                    <SelectedRowStyle CssClass="SelectedRowStyle-css" />
                    <EmptyDataRowStyle CssClass="EmptyDataRowStyle-css" />
                </asp:GridView>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
