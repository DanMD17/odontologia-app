using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Data;

namespace Logic
{
    public class SecretariesLog
    {
        SecretariesDat objSec = new SecretariesDat();

        // Método para mostrar todas las secretarias
        public DataSet showSecretaries()
        {
            return objSec.showSecretaries();
        }

        // Método para insertar una nueva secretaria
        public bool saveSecretary(int _fkEmpId, string _funcion, string _aniosExperiencia)
        {
            return objSec.saveSecretary(_fkEmpId, _funcion, _aniosExperiencia);
        }


        // Método para actualizar una secretaria
        public bool updateSecretaria(int _secId, int _fkEmpId, string _funcion, string _aniosExperiencia)
        {
            return objSec.updateSecretaria(_secId, _fkEmpId, _funcion, _aniosExperiencia);
        }

        // Método para eliminar una secretaria
        public bool deleteSecretaria(int _secId)
        {
            return objSec.deleteSecretaria(_secId);
        }
    }
}