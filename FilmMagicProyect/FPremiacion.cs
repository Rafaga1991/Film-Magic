using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FilmMagicProyect
{
    public partial class FPremiacion : Form
    {
        private DataSet clientes;

        public FPremiacion()
        {
            InitializeComponent();

             clientes = ConexionSql.Ejecutar("select cliente.clienteNombre as nombre , sum(alquiler.alquilerMontoTotal)as montoTotalMesActual from cliente inner join alquiler on cliente.clienteId=alquilerClienteId where month((alquiler.alquilerFechaAlquiler))=month(GETDATE())  group by cliente.clienteNombre having sum(alquiler.alquilerMontoTotal)>100 ;");
            tblClientes.DataSource = clientes.Tables[0];


        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {

            DataSet clientes = null;

            if (radMensual.Checked)
            {

              clientes = ConexionSql.Ejecutar("select cliente.clienteNombre as nombre , sum(alquiler.alquilerMontoTotal)as montoTotalMesActual from cliente inner join alquiler on cliente.clienteId=alquilerClienteId where month((alquiler.alquilerFechaAlquiler))=month(GETDATE())  group by cliente.clienteNombre having sum(alquiler.alquilerMontoTotal)>100 ;");

              
            }
            else
            {
                

            }

         
        }

        private void radAnual_CheckedChanged(object sender, EventArgs e)
        {
            clientes = ConexionSql.Ejecutar("select cliente.clienteNombre as nombre , sum(alquiler.alquilerMontoTotal)as montoTotalAnioActual from cliente inner join alquiler on cliente.clienteId=alquilerClienteId where year((alquiler.alquilerFechaAlquiler))=year(GETDATE()) group by cliente.clienteNombre  having sum(alquiler.alquilerMontoTotal)>300;");
            tblClientes.DataSource = clientes.Tables[0];
        }

        private void radMensual_CheckedChanged(object sender, EventArgs e)
        {
            clientes = ConexionSql.Ejecutar("select cliente.clienteNombre as nombre , sum(alquiler.alquilerMontoTotal)as montoTotalMesActual from cliente inner join alquiler on cliente.clienteId=alquilerClienteId where month((alquiler.alquilerFechaAlquiler))=month(GETDATE())  group by cliente.clienteNombre having sum(alquiler.alquilerMontoTotal)>100;");
            tblClientes.DataSource = clientes.Tables[0];
        }

        private void btnAtras_Click(object sender, EventArgs e)
        {
            this.Dispose();
            new FPrincipal();
        }

        private void iconoCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }


  
    
}
