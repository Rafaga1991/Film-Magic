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
using System.Data.SqlClient;

namespace FilmMagicProyect
{
    public partial class FPrincipal : Form
    {
        Articulo datosInventario; 
        Cliente ObjetoCliente;
        Producto ObjetoProducto;
        public FPrincipal()
        {
            InitializeComponent();
        }

        private void iconoCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FPrincipal_Load(object sender, EventArgs e)
        {
            timer1.Start();

            ObjetoCliente = new Cliente();
            datosInventario = new Articulo();
            int cant = 0;

            #region
            DataSet cantidad = ObjetoCliente.listarClientes();
            labCli.Text = cantidad.Tables[0].Rows.Count.ToString();
            ObjetoProducto = new Producto();

            List<string> categorias = new List<string>(); 
            DataSet listaCategorias = ObjetoProducto.listarCategoria(); 
            DataSet Lista;
            string mensaje = "";
            

            for (int x = 0; x < listaCategorias.Tables[0].Rows.Count; x++) 
            { 
                categorias.Add(listaCategorias.Tables[0].Rows[x]["categoriaCodigo"].ToString()); 
            }

            Lista = datosInventario.buscarEnInventario(categorias[3]); 
            for (int k = 0; k < Lista.Tables[0].Rows.Count; k++) 
            { 
                cant += int.Parse(Lista.Tables[0].Rows[k]["productoCantidad"].ToString()); 
            }
            labD.Text = cant.ToString(); 
            cant = 0;

            Lista = datosInventario.buscarEnInventario(categorias[2]); 
            for (int k = 0; k < Lista.Tables[0].Rows.Count; k++) 
            { 
                cant += int.Parse(Lista.Tables[0].Rows[k]["productoCantidad"].ToString()); 
            }
            labC.Text = cant.ToString(); cant = 0;

            Lista = datosInventario.buscarEnInventario(categorias[11]); 
            for (int k = 0; k < Lista.Tables[0].Rows.Count; k++) 
            { 
                cant += int.Parse(Lista.Tables[0].Rows[k]["productoCantidad"].ToString()); 
            }
            labM.Text = cant.ToString(); cant = 0;

            Lista = datosInventario.buscarEnInventario(categorias[0]); 
            for (int k = 0; k < Lista.Tables[0].Rows.Count; k++) 
            { 
                cant += int.Parse(Lista.Tables[0].Rows[k]["productoCantidad"].ToString()); 
            }
            labA.Text = cant.ToString(); cant = 0;

            Lista = datosInventario.buscarEnInventario(categorias[4]); 
            for (int k = 0; k < Lista.Tables[0].Rows.Count; k++) 
            { 
                cant += int.Parse(Lista.Tables[0].Rows[k]["productoCantidad"].ToString()); 
            }
            labT.Text = cant.ToString(); cant = 0;

            cargarDataGrid();

            for (int i = 0; i < categorias.Count(); i++)
            {
                mensaje += categorias[i];
            }
            #endregion

        }

        private void cargarDataGrid()
        {

            datosInventario = new Articulo(); 
            DataSet datos = datosInventario.listarInventario();

            for (int k = 0; k < datos.Tables[0].Rows.Count; k++) 
            { 
                dataGridViewInventario.Rows.Add(datos.Tables[0].Rows[k]["productoCodigo"], datos.Tables[0].Rows[k]["productoTitulo"], datos.Tables[0].Rows[k]["productoCantidad"], datos.Tables[0].Rows[k]["productoPrecio"]); 
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            labHora.Text = DateTime.Now.ToString("HH:mm:ss"); 
            labFecha.Text = DateTime.Now.ToLongDateString();
        }
    }
}
