<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="WFDiagnosis.aspx.cs" Inherits="Presentation.WFDiagnosis" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <%--Estilos--%>
 <link href="resources/css/datatables.min.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:HiddenField ID="HFDiagnosisID" runat="server" />

    <%-- Formulario de Diagnóstico --%>
    <asp:Label ID="Label1" runat="server" Text="Ingrese la descripción del diagnóstico"></asp:Label>
    <asp:TextBox ID="TBDescription" runat="server"></asp:TextBox>
    <br />
    
    <%-- Fecha --%>
    <asp:Label ID="Label2" runat="server" Text="Ingrese la fecha"></asp:Label>
    <asp:TextBox ID="TBDate" runat="server"></asp:TextBox>
    <br />
    
    <%-- Observaciones --%>
    <asp:Label ID="Label3" runat="server" Text="Ingrese las observaciones"></asp:Label>
    <asp:TextBox ID="TBObservations" runat="server"></asp:TextBox>
    <br />
    
    <%-- Seleccionar Cita --%>
    <asp:Label ID="Label4" runat="server" Text="Seleccione una cita"></asp:Label>
    <asp:DropDownList ID="DDLQuotes" runat="server"></asp:DropDownList>
    
    <%-- Seleccionar Historial Clínico --%>
    <asp:Label ID="Label5" runat="server" Text="Seleccione un historial clínico"></asp:Label>
    <asp:DropDownList ID="DDLClinicalHistory" runat="server"></asp:DropDownList>
    
    <%-- Botón guardar --%>
    <div>
        <asp:Button ID="BtnSave" runat="server" Text="Guardar" OnClick="BtnSave_Click" />
        <asp:Label ID="LblMsg" runat="server" Text=""></asp:Label>
    </div>
    <br />
   


  <%--Lista de Diagnosticos--%>
  <h2>Lista de Diagnosticos</h2>
  <table id="diagnosisTable" class="display" style="width: 100%">
      <thead>
          <tr>
              <th>ID</th>
              <th>Descripcion</th>
              <th>Fecha</th>
              <th>Observaciones</th>
              <th>FkHistorialClinico</th>
              <th>HistorialClinico</th>
              <th>FkCitas</th>
              <th>Citas</th>
          </tr>
      </thead>
      <tbody>
      </tbody>
  </table>

  <script src="resources/js/datatables.min.js" type="text/javascript"></script>

  <%--Productos--%>
  <script type="text/javascript">
      $(document).ready(function () {
          $('#diagnosisTable').DataTable({
              "processing": true,
              "serverSide": false,
              "ajax": {
                  "url": "WFDiagnosis.aspx/ListDiagnosis",// Se invoca el WebMethod Listar diagnosticos
                  "type": "POST",
                  "contentType": "application/json",
                  "data": function (d) {
                      return JSON.stringify(d);// Convierte los datos a JSON
                  },
                  "dataSrc": function (json) {
                      return json.d.data;// Obtiene la lista de productos del resultado
                  }
              },
              "columns": [
                  { "data": "DiagnosisID" },
                  { "data": "Description" },
                  { "data": "Date" },
                  { "data": "Observations" },
                  { "data": "FkClinicalHistory", "visible": false },
                  { "data": "DateCinicalHistory" },
                  { "data": "FkQuotes", "visible": false },
                  { "data": "DateQuote" },
                  {
                      "data": null,
                      "render": function (data, type, row) {
                          return `<button class="edit-btn" data-id="${row.DiagnosisID}">Editar</button>
                             <button class="delete-btn" data-id="${row.DiagnosisID}">Eliminar</button>`;
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

          // Editar un diagnostico
          $('#diagnosisTable').on('click', '.edit-btn', function () {
              //const id = $(this).data('id');
              const rowData = $('#diagnosisTable').DataTable().row($(this).parents('tr')).data();
              //alert(JSON.stringify(rowData, null, 2));
              loadDiagnosisData(rowData);
          });

          // Eliminar un producto
          $('#diagnosisTable').on('click', '.delete-btn', function () {
              const id = $(this).data('id');// Obtener el ID del diagnostico
              if (confirm("¿Estás seguro de que deseas eliminar este diagnostico?")) {
                  deleteDiagnosis(id);// Invoca a la función para eliminar el diagnostico
              }
          });
      });

      // Cargar los datos en los TextBox y DDL para actualizar
      function loadDiagnosisData(rowData) {
          $('#<%= HFDiagnosisID.ClientID %>').val(rowData.DiagnosisID);
        $('#<%= TBDescription.ClientID %>').val(rowData.Description);
        $('#<%= TBDate.ClientID %>').val(rowData.Date);
        $('#<%= TBObservations.ClientID %>').val(rowData.Observations);
        $('#<%= DDLClinicalHistory.ClientID %>').val(rowData.FkClinicalHistory);
        $('#<%= DDLQuotes.ClientID %>').val(rowData.FkQuotes);
      }

      // Función para eliminar un producto
      function deleteDiagnosis(id) {
          $.ajax({
              type: "POST",
              url: "WFDiagnosis.aspx/DeleteDiagnosis",// Se invoca el WebMethod Eliminar un Producto
              contentType: "application/json; charset=utf-8",
              data: JSON.stringify({ id: id }),
              success: function (response) {
                  $('#diagnosisTable').DataTable().ajax.reload();// Recargar la tabla después de eliminar
                  alert("Diagnostico eliminado exitosamente.");
              },
              error: function () {
                  alert("Error al eliminar el producto.");
              }
          });
      }
  </script>
</asp:Content>
