<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="WFEmployees.aspx.cs" Inherits="Presentation.WFEmployees" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--Estilos--%>
    <link href="resources/css/datatables.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="card m-1">
        <div class="card-header">
            Gestión de Empleados
        </div>
        <div class="card-body">
            <form id="FrmEmployees" runat="server">
                <%--ID--%>
                <asp:HiddenField ID="HFEmployeeID" runat="server" />
                <div class="row m-1">
                    <div class="col">
                        <%--Identficacion--%>
                        <asp:Label ID="Label1" CssClass="form-label" runat="server" Text="Ingrese la identificacion"></asp:Label>
                        <asp:TextBox ID="TBIdentification" CssClass="form-control" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RFVIdentification" runat="server" ControlToValidate="TBIdentification"
                            CssClass="text-danger" ErrorMessage="Este campo es obligatorio."></asp:RequiredFieldValidator>
                    </div>
                    <div class="col">
                        <%--Nombres--%>
                        <asp:Label ID="Label2" CssClass="form-label" runat="server" Text="Ingrese el nombre"></asp:Label>
                        <asp:TextBox ID="TBName" CssClass="form-control" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RFVName" runat="server" ControlToValidate="TBName"
                            CssClass="text-danger" ErrorMessage="Este campo es obligatorio."></asp:RequiredFieldValidator>
                    </div>
                    <div class="col">
                        <%--Apellidos--%>
                        <asp:Label ID="Label3" CssClass="form-label" runat="server" Text="Ingrese el apellido"></asp:Label>
                        <asp:TextBox ID="TBLastName" CssClass="form-control" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RFVLastName" runat="server" ControlToValidate="TBLastName"
                            CssClass="text-danger" ErrorMessage="Este campo es obligatorio."></asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="row m-1">
                    <div class="col-4">
                        <%--Telefono--%>
                        <asp:Label ID="Label4" CssClass="form-label" runat="server" Text="Ingrese el numero de celular"></asp:Label>
                        <asp:TextBox ID="TBCellPhone" CssClass="form-control" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RFVCellPhone" runat="server" ControlToValidate="TBCellPhone"
                            CssClass="text-danger" ErrorMessage="Este campo es obligatorio."></asp:RequiredFieldValidator>
                    </div>
                    <div class="col-4">
                        <%--Correo Electronico--%>
                        <asp:Label ID="Label6" CssClass="form-label" runat="server" Text="Ingrese el correo"></asp:Label>
                        <asp:TextBox ID="TBEmail" CssClass="form-control" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RFVEmail" runat="server" ControlToValidate="TBEmail"
                            CssClass="text-danger" ErrorMessage="Este campo es obligatorio."></asp:RequiredFieldValidator>
                    </div>
                    <div class="col-4">
                        <%--Direccion--%>
                        <asp:Label ID="Label5" CssClass="form-label" runat="server" Text="Ingrese la direccion"></asp:Label>
                        <asp:TextBox ID="TBAddress" CssClass="form-control" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RFVAddress" runat="server" ControlToValidate="TBAddress"
                            CssClass="text-danger" ErrorMessage="Este campo es obligatorio."></asp:RequiredFieldValidator>
                    </div>
                </div>
                <%--Botones guardar y actualizar--%>
                <div class="row m-1">
                    <div class="col">
                        <asp:Button ID="BtnSave" CssClass="btn btn-success" runat="server" Text="Guardar" OnClick="BtnSave_Click" />
                        <asp:Button ID="BtnUpdate" CssClass="btn btn-primary"  runat="server" Text="Actualizar" OnClick="BtnUpdate_Click" />
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
                Lista de Empleados
            </div>
            <div class="card-body">
                <%--Lista de empleados--%>
                <div class="table-responsive">
                    <%--Tabla de Empleados--%>
                    <table id="employeesTable" class="display" style="width: 100%">
                        <thead>
                            <tr>
                                <th>EmpleadoID</th>
                                <th>Identificacion</th>
                                <th>Nombre</th>
                                <th>Apellidos</th>
                                <th>Celular</th>
                                <th>Correo</th>
                                <th>Direccion</th>
                            </tr>
                        </thead>
                        <tbody></tbody>
                    </table>
                </div>
            </div>
        </asp:Panel>
    </div>

    <script src="resources/js/datatables.min.js" type="text/javascript"></script>

    <%-- Empleado --%>
    <script type="text/javascript">
        $(document).ready(function () {
            const showEditButton = '<%= _showEditButton %>' === 'True';
            const showDeleteButton = '<%= _showDeleteButton %>' === 'True';
            $('#employeesTable').DataTable({
                "processing": true,
                "serverSide": false,
                "ajax": {
                    "url": "WFEmployees.aspx/ListEmployees",// Se invoca el WebMethod Listar empleados
                    "type": "POST",
                    "contentType": "application/json",
                    "data": function (d) {
                        return JSON.stringify(d);// Convierte los datos a JSON
                    },
                    "dataSrc": function (json) {
                        return json.d.data;// Obtiene la lista de empleados del resultado
                    }
                },
                "columns": [
                    { "data": "EmployeeID" },
                    { "data": "Identification" },
                    { "data": "Name" },
                    { "data": "LastName" },
                    { "data": "CellPhone" },
                    { "data": "Email" },
                    { "data": "Address" },
                    {
                        "data": null,
                        "render": function (data, type, row) {
                            let buttons = '';
                            if (showEditButton) {
                                buttons += `<button class="btn btn-info edit-btn" data-id="${row.EmployeeID}">Editar</button>`;
                            }
                            if (showDeleteButton) {
                                buttons += `<button class="btn btn-danger delete-btn" data-id="${row.EmployeeID}">Eliminar</button>`;
                            }
                            return buttons;
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

            // Editar un empleado
            $('#employeesTable').on('click', '.edit-btn', function () {
                const rowData = $('#employeesTable').DataTable().row($(this).parents('tr')).data();
                loadEmployeesData(rowData);
                $('#<%= BtnSave.ClientID %>').hide();
                $('#<%= BtnUpdate.ClientID %>').show();
            });

            // Eliminar un empleado
            $('#employeesTable').on('click', '.delete-btn', function () {
                const id = $(this).data('id');// Obtener el ID del empleado
                if (confirm("¿Estás seguro de que deseas eliminar este empleado?")) {
                    deleteEmployee(id);// Invoca a la función para eliminar el empleado
                }
            });
        });

        // Cargar los datos en los TextBox y DDL para actualizar
        function loadEmployeesData(rowData) {
            $('#<%= HFEmployeeID.ClientID %>').val(rowData.EmployeeID);
            $('#<%= TBIdentification.ClientID %>').val(rowData.Identification);
            $('#<%= TBName.ClientID %>').val(rowData.Name);
            $('#<%= TBLastName.ClientID %>').val(rowData.LastName);
            $('#<%= TBCellPhone.ClientID %>').val(rowData.CellPhone);
            $('#<%= TBEmail.ClientID %>').val(rowData.Email);
            $('#<%= TBAddress.ClientID %>').val(rowData.Address);
        }

        // Función para eliminar un empleado
        function deleteEmployee(id) {
            $.ajax({
                type: "POST",
                url: "WFEmployees.aspx/DeleteEmployee",// Se invoca el WebMethod Eliminar un empleado
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify({ id: id }),
                success: function (response) {
                    $('#employeesTable').DataTable().ajax.reload();// Recargar la tabla después de eliminar
                    alert("Empleado eliminado exitosamente.");
                },
                error: function () {
                    alert("Error al eliminar el empleado.");
                }
            });
        }
    </script>
</asp:Content>
