<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="WFEmployees.aspx.cs" Inherits="Presentation.WFEmployees" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--Estilos--%>
    <link href="resources/css/datatables.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--ID--%>
    <asp:HiddenField ID="HFEmployeeID" runat="server" />

    <%--Identficacion--%>
    <asp:Label ID="Label1" runat="server" Text="Ingrese la identificacion"></asp:Label>
    <asp:TextBox ID="TBIdentification" runat="server"></asp:TextBox>
    <br />
    <%--Nombres--%>
    <asp:Label ID="Label2" runat="server" Text="Ingrese el nombre"></asp:Label>
    <asp:TextBox ID="TBName" runat="server"></asp:TextBox>
    <br />
    <%--Apellidos--%>
    <asp:Label ID="Label3" runat="server" Text="Ingrese el apellido"></asp:Label>
    <asp:TextBox ID="TBLastName" runat="server"></asp:TextBox>
    <br />
    <%--Telefono--%>
    <asp:Label ID="Label4" runat="server" Text="Ingrese el numero de celular"></asp:Label>
    <asp:TextBox ID="TBCellPhone" runat="server"></asp:TextBox>
    <br />
    <%--Direccion--%>
    <asp:Label ID="Label5" runat="server" Text="Ingrese la direccion"></asp:Label>
    <asp:TextBox ID="TBAddress" runat="server"></asp:TextBox>
    <br />
    <%--Correo Electronico--%>
    <asp:Label ID="Label6" runat="server" Text="Ingrese el correo"></asp:Label>
    <asp:TextBox ID="TBEmail" runat="server"></asp:TextBox>
    <br />
    <%--Botones guardar y actualizar--%>
    <div>
        <asp:Button ID="BtnSave" runat="server" Text="Guardar" OnClick="BtnSave_Click" />
        <asp:Button ID="BtbUpdate" runat="server" Text="Actualizar" OnClick="BtbUpdate_Click" />
        <asp:Label ID="LblMsg" runat="server" Text=""></asp:Label>
    </div>
    <br />
    <%--Lista de Empleados--%>
    <h2>Lista de Empleados</h2>
    <table id="employeesTable" class="display" style="width: 100%">
        <thead>
            <tr>
                <th>EmpleadoID</th>
                <th>Identificacion</th>
                <th>Nombre</th>
                <th>Apellidos</th>
                <th>Celular</th>
                <th>Direccion</th>
                <th>Correo</th>
            </tr>
        </thead>
        <tbody>
        </tbody>
    </table>

    <script src="resources/js/datatables.min.js" type="text/javascript"></script>

      <%--Empleado--%>
  <script type="text/javascript">
      $(document).ready(function () {
          $('#employeesTable').DataTable({
              "processing": true,
              "serverSide": false,
              "ajax": {
                  "url": "WFEmployees.aspx/ListEmployees",// Se invoca el WebMethod Listar empleados
                  "type": "POST",
                  "contentType": "application/json",
                  "data": function (d) {
                      return JSON.stringify(d);// Convierte los datos a JSON
                  },
                  "dataSrc": function (json) {
                      return json.d.data;// Obtiene la lista de empleados del resultado
                  }
              },
              "columns": [
                  { "data": "EmployeeID" },
                  { "data": "Identification" },
                  { "data": "Name" },
                  { "data": "LastName" },
                  { "data": "CellPhone" },
                  { "data": "Address" },
                  { "data": "Email" },
                  {
                      "data": null,
                      "render": function (data, type, row) {
                          return `<button class="edit-btn" data-id="${row.EmployeeID}">Editar</button>
                             <button class="delete-btn" data-id="${row.EmployeeID}">Eliminar</button>`;
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

          // Editar un empleado
          $('#employeesTable').on('click', '.edit-btn', function () {
              //const id = $(this).data('id');
              const rowData = $('#employeesTable').DataTable().row($(this).parents('tr')).data();
              //alert(JSON.stringify(rowData, null, 2));
              loadEmployeesData(rowData);
          });

          // Eliminar un empleado
          $('#employeesTable').on('click', '.delete-btn', function () {
              const id = $(this).data('id');// Obtener el ID del empleado
              if (confirm("¿Estás seguro de que deseas eliminar este empleado?")) {
                  deleteEmployee(id);// Invoca a la función para eliminar el empleado
              }
          });
      });

      // Cargar los datos en los TextBox y DDL para actualizar
      function loadEmployeesData(rowData) {
          $('#<%= HFEmployeeID.ClientID %>').val(rowData.EmployeeID);
          $('#<%= TBIdentification.ClientID %>').val(rowData.Identification);
          $('#<%= TBName.ClientID %>').val(rowData.Name);
          $('#<%= TBLastName.ClientID %>').val(rowData.LastName);
          $('#<%= TBCellPhone.ClientID %>').val(rowData.CellPhone);
          $('#<%= TBAddress.ClientID %>').val(rowData.Address);
          $('#<%= TBEmail.ClientID %>').val(rowData.Email);
      }

      // Función para eliminar un empleado
      function deleteEmployee(id) {
          $.ajax({
              type: "POST",
              url: "WFEmployees.aspx/DeleteEmployee",// Se invoca el WebMethod Eliminar un empleado
              contentType: "application/json; charset=utf-8",
              data: JSON.stringify({ id: id }),
              success: function (response) {
                  $('#employeesTable').DataTable().ajax.reload();// Recargar la tabla después de eliminar
                  alert("Empleado eliminado exitosamente.");
              },
              error: function () {
                  alert("Error al eliminar el empleado.");
              }
          });
      }
  </script>
</asp:Content>