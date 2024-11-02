<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="WFTreatments.aspx.cs" Inherits="Presentation.WFTreatments" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--Formulario de Tratamientos--%>
    <asp:TextBox ID="TBId" runat="server" Visible="false"></asp:TextBox>
    
    <%--Nombre--%>
    <asp:Label ID="Label1" runat="server" Text="Ingrese el nombre del tratamiento"></asp:Label>
    <asp:TextBox ID="TBName" runat="server"></asp:TextBox>
    <br />
    
    <%--Descripción--%>
    <asp:Label ID="Label2" runat="server" Text="Ingrese la descripción"></asp:Label>
    <asp:TextBox ID="TBDescription" runat="server"></asp:TextBox>
    <br />
    
    <%--Fecha--%>
    <asp:Label ID="Label3" runat="server" Text="Ingrese la fecha"></asp:Label>
    <asp:TextBox ID="TBDate" runat="server" TextMode="Date"></asp:TextBox>
    <br />
    
    <%--Observaciones--%>
    <asp:Label ID="Label4" runat="server" Text="Ingrese las observaciones"></asp:Label>
    <asp:TextBox ID="TBObservations" runat="server"></asp:TextBox>
    <br />
    
    <%--FK Cita ID--%>
    <asp:Label ID="Label5" runat="server" Text="ID de la cita"></asp:Label>
    <asp:TextBox ID="TBFkCitaId" runat="server"></asp:TextBox>
    <br />
    
    <%--FK Historial ID--%>
    <asp:Label ID="Label6" runat="server" Text="ID del historial"></asp:Label>
    <asp:TextBox ID="TBFkHistId" runat="server"></asp:TextBox>
    <br />
    
    <%--FK Auxiliar ID--%>
    <asp:Label ID="Label7" runat="server" Text="ID del auxiliar"></asp:Label>
    <asp:TextBox ID="TBFkAuxId" runat="server"></asp:TextBox>
    <br />
    
    <%--Botones guardar y actualizar--%>
    <div>
        <asp:Button ID="BtnSave" runat="server" Text="Guardar" OnClick="BtnSave_Click" />
        <asp:Button ID="BtnUpdate" runat="server" Text="Actualizar" OnClick="BtnUpdate_Click" />
        <asp:Label ID="LblMsg" runat="server" Text=""></asp:Label>
    </div>
    <br />
    
    <%--Lista de Tratamientos--%>
    <div>
        <asp:GridView ID="GVTreatments" runat="server"></asp:GridView>
    </div>
</asp:Content>
