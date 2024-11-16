<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="WFQuotes.aspx.cs" Inherits="Presentation.WFQuotes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="resources/css/datatables.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--Formulario para Citas--%>
    <asp:HiddenField ID="HFQuoteID" runat="server"></asp:HiddenField>

    <asp:Label ID="Label1" runat="server" Text="Ingrese la fecha de la cita"></asp:Label>
    <asp:TextBox ID="TBDate" runat="server" TextMode="Date"></asp:TextBox>
    <br />

    <asp:Label ID="Label2" runat="server" Text="Ingrese la hora de la cita"></asp:Label>
    <asp:TextBox ID="TBTime" runat="server" TextMode="Time"></asp:TextBox>
    <br />

    <asp:Label ID="Label3" runat="server" Text="Estado de la cita"></asp:Label>
    <asp:TextBox ID="TBStatus" runat="server"></asp:TextBox>
    <br />

    <asp:Label ID="Label4" runat="server" Text="Seleccione un paciente"></asp:Label>
    <asp:DropDownList ID="DDLPatient" runat="server"></asp:DropDownList>
    <br />

    <asp:Label ID="Label5" runat="server" Text="Seleccione un Odontólogo"></asp:Label>
    <asp:DropDownList ID="DDLDentist" runat="server"></asp:DropDownList>
    <br />

     <%--Botones--%>
    <div>
        <asp:Button ID="BtnSave" runat="server" Text="Guardar" OnClick="BtnSave_Click" />
        <asp:Button ID="BtnUpdate" runat="server" Text="Actualizar" OnClick="BtnUpdate_Click" />
        <asp:Label ID="LblMsg" runat="server" Text=""></asp:Label>
    </div>
    <br />

    <%--Lista de Citas--%>
    <h2>Lista de Citas</h2>
    <table id="quotesTable" class="display" style="width: 100%">
        <thead>
            <tr>
                <th>ID</th>
                <th>Fecha</th>
                <th>Hora</th>
                <th>Estado</th>
                <th>FkPaciente</th>
                <th>FkOdontologo</th>
            </tr>
        </thead>
        <tbody>
        </tbody>
    </table>

    <script src="resources/js/datatables.min.js" type="text/javascript"></script>

    <script type="text/javascript">
    $(document).ready(function () {
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

                {
                    "data": null,
                    "render": function (data, type, row) {
                        return `<button class="edit-btn" data-id="${row.QuoteID}">Editar</button>
                                <button class="delete-btn" data-id="${row.QuoteID}">Eliminar</button>`;
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
            $('#<%= TBStatus.ClientID %>').val(rowData.Status);
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
