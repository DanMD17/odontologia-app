using Logic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentation
{
    public partial class WFQuotes : System.Web.UI.Page
    {
        // Crear el objeto de la capa lógica
        QuotesLog objQuotesLog = new QuotesLog();
        PatientsLog objPat = new PatientsLog();
        DentistsLog objDent = new DentistsLog();

        private int _quoteId, _fkPatientId, _fkDentistId;
        private DateTime _date;
        private TimeSpan _time;
        private string _status;
        private bool executed;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                showPatientsDDL();
                showDentistsDDL();
                //showQuotes();
            }
        }

        // Método para listar las citas
        [WebMethod]
        public static object ListQuotes()
        {
            QuotesLog objQuotesLog = new QuotesLog();

            // Se obtiene un DataSet que contiene la lista de citas desde la base de datos.
            var dataSet = objQuotesLog.showQuotes();

            // Se crea una lista para almacenar las citas que se van a devolver.
            var quotesList = new List<object>();

            // Se itera sobre cada fila del DataSet (que representa una cita).
            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                quotesList.Add(new
                {
                    QuoteID = row["cita_id"],
                    Date = row["cita_fecha"],
                    Time = row["cita_hora"],
                    Status = row["cita_estado"],
                    FkPatientId = row["tbl_pacientes_paci_id"],
                    NamePatient = row["paci_nombre"],
                    FkDentistId = row["tbl_odontologos_odo_id"],
                    SpecialtyDentist = row["odo_especialidad"]
                });
            }

            // Devuelve un objeto en formato JSON que contiene la lista de citas.
            return new { data = quotesList };
        }

        // Eliminar una cita
        [WebMethod]
        public static bool DeleteQuote(int id)
        {
            QuotesLog objQuotesLog = new QuotesLog();

            // Invocar al método para eliminar la cita y devolver el resultado
            return objQuotesLog.deleteQuote(id);
        }

        // Método para mostrar los pacientes en el DDL
        private void showPatientsDDL()
        {
            DDLPatient.DataSource = objPat.showPatientsDDL();
            DDLPatient.DataValueField = "paci_id"; // Nombre de la llave primaria de la tabla de pacientes
            DDLPatient.DataTextField = "paci_nombre";
            DDLPatient.DataBind();
            DDLPatient.Items.Insert(0, "Seleccione");
        }

        // Método para mostrar los odontólogos en el DDL
        private void showDentistsDDL()
        {
            DDLDentist.DataSource = objDent.showDentistsDDL();
            DDLDentist.DataValueField = "odo_id"; // Nombre de la llave primaria de la tabla de odontólogos
            DDLDentist.DataTextField = "odo_especialidad";
            DDLDentist.DataBind();
            DDLDentist.Items.Insert(0, "Seleccione");
        }

        // Método para limpiar los TextBox y los DDL
        private void clear()
        {
            HFQuoteID.Value = "";
            TBDate.Text = "";
            TBTime.Text = "";
            TBStatus.Text = "";
            DDLPatient.SelectedIndex = 0;
            DDLDentist.SelectedIndex = 0;
        }

        // Evento que se ejecuta cuando se da clic en el botón guardar
        protected void BtnSave_Click(object sender, EventArgs e)
        {
            _date = Convert.ToDateTime(TBDate.Text);
            _time = TimeSpan.Parse(TBTime.Text);
            _status = TBStatus.Text;
            _fkPatientId = Convert.ToInt32(DDLPatient.SelectedValue);
            _fkDentistId = Convert.ToInt32(DDLDentist.SelectedValue);

            executed = objQuotesLog.saveQuote(_date, _time, _status, _fkPatientId, _fkDentistId);

            if (executed)
            {
                LblMsg.Text = "La cita se guardó exitosamente!";
                clear();
            }
            else
            {
                LblMsg.Text = "Error al guardar :(";
            }
        }

        // Evento del botón actualizar
        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(HFQuoteID.Value))
            {
                LblMsg.Text = "No se ha seleccionado una cita para actualizar.";
                return;
            }

            _quoteId = Convert.ToInt32(HFQuoteID.Value);
            _date = Convert.ToDateTime(TBDate.Text);
            _time = TimeSpan.Parse(TBTime.Text);
            _status = TBStatus.Text;
            _fkPatientId = Convert.ToInt32(DDLPatient.SelectedValue);
            _fkDentistId = Convert.ToInt32(DDLDentist.SelectedValue);

            executed = objQuotesLog.updateQuote(_quoteId, _date, _time, _status, _fkPatientId, _fkDentistId);

            if (executed)
            {
                LblMsg.Text = "La cita se actualizó exitosamente!";
                clear();
            }
            else
            {
                LblMsg.Text = "Error al actualizar";
            }
        }
    }
}
