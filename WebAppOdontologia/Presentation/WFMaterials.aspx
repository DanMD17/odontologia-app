<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="WFMaterials.aspx.cs" Inherits="Presentation.WFMaterials" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--Aqui va todo lo de el formulario de Materiales--%>
<asp:TextBox ID="TBId" runat="server"></asp:TextBox>
<%--Nombre del material--%>
<asp:Label ID="Label1" runat="server" Text="Ingrese el nombre del material"></asp:Label>
<asp:TextBox ID="TBmaterialName" runat="server"></asp:TextBox>
<br />
<%--Descripcion del material--%>
<asp:Label ID="Label2" runat="server" Text="Describa el material empleado"></asp:Label>
<asp:TextBox ID="TBmaterialDescription" runat="server"></asp:TextBox>
<br />
<%--Cantidad del material--%>
<asp:Label ID="Label3" runat="server" Text="Introduzca la cantidad de material utilizado"></asp:Label>
<asp:TextBox ID="TBmaterialQuantity" runat="server"></asp:TextBox>
<br />
<%--Seleccionar tratamientp--%>
<asp:Label ID="Label4" runat="server" Text="Seleccione un tratamiento"></asp:Label>
<asp:DropDownList ID="DDLTreatments" runat="server" CssClass="fromn-select"></asp:DropDownList>
<%--Botones guardar y actualizar--%>
<div>
    <asp:Button ID="BtnSave" runat="server" Text="Guardar" OnClick="BtnSave_Click" />
    <asp:Button ID="BtbUpdate" runat="server" Text="Actualizar" OnClick="BtbUpdate_Click" />
    <asp:Label ID="LblMsg" runat="server" Text=""></asp:Label>
</div>
<br />
<%--Lista de Materiales--%>
<div>
    <asp:GridView ID="GVMateriales" runat="server"></asp:GridView>
</div>
</asp:Content>
