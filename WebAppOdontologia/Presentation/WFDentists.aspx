<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="WFDentists.aspx.cs" Inherits="Presentation.WFDentists" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--Estilos--%>
    <link href="resources/css/datatables.min.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <%--ID--%>
    <asp:HiddenField ID="HFDentistID" runat="server" />

    <%--Formulario de Odontólogos--%>
    <asp:Label ID="Label1" runat="server" Text="Ingrese la especialidad"></asp:Label>
    <asp:TextBox ID="TBSpecialty" runat="server"></asp:TextBox>
    <br />

    <%--Seleccionar empleado--%>
    <asp:Label ID="Label2" runat="server" Text="Seleccione un empleado"></asp:Label>
    <asp:DropDownList ID="DDLEmployee" runat="server" CssClass="form-select"></asp:DropDownList>

    <%--Botones guardar--%>
    <div>
        <asp:Button ID="BtnSave" runat="server" Text="Guardar" OnClick="BtnSave_Click" />
        <asp:Button ID="BtnUpdate" runat="server" Text="Actualizar" OnClick="BtnUpdate_Click" />
        <asp:Label ID="LblMsg" runat="server" Text=""></asp:Label>
    </div>
    <br />

    <%--Lista de Dentistas--%>
    <h2>Lista de Dentistas</h2>
    <table id="DentistsTable" class="display" style="width: 100%">
        <thead>
            <tr>
                <th>DentistaID</th>
                <th>Especialidad</th>
                <th>FkEmpleado</th>
                <th>Empleado</th>
            </tr>
        </thead>
        <tbody>
        </tbody>
    </table>

    <script src="resources/js/datatables.min.js" type="text/javascript"></script>

    <%--Dentistas--%>
    <script type="text/javascript">
        $(document).ready(function () {
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
                            return `<button class="edit-btn" data-id="${row.DentistID}">Editar</button>
                             <button class="delete-btn" data-id="${row.DentistID}">Eliminar</button>`;
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
