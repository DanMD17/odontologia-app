﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="WFMaterials.aspx.cs" Inherits="Presentation.WFMaterials" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <%--Estilos--%>
    <link href="resources/css/datatables.min.css" rel="stylesheet" />

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

    <%-- DDL Para Seleccionar tratamiento--%>
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

    <h2>Lista de Materiales</h2>
    <table id="materialsTable" class="display" style="width: 100%">
        <thead>
            <tr>
                <th>MaterialID</th>
                <th>Nombre</th>
                <th>Descripcion</th>
                <th>Cantidad</th>
                <th>FkTratamiento</th>
                <th>Tratamiento</th>
            </tr>
        </thead>
        <tbody>
        </tbody>
    </table>


    <script src="resources/js/datatables.min.js" type="text/javascript"></script>

    <%--Materials--%>
    <script>          

        <script type="text/javascript">
            $(document).ready(function () {
                $('#materialsTable').DataTable({
                    "processing": true,
                    "serverSide": false,
                    "ajax": {
                        "url": "WFMaterials.aspx/ListMaterials", // WebMethod para listar materiales
                        "type": "POST",
                        "contentType": "application/json",
                        "data": function (d) {
                            return JSON.stringify(d); // Convierte los datos a JSON
                        },
                        "dataSrc": function (json) {
                            return json.d.data; // Obtiene la lista de materiales del resultado
                        }
                    },
                    "columns": [
                        { "data": "MaterialID" },
                        { "data": "Nombre" },
                        { "data": "Descripcion" },
                        { "data": "Cantidad" },
                        { "data": "FkTratamiento", "visible": false },
                        { "data": "Tratamiento" },
                        {
                            "data": null,
                            "render": function (data, type, row) {
                                return `<button class="edit-btn" data-id="${row.MaterialID}">Editar</button>
                                <button class="delete-btn" data-id="${row.MaterialID}">Eliminar</button>`;
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

            // Editar un material
            $('#materialsTable').on('click', '.edit-btn', function () {
                const rowData = $('#materialsTable').DataTable().row($(this).parents('tr')).data();
            loadMaterialData(rowData);
            });

            // Eliminar un material
            $('#materialsTable').on('click', '.delete-btn', function () {
                const id = $(this).data('id'); // Obtener el ID del material
            if (confirm("¿Está seguro de que desea eliminar este material?")) {
                deleteMaterial(id); // Invoca a la función para eliminar el material
                }
            });
        });

            // Función para cargar los datos del material en los TextBox y DDL para actualizar
            function loadMaterialData(rowData) {
                $('#<%= TBId.ClientID %>').val(rowData.MaterialID);
        $('#<%= TBmaterialName.ClientID %>').val(rowData.Nombre);
        $('#<%= TBmaterialDescription.ClientID %>').val(rowData.Descripcion);
        $('#<%= TBmaterialQuantity.ClientID %>').val(rowData.Cantidad);
        $('#<%= DDLTreatments.ClientID %>').val(rowData.FkTratamiento);
        }

            // Función para eliminar un material
            function deleteMaterial(id) {
                $.ajax({
                    type: "POST",
                    url: "WFMaterials.aspx/DeleteMaterial", // WebMethod para eliminar un material
                    contentType: "application/json; charset=utf-8",
                    data: JSON.stringify({ id: id }),
                    success: function (response) {
                        $('#materialsTable').DataTable().ajax.reload(); // Recargar la tabla después de eliminar
                        alert("Material eliminado exitosamente.");
                    },
                    error: function () {
                        alert("Error al eliminar el material.");
                    }
                });
        }
    </script>
</asp:Content>
