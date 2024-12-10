<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="WFQuotes.aspx.cs" Inherits="Presentation.WFQuotes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="resources/css/datatables.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="card m-1">
        <div class="card-header">
            Gestión de Citas
       
        </div>
        <div class="card-body">
            <form id="FrmQuotes" runat="server">
                <%--Id--%>
                <asp:HiddenField ID="HFQuoteID" runat="server"></asp:HiddenField>
                <div class="row m-1">
                    <div class="col-4">
                        <asp:Label ID="Label1" CssClass="form-label" runat="server" Text="Ingrese la fecha de la cita"></asp:Label>
                        <asp:TextBox ID="TBDate" CssClass="form-select" runat="server" TextMode="Date"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RFVDate" runat="server" ControlToValidate="TBDate"
                            ErrorMessage="Este campo es obligatorio" Display="Dynamic" CssClass="text-danger">
                        </asp:RequiredFieldValidator>
                    </div>
                    <div class="col-4">
                        <asp:Label ID="Label2" CssClass="form-label" runat="server" Text="Ingrese la hora de la cita"></asp:Label>
                        <asp:TextBox ID="TBTime" CssClass="form-select" runat="server" TextMode="Time"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RFVTime" runat="server" ControlToValidate="TBTime"
                            ErrorMessage="Este campo es obligatorio" Display="Dynamic" CssClass="text-danger">
                        </asp:RequiredFieldValidator>
                    </div>
                    <div class="col-4">
                        <asp:Label ID="Label3" CssClass="form-label" runat="server" Text="Especifique el estado de la cita"></asp:Label>
                        <asp:DropDownList ID="DDLState" CssClass="form-select" runat="server">
                            <asp:ListItem Value="0">Seleccione</asp:ListItem>
                            <asp:ListItem Value="Activo">Activo</asp:ListItem>
                            <asp:ListItem Value="Inactivo">Inactivo</asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RFVState" runat="server" ControlToValidate="DDLState"
                            ErrorMessage="Este campo es obligatorio" InitialValue="0" Display="Dynamic" CssClass="text-danger">
                        </asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="row m-1">
                    <div class="col-3">
                        <asp:Label ID="Label4" CssClass="form-label" runat="server" Text="Seleccione un paciente"></asp:Label>
                        <asp:DropDownList ID="DDLPatient" CssClass="form-select" runat="server">
                            <asp:ListItem Value="0">Seleccione</asp:ListItem>
                        </asp:DropDownList>
                        <asp:Label ID="LblMsgPac" runat="server" Text="" CssClass="text-danger"></asp:Label>

                    </div>
                    <div class="col-3">
                        <asp:Label ID="Label5" CssClass="form-label" runat="server" Text="Seleccione un odontologo"></asp:Label>
                        <asp:DropDownList ID="DDLDentist" CssClass="form-select" runat="server">
                            <asp:ListItem Value="0">Seleccione</asp:ListItem>
                        </asp:DropDownList>
                        <asp:Label ID="LblMsgOdo" runat="server" Text="" CssClass="text-danger"></asp:Label>
                    </div>
                </div>
                <div class="row m-1">
                    <div class="col">
                        <%--Botones guardar y actualizar--%>
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
                Lista de citas
           
            </div>
            <div class="card-body">
                <%--Lista de Citas--%>
                <div class="table-responsive">
                    <%--Tabla de Citas--%>
                    <table id="quotesTable" class="display" style="width: 100%">
                        <thead>
                            <tr>
                                <th>ID</th>
                                <th>Fecha</th>
                                <th>Hora</th>
                                <th>Estado</th>
                                <th>FkPaciente</th>
                                <th>Paciente</th>
                                <th>FkOdontologo</th>
                                <th>Especialidad del odontologo</th>
                                 <th>Nombre del odontologo</th>
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
            $('#quotesTable').DataTable({
                "processing": true,
                "serverSide": false,
                "ajax": {
                    "url": "WFQuotes.aspx/ListQuotes", // WebMethod para listar citas
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
                    { "data": "QuoteID" },
                    { "data": "Date" },
                    { "data": "Time" },
                    { "data": "Status" },
                    { "data": "FkPatientId", "visible": false },
                    { "data": "NamePatient" },
                    { "data": "FkDentistId", "visible": false },
                    { "data": "SpecialtyDentist" },
                    { "data": "NameDentist" },

                    {
                        "data": null,
                        "render": function (data, type, row) {
                            let buttons = '';
                            if (showEditButton) {
                                buttons += `<button class="btn btn-info edit-btn" data-id="${row.QuoteID}">Editar</button>`;
                            }
                            if (showDeleteButton) {
                                buttons += `<button class="btn btn-danger delete-btn" data-id="${row.QuoteID}">Eliminar</button>`;
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

            // Evento para editar una cita
            $('#quotesTable').on('click', '.edit-btn', function () {
                const rowData = $('#quotesTable').DataTable().row($(this).parents('tr')).data();
                loadQuoteData(rowData);
                $('#<%= BtnSave.ClientID %>').hide();
                $('#<%= BtnUpdate.ClientID %>').show();
            });

            // Evento para eliminar una cita
            $('#quotesTable').on('click', '.delete-btn', function () {
                const id = $(this).data('id');
                if (confirm("¿Estás seguro de que deseas eliminar esta cita?")) {
                    deleteQuote(id);
                }
            });
        });

        // Función para cargar los datos de la cita en el formulario
        function loadQuoteData(rowData) {
            $('#<%= HFQuoteID.ClientID %>').val(rowData.QuoteID);
            $('#<%= TBDate.ClientID %>').val(rowData.Date);
            $('#<%= TBTime.ClientID %>').val(rowData.Time);
            $('#<%= DDLState.ClientID %>').val(rowData.Status);
            $('#<%= DDLPatient.ClientID %>').val(rowData.FkPatientId);
            $('#<%= DDLDentist.ClientID %>').val(rowData.FkDentistId);
        }

        // Función para eliminar una cita
        function deleteQuote(id) {
            $.ajax({
                type: "POST",
                url: "WFQuotes.aspx/DeleteQuote", // WebMethod para eliminar una cita
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify({ id: id }),
                success: function (response) {
                    $('#quotesTable').DataTable().ajax.reload(); // Recarga la tabla después de eliminar
                    alert("Cita eliminada exitosamente.");
                },
                error: function () {
                    alert("Error al eliminar la cita.");
                }
            });
        }
    </script>
</asp:Content>
