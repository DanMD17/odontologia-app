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
                showDentists();
                showEmployeesDDL();
            }
        }

        private void showDentists()
        {
            DataSet ds = new DataSet();
            ds = objDen.showDentists();
            GVDentists.DataSource = ds;
            GVDentists.DataBind();
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

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            _specialty = TBSpecialty.Text;
            _fkEmployee = Convert.ToInt32(DDLEmployee.SelectedValue);

            executed = objDen.saveDentist(_specialty, _fkEmployee);

            if (executed)
            {
                LblMsg.Text = "El Odontólogo se guardó exitosamente!";
                showDentists();
            }
            else
            {
                LblMsg.Text = "Error al guardar";
            }
        }
    }
}
    
