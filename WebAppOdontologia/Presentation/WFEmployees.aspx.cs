using System;
using Logic;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.CodeDom.Compiler;
using System.Net;

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
                showEmployees();
            }
        }
        //Metodo para mostrar los empleados
        private void showEmployees()
        {
            DataSet ds = new DataSet();
            ds = objEmp.showEmployees();
            GVEmployees.DataSource = ds;
            GVEmployees.DataBind();
        }

        //Boton de guardar
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
                LblMsg.Text = "El producto se guardó exitosamente!";
                showEmployees();
            }
            else
            {
                LblMsg.Text = "Error al guardar :(";
            }
        }
    }
}