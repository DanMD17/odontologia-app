using Logic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentation
{
    public partial class WFMaterials : System.Web.UI.Page
    {
        // Se crean los objetos
        MaterialsLog objMat = new MaterialsLog();
        TreatmentsLog objTreat = new TreatmentsLog();

        private int _IdMat, _fkTrataId, _materialQuantity;
        private string _materialName, _materialDescription;
        private bool executed;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Aquí se invocan todos los métodos
                //showMaterials();
                showTreatmentsDDL();
            }
        }

        // Método para listar los materiales a través de un WebMethod
        [WebMethod]
        public static object ListMaterials()
        {
            MaterialsLog objMat = new MaterialsLog();

            // Obtener un DataSet con la lista de materiales
            var dataSet = objMat.showMaterials();

            // Crear una lista para almacenar los materiales
            var materialsList = new List<object>();

            // Iterar sobre cada fila del DataSet
            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                materialsList.Add(new
                {
                    MaterialID = row["material_id"],
                    Nombre = row["material_nombre"],
                    Descripcion = row["material_descripcion"],
                    Cantidad = row["material_cantidad"],
                    FkTratamiento = row["tbl_tratamiento_trata_id"],
                    Tratamiento = row["trata_nombre"]
                });
            }

            // Devolver el listado en formato JSON
            return new { data = materialsList };
        }

        // Método para mostrar los tratamientos en el DDL
        private void showTreatmentsDDL()
        {
            DDLTreatments.DataSource = objTreat.showTreatmentsDDL();
            DDLTreatments.DataValueField = "trata_id"; // Nombre de la llave primaria
            DDLTreatments.DataTextField = "trata_nombre";
            DDLTreatments.DataBind();
            DDLTreatments.Items.Insert(0, "Seleccione");
        }

        // Método para guardar un material nuevo
        protected void BtnSave_Click(object sender, EventArgs e)
        {
            _materialDescription = TBmaterialDescription.Text;
            _materialName = TBmaterialName.Text;
            _materialQuantity = Convert.ToInt32(TBmaterialQuantity.Text);
            _fkTrataId = Convert.ToInt32(DDLTreatments.SelectedValue);

            executed = objMat.saveMaterial(_materialDescription, _materialName, _materialQuantity, _fkTrataId);

            if (executed)
            {
                LblMsg.Text = "El material se guardó exitosamente!";
                showMaterials();
                clearFields(); // Se invoca el metodo para limpiar los campos
            }
            else
            {
                LblMsg.Text = "Error al guardar";
            }
        }

        // Método para actualizar un material existente
        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            // Verifica si se ha seleccionado un material para actualizar
            if (string.IsNullOrEmpty(TBId.Value))
            {
                LblMsg.Text = "No se ha seleccionado un material para actualizar.";
                return;
            }
            _IdMat = Convert.ToInt32(TBId.Value);
            _materialDescription = TBmaterialDescription.Text;
            _materialName = TBmaterialName.Text;
            _materialQuantity = Convert.ToInt32(TBmaterialQuantity.Text);
            _fkTrataId = Convert.ToInt32(DDLTreatments.SelectedValue);

            executed = objMat.updateMaterial(_IdMat, _materialDescription, _materialName, _materialQuantity, _fkTrataId);

            if (executed)
            {
                LblMsg.Text = "El material se actualizó exitosamente!";
                clear(); // Se invoca el metodo para limpiar los campos
            }
            else
            {
                LblMsg.Text = "Error al actualizar";
            }
        }

        // Método para eliminar un material a través de un WebMethod
        [WebMethod]
        public static bool DeleteMaterial(int id)
        {
            // Crear una instancia de la clase de lógica de materiales
            MaterialsLog objMat = new MaterialsLog();

            // Invocar al método para eliminar el material y devolver el resultado
            return objMat.deleteMaterial(id);
        }

        // Método para mostrar los materiales en el GridView
        private void showMaterials()
        {
            DataSet ds = objMat.showMaterials();
            GVMateriales.DataSource = ds;
            GVMateriales.DataBind();
        }

        // Método para limpiar los TextBox y el DDL
        private void clear()
        {
            TBId.Value = "";
            TBmaterialName.Text = "";
            TBmaterialDescription.Text = "";
            TBmaterialQuantity.Text = "";
            DDLTreatments.SelectedIndex = 0;
        }
    }
}
