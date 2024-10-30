using System;
using System.Data;
using Data;

namespace Logic
{
    public class TreatmentsLog
    {
        TreatmentsDat objTreatments = new TreatmentsDat();

        // Método para mostrar todos los tratamientos realizados
        public DataSet showTreatments()
        {
            return objTreatments.showTreatments();
        }

        // Método para mostrar tratamientos en un dropdown list
        public DataSet showTreatmentsDDL()
        {
            return objTreatments.showTreatmentsDDL();
        }

        // Método para insertar un nuevo tratamiento realizado
        public bool saveTreatment(string nombre, string descripcion, DateTime fecha, string observaciones, int fkCitaId, int fkHistId, int fkAuxId)
        {
            return objTreatments.saveTreatment(nombre, descripcion, fecha, observaciones, fkCitaId, fkHistId, fkAuxId);
        }

        // Método para actualizar un tratamiento realizado
        public bool updateTreatment(int trataId, string nombre, string descripcion, DateTime fecha, string observaciones, int fkCitaId, int fkHistId, int fkAuxId)
        {
            return objTreatments.updateTreatment(trataId, nombre, descripcion, fecha, observaciones, fkCitaId, fkHistId, fkAuxId);
        }

        // Método para eliminar un tratamiento realizado
        public bool deleteTreatment(int trataId)
        {
            return objTreatments.deleteTreatment(trataId);
        }
    }
}
