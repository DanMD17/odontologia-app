using System;
using Logic;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Model;

namespace Presentation
{
    public partial class WFPatients : System.Web.UI.Page
    {
        PatientsLog objPat = new PatientsLog();

        private int _patientId;
        private string _name, _lastName, _address, _cellPhone, _email;
        private DateTime _dateOfBirth;
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
                FrmPatients.Visible = false;
                PanelAdmin.Visible = false;
                //showPatients();
            }
            validatePermissionRol();
        }
        // Método para listar los pacientes
        [WebMethod]
        public static object ListPatients()
        {
            PatientsLog objPat = new PatientsLog();

            // Se obtiene un DataSet que contiene la lista de pacientes desde la base de datos.
            var dataSet = objPat.showPatients();

            // Se crea una lista para almacenar los pacientes que se van a devolver.
            var patientsList = new List<object>();

            // Se itera sobre cada fila del DataSet (que representa un paciente).
            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                patientsList.Add(new
                {
                    PatientID = row["paci_id"],
                    Name = row["paci_nombre"],
                    LastName = row["paci_apellido"],
                    Address = row["paci_direccion"],
                    Phone = row["paci_celular"],
                    Email = row["paci_correo"],
                    DateOfBirth = Convert.ToDateTime(row["paci_fecha_nacimiento"]).ToString("yyyy-MM-dd")
                });
            }

            // Devuelve un objeto en formato JSON que contiene la lista de pacientes.
            return new { data = patientsList };
        }

        // Eliminar un paciente
        [WebMethod]
        public static bool DeletePatient(int id)
        {
            PatientsLog objPat = new PatientsLog();

            // Invocar al método para eliminar el paciente y devolver el resultado
            return objPat.deletePatient(id);
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
                            FrmPatients.Visible = true;// Se pone visible el formulario
                            BtnSave.Visible = true;// Se pone visible el boton guardar
                            break;
                        case "ACTUALIZAR":
                            FrmPatients.Visible = true;
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
                            FrmPatients.Visible = true;
                            BtnSave.Visible = true;
                            PanelAdmin.Visible = true;
                            break;
                        case "ACTUALIZAR":
                            FrmPatients.Visible = true;
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
                            FrmPatients.Visible = true;
                            BtnSave.Visible = true;
                            PanelAdmin.Visible = true;
                            break;
                        case "ACTUALIZAR":
                            FrmPatients.Visible = true;
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
                            FrmPatients.Visible = true;
                            BtnSave.Visible = true;
                            PanelAdmin.Visible = true;
                            break;
                        case "ACTUALIZAR":
                            FrmPatients.Visible = true;
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
        // Método para limpiar los TextBox y los DDL
        private void clear()
        {
            HFPatientsID.Value = "";
            TBName.Text = "";
            TBLastName.Text = "";
            TBAddress.Text = "";
            TBPhone.Text = "";
            TBEmail.Text = "";
            TBDateOfBirth.Text = DateTime.Now.ToString("yyyy-MM-dd");
        }

        // Evento que se ejecuta cuando se da clic en el botón guardar
        protected void BtnSave_Click(object sender, EventArgs e)
        {
            _name = TBName.Text;
            _lastName = TBLastName.Text;
            _address = TBAddress.Text;
            _cellPhone = TBPhone.Text;
            _email = TBEmail.Text;
            _dateOfBirth = DateTime.Parse(TBDateOfBirth.Text);

            executed = objPat.savePatient(_name, _lastName, _dateOfBirth, _address, _cellPhone, _email);

            if (executed)
            {
                LblMsg.Text = "El paciente se guardó exitosamente!";
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
            if (string.IsNullOrEmpty(HFPatientsID.Value))
            {
                LblMsg.Text = "No se ha seleccionado un paciente para actualizar.";
                return;
            }

            _patientId = Convert.ToInt32(HFPatientsID.Value);
            _name = TBName.Text;
            _lastName = TBLastName.Text;
            _address = TBAddress.Text;
            _cellPhone = TBPhone.Text;
            _email = TBEmail.Text;
            _dateOfBirth = DateTime.Parse(TBDateOfBirth.Text);

            executed = objPat.updatePatient(_patientId, _name, _lastName, _dateOfBirth, _address, _cellPhone, _email);

            if (executed)
            {
                LblMsg.Text = "El paciente se actualizó exitosamente!";
                clear();
            }
            else
            {
                LblMsg.Text = "Error al actualizar";
            }
        }
    }
}
