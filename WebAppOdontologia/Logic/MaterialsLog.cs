using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Data;

namespace Logic
{
    public class MaterialsLog
    {
        MaterialsDat objMaterial = new MaterialsDat();

        // Método para mostrar todos los materiales
        public DataSet showMaterials()
        {
            return objMaterial.showMaterials();
        }

        // Método para guardar un nuevo material
        public bool saveMaterial(string _nombre, string _descripcion, int _cantidad, int _fktrataId)
        {
            return objMaterial.saveMaterial(_nombre, _descripcion, _cantidad,  _fktrataId);
        }

        // Método para actualizar un material existente
        public bool updateMaterial(int _materialId, string _nombre, string _descripcion, int _cantidad, int _fkTrataId)
        {
            return objMaterial.updateMaterial(_materialId, _nombre, _descripcion, _cantidad, _fkTrataId);
        }

        // Método para borrar un material
        public bool deleteMaterial(int _materialId)
        {
            return objMaterial.deleteMaterial(_materialId);
        }
    }
}