<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="WFRoles.aspx.cs" Inherits="Presentation.WFRoles" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <%--Estilos--%>
    <link href="resources/css/datatables.min.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <%--Id del Rol--%>
    <asp:HiddenField ID="HFRolID" runat="server" />

    <%--Nombre del Rol--%>
    <asp:Label ID="Label1" runat="server" Text="Ingrese el nombre del rol"></asp:Label>
    <asp:TextBox ID="TBNombreRol" runat="server"></asp:TextBox>
    <br />

    <%--Descripción del Rol--%>
    <asp:Label ID="Label2" runat="server" Text="Ingrese la descripción"></asp:Label>
    <asp:TextBox ID="TBDescripcionRol" runat="server"></asp:TextBox>
    <br />

    <%--Botones--%>
    <asp:Button ID="BtnSave" runat="server" Text="Guardar" OnClick="BtnSave_Click" />
    <asp:Button ID="BtnUpdate" runat="server" Text="Actualizar" OnClick="BtnUpdate_Click" />
    <asp:Label ID="LblMsg" runat="server" Text=""></asp:Label>
    <br />

    <%--Lista de Roles--%>
    <h2>Lista de Roles</h2>
    <table id="rolesTable" class="display" style="width: 100%">
        <thead>
            <tr>
                <th>RolID</th>
                <th>Nombre</th>
                <th>Descripción</th>
            </tr>
        </thead>
        <tbody>
        </tbody>
    </table>

    <script src="resources/js/datatables.min.js" type="text/javascript"></script>

    <%--Script para Roles--%>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#rolesTable').DataTable({
                "processing": true,
                "serverSide": false,
                "ajax": {
                    "url": "WFRoles.aspx/ListRoles", // Se invoca el WebMethod Listar roles
                    "type": "POST",
                    "contentType": "application/json",
                    "data": function (d) {
                        return JSON.stringify(d);
                    },
                    "dataSrc": function (json) {
                        return json.d.data;
                    }
                },
                "columns": [
                    { "data": "RolID" },
                    { "data": "Nombre" },
                    { "data": "Descripcion" },
                    {
                        "data": null,
                        "render": function (data, type, row) {
                            return `<button class="edit-btn" data-id="${row.RolID}">Editar</button>
                                    <button class="delete-btn" data-id="${row.RolID}">Eliminar</button>`;
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

            // Editar un rol
            $('#rolesTable').on('click', '.edit-btn', function () {
                const rowData = $('#rolesTable').DataTable().row($(this).parents('tr')).data();
                loadRoleData(rowData);
            });

            // Eliminar un rol
            $('#rolesTable').on('click', '.delete-btn', function () {
                const id = $(this).data('id');
                if (confirm("¿Estás seguro de que deseas eliminar este rol?")) {
                    deleteRole(id);
                }
            });
        });

        // Función para cargar datos del rol en los campos de edición y actualizar 
        function loadRoleData(rowData) {
            $('#<%= HFRolID.ClientID %>').val(rowData.RolID);
            $('#<%= TBNombreRol.ClientID %>').val(rowData.Nombre);
            $('#<%= TBDescripcionRol.ClientID %>').val(rowData.Descripcion);
        }

        // Función para eliminar un rol
        function deleteRole(id) {
            $.ajax({
                type: "POST",
                url: "WFRoles.aspx/DeleteRole", // Se invoca el WebMethod Eliminar un rol
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify({ id: id }),
                success: function (response) {
                    $('#rolesTable').DataTable().ajax.reload(); // Recargar la tabla después de eliminar
                    alert("Rol eliminado exitosamente.");
                },
                error: function () {
                    alert("Error al eliminar el rol.");
                }
            });
        }
    </script>
</asp:Content>
