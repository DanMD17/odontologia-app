<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="WFTreatments.aspx.cs" Inherits="Presentation.WFTreatments" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="resources/css/datatables.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--Formulario de Tratamientos--%>
    <asp:HiddenField ID="HFTreatmentID" runat="server"></asp:HiddenField>
    
    <%--Nombre--%>
    <asp:Label ID="Label1" runat="server" Text="Ingrese el nombre del tratamiento"></asp:Label>
    <asp:TextBox ID="TBName" runat="server"></asp:TextBox>
    <br />
    
    <%--Descripción--%>
    <asp:Label ID="Label2" runat="server" Text="Ingrese la descripción"></asp:Label>
    <asp:TextBox ID="TBDescription" runat="server"></asp:TextBox>
    <br />
    
    <%--Fecha--%>
    <asp:Label ID="Label3" runat="server" Text="Ingrese la fecha"></asp:Label>
    <asp:TextBox ID="TBDate" runat="server" TextMode="Date"></asp:TextBox>
    <br />
    
    <%--Observaciones--%>
    <asp:Label ID="Label4" runat="server" Text="Ingrese las observaciones"></asp:Label>
    <asp:TextBox ID="TBObservations" runat="server"></asp:TextBox>
    <br />
    
    <%--FK Cita ID--%>
    <asp:Label ID="Label5" runat="server" Text="Seleccione una cita"></asp:Label>
    <asp:DropDownList ID="DDLQuotes" runat="server"></asp:DropDownList>
    <br />
    
    <%--DDL Historial--%>
    <asp:Label ID="Label6" runat="server" Text="Seleccione un historial"></asp:Label>
    <asp:DropDownList ID="DDLHistory" runat="server"></asp:DropDownList>
    <br />
    
    <%--DDL Auxiliar ID--%>
    <asp:Label ID="Label7" runat="server" Text="Seleccione un auxiliar"></asp:Label>
    <asp:DropDownList ID="DDLAux" runat="server"></asp:DropDownList>
    <br />
    
    <%--Botones--%>
    <div>
        <asp:Button ID="BtnSave" runat="server" Text="Guardar" OnClick="BtnSave_Click" />
        <asp:Button ID="BtnUpdate" runat="server" Text="Actualizar" OnClick="BtnUpdate_Click" />
        <asp:Label ID="LblMsg" runat="server" Text=""></asp:Label>
    </div>
    <br />

    <%--Lista de Tratamientos--%>
    <h2>Lista de Tratamientos</h2>
    <table id="treatmentsTable" class="display" style="width: 100%">
        <thead>
            <tr>
                <th>ID</th>
                <th>Nombre</th>
                <th>Descripcion</th>
                <th>Fecha</th>
                <th>Observaciones</th>
                <th>FkCita</th>
                <th>FkHistorial</th>
                <th>FkAuxiliar</th>
            </tr>
        </thead>
        <tbody>
        </tbody>
    </table>

    <script src="resources/js/datatables.min.js" type="text/javascript"></script>

    <script type="text/javascript">
    $(document).ready(function () {
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
                { "data": "StatusQuote"},
                { "data": "FkHistId", "visible": false },
                { "data": "DescriptionHistory" },
                { "data": "FkAuxId", "visible": false },
                { "data": "FunctionAuxiliaries" },
                {
                    "data": null,
                    "render": function (data, type, row) {
                        return `<button class="edit-btn" data-id="${row.TreatmentID}">Editar</button>
                                <button class="delete-btn" data-id="${row.TreatmentID}">Eliminar</button>`;
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
        $('#<%= TBName.ClientID %>').val(rowData.Name);
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
