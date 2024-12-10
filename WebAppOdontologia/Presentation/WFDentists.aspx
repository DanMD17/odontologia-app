<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="WFDentists.aspx.cs" Inherits="Presentation.WFDentists" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--Estilos--%>
    <link href="resources/css/datatables.min.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="card m-1">
        <div class="card-header">
            Gestión de Odontologos
        </div>
        <div class="card-body">
            <form id="FrmDentists" runat="server">
                <%--Id--%>
                <asp:HiddenField ID="HFDentistID" runat="server" />
                <div class="row m-1">
                    <div class="col-4">
                        <%--Formulario de Odontólogos--%>
                        <asp:Label ID="Label1" CssClass="form-label" runat="server" Text="Ingrese la especialidad"></asp:Label>
                        <asp:TextBox ID="TBSpecialty" CssClass="form-control" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RFVSpecialty" runat="server" ControlToValidate="TBSpecialty"
                            CssClass="text-danger" ErrorMessage="Este campo es obligatorio."></asp:RequiredFieldValidator>
                    </div>
                    <div class="col-3">
                        <%--Seleccionar empleado--%>
                        <asp:Label ID="Label2" CssClass="form-label" runat="server" Text="Seleccione un empleado"></asp:Label>
                        <asp:DropDownList ID="DDLEmployee" CssClass="form-select" runat="server"></asp:DropDownList>
                        <asp:Label ID="LblMsgEmp" runat="server" Text="" CssClass="text-danger"></asp:Label>
                    </div>
                </div>
                <div class="row m-1">
                    <div class="col">
                        <asp:Button ID="BtnSave" CssClass="btn btn-success" runat="server" Text="Guardar" OnClick="BtnSave_Click" />
                        <asp:Button ID="BtnUpdate"  CssClass="btn btn-primary"  runat="server" Text="Actualizar" OnClick="BtnUpdate_Click" />
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
                Lista de odontologos
            </div>
            <div class="card-body">
                <%--Lista de Dentistas--%>
                <div class="table-responsive">
                    <%--Tabla de Dentistas--%>
                    <table id="DentistsTable" class="display" style="width: 100%">
                        <thead>
                            <tr>
                                <th>DentistaID</th>
                                <th>Especialidad</th>
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

    <%--Dentistas--%>
    <script type="text/javascript">
        $(document).ready(function () {
            const showEditButton = '<%= _showEditButton %>' === 'True';
            const showDeleteButton = '<%= _showDeleteButton %>' === 'True';
            $('#DentistsTable').DataTable({
                "processing": true,
                "serverSide": false,
                "ajax": {
                    "url": "WFDentists.aspx/ListDentists",// Se invoca el WebMethod Listar dentistas
                    "type": "POST",
                    "contentType": "application/json",
                    "data": function (d) {
                        return JSON.stringify(d);// Convierte los datos a JSON
                    },
                    "dataSrc": function (json) {
                        return json.d.data;// Obtiene la lista de dentistas del resultado
                    }
                },
                "columns": [
                    { "data": "DentistID" },
                    { "data": "Specialty" },
                    { "data": "FkEmployee", "visible": false },
                    { "data": "NameEmployee" },
                    {
                        "data": null,
                        "render": function (data, type, row) {
                            let buttons = '';
                            if (showEditButton) {
                                buttons += `<button class="btn btn-info edit-btn" data-id="${row.DentistID}">Editar</button>`;
                            }
                            if (showDeleteButton) {
                                buttons += `<button class="btn btn-danger delete-btn" data-id="${row.DentistID}">Eliminar</button>`;
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
            // Editar un dentista
            $('#DentistsTable').on('click', '.edit-btn', function () {
                //const id = $(this).data('id');
                const rowData = $('#DentistsTable').DataTable().row($(this).parents('tr')).data();
                //alert(JSON.stringify(rowData, null, 2));
                loadDentistsData(rowData);
                $('#<%= BtnSave.ClientID %>').hide();
                $('#<%= BtnUpdate.ClientID %>').show();
            });

            // Eliminar un dentista
            $('#DentistsTable').on('click', '.delete-btn', function () {
                const id = $(this).data('id');// Obtener el ID del dentista
                if (confirm("¿Estás seguro de que deseas eliminar este Dentista")) {
                    deleteDentist(id);// Invoca a la función para eliminar el dentista
                }
            });
        });

        // Función para cargar los datos en los TextBox y DDL para actualizar
        function loadDentistsData(rowData) {
            $('#<%= HFDentistID.ClientID %>').val(rowData.DentistID);
            $('#<%= TBSpecialty.ClientID %>').val(rowData.Specialty);
            $('#<%= DDLEmployee.ClientID %>').val(rowData.FkEmployee);
        }

        // Función para eliminar un dentista
        function deleteDentist(id) {
            $.ajax({
                type: "POST",
                url: "WFDentists.aspx/DeleteDentist",// Se invoca el WebMethod Eliminar un Dentista
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify({ id: id }),
                success: function (response) {
                    $('#DentistsTable').DataTable().ajax.reload();// Recargar la tabla después de eliminar
                    alert("Dentista eliminado exitosamente.");
                },
                error: function () {
                    alert("Error al eliminar el Dentista.");
                }
            });
        }




    </script>
</asp:Content>
