<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Dash_PortCall_Month.aspx.cs"
    Inherits="Infrastructure_Snippets_Dash_PortCall_Month" %>

<asp:chart id="ChrtPortCallsMonth" runat="server" width="600px" ondatabound="ChrtPortCallsMonth_DataBound"
    rendertype="BinaryStreaming">
       
            <Series>
            
                <asp:Series Name="SeriesPortCallsMonth" YValueMembers="PortCount" 
                    XValueMember="rownum" BorderColor="Transparent" Legend="LegendVC" >
               
                </asp:Series>
               
            </Series>
            <ChartAreas>
                <asp:ChartArea Name="ChartAreaPortCallsMonth" BorderColor="DarkGray" >
         
                    <AxisY LineColor="DarkGray" IsLabelAutoFit="False">
                        <MajorGrid LineColor="DarkGray" />
                        <LabelStyle Font="Tahoma, 9pt" />
                    </AxisY>
                    <AxisX LineColor="DarkGray" IsLabelAutoFit="False">
                        <MajorGrid LineColor="DarkGray" />
                    
                        <LabelStyle Font="Tahoma, 9pt" />
                    
                    </AxisX>
                    <AxisX2 LineColor="DarkGray">
                    </AxisX2>
                    <AxisY2 LineColor="DarkGray">
                    </AxisY2>
         
                </asp:ChartArea>
            </ChartAreas>
            <Legends>
                <asp:Legend Name="LegendVC">
                    <Position Height="8.026756" Width="17.8631058" X="79.1368942" />
                    <CellColumns>
                        <asp:LegendCellColumn BackColor="Transparent" 
                            Font="Microsoft Sans Serif, 8.25pt, style=Bold" ForeColor="Black" 
                            Name="ColumnVC" Text="VC - Vessel Count">
                        </asp:LegendCellColumn>
                    </CellColumns>
                </asp:Legend>
            </Legends>
            <Titles>
                <asp:Title Name="Title1">
                </asp:Title>
            </Titles>
            <BorderSkin BorderColor="DarkGray" />
        </asp:chart>
