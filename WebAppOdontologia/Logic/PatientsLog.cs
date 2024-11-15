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
    public class PatientsLog
    {
        PatientsDat objPat = new PatientsDat();

        public DataSet showPatientsDDL()
        {
            return objPat.showPatientsDDL();
        }

        // Método para mostrar todos los pacientes
        public DataSet showPatients()
        {
            return objPat.showPatients();
        }

        // Método para guardar un nuevo paciente
        public bool savePatient(string _pNombre, string _pApellido, DateTime _pFechaNacimiento, string _pDireccion, string _pCelular, string _pCorreo)
        {
            return objPat.savePatient(_pNombre, _pApellido, _pFechaNacimiento, _pDireccion, _pCelular, _pCorreo);
        }

        // Método para actualizar un paciente
        public bool updatePatient(int _pPacienteId, string _pNombre, string _pApellido, DateTime _pFechaNacimiento, string _pDireccion, string _pCelular, string _pCorreo)
        {
            return objPat.updatePatient(_pPacienteId, _pNombre, _pApellido, _pFechaNacimiento, _pDireccion, _pCelular, _pCorreo);
        }

        // Método para borrar un paciente
        public bool deletePatient(int _pPacienteId)
        {
            return objPat.deletePatient(_pPacienteId);
        }
    }
}