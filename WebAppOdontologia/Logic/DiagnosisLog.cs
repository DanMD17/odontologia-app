using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Data;

namespace Logic
{
    public class DiagnosisLog
    {
        DiagnosisDat objDiag = new DiagnosisDat();


        // Método para mostrar todos los diagnósticos
        public DataSet showDiagnosis()
        {
            return objDiag.showDiagnosis();
        }

        // Método para guardar un nuevo diagnóstico
        public bool saveDiagnosis(int _fkCitaId, string _pDiagDescripcion, DateTime _pDiagFecha, string _pDiagObservaciones, int _fkHistorialId)
        {
            return objDiag.saveDiagnosis(_fkCitaId, _pDiagDescripcion, _pDiagFecha, _pDiagObservaciones, _fkHistorialId);
        }

        // Método para actualizar un diagnóstico
        public bool updateDiagnosis(int _pDiagId, int _fkCitaId, string _pDiagDescripcion, DateTime _pDiagFecha, string _pDiagObservaciones, int _fkHistorialId)
        {

            return objDiag.updateDiagnosis(_pDiagId, _fkCitaId, _pDiagDescripcion, _pDiagFecha, _pDiagObservaciones, _fkHistorialId);
        }

        // Método para borrar un diagnóstico
        public bool deleteDiagnosis(int _pDiagId)
        {
            return objDiag.deleteDiagnosis(_pDiagId);
        }
    }
}