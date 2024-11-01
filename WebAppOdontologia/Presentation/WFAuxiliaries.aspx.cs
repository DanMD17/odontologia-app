using System;
using Logic;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Runtime.Remoting;

namespace Presentation
{
    public partial class WFAuxiliaries : System.Web.UI.Page
    {
        AuxiliariesLog objAux = new AuxiliaresLog();
        EmployeesLog objEmp = new EmployeesLog();

        private int _idAux, _fkEmployee;
        private string _function, _educationalLevel;
        private bool executed;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Aqui se invocan todos los metodos
                showAssistants();
                showEmployeesDDL();
            }
        }

        private void showAssistants()
        {
            DataSet ds = new DataSet();
            ds = objAux.showAssistants();
            GVAuxiliaries.DataSource = ds;
            GVAuxiliaries.DataBind();
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
            _educationalLevel = TBEducationalLevel.Text;
            _function = TBFunction.Text;
            _fkEmployee = Convert.ToInt32(DDLEmployee.SelectedValue);

            executed = objAux.saveAssistant(_fkEmployee, _function, _educationalLevel);

            if (executed)
            {
                LblMsg.Text = "La secretaria se guardó exitosamente!";
                showAssistants();
            }
            else
            {
                LblMsg.Text = "Error al guardar :(";
            }
        }
    }
}