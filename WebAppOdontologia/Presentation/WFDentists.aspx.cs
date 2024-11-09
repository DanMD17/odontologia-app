using Logic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentation
{
    public partial class WFDentists : System.Web.UI.Page
    {
        DentistsLog objDen = new DentistsLog();
        EmployeesLog objEmp = new EmployeesLog();

        private int _idDen, _fkEmployee;
        private string _specialty;
        private bool executed;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Aqui se invocan todos los metodos
                //showDentists();
                showEmployeesDDL();
            }
        }
        //Metodo para mostrar todos los Dentistas
        [WebMethod]
        public static object ListDentists()
        {
            DentistsLog objDen = new DentistsLog();

            // Se obtiene un DataSet que contiene la lista de los dentistas desde la base de datos.
            var dataSet = objDen.showDentists();

            // Se crea una lista para almacenar los dentistas que se van a devolver.
            var dentistsList = new List<object>();

            // Se itera sobre cada fila del DataSet (que representa un dentista).
            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                dentistsList.Add(new
                {
                    DentistID = row["odo_id"],
                    Specialty = row["odo_especialidad"],
                    FkEmployee = row["tbl_empleado_emp_id"],
                    NameEmployee = row["emp_nombre"]
                });
            }

            // Devuelve un objeto en formato JSON que contiene la lista de Dentistas.
            return new { data = dentistsList };
        }

        //Metodo para eliminar
        [WebMethod]
        public static bool DeleteDentist(int id)
        {
            // Crear una instancia de la clase de lógica de dentistas
            DentistsLog objDen = new DentistsLog();

            // Invocar al método para eliminar el auxiliar y devolver el resultado
            return objDen.deleteDentist(id);
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
            HFDentistID.Value = "";
            TBFSpecialty.Text = "";
            DDLEmployee.SelectedIndex = 0;
        }

        //Boton de guardar un dentista
        protected void BtnSave_Click(object sender, EventArgs e)
        {
            _specialty = TBSpecialty.Text;
            _fkEmployee = Convert.ToInt32(DDLEmployee.SelectedValue);

            executed = objDen.saveAssistant(_fkEmployee, _specialty);

            if (executed)
            {
                LblMsg.Text = "El Dentista se guardó exitosamente!";
            }
            else
            {
                LblMsg.Text = "Error al guardar :(";
            }
        }

        // Evento del boton actualizar
        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            // Verifica si se ha seleccionado un dentista para actualizar
            if (string.IsNullOrEmpty(HFDentistID.Value))
            {
                LblMsg.Text = "No se ha seleccionado un dentista para actualizar.";
                return;
            }
            _idDen = Convert.ToInt32(HFDentistID.Value);
            _specialty = TBSpecialty.Text;
            _fkEmployee = Convert.ToInt32(DDLEmployee.SelectedValue);

            executed = objDen.updateDentist(_idDen, _fkEmployee, _specialty);

            if (executed)
            {
                LblMsg.Text = "El Dentista se actualizo exitosamente!";
                clear(); //Se invoca el metodo para limpiar los campos 
            }
            else
            {
                LblMsg.Text = "Error al actualizar";
            }
        }
    }
}
