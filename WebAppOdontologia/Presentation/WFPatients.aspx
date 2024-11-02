<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="WFPatients.aspx.cs" Inherits="Presentation.WFPatients" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--Formulario para Pacientes--%>
    <asp:TextBox ID="TBId" runat="server" Visible="false"></asp:TextBox>
    <%--Nombre--%>
    <asp:Label ID="Label1" runat="server" Text="Ingrese el nombre"></asp:Label>
    <asp:TextBox ID="TBName" runat="server"></asp:TextBox>
    <br />
    <%--Apellido--%>
    <asp:Label ID="Label2" runat="server" Text="Ingrese el apellido"></asp:Label>
    <asp:TextBox ID="TBLastName" runat="server"></asp:TextBox>
    <br />
    <%--Fecha de Nacimiento--%>
    <asp:Label ID="Label3" runat="server" Text="Ingrese la fecha de nacimiento"></asp:Label>
    <asp:TextBox ID="TBDateOfBirth" runat="server" TextMode="Date"></asp:TextBox>
    <br />
    <%--Dirección--%>
    <asp:Label ID="Label4" runat="server" Text="Ingrese la dirección"></asp:Label>
    <asp:TextBox ID="TBAddress" runat="server"></asp:TextBox>
    <br />
    <%--Celular--%>
    <asp:Label ID="Label5" runat="server" Text="Ingrese el número de celular"></asp:Label>
    <asp:TextBox ID="TBCellPhone" runat="server"></asp:TextBox>
    <br />
    <%--Correo Electrónico--%>
    <asp:Label ID="Label6" runat="server" Text="Ingrese el correo"></asp:Label>
    <asp:TextBox ID="TBEmail" runat="server"></asp:TextBox>
    <br />
    <%--Botones de Guardar y Actualizar--%>
    <div>
        <asp:Button ID="BtnSave" runat="server" Text="Guardar" OnClick="BtnSave_Click" />
        <asp:Button ID="BtnUpdate" runat="server" Text="Actualizar" OnClick="BtnUpdate_Click" />
        <asp:Label ID="LblMsg" runat="server" Text=""></asp:Label>
    </div>
    <br />
    <%--Lista de Pacientes--%>
    <div>
        <asp:GridView ID="GVPatients" runat="server"></asp:GridView>
    </div>
</asp:Content>
