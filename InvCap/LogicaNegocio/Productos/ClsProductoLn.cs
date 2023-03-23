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
                NombreSP = "[dbo].[so_Producto_Index]",
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
                NombreTabla = "Producto$",
                NombreSP = "[dbo].[SP_Producto_Create]",
                Scalar = true
            };

            ObjDataBase.DtParametros.Rows.Add(@"@Descripcion", "18", objProducto.Descripcion);
            ObjDataBase.DtParametros.Rows.Add(@"@Categoria", "18", objProducto.Categoria);
            ObjDataBase.DtParametros.Rows.Add(@"@Almacen", "18", objProducto.Almacen);
            ObjDataBase.DtParametros.Rows.Add(@"@PrecioCompra", "10", objProducto.PrecioCompra);
            ObjDataBase.DtParametros.Rows.Add(@"@PrecioVenta", "10", objProducto.PredcioVenta);
            ObjDataBase.DtParametros.Rows.Add(@"@Stock", "10", objProducto.Stock);

            Ejecutar(ref objProducto);
        }

        public void Read(ref ClsProducto objProducto)
        {
            ObjDataBase = new ClsDataBase()
            {
                NombreTabla = "Producto$",
                NombreSP = "[dbo].[sp_Producto_Read]",
                Scalar = false
            };
            ObjDataBase.DtParametros.Rows.Add(@"@IdProducto", "4", objProducto.Id);
            Ejecutar(ref objProducto);
        }

        public void Update(ref ClsProducto objProducto)
        {
            ObjDataBase = new ClsDataBase()
            {
                NombreTabla = "Producto$",
                NombreSP = "[dbo].[sp_Producto_Update]",
                Scalar = true
            };
            ObjDataBase.DtParametros.Rows.Add(@"@Descripcion", "18", objProducto.Descripcion);
            ObjDataBase.DtParametros.Rows.Add(@"@Categoria", "18", objProducto.Categoria);
            ObjDataBase.DtParametros.Rows.Add(@"@Almacen", "18", objProducto.Almacen);
            ObjDataBase.DtParametros.Rows.Add(@"@PrecioCompra", "10", objProducto.PrecioCompra);
            ObjDataBase.DtParametros.Rows.Add(@"@PrecioVenta", "10", objProducto.PredcioVenta);
            ObjDataBase.DtParametros.Rows.Add(@"@Stock", "10", objProducto.Stock);

            Ejecutar(ref objProducto);
        }

        public void Delete(ref ClsProducto objProducto)
        {
            ObjDataBase = new ClsDataBase()
            {
                NombreTabla = "Usuario",
                NombreSP = "[dbo].[sp_Productos_Delete]",
                Scalar = true
            };
            ObjDataBase.DtParametros.Rows.Add(@"@IdProducto", "4", objProducto.Id);
            Ejecutar(ref objProducto);
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
