<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="WFClinicalHistory.aspx.cs" Inherits="Presentation.WFClinicalHistory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <%--Estilos--%>
    <link href="resources/css/datatables.min.css" rel="stylesheet" />

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <form runat="server">

        <%--Formulario de Historial Clínico--%>
        <asp:HiddenField ID="HFClinicalHistoryID" runat="server" />

        <%--Fecha de creación--%>
        <asp:Label ID="Label1" runat="server" Text="Ingrese la fecha de creación"></asp:Label>
        <asp:TextBox ID="TBCreacionDate" runat="server"></asp:TextBox>
        <br />

        <%--Descripción general--%>
        <asp:Label ID="Label2" runat="server" Text="Ingrese la descripción general"></asp:Label>
        <asp:TextBox ID="TBOverview" runat="server" TextMode="MultiLine" Rows="4"></asp:TextBox>
        <br />

        <%--Seleccionar paciente--%>
        <asp:Label ID="Label3" runat="server" Text="Seleccione un paciente"></asp:Label>
        <asp:DropDownList ID="DDLPatient" runat="server" CssClass="form-select"></asp:DropDownList>

        <%--Botones guardar y actualizar--%>
        <div>
            <asp:Button ID="BtnSave" runat="server" Text="Guardar" OnClick="BtnSave_Click" />
            <asp:Button ID="BtbUpdate" runat="server" Text="Actualizar" OnClick="BtbUpdate_Click" />
            <asp:Label ID="LblMsg" runat="server" Text=""></asp:Label>
        </div>
        <br />

        <%--Lista de Historial Clínico--%>
        <div>
            <asp:GridView ID="GVClinicalHistory" runat="server"></asp:GridView>
        </div>
        <br />
    </form>

    <%--Lista de Historiales Clinicos--%>
    <h2>Lista de Historias Clinicas</h2>
    <table id="productsTable" class="display" style="width: 100%">
        <thead>
            <tr>
                <th>HistorialID</th>
                <th>FechaDeCreacion</th>
                <th>DescripcionGeneral</th>
                <th>FkPaciente</th>
                <th>Paciente</th>

            </tr>
        </thead>
        <tbody>
        </tbody>
    </table>

    <script src="resources/js/datatables.min.js" type="text/javascript"></script>

    <%--Auxiliares--%>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#ClinicalHistoryTable').DataTable({
                "processing": true,
                "serverSide": false,
                "ajax": {
                    "url": "WFClinicalHistory.aspx/ListClinicalHistory",// Se invoca el WebMethod Listar auxiliares
                    "type": "POST",
                    "contentType": "application/json",
                    "data": function (d) {
                        return JSON.stringify(d);// Convierte los datos a JSON
                    },
                    "dataSrc": function (json) {
                        return json.d.data;// Obtiene la lista de auxiliares del resultado
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
                            return `<button class="edit-btn" data-id="${row.ClinicalHistoryID}">Editar</button>
                             <button class="delete-btn" data-id="${row.ClinicalHistoryID}">Eliminar</button>`;
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
                if (confirm("¿Estás seguro de que deseas eliminar este Historial Clinico?")) {
                    deleteAssistant(id);// Invoca a la función para eliminar el auxiliar
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


