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


namespace CapaVista
{
    public partial class frmEmpleado : Form
    {
        

        public frmEmpleado()
        {
            InitializeComponent();
            funcLlenarTipoPass();
            CmbDepto.DropDownStyle = ComboBoxStyle.DropDownList;
            CmbPuesto.DropDownStyle = ComboBoxStyle.DropDownList;
            
        }

        public void funcLlenarTipoPass()
        {
           // DataTable Datos = Cont.funcCmbTipoPass();
            //CmbDepto.DataSource = Datos;
            CmbDepto.DisplayMember = "TIPOPASAPORTE";
            CmbDepto.ResetText();
        }


        string Nombres;
        int estatus, sueldo, codigoP,codigoD;

        private void rbtnInactivo_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnInactivo.Checked == true)
            {
                estatus = 0;
            }
        }

        private void BtnBuscar_Click(object sender, EventArgs e)
        {
           



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

        private void button2_Click(object sender, EventArgs e)
        {


            Nombres = TxtNombre.Text;
            codigoP = CmbDepto.SelectedIndex + 1;
            codigoD = CmbPuesto.SelectedIndex + 1;
            sueldo = Convert.ToInt32(Txtsueldo.Text);

            
            //Cont.funcInsertarPasaporte(Nombres, codigoP, codigoD, sueldo, estatus);
            MessageBox.Show("Se ha ingresado el Pasaporte con Éxito", "INGRESO EMPLEADO", MessageBoxButtons.OK, MessageBoxIcon.Information);



        }
    }
}
