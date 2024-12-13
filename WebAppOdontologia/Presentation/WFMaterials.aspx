<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="WFMaterials.aspx.cs" Inherits="Presentation.WFMaterials" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--Estilos--%>
    <link href="resources/css/datatables.min.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="card m-1">
        <div class="card-header">
            Gestión de Materiales
       
       
        </div>
        <div class="card-body">
            <form id="FrmMaterials" runat="server">
                <%--Id--%>
                <asp:HiddenField ID="HFMaterialsID" runat="server" />
                <div class="row m-1">
                    <div class="col-3">
                        <%--Nombre del material--%>
                        <asp:Label ID="Label1" CssClass="form-label" runat="server" Text="Ingrese el nombre del material"></asp:Label>
                        <asp:DropDownList ID="DDLMate" CssClass="form-select" runat="server">
                            <asp:ListItem Value="0">Seleccione</asp:ListItem>
                            <asp:ListItem Value="Pasta profiláctica">Pasta profiláctica</asp:ListItem>
                            <asp:ListItem Value="Cepillos profilácticos">Cepillos profilácticos</asp:ListItem>
                            <asp:ListItem Value="Puntas de ultrasonido">Puntas de ultrasonido</asp:ListItem>
                            <asp:ListItem Value="Hilo dental">Hilo dental</asp:ListItem>
                            <asp:ListItem Value="Enjuague bucal antiséptico">Enjuague bucal antiséptico</asp:ListItem>
                            <asp:ListItem Value="Irrigadores o jeringas de agua">Irrigadores o jeringas de agua</asp:ListItem>
                            <asp:ListItem Value="Amalgama dental">Amalgama dental</asp:ListItem>
                            <asp:ListItem Value="Resinas compuestas">Resinas compuestas</asp:ListItem>
                            <asp:ListItem Value="Adhesivos dentales">Adhesivos dentales</asp:ListItem>
                            <asp:ListItem Value="Ácido grabador">Ácido grabador</asp:ListItem>
                            <asp:ListItem Value="Aplicadores de resina y adhesivo">Aplicadores de resina y adhesivo</asp:ListItem>
                            <asp:ListItem Value="Matrices dentales">Matrices dentales</asp:ListItem>
                            <asp:ListItem Value="Porta amalgamas o porta composite">Porta amalgamas o porta composite</asp:ListItem>
                            <asp:ListItem Value="Espátulas y cucharillas de dentina">Espátulas y cucharillas de dentina</asp:ListItem>
                            <asp:ListItem Value="Limas de endodoncia">Limas de endodoncia</asp:ListItem>
                            <asp:ListItem Value="Selladores de conductos">Selladores de conductos</asp:ListItem>
                            <asp:ListItem Value="Jeringas de irrigación">Jeringas de irrigación</asp:ListItem>
                            <asp:ListItem Value="Hipoclorito de sodio">Hipoclorito de sodio</asp:ListItem>
                            <asp:ListItem Value="Hidróxido de calcio">Hidróxido de calcio</asp:ListItem>
                            <asp:ListItem Value="Localizadores de ápice">Localizadores de ápice</asp:ListItem>
                            <asp:ListItem Value="Micromotor endodóntico">Micromotor endodóntico</asp:ListItem>
                            <asp:ListItem Value="Forceps dentales">Forceps dentales</asp:ListItem>
                            <asp:ListItem Value="Elevadores o botadores">Elevadores o botadores</asp:ListItem>
                            <asp:ListItem Value="Cucharillas de alveolo">Cucharillas de alveolo</asp:ListItem>
                            <asp:ListItem Value="Pinzas hemostáticas">Pinzas hemostáticas</asp:ListItem>
                            <asp:ListItem Value="Suturas">Suturas</asp:ListItem>
                            <asp:ListItem Value="Porta agujas">Porta agujas</asp:ListItem>
                            <asp:ListItem Value="Gasas estériles">Gasas estériles</asp:ListItem>
                            <asp:ListItem Value="Brackets">Brackets</asp:ListItem>
                            <asp:ListItem Value="Arcos metálicos">Arcos metálicos</asp:ListItem>
                            <asp:ListItem Value="Bandas y ligaduras elásticas">Bandas y ligaduras elásticas</asp:ListItem>
                            <asp:ListItem Value="Alicates de ortodoncia">Alicates de ortodoncia</asp:ListItem>
                            <asp:ListItem Value="Separadores interdentales">Separadores interdentales</asp:ListItem>
                            <asp:ListItem Value="Gel de blanqueamiento">Gel de blanqueamiento</asp:ListItem>
                            <asp:ListItem Value="Protector gingival">Protector gingival</asp:ListItem>
                            <asp:ListItem Value="Lámpara de luz LED o láser">Lámpara de luz LED o láser</asp:ListItem>
                            <asp:ListItem Value="Cubetas para blanqueamiento">Cubetas para blanqueamiento</asp:ListItem>
                            <asp:ListItem Value="Bisturíes y hojas de bisturí">Bisturíes y hojas de bisturí</asp:ListItem>
                            <asp:ListItem Value="Pinzas quirúrgicas">Pinzas quirúrgicas</asp:ListItem>
                            <asp:ListItem Value="Curetas quirúrgicas">Curetas quirúrgicas</asp:ListItem>
                            <asp:ListItem Value="Separadores">Separadores</asp:ListItem>
                            <asp:ListItem Value="Agujas de sutura">Agujas de sutura</asp:ListItem>
                            <asp:ListItem Value="Clorhexidina o solución salina">Clorhexidina o solución salina</asp:ListItem>
                            <asp:ListItem Value="Material de impresión">Material de impresión</asp:ListItem>
                            <asp:ListItem Value="Cubetas de impresión">Cubetas de impresión</asp:ListItem>
                            <asp:ListItem Value="Ceras dentales">Ceras dentales</asp:ListItem>
                            <asp:ListItem Value="Acrílico dental">Acrílico dental</asp:ListItem>
                            <asp:ListItem Value="Fresones de desgaste y ajuste">Fresones de desgaste y ajuste</asp:ListItem>
                            <asp:ListItem Value="Guantes estériles y no estériles">Guantes estériles y no estériles</asp:ListItem>
                            <asp:ListItem Value="Mascarillas quirúrgicas">Mascarillas quirúrgicas</asp:ListItem>
                            <asp:ListItem Value="Gorro y protector facial">Gorro y protector facial</asp:ListItem>
                            <asp:ListItem Value="Material para esterilización">Material para esterilización</asp:ListItem>
                            <asp:ListItem Value="Desinfectantes de superficies">Desinfectantes de superficies</asp:ListItem>
                            <asp:ListItem Value="Otro">Otro</asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RFVName" runat="server" ControlToValidate="DDLMate"
                            CssClass="text-danger" InitialValue="0" Display="Dynamic" ErrorMessage="Este campo es obligatorio."></asp:RequiredFieldValidator>
                    </div>
                    <div class="col">
                        <%--Descripcion del material--%>
                        <asp:Label ID="Label2" CssClass="form-label" runat="server" Text="Describa el material empleado"></asp:Label>
                        <asp:TextBox ID="TBmaterialDescription" CssClass="form-control" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RFVDescription" runat="server" ControlToValidate="TBmaterialDescription"
                            CssClass="text-danger" ErrorMessage="Este campo es obligatorio."></asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="row m-1">
                    <div class="col-4">
                        <%--Cantidad del material--%>
                        <asp:Label ID="Label3" CssClass="form-label" runat="server" Text="Introduzca la cantidad de material utilizado"></asp:Label>
                        <asp:TextBox ID="TBmaterialQuantity" CssClass="form-control" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RFVQuantity" runat="server" ControlToValidate="TBmaterialQuantity"
                            CssClass="text-danger" ErrorMessage="Este campo es obligatorio (valores enteros)."></asp:RequiredFieldValidator>
                    </div>
                    <div class="col-3">
                        <%--DDL Para Seleccionar tratamiento--%>
                        <asp:Label ID="Label4" CssClass="form-label" runat="server" Text="Seleccione un tratamiento"></asp:Label>
                        <asp:DropDownList ID="DDLTreatments" CssClass="form-select" runat="server"></asp:DropDownList>
                        <asp:Label ID="LblMsgtre" runat="server" CssClass="text-danger" Text=""></asp:Label>
                    </div>
                </div>
                <div class="row m-1">
                    <div class="col">
                        <%--Botones guardar y actualizar--%>
                        <asp:Button ID="BtnSave" CssClass="btn btn-success" runat="server" Text="Guardar" OnClick="BtnSave_Click" />
                        <asp:Button ID="BtnUpdate" CssClass="btn btn-primary" runat="server" Text="Actualizar" OnClick="BtnUpdate_Click" />
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
                Lista de materiales
           
           
            </div>
            <div class="card-body">
                <%--Lista de Materiales--%>
                <div class="table-responsive">
                    <%--Tabla de Materiales--%>
                    <table id="materialsTable" class="display" style="width: 100%">
                        <thead>
                            <tr>
                                <th>MaterialID</th>
                                <th>Nombre</th>
                                <th>Descripcion</th>
                                <th>Cantidad</th>
                                <th>FkTratamiento</th>
                                <th>Tratamiento</th>
                                <th>Fecha</th>
                                <th>Paciente</th>
                                <th>Acciones</th>
                                <!-- Nueva columna para acciones de edición y eliminación -->
                            </tr>
                        </thead>
                        <tbody></tbody>
                    </table>
                </div>
            </div>
        </asp:Panel>
    </div>


    <script src="resources/js/datatables.min.js" type="text/javascript"></script>

    <%--Script de la tabla de Materiales--%>
    <script type="text/javascript">
        $(document).ready(function () {
            const showEditButton = '<%= _showEditButton %>' === 'True';
            const showDeleteButton = '<%= _showDeleteButton %>' === 'True';
            $('#materialsTable').DataTable({
                "processing": true,
                "serverSide": false,
                "ajax": {
                    "url": "WFMaterials.aspx/ListMaterials", // WebMethod para listar materiales
                    "type": "POST",
                    "contentType": "application/json",
                    "data": function (d) {
                        return JSON.stringify(d); // Convierte los datos a JSON
                    },
                    "dataSrc": function (json) {
                        return json.d.data; // Obtiene la lista de materiales del resultado
                    }
                },
                "columns": [
                    { "data": "MaterialID" },
                    { "data": "Nombre" },
                    { "data": "Descripcion" },
                    { "data": "Cantidad" },
                    { "data": "FkTratamiento", "visible": false },
                    { "data": "TratamientoNombre" },
                    { "data": "TratamientoFecha" },
                    { "data": "NombrePaciente" },
                    {
                        "data": null,
                        "render": function (data, type, row) {
                            let buttons = '';
                            if (showEditButton) {
                                buttons += `<button class="btn btn-info edit-btn" data-id="${row.MaterialID}">Editar</button>`;
                            }
                            if (showDeleteButton) {
                                buttons += `<button class="btn btn-danger delete-btn" data-id="${row.MaterialID}">Eliminar</button>`;
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

            // Editar un material
            $('#materialsTable').on('click', '.edit-btn', function () {
                const rowData = $('#materialsTable').DataTable().row($(this).parents('tr')).data();
                loadMaterialData(rowData);
                $('#<%= BtnSave.ClientID %>').hide();
                $('#<%= BtnUpdate.ClientID %>').show();
            });

            // Eliminar un material
            $('#materialsTable').on('click', '.delete-btn', function () {
                const id = $(this).data('id'); // Obtener el ID del material
                if (confirm("¿Está seguro de que desea eliminar este material?")) {
                    deleteMaterial(id); // Invoca a la función para eliminar el material
                }
            });
        });

        // Función para cargar los datos del material en los TextBox y DDL para actualizar
        function loadMaterialData(rowData) {
            $('#<%= HFMaterialsID.ClientID %>').val(rowData.MaterialID);
            $('#<%= DDLMate.ClientID %>').val(rowData.Nombre);
            $('#<%= TBmaterialDescription.ClientID %>').val(rowData.Descripcion);
            $('#<%= TBmaterialQuantity.ClientID %>').val(rowData.Cantidad);
            $('#<%= DDLTreatments.ClientID %>').val(rowData.FkTratamiento);
        }

        // Función para eliminar un material
        function deleteMaterial(id) {
            $.ajax({
                type: "POST",
                url: "WFMaterials.aspx/DeleteMaterial", // WebMethod para eliminar un material
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify({ id: id }),
                success: function (response) {
                    $('#materialsTable').DataTable().ajax.reload(); // Recargar la tabla después de eliminar
                    alert("Material eliminado exitosamente.");
                },
                error: function () {
                    alert("Error al eliminar el material.");
                }
            });
        }
    </script>

</asp:Content>
<%--  --%>