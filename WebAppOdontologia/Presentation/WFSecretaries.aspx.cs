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
                showSecretaries();
                showEmployeesDDL();
            }
        }

        private void showSecretaries()
        {
            DataSet ds = new DataSet();
            ds = objSec.showSecretaries();
            GVSecretaries.DataSource = ds;
            GVSecretaries.DataBind();
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
            _yearsExp = TBYearsExp.Text;
            _function = TBFunction.Text;
            _fkEmployee = Convert.ToInt32(DDLEmployee.SelectedValue);

            executed = objSec.saveSecretary(_fkEmployee, _function, _yearsExp);

            if (executed)
            {
                LblMsg.Text = "La secretaria se guardó exitosamente!";
                showSecretaries();
            }
            else
            {
                LblMsg.Text = "Error al guardar :(";
            }
        }
    }
}