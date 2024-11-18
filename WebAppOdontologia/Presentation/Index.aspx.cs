using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentation
{
    public partial class Index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {

            }
            validatePermissionRol();

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
                //LblMsg.Text = "Bienvenido, Gerente!";

                masterPage.linkUsers.Visible = false;// Se oculta el enlace de Usuario
                masterPage.linkPermission.Visible = false;
                masterPage.linkPermissionRol.Visible = false;
                masterPage.linkRoles.Visible = false;// Se oculta el enlace de Permiso Rol

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
                //LblMsg.Text = "Bienvenido, Secretaria!";
                masterPage.linkUsers.Visible = false;
                masterPage.linkPermission.Visible = false;
                masterPage.linkPermissionRol.Visible = false;
                masterPage.linkRoles.Visible = false;

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
                //LblMsg.Text = "Bienvenido, Secretaria!";
                masterPage.linkUsers.Visible = false;
                masterPage.linkPermission.Visible = false;
                masterPage.linkPermissionRol.Visible = false;
                masterPage.linkRoles.Visible = false;

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
    }
}