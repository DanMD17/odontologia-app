using Logic;
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
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Aqui se invocan todos los metodos
                //showSecretaries();
                showEmployeesDDL();
            }
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
                    FkEmployee = row["tbl_empleado_emp_id"],
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
        }

        //Evento que se ejecuta cuando se da clic en el boton guardar
        protected void BtnSave_Click(object sender, EventArgs e)
        {
            _yearsExp = TBYearsExp.Text;
            _function = TBFunction.Text;
            _fkEmployee = Convert.ToInt32(DDLEmployee.SelectedValue);

            executed = objSec.saveSecretary(_fkEmployee, _function, _yearsExp);

            if (executed)
            {
                LblMsg.Text = "La secretaria se guardó exitosamente!";
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
            _fkEmployee = Convert.ToInt32(DDLEmployee.SelectedValue);

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