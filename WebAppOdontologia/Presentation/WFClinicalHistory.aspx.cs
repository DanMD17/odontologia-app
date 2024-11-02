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
    public partial class WFClinicalHistory : System.Web.UI.Page
    {
        ClinicalHistoryLog objCliH = new ClinicalHistoryLog();
        PatientsLog objPati = new PatientsLog();

        private int _id, _fkPatient;
        private DateTime _pCreacionDate;
        private string _pOverview;
        private bool executed;


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Aqui se invocan todos los metodos
                showPatientsDDL();
               showClinicalHistories();
            }
        }


        private void showClinicalHistories()
        {
            DataSet ds = new DataSet();
            ds = objCliH.showClinicalHistories();
            GVClinicalHistory.DataSource = ds;
            GVClinicalHistory.DataBind();
        }

        //Metodo para mostrar los empleados en el DDL
        private void showPatientsDDL()
        {
            DDLPatient.DataSource = objPati.showPatientsDDL();
            DDLPatient.DataValueField = "paci_id"; //Nombre de la llave primaria
            DDLPatient.DataTextField = "paci_nombre";
            DDLPatient.DataBind();
            DDLPatient.Items.Insert(0, "Seleccione");
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            _pCreacionDate = Convert.ToDateTime(TBCreacionDate.Text);
            _pOverview = TBOverview.Text;
            _fkPatient = Convert.ToInt32(DDLPatient.SelectedValue);

            executed = objCliH.saveClinicalHistory(_fkPatient, _pCreacionDate, _pOverview);

            if (executed)
            {
                LblMsg.Text = "La Historia Clinica se guardó exitosamente!";
                showClinicalHistories();
            }
            else
            {
                LblMsg.Text = "Error al guardar :(";
            }
        }
    }
}

