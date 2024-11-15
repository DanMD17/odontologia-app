using System;
using Logic;
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
                //showPatients();
            }
        }
        // Método para listar los pacientes
        [WebMethod]
        public static object ListPatients()
        {
            PatientsLog objPat = new PatientsLog();

            // Se obtiene un DataSet que contiene la lista de pacientes desde la base de datos.
            var dataSet = objPat.showPatients();

            // Se crea una lista para almacenar los pacientes que se van a devolver.
            var patientsList = new List<object>();

            // Se itera sobre cada fila del DataSet (que representa un paciente).
            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                patientsList.Add(new
                {
                    PatientID = row["paci_id"],
                    Name = row["paci_nombre"],
                    LastName = row["paci_apellido"],
                    Address = row["paci_direccion"],
                    CellPhone = row["paci_celular"],
                    Email = row["paci_correo"],
                    DateOfBirth = row["paci_fecha_nacimiento"]
                });
            }

            // Devuelve un objeto en formato JSON que contiene la lista de pacientes.
            return new { data = patientsList };
        }

        // Eliminar un paciente
        [WebMethod]
        public static bool DeletePatient(int id)
        {
            PatientsLog objPat = new PatientsLog();

            // Invocar al método para eliminar el paciente y devolver el resultado
            return objPat.deletePatient(id);
        }

        // Método para limpiar los TextBox y los DDL
        private void clear()
        {
            HFPatientsID.Value = "";
            TBName.Text = "";
            TBLastName.Text = "";
            TBAddress.Text = "";
            TBPhone.Text = "";
            TBEmail.Text = "";
            TBDateOfBirth.Text = "";
        }

        // Evento que se ejecuta cuando se da clic en el botón guardar
        protected void BtnSave_Click(object sender, EventArgs e)
        {
            _name = TBName.Text;
            _lastName = TBLastName.Text;
            _address = TBAddress.Text;
            _cellPhone = TBPhone.Text;
            _email = TBEmail.Text;
            _dateOfBirth = Convert.ToDateTime(TBDateOfBirth.Text);

            executed = objPat.savePatient(_name, _lastName, _dateOfBirth, _address, _cellPhone, _email);

            if (executed)
            {
                LblMsg.Text = "El paciente se guardó exitosamente!";
            }
            else
            {
                LblMsg.Text = "Error al guardar :(";
            }
        }

        // Evento del botón actualizar
        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(HFPatientsID.Value))
            {
                LblMsg.Text = "No se ha seleccionado un paciente para actualizar.";
                return;
            }

            _patientId = Convert.ToInt32(HFPatientsID.Value);
            _name = TBName.Text;
            _lastName = TBLastName.Text;
            _address = TBAddress.Text;
            _cellPhone = TBPhone.Text;
            _email = TBEmail.Text;
            _dateOfBirth = Convert.ToDateTime(TBDateOfBirth.Text);

            executed = objPat.updatePatient(_patientId, _name, _lastName, _dateOfBirth, _address, _cellPhone, _email);

            if (executed)
            {
                LblMsg.Text = "El paciente se actualizó exitosamente!";
                clear();
            }
            else
            {
                LblMsg.Text = "Error al actualizar";
            }
        }
    }
}
