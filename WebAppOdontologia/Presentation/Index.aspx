<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="Presentation.Index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="resources/css/styles.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Label ID="LblMsg" runat="server" Text=""></asp:Label>
    <div id="carouselExampleCaptions" class="carousel slide">
        <div class="carousel-indicators">
            <button type="button" data-bs-target="#carouselExampleCaptions" data-bs-slide-to="0" class="active" aria-current="true" aria-label="Slide 1"></button>
            <button type="button" data-bs-target="#carouselExampleCaptions" data-bs-slide-to="1" aria-label="Slide 2"></button>
            <button type="button" data-bs-target="#carouselExampleCaptions" data-bs-slide-to="2" aria-label="Slide 3"></button>
        </div>
        <div class="carousel-inner">
            <div class="carousel-item active">
                <img src="resources/images/odonto-imgPrin.jpg" class="d-block w-100 img-fluid" alt="...">
                <div class="carousel-caption d-none d-md-block">
                    <h5 class="texto-slides">¡Bienvenido al consultorio odontológico FUP!</h5>
                    <p></p>
                </div>
            </div>
            <div class="carousel-item">
                <img src="resources/images/odonto-img1.jpg" class="d-block w-100 img-fluid" alt="...">
                <div class="carousel-caption d-none d-md-block">
                    <h5 class="texto-slides">Gestiona pacientes, asigna citas, mira el historial clinico y asigna tratamientos</h5>
                    <p></p>
                </div>
            </div>
            <div class="carousel-item">
                <img src="resources/images/odonto-img2.jpg" class="d-block w-100 img-fluid" alt="...">
                <div class="carousel-caption d-none d-md-block">
                    <h5 class="texto-slides">Gestiona tus odontologos, secretarias y auxiliares</h5>
                    <p></p>
                </div>
            </div>
        </div>
        <button class="carousel-control-prev" type="button" data-bs-target="#carouselExampleCaptions" data-bs-slide="prev">
            <span class="carousel-control-prev-icon" aria-hidden="true"></span>
            <span class="visually-hidden">Previous</span>
        </button>
        <button class="carousel-control-next" type="button" data-bs-target="#carouselExampleCaptions" data-bs-slide="next">
            <span class="carousel-control-next-icon" aria-hidden="true"></span>
            <span class="visually-hidden">Next</span>
        </button>
    </div>

    <br>

    <div class="container-fluid d-flex flex-column align-items-center">
        <div class="row justify-content-center">
            <div class="col">
                <div class="card border-success mb-3" style="max-width: 18rem;">
                    <div class="card-body text-success">
                        <asp:Label ID="LblCantUsu" runat="server" Text="" CssClass="fs-4 fw-bold"></asp:Label>
                        <h5 class="card-title">Total Usuarios</h5>
                    </div>
                    <div class="card-footer bg-transparent border-success text-center">
                        <a class="small-box-footer" href="WFUsers.aspx">Mas info
                <i class="lni lni-chevron-right-circle"></i>
                        </a>
                    </div>
                </div>
            </div>

            <div class="col">
                <div class="card border-success mb-3" style="max-width: 18rem;">
                    <div class="card-body text-success">
                        <asp:Label ID="LblCantQuo" runat="server" Text="" CssClass="fs-4 fw-bold"></asp:Label>
                        <h5 class="card-title">Total Citas</h5>
                    </div>
                    <div class="card-footer bg-transparent border-success text-center">
                        <a class="small-box-footer" href="WFQuotes.aspx">Mas info
                   <i class="lni lni-chevron-right-circle"></i>
                        </a>
                    </div>
                </div>
            </div>

            <div class="col">
                <div class="card border-success mb-3" style="max-width: 18rem;">
                    <div class="card-body text-success">
                        <asp:Label ID="LblCantMate" runat="server" Text="" CssClass="fs-4 fw-bold"></asp:Label>
                        <h5 class="card-title">Total Materiales</h5>
                    </div>
                    <div class="card-footer bg-transparent border-success text-center">
                        <a class="small-box-footer" href="WFMaterials.aspx">Mas info
                    <i class="lni lni-chevron-right-circle"></i>
                        </a>
                    </div>
                </div>
            </div>

            <div class="col">
                <div class="card border-success mb-3" style="max-width: 18rem;">
                    <div class="card-body text-success">
                        <asp:Label ID="LblCantPaci" runat="server" Text="" CssClass="fs-4 fw-bold"></asp:Label>
                        <h5 class="card-title">Total Pacientes</h5>
                    </div>
                    <div class="card-footer bg-transparent border-success text-center">
                        <a class="small-box-footer" href="WFPatients.aspx">Mas info
                    <i class="lni lni-chevron-right-circle"></i>
                        </a>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="container-fluid d-flex flex-column align-items-center">
        <div class="row justify-content-center">
            <div class="col-6">
                <div class="card border-info mb-3">
                    <div class="card-header">
                        <i class="lni lni-bar-chart-4"></i>
                        Cantidad de citas por mes
                    </div>
                    <div class="card-body">
                        <div id="linechartQuoMonth" style="width: 100%; height: 100%; min-height: 400px;"></div>
                    </div>
                </div>
            </div>


            <div class="col-6">
                <div class="card border-info mb-3">
                    <div class="card-header">
                        <i class="lni lni-bar-chart-4"></i>
                        Cantidad de citas por dentista
                    </div>
                    <div class="card-body">
                        <div id="piechartQuoDent" style="width: 100%; height: 100%; min-height: 400px;"></div>
                    </div>
                </div>
            </div>

            <div class="col-6">
                <div class="card border-info mb-3">
                    <div class="card-header">
                        <i class="lni lni-bar-chart-4"></i>
                        Cantidad de usuarios por rol
                    </div>
                    <div class="card-body">
                        <div id="barchartUsersRoles" style="width: 100%; height: 100%; min-height: 400px;"></div>
                    </div>
                </div>
            </div>
        </div>
    </div>


    <!-- JQuery -->
    <script src="https://code.jquery.com/jquery-3.7.1.js" integrity="sha256-eKhayi8LEQwp4NKxN+CfCh+3qOVUtJn3QNZ0TciWLP4=" crossorigin="anonymous"></script>
    <!-- Load the Google Charts API -->
    <script src="https://www.gstatic.com/charts/loader.js"></script>

    <script type="text/javascript">
        // Carga la API de Google Charts
        google.charts.load('current', { 'packages': ['corechart'] });

        // Llama al WebMethod y dibuja el gráfico al cargar la API
        google.charts.setOnLoadCallback(fetchDataAndDrawChartMonth);

        // Función para obtener datos desde el WebMethod para el gráfico de citas por mes
        function fetchDataAndDrawChartMonth() {
            $.ajax({
                url: 'Index.aspx/ListQuotesPerMonth', // Ajustar con el nombre de tu archivo ASPX
                type: 'POST',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                success: function (response) {
                    // Procesar los datos devueltos por el WebMethod
                    var rawData = response.d.data;

                    // Crear la tabla de datos para Google Charts
                    var data = new google.visualization.DataTable();
                    data.addColumn('string', 'Mes');
                    data.addColumn('number', 'Total de Citas');

                    // Llenar la tabla con los datos del WebMethod
                    rawData.forEach(function (item) {
                        data.addRow([item.Month, parseInt(item.TotalQuotes)]);
                    });

                    // Configuración del gráfico
                    var options = {
                        title: 'Cantidad de citas por mes',
                        width: '100%',
                        height: 400,
                        chartArea: { width: '80%', height: '70%' },
                        hAxis: {
                            title: 'Meses',
                            titleTextStyle: { italic: false },
                        },
                        vAxis: {
                            title: 'Total de citas',
                            titleTextStyle: { italic: false },
                        },
                        legend: { position: 'none' },
                        curveType: 'function', // Suaviza las líneas
                    };

                    // Dibuja la gráfica de línea
                    var chart = new google.visualization.LineChart(document.getElementById('linechartQuoMonth'));
                    chart.draw(data, options);
                },
                error: function (error) {
                    console.error('Error al obtener los datos: ', error);
                }
            });
        }

        // Llama al WebMethod y dibuja el gráfico al cargar la API
        google.charts.setOnLoadCallback(fetchDataAndDrawChartDentist);

        // Función para obtener datos desde el WebMethod para el gráfico de citas por dentista
        function fetchDataAndDrawChartDentist() {
            $.ajax({
                url: 'Index.aspx/ListCountQuotesDentists', // Ajustar con el nombre de tu archivo ASPX
                type: 'POST',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                success: function (response) {
                    // Procesar los datos devueltos por el WebMethod
                    var rawData = response.d.data;

                    // Crear la tabla de datos para Google Charts
                    var data = new google.visualization.DataTable();
                    data.addColumn('string', 'Nombre del Odontólogo');
                    data.addColumn('number', 'Total de Citas');

                    // Llenar la tabla con los datos del WebMethod
                    rawData.forEach(function (item) {
                        data.addRow([item.DentistName, parseInt(item.TotalQuotes)]);
                    });

                    // Configuración del gráfico
                    var options = {
                        title: 'Cantidad de citas por dentista',
                        width: '100%',
                        height: 400,
                        chartArea: { width: '90%', height: '80%' }
                    };

                    // Dibuja la gráfica de pastel
                    var chart = new google.visualization.PieChart(document.getElementById('piechartQuoDent'));
                    chart.draw(data, options);
                },
                error: function (error) {
                    console.error('Error al obtener los datos: ', error);
                }
            });
        }

        // Llama al WebMethod y dibuja el gráfico al cargar la API
        google.charts.setOnLoadCallback(fetchDataAndDrawChartRoles);

        // Función para obtener datos desde el WebMethod para el gráfico de usuarios por rol
        function fetchDataAndDrawChartRoles() {
            $.ajax({
                url: 'Index.aspx/ListUsersPerRole', // URL al WebMethod
                type: 'POST',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                success: function (response) {
                    var rawData = response.d.data;

                    // Crear la tabla de datos para Google Charts
                    var data = new google.visualization.DataTable();
                    data.addColumn('string', 'Rol');
                    data.addColumn('number', 'Total Usuarios');

                    // Agregar datos
                    rawData.forEach(function (item) {
                        data.addRow([item.RoleName, parseInt(item.TotalUsers)]);
                    });

                    // Configuración del gráfico
                    var options = {
                        title: 'Cantidad de Usuarios por Rol',
                        width: '100%',
                        height: 400,
                        hAxis: {
                            title: 'Roles',
                            slantedText: true,
                            slantedTextAngle: 45
                        },
                        vAxis: {
                            title: 'Total Usuarios',
                            minValue: 0
                        },
                        chartArea: { width: '70%', height: '70%' },
                        legend: { position: 'none' }
                    };

                    // Dibuja el gráfico de columnas
                    var chart = new google.visualization.ColumnChart(document.getElementById('barchartUsersRoles'));
                    chart.draw(data, options);
                },
                error: function (error) {
                    console.error('Error al obtener los datos: ', error);
                }
            });
        }

        // Redibuja los gráficos al cambiar el tamaño de la ventana
        window.addEventListener('resize', function () {
            fetchDataAndDrawChartMonth();
            fetchDataAndDrawChartDentist();
            fetchDataAndDrawChartRoles();
        });
</script>


</asp:Content>

