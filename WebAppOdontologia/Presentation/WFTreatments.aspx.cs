using System;
using Logic;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentation
{
    public partial class WFTreatments : System.Web.UI.Page
    {
        TreatmentsLog objTreatments = new TreatmentsLog();

        private int _treatmentId;
        private string _name, _description, _observations;
        private DateTime _date;
        private int _fkCitaId, _fkHistId, _fkAuxId;
        private bool executed;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                showTreatments();
            }
        }

        private void showTreatments()
        {
            DataSet ds = objTreatments.showTreatments();
            GVTreatments.DataSource = ds;
            GVTreatments.DataBind();
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            _name = TBName.Text;
            _description = TBDescription.Text;
            _date = DateTime.Parse(TBDate.Text);
            _observations = TBObservations.Text;
            _fkCitaId = int.Parse(TBFkCitaId.Text);
            _fkHistId = int.Parse(TBFkHistId.Text);
            _fkAuxId = int.Parse(TBFkAuxId.Text);

            executed = objTreatments.saveTreatment(_name, _description, _date, _observations, _fkCitaId, _fkHistId, _fkAuxId);

            if (executed)
            {
                LblMsg.Text = "El tratamiento se guardó exitosamente!";
                showTreatments();
            }
            else
            {
                LblMsg.Text = "Error al guardar el tratamiento.";
            }
        }

        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            _treatmentId = int.Parse(TBId.Text);
            _name = TBName.Text;
            _description = TBDescription.Text;
            _date = DateTime.Parse(TBDate.Text);
            _observations = TBObservations.Text;
            _fkCitaId = int.Parse(TBFkCitaId.Text);
            _fkHistId = int.Parse(TBFkHistId.Text);
            _fkAuxId = int.Parse(TBFkAuxId.Text);

            executed = objTreatments.updateTreatment(_treatmentId, _name, _description, _date, _observations, _fkCitaId, _fkHistId, _fkAuxId);

            if (executed)
            {
                LblMsg.Text = "El tratamiento se actualizó exitosamente!";
                showTreatments();
            }
            else
            {
                LblMsg.Text = "Error al actualizar el tratamiento.";
            }
        }
    }
}
