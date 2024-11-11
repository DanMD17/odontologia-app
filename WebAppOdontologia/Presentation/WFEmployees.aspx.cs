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

namespace Presentation
{
    public partial class WFEmployees : System.Web.UI.Page
    {
        //Crear los objetos
        EmployeesLog objEmp = new EmployeesLog();

        private int _idEmp;
        private string _identification, _name, _lastName, _cellPhone, _address, _email;
        private bool executed;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Aqui se invocan todos los metodos
                //showEmployees();
            }
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

            // Se itera sobre cada fila del DataSet (que representa un empleado)
            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                employeesList.Add(new
                {
                    EmployeeID = row["emp_id"],
                    Identification = row["emp_identificacion"],
                    Name = row["emp_nombre"],
                    LastName = row["emp_apellidos"],
                    CellPhone = row["emp_celular"],
                    Address = row["emp_direccion"],
                    Email = row["emp_correo"]
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

        //Metodo para limpiar los TextBox y los DDL
        private void clear()
        {
            HFEmployeeID.Value = "";
            TBIdentification.Text = "";
            TBName.Text = "";
            TBLastName.Text = "";
            TBCellPhone.Text = "";
            TBAddress.Text = "";
            TBEmail.Text = "";
        }

        //Evento del boton de guardar
        protected void BtnSave_Click(object sender, EventArgs e)
        {
            _identification = TBIdentification.Text;
            _name = TBName.Text;
            _lastName = TBLastName.Text;
            _cellPhone = TBCellPhone.Text;
            _address = TBAddress.Text;
            _email = TBEmail.Text;

            executed = objEmp.saveEmployee(_identification, _name, _lastName, _cellPhone, _address, _email);

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

            executed = objEmp.updateEmployee(_idEmp, _identification, _name, _lastName, _cellPhone,_address, _email);

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