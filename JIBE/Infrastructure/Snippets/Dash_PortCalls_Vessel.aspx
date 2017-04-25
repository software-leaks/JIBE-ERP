<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Dash_PortCalls_Vessel.aspx.cs" Inherits="Infrastructure_Snippets_Dash_PortCalls_Vessel" %>

        <asp:Chart ID="ChrtPortCallsVessel" runat="server" Width="600px"  ondatabound="ChrtPortCallsVessel_DataBound"   RenderType="BinaryStreaming"  >
            <Series>
                <asp:Series Name="SeriesPortCallsVessel"  XValueMember="RNUM"  YValueMembers="portcount"   >
              
                </asp:Series>
            </Series>
            <ChartAreas>
                <asp:ChartArea Name="ChartAreaPortCallsVessel"  BorderColor="DarkGray" >
                 <AxisY LineColor="DarkGray"   >
                        <MajorGrid LineColor="DarkGray" />
                          <LabelStyle Font="Tahoma, 9pt" />
                    </AxisY>
                    <AxisX LineColor="DarkGray">
                        <MajorGrid LineColor="DarkGray" />
                      <LabelStyle Font="Tahoma, 9pt" />
                    </AxisX>
                    <AxisX2 LineColor="DarkGray">
                    </AxisX2>
                    <AxisY2 LineColor="DarkGray">
                    </AxisY2>
               
                </asp:ChartArea>
            </ChartAreas>
        </asp:Chart>
  
