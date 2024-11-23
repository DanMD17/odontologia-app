﻿using Logic;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Remoting;
using System.Web;
using System.Web.Security;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentation
{
    public partial class WFPermissions : System.Web.UI.Page
    {

        PermissionsLog objPer = new PermissionsLog();
        private int _id;
        private string _name, _description;
        private bool executed = false;

        /*
         *  Variables de tipo pública que indiquen si el usuario tiene
         *  permiso para ver los botones editar y eliminar.
         */
        public bool _showEditButton { get; set; } = false;
        public bool _showDeleteButton { get; set; } = false;

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                // Los botones y otros elementos se inicializan en false, no visibles.
                BtnSave.Visible = false;
                BtnUpdate.Visible = false;
                FrmPermission.Visible = false;
                PanelAdmin.Visible = false;

            }validatePermissionRol();
        }
        //Metodo para mostrar todos los Permisos
        [WebMethod]
        public static object ListPermissions()
        {
            PermissionsLog objPer = new PermissionsLog();

            // Se obtiene un DataSet que contiene la lista de Permisos desde la base de datos.
            var dataSet = objPer.showPermissions();

            // Se crea una lista para almacenar los Roles que se van a devolver.
            var permisosList = new List<object>();

            // Se itera sobre cada fila del DataSet (que representa un consultorio).
            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                permisosList.Add(new
                {
                    PermissionID = row["per_id"],
                    Name = row["per_nombre"],
                    Description = row["per_descripcion"]
                });
            }
            // Devuelve un objeto en formato JSON que contiene la lista de productos.
            return new { data = permisosList };
        }
        [WebMethod]

        //Método para eliminar un Permiso
        public static bool DeletePermission(int id)
        {
            // Crear una instancia de la clase de lógica de rol
            PermissionsLog objPer = new PermissionsLog();

            // Invocar al método para eliminar el Rol y devolver el resultado
            return objPer.deletePermision(id);
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
                            FrmPermission.Visible = true;// Se pone visible el formulario
                            BtnSave.Visible = true;// Se pone visible el boton guardar
                            break;
                        case "ACTUALIZAR":
                            FrmPermission.Visible = true;
                            BtnUpdate.Visible = true;
                            PanelAdmin.Visible = true;
                            _showEditButton = true;
                            break;
                        case "MOSTRAR":
                            //LblMsg.Text += " Tienes permiso de Mostrar!";
                            PanelAdmin.Visible = true;
                            break;
                        case "ELIMINAR":
                            //LblMsg.Text += " Tienes permiso de Eliminar!";
                            PanelAdmin.Visible = true;
                            _showDeleteButton = true;
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
                //masterPage.linkPermissionRol.Visible = false;// Se oculta el enlace de Permiso Rol
                //masterPage.linkRoles.Visible = false;

                foreach (var permiso in objUser.Permisos)
                {
                    switch (permiso.Nombre)
                    {
                        case "CREAR":
                            FrmPermission.Visible = true;
                            BtnSave.Visible = true;
                            PanelAdmin.Visible = true;
                            break;
                        case "ACTUALIZAR":
                            FrmPermission.Visible = true;
                            BtnUpdate.Visible = true;
                            PanelAdmin.Visible = true;
                            _showEditButton = true;
                            break;
                        case "MOSTRAR":
                            //LblMsg.Text += " Tienes permiso de Mostrar!";
                            PanelAdmin.Visible = true;
                            break;
                        case "ELIMINAR":
                            //LblMsg.Text += " Tienes permiso de Eliminar!";
                            PanelAdmin.Visible = true;
                            _showDeleteButton = true;
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
                            FrmPermission.Visible = true;
                            BtnSave.Visible = true;
                            PanelAdmin.Visible = true;
                            break;
                        case "ACTUALIZAR":
                            FrmPermission.Visible = true;
                            BtnUpdate.Visible = true;
                            PanelAdmin.Visible = true;
                            _showEditButton = true;
                            break;
                        case "MOSTRAR":
                            PanelAdmin.Visible = true;
                            break;
                        case "ELIMINAR":
                            PanelAdmin.Visible = true;
                            _showDeleteButton = true;
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
                            FrmPermission.Visible = true;
                            BtnSave.Visible = true;
                            PanelAdmin.Visible = true;
                            break;
                        case "ACTUALIZAR":
                            FrmPermission.Visible = true;
                            BtnUpdate.Visible = true;
                            PanelAdmin.Visible = true;
                            _showEditButton = true;
                            break;
                        case "MOSTRAR":
                            PanelAdmin.Visible =true ;
                            break;
                        case "ELIMINAR":
                            PanelAdmin.Visible = true;
                            _showDeleteButton = true;
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

        private void clear()
        {
            HFPermissionID.Value = "";
            DDLNombrePer.SelectedIndex = 0;
            TBDescription.Text = "";
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            _name = DDLNombrePer.Text.ToUpper();
            _description = TBDescription.Text;

            executed = objPer.savePermission(_name, _description);

            if (executed)
            {
                LblMsg.Text = "Se guardó exitosamente ";

                clear();
            }
            else
            {
                LblMsg.Text = "Error al guardar ";
            }

        }

        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            // Verifica si se ha seleccionado un Permiso  para actualizar
            if (string.IsNullOrEmpty(HFPermissionID.Value))
            {
                LblMsg.Text = "No se ha seleccionado un Permiso para actualizar.";
                return;
            }

            _id = Convert.ToInt32(HFPermissionID.Value);
            _name = DDLNombrePer.Text.ToUpper();
            _description = TBDescription.Text;

            executed = objPer.updatePermision(_id, _name, _description);

            if (executed)
            {
                LblMsg.Text = "Se ACTUALIZÓ exitosamente ";

                clear();
            }
            else
            {
                LblMsg.Text = "Error al ACTUALIZAR ";
            }
        }
    }
}