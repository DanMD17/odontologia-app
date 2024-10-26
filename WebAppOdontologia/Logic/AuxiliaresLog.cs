using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Logic
{
    public class AuxiliaresLog
    {
        AuxiliariesDat objAssist = new AuxiliariesDat();

        //Metodo para mostrar unicamente el id y la descripcion de los Provedores, en el DropDownList
        public DataSet showAssistantsDDLL()
        {
            return objAssist.showAssistantsDDL();
        }

        //Metodo para mostrar los Provedores
        public DataSet showAssistants()
        {
            return objAssist.showAssistants();
        }

        //Metodo para guardar un nuevo Proveedor
        public bool saveProvider(string _fkEmpId, string _auxFuncion, string _auxNivelEducativo)
        {
            return objAssist.saveProvider(_fkEmpId, _auxFuncion, _auxNivelEducativo);
        }

        //Metodo para actualizar un Proveedor
        public bool updateProvider(int _id, string _fkEmpId, string _auxFuncion, string _auxNivelEducativo)
        {
            return objAssist.updateProvider( _id, _fkEmpId, _auxFuncion, _auxNivelEducativo);
        }
    }
}