using Logic;
using Mysqlx.Crud;
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
                //showClinicalHistories();
                //Se asigna la fecha actual al TextBox en formato "yyyy-MM-dd".
                TBCreacionDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                showPatientsDDL();
            }
        }

        //Metodo para mostrar todos las historias clinicas

        [WebMethod]
        public static object ListClinicalHistory()
        {
            ClinicalHistoryLog objCliH = new ClinicalHistoryLog();

            // Se obtiene un DataSet que contiene la lista de las historias clinicas desde la base de datos.
            var dataSet = objCliH.showClinicalHistories();

            // Se crea una lista para almacenar las historias clinicas que se van a devolver.
            var clinicalHistoryList = new List<object>();

            // Se itera sobre cada fila del DataSet (que representa una historia clinica).
            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                clinicalHistoryList.Add(new
                {
                    ClinicalHistoryID = row["hist_id"],
                    CreacionDate = Convert.ToDateTime(row["hist_fecha_creacion"]).ToString("yyyy-MM-dd"), // Formato de fecha específico.
                    Overview = row["hist_descripcion_general"],
                    FkPatient = row["tbl_pacientes_paci_id"],
                    NamePatient = row["paci_nombre"]

                });
            }

            // Devuelve un objeto en formato JSON que contiene la lista de historiales clinicos.
            return new { data = clinicalHistoryList };
        }

        [WebMethod]
        public static bool DeleteClinicalHistory(int id)
        {
            // Crear una instancia de la clase de lógica de Historiales clinicos
            ClinicalHistoryLog objCliH = new ClinicalHistoryLog();

            // Invocar al método para eliminar el historial clinico y devolver el resultado
            return objCliH.deleteClinicalHistory(id);
        }

        //Metodo para mostrar los pacientes en el DDL
        private void showPatientsDDL()
        {
            DDLPatient.DataSource = objPati.showPatientsDDL();
            DDLPatient.DataValueField = "paci_id";//Nombre de la llave primaria
            DDLPatient.DataTextField = "paci_nombre";
            DDLPatient.DataBind();
            DDLPatient.Items.Insert(0, "Seleccione");
        }
        //Metodo para limpiar los TextBox y los DDL
        private void clear()
        {
            HFClinicalHistoryID.Value = "";
            TBCreacionDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            TBOverview.Text = "";
            DDLPatient.SelectedIndex = 0;
            
        }

        //Boton de guardar un historial clinico
        protected void BtnSave_Click(object sender, EventArgs e)
        {
            _pCreacionDate = Convert.ToDateTime(TBCreacionDate.Text);
            _pOverview = TBOverview.Text;
            _fkPatient = Convert.ToInt32(DDLPatient.SelectedValue);

            executed = objCliH.saveClinicalHistory(_fkPatient, _pCreacionDate, _pOverview);

            if (executed)
            {
                LblMsg.Text = "El Historial Clinico se guardo exitosamente!";

            }
            else
            {
                LblMsg.Text = "Error al guardar";
            }
        }

        // Evento del boton actualizar
        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            // Verifica si se ha seleccionado un historial clinico para actualizar
            if (string.IsNullOrEmpty(HFClinicalHistoryID.Value))
            {
                LblMsg.Text = "No se ha seleccionado una historia clinica para actualizar.";
                return;
            }
            _id = Convert.ToInt32(HFClinicalHistoryID.Value);
            _pCreacionDate = Convert.ToDateTime(TBCreacionDate.Text);
            _pOverview = TBOverview.Text;
            _fkPatient = Convert.ToInt32(DDLPatient.SelectedValue);


            executed = objCliH.updateClinicalHistory( _fkPatient, _pCreacionDate, _pOverview, _id);

            if (executed)
            {
                LblMsg.Text = "La Historia Clinica se actualizo exitosamente!";
                clear(); //Se invoca el metodo para limpiar los campos 
            }
            else
            {
                LblMsg.Text = "Error al actualizar";
            }
        }

    }
}

