<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="WFAuxiliaries.aspx.cs" Inherits="Presentation.WFAuxiliaries" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--Estilos--%>
    <link href="resources/css/datatables.min.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--ID oculto--%>
    <asp:HiddenField ID="HFAssistantID" runat="server" />

    <%--Formulario de Auxiliares--%>
    <div>
        <asp:Label ID="Label1" runat="server" Text="Función del auxiliar:"></asp:Label>
        <asp:TextBox ID="TBFunction" runat="server" CssClass="form-control" required="required"></asp:TextBox>
        <br />

        <asp:Label ID="Label2" runat="server" Text="Nivel educativo:"></asp:Label>
        <asp:TextBox ID="TBEducationalLevel" runat="server" CssClass="form-control" required="required"></asp:TextBox>
        <br />

        <asp:Label ID="Label3" runat="server" Text="Seleccione un empleado:"></asp:Label>
        <asp:DropDownList ID="DDLEmployee" runat="server" CssClass="form-control" required="required"></asp:DropDownList>
        <br />

        <%--Botones de Guardar y Actualizar--%>
        <asp:Button ID="BtnSave" runat="server" Text="Guardar" OnClick="BtnSave_Click" CssClass="btn btn-primary" />
        <asp:Button ID="BtnUpdate" runat="server" Text="Actualizar" OnClick="BtnUpdate_Click" CssClass="btn btn-secondary" />
        <asp:Label ID="LblMsg" runat="server" Text="" CssClass="msg-label"></asp:Label>
    </div>
    <br />

    <%--Tabla de Auxiliares--%>
    <h2>Lista de Auxiliares</h2>
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

    <script src="resources/js/datatables.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
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
                            return `<button class="edit-btn" data-id="${row.AssistantID}">Editar</button>
                                    <button class="delete-btn" data-id="${row.AssistantID}">Eliminar</button>`;
                        }
                    }
                ],
                "language": {
                    // Traducciones
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
            $('#<%= TBEducationalLevel.ClientID %>').val(rowData.EducationalLevel);
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
