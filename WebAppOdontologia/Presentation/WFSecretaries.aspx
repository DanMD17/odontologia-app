<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="WFSecretaries.aspx.cs" Inherits="Presentation.WFSecretaries" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--Estilos--%>
    <link href="resources/css/datatables.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="card m-1">
        <div class="card-header">
            Gestión de Secretarias
        </div>
        <div class="card-body">
            <form id="FrmSecretaries" runat="server">
                <%--Id--%>
                <asp:HiddenField ID="HFSecretariesID" runat="server" />
                <div class="row m-1">
                    <div class="col">
                        <%--Funcion secretaria--%>
                        <asp:Label ID="Label1" CssClass="form-label" runat="server" Text="Ingrese la funcion de la secretaria"></asp:Label>
                        <asp:TextBox ID="TBFunction" CssClass="form-control" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RFVFunction" runat="server" ControlToValidate="TBFunction"
                            CssClass="text-danger" ErrorMessage="Este campo es obligatorio."></asp:RequiredFieldValidator>
                    </div>
                    <div class="col-4">
                        <%--Años de experiencia secretaria--%>
                        <asp:Label ID="Label2" CssClass="form-label" runat="server" Text="Ingrese los anios de experiencia"></asp:Label>
                        <asp:TextBox ID="TBYearsExp" CssClass="form-control" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RFVYearsExp" runat="server" ControlToValidate="TBYearsExp"
                            CssClass="text-danger" ErrorMessage="Este campo es obligatorio."></asp:RequiredFieldValidator>
                    </div>
                    <div class="col-2">
                        <%--DDL para seleccionar empleado--%>
                        <asp:Label ID="Label3" CssClass="form-label" runat="server" Text="Seleccione un empleado"></asp:Label>
                        <asp:DropDownList ID="DDLEmployee" CssClass="form-select" runat="server"></asp:DropDownList>
                    </div>
                </div>
                <div class="row m-1">
                    <div class="col">
                        <asp:Button ID="BtnSave" runat="server" Text="Guardar" OnClick="BtnSave_Click" />
                        <asp:Button ID="BtnUpdate" runat="server" Text="Actualizar" OnClick="BtnUpdate_Click" />
                        <asp:Label ID="LblMsg" runat="server" Text=""></asp:Label>
                        <br />
                    </div>
                </div>
            </form>
        </div>
    </div>

    <div class="card m-1">
        <%--Panel para la gestion del Administrador--%>
        <asp:Panel ID="PanelAdmin" runat="server">
            <div class="card-header">
                Lista de Secretarias
            </div>
            <div class="card-body">
                <%--Lista de Secretarias--%>
                <div class="table-responsive">
                    <%--Tabla de Secretarias--%>
                    <h2>Lista de Secretarias</h2>
                    <table id="secretariesTable" class="display" style="width: 100%">
                        <thead>
                            <tr>
                                <th>SecretariaID</th>
                                <th>Funcion</th>
                                <th>AniosExperiencia</th>
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

    <%--Secretaries--%>
    <script type="text/javascript">
        $(document).ready(function () {
            const showEditButton = '<%= _showEditButton %>' === 'True';
            const showDeleteButton = '<%= _showDeleteButton %>' === 'True';
            $('#secretariesTable').DataTable({
                "processing": true,
                "serverSide": false,
                "ajax": {
                    "url": "WFSecretaries.aspx/ListSecretaries",// Se invoca el WebMethod Listar las Secretarias
                    "type": "POST",
                    "contentType": "application/json",
                    "data": function (d) {
                        return JSON.stringify(d);// Convierte los datos a JSON
                    },
                    "dataSrc": function (json) {
                        return json.d.data;// Obtiene la lista de secretarias del resultado
                    }
                },
                "columns": [
                    { "data": "SecretariatID" },
                    { "data": "Function" },
                    { "data": "YearsExp" },
                    { "data": "FkEmployee", "visible": false },
                    { "data": "NameEmployee" },
                    {
                        "data": null,
                        "render": function (data, type, row) {
                            let buttons = '';
                            if (showEditButton) {
                                buttons += `<button class="btn btn-info edit-btn" data-id="${row.SecretariatID}">Editar</button>`;
                            }
                            if (showDeleteButton) {
                                buttons += `<button class="btn btn-danger delete-btn" data-id="${row.SecretariatID}">Eliminar</button>`;
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

            // Editar una secretaria
            $('#secretariesTable').on('click', '.edit-btn', function () {
                //const id = $(this).data('id');
                const rowData = $('#secretariesTable').DataTable().row($(this).parents('tr')).data();
                //alert(JSON.stringify(rowData, null, 2));
                loadSecretariatData(rowData);
            });

            // Eliminar una secretaria
            $('#secretariesTable').on('click', '.delete-btn', function () {
                const id = $(this).data('id');// Obtener el ID de la secretaria
                if (confirm("¿Estás seguro de que deseas eliminar esta secretaria?")) {
                    deleteSecretariat(id);// Invoca a la función para eliminar la secretaria
                }
            });
        });

        // Función para agregar los datos en los TextBox y DDL para actualizar
        function loadSecretariatData(rowData) {
            $('#<%= HFSecretariesID.ClientID %>').val(rowData.SecretariatID);
            $('#<%= TBFunction.ClientID %>').val(rowData.Function);
            $('#<%= TBYearsExp.ClientID %>').val(rowData.YearsExp);
            $('#<%= DDLEmployee.ClientID %>').val(rowData.FkEmployee);
        }

        // Función para eliminar una secretaria
        function deleteSecretariat(id) {
            $.ajax({
                type: "POST",
                url: "WFSecretaries.aspx/DeleteSecretariat",// Se invoca el WebMethod Eliminar una secretaria
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify({ id: id }),
                success: function (response) {
                    $('#secretariesTable').DataTable().ajax.reload();// Recargar la tabla después de eliminar
                    alert("Secretaria eliminada exitosamente.");
                },
                error: function () {
                    alert("Error al eliminar la secretaria.");
                }
            });
        }
    </script>
</asp:Content>
