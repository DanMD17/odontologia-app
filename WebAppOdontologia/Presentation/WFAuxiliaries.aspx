﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="WFAuxiliaries.aspx.cs" Inherits="Presentation.WFAuxiliaries" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--Estilos--%>
    <link href="resources/css/datatables.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <%--ID--%>
    <asp:HiddenField ID="HFAssistantID" runat="server" />

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
    <br />

    <%--Botones guardar y actualizar--%>
    <div>
        <asp:Button ID="BtnSave" runat="server" Text="Guardar" OnClick="BtnSave_Click" />
        <asp:Button ID="BtbUpdate" runat="server" Text="Actualizar" OnClick="BtbUpdate_Click" />
        <asp:Label ID="LblMsg" runat="server" Text=""></asp:Label>
    </div>
    <br />

    <%--Lista de Auxiliares--%>
    <h2>Lista de Auxiliares</h2>
    <table id="assistantsTable" class="display" style="width: 100%">
        <thead>
            <tr>
                <th>AuxiliarID</th>
                <th>Funcion</th>
                <th>NivelEducativo</th>
                <th>FkEmpleado</th>
                <th>Empleado</th>
            </tr>
        </thead>
        <tbody>
        </tbody>
    </table>

    <script src="resources/js/datatables.min.js" type="text/javascript"></script>

    <%--Auxiliares--%>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#assistantsTable').DataTable({
                "processing": true,
                "serverSide": false,
                "ajax": {
                    "url": "WFAuxiliaries.aspx/ListAssistants",// Se invoca el WebMethod Listar auxiliares
                    "type": "POST",
                    "contentType": "application/json",
                    "data": function (d) {
                        return JSON.stringify(d);// Convierte los datos a JSON
                    },
                    "dataSrc": function (json) {
                        return json.d.data;// Obtiene la lista de auxiliares del resultado
                    }
                },
                "columns": [
                    { "data": "AssistantID" },
                    { "data": "Function" },
                    { "data": "EducationalLevel" },
                    { "data": "FkEmployee", "visible": false },
                    { "data": "NameEmployee" },
                    {
                        "data": null,
                        "render": function (data, type, row) {
                            return `<button class="edit-btn" data-id="${row.AssistantID}">Editar</button>
                             <button class="delete-btn" data-id="${row.AssistantID}">Eliminar</button>`;
                        }
                    }
                ],
                "language": {
                    "lengthMenu": "Mostrar _MENU_ registros por página",
                    "zeroRecords": "No se encontraron resultados",
                    "info": "Mostrando página _PAGE_ de _PAGES_",
                    "infoEmpty": "No hay registros disponibles",
                    "infoFiltered": "(filtrado de _MAX_ registros totales)",
                    "search": "Buscar:",
                    "paginate": {
                        "first": "Primero",
                        "last": "Último",
                        "next": "Siguiente",
                        "previous": "Anterior"
                    }
                }

            });

            // Editar un auxiliar
            $('#assistantsTable').on('click', '.edit-btn', function () {
                //const id = $(this).data('id');
                const rowData = $('#assistantsTable').DataTable().row($(this).parents('tr')).data();
                //alert(JSON.stringify(rowData, null, 2));
                loadAssistantData(rowData);
            });

            // Eliminar un auxiliar
            $('#assistantsTable').on('click', '.delete-btn', function () {
                const id = $(this).data('id');// Obtener el ID del auxiliar
                if (confirm("¿Estás seguro de que deseas eliminar este auxiliar?")) {
                    deleteAssistant(id);// Invoca a la función para eliminar el auxiliar
                }
            });
        });

        // Función para cargar los datos en los TextBox y DDL para actualizar
        function loadAssistantData(rowData) {
            $('#<%= HFAssistantID.ClientID %>').val(rowData.AssistantID);
            $('#<%= TBFunction.ClientID %>').val(rowData.Function);
            $('#<%= TBEducationalLevel.ClientID %>').val(rowData.EducationalLevel);
            $('#<%= DDLEmployee.ClientID %>').val(rowData.FkEmployee);
        }

        // Función para eliminar un auxiliar
        function deleteAssistant(id) {
            $.ajax({
                type: "POST",
                url: "WFAuxiliaries.aspx/DeleteAssistant",//Se invoca el WebMethod Eliminar un auxiliar
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify({ id: id }),
                success: function (response) {
                    $('#assistantsTable').DataTable().ajax.reload();//Recargar la tabla después de eliminar
                    alert("Auxiliar eliminado exitosamente.");
                },
                error: function () {
                    alert("Error al eliminar el auxiliar.");
                }
            });
        }
    </script>
</asp:Content>
