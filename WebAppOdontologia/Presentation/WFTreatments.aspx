<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="WFTreatments.aspx.cs" Inherits="Presentation.WFTreatments" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link href="resources/css/datatables.min.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="card m-1">
        <div class="card-header">
            Gestión de Tratamientos
       
       
        </div>
        <div class="card-body">
            <form id="FrmTreatments" runat="server">
                <%--Id--%>
                <asp:HiddenField ID="HFTreatmentID" runat="server"></asp:HiddenField>
                <div class="row m-1">
                    <div class="col-4">
                        <%--Nombre--%>
                        <asp:Label ID="Label1" CssClass="form-label" runat="server" Text="Ingrese el nombre del tratamiento"></asp:Label>
                        <asp:DropDownList ID="DDLTreat" CssClass="form-select" runat="server">
                            <asp:ListItem Value="0">Seleccione</asp:ListItem>
                            <asp:ListItem Value="Limpieza dental">Limpieza dental</asp:ListItem>
                            <asp:ListItem Value="Profilaxis">Profilaxis</asp:ListItem>
                            <asp:ListItem Value="Fluorización">Fluorización</asp:ListItem>
                            <asp:ListItem Value="Sellantes dentales">Sellantes dentales</asp:ListItem>
                            <asp:ListItem Value="Empastes dentales">Empastes dentales</asp:ListItem>
                            <asp:ListItem Value="Restauración estética">Restauración estética</asp:ListItem>
                            <asp:ListItem Value="Tratamiento de caries">Tratamiento de caries</asp:ListItem>
                            <asp:ListItem Value="Blanqueamiento dental">Blanqueamiento dental</asp:ListItem>
                            <asp:ListItem Value="Extracción dental">Extracción dental</asp:ListItem>
                            <asp:ListItem Value="Endodoncia">Endodoncia</asp:ListItem>
                            <asp:ListItem Value="Colocación de coronas">Colocación de coronas</asp:ListItem>
                            <asp:ListItem Value="Colocación de puentes">Colocación de puentes</asp:ListItem>
                            <asp:ListItem Value="Prótesis dentales removibles">Prótesis dentales removibles</asp:ListItem>
                            <asp:ListItem Value="Prótesis dentales fijas">Prótesis dentales fijas</asp:ListItem>
                            <asp:ListItem Value="Ortodoncia con brackets">Ortodoncia con brackets</asp:ListItem>
                            <asp:ListItem Value="Ortodoncia invisible">Ortodoncia invisible</asp:ListItem>
                            <asp:ListItem Value="Cirugía de cordales">Cirugía de cordales</asp:ListItem>
                            <asp:ListItem Value="Implantes dentales">Implantes dentales</asp:ListItem>
                            <asp:ListItem Value="Tratamiento periodontal">Tratamiento periodontal</asp:ListItem>
                            <asp:ListItem Value="Control de infecciones bucales">Control de infecciones bucales</asp:ListItem>
                            <asp:ListItem Value="Otro">Otro</asp:ListItem>
                        </asp:DropDownList>
                        <%--Valida que se coloque un nombre--%>
                        <asp:RequiredFieldValidator ID="RFVName" runat="server" ControlToValidate="DDLTreat"
                            CssClass="text-danger" InitialValue="0" Display="Dynamic" ErrorMessage="Este campo es obligatorio."></asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="row m-1">
                    <div class="col-6">
                        <%--Descripción--%>
                        <asp:Label ID="Label2" runat="server" Text="Ingrese la descripción"></asp:Label>
                        <asp:TextBox ID="TBDescription" CssClass="form-control" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RFVDescription" runat="server" ControlToValidate="TBDescription"
                            CssClass="text-danger" ErrorMessage="Este campo es obligatorio."></asp:RequiredFieldValidator>
                    </div>
                    <div class="col-6">
                        <%--Observaciones--%>
                        <asp:Label ID="Label4" runat="server" Text="Ingrese las observaciones"></asp:Label>
                        <asp:TextBox ID="TBObservations" CssClass="form-control" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RFVObservations" runat="server" ControlToValidate="TBObservations"
                            CssClass="text-danger" ErrorMessage="Este campo es obligatorio."></asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="row m-1">
                    <div class="col-3">
                        <%--Fecha--%>
                        <asp:Label ID="Label3" CssClass="form-label" runat="server" Text="Ingrese la fecha"></asp:Label>
                        <asp:TextBox ID="TBDate" CssClass="form-control" runat="server" TextMode="Date"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RFVDate" runat="server" ControlToValidate="TBDate"
                            CssClass="text-danger" ErrorMessage="Este campo es obligatorio."></asp:RequiredFieldValidator>
                    </div>
                    <div class="col-3">
                        <%--FK Cita ID--%>
                        <asp:Label ID="Label5" CssClass="form-label" runat="server" Text="Seleccione una cita"></asp:Label>
                        <asp:DropDownList ID="DDLQuotes" CssClass="form-select" runat="server"></asp:DropDownList>
                        <%--Valida que el DropDownList este seleccionado con algun valor--%>
                        <asp:Label ID="LblMsgQuo" runat="server" Text="" CssClass="text-danger"></asp:Label>
                    </div>
                    <div class="col-3">
                        <%--DDL Historial--%>
                        <asp:Label ID="Label6" CssClass="form-label" runat="server" Text="Seleccione un historial"></asp:Label>
                        <asp:DropDownList ID="DDLHistory" CssClass="form-select" runat="server" AutoPostBack="true">
                        </asp:DropDownList>
                        <asp:Label ID="LblMsgHis" runat="server" Text="" CssClass="text-danger"></asp:Label>
                    </div>
                    <div class="col-3">
                        <%--DDL Auxiliar ID--%>
                        <asp:Label ID="Label7" CssClass="form-label" runat="server" Text="Seleccione un auxiliar"></asp:Label>
                        <asp:DropDownList ID="DDLAux" CssClass="form-select" runat="server"></asp:DropDownList>
                        <asp:Label ID="LblMsgAux" runat="server" Text="" CssClass="text-danger"></asp:Label>
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
                Lista de Tratamientos
           
           
            </div>
            <div class="card-body">
                <%--Lista de tratamientos--%>
                <div class="table-responsive">
                    <%--tabla de Tratamientos--%>
                    <table id="treatmentsTable" class="display" style="width: 97%">
                        <thead>
                            <tr>
                                <th>ID</th>
                                <th>Nombre</th>
                                <th>Descripcion</th>
                                <th>Fecha</th>
                                <th>Observaciones</th>
                                <th>Fk Cita</th>
                                <th>Cita</th>
                                <th>Fk historial</th>
                                <th>Historial</th>
                                <th>Fk Aux</th>
                                <th>Auxiliar</th>
                                <th>Paciente</th>
                            </tr>
                        </thead>
                        <tbody></tbody>
                    </table>
                </div>
            </div>
        </asp:Panel>
    </div>

    <script src="resources/js/datatables.min.js" type="text/javascript"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            const showEditButton = '<%= _showEditButton %>' === 'True';
            const showDeleteButton = '<%= _showDeleteButton %>' === 'True';
            $('#treatmentsTable').DataTable({
                "processing": true,
                "serverSide": false,
                "ajax": {
                    "url": "WFTreatments.aspx/ListTreatments", // WebMethod para listar tratamientos
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
                    { "data": "TreatmentID" },
                    { "data": "Name" },
                    { "data": "Description" },
                    { "data": "Date" },
                    { "data": "Observations" },
                    { "data": "FkCitaId", "visible": false },
                    { "data": "StatusQuote" },
                    { "data": "FkHistId", "visible": false },
                    { "data": "DescriptionHistory" },
                    { "data": "FkAuxId", "visible": false },
                    { "data": "AuxName" },
                    { "data": "PatientName" },
                    {
                        "data": null,
                        "render": function (data, type, row) {
                            let buttons = '';
                            if (showEditButton) {
                                buttons += `<button class="btn btn-info edit-btn" data-id="${row.TreatmentID}">Editar</button>`;
                            }
                            if (showDeleteButton) {
                                buttons += `<button class="btn btn-danger delete-btn" data-id="${row.TreatmentID}">Eliminar</button>`;
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

            // Evento para editar un tratamiento
            $('#treatmentsTable').on('click', '.edit-btn', function () {
                const rowData = $('#treatmentsTable').DataTable().row($(this).parents('tr')).data();
                loadTreatmentData(rowData);
                $('#<%= BtnSave.ClientID %>').hide();
                $('#<%= BtnUpdate.ClientID %>').show();
            });

            // Evento para eliminar un tratamiento
            $('#treatmentsTable').on('click', '.delete-btn', function () {
                const id = $(this).data('id');
                if (confirm("¿Estás seguro de que deseas eliminar este tratamiento?")) {
                    deleteTreatment(id);
                }
            });
        });

        // Función para cargar los datos del tratamiento en el formulario
        function loadTreatmentData(rowData) {
            $('#<%= HFTreatmentID.ClientID %>').val(rowData.TreatmentID);
            $('#<%= DDLTreat.ClientID %>').val(rowData.Name);
            $('#<%= TBDescription.ClientID %>').val(rowData.Description);
            $('#<%= TBDate.ClientID %>').val(rowData.Date);
            $('#<%= TBObservations.ClientID %>').val(rowData.Observations);
            $('#<%= DDLQuotes.ClientID %>').val(rowData.FkCitaId);
            $('#<%= DDLHistory.ClientID %>').val(rowData.FkHistId);
            $('#<%= DDLAux.ClientID %>').val(rowData.FkAuxId);
        }

        // Función para eliminar un tratamiento
        function deleteTreatment(id) {
            $.ajax({
                type: "POST",
                url: "WFTreatments.aspx/DeleteTreatment", // WebMethod para eliminar un tratamiento
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify({ id: id }),
                success: function (response) {
                    $('#treatmentsTable').DataTable().ajax.reload(); // Recarga la tabla después de eliminar
                    alert("Tratamiento eliminado exitosamente.");
                },
                error: function () {
                    alert("Error al eliminar el tratamiento.");
                }
            });
        }
    </script>

</asp:Content>
