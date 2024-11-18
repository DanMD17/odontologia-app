<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="WFUsers.aspx.cs" Inherits="Presentation.WFUsers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--Estilos--%>
    <link href="resources/css/dataTables.min.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="card m-1">
    <div class="card-header">
        Gestión de Usuarios
    </div>
    <div class="card-body">
        <form runat="server">
            <%--Id--%>
            <asp:HiddenField ID="HFUserId" runat="server" />
            <div class="row m-1">
                <div class="col">
                    <%--Correo--%>
                    <asp:Label ID="Label1" CssClass="form-label" runat="server" Text="Ingrese el correo"></asp:Label>
                    <asp:TextBox ID="TBMail" CssClass="form-control" runat="server" TextMode="Email"></asp:TextBox>
                </div>
                <div class="col-4">
                    <%--Contraseña--%>
                    <asp:Label ID="Label2" CssClass="form-label" runat="server" Text="Ingrese la contraseña"></asp:Label>
                    <asp:TextBox ID="TBContrasena" CssClass="form-control" runat="server" TextMode="Password"></asp:TextBox>
                </div>
                <div class="col-2">
                    <%--Estados--%>
                    <asp:Label ID="Label3" CssClass="form-label" runat="server" Text="Estado"></asp:Label>
                    <asp:DropDownList ID="DDLState" CssClass="form-select" runat="server">
                        <asp:ListItem Value="0">Seleccione</asp:ListItem>
                        <asp:ListItem Value="Activo">Activo</asp:ListItem>
                        <asp:ListItem Value="Inactivo">Inactivo</asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
            <div class="row m-1">
                <div class="col-2">
                    <%-- Fecha--%>
                    <asp:Label ID="Label4" CssClass="form-label" runat="server" Text="Fecha de creación"></asp:Label>
                    <asp:TextBox ID="TBDate" CssClass="form-control" runat="server" TextMode="Date"></asp:TextBox>
                </div>
                <div class="col-4">
                    <%--Rol--%>
                    <asp:Label ID="Label5" CssClass="form-label" runat="server" Text="Rol"></asp:Label>
                    <asp:DropDownList ID="DDLRol" CssClass="form-select" runat="server"></asp:DropDownList>
                </div>
                <div class="col">
                    <%--Empleado--%>
                    <asp:Label ID="Label6" CssClass="form-label" runat="server" Text="Empleado"></asp:Label>
                    <asp:DropDownList ID="DDLEmployees" CssClass="form-select" runat="server"></asp:DropDownList>
                </div>
            </div>
            <div class="row m-1">
                <div class="col">
                    <%--Botones--%>
                    <asp:Button ID="BtnSave" CssClass="btn btn-success" runat="server" Text="Guardar" OnClick="BtnSave_Click" />
                    <asp:Button ID="BtnUpdate" CssClass="btn btn-primary" runat="server" Text="Actualizar" OnClick="BtnUpdate_Click" />
                    <asp:Label ID="Label7" CssClass="form-label" runat="server" Text=""></asp:Label>
                </div>
            </div>
        </form>
    </div>
</div>
    <asp:Label ID="LblMsg" runat="server" Text=""></asp:Label>

    <div class="card m-1">
        <%--Panel para la gestion del Administrador--%>
        <asp:Panel ID="PanelAdmin" runat="server">
            <div class="card-header">
                Lista de Usuarios
            </div>
            <div class="card-body">
                <%--Lista de Usuarios--%>
                <div class="table-responsive">
                    <table id="usersTable" class="display" style="width: 100%">
                        <thead>
                            <tr>
                                <th>ID</th>
                                <th>Correo</th>
                                <th>Contraseña</th>
                                <th>Salt</th>
                                <th>Estado</th>
                                <th>Fecha de Creación</th>
                                <th>FkRol</th>
                                <th>Rol</th>
                                <th>FkEmpleado</th>
                                <th>Empleado</th>
                                <th>Acciones</th>
                                <!-- Columna para los botones de acción -->
                            </tr>
                        </thead>
                        <tbody>
                        </tbody>
                    </table>
                </div>
            </div>
        </asp:Panel>
    </div>
    <script src="resources/js/datatables.min.js" type="text/javascript"></script>

    <%--Usuarios--%>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#usersTable').DataTable({
                "processing": true,
                "serverSide": false,
                "ajax": {
                    "url": "WFUsers.aspx/ListUsers", // WebMethod para listar usuarios
                    "type": "POST",
                    "contentType": "application/json",
                    "data": function (d) {
                        return JSON.stringify(d); // Convierte los datos a JSON
                    },
                    "dataSrc": function (json) {
                        return json.d.data; // Devuelve los datos de los usuarios
                    }
                },
                "columns": [
                    { "data": "UserID" },
                    { "data": "Mail" },
                    { "data": "Password" },
                    { "data": "Salt", "visible": false },
                    { "data": "State" },
                    { "data": "Date" },
                    { "data": "FkRol", "visible": false },
                    { "data": "NameRol" },
                    { "data": "FkEmployee", "visible": false },
                    { "data": "NameEmployee" },
                    {
                        "data": null,
                        "render": function (row) {
                            //alert(JSON.stringify(row, null, 2));
                            return `<button class="btn btn-info edit-btn" data-id="${row.UserID}">Editar</button>`;
                            
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

            // Editar un usuario
            $('#usersTable').on('click', '.edit-btn', function () {
                const rowData = $('#usersTable').DataTable().row($(this).parents('tr')).data();
                loadUsersData(rowData);
            });

            // Eliminar un usuario
            $('#usersTable').on('click', '.delete-btn', function () {
                const id = $(this).data('id');
                if (confirm("¿Está seguro de que desea eliminar este usuario?")) {
                    deleteUser(id);
                }
            });
        });

        // Cargar los datos en los TextBox y DDL para actualizar
        function loadUsersData(rowData) {
            $('#<%= HFUserId.ClientID %>').val(rowData.UserID);
            $('#<%= TBMail.ClientID %>').val(rowData.Mail);
            $('#<%= DDLState.ClientID %>').val(rowData.State);
            $('#<%= TBDate.ClientID %>').val(rowData.Date);
            $('#<%= DDLRol.ClientID %>').val(rowData.FkRol);
            $('#<%= DDLEmployees.ClientID %>').val(rowData.FkEmployee);
        }

        // Función para eliminar un usuario
        function deleteUser(id) {
            $.ajax({
                type: "POST",
                url: "WFUsers.aspx/DeleteUser", // WebMethod para eliminar usuario
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify({ id: id }),
                success: function () {
                    $('#usersTable').DataTable().ajax.reload(); // Recargar la tabla después de eliminar el usuario
                    alert("Usuario eliminado exitosamente.");
                },
                error: function () {
                    alert("Error al eliminar el usuario.");
                }
            });
        }
    </script>
</asp:Content>
