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
    public partial class FDevolucion : Form
    {
        Producto objetoJuego; 
        Articulo objetoArticulo; 
        Cliente objetoCliente;  
        DataSet nuevoArticulo; 
        Devolucion ODevolucion;
        public FDevolucion()
        {
            InitializeComponent();
        }

        private void iconoCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBoxBus_Click(object sender, EventArgs e)
        {
            try
            {
                objetoCliente = new Cliente(); 
                string codigo = textBoxID.Text; 
                DataSet datos = objetoCliente.buscar(float.Parse(codigo));

                textBoxN.Text = datos.Tables[0].Rows[0]["clienteNombre"].ToString(); 
                textBoxC.Text = datos.Tables[0].Rows[0]["clienteCedula"].ToString(); 
                textBoxD.Text = datos.Tables[0].Rows[0]["clienteDireccion"].ToString(); 
                textBoxT.Text = datos.Tables[0].Rows[0]["clienteTelefono"].ToString();
                cargarDataGrid(Convert.ToInt32(datos.Tables[0].Rows[0]["clienteId"].ToString()));
            }
            catch (Exception error)
            {
                MessageBox.Show("Usuario Incorrecto o No Existe", "Error"); 

            }

            
        }

        private void FDevolucion_Load(object sender, EventArgs e)
        {
            objetoJuego = new Producto(); 
            objetoArticulo = new Articulo();

            ODevolucion = new Devolucion(); 
            DataSet categoria = objetoJuego.listarCategoria(); 
            DataSet lista; 
            

                lista = objetoArticulo.listarInventario();

                for (int y = 0; y < lista.Tables[0].Rows.Count; y++) 
                { 
                    if (lista.Tables[0].Rows[y]["productoTitulo"].ToString() != " ") 
                    { 
                        comboP.Items.Add(lista.Tables[0].Rows[y]["productoTitulo"].ToString()); 
                    } 
                }
            
        }

        private void comboP_SelectedIndexChanged(object sender, EventArgs e)
        {
            objetoArticulo = new Articulo(); 
            nuevoArticulo = objetoArticulo.buscarArticulo(comboP.Text);
        }

        private void btnAna_Click(object sender, EventArgs e)
        {
            dataGridViewP.Rows.Add(nuevoArticulo.Tables[0].Rows[0]["productoCategoria"].ToString(), nuevoArticulo.Tables[0].Rows[0]["productoTitulo"].ToString(), Convert.ToInt32(textBoxCa.Text));
        }

        private void btnCompl_Click(object sender, EventArgs e)
        {
            int alquilerID = Convert.ToInt32(textBoxAID.Text);
            int productoID = ODevolucion.buscarProductoId(comboP.Text); 
            string mensaje = ODevolucion.devolucionRenta(alquilerID, dateF.Value.ToString("dd-MM-yyyy"), productoID, Convert.ToInt32(textBoxCa.Text));

            MessageBox.Show(mensaje); 
            int valor = ODevolucion.verificarAtraso(Convert.ToDateTime(dateF.Value.ToString()), int.Parse(textBoxID.Text)); 
            if (valor > 0) 
            { 
                MessageBox.Show("Para su siguiente renta tiene una mora de 15 $RD"); 
            } 
            else 
            { 
                MessageBox.Show("Vuelva Pronto"); 
            }
        }

        private void cargarDataGrid(int clienteID)
        {            
            Devolucion buscar = new Devolucion();
            DataSet datos = buscar.LlenarDataGrid(clienteID);
            dataGridView1.DataSource = datos.Tables[0];
        }
    }
}
