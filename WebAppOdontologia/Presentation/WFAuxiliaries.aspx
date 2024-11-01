<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="WFAuxiliaries.aspx.cs" Inherits="Presentation.WFAuxiliaries" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--Aki va todo lo de el formulario de Auxiliares--%>
    <asp:TextBox ID="TBId" runat="server"></asp:TextBox>
    <%--Funcion del auxiliar--%>
    <asp:Label ID="Label1" runat="server" Text="Ingrese la funcion"></asp:Label>
    <asp:TextBox ID="TBFunction" runat="server"></asp:TextBox>
    <br />
    <%--Nivel educativo del auxiliar--%>
    <asp:Label ID="Label2" runat="server" Text="Ingrese el nivel educativo del auxiliar"></asp:Label>
    <asp:TextBox ID="TBEducationalLevel" runat="server"></asp:TextBox>
    <br />
    <%--Seleccionar empleado--%>
    <asp:Label ID="Label3" runat="server" Text="Seleccione un empleado"></asp:Label>
    <asp:DropDownList ID="DDLEmployee" runat="server" CssClass="fromn-select"></asp:DropDownList>
    <%--Botones guardar y actualizar--%>
    <div>
        <asp:Button ID="BtnSave" runat="server" Text="Guardar" OnClick="BtnSave_Click" />
        <asp:Button ID="BtbUpdate" runat="server" Text="Actualizar" OnClick="BtbUpdate_Click" />
        <asp:Label ID="LblMsg" runat="server" Text=""></asp:Label>
    </div>
    <br />
    <%--Lista de Empleados--%>
    <div>
        <asp:GridView ID="GVAuxiliaries" runat="server"></asp:GridView>
    </div>
</asp:Content>
