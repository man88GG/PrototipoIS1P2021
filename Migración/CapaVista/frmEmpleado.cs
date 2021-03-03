using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Odbc;
using CapaControlador;


namespace CapaVista
{
    public partial class frmEmpleado : Form
    {
        ClsControlador Cont = new ClsControlador();

        public frmEmpleado()
        {
            InitializeComponent();
            funcDepto();
            funcPuesto();
            CmbDepto.DropDownStyle = ComboBoxStyle.DropDownList;
            CmbPuesto.DropDownStyle = ComboBoxStyle.DropDownList;
            
        }

        public void funcDepto()
        {
            DataTable Datos = Cont.funcDepto();
            CmbDepto.DataSource = Datos;
            CmbDepto.DisplayMember = "nombre_departamento";
            CmbDepto.ResetText();
        }

        public void funcPuesto()
        {
            DataTable Datos = Cont.funcPuesto();
            CmbPuesto.DataSource = Datos;
            CmbPuesto.DisplayMember = "nombre_puesto";
            CmbPuesto.ResetText();
        }

        string Nombres, IDE,sueldo;
        int estatus, codigoP,codigoD;

        private void rbtnInactivo_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnInactivo.Checked == true)
            {
                estatus = 0;
            }
        }

        private void BtnBuscar_Click(object sender, EventArgs e)
        {
            //Mensaje de Validación
            if (txtIdEmpleado.Text == "") { MessageBox.Show("ADVERTENCIA: El campo de busqueda no puede estar vacío.", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            else
            {
                //se desbloquean los componentes en los que se puede agregar/cambiar información
                BtnIngresar.Enabled = true;
                funcDesBloqueo();

                IDE = txtIdEmpleado.Text;
                String estado;
                //Inicio para Busqueda
                OdbcDataReader Lector = Cont.funcBuscarEmpleado(txtIdEmpleado.Text);
                if (Lector.HasRows == true)
                {
                    while (Lector.Read())
                    {

                        //Se agrega el valor del lector a los textbox dependiendo la posicion 
                        TxtNombre.Text = Lector.GetString(0);
                        CmbPuesto.Text = Lector.GetString(1);
                        CmbDepto.Text = Lector.GetString(2);
                        Txtsueldo.Text = Lector.GetString(3);
                        estado = Lector.GetString(4);

                        if(estado == "1")
                        {
                            rbtnActivo.Checked = true;
                            
                        }
                        else
                          if(estado == "0")
                        {
                            rbtnInactivo.Checked = true;
                           
                        }

                    }
                }
                else
                {
                    //Mensaje de error
                    MessageBox.Show("ERROR: El ID de ese Empleado no se encuentra Registrado.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    funcBloqueo();
                    funcLimpieza();
                }

            }//fin ifelse


        }

        private void rbtnActivo_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnActivo.Checked == true)
            {
                estatus = 1;
            }
        }

        private void lblIDepa_Click(object sender, EventArgs e)
        {

        }

        private void BtnModificar_Click(object sender, EventArgs e)
        {

         
            //segunda verificación de datos de cajas de texto vacias
            if (TxtNombre.Text == "" || Txtsueldo.Text == "" || (rbtnActivo.Checked == false && rbtnInactivo.Checked == false)) { MessageBox.Show("ADVERTENCIA: Uno o más campos están vacíos.", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            else
            {

                //Mensaje de Pregunta
                if (MessageBox.Show("¿Desea agregar modificar este Empleadoe ?", "EMPLEADO", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != System.Windows.Forms.DialogResult.Yes) { }
                else
                {


                    //Se da a las variables los valores correspondientes para enviarse a la capa Controlador
                    //datos Pasaporte
                    
                    codigoP = CmbDepto.SelectedIndex + 1;
                    codigoD = CmbPuesto.SelectedIndex + 1;
                    sueldo = Txtsueldo.Text;


                    Cont.funcModificarEmpleado(codigoP, codigoD, sueldo, estatus, IDE);
                    MessageBox.Show("Se ha ingresado han modificado los datos del Empleado con Éxito", "MODIFICACION EMPLEADO", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    funcLimpieza();
                    funcBloqueo();

                    //envío de datos hacia capa Controlador



                }//fin elseif Pregunta

            }//fin elseif txt


        }

        private void button2_Click(object sender, EventArgs e)
        {

          
            //segunda verificación de datos de cajas de texto vacias
            if (TxtNombre.Text == "" || Txtsueldo.Text == "" || ( rbtnActivo.Checked==false && rbtnInactivo.Checked == false)) { MessageBox.Show("ADVERTENCIA: Uno o más campos están vacíos.", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            else
            {

                //Mensaje de Pregunta
                if (MessageBox.Show("¿Desea agregar un nuevo Empleadoe ?", "EMPLEADO", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != System.Windows.Forms.DialogResult.Yes) { }
                else
                {


                    //Se da a las variables los valores correspondientes para enviarse a la capa Controlador
                    //datos Pasaporte
                    Nombres = TxtNombre.Text;
                    codigoP = CmbDepto.SelectedIndex + 1;
                    codigoD = CmbPuesto.SelectedIndex + 1;
                    sueldo = Txtsueldo.Text;

                 
                    Cont.funcInsertarEmpleado(Nombres, codigoP, codigoD, sueldo, estatus);
                    MessageBox.Show("Se ha ingresado el Pasaporte con Éxito", "INGRESO PASAPORTE", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    funcLimpieza();
                    funcBloqueo();

                    //envío de datos hacia capa Controlador

                }//fin elseif Pregunta

            }//fin elseif txt



        }


        private void funcLimpieza()
        {
           
            txtIdEmpleado.Text = "";
            TxtNombre.Text = "";
            Txtsueldo.Text = "";
            CmbDepto.Text = "";
            CmbPuesto.Text = "";
            rbtnActivo.Checked = false;
            rbtnInactivo.Checked = false;


        }

        //Función de Bloqueo
        private void funcBloqueo()
        {
            
            BtnModificar.Enabled = false;
           

        }

        private void funcDesBloqueo()
        {
            BtnModificar.Enabled = true;

        }

    }
}
