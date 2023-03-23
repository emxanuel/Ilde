using Entidades.Usuarios;
using Entidades.Herramientas;
using LogicaNegocio.Usuarios;
using System.Windows.Forms;
using System;
using System.Drawing;

namespace Inventario.Principal
{
    public partial class FrmUsuario : Form
    {
        private ClsUsuario ObjUsuario = null;
        private readonly ClsUsuarioLn ObjUsuarioLn = new ClsUsuarioLn();
        public FrmUsuario()
        {
            InitializeComponent();
            CargarListaUsuarios();
        }

        private void FrmUsuario_Load(object sender, System.EventArgs e)
        {
            cbRol.Items.Add(new ClsOpcionCombo() { Valor = 1, Texto = "Administrador" });
            cbRol.Items.Add(new ClsOpcionCombo() { Valor = 2, Texto = "Empleado" });
            cbRol.DisplayMember = "Texto";
            cbRol.ValueMember = "Valor";
            cbRol.SelectedIndex = 0;

            foreach (DataGridViewColumn cl in dtgvUsuarios.Columns)
            {
                if (cl.Visible == true && cl.Name != "Editar")
                {
                    cbBuscar.Items.Add(new ClsOpcionCombo() { Valor = cl.Name, Texto = cl.HeaderText });
                }
            }
            cbBuscar.DisplayMember = "Texto";
            cbBuscar.ValueMember = "Valor";
            cbBuscar.SelectedIndex = 0;

        }
        private void CargarListaUsuarios()
        {
            ObjUsuario = new ClsUsuario();
            ObjUsuarioLn.Index(ref ObjUsuario);
            if (ObjUsuario.MensajeError == null)
            {
                dtgvUsuarios.DataSource = ObjUsuario.DtResultados;
            }
            else
            {
                MessageBox.Show(ObjUsuario.MensajeError, "Mensaje de error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnGuardar_Click(object sender, System.EventArgs e)
        {

            ObjUsuario = new ClsUsuario()
            {

                NombreCompleto = tbNombreCompleto.Text,
                NombreUsuario = tbUsuario.Text,
                Clave = tbContraseña.Text,
                IdPermisos = Convert.ToInt32(((ClsOpcionCombo)cbRol.SelectedItem).Valor.ToString()),
                Estado = chkEstado.Checked


            };
            ObjUsuarioLn.Create(ref ObjUsuario);

            
            if (ObjUsuario.MensajeError == null)
            {
                if (tbContraseña.Text.Trim() != tbConfContraseña.Text.Trim())
            {
                lblresultado.Text = "Las contraseñas no coinciden";
                lblresultado.ForeColor = Color.Red;
                return;
            }
         
                MessageBox.Show("El ID: " + ObjUsuario.ValorScalar+", fue agregado correctamente");
                CargarListaUsuarios();
            }
            else
            {
                MessageBox.Show(ObjUsuario.MensajeError, "Mensaje de error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                
            }


            

        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            ObjUsuario = new ClsUsuario()
            {
                IdUsuario = Convert.ToByte(lbIdUsuario.Text),
                NombreCompleto = tbNombreCompleto.Text,
                NombreUsuario = tbUsuario.Text,
                Clave = tbContraseña.Text,
                IdPermisos = Convert.ToInt32(((ClsOpcionCombo)cbRol.SelectedItem).Valor.ToString()),
                Estado = chkEstado.Checked
            };

            ObjUsuarioLn.Update(ref ObjUsuario);

            if (ObjUsuario.MensajeError == null)
            {
                MessageBox.Show("El usuario fue actualizado correctamente");
                CargarListaUsuarios();
            }
            else
            {
                MessageBox.Show(ObjUsuario.MensajeError, "Mensaje de error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }


        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {


            ObjUsuario = new ClsUsuario()
            {
                IdUsuario = Convert.ToByte(lbIdUsuario.Text)
            };


            ObjUsuarioLn.Delete(ref ObjUsuario);

            CargarListaUsuarios();
        }


        private void btnBuscar_Click(object sender, EventArgs e)
        {

        }

        private void dtgvUsuarios_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dtgvUsuarios.CurrentCell.ColumnIndex == 0)
                {
                    ObjUsuario = new ClsUsuario()
                    {
                        IdUsuario = Convert.ToByte(dtgvUsuarios.Rows[e.RowIndex].Cells["IdUsuario"].Value.ToString())
                    };

                    lbIdUsuario.Text = ObjUsuario.IdUsuario.ToString();//ObjUsuario.IdUsuario.ToString();

                    ObjUsuarioLn.Read(ref ObjUsuario);
                    tbNombreCompleto.Text = ObjUsuario.NombreCompleto;
                    tbUsuario.Text = ObjUsuario.NombreUsuario;
                    tbContraseña.Text = ObjUsuario.Clave;
                    ((ClsOpcionCombo)cbRol.SelectedItem).Valor = ObjUsuario.IdPermisos;
                    chkEstado.Checked = ObjUsuario.Estado;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
    
}
