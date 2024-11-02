<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="WFDentist.aspx.cs" Inherits="Presentation.WFDentists" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <%--Formulario de Odontólogos--%>
    <asp:Label ID="Label1" runat="server" Text="Ingrese la especialidad"></asp:Label>
    <asp:TextBox ID="TBSpecialty" runat="server"></asp:TextBox>
    <br />
    
    <%--Seleccionar empleado--%>
    <asp:Label ID="Label2" runat="server" Text="Seleccione un empleado"></asp:Label>
    <asp:DropDownList ID="DDLEmployee" runat="server" CssClass="form-select"></asp:DropDownList>
    
    <%--Botones guardar--%>
    <div>
        <asp:Button ID="BtnSave" runat="server" Text="Guardar" OnClick="BtnSave_Click" />
        <asp:Label ID="LblMsg" runat="server" Text=""></asp:Label>
    </div>
    <br />
    
    <%--Lista de Odontólogos--%>
    <div>
        <asp:GridView ID="GVDentists" runat="server"></asp:GridView>
    </div>
</asp:Content>
