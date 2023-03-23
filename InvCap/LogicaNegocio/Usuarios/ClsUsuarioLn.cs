using AccesoDatos.DataBase;
using Entidades.Usuarios;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Net.Http;
using System.Configuration;
using System.Security.Claims;
using System.Collections.Generic;

namespace LogicaNegocio.Usuarios
{
    public class ClsUsuarioLn
    {
        #region Variables privadas //Colocar la variable paa conectar a la database
        private ClsDataBase ObjDataBase = null;
        #endregion

        #region Metodo Index //Se utiliza para listar informacion y mostrarla en pantalla
        public void Index(ref ClsUsuario ObjUsuario)
        {
            ObjDataBase = new ClsDataBase()
            {
                NombreTabla = "Usuarios",
                NombreSP = "[dbo].[sp_Usuarios_Index]",
                Scalar= false
            };
            Ejecutar(ref ObjUsuario);
        }//no retorna nada, recibe por referencia un objeto
        #endregion

        #region CRUD Usuario
        public void Create(ref ClsUsuario ObjUsuario)
        {
            ObjDataBase = new ClsDataBase()
            {
                NombreTabla = "Usuario",
                NombreSP = "[dbo].[SP_Usuarios_Create]",
                Scalar = true
            };

            ObjDataBase.DtParametros.Rows.Add(@"@NombreCompleto", "18", ObjUsuario.NombreCompleto);
            ObjDataBase.DtParametros.Rows.Add(@"@NombreUsuario", "18", ObjUsuario.NombreUsuario);
            ObjDataBase.DtParametros.Rows.Add(@"@Clave", "18", ObjUsuario.Clave);
            ObjDataBase.DtParametros.Rows.Add(@"@IdPermisos", "4", ObjUsuario.IdPermisos);
            ObjDataBase.DtParametros.Rows.Add(@"@Estado", "1", ObjUsuario.Estado);

            Ejecutar(ref ObjUsuario);
        }

        public void Read(ref ClsUsuario ObjUsuario)
        {
            ObjDataBase = new ClsDataBase()
            {
                NombreTabla = "Usuario",
                NombreSP = "[dbo].[sp_Usuarios_Read]",
                Scalar = false
            };
            ObjDataBase.DtParametros.Rows.Add(@"@IdUsuario", "4", ObjUsuario.IdUsuario);
            Ejecutar(ref ObjUsuario);
        }

        public void Update(ref ClsUsuario ObjUsuario)
        {
            ObjDataBase = new ClsDataBase()
            {
                NombreTabla = "Usuario",
                NombreSP = "[dbo].[sp_Usuarios_Update]",
                Scalar = true
            };
            ObjDataBase.DtParametros.Rows.Add(@"@IdUsuario", "4", ObjUsuario.IdUsuario);
            ObjDataBase.DtParametros.Rows.Add(@"@NombreCompleto", "18", ObjUsuario.NombreCompleto);
            ObjDataBase.DtParametros.Rows.Add(@"@NombreUsuario", "18", ObjUsuario.NombreUsuario);
            ObjDataBase.DtParametros.Rows.Add(@"@Clave", "18", ObjUsuario.Clave);
            ObjDataBase.DtParametros.Rows.Add(@"@IdPermisos", "4", ObjUsuario.IdPermisos);
            ObjDataBase.DtParametros.Rows.Add(@"@Estado", "1", ObjUsuario.Estado);

            Ejecutar(ref ObjUsuario);
        }

        public void Delete(ref ClsUsuario ObjUsuario)
        {
            ObjDataBase = new ClsDataBase()
            {
                NombreTabla = "Usuario",
                NombreSP = "[dbo].[sp_Usuarios_Delete]",
                Scalar = true
            };
            ObjDataBase.DtParametros.Rows.Add(@"@IdUsuario", "4", ObjUsuario.IdUsuario);
            Ejecutar(ref ObjUsuario);
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
            ClsUsuario objUsuario = new ClsUsuario()
            {
                NombreCompleto = reader[0].ToString(),
                NombreUsuario = reader[1].ToString(),
                Clave = reader[2].ToString(),
                IdPermisos = int.Parse(reader[3].ToString()),
                Estado = Convert.ToBoolean(reader[4])
            };
            conn.Close();

            return objUsuario;
        }


        #endregion

        #region Metodos privados
        private void Ejecutar(ref ClsUsuario ObjUsuario)
        {
            ObjDataBase.CRUD(ref ObjDataBase);

            if (ObjDataBase.MensajeErrorDB == null)
            {
                if (ObjDataBase.Scalar)
                {
                    ObjUsuario.ValorScalar = ObjDataBase.ValorScalar; 
                }
                else
                {
                    ObjUsuario.DtResultados = ObjDataBase.DsResultados.Tables[0];
                    if (ObjUsuario.DtResultados.Rows.Count == 1)
                    {
                        foreach (DataRow item in ObjUsuario.DtResultados.Rows)
                        {
                            ObjUsuario.IdUsuario = Convert.ToInt32(item["IdUsuario"].ToString());
                            ObjUsuario.NombreCompleto = (item["NombreCompleto"].ToString());
                            ObjUsuario.NombreUsuario = (item["NombreUsuario"].ToString());
                            ObjUsuario.Clave = (item["Clave"].ToString());
                            ObjUsuario.IdPermisos = Convert.ToInt32(item["IdPermisos"].ToString());
                            ObjUsuario.Estado = Convert.ToBoolean(item["Estado"].ToString());

                        }
                    }
                }
            }
            else
            {
                ObjUsuario.MensajeError = ObjDataBase.MensajeErrorDB;
            }
        }
        #endregion
    }
}
