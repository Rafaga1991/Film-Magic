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
using System.Runtime.InteropServices;

namespace FilmMagicProyect
{
    public partial class Login : Form
    {
        Empleado objetoEmpleado;
        public static string usuario;
        public static string cargo;
        public Login()
        {
            InitializeComponent();
        }

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        [System.Runtime.InteropServices.DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        private void btnEnter_Click(object sender, EventArgs e)
        {
            objetoEmpleado = new Empleado();
            DataSet datos = objetoEmpleado.logear();
            for (int x = 0; x < datos.Tables[0].Rows.Count; x++)
            {
                if (textUser.Text == datos.Tables[0].Rows[x]["usuarioAvatar"].ToString() && textPass.Text == datos.Tables[0].Rows[x]["usuarioClave"].ToString())
                {
                    usuario = datos.Tables[0].Rows[x]["usuarioNombre"].ToString();
                    cargo = datos.Tables[0].Rows[x]["usuarioAvatar"].ToString(); 
                    this.Hide();
                    Inicio principal = new Inicio();
                    principal.Show();


                }
                else
                {
                    labelMensaje.Visible = true;
                    labelMensaje.Text = "Usuario Incorrecto";
                    textPass.Clear();
                    textUser.Clear();
                }
            }
        }

        private void iconoCerrar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void panel_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
    }
}
