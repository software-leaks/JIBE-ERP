<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="VesselGA.aspx.cs"
    Inherits="VesselGA" Title="VesselGA" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="../../UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script type="text/javascript" src="../../Scripts/jquery-1.4.1.min.js"></script>
    <script type="text/javascript" src="../../Scripts/jquery-ui.min.js"></script>
    <link href="../../Styles/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.draggable.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.mouse.js" type="text/javascript"></script>
    <script src="../../Scripts/ModalPopUp.js" type="text/javascript"></script>
    <link href="../../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <link href="../../Purchase/styles/Purchase.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/Common_Functions.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        function Divaddnewlink() {
            document.getElementById("divadd").style.display = "block";
        }
        function hidePopup() {
            document.getElementById("divadd").style.display = "none";
        }
        $(document).ready(function () {
            $(".draggable").draggable();
        });

        function OnAddMaker() {
            document.getElementById('Iframe').src = '../../Infrastructure/Libraries/CompanyType.aspx';
            showModal('dvIframe');
            return false;
        }

        function validation() {

            //            if (document.getElementById("ctl00_MainContent_DDLVessel1").value == "0") {
            //                alert("Please select Vessel.");
            //                document.getElementById("ctl00_MainContent_DDLVessel1").focus();
            //                return false;
            //            }


            //            if (document.getElementById("ctl00_MainContent_HiddenFlag").value != "Edit") {

            //                if (document.getElementById("ctl00_MainContent_ddlRelation").value == "0") {
            //                    alert("Please select relation with parent.");
            //                    document.getElementById("ctl00_MainContent_ddlRelation").focus();
            //                    return false;
            //                }
            //                if (document.getElementById("ctl00_MainContent_ddlParentCompany").value == "0") {
            //                    alert("Please select parent company.");
            //                    document.getElementById("ctl00_MainContent_ddlParentCompany").focus();
            //                    return false;
            //                }

            //            }

            if (document.getElementById("ctl00_MainContent_txtPathID").value.trim() == "") {
                alert("Please enter PathID.");
                document.getElementById("ctl00_MainContent_txtPathID").focus();
                return false;
            }
            //            if (document.getElementById("ctl00_MainContent_txtCompCode").value != "") {

            //                if (isNaN(document.getElementById("ctl00_MainContent_txtCompCode").value)) {
            //                    alert("This field is allow only numeric value");
            //                    document.getElementById("ctl00_MainContent_txtCompCode").focus()
            //                    return false;
            //                }

            //            }

            if (document.getElementById("ctl00_MainContent_txtPathName").value.trim() == "") {
                alert("Please enter Path name.");
                document.getElementById("ctl00_MainContent_txtPathName").focus();
                return false;
            }

            //debugger;
            if (document.getElementById("ctl00_MainContent_chkIsGA").checked == true) {
                var hdnFlag = document.getElementById("ctl00_MainContent_HiddenFlag");
                if (hdnFlag.value == "Add") {
                    if (document.getElementById("ctl00_MainContent_FileUploaderIMG").value == "") {
                        if (document.getElementById("ctl00_MainContent_FileUploadSVG").value != "") {
                            return true;
                        }
                        else {
                            alert("Upload atleast one file.");
                            return false;
                        }
                    }
                    else {
                        return true;
                    }
                }
                else {

                    var dl = document.getElementById("<%=gvPMSJobAttachment.ClientID%>");

                    var inputs = dl.innerText; //.substring(dl.innertext.lastIndexOf(' ')).toLowerCase();

                    //                    var inputsIMG = inputs.substring(inputs.lastIndexOf(' ')).toLowerCase(); ;
                    //                    var inputsSVG = inputs.substring(inputs.lastIndexOf(' ') + 1).toLowerCase(); ;

                    if (inputs == "") {
                        if (document.getElementById("ctl00_MainContent_FileUploaderIMG").value == "") {
                            if (document.getElementById("ctl00_MainContent_FileUploadSVG").value != "") {
                                return true;
                            }
                            else {
                                alert("Upload atleast one file.");
                                return false;
                            }
                        }
                        else {
                            return true;
                        }
                    }
                    else {
                        return true;
                    }



                    //                    if (document.getElementById("ctl00_MainContent_lblATTACHMENTIMG_PATH").value == "") {
                    //                        if (document.getElementById("ctl00_MainContent_lblATTACHMENTSVG_PATH").value != "") {
                    //                            return true;
                    //                        }
                    //                        else {
                    //                            if (document.getElementById("ctl00_MainContent_FileUploaderIMG").value == "") {
                    //                                if (document.getElementById("ctl00_MainContent_FileUploadSVG").value != "") {
                    //                                    return true;
                    //                                }
                    //                                else {
                    //                                    alert("Please upload one of the file.");
                    //                                    return false;
                    //                                }
                    //                            }
                    //                            else {
                    //                                return true;
                    //                            }
                    //                        }
                    //                    }
                    //                    else {

                    // }


                    //return true;
                }
            }

            return true;
        }



        function ValidateFileUploadIMG() {
            var fuData = document.getElementById("ctl00_MainContent_FileUploaderIMG");
            var FileUploadPath = fuData.value;
            if (FileUploadPath != "") {
                var Extension = FileUploadPath.substring(
                    FileUploadPath.lastIndexOf('.') + 1).toLowerCase();

                if (Extension == "jpeg" || Extension == "jpg" || Extension == "png" || Extension == "bmp") {
                }
                else {

                    alert("Upload only PNG, JPG, JPEG or BMP file types.");
                    fuData.value = "";
                    return false;
                }
            }
        }

        function ValidateFileUploadSVG() {
            var fuData = document.getElementById("ctl00_MainContent_FileUploadSVG");
            var FileUploadPath = fuData.value;
            if (FileUploadPath != "") {
                var Extension = FileUploadPath.substring(
                    FileUploadPath.lastIndexOf('.') + 1).toLowerCase();

                if (Extension == "svg") {
                }
                else {

                    alert("Upload only SVG file type.");
                    fuData.value = "";
                    return false;
                }
            }
        }

        //        function ValidateCheck() {
        //            var fuData = document.getElementById("ctl00_MainContent_chkIsGA");
        //            var FileUploadPath = fuData.value;

        //            alert(FileUploadPath);
        //        }


        function RefreshMakerFromChild() {

            document.getElementById("ctl00_MainContent_btnHiddenSubmit").click();
        }

        function CallingServerSideFunction() {


            alert('test');
            alert(document.getElementById("ctl00_MainContent_DDLVessel1").value);
            var parm = document.getElementById("ctl00_MainContent_DDLVessel1").value;
            //            var parm = document.getElementById("ctl00_MainContent_DDLVessel1");
            //            alert(parm);
            //PageMethods.GetData(parm.options[parm.selectedIndex].value);
            PageMethods.GetData(parm);
        }

               
    </script>
    <style type="text/css">
        .style2
        {
            width: 10%;
        }
        .style3
        {
            width: 20%;
        }
        .style4
        {
            width: 22%;
        }
        .style5
        {
            width: 14%;
        }
        .style6
        {
            width: 23%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <center>
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
        <div style="border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px; width: 95%;">
            <div class="page-title">
                Vessel GA
            </div>
            <div style="width: 100%; height: 650px; color: Black;">
                <asp:UpdatePanel ID="UpdatePaneCompany" runat="server">
                    <ContentTemplate>
                        <div class="subHeader" style="display: none; position: relative; right: 0px">
                            <asp:Button ID="btnHiddenSubmit" runat="server" Text="btnHiddenSubmit" OnClick="btnHiddenSubmit_Click" />
                        </div>
                        <div style="padding-top: 5px; padding-bottom: 5px; height: 70px; width: 100%">
                            <table width="100%" cellpadding="2" cellspacing="2">
                                <tr>
                                    <%--  <td style="width: 15%">
                                        <asp:DropDownList ID="DDLFleet" AppendDataBoundItems="true" runat="server"
                                            Width="200px">
                                        </asp:DropDownList>
                                    </td>--%>
                                    <%-- <td align="left">--%>
                                    <%--  <asp:ImageButton ID="imbAddMaker" ImageUrl="~/Images/AddMaker.png" Height="12px"
                                            ToolTip="Add Company Type" runat="server" OnClientClick="return OnAddMaker();" />--%>
                                    <%--   </td>--%>
                                    <%-- <td align="right" class="style6">
                                        Vessel :&nbsp;
                                    </td>--%>
                                    <td align="right" class="style6">
                                        Vessel Types :&nbsp;
                                    </td>
                                    <td style="width: 15%">
                                        <asp:DropDownList ID="DDLVessel" AppendDataBoundItems="true" runat="server" Width="100%">
                                        </asp:DropDownList>
                                    </td>
                                    <td align="right" class="style5">
                                        <%--  Fleet :&nbsp;--%>
                                        Import using Excel : 
                                    </td>
                                    <td>
                                      <asp:FileUpload ID="FileUpload1" runat="server" Height="24px" Style="font-size: small" />
                                      </td>
                                      <td>
                                             <asp:Button ID="btnUpload" runat="server" Text="Import" Style="font-size: small"
                        OnClick="btnUpload_Click" Width="121px" />
                                    </td>

                                    <td align="right" class="style3">
                                        Path Name :&nbsp;
                                    </td>
                                    <td align="left" class="style4">
                                        <asp:TextBox ID="txtfilter" runat="server" Width="80%"></asp:TextBox>
                                        <ajaxToolkit:TextBoxWatermarkExtender ID="TBWE2" runat="server" TargetControlID="txtfilter"
                                            WatermarkText="Type to search" WatermarkCssClass="watermarked" />
                                    </td>
                                    <td align="center" style="width: 30px">
                                        <asp:ImageButton ID="btnFilter" runat="server" OnClick="btnFilter_Click" ToolTip="Search"
                                            ImageUrl="~/Images/SearchButton.png" />
                                    </td>
                                    <td align="center" style="width: 30px">
                                        <asp:ImageButton ID="btnRefresh" runat="server" ImageUrl="~/Images/Refresh-icon.png"
                                            OnClick="btnRefresh_Click" ToolTip="Refresh" />
                                    </td>
                                    <td align="center" style="width: 30px">
                                        <asp:ImageButton ID="ImgAdd" runat="server" ImageUrl="~/Images/Add-icon.png" OnClick="ImgAdd_Click"
                                            ToolTip="Add New Company" />
                                    </td>
                                    <td style="width: 30px">
                                        <asp:ImageButton ID="ImgExpExcel" runat="server" ToolTip="Export to excel" OnClick="ImgExpExcel_Click"
                                            Visible="false" ImageUrl="~/Images/Exptoexcel.png" />
                                    </td>
                                </tr>
                                <%--  <tr>
                                    <td align="right">
                                        Currency :&nbsp;
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="ddlCurrencyFilter" AppendDataBoundItems="true" runat="server"
                                            Width="50%">
                                        </asp:DropDownList>
                                    </td>
                                    <td align="right">
                                        Country :&nbsp;
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="ddlCountryFilter" AppendDataBoundItems="true" runat="server"
                                             Width="100%">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                </tr>--%>
                            </table>
                        </div>
                        <div style="margin-left: auto; margin-right: auto; text-align: center;">
                            <div>
                                <asp:GridView ID="GridViewCompany" runat="server" CellPadding="1" OnRowDataBound="GridViewCompany_RowDataBound"
                                    OnSorting="GridViewCompany_Sorting" EmptyDataText="NO RECORDS FOUND!" AllowSorting="True"
                                    AutoGenerateColumns="False" Width="100%" DataKeyNames="ID">
                                    <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                    <RowStyle CssClass="RowStyle-css" />
                                    <HeaderStyle CssClass="HeaderStyle-css" Height="25px" />
                                    <PagerStyle CssClass="PMSPagerStyle-css" />
                                    <SelectedRowStyle BackColor="#FFFFCC" />
                                    <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Path ID">
                                            <%--   <HeaderTemplate>
                                                <asp:LinkButton ID="lblCodeHeader" runat="server" CommandName="Sort" CommandArgument="Path_ID" 
                                                    ForeColor="Black">Path ID&nbsp;</asp:LinkButton>
                                                <img id="Company_Code" runat="server" visible="false" />
                                            </HeaderTemplate>--%>
                                            <ItemTemplate>
                                                <asp:Label ID="lblPathID" runat="server" Text='<%#Eval("Path_ID")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="50px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Path Name">
                                            <%-- <HeaderTemplate>
                                                <asp:LinkButton ID="lblCompany_TypeHeader" runat="server" CommandName="Sort" CommandArgument="Company_Type"
                                                    ForeColor="Black">Path Name&nbsp;</asp:LinkButton>
                                                <img id="Company_Type" runat="server" visible="false" />
                                            </HeaderTemplate>--%>
                                            <ItemTemplate>
                                                <asp:Label ID="lblPath_Name" runat="server" Text='<%# Bind("Path_Name") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="120px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <%-- <asp:TemplateField HeaderText="Vessel Name">
                                            <HeaderTemplate>
                                                <asp:LinkButton ID="lblNameHeader" runat="server" CommandName="Sort" CommandArgument="Company_Name"
                                                    ForeColor="Black">Vessel Name&nbsp;</asp:LinkButton>
                                                <img id="Company_Name" runat="server" visible="false" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lblName" Style="color: Black" runat="server" Text='<%#Eval("Vessel_Name")%>'
                                                    CommandArgument='<%#Eval("ID")%>' OnCommand="onUpdate"></asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="220px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>--%>
                                        <asp:TemplateField HeaderText="Img">
                                            <ItemTemplate>
                                                <asp:Label ID="lblIMG" runat="server" Text='<%#Eval("IMG")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="80px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Svg">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSVG" runat="server" Text='<%#Eval("SVG")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="80px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <%--<asp:TemplateField HeaderText="SVG">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSVG" runat="server" Width="180px" Text='<%# Eval("SVG") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" CssClass="PMSGridItemStyle-css"></ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>--%>
                                        <asp:TemplateField HeaderText="Parent Path">
                                            <ItemTemplate>
                                                <asp:Label ID="lblIncorp" runat="server" Text='<%# Eval("Parent_Path") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="80px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Is GA">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkIsGA" runat="server" Enabled="false" Checked='<%# Eval("Is_GA").ToString()=="0"?false:true %>'
                                                    ForeColor="white" />
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Center" Width="80px"></ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <%--<asp:TemplateField HeaderText="Curr.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblBase_Curr" runat="server" Text='<%# Bind("Currency_code") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="50px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Address" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAddress" runat="server" Text='<%# Bind("Address") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtAddress" runat="server" TextMode="MultiLine" Text='<%# Bind("Address") %>'
                                                    Width="120px"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" CssClass="PMSGridItemStyle-css"></ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Country">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCountry" runat="server" Text='<%# Bind("COUNTRY_NAME") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="70px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Email">
                                            <ItemTemplate>
                                                <asp:Label ID="lblEmail" runat="server" Text='<%# Bind("Email1") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="70px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Phone">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPhone" runat="server" Text='<%# Bind("Phone1") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" CssClass="PMSGridItemStyle-css"></ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>--%>
                                        <asp:TemplateField HeaderText="Action">
                                            <HeaderTemplate>
                                                Action
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <table cellpadding="2" cellspacing="2">
                                                    <tr>
                                                        <%-- <td>
                                                            <asp:ImageButton ID="ImgVerified" runat="server" Text="Verified" OnCommand="onVerified"
                                                                Visible='<%# uaEditFlag %>' CommandArgument='<%#Eval("[ID]")%>' ForeColor="Black"
                                                                ToolTip="Verify " ImageUrl="~/Images/Allot-Flag-Completed.PNG" Height="16px"></asp:ImageButton>
                                                        </td>
                                                          <td>
                                                            <asp:ImageButton ID="ImgUnverified" runat="server" Text="Verified" OnCommand="onVerified"
                                                                Visible='<%# uaEditFlag %>' CommandArgument='<%#Eval("[ID]")%>' ForeColor="Black"
                                                                ToolTip="Verify" ImageUrl="~/Images/Allot-Flag-Active.PNG" Height="16px"></asp:ImageButton>
                                                        </td>--%>
                                                        <td>
                                                            <asp:ImageButton ID="ImgUpdate" runat="server" Text="Update" OnCommand="onUpdate"
                                                                Visible='<%# uaEditFlag %>' CommandArgument='<%#Eval("[ID]")%>' ForeColor="Black"
                                                                ToolTip="Edit" ImageUrl="~/Images/Edit.gif" Height="16px"></asp:ImageButton>
                                                        </td>
                                                        <td>
                                                            <asp:ImageButton ID="ImgDelete" runat="server" Text="Delete" OnCommand="onDelete"
                                                                Visible='<%# uaDeleteFlage %>' OnClientClick="return confirm('Are you sure want to delete?')"
                                                                CommandArgument='<%#Eval("[ID]")%>' ForeColor="Black" ToolTip="Delete" ImageUrl="~/Images/delete.png"
                                                                Height="16px"></asp:ImageButton>
                                                        </td>
                                                        <%--  <td>
                                                            <asp:Image ID="imgRecordInfo" ImageAlign="AbsMiddle" ImageUrl="~/Images/RecordInformation.png"
                                                                Height="16px" Width="16px" runat="server" onclick='<%# "Get_Record_Information(&#39;LIB_COMPANY&#39;,&#39;ID="+Eval("ID").ToString()+"&#39;,event,this)" %>' />
                                                        </td>--%>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Center" Width="40px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                <uc1:ucCustomPager ID="ucCustomPagerItems" runat="server" PageSize="30" OnBindDataItem="BindVesselGA" />
                                <asp:HiddenField ID="HiddenFlag" runat="server" EnableViewState="False" />
                            </div>
                            <br />
                        </div>
                        <div id="divadd" title="<%= OperationMode %>" style="display: none; border: 1px solid Gray;
                            font-family: Tahoma; text-align: left; font-size: 12px; color: Black; width: 25%;">
                            <center>
                                <table width="98%" cellpadding="2" cellspacing="2">
                                    <%--    <tr>
                                        <td align="right" class="style2">
                                            Vessel: &nbsp;
                                        </td>
                                        <td style="color: #FF0000; width: 1%" align="right">
                                            
                                        </td>
                                        <td align="right" style="width: 25%">
                                            <asp:DropDownList ID="DDLVessel1" runat="server" Width="100%" 
                                                CssClass="txtInput" AutoPostBack="True"  onselectedindexchanged="DDLVessel1_SelectedIndexChanged" 
                                              >
                                            </asp:DropDownList> 
                                        </td>
                                    
                                    </tr>--%>
                                    <tr>
                                        <td align="right" class="style2">
                                            Path ID : &nbsp;
                                        </td>
                                        <td style="color: #FF0000; width: 1%" align="right">
                                            *
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txtPathID" runat="server" MaxLength="50" CssClass="txtInput" Width="99%"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" class="style2">
                                            Path Name : &nbsp;
                                        </td>
                                        <td style="color: #FF0000; width: 1%" align="right">
                                            *
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txtPathName" runat="server" MaxLength="50" CssClass="txtInput" Width="99%"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" class="style2">
                                            Vessel Type: &nbsp;
                                        </td>
                                        <td style="color: #FF0000; width: 1%" align="right">
                                        </td>
                                        <td align="right" style="width: 25%">
                                            <asp:DropDownList ID="ddlVesselType" runat="server" Width="100%" CssClass="txtInput"
                                                AutoPostBack="True" OnSelectedIndexChanged="ddlVesselType_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <%--  onchange="CallingServerSideFunction()"  --%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" class="style2">
                                            Parent ID : &nbsp;
                                        </td>
                                        <td style="color: #FF0000; width: 1%" align="right">
                                            <%--<asp:Label ID="td_ParentCompany" Text="*" runat="server" ForeColor="Red"></asp:Label>--%>
                                        </td>
                                        <td align="left">
                                            <asp:DropDownList ID="ddlParentID" runat="server" Width="100%" CssClass="txtInput">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" class="style2">
                                            Upload Image : &nbsp;
                                        </td>
                                        <td style="color: #FF0000; width: 1%" align="right">
                                            <%--<asp:Label ID="Label3" Text="*" runat="server" ForeColor="Red"></asp:Label>--%>
                                        </td>
                                        <td align="left">
                                            <%-- <asp:TextBox ID="txtRegNo" runat="server" MaxLength="250" CssClass="txtInput" Width="100%"></asp:TextBox>--%>
                                            <asp:FileUpload ID="FileUploaderIMG" Style="width: 90%; height: 18px; background-color: #F2F2F2;
                                                border: 1px solid #cccccc; font-size: 12px; cursor: pointer" runat="server" onchange="ValidateFileUploadIMG()" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" class="style2">
                                            Upload SVG : &nbsp;
                                        </td>
                                        <td style="color: #FF0000; width: 1%" align="right">
                                            <%--<asp:Label ID="Label4" Text="*" runat="server" ForeColor="Red"></asp:Label>--%>
                                        </td>
                                        <td align="left">
                                            <%--<asp:TextBox ID="TextBox1" runat="server" MaxLength="250" CssClass="txtInput" Width="100%"></asp:TextBox>--%>
                                            <asp:FileUpload ID="FileUploadSVG" Style="width: 90%; height: 18px; background-color: #F2F2F2;
                                                border: 1px solid #cccccc; font-size: 12px; cursor: pointer" runat="server" onchange="ValidateFileUploadSVG()" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" class="style2">
                                            Is GA : &nbsp;
                                        </td>
                                        <td style="color: #FF0000; width: 1%" align="right">
                                        </td>
                                        <td align="left">
                                            <asp:CheckBox ID="chkIsGA" runat="server"></asp:CheckBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="color: #FF0000; width: 1%" align="right">
                                        </td>
                                        <td style="color: #FF0000; width: 1%" align="right">
                                        </td>
                                        <td>
                                            <asp:DataList ID="gvPMSJobAttachment" runat="server" RepeatColumns="5" RepeatDirection="Vertical"
                                                RepeatLayout="Table" CellSpacing="2">
                                                <ItemTemplate>
                                                    <div style="background-color: #C3EBFF; border-radius: 2px; padding: 1px; border: 1px solid #ACC9C9">
                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    <table>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:HyperLink ID="lblIMG" runat="server" Visible='<%# Eval("IMG").ToString()==""?false:true %>'
                                                                                    Text='<%# Eval("IMG")%>' NavigateUrl='<%# "../../Uploads/VesselGA/" + Eval("IMG").ToString()%>'
                                                                                    Target="_blank"></asp:HyperLink>
                                                                                <asp:Label ID="lblATTACHMENTIMG_PATH" Visible="false" runat="server" Text='<%# Eval("IMG")%>'></asp:Label>
                                                                            </td>
                                                                            <td align="center">
                                                                                <asp:ImageButton ID="imgbtnDeleteAssembly" runat="server" OnCommand="imgbtnDeleteAssembly_Click"
                                                                                    CommandArgument='<%# Eval("IMG") %>' AlternateText="delete" ImageAlign="AbsMiddle"
                                                                                    Visible='<%# Eval("IMG").ToString()==""?false:true %>' ImageUrl="~/Images/Delete.png" />
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                                <td style="color: #FF0000; background-color: #ACC9C9; width: 0px" align="right">
                                                                </td>
                                                                <td>
                                                                    <table>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:HyperLink ID="lblSVG" runat="server" Text='<%# Eval("SVG")%>' Visible='<%# Eval("SVG").ToString()==""?false:true %>'
                                                                                    NavigateUrl='<%# "../../Uploads/VesselGA/" + Eval("SVG").ToString()%>' Target="_blank"></asp:HyperLink>
                                                                                <asp:Label ID="lblATTACHMENTSVG_PATH" Visible="false" runat="server" Text='<%# Eval("SVG")%>'></asp:Label>
                                                                            </td>
                                                                            <td align="center">
                                                                                <asp:ImageButton ID="imgbtnDeleteSVG" runat="server" OnCommand="imgbtnDeleteSVG_Click"
                                                                                    CommandArgument='<%# Eval("SVG") %>' AlternateText="delete" ImageAlign="AbsMiddle"
                                                                                    Visible='<%# Eval("SVG").ToString()==""?false:true %>' ImageUrl="~/Images/Delete.png" />
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:DataList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="6" style="font-size: 11px; text-align: center; border-style: solid;
                                            padding: 5px 0px 5px 0px; border-color: Silver; border-width: 1px; background-color: #d8d8d8;">
                                            <asp:Button ID="btnsave" runat="server" Text="Save" OnClientClick="return  validation();"
                                                OnClick="btnsave_Click" />
                                            <asp:TextBox ID="txtCompanyID" runat="server" Visible="false" Width="1%"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="6">
                                            <div id="dvRecordInformation" style="text-align: left; width: 100%; border: 0px solid #cccccc;
                                                background-color: #FDFDFD">
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="6" align="right" style="color: #FF0000; font-size: small;">
                                            * Mandatory fields
                                        </td>
                                    </tr>
                                </table>
                            </center>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btnsave" />
                         <asp:PostBackTrigger ControlID="btnUpload" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
        <div id="dvIframe" style="display: none; width: 600px;" title=''>
            <iframe id="Iframe" src="" frameborder="0" style="height: 295px; width: 100%"></iframe>
        </div>
    </center>
</asp:Content>
