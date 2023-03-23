using Entidades.Usuarios;
using LogicaNegocio.Usuarios;
using System;
using System.Windows.Forms;

namespace Inventario.Principal
{
    public partial class FrmLogin : Form
    {
        private ClsUsuario ObjUsuario = null;
        private ClsUsuarioLn ObjUsuarioLn = new ClsUsuarioLn();
        public FrmLogin()
        {
            CargarListaUsuarios();
            InitializeComponent();
            
        }

        private void CargarListaUsuarios()
        {
            ObjUsuario = new ClsUsuario();
            ObjUsuarioLn.Index(ref ObjUsuario);
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {

            if (ObjUsuarioLn.Login(tbNombreUsuario.Text, tbPassword.Text))
            {
                var form = new Inicio(new ClsUsuarioLn().createUserObject(tbNombreUsuario.Text, tbPassword.Text));
                form.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Usuario no encontrado");
            }

        }
    }
}
