using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaVistaSeguridad;
using System.Data;
using System.Windows.Forms;

namespace CapaModelo
{
    
    public class ClsSentencias
    {
        

        clsConexion Con = new clsConexion();

        public DataTable FuncDepto()
        {
            DataTable Datos = new DataTable();
            try
            {
                string CargaTipoPass = "SELECT * FROM DEPARTAMENTO";
                OdbcCommand Query_Busqueda1 = new OdbcCommand(CargaTipoPass, Con.conexion());

                OdbcDataAdapter Lector = new OdbcDataAdapter();
                Lector.SelectCommand = Query_Busqueda1;
                Lector.Fill(Datos);

                Con.desconexion(Con.conexion());
                return Datos;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al ejecutar SQL: " +
                    System.Environment.NewLine + System.Environment.NewLine +
                    ex.GetType().ToString() + System.Environment.NewLine +
                    ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return Datos;
            }
        }//fin


        public DataTable FuncPuesto()
        {
            DataTable Datos = new DataTable();
            try
            {
                string CargaTipoPass = "SELECT * FROM PUESTO";
                OdbcCommand Query_Busqueda1 = new OdbcCommand(CargaTipoPass, Con.conexion());

                OdbcDataAdapter Lector = new OdbcDataAdapter();
                Lector.SelectCommand = Query_Busqueda1;
                Lector.Fill(Datos);

                Con.desconexion(Con.conexion());
                return Datos;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al ejecutar SQL: " +
                    System.Environment.NewLine + System.Environment.NewLine +
                    ex.GetType().ToString() + System.Environment.NewLine +
                    ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return Datos;
            }
        }//fin


        //consulta para buscar al empleado
        public OdbcDataReader FuncBuscarEmpleado(string ID)
        {
            try
            {
                //sentencia para realizar la busqueda obteniendo los nombres de las diferentes entidades e igualando los ID de las diferentes tablas
                string sentencia = "SELECT E.NOMBRE_EMPLEADO, P.NOMBRE_PUESTO, D.nombre_departamento, E.sueldo_empleado, E.estatus_empleado FROM EMPLEADO AS E, DEPARTAMENTO AS D, PUESTO AS P WHERE P.codigo_puesto = E.codigo_puesto AND E.codigo_departamento = D.codigo_departamento AND E.codigo_empleado = '" + ID + "'";

                OdbcCommand Query_BusquedaReclu = new OdbcCommand(sentencia, Con.conexion());
                OdbcDataReader Lector = Query_BusquedaReclu.ExecuteReader();
                return Lector;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al ejecutar SQL: " +
                    System.Environment.NewLine + System.Environment.NewLine +
                    ex.GetType().ToString() + System.Environment.NewLine +
                    ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        //Consulta para ingresar datos en la entidad EMPLEADO
        public void FuncInsertarEmpleado(string Nombres, int codigoP, int codigoD, string sueldo, int estatus)
        {
            try
            {
                int IdEmpleado;
                string CorrelativoReclu = "SELECT IFNULL(MAX(codigo_empleado),0) +1 FROM empleado";
                OdbcCommand QueryIdReclu = new OdbcCommand(CorrelativoReclu, Con.conexion());
                IdEmpleado = Convert.ToInt32(QueryIdReclu.ExecuteScalar());
                OdbcDataReader Ejecucion1 = QueryIdReclu.ExecuteReader();

                //falta firma, fotografia y lugar nacimiento
                //Sentencia para insertar datos a entidad Reclutamiento
                string SentenciaRecluta = "INSERT INTO empleado (codigo_empleado, nombre_empleado, codigo_puesto, " +
                    "codigo_departamento, sueldo_empleado, estatus_empleado) VALUES " + "('" + IdEmpleado + "','" + Nombres + "','" + codigoP + "','" + codigoD + "','"
                    + sueldo + "','" + estatus + "')";


                OdbcCommand Query_IngresoRec = new OdbcCommand(SentenciaRecluta, Con.conexion());

                Query_IngresoRec.ExecuteNonQuery();


            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al ejecutar SQL: " +
                    System.Environment.NewLine + System.Environment.NewLine +
                    ex.GetType().ToString() + System.Environment.NewLine +
                    ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //consulta para modificar en la entidad Reclutamiento
        public void FuncModificarEmpleado(int codigoP, int codigoD, string sueldo, int estatus, string IDE)
        {
            try
            {


                string sentencia = "UPDATE empleado SET codigo_puesto='" + codigoP + "', codigo_departamento='" + codigoD + "', sueldo_empleado='" + sueldo +
                    "', estatus_empleado='" + estatus + "' WHERE codigo_empleado= '" + IDE + "'";


                OdbcCommand Query_Validacion1 = new OdbcCommand(sentencia, Con.conexion());

                Query_Validacion1.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al ejecutar SQL: " +
                    System.Environment.NewLine + System.Environment.NewLine +
                    ex.GetType().ToString() + System.Environment.NewLine +
                    ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }




    }
}
