using Logic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentation
{
    public partial class WFRolesPermissions : System.Web.UI.Page
    {
        // Crear los objetos de lógica
        RolesLog objRol = new RolesLog();
        PermissionsLog objPer = new PermissionsLog();
        Roles_PermissionLog objRolPer = new Roles_PermissionLog();

        private int _idRol, _idPermiso;
        private bool executed;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Invocar métodos para mostrar roles y permisos en los DDL
                showRoleDDL();
                showPermissionDDL();
            }
        }

        // Método para listar las relaciones de roles y permisos
        [WebMethod]
        public static object ListRolesPermissions()
        {
            Roles_PermissionLog objRolPer = new Roles_PermissionLog();
            var dataSet = objRolPer.showRolesPermissions();
            var rolesPermissionsList = new List<object>();

            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                rolesPermissionsList.Add(new
                {
                    RoleID = row["rol_id"],
                    RoleName = row["rol_nombre"],
                    PermissionID = row["per_id"],
                    PermissionName = row["per_nombre"]
                });
            }

            return new { data = rolesPermissionsList };
        }

        // Método para mostrar los roles en el DropDownList
        private void showRoleDDL()
        {
            DDLRoles.DataSource = objRol.showRolesDDL();  // 
            DDLRoles.DataValueField = "rol_id";  // llave primaria
            DDLRoles.DataTextField = "rol_nombre";  // nombre del rol
            DDLRoles.DataBind();
            DDLRoles.Items.Insert(0, new ListItem("Seleccione", "0"));
        }

        // Método para mostrar los permisos en el DropDownList
        private void showPermissionDDL()
        {
            DDLPermissions.DataSource = objPer.showPermissionsDDL();  // 
            DDLPermissions.DataValueField = "permiso_id";  // llave primaria
            DDLPermissions.DataTextField = "permiso_nombre";  // nombre del permiso
            DDLPermissions.DataBind();
            DDLPermissions.Items.Insert(0, new ListItem("Seleccione", "0"));
        }

        // Método para limpiar los controles
        private void clear()
        {
            HFRolePermissionID.Value = "";
            DDLRoles.SelectedIndex = 0;
            DDLPermissions.SelectedIndex = 0;
        }

        // Evento para guardar el permiso asignado a un rol
        protected void BtnSave_Click(object sender, EventArgs e)
        {
            // Obtener los valores seleccionados en los DDL
            _idRol = Convert.ToInt32(DDLRoles.SelectedValue);
            _idPermiso = Convert.ToInt32(DDLPermissions.SelectedValue);

            if (_idRol == 0 || _idPermiso == 0)
            {
                LblMsg.Text = "Debe seleccionar un rol y un permiso.";
                return;
            }

            executed = objRolPer.saveRolePermission(_idRol, _idPermiso);

            if (executed)
            {
                LblMsg.Text = "El permiso se asignó exitosamente al rol!";
            }
            else
            {
                LblMsg.Text = "Error al asignar el permiso.";
            }
        }

        // Evento para eliminar una asignación de permiso a rol
        [WebMethod]
        public static bool DeleteRolePermission(int id)
        {
            Roles_PermissionLog objRolPer = new Roles_PermissionLog();
            return objRolPer.deleteRolePermission(id);
        }

        // Evento para actualizar la asignación de un permiso a rol
        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(HFRolePermissionID.Value))
            {
                LblMsg.Text = "No se ha seleccionado una asignación para actualizar.";
                return;
            }

            int rolePermissionID = Convert.ToInt32(HFRolePermissionID.Value);
            _tbl_roles_rol_id = Convert.ToInt32(DDLRoles.SelectedValue);
            _tbl_permiso_id_per = Convert.ToInt32(DDLPermissions.SelectedValue);

            executed = objRolPer.updateRolePermission(rolePermissionID, _tbl_roles_rol_id, _tbl_permiso_id_per);

            if (executed)
            {
                LblMsg.Text = "La asignación se actualizó exitosamente!";
                clear();
            }
            else
            {
                LblMsg.Text = "Error al actualizar la asignación";
            }
        }
    }
}
