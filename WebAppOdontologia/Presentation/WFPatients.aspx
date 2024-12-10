<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="WFPatients.aspx.cs" Inherits="Presentation.WFPatients" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--Estilos--%>
    <link href="resources/css/datatables.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="card m-1">
        <div class="card-header">
            Gestión de Pacientes
       
        </div>
        <div class="card-body">
            <form id="FrmPatients" runat="server">
                <%--Id--%>
                <asp:HiddenField ID="HFPatientsID" runat="server" />
                <div class="row m-1">
                    <div class="col-4">
                        <%--Nombre--%>
                        <asp:Label ID="Label1" CssClass="form-label" runat="server" Text="Ingrese el nombre"></asp:Label>
                        <asp:TextBox ID="TBName" CssClass="form-control" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RFVName" runat="server" ControlToValidate="TBName"
                            CssClass="text-danger" ErrorMessage="Este campo es obligatorio."></asp:RequiredFieldValidator>
                    </div>
                    <div class="col-4">
                        <%--Apellido--%>
                        <asp:Label ID="Label2" CssClass="form-label" runat="server" Text="Ingrese el apellido"></asp:Label>
                        <asp:TextBox ID="TBLastName" CssClass="form-control" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RFVLastName" runat="server" ControlToValidate="TBLastName"
                            CssClass="text-danger" ErrorMessage="Este campo es obligatorio."></asp:RequiredFieldValidator>
                    </div>
                    <div class="col-3">
                        <%--Fecha de Nacimiento--%>
                        <asp:Label ID="Label3" CssClass="form-label" runat="server" Text="Ingrese la fecha de nacimiento"></asp:Label>
                        <asp:TextBox ID="TBDateOfBirth" CssClass="form-select" runat="server" TextMode="Date"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RFVDate" runat="server" ControlToValidate="TBDateOfBirth"
                            ErrorMessage="Este campo es obligatorio" Display="Dynamic" CssClass="text-danger">
                        </asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="row m-1">
                    <div class="col-4">
                        <%--Dirección--%>
                        <asp:Label ID="Label4" CssClass="form-label" runat="server" Text="Ingrese la dirección"></asp:Label>
                        <asp:TextBox ID="TBAddress" CssClass="form-control" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RFVAddress" runat="server" ControlToValidate="TBAddress"
                            CssClass="text-danger" ErrorMessage="Este campo es obligatorio."></asp:RequiredFieldValidator>
                    </div>
                    <div class="col-4">
                        <%--Celular--%>
                        <asp:Label ID="Label5" CssClass="form-label" runat="server" Text="Ingrese el número de celular"></asp:Label>
                        <asp:TextBox ID="TBPhone" CssClass="form-control" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RFVPhone" runat="server" ControlToValidate="TBPhone"
                            CssClass="text-danger" ErrorMessage="Este campo es obligatorio."></asp:RequiredFieldValidator>
                    </div>
                    <div class="col-4">
                        <%--Correo Electrónico--%>
                        <asp:Label ID="Label6" CssClass="form-label" runat="server" Text="Ingrese el correo"></asp:Label>
                        <asp:TextBox ID="TBEmail" CssClass="form-control" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RFVEmail" runat="server" ControlToValidate="TBEmail"
                            CssClass="text-danger" ErrorMessage="Este campo es obligatorio."></asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="row m-1">
                    <div class="col">
                        <%--Botones guardar y eliminar--%>
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
                Lista de Pacientes
           
            </div>
            <div class="card-body">
                <%--Lista de Pacientes--%>
                <div class="table-responsive">
                    <%--Tabla de Pacientes--%>
                    <table id="patientsTable" class="display" style="width: 100%">
                        <thead>
                            <tr>
                                <th>ID</th>
                                <th>Nombre</th>
                                <th>Apellido</th>
                                <th>FechaDeNacimiento</th>
                                <th>Direccion</th>
                                <th>Celular</th>
                                <th>CorreoElectronico</th>
                            </tr>
                        </thead>
                        <tbody></tbody>
                    </table>
                </div>
            </div>
        </asp:Panel>
    </div>

    <script src="resources/js/datatables.min.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            const showEditButton = '<%= _showEditButton %>' === 'True';
            const showDeleteButton = '<%= _showDeleteButton %>' === 'True';
            $('#patientsTable').DataTable({
                "processing": true,
                "serverSide": false,
                "ajax": {
                    "url": "WFPatients.aspx/ListPatients", // Invoca el WebMethod para listar los pacientes
                    "type": "POST",
                    "contentType": "application/json",
                    "data": function (d) {
                        return JSON.stringify(d); // Convierte datos a JSON
                    },
                    "dataSrc": function (json) {
                        return json.d.data; // Obtiene los datos de la respuesta
                    }
                },
                "columns": [
                    { "data": "PatientID" },
                    { "data": "Name" },
                    { "data": "LastName" },
                    { "data": "DateOfBirth" },
                    { "data": "Address" },
                    { "data": "Phone" },
                    { "data": "Email" },
                    {
                        "data": null,
                        "render": function (data, type, row) {
                            let buttons = '';
                            if (showEditButton) {
                                buttons += `<button class="btn btn-info edit-btn" data-id="${row.PatientID}">Editar</button>`;
                            }
                            if (showDeleteButton) {
                                buttons += `<button class="btn btn-danger delete-btn" data-id="${row.PatientID}">Eliminar</button>`;
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

            // Evento para editar un paciente
            $('#patientsTable').on('click', '.edit-btn', function () {
                const rowData = $('#patientsTable').DataTable().row($(this).parents('tr')).data();
                loadPatientData(rowData);
                $('#<%= BtnSave.ClientID %>').hide();
                $('#<%= BtnUpdate.ClientID %>').show();
            });

            // Evento para eliminar un paciente
            $('#patientsTable').on('click', '.delete-btn', function () {
                const id = $(this).data('id');
                if (confirm("¿Estás seguro de que deseas eliminar este paciente?")) {
                    deletePatient(id);
                }
            });
        });

        // Función para cargar los datos del paciente en el formulario
        function loadPatientData(rowData) {
            $('#<%= HFPatientsID.ClientID %>').val(rowData.PatientID);
            $('#<%= TBName.ClientID %>').val(rowData.Name);
            $('#<%= TBLastName.ClientID %>').val(rowData.LastName);
            $('#<%= TBDateOfBirth.ClientID %>').val(rowData.DateOfBirth);
            $('#<%= TBAddress.ClientID %>').val(rowData.Address);
            $('#<%= TBPhone.ClientID %>').val(rowData.Phone);
            $('#<%= TBEmail.ClientID %>').val(rowData.Email);
        }

        // Función para eliminar un paciente
        function deletePatient(id) {
            $.ajax({
                type: "POST",
                url: "WFPatients.aspx/DeletePatient", // Invoca el WebMethod para eliminar un paciente
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify({ id: id }),
                success: function (response) {
                    $('#patientsTable').DataTable().ajax.reload(); // Recarga la tabla después de eliminar
                    alert("Paciente eliminado exitosamente.");
                },
                error: function () {
                    alert("Error al eliminar el paciente.");
                }
            });
        }
    </script>


</asp:Content>
