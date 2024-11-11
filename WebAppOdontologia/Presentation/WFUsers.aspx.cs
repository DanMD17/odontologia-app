using Logic;
using SimpleCrypto;
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
    public partial class WFUsers : System.Web.UI.Page
    {
        //Crear los objetos
        RolLog objRol = new RolLog();
        EmployeesLog objEmp = new EmployeesLog();
        UsersLog objUse = new UsersLog();

        private int _id, _fkrol, _fkemployee;
        private string _mail, _password, _salt, _state, _encryptedPassword;
        private DateTime _date;
        private bool executed = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Se asigna la fecha actual al TextBox en formato "yyyy-MM-dd".
                TBDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                //Aqui se invocan todos los metodos
                showRolesDDL();
                showEmployeesDDL();
            }

        }
        // Metodo web para mostrar todos los usuarios
        [WebMethod]
        public static object ListUsers()
        {
            UsersLog objUse = new UsersLog();

            // Se obtiene un DataSet que contiene la lista de usuarios desde la base de datos.
            var dataSet = objUse.showUsers();

            // Se crea una lista para almacenar los usuarios que se van a devolver.
            var usersList = new List<object>();

            // Se itera sobre cada fila del DataSet (que representa un usuario).
            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                usersList.Add(new
                {
                    UserID = row["usu_id"],
                    Mail = row["usu_correo"],
                    Password = row["usu_contrasena"],
                    Salt = row["usu_salt"],
                    State = row["usu_estado"],
                    Date = Convert.ToDateTime(row["usu_fecha_creacion"]).ToString("yyyy-MM-dd"), // Formato de fecha específico.
                    FkRol = row["tbl_rol_rol_id"],
                    NameRol = row["rol_nombre"],
                    FkEmployee = row["tbl_empleado_emp_id"],
                    NameEmployee = row["emp_nombres"]
                });
            }

            // Devuelve un objeto en formato JSON que contiene la lista de usuarios.
            return new { data = usersList };
        }

        //Metodo para mostrar los roles en el DDL
        private void showRolesDDL()
        {
            DDLRol.DataSource = objRol.showRolesDDL();
            DDLRol.DataValueField = "rol_id";//Nombre de la llave primaria
            DDLRol.DataTextField = "rol_nombre";
            DDLRol.DataBind();
            DDLRol.Items.Insert(0, "Seleccione");
        }
        //Metodo para mostrar los empleados en el DDL
        private void showEmployeesDDL()
        {
            DDLEmployees.DataSource = objEmp.showEmployeesDDL();
            DDLEmployees.DataValueField = "emp_id";//Nombre de la llave primaria
            DDLEmployees.DataTextField = "nombre";
            DDLEmployees.DataBind();
            DDLEmployees.Items.Insert(0, "Seleccione");
        }
        //Metodo para limpiar los TextBox y los DDL
        private void clear()
        {
            HFUserId.Value = "";
            TBMail.Text = "";
            TBContrasena.Text = "";
            DDLState.SelectedIndex = 0;
            TBDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            DDLRol.SelectedIndex = 0;
            DDLEmployees.SelectedIndex = 0;
        }
        // Evento que se ejecuta cuando se da clic en el boton guardar
        protected void BtnSave_Click(object sender, EventArgs e)
        {
            /*
             * PBKDF2: Password-Based Key Derivation Function 2, es un algoritmo para proteger contraseñas,
             * ya que es seguro contra ataques de fuerza bruta, genera un hash mediante múltiples iteraciones
             */
            ICryptoService cryptoService = new PBKDF2();

            _mail = TBMail.Text;
            _password = TBContrasena.Text;
            _salt = cryptoService.GenerateSalt();// Se generar un salt único para esa contraseña.
            _encryptedPassword = cryptoService.Compute(_password);// Se generar un hash de la contraseña.

            _state = DDLState.SelectedValue;
            _date = DateTime.Parse(TBDate.Text);
            _fkrol = Convert.ToInt32(DDLRol.SelectedValue);
            _fkemployee = Convert.ToInt32(DDLEmployees.SelectedValue);

            executed = objUse.saveUsers(_mail, _encryptedPassword, _salt, _state, _date, _fkrol, _fkemployee);

            if (executed)
            {
                LblMsg.Text = "El usuario se guardo exitosamente!";
                clear();//Se invoca el metodo para limpiar los campos 
            }
            else
            {
                LblMsg.Text = "Error al guardar";
            }

        }
        // Evento que se ejecuta cuando se da clic en el boton actualizar 
        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            // Verifica si se ha seleccionado un usuario para actualizar
            if (string.IsNullOrEmpty(HFUserId.Value))
            {
                LblMsg.Text = "No se ha seleccionado un usuario para actualizar.";
                return;
            }

            ICryptoService cryptoService = new PBKDF2();

            _id = Convert.ToInt32(HFUserId.Value);
            _mail = TBMail.Text;
            _password = TBContrasena.Text;
            _salt = cryptoService.GenerateSalt();// Se generar un salt único para esa contraseña.
            _encryptedPassword = cryptoService.Compute(_password);// Se generar un hash de la contraseña.

            _state = DDLState.SelectedValue;
            _date = DateTime.Parse(TBDate.Text);
            _fkrol = Convert.ToInt32(DDLRol.SelectedValue);
            _fkemployee = Convert.ToInt32(DDLEmployees.SelectedValue);

            executed = objUse.updateUsers(_id, _mail, _encryptedPassword, _salt, _state, _date, _fkrol, _fkemployee);

            if (executed)
            {
                LblMsg.Text = "El usuario se actualizo exitosamente!";
                clear();//Se invoca el metodo para limpiar los campos 
            }
            else
            {
                LblMsg.Text = "Error al actualizar";
            }
        }
    }
}