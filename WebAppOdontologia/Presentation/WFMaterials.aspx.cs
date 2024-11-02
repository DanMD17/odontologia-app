using System;
using Logic;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Runtime.Remoting;

namespace Presentation
{
    public partial class WFMaterials : System.Web.UI.Page
    {
        //Se crean los objetos 
        MaterialsLog objMat = new MaterialsLog();
        TreatmentsLog objTreat = new TreatmentsLog();

        private int _IdMat, _fkTrataId, _materialQuantity;
        private string _materialName, _materialDescription;
        private bool executed;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Aqui se invocan todos los metodos

                showMaterials();
                showTreatmentsDDL();
            }
        }
        private void showMaterials()
        {
            DataSet ds = new DataSet();
            ds = objMat.showMaterials();
            GVMateriales.DataSource = ds;
            GVMateriales.DataBind();
        }

        //Metodo para mostrar los tratamientos en el DDL
        private void showTreatmentsDDL()
        {
            DDLTreatments.DataSource = objTreat.showTreatmentsDDL();
            DDLTreatments.DataValueField = "trata_id"; //Nombre de la llave primaria
            DDLTreatments.DataTextField = "trata_nombre";
            DDLTreatments.DataBind();
            DDLTreatments.Items.Insert(0, "Seleccione");
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            _materialDescription = TBmaterialDescription.Text;
            _materialName = TBmaterialName.Text;
            _materialQuantity = Convert.ToInt32(DDLTreatments.SelectedValue);
            _fkTrataId = Convert.ToInt32(DDLTreatments.SelectedValue);


            executed = objMat.saveMaterial(_materialDescription, _materialName, _materialQuantity, _fkTrataId);

            if (executed)
            {

                LblMsg.Text = "El material se guardó exitosamente!";
                showMaterials();
            }
            else
            {
                LblMsg.Text = "Error al guardar ";
            }

        }
    }
}