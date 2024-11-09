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

        // Método para limpiar los TextBox
        private void clear()
        {
            HFRolesID.Value = "";
            TBName.Text = "";
            TBDescription.Text = "";
        }

        // Evento al hacer clic en el botón Guardar
        protected void BtnSave_Click(object sender, EventArgs e)
        {
            _nombre = TBName.Text;
            _descripcion = TBDescription.Text;

            executed = objRol.saveRoles(_nombre, _descripcion);

            if (executed)
            {
                LblMsg.Text = "Rol guardado exitosamente!";
            }
            else
            {
                LblMsg.Text = "Error al guardar el rol.";
            }
        }

        // Evento al hacer clic en el botón Actualizar
        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
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
                clear(); //Se invoca el metodo para limpiar los campos 
            }
            else
            {
                LblMsg.Text = "Error al actualizar el rol.";
            }
        }
    }
}


