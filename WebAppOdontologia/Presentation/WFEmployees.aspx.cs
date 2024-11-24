using System;
using Logic;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Services;
using System.CodeDom.Compiler;
using System.Net;
using System.Security.Cryptography;
using Model;

namespace Presentation
{
    public partial class WFEmployees : System.Web.UI.Page
    {
        //Crear los objetos
        EmployeesLog objEmp = new EmployeesLog();

        private int _idEmp;
        private string _identification, _name, _lastName, _cellPhone, _address, _email;
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
                FrmEmployees.Visible = false;
                PanelAdmin.Visible = false;

                //Aqui se invocan todos los metodos 
                //showEmployees();
            }
            validatePermissionRol();
        }

        //Metodo para listar empleados
        [WebMethod]
        public static object ListEmployees()
        {
            EmployeesLog objEmp = new EmployeesLog();

            // Se obtiene un DataSet que contiene la lista de empleados desde la base de datos.
            var dataSet = objEmp.showEmployees();

            // Se crea una lista para almacenar los empleados que se van a devolver.
            var employeesList = new List<object>();

            // Se itera sobre cada fila del DataSet (que representa un empleado).
            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                employeesList.Add(new
                {
                    EmployeeID = row["emp_id"],
                    Identification = row["emp_identificacion"],
                    Name = row["emp_nombre"],
                    LastName = row["emp_apellidos"],
                    CellPhone = row["emp_celular"],
                    Email = row["emp_correo"],
                    Address = row["emp_direccion"]
                });
            }

            // Devuelve un objeto en formato JSON que contiene la lista de empleados.
            return new { data = employeesList };
        }

        //Metodo para eliminar
        [WebMethod]
        public static bool DeleteEmployee(int id)
        {
            // Crear una instancia de la clase de lógica de empleados
            EmployeesLog objEmp = new EmployeesLog();

            // Invocar al método para eliminar el empleado y devolver el resultado
            return objEmp.deleteEmployee(id);
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
                            FrmEmployees.Visible = true;// Se pone visible el formulario
                            BtnSave.Visible = true;// Se pone visible el boton guardar
                            break;
                        case "ACTUALIZAR":
                            FrmEmployees.Visible = true;
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
                            FrmEmployees.Visible = true;
                            BtnSave.Visible = true;
                            PanelAdmin.Visible = true;
                            break;
                        case "ACTUALIZAR":
                            FrmEmployees.Visible = true;
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
                            FrmEmployees.Visible = true;
                            BtnSave.Visible = true;
                            PanelAdmin.Visible = true;
                            break;
                        case "ACTUALIZAR":
                            FrmEmployees.Visible = true;
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
                            FrmEmployees.Visible = true;
                            BtnSave.Visible = true;
                            PanelAdmin.Visible = true;
                            break;
                        case "ACTUALIZAR":
                            FrmEmployees.Visible = true;
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

        //Metodo para limpiar los TextBox y los DDL
        private void clear()
        {
            HFEmployeeID.Value = "";
            TBIdentification.Text = "";
            TBName.Text = "";
            TBLastName.Text = "";
            TBCellPhone.Text = "";
            TBEmail.Text = "";
            TBAddress.Text = "";
        }

        //Evento del boton de guardar
        protected void BtnSave_Click(object sender, EventArgs e)
        {
            _identification = TBIdentification.Text;
            _name = TBName.Text;
            _lastName = TBLastName.Text;
            _cellPhone = TBCellPhone.Text;
            _email = TBEmail.Text;
            _address = TBAddress.Text;

            executed = objEmp.saveEmployee(_identification, _name, _lastName, _cellPhone, _email, _address);

            if (executed)
            {
                LblMsg.Text = "El empleado se guardó exitosamente!";
            }
            else
            {
                LblMsg.Text = "Error al guardar :(";
            }
        }

        // Evento del boton actualizar
        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            // Verifica si se ha seleccionado un empleado para actualizar
            if (string.IsNullOrEmpty(HFEmployeeID.Value))
            {
                LblMsg.Text = "No se ha seleccionado un empleado para actualizar.";
                return;
            }
            _idEmp = Convert.ToInt32(HFEmployeeID.Value);
            _identification = TBIdentification.Text;
            _name = TBName.Text;
            _lastName = TBLastName.Text;
            _cellPhone = TBCellPhone.Text;
            _address = TBAddress.Text;
            _email = TBEmail.Text;

            executed = objEmp.updateEmployee(_idEmp, _identification, _name, _lastName, _cellPhone, _email, _address);

            if (executed)
            {
                LblMsg.Text = "El empleado se actualizo exitosamente!";
                clear(); //Se invoca el metodo para limpiar los campos 
            }
            else
            {
                LblMsg.Text = "Error al actualizar";
            }
        }
    }
}