using System;
using Data;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Logic
{
    public class AuxiliariesLog
    {
        AuxiliariesDat objAssist = new AuxiliariesDat();

        //Metodo para mostrar unicamente el id y la descripcion de los auxiliares en DDL
        public DataSet showAssistantsDDL()
        {
            return objAssist.showAssistantsDDL();
        }

        //Metodo para mostrar los auxiliar
        public DataSet showAssistants()
        {
            return objAssist.showAssistants();
        }

        //Metodo para guardar un nuevo auxiliar
        public bool saveAssistant(int _fkEmpId, string _auxFuncion, string _auxNivelEducativo)
        {
            return objAssist.saveAssistant(_fkEmpId, _auxFuncion, _auxNivelEducativo);
        }

        //Metodo para actualizar un auxiliar
        public bool updateAssistant(int _id, int _fkEmpId, string _auxFuncion, string _auxNivelEducativo)
        {
            return objAssist.updateAssistant(_id, _fkEmpId, _auxFuncion, _auxNivelEducativo);
        }

        //Metodo para borrar un auxiliar
        public bool deleteAssistant(int _idAuxiliar)
        {
            return objAssist.deleteAssistant(_idAuxiliar);
        }
    }
}