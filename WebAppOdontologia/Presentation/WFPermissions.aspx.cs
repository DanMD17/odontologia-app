using Logic;
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


        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {

            }
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

        private void clear()
        {
            HFPermissionID.Value = "";
            TBName.Text = "";
            TBDescription.Text = "";
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            _name = TBName.Text;
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
            _name = TBName.Text;
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