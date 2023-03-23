using AccesoDatos.DataBase;
using Entidades.Usuarios;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades.Productos;

namespace LogicaNegocio.Productos
{
    public class ClsProductoLn
    {
        #region Variables privadas //Colocar la variable paa conectar a la database
        private ClsDataBase ObjDataBase = null;
        #endregion

        #region Metodo Index //Se utiliza para listar informacion y mostrarla en pantalla
        public void Index(ref ClsProducto objProducto)
        {
            ObjDataBase = new ClsDataBase()
            {
                NombreTabla = "Producto$",
                NombreSP = "[dbo].[sp_Usuarios_Index]",
                Scalar = false
            };
            Ejecutar(ref objProducto);
        }//no retorna nada, recibe por referencia un objeto
        #endregion

        #region CRUD Usuario
        public void Create(ref ClsProducto objProducto)
        {
            ObjDataBase = new ClsDataBase()
            {
                NombreTabla = "Usuario",
                NombreSP = "[dbo].[SP_Usuarios_Create]",
                Scalar = true
            };

            ObjDataBase.DtParametros.Rows.Add(@"@NombreCompleto", "18", objProducto.NombreCompleto);
            ObjDataBase.DtParametros.Rows.Add(@"@NombreUsuario", "18", objProducto.NombreUsuario);
            ObjDataBase.DtParametros.Rows.Add(@"@Clave", "18", objProducto.Clave);
            ObjDataBase.DtParametros.Rows.Add(@"@IdPermisos", "4", objProducto.IdPermisos);
            ObjDataBase.DtParametros.Rows.Add(@"@Estado", "1", objProducto.Estado);

            Ejecutar(ref objProducto);
        }

        public void Read(ref ClsProducto objProducto)
        {
            ObjDataBase = new ClsDataBase()
            {
                NombreTabla = "Usuario",
                NombreSP = "[dbo].[sp_Usuarios_Read]",
                Scalar = false
            };
            ObjDataBase.DtParametros.Rows.Add(@"@IdUsuario", "4", objProducto.IdUsuario);
            Ejecutar(ref objProducto);
        }

        public void Update(ref ClsProducto objProducto)
        {
            ObjDataBase = new ClsDataBase()
            {
                NombreTabla = "Usuario",
                NombreSP = "[dbo].[sp_Usuarios_Update]",
                Scalar = true
            };
            ObjDataBase.DtParametros.Rows.Add(@"@IdUsuario", "4", objProducto.IdUsuario);
            ObjDataBase.DtParametros.Rows.Add(@"@NombreCompleto", "18", objProducto.NombreCompleto);
            ObjDataBase.DtParametros.Rows.Add(@"@NombreUsuario", "18", objProducto.NombreUsuario);
            ObjDataBase.DtParametros.Rows.Add(@"@Clave", "18", objProducto.Clave);
            ObjDataBase.DtParametros.Rows.Add(@"@IdPermisos", "4", objProducto.IdPermisos);
            ObjDataBase.DtParametros.Rows.Add(@"@Estado", "1", objProducto.Estado);

            Ejecutar(ref objProducto);
        }

        public void Delete(ref ClsProducto objProducto)
        {
            ObjDataBase = new ClsDataBase()
            {
                NombreTabla = "Usuario",
                NombreSP = "[dbo].[sp_Usuarios_Delete]",
                Scalar = true
            };
            ObjDataBase.DtParametros.Rows.Add(@"@IdUsuario", "4", objProducto.IdUsuario);
            Ejecutar(ref objProducto);
        }

        public bool Login(string usuario, string clave)
        {
            SqlConnection conn = new SqlConnection("Data Source=DESKTOP-PPTKEH7\\MSSQLSERVER01;Initial Catalog=InventarioIlde;Integrated Security=True");
            conn.Open();
            string query = "select count(*) from usuario where nombreusuario = '" + usuario + "' and clave= '" + clave + "'";
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            int count = int.Parse(reader[0].ToString());
            conn.Close();

            return count == 1;
        }

        public ClsUsuario createUserObject(string usuario, string clave)
        {
            SqlConnection conn = new SqlConnection("Data Source=DESKTOP-PPTKEH7\\MSSQLSERVER01;Initial Catalog=InventarioIlde;Integrated Security=True");
            conn.Open();
            string query = "select NombreCompleto, NombreUsuario, Clave, IdPermisos, Estado from usuario where nombreusuario = '" + usuario + "' and clave= '" + clave + "'";
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            ClsProducto objProducto = new ClsUsuario()
            {
                NombreCompleto = reader[0].ToString(),
                NombreUsuario = reader[1].ToString(),
                Clave = reader[2].ToString(),
                IdPermisos = int.Parse(reader[3].ToString()),
                Estado = Convert.ToBoolean(reader[4])
            };
            conn.Close();

            return objProducto;
        }


        #endregion

        #region Metodos privados
        private void Ejecutar(ref ClsProducto objProducto)
        {
            ObjDataBase.CRUD(ref ObjDataBase);

            if (ObjDataBase.MensajeErrorDB == null)
            {
                if (ObjDataBase.Scalar)
                {
                    objProducto.ValorScalar = ObjDataBase.ValorScalar;
                }
                else
                {
                    objProducto.DtResultados = ObjDataBase.DsResultados.Tables[0];
                    if (objProducto.DtResultados.Rows.Count == 1)
                    {
                        foreach (DataRow item in objProducto.DtResultados.Rows)
                        {
                            objProducto.IdUsuario = Convert.ToInt32(item["IdUsuario"].ToString());
                            objProducto.NombreCompleto = (item["NombreCompleto"].ToString());
                            objProducto.NombreUsuario = (item["NombreUsuario"].ToString());
                            objProducto.Clave = (item["Clave"].ToString());
                            objProducto.IdPermisos = Convert.ToInt32(item["IdPermisos"].ToString());
                            objProducto.Estado = Convert.ToBoolean(item["Estado"].ToString());

                        }
                    }
                }
            }
            else
            {
                objProducto.MensajeError = ObjDataBase.MensajeErrorDB;
            }
        }
    }
}
