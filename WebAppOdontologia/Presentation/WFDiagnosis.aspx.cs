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
    public partial class WFDiagnosis : System.Web.UI.Page
    {
        DiagnosisLog objDiag = new DiagnosisLog();
        QuotesLog objQuo = new QuotesLog();
        ClinicalHistoryLog objCliniH = new ClinicalHistoryLog();

        private int _idDiag, _fkQuotes, _fkClinicalHistory;
        private string _description, _observations;
        private DateTime _date;
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
                FrmDiagnosis.Visible = false;
                PanelAdmin.Visible = false;
                //Aqui se invocan todos los metodos
                // showDiagnosis();
                TBDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                
                showClinicalHistoryDDL();
                showQuotesDDL();
            }
            validatePermissionRol();
        }

        [WebMethod]
        public static object ListDiagnosis()
        {
            DiagnosisLog objDiag = new DiagnosisLog();

            // Se obtiene un DataSet que contiene la lista de usuarios desde la base de datos.
            var dataSet = objDiag.showDiagnosis();

            // Se crea una lista para almacenar los usuarios que se van a devolver.
            var diagnosisList = new List<object>();

            // Se itera sobre cada fila del DataSet (que representa un usuario).
            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                diagnosisList.Add(new
                {
                    DiagID = row["diag_id"],
                    Description = row["diag_descripcion"],
                    Date = Convert.ToDateTime(row["diag_fecha"]).ToString("yyyy-MM-dd"),
                    Observations = row["diag_observaciones"],
                    FkQuotes = row["tbl_citas_cita_id"],
                    Status = row["cita_estado"],
                    FkClinicalHistory = row["tbl_historialclinico_hist_id"],
                    DescriptionCH = row["hist_descripcion_general"]
                });
            }

            // Devuelve un objeto en formato JSON que contiene la lista de diagnosis.
            return new { data = diagnosisList };
        }

        [WebMethod]
        public static bool DeleteDiagnosis(int id)
        {
            // Crear una instancia de la clase de lógica de diagnosticos
            DiagnosisLog objDiag = new DiagnosisLog();

            // Invocar al método para eliminar el diagnostico y devolver el resultado
            return objDiag.deleteDiagnosis(id);
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
                            FrmDiagnosis.Visible = true;// Se pone visible el formulario
                            BtnSave.Visible = true;// Se pone visible el boton guardar
                            break;
                        case "ACTUALIZAR":
                            FrmDiagnosis.Visible = true;
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
                            FrmDiagnosis.Visible = true;
                            BtnSave.Visible = true;
                            PanelAdmin.Visible = true;
                            break;
                        case "ACTUALIZAR":
                            FrmDiagnosis.Visible = true;
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
                            FrmDiagnosis.Visible = true;
                            BtnSave.Visible = true;
                            PanelAdmin.Visible = true;
                            break;
                        case "ACTUALIZAR":
                            FrmDiagnosis.Visible = true;
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
                            FrmDiagnosis.Visible = true;
                            BtnSave.Visible = true;
                            PanelAdmin.Visible = true;
                            break;
                        case "ACTUALIZAR":
                            FrmDiagnosis.Visible = true;
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


        //Metodo para mostrar las historias clinicas en el DDL
        private void showClinicalHistoryDDL()
        {
            DDLClinicalHistory.DataSource = objCliniH.showClinicalHistoriesDDL();
            DDLClinicalHistory.DataValueField = "hist_id";//Nombre de la llave primaria
            DDLClinicalHistory.DataTextField = "hist_descripcion_general";
            DDLClinicalHistory.DataBind();
            DDLClinicalHistory.Items.Insert(0, "Seleccione");
        }
        //Metodo para mostrar las citas en el DDL
        private void showQuotesDDL()
        {
            DDLQuotes.DataSource = objQuo.showQuotesDDL();
            DDLQuotes.DataValueField = "cita_id";//Nombre de la llave primaria
            DDLQuotes.DataTextField = "cita_estado";
            DDLQuotes.DataBind();
            DDLQuotes.Items.Insert(0, "Seleccione");
        }
        //Metodo para limpiar los TextBox y los DDL
        private void clear()
        {
            HFDiagnosisID.Value = "";
            TBDescription.Text = "";
            TBDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            TBObservations.Text = "";
            DDLClinicalHistory.SelectedIndex = 0;
            DDLQuotes.SelectedIndex = 0;
        }
        //Eventos que se ejecutan cuando se da clic en los botones
        protected void BtnSave_Click(object sender, EventArgs e)
        {
            _description = TBDescription.Text;
            _date = DateTime.Parse(TBDate.Text);
            _observations = TBObservations.Text;
            _fkClinicalHistory = Convert.ToInt32(DDLClinicalHistory.SelectedValue);
            _fkQuotes = Convert.ToInt32(DDLQuotes.SelectedValue);

            executed = objDiag.saveDiagnosis(_fkQuotes, _description, _date, _observations, _fkClinicalHistory);

            if (executed)
            {
                LblMsg.Text = "El Diagnostico se guardo exitosamente!";
            }
            else
            {
                LblMsg.Text = "Error al guardar";
            }
        }
        // Evento del boton actualizar
        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            // Verifica si se ha seleccionado un diagnostico para actualizar
            if (string.IsNullOrEmpty(HFDiagnosisID.Value))
            {
                LblMsg.Text = "No se ha seleccionado un diagnostico para actualizar.";
                return;
            }
            _idDiag = Convert.ToInt32(HFDiagnosisID.Value);
            _description = TBDescription.Text;
            _date = DateTime.Parse(TBDate.Text);
            _observations = TBObservations.Text;
            _fkClinicalHistory = Convert.ToInt32(DDLClinicalHistory.SelectedValue);
            _fkQuotes = Convert.ToInt32(DDLQuotes.SelectedValue);

            executed = objDiag.updateDiagnosis(_idDiag, _fkQuotes, _description, _date, _observations, _fkClinicalHistory);

            if (executed)
            {
                LblMsg.Text = "El diagnostico se actualizo exitosamente!";
                clear(); //Se invoca el metodo para limpiar los campos 
            }
            else
            {
                LblMsg.Text = "Error al actualizar";
            }
            
        }
    }
}


