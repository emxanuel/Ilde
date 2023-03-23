using Entidades.Usuarios;
using Inventario.Productos;
using System.Windows.Forms;

namespace Inventario.Principal
{
    public partial class Inicio : Form
    {
        public ClsUsuario usuario { get; set; }

        public Inicio(ClsUsuario user)
        {
            InitializeComponent();
            usuario = user;
            labelUsuario.Text = "Usuario: " + usuario.NombreUsuario;
        }

        private void button3_Click(object sender, System.EventArgs e)
        {
            var f = new RegistrarProductos();
            f.Show();
            this.Hide();
        }
    }
}
