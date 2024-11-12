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
    public class DentistsLog
    {
        DentistsDat objDen = new DentistsDat();

        //Metodo para mostrar unicamente el id  de los odontólogos, en el DropDownList
        public DataSet showDentistsDDL()
        {
            return objDen.showDentistsDDL();
        }

        // Método para mostrar todos los odontólogos
        public DataSet showDentists()
        {
            return objDen.showDentists();
        }

        // Método para insertar un nuevo odontólogo
        public bool saveDentist(string _especialidad, int _fkempId)
        {
            return objDen.saveDentist(_especialidad, _fkempId);
        }

        // Método para actualizar un odontólogo
        public bool updateDentist(int _odoId, string _especialidad, int _fkempId)
        {
            return objDen.updateDentist(_odoId, _especialidad, _fkempId);
        }

        // Método para eliminar un odontólogo
        public bool deleteDentist(int _odoId)
        {
            return objDen.deleteDentist(_odoId);
        }
    }
}