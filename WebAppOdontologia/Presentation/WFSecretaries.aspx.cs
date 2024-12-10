using Logic;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentation
{
    public partial class WFSecretaries : System.Web.UI.Page
    {
        //Crear los objetos
        SecretariesLog objSec = new SecretariesLog();
        EmployeesLog objEmp = new EmployeesLog();

        private int _idSec, _fkEmployee;
        private string _function, _yearsExp;
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
                FrmSecretaries.Visible = false;
                PanelAdmin.Visible = false;
                //Aqui se invocan todos los metodos
                //showSecretaries();
                showEmployeesDDL();
            }
            validatePermissionRol();
        }

        //Metodo para listar las secretarias
        [WebMethod]
        public static object ListSecretaries()
        {
            SecretariesLog objSec = new SecretariesLog();

            // Se obtiene un DataSet que contiene la lista de secretarias desde la base de datos.
            var dataSet = objSec.showSecretaries();

            // Se crea una lista para almacenar las secretarias que se van a devolver.
            var secretariesList = new List<object>();

            // Se itera sobre cada fila del DataSet (que representa una secretaria).
            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                secretariesList.Add(new
                {
                    SecretariatID = row["id_sec"],
                    Function = row["sec_funcion"],
                    YearsExp = row["sec_anios_experiencia"],
                    FkEmployee = row["tbl_empleados_emp_id"],
                    NameEmployee = row["emp_nombre"]
                });
            }

            // Devuelve un objeto en formato JSON que contiene la lista de secretarias.
            return new { data = secretariesList };
        }

        //Eliminar una secretaria
        [WebMethod]
        public static bool DeleteSecretariat(int id)
        {
            // Crear una instancia de la clase de lógica de secretarias
            SecretariesLog objSec = new SecretariesLog();

            // Invocar al método para eliminar la secretaria y devolver el resultado
            return objSec.deleteSecretaria(id);
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
                            FrmSecretaries.Visible = true;// Se pone visible el formulario
                            BtnSave.Visible = true;// Se pone visible el boton guardar
                            break;
                        case "ACTUALIZAR":
                            FrmSecretaries.Visible = true;
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
                            FrmSecretaries.Visible = true;
                            BtnSave.Visible = true;
                            PanelAdmin.Visible = true;
                            break;
                        case "ACTUALIZAR":
                            FrmSecretaries.Visible = true;
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
                            FrmSecretaries.Visible = true;
                            BtnSave.Visible = true;
                            PanelAdmin.Visible = true;
                            break;
                        case "ACTUALIZAR":
                            FrmSecretaries.Visible = true;
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
                            FrmSecretaries.Visible = true;
                            BtnSave.Visible = true;
                            PanelAdmin.Visible = true;
                            break;
                        case "ACTUALIZAR":
                            FrmSecretaries.Visible = true;
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

        //Metodo para mostrar los empleados en el DDL
        private void showEmployeesDDL()
        {
            DDLEmployee.DataSource = objEmp.showEmployeesDDL();
            DDLEmployee.DataValueField = "emp_id"; //Nombre de la llave primaria
            DDLEmployee.DataTextField = "emp_nombre";
            DDLEmployee.DataBind();
            DDLEmployee.Items.Insert(0, "Seleccione");
        }

        //Metodo para limpiar los TextBox y los DDL
        private void clear()
        {
            HFSecretariesID.Value = "";
            TBFunction.Text = "";
            TBYearsExp.Text = "";
            DDLEmployee.SelectedIndex = 0;
            LblMsgEmp.Text = "";
        }

        //Evento que se ejecuta cuando se da clic en el boton guardar
        protected void BtnSave_Click(object sender, EventArgs e)
        {
            _yearsExp = TBYearsExp.Text;
            _function = TBFunction.Text;
            //_fkEmployee = Convert.ToInt32(DDLEmployee.SelectedValue);

            if (!int.TryParse(DDLEmployee.SelectedValue, out _fkEmployee) || _fkEmployee == 0)
            {
                LblMsgEmp.Text = "Este campo es obligatorio";
                return;

            }

            executed = objSec.saveSecretary(_fkEmployee, _function, _yearsExp);

            if (executed)
            {
                LblMsg.Text = "La secretaria se guardó exitosamente!";
                clear();
            }
            else
            {
                LblMsg.Text = "Error al guardar :(";
            }
        }

        // Evento del boton actualizar
        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            // Verifica si se ha seleccionado una secretaria para actualizar
            if (string.IsNullOrEmpty(HFSecretariesID.Value))
            {
                LblMsg.Text = "No se ha seleccionado una secretaria para actualizar.";
                return;
            }
            _idSec = Convert.ToInt32(HFSecretariesID.Value);
            _function = TBFunction.Text;
            _yearsExp = TBYearsExp.Text;
            //_fkEmployee = Convert.ToInt32(DDLEmployee.SelectedValue);
            if (!int.TryParse(DDLEmployee.SelectedValue, out _fkEmployee) || _fkEmployee == 0)
            {
                LblMsgEmp.Text = "Este campo es obligatorio";
                return;

            }

            executed = objSec.updateSecretaria(_idSec, _fkEmployee, _function, _yearsExp);

            if (executed)
            {
                LblMsg.Text = "La secretaria se actualizo exitosamente!";
                clear(); //Se invoca el metodo para limpiar los campos 
            }
            else
            {
                LblMsg.Text = "Error al actualizar";
            }
        }
    }
}