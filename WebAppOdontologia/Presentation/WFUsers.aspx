<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="WFUsers.aspx.cs" Inherits="Presentation.WFUsers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--Estilos--%>
    <link href="resources/css/dataTables.min.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

        <%--Id--%>
        <asp:HiddenField ID="HFUserId" runat="server" />

        <%--Correo--%>
        <asp:Label ID="Label1" runat="server" Text="Ingrese el correo"></asp:Label>
        <asp:TextBox ID="TBMail" runat="server" TextMode="Email"></asp:TextBox><br />

        <%--Contraseña--%>
        <asp:Label ID="Label2" runat="server" Text="Ingrese la contraseña"></asp:Label>
        <asp:TextBox ID="TBContrasena" runat="server" TextMode="Password"></asp:TextBox><br />

        <%--Estado--%>
        <asp:Label ID="Label3" runat="server" Text="Estado"></asp:Label>
        <asp:DropDownList ID="DDLState" runat="server">
            <asp:ListItem Value="0">Seleccione</asp:ListItem>
            <asp:ListItem Value="Activo">Activo</asp:ListItem>
            <asp:ListItem Value="Inactivo">Inactivo</asp:ListItem>
        </asp:DropDownList><br />

        <%--Fecha de creación--%>
        <asp:Label ID="Label4" runat="server" Text="Fecha de creación"></asp:Label>
        <asp:TextBox ID="TBDate" runat="server" TextMode="Date"></asp:TextBox><br />

        <%--Rol--%>
        <asp:Label ID="Label5" runat="server" Text="Rol"></asp:Label>
        <asp:DropDownList ID="DDLRol" runat="server"></asp:DropDownList><br />

        <%--Empleado--%>
        <asp:Label ID="Label6" runat="server" Text="Empleado"></asp:Label>
        <asp:DropDownList ID="DDLEmployees" runat="server"></asp:DropDownList><br />

        <%--Botones--%>
        <asp:Button ID="BtnSave" runat="server" Text="Guardar" OnClick="BtnSave_Click" />
        <asp:Button ID="BtnUpdate" runat="server" Text="Actualizar" OnClick="BtnUpdate_Click" /><br />

        <asp:Label ID="LblMsg" runat="server" Text=""></asp:Label>


    <%--Lista de Usuarios--%>
    <h2>Lista de Usuarios</h2>
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
                <th>Acciones</th> <!-- Columna para los botones de acción -->
            </tr>
        </thead>
        <tbody>
        </tbody>
    </table>
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
                    { "data": "Salt" },
                    { "data": "State" },
                    { "data": "Date" },
                    { "data": "FkRol", "visible": false },
                    { "data": "NameRol" },
                    { "data": "FkEmployee", "visible": false },
                    { "data": "NameEmployee" },
                    {
                        "data": null,
                        "render": function (row) {
                            return `<button class="edit-btn" data-id="${row.UserID}">Editar</button>`;
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
