<%@ Page Title="Category Rating" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="CategoryRating.aspx.cs" Inherits="Technical_Worklist_CategoryRating" EnableEventValidation="false"    %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="../../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/jquery.min1.8.js" type="text/javascript"></script>
    <script src="../../Scripts/Common_Functions.js" type="text/javascript"></script>
    <script src="../../Scripts/ModalPopUp.js" type="text/javascript"></script>
    <link href="../../Styles/crew_css.css" rel="stylesheet" type="text/css" />
   <%-- <script src="../../Scripts/SMSPopup.js" type="text/javascript"></script>--%>
   <%-- <link href="../../Styles/SMSPopup.css" rel="stylesheet" type="text/css" />--%>
    <script src="../../Scripts/boxover.js" type="text/javascript"></script>
    <style type="text/css">
      .txtbox
      {
          white-space:wrap;
          
      }
      
        .MultiLineTextbox
        {
            overflow:hidden;
            font-family:Tahoma;
             font-size: 12px;
        }
        .badge1
        {
             position: relative;
             text-decoration:none;
        }
        .badge1[data-badge]:after
        {
            content: attr(data-badge);
            position:absolute;
            top: -12px;
            right: -10px;
            font-size: 12px;
           font-family:Tahoma;
            background: green;
            color: white;
            width: 25px;
            height: 25px;
            text-align: center;
            text-decoration:none;
            line-height: 25px;
            border-radius: 50%;
           
        }
           .badgeFirfox
        {
             position: relative;
             text-decoration:none;
        }
          .badgeFirfox[data-badge]:after
        {
            content: attr(data-badge);
            position:absolute;
            top: 0px;
            right: -10px;
           font-size: 12px;
          font-family:Tahoma ;
            background: green;
            color: white;
            width: 25px;
            height: 25px;
            text-align: center;
            text-decoration:none;
            line-height: 25px;
            border-radius: 50%;
           
        }
        
            .badgeSafari
        {
             position: relative;
             text-decoration:none;
        }
          .badgeSafari[data-badge]:after
        {
            content: attr(data-badge);
            position:absolute;
            top: -12px;
            right: -50px;
           font-size: 12px;
            font-family:Tahoma;
            background: green;
            color: white;
            width: 25px;
            height: 25px;
            text-align: center;
            text-decoration:none;
            line-height: 25px;
            border-radius: 50%;
           
        }
             .badgeChrome
        {
             position: relative;
             text-decoration:none;
        }
          .badgeChrome[data-badge]:after
        {
            content: attr(data-badge);
            position:absolute;
            top: 8px;
            right: -55px;
           font-size: 12px;
           font-family:Tahoma;
            background: green;
            color: white;
            width: 25px;
            height: 25px;
            text-align: center;
            text-decoration:none;
            line-height: 25px;
            border-radius: 50%;
           
        }
        .page
        {
            width: 1700px;
            background-color: #fff;
            margin: 5px auto 0px auto;
            border: 1px solid #496077;
        }
        .Newdropdown-css
        {
            font-size: 12px;
            font-family: Tahoma;
            background-color: White;
            text-align: center;
            font-family:Tahoma;
        }
        .LinkButton
        {
            text-decoration: none;
        }
        .LinkButton:hover
        {
            text-decoration: underline;
        }
        .Rating-FooterStyle-css
        {
            /* background: url(../../Images/gridheaderbg-silver-image.png) left -0px repeat-x;*/
            color: #333333;
            background-color: #fff;
            font-weight: bold;
            font-size: 12px;
            border: 1px solid #000;
            font-family:Tahoma;
        }
        .Rating-FooterStyle-css td
        {
            border: 1px solid #000;
            border-collapse: collapse;
        }
        .Rating-HeaderStyle-css
        {
            /* background: url(../Images/gridheaderbg-silver-image.png) left -0px repeat-x; /*background: url(../Images/gradient-blue.png) left -500px repeat-x;*/
            color: #333333;
            font-size: 12px;
            padding: 5px;
            text-align: center;
            vertical-align: middle;
            background-color: #CCCCCC;
            border: 1px solid #000;
            border-collapse: collapse;
            font-family:Tahoma;
        }
        .Rating-HeaderStyle-css th
        {
            border: 1px solid #000;
            border-collapse: collapse;
        }
          .PaddingCellCss
          {
              white-space:normal;
              max-width:175px;
          }
    </style>
    <script type="text/javascript" type="text/javascript">

        var _txtRemark = null;
        function onAddRemark(txtbox) {

            document.getElementById($('[id$=txtPopupRemark]').attr('id')).value = "";
            _txtRemark = txtbox;
            txtbox.style.background = "yellow";
            txtbox.disabled = true;
            document.getElementById($('[id$=txtPopupRemark]').attr('id')).value = _txtRemark.value;
            showModal('dvRemark', true, onSaveRemark,null);
            $("#dvRemark").prop('title', 'Add Remark');
            
            //alert("Click");
        }
        function onSaveRemark() {
            var txt = document.getElementById(_txtRemark.id);
            txt.value = document.getElementById($('[id$=txtPopupRemark]').attr('id')).value;
            txt.innerText = document.getElementById($('[id$=txtPopupRemark]').attr('id')).value;
            txt.innerHtml = document.getElementById($('[id$=txtPopupRemark]').attr('id')).value;
            txt.style.background = "white";

            if (txt.value != "") {

                document.getElementById("ctl00_MainContent_hdnDirtyCounter").value = "1";
            }
           txt.disabled = false;
           hideModal('dvRemark');
           return false;
        }
        function UpdatePage() {

           // alert("In Parent");
            hideModal("dvJob");

             //document.getElementById(JobCountControlID).setAttribute("data-badge",JobCount);

            __doPostBack('<%=BtnTemp.UniqueID %>', 'onclick');

        }
        function onLoad() {


            var isOpera = !!window.opera || navigator.userAgent.indexOf(' OPR/') >= 0;
            // Opera 8.0+ (UA detection to detect Blink/v8-powered Opera)
            var isFirefox = typeof InstallTrigger !== 'undefined';   // Firefox 1.0+
            var isSafari = Object.prototype.toString.call(window.HTMLElement).indexOf('Constructor') > 0;
            // At least Safari 3+: "[object HTMLElementConstructor]"
            var isChrome = !!window.chrome && !isOpera;              // Chrome 1+
            var isIE = /*@cc_on!@*/false || !!document.documentMode;   // At least IE6

//            var output = 'Detecting browsers by ducktyping:<hr>';
//            output += 'isFirefox: ' + isFirefox + '<br>';
//            output += 'isChrome: ' + isChrome + '<br>';
//            output += 'isSafari: ' + isSafari + '<br>';
//            output += 'isOpera: ' + isOpera + '<br>';
//            output += 'isIE: ' + isIE + '<br>';
//            document.body.innerHTML = output;

            if (isFirefox == true) {
                var cont = document.getElementsByClassName("badge1");
                var len = cont.length;
                var i=0;
                for (i = 0; i < cont.length; i++) {
                  
                    cont[i].className = "badgeFirfox";

                   // alert(cont[i].value);

                    cont = document.getElementsByClassName("badge1");
                    if (cont.length == 0) {
                        break;
                    }
                    else {
                        i = -1;
                    }
                }

            }
            if (isSafari == true) {
                var cont = document.getElementsByClassName("badge1");
                var len = cont.length;
                var i = 0;
                for (i = 0; i < cont.length; i++) {

                    cont[i].className = "badgeSafari";

                   // alert(cont[i].value);

                    cont = document.getElementsByClassName("badge1");
                    if (cont.length == 0) {
                        break;
                    }
                    else {
                        i = -1;
                    }
                }

            }
            if (isChrome == true) {
                var cont = document.getElementsByClassName("badge1");
                var len = cont.length;
                var i = 0;
                for (i = 0; i < cont.length; i++) {

                    cont[i].className = "badgeChrome";

                  //  alert(cont[i].value);

                    cont = document.getElementsByClassName("badge1");
                    if (cont.length == 0) {
                        break;
                    }
                    else {
                        i = -1;
                    }
                }

            }
            if (isOpera == true) {
                var cont = document.getElementsByClassName("badge1");
                var len = cont.length;
                var i = 0;
                for (i = 0; i < cont.length; i++) {

                    cont[i].className = "badgeChrome";

                   // alert(cont[i].value);

                    cont = document.getElementsByClassName("badge1");
                    if (cont.length == 0) {
                        break;
                    }
                    else {
                        i = -1;
                    }
                }

            }

        }
        function showDialog(url) {
            window.open(url);
        }
        function AddDefect(LocationID, InspID, VesselID) {

           
            document.getElementById('IframeJob').src = 'AddConditionReport.aspx?LocationID=' + LocationID + '&InspID=' + InspID + '&VesselID=' + VesselID + '&LocationNodeID=-1';

            showModal('dvJob');
            $("#dvJob").prop('title', 'Worklist/OT03');
            return false;
        }
        function showWorklist() {

            showModal('dvWorklist');
            $("#dvWorklist").prop('title', 'Worklist');

            return false;
        }
        function Confirm() {

            // alert(document.getElementById("ctl00_MainContent_hdnDirtyCounter").value);
            if (parseInt(document.getElementById("ctl00_MainContent_hdnDirtyCounter").value) > 0) {
                if (confirm("Do you want to save changes?")) {
                    document.getElementById("ctl00_MainContent_hdnConfirm").value = "Yes";
                } else {

                    document.getElementById("ctl00_MainContent_hdnConfirm").value = "No";
                }
            }


        }


        function Check_Click(objRef) {
            //Get the Row based on checkbox
            var row = objRef.parentNode.parentNode;


            //Get the reference of GridView
            var GridView = row.parentNode;

            //Get all input elements in Gridview
            var inputList = GridView.getElementsByTagName("input");

            for (var i = 0; i < inputList.length; i++) {
                //The First element is the Header Checkbox
                var headerCheckBox = inputList[0];

                //Based on all or none checkboxes
                //are checked check/uncheck Header Checkbox
                var checked = true;
                if (inputList[i].type == "checkbox" && inputList[i] != headerCheckBox) {
                    if (!inputList[i].checked) {
                        checked = false;
                        break;
                    }
                }
            }
            headerCheckBox.checked = checked;

        }
        function checkAll(objRef) {
            var GridView = objRef.parentNode.parentNode.parentNode;
            var inputList = GridView.getElementsByTagName("input");
            for (var i = 0; i < inputList.length; i++) {
                //Get the Cell To find out ColumnIndex
                var row = inputList[i].parentNode.parentNode;
                if (inputList[i].type == "checkbox" && objRef != inputList[i]) {


                    if (objRef.checked) {
                        //If the header checkbox is checked
                        //check all checkboxes
                        //and highlight all rows
                        if (inputList[i].disabled == false) {
                            inputList[i].checked = true;
                        }
                    }
                    else {
                        //If the header checkbox is checked
                        //uncheck all checkboxes
                        //and change rowcolor back to original

                        inputList[i].checked = false;
                    }
                }
            }
        }
        function GetRating(event, th) {


            //alert(user.browser.family + "," + user.browser.version + "," + user.os.name);
            var rowIndex = th.offsetParent.parentNode.rowIndex;
            var cellIndex = th.offsetParent.cellIndex;
            var ddlRating = $('[id$=grdSubCatRating]').find(th).val();
            //      alert(ddlRating);
            var grd = document.getElementById($('[id$=grdSubCatRating]').attr('id'));

            var lblRate = $(th).closest("tr").find('[id$=lblSubCatRating]');
            var lblRateControl = document.getElementById(lblRate.attr('id'));
            //      alert(lblRate.attr('id'));
            //    alert(lblRate.attr('text'));

            var Rate = ddlRating.toString();
            //   alert(Rate.split('_')[0]);
            lblRateControl.innerText = Rate.split('_')[0];
            lblRateControl.textContent = Rate.split('_')[0];
            document.getElementById("ctl00_MainContent_hdnDirtyCounter").value = 1;
            //  alert(lblRateControl.innerText)
            //  lblRateControl.innerText = ddlRating.toString().;
            //lblRate.attr('text', ddlRating);
            //  alert(lblRateControl.innerText);



        }

        
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div id="page-content" style="border: 1px solid #cccccc; z-index: -2;white-space:nowrap;">
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
        <div style="border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px; height: 100%;">
            <div id="page-header" class="page-title">
                <b>Categories Rating</b>
            </div>
            <div>
            
                <table cellpadding="5">
                    <tr>
                        <td>
                      
                            <asp:Label ID="Label1" runat="server" Text="Vessel Name" Font-Bold="true"></asp:Label>
                            :
                        </td>
                        <td>
                            <asp:Label ID="lblVesselName" runat="server" Text="" Font-Bold="true"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="Label3" runat="server" Text="Inspection Type" Font-Bold="true"></asp:Label>
                            :
                        </td>
                        <td colspan="2">
                            <asp:Label ID="lblInspType" runat="server" Text="" Font-Bold="true"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="Label6" runat="server" Text="Inspector Name" Font-Bold="true"></asp:Label>
                            :
                        </td>
                        <td>
                            <asp:Label ID="lblInspectorName" runat="server" Text="" Font-Bold="true"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="vertical-align: top;">
                            <asp:Label ID="Label2" runat="server" Text="Date of Visit" Font-Bold="true"></asp:Label>
                            :
                        </td>
                        <td>
                            <asp:Label ID="Label4" runat="server" Text="From" Font-Bold="true"></asp:Label>
                            :
                        </td>
                        <td>
                            <asp:Label ID="lblFrom" runat="server" Text="" Font-Bold="true"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="Label5" runat="server" Text="To" Font-Bold="true"></asp:Label>
                            :
                        </td>
                        <td>
                            <asp:Label ID="lblTo" runat="server" Text="" Font-Bold="true"></asp:Label>
                        </td>
                    </tr>
                </table>
               
            </div>
            <table style="" border="1">
                <tr>
                    <td style="width: 680px; vertical-align: top;">
                        <asp:Panel ID="pnlCategory" runat="server">
                            <asp:UpdatePanel ID="updCat" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <div id="dvCategory">
                                        <style type="text/css">
                                            .Rating-HeaderStyle-css
                                            {
                                                /* background: url(../Images/gridheaderbg-silver-image.png) left -0px repeat-x; /*background: url(../Images/gradient-blue.png) left -500px repeat-x;*/
                                                color: #333333;
                                                font-size: 12px;
                                                padding: 5px;
                                                text-align: center;
                                                vertical-align: middle;
                                                background-color: #CCCCCC;
                                                border: 1px solid #000;
                                                border-collapse: collapse;
                                                font-family: Tahoma;
                                            }
                                        </style>
                                        <asp:GridView ID="grdCatRating" GridLines="Vertical" CellPadding="8" runat="server"
                                            ShowFooter="True" AutoGenerateColumns="false" OnRowCommand="grdCatRating_RowCommand"
                                            BorderWidth="1px"   OnRowCreated="grdCatRating_RowCreated" OnRowDataBound="grdCatRating_RowDataBound" >
                                            <HeaderStyle CssClass="Rating-HeaderStyle-css" Font-Size="12px" Font-Names="Tahoma" />
                                            <FooterStyle CssClass="Rating-FooterStyle-css" Font-Size="12px" Font-Names="Tahoma" />
                                            <RowStyle CssClass="RowStyle-css" BorderStyle="Solid" BorderColor="#000" BorderWidth="1px" Font-Size="12px" Font-Names="Tahoma" />
                                            <AlternatingRowStyle CssClass="AlternatingRowStyle-css" Font-Size="12px" Font-Names="Tahoma" />
                                            <SelectedRowStyle BackColor="#FFFFCC" />
                                            <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                                            <Columns>
                                                <asp:BoundField HeaderText="S.No." DataField="RNo" ItemStyle-HorizontalAlign="Center"
                                                    ItemStyle-Width="50px" HeaderStyle-HorizontalAlign="Center" />
                                                <asp:BoundField HeaderText="Code" DataField="Code" Visible="false" />
                                                <asp:BoundField HeaderText="Section Description" DataField="Description" ItemStyle-Width="200px" />
                                                <asp:BoundField HeaderText="Last Report" DataField="LastReport" ItemStyle-HorizontalAlign="Center"
                                                    ItemStyle-Width="60px" />
                                                <asp:BoundField HeaderText="This Report" DataField="ThisReport" ItemStyle-HorizontalAlign="Center"
                                                    ItemStyle-Width="60px" />
                                                <%-- <asp:BoundField HeaderText="Rating" DataField="Rating" ItemStyle-HorizontalAlign="Center"  />--%>
                                                <asp:TemplateField HeaderText="Ratings">
                                                   
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCatRating" runat="server" Text='<%# Bind("Rating") %>' Width="60px"></asp:Label>
                                                        <%--ToolTip='<%# Eval("ThisReport").ToString()==""? "": Convert.ToInt32(Eval("ThisReport")) >5 ? "Category average is above 5":"" %>'--%>
                                                    </ItemTemplate>
                                                    <ItemStyle Wrap="true" HorizontalAlign="Center" CssClass="PaddingCellCss" Width="60px">
                                                    </ItemStyle>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Remark">
                                                  
                                                    <ItemTemplate>
                                                       <%-- <asp:BoundField HeaderText="Remark" DataField="AddRemark" ItemStyle-HorizontalAlign="Center"
                                                            Visible="true" ItemStyle-Width="200px"  />--%>
                                                             <div style=" white-space:normal; color:Black;text-align:left ">
                                                             <asp:Label ID="lblRemark" runat="server" Text='<%# Bind("AddRemark") %>'></asp:Label>
                                                              </div>
                                                       </ItemTemplate>
                                                     <ItemStyle Wrap="true" HorizontalAlign="Center" CssClass="PaddingCellCss" Width="320px">
                                                    </ItemStyle>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="SubCategory">
                                                    <HeaderTemplate>
                                                        Sub Category
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                      <%--  <asp:LinkButton ID="lnkViewSubCategory" runat="server" ToolTip="View Subcategories"
                                                            Font-Size="12px" ForeColor="GrayText" Font-Bold="true" CssClass="LinkButton"
                                                            OnClientClick="Confirm();" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' >View</asp:LinkButton>--%>
                                                        <asp:ImageButton ID="ImgViewSubCat" runat="server" ToolTip="View Subcategories"
                                                            Font-Size="12px" ForeColor="GrayText" Font-Bold="true" CssClass="LinkButton"
                                                            OnClientClick="Confirm();" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' ImageUrl="~/Images/SubCat.png" Width="20px" Height="20px" />
                                                        <asp:HiddenField ID="hdnCatRNo" runat="server" Value='<%# Bind("RNo") %>' />
                                                        <asp:HiddenField ID="hdnCatCode" runat="server" Value='<%# Bind("Code") %>' />
                                                        <asp:HiddenField ID="hdnCatDesc" runat="server" Value='<%# Bind("Description") %>' />
                                                        <asp:HiddenField ID="hdnAddRemaks" runat="server" Value='<%# Bind("AddRemark") %>' />
                                                    </ItemTemplate>
                                                    <ItemStyle Wrap="true" HorizontalAlign="Center" CssClass="PaddingCellCss"></ItemStyle>
                                                </asp:TemplateField>
                                            </Columns>
                                            <EmptyDataTemplate>
                                                No Records Found
                                            </EmptyDataTemplate>
                                        </asp:GridView>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </asp:Panel>
                    </td>
                    <td style="width: 1200px; vertical-align: top;">
                        <asp:UpdatePanel ID="updSubCat" runat="server">
                            <ContentTemplate>
                             <asp:Button ID="BtnTemp" runat="server" Text="Button" onclick="BtnTemp_Click" 
                    Visible="False" />
                                <asp:Panel ID="pnlSubcategory" runat="server">
                                    <div id="dvSubCategory" style="overflow-y: scroll; height: 660px;">
                                      <style type="text/css">
                                            .Rating-HeaderStyle-css
                                            {
                                                /* background: url(../Images/gridheaderbg-silver-image.png) left -0px repeat-x; /*background: url(../Images/gradient-blue.png) left -500px repeat-x;*/
                                                color: #333333;
                                                font-size: 12px;
                                                padding: 5px;
                                                text-align: center;
                                                vertical-align: middle;
                                                background-color: #CCCCCC;
                                                border: 1px solid #000;
                                                border-collapse: collapse;
                                                font-family: Tahoma;
                                            }
                                        </style>
                                        <div id="dvSubCategoryInner">
                                            <asp:GridView ID="grdSubCatRating" GridLines="Vertical" CellPadding="8" runat="server"
                                                ShowFooter="true" AutoGenerateColumns="False" OnRowDataBound="grdSubCatRating_RowDataBound"
                                                BorderWidth="1px" OnRowCommand="grdSubCatRating_RowCommand" OnRowCreated="grdSubCatRating_RowCreated">
                                                <HeaderStyle CssClass="Rating-HeaderStyle-css" Font-Size="12px" Font-Names="Tahoma"  />
                                                <FooterStyle CssClass="Rating-FooterStyle-css" Font-Size="12px" Font-Names="Tahoma"/>
                                                <RowStyle CssClass="RowStyle-css" BorderStyle="Solid" BorderColor="#000" BorderWidth="1px" Font-Size="12px" Font-Names="Tahoma" />
                                                <AlternatingRowStyle CssClass="AlternatingRowStyle-css" Font-Size="12px" Font-Names="Tahoma"/>
                                                <SelectedRowStyle BackColor="#FFFFCC" />
                                                <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                                                <Columns>
                                                    <asp:BoundField HeaderText="Code" DataField="Code" Visible="false" />
                                                    <asp:TemplateField HeaderText="">
                                                        <HeaderTemplate>
                                                            <asp:Label ID="lblCatNo" runat="server" Text=""></asp:Label>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSubCatNo" runat="server" Text='<%# Bind("RNo") %>'></asp:Label>
                                                            <asp:HiddenField ID="hdnSubCatCode" runat="server" Value='<%# Bind("Code") %>' />
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="true" HorizontalAlign="Center" CssClass="PaddingCellCss" Width="50px">
                                                        </ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="">
                                                        <HeaderTemplate>
                                                            <asp:Label ID="lblCatDesc" runat="server" Text="" Width="120px"  CssClass="PaddingCellCss"></asp:Label>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSubCatDesc" runat="server" Text='<%# Bind("Description") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="true" HorizontalAlign="Left" CssClass="PaddingCellCss" Width="120px">
                                                        </ItemStyle>
                                                        <HeaderStyle  HorizontalAlign="Left" CssClass="PaddingCellCss" Width="120px">
                                                        </HeaderStyle>
                                                    </asp:TemplateField>
                                                    <asp:BoundField HeaderText="2nd Last Report" DataField="SecLastReport" ItemStyle-HorizontalAlign="Center"
                                                        ItemStyle-Width="60px">
                                                        <ItemStyle HorizontalAlign="Center" Width="60px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField HeaderText="Last Report" DataField="LastReport" ItemStyle-HorizontalAlign="Center"
                                                        ItemStyle-Width="60px">
                                                        <ItemStyle HorizontalAlign="Center" Width="60px" />
                                                    </asp:BoundField>
                                                    <asp:TemplateField HeaderText="This Report">
                                                       
                                                        <ItemTemplate>
                                                            <asp:HiddenField ID="hdnThisReport" runat="server" Value='<%# Bind("ThisReport") %>' />
                                                            <asp:DropDownList ID="ddlRating" runat="server" CssClass="Newdropdown-css" AutoPostBack="True"
                                                                onchange="return GetRating(event,this);" ToolTip="Select Rating" Width="60px">
                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="false" HorizontalAlign="Center" Width="60px" CssClass="PaddingCellCss">
                                                        </ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Ratings">
                                                    
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSubCatRating" runat="server" Text='<%# Bind("Rating") %>' Width="60px"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="true" HorizontalAlign="Center" CssClass="PaddingCellCss" Width="60px">
                                                        </ItemStyle>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Remarks">
                                                       
                                                        <ItemTemplate>
                                                         <div style=" white-space:normal; color:Black;text-align:left; ">
                                                            <asp:TextBox ID="txtRemarks" runat="server" Text='<%# Bind("Remarks") %>' CssClass="MultiLineTextbox" Width="170px"  Height="15px" TextMode="MultiLine"  onclick='<%# String.Format("return onAddRemark({0})", (((GridViewRow)Container).FindControl("txtRemarks") as TextBox).ClientID) %>'></asp:TextBox>
                                                            <%-- <asp:ImageButton ID="imgAddRemark" runat="server" ImageUrl="../../Images/add.GIF" height="10px" OnClick='<%# String.Format("return onAddRemark({0})", (((GridViewRow)Container).FindControl("txtRemarks") as TextBox).ClientID) %>'/>--%>
                                                            <asp:Label ID="lblRemarks" runat="server" Text='<%# Bind("Remarks") %>'  Width="330px"
                                                                Visible="false"  ></asp:Label>
                                                                </div>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="true" HorizontalAlign="Justify" CssClass="PaddingCellCss" Width="330px"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <HeaderTemplate>
                                                            <asp:Label ID="lblAttach" runat="server">Jobs Attached</asp:Label>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <%-- <asp:Label ID="lblJobCount" runat="server" CssClass="badge1"></asp:Label>--%>
                                                            <asp:LinkButton ID="lnkAssignChecklist" runat="server" CommandName="Assign" CommandArgument='<%#Eval("Code") %>'
                                                                CssClass="badge1"  ></asp:LinkButton>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="70px" CssClass="PaddingCellCss">
                                                        </ItemStyle>
                                                        <HeaderStyle Wrap="false" HorizontalAlign="Left" CssClass="PaddingCellHCss" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <HeaderTemplate>
                                                            <asp:Label ID="lblDelete" runat="server">Action</asp:Label>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            &nbsp;&nbsp;
                                                            <%-- <asp:HyperLink ID="HyperLink2" runat="server" ImageUrl="../../Images/add.GIF"
                                                            NavigateUrl='<%#"AddNewJob.aspx?LocationID="+Eval("Code")+"&InspID="+Request.QueryString["InspID"]+"&VesselID="+Request.QueryString["VesselID"]+"&OFFID=0&WLID=0&VID=0"%>' Target="_blank" ToolTip='Add Job' Visible="true"></asp:HyperLink>
                                                            --%>
                                                            <asp:ImageButton ID="imgCondRpt" runat="server" ImageUrl="../../Images/add.GIF" CommandName="AddDefect"  CommandArgument='<%#  Eval("Code") + ","+Request.QueryString["InspID"].ToString()+","+Request.QueryString["VesselID"].ToString()  %>' />
                                                            
                                                       
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="true" HorizontalAlign="Justify" Width="70px" CssClass="PaddingCellCss">
                                                        </ItemStyle>
                                                        <HeaderStyle Wrap="false" HorizontalAlign="Left" CssClass="PaddingCellHCss" />
                                                    </asp:TemplateField>
                                                </Columns>
                                                <EmptyDataTemplate>
                                                    No Records Found
                                                </EmptyDataTemplate>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>
                                            <asp:Panel ID="pnlAddRemark" runat="server" Visible="false">
                                                <table style="width: 100%;">
                                                    <tr>
                                                        <th>
                                                            Additional Remarks
                                                        </th>
                                                        <tr>
                                                            <td style="white-space:pre-wrap;">
                                                                <asp:TextBox ID="txtAddRemarks" runat="server" Width="100%" Height="100px" 
                                                                    TextMode="MultiLine" Font-Names="Tahoma" Font-Size="12px"  ></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Button ID="BtnSaveRating" runat="server" Text="Save Rating" OnClick="BtnSaveRating_Click"
                                                                    ToolTip="Save Rating" />
                                                            </td>
                                                        </tr>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </asp:Panel>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </table>
            <div id="dvWorklist" style="display: none; height: 520px;" title="Inspection Scheduling">
                <asp:UpdatePanel ID="UpdatePanel3" runat="server" >
                    <ContentTemplate>
                        <div id="dx" style="display: none">
                        </div>
                        <div id="dy" style="display: none">
                        </div>
                        <script type="text/javascript">
                            $(document).on("mousemove", function (event) {

                                $("#dx").text(event.pageX);
                                $("#dy").text(event.pageY);

                            });


                            function showFollowups(V, W, O, M) {

                                var evt = window.event || M; // this assign evt with the event object
                                var src = evt.srcElement; // this assign current with the event target
                                var pos = 0;
                                var width = 0;
                                var x = 0;
                                var y = 0;
                                var min = 0;
                                if (src == null) {
                                    src = evt.target;
                                    x = evt.x;
                                    y = evt.y;
                                    min = 210;
                                }
                                else {
                                    pos = $(src).offset();
                                    width = $(src).width();
                                    x = pos.left;
                                    y = pos.top;
                                    min = 120;
                                }
                                // var src = window.event.srcElement;
                                x = $("#dx").text();
                                y = $("#dy").text();


                                var url = 'Task_Followups.aspx?WLID=' + W + '&VID=' + V + '&OFFID=' + O;

                                $('#iframeFollowups').load(function () { this.style.height = (this.contentWindow.document.body.offsetHeight + 20) + 'px'; });
                                $('#iframeFollowups').attr("src", url);
                                $('#dialog').show();
                                $("#dialog").css({ "left": (x - 600) + "px", "top": (y - min) + "px", "width": 600 });


                            }
                        </script>
                        <div id="dialog" title="Follow-ups" style="top: 0px; left: 0px; width: 600px; display: none;
                            position: absolute;">
                            <iframe id="iframeFollowups" style="width: 100%; height: 100%; border: 0px;"></iframe>
                        </div>
                        <input type="hidden" runat="server" id="hdnFlagCheck" value="false" />
                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td style="width: 100%">
                                    <div style="width: 100%; overflow: auto; text-align: left; height: 450px;">
                                        <div id="Div2" style="border: 1px solid #aabbdd; background-color: #efefef; padding: 8px;
                                            margin-top: 5px; margin-bottom: 2px;">
                                            <%--<div style="float: right; position: relative; font-weight: normal;">
                                                <asp:Image ID="imgFlag" runat="server" ImageUrl="~/Images/Flag_ON.png" ImageAlign="AbsMiddle" />Flagged
                                                for Technical Meeting</div>--%>
                                            <div>
                                                <table cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td style="font-weight: bold; padding-right: 20px;">
                                                            Total Jobs:&nbsp;&nbsp;<asp:Label ID="lblRecordCount" runat="server" Text="0"></asp:Label>
                                                        </td>
                                                        <td style="font-weight: normal; padding-right: 10px;">
                                                            Priority:
                                                        </td>
                                                        <td>
                                                            <div style="height: 6px; width: 6px; background-color: Red;" title="Priority: URGENT">
                                                            </div>
                                                        </td>
                                                        <td style="font-weight: bold; padding-right: 20px;">
                                                            &nbsp;&nbsp;Urgent
                                                        </td>
                                                        <td>
                                                            <div style="height: 6px; width: 6px; background-color: Yellow;" title="Priority: URGENT">
                                                            </div>
                                                        </td>
                                                        <td style="font-weight: bold; padding-right: 20px;">
                                                            &nbsp;&nbsp;High
                                                        </td>
                                                        <td>
                                                            <div style="height: 6px; width: 6px; background-color: transparent;" title="Priority: URGENT">
                                                            </div>
                                                        </td>
                                                        <td style="font-weight: bold; padding-right: 20px;">
                                                            JOB Status Filter:&nbsp;
                                                        </td>
                                                        <td style="font-weight: bold; padding-right: 20px;">
                                                            <asp:RadioButtonList ID="rblJobStaus" runat="server" RepeatDirection="Horizontal"
                                                                TextAlign="Right" CellPadding="1" CellSpacing="0" AutoPostBack="True" OnSelectedIndexChanged="Filter_Changed">
                                                                <asp:ListItem Value="0" Text="All"></asp:ListItem>
                                                                <asp:ListItem Value="PENDING" Text="Pending" Selected="True"></asp:ListItem>
                                                                <asp:ListItem Value="COMPLETED" Text="Completed"></asp:ListItem>
                                                                <asp:ListItem Value="REWORKED" Text="Re-worked"></asp:ListItem>
                                                                <asp:ListItem Value="CLOSED" Text="Verified"></asp:ListItem>
                                                                <asp:ListItem Value="OVERDUEPENDING" Text="Overdue"></asp:ListItem>
                                                            </asp:RadioButtonList>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </div>
                                        <asp:GridView ID="grdJoblist" runat="server" AutoGenerateColumns="false" CellPadding="4"
                                            ShowHeaderWhenEmpty="true" DataKeyNames="WORKLIST_ID,VESSEL_ID,OFFICE_ID" EnableModelValidation="True"
                                            AllowSorting="false" Width="100%" GridLines="None" OnRowCommand="grdJoblist_RowCommand"
                                            OnRowDataBound="grdJoblist_RowDataBound" OnSorting="grdJoblist_Sorting" AllowPaging="false">
                                            <Columns>
                                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                                    ItemStyle-Width="10px" Visible="true">
                                                    <ItemTemplate>
                                                        <asp:Image ID="imgAnyChanges" runat="server" Height="16px" Width="16px" ImageUrl="~/Images/exclamation.gif"
                                                            Visible='<%#Eval("MODIFIED").ToString()=="1"?true:false %>' ToolTip="Modified in last 3 days." />
                                                        <div style="height: 6px; width: 6px; background-color: <%#Eval("WL_PRIORITY_COLOR").ToString()%>"
                                                            title="Priority: <%#Eval("PRIORITY").ToString()%>">
                                                        </div>
                                                    </ItemTemplate>
                                                    <ControlStyle Width="0px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                    HeaderText="Vessel" SortExpression="Vessel_Short_Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblVesselShortName" runat="server" Text='<%#Eval("Vessel_Name") %>'
                                                            Style="white-space: nowrap"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                                    HeaderText="Code" SortExpression="WORKLIST_ID">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbljobcodegriditem" runat="server" Text='<%#Eval("WLID_DISPLAY") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ControlStyle Width="35px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Job Description" SortExpression="JOB_DESCRIPTION"
                                                    ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <a href='ViewJob.aspx?OFFID=<%#Eval("OFFICE_ID") %>&WLID=<%#Eval("WORKLIST_ID") %>&VID=<%#Eval("VESSEL_ID") %>'
                                                            target="_blank" style="cursor: hand; color: Blue; text-decoration: none;">
                                                            <asp:Label ID="jd" runat="server" ToolTip='<%#Eval("JOB_DESCRIPTION")%>' Text='<%#Eval("JOB_DESCRIPTION").ToString().Length > 80 ?  Eval("JOB_DESCRIPTION").ToString().Substring(0, 80) + "..." : Eval("JOB_DESCRIPTION").ToString() %>'></asp:Label></a>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                    HeaderText="Assignor" SortExpression="AssignorName">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgrdAssignor" runat="server" Text='<%#Eval("AssignorName") %>'></asp:Label></ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                    HeaderText="PIC" SortExpression="PIC">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPIC" runat="server" Text='<%#Eval("USER_NAME") %>'></asp:Label></ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                    HeaderText="Date Raised" SortExpression="DATE_RAISED">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgrdRaisedDate" runat="server" Text='<%# Eval("DATE_RAISED","{0:dd/MMM/yy}").ToString() == "01/01/1900" ? "" : Eval("DATE_RAISED","{0:d/MMM/yy}")  %>'></asp:Label></ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                    HeaderText="Office Dept" SortExpression="INOFFICE_DEPT">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgrdofficeDept" runat="server" Text='<%#Eval("INOFFICE_DEPT") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                    HeaderText="Vessel Dept" SortExpression="ONSHIP_DEPT">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgrdVesselDept" runat="server" Text='<%#Eval("ONSHIP_DEPT") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                                    HeaderText="Expected Compln" SortExpression="DATE_ESTMTD_CMPLTN">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDATE_ESTMTD_CMPLTN" runat="server" Text='<%# Eval("DATE_ESTMTD_CMPLTN","{0:dd/MMM/yy}").ToString() == "01/01/1900" ? "" : Eval("DATE_ESTMTD_CMPLTN","{0:d/MMM/yy}") %>'></asp:Label></ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                                    HeaderText="Completed" SortExpression="DATE_COMPLETED">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgrdCompletedOn" runat="server" Text='<%# Eval("DATE_COMPLETED","{0:dd/MMM/yy}").ToString() == "01/01/1900" ? "" : Eval("DATE_COMPLETED","{0:d/MMM/yy}") %>'></asp:Label></ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                                    HeaderText="NCR" SortExpression="NCR">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgrdNCR" runat="server" Text='<%#Eval("NCR").ToString()=="0"?"":"YES" %>'></asp:Label></ItemTemplate>
                                                    <ControlStyle Width="30px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                                    HeaderText="Att.">
                                                    <ItemTemplate>
                                                        <asp:Image ID="ImgAttachment" runat="server" ImageUrl="~/Images/attach.png" AlternateText="Attachment" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="WORKLIST_STATUS" HeaderText="Status" />
                                               <%-- <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                                    HeaderText="">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="imgFlagOFF" runat="server" ImageUrl="~/Images/Flag_Off.png"
                                                            Visible='<%# !Convert.ToBoolean(Eval("FLAG_Tech_Meeting")) %>' CommandName="FlagJobForMeeting"
                                                            CommandArgument='<%#Eval("OFFICE_ID").ToString()+ "," + Eval("WORKLIST_ID").ToString() + "," + Eval("VESSEL_ID").ToString()+ ",1"%>' />
                                                        <asp:ImageButton ID="imgFlagON" runat="server" ImageUrl="~/Images/Flag_ON.png" Visible='<%# Convert.ToBoolean(Eval("FLAG_Tech_Meeting")) %>'
                                                            CommandName="FlagJobForMeeting" CommandArgument='<%#Eval("OFFICE_ID").ToString()+ "," + Eval("WORKLIST_ID").ToString() + "," + Eval("VESSEL_ID").ToString()+ ",0"%>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
                                                <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        <asp:CheckBox ID="checkAll" runat="server" onclick="checkAll(this);" />
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="checkRow" runat="server" onclick="Check_Click(this)" Enabled='<%# EnableCheckbox(Eval("WORKLIST_ID").ToString(),Eval("VESSEL_ID").ToString(),Eval("OFFICE_ID").ToString()) %>' Checked='<%# SelectCheckbox(Eval("WORKLIST_ID").ToString(),Eval("VESSEL_ID").ToString(),Eval("OFFICE_ID").ToString()) %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <EmptyDataTemplate>
                                                <label id="Label1" runat="server">
                                                    No jobs found !!</label>
                                            </EmptyDataTemplate>
                                            <HeaderStyle CssClass="HeaderStyle-css Crew-HeaderStyle-css" />
                                            <PagerStyle CssClass="PagerStyle-css" />
                                            <RowStyle CssClass="RowStyle-css" />
                                            <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                            <SelectedRowStyle CssClass="SelectedRowStyle-css" />
                                            <SortedAscendingCellStyle CssClass="SortedAscendingCellStyle-css" />
                                            <SortedAscendingHeaderStyle CssClass="SortedAscendingHeaderStyle-css" />
                                            <SortedDescendingCellStyle CssClass="SortedDescendingCellStyle-css" />
                                            <SortedDescendingHeaderStyle CssClass="SortedDescendingHeaderStyle" />
                                        </asp:GridView>
                                        <auc:CustomPager ID="ucCustomPagerctp" OnBindDataItem="Search_Worklist" AlwaysGetRecordsCount="true"
                                            PageSize="10" RecordCountCaption="Total Jobs" runat="server" />
                                    </div>
                                </td>
                            </tr>
                        </table>
                        <div style="text-align: right">
                            <asp:Button ID="btnAssign" runat="server" Text="Assign" Style="margin-top: 10px;
                                width: 100px" OnClick="btnAssign_Click" Visible="false" />
                            &nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnAssignandClose" runat="server" Text="Save and Close" Style="margin-top: 10px;
                                width: 200px" OnClick="btnAssignandClose_Click" />
                            <asp:Button ID="btnGenerateReport" runat="server" Text="Generate Report" Style="margin-top: 10px;
                                width: 150px; margin-left: 10px; margin-right: 10px" OnClick="btnGenerateReport_Click" Visible="false" />
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>

        <div id="dvRemark" style="display: none; width:360px;vertical-align:top; " title="Add Remark">

          <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                    <ContentTemplate>
                     
                            <table >
                                <tr>
                                    <td style="white-space:pre-wrap;">
                                        <%-- <asp:TextBox ID="txtPopupRemark" runat="server" TextMode="MultiLine" width="350px" Height="150px"></asp:TextBox>--%>
                                        <asp:TextBox ID="txtPopupRemark" runat="server" TextMode="MultiLine" Width="350px"
                                            Height="150px" Font-Names="Tahoma" Font-Size="12px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: center;">
                                        <asp:Button ID="BtnSaveRemark" runat="server" Text="Ok" OnClientClick="return onSaveRemark();" />
                                    </td>
                                </tr>
                            </table>
                       
        </ContentTemplate>
        </asp:UpdatePanel>
        </div>

        <div id="dvJob" style="display: none; width: 800px;" title="OT03-Work List">

            <iframe id="IframeJob" src="" frameborder="0" style="height:500px; width: 100%">
            </iframe>
        </div>
    </div>
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <asp:HiddenField ID="hdnDirtyCounter" runat="server" />
            <asp:HiddenField ID="hdnConfirm" runat="server" />
            <asp:HiddenField ID="hdnrnd" runat="server" />
            <asp:HiddenField ID="hdnCurrentCatNo" runat="server" />
            <asp:HiddenField ID="hdnCurrentCatDesc" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
