<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="WFDiagnosis.aspx.cs" Inherits="Presentation.WFDiagnosis" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <%-- Formulario de Diagnóstico --%>
    <asp:Label ID="Label1" runat="server" Text="Ingrese la descripción del diagnóstico"></asp:Label>
    <asp:TextBox ID="TBDescription" runat="server"></asp:TextBox>
    <br />
    
    <%-- Fecha --%>
    <asp:Label ID="Label2" runat="server" Text="Ingrese la fecha"></asp:Label>
    <asp:TextBox ID="TBDate" runat="server"></asp:TextBox>
    <br />
    
    <%-- Observaciones --%>
    <asp:Label ID="Label3" runat="server" Text="Ingrese las observaciones"></asp:Label>
    <asp:TextBox ID="TBObservations" runat="server"></asp:TextBox>
    <br />
    
    <%-- Seleccionar Cita --%>
    <asp:Label ID="Label4" runat="server" Text="Seleccione una cita"></asp:Label>
    <asp:DropDownList ID="DDLQuotes" runat="server"></asp:DropDownList>
    
    <%-- Seleccionar Historial Clínico --%>
    <asp:Label ID="Label5" runat="server" Text="Seleccione un historial clínico"></asp:Label>
    <asp:DropDownList ID="DDLClinicalHistory" runat="server"></asp:DropDownList>
    
    <%-- Botón guardar --%>
    <div>
        <asp:Button ID="BtnSave" runat="server" Text="Guardar" OnClick="BtnSave_Click" />
        <asp:Label ID="LblMsg" runat="server" Text=""></asp:Label>
    </div>
    <br />
    
    <%-- Lista de Diagnósticos --%>
    <div>
        <asp:GridView ID="GVDiagnosis" runat="server"></asp:GridView>
    </div>

</asp:Content>
