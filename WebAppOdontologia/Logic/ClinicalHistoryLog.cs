using Data;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Runtime.Remoting;
using System.Web;

namespace Logic
{
    public class ClinicalHistoryLog
    {
        ClinicalHistoryDat objHist = new ClinicalHistoryDat();

        // Método para mostrar todos los historiales clínicos
        public DataSet showClinicalHistories()
        {
            return objHist.showClinicalHistories();
        }

        public DataSet showClinicalHistoriesDDL()
        {
            return objHist.showClinicalHistoriesDDL();
        }

        // Método para guardar un nuevo historial clínico
        public bool saveClinicalHistory(int _fkPacId, DateTime _pFechaCreacion, string _pDescripcionGeneral)

        {
            return objHist.saveClinicalHistory(_fkPacId, _pFechaCreacion, _pDescripcionGeneral);
        }

        // Método para actualizar un historial clínico
        public bool updateClinicalHistory(int _pHistorialId, DateTime _pFechaCreacion, string _pDescripcionGeneral, int _fkPacId)
        {
            return objHist.updateClinicalHistory(_pHistorialId, _pFechaCreacion, _pDescripcionGeneral, _fkPacId);
        }

        // Método para borrar un historial clínico
        public bool deleteClinicalHistory(int _pHistId)
        {
            return objHist.deleteClinicalHistory(_pHistId);
        }
    }
}
   