<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="WFDiagnosis.aspx.cs" Inherits="Presentation.WFDiagnosis" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--Estilos--%>
    <link href="resources/css/datatables.min.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="card m-1">
        <div class="card-header">
            Gestión de Diagnosticos
       
        </div>
        <div class="card-body">
            <form id="FrmDiagnosis" runat="server">
                <%--Id--%>
                <asp:HiddenField ID="HFDiagnosisID" runat="server" />

                <div class="row m-1">
                    <div class="col">
                        <%-- Formulario de Diagnóstico --%>
                        <asp:Label ID="Label1" CssClass="form-label" runat="server" Text="Ingrese la descripción del diagnóstico"></asp:Label>
                        <asp:TextBox ID="TBDescription" CssClass="form-control" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RFVDescription" runat="server" ControlToValidate="TBDescription"
                            CssClass="text-danger" ErrorMessage="Este campo es obligatorio."></asp:RequiredFieldValidator>
                    </div>
                    <div class="col-2">
                        <%-- Fecha --%>
                        <asp:Label ID="Label2" CssClass="form-label" runat="server" Text="Ingrese la fecha"></asp:Label>
                        <asp:TextBox ID="TBDate" CssClass="form-control" runat="server" TextMode="Date"></asp:TextBox>
                    </div>
                </div>
                <div class="row m-1">
                    <div class="col">
                        <%-- Observaciones --%>
                        <asp:Label ID="Label3" CssClass="form-label" runat="server" Text="Ingrese las observaciones"></asp:Label>
                        <asp:TextBox ID="TBObservations" CssClass="form-control" runat="server" TextMode="MultiLine" Rows="4"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RFVObservations" runat="server" ControlToValidate="TBObservations"
                            CssClass="text-danger" ErrorMessage="Este campo es obligatorio."></asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="row m-1">
                    <div class="col-3">
                        <%-- Seleccionar Historial Clínico --%>
                        <asp:Label ID="Label5" CssClass="form-label" runat="server" Text="Historial clínico"></asp:Label>
                        <asp:DropDownList ID="DDLClinicalHistory" CssClass="form-select" runat="server"></asp:DropDownList>
                        <asp:Label ID="LblMsgCH" runat="server" Text="" CssClass="text-danger"></asp:Label>
                    </div>
                    <div class="col-3">
                        <%-- Seleccionar Cita --%>
                        <asp:Label ID="Label4" CssClass="form-label" runat="server" Text="Cita"></asp:Label>
                        <asp:DropDownList ID="DDLQuotes" CssClass="form-select" runat="server"></asp:DropDownList>
                        <asp:Label ID="LblMsgQuo" runat="server" Text="" CssClass="text-danger"></asp:Label>
                    </div>
                </div>
                <%-- Botón guardar --%>
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
                Lista de diagnosticos
           
            </div>
            <div class="card-body">
                <%--Lista de Diagnosticos--%>
                <div class="table-responsive">
                    <%--Tabla de Diagnosticos--%>
                    <table id="diagnosisTable" class="display" style="width: 100%">
                        <thead>
                            <tr>
                                <th>ID</th>
                                <th>Descripcion</th>
                                <th>Fecha</th>
                                <th>Observaciones</th>
                                <th>FkCitas</th>
                                <th>Citas</th>
                                <th>FkHistorialClinico</th>
                                <th>Historial Clinico</th>
                                <th>FkPatients</th>
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

    <%--Diagnosticos--%>
    <script type="text/javascript">
        $(document).ready(function () {
            const showEditButton = '<%= _showEditButton %>' === 'True';
            const showDeleteButton = '<%= _showDeleteButton %>' === 'True';
            $('#diagnosisTable').DataTable({
                "processing": true,
                "serverSide": false,
                "ajax": {
                    "url": "WFDiagnosis.aspx/ListDiagnosis",// Se invoca el WebMethod Listar diagnosticos
                    "type": "POST",
                    "contentType": "application/json",
                    "data": function (d) {
                        return JSON.stringify(d);// Convierte los datos a JSON
                    },
                    "dataSrc": function (json) {
                        return json.d.data;// Obtiene la lista de productos del resultado
                    }
                },
                "columns": [
                    { "data": "DiagID" },
                    { "data": "Description" },
                    { "data": "Date" },
                    { "data": "Observations" },
                    { "data": "FkQuotes", "visible": false },
                    { "data": "QuoteDate" },
                    { "data": "FkClinicalHistory", "visible": false },
                    { "data": "DescriptionCH" },
                    { "data": "FkPatients", "visible": false },
                    { "data": "NamePatient" },

                    {
                        "data": null,
                        "render": function (data, type, row) {
                            let buttons = '';
                            if (showEditButton) {
                                buttons += `<button class="btn btn-info edit-btn" data-id="${row.DiagID}">Editar</button>`;
                            }
                            if (showDeleteButton) {
                                buttons += `<button class="btn btn-danger delete-btn" data-id="${row.DiagID}">Eliminar</button>`;
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

            // Editar un diagnostico
            $('#diagnosisTable').on('click', '.edit-btn', function () {
                const rowData = $('#diagnosisTable').DataTable().row($(this).parents('tr')).data();
                loadDiagnosisData(rowData);
                $('#<%= BtnSave.ClientID %>').hide();
                $('#<%= BtnUpdate.ClientID %>').show();

            });

            // Eliminar un producto
            $('#diagnosisTable').on('click', '.delete-btn', function () {
                const id = $(this).data('id');// Obtener el ID del diagnostico
                if (confirm("¿Estás seguro de que deseas eliminar este Diagnostico?")) {
                    deleteDiagnosis(id);// Invoca a la función para eliminar el diagnostico
                }
            });
        });

        // Cargar los datos en los TextBox y DDL para actualizar
        function loadDiagnosisData(rowData) {
            $('#<%= HFDiagnosisID.ClientID %>').val(rowData.DiagID);
            $('#<%= TBDescription.ClientID %>').val(rowData.Description);
            $('#<%= TBDate.ClientID %>').val(rowData.Date);
            $('#<%= TBObservations.ClientID %>').val(rowData.Observations);
            $('#<%= DDLQuotes.ClientID %>').val(rowData.FkQuotes);
            $('#<%= DDLClinicalHistory.ClientID %>').val(rowData.FkClinicalHistory);

        }

        // Función para eliminar un producto
        function deleteDiagnosis(id) {
            $.ajax({
                type: "POST",
                url: "WFDiagnosis.aspx/DeleteDiagnosis",// Se invoca el WebMethod Eliminar un Producto
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify({ id: id }),
                success: function (response) {
                    $('#diagnosisTable').DataTable().ajax.reload();// Recargar la tabla después de eliminar
                    alert("Diagnostico eliminado exitosamente.");
                },
                error: function () {
                    alert("Error al eliminar el producto.");
                }
            });
        }
    </script>
</asp:Content>
