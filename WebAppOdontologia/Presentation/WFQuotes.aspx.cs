using System;
using System.Data;
using Logic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentation
{
    public partial class WFQuotes : System.Web.UI.Page
    {
        // Crear el objeto de la capa lógica
        QuotesLog objQuotesLog = new QuotesLog();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Invocar el método para mostrar las citas
                showQuotes();
            }
        }

        // Método para mostrar las citas en el GridView
        private void showQuotes()
        {
            DataSet ds = objQuotesLog.showQuotes();
            GVQuotes.DataSource = ds;
            GVQuotes.DataBind();
        }

        // Evento del botón de guardar
        protected void BtnSave_Click(object sender, EventArgs e)
        {
            DateTime fecha = DateTime.Parse(TBDate.Text);
            TimeSpan hora = TimeSpan.Parse(TBTime.Text);
            string estado = TBStatus.Text;
            int fkPaciId = int.Parse(TBPatientId.Text);
            int fkOdoId = int.Parse(TBDentistId.Text);

            bool executed = objQuotesLog.saveQuote(fecha, hora, estado, fkPaciId, fkOdoId);

            if (executed)
            {
                LblMsg.Text = "¡La cita se guardó exitosamente!";
                showQuotes();
            }
            else
            {
                LblMsg.Text = "Error al guardar la cita.";
            }
        }
    }
}
