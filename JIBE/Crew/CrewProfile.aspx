<%@ Page Title="Crew Profile" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="CrewProfile.aspx.cs" Inherits="Crew_CrewProfile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <style type="text/css">
        #pageTitle
        {
            background-color: gray;
            color: White;
            font-size: 12px;
            text-align: center;
            padding: 2px;
            font-weight: bold;
        }
        #page-content
        {
            font-size: 12px;
        }
        .header
        {
            margin: 0 0 0 0;
            padding: 6px 2 6px 2;
            color: #FFF;
        }
        h4
        {
            font-size: 1.2em;
            color: #ffffff;
            font-weight: bold;
            margin: 0 0 0 5px;
        }
        .content
        {
            background: white;
            padding: 5px;
            margin: 5px;
        }
        .dataTable
        {
            background-color: #ffffff;
            border: 1px solid #efefef;
            border-collapse: collapse;
            color: Teal;
        }
        .dataTable td
        {
            background-color: #ffffff;
            border: 1px solid #efefef;
            border-collapse: collapse;
        }
        .dataTable .data
        {
            vertical-align: top;
            background-color: #ffffff;
            color: Black;
        }
        .inline-edit
        {
            font-size: 10px;
            text-decoration: none;
            font-weight: normal;
            color: Blue;
        }
        .up
        {
            background-image: url(../Images/up.png);
            background-repeat: no-repeat;
        }
        .down
        {
            background-image: url(../Images/down.png);
            background-repeat: no-repeat;
        }
        .class-doc-list
        {
            font-size: 11px;
        }
        .class-doc-edit
        {
            font-size: 11px;
        }
        .control-edit
        {
            font-family: Tahoma;
            font-size: 12px;
            padding: 0px;
            width: 150px;
        }
        .error-message
        {
            font-size: 12px;
            font-weight: bold;
            color: Red;
            text-align: center;
            background-color: Yellow;
        }
        .tooltip
        {
            display: none;
            background: transparent url(../Images/black_arrow.png);
            font-size: 12px;
            height: 70px;
            width: 160px;
            padding: 25px;
            color: #fff;
        }
        .user-answer-table
        {
            font-size: 10px;
            border: 1px solid gray;
        }
        .user-answer-table td
        {
            border: 1px solid #dfdfdf;
            white-space: nowrap;
        }
        .interviewer-table
        {
            border: 1px solid gray;
            background-color: #bfcfdf;
            width: 100%;
        }
        .interviewer-table td
        {
            background-color: #bfcfdf;
        }
        .interview-result-table
        {
            font-size: 10px;
            border: 1px solid gray;
        }
        .interview-reasult
        {
            background-color: Yellow;
            text-align: center;
        }
        .interview-result-table td
        {
            vertical-align: top;
        }
        .Question
        {
            width: 80%;
        }
        .ans1
        {
            background-color: Green;
            border: 1px solid black;
            color: White;
            text-align: center;
            cursor: pointer;
        }
        .ans2
        {
            background-color: #FFBF00;
            border: 1px solid black;
            text-align: center;
            cursor: pointer;
        }
        .ans3
        {
            background-color: #FFBF00;
            border: 1px solid black;
            text-align: center;
            cursor: pointer;
        }
        .ans4
        {
            background-color: Red;
            border: 1px solid black;
            color: White;
            text-align: center;
            cursor: pointer;
        }
        .AttributeEditBox
        {
            border: 0;
            width: 100%;
        }
        .required
        {
            background-color: #F2F5A9;
            border: 1px solid #dcdcdc;
            height: 18px;
        }
        .popup-background
        {
            top: 0;
            left: 0;
            width: 100%;
            height: 100%;
        }
        .popup-content
        {
            position: absolute;
            float: left;
            z-index: 1;
            padding: 10px;
        }
        .imgCOC
        {
            vertical-align: middle;
        }
        
        
        .gradiant-css-orange
        {
            filter: progid:DXImageTransform.Microsoft.Gradient(GradientType=0,StartColorStr='#F8D79B',EndColorStr='#F1C15F');
            background: -webkit-gradient(linear, left top, left bottom, from(#F8D79B), to(#F1C15F));
            background: -moz-linear-gradient(top,  #F8D79B,  #F1C15F);
            color: Black;
        }
        .gradiant-css-blue
        {
            filter: progid:DXImageTransform.Microsoft.Gradient(GradientType=0,StartColorStr='#CEE3F6',EndColorStr='#99BBE6');
            background: -webkit-gradient(linear, left top, left bottom, from(#CEE3F6), to(#99BBE6));
            background: -moz-linear-gradient(top,  #CEE3F6,  #99BBE6);
            color: Black;
        }
        .print
        {
            cursor: pointer;
            float: right;
            margin: 2px 5px 0px 0px;
        }
    </style>
    <script type="text/javascript">
        function PrintDiv(dvID) {

            var a = window.open('', '', 'left =' + screen.width + ',top=' + screen.height + ',width=0,height=0,toolbar=0,scrollbars=0,status=0');
            a.document.write(document.getElementById(dvID).innerHTML);
            a.document.close();
            a.focus();
            a.print();
            a.close();
            return false;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div id="page-title" class="page-title">
        Staff Profile
        <img src="../Images/Printer.png" title="*Print*" class="print" alt=""
            onclick="PrintDiv('dvPageContent')" />
    </div>
    <div id="dvPageContent" style="padding: 5px;">
        <asp:Panel ID="pnlView_PersonalDetails" runat="server" Visible="true">
            <fieldset style="text-align: left; margin: 0px; padding: 2px;">
                <legend>Personal Details : </legend>
                <table border="0" style="width: 100%" class="dataTable">
                    <tr>
                        <td>
                            Surname
                        </td>
                        <td class="data">
                            <asp:Label ID="lblSurname" runat="server"></asp:Label>
                        </td>
                        <td>
                            First Name
                        </td>
                        <td class="data">
                            <asp:Label ID="lblGivenName" runat="server"></asp:Label>
                        </td>
                        <td>
                            Date of Birth
                        </td>
                        <td class="data">
                            <asp:Label ID="lblDateOfBirth" runat="server"></asp:Label>
                        </td>
                        <td rowspan="7" style="width: 120px; text-align: center;">
                            <asp:Image ID="imgCrewPic" ImageUrl="" runat="server" Visible="false" Style="max-width: 105px;
                                padding: 3px;" AlternateText="Staff Photo" />
                            <asp:Image ID="imgNoPic" Style="padding: 3px;" ImageUrl="~/Images/NoPhoto.png" runat="server"
                                Visible="false" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Alias
                        </td>
                        <td class="data">
                            <asp:Label ID="lblAlias" runat="server"></asp:Label>
                        </td>
                        <td>
                            Place of Birth
                        </td>
                        <td class="data">
                            <asp:Label ID="lblPlaceOfBirth" runat="server"></asp:Label>
                        </td>
                        <td>
                            Nationality
                        </td>
                        <td class="data">
                            <asp:Label ID="lblNationality" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Marital Status
                        </td>
                        <td class="data">
                            <asp:Label ID="lblMaritalStatus" runat="server"></asp:Label>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            Telephone
                        </td>
                        <td class="data">
                            <asp:Label ID="lblTelephone" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td runat="server" id="tdIPA1" rowspan="3" style="vertical-align: top">
                            Address
                        </td>
                        <td runat="server" id="tdIPA2" rowspan="3" colspan="3" class="data">
                            <asp:Label ID="lblAddress" runat="server"></asp:Label>
                        </td>
                        <td>
                            Mobile
                        </td>
                        <td class="data">
                            <asp:Label ID="lblMobile" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr runat="server" id="trUSADDRESS">
                        <td rowspan="3" style="vertical-align: top">
                            Address Line1
                        </td>
                        <td class="data">
                            <asp:Label ID="lblAL1" runat="server"></asp:Label>
                        </td>
                        <td>
                            Address Line2
                        </td>
                        <td class="data">
                            <asp:Label ID="lblAL2" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Fax
                        </td>
                        <td class="data">
                            <asp:Label ID="lblFax" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            E-mail
                        </td>
                        <td class="data">
                            <asp:Label ID="lblEMail" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="6">
                            Nearest International Airport: &nbsp;<asp:Label ID="lblIntlAirport" CssClass="data"
                                runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr runat="server" id="trCountry">
                        <td>
                            Country
                        </td>
                        <td class="data">
                            <asp:Label ID="lblICountry" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
            </fieldset>
        </asp:Panel>
        <asp:Panel ID="pnlView_Passport" runat="server">
            <fieldset style="text-align: left; margin: 0px; padding: 2px; font-size: 11px;">
                <legend>Passport and Seaman Book : </legend>
                <table border="0" style="width: 100%" class="dataTable">
                    <tr>
                        <td>
                            Passport No
                        </td>
                        <td class="data">
                            <asp:Label ID="lblPassport_No" runat="server"></asp:Label>
                        </td>
                        <td>
                            Place of Issue
                        </td>
                        <td class="data">
                            <asp:Label ID="lblPassport_Place" runat="server"></asp:Label>
                        </td>
                        <td>
                            Issue Date
                        </td>
                        <td class="data">
                            <asp:Label ID="lblPassport_IssueDt" runat="server"></asp:Label>
                        </td>
                        <td>
                            Expiry Date
                        </td>
                        <td class="data">
                            <asp:Label ID="lblPassport_ExpDt" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Seaman Bk. No
                        </td>
                        <td class="data">
                            <asp:Label ID="lblSeamanBk_No" runat="server"></asp:Label>
                        </td>
                        <td>
                            Place of Issue
                        </td>
                        <td class="data">
                            <asp:Label ID="lblSeamanBk_Place" runat="server"></asp:Label>
                        </td>
                        <td>
                            Issue Date
                        </td>
                        <td class="data">
                            <asp:Label ID="lblSeamanBk_IssueDt" runat="server"></asp:Label>
                        </td>
                        <td>
                            Expiry Date
                        </td>
                        <td class="data">
                            <asp:Label ID="lblSeamanBk_ExpDt" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
            </fieldset>
        </asp:Panel>
        <asp:Panel ID="pnlView_NextOfKin" runat="server">
            <fieldset style="text-align: left; margin: 0px; padding: 2px; font-size: 11px;">
                <legend>Next of Kin Details : </legend>
                <table border="0" cellpadding="4" class="dataTable">
                    <tr>
                        <td style="width: 100px">
                            First Name
                        </td>
                        <td class="data" style="width: 300px">
                            <asp:Label ID="lblNOKName" runat="server" Width="300px"></asp:Label>
                        </td>
                        <td style="width: 100px">
                            Surname
                        </td>
                        <td class="data" style="width: 100px">
                            <asp:Label ID="lblNOKSurname" runat="server" Width="106px">                                                        
                            </asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 100px">
                            Relationship
                        </td>
                        <td class="data" style="width: 300px">
                            <asp:Label ID="lblNOKrelationship" runat="server" Width="106px">                                                        
                            </asp:Label>
                        </td>
                        <td style="width: 100px">
                            Phone Number
                        </td>
                        <td class="data" style="width: 100px">
                            <asp:Label ID="lblNOKPhone" runat="server" Width="100px"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 100px">
                            Date Of Birth
                        </td>
                        <td class="data" style="width: 300px">
                            <asp:Label ID="lblNOKDOB" runat="server" Width="106px">                                                        
                            </asp:Label>
                        </td>
                        <td id="tdCountry1" runat="server" style="width: 100px">
                            Country
                        </td>
                        <td class="data" id="tdCountry2" runat="server" style="width: 100px">
                            <asp:Label ID="lblNOKCountry" runat="server" Width="100px"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td id="tdIA1" runat="server" style="width: 100px">
                            Address
                        </td>
                        <td id="tdIA2" runat="server" rowspan="2" class="data" style="width: 300px">
                            <asp:Label ID="lblNOKAddress" runat="server" Width="300px"></asp:Label>
                        </td>
                        <td id="tdA1" runat="server" style="width: 100px">
                            Address Line 1
                        </td>
                        <td id="tdA2" runat="server" class="data" style="width: 300px">
                            <asp:Label ID="lblA1" runat="server" Width="300px"></asp:Label>
                        </td>
                        <td id="tdA3" runat="server" style="width: 100px">
                            Address Line 2
                        </td>
                        <td id="tdA4" runat="server" class="data" style="width: 300px">
                            <asp:Label ID="lblA2" runat="server" Width="300px"></asp:Label>
                        </td>
                        <td id="tdSSN1" runat="server" style="width: 100px">
                            SSN
                        </td>
                        <td id="tdSSN2" runat="server" class="data" style="width: 300px">
                            <asp:Label ID="lblNOKSSN" runat="server" Width="300px"></asp:Label>
                        </td>
                    </tr>
                </table>
            </fieldset>
        </asp:Panel>
        <asp:Panel ID="pnlDependents" runat="server" Visible="true">
            <fieldset style="text-align: left; margin: 0px; padding: 2px; font-size: 11px;">
                <legend>Dependents : </legend>
                <asp:ObjectDataSource ID="objDS_Dependents" runat="server" SelectMethod="Get_Crew_DependentsByCrewID"
                    TypeName="SMS.Business.Crew.BLL_Crew_CrewDetails">
                    <SelectParameters>
                        <asp:QueryStringParameter Name="CrewID" QueryStringField="ID" Type="Int32" />
                        <asp:Parameter Name="IsNOK" Type="Int32" DefaultValue="0" />
                    </SelectParameters>
                </asp:ObjectDataSource>
                <asp:GridView ID="GridView_Dependents" runat="server" DataKeyNames="ID" BackColor="White"
                    BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" GridLines="Horizontal"
                    Width="100%" Font-Size="11px" AutoGenerateColumns="False" DataSourceID="objDS_Dependents"
                    AllowSorting="True" ForeColor="Black">
                    <Columns>
                        <asp:TemplateField HeaderText="Dependent Name">
                            <ItemTemplate>
                                <asp:Label ID="lblFullName" runat="server" Text='<%#Bind("Fullname")%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="200px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Relationship ">
                            <ItemTemplate>
                                <asp:Label ID="lblRelationship" runat="server" Text='<%#Bind("Relationship")%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-Width="100px" HeaderText="Beneficiary?">
                            <ItemTemplate>
                                <asp:Label ID="lblBeneficiary" runat="server" Text='<%# Convert.ToBoolean(Eval("IsBeneficiary"))?"Yes":"No" %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                    </Columns>
                    <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                    <HeaderStyle BackColor="#333333" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
                    <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                    <SortedAscendingCellStyle BackColor="#F7F7F7" />
                    <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
                    <SortedDescendingCellStyle BackColor="#E5E5E5" />
                    <SortedDescendingHeaderStyle BackColor="#242121" />
                </asp:GridView>
            </fieldset>
        </asp:Panel>
        <asp:Panel ID="pnlView_PreJoining" runat="server" Visible="true">
            <fieldset style="text-align: left; margin: 0px; padding: 2px; font-size: 11px;">
                <legend>Pre-Joining Experiences: </legend>
                <div id="dvGrid_PreJoiningExp">
                    <asp:GridView ID="GridView_PreJoiningExp" runat="server" DataKeyNames="ID" BackColor="White"
                        BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" GridLines="Horizontal"
                        Width="100%" Font-Size="11px" AutoGenerateColumns="False" DataSourceID="objDS_PreJoiningExp"
                        AllowSorting="True" ForeColor="Black">
                        <Columns>
                            <asp:TemplateField HeaderText="Vessel Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblVessel_Name" runat="server" Text='<%# Bind("Vessel_Name")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Flag">
                                <ItemTemplate>
                                    <asp:Label ID="lblFlag" runat="server" Text='<%# Bind("FLAG")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Vessel Type">
                                <ItemTemplate>
                                    <asp:Label ID="lblVessel_Type" runat="server" Text='<%# Bind("Vessel_Type")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="DWT">
                                <ItemTemplate>
                                    <asp:Label ID="lblDWT" runat="server" Text='<%# Bind("DWT")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="GRT">
                                <ItemTemplate>
                                    <asp:Label ID="lblGRT" runat="server" Text='<%# Bind("GRT")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="M/E Make/Model">
                                <ItemTemplate>
                                    <asp:Label ID="lblME_MakeModel" runat="server" Text='<%# Bind("ME_MakeModel")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="M/E BHP">
                                <ItemTemplate>
                                    <asp:Label ID="lblME_BHP" runat="server" Text='<%# Bind("ME_BHP")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Rank">
                                <ItemTemplate>
                                    <asp:Label ID="lblRank_Name" runat="server" Text='<%# Bind("Rank_Short_Name")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="From">
                                <ItemTemplate>
                                    <asp:Label ID="lblDate_From" runat="server" Text='<%# UDFLib.ConvertUserDateFormat(Convert.ToString(Eval("Date_From")))%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="To">
                                <ItemTemplate>
                                    <asp:Label ID="lblDate_To" runat="server" Text='<%# UDFLib.ConvertUserDateFormat( Convert.ToString(Eval("Date_To")))%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Month/Days">
                                <ItemTemplate>
                                    <asp:Label ID="lblDAYS" runat="server" Text='<%# Eval("MONTHS") + " M / " + Eval("DAYS") + " D"%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Company Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblCOMPANYNAME" runat="server" Text='<%# Bind("COMPANYNAME")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                        <HeaderStyle BackColor="#333333" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
                        <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                        <SortedAscendingCellStyle BackColor="#F7F7F7" />
                        <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
                        <SortedDescendingCellStyle BackColor="#E5E5E5" />
                        <SortedDescendingHeaderStyle BackColor="#242121" />
                    </asp:GridView>
                </div>
                <asp:ObjectDataSource ID="objDS_PreJoiningExp" runat="server" SelectMethod="Get_CrewPreJoiningExp"
                    TypeName="SMS.Business.Crew.BLL_Crew_CrewDetails" DeleteMethod="DEL_CrewPreJoiningExp"
                    InsertMethod="INS_CrewPreJoiningExp" UpdateMethod="UPDATE_CrewPreJoiningExp">
                    <DeleteParameters>
                        <asp:Parameter Name="ID" Type="Int32" />
                        <asp:SessionParameter Name="Deleted_By" SessionField="userid" Type="Int32" />
                    </DeleteParameters>
                    <InsertParameters>
                        <asp:Parameter Name="Vessel_Name" Type="String" />
                        <asp:Parameter Name="Flag" Type="String" />
                        <asp:Parameter Name="Vessel_Type" Type="String" />
                        <asp:Parameter Name="DWT" Type="String" />
                        <asp:Parameter Name="GRT" Type="String" />
                        <asp:Parameter Name="CompanyName" Type="String" />
                        <asp:Parameter Name="Rank" Type="Int32" />
                        <asp:Parameter Name="Date_From" Type="String" />
                        <asp:Parameter Name="Date_To" Type="String" />
                        <asp:SessionParameter Name="Created_By" SessionField="userid" Type="Int32" />
                        <asp:Parameter Name="ME_MakeModel" Type="String" />
                        <asp:Parameter Name="ME_BHP" Type="Int32" />
                    </InsertParameters>
                    <SelectParameters>
                        <asp:QueryStringParameter DefaultValue="" Name="ID" QueryStringField="ID" Type="Int32" />
                    </SelectParameters>
                    <UpdateParameters>
                        <asp:Parameter Name="ID" Type="Int32" />
                        <asp:Parameter Name="Vessel_Name" Type="String" />
                        <asp:Parameter Name="Flag" Type="String" />
                        <asp:Parameter Name="Vessel_Type" Type="String" />
                        <asp:Parameter Name="DWT" Type="String" />
                        <asp:Parameter Name="GRT" Type="String" />
                        <asp:Parameter Name="CompanyName" Type="String" />
                        <asp:Parameter Name="Rank" Type="Int32" />
                        <asp:Parameter Name="Date_From" Type="String" />
                        <asp:Parameter Name="Date_To" Type="String" />
                        <asp:SessionParameter Name="Modified_By" SessionField="userid" Type="Int32" />
                        <asp:Parameter Name="ME_MakeModel" Type="String" />
                        <asp:Parameter Name="ME_BHP" Type="Int32" />
                    </UpdateParameters>
                </asp:ObjectDataSource>
            </fieldset>
        </asp:Panel>
        <asp:Panel ID="pnlView_PreviousContacts" runat="server" Visible="true">
            <fieldset style="text-align: left; margin: 0px; padding: 2px; font-size: 11px;">
                <legend>Previous Operator's Contacts : </legend>
                <table>
                    <tr>
                        <td>
                            1.
                        </td>
                        <td>
                            Worked with Multinational Crew:
                        </td>
                        <td colspan="4" class="data">
                            <asp:Label ID="lblMultinationalcrew" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            Nationalities:
                        </td>
                        <td colspan="4" class="data">
                            <asp:Label ID="lblNationalities" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            2.
                        </td>
                        <td colspan="5">
                            Previous Operator's Contacts:
                        </td>
                    </tr>
                    <tr>
                        <td colspan="6">
                            <asp:GridView ID="GridView_PreviousContacts" runat="server" AutoGenerateColumns="False"
                                BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px"
                                CellPadding="2" Width="99%" EmptyDataText="" CaptionAlign="Bottom" GridLines="Horizontal"
                                DataKeyNames="ID" AllowPaging="True" PageSize="10" Font-Size="11px" AllowSorting="True"
                                ForeColor="Black">
                                <Columns>
                                    <asp:TemplateField HeaderText="Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblName" runat="server" Text='<%# Eval("name")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="200px" HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="PIC">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPIC" runat="server" Text='<%# Eval("pic")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Telephone">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTele" runat="server" Text='<%# Eval("Telephone")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="100px" HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Fax">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFax" runat="server" Text='<%# Eval("Fax")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="80px" HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="E-Mail">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFax" runat="server" Text='<%# Eval("Email")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="80px" HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                </Columns>
                                <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                                <HeaderStyle BackColor="#333333" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                <SelectedRowStyle BackColor="#BFCFDF" Font-Bold="True" ForeColor="Black" />
                                <SortedAscendingCellStyle BackColor="#F7F7F7" />
                                <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
                                <SortedDescendingCellStyle BackColor="#E5E5E5" />
                                <SortedDescendingHeaderStyle BackColor="#242121" />
                                <PagerStyle Font-Size="Larger" CssClass="pager" BackColor="White" ForeColor="Black"
                                    HorizontalAlign="Right" />
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </fieldset>
        </asp:Panel>
        <asp:Panel ID="pnlView_Voyages" runat="server">
            <fieldset style="text-align: left; margin: 0px; padding: 2px; font-size: 11px;">
                <legend>Voyages: </legend>
                <div id="dvGrid_Voyages">
                    <asp:GridView ID="GridView_Voyages" runat="server" DataKeyNames="ID" BackColor="White"
                        BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" GridLines="Horizontal"
                        Width="100%" Font-Size="11px" AutoGenerateColumns="False" DataSourceID="objDS_CrewVoyages"
                        AllowSorting="True" ForeColor="Black">
                        <Columns>
                            <asp:TemplateField HeaderText="Vessel">
                                <ItemTemplate>
                                    <asp:Label ID="lblVesselName" runat="server" Text='<%# Eval("VESSEL_SHORT_NAME") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Joining Rank">
                                <ItemTemplate>
                                    <asp:Label ID="lblJoiningRank" runat="server" Text='<%# Eval("Rank_Name") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Joining Date">
                                <ItemTemplate>
                                    <asp:Label ID="lblJoiningDate" runat="server" Text='<%#UDFLib.ConvertUserDateFormat(Convert.ToString(Eval("Joining_Date"))) %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Sign On Date">
                                <ItemTemplate>
                                    <asp:Label ID="lblSignOnDate" runat="server" Text='<%#UDFLib.ConvertUserDateFormat(Convert.ToString(Eval("Sign_On_Date"))) %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Sign On Port">
                                <ItemTemplate>
                                    <asp:Label ID="lblJoiningPort" runat="server" Text='<%# Eval("PORT_NAME") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="EOC Date">
                                <ItemTemplate>
                                    <asp:Label ID="lnkCOCDate" runat="server" Text='<%#UDFLib.ConvertUserDateFormat(Convert.ToString(Eval("COCDate"))) %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Sign Off Date">
                                <ItemTemplate>
                                    <asp:Label ID="lblSignOffDate" runat="server" Text='<%#UDFLib.ConvertUserDateFormat(Convert.ToString(Eval("Sign_Off_Date"))) %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Sign Off Port">
                                <ItemTemplate>
                                    <asp:Label ID="lblSignOffPort" runat="server" Text='<%# Eval("Sign_Off_Port_name") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Sign Off Reason">
                                <ItemTemplate>
                                    <asp:Label ID="lblSignOffReason" runat="server" Text='<%# Eval("Reason") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                        <HeaderStyle BackColor="#333333" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
                        <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                        <SortedAscendingCellStyle BackColor="#F7F7F7" />
                        <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
                        <SortedDescendingCellStyle BackColor="#E5E5E5" />
                        <SortedDescendingHeaderStyle BackColor="#242121" />
                    </asp:GridView>
                </div>
                <asp:ObjectDataSource ID="objDS_CrewVoyages" runat="server" SelectMethod="Get_CrewVoyages"
                    TypeName="SMS.Business.Crew.BLL_Crew_CrewDetails" DeleteMethod="DEL_CrewVoyages"
                    UpdateMethod="UPDATE_CrewVoyages">
                    <SelectParameters>
                        <asp:QueryStringParameter DefaultValue="" Name="ID" QueryStringField="ID" Type="Int32" />
                    </SelectParameters>
                </asp:ObjectDataSource>
            </fieldset>
            <div style="text-align: left; font-size: 11px;">
                <p style="font-weight: bold">
                    Crew Change Event Planned:</p>
                <table cellspacing="1" cellpadding="4" style="border: 1px solid #610B5E; width: 100%">
                    <tr style="background-color: #610B5E; color: White">
                        <th style="width: 100px">
                            Vessel
                        </th>
                        <th style="width: 100px">
                            Port
                        </th>
                        <th style="width: 100px">
                            Event Date
                        </th>
                        <th style="width: 60px">
                            Staff Code
                        </th>
                        <th style="width: 300px">
                            Staff Name
                        </th>
                        <th style="width: 60px">
                            Rank
                        </th>
                        <th style="width: 60px">
                            ON / OFF
                        </th>
                        <th style="width: 60px">
                            Additional
                        </th>
                    </tr>
                    <asp:Repeater runat="server" ID="rpt1">
                        <ItemTemplate>
                            <tr style="background-color: #D8D8D8; color: Black; font-weight: bold;">
                                <td>
                                    <%# ((System.Data.DataRowView)Container.DataItem)["Vessel_Short_Name"]%>
                                </td>
                                <td>
                                    <%# ((System.Data.DataRowView)Container.DataItem)["Port_Name"]%>
                                </td>
                                <td>
                                    <%# ((System.Data.DataRowView)Container.DataItem)["Event_Date"]%>
                                </td>
                                <td colspan="5">
                                </td>
                            </tr>
                            <asp:Repeater runat="server" ID="rpt2" DataSource='<%# ((System.Data.DataRowView) Container.DataItem).Row.GetChildRows("EventMembers") %>'>
                                <ItemTemplate>
                                    <tr style="background-color: #CEF6F5; color: Black">
                                        <td colspan="3">
                                        </td>
                                        <td>
                                            <a href='CrewDetails.aspx?ID=<%# ((System.Data.DataRow) Container.DataItem)["CrewID"] %>'
                                                target="_blank">
                                                <%# ((System.Data.DataRow) Container.DataItem)["Staff_Code"] %></a>
                                        </td>
                                        <td>
                                            <%# ((System.Data.DataRow) Container.DataItem)["Staff_Name"] %>
                                        </td>
                                        <td>
                                            <%# ((System.Data.DataRow)Container.DataItem)["Rank_Short_Name"]%>
                                        </td>
                                        <td>
                                            <%# ((System.Data.DataRow) Container.DataItem)["ON_OFF"] %>
                                        </td>
                                        <td>
                                            <%# ((System.Data.DataRow) Container.DataItem)["ADDITIONAL"] %>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
            </div>
        </asp:Panel>
    </div>
</asp:Content>
