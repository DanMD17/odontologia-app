<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="WFSecretaries.aspx.cs" Inherits="Presentation.WFSecretaries" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <asp:TextBox ID="TBId" runat="server"></asp:TextBox>
        <asp:Label ID="Label1" runat="server" Text="Ingrese la funcion de la secretaria"></asp:Label>
        <asp:TextBox ID="TBFunction" runat="server"></asp:TextBox>
        <br />
        <asp:Label ID="Label2" runat="server" Text="Ingrese los anios de experiencia"></asp:Label>
        <asp:TextBox ID="TBYearsExp" runat="server"></asp:TextBox>
        <br />
        <asp:Label ID="Label3" runat="server" Text="Seleccione un empleado"></asp:Label>
        <asp:DropDownList ID="DDLEmployee" runat="server" CssClass="fromn-select"></asp:DropDownList>
    </div>
    <div>  
        <asp:Button ID="BtnSave" runat="server" Text="Guardar" OnClick="BtnSave_Click" />
        <asp:Button ID="BtnUpdate" runat="server" Text="Actualizar" />
        <asp:Label ID="LblMsg" runat="server" Text=""></asp:Label>
    </div>
    <div>
        <asp:GridView ID="GVSecretaries" runat="server"></asp:GridView>
    </div>
</asp:Content>
