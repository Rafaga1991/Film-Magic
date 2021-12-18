using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace FilmMagicProyect
{
    public partial class Inicio : Form
    {
        public Inicio()
        {
            InitializeComponent();
        }

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")] 
        private extern static void ReleaseCapture();

        [System.Runtime.InteropServices.DllImport("user32.DLL", EntryPoint = "SendMessage")] 
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (MenuVertical.Width == 250)
            {
                MenuVertical.Width = 64;
                labNombre.Visible = false;
                labCargo.Visible = false;
                pictureBox3.Visible = false;
                panel1.Visible = false;
            }
            else
            {
                MenuVertical.Width = 250;
                labNombre.Visible = true;
                labCargo.Visible = true;
                pictureBox3.Visible = true;
                panel1.Visible = true;
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void iconoMnimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            IconMaximizar.Visible = true;
            iconoMnimizar.Visible = false;
        }

        private void IconMaximizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            IconMaximizar.Visible = false;
            iconoMnimizar.Visible = true;
        }

        private void iconoRestaurar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void BarraTitulo_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
        
        private void abrirFormInPanel(object formHijo)
        {
            if (this.panelContenedor.Controls.Count > 0)
                this.panelContenedor.Controls.RemoveAt(0);
            Form fh = formHijo as Form;
            fh.TopLevel = false;
            fh.Dock = DockStyle.Fill;
            this.panelContenedor.Controls.Add(fh);
            this.panelContenedor.Tag = fh;
            fh.Show();
        }

        private void Inicio_Load(object sender, EventArgs e)
        {
            labNombre.Text = Login.usuario;
            labCargo.Text = Login.cargo;

            abrirFormInPanel(new FPrincipal());
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            abrirFormInPanel(new FPrincipal());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            abrirFormInPanel(new FRenta());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            abrirFormInPanel(new FDevolucion());
        }

        private void button3_Click(object sender, EventArgs e)
        {
            abrirFormInPanel(new FRegistro());
        }

        private void button4_Click(object sender, EventArgs e)
        {
            abrirFormInPanel(new FPremiacion());
        }

        private void panelContenedor_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
