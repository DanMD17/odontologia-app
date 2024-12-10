using Logic;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentation
{
    public partial class Index : System.Web.UI.Page
    {
        //Crear los objetos
        UsersLog objUser = new UsersLog();
        QuotesLog objQuotes = new QuotesLog();
        MaterialsLog objMaterial = new MaterialsLog();
        PatientsLog objPat = new PatientsLog();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                showCountUsers();
                showCountQuotes();
                showCountMaterials();
                showCountPatients();
            }
            validatePermissionRol();

        }

        [WebMethod]
        public static object ListCountQuotesDentists()
        {
            QuotesLog objQuotes = new QuotesLog();

            // Se obtiene un DataSet que contiene la lista de productos que existen por categoria
            var dataSet = objQuotes.showCountQuotesDentists();

            // Se crea una lista para almacenar las cantidades que de productos x categorias 
            var quoDentList = new List<object>();

            // Se itera sobre cada fila del DataSet.
            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                quoDentList.Add(new
                {
                    DentistName = row["NombreOdontologo"],
                    TotalQuotes = row["TotalCitas"],
                });
            }

            // Devuelve un objeto en formato JSON que contiene la lista de productos x categorias.
            return new { data = quoDentList };
        }

        [WebMethod]
        public static object ListQuotesPerMonth()
        {
            QuotesLog objQuotes = new QuotesLog();

            // Se obtiene un DataSet que contiene la lista de productos que existen por categoria
            var dataSet = objQuotes.showQuotesPerMonth();

            // Se crea una lista para almacenar las cantidades que de productos x categorias 
            var quoMonthList = new List<object>();

            // Se itera sobre cada fila del DataSet.
            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                quoMonthList.Add(new
                {
                    Month = row["mes"],
                    TotalQuotes = row["total_citas"],
                });
            }

            // Devuelve un objeto en formato JSON que contiene la lista de productos x categorias.
            return new { data = quoMonthList };
        }

        [WebMethod]
        public static object ListUsersPerRole()
        {
            UsersLog objUserLog = new UsersLog();

            // Llama al método lógico que ya tienes para obtener el DataSet
            var dataSet = objUserLog.showUsersPerRol();

            // Crear una lista para almacenar la información estructurada
            var usersRoleList = new List<object>();

            // Itera sobre las filas del DataSet
            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                usersRoleList.Add(new
                {
                    RoleName = row["rol_nombre"].ToString(),
                    TotalUsers = Convert.ToInt32(row["total_usuarios"]),
                });
            }

            // Devuelve los datos en formato JSON
            return new { data = usersRoleList };
        }

        // Metodo para validar permisos roles
        private void validatePermissionRol()
        {
            // Se Obtiene el usuario actual desde la sesión
            var objUser = (User)Session["User"];

            // Variable para acceder a la MasterPage y modificar la visibilidad de los enlaces.
            var masterPage = (Main)Master;

            if (objUser == null)
            {
                // Redirige a la página de inicio de sesión si el usuario no está autenticado
                Response.Redirect("WFDefault.aspx");
                return;
            }
            // Obtener el rol del usuario
            var userRole = objUser.Rol.Nombre;

            if (userRole == "Administrador")
            {
                //LblMsg.Text = "Bienvenido, Administrador!";

                foreach (var permiso in objUser.Permisos)
                {
                    switch (permiso.Nombre)
                    {
                        case "CREAR":

                            break;
                        case "ACTUALIZAR":

                            break;
                        case "MOSTRAR":
                            //LblMsg.Text += " Tienes permiso de Mostrar!";
                            break;
                        case "ELIMINAR":
                            //LblMsg.Text += " Tienes permiso de Eliminar!";
                            break;
                        default:
                            // Si el permiso no coincide con ninguno de los casos anteriores
                            LblMsg.Text += $" Permiso desconocido: {permiso.Nombre}";
                            break;
                    }
                }
            }
            else if (userRole == "Odontologo")
            {
                masterPage.SecurityMenu.Visible = false; // Ocultar el menú Seguridad

                //masterPage.linkUsers.Visible = false;// Se oculta el enlace de Usuario
                //masterPage.linkPermission.Visible = false;
                //masterPage.linkPermissionRol.Visible = false;
                //masterPage.linkRoles.Visible = false;// Se oculta el enlace de Permiso Rol

                foreach (var permiso in objUser.Permisos)
                {
                    switch (permiso.Nombre)
                    {
                        case "CREAR":

                            break;
                        case "ACTUALIZAR":

                            break;
                        case "MOSTRAR":
                            //LblMsg.Text += " Tienes permiso de Mostrar!";
                            break;
                        case "ELIMINAR":
                            //LblMsg.Text += " Tienes permiso de Eliminar!";
                            break;
                        default:
                            // Si el permiso no coincide con ninguno de los casos anteriores
                            LblMsg.Text += $" Permiso desconocido: {permiso.Nombre}";
                            break;
                    }
                }

            }
            else if (userRole == "Secretaria")
            {
                masterPage.SecurityMenu.Visible = false; // Ocultar el menú Seguridad
                
                //masterPage.linkUsers.Visible = false;
                //masterPage.linkPermission.Visible = false;
                //masterPage.linkPermissionRol.Visible = false;
                //masterPage.linkRoles.Visible = false;

                foreach (var permiso in objUser.Permisos)
                {
                    switch (permiso.Nombre)
                    {
                        case "CREAR":

                            break;
                        case "ACTUALIZAR":

                            break;
                        case "MOSTRAR":

                            break;
                        case "ELIMINAR":

                            break;
                        default:
                            // Si el permiso no coincide con ninguno de los casos anteriores
                            LblMsg.Text += $" Permiso desconocido: {permiso.Nombre}";
                            break;
                    }
                }
            }
            else if (userRole == "Auxiliar")
            {
                masterPage.SecurityMenu.Visible = false; // Ocultar el menú Seguridad 
                
                //masterPage.linkUsers.Visible = false;
                //masterPage.linkPermission.Visible = false;
                //masterPage.linkPermissionRol.Visible = false;
                //masterPage.linkRoles.Visible = false;

                foreach (var permiso in objUser.Permisos)
                {
                    switch (permiso.Nombre)
                    {
                        case "CREAR":

                            break;
                        case "ACTUALIZAR":
                            break;
                        case "MOSTRAR":
                            break;
                        case "ELIMINAR":
                            break;
                        default:
                            // Si el permiso no coincide con ninguno de los casos anteriores
                            LblMsg.Text += $" Permiso desconocido: {permiso.Nombre}";
                            break;
                    }
                }
            }
            else
            {
                // Si el rol no es reconocido, se deniega el acceso
                LblMsg.Text = "Rol no reconocido. No tienes permisos suficientes para acceder a esta página.";
                Response.Redirect("Index.aspx");
            }
        }

        private void showCountUsers()
        {
            int count = objUser.showCountUsers();
            LblCantUsu.Text = count.ToString();
        }

        private void showCountQuotes()
        {
            int count = objQuotes.showCountQuotes();
            LblCantQuo.Text = count.ToString();
        }

        private void showCountMaterials()
        {
            int count = objMaterial.showCountMaterials();
            LblCantMate.Text = count.ToString();
        }

        private void showCountPatients()
        {
            int count = objPat.showCountPatients();
            LblCantPaci.Text = count.ToString();
        }
    }
}