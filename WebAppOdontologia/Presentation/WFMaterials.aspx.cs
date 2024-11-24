using Logic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Security.Cryptography;
using System.Linq;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Model;

namespace Presentation
{
    public partial class WFMaterials : System.Web.UI.Page
    {
        // Se crean los objetos
        MaterialsLog objMat = new MaterialsLog();
        TreatmentsLog objTreat = new TreatmentsLog();

        private int _IdMat, _fkTrataId, _materialQuantity;
        private string _materialName, _materialDescription;
        private bool executed;

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
                BtnSave.Visible = false;
                BtnUpdate.Visible = false;
                FrmMaterials.Visible = false;
                PanelAdmin.Visible = false;
                // Aquí se invocan todos los métodos
                //showMaterials();
                showTreatmentsDDL();
            }
            validatePermissionRol();
        }

        // Método para listar los materiales a través de un WebMethod
        [WebMethod]
        public static object ListMaterials()
        {
            MaterialsLog objMat = new MaterialsLog();

            // Obtener un DataSet con la lista de materiales
            var dataSet = objMat.showMaterials();

            // Crear una lista para almacenar los materiales
            var materialsList = new List<object>();

            // Iterar sobre cada fila del DataSet
            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                materialsList.Add(new
                {
                    MaterialID = row["mate_id"],
                    Nombre = row["mate_nombre"],
                    Descripcion = row["mate_descripcion"],
                    Cantidad = row["mate_cantidad"],
                    FkTratamiento = row["tbl_tratamientos_realizados_trata_id"],
                    Tratamiento = row["trata_nombre"]
                });
            }

            // Devolver el listado en formato JSON
            return new { data = materialsList };
        }

        // Método para mostrar los tratamientos en el DDL
        private void showTreatmentsDDL()
        {
            DDLTreatments.DataSource = objTreat.showTreatmentsDDL();
            DDLTreatments.DataValueField = "trata_id"; // Nombre de la llave primaria
            DDLTreatments.DataTextField = "trata_nombre";
            DDLTreatments.DataBind();
            DDLTreatments.Items.Insert(0, "Seleccione");
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
                            FrmMaterials.Visible = true;// Se pone visible el formulario
                            BtnSave.Visible = true;// Se pone visible el boton guardar
                            break;
                        case "ACTUALIZAR":
                            FrmMaterials.Visible = true;
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
                            FrmMaterials.Visible = true;
                            BtnSave.Visible = true;
                            PanelAdmin.Visible = true;
                            break;
                        case "ACTUALIZAR":
                            FrmMaterials.Visible = true;
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
                            FrmMaterials.Visible = true;
                            BtnSave.Visible = true;
                            PanelAdmin.Visible = true;
                            break;
                        case "ACTUALIZAR":
                            FrmMaterials.Visible = true;
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
                            FrmMaterials.Visible = true;
                            BtnSave.Visible = true;
                            PanelAdmin.Visible = true;
                            break;
                        case "ACTUALIZAR":
                            FrmMaterials.Visible = true;
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
            else
            {
                // Si el rol no es reconocido, se deniega el acceso
                LblMsg.Text = "Rol no reconocido. No tienes permisos suficientes para acceder a esta página.";
                Response.Redirect("Index.aspx");
            }
        }
        // Método para guardar un material nuevo
        protected void BtnSave_Click(object sender, EventArgs e)
        {
            _materialDescription = TBmaterialDescription.Text;
            _materialName = TBmaterialName.Text;
            _materialQuantity = Convert.ToInt32(TBmaterialQuantity.Text);
            _fkTrataId = Convert.ToInt32(DDLTreatments.SelectedValue);

            executed = objMat.saveMaterial(_materialName, _materialDescription, _materialQuantity, _fkTrataId);

            if (executed)
            {
                LblMsg.Text = "El material se guardó exitosamente!";
                clear(); // Se invoca el metodo para limpiar los campos
            }
            else
            {
                LblMsg.Text = "Error al guardar";
            }
        }

        // Método para actualizar un material existente
        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            // Verifica si se ha seleccionado un material para actualizar
            if (string.IsNullOrEmpty(HFMaterialsID.Value))
            {
                LblMsg.Text = "No se ha seleccionado un material para actualizar.";
                return;
            }
            _IdMat = Convert.ToInt32(HFMaterialsID.Value);
            _materialDescription = TBmaterialDescription.Text;
            _materialName = TBmaterialName.Text;
            _materialQuantity = Convert.ToInt32(TBmaterialQuantity.Text);
            _fkTrataId = Convert.ToInt32(DDLTreatments.SelectedValue);

            executed = objMat.updateMaterial(_IdMat, _materialName, _materialDescription, _materialQuantity, _fkTrataId);

            if (executed)
            {
                LblMsg.Text = "El material se actualizó exitosamente!";
                clear(); // Se invoca el metodo para limpiar los campos
            }
            else
            {
                LblMsg.Text = "Error al actualizar";
            }
        }

        // Método para eliminar un material a través de un WebMethod
        [WebMethod]
        public static bool DeleteMaterial(int id)
        {
            // Crear una instancia de la clase de lógica de materiales
            MaterialsLog objMat = new MaterialsLog();

            // Invocar al método para eliminar el material y devolver el resultado
            return objMat.deleteMaterial(id);
        }


        // Método para limpiar los TextBox y el DDL
        private void clear()
        {
            HFMaterialsID.Value = "";
            TBmaterialName.Text = "";
            TBmaterialDescription.Text = "";
            TBmaterialQuantity.Text = "";
            DDLTreatments.SelectedIndex = 0;
        }
    }
}
