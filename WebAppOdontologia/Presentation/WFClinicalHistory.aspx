<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="WFClinicalHistory.aspx.cs" Inherits="Presentation.WFClinicalHistory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <%--Formulario de Historial Clínico--%>
    <asp:TextBox ID="TBId" runat="server" ></asp:TextBox>
    
    <%--Fecha de creación--%>
    <asp:Label ID="Label1" runat="server" Text="Ingrese la fecha de creación"></asp:Label>
    <asp:TextBox ID="TBCreacionDate" runat="server"></asp:TextBox>
    <br />
    
    <%--Descripción general--%>
    <asp:Label ID="Label2" runat="server" Text="Ingrese la descripción general"></asp:Label>
    <asp:TextBox ID="TBOverview" runat="server" TextMode="MultiLine" Rows="4"></asp:TextBox>
    <br />
    
    <%--Seleccionar paciente--%>
    <asp:Label ID="Label3" runat="server" Text="Seleccione un paciente"></asp:Label>
    <asp:DropDownList ID="DDLPatient" runat="server" CssClass="form-select"></asp:DropDownList>
    
    <%--Botones guardar y actualizar--%>
    <div>
        <asp:Button ID="BtnSave" runat="server" Text="Guardar" OnClick="BtnSave_Click" />
        <asp:Button ID="BtbUpdate" runat="server" Text="Actualizar" OnClick="BtbUpdate_Click" />
    <asp:Label ID="LblMsg" runat="server" Text=""></asp:Label>
    </div>
    <br />
    
    <%--Lista de Historial Clínico--%>
    <div>
        <asp:GridView ID="GVClinicalHistory" runat="server"></asp:GridView>
    </div>
</asp:Content>
