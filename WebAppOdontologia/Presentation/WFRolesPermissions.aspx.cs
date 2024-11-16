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
    public partial class WFRolesPermissions : System.Web.UI.Page
    {
        //Crear los objetos
        PermissionsLog objPer = new PermissionsLog();
        RolesLog objRol = new RolesLog();
        PermisoRolLog objPerRol = new PermisoRolLog();

        private int _id, _fkRol, _fkPermiso;
        private DateTime _date;
        private bool executed = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Se asigna la fecha actual al TextBox en formato "yyyy-MM-dd".
                TBDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                //Aqui se invocan todos los metodos
                showRolesDDL();
                showPermissionsDDL();
            }
            //validatePermissionRol();
        }
        [WebMethod]
        public static object ListPermissionsRoles()
        {
            PermisoRolLog objPerRol = new PermisoRolLog();

            // Se obtiene un DataSet que contiene la lista de los permisos roles desde la base de datos.
            var dataSet = objPerRol.showPermissionRol();

            // Se crea una lista para almacenar los permisos roles que se van a devolver.
            var permissionsRolesList = new List<object>();

            // Se itera sobre cada fila del DataSet (que representa un permiso rol).
            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                permissionsRolesList.Add(new
                {
                    RolPermisoID = row["rol_permiso"],
                    RolID = row["tbl_roles_rol_id"],
                    NameRol = row["rol_nombre"],
                    PermissionID = row["tbl_permisos_id_per"],
                    NamePermission = row["per_nombre"],
                    Date = Convert.ToDateTime(row["per_rol_fecha_asignacion"]).ToString("yyyy-MM-dd") // Formato de fecha específico.
                });
            }

            // Devuelve un objeto en formato JSON que contiene la lista de permisos roles.
            return new { data = permissionsRolesList };
        }

        [WebMethod]
        public static bool DeletePermissionsRoles(int id)
        {
            // Crear una instancia de la clase de lógica de permisos roles
            PermisoRolLog objPerRol = new PermisoRolLog();

            // Invocar al método para eliminar el permiso rol y devolver el resultado
            return objPerRol.deletePermissionRol(id);
        }

        // Metodo para validar permisos roles
        //private void validatePermissionRol()
        //{
        //    // Se Obtiene el usuario actual desde la sesión
        //    var objUser = (User)Session["User"];

        //    // Variable para acceder a la MasterPage y modificar la visibilidad de los enlaces.
        //    var masterPage = (Main)Master;

        //    if (objUser == null)
        //    {
        //        // Redirige a la página de inicio de sesión si el usuario no está autenticado
        //        Response.Redirect("WFDefault.aspx");
        //        return;
        //    }
        //    // Obtener el rol del usuario
        //    var userRole = objUser.Rol.Nombre;

        //    if (userRole == "Administrador")
        //    {
        //        //LblMsg.Text = "Bienvenido, Administrador!";

        //        foreach (var permiso in objUser.Permisos)
        //        {
        //            switch (permiso.Nombre)
        //            {
        //                case "CREAR":

        //                    break;
        //                case "ACTUALIZAR":

        //                    break;
        //                case "MOSTRAR":
        //                    //LblMsg.Text += " Tienes permiso de Mostrar!";
        //                    break;
        //                case "ELIMINAR":
        //                    //LblMsg.Text += " Tienes permiso de Eliminar!";
        //                    break;
        //                default:
        //                    // Si el permiso no coincide con ninguno de los casos anteriores
        //                    LblMsg.Text += $" Permiso desconocido: {permiso.Nombre}";
        //                    break;
        //            }
        //        }
        //    }
        //    else if (userRole == "Auxiliar")
        //    {
        //        //LblMsg.Text = "Bienvenido, Gerente!";

        //        masterPage.linkUser.Visible = false;// Se oculta el enlace de Usuario
        //        masterPage.linkPermission.Visible = false;
        //        masterPage.linkPermissionRol.Visible = false;// Se oculta el enlace de Permiso Rol

        //        foreach (var permiso in objUser.Permisos)
        //        {
        //            switch (permiso.Nombre)
        //            {
        //                case "CREAR":

        //                    break;
        //                case "ACTUALIZAR":

        //                    break;
        //                case "MOSTRAR":
        //                    //LblMsg.Text += " Tienes permiso de Mostrar!";
        //                    break;
        //                case "ELIMINAR":
        //                    //LblMsg.Text += " Tienes permiso de Eliminar!";
        //                    break;
        //                default:
        //                    // Si el permiso no coincide con ninguno de los casos anteriores
        //                    LblMsg.Text += $" Permiso desconocido: {permiso.Nombre}";
        //                    break;
        //            }
        //        }

        //    }
        //    else if (userRole == "Secretaria")
        //    {
        //        //LblMsg.Text = "Bienvenido, Secretaria!";
        //        masterPage.linkUsers.Visible = false;
        //        masterPage.linkPermission.Visible = false;
        //        masterPage.linkPermissionRol.Visible = false;

        //        foreach (var permiso in objUser.Permisos)
        //        {
        //            switch (permiso.Nombre)
        //            {
        //                case "CREAR":

        //                    break;
        //                case "ACTUALIZAR":

        //                    break;
        //                case "MOSTRAR":

        //                    break;
        //                case "ELIMINAR":

        //                    break;
        //                default:
        //                    // Si el permiso no coincide con ninguno de los casos anteriores
        //                    LblMsg.Text += $" Permiso desconocido: {permiso.Nombre}";
        //                    break;
        //            }
        //        }
        //    }
        //    else
        //    {
        //        // Si el rol no es reconocido, se deniega el acceso
        //        LblMsg.Text = "Rol no reconocido. No tienes permisos suficientes para acceder a esta página.";
        //        Response.Redirect("WFInicio.aspx");
        //    }
        //}
        //Metodo para mostrar los roles en el DDL
        private void showRolesDDL()
        {
            DDLRoles.DataSource = objRol.showRolesDDL();
            DDLRoles.DataValueField = "rol_id";//Nombre de la llave primaria
            DDLRoles.DataTextField = "rol_nombre";
            DDLRoles.DataBind();
            DDLRoles.Items.Insert(0, "Seleccione");
        }
        //Metodo para mostrar los permisos en el DDL
        private void showPermissionsDDL()
        {
            DDLPermisos.DataSource = objPer.showPermissionDDl();
            DDLPermisos.DataValueField = "per_id";//Nombre de la llave primaria
            DDLPermisos.DataTextField = "per_nombre";
            DDLPermisos.DataBind();
            DDLPermisos.Items.Insert(0, "Seleccione");
        }
        //Metodo para limpiar los TextBox y los DDL
        private void clear()
        {
            HFRolPermisoID.Value = "";
            DDLRoles.SelectedIndex = 0;
            DDLPermisos.SelectedIndex = 0;
            TBDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
        }
        //Eventos que se ejecutan cuando se da clic en los botones
        protected void BtnGuardar_Click(object sender, EventArgs e)
        {
            _fkRol = Convert.ToInt32(DDLRoles.SelectedValue);
            _fkPermiso = Convert.ToInt32(DDLPermisos.SelectedValue);
            _date = DateTime.Parse(TBDate.Text);

            executed = objPerRol.savePermissionRol(_fkRol, _fkPermiso, _date);

            if (executed)
            {
                LblMsg.Text = "El permiso rol se guardo exitosamente!";
                clear();//Se invoca el metodo para limpiar los campos 
            }
            else
            {
                LblMsg.Text = "Error al guardar";
            }
        }
        protected void BtnActualizar_Click(object sender, EventArgs e)
        {
            // Verifica si se ha seleccionado un permiso rol para actualizar
            if (string.IsNullOrEmpty(HFRolPermisoID.Value))
            {
                LblMsg.Text = "No se ha seleccionado un permiso rol para actualizar.";
                return;
            }
            _id = Convert.ToInt32(HFRolPermisoID.Value);
            _fkRol = Convert.ToInt32(DDLRoles.SelectedValue);
            _fkPermiso = Convert.ToInt32(DDLPermisos.SelectedValue);
            _date = DateTime.Parse(TBDate.Text);

            executed = objPerRol.updatePermissionRol(_id, _fkRol, _fkPermiso, _date);

            if (executed)
            {
                LblMsg.Text = "El permiso rol se actualizo exitosamente!";
                clear();//Se invoca el metodo para limpiar los campos 
            }
            else
            {
                LblMsg.Text = "Error al actualizar";
            }
        }
    }
}