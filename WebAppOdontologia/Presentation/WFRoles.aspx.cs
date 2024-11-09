using Logic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentation
{
    public partial class WFRoles : System.Web.UI.Page
    {
        // Crear el objeto de la capa lógica para roles
        RolesLog objRol = new RolesLog();

        private int _id;
        private string _nombre, _descripcion;
        private bool executed;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Llamar al método para cargar los roles en el DropDownList
                showRolesDDL();
            }
        }

        // Método para listar roles
        [WebMethod]
        public static object ListRoles()
        {
            RolesLog objRol = new RolesLog();

            // Obtener el DataSet con la lista de roles desde la base de datos
            var dataSet = objRol.showRoles();

            // Crear una lista para almacenar los roles
            var rolesList = new List<object>();

            // Iterar sobre cada fila en el DataSet (cada fila representa un rol)
            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                rolesList.Add(new
                {
                    RoleID = row["rol_id"],
                    Name = row["rol_nombre"],
                    Description = row["rol_descripcion"]
                });
            }

            // Devolver el resultado en formato JSON
            return new { data = rolesList };
        }

        // Método para eliminar un rol
        [WebMethod]
        public static bool DeleteRole(int id)
        {
            // Crear una instancia de la clase de lógica de Roles
            RolesLog objRol = new RolesLog();

            // Invocar el método de eliminación de rol y devolver el resultado
            return objRol.deleteRol(id);
        }

        // Método para mostrar los roles en el DDL
        private void showRolesDDL()
        {
            DDLRoles.DataSource = objRol.showRoles();  // Método que retorna roles
            DDLRoles.DataValueField = "rol_id"; // La llave primaria
            DDLRoles.DataTextField = "rol_nombre"; // El nombre del rol
            DDLRoles.DataBind();
            DDLRoles.Items.Insert(0, "Seleccione");
        }


        // Método para limpiar los TextBox
        private void clear()
        {
            HFRolesID.Value = "";
            TBName.Text = "";
            TBDescription.Text = "";
            DDLRoles.SelectedIndex = 0;
        }

        // Botón de guardar un rol
        protected void BtnSaveRole_Click(object sender, EventArgs e)
        {
            // Asignar los valores de los campos a las variables
            _roleName = TBRolName.Text;
            _roleDescription = TBRolDescription.Text;

            // Ejecutar el método de la lógica para guardar el rol
            executed = objRol.saveRole(_roleName, _roleDescription);

            // Verificar si el rol se guardó exitosamente
            if (executed)
            {
                LblMsg.Text = "El rol se guardó exitosamente!";
            }
            else
            {
                LblMsg.Text = "Error al guardar el rol :(";
            }
        }


        // Evento al hacer clic en el botón Actualizar
        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            // Verificar si se ha seleccionado un rol para actualizar
            if (string.IsNullOrEmpty(HFRolesID.Value))
            {
                LblMsg.Text = "No se ha seleccionado un rol para actualizar.";
                return;
            }

            _id = Convert.ToInt32(HFRolesID.Value);
            _nombre = TBName.Text;
            _descripcion = TBDescription.Text;

            executed = objRol.updateRol(_id, _nombre, _descripcion);


            if (executed)
            {
                LblMsg.Text = "Rol actualizado exitosamente!";
                showRolesDDL(); // Actualiza el DropDownList de roles
                clear(); // Limpia los campos después de actualizar
            }
            else
            {
                LblMsg.Text = "Error al actualizar el rol.";
            }
        }
    }
}
