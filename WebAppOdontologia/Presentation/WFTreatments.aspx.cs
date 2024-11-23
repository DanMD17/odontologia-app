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
    public partial class WFTreatments : System.Web.UI.Page
    {
        TreatmentsLog objTreatments = new TreatmentsLog();
        QuotesLog objQuotes = new QuotesLog();
        ClinicalHistoryLog objHistory = new ClinicalHistoryLog();
        AuxiliariesLog objAux = new AuxiliariesLog();

        private int _treatmentId;
        private string _name, _description, _observations;
        private DateTime _date;
        private int _fkCitaId, _fkHistId, _fkAuxId;
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
                // Los botones y otros elementos se inicializan en false, no visibles.
                BtnSave.Visible = false;
                BtnUpdate.Visible = false;
                FrmTreatments.Visible = false;
                PanelAdmin.Visible = false;
                TBDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                showQuotesDDL();
                showClinicalHistoriesDDL();
                showAssistantsDDL();
                //showTreatments();
            }
            validatePermissionRol();
        }
        

        // Método para listar los tratamientos
        [WebMethod]
        public static object ListTreatments()
        {
            TreatmentsLog objTreatments = new TreatmentsLog();

            // Se obtiene un DataSet que contiene la lista de tratamientos desde la base de datos.
            var dataSet = objTreatments.showTreatments();

            // Se crea una lista para almacenar los tratamientos que se van a devolver.
            var treatmentsList = new List<object>();

            // Se itera sobre cada fila del DataSet (que representa un tratamiento).
            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                treatmentsList.Add(new
                {
                    TreatmentID = row["trata_id"],
                    Name = row["trata_nombre"],
                    Description = row["trata_descripcion"],
                    Date = Convert.ToDateTime(row["trata_fecha"]).ToString("yyyy-MM-dd"),
                    Observations = row["trata_observaciones"],
                    FkCitaId = row["tbl_citas_cita_id"],
                    StatusQuote = row["cita_estado"],
                    FkHistId = row["tbl_historialclinico_hist_id"],
                    DescriptionHistory = row["hist_descripcion_general"],
                    FkAuxId = row["tbl_auxiliares_aux_id"],
                    FunctionAuxiliaries = row["aux_funcion"],
                });
            }

            // Devuelve un objeto en formato JSON que contiene la lista de tratamientos.
            return new { data = treatmentsList };
        }

        // Eliminar un tratamiento
        [WebMethod]
        public static bool DeleteTreatment(int id)
        {
            TreatmentsLog objTreatments = new TreatmentsLog();

            // Invocar al método para eliminar el tratamiento y devolver el resultado
            return objTreatments.deleteTreatment(id);
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
                            FrmTreatments.Visible = true;// Se pone visible el formulario
                            BtnSave.Visible = true;// Se pone visible el boton guardar
                            break;
                        case "ACTUALIZAR":
                            FrmTreatments.Visible = true;
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
                            FrmTreatments.Visible = true;
                            BtnSave.Visible = true;
                            PanelAdmin.Visible = true;
                            break;
                        case "ACTUALIZAR":
                            FrmTreatments.Visible = true;
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
                            FrmTreatments.Visible = true;
                            BtnSave.Visible = true;
                            PanelAdmin.Visible = true;
                            break;
                        case "ACTUALIZAR":
                            FrmTreatments.Visible = true;
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
                            FrmTreatments.Visible = true;
                            BtnSave.Visible = true;
                            PanelAdmin.Visible = true;
                            break;
                        case "ACTUALIZAR":
                            FrmTreatments.Visible = true;
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
        // Método para mostrar las citas en el DDL
        private void showQuotesDDL()
        {
            DDLQuotes.DataSource = objQuotes.showQuotesDDL();
            DDLQuotes.DataValueField = "cita_id"; // Nombre de la llave primaria de la tabla de citas
            DDLQuotes.DataTextField = "cita_estado";
            DDLQuotes.DataBind();
            DDLQuotes.Items.Insert(0, "Seleccione");
        }

        // Método para mostrar las historias clínicas en el DDL
        private void showClinicalHistoriesDDL()
        {
            DDLHistory.DataSource = objHistory.showClinicalHistoriesDDL();
            DDLHistory.DataValueField = "hist_id"; // Nombre de la llave primaria de la tabla de historia clínica
            DDLHistory.DataTextField = "hist_descripcion_general";
            DDLHistory.DataBind();
            DDLHistory.Items.Insert(0, "Seleccione");
        }

        // Método para mostrar los auxiliares en el DDL
        private void showAssistantsDDL()
        {
            DDLAux.DataSource = objAux.showAssistantsDDL();
            DDLAux.DataValueField = "aux_id"; // Nombre de la llave primaria de la tabla de auxiliares
            DDLAux.DataTextField = "aux_funcion";
            DDLAux.DataBind();
            DDLAux.Items.Insert(0, "Seleccione");
        }

        // Método para limpiar los TextBox y los DDL
        private void clear()
        {
            HFTreatmentID.Value = "";
            TBName.Text = "";
            TBDescription.Text = "";
            TBObservations.Text = "";
            TBDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            DDLQuotes.SelectedIndex = 0;
            DDLHistory.SelectedIndex = 0;
            DDLAux.SelectedIndex = 0;
        }

        // Evento que se ejecuta cuando se da clic en el botón guardar
        protected void BtnSave_Click(object sender, EventArgs e)
        {
            _name = TBName.Text;
            _description = TBDescription.Text;
            _observations = TBObservations.Text;
            _date = DateTime.Parse(TBDate.Text);
            _fkCitaId = Convert.ToInt32(DDLQuotes.SelectedValue);
            _fkHistId = Convert.ToInt32(DDLHistory.SelectedValue);
            _fkAuxId = Convert.ToInt32(DDLAux.SelectedValue);

            executed = objTreatments.saveTreatment(_name, _description, _date, _observations, _fkCitaId, _fkHistId, _fkAuxId);

            if (executed)
            {
                LblMsg.Text = "El tratamiento se guardó exitosamente!";
                clear();
            }
            else
            {
                LblMsg.Text = "Error al guardar :(";
            }
        }

        // Evento del botón actualizar
        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(HFTreatmentID.Value))
            {
                LblMsg.Text = "No se ha seleccionado un tratamiento para actualizar.";
                return;
            }

            _treatmentId = Convert.ToInt32(HFTreatmentID.Value);
            _name = TBName.Text;
            _description = TBDescription.Text;
            _observations = TBObservations.Text;
            _date = DateTime.Parse(TBDate.Text);
            _fkCitaId = Convert.ToInt32(DDLQuotes.SelectedValue);
            _fkHistId = Convert.ToInt32(DDLHistory.SelectedValue);
            _fkAuxId = Convert.ToInt32(DDLAux.SelectedValue);

            executed = objTreatments.updateTreatment(_treatmentId, _name, _description, _date, _observations, _fkCitaId, _fkHistId, _fkAuxId);

            if (executed)
            {
                LblMsg.Text = "El tratamiento se actualizó exitosamente!";
                clear();
            }
            else
            {
                LblMsg.Text = "Error al actualizar";
            }
        }

    }
}
