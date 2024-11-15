<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="WFPatients.aspx.cs" Inherits="Presentation.WFPatients" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--Estilos--%>
    <link href="resources/css/datatables.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <%--Formulario para Pacientes--%>

    <%--Id--%>
    <asp:HiddenField ID="HFPatientsID" runat="server" />

    <%--Nombre--%>
    <asp:Label ID="Label1" runat="server" Text="Ingrese el nombre"></asp:Label>
    <asp:TextBox ID="TBName" runat="server"></asp:TextBox>
    <br />
    <%--Apellido--%>
    <asp:Label ID="Label2" runat="server" Text="Ingrese el apellido"></asp:Label>
    <asp:TextBox ID="TBLastName" runat="server"></asp:TextBox>
    <br />
    <%--Fecha de Nacimiento--%>
    <asp:Label ID="Label3" runat="server" Text="Ingrese la fecha de nacimiento"></asp:Label>
    <asp:TextBox ID="TBDateOfBirth" runat="server" TextMode="Date"></asp:TextBox>
    <br />
    <%--Dirección--%>
    <asp:Label ID="Label4" runat="server" Text="Ingrese la dirección"></asp:Label>
    <asp:TextBox ID="TBAddress" runat="server"></asp:TextBox>
    <br />
    <%--Celular--%>
    <asp:Label ID="Label5" runat="server" Text="Ingrese el número de celular"></asp:Label>
    <asp:TextBox ID="TBPhone" runat="server"></asp:TextBox>
    <br />
    <%--Correo Electrónico--%>
    <asp:Label ID="Label6" runat="server" Text="Ingrese el correo"></asp:Label>
    <asp:TextBox ID="TBEmail" runat="server"></asp:TextBox>
    <br />

    <%--Botones--%>
    <div>
        <asp:Button ID="BtnSave" runat="server" Text="Guardar" OnClick="BtnSave_Click" />
        <asp:Button ID="BtnUpdate" runat="server" Text="Actualizar" />
        <asp:Label ID="LblMsg" runat="server" Text=""></asp:Label>
    </div>
    <br />

    <%--Lista de Pacientes--%>
    <h2>Lista de Pacientes</h2>
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
        <tbody>
        </tbody>
    </table>

    <script src="resources/js/datatables.min.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {
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
                            return `<button class="edit-btn" data-id="${row.PatientID}">Editar</button>
                                <button class="delete-btn" data-id="${row.PatientID}">Eliminar</button>`;
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
