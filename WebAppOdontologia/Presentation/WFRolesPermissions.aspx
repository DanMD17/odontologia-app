<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="WFPermissionsRoles.aspx.cs" Inherits="Presentation.WFPermissionsRoles" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form id="FrmPermissionRol" runat="server">
        <%--Id--%>
        <asp:HiddenField ID="HFRolPermisoID" runat="server" />
        <%--Roles--%>
        <asp:Label ID="Label1" runat="server" Text="Roles"></asp:Label>
        <asp:DropDownList ID="DDLRoles" runat="server"></asp:DropDownList>
        <br />
        <%--Permisos--%>
        <asp:Label ID="Label2" runat="server" Text="Permisos"></asp:Label>
        <asp:DropDownList ID="DDLPermisos" runat="server"></asp:DropDownList>
        <br />
        <%-- Fecha--%>
        <asp:Label ID="Label4" runat="server" Text="Fecha de Asignación"></asp:Label>
        <asp:TextBox ID="TBDate" runat="server" TextMode="Date"></asp:TextBox>
        <br />
        <%--Botones Guardar y Actualizar--%>
        <asp:Button ID="BtnGuardar" runat="server" Text="Guardar" OnClick="BtnGuardar_Click" />
        <asp:Button ID="BtnActualizar" runat="server" Text="Actualizar" OnClick="BtnActualizar_Click" />
        <asp:Label ID="LblMsg" runat="server" Text=""></asp:Label>
    </form>
    <%--Lista de Productos--%>
    <h2>Lista de Permisos Roles</h2>
    <table id="permisoRolTable" class="display" style="width: 100%">
        <thead>
            <tr>
                <th>ID</th>
                <th>IdRol</th>
                <th>Rol</th>
                <th>IdPermiso</th>
                <th>Permiso</th>
                <th>Fecha de Asignación</th>
            </tr>
        </thead>
        <tbody>
        </tbody>
    </table>

    <%--Datatables--%>
    <script src="resources/js/datatables.min.js" type="text/javascript"></script>

    <%--Permisos Roles--%>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#permisoRolTable').DataTable({
                "processing": true,
                "serverSide": false,
                "ajax": {
                    "url": "WFPermissionsRoles.aspx/ListPermissionsRoles",// Se invoca el WebMethod Listar Permisos Roles
                    "type": "POST",
                    "contentType": "application/json",
                    "data": function (d) {
                        return JSON.stringify(d);// Convierte los datos a JSON
                    },
                    "dataSrc": function (json) {
                        return json.d.data;// Obtiene la lista de permisos roles del resultado
                    }
                },
                "columns": [
                    { "data": "RolPermisoID" },
                    { "data": "RolID", "visible": false },
                    { "data": "NameRol" },
                    { "data": "PermissionID", "visible": false },
                    { "data": "NamePermission" },
                    { "data": "Date" },
                    {
                        "data": null,
                        "render": function (row) {
                            return `<button class="edit-btn" data-id="${row.RolPermisoID}">Editar</button>
                             <button class="delete-btn" data-id="${row.RolPermisoID}">Eliminar</button>`;
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

            // Editar un permiso rol
            $('#permisoRolTable').on('click', '.edit-btn', function () {
                //const id = $(this).data('id');
                const rowData = $('#permisoRolTable').DataTable().row($(this).parents('tr')).data();
                //alert(JSON.stringify(rowData, null, 2));
                permissionsRolesData(rowData);
            });

            // Eliminar un permiso rol
            $('#permisoRolTable').on('click', '.delete-btn', function () {
                const id = $(this).data('id');// Obtener el ID del permiso rol
                if (confirm("¿Estás seguro de que deseas eliminar este permiso rol?")) {
                    deletePermissionsRoles(id);// Invoca a la función para eliminar el permiso rol
                }
            });
        });

        // Cargar los datos en los TextBox y DDL para actualizar
        function permissionsRolesData(rowData) {
            $('#<%= HFRolPermisoID.ClientID %>').val(rowData.RolPermisoID);
            $('#<%= DDLRoles.ClientID %>').val(rowData.RolID);
            $('#<%= DDLPermisos.ClientID %>').val(rowData.PermissionID);
            $('#<%= TBDate.ClientID %>').val(rowData.Date);
        }

        // Función para eliminar un producto
        function deletePermissionsRoles(id) {
            $.ajax({
                type: "POST",
                url: "WFPermissionsRoles.aspx/DeletePermissionsRoles",// Se invoca el WebMethod Eliminar un Permiso Rol
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify({ id: id }),
                success: function (response) {
                    $('#permisoRolTable').DataTable().ajax.reload();// Recargar la tabla después de eliminar
                    alert("Permiso rol eliminado exitosamente.");
                    //alert(JSON.stringify(response));
                },
                error: function () {
                    alert("Error al eliminar el permiso rol.");
                }
            });
        }
    </script>
</asp:Content>
