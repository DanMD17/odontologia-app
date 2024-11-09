<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="WFRolesPermissions.aspx.cs" Inherits="Presentation.WFRolesPermissions" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%-- Estilos --%>
    <link href="resources/css/datatables.min.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%-- Campo oculto para el ID de Rol-Permiso --%>
    <asp:HiddenField ID="HFRolesPermissionID" runat="server" />

    <%-- Selección de Rol --%>
    <asp:Label ID="Label1" runat="server" Text="Seleccione un rol"></asp:Label>
    <asp:DropDownList ID="DDLRoles" runat="server" CssClass="fromn-select"></asp:DropDownList>
    <br />

    <%-- Selección de Permiso --%>
    <asp:Label ID="Label2" runat="server" Text="Seleccione un permiso"></asp:Label>
    <asp:DropDownList ID="DDLPermissions" runat="server" CssClass="fromn-select"></asp:DropDownList>
    <br />

    <asp:Button ID="BtnSave" runat="server" Text="Guardar" OnClick="BtnSave_Click" />
    <asp:Button ID="BtnUpdate" runat="server" Text="Actualizar" />
    <asp:Label ID="LblMsg" runat="server" Text=""></asp:Label>
    <br />

    <%-- Lista de Roles y Permisos --%>
    <h2>Lista de Roles y Permisos</h2>
    <table id="rolesPermissionsTable" class="display" style="width: 100%">
        <thead>
            <tr>
                <th>RoleID</th>
                <th>RoleName</th>
                <th>PermissionID</th>
                <th>PermissionName</th>
            </tr>
        </thead>
        <tbody>
        </tbody>
    </table>

    <script src="resources/js/datatables.min.js" type="text/javascript"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            // Inicializa la tabla de roles y permisos
            $('#rolesPermissionsTable').DataTable({
                "processing": true,
                "serverSide": false,
                "ajax": {
                    "url": "WFRolesPermissions.aspx/ListRolesPermissions", // Método Web para listar roles y permisos
                    "type": "POST",
                    "contentType": "application/json",
                    "data": function (d) {
                        return JSON.stringify(d); // Convierte los datos a JSON
                    },
                    "dataSrc": function (json) {
                        return json.d.data; // Obtiene la lista de roles y permisos
                    }
                },
                "columns": [
                    { "data": "RoleID" },
                    { "data": "RoleName" },
                    { "data": "PermissionID" },
                    { "data": "PermissionName" },
                    {
                        "data": null,
                        "render": function (data, type, row) {
                            return `<button class="edit-btn" data-id="${row.RoleID}" data-permission="${row.PermissionID}">Editar</button>
                                    <button class="delete-btn" data-id="${row.RoleID}" data-permission="${row.PermissionID}">Eliminar</button>`;
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

            // Función para editar rol-permiso
            $('#rolesPermissionsTable').on('click', '.edit-btn', function () {
                const rowData = $('#rolesPermissionsTable').DataTable().row($(this).parents('tr')).data();
                loadRolePermissionData(rowData);
            });

            // Función para eliminar rol-permiso
            $('#rolesPermissionsTable').on('click', '.delete-btn', function () {
                const roleId = $(this).data('id');
                const permissionId = $(this).data('permission');
                if (confirm("¿Estás seguro de que deseas eliminar este rol-permiso?")) {
                    deleteRolePermission(roleId, permissionId);
                }
            });

            // Cargar los roles y permisos al cargar la página
            loadRoles();
            loadPermissions();
        });

        // Cargar los roles en el DropDownList de roles
        function loadRoles() {
            $.ajax({
                type: "POST",
                url: "WFRolesPermissions.aspx/GetRoles", // Método Web para obtener roles
                contentType: "application/json; charset=utf-8",
                success: function (response) {
                    const roles = response.d;
                    roles.forEach(role => {
                        $('#DDLRoles').append(new Option(role.RoleName, role.RoleID));
                    });
                },
                error: function () {
                    alert("Error al cargar los roles.");
                }
            });
        }

        // Cargar los permisos en el DropDownList de permisos
        function loadPermissions() {
            $.ajax({
                type: "POST",
                url: "WFRolesPermissions.aspx/GetPermissions", // Método Web para obtener permisos
                contentType: "application/json; charset=utf-8",
                success: function (response) {
                    const permissions = response.d;
                    permissions.forEach(permission => {
                        $('#DDLPermissions').append(new Option(permission.PermissionName, permission.PermissionID));
                    });
                },
                error: function () {
                    alert("Error al cargar los permisos.");
                }
            });
        }

        // Cargar datos en los DropDownLists para actualizar
        function loadRolePermissionData(rowData) {
            $('#<%= HFRolesPermissionID.ClientID %>').val(rowData.RoleID);
            $('#<%= DDLPermissions.ClientID %>').val(rowData.PermissionID);
        }

        // Eliminar un rol-permiso
        function deleteRolePermission(roleId, permissionId) {
            $.ajax({
                type: "POST",
                url: "WFRolesPermissions.aspx/DeleteRolePermission", // Método Web para eliminar rol-permiso
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify({ roleId: roleId, permissionId: permissionId }),
                success: function (response) {
                    $('#rolesPermissionsTable').DataTable().ajax.reload();
                    alert("Rol-Permiso eliminado exitosamente.");
                },
                error: function () {
                    alert("Error al eliminar el rol-permiso.");
                }
            });
        }
    </script>
</asp:Content>
