<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="WFEmployees.aspx.cs" Inherits="Presentation.WFEmployees" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--Aki va todo lo de el formulario de Empleados--%>
    <asp:TextBox ID="TBId" runat="server"></asp:TextBox>
    <%--Identficacion--%>
    <asp:Label ID="Label1" runat="server" Text="Ingrese la identificacion"></asp:Label>
    <asp:TextBox ID="TBIdentification" runat="server"></asp:TextBox>
    <br />
    <%--Nombres--%>
    <asp:Label ID="Label2" runat="server" Text="Ingrese el nombre"></asp:Label>
    <asp:TextBox ID="TBName" runat="server"></asp:TextBox>
    <br />
    <%--Apellidos--%>
    <asp:Label ID="Label3" runat="server" Text="Ingrese el apellido"></asp:Label>
    <asp:TextBox ID="TBLastName" runat="server"></asp:TextBox>
    <br />
    <%--Telefono--%>
    <asp:Label ID="Label4" runat="server" Text="Ingrese el numero de celular"></asp:Label>
    <asp:TextBox ID="TBCellPhone" runat="server"></asp:TextBox>
    <br />
    <%--Direccion--%>
    <asp:Label ID="Label5" runat="server" Text="Ingrese la direccion"></asp:Label>
    <asp:TextBox ID="TBAddress" runat="server"></asp:TextBox>
    <br />
    <%--Correo Electronico--%>
    <asp:Label ID="Label6" runat="server" Text="Ingrese el correo"></asp:Label>
    <asp:TextBox ID="TBEmail" runat="server"></asp:TextBox>
    <br />
    <%--Botones guardar y actualizar--%>
    <div>
        <asp:Button ID="BtnSave" runat="server" Text="Guardar" OnClick="BtnSave_Click" />
        <asp:Button ID="BtbUpdate" runat="server" Text="Actualizar" OnClick="BtbUpdate_Click" />
        <asp:Label ID="LblMsg" runat="server" Text=""></asp:Label>
    </div>
    <br />
    <%--Lista de Empleados--%>
    <div>
        <asp:GridView ID="GVEmployees" runat="server"></asp:GridView>
    </div>
</asp:Content>