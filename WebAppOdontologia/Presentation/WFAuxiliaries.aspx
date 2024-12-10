<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="WFAuxiliaries.aspx.cs" Inherits="Presentation.WFAuxiliaries" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--Estilos--%>
    <link href="resources/css/datatables.min.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="card m-1">
        <div class="card-header">
            Gestión de Auxiliares
        </div>
        <div class="card-body">
            <form id="FrmAuxiliaries" runat="server">
                <%--Id--%>
                <asp:HiddenField ID="HFAssistantID" runat="server" />
                <div class="row m-1">
                    <div class="col">
                        <%--Funcion--%>
                        <asp:Label ID="Label4" runat="server" Text="Función del auxiliar:"></asp:Label>
                        <asp:TextBox ID="TBFunction" runat="server" CssClass="form-control" required="required"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RFVFunction" runat="server" ControlToValidate="TBFunction"
                            CssClass="text-danger" ErrorMessage="Este campo es obligatorio."></asp:RequiredFieldValidator>
                    </div>
                    <div class="col-4">
                        <%--Educacion--%>
                        <asp:Label ID="Label5" runat="server" Text="Nivel educativo:"></asp:Label>
                        <asp:DropDownList ID="DDLAux" CssClass="form-select" runat="server">
                            <asp:ListItem Value="0">Seleccione</asp:ListItem>
                            <asp:ListItem Value="Educación básica secundaria">Educación básica secundaria</asp:ListItem>
                            <asp:ListItem Value="Educación media vocacional">Educación media vocacional</asp:ListItem>
                            <asp:ListItem Value="Técnico en salud oral">Técnico en salud oral</asp:ListItem>
                            <asp:ListItem Value="Técnico en asistencia odontológica">Técnico en asistencia odontológica</asp:ListItem>
                            <asp:ListItem Value="Tecnólogo en salud oral">Tecnólogo en salud oral</asp:ListItem>
                            <asp:ListItem Value="Pregrado en odontología">Pregrado en odontología</asp:ListItem>
                            <asp:ListItem Value="Otro">Otro</asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RFVEducationalLevel" runat="server" ControlToValidate="DDLAux"
                            CssClass="text-danger" InitialValue="0" Display="Dynamic" ErrorMessage="Este campo es obligatorio."></asp:RequiredFieldValidator>
                    </div>
                    <div class="col-2">
                        <%--Empleo--%>
                        <asp:Label ID="Label6" runat="server" Text="Seleccione un empleado:"></asp:Label>
                        <asp:DropDownList ID="DDLEmployee" runat="server" CssClass="form-select" required="required">
                            <asp:ListItem Value="0">Seleccione</asp:ListItem>
                        </asp:DropDownList>
                        <asp:Label ID="LblMsgAux" runat="server" Text="" CssClass="text-danger"></asp:Label>
                        <br />
                    </div>
                </div>
                <div class="row m-1">
                    <div class="col">
                        <%--Botones de Guardar y Actualizar--%>
                        <asp:Button ID="BtnSave" runat="server" Text="Guardar" OnClick="BtnSave_Click" CssClass="btn btn-success" />
                        <asp:Button ID="BtnUpdate" CssClass="btn btn-primary" runat="server" Text="Actualizar" OnClick="BtnUpdate_Click" />
                        <asp:Label ID="LblMsg" runat="server" Text="" CssClass="msg-label"></asp:Label>
                    </div>
                </div>
            </form>
        </div>
    </div>

    <div class="card m-1">
        <%--Panel para la gestion del Administrador--%>
        <asp:Panel ID="PanelAdmin" runat="server">
            <div class="card-header">
                Lista de Usuarios
            </div>
            <div class="card-body">
                <%--Lista de Auxiliares--%>
                <div class="table-responsive">
                    <%--Tabla de Auxiliares--%>
                    <table id="assistantsTable" class="display" style="width: 100%">
                        <thead>
                            <tr>
                                <th>AuxiliarID</th>
                                <th>Función</th>
                                <th>NivelEducativo</th>
                                <th>FkEmpleado</th>
                                <th>Empleado</th>
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
            const table = $('#assistantsTable').DataTable({
                "processing": true,
                "ajax": {
                    "url": "WFAuxiliaries.aspx/ListAssistants",
                    "type": "POST",
                    "contentType": "application/json",
                    "dataSrc": function (json) {
                        return json.d.data;
                    }
                },
                "columns": [
                    { "data": "AssistantID" },
                    { "data": "Function" },
                    { "data": "EducationalLevel" },
                    { "data": "FkEmployee", "visible": false },
                    { "data": "NameEmployee" },
                    {
                        "data": null,
                        "render": function (data, type, row) {
                            let buttons = '';
                            if (showEditButton) {
                                buttons += `<button class="btn btn-info edit-btn" data-id="${row.AssistantID}">Editar</button>`;
                            }
                            if (showDeleteButton) {
                                buttons += `<button class="btn btn-danger delete-btn" data-id="${row.AssistantID}">Eliminar</button>`;
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

            // Editar auxiliar
            $('#assistantsTable').on('click', '.edit-btn', function () {
                const rowData = table.row($(this).parents('tr')).data();
                loadAssistantData(rowData);
                $('#<%= BtnSave.ClientID %>').hide();
                $('#<%= BtnUpdate.ClientID %>').show();
            });

            // Eliminar auxiliar
            $('#assistantsTable').on('click', '.delete-btn', function () {
                const id = $(this).data('id');
                if (confirm("¿Deseas eliminar este auxiliar?")) {
                    deleteAssistant(id);
                }
            });
        });

        // Cargar datos en el formulario para editar
        function loadAssistantData(rowData) {
            $('#<%= HFAssistantID.ClientID %>').val(rowData.AssistantID);
            $('#<%= TBFunction.ClientID %>').val(rowData.Function);
            $('#<%= DDLAux.ClientID %>').val(rowData.EducationalLevel);
            $('#<%= DDLEmployee.ClientID %>').val(rowData.FkEmployee);
        }

        // Eliminar un auxiliar
        function deleteAssistant(id) {
            $.ajax({
                type: "POST",
                url: "WFAuxiliaries.aspx/DeleteAssistant",
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify({ id: id }),
                success: function () {
                    $('#assistantsTable').DataTable().ajax.reload();
                    $('#<%= LblMsg.ClientID %>').text("Auxiliar eliminado correctamente.");
                },
                error: function () {
                    $('#<%= LblMsg.ClientID %>').text("Error al eliminar el auxiliar.");
                }
            });
        }
    </script>
</asp:Content>
