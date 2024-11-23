<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="WFClinicalHistory.aspx.cs" Inherits="Presentation.WFClinicalHistory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <%--Estilos--%>
    <link href="resources/css/datatables.min.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="card m-1">
        <div class="card-header">
            Gestión de Historial clinico
        </div>
        <div class="card-body">
            <form id="FrmClinicalHistory" runat="server">
                <%--Formulario de Historial Clínico--%>
                <%-- ID --%>
                <asp:HiddenField ID="HFClinicalHistoryID" runat="server" />
                <div class="row m-1">
                    <div class="col-2">
                        <%--Fecha de creación--%>
                        <asp:Label ID="Label1" CssClass="form-label" runat="server" Text="Ingrese la fecha de creación"></asp:Label>
                        <asp:TextBox ID="TBCreacionDate" CssClass="form-control" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RFVDate" runat="server" ControlToValidate="TBCreacionDate"
                            CssClass="text-danger" ErrorMessage="Este campo es obligatorio."></asp:RequiredFieldValidator>
                    </div>
                    <div class="col">
                        <%--Descripción general--%>
                        <asp:Label ID="Label2" CssClass="form-label" runat="server" Text="Ingrese la descripción general"></asp:Label>
                        <asp:TextBox ID="TBOverview" CssClass="form-control" runat="server" TextMode="MultiLine" Rows="4"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RFVOverview" runat="server" ControlToValidate="TBOverview"
                            CssClass="text-danger" ErrorMessage="Este campo es obligatorio."></asp:RequiredFieldValidator>
                    </div>
                    <div class="col-2">
                        <%--Seleccionar paciente--%>
                        <asp:Label ID="Label3" CssClass="form-label" runat="server" Text="Seleccione un paciente"></asp:Label>
                        <asp:DropDownList ID="DDLPatient" CssClass="form-select" runat="server"></asp:DropDownList>
                    </div>
                </div>
                <%--Botones guardar y actualizar--%>
                <div class="row m-1">
                    <div class="col">
                        <asp:Button ID="BtnSave" runat="server" Text="Guardar" OnClick="BtnSave_Click" />
                        <asp:Button ID="BtnUpdate" runat="server" Text="Actualizar" OnClick="BtnUpdate_Click" />
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
                Lista de Historiales clinicos
            </div>
            <div class="card-body">
                <%--Lista de Historiales Clinico--%>
                <div class="table-responsive">
                    <%--Tabla de Historiales Clinicos--%>
                    <table id="ClinicalHistoryTable" class="display" style="width: 100%">
                        <thead>
                            <tr>
                                <th>HistorialID</th>
                                <th>FechaDeCreacion</th>
                                <th>DescripcionGeneral</th>
                                <th>FkPaciente</th>
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

    <%--Historiales Clinicos--%>
    <script type="text/javascript">
        $(document).ready(function () {
            const showEditButton = '<%= _showEditButton %>' === 'True';
            const showDeleteButton = '<%= _showDeleteButton %>' === 'True';
            $('#ClinicalHistoryTable').DataTable({
                "processing": true,
                "serverSide": false,
                "ajax": {
                    "url": "WFClinicalHistory.aspx/ListClinicalHistory",// Se invoca el WebMethod Listar historial clinico
                    "type": "POST",
                    "contentType": "application/json",
                    "data": function (d) {
                        return JSON.stringify(d);// Convierte los datos a JSON
                    },
                    "dataSrc": function (json) {
                        return json.d.data;// Obtiene la lista de historiales clinicos del resultado
                    }
                },
                "columns": [
                    { "data": "ClinicalHistoryID" },
                    { "data": "CreacionDate" },
                    { "data": "Overview" },
                    { "data": "FkPatient", "visible": false },
                    { "data": "NamePatient" },
                    {
                        "data": null,
                        "render": function (data, type, row) {
                            let buttons = '';
                            if (showEditButton) {
                                buttons += `<button class="btn btn-info edit-btn" data-id="${row.ClinicalHistoryID}">Editar</button>`;
                            }
                            if (showDeleteButton) {
                                buttons += `<button class="btn btn-danger delete-btn" data-id="${row.ClinicalHistoryID}">Eliminar</button>`;
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

            // Editar un historial clinico
            $('#ClinicalHistoryTable').on('click', '.edit-btn', function () {
                //const id = $(this).data('id');
                const rowData = $('#ClinicalHistoryTable').DataTable().row($(this).parents('tr')).data();
                //alert(JSON.stringify(rowData, null, 2));
                loadClinicalHistoryData(rowData);
            });

            // Eliminar un historial clinico
            $('#ClinicalHistoryTable').on('click', '.delete-btn', function () {
                const id = $(this).data('id');// Obtener el ID del historial clinico
                if (confirm("¿Estás seguro de que deseas eliminar el Historial Clinico?")) {
                    deleteClinicalHistory(id);// Invoca a la función para eliminar el historial clinico
                }
            });
        });

        // Función para cargar los datos en los TextBox y DDL para actualizar
        function loadClinicalHistoryData(rowData) {
            $('#<%= HFClinicalHistoryID.ClientID %>').val(rowData.ClinicalHistoryID);
            $('#<%= TBCreacionDate.ClientID %>').val(rowData.CreacionDate);
            $('#<%= TBOverview.ClientID %>').val(rowData.Overview);
            $('#<%= DDLPatient.ClientID %>').val(rowData.FkPatient);
        }

        // Función para eliminar un historial clinico 
        function deleteClinicalHistory(id) {
            $.ajax({
                type: "POST",
                url: "WFClinicalHistory.aspx/DeleteClinicalHistory",// Se invoca el WebMethod Eliminar un hsitorial clinico
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify({ id: id }),
                success: function (response) {
                    $('#ClinicalHistoryTable').DataTable().ajax.reload();// Recargar la tabla después de eliminar
                    alert("Historial Clinico eliminado exitosamente.");
                },
                error: function () {
                    alert("Error al eliminar el Historial Clinico.");
                }
            });
        }
    </script>
</asp:Content>


