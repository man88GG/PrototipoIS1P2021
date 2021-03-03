using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaModelo;

namespace CapaControlador
{
    public class ClsControlador
    {

        ClsSentencias Sn = new ClsSentencias();

        public DataTable funcDepto()
        {
            DataTable Items = Sn.FuncDepto();
            return Items;
        }

        public DataTable funcPuesto()
        {
            DataTable Items = Sn.FuncPuesto();
            return Items;
        }

        public OdbcDataReader funcBuscarEmpleado(string IdEmpleado)
        {
            OdbcDataReader Lector = Sn.FuncBuscarEmpleado(IdEmpleado);
            return Lector;
        }

        public void funcInsertarEmpleado(string Nombres, int codigoP,int codigoD, string sueldo,int estatus)
        {
            Sn.FuncInsertarEmpleado(Nombres, codigoP, codigoD, sueldo, estatus);
        }

        //Paso de datos para consulta modificar en la entidad PASAPORTE
        public void funcModificarEmpleado(int codigoP, int codigoD, string sueldo, int estatus, string IDE)
        {

            Sn.FuncModificarEmpleado(codigoP, codigoD, sueldo, estatus, IDE);


        }



    }
}
