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
                
                showDiagnosis();
                showQuotesDDL();
                showClinicalHistoryDDL();
            }
        }

        private void showDiagnosis()
        {
            DataSet ds = new DataSet();
            ds = objDiag.showDiagnosis();
            GVDiagnosis.DataSource = ds;
            GVDiagnosis.DataBind();
        }

        //Metodo para mostrar las citas en el DDL
        private void showQuotesDDL()
        {
            DDLQuotes.DataSource = objQuo.showQuotesDDL();
            DDLQuotes.DataValueField = "cita_id"; //Nombre de la llave primaria
            DDLQuotes.DataTextField = "cita_descripcion";
            DDLQuotes.DataBind();
            DDLQuotes.Items.Insert(0, "Seleccione");
        }

        private void showClinicalHistoryDDL()
        {
            DDLClinicalHistory.DataSource = objCliniH.showClinicalHistoriesDDL();
            DDLClinicalHistory.DataValueField = "hist_id"; //Nombre de la llave primaria
            DDLClinicalHistory.DataTextField = "hist_descripcion";
            DDLClinicalHistory.DataBind();
            DDLClinicalHistory.Items.Insert(0, "Seleccione");
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {

            _description = TBDescription.Text;
            _observations = TBObservations.Text;
            _date = Convert.ToDateTime(TBDate.Text);
            _fkQuotes = Convert.ToInt32(DDLQuotes.SelectedValue);
            _fkClinicalHistory = Convert.ToInt32(DDLClinicalHistory.SelectedValue);

            executed = objDiag.saveDiagnosis(_fkQuotes, _description, _date, _observations, _fkClinicalHistory);
            

            if (executed)
            {
                LblMsg.Text = "El diagnostico se guardó exitosamente!";
                showDiagnosis();
            }
            else
            {
                LblMsg.Text = "Error al guardar :(";
            }
        }
    }
}


