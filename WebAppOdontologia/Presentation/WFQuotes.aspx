<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="WFQuotes.aspx.cs" Inherits="Presentation.WFQuotes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--Formulario para Citas--%>
    <asp:Label ID="Label1" runat="server" Text="Ingrese la fecha de la cita"></asp:Label>
    <asp:TextBox ID="TBDate" runat="server" TextMode="Date"></asp:TextBox>
    <br />

    <asp:Label ID="Label2" runat="server" Text="Ingrese la hora de la cita"></asp:Label>
    <asp:TextBox ID="TBTime" runat="server" TextMode="Time"></asp:TextBox>
    <br />

    <asp:Label ID="Label3" runat="server" Text="Estado de la cita"></asp:Label>
    <asp:TextBox ID="TBStatus" runat="server"></asp:TextBox>
    <br />

    <asp:Label ID="Label4" runat="server" Text="ID del Paciente"></asp:Label>
    <asp:TextBox ID="TBPatientId" runat="server"></asp:TextBox>
    <br />

    <asp:Label ID="Label5" runat="server" Text="ID del Odontólogo"></asp:Label>
    <asp:TextBox ID="TBDentistId" runat="server"></asp:TextBox>
    <br />

    <%--Botones Guardar y Actualizar--%>
    <div>
        <asp:Button ID="BtnSave" runat="server" Text="Guardar" OnClick="BtnSave_Click" />
        <asp:Button ID="BtnUpdate" runat="server" Text="Actualizar" OnClick="BtnUpdate_Click" />
        <asp:Label ID="LblMsg" runat="server" Text=""></asp:Label>
    </div>
    <br />

    <%--Lista de Citas--%>
    <div>
        <asp:GridView ID="GVQuotes" runat="server"></asp:GridView>
    </div>
</asp:Content>
