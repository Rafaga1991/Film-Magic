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
    public partial class FRenta : Form
    {
        Producto objetoProducto; 
        Cliente objetoCliente; 
        DataSet elementos; 
        Alquiler Orenta; 
        int precio;
        public FRenta()
        {
            InitializeComponent();
        }

        private void iconoCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FRenta_Load(object sender, EventArgs e)
        {
            objetoProducto = new Producto(); 
            objetoCliente = new Cliente(); 
            Orenta = new Alquiler();

            elementos = objetoCliente.listarClientes();

            for (int i = 0; i < elementos.Tables[0].Rows.Count; i++)
            {
                comboID.Items.Add(elementos.Tables[0].Rows[i]["clienteCodigo"].ToString());

            }

            elementos.Clear();
        }

        private void comboC_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboP.Text = null;
            pictureBox1.Refresh();

            objetoProducto = new Producto();

            elementos = objetoProducto.listarProductosRentar(comboC.Text); 
            comboP.Items.Clear();

            for (int i = 0; i < elementos.Tables[0].Rows.Count; i++)
            {
                comboP.Items.Add(elementos.Tables[0].Rows[i]["productoTitulo"].ToString());

            }

            elementos.Clear();
        }

        private void comboP_SelectedIndexChanged(object sender, EventArgs e)
        {
            objetoProducto = new Producto();

            elementos = objetoProducto.listarDetalles(comboP.Text); 
            for (int index = 0; index < elementos.Tables[0].Rows.Count; index++)
            {

                if (comboP.Text == elementos.Tables[0].Rows[index]["productoTitulo"].ToString())
                {

                    pictureBox1.Image = Image.FromFile(elementos.Tables[0].Rows[index]["productoImagen"].ToString());

                    labPrecio.Text = elementos.Tables[0].Rows[index]["productoPrecio"].ToString() + " RD$"; 
                    precio = Convert.ToInt32(elementos.Tables[0].Rows[index]["productoPrecio"]); break;
                }
            }
            elementos.Clear();
        }

        private void pictureBoxBus_Click(object sender, EventArgs e)
        {
            objetoCliente = new Cliente(); 
            string codigo = comboID.Text; 
            DataSet datos = objetoCliente.buscar(float.Parse(codigo));

            textBoxN.Text = datos.Tables[0].Rows[0]["clienteNombre"].ToString();
            textBoxC.Text = datos.Tables[0].Rows[0]["clienteCedula"].ToString();
            textBoxD.Text = datos.Tables[0].Rows[0]["clienteDireccion"].ToString();
            textBoxT.Text = datos.Tables[0].Rows[0]["clienteTelefono"].ToString();
        }

        private void comboTR_SelectedIndexChanged(object sender, EventArgs e)
        {
            string tipo = ""; 
            if (comboTR.Text == "Video Juegos") 
            { 
                tipo = "V"; 
            } 
            else 
                tipo = "P"; 

            elementos = objetoProducto.listarCategoria(tipo); 
            comboC.Items.Clear();

            for (int i = 0; i < elementos.Tables[0].Rows.Count; i++)
            {
                comboC.Items.Add(elementos.Tables[0].Rows[i]["categoriaNombre"].ToString());

            }

            elementos.Clear();
        }

        private void btnRen_Click(object sender, EventArgs e)
        {
            string mensajeSalida = ""; 
            int idAlquiler = Orenta.buscarUltId(); 
            string numeroAlquiler = Orenta.buscarUltNUmero(); 
            int cliId = Orenta.buscarclienteId(comboID.Text);


            var dateTime = DateTime.Now.Date;
            string fechaHoy = dateTime.ToShortDateString();
            string fechaRetorno = dateF.Value.ToString("dd-MM-yyyy");


            try
            {
                mensajeSalida = Orenta.renta('I', idAlquiler, numeroAlquiler, cliId, fechaHoy, fechaRetorno, comboP.Text, int.Parse(textBoxCa.Text), float.Parse(precio.ToString()));

                MessageBox.Show(mensajeSalida);
            }
            catch (Exception ex) 
            { 
                MessageBox.Show(mensajeSalida + " " + ex); 
            }
        }
    }
}
