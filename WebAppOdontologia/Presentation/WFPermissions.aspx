<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="WFPermissions.aspx.cs" Inherits="Presentation.WFPermissions" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--estilos--%>
    <link href="resources/css/datatables.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />

        <%--ID--%>
        <asp:HiddenField ID="HFPermissionID" runat="server" />
        <br />
        <%-- Nombre Permiso--%>
        <asp:Label ID="Label1" runat="server" Text="Ingrese Nombre de Permiso"></asp:Label>
        <asp:TextBox ID="TBName" runat="server"></asp:TextBox>
        <br />

        <%-- Descripción Permiso--%>
        <asp:Label ID="Label2" runat="server" Text="Ingrese la descripcion"></asp:Label>
        <asp:TextBox ID="TBDescription" runat="server"></asp:TextBox>
        <br />

        <%-- Botones Guardar y actualizar --%>
        <div>
            <asp:Button ID="BtnSave" runat="server" Text="Guardar" OnClick="BtnSave_Click" />
            <asp:Button ID="BtnUpdate" runat="server" Text="Actualizar" OnClick="BtnUpdate_Click" />
            <asp:Label ID="LblMsg" runat="server" Text=""></asp:Label>
        </div>

    <br />
    <%--Lista de Permisos --%>

    <h2>Lista de Permisos </h2>
    <table id="permissionTable" class="display" style="width: 100%">
        <thead>
            <tr>
                <th>ID</th>
                <th>NombrePermiso</th>
                <th>Descripcion</th>
            </tr>
        </thead>
        <tbody>
        </tbody>
    </table>

    <script src="resources/js/datatables.min.js" type="text/javascript"></script>

    <%--Roles--%>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#permissionTable').DataTable({
                "processing": true,
                "serverSide": false,
                "ajax": {
                    "url": "WFPermissions.aspx/ListPermissions",// Se invoca el WebMethod Listar Permisos
                    "type": "POST",
                    "contentType": "application/json",
                    "data": function (d) {
                        return JSON.stringify(d);// Convierte los datos a JSON
                    },
                    "dataSrc": function (json) {
                        return json.d.data;// Obtiene la lista de Permisos del resultado
                    }
                },
                "columns": [
                    { "data": "PermissionID" },
                    { "data": "Name" },
                    { "data": "Description" },

                    {
                        "data": null,
                        "render": function (data, type, row) {
                            return `<button class="edit-btn" data-id="${row.PermissionID}">Editar</button>
                             <button class="delete-btn" data-id="${row.PermissionID}">Eliminar</button>`;
                        }
                    }
                ],
                "language": {
                    "lengthMenu": "Mostrar MENU registros por página",
                    "zeroRecords": "No se encontraron resultados",
                    "info": "Mostrando página PAGE de PAGES",
                    "infoEmpty": "No hay registros disponibles",
                    "infoFiltered": "(filtrado de MAX registros totales)",
                    "search": "Buscar:",
                    "paginate": {
                        "first": "Primero",
                        "last": "Último",
                        "next": "Siguiente",
                        "previous": "Anterior"
                    }
                }

            });

            // Editar un Permiso 
            $('#permissionTable').on('click', '.edit-btn', function () {
                //const id = $(this).data('id');
                const rowData = $('#permissionTable').DataTable().row($(this).parents('tr')).data();
                //alert(JSON.stringify(rowData, null, 2));
                loadPermissionData(rowData);
            });

            // Eliminar un Permiso
            $('#permissionTable').on('click', '.delete-btn', function () {
                const id = $(this).data('id');// Obtener el ID del Permiso
                if (confirm("¿Estás seguro de que deseas eliminar este Permiso ?")) {
                    deletepermission(id);// Invoca a la función para eliminar el permiso
                }
            });
        });

        // Cargar los datos en los TextBox 
        function loadPermissionData(rowData) {
            $('#<%= HFPermissionID.ClientID %>').val(rowData.PermissionID);
            $('#<%= TBName.ClientID %>').val(rowData.Name);
            $('#<%= TBDescription.ClientID %>').val(rowData.Description);

        }

        // Función para eliminar un Permiso
        function deletepermission(id) {
            $.ajax({
                type: "POST",
                url: "WFPermissions.aspx/DeletePermission",// Se invoca el WebMethod Eliminar un Permiso
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify({ id: id }),
                success: function (response) {
                    $('#permissionTable').DataTable().ajax.reload();// Recargar la tabla después de eliminar
                    alert("Permiso eliminado exitosamente.");
                },
                error: function () {
                    alert("Error al eliminar el Permiso .");
                }
            });
        }
    </script>
</asp:Content>