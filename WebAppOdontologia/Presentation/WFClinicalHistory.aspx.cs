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
    public partial class WFClinicalHistory : System.Web.UI.Page
    {
        ClinicalHistoryLog objCliH = new ClinicalHistoryLog();
        PatientsLog objPati = new PatientsLog();

        private int _id, _fkPatient;
        private DateTime _pCreacionDate;
        private string _pOverview;
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
                FrmClinicalHistory.Visible = false;
                PanelAdmin.Visible = false;
                //Aqui se invocan todos los metodos 
                //showClinicalHistories();
                //Se asigna la fecha actual al TextBox en formato "yyyy-MM-dd".
                TBCreacionDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                showPatientsDDL();
            }
            validatePermissionRol();
        }

        //Metodo para mostrar todos las historias clinicas

        [WebMethod]
        public static object ListClinicalHistory()
        {
            ClinicalHistoryLog objCliH = new ClinicalHistoryLog();

            // Se obtiene un DataSet que contiene la lista de las historias clinicas desde la base de datos.
            var dataSet = objCliH.showClinicalHistories();

            // Se crea una lista para almacenar las historias clinicas que se van a devolver.
            var clinicalHistoryList = new List<object>();

            // Se itera sobre cada fila del DataSet (que representa una historia clinica).
            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                clinicalHistoryList.Add(new
                {
                    ClinicalHistoryID = row["hist_id"],
                    CreacionDate = Convert.ToDateTime(row["hist_fecha_creacion"]).ToString("yyyy-MM-dd"), // Formato de fecha específico.
                    Overview = row["hist_descripcion_general"],
                    FkPatient = row["tbl_pacientes_paci_id"],
                    NamePatient = row["paci_nombre"]

                });
            }

            // Devuelve un objeto en formato JSON que contiene la lista de historiales clinicos.
            return new { data = clinicalHistoryList };
        }

        [WebMethod]
        public static bool DeleteClinicalHistory(int id)
        {
            // Crear una instancia de la clase de lógica de Historiales clinicos
            ClinicalHistoryLog objCliH = new ClinicalHistoryLog();

            // Invocar al método para eliminar el historial clinico y devolver el resultado
            return objCliH.deleteClinicalHistory(id);
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
                            FrmClinicalHistory.Visible = true;// Se pone visible el formulario
                            BtnSave.Visible = true;// Se pone visible el boton guardar
                            break;
                        case "ACTUALIZAR":
                            FrmClinicalHistory.Visible = true;
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
                            FrmClinicalHistory.Visible = true;
                            BtnSave.Visible = true;
                            PanelAdmin.Visible = true;
                            break;
                        case "ACTUALIZAR":
                            FrmClinicalHistory.Visible = true;
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
                            FrmClinicalHistory.Visible = true;
                            BtnSave.Visible = true;
                            PanelAdmin.Visible = true;
                            break;
                        case "ACTUALIZAR":
                            FrmClinicalHistory.Visible = true;
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
                            FrmClinicalHistory.Visible = true;
                            BtnSave.Visible = true;
                            PanelAdmin.Visible = true;
                            break;
                        case "ACTUALIZAR":
                            FrmClinicalHistory.Visible = true;
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

        //Metodo para mostrar los pacientes en el DDL
        private void showPatientsDDL()
        {
            DDLPatient.DataSource = objPati.showPatientsDDL();
            DDLPatient.DataValueField = "paci_id";//Nombre de la llave primaria
            DDLPatient.DataTextField = "paci_nombre";
            DDLPatient.DataBind();
            DDLPatient.Items.Insert(0, "Seleccione");
        }
        //Metodo para limpiar los TextBox y los DDL
        private void clear()
        {
            HFClinicalHistoryID.Value = "";
            TBCreacionDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            TBOverview.Text = "";
            DDLPatient.SelectedIndex = 0;

        }

        //Boton de guardar un historial clinico
        protected void BtnSave_Click(object sender, EventArgs e)
        {
            _pCreacionDate = DateTime.Parse(TBCreacionDate.Text);
            _pOverview = TBOverview.Text;
            _fkPatient = Convert.ToInt32(DDLPatient.SelectedValue);

            executed = objCliH.saveClinicalHistory(_fkPatient, _pCreacionDate, _pOverview);

            if (executed)
            {
                LblMsg.Text = "El Historial Clinico se guardo exitosamente!";

            }
            else
            {
                LblMsg.Text = "Error al guardar";
            }
        }

        // Evento del boton actualizar
        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            // Verifica si se ha seleccionado un historial clinico para actualizar
            if (string.IsNullOrEmpty(HFClinicalHistoryID.Value))
            {
                LblMsg.Text = "No se ha seleccionado una historia clinica para actualizar.";
                return;
            }
            _id = Convert.ToInt32(HFClinicalHistoryID.Value);
            _pCreacionDate = DateTime.Parse(TBCreacionDate.Text);
            _pOverview = TBOverview.Text;
            _fkPatient = Convert.ToInt32(DDLPatient.SelectedValue);


            executed = objCliH.updateClinicalHistory(_id, _pCreacionDate, _pOverview, _fkPatient);

            if (executed)
            {
                LblMsg.Text = "La Historia Clinica se actualizo exitosamente!";
                clear(); //Se invoca el metodo para limpiar los campos 
            }
            else
            {
                LblMsg.Text = "Error al actualizar";
            }
        }

    }
}

