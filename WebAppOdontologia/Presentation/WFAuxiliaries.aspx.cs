using System;
using Logic;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Runtime.Remoting;
using System.Web.Services;
using System.Security.Cryptography;

namespace Presentation
{
    public partial class WFAuxiliaries : System.Web.UI.Page
    {
        AuxiliariesLog objAux = new AuxiliariesLog();
        EmployeesLog objEmp = new EmployeesLog();

        private int _idAux, _fkEmployee;
        private string _function, _educationalLevel;
        private bool executed;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Aqui se invocan todos los metodos
                //showAssistants();
                showEmployeesDDL();
            }
        }

        //Metodo para mostrar todos los auxiliares
        [WebMethod]
        public static object ListAssistants()
        {
            AuxiliariesLog objAux = new AuxiliariesLog();

            // Se obtiene un DataSet que contiene la lista de auxiliares desde la base de datos.
            var dataSet = objAux.showAssistants();

            // Se crea una lista para almacenar los auxiliares que se van a devolver.
            var assistantsList = new List<object>();

            // Se itera sobre cada fila del DataSet (que representa un auxiliar).
            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                assistantsList.Add(new
                {
                    AssistantID = row["aux_id"],
                    Function = row["aux_funcion"],
                    EducationalLevel = row["aux_nivel_educativo"],
                    FkEmployee = row["tbl_empleado_emp_id"],
                    NameEmployee = row["emp_nombre"]
                });
            }

            // Devuelve un objeto en formato JSON que contiene la lista de auxiliares.
            return new { data = assistantsList };
        }

        //Metodo para eliminar
        [WebMethod]
        public static bool DeleteAssistant(int id)
        {
            // Crear una instancia de la clase de lógica de auxiliares
            AuxiliariesLog objAux = new AuxiliariesLog();

            // Invocar al método para eliminar el auxiliar y devolver el resultado
            return objAux.deleteAssistant(id);
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
            HFAssistantID.Value = "";
            TBFunction.Text = "";
            TBEducationalLevel.Text = "";
            DDLEmployee.SelectedIndex = 0;
        }

        //Boton de guardar un auxiliar
        protected void BtnSave_Click(object sender, EventArgs e)
        {
            _educationalLevel = TBEducationalLevel.Text;
            _function = TBFunction.Text;
            _fkEmployee = Convert.ToInt32(DDLEmployee.SelectedValue);

            executed = objAux.saveAssistant(_fkEmployee, _function, _educationalLevel);

            if (executed)
            {
                LblMsg.Text = "El auxiliar se guardó exitosamente!";
            }
            else
            {
                LblMsg.Text = "Error al guardar :(";
            }
        }

        // Evento del boton actualizar
        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            // Verifica si se ha seleccionado un auxiliar para actualizar
            if (string.IsNullOrEmpty(HFAssistantID.Value))
            {
                LblMsg.Text = "No se ha seleccionado un auxiliar para actualizar.";
                return;
            }
            _idAux = Convert.ToInt32(HFAssistantID.Value);
            _function = TBFunction.Text;
            _educationalLevel = TBEducationalLevel.Text;
            _fkEmployee = Convert.ToInt32(DDLEmployee.SelectedValue);

            executed = objAux.updateAssistant(_idAux, _fkEmployee, _function, _function);

            if (executed)
            {
                LblMsg.Text = "El auxiliar se actualizo exitosamente!";
                clear(); //Se invoca el metodo para limpiar los campos 
            }
            else
            {
                LblMsg.Text = "Error al actualizar";
            }
        }
    }
}