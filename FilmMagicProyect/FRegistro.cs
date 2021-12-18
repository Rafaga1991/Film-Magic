using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FilmMagicProyect.Objetos;

namespace FilmMagicProyect
{
    public partial class FRegistro : Form
    {
        Cliente nuevo;
        public FRegistro()
        {
            InitializeComponent();
        }

        private void iconoCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void limpiarTextBox()
        {
            textBoxB.Clear();
            textBoxC.Clear();
            textBoxCC.Clear();
            textBoxT.Clear();
            textBoxD.Clear();
            textBoxN.Clear();
        }

        private void cargarDataGrid()
        {

            nuevo = new Cliente(); 
            DataSet datos = nuevo.LlenarDataGrid("Cliente"); 
            dataGridView1.DataSource = datos.Tables[0];
        }

        private void button1_Click(object sender, EventArgs e)
        {
            limpiarTextBox();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            nuevo = new Cliente(textBoxC.Text, textBoxN.Text, textBoxD.Text, textBoxT.Text, int.Parse(textBoxCC.Text)); 
            if (nuevo.agregar()) 
            { 
                string mensaje = "Usuario Agregado con exito";
                MessageBox.Show(mensaje);
                dataGridView1.Refresh(); 
                cargarDataGrid(); 
                limpiarTextBox(); 
            } 
            else
                MessageBox.Show("Usuario No Agregado");
        }

        private void textBoxB_TextChanged(object sender, EventArgs e)
        {
            nuevo = new Cliente(); 
            DataSet datos = nuevo.consultar(textBoxB.Text); 
            dataGridView1.DataSource = datos.Tables[0];
        }

        private void FRegistro_Load(object sender, EventArgs e)
        {
            cargarDataGrid();
        }
    }
}
