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
    public partial class WFDiagnosis : System.Web.UI.Page
    {
        DiagnosisLog objDiag = new DiagnosisLog();
        QuotesLog objQuo = new QuotesLog();
        ClinicalHistoryLog objCliniH = new ClinicalHistoryLog();

        private int _idDiag, _fkQuotes, _fkClinicalHistory;
        private string _description, _observations;
        private DateTime _date;
        private bool executed;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Aqui se invocan todos los metodos
                
               // showDiagnosis();
                showQuotesDDL();
                showClinicalHistoryDDL();
            }
        }

        [WebMethod]
        public static object ListDiagnosis()
        {
            DiagnosisLog objDiag = new DiagnosisLog();

            // Se obtiene un DataSet que contiene la lista de diagnosticos desde la base de datos.
            var dataSet = objDiag.showDiagnosis();

            // Se crea una lista para almacenar los diagnosticos que se van a devolver.
            var diagnosisList = new List<object>();

            // Se itera sobre cada fila del DataSet (que representa un diagnostico).
            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                diagnosisList.Add(new
                {
                    DiagnosisID = row["diag_id"],
                    Description = row["diag_descripcion"],
                    Date = Convert.ToDateTime(row["diag_fecha"]).ToString("yyyy-MM-dd"), // Formato de fecha específico.
                    Observations = row["diag_observaciones"], 
                    FkClinicalHistory = row["tbl_historialclinico_hist_id"],
                    DateClinicalHistory = row["Hist_fecha_creacion"],
                    FkQuotes = row["tbl_citas_cita_id"],
                    DateQuote = row["cita_fecha"]
                });
            }

            // Devuelve un objeto en formato JSON que contiene la lista de diagnosticos.
            return new { data = diagnosisList };
        }

        [WebMethod]
        public static bool DeleteDiagnosis(int id)
        {
            // Crear una instancia de la clase de lógica de diagnosticos
            DiagnosisLog objDiag = new DiagnosisLog();

            // Invocar al método para eliminar el diagnostico y devolver el resultado
            return objDiag.deleteDiagnosis(id);
        }

        //Metodo para mostrar las historias clinicas en el DDL
        private void showClinicalHistoryDDL()
        {
            DDLClinicalHistory.DataSource = objCliniH.showClinicalHistoriesDDL();
            DDLClinicalHistory.DataValueField = "hist_id";//Nombre de la llave primaria
            DDLClinicalHistory.DataTextField = "hist_descripcion_general";
            DDLClinicalHistory.DataBind();
            DDLClinicalHistory.Items.Insert(0, "Seleccione");
        }
        //Metodo para mostrar las citas en el DDL
        private void showQuotesDDL()
        {
            DDLQuote.DataSource = objQuo.showQuotesDDL();
            DDLQuote.DataValueField = "cita_id";//Nombre de la llave primaria
            DDLQuote.DataTextField = "cita_estado";
            DDLQuote.DataBind();
            DDLQuote.Items.Insert(0, "Seleccione");
        }
        //Metodo para limpiar los TextBox y los DDL
        private void clear()
        {
            HFDiagnosisID.Value = "";
            TBDescription.Text = "";
            TBDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            TBObservations.Text = "";
            DDLClinicalHistory.SelectedIndex = 0;
            DDLQuotes.SelectedIndex = 0;
        }
        //Eventos que se ejecutan cuando se da clic en los botones
        protected void BtnSave_Click(object sender, EventArgs e)
        {
            _description = TBDescription.Text;
            _date = DateTime.Parse(TBDate.Text);
            _observations = TBObservations.Text;
            _fkClinicalHistroy = Convert.ToInt32(DDLClinicalHistory.SelectedValue);
            _fkQuotes = Convert.ToInt32(DDLQuotes.SelectedValue);

            executed = objDiag.saveDiagnosis( _description, _date, _observations, _fkClinicalHistory, _fkQuotes);

            if (executed)
            {
                LblMsg.Text = "El Diagnostico se guardo exitosamente!";

            }
            else
            {
                LblMsg.Text = "Error al guardar";
            }
        }
        // Evento del boton actualizar
        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            // Verifica si se ha seleccionado un diagnostico para actualizar
            if (string.IsNullOrEmpty(HFProductID.Value))
            {
                LblMsg.Text = "No se ha seleccionado un diagnostico para actualizar.";
                return;
            }
            _id = Convert.ToInt32(HFProductID.Value);
            _description = TBDescription.Text;
            _date = DateTime.Parse(TBDate.Text);
            _observations = TBObservations.Text;
            _fkClinicalHistory = Convert.ToInt32(DDLClinicalHistory.SelectedValue);
            _fkQuotes = Convert.ToInt32(DDLQuotes.SelectedValue);

            executed = objDiag.updateDiagnosis(_id, _description, _date, _observations, _fkClinicalHistory, _fkQuotes);

            if (executed)
            {
                LblMsg.Text = "El diagnostico se actualizo exitosamente!";
                clear(); //Se invoca el metodo para limpiar los campos 
            }
            else
            {
                LblMsg.Text = "Error al actualizar";
            }
            
        }
    }
}


