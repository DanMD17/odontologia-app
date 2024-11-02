using System;
using Logic;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentation
{
    public partial class WFPatients : System.Web.UI.Page
    {
        PatientsLog objPat = new PatientsLog();

        private int _patientId;
        private string _name, _lastName, _address, _cellPhone, _email;
        private DateTime _dateOfBirth;
        private bool executed;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                showPatients();
            }
        }

        // Método para mostrar pacientes
        private void showPatients()
        {
            DataSet ds = new DataSet();
            ds = objPat.showPatients();
            GVPatients.DataSource = ds;
            GVPatients.DataBind();
        }

        // Botón de guardar
        protected void BtnSave_Click(object sender, EventArgs e)
        {
            _name = TBName.Text;
            _lastName = TBLastName.Text;
            _dateOfBirth = DateTime.Parse(TBDateOfBirth.Text);
            _address = TBAddress.Text;
            _cellPhone = TBCellPhone.Text;
            _email = TBEmail.Text;

            executed = objPat.savePatient(_name, _lastName, _dateOfBirth, _address, _cellPhone, _email);

            if (executed)
            {
                LblMsg.Text = "El paciente se guardó exitosamente!";
                showPatients();
            }
            else
            {
                LblMsg.Text = "Error al guardar el paciente.";
            }
        }

        // Botón de actualizar
        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            _patientId = int.Parse(TBId.Text);
            _name = TBName.Text;
            _lastName = TBLastName.Text;
            _dateOfBirth = DateTime.Parse(TBDateOfBirth.Text);
            _address = TBAddress.Text;
            _cellPhone = TBCellPhone.Text;
            _email = TBEmail.Text;

            executed = objPat.updatePatient(_patientId, _name, _lastName, _dateOfBirth, _address, _cellPhone, _email);

            if (executed)
            {
                LblMsg.Text = "El paciente se actualizó exitosamente!";
                showPatients();
            }
            else
            {
                LblMsg.Text = "Error al actualizar el paciente.";
            }
        }
    }
}
