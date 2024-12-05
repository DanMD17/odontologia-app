<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="WFRoles.aspx.cs" Inherits="Presentation.WFRoles" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <%--Estilos--%>
    <link href="resources/css/datatables.min.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="card m-1">
        <div class="card-header">
            Gestión de Roles
        </div>
        <div class="card-body">
            <form id="FrmRoles" runat="server">
                <%--Id--%>
                <asp:HiddenField ID="HFRolID" runat="server" />
                <div class="row m-1">
                    <div class="col-2">
                        <%--Nombre del Rol--%>
                        <asp:Label ID="Label1" CssClass="form-label" runat="server" Text="Ingrese el nombre del rol"></asp:Label>
                        <asp:TextBox ID="TBNombreRol" CssClass="form-control" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RFVNombreRol" runat="server" ControlToValidate="TBNombreRol"
                            CssClass="text-danger" ErrorMessage="Este campo es obligatorio."></asp:RequiredFieldValidator>
                    </div>
                    <div class="col">
                        <%--Descripción del Rol--%>
                        <asp:Label ID="Label2" CssClass="form-label" runat="server" Text="Ingrese la descripción"></asp:Label>
                        <asp:TextBox ID="TBDescripcionRol" CssClass="form-control" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RFVDescription" runat="server" ControlToValidate="TBDescripcionRol"
                            CssClass="text-danger" ErrorMessage="Este campo es obligatorio."></asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="row m-1">
                    <div class="col">
                        <%--Botones--%>
                        <asp:Button ID="BtnSave" CssClass="btn btn-success" runat="server" Text="Guardar" OnClick="BtnSave_Click" />
                        <asp:Button ID="BtnUpdate" CssClass="btn btn-primary" runat="server" Text="Actualizar" OnClick="BtnUpdate_Click" />
                        <asp:Label ID="LblMsg" runat="server" Text=""></asp:Label>

                    </div>
                </div>
            </form>
        </div>
    </div>

    <div class="card m-1">
        <%--Panel para la gestion del Administrador--%>
        <asp:Panel ID="PanelAdmin" runat="server">
            <div class="card-header">
                Lista de Roles
            </div>
            <div class="card-body">
                <%--Lista de Roles--%>
                <div class="table-responsive">
                    <%--Tabla de Roles--%>
                    <table id="rolesTable" class="display" style="width: 100%">
                        <thead>
                            <tr>
                                <th>RolID</th>
                                <th>Nombre</th>
                                <th>Descripción</th>
                            </tr>
                        </thead>
                        <tbody></tbody>
                    </table>
                </div>
            </div>
        </asp:Panel>
    </div>

    <script src="resources/js/datatables.min.js" type="text/javascript"></script>

    <%--Script para Roles--%>
    <script type="text/javascript">
        $(document).ready(function () {
            <%--const showEditButton = '<%= _showEditButton %>' === 'True';
            const showDeleteButton = '<%= _showDeleteButton %>' === 'True';--%>
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
                    { "data": "Name" },
                    { "data": "Description" },
                    {
                        "data": null,
                        "render": function (data, type, row) {
                            //let buttons = '';
                            //if (showEditButton) {
                            //    buttons += `<button class="btn btn-info edit-btn" data-id="${row.ProductID}">Editar</button>`;
                            //}
                            //if (showDeleteButton) {
                            //    buttons += `<button class="btn btn-danger delete-btn" data-id="${row.ProductID}">Eliminar</button>`;
                            //}
                            //return buttons;
                            return `<button class="btn btn-info edit-btn" data-id="${row.RolID}">Editar</button>
                            <button class="btn btn-danger delete-btn" data-id="${row.RolID}">Eliminar</button>`;
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
            $('#<%= TBNombreRol.ClientID %>').val(rowData.Name);
            $('#<%= TBDescripcionRol.ClientID %>').val(rowData.Description);
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
